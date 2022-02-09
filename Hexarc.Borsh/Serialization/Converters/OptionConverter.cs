namespace Hexarc.Borsh.Serialization.Converters;

public abstract class OptionConverter<TNullable, TUnderlying> : BorshConverter<TNullable?>
{
    private readonly BorshConverter<TUnderlying> UnderlyingConverter;

    protected OptionConverter(BorshConverter<TUnderlying> underlyingConverter) =>
        this.UnderlyingConverter = underlyingConverter;

    public sealed override void Write(BorshWriter writer, TNullable? value, BorshSerializerOptions options)
    {
        if (this.TryGetValue(value, out var output))
        {
            this.WriteSome(writer, output, options);
        }
        else
        {
            this.WriteNone(writer);
        }
    }

    protected abstract Boolean TryGetValue(TNullable? input, out TUnderlying output);

    private void WriteNone(BorshWriter writer) =>
        writer.WriteByte(0);

    private void WriteSome(BorshWriter writer, TUnderlying value, BorshSerializerOptions options)
    {
        writer.WriteByte(1);
        this.UnderlyingConverter.Write(writer, value, options);
    }
}

public sealed class ValueOptionConverter<T> : OptionConverter<T?, T>
    where T : struct
{
    public ValueOptionConverter(BorshConverter<T> underlyingConverter) :
        base(underlyingConverter) { }

    protected override Boolean TryGetValue(T? input, out T output)
    {
        if (input.HasValue)
        {
            output = input.Value;
            return true;
        }
        else
        {
            output = default;
            return false;
        }
    }
}

public sealed class ReferenceOptionConverter<T> : OptionConverter<T?, T>
    where T : class
{
    public ReferenceOptionConverter(BorshConverter<T> underlyingConverter) :
        base(underlyingConverter) { }

    protected override Boolean TryGetValue(T? input, out T output)
    {
        if (input is null)
        {
            output = default!;
            return false;
        }
        else
        {
            output = input;
            return true;
        }
    }
}
