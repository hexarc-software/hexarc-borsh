namespace Hexarc.Borsh.Serialization;

public abstract class BorshAttribute : Attribute {}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class BorshObjectAttribute : BorshAttribute {}

[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshIgnoreAttribute : BorshAttribute { }

[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshOptionalAttribute : BorshAttribute { }

[AttributeUsage(AttributeTargets.Constructor)]
public sealed class BorshConstructorAttribute : BorshAttribute { }

[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshPropertyOrderAttribute : BorshAttribute
{
    public Byte Order { get; }

    public BorshPropertyOrderAttribute(Byte order) =>
        this.Order = order;
}

[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshFixedArrayAttribute : BorshAttribute
{
    public Int32 Length { get; }

    public BorshFixedArrayAttribute(Int32 length) =>
        this.Length = length;
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct | AttributeTargets.Enum |
                AttributeTargets.Parameter | AttributeTargets.Field)]
internal class BorshConverterAttribute : BorshAttribute
{
    public Type ConverterType { get; }

    public BorshConverterAttribute(Type converterType) =>
        this.ConverterType = converterType;
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public sealed class BorshUnionAttribute : BorshAttribute
{
    public Byte Order { get; }

    public Type CaseType { get; }

    public BorshUnionAttribute(Byte order, Type caseType)
    {
        this.Order = order;
        this.CaseType = caseType;
    }
}
