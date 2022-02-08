using Hexarc.Borsh.Serialization;

namespace Hexarc.Borsh;

public static partial class BorshSerializer
{
    public static Byte[] Serialize<TValue>(TValue value, BorshSerializerOptions? options = null)
    {
        var output = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(output);

        options ??= BorshSerializerOptions.Default;
        var converter = options.GetConverter(typeof(TValue));
        if (converter is BorshConverter<TValue> typedConverter)
        {
            typedConverter.WriteCore(writer, value, options);
        }
        else
        {
            converter.WriteCoreAsObject(writer, value!, options);
        }

        return output.WrittenMemory.ToArray();
    }

    public static Task SerializeAsync<TValue>(Stream stream, TValue value, BorshSerializerOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public static TValue Deserialize<TValue>(Byte[] bytes)
    {
        throw new NotImplementedException();
    }
}
