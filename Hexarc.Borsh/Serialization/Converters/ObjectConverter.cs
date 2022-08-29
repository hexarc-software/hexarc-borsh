using System.Reflection;

namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for user defined types like classes or structures.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ObjectConverter<T> : BorshConverter<T> where T : notnull
{
    private readonly TypeAccessor _accessor;

    private readonly String[] _orderedProperties;

    private readonly BorshConstructor? _constructor;

    private readonly Dictionary<String, BorshConverter> _converters;

    /// <summary>
    /// Creates an instance of the <see cref="ObjectConverter{T}"/> class.
    /// </summary>
    /// <param name="options">The serialization options.</param>
    public ObjectConverter(BorshSerializerOptions options)
    {
        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute<BorshIgnoreAttribute>() is null)
            .Select(BorshObjectProperty.Create)
            .OrderBy(p => p.Order)
            .ToArray();

        this._accessor = new TypeAccessor(type);
        this._orderedProperties = Array.ConvertAll(properties, p => p.Name);
        this._converters = Array
            .ConvertAll(properties, p => (p.Name, Converter: p.ComputeConverter(options)))
            .ToDictionary(x => x.Name, x => x.Converter);
        this._constructor = BorshConstructor.FromConstructorInfos(type.GetConstructors());
    }

    /// <inheritdoc />
    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        foreach (var property in this._orderedProperties)
        {
            var converter = this._converters[property];
            converter.WriteCoreAsObject(writer, this._accessor[value, property]!, options);
        }
    }

    /// <inheritdoc />
    public override T Read(ref BorshReader reader, BorshSerializerOptions options) =>
        this._constructor is null
            ? this.ReadViaProperties(ref reader, options)
            : this.ReadViaConstructor(ref reader, options);

    private T ReadViaProperties(ref BorshReader reader, BorshSerializerOptions options)
    {
        var value = Activator.CreateInstance<T>();
        foreach (var property in this._orderedProperties)
        {
            var converter = this._converters[property];
            this._accessor[value, property] = converter.ReadCoreAsObject(ref reader, options);
        }

        return value;
    }

    private T ReadViaConstructor(ref BorshReader reader, BorshSerializerOptions options)
    {
        var properties = this.ReadProperties(ref reader, options);
        return this._constructor!.Invoke<T>(properties);
    }

    private Object?[] ReadProperties(ref BorshReader reader, BorshSerializerOptions options)
    {
        var properties = new Object?[this._orderedProperties.Length];
        foreach (var property in this._orderedProperties)
        {
            var converter = this._converters[property];
            var value = converter.ReadCoreAsObject(ref reader, options);
            var index = this._constructor![property];
            properties[index] = value;
        }

        return properties.ToArray();
    }
}

internal sealed record BorshConstructor(ConstructorInfo ConstructorInfo, String[] Parameters)
{
    public Int32 this[String name] =>
        Array.FindIndex(this.Parameters, x => x.Equals(name, StringComparison.OrdinalIgnoreCase));

    public T Invoke<T>(params Object?[] arguments) =>
        (T)this.ConstructorInfo.Invoke(arguments);

    public static BorshConstructor? FromConstructorInfos(ConstructorInfo[] constructorInfos)
    {
        var constructorInfo = constructorInfos.Length == 1 ?
            constructorInfos[0] :
            constructorInfos.SingleOrDefault(x => x.GetCustomAttribute<BorshConstructorAttribute>() is not null);
        return constructorInfo is null ? default : FromConstructorInfo(constructorInfo);
    }

    private static BorshConstructor? FromConstructorInfo(ConstructorInfo constructorInfo)
    {
        var parameters = Array.ConvertAll(constructorInfo.GetParameters(), x => x.Name!);
        return parameters.Any() ? new BorshConstructor(constructorInfo, parameters) : default;
    }
}

internal sealed record BorshObjectProperty(Type Type, String Name, Int32 Order, Boolean IsOptional, Int32? ArrayLength)
{
    private static readonly Type s_indirectOptionConverterType = typeof(IndirectOptionConverter<>);

    private static readonly Type s_indirectFixedArrayConverterType = typeof(IndirectFixedArrayConverter<>); 
    
    public static BorshObjectProperty Create(PropertyInfo propertyInfo)
    {
        var optionalAttr = propertyInfo.GetCustomAttribute<BorshOptionalAttribute>();
        var orderAttr = propertyInfo.GetCustomAttribute<BorshPropertyOrderAttribute>();
        var fixedArrayAttr = propertyInfo.GetCustomAttribute<BorshFixedArrayAttribute>();

        var type = propertyInfo.PropertyType;
        var name = propertyInfo.Name;
        var order = orderAttr switch
        {
            { Order: var x } => x,
            null => throw new InvalidOperationException("Property must be annotated with the BorshPropertyOrder attribute")
        };
        var isOptional = optionalAttr is not null;
        var arrayLength = fixedArrayAttr?.Length;

        return new BorshObjectProperty(type, name, order, isOptional, arrayLength);
    }

    public BorshConverter ComputeConverter(BorshSerializerOptions options)
     {
         var fixedArrayConverter = this.ComputeFixedArrayConverter(options);
         
         if (this.IsOptional)
         {
             var concreteConverterType = s_indirectOptionConverterType.MakeGenericType(this.Type);
             return Activator.CreateInstance(concreteConverterType, options, fixedArrayConverter) as BorshConverter ??
                    throw new InvalidOperationException();
         }
         else
         {
             return fixedArrayConverter ?? options.GetConverter(this.Type);
         }
     }

    private BorshConverter? ComputeFixedArrayConverter(BorshSerializerOptions options)
    {
        if (this.ArrayLength is null) return default;

        var itemType = this.Type.GetElementType() ?? throw new InvalidOperationException("Property is not an array");
        var indirectFixedArrayConverterType = s_indirectFixedArrayConverterType.MakeGenericType(itemType);
        return Activator.CreateInstance(indirectFixedArrayConverterType, options, this.ArrayLength.Value) as BorshConverter;
    }
}
