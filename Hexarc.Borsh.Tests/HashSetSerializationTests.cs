using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class HashSetSerializationTests
{
    [TestCase(new Int32[] {}, new Byte[] { 0, 0, 0, 0 })]
    [TestCase(new [] { 1 }, new Byte[] { 1, 0, 0, 0, 1, 0, 0, 0 })]
    public void HashSetSerialization_ShouldMatchExpectation(Int32[] array, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(new HashSet<Int32>(array));
        Assert.AreEqual(expected, result);
    }

    [TestCase(new Byte[] { 0, 0, 0, 0 }, new Int32[] { })]
    [TestCase(new Byte[] { 1, 0, 0, 0, 1, 0, 0, 0 }, new [] { 1 })]
    public void ArrayDeserialization_ShouldMatchExpectation(Byte[] bytes, Int32[] expected)
    {
        var result = BorshSerializer.Deserialize<Byte[]>(bytes);
        Assert.AreEqual(new HashSet<Int32>(expected), result);
    }
}
