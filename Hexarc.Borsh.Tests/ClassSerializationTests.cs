using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class ClassSerializationTests
{
    [TestCaseSource(nameof(PointCases))]
    public void SerializePoint_ShouldMatchExpectation(Point point, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(point);
        Assert.AreEqual(expected, result);
    }

    private static Object[] PointCases =
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

    public sealed class Point
    {
        public Single X { get; init; }
        public Single Y { get; init; }
        public Single Z { get; init; }
    }
}
