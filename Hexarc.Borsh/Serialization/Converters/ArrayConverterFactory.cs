namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ArrayConverterFactory : BorshConverterFactory
{
    private static readonly Type s_arrayConverterType = typeof(ArrayConverter<>);

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) =>
        type.IsArray;

    /// <inheritdoc />
    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var itemType = type.GetElementType() ?? throw new ArgumentException("Array type expected", nameof(type));
        var converterType = s_arrayConverterType.MakeGenericType(itemType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
