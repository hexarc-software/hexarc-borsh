namespace Hexarc.Borsh.Serialization.Converters;

public sealed class OptionConverter<T> : BorshConverter<Option<T>> where T : class
{
    public override void Write(BorshWriter writer, Option<T> value, BorshSerializerOptions options) =>
        writer.WriteOption(value, options.GetConverter<T>(), options);

    public override Option<T> Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadOption<T>(options.GetConverter<T>(), options);
}

public sealed class IndirectOptionConverter<T> : BorshConverter<T?> where T : class
{
    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options) =>
        writer.WriteIndirectOption(value, options.GetConverter<T>(), options);

    public override T? Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadIndirectOption(options.GetConverter<T>(), options);
}
