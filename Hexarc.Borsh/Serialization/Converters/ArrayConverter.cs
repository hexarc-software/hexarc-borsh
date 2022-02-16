namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ArrayConverter<T> : BorshConverter<T[]>
{
    private readonly BorshConverter<T> ItemConverter;

    public ArrayConverter(BorshSerializerOptions options) =>
        this.ItemConverter = options.GetConverter<T>();

    public override void Write(BorshWriter writer, T[] value, BorshSerializerOptions options)
    {
        writer.WriteInt32(value.Length);
        foreach (var item in value)
        {
            this.ItemConverter.Write(writer, item, options);
        }
    }

    public override T[] Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var length = reader.ReadInt32();
        var array = new T[length];
        for (var i = 0; i < length; i++)
        {
            array[i] = this.ItemConverter.Read(ref reader, options);
        }

        return array;
    }
}
