using System.Runtime.CompilerServices;
using Hexarc.Borsh.Serialization.Metadata;
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
        this.ReadSpan(Constants.BooleanSize)[0] == 1;

    public Byte ReadByte() =>
        this.ReadSpan(Constants.ByteSize)[0];

    public SByte ReadSByte() =>
        (SByte)this.ReadSpan(Constants.SByteSize)[0];

    public Int16 ReadInt16() =>
        ReadInt16LittleEndian(this.ReadSpan(Constants.Int16Size));

    public UInt16 ReadUInt16() =>
        ReadUInt16LittleEndian(this.ReadSpan(Constants.UInt16Size));

    public Int32 ReadInt32() =>
        ReadInt32LittleEndian(this.ReadSpan(Constants.Int32Size));

    public UInt32 ReadUInt32() =>
        ReadUInt32LittleEndian(this.ReadSpan(Constants.UInt32Size));

    public Int64 ReadInt64() =>
        ReadInt64LittleEndian(this.ReadSpan(Constants.Int64Size));

    public UInt64 ReadUInt64() =>
        ReadUInt64LittleEndian(this.ReadSpan(Constants.UInt64Size));

    public Single ReadSingle() =>
        ReadSingleLittleEndian(this.ReadSpan(Constants.SingleSize));

    public Double ReadDouble() =>
        ReadDoubleLittleEndian(this.ReadSpan(Constants.DoubleSize));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ReadOnlySpan<Byte> ReadSpan(Int32 size)
    {
        var span = this._buffer.Slice(this._bufferIndex, size);
        this._bufferIndex += size;
        return span;
    }
}
