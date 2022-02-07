namespace Hexarc.Borsh.Serialization.Converters;

public sealed class Int32Converter : BorshConverter<Int32>
{
    public override void Write(BorshWriter writer, Int32 value, BorshSerializerOptions options) =>
        writer.WriteInt32(value);
}
