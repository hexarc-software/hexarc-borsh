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
}
