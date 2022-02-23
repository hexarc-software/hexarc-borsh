using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class TupleSerializationTests
{
    [Test]
    public void TupleT1Serialization_ShouldMatchExpectation()
    {
        var value = new ValueTuple<Int32>(0);
        var expected = new Byte[] { 0, 0, 0, 0 };
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TupleT15Serialization_ShouldMatchExpectation()
    {
        var value = (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        var expected = new Byte[15 * 4];
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TupleT15Deserialization_ShouldMatchExpectation()
    {
        var value = new Byte[15 * 4];
        var expected = (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        var result = BorshSerializer.Deserialize<(Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32, Int32)>(value);
        Assert.AreEqual(expected, result);
    }
}
