namespace Hexarc.Borsh.Serialization;

/// <summary>
/// Serves as the base class for all BORSH serialization attributes.
/// </summary>
public abstract class BorshAttribute : Attribute {}

/// <summary>
/// Marks a struct or class suitable from being serialized or deserialized.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class BorshObjectAttribute : BorshAttribute {}

/// <summary>
/// Prevents a property from being serialized or deserialized.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshIgnoreAttribute : BorshAttribute { }

/// <summary>
/// Marks a property as BORSH-optional.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshOptionalAttribute : BorshAttribute { }

/// <summary>
/// Marks a type constructor to be used on deserialization.
/// </summary>
[AttributeUsage(AttributeTargets.Constructor)]
public sealed class BorshConstructorAttribute : BorshAttribute { }

/// <summary>
/// Marks the serialization position of a property.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshPropertyOrderAttribute : BorshAttribute
{
    /// <summary>
    /// Gets the serialization position of a marked property.
    /// </summary>
    public Byte Order { get; }

    /// <summary>
    /// Creates an instance of the <see cref="BorshPropertyOrderAttribute"/> class.
    /// </summary>
    /// <param name="order">The serialization order position.</param>
    public BorshPropertyOrderAttribute(Byte order) =>
        this.Order = order;
}

/// <summary>
/// Marks an array property as fixed array with the specified length.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class BorshFixedArrayAttribute : BorshAttribute
{
    /// <summary>
    /// Gets the specified array length.
    /// </summary>
    public Int32 Length { get; }

    /// <summary>
    /// Creates an instance of the <see cref="BorshFixedArrayAttribute"/> class.
    /// </summary>
    /// <param name="length">The specified array length of a marked property.</param>
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

/// <summary>
/// Allows to specify a case for a union type. 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
public sealed class BorshUnionAttribute : BorshAttribute
{
    /// <summary>
    /// Gets the serialization order of the provided case.
    /// </summary>
    public Byte Order { get; }

    /// <summary>
    /// Gets the type of the provided case.
    /// </summary>
    public Type CaseType { get; }

    /// <summary>
    /// Creates an instance of the <see cref="BorshUnionAttribute"/> class.
    /// </summary>
    /// <param name="order">The serialization order of a provided case.</param>
    /// <param name="caseType">The type of a provided case.</param>
    public BorshUnionAttribute(Byte order, Type caseType)
    {
        this.Order = order;
        this.CaseType = caseType;
    }
}
