namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="Nullable{T}"/> objects.
/// </summary>
/// <typeparam name="T">The underlying type of serializable nullable objects.</typeparam>
public sealed class NullableConverter<T> : BorshConverter<T?>
    where T : struct
{
    private readonly BorshConverter<T> _underlyingConverter;

    /// <summary>
    /// Creates an instance of the <see cref="NullableConverter{T}"/> class.
    /// </summary>
    /// <param name="underlyingConverter">The converter for the underlying type.</param>
    public NullableConverter(BorshConverter<T> underlyingConverter) =>
        this._underlyingConverter = underlyingConverter;

    /// <inheritdoc />
    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options) =>
        writer.WriteNullable(value, this._underlyingConverter, options);

    /// <inheritdoc />
    public override T? Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadNullable(this._underlyingConverter, options);
}
