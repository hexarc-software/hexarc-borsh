using System.Reflection;
using FastMember;

namespace Hexarc.Borsh.Serialization.Converters;

public class ObjectConverter<T> : BorshConverter<T> where T : notnull
{
    private readonly TypeAccessor _accessor;

    private readonly String[] _orderedProperties;

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
        this._orderedProperties = Array.ConvertAll(properties, p => p.PropertyInfo.Name);
        this._converters = Array
            .ConvertAll(properties, p => (p.PropertyInfo.Name, Converter: p.ComputeConverter(options)))
            .ToDictionary(x => x.Name, x => x.Converter);
    }

    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        foreach (var property in this._orderedProperties)
        {
            var converter = this._converters[property];
            converter.WriteCoreAsObject(writer, this._accessor[value, property], options);
        }
    }

    public override T Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var value = Activator.CreateInstance<T>();
        foreach (var property in this._orderedProperties)
        {
            var converter = this._converters[property];
            this._accessor[value, property] = converter.ReadCoreAsObject(ref reader, options);
        }

        return value;
    }

    private sealed record BorshObjectProperty(PropertyInfo PropertyInfo, Int32 Order, Boolean IsOptional)
    {
        public static BorshObjectProperty Create(PropertyInfo propertyInfo)
        {
            var isOptional = propertyInfo.GetCustomAttribute<BorshOptionalAttribute>() is not null;
            var orderAttribute = propertyInfo.GetCustomAttribute<BorshOrderAttribute>();

            if (orderAttribute is null)
            {
                throw new InvalidOperationException("Property must be annotated with the BorshOrder attribute");
            }

            return new BorshObjectProperty(propertyInfo, orderAttribute.Order, isOptional);
        }

        public BorshConverter ComputeConverter(BorshSerializerOptions options)
        {
            if (this.IsOptional)
            {
                var baseConverterType = typeof(IndirectOptionConverter<>);
                var concreteConverterType = baseConverterType.MakeGenericType(this.PropertyInfo.PropertyType);
                return Activator.CreateInstance(concreteConverterType) as BorshConverter ??
                       throw new InvalidOperationException();
            }
            else
            {
                return options.GetConverter(this.PropertyInfo.PropertyType);
            }
        }
    }
}
