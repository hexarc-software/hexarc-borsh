namespace Hexarc.Borsh.Serialization.Converters;

public sealed class SByteConverter : BorshConverter<SByte>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, SByte value, BorshSerializerOptions options) =>
        writer.WriteSByte(value);

    /// <inheritdoc />
    public override SByte Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadSByte();
}
