using System;
using System.IO;
using System.Text;

namespace Ceilidh.LibCriPak
{
    internal static class Tools
    {
        public static string ReadCString(BinaryReader rd, int maxLength = 255, Encoding encoding = null)
        {
            var data = new byte[maxLength];
            var strLen = 0;

            for (var i = 0; i < maxLength; i++)
            {
                var b = rd.ReadByte();
                if (b == 0) break;
                data[i] = b;
                strLen++;
            }

            return (encoding ?? Encoding.GetEncoding("SJIS")).GetString(data, 0, strLen);
        }

        public static unsafe string ReadCString(byte[] data, int index, int maxLength = int.MaxValue, Encoding encoding = null)
        {
            maxLength = Math.Min(maxLength, data.Length);

            var l = 0;
            fixed (byte* ptr = data)
                for (var i = 0; i < maxLength; i++)
                {
                    if (ptr[i] == 0) break;
                    l++;
                }

            return (encoding ?? Encoding.GetEncoding("SJIS")).GetString(data, index, l);
        }
    }
}
