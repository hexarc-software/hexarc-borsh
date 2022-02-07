using System;
using System.Buffers;
using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

public class ConverterTests
{
    [TestCase("", new Byte[] { 0, 0, 0, 0 })]
    [TestCase("Test", new Byte[] { 4, 0, 0, 0, 84, 101, 115, 116 })]
    [TestCase("🤡💣🐷", new Byte[] { 12, 0, 0, 0, 240, 159, 164, 161, 240, 159, 146, 163, 240, 159, 144, 183 })]
    public void String_WriteBasicValue_ShouldNotFail(String source, Byte[] target)
    {
        var converter = new StringConverter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(true, new Byte[] { 1 })]
    [TestCase(false, new Byte[] { 0 })]
    public void Boolean_WriteValue_ShouldNotFail(Boolean source, Byte[] target)
    {
        var converter = new BooleanConverter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(1, new Byte[] { 1 })]
    [TestCase(0, new Byte[] { 0 })]
    public void Byte_WriteValue_ShouldNotFail(Byte source, Byte[] target)
    {
        var converter = new ByteConverter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(127, new Byte[] { 127 })]
    [TestCase(-128, new Byte[] { 128 })]
    public void SByte_WriteValue_ShouldNotFail(SByte source, Byte[] target)
    {
        var converter = new SByteConverter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32, new Byte[] { 32, 0 })]
    [TestCase(-512, new Byte[] { 0, 254 })]
    public void Int16_WriteValue_ShouldNotFail(Int16 source, Byte[] target)
    {
        var converter = new Int16Converter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase((UInt16)32, new Byte[] { 32, 0 })]
    public void UInt16_WriteValue_ShouldNotFail(UInt16 source, Byte[] target)
    {
        var converter = new UInt16Converter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32, new Byte[] { 32, 0, 0, 0 })]
    [TestCase(-512, new Byte[] { 0, 254, 255, 255 })]
    public void Int32_WriteValue_ShouldNotFail(Int32 source, Byte[] target)
    {
        var converter = new Int32Converter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32u, new Byte[] { 32, 0, 0, 0 })]
    public void UInt32_WriteValue_ShouldNotFail(UInt32 source, Byte[] target)
    {
        var converter = new UInt32Converter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32, new Byte[] { 32, 0, 0, 0, 0, 0, 0, 0 })]
    [TestCase(-512, new Byte[] { 0, 254, 255, 255, 255, 255, 255, 255 })]
    public void Int64_WriteValue_ShouldNotFail(Int64 source, Byte[] target)
    {
        var converter = new Int64Converter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32u, new Byte[] { 32, 0, 0, 0, 0, 0, 0, 0 })]
    public void UInt64_WriteValue_ShouldNotFail(UInt64 source, Byte[] target)
    {
        var converter = new UInt64Converter();
        var writer = new ArrayBufferWriter<Byte>();
        converter.Write(writer, source);
        var output = writer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }
}
