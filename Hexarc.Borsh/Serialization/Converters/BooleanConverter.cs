namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="Boolean"/> primitives.
/// </summary>
public sealed class BooleanConverter : BorshConverter<Boolean>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, Boolean value, BorshSerializerOptions options) =>
        writer.WriteBoolean(value);

    /// <inheritdoc />
    public override Boolean Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadBoolean();
}
