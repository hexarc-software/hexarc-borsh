namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ListConverterFactory : BorshConverterFactory
{
    private static readonly Type ListType = typeof(List<>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == ListType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var itemType = type.GetElementType() ?? throw new ArgumentException("List<T> type expected", nameof(type));
        var converterType = typeof(ListConverter<>).MakeGenericType(itemType);
        return Activator.CreateInstance(converterType, options)  as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
