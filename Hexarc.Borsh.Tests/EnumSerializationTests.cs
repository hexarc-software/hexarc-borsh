using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class EnumSerializationTests
{
    public enum Numbers
    {
        One,
        Two,
        Three
    }

    [TestCase(Numbers.One, new Byte[] { 0 })]
    [TestCase(Numbers.Two, new Byte[] { 1 })]
    [TestCase(Numbers.Three, new Byte[] { 2 })]
    public void BasicEnumSerialization_ShouldMatchExpectation(Numbers value, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }

    [TestCase(new Byte[] { 0 }, Numbers.One)]
    [TestCase(new Byte[] { 1 }, Numbers.Two)]
    [TestCase(new Byte[] { 2 }, Numbers.Three)]
    public void BasicEnumDeserialization_ShouldMatchExpectation(Byte[] bytes, Numbers expected)
    {
        var result = BorshSerializer.Deserialize<Numbers>(bytes);
        Assert.AreEqual(expected, result);
    }
}
