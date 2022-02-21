namespace Hexarc.Borsh.Serialization.Converters;

public sealed class DateTimeConverter : BorshConverter<DateTime>
{
    public override void Write(BorshWriter writer, DateTime value, BorshSerializerOptions options) =>
        writer.WriteDateTime(value);

    public override DateTime Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadDateTime();
}
