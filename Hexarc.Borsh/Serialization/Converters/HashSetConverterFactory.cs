namespace Hexarc.Borsh.Serialization.Converters;

public sealed class HashSetConverterFactory : BorshConverterFactory
{
    private readonly Type _hashSetType = typeof(HashSet<>);

    private readonly Type _hashSetConverterType = typeof(HashSetConverter<>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == this._hashSetType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var itemType = type.GetGenericArguments().First();
        var converterType = this._hashSetConverterType.MakeGenericType(itemType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
