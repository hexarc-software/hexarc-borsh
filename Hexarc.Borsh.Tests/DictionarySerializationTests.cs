using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public sealed class DictionarySerializationTests
{
    [TestCaseSource(nameof(Cases))]
    public void DictionarySerialization_ShouldMatchExpectation(Dictionary<Int32, Int32> dictionary, Byte[] expected)
    {
        var result = BorshSerializer.Serialize(dictionary);
        Assert.AreEqual(expected, result);
    }

    [TestCaseSource(nameof(Cases))]
    public void DictionaryDeserialization_ShouldMatchExpectation(Dictionary<Int32, Int32> expected, Byte[] bytes)
    {
        var result = BorshSerializer.Deserialize<Dictionary<Int32, Int32>>(bytes);
        Assert.AreEqual(expected, result);
    }

    private static Object[] Cases =
    {
        new Object[]
        {
            new Dictionary<Int32, Int32>(),
            new Byte[] { 0, 0, 0, 0 }
        },
        new Object[]
        {
            new Dictionary<Int32, Int32>
            {
                { 1, 1 }
            },
            new Byte[] { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 }
        }
    };
}
