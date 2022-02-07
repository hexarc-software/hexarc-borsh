using System;
using System.Buffers;
using NUnit.Framework;
using Hexarc.Borsh.Serialization.Metadata;

namespace Hexarc.Borsh.Tests;

public class ConverterTests
{
    [TestCase("", new Byte[] { 0, 0, 0, 0 })]
    [TestCase("Test", new Byte[] { 4, 0, 0, 0, 84, 101, 115, 116 })]
    [TestCase("🤡💣🐷", new Byte[] { 12, 0, 0, 0, 240, 159, 164, 161, 240, 159, 146, 163, 240, 159, 144, 183 })]
    public void String_WriteBasicValue_ShouldNotFail(String source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.StringConverter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(true, new Byte[] { 1 })]
    [TestCase(false, new Byte[] { 0 })]
    public void Boolean_WriteValue_ShouldNotFail(Boolean source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.BooleanConverter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(1, new Byte[] { 1 })]
    [TestCase(0, new Byte[] { 0 })]
    public void Byte_WriteValue_ShouldNotFail(Byte source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.ByteConverter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(127, new Byte[] { 127 })]
    [TestCase(-128, new Byte[] { 128 })]
    public void SByte_WriteValue_ShouldNotFail(SByte source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.SByteConverter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32, new Byte[] { 32, 0 })]
    [TestCase(-512, new Byte[] { 0, 254 })]
    public void Int16_WriteValue_ShouldNotFail(Int16 source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.Int16Converter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase((UInt16)32, new Byte[] { 32, 0 })]
    public void UInt16_WriteValue_ShouldNotFail(UInt16 source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.UInt16Converter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32, new Byte[] { 32, 0, 0, 0 })]
    [TestCase(-512, new Byte[] { 0, 254, 255, 255 })]
    public void Int32_WriteValue_ShouldNotFail(Int32 source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.Int32Converter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32u, new Byte[] { 32, 0, 0, 0 })]
    public void UInt32_WriteValue_ShouldNotFail(UInt32 source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.UInt32Converter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32, new Byte[] { 32, 0, 0, 0, 0, 0, 0, 0 })]
    [TestCase(-512, new Byte[] { 0, 254, 255, 255, 255, 255, 255, 255 })]
    public void Int64_WriteValue_ShouldNotFail(Int64 source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.Int64Converter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(32u, new Byte[] { 32, 0, 0, 0, 0, 0, 0, 0 })]
    public void UInt64_WriteValue_ShouldNotFail(UInt64 source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.UInt64Converter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [TestCase(Single.PositiveInfinity, new Byte[] { 0, 0, 128, 127 })]
    [TestCase(1.1f, new Byte[] { 205, 204, 140, 63 })]
    public void Single_WriteValue_ShouldNotFail(Single source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.SingleConverter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [Test]
    public void Single_WriteNanValue_ShouldThrowArgumentException()
    {
        var writer = new BorshWriter(new ArrayBufferWriter<Byte>());
        Assert.Catch<ArgumentException>(() =>
            BorshMetadataServices.SingleConverter.Write(writer, Single.NaN, new BorshSerializerOptions()));
    }

    [TestCase(Double.PositiveInfinity, new Byte[] { 0, 0, 0, 0, 0, 0, 240, 127 })]
    [TestCase(1.1d, new Byte[] { 154, 153, 153, 153, 153, 153, 241, 63 })]
    public void Double_WriteValue_ShouldNotFail(Double source, Byte[] target)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        BorshMetadataServices.DoubleConverter.Write(writer, source, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(target, output);
    }

    [Test]
    public void Double_WriteNanValue_ShouldThrowArgumentException()
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        Assert.Catch<ArgumentException>(() =>
            BorshMetadataServices.DoubleConverter.Write(writer, Double.NaN, new BorshSerializerOptions()));
    }
}
