namespace Hexarc.Borsh;

public static class BorshSerializer
{
    /// <summary>
    /// Converts the provided value into a byte array.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">Options to control the conversion behavior.</param>
    /// <returns>A borsh representation of the value.</returns>
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

    /// <summary>
    /// Reads the byte array representing a single BORSH value into a <typeparamref name="TValue"/>
    /// </summary>
    /// <param name="bytes">BORSH byte array to parse.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <returns>A <typeparamref name="TValue"/> representation of the BORSH value.</returns>
    public static TValue Deserialize<TValue>(Byte[] bytes, BorshSerializerOptions? options = null)
    {
        var reader = new BorshReader(bytes);
        options ??= BorshSerializerOptions.Default;
        var converter = options.GetConverter<TValue>();
        return converter.ReadCore(ref reader, options);
    }
}
