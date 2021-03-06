namespace Hexarc.Borsh.Serialization.Converters;

public sealed class NullableConverter<T> : BorshConverter<T?>
    where T : struct
{
    private readonly BorshConverter<T> _underlyingConverter;

    public NullableConverter(BorshConverter<T> underlyingConverter) =>
        this._underlyingConverter = underlyingConverter;

    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options) =>
        writer.WriteNullable(value, this._underlyingConverter, options);

    public override T? Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadNullable(this._underlyingConverter, options);
}
