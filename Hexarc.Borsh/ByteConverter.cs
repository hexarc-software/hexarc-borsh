namespace Hexarc.Borsh;

public sealed class ByteConverter : BorshConverter<Byte>
{
    public override void Write(IBufferWriter<Byte> writer, Byte value)
    {
        const Int32 valueSizeInBytes = 1;
        var span = writer.GetSpan(valueSizeInBytes);
        span[0] = value;
        writer.Advance(valueSizeInBytes);
    }
}