namespace BinOptIO.IO.Readers;

public interface IBinaryReader
{
    int Length { get; }

    int Position { get; }

    int BytesAvailable { get; }

    byte ReadByte();

    sbyte ReadSByte();

    bool ReadBool();

    short ReadInt16();

    ushort ReadUInt16();

    int ReadInt32();

    uint ReadUInt32();

    long ReadInt64();

    ulong ReadUInt64();

    float ReadFloat();

    double ReadDouble();

    string ReadUtfBytes(int count);

    string ReadUtfBytes();

    bool[] ReadFlags();

    ReadOnlyMemory<byte> ReadMemory(int count);

    ReadOnlySpan<byte> ReadSpan(int count);

    void Seek(int offset, SeekOrigin origin = SeekOrigin.Begin);
}
