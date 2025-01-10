using System.Buffers;
using System.Buffers.Binary;

namespace BinOptIO.IO.Readers;

public sealed class LittleEndianReader
    : BinaryReader
{
    public LittleEndianReader(ReadOnlyMemory<byte> buffer)
        : base(buffer) { }

    public LittleEndianReader(ReadOnlySequence<byte> buffer)
        : base(buffer) { }

    public LittleEndianReader(Stream stream)
        : base(stream) { }

    public override short ReadInt16() =>
        BinaryPrimitives.ReadInt16LittleEndian(ReadSpan(sizeof(short)));

    public override ushort ReadUInt16() =>
        BinaryPrimitives.ReadUInt16LittleEndian(ReadSpan(sizeof(ushort)));

    public override int ReadInt32() =>
        BinaryPrimitives.ReadInt32LittleEndian(ReadSpan(sizeof(int)));

    public override uint ReadUInt32() =>
        BinaryPrimitives.ReadUInt32LittleEndian(ReadSpan(sizeof(uint)));

    public override long ReadInt64() =>
        BinaryPrimitives.ReadInt64LittleEndian(ReadSpan(sizeof(long)));

    public override ulong ReadUInt64() =>
        BinaryPrimitives.ReadUInt64LittleEndian(ReadSpan(sizeof(ulong)));

    public override float ReadFloat() =>
        BinaryPrimitives.ReadSingleLittleEndian(ReadSpan(sizeof(float)));

    public override double ReadDouble() =>
        BinaryPrimitives.ReadDoubleLittleEndian(ReadSpan(sizeof(double)));
}