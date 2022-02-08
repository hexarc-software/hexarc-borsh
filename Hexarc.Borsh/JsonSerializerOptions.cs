using Hexarc.Borsh.Serialization;
using Hexarc.Borsh.Serialization.Metadata;

namespace Hexarc.Borsh;

public sealed class BorshSerializerOptions
{
    internal static readonly BorshSerializerOptions Default = new();

    private readonly BorshConverterRegistry ConverterRegistry = new();

    internal BorshConverter GetConverter(Type type) =>
        this.ConverterRegistry.GetConverter(type);
}
