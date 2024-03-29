namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="Double"/> primitives.
/// </summary>
public sealed class DoubleConverter : BorshConverter<Double>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, Double value, BorshSerializerOptions options) =>
        writer.WriteDouble(value);

    /// <inheritdoc />
    public override Double Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadDouble();
}
