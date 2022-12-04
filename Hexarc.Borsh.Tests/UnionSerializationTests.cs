using NUnit.Framework;
using Hexarc.Borsh.Serialization;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public sealed class UnionSerializationTests
{
    [Test]
    public void GenericUnionSerialization_ShouldMatchExpectation()
    {
        Figure square = new Square { SideSize = 1 };
        var expected = new Byte[] { 1, 1, 0, 0, 0 };
        var raw = BorshSerializer.Serialize(square);
        Assert.AreEqual(expected, raw);
    }

    [Test]
    public void GenericUnionDeserialization_ShouldMatchExpectation()
    {
        var raw = new Byte[] { 1, 1, 0, 0, 0 };
        var restored = BorshSerializer.Deserialize<Figure>(raw);
        Assert.IsTrue(restored is Square { SideSize: 1 });
    }
    
    [Test]
    public void UnionSerialization_ShouldMatchExpectation()
    {
        Animal square = new Dog();
        var expected = new Byte[] { 0 };
        var raw = BorshSerializer.Serialize(square);
        Assert.AreEqual(expected, raw);
    }

    [Test]
    public void UnionDeserialization_ShouldMatchExpectation()
    {
        var raw = new Byte[] { 1 };
        var restored = BorshSerializer.Deserialize<Animal>(raw);
        Assert.IsTrue(restored is Cat);
    }

    [BorshObject]
    [BorshUnion<Circle>(0)]
    [BorshUnion<Square>(1)]
    private abstract class Figure {}

    [BorshObject]
    private sealed class Circle : Figure
    {
        [BorshPropertyOrder(0)]
        public required Int32 Radius { get; init; }
    }

    [BorshObject]
    private sealed class Square : Figure
    {
        [BorshPropertyOrder(0)]
        public required Int32 SideSize { get; init; }
    }
    
    [BorshObject]
    [BorshUnion(0, typeof(Dog))]
    [BorshUnion(1, typeof(Cat))]
    private abstract class Animal {}

    [BorshObject]
    private sealed class Dog : Animal { }

    [BorshObject]
    private sealed class Cat : Animal { }
}
