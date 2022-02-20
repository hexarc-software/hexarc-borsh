namespace Hexarc.Borsh.Serialization.Converters;

public sealed class OptionConverterFactory : BorshConverterFactory
{
    private readonly Type _optionType = typeof(Option<>);

    private readonly Type _optionConverter = typeof(OptionConverter<>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == this._optionType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var valueType = type.GetGenericArguments().First();
        var converterType = this._optionConverter.MakeGenericType(valueType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
