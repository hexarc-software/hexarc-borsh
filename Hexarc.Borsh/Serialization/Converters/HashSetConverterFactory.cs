namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Supports creating converters for different <see cref="HashSet{T}"/> types. 
/// </summary>
public sealed class HashSetConverterFactory : BorshConverterFactory
{
    private static readonly Type s_hashSetType = typeof(HashSet<>);

    private static readonly Type s_hashSetConverterType = typeof(HashSetConverter<>);

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == s_hashSetType;

    /// <inheritdoc />
    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var itemType = type.GetGenericArguments().First();
        var converterType = s_hashSetConverterType.MakeGenericType(itemType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
