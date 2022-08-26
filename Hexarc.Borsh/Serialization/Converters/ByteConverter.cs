namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="Byte"/> primitives.
/// </summary>
public sealed class ByteConverter : BorshConverter<Byte>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, Byte value, BorshSerializerOptions options) =>
        writer.WriteByte(value);

    /// <inheritdoc />
    public override Byte Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadByte();
}
