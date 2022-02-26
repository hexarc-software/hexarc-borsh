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
    [BorshObject]
    public abstract class Figure {}

    [BorshObject]
    public sealed class Circle : Figure
    {
        [BorshPropertyOrder(1)]
        public Int32 Radius { get; init; }
    }

    [BorshObject]
    public sealed class Square : Figure
    {
        [BorshPropertyOrder(0)]
        public Int32 SideSize { get; init; }
    }
}
