using System.Reflection;

namespace Hexarc.Borsh.Serialization.Metadata;

public sealed class BorshTypeInfo
{
    public Type Type { get; }

    public Boolean IsNullable { get; }

    public BorshTypeInfo(NullabilityInfo nullabilityInfo)
    {
        var underlyingType = Nullable.GetUnderlyingType(nullabilityInfo.Type);
        this.Type = underlyingType ?? nullabilityInfo.Type;
        this.IsNullable = nullabilityInfo.ReadState is NullabilityState.Nullable;
    }
}
