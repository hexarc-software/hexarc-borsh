using System;
using System.Buffers;
using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

public class ConverterTests
{
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

    [TestCase(32, new Byte[] { 32, 0, 0, 0 })]
    public void Int32_WriteValue_ShouldNotFail(Int32 source, Byte[] target)
    {
        var converter = new Int32Converter();
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
}
