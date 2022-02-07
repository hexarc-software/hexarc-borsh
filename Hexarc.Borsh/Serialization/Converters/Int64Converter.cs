namespace Hexarc.Borsh.Serialization.Converters;

public sealed class Int64Converter : BorshConverter<Int64>
{
    public override void Write(BorshWriter writer, Int64 value, BorshSerializerOptions options) =>
        writer.WriteInt64(value);
}
