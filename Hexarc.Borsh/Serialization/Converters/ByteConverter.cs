namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ByteConverter : BorshConverter<Byte>
{
    public override void Write(IBufferWriter<Byte> writer, Byte value, BorshSerializerOptions options)
    {
        const Int32 valueSizeInBytes = 1;
        var span = writer.GetSpan(valueSizeInBytes);
        span[0] = value;
        writer.Advance(valueSizeInBytes);
    }
}
