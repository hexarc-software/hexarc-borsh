namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for BORSH optional types in indirect operations.
/// </summary>
/// <typeparam name="T">The underlying type of a serializable option type.</typeparam>
public sealed class IndirectOptionConverter<T> : BorshConverter<T?> where T : class
{
    private readonly BorshConverter<T> _underlyingConverter;

    /// <summary>
    /// Creates an instance of the <see cref="IndirectOptionConverter{T}"/> class.
    /// </summary>
    /// <param name="options">The serialization options.</param>
    /// <param name="underlyingConverter">The converter for the underlying type of a serializable option type.</param>
    public IndirectOptionConverter(BorshSerializerOptions options, BorshConverter<T>? underlyingConverter = null) =>
        this._underlyingConverter = underlyingConverter ?? options.GetConverter<T>();
    
    /// <inheritdoc />
    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options) =>
        writer.WriteIndirectOption(value, this._underlyingConverter, options);

    /// <inheritdoc />
    public override T? Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadIndirectOption(this._underlyingConverter, options);
}
