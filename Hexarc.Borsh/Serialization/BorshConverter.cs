namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverter
{
    internal BorshConverter() { }

    internal abstract void WriteCoreAsObject(
        ArrayBufferWriter<Byte> buffer,
        Object value,
        BorshSerializerOptions options);
}
