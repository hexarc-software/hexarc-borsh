namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for the <see cref="List{T}"/> class.
/// </summary>
/// <typeparam name="T">The element type of a serializable list.</typeparam>
public sealed class ListConverter<T> : BorshConverter<List<T>>
{
    private readonly BorshConverter<T> _itemConverter;

    /// <summary>
    /// Creates an instance of the <see cref="ListConverter{T}"/> class.
    /// </summary>
    /// <param name="options">The serialization options.</param>
    public ListConverter(BorshSerializerOptions options) =>
        this._itemConverter = options.GetConverter<T>();

    /// <inheritdoc />
    public override void Write(BorshWriter writer, List<T> list, BorshSerializerOptions options)
    {
        writer.WriteInt32(list.Count);
        foreach (var item in list)
        {
            this._itemConverter.Write(writer, item, options);
        }
    }

    /// <inheritdoc />
    public override List<T> Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var count = reader.ReadInt32();
        var list = new List<T>(count);
        for (var i = 0; i < count; i++)
        {
            list[i] = this._itemConverter.Read(ref reader, options);
        }

        return list;
    }
}
