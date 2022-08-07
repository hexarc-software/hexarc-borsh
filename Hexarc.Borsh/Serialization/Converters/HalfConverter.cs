namespace Hexarc.Borsh.Serialization.Converters;

public sealed class HalfConverter : BorshConverter<Half>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, Half value, BorshSerializerOptions options) =>
        writer.WriteHalf(value);

    /// <inheritdoc />
    public override Half Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadHalf();
}
