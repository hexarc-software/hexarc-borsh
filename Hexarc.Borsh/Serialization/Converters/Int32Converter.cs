namespace Hexarc.Borsh.Serialization.Converters;

public sealed class Int32Converter : BorshConverter<Int32>
{
    public override void Write(IBufferWriter<Byte> writer, Int32 value, BorshSerializerOptions options)
    {
        const Int32 valueSizeInBytes = 4;
        var span = writer.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteInt32LittleEndian(span, value);
        writer.Advance(valueSizeInBytes);
    }
}
