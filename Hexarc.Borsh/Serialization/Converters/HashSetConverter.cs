namespace Hexarc.Borsh.Serialization.Converters;

public sealed class HashSetConverter<T> : BorshConverter<HashSet<T>>
{
    private readonly BorshConverter<T> ItemConverter;

    public HashSetConverter(BorshSerializerOptions options) =>
        this.ItemConverter = options.GetConverter<T>();

    public override void Write(BorshWriter writer, HashSet<T> value, BorshSerializerOptions options)
    {
        writer.WriteInt32(value.Count);
        foreach (var item in value.OrderBy(x => x))
        {
            this.ItemConverter.Write(writer, item, options);
        }
    }

    public override HashSet<T> Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var capacity = reader.ReadInt32();
        var set = new HashSet<T>(capacity);
        for (var i = 0; i < capacity; i++)
        {
            set.Add(this.ItemConverter.Read(ref reader, options));
        }

        return set;
    }
}
