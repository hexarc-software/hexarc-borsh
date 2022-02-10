using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class NullableSerializationTests
{
    [TestCase(null, new Byte[] { 0 })]
    [TestCase(1, new Byte[] { 1, 1, 0, 0, 0 })]
    public void NullableInt32Serialization_ShouldMatchExpectation(Int32? value, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }
}
