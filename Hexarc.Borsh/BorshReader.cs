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
        const Int32 valueSizeInBytes = 1;
        var span = this._buffer.Slice(_bufferIndex, valueSizeInBytes);
        this._bufferIndex += valueSizeInBytes;
        return span[0] == 1;
    }

    public Byte ReadByte()
    {
        const Int32 valueSizeInBytes = 1;
        var span = this._buffer.Slice(_bufferIndex, valueSizeInBytes);
        this._bufferIndex += valueSizeInBytes;
        return span[0];
    }

    public SByte ReadSByte()
    {
        const Int32 valueSizeInBytes = 1;
        var span = this._buffer.Slice(_bufferIndex, valueSizeInBytes);
        this._bufferIndex += valueSizeInBytes;
        return (SByte)span[0];
    }
}
