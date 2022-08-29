namespace Hexarc.Borsh.Serialization.Converters;

public sealed class NullableConverter<T> : BorshConverter<T?>
    where T : struct
{
    private readonly BorshConverter<T> _underlyingConverter;

    /// <summary>
    /// Creates an instance of the <see cref="NullableConverter{T}"/> class.
    /// </summary>
    /// <param name="underlyingConverter"></param>
    public NullableConverter(BorshConverter<T> underlyingConverter) =>
        this._underlyingConverter = underlyingConverter;

    /// <inheritdoc />
    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options) =>
        writer.WriteNullable(value, this._underlyingConverter, options);

    /// <inheritdoc />
    public override T? Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadNullable(this._underlyingConverter, options);
}
