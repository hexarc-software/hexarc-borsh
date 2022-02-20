namespace Hexarc.Borsh.Serialization.Converters;

public sealed class EnumConverterFactory : BorshConverterFactory
{
    private readonly Type _enumConverterType = typeof(EnumConverter<>);

    public override Boolean CanConvert(Type type) => type.IsEnum;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var converterType = this._enumConverterType.MakeGenericType(type);
        return Activator.CreateInstance(converterType) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
