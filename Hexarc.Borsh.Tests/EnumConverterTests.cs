using System.Buffers;
using Hexarc.Borsh.Serialization.Converters;
using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class EnumConverterTests
{
    public enum TestEnum
    {
        TestValue1 = 1,
        TestValue2 = 2
    }

    [TestCase(TestEnum.TestValue1, new Byte[] { 0 })]
    [TestCase(TestEnum.TestValue2, new Byte[] { 1 })]
    public void EnumConverter_ShouldMatchExpectation(TestEnum value, Byte[] bytes)
    {
        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        var factory = new EnumConverterFactory();
        var converter = (factory.CreateConverter(
                typeof(TestEnum), new BorshSerializerOptions()) as EnumConverter<TestEnum>)!;
        converter.Write(writer, value, new BorshSerializerOptions());
        var output = buffer.WrittenMemory.ToArray();
        Assert.AreEqual(bytes, output);
    }
}
