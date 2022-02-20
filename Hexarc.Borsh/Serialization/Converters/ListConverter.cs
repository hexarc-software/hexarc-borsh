namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ListConverter<T> : BorshConverter<List<T>>
{
    private readonly BorshConverter<T> _itemConverter;

    public ListConverter(BorshSerializerOptions options) =>
        this._itemConverter = options.GetConverter<T>();

    public override void Write(BorshWriter writer, List<T> list, BorshSerializerOptions options)
    {
        writer.WriteInt32(list.Count);
        foreach (var item in list)
        {
            this._itemConverter.Write(writer, item, options);
        }
    }

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
