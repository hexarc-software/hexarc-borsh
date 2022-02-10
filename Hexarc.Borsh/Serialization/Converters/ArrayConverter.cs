namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ArrayConverter<TCollection, TElement> : IEnumerableDefaultConverter<TElement[], TElement>
{
    public override void Write(BorshWriter writer, TElement[] value, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override TElement[] Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
