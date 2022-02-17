using Hexarc.Borsh.Serialization;
using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class ClassSerializationTests
{
    [TestCaseSource(nameof(SerializePointCases))]
    public void SerializePoint_ShouldMatchExpectation(Point point, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(point);
        Assert.AreEqual(expected, result);
    }

    [TestCaseSource(nameof(DeserializePointCases))]
    public void DeserializePoint_ShouldMatchExpectation(Byte[] bytes, Point expected)
    {
        var result = BorshSerializer.Deserialize<Point>(bytes);
        Assert.AreEqual(expected, result);
    }

    private static Object[] SerializePointCases =
    {
        new Object[]
        {
            new Point { X = 0, Y = 0, Z = 0 },
            new Byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        },
        new Object[]
        {
            new Point { X = 5346f, Y = -245345f, Z = 2466f },
            new Byte[] { 0, 16, 167, 69, 64, 152, 111, 200, 0, 32, 26, 69 }
        }
    };

    private static Object[] DeserializePointCases =
    {
        new Object[]
        {
            new Byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new Point { X = 0, Y = 0, Z = 0 }
        },
        new Object[]
        {
            new Byte[] { 0, 16, 167, 69, 64, 152, 111, 200, 0, 32, 26, 69 },
            new Point { X = 5346f, Y = -245345f, Z = 2466f }
        }
    };

    public sealed class Point : IEquatable<Point>
    {
        public Single X { get; init; }
        public Single Y { get; init; }
        public Single Z { get; init; }

        [BorshIgnore]
        public String? Memo { get; init; }

        public Boolean Equals(Point? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Math.Abs(this.X - other.X) <= 0.0000001f &&
                   Math.Abs(this.Y - other.Y) <= 0.0000001f &&
                   Math.Abs(this.Z - other.Z) <= 0.0000001f;
        }

        public override Boolean Equals(Object? obj) =>
            ReferenceEquals(this, obj) || obj is Point other && Equals(other);

        public override Int32 GetHashCode() =>
            HashCode.Combine(X, Y, Z);
    }
}
