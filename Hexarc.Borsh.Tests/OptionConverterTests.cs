using System.Buffers;
using Hexarc.Borsh.Serialization;
using Hexarc.Borsh.Serialization.Converters;
using Hexarc.Borsh.Serialization.Metadata;
using NUnit.Framework;

namespace Hexarc.Borsh.Tests;

[TestFixture]
public class OptionConverterTests
{
    [Test]
    public void CreateValueConverter_ShouldWork()
    {
        var type = typeof(Int32);
        var underlyingConverter = BorshMetadataServices.Int32Converter;
        var baseConverterType = typeof(ValueOptionConverter<>);
        var concreteConverterType = baseConverterType.MakeGenericType(type);
        var converter = Activator.CreateInstance(
            concreteConverterType, underlyingConverter) as BorshConverter<Int32?>;
        Assert.NotNull(converter);

        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        converter!.Write(writer, default, new BorshSerializerOptions());
        Assert.AreEqual(new Byte[] { 0 }, buffer.WrittenMemory.ToArray());
    }

    [Test]
    public void CreateReferenceConverter_ShouldWork()
    {
        var type = typeof(String);
        var underlyingConverter = BorshMetadataServices.StringConverter;
        var baseConverterType = typeof(ReferenceOptionConverter<>);
        var concreteConverterType = baseConverterType.MakeGenericType(type);
        var converter = Activator.CreateInstance(
            concreteConverterType, underlyingConverter) as BorshConverter<String>;
        Assert.NotNull(converter);

        var buffer = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(buffer);
        converter!.Write(writer, "1", new BorshSerializerOptions());
        Assert.AreEqual(new Byte[] { 1, 1, 0, 0, 0, 49 }, buffer.WrittenMemory.ToArray());
    }
}
