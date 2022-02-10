namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ObjectConverterFactory : BorshConverterFactory
{
    public override Boolean CanConvert(Type type) => true;
    internal override Object? ReadCoreAsObject(ref BorshReader reader, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var baseConverterType = typeof(ObjectDefaultConverter<>);
        var concreteConverterType = baseConverterType.MakeGenericType(type);
        return Activator.CreateInstance(concreteConverterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
