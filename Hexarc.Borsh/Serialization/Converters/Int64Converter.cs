namespace Hexarc.Borsh.Serialization.Converters;

public sealed class Int64Converter : BorshConverter<Int64>
{
    public override void Write(IBufferWriter<Byte> writer, Int64 value)
    {
        const Int32 valueSizeInBytes = 8;
        var span = writer.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteInt64LittleEndian(span, value);
        writer.Advance(valueSizeInBytes);
    }
}
