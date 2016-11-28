using System.IO;

namespace DarkAgesLib
{
    public static class BinaryReaderExtensions
    {
        public static short ReadInt16(this BinaryReader reader, bool bigEndian)
        {
            if (!bigEndian)
            {
                return reader.ReadInt16();
            }

            byte[] buffer = reader.ReadBytes(2);
            return (short)(buffer[1] | buffer[0] << 8);
        }

        public static ushort ReadUInt16(this BinaryReader reader, bool bigEndian)
        {
            if (!bigEndian)
            {
                return reader.ReadUInt16();
            }

            byte[] buffer = reader.ReadBytes(2);
            return (ushort)(buffer[1] | buffer[0] << 8);
        }

        public static int ReadInt32(this BinaryReader reader, bool bigEndian)
        {
            if (!bigEndian)
            {
                return reader.ReadInt32();
            }

            byte[] buffer = reader.ReadBytes(4);
            return buffer[3] | buffer[2] << 8 | buffer[1] << 16 | buffer[0] << 24;
        }

        public static uint ReadUInt32(this BinaryReader reader, bool bigEndian)
        {
            if (!bigEndian)
            {
                return reader.ReadUInt32();
            }

            byte[] buffer = reader.ReadBytes(4);
            return (uint)(buffer[3] | buffer[2] << 8 | buffer[1] << 16 | buffer[0] << 24);
        }
    }
}
