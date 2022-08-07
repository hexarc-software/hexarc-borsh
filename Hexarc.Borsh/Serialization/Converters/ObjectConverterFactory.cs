using System.Reflection;

namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ObjectConverterFactory : BorshConverterFactory
{
    private static readonly Type s_objectConverterType = typeof(ObjectConverter<>);

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) =>
        type.GetCustomAttribute<BorshObjectAttribute>() is not null;

    /// <inheritdoc />
    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var converterType = s_objectConverterType.MakeGenericType(type);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
