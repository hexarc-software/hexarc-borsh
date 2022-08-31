namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Supports creating converters for different <see cref="Nullable{T}"/> types. 
/// </summary>
public sealed class NullableConverterFactory : BorshConverterFactory
{
    private static readonly Type s_nullableType = typeof(Nullable<>);

    private static readonly Type s_nullableConverterType = typeof(NullableConverter<>);

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == s_nullableType;

    /// <inheritdoc />
    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ??
                             throw new ArgumentException("Provided type is not Nullable<>", nameof(type));
        var underlyingConverter = options.GetConverter(underlyingType);
        var converterType = s_nullableConverterType.MakeGenericType(underlyingType);
        return Activator.CreateInstance(converterType, underlyingConverter) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
