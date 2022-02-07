namespace Hexarc.Borsh.Serialization.Converters;

public sealed class BooleanConverter : BorshConverter<Boolean>
{
    public override void Write(BorshWriter writer, Boolean value, BorshSerializerOptions options) =>
        writer.WriteBoolean(value);
}
