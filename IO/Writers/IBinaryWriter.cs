namespace BinOptIO.IO.Writers;

public interface IBinaryWriter
{
    int Length { get; }

    int Position { get; }

    int BytesAvailable { get; }

    void WriteByte(byte value);

    void WriteSByte(sbyte value);

    void WriteInt16(short value);

    void WriteUInt16(ushort value);

    void WriteInt32(int value);

    void WriteUInt32(uint value);

    void WriteInt64(long value);

    void WriteUInt64(ulong value);

    void WriteFloat(float value);

    void WriteDouble(double value);

    void WriteChars(string value);

    void WriteUtfBytes(string value);

    int WriteFlags(params bool[] flags);

    void WriteSpan(ReadOnlySpan<byte> span);

    void WriteMemory(ReadOnlyMemory<byte> memory);

    Memory<byte> BufferAsMemory();

    Span<byte> BufferAsSpan();

    byte[] BufferAsArray();

    void Seek(int offset, SeekOrigin origin = SeekOrigin.Begin);
}
