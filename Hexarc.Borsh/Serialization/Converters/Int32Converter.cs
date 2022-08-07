namespace Hexarc.Borsh.Serialization.Converters;

public sealed class Int32Converter : BorshConverter<Int32>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, Int32 value, BorshSerializerOptions options) =>
        writer.WriteInt32(value);

    /// <inheritdoc />
    public override Int32 Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadInt32();
}
