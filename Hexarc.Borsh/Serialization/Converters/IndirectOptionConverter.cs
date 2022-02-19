namespace Hexarc.Borsh.Serialization.Converters;

public sealed class IndirectOptionConverter<T> : BorshConverter<T?> where T : class
{
    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options) =>
        writer.WriteIndirectOption(value, options.GetConverter<T>(), options);

    public override T? Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadIndirectOption(options.GetConverter<T>(), options);
}