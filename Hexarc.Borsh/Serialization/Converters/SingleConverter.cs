namespace Hexarc.Borsh.Serialization.Converters;

public sealed class SingleConverter : BorshConverter<Single>
{
    public override void Write(IBufferWriter<Byte> writer, Single value, BorshSerializerOptions options)
    {
        if (Single.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }

        const Int32 valueSizeInBytes = 4;
        var span = writer.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteSingleLittleEndian(span, value);
        writer.Advance(valueSizeInBytes);
    }
}
