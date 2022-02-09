namespace Hexarc.Borsh.Serialization.Converters;

public sealed class OptionConverterFactory : BorshConverterFactory
{
    public override Boolean CanConvert(Type type)
    {
        throw new NotImplementedException();
    }

    public override BorshConverter? CreateConverter(Type type, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
