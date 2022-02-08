namespace Hexarc.Borsh.Serialization;

public abstract class BorshConverter<T> : BorshConverter
{
    internal override Type TypeToConvert { get; } = typeof(T);

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
}
