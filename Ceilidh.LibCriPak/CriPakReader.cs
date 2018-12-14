using System.IO;

namespace Ceilidh.LibCriPak
{
    public static class CriPakReader
    {
        private const string CPK_MAGIC = "CPK ", UTF_MAGIC = "@UTF";

        public static bool TryReadCriPak(Stream stream, out CriPak pak)
        {
            pak = new CriPak();
            var br = new BinaryReader(stream);
            var endian = new EndianConverter(EndianConverter.PlatformEndianness, EndianConverter.Endianness.BigEndian);

            if (Tools.ReadCString(br, 4) != CPK_MAGIC)
                return false;

            var utfHeader = ReadUtfHeader(br);
            pak.IsUtfEncrypted = utfHeader.IsUtfEncrypted;

            return false;
        }

        private static UtfHeader ReadUtfHeader(BinaryReader br)
        {
            var endian = new EndianConverter(EndianConverter.PlatformEndianness, EndianConverter.Endianness.LittleEndian);

            var unk1 = endian.Convert(br.ReadInt32());
            var utfSize = endian.Convert(br.ReadInt64());
            var utfPacket = br.ReadBytes((int)utfSize);

            if (Tools.ReadCString(utfPacket, 0, 4) == UTF_MAGIC)
                return new UtfHeader(unk1, utfSize, utfPacket, false);

            DecryptUtf(utfPacket);

            return new UtfHeader(unk1, utfSize, utfPacket, true);
        }

        private static unsafe void DecryptUtf(byte[] packet)
        {
            const int t = 0x00004115, initialM = 0x0000655f;
            var m = initialM;

            fixed (byte* ptr = packet)
                for (var i = 0; i < packet.Length; i++)
                {
                    ptr[i] = (byte) (ptr[i] ^ (byte) (m & 0xff));
                    m *= t;
                }
        }

        private readonly struct UtfHeader
        {
            public int Unk1 { get; }
            public long UtfSize { get; }
            public byte[] UtfPacket { get; }
            public bool IsUtfEncrypted { get; }

            public UtfHeader(int unk1, long utfSize, byte[] utfPacket, bool isUtfEncrypted)
            {
                Unk1 = unk1;
                UtfSize = utfSize;
                UtfPacket = utfPacket;
                IsUtfEncrypted = isUtfEncrypted;
            }
        }
    }
}
