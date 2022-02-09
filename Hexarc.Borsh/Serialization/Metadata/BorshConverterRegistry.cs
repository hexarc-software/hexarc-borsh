using Hexarc.Borsh.Serialization.Converters;

namespace Hexarc.Borsh.Serialization.Metadata;

public sealed class BorshConverterRegistry
{
    private static Dictionary<Type, BorshConverter> BuiltInConverters =>
        _builtInConverters ??= PrepareBuiltInConverters();

    private static Dictionary<Type, BorshConverter>? _builtInConverters;

    private static BorshConverterFactory[] BuiltInConverterFactories =>
        _builtInConverterFactories ??= PrepareBuiltInConverterFactories();

    private static BorshConverterFactory[]? _builtInConverterFactories;

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

    private static BorshConverterFactory[] PrepareBuiltInConverterFactories() =>
        new BorshConverterFactory[]
        {
            new EnumConverterFactory(),
            new IEnumerableConverterFactory()
        };

    private BorshSerializerOptions Options { get; }

    public BorshConverterRegistry(BorshSerializerOptions options)
    {
        this.Options = options;
    }

    public BorshConverter GetConverter(Type type)
    {
        var converter = default(BorshConverter);

        if (BuiltInConverters.TryGetValue(type, out converter))
        {
            return converter;
        }
        else
        {
            foreach (var converterFactory in BuiltInConverterFactories)
            {
                if (converterFactory.CanConvert(type))
                {
                    converter = converterFactory.CreateConverter(type, this.Options);
                    break;
                }
            }
        }

        if (converter is null)
        {
            throw new ArgumentException("Cannot find a converter for the given type", type.FullName);
        }

        return converter;
    }
}

internal static class DictionaryExtensions
{
    public static void AddConverter(this Dictionary<Type, BorshConverter> converters, BorshConverter converter) =>
        converters.Add(converter.TypeToConvert, converter);
}
