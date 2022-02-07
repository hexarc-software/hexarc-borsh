namespace Hexarc.Borsh.Serialization.Converters;

public sealed class SByteConverter : BorshConverter<SByte>
{
    public override void Write(IBufferWriter<Byte> writer, SByte value, BorshSerializerOptions options)
    {
        const Int32 valueSizeInBytes = 1;
        var span = writer.GetSpan(valueSizeInBytes);
        span[0] = (Byte)value;
        writer.Advance(valueSizeInBytes);
    }
}
