namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverter<T>
{
    public abstract void Write(IBufferWriter<Byte> writer, T value);
}
