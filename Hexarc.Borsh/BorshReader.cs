using System.Runtime.CompilerServices;
using static System.Buffers.Binary.BinaryPrimitives;

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

    public Boolean ReadBoolean() =>
        this.ReadSpan(sizeof(Boolean))[0] == 1;

    public Byte ReadByte() =>
        this.ReadSpan(sizeof(Byte))[0];

    public SByte ReadSByte() =>
        (SByte)this.ReadSpan(sizeof(SByte))[0];

    public Int16 ReadInt16() =>
        ReadInt16LittleEndian(this.ReadSpan(sizeof(Int16)));

    public UInt16 ReadUInt16() =>
        ReadUInt16LittleEndian(this.ReadSpan(sizeof(UInt16)));

    public Int32 ReadInt32() =>
        ReadInt32LittleEndian(this.ReadSpan(sizeof(Int32)));

    public UInt32 ReadUInt32() =>
        ReadUInt32LittleEndian(this.ReadSpan(sizeof(UInt32)));

    public Int64 ReadInt64() =>
        ReadInt64LittleEndian(this.ReadSpan(sizeof(Int64)));

    public UInt64 ReadUInt64() =>
        ReadUInt64LittleEndian(this.ReadSpan(sizeof(UInt64)));

    public Single ReadSingle() =>
        ReadSingleLittleEndian(this.ReadSpan(sizeof(Single)));

    public Double ReadDouble() =>
        ReadDoubleLittleEndian(this.ReadSpan(sizeof(Double)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ReadOnlySpan<Byte> ReadSpan(Int32 size)
    {
        var span = this._buffer.Slice(this._bufferIndex, size);
        this._bufferIndex += size;
        return span;
    }
}
