namespace Hexarc.Borsh.Serialization.Converters;

public sealed class NullableConverter<T> : BorshConverter<T?>
    where T : struct
{
    private readonly BorshConverter<T> UnderlyingConverter;

    public NullableConverter(BorshConverter<T> underlyingConverter) =>
        this.UnderlyingConverter = underlyingConverter;

    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options) =>
        writer.WriteNullable(value, this.UnderlyingConverter, options);

    public override T? Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadNullable(options.GetConverter<T>(), options);
}
