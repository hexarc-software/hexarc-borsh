namespace Hexarc.Borsh.Serialization.Converters;

public sealed class NullableConverterFactory : BorshConverterFactory
{
    private readonly Type _nullableType = typeof(Nullable<>);

    private readonly Type _nullableConverterType = typeof(NullableConverter<>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == this._nullableType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ??
                             throw new ArgumentException("Provided type is not Nullable<>", nameof(type));
        var underlyingConverter = options.GetConverter(underlyingType);
        var converterType = _nullableConverterType.MakeGenericType(underlyingType);
        return Activator.CreateInstance(converterType, underlyingConverter) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
