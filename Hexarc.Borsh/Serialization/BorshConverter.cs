namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverter
{
    internal BorshConverter() { }

    internal abstract Type TypeToConvert { get; }

    public abstract Boolean CanConvert(Type type);

    internal abstract void WriteCoreAsObject(
        BorshWriter writer,
        Object value,
        BorshSerializerOptions options);
}
