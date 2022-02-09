namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ArrayConverter<TCollection, TElement> : IEnumerableDefaultConverter<TElement[], TElement>
{
    public override void Write(BorshWriter writer, TElement[] value, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
