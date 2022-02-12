using System.Reflection.Metadata;
using Hexarc.Borsh.Serialization.Metadata;

namespace Hexarc.Borsh;

public ref struct BorshReader
{
    private readonly ReadOnlySpan<Byte> _buffer;

    private Int32 _bufferIndex;

    public BorshReader(ReadOnlySpan<Byte> borshData)
    {
        this._buffer = borshData;
        this._bufferIndex = 0;
    }

    public Boolean ReadBoolean()
    {
        var span = this._buffer.Slice(_bufferIndex, Constants.BooleanSize);
        this._bufferIndex += Constants.BooleanSize;
        return span[0] == 1;
    }

    public Byte ReadByte()
    {
        var span = this._buffer.Slice(_bufferIndex, Constants.ByteSize);
        this._bufferIndex += Constants.ByteSize;
        return span[0];
    }

    public SByte ReadSByte()
    {
        var span = this._buffer.Slice(_bufferIndex, Constants.SByteSize);
        this._bufferIndex += Constants.SByteSize;
        return (SByte)span[0];
    }

    public Int16 ReadInt16()
    {
        var span = this._buffer.Slice(_bufferIndex, Constants.Int16Size);
        this._bufferIndex += Constants.Int16Size;
        return BinaryPrimitives.ReadInt16LittleEndian(span);
    }

    public UInt16 ReadUInt16()
    {
        const Int32 valueSizeInBytes = 2;
        var span = this._buffer.Slice(_bufferIndex, Constants.UInt16Size);
        this._bufferIndex += Constants.UInt16Size;
        return BinaryPrimitives.ReadUInt16LittleEndian(span);
    }
}
