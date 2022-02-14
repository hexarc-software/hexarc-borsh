using System.Reflection;
using FastMember;

namespace Hexarc.Borsh.Serialization.Converters;

public class ObjectDefaultConverter<T> : BorshConverter<T> where T : notnull
{
    private readonly TypeAccessor Accessor;

    private readonly Dictionary<String, BorshConverter> Converters;

    public ObjectDefaultConverter(BorshSerializerOptions options)
    {
        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

        this.Accessor = TypeAccessor.Create(type);
        this.Converters = Array
            .ConvertAll(properties, p => (p.Name, Converter: options.GetConverter(p.PropertyType)))
            .ToDictionary(x => x.Name, x => x.Converter);
    }

    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        foreach (var (name, converter) in this.Converters)
        {
            converter.WriteCoreAsObject(writer, this.Accessor[value, name], options);
        }
    }

    public override T Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var value = Activator.CreateInstance<T>();
        foreach (var (name, converter) in this.Converters)
        {
            this.Accessor[value, name] = converter.ReadCoreAsObject(ref reader, options);
        }

        return value;
    }
}
