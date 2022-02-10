namespace Hexarc.Borsh.Serialization.Converters;

public sealed class EnumConverterFactory : BorshConverterFactory
{
    public override Boolean CanConvert(Type type) => type.IsEnum;
    internal override Object? ReadCoreAsObject(ref BorshReader reader, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options) =>
        (BorshConverter)Activator.CreateInstance(typeof(EnumConverter<>).MakeGenericType(type))!;
}
