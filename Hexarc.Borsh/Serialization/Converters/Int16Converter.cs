namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="Int16"/> primitives.
/// </summary>
public sealed class Int16Converter : BorshConverter<Int16>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, Int16 value, BorshSerializerOptions options) =>
        writer.WriteInt16(value);

    /// <inheritdoc />
    public override Int16 Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadInt16();
}
