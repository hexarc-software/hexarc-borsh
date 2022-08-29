namespace Hexarc.Borsh.Serialization;

/// <summary>
/// The base class of all BORSH converter factories.
/// </summary>
public abstract class BorshConverterFactory : BorshConverter
{
    /// <summary>
    /// Creates a converter for a requested type.
    /// </summary>
    /// <param name="type">The type to create a converter for.</param>
    /// <param name="options">The serialization options.</param>
    /// <returns>
    /// A converter that is compatible with the requested type.
    /// </returns>
    public abstract BorshConverter CreateConverter(Type type, BorshSerializerOptions options);

    internal override Type TypeToConvert => null!;

    internal sealed override void WriteCoreAsObject(BorshWriter writer, Object value, BorshSerializerOptions options) =>
        throw new InvalidOperationException();

    internal sealed override Object ReadCoreAsObject(ref BorshReader reader, BorshSerializerOptions options) =>
        throw new InvalidOperationException();
}
