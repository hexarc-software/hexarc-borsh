using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Text;
using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

public class PrimitiveTests
{
    [Test]
    public void ToBytes()
    {
        const String str = "🤡";
        var length = Encoding.UTF8.GetByteCount(str);

        var span = new Span<Byte>(new Byte[sizeof(Int32) + length]);
        BinaryPrimitives.WriteInt32LittleEndian(span[..4], length);
        Encoding.UTF8.GetBytes(str, span[4..]);

        Console.WriteLine(span.ToArray().Dump(DumpStyle.CSharp));
    }

    [Test]
    public void WriteTwoInt32()
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var integers = new[] { 1, 2 };
        foreach (var x in integers)
        {
            var span = buffer.GetSpan(4);
            buffer.Advance(4);
            BinaryPrimitives.WriteInt32LittleEndian(span, x);
        }
        Console.WriteLine(buffer.WrittenMemory.ToArray().Dump(DumpStyle.CSharp));
    }

    [Test]
    public void WriteTwoString()
    {
        var values = new[] { "alpha", "zulu" };
        var buffer = new ArrayBufferWriter<Byte>();
        foreach (var x in values)
        {
            x.WriteToBuffer(buffer);
        }
        //[5, 0, 0, 0, 97, 108, 112, 104, 97, 4, 0, 0, 0, 122, 117, 108, 117]
        Console.WriteLine(buffer.WrittenMemory.ToArray().Dump(DumpStyle.CSharp));
    }
}

public static class StringExt
{
    public static void WriteToBuffer(this String value, ArrayBufferWriter<Byte> buffer)
    {
        const Int32 sizeByteCount = sizeof(Int32);
        var valueByteCount = Encoding.UTF8.GetByteCount(value);
        var neededByteCount = sizeByteCount + valueByteCount;
        var span = buffer.GetSpan(neededByteCount);

        BinaryPrimitives.WriteInt32LittleEndian(span[..sizeByteCount], valueByteCount);
        Encoding.UTF8.GetBytes(value, span[sizeByteCount..]);
        buffer.Advance(neededByteCount);
    }
}
