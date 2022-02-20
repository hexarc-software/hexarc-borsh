namespace Hexarc.Borsh.Serialization.Converters;

public sealed class DictionaryConverterFactory : BorshConverterFactory
{
    private readonly Type _dictionaryType = typeof(Dictionary<,>);

    private readonly Type _dictionaryConverterType = typeof(DictionaryConverter<,>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == this._dictionaryType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var genericArguments = type.GetGenericArguments();
        var keyType = genericArguments[0];
        var valueType = genericArguments[1];
        var converterType = this._dictionaryConverterType.MakeGenericType(keyType, valueType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
