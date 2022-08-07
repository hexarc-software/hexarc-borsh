namespace Hexarc.Borsh.Serialization.Converters;

public sealed class SingleConverter : BorshConverter<Single>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, Single value, BorshSerializerOptions options) =>
        writer.WriteSingle(value);

    /// <inheritdoc />
    public override Single Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadSingle();
}
