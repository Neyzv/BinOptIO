using System.Buffers;
using System.Text;
using BinOptIO.Memory;

namespace BinOptIO.IO.Writers;

public abstract class BinaryWriter
    : IBinaryWriter, IDisposable
{
    private readonly ushort _bufferUpgradeSizeAmount;

    private IMemoryOwner<byte> _buffer = MemoryOwner.Empty;

    public int Length =>
        _buffer.Memory.Length;

    public int Position { get; private set; }

    public int BytesAvailable =>
        Length - Position;

    protected BinaryWriter(ushort bufferUpgradeSizeAmount = byte.MaxValue) =>
        _bufferUpgradeSizeAmount = bufferUpgradeSizeAmount;

    private void AdjustBufferSizeIfNeeded(int count)
    {
        if (count <= BytesAvailable)
            return;

        var newBufferSize = _bufferUpgradeSizeAmount * (int)Math.Ceiling(count / (double)_bufferUpgradeSizeAmount);

        if ((uint)newBufferSize > Array.MaxLength)
            throw new OutOfMemoryException("The requested operation would exceed the maximum size of an array.");

        var newBuffer = MemoryPool<byte>.Shared.Rent(newBufferSize);

        _buffer.Memory.CopyTo(newBuffer.Memory);

        _buffer.Dispose();
        _buffer = newBuffer;
    }

    protected Span<byte> CreateSpan(int length)
    {
        AdjustBufferSizeIfNeeded(length);

        var span = _buffer.Memory.Span.Slice(Position, length);

        Position += length;

        return span;
    }

    public void WriteByte(byte value) =>
        CreateSpan(sizeof(byte))[0] = value;

    public void WriteSByte(sbyte value) =>
        CreateSpan(sizeof(sbyte))[0] = (byte)value;

    public abstract void WriteInt16(short value);

    public abstract void WriteUInt16(ushort value);

    public abstract void WriteInt32(int value);

    public abstract void WriteUInt32(uint value);

    public abstract void WriteInt64(long value);

    public abstract void WriteUInt64(ulong value);

    public abstract void WriteFloat(float value);

    public abstract void WriteDouble(double value);

    public void WriteChars(string value) =>
        WriteSpan(Encoding.UTF8.GetBytes(value));

    public void WriteUtfBytes(string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);

        WriteUInt16((ushort)bytes.Length);
        WriteSpan(bytes);
    }

    public int WriteFlags(params bool[] flags)
    {
        var res = new byte[(int)Math.Ceiling(flags.Length / (double)BinaryConst.BitPerBytes)];

        for (int i = 0; i < flags.Length; i++)
            if (flags[i])
                res[(int)Math.Floor(i / (double)BinaryConst.BitPerBytes)] |= (byte)(1 << (i % BinaryConst.BitPerBytes));

        WriteByte((byte)res.Length);
        WriteSpan(res);

        return res.Length;
    }

    public void WriteSpan(ReadOnlySpan<byte> span) =>
        span.CopyTo(CreateSpan(span.Length));

    public void WriteMemory(ReadOnlyMemory<byte> memory) =>
        WriteSpan(memory.Span);

    public Memory<byte> BufferAsMemory() =>
        _buffer.Memory[..Position];

    public Span<byte> BufferAsSpan() =>
        _buffer.Memory.Span[..Position];

    public byte[] BufferAsArray() =>
        _buffer.Memory.Span[..Position].ToArray();

    public void Seek(int offset, SeekOrigin origin = SeekOrigin.Begin)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        
        switch (origin)
        {
            case SeekOrigin.Begin:
                AdjustBufferSizeIfNeeded(offset - Length);
                Position = offset;

                break;

            case SeekOrigin.Current:
                AdjustBufferSizeIfNeeded(offset);
                Position += offset;

                break;

            case SeekOrigin.End:
                Position = Length - offset;
                
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(origin));
        }
    }

    public void Dispose()
    {
        _buffer.Dispose();
        
        GC.SuppressFinalize(this);
    }
}