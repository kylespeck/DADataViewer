using DarkAgesLib.Data;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;

namespace DarkAgesLib.Drawing
{
    public class HPFFile
    {
        #region Fields

        private byte[] _unknownBytes;
        private byte[] _data;
        private ReadOnlyCollection<byte> _dataCollection;

        #endregion Fields


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public HPFFile(Stream stream)
        {
            Init(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public HPFFile(DataArchiveEntry entry)
        {
            using (var stream = entry.Open())
            {
                Init(stream);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get
            {
                return 28;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get
            {
                return _data.Length / 28;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] UnknownBytes
        {
            get
            {
                return _unknownBytes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<byte> Data
        {
            get
            {
                return _dataCollection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[int index]
        {
            get
            {
                return _data[index];
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="palette"></param>
        /// <returns></returns>
        public Bitmap Render(Palette palette)
        {
            return palette.Render(_data, Width, Height);
        }

        #endregion Public/Protected


        #region Private/Internal

        private void Init(Stream stream)
        {
            byte[] decompressedBytes;

            if (stream.ReadByte() == 0x55)
            {
                byte[] compressedBytes = new byte[stream.Length];
                compressedBytes[0] = 0x55;
                stream.Read(compressedBytes, 1, compressedBytes.Length - 1);
                decompressedBytes = Decompress(compressedBytes);
            }
            else
            {
                decompressedBytes = new byte[stream.Length];
                stream.Position--;
                stream.Read(decompressedBytes, 0, decompressedBytes.Length);
            }

            _unknownBytes = new byte[8];
            _data = new byte[decompressedBytes.Length - 8];
            _dataCollection = new ReadOnlyCollection<byte>(_data);

            Buffer.BlockCopy(decompressedBytes, 0, _unknownBytes, 0, 8);
            Buffer.BlockCopy(decompressedBytes, 8, _data, 0, _data.Length);
        }

        private byte[] Decompress(byte[] hpfBytes)
        {
            // Random Variables
            uint k = 7;
            uint val = 0;
            uint val2 = 0;
            uint val3 = 0;
            uint i = 0;
            uint j = 0;
            uint l = 0;
            uint m = 0;

            // Read HPF File
            int hpfSize = hpfBytes.Length;

            // Allocate Memory for Decoded Picture
            byte[] rawBytes = new byte[hpfSize * 10];

            // Prepare Arrays
            uint[] int_odd = new uint[256];
            uint[] int_even = new uint[256];
            byte[] byte_pair = new byte[513];

            for (i = 0; i < 256; i++)
            {
                int_odd[i] = (2 * i + 1);
                int_even[i] = (2 * i + 2);

                byte_pair[i * 2 + 1] = (byte)i;
                byte_pair[i * 2 + 2] = (byte)i;
            }

            #region HPF Decompression
            while (val != 0x100)
            {
                val = 0;
                while (val <= 0xFF)
                {
                    if (k == 7)
                    {
                        l++;
                        k = 0;
                    }
                    else
                        k++;

                    if ((hpfBytes[4 + l - 1] & (1 << (int)k)) != 0)
                        val = int_even[val];
                    else
                        val = int_odd[val];
                }

                val3 = val;
                val2 = byte_pair[val];

                while (val3 != 0 && val2 != 0)
                {
                    i = byte_pair[val2];
                    j = int_odd[i];

                    if (j == val2)
                    {
                        j = int_even[i];
                        int_even[i] = val3;
                    }
                    else
                        int_odd[i] = val3;

                    if (int_odd[val2] == val3)
                        int_odd[val2] = j;
                    else
                        int_even[val2] = j;

                    byte_pair[val3] = (byte)i;
                    byte_pair[j] = (byte)val2;
                    val3 = i;
                    val2 = byte_pair[val3];
                }

                val += 0xFFFFFF00;

                if (val != 0x100)
                {
                    rawBytes[m] = (byte)val;
                    m++;
                }
            }
            #endregion

            // Copy Data to Exact Array
            byte[] finalData = new byte[m];
            Buffer.BlockCopy(rawBytes, 0, finalData, 0, (int)m);

            // Return Finalized Data
            return finalData;
        }

        #endregion Private/Internal
    }
}
