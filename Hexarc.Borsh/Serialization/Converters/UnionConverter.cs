using System.Reflection;

namespace Hexarc.Borsh.Serialization.Converters;

/// <summary>
/// Provides serialization for <see cref="UnionConverter{T}"/> objects.
/// </summary>
/// <typeparam name="T">The union annotated type for serialization.</typeparam>
public sealed class UnionConverter<T> : BorshConverter<T> where T : class
{
    private readonly Dictionary<Type, Byte> _caseTypeOrders;
    private readonly Dictionary<Byte, BorshConverter> _caseConverters;

    /// <summary>
    /// Creates an instance of the <see cref="UnionConverter{T}"/> class.
    /// </summary>
    /// <param name="options">The serialization options.</param>
    /// <exception cref="InvalidOperationException">Throws if the provided type is misconfigured.</exception>
    public UnionConverter(BorshSerializerOptions options)
    {
        var type = typeof(T);
        this._caseTypeOrders = type.GetCustomAttributes<BorshUnionAttribute>()
            .ToDictionary(x => x.CaseType, x => x.Order);
        this._caseConverters = this._caseTypeOrders
            .ToDictionary(
                x => x.Value,
                x => options.GetConverter(x.Key) ?? throw new InvalidOperationException());
    }

    /// <inheritdoc />
    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        var type = value.GetType();
        var order = this._caseTypeOrders[type];
        writer.WriteByte(order);
        this._caseConverters[order].WriteCoreAsObject(writer, value, options);
    }

    /// <inheritdoc />
    public override T Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var key = reader.ReadByte();
        return this._caseConverters[key].ReadCoreAsObject(ref reader, options) as T ?? throw new InvalidOperationException();
    }
}
