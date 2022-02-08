namespace Hexarc.Borsh.Serialization.Converters;

public sealed class EnumConverterFactory : BorshConverterFactory
{
    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options) =>
        (BorshConverter)Activator.CreateInstance(typeof(EnumConverter<>).MakeGenericType(type))!;
}
