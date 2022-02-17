namespace Hexarc.Borsh.Serialization.Converters;

public sealed class DictionaryConverterFactory : BorshConverterFactory
{
    private readonly Type DictionaryType = typeof(Dictionary<,>);

    public override Boolean CanConvert(Type type) =>
        type.GetGenericTypeDefinition() == this.DictionaryType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var genericArguments = type.GetGenericArguments();
        var keyType = genericArguments[0];
        var valueType = genericArguments[1];
        var converterType = typeof(DictionaryConverter<,>).MakeGenericType(keyType, valueType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
