namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverter
{
    internal BorshConverter() { }

    internal abstract void WriteCoreAsObject(
        BorshWriter writer,
        Object value,
        BorshSerializerOptions options);
}
