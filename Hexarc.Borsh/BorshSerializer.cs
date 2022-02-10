using Hexarc.Borsh.Serialization;

namespace Hexarc.Borsh;

public static partial class BorshSerializer
{
    public static Byte[] Serialize<TValue>(TValue value, BorshSerializerOptions? options = null)
    {
        var output = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(output);
        var type = typeof(TValue);

        options ??= BorshSerializerOptions.Default;
        var converter = options.GetConverter(type);

        if (converter is BorshConverter<TValue> typedConverter)
        {
            typedConverter.WriteCore(writer, value, options);
        }
        else
        {
            converter.WriteCoreAsObject(writer, value, options);
        }

        return output.WrittenMemory.ToArray();
    }

    public static TValue Deserialize<TValue>(Byte[] bytes, BorshSerializerOptions? options = null)
    {
        var type = typeof(TValue);
        var reader = new BorshReader(bytes);

        options ??= BorshSerializerOptions.Default;
        var converter = options.GetConverter(type);

        if (converter is BorshConverter<TValue> typedConverter)
        {
            return typedConverter.ReadCore(ref reader, options);
        }
        else
        {
            return (TValue)converter.ReadCoreAsObject(ref reader, options);
        }
    }
}
