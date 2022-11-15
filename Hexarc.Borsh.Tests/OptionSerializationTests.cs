using NUnit.Framework;
using Hexarc.Borsh.Serialization;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public sealed class OptionSerializationTests
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
            new AnnotatedPerson(),
            new Byte[] { 0, 0 }
        },
        new Object[]
        {
            new AnnotatedPerson { FirstName = "Test", LastName = "Test" },
            new Byte[] { 1, 4, 0, 0, 0, 84, 101, 115, 116, 1, 4, 0, 0, 0, 84, 101, 115, 116 }
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
    }
}
