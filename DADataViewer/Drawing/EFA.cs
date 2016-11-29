using DarkAgesLib.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;

namespace DarkAgesLib.Drawing
{
    public class EFAFile : IEnumerable<EFAFrame>
    {
        #region Fields

        private int _unknown1;
        private int _expectedNumberOfFrames;
        private byte[] _unknown2;

        private EFAFrame[] _frames;
        private ReadOnlyCollection<EFAFrame> _framesCollection;

        #endregion Fields


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public EFAFile(Stream stream)
        {
            Init(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public EFAFile(DataArchiveEntry entry)
        {
            using (var stream = entry.Open())
            {
                Init(stream);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown1
        {
            get
            {
                return _unknown1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ExpectedNumberOfFrames
        {
            get
            {
                return _expectedNumberOfFrames;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Unknown2
        {
            get
            {
                return _unknown2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<EFAFrame> Frames
        {
            get
            {
                return _framesCollection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<EFAFrame> GetEnumerator()
        {
            return ((IEnumerable<EFAFrame>)_frames).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<EFAFrame>)_frames).GetEnumerator();
        }

        #endregion Public/Protected


        #region Private/Internal

        private void Init(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, true))
            {
                stream.Seek(0, SeekOrigin.Begin);

                _unknown1 = reader.ReadInt32();
                _expectedNumberOfFrames = reader.ReadInt32();
                _unknown2 = reader.ReadBytes(56);

                _frames = new EFAFrame[_expectedNumberOfFrames];
                _framesCollection = new ReadOnlyCollection<EFAFrame>(_frames);

                var frameHeaders = new EFAFrameHeader[_expectedNumberOfFrames];

                for (int i = 0; i < _expectedNumberOfFrames; ++i)
                {
                    frameHeaders[i] = EFAFrameHeader.Read(reader);
                }

                for (int i = 0; i < _expectedNumberOfFrames; ++i)
                {
                    var header = frameHeaders[i];
                    byte[] compressedData = reader.ReadBytes(header.Size);

                    using (var compressedStream = new MemoryStream(compressedData))
                    using (var decompressedStream = new MemoryStream())
                    using (var decompressionStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
                    {
                        compressedStream.Seek(2, SeekOrigin.Begin);
                        decompressionStream.CopyTo(decompressedStream);
                        _frames[i] = new EFAFrame(frameHeaders[i], decompressedStream.ToArray());
                    }
                }
            }
        }

        #endregion Private/Internal
    }

    public class EFAFrame
    {
        #region Fields

        private EFAFrameHeader _header;
        private byte[] _data;
        private ReadOnlyCollection<byte> _dataCollection;

        #endregion Fields


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        public EFAFrame(EFAFrameHeader header, byte[] data)
        {
            _header = header;
            _data = data;
            _dataCollection = new ReadOnlyCollection<byte>(_data);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown1
        {
            get
            {
                return _header.Unknown1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Offset
        {
            get
            {
                return _header.Offset;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get
            {
                return _header.Size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RawSize
        {
            get
            {
                return _header.RawSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown2
        {
            get
            {
                return _header.Unknown2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown3
        {
            get
            {
                return _header.Unknown3;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get
            {
                return _header.Width;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PixelWidth
        {
            get
            {
                return Width / 2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PixelHeight
        {
            get
            {
                return ByteCount / Width;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown4
        {
            get
            {
                return _header.Unknown4;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ByteCount
        {
            get
            {
                return _header.ByteCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown5
        {
            get
            {
                return _header.Unknown5;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short OriginX
        {
            get
            {
                return _header.OriginX;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short OriginY
        {
            get
            {
                return _header.OriginY;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int OriginFlags
        {
            get
            {
                return _header.OriginFlags;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short Pad1X
        {
            get
            {
                return _header.Pad1X;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short Pad1Y
        {
            get
            {
                return _header.Pad1Y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Pad1Flags
        {
            get
            {
                return _header.Pad1Flags;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short Pad2X
        {
            get
            {
                return _header.Pad2X;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short Pad2Y
        {
            get
            {
                return _header.Pad2Y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Pad2Flags
        {
            get
            {
                return _header.Pad2Flags;
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
        /// <returns></returns>
        public Bitmap Render()
        {
            var bitmap = new Bitmap(PixelWidth, PixelHeight, PixelFormat.Format16bppRgb565);
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, PixelWidth, PixelHeight), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            IntPtr scan0 = bitmapData.Scan0;
            Marshal.Copy(_data, 0, scan0, ByteCount);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }

        #endregion Public/Protected
    }

    public class EFAFrameHeader
    {
        #region Fields

        private int _unknown1;
        private int _offset;
        private int _size;
        private int _rawSize;
        private int _unknown2;
        private int _unknown3;
        private int _width;
        private int _unknown4;
        private int _byteCount;
        private int _unknown5;
        private short _originX;
        private short _originY;
        private int _originFlags;
        private short _pad1X;
        private short _pad1Y;
        private int _pad1Flags;
        private short _pad2X;
        private short _pad2Y;
        private int _pad2Flags;

        #endregion Fields


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static EFAFrameHeader Read(BinaryReader reader)
        {
            return new EFAFrameHeader(reader);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown1
        {
            get
            {
                return _unknown1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Offset
        {
            get
            {
                return _offset;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Size
        {
            get
            {
                return _size;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RawSize
        {
            get
            {
                return _rawSize;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown2
        {
            get
            {
                return _unknown2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown3
        {
            get
            {
                return _unknown3;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown4
        {
            get
            {
                return _unknown4;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ByteCount
        {
            get
            {
                return _byteCount;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Unknown5
        {
            get
            {
                return _unknown5;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short OriginX
        {
            get
            {
                return _originX;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short OriginY
        {
            get
            {
                return _originY;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int OriginFlags
        {
            get
            {
                return _originFlags;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short Pad1X
        {
            get
            {
                return _pad1X;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short Pad1Y
        {
            get
            {
                return _pad1Y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Pad1Flags
        {
            get
            {
                return _pad1Flags;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short Pad2X
        {
            get
            {
                return _pad2X;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public short Pad2Y
        {
            get
            {
                return _pad2Y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Pad2Flags
        {
            get
            {
                return _pad2Flags;
            }
        }

        #endregion Public/Protected


        #region Private/Internal

        private EFAFrameHeader(BinaryReader reader)
        {
            _unknown1 = reader.ReadInt32();
            _offset = reader.ReadInt32();
            _size = reader.ReadInt32();
            _rawSize = reader.ReadInt32();
            _unknown2 = reader.ReadInt32();
            _unknown3 = reader.ReadInt32();
            _width = reader.ReadInt32();
            _unknown4 = reader.ReadInt32();
            _byteCount = reader.ReadInt32();
            _unknown5 = reader.ReadInt32();
            _originX = reader.ReadInt16();
            _originY = reader.ReadInt16();
            _originFlags = reader.ReadInt32();
            _pad1X = reader.ReadInt16();
            _pad1Y = reader.ReadInt16();
            _pad1Flags = reader.ReadInt32();
            _pad2X = reader.ReadInt16();
            _pad2Y = reader.ReadInt16();
            _pad2Flags = reader.ReadInt32();
        }

        #endregion
    }
}
