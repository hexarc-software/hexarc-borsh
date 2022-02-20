namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ArrayConverter<T> : BorshConverter<T[]>
{
    private readonly BorshConverter<T> _itemConverter;

    public ArrayConverter(BorshSerializerOptions options) =>
        this._itemConverter = options.GetConverter<T>();

    public override void Write(BorshWriter writer, T[] array, BorshSerializerOptions options)
    {
        writer.WriteInt32(array.Length);
        foreach (var item in array)
        {
            this._itemConverter.Write(writer, item, options);
        }
    }

    public override T[] Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var length = reader.ReadInt32();
        var array = new T[length];
        for (var i = 0; i < length; i++)
        {
            array[i] = this._itemConverter.Read(ref reader, options);
        }

        return array;
    }
}
