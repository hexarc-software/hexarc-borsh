using System.Reflection;
using FastMember;

namespace Hexarc.Borsh.Serialization.Converters;

public class ObjectDefaultConverter<T> : BorshObjectConverter<T>
{
    private readonly Action<BorshWriter, T>[] PropertyWriters;

    public ObjectDefaultConverter(BorshSerializerOptions options) =>
        this.PropertyWriters = this.CreatePropertyWriters(options);

    private Action<BorshWriter, T>[] CreatePropertyWriters(BorshSerializerOptions options)
    {
        var type = typeof(T);
        var accessor = TypeAccessor.Create(type);
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        return Array.ConvertAll(properties, property => this.CreatePropertyWriter(accessor, property, options));
    }

    private Action<BorshWriter, T> CreatePropertyWriter(
        TypeAccessor accessor,
        PropertyInfo propertyInfo,
        BorshSerializerOptions options)
    {
        var type = propertyInfo.PropertyType;
        var name = propertyInfo.Name;
        var converter = options.GetConverter(type);
        return (writer, value) => converter.WriteCoreAsObject(writer, accessor[value, name], options);
    }

    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        foreach (var propertyWriter in this.PropertyWriters)
        {
            propertyWriter(writer, value);
        }
    }
}
