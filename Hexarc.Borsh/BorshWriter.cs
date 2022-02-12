using Hexarc.Borsh.Serialization;
using Hexarc.Borsh.Serialization.Metadata;

namespace Hexarc.Borsh;

public sealed class BorshWriter
{
    private readonly IBufferWriter<Byte> _output;

    public BorshWriter(IBufferWriter<Byte> bufferWriter)
    {
        this._output = bufferWriter;
    }

    public void WriteBoolean(Boolean value)
    {
        var span = this._output.GetSpan(Constants.BooleanSize);
        span[0] = (Byte)(value ? 1 : 0);
        this._output.Advance(Constants.BooleanSize);
    }

    public void WriteByte(Byte value)
    {
        var span = this._output.GetSpan(Constants.ByteSize);
        span[0] = value;
        this._output.Advance(Constants.ByteSize);
    }

    public void WriteSByte(SByte value)
    {
        var span = this._output.GetSpan(Constants.SByteSize);
        span[0] = (Byte)value;
        this._output.Advance(Constants.SByteSize);
    }

    public void WriteInt16(Int16 value)
    {
        var span = this._output.GetSpan(Constants.Int16Size);
        BinaryPrimitives.WriteInt16LittleEndian(span, value);
        this._output.Advance(Constants.Int16Size);
    }

    public void WriteUInt16(UInt16 value)
    {
        var span = this._output.GetSpan(Constants.UInt16Size);
        BinaryPrimitives.WriteUInt16LittleEndian(span, value);
        this._output.Advance(Constants.UInt16Size);
    }

    public void WriteInt32(Int32 value)
    {
        var span = this._output.GetSpan(Constants.Int32Size);
        BinaryPrimitives.WriteInt32LittleEndian(span, value);
        this._output.Advance(Constants.Int32Size);
    }

    public void WriteUInt32(UInt32 value)
    {
        var span = this._output.GetSpan(Constants.UInt32Size);
        BinaryPrimitives.WriteUInt32LittleEndian(span, value);
        this._output.Advance(Constants.UInt32Size);
    }

    public void WriteInt64(Int64 value)
    {
        var span = this._output.GetSpan(Constants.Int64Size);
        BinaryPrimitives.WriteInt64LittleEndian(span, value);
        this._output.Advance(Constants.Int64Size);
    }

    public void WriteUInt64(UInt64 value)
    {
        var span = this._output.GetSpan(Constants.UInt64Size);
        BinaryPrimitives.WriteUInt64LittleEndian(span, value);
        this._output.Advance(Constants.UInt64Size);
    }

    public void WriteSingle(Single value)
    {
        if (Single.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }

        var span = this._output.GetSpan(Constants.SingleSize);
        BinaryPrimitives.WriteSingleLittleEndian(span, value);
        this._output.Advance(Constants.SingleSize);
    }

    public void WriteDouble(Double value)
    {
        if (Double.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }

        var span = this._output.GetSpan(Constants.DoubleSize);
        BinaryPrimitives.WriteDoubleLittleEndian(span, value);
        this._output.Advance(Constants.DoubleSize);
    }

    public void WriteString(String value)
    {
        var valueByteCount = Encoding.UTF8.GetByteCount(value);
        this.WriteInt32(valueByteCount);

        var span = this._output.GetSpan(valueByteCount);
        Encoding.UTF8.GetBytes(value, span);
        this._output.Advance(valueByteCount);
    }

    public void WriteOption<T>(
        T? value,
        BorshConverter<T> converter,
        BorshSerializerOptions options) where T : class
    {
        if (value is null)
        {
            this.WriteByte(0);
        }
        else
        {
            this.WriteByte(1);
            converter.Write(this, value, options);
        }
    }

    public void WriteOption<T>(
        T? value,
        BorshConverter<T> converter,
        BorshSerializerOptions options) where T : struct
    {
        if (value is null)
        {
            this.WriteByte(0);
        }
        else
        {
            this.WriteByte(1);
            converter.Write(this, value.Value, options);
        }
    }
}
