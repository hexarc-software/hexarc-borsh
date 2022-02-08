namespace Hexarc.Borsh.Serialization.Metadata;

public sealed class BorshConverterRegistry
{
    private static Dictionary<Type, BorshConverter> BuiltInConverters =>
        _builtInConverters ??= PrepareBuiltInConverters();

    private static Dictionary<Type, BorshConverter>? _builtInConverters;

    private static Dictionary<Type, BorshConverter> PrepareBuiltInConverters()
    {
        var converters = new Dictionary<Type, BorshConverter>();
        converters.AddConverter(BorshMetadataServices.BooleanConverter);
        converters.AddConverter(BorshMetadataServices.ByteConverter);
        converters.AddConverter(BorshMetadataServices.SByteConverter);
        converters.AddConverter(BorshMetadataServices.Int16Converter);
        converters.AddConverter(BorshMetadataServices.Int32Converter);
        converters.AddConverter(BorshMetadataServices.Int64Converter);
        converters.AddConverter(BorshMetadataServices.UInt16Converter);
        converters.AddConverter(BorshMetadataServices.UInt32Converter);
        converters.AddConverter(BorshMetadataServices.UInt64Converter);
        converters.AddConverter(BorshMetadataServices.SingleConverter);
        converters.AddConverter(BorshMetadataServices.DoubleConverter);
        converters.AddConverter(BorshMetadataServices.StringConverter);
        return converters;
    }

    public BorshConverter GetConverter(Type type)
    {
        return BuiltInConverters[type];
    }
}

internal static class DictionaryExtensions
{
    public static void AddConverter(this Dictionary<Type, BorshConverter> converters, BorshConverter converter) =>
        converters.Add(converter.TypeToConvert, converter);
}
