namespace Hexarc.Borsh.Serialization.Converters;

public sealed class HashSetConverter<T> : BorshConverter<HashSet<T>>
{
    private readonly BorshConverter<T> _itemConverter;

    public HashSetConverter(BorshSerializerOptions options) =>
        this._itemConverter = options.GetConverter<T>();

    public override void Write(BorshWriter writer, HashSet<T> hashSet, BorshSerializerOptions options)
    {
        writer.WriteInt32(hashSet.Count);
        foreach (var item in hashSet.OrderBy(x => x))
        {
            this._itemConverter.Write(writer, item, options);
        }
    }

    public override HashSet<T> Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var count = reader.ReadInt32();
        var set = new HashSet<T>(count);
        for (var i = 0; i < count; i++)
        {
            set.Add(this._itemConverter.Read(ref reader, options));
        }

        return set;
    }
}
