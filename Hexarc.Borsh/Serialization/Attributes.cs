namespace Hexarc.Borsh.Serialization;

public abstract class BorshAttribute : Attribute {}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class BorshObjectAttribute : BorshAttribute {}

[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshIgnoreAttribute : BorshAttribute { }

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum |
                AttributeTargets.Parameter | AttributeTargets.Field)]
public sealed class BorshConverterAttribute : BorshAttribute
{
    public Type ConverterType { get; }

    public BorshConverterAttribute(Type converterType) =>
        this.ConverterType = converterType;
}
