using DarkAgesLib.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;

namespace DarkAgesLib.Drawing
{
    public class EPFFile : IEnumerable<EPFFrame>
    {
        #region Fields

        private int _expectedNumberOfFrames;
        private int _width;
        private int _height;
        private byte[] _unknownBytes;
        private int _tocAddress;

        private EPFFrame[] _frames;
        private ReadOnlyCollection<EPFFrame> _framesCollection;

        #endregion Fields


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public EPFFile(Stream stream)
        {
            Init(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public EPFFile(DataArchiveEntry entry)
        {
            using (var stream = entry.Open())
            {
                Init(stream);
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
        public int Height
        {
            get
            {
                return _height;
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
        public ReadOnlyCollection<EPFFrame> Frames
        {
            get
            {
                return _framesCollection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public EPFFrame this[int index]
        {
            get
            {
                return _frames[index];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<EPFFrame> GetEnumerator()
        {
            return ((IEnumerable<EPFFrame>)_frames).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<EPFFrame>)_frames).GetEnumerator();
        }

        #endregion Public/Protected


        #region Private/Internal

        private void Init(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, true))
            {
                stream.Seek(0, SeekOrigin.Begin);

                _expectedNumberOfFrames = reader.ReadInt16();
                _width = reader.ReadInt16();
                _height = reader.ReadInt16();
                _unknownBytes = reader.ReadBytes(2);
                _tocAddress = reader.ReadInt32() + 12;

                _frames = new EPFFrame[_expectedNumberOfFrames];
                _framesCollection = new ReadOnlyCollection<EPFFrame>(_frames);

                for (int i = 0; i < _expectedNumberOfFrames; ++i)
                {
                    stream.Seek(_tocAddress + i * 16, SeekOrigin.Begin);

                    int top = reader.ReadInt16();
                    int left = reader.ReadInt16();
                    int bottom = reader.ReadInt16();
                    int right = reader.ReadInt16();

                    int width = right - left;
                    int height = bottom - top;

                    int startAddress = reader.ReadInt32() + 12;
                    int endAddress = reader.ReadInt32() + 12;

                    stream.Seek(startAddress, SeekOrigin.Begin);

                    byte[] data;

                    if (endAddress - startAddress == width * height)
                    {
                        data = reader.ReadBytes(endAddress - startAddress);
                    }
                    else
                    {
                        data = reader.ReadBytes(_tocAddress - startAddress);
                    }

                    _frames[i] = new EPFFrame(top, left, bottom, right, data);
                }
            }
        }

        #endregion Private/Internal
    }

    public class EPFFrame
    {
        #region Fields

        private int _top;
        private int _left;
        private int _bottom;
        private int _right;
        private byte[] _data;
        private ReadOnlyCollection<byte> _dataCollection;

        #endregion


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        /// <param name="data"></param>
        public EPFFrame(int top, int left, int bottom, int right, byte[] data)
        {
            _top = top;
            _left = left;
            _bottom = bottom;
            _right = right;
            _data = new byte[data.Length];
            Buffer.BlockCopy(data, 0, _data, 0, data.Length);
            _dataCollection = new ReadOnlyCollection<byte>(_data);
        }

        /// <summary>
        /// 
        /// </summary>
        public int Top
        {
            get
            {
                return _top;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Left
        {
            get
            {
                return _left;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Bottom
        {
            get
            {
                return _bottom;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Right
        {
            get
            {
                return _right;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Width
        {
            get
            {
                return Right - Left;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Height
        {
            get
            {
                return Bottom - Top;
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
    }
}
