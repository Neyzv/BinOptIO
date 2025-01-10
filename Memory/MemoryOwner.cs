using System.Buffers;

namespace BinOptIO.Memory;

public sealed class MemoryOwner
    : IMemoryOwner<byte>
{
    public static readonly IMemoryOwner<byte> Empty = new MemoryOwner();

    public Memory<byte> Memory { get; } = Memory<byte>.Empty;

    public void Dispose() { }
}