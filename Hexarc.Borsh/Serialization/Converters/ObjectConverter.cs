using System.Reflection;
using FastMember;

namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ObjectConverter<T> : BorshConverter<T> where T : notnull
{
    private readonly TypeAccessor _accessor;

    private readonly String[] _orderedProperties;

    private readonly BorshConstructor? _constructor;

    private readonly Dictionary<String, BorshConverter> _converters;

    public ObjectConverter(BorshSerializerOptions options)
    {
        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute<BorshIgnoreAttribute>() is null)
            .Select(BorshObjectProperty.Create)
            .OrderBy(p => p.Order)
            .ToArray();

        this._accessor = TypeAccessor.Create(type);
        this._orderedProperties = Array.ConvertAll(properties, p => p.Name);
        this._converters = Array
            .ConvertAll(properties, p => (p.Name, Converter: p.ComputeConverter(options)))
            .ToDictionary(x => x.Name, x => x.Converter);
        this._constructor = BorshConstructor.FromConstructorInfos(type.GetConstructors());
    }

    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        foreach (var property in this._orderedProperties)
        {
            var converter = this._converters[property];
            converter.WriteCoreAsObject(writer, this._accessor[value, property], options);
        }
    }

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

    private Object[] ReadProperties(ref BorshReader reader, BorshSerializerOptions options)
    {
        var properties = new Object[this._orderedProperties.Length];
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

    public T Invoke<T>(params Object[] arguments) =>
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

internal sealed record BorshObjectProperty(Type Type, String Name, Int32 Order, Boolean IsOptional)
{
    public static BorshObjectProperty Create(PropertyInfo propertyInfo)
    {
        var optionalAttr = propertyInfo.GetCustomAttribute<BorshOptionalAttribute>();
        var orderAttr = propertyInfo.GetCustomAttribute<BorshPropertyOrderAttribute>();

        var type = propertyInfo.PropertyType;
        var name = propertyInfo.Name;
        var order = orderAttr switch
        {
            { Order: var x } => x,
            null => throw new InvalidOperationException("Property must be annotated with the BorshPropertyOrder attribute")
        };
        var isOptional = optionalAttr is not null;

        return new BorshObjectProperty(type, name, order, isOptional);
    }

    public BorshConverter ComputeConverter(BorshSerializerOptions options)
     {
         if (this.IsOptional)
         {
             var baseConverterType = typeof(IndirectOptionConverter<>);
             var concreteConverterType = baseConverterType.MakeGenericType(this.Type);
             return Activator.CreateInstance(concreteConverterType) as BorshConverter ??
                    throw new InvalidOperationException();
         }
         else
         {
             return options.GetConverter(this.Type);
         }
     }
}
