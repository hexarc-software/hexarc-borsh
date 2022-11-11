using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Hexarc.Borsh;

internal static class BinaryPrimitivesExt
{
    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteUInt128LittleEndian(Span<Byte> destination, UInt128 value)
    {
        if (!BitConverter.IsLittleEndian)
        {
            MemoryMarshal.Write(destination, ref value);
            var tmp = ReverseEndiannessUInt128(
                MemoryMarshal.Read<UInt64>(destination[..8]),
                MemoryMarshal.Read<UInt64>(destination[8..]));
            MemoryMarshal.Write(destination, ref tmp);
        }
        else
        {
            MemoryMarshal.Write(destination, ref value);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteInt128LittleEndian(Span<Byte> destination, Int128 value)
    {
        if (!BitConverter.IsLittleEndian)
        {
            MemoryMarshal.Write(destination, ref value);
            var tmp = ReverseEndiannessUInt128(
                MemoryMarshal.Read<UInt64>(destination[..8]),
                MemoryMarshal.Read<UInt64>(destination[8..]));
            MemoryMarshal.Write(destination, ref tmp);
        }
        else
        {
            MemoryMarshal.Write(destination, ref value);
        }
    }

    [CLSCompliant(false)]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UInt128 ReadUInt128LittleEndian(ReadOnlySpan<Byte> source) =>
        !BitConverter.IsLittleEndian
            ? ReverseEndiannessUInt128(
                MemoryMarshal.Read<UInt64>(source[..8]),
                MemoryMarshal.Read<UInt64>(source[8..]))
            : MemoryMarshal.Read<UInt128>(source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int128 ReadInt128LittleEndian(ReadOnlySpan<Byte> source) =>
        !BitConverter.IsLittleEndian
            ? ReverseEndiannessInt128(
                MemoryMarshal.Read<UInt64>(source[..8]),
                MemoryMarshal.Read<UInt64>(source[8..]))
            : MemoryMarshal.Read<Int128>(source);

    internal static Int128 ReverseEndiannessInt128(UInt64 lower, UInt64 upper) =>
        new(BinaryPrimitives.ReverseEndianness(lower), BinaryPrimitives.ReverseEndianness(upper));

    internal static UInt128 ReverseEndiannessUInt128(UInt64 lower, UInt64 upper) =>
        new(BinaryPrimitives.ReverseEndianness(lower), BinaryPrimitives.ReverseEndianness(upper));
}
