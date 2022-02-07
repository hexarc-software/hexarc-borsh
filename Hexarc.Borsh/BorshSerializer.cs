namespace Hexarc.Borsh;

public static partial class BorshSerializer
{
    public static Byte[] Serialize<TValue>(TValue value, BorshSerializerOptions? options = null)
    {
        options ??= BorshSerializerOptions.Default;
        var writer = new ArrayBufferWriter<Byte>();
        return writer.WrittenMemory.ToArray();
    }

    public static TValue Deserialize<TValue>(Byte[] bytes)
    {
        throw new NotImplementedException();
    }
}
