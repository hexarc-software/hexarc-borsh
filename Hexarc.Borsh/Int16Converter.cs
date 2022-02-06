using System.Buffers;
using System.Buffers.Binary;

namespace Hexarc.Borsh;

public sealed class Int16Converter : BorshConverter<Int16>
{
    public override void Write(IBufferWriter<Byte> writer, Int16 value)
    {
        const Int32 valueSizeInBytes = 2;
        var span = writer.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteInt16LittleEndian(span, value);
        writer.Advance(valueSizeInBytes);
    }
}
