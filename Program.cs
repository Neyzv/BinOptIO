using BinOptIO.IO.Readers;
using BinOptIO.IO.Writers;

// WRITE
using var writer = new BigEndianWriter();
writer.WriteUtfBytes("Hello World !");
writer.WriteByte(255);
writer.WriteByte(255);
writer.WriteFlags(true, false, true);

// READ
var reader = new BigEndianReader(writer.BufferAsMemory());
Console.WriteLine(reader.ReadUtfBytes());
Console.WriteLine(reader.ReadUInt16());

foreach (var item in reader.ReadFlags())
    Console.WriteLine(item);