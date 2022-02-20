namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ObjectConverterFactory : BorshConverterFactory
{
    private readonly Type _objectConverterType = typeof(ObjectConverter<>);

    public override Boolean CanConvert(Type type) => true;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var converterType = this._objectConverterType.MakeGenericType(type);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
