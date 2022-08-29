namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for BORSH fixed arrays in indirect operations.
/// </summary>
/// <typeparam name="T">The element type of serializable arrays.</typeparam>
public sealed class IndirectFixedArrayConverter<T> : BorshConverter<T[]>
{
    private readonly BorshConverter<T> _itemConverter;

    private readonly Int32 _arrayLength;

    /// <summary>
    /// Creates an instance of the <see cref="IndirectFixedArrayConverter{T}"/> class.
    /// </summary>
    /// <param name="options">The serialization options.</param>
    /// <param name="arrayLength">The length of an array that is being serialized or deserialized.</param>
    public IndirectFixedArrayConverter(BorshSerializerOptions options, Int32 arrayLength)
    {
        this._itemConverter = options.GetConverter<T>();
        this._arrayLength = arrayLength;
    }

    /// <inheritdoc />
    public override void Write(BorshWriter writer, T[] array, BorshSerializerOptions options)
    {
        if (array.Length != this._arrayLength)
        {
            throw new ArgumentOutOfRangeException($"Expected array length {this._arrayLength}, instead got {array.Length}");
        }
        
        foreach (var item in array)
        {
            this._itemConverter.Write(writer, item, options);
        }
    }

    /// <inheritdoc />
    public override T[] Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var array = new T[this._arrayLength];
        for (var i = 0; i < this._arrayLength; i++)
        {
            array[i] = this._itemConverter.Read(ref reader, options);
        }

        return array;
    }
}
