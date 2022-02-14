namespace Hexarc.Borsh.Serialization.Converters;

public sealed class OptionConverterFactory : BorshConverterFactory
{
    private readonly Type OptionBaseType = typeof(Option<>);

    private readonly Type OptionBaseConverter = typeof(OptionConverter<>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == this.OptionBaseType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var valueType = type.GetGenericArguments().First();
        return (BorshConverter)Activator.CreateInstance(this.OptionBaseConverter.MakeGenericType(valueType))!;
    }
}
