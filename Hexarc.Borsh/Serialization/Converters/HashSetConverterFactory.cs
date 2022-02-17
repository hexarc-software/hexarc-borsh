namespace Hexarc.Borsh.Serialization.Converters;

public sealed class HashSetConverterFactory : BorshConverterFactory
{
    private readonly Type HashSetType = typeof(HashSet<>);

    public override Boolean CanConvert(Type type) =>
        type.GetGenericTypeDefinition() == this.HashSetType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var itemType = type.GetGenericArguments().First();
        var converterType = typeof(HashSetConverter<>).MakeGenericType(itemType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
