namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverterFactory : BorshConverter
{
    public abstract BorshConverter? CreateConverter(Type type, BorshSerializerOptions options);

    internal override Type TypeToConvert => null!;

    internal override void WriteCoreAsObject(BorshWriter writer, Object value, BorshSerializerOptions options) =>
        throw new InvalidOperationException();
}
