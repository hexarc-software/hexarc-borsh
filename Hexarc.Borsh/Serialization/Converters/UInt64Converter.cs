namespace Hexarc.Borsh.Serialization.Converters;

public sealed class UInt64Converter : BorshConverter<UInt64>
{
    public override void Write(IBufferWriter<Byte> writer, UInt64 value)
    {
        const Int32 valueSizeInBytes = 8;
        var span = writer.GetSpan(valueSizeInBytes);
        BinaryPrimitives.WriteUInt64LittleEndian(span, value);
        writer.Advance(valueSizeInBytes);
    }
}
