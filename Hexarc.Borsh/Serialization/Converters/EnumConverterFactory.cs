namespace Hexarc.Borsh.Serialization.Converters;

public sealed class EnumConverterFactory : BorshConverterFactory
{
    private static readonly Type s_enumConverterType = typeof(EnumConverter<>);

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) => type.IsEnum;

    /// <inheritdoc />
    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var converterType = s_enumConverterType.MakeGenericType(type);
        return Activator.CreateInstance(converterType) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
