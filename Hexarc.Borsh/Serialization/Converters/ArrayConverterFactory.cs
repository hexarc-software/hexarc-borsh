namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ArrayConverterFactory : BorshConverterFactory
{
    private readonly Type _arrayConverterType = typeof(ArrayConverter<>);

    public override Boolean CanConvert(Type type) =>
        type.IsArray;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var itemType = type.GetElementType() ?? throw new ArgumentException("Array type expected", nameof(type));
        var converterType = this._arrayConverterType.MakeGenericType(itemType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
