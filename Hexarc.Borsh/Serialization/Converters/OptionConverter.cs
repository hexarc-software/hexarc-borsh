namespace Hexarc.Borsh.Serialization.Converters;

public sealed class OptionConverter<T> : BorshConverter<Option<T>> where T : class
{
    private readonly BorshConverter<T> _underlyingConverter;

    public OptionConverter(BorshSerializerOptions options) =>
        this._underlyingConverter = options.GetConverter<T>();

    /// <inheritdoc />
    public override void Write(BorshWriter writer, Option<T> value, BorshSerializerOptions options) =>
        writer.WriteOption(value, this._underlyingConverter, options);

    /// <inheritdoc />
    public override Option<T> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadOption<T>(this._underlyingConverter, options);
}
