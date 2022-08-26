namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="UInt32"/> primitives.
/// </summary>
[CLSCompliant(false)]
public sealed class UInt32Converter : BorshConverter<UInt32>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, UInt32 value, BorshSerializerOptions options) =>
        writer.WriteUInt32(value);

    /// <inheritdoc />
    public override UInt32 Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadUInt32();
}
