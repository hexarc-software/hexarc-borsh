namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ObjectConverterFactory : BorshConverterFactory
{
    public override Boolean CanConvert(Type type) => true;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var baseConverterType = typeof(ObjectConverter<>);
        var concreteConverterType = baseConverterType.MakeGenericType(type);
        return Activator.CreateInstance(concreteConverterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
