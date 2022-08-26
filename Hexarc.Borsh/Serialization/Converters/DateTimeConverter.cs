namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="DateTime"/> objects.
/// </summary>
public sealed class DateTimeConverter : BorshConverter<DateTime>
{
    /// <inheritdoc />
    public override void Write(BorshWriter writer, DateTime value, BorshSerializerOptions options) =>
        writer.WriteDateTime(value);

    /// <inheritdoc />
    public override DateTime Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadDateTime();
}
