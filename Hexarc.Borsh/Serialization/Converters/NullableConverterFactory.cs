namespace Hexarc.Borsh.Serialization.Converters;

public sealed class NullableConverterFactory : BorshConverterFactory
{
    private static readonly Type NullableType = typeof(Nullable<>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == NullableType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var underlyingType = Nullable.GetUnderlyingType(type) ??
                             throw new ArgumentException("Provided type is not Nullable<>", nameof(type));
        var underlyingConverter = options.GetConverter(underlyingType);
        var baseConverterType = typeof(NullableConverter<>);
        var concreteConverterType = baseConverterType.MakeGenericType(underlyingType);
        return Activator.CreateInstance(concreteConverterType, underlyingConverter) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
