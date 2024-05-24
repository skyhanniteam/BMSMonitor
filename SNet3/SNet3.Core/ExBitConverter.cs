using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNet3.Core
{
    public class ExBitConverter 
    {
        public static int ToInt24(byte[] bytes, int startIndex)
        {
            var value = bytes[startIndex] | bytes[startIndex + 1] << 8 | bytes[startIndex + 2] << 16;
            if ((value & 0x00800000) > 0)
                value |= 0xFF << 24;
            return value;
        }
        public static uint ToUInt24(byte[] bytes, int startIndex)
        {
            return (uint)(bytes[startIndex] | bytes[startIndex + 1] << 8 | bytes[startIndex + 2] << 16);
        }

        public static byte[] GetBytesInt24(int value)
        {            
            var bytes = new byte[3];
            bytes[0] = (byte)value;
            bytes[1] = (byte)(value >> 8);
            bytes[2] = (byte)(value >> 16);
            return bytes;

        }
    }
}
