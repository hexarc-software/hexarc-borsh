namespace Hexarc.Borsh.Serialization.Converters;

public sealed class DictionaryConverter<TKey, TValue> : BorshConverter<Dictionary<TKey, TValue>>
    where TKey : notnull
{
    private readonly BorshConverter<TKey> KeyConverter;
    private readonly BorshConverter<TValue> ValueConverter;

    public DictionaryConverter(BorshSerializerOptions options)
    {
        this.KeyConverter = options.GetConverter<TKey>();
        this.ValueConverter = options.GetConverter<TValue>();
    }

    public override void Write(BorshWriter writer, Dictionary<TKey, TValue> dictionary, BorshSerializerOptions options)
    {
        writer.WriteInt32(dictionary.Count);
        foreach (var (key, value) in dictionary.OrderBy(x => x.Key))
        {
            this.KeyConverter.Write(writer, key, options);
            this.ValueConverter.Write(writer, value, options);
        }
    }

    public override Dictionary<TKey, TValue> Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var capacity = reader.ReadInt32();
        var dictionary = new Dictionary<TKey, TValue>(capacity);
        for (var i = 0; i < capacity; i++)
        {
            var key = this.KeyConverter.Read(ref reader, options);
            var value = this.ValueConverter.Read(ref reader, options);
            dictionary.Add(key, value);
        }

        return dictionary;
    }
}
