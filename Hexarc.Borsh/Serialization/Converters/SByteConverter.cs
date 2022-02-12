namespace Hexarc.Borsh.Serialization.Converters;

public sealed class SByteConverter : BorshConverter<SByte>
{
    public override void Write(BorshWriter writer, SByte value, BorshSerializerOptions options) =>
        writer.WriteSByte(value);

    public override SByte Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadSByte();
}
