namespace Hexarc.Borsh.Serialization.Converters;

public sealed class OptionConverterFactory : BorshConverterFactory
{
    private readonly Type OptionBaseType = typeof(Option<>);

    private readonly Type OptionBaseConverter = typeof(OptionConverter<>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == this.OptionBaseType;

    internal override Object? ReadCoreAsObject(ref BorshReader reader, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override BorshConverter? CreateConverter(Type type, BorshSerializerOptions options)
    {
        var valueType = type.GetGenericArguments().First();
        return (BorshConverter)Activator.CreateInstance(this.OptionBaseConverter.MakeGenericType(valueType))!;
    }
}
