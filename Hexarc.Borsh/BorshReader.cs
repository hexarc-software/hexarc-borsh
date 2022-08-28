using System.Runtime.CompilerServices;
using Hexarc.Borsh.Serialization;
using static System.Buffers.Binary.BinaryPrimitives;

namespace Hexarc.Borsh;

/// <summary>
/// Providers an API for reading BORSH-compatible primitives.
/// </summary>
public ref struct BorshReader
{
    private readonly ReadOnlySpan<Byte> _buffer;

    private Int32 _bufferIndex;

    /// <summary>
    /// Creates an instance of the <see cref="BorshReader"/> class.
    /// </summary>
    /// <param name="borshData"></param>
    public BorshReader(ReadOnlySpan<Byte> borshData)
    {
        this._buffer = borshData;
        this._bufferIndex = 0;
    }

    /// <summary>
    /// Reads a <see cref="Boolean"/> value from the buffer.
    /// </summary>
    /// <returns>A boolean value.</returns>
    public Boolean ReadBoolean() =>
        this.ReadSpan(sizeof(Boolean))[0] == 1;

    /// <summary>
    /// Reads a <see cref="Byte"/> value from the buffer.
    /// </summary>
    /// <returns>A byte value.</returns>
    public Byte ReadByte() =>
        this.ReadSpan(sizeof(Byte))[0];

    /// <summary>
    /// Reads a <see cref="SByte"/> value from the buffer.
    /// </summary>
    /// <returns>A signed byte value.</returns>
    [CLSCompliant(false)]
    public SByte ReadSByte() =>
        (SByte)this.ReadSpan(sizeof(SByte))[0];

    /// <summary>
    /// Reads a <see cref="Int16"/> value from the buffer.
    /// </summary>
    /// <returns>A 16-bit signed integer value.</returns>
    public Int16 ReadInt16() =>
        ReadInt16LittleEndian(this.ReadSpan(sizeof(Int16)));

    /// <summary>
    /// Reads a <see cref="UInt16"/> value from the buffer.
    /// </summary>
    /// <returns>A 16-bit unsigned integer value.</returns>
    [CLSCompliant(false)]
    public UInt16 ReadUInt16() =>
        ReadUInt16LittleEndian(this.ReadSpan(sizeof(UInt16)));

    /// <summary>
    /// Reads a <see cref="Int32"/> value from the buffer.
    /// </summary>
    /// <returns>A 32-bit signed integer value.</returns>
    public Int32 ReadInt32() =>
        ReadInt32LittleEndian(this.ReadSpan(sizeof(Int32)));

    /// <summary>
    /// Reads a <see cref="UInt32"/> value from the buffer.
    /// </summary>
    /// <returns>A 32-bit unsigned integer value.</returns>
    [CLSCompliant(false)]
    public UInt32 ReadUInt32() =>
        ReadUInt32LittleEndian(this.ReadSpan(sizeof(UInt32)));

    /// <summary>
    /// Reads a <see cref="Int64"/> value from the buffer.
    /// </summary>
    /// <returns>A 64-bit signed integer value.</returns>
    public Int64 ReadInt64() =>
        ReadInt64LittleEndian(this.ReadSpan(sizeof(Int64)));

    /// <summary>
    /// Reads a <see cref="UInt64"/> value from the buffer.
    /// </summary>
    /// <returns>A 64-bit unsigned integer value.</returns>
    [CLSCompliant(false)]
    public UInt64 ReadUInt64() =>
        ReadUInt64LittleEndian(this.ReadSpan(sizeof(UInt64)));

    /// <summary>
    /// Reads a <see cref="Half"/> value from the buffer.
    /// </summary>
    /// <returns>A half-precision floating-point value.</returns>
    public Half ReadHalf() =>
        ReadHalfLittleEndian(this.ReadSpan(2));

    /// <summary>
    /// Reads a <see cref="Single"/> value from the buffer.
    /// </summary>
    /// <returns>A single-precision floating-point value.</returns>
    public Single ReadSingle() =>
        ReadSingleLittleEndian(this.ReadSpan(sizeof(Single)));

    /// <summary>
    /// Reads a <see cref="Double"/> value from the buffer.
    /// </summary>
    /// <returns>A double-precision floating-point value.</returns>
    public Double ReadDouble() =>
        ReadDoubleLittleEndian(this.ReadSpan(sizeof(Double)));

    /// <summary>
    /// Reads a <see cref="String"/> value from the buffer.
    /// </summary>
    /// <returns>A string value.</returns>
    public String ReadString()
    {
        var valueByteCount = this.ReadInt32();
        return Encoding.UTF8.GetString(this.ReadSpan(valueByteCount));
    }

    /// <summary>
    /// Reads a <see cref="DateTime"/> value from the buffer.
    /// </summary>
    /// <returns>A date time value.</returns>
    public DateTime ReadDateTime()
    {
        var timestamp = this.ReadInt64();
        return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).UtcDateTime;
    }

    /// <summary>
    /// Reads a <see cref="Option{T}"/> value from the buffer.
    /// </summary>
    /// <param name="converter">The converter for the underlying value.</param>
    /// <param name="options">The serialization options.</param>
    /// <typeparam name="T">The option underlying type.</typeparam>
    /// <returns>An optional value.</returns>
    public Option<T> ReadOption<T>(
        BorshConverter<T> converter,
        BorshSerializerOptions options) where T : class
    {
        var @case = this.ReadByte();
        if (@case == 0)
        {
            return Option<T>.None();
        }
        else
        {
            var value = converter.ReadCore(ref this, options);
            return Option<T>.Some(value);
        }
    }

    /// <summary>
    /// Reads a <see cref="Option{T}"/> value from the buffer in indirect operations.
    /// </summary>
    /// <param name="converter">The converter for the underlying value.</param>
    /// <param name="options">The serialization options.</param>
    /// <typeparam name="T">The option underlying type.</typeparam>
    /// <returns>An optional value.</returns>
    public T? ReadIndirectOption<T>(
        BorshConverter<T> converter,
        BorshSerializerOptions options) where T : class
    {
        var @case = this.ReadByte();
        if (@case == 0)
        {
            return default;
        }
        else
        {
            return converter.ReadCore(ref this, options);
        }
    }

    /// <summary>
    /// Reads a <see cref="Nullable{T}"/> value from the buffer.
    /// </summary>
    /// <param name="converter">The converter for the underlying value.</param>
    /// <param name="options">The serialization options.</param>
    /// <typeparam name="T">The nullable underlying type.</typeparam>
    /// <returns>A nullable value.</returns>
    public T? ReadNullable<T>(
        BorshConverter<T> converter,
        BorshSerializerOptions options) where T : struct
    {
        var @case = this.ReadByte();
        if (@case == 0)
        {
            return default;
        }
        else
        {
            return converter.ReadCore(ref this, options);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ReadOnlySpan<Byte> ReadSpan(Int32 size)
    {
        var span = this._buffer.Slice(this._bufferIndex, size);
        this._bufferIndex += size;
        return span;
    }
}
