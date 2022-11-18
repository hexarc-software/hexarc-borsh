namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="UInt128"/> primitives.
/// </summary>
[CLSCompliant(false)]
public sealed class UInt128Converter : BorshConverter<UInt128>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, UInt128 value, BorshSerializerOptions options) =>
        writer.WriteUInt128(value);

    /// <inheritdoc />
    public override UInt128 Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadUInt128();
}
