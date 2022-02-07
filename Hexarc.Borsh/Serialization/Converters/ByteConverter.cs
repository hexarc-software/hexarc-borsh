namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ByteConverter : BorshConverter<Byte>
{
    public override void Write(BorshWriter writer, Byte value, BorshSerializerOptions options) =>
        writer.WriteByte(value);
}
