namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverter<T> : BorshConverter
{
    public abstract void Write(IBufferWriter<Byte> writer, T value, BorshSerializerOptions options);

    internal void WriteCore(
        IBufferWriter<Byte> writer,
        T value,
        BorshSerializerOptions options
    ) => this.Write(writer, value, options);

    internal sealed override void WriteCoreAsObject(
        ArrayBufferWriter<Byte> writer,
        Object value,
        BorshSerializerOptions options
    ) => this.WriteCore(writer, (T)value, options);
}
