using NUnit.Framework;
using Hexarc.Borsh.Serialization;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public sealed class FixedArraySerializationTests
{
    [TestCaseSource(nameof(TestData))]
    public void FixedArraySerialization_ShouldMatchExpectation(Data value, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }
    
    [TestCaseSource(nameof(TestData))]
    public void FixedArrayDeserialization_ShouldMatchExpectation(Data expected, Byte[] value)
    {
        var result = BorshSerializer.Deserialize<Data>(value);
        Assert.AreEqual(expected, result);
    }
    
    private static Object[] TestData =
    {
        new Object[]
        {
            new Data { Numbers = new[] { 1, 2, 3 } },
            new Byte[] { 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 0 }
        },
        new Object[]
        {
            new Data { Numbers = new[] { 1, 2, 3 }, OptionalNumbers = new [] { 3, 3 }},
            new Byte[] { 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 1, 3, 0, 0, 0, 3, 0, 0, 0 }
        }
    };
    
    [BorshObject]
    public sealed class Data
    {
        [BorshPropertyOrder(0)]
        [BorshFixedArray(3)]
        public required Int32[] Numbers { get; init; }
        
        [BorshPropertyOrder(1)]
        [BorshOptional]
        [BorshFixedArray(2)]
        public Int32[]? OptionalNumbers { get; init; }

        public Boolean Equals(Data? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return this.Numbers.SequenceEqual(other.Numbers) &&
                   ((this.OptionalNumbers is null && other.OptionalNumbers is null) ||
                    (this.OptionalNumbers is not null && other.OptionalNumbers is not null &&
                    this.OptionalNumbers.SequenceEqual(other.OptionalNumbers)));
        }

        public override Boolean Equals(Object? obj) =>
            ReferenceEquals(this, obj) || obj is Data other && Equals(other);

        public override Int32 GetHashCode() =>
            HashCode.Combine(this.Numbers, this.OptionalNumbers);
    }
}
