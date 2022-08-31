namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Supports creating converters for different <see cref="Dictionary{TKey,TValue}"/> types. 
/// </summary>
public sealed class DictionaryConverterFactory : BorshConverterFactory
{
    private static readonly Type s_dictionaryType = typeof(Dictionary<,>);

    private static readonly Type s_dictionaryConverterType = typeof(DictionaryConverter<,>);

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == s_dictionaryType;

    /// <inheritdoc />
    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var genericArguments = type.GetGenericArguments();
        var keyType = genericArguments[0];
        var valueType = genericArguments[1];
        var converterType = s_dictionaryConverterType.MakeGenericType(keyType, valueType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
