using System.Buffers.Binary;
using System.Reflection;
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
            var tmp = ReverseEndianness(value);
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
            var tmp = ReverseEndianness(value);
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
            ? ReverseEndianness(MemoryMarshal.Read<UInt128>(source))
            : MemoryMarshal.Read<UInt128>(source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int128 ReadInt128LittleEndian(ReadOnlySpan<Byte> source) =>
        !BitConverter.IsLittleEndian
            ? ReverseEndianness(MemoryMarshal.Read<Int128>(source))
            : MemoryMarshal.Read<Int128>(source);

    internal static Int128 ReverseEndianness(Int128 value) =>
        (Int128)(ReverseEndiannessInt128.Invoke(null, new Object[] { value })
                 ?? throw new InvalidOperationException());

    internal static UInt128 ReverseEndianness(UInt128 value) =>
        (UInt128)(ReverseEndiannessUInt128.Invoke(null, new Object[] { value })
                  ?? throw new InvalidOperationException());

    internal static readonly Type BinaryPrimitivesType = 
        typeof(BinaryPrimitives);

    internal static readonly MethodInfo ReverseEndiannessInt128 =
        BinaryPrimitivesType.GetMethod(
            "ReverseEndianness",
            BindingFlags.Static | BindingFlags.NonPublic,
            new[] { typeof(Int128) })
        ?? throw new InvalidOperationException();

    internal static readonly MethodInfo ReverseEndiannessUInt128 =
        BinaryPrimitivesType.GetMethod(
            "ReverseEndianness",
            BindingFlags.Static | BindingFlags.NonPublic,
            new[] { typeof(UInt128) })
        ?? throw new InvalidOperationException();
}
