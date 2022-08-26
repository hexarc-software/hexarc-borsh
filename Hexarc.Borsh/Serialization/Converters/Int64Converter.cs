namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="Int64"/> primitives.
/// </summary>
public sealed class Int64Converter : BorshConverter<Int64>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, Int64 value, BorshSerializerOptions options) =>
        writer.WriteInt64(value);

    /// <inheritdoc />
    public override Int64 Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadInt64();
}
