using System;

namespace Ceilidh.LibCriPak
{
    internal class EndianConverter
    {
        public enum Endianness
        {
            BigEndian,
            LittleEndian,
            NetworkOrder = BigEndian
        }

        public static Endianness PlatformEndianness =>
            BitConverter.IsLittleEndian ? Endianness.LittleEndian : Endianness.BigEndian;

        public Endianness NativeEndianness { get; }
        public Endianness SourceEndianness { set; get; }

        public EndianConverter(Endianness nativeEndianness, Endianness sourceEndianness)
        {
            NativeEndianness = nativeEndianness;
            SourceEndianness = sourceEndianness;
        }

        public short Convert(short n)
        {
            if (NativeEndianness == SourceEndianness) return n;

            short i = 0;

            i |= unchecked((short)((n & 0x00ff) << 8));
            i |= unchecked((short)((n & 0x00ff) >> 8));

            return i;
        }

        public ushort Convert(ushort n)
        {
            if (NativeEndianness == SourceEndianness) return n;

            ushort i = 0;

            i |= unchecked((ushort)((n & 0x00ff) << 8));
            i |= unchecked((ushort)((n & 0x00ff) >> 8));

            return i;
        }

        public int Convert(int n)
        {
            if (NativeEndianness == SourceEndianness) return n;

            var i = 0;

            i |= (n & 0x000000ff) << 24;
            i |= (n & 0x0000ff00) << 8;
            i |= (n & 0x00ff0000) >> 8;
            i |= (n & unchecked((int)0xff000000)) >> 24;

            return i;
        }

        public uint Convert(uint n)
        {
            if (NativeEndianness == SourceEndianness) return n;

            var i = 0U;

            i |= (n & 0x000000ff) << 24;
            i |= (n & 0x0000ff00) << 8;
            i |= (n & 0x00ff0000) >> 8;
            i |= (n & 0xff000000) >> 24;

            return i;
        }

        public long Convert(long n)
        {
            if (NativeEndianness == SourceEndianness) return n;

            var i = 0L;

            i |= (n & 0x00000000000000ffL) << 56;
            i |= (n & 0x000000000000ff00L) << 40;
            i |= (n & 0x0000000000ff0000L) << 24;
            i |= (n & 0x00000000ff000000L) << 8;
            i |= (n & 0x000000ff00000000L) >> 8;
            i |= (n & 0x0000ff0000000000L) >> 24;
            i |= (n & 0x00ff000000000000L) >> 40;
            i |= (n & unchecked((long)0xff00000000000000L)) >> 56;

            return i;
        }

        public ulong Convert(ulong n)
        {
            if (NativeEndianness == SourceEndianness) return n;

            var i = 0UL;

            i |= (n & 0x00000000000000ffUL) << 56;
            i |= (n & 0x000000000000ff00UL) << 40;
            i |= (n & 0x0000000000ff0000UL) << 24;
            i |= (n & 0x00000000ff000000UL) << 8;
            i |= (n & 0x000000ff00000000UL) >> 8;
            i |= (n & 0x0000ff0000000000UL) >> 24;
            i |= (n & 0x00ff000000000000UL) >> 40;
            i |= (n & 0xff00000000000000UL) >> 56;

            return i;
        }
    }
}
