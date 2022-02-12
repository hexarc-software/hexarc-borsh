namespace Hexarc.Borsh.Serialization.Converters;

public sealed class DoubleConverter : BorshConverter<Double>
{
    public override void Write(BorshWriter writer, Double value, BorshSerializerOptions options) =>
        writer.WriteDouble(value);

    public override Double Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadDouble();
}
