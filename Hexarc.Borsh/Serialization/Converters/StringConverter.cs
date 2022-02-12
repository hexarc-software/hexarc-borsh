namespace Hexarc.Borsh.Serialization.Converters;

public sealed class StringConverter : BorshConverter<String>
{
    public override void Write(BorshWriter writer, String value, BorshSerializerOptions options) =>
        writer.WriteString(value);

    public override String Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadString();
}
