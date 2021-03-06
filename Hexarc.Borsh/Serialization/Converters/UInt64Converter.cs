namespace Hexarc.Borsh.Serialization.Converters;

public sealed class UInt64Converter : BorshConverter<UInt64>
{
    public override void Write(BorshWriter writer, UInt64 value, BorshSerializerOptions options) =>
        writer.WriteUInt64(value);

    public override UInt64 Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadUInt64();
}
