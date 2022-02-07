using System.Collections.Concurrent;
using Hexarc.Borsh.Serialization;

namespace Hexarc.Borsh;

public sealed class BorshSerializerOptions
{
    internal static readonly BorshSerializerOptions Default = new();

    private readonly ConcurrentDictionary<Type, BorshConverter> _converters = new();
}
