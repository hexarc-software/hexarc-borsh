using System.Buffers;
using System.Buffers.Binary;
using System.Text;

namespace Hexarc.Borsh;

public sealed class StringConverter : BorshConverter<String>
{
    public override void Write(IBufferWriter<Byte> writer, String value)
    {
        const Int32 sizeByteCount = sizeof(Int32);
        var valueByteCount = Encoding.UTF8.GetByteCount(value);
        var neededByteCount = sizeByteCount + valueByteCount;
        var span = writer.GetSpan(neededByteCount);

        BinaryPrimitives.WriteInt32LittleEndian(span[..sizeByteCount], valueByteCount);
        Encoding.UTF8.GetBytes(value, span[sizeByteCount..]);
        writer.Advance(neededByteCount);
    }
}
