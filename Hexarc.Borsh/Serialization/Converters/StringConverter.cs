namespace Hexarc.Borsh.Serialization.Converters;

public sealed class StringConverter : BorshConverter<String>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, String value, BorshSerializerOptions options) =>
        writer.WriteString(value);

    /// <inheritdoc />
    public override String Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadString();
}
