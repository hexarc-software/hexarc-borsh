namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="SByte"/> primitives.
/// </summary>
[CLSCompliant(false)]
public sealed class SByteConverter : BorshConverter<SByte>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, SByte value, BorshSerializerOptions options) =>
        writer.WriteSByte(value);

    /// <inheritdoc />
    public override SByte Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadSByte();
}
