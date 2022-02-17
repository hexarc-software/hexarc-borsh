using Hexarc.Borsh.Serialization.Converters;

namespace Hexarc.Borsh.Serialization.Metadata;

public sealed class BorshConverterRegistry
{
    /// <summary>
    /// Gets the built-in converters for primitive types.
    /// </summary>
    private static Dictionary<Type, BorshConverter> BuiltInConverters =>
        _builtInConverters ??= PrepareBuiltInConverters();

    private static Dictionary<Type, BorshConverter>? _builtInConverters;

    /// <summary>
    /// Gets the built-in converter factories for the general .NET types
    /// like enum, array-like, object-like ones.
    /// </summary>
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
            new NullableConverterFactory(),
            new EnumConverterFactory(),
            new ArrayConverterFactory(),
            new HashSetConverterFactory(),
            new DictionaryConverterFactory(),
            new OptionConverterFactory(),
            new ObjectConverterFactory()
        };

    private BorshSerializerOptions Options { get; }

    public BorshConverterRegistry(BorshSerializerOptions options) =>
        this.Options = options;

    public BorshConverter GetConverter(Type type)
    {
        if (!BuiltInConverters.TryGetValue(type, out var converter))
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

    public BorshConverter<T> GetConverter<T>() =>
        this.GetConverter(typeof(T)) as BorshConverter<T> ?? throw new InvalidOperationException();
}

internal static class DictionaryExtensions
{
    public static void AddConverter(this Dictionary<Type, BorshConverter> converters, BorshConverter converter) =>
        converters.Add(converter.TypeToConvert, converter);
}
