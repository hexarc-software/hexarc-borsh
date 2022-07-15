namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverter<T> : BorshConverter
{
    internal override Type TypeToConvert { get; } = typeof(T);

    public override Boolean CanConvert(Type type) =>
        type == typeof(T);

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
