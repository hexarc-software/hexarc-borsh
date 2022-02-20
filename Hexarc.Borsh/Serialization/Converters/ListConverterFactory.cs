namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ListConverterFactory : BorshConverterFactory
{
    private readonly Type _listType = typeof(List<>);

    private readonly Type _listConverterType = typeof(ListConverter<>);

    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == this._listType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var itemType = type.GetElementType() ?? throw new ArgumentException("List<T> type expected", nameof(type));
        var converterType = this._listConverterType.MakeGenericType(itemType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
