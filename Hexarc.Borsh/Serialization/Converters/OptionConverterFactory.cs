namespace Hexarc.Borsh.Serialization.Converters;

public sealed class OptionConverterFactory : BorshConverterFactory
{
    private static readonly Type s_optionType = typeof(Option<>);

    private static readonly Type s_optionConverterType = typeof(OptionConverter<>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == s_optionType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var valueType = type.GetGenericArguments().First();
        var converterType = s_optionConverterType.MakeGenericType(valueType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
