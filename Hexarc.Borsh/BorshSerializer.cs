namespace Hexarc.Borsh;

public static class BorshSerializer
{
    public static Byte[] Serialize<TValue>(TValue value)
    {
        throw new NotSupportedException();
    }

    public static TValue Deserialize<TValue>(Byte[] bytes)
    {
        throw new NotImplementedException();
    }
}
