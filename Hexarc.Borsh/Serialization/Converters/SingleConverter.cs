namespace Hexarc.Borsh.Serialization.Converters;

public sealed class SingleConverter : BorshConverter<Single>
{
    public override void Write(BorshWriter writer, Single value, BorshSerializerOptions options) =>
        writer.WriteSingle(value);

    public override Single Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadSingle();
}
