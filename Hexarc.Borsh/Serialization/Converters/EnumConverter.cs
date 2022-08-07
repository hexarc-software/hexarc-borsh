namespace Hexarc.Borsh.Serialization.Converters;

public sealed class EnumConverter<T> : BorshConverter<T> where T : struct, Enum
{
    private readonly T[] _cachedValues = Enum.GetValues<T>();

    /// <inheritdoc />
    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        var position = Array.IndexOf(this._cachedValues, value);
        if (position == -1)
        {
            throw new IndexOutOfRangeException("Given value does not belong to thr requested enum");
        }
        if (position > Byte.MaxValue)
        {
            throw new ArgumentException("Enum value index cannot be more than Byte.MaxValue", nameof(value));
        }
        writer.WriteByte((Byte)position);
    }

    /// <inheritdoc />
    public override T Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var index = reader.ReadByte();
        return this._cachedValues[index];
    }
}
