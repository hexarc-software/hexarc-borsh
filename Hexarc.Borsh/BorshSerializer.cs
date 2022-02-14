namespace Hexarc.Borsh;

public static partial class BorshSerializer
{
    public static Byte[] Serialize<TValue>(TValue value, BorshSerializerOptions? options = null)
    {
        if (value is null && !typeof(TValue).IsValueType)
        {
            throw new ArgumentException("BORSH does not support null reference values", nameof(value));
        }

        var output = new ArrayBufferWriter<Byte>();
        var writer = new BorshWriter(output);

        options ??= BorshSerializerOptions.Default;
        var converter = options.GetConverter<TValue>();
        converter.WriteCore(writer, value, options);

        return output.WrittenMemory.ToArray();
    }

    public static TValue Deserialize<TValue>(Byte[] bytes, BorshSerializerOptions? options = null)
    {
        var reader = new BorshReader(bytes);
        options ??= BorshSerializerOptions.Default;
        var converter = options.GetConverter<TValue>();
        return converter.ReadCore(ref reader, options);
    }
}
