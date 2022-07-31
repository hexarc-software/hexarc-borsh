using Hexarc.Borsh.Serialization;
using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class OptionSerializationTests
{
    [TestCaseSource(nameof(StringCases))]
    public void OptionStringSerialization_ShouldMatchExpectation(Option<String> value, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }

    private static Object[] StringCases =
    {
        new Object[]
        {
            Option<String>.None(),
            new Byte[] { 0 }
        },
        new Object[]
        {
            Option<String>.Some("Test"),
            new Byte[] { 1, 4, 0, 0, 0, 84, 101, 115, 116 }
        }
    };

    [TestCaseSource(nameof(AnnotatedPersonCases))]
    public void AnnotatedPersonSerialization_ShouldMatchExpectation(AnnotatedPerson value, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(value);
        Assert.AreEqual(expected, result);
    }

    private static Object[] AnnotatedPersonCases =
    {
        new Object[]
        {
            new AnnotatedPerson { Number = new [] { 1 } },
            new Byte[] { 0, 0, 0, 1, 0, 0, 0 }
        },
        new Object[]
        {
            new AnnotatedPerson { FirstName = "Test", LastName = "Test", Code = new[] { 0, 0 }, Number = new [] { 0 } },
            new Byte[] { 1, 4, 0, 0, 0, 84, 101, 115, 116, 1, 4, 0, 0, 0, 84, 101, 115, 116, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        }
    };

    [BorshObject]
    public class AnnotatedPerson
    {
        [BorshOptional]
        [BorshPropertyOrder(0)]
        public String? FirstName { get; init; }

        [BorshOptional]
        [BorshPropertyOrder(1)]
        public String? LastName { get; init; }
        
        [BorshOptional]
        [BorshPropertyOrder(2)]
        [BorshFixedArray(2)]
        public Int32[]? Code { get; init; }

        [BorshPropertyOrder(3)]
        [BorshFixedArray(1)]
        public Int32[] Number { get; init; } = default!;
    }
}
