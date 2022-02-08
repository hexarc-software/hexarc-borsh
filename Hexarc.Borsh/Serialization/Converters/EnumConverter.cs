namespace Hexarc.Borsh.Serialization.Converters;

public sealed class EnumConverter<T> : BorshConverter<T> where T : struct, Enum
{
    private readonly T[] CachedValues = Enum.GetValues<T>();

    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        var position = Array.IndexOf(this.CachedValues, value);
        if (position > Byte.MaxValue)
        {
            throw new ArgumentException("Enum value index cannot be more than Byte.MaxValue", nameof(value));
        }
        writer.WriteByte((Byte)position);
    }
}
