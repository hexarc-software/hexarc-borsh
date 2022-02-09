using System.Collections;

namespace Hexarc.Borsh.Serialization.Converters;

public sealed class IEnumerableConverterFactory : BorshConverterFactory
{
    public override Boolean CanConvert(Type type) =>
        typeof(IEnumerable).IsAssignableFrom(type);

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
