
using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace BinOptIO.IO.Readers;

public abstract class BinaryReader
    : IBinaryReader
{
    private readonly ReadOnlyMemory<byte> _buffer;

    public int Length =>
        _buffer.Length;

    public int Position { get; private set; }

    public int BytesAvailable =>
        Length - Position;

    public BinaryReader(ReadOnlyMemory<byte> buffer) =>
        _buffer = buffer;

    public BinaryReader(ReadOnlySequence<byte> buffer)
    {
        if (buffer.IsSingleSegment)
            _buffer = buffer.First;
        else if (SequenceMarshal.TryGetReadOnlyMemory(buffer, out var memory))
            _buffer = memory;
        else
            _buffer= buffer.ToArray();
    }

    public BinaryReader(Stream stream)
    {
        if (stream is not MemoryStream ms)
        {
            ms = new MemoryStream();
            stream.CopyTo(ms);
        }

        _buffer = ms.ToArray();
    }

    public byte ReadByte() =>
        ReadSpan(sizeof(byte))[0];

    public sbyte ReadSByte() =>
        (sbyte)ReadSpan(sizeof(byte))[0];

    public bool ReadBool() =>
        ReadSpan(sizeof(byte))[0] is not 0;

    public abstract short ReadInt16();

    public abstract ushort ReadUInt16();

    public abstract int ReadInt32();

    public abstract uint ReadUInt32();

    public abstract long ReadInt64();

    public abstract ulong ReadUInt64();

    public abstract float ReadFloat();

    public abstract double ReadDouble();

    public string ReadChars(int count) =>
        Encoding.UTF8.GetString(ReadSpan(count));

    public string ReadUtfBytes() =>
        ReadChars(ReadUInt16());

    public bool[] ReadFlags()
    {
        var byteCount = ReadByte();
        var bytes = ReadSpan(byteCount);

        var res = new bool[byteCount * BinaryConst.BitPerBytes];

        for (var i = 0; i < byteCount * BinaryConst.BitPerBytes; i++)
            res[i] = (bytes[(int)Math.Floor(i / (double)BinaryConst.BitPerBytes)] & (1 << i % BinaryConst.BitPerBytes)) != 0;

        return res;
    }

    public ReadOnlyMemory<byte> ReadMemory(int count)
    {
        var memory = _buffer.Slice(Position, count);
        Position += count;

        return memory;
    }

    public ReadOnlySpan<byte> ReadSpan(int count) =>
        ReadMemory(count).Span;

    public void Seek(int offset, SeekOrigin origin = SeekOrigin.Begin)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, _buffer.Length);
        
        Position = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => Position + offset,
            SeekOrigin.End => _buffer.Length - offset,
            _ => throw new ArgumentOutOfRangeException(nameof(origin)),
        };
    }
}