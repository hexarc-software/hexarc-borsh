namespace Hexarc.Borsh.Serialization.Converters;

public sealed class Int16Converter : BorshConverter<Int16>
{
    public override void Write(BorshWriter writer, Int16 value, BorshSerializerOptions options) =>
        writer.WriteInt16(value);

    public override Int16 Read(ref BorshReader reader, BorshSerializerOptions options) =>
        reader.ReadInt16();
}
