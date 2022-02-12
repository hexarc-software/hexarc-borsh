using System.Runtime.CompilerServices;
using Hexarc.Borsh.Serialization;
using static System.Buffers.Binary.BinaryPrimitives;

namespace Hexarc.Borsh;

public sealed class BorshWriter
{
    private readonly IBufferWriter<Byte> _output;

    public BorshWriter(IBufferWriter<Byte> bufferWriter) =>
        this._output = bufferWriter;

    public void WriteBoolean(Boolean value) =>
        this.AllocateSpan(sizeof(Boolean))[0] = (Byte)(value ? 1 : 0);

    public void WriteByte(Byte value) =>
        this.AllocateSpan(sizeof(Byte))[0] = value;

    public void WriteSByte(SByte value) =>
        this.AllocateSpan(sizeof(SByte))[0] = (Byte)value;

    public void WriteInt16(Int16 value) =>
        WriteInt16LittleEndian(this.AllocateSpan(sizeof(Int16)), value);

    public void WriteUInt16(UInt16 value) =>
        WriteUInt16LittleEndian(this.AllocateSpan(sizeof(UInt16)), value);

    public void WriteInt32(Int32 value) =>
        WriteInt32LittleEndian(this.AllocateSpan(sizeof(Int32)), value);

    public void WriteUInt32(UInt32 value) =>
        WriteUInt32LittleEndian(this.AllocateSpan(sizeof(UInt32)), value);

    public void WriteInt64(Int64 value) =>
        WriteInt64LittleEndian(this.AllocateSpan(sizeof(Int64)), value);

    public void WriteUInt64(UInt64 value) =>
        WriteUInt64LittleEndian(this.AllocateSpan(sizeof(UInt64)), value);

    public void WriteSingle(Single value)
    {
        if (Single.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }
        WriteSingleLittleEndian(this.AllocateSpan(sizeof(Single)), value);
    }

    public void WriteDouble(Double value)
    {
        if (Double.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }
        WriteDoubleLittleEndian(this.AllocateSpan(sizeof(Double)), value);
    }

    public void WriteString(String value)
    {
        var valueByteCount = Encoding.UTF8.GetByteCount(value);
        this.WriteInt32(valueByteCount);
        Encoding.UTF8.GetBytes(value, this.AllocateSpan(valueByteCount));
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Span<Byte> AllocateSpan(Int32 size)
    {
        var span = this._output.GetSpan(size);
        this._output.Advance(size);
        return span;
    }
}
