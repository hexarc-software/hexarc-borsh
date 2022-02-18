namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ListConverter<T> : BorshConverter<List<T>>
{
    private readonly BorshConverter<T> ItemConverter;

    public ListConverter(BorshSerializerOptions options) =>
        this.ItemConverter = options.GetConverter<T>();

    public override void Write(BorshWriter writer, List<T> value, BorshSerializerOptions options)
    {
        writer.WriteInt32(value.Count);
        foreach (var item in value)
        {
            this.ItemConverter.Write(writer, item, options);
        }
    }

    public override List<T> Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var count = reader.ReadInt32();
        var list = new List<T>(count);
        for (var i = 0; i < count; i++)
        {
            list[i] = this.ItemConverter.Read(ref reader, options);
        }

        return list;
    }
}
