namespace Hexarc.Borsh.Serialization.Converters;

public sealed class IndirectOptionConverter<T> : BorshConverter<T?> where T : class
{
    private readonly BorshConverter<T> _underlyingConverter;

    public IndirectOptionConverter(BorshSerializerOptions options, BorshConverter<T>? underlyingConverter = null) =>
        this._underlyingConverter = underlyingConverter ?? options.GetConverter<T>();

    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options) =>
        writer.WriteIndirectOption(value, this._underlyingConverter, options);

    public override T? Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadIndirectOption(this._underlyingConverter, options);
}
