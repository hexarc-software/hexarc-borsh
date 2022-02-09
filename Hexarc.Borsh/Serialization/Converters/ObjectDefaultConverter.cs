namespace Hexarc.Borsh.Serialization.Converters;

public class ObjectDefaultConverter<T> : BorshObjectConverter<T> where T : notnull
{
    public override void Write(BorshWriter writer, T value, BorshSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
