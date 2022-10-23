using System.Collections.Concurrent;
using Hexarc.Borsh.Serialization.Converters;

namespace Hexarc.Borsh.Serialization.Metadata;

/// <summary>
/// This class provides an ability for storing and inquiring
/// converters that are used during serialization/deserialization.
/// </summary>
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
        converters.AddConverter(BorshMetadataServices.Int128Converter);
        converters.AddConverter(BorshMetadataServices.UInt16Converter);
        converters.AddConverter(BorshMetadataServices.UInt32Converter);
        converters.AddConverter(BorshMetadataServices.UInt64Converter);
        converters.AddConverter(BorshMetadataServices.UInt128Converter);
        converters.AddConverter(BorshMetadataServices.HalfConverter);
        converters.AddConverter(BorshMetadataServices.SingleConverter);
        converters.AddConverter(BorshMetadataServices.DoubleConverter);
        converters.AddConverter(BorshMetadataServices.StringConverter);
        converters.AddConverter(BorshMetadataServices.DateTimeConverter);
        return converters;
    }

    private static BorshConverterFactory[] PrepareBuiltInConverterFactories() =>
        new BorshConverterFactory[]
        {
            new NullableConverterFactory(),
            new EnumConverterFactory(),
            new TupleConverterFactory(),
            new ArrayConverterFactory(),
            new ListConverterFactory(),
            new HashSetConverterFactory(),
            new DictionaryConverterFactory(),
            new OptionConverterFactory(),
            new UnionConverterFactory(),
            new ObjectConverterFactory()
        };

    private readonly BorshSerializerOptions _options;

    private readonly ConcurrentDictionary<Type, BorshConverter> _cachedConverters = new();

    /// <summary>
    /// Creates an instance of the <see cref="BorshConverterRegistry"/> class.
    /// </summary>
    /// <param name="options">The serialization options used for converters.</param>
    public BorshConverterRegistry(BorshSerializerOptions options) =>
        this._options = options;

    /// <summary>
    /// Retrieves the converter for a requested type.
    /// </summary>
    /// <param name="type">The type to find the converter for.</param>
    /// <returns>The found converter for the requested type.</returns>
    /// <exception cref="ArgumentException">
    /// Throws if no converter found.
    /// </exception>
    public BorshConverter GetConverter(Type type)
    {
        if (this._cachedConverters.TryGetValue(type, out var converter))
        {
            return converter;
        }

        if (!BuiltInConverters.TryGetValue(type, out converter))
        {
            foreach (var converterFactory in BuiltInConverterFactories)
            {
                if (converterFactory.CanConvert(type))
                {
                    converter = converterFactory.CreateConverter(type, this._options);
                    break;
                }
            }
        }

        if (converter is null)
        {
            throw new ArgumentException("Cannot find a converter for the given type", type.FullName);
        }

        this._cachedConverters.TryAdd(type, converter);
        return converter;
    }

    /// <summary>
    /// Retrieves the converter for a requested type.
    /// </summary>
    /// <typeparam name="T">The type to find the converter for.</typeparam>
    /// <returns>The found converter for the requested type.</returns>
    /// <exception cref="InvalidOperationException">
    /// Throws if the found converter can not be casted to the generalized BORSH converter class.
    /// </exception>
    public BorshConverter<T> GetConverter<T>() =>
        this.GetConverter(typeof(T)) as BorshConverter<T> ?? throw new InvalidOperationException();
}

internal static class DictionaryExtensions
{
    public static void AddConverter(this Dictionary<Type, BorshConverter> converters, BorshConverter converter) =>
        converters.Add(converter.TypeToConvert, converter);
}
