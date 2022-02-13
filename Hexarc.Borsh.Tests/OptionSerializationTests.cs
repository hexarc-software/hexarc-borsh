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
}
