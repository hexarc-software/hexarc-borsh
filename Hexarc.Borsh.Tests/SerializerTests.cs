using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

public class SerializerTests
{
    [TestCase(1, new Byte[] { 1 })]
    public void SerializeByte_ShouldMatchExpectation(Byte value, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }

    [TestCase(341, new Byte[] { 85, 1, 0, 0 })]
    public void SerializeInt32_ShouldMatchExpectation(Int32 value, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }

    [TestCase("🤡💣🐷", new Byte[] { 12, 0, 0, 0, 240, 159, 164, 161, 240, 159, 146, 163, 240, 159, 144, 183 })]
    public void SerializeString_ShouldMatchExpectation(String value, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }
}
