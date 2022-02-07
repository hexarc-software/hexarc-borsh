namespace Hexarc.Borsh.Serialization.Converters;

public sealed class BooleanConverter : BorshConverter<Boolean>
{
    public override void Write(IBufferWriter<Byte> writer, Boolean value, BorshSerializerOptions options)
    {
        const Int32 valueSizeInBytes = 1;
        var span = writer.GetSpan(valueSizeInBytes);
        span[0] = (Byte)(value ? 1 : 0);
        writer.Advance(valueSizeInBytes);
    }
}
