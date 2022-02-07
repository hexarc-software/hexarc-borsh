namespace Hexarc.Borsh.Serialization.Converters;

public sealed class DoubleConverter : BorshConverter<Double>
{
    public override void Write(IBufferWriter<Byte> writer, Double value)
    {
        if (Double.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }

        const Int32 valueSizeInBytes = 8;
        var span = writer.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteDoubleLittleEndian(span, value);
        writer.Advance(valueSizeInBytes);
    }
}
