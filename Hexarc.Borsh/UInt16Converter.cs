using System.Buffers;
using System.Buffers.Binary;

namespace Hexarc.Borsh;

public sealed class UInt16Converter : BorshConverter<UInt16>
{
    public override void Write(IBufferWriter<Byte> writer, UInt16 value)
    {
        const Int32 valueSizeInBytes = 2;
        var span = writer.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteUInt16LittleEndian(span, value);
        writer.Advance(valueSizeInBytes);
    }
}