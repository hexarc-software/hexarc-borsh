namespace Hexarc.Borsh.Serialization.Converters;

public sealed class HalfConverter : BorshConverter<Half>
{
    public override void Write(BorshWriter writer, Half value, BorshSerializerOptions options) =>
        writer.WriteHalf(value);

    public override Half Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadHalf();
}
