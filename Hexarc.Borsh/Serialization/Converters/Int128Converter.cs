namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="Int128"/> primitives.
/// </summary>
public sealed class Int128Converter : BorshConverter<Int128>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, Int128 value, BorshSerializerOptions options) =>
        writer.WriteInt128(value);

    /// <inheritdoc />
    public override Int128 Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadInt128();
}
