using NUnit.Framework;
using Hexarc.Borsh.Serialization;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public sealed class UnionSerializationTests
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

    [BorshObject]
    [BorshUnion(0, typeof(Circle))]
    [BorshUnion(1, typeof(Square))]
    public abstract class Figure {}

    [BorshObject]
    public sealed class Circle : Figure
    {
        [BorshPropertyOrder(1)]
        public required Int32 Radius { get; init; }
    }

    [BorshObject]
    public sealed class Square : Figure
    {
        [BorshPropertyOrder(0)]
        public required Int32 SideSize { get; init; }
    }
}
