namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ReferenceOptionConverter<T> : BorshConverter<T?>
    where T : class
{
    private readonly BorshConverter<T> UnderlyingConverter;

    public ReferenceOptionConverter(BorshConverter<T> underlyingConverter) =>
        this.UnderlyingConverter = underlyingConverter;

    public override void Write(BorshWriter writer, T? value, BorshSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteByte(0);
        }
        else
        {
            writer.WriteByte(1);
            this.UnderlyingConverter.Write(writer, value, options);
        }
    }
}
