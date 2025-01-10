using System.Buffers.Binary;

namespace BinOptIO.IO.Writers;

public sealed class BigEndianWriter
    : BinaryWriter
{
    public override void WriteInt16(short value) =>
        BinaryPrimitives.WriteInt16BigEndian(CreateSpan(sizeof(short)), value);

    public override void WriteUInt16(ushort value) =>
        BinaryPrimitives.WriteUInt16BigEndian(CreateSpan(sizeof(ushort)), value);

    public override void WriteInt32(int value) =>
        BinaryPrimitives.WriteInt32BigEndian(CreateSpan(sizeof(int)), value);

    public override void WriteUInt32(uint value) =>
        BinaryPrimitives.WriteUInt32BigEndian(CreateSpan(sizeof(uint)), value);

    public override void WriteInt64(long value) =>
        BinaryPrimitives.WriteInt64BigEndian(CreateSpan(sizeof(long)), value);

    public override void WriteUInt64(ulong value) =>
        BinaryPrimitives.WriteUInt64BigEndian(CreateSpan(sizeof(ulong)), value);

    public override void WriteFloat(float value) =>
        BinaryPrimitives.WriteSingleBigEndian(CreateSpan(sizeof(float)), value);

    public override void WriteDouble(double value) =>
        BinaryPrimitives.WriteDoubleBigEndian(CreateSpan(sizeof(double)), value);
}