namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ArrayConverter<T> : BorshConverter<T[]>
{
    public override void Write(BorshWriter writer, T[] value, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override T[] Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
