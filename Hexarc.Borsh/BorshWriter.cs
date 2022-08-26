using System.Runtime.CompilerServices;
using Hexarc.Borsh.Serialization;
using static System.Buffers.Binary.BinaryPrimitives;

namespace Hexarc.Borsh;

/// <summary>
/// Providers a API for writing BORSH-compatible primitives.
/// </summary>
public sealed class BorshWriter
{
    private readonly IBufferWriter<Byte> _output;

    /// <summary>
    /// Creates an instance of the <see cref="BorshWriter"/> class.
    /// </summary>
    /// <param name="bufferWriter">The byte buffer to write data into.</param>
    public BorshWriter(IBufferWriter<Byte> bufferWriter) =>
        this._output = bufferWriter;

    /// <summary>
    /// Writes a <see cref="Boolean"/> value into the buffer.
    /// </summary>
    /// <param name="value">The boolean value to write.</param>
    public void WriteBoolean(Boolean value) =>
        this.AllocateSpan(sizeof(Boolean))[0] = (Byte)(value ? 1 : 0);

    /// <summary>
    /// Writes a <see cref="Byte"/> value into the buffer.
    /// </summary>
    /// <param name="value">The byte value to write.</param>
    public void WriteByte(Byte value) =>
        this.AllocateSpan(sizeof(Byte))[0] = value;

    /// <summary>
    /// Writes a <see cref="SByte"/> value into the buffer.
    /// </summary>
    /// <param name="value">The signed byte value to write.</param>
    [CLSCompliant(false)]
    public void WriteSByte(SByte value) =>
        this.AllocateSpan(sizeof(SByte))[0] = (Byte)value;

    /// <summary>
    /// Writes a <see cref="Int16"/> value into the buffer.
    /// </summary>
    /// <param name="value">The 16-bit signed integer value to write.</param>
    public void WriteInt16(Int16 value) =>
        WriteInt16LittleEndian(this.AllocateSpan(sizeof(Int16)), value);

    /// <summary>
    /// Writes a <see cref="UInt16"/> value into the buffer.
    /// </summary>
    /// <param name="value">The 16-bit unsigned integer value to write.</param>
    [CLSCompliant(false)]
    public void WriteUInt16(UInt16 value) =>
        WriteUInt16LittleEndian(this.AllocateSpan(sizeof(UInt16)), value);

    /// <summary>
    /// Writes a <see cref="Int32"/> value into the buffer.
    /// </summary>
    /// <param name="value">The 32-bit signed integer value to write.</param>
    public void WriteInt32(Int32 value) =>
        WriteInt32LittleEndian(this.AllocateSpan(sizeof(Int32)), value);

    /// <summary>
    /// Writes a <see cref="UInt32"/> value into the buffer.
    /// </summary>
    /// <param name="value">The 32-bit unsigned integer value to write.</param>
    [CLSCompliant(false)]
    public void WriteUInt32(UInt32 value) =>
        WriteUInt32LittleEndian(this.AllocateSpan(sizeof(UInt32)), value);

    /// <summary>
    /// Writes a <see cref="Int64"/> value into the buffer.
    /// </summary>
    /// <param name="value">The 64-bit signed integer value to write.</param>
    public void WriteInt64(Int64 value) =>
        WriteInt64LittleEndian(this.AllocateSpan(sizeof(Int64)), value);

    /// <summary>
    /// Writes a <see cref="UInt64"/> value into the buffer.
    /// </summary>
    /// <param name="value">The 64-bit unsigned integer value to write.</param>
    [CLSCompliant(false)]
    public void WriteUInt64(UInt64 value) =>
        WriteUInt64LittleEndian(this.AllocateSpan(sizeof(UInt64)), value);

    /// <summary>
    /// Writes a <see cref="Half"/> value into the buffer.
    /// </summary>
    /// <param name="value">The the half-precision floating-point value to write.</param>
    public void WriteHalf(Half value)
    {
        if (Half.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }
        WriteHalfLittleEndian(this.AllocateSpan(2), value);
    }

    /// <summary>
    /// Writes a <see cref="Single"/> value into the buffer.
    /// </summary>
    /// <param name="value">The the single-precision floating-point value to write.</param>
    public void WriteSingle(Single value)
    {
        if (Single.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }
        WriteSingleLittleEndian(this.AllocateSpan(sizeof(Single)), value);
    }

    /// <summary>
    /// Writes a <see cref="Double"/> value into the buffer.
    /// </summary>
    /// <param name="value">The the double-precision floating-point value to write.</param>
    public void WriteDouble(Double value)
    {
        if (Double.IsNaN(value))
        {
            throw new ArgumentException("NaN cannot be written as valid BORSH", nameof(value));
        }
        WriteDoubleLittleEndian(this.AllocateSpan(sizeof(Double)), value);
    }

    /// <summary>
    /// Writes a <see cref="String"/> value into the buffer.
    /// </summary>
    /// <param name="value">The the string value to write.</param>
    public void WriteString(String value)
    {
        var valueByteCount = Encoding.UTF8.GetByteCount(value);
        this.WriteInt32(valueByteCount);
        Encoding.UTF8.GetBytes(value, this.AllocateSpan(valueByteCount));
    }

    /// <summary>
    /// Writes a <see cref="DateTime"/> value into the buffer.
    /// </summary>
    /// <param name="value">The the date time value to write.</param>
    public void WriteDateTime(DateTime value) =>
        this.WriteInt64(((DateTimeOffset)value).ToUnixTimeMilliseconds());

    /// <summary>
    /// Writes a <see cref="Option{T}"/> value into the buffer.
    /// </summary>
    /// <param name="value">The the optional value to write.</param>
    /// <param name="converter">The converter for the underlying option value.</param>
    /// <param name="options">The serialization options.</param>
    /// <typeparam name="T">The option underlying type.</typeparam>
    public void WriteOption<T>(
        Option<T> value,
        BorshConverter<T> converter,
        BorshSerializerOptions options) where T : class
    {
        if (value is Some<T> some)
        {
            this.WriteByte(1);
            converter.Write(this, some.Value, options);
        }
        else
        {
            this.WriteByte(0);
        }
    }

    /// <summary>
    /// Writes a <see cref="Option{T}"/> value into the buffer in an indirect operations.
    /// </summary>
    /// <param name="value">The the optional value to write.</param>
    /// <param name="converter">The converter for the underlying option value.</param>
    /// <param name="options">The serialization options.</param>
    /// <typeparam name="T">The option underlying type.</typeparam>
    public void WriteIndirectOption<T>(
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

    /// <summary>
    /// Writes a <see cref="Nullable{T}"/> value into the buffer.
    /// </summary>
    /// <param name="value">The the nullable value to write.</param>
    /// <param name="converter">The converter for the underlying nullable value.</param>
    /// <param name="options">The serialization options.</param>
    /// <typeparam name="T">The option underlying type.</typeparam>
    public void WriteNullable<T>(
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
