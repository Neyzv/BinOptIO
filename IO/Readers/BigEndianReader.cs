using System.Buffers;
using System.Buffers.Binary;

namespace BinOptIO.IO.Readers;

public sealed class BigEndianReader
    : BinaryReader
{
    public BigEndianReader(ReadOnlyMemory<byte> buffer)
        : base(buffer) { }

    public BigEndianReader(ReadOnlySequence<byte> buffer)
        : base(buffer) { }

    public BigEndianReader(Stream stream)
        : base(stream) { }

    public override short ReadInt16() =>
        BinaryPrimitives.ReadInt16BigEndian(ReadSpan(sizeof(short)));

    public override ushort ReadUInt16() =>
        BinaryPrimitives.ReadUInt16BigEndian(ReadSpan(sizeof(ushort)));

    public override int ReadInt32() =>
        BinaryPrimitives.ReadInt32BigEndian(ReadSpan(sizeof(int)));

    public override uint ReadUInt32() =>
        BinaryPrimitives.ReadUInt32BigEndian(ReadSpan(sizeof(uint)));

    public override long ReadInt64() =>
        BinaryPrimitives.ReadInt64BigEndian(ReadSpan(sizeof(long)));

    public override ulong ReadUInt64() =>
        BinaryPrimitives.ReadUInt64BigEndian(ReadSpan(sizeof(ulong)));

    public override float ReadFloat() =>
        BinaryPrimitives.ReadSingleBigEndian(ReadSpan(sizeof(float)));

    public override double ReadDouble() =>
        BinaryPrimitives.ReadDoubleBigEndian(ReadSpan(sizeof(double)));
}