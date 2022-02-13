using Hexarc.Borsh.Serialization;
using Hexarc.Borsh.Serialization.Metadata;

namespace Hexarc.Borsh;

public sealed class BorshSerializerOptions
{
    internal static readonly BorshSerializerOptions Default = new();

    private readonly BorshConverterRegistry ConverterRegistry;

    public BorshSerializerOptions() =>
        this.ConverterRegistry = new BorshConverterRegistry(this);

    internal BorshConverter<T> GetConverter<T>() =>
        this.ConverterRegistry.GetConverter<T>();

    internal BorshConverter GetConverter(Type type) =>
        this.ConverterRegistry.GetConverter(type);
}
