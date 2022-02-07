namespace Hexarc.Borsh;

public sealed class SByteConverter : BorshConverter<SByte>
{
    public override void Write(IBufferWriter<Byte> writer, SByte value)
    {
        const Int32 valueSizeInBytes = 1;
        var span = writer.GetSpan(valueSizeInBytes);
        span[0] = (Byte)value;
        writer.Advance(valueSizeInBytes);
    }
}