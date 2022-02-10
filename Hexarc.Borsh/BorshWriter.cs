using Hexarc.Borsh.Serialization;

namespace Hexarc.Borsh;

public ref struct BorshReader
{
    private readonly ReadOnlySpan<Byte> _buffer;

    private Int32 _bufferIndex;

    public BorshReader(ReadOnlySpan<Byte> borshData)
    {
        this._buffer = borshData;
        this._bufferIndex = 0;
    }

    public Boolean ReadBoolean()
    {
        const Int32 valueSizeInBytes = 1;
        var span = this._buffer.Slice(_bufferIndex, valueSizeInBytes);
        this._bufferIndex += valueSizeInBytes;
        return span[0] == 1;
    }
}

public sealed class BorshWriter
{
    private readonly IBufferWriter<Byte> _output;

    public BorshWriter(IBufferWriter<Byte> bufferWriter)
    {
        this._output = bufferWriter;
    }

    public void WriteBoolean(Boolean value)
    {
        const Int32 valueSizeInBytes = 1;
        var span = this._output.GetSpan(valueSizeInBytes);
        span[0] = (Byte)(value ? 1 : 0);
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteByte(Byte value)
    {
        const Int32 valueSizeInBytes = 1;
        var span = this._output.GetSpan(valueSizeInBytes);
        span[0] = value;
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteSByte(SByte value)
    {
        const Int32 valueSizeInBytes = 1;
        var span = this._output.GetSpan(valueSizeInBytes);
        span[0] = (Byte)value;
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteInt16(Int16 value)
    {
        const Int32 valueSizeInBytes = 2;
        var span = this._output.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteInt16LittleEndian(span, value);
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteUInt16(UInt16 value)
    {
        const Int32 valueSizeInBytes = 2;
        var span = this._output.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteUInt16LittleEndian(span, value);
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteInt32(Int32 value)
    {
        const Int32 valueSizeInBytes = 4;
        var span = this._output.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteInt32LittleEndian(span, value);
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteUInt32(UInt32 value)
    {
        const Int32 valueSizeInBytes = 4;
        var span = this._output.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteUInt32LittleEndian(span, value);
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteInt64(Int64 value)
    {
        const Int32 valueSizeInBytes = 8;
        var span = this._output.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteInt64LittleEndian(span, value);
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteUInt64(UInt64 value)
    {
        const Int32 valueSizeInBytes = 8;
        var span = this._output.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteUInt64LittleEndian(span, value);
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteSingle(Single value)
    {
        if (Single.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }

        const Int32 valueSizeInBytes = 4;
        var span = this._output.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteSingleLittleEndian(span, value);
        this._output.Advance(valueSizeInBytes);
    }

    public void WriteDouble(Double value)
    {
        if (Double.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }

        const Int32 valueSizeInBytes = 8;
        var span = this._output.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteDoubleLittleEndian(span, value);
        this._output.Advance(valueSizeInBytes);
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
