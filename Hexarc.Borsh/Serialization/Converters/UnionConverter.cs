using System.Reflection;

namespace Hexarc.Borsh.Serialization.Converters;

public sealed class UnionConverter<T> : BorshConverter<T> where T : class
{
    private readonly Dictionary<Type, Byte> _caseTypeOrders;
    private readonly Dictionary<Byte, BorshConverter> _caseConverters;

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

    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        var type = value.GetType();
        var order = this._caseTypeOrders[type];
        writer.WriteByte(order);
        this._caseConverters[order].WriteCoreAsObject(writer, value, options);
    }

    public override T Read(ref BorshReader reader, BorshSerializerOptions options)
    {
        var key = reader.ReadByte();
        return this._caseConverters[key].ReadCoreAsObject(ref reader, options) as T ?? throw new InvalidOperationException();
    }
}
