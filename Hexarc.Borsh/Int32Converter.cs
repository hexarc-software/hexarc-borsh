using System.Buffers;
using System.Buffers.Binary;

namespace Hexarc.Borsh;

public sealed class Int32Converter : BorshConverter<Int32>
{
    public override void Write(IBufferWriter<Byte> writer, Int32 value)
    {
        const Int32 valueSizeInBytes = 4;
        var span = writer.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteInt32LittleEndian(span, value);
        writer.Advance(valueSizeInBytes);
    }
}
