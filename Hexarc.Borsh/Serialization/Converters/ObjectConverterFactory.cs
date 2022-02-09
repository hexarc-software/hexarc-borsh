namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ObjectConverterFactory : BorshConverterFactory
{
    public override Boolean CanConvert(Type type) => true;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
