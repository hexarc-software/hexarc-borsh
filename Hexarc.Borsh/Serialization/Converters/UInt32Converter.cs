namespace Hexarc.Borsh.Serialization.Converters;

public sealed class UInt32Converter : BorshConverter<UInt32>
{
    public override void Write(IBufferWriter<Byte> writer, UInt32 value)
    {
        const Int32 valueSizeInBytes = 4;
        var span = writer.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteUInt32LittleEndian(span, value);
        writer.Advance(valueSizeInBytes);
    }
}
