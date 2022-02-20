using System.Reflection;
using FastMember;

namespace Hexarc.Borsh.Serialization.Converters;

public class ObjectConverter<T> : BorshConverter<T> where T : notnull
{
    private readonly TypeAccessor _accessor;

    private readonly Dictionary<String, BorshConverter> _converters;

    public ObjectConverter(BorshSerializerOptions options)
    {
        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute<BorshIgnoreAttribute>() is null)
            .Select(BorshObjectProperty.Create)
            .ToArray();

        this._accessor = TypeAccessor.Create(type);
        this._converters = Array
            .ConvertAll(properties, p => (p.PropertyInfo.Name, Converter: p.ComputeConverter(options)))
            .ToDictionary(x => x.Name, x => x.Converter);
    }

    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        foreach (var (name, converter) in this._converters)
        {
            converter.WriteCoreAsObject(writer, this._accessor[value, name], options);
        }
    }

    public override T Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var value = Activator.CreateInstance<T>();
        foreach (var (name, converter) in this._converters)
        {
            this._accessor[value, name] = converter.ReadCoreAsObject(ref reader, options);
        }

        return value;
    }

    private sealed record BorshObjectProperty(PropertyInfo PropertyInfo, Boolean IsOptional)
    {
        public static BorshObjectProperty Create(PropertyInfo propertyInfo)
        {
            var isOptional = propertyInfo.GetCustomAttribute<BorshOptionalAttribute>() is not null;
            return new BorshObjectProperty(propertyInfo, isOptional);
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
