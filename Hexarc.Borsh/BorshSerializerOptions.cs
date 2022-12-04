using Hexarc.Borsh.Serialization;
using Hexarc.Borsh.Serialization.Metadata;

namespace Hexarc.Borsh;

/// <summary>
/// Represents the BORSH serialization settings.
/// </summary>
public sealed class BorshSerializerOptions
{
    internal static readonly BorshSerializerOptions Default = new();

    private readonly BorshConverterRegistry _converterRegistry;

    /// <summary>
    /// Creates an instance of the <see cref="BorshSerializerOptions"/> class.
    /// </summary>
    public BorshSerializerOptions() =>
        this._converterRegistry = new BorshConverterRegistry(this);

    internal BorshConverter<T> GetConverter<T>() =>
        this._converterRegistry.GetConverter<T>();

    internal BorshConverter GetConverter(Type type) =>
        this._converterRegistry.GetConverter(type);
}
