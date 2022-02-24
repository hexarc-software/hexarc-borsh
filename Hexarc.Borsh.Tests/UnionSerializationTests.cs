using Hexarc.Borsh.Serialization;
using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class UnionSerializationTests
{
    [Test]
    public void UnionSerialization_ShouldMatchExpectation()
    {
        Figure square = new Square { SideSize = 1 };
        var expected = new Byte[] { 1, 1, 0, 0, 0 };
        var raw = BorshSerializer.Serialize(square);
        Assert.AreEqual(expected, raw);
    }

    [Test]
    public void UnionDeserialization_ShouldMatchExpectation()
    {
        var raw = new Byte[] { 1, 1, 0, 0, 0 };
        var restored = BorshSerializer.Deserialize<Figure>(raw);
        Assert.IsTrue(restored is Square { SideSize: 1 });
    }

    [BorshUnion(0, typeof(Circle))]
    [BorshUnion(1, typeof(Square))]
    public abstract class Figure {}

    public sealed class Circle : Figure
    {
        public Int32 Radius { get; init; }
    }

    public sealed class Square : Figure
    {
        public Int32 SideSize { get; init; }
    }
}
