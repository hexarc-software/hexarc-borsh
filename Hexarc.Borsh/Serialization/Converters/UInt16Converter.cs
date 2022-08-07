namespace Hexarc.Borsh.Serialization.Converters;

public sealed class UInt16Converter : BorshConverter<UInt16>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, UInt16 value, BorshSerializerOptions options) =>
        writer.WriteUInt16(value);

    /// <inheritdoc />
    public override UInt16 Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadUInt16();
}
