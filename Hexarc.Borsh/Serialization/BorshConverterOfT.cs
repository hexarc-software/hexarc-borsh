namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverter<T> : BorshConverter
{
    internal override Type TypeToConvert { get; } = typeof(T);

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) =>
        type == typeof(T);

    /// <summary>
    /// Writes an object to a given <see cref="BorshWriter"/> target.
    /// </summary>
    /// <param name="writer">The writer target.</param>
    /// <param name="value"></param>
    /// <param name="options">The serialization options.</param>
    public abstract void Write(
        BorshWriter writer,
        T value,
        BorshSerializerOptions options);

    internal void WriteCore(
        BorshWriter writer,
        T value,
        BorshSerializerOptions options
    ) => this.Write(writer, value, options);

    internal sealed override void WriteCoreAsObject(
        BorshWriter writer,
        Object value,
        BorshSerializerOptions options
    ) => this.WriteCore(writer, (T)value, options);

    /// <summary>
    /// Reads an object from a given <see cref="BorshReader"/> source.
    /// </summary>
    /// <param name="reader">The reader source.</param>
    /// <param name="options">The serialization options.</param>
    /// <returns>
    /// An object that is read from the source.
    /// </returns>
    public abstract T Read(
        ref BorshReader reader,
        BorshSerializerOptions options);

    internal T ReadCore(
        ref BorshReader reader,
        BorshSerializerOptions options
    ) => this.Read(ref reader, options);

    internal override Object ReadCoreAsObject(
        ref BorshReader reader,
        BorshSerializerOptions options
    ) => this.ReadCore(ref reader, options)!;
}
