using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public sealed class NullableSerializationTests
{
    [TestCase(null, new Byte[] { 0 })]
    [TestCase(1, new Byte[] { 1, 1, 0, 0, 0 })]
    public void NullableInt32Serialization_ShouldMatchExpectation(Int32? value, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }

    [TestCase(new Byte[] { 0 }, null)]
    [TestCase(new Byte[] { 1, 1, 0, 0, 0 }, 1)]
    public void NullableInt32Deserialization_ShouldMatchExpectation(Byte[] bytes, Int32? expected)
    {
        var result = BorshSerializer.Deserialize<Int32?>(bytes);
        Assert.AreEqual(expected, result);
    }
}
