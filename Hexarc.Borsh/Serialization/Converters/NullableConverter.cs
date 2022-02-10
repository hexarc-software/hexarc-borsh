namespace Hexarc.Borsh.Serialization.Converters;

public sealed class NullableConverter<T> : BorshConverter<T?>
    where T : struct
{
    private readonly BorshConverter<T> UnderlyingConverter;

    public NullableConverter(BorshConverter<T> underlyingConverter) =>
        this.UnderlyingConverter = underlyingConverter;

    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options) =>
        writer.WriteOption(value, this.UnderlyingConverter, options);
}
