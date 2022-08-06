namespace Hexarc.Borsh.Serialization;

/// <summary>
/// Converts an object or value to or from BORSH.
/// </summary>
public abstract class BorshConverter
{
    internal BorshConverter() { }

    internal abstract Type TypeToConvert { get; }

    /// <summary>
    /// Determines whether the specified type can be converted.
    /// </summary>
    /// <param name="type">The type to compare against.</param>
    /// <returns>
    /// True if the type can be converted, False otherwise.
    /// </returns>
    public abstract Boolean CanConvert(Type type);

    internal abstract void WriteCoreAsObject(
        BorshWriter writer,
        Object value,
        BorshSerializerOptions options);

    internal abstract Object ReadCoreAsObject(
        ref BorshReader reader,
        BorshSerializerOptions options);
}
