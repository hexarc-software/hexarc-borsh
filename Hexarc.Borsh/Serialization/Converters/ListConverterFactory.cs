namespace Hexarc.Borsh.Serialization.Converters;

public sealed class ListConverterFactory : BorshConverterFactory
{
    private static readonly Type s_listType = typeof(List<>);

    private static readonly Type s_listConverterType = typeof(ListConverter<>);

    /// <inheritdoc />
    public override Boolean CanConvert(Type type) =>
        type.IsGenericType && type.GetGenericTypeDefinition() == s_listType;

    public override BorshConverter CreateConverter(Type type, BorshSerializerOptions options)
    {
        var itemType = type.GetElementType() ?? throw new ArgumentException("List<T> type expected", nameof(type));
        var converterType = s_listConverterType.MakeGenericType(itemType);
        return Activator.CreateInstance(converterType, options) as BorshConverter ??
               throw new InvalidOperationException("Cannot create a converter instance");
    }
}
