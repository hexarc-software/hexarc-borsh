namespace Hexarc.Borsh.Serialization.Converters;

public abstract class IEnumerableDefaultConverter<TCollection, TElement> :
    BorshCollectionConverter<TCollection, TElement> where TCollection : IEnumerable<TElement>
{

}
