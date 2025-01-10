using System.Buffers.Binary;

namespace BinOptIO.IO.Writers;

public sealed class LittleEndianWriter
    : BinaryWriter
{
    public override void WriteInt16(short value) =>
        BinaryPrimitives.WriteInt16LittleEndian(CreateSpan(sizeof(short)), value);

    public override void WriteUInt16(ushort value) =>
        BinaryPrimitives.WriteUInt16LittleEndian(CreateSpan(sizeof(ushort)), value);

    public override void WriteInt32(int value) =>
        BinaryPrimitives.WriteInt32LittleEndian(CreateSpan(sizeof(int)), value);

    public override void WriteUInt32(uint value) =>
        BinaryPrimitives.WriteUInt32LittleEndian(CreateSpan(sizeof(uint)), value);

    public override void WriteInt64(long value) =>
        BinaryPrimitives.WriteInt64LittleEndian(CreateSpan(sizeof(long)), value);

    public override void WriteUInt64(ulong value) =>
        BinaryPrimitives.WriteUInt64LittleEndian(CreateSpan(sizeof(ulong)), value);

    public override void WriteFloat(float value) =>
        BinaryPrimitives.WriteSingleLittleEndian(CreateSpan(sizeof(float)), value);

    public override void WriteDouble(double value) =>
        BinaryPrimitives.WriteDoubleLittleEndian(CreateSpan(sizeof(double)), value);
}