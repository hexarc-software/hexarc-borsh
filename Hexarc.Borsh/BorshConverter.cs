using System.Buffers;

namespace Hexarc.Borsh;

public abstract class BorshConverter<T>
{
    public abstract void Write(IBufferWriter<Byte> writer, T value);
}
