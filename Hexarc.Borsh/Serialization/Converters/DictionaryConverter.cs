namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for the <see cref="Dictionary{TKey,TValue}"/> class.
/// </summary>
/// <typeparam name="TKey">The key type of serializable dictionaries.</typeparam>
/// <typeparam name="TValue">The value type of serializable dictionaries.</typeparam>
public sealed class DictionaryConverter<TKey, TValue> : BorshConverter<Dictionary<TKey, TValue>>
    where TKey : notnull
{
    private readonly BorshConverter<TKey> _keyConverter;

    private readonly BorshConverter<TValue> _valueConverter;

    /// <summary>
    /// Creates an instance of the <see cref="DictionaryConverter{TKey,TValue}"/> class.
    /// </summary>
    /// <param name="options">The serialization options.</param>
    public DictionaryConverter(BorshSerializerOptions options)
    {
        this._keyConverter = options.GetConverter<TKey>();
        this._valueConverter = options.GetConverter<TValue>();
    }

    /// <inheritdoc />
    public override void Write(BorshWriter writer, Dictionary<TKey, TValue> dictionary, BorshSerializerOptions options)
    {
        writer.WriteInt32(dictionary.Count);
        foreach (var (key, value) in dictionary.OrderBy(x => x.Key))
        {
            this._keyConverter.Write(writer, key, options);
            this._valueConverter.Write(writer, value, options);
        }
    }

    /// <inheritdoc />
    public override Dictionary<TKey, TValue> Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var count = reader.ReadInt32();
        var dictionary = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; i++)
        {
            var key = this._keyConverter.Read(ref reader, options);
            var value = this._valueConverter.Read(ref reader, options);
            dictionary.Add(key, value);
        }

        return dictionary;
    }
}
