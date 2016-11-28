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
    public class MPFFile : IEnumerable<MPFFrame>
    {
        #region Fields

        private int _expectedNumberOfFrames;
        private int _width;
        private int _height;

        private byte[] _unknownBytes;
        private int _stopFrameIndex;
        private int _stopFrameCount;
        private int _walkFrameIndex;
        private int _walkFrameCount;
        private int _attackFrameIndex;
        private int _attackFrameCount;
        private int _stopMotionFrameCount;
        private int _stopMotionProbability;
        private int _attack2FrameIndex;
        private int _attack2FrameCount;
        private int _attack3FrameIndex;
        private int _attack3FrameCount;
        private int _paletteNumber;

        private List<MPFFrame> _frames;
        private ReadOnlyCollection<MPFFrame> _framesCollection;

        #endregion Fields


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public MPFFile(Stream stream)
        {
            Init(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public MPFFile(DataArchiveEntry entry) : this(entry.Open())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public MPFFile(string fileName) : this(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
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

            set
            {
                if (_width != value)
                {
                    _width = value;
                }
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

            set
            {
                if (_height != value)
                {
                    _height = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Size Size
        {
            get
            {
                return new Size(Width, Height);
            }

            set
            {
                if (Size != value)
                {
                    _width = value.Width;
                    _height = value.Height;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Unknown1
        {
            get
            {
                if (_unknownBytes == null)
                {
                    _unknownBytes = new byte[0];
                }
                return _unknownBytes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StopFrameIndex
        {
            get
            {
                return _stopFrameIndex;
            }

            set
            {
                if (_stopFrameIndex != value)
                {
                    _stopFrameIndex = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StopFrameCount
        {
            get
            {
                return _stopFrameCount;
            }

            set
            {
                _stopFrameCount = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int WalkFrameIndex
        {
            get
            {
                return _walkFrameIndex;
            }

            set
            {
                _walkFrameIndex = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int WalkFrameCount
        {
            get
            {
                return _walkFrameCount;
            }

            set
            {
                _walkFrameCount = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AttackFrameIndex
        {
            get
            {
                return _attackFrameIndex;
            }

            set
            {
                _attackFrameIndex = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int AttackFrameCount
        {
            get
            {
                return _attackFrameCount;
            }

            set
            {
                _attackFrameCount = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StopMotionFrameCount
        {
            get
            {
                return _stopMotionFrameCount;
            }

            set
            {
                _stopMotionFrameCount = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StopMotionProbability
        {
            get
            {
                return _stopMotionProbability;
            }

            set
            {
                _stopMotionProbability = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Attack2StartIndex
        {
            get
            {
                return _attack2FrameIndex;
            }

            set
            {
                _attack2FrameIndex = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Attack2FrameCount
        {
            get
            {
                return _attack2FrameCount;
            }

            set
            {
                _attack2FrameCount = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Attack3StartIndex
        {
            get
            {
                return _attack3FrameIndex;
            }

            set
            {
                _attack3FrameIndex = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Attack3FrameCount
        {
            get
            {
                return _attack3FrameCount;
            }

            set
            {
                _attack3FrameCount = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PaletteNumber
        {
            get
            {
                return _paletteNumber;
            }

            set
            {
                _paletteNumber = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<MPFFrame> Frames
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
        public MPFFrame this[int index]
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
        public IEnumerator<MPFFrame> GetEnumerator()
        {
            return ((IEnumerable<MPFFrame>)_frames).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<MPFFrame>)_frames).GetEnumerator();
        }

        #endregion Public/Protected


        #region Private/Internal

        private void Init(Stream stream)
        {
            using (var reader = new BinaryReader(stream, Encoding.Default, true))
            {
                stream.Seek(0, SeekOrigin.Begin);

                if (reader.ReadInt32() == -1)
                {
                    int something = reader.ReadInt32();

                    if (something == 4)
                    {
                        _unknownBytes = reader.ReadBytes(8);
                    }
                    else
                    {
                        _unknownBytes = BitConverter.GetBytes(something);
                    }
                }
                else
                {
                    _unknownBytes = new byte[0];
                    stream.Seek(-4, SeekOrigin.Current);
                }

                _expectedNumberOfFrames = reader.ReadByte();

                _frames = new List<MPFFrame>();
                _framesCollection = new ReadOnlyCollection<MPFFrame>(_frames);

                _width = reader.ReadInt16();
                _height = reader.ReadInt16();

                int expectedDataSize = reader.ReadInt32();

                _walkFrameIndex = reader.ReadByte();
                _walkFrameCount = reader.ReadByte();

                if (reader.ReadInt16() == -1)
                {
                    _stopFrameIndex = reader.ReadByte();
                    _stopFrameCount = reader.ReadByte();
                    _stopMotionFrameCount = reader.ReadByte();
                    _stopMotionProbability = reader.ReadByte();
                    _attackFrameIndex = reader.ReadByte();
                    _attackFrameCount = reader.ReadByte();
                    _attack2FrameIndex = reader.ReadByte();
                    _attack2FrameCount = reader.ReadByte();
                    _attack3FrameIndex = reader.ReadByte();
                    _attack3FrameCount = reader.ReadByte();
                }
                else
                {
                    stream.Seek(-2, SeekOrigin.Current);
                    _attackFrameIndex = reader.ReadByte();
                    _attackFrameCount = reader.ReadByte();
                    _stopFrameIndex = reader.ReadByte();
                    _stopFrameCount = reader.ReadByte();
                    _stopMotionFrameCount = reader.ReadByte();
                    _stopMotionProbability = reader.ReadByte();
                }

                long dataStart = stream.Length - expectedDataSize;

                for (int i = 0; i < _expectedNumberOfFrames; ++i)
                {
                    int left = reader.ReadInt16();
                    int top = reader.ReadInt16();
                    int right = reader.ReadInt16();
                    int bottom = reader.ReadInt16();
                    int xOffset = reader.ReadInt16(true);
                    int yOffset = reader.ReadInt16(true);
                    int startAddress = reader.ReadInt32();

                    if (left == -1 && top == -1)
                    {
                        _paletteNumber = startAddress;
                        --_expectedNumberOfFrames;
                        continue;
                    }

                    int frameWidth = right - left;
                    int frameHeight = bottom - top;

                    byte[] data;

                    if (frameWidth > 0 && frameHeight > 0)
                    {
                        long position = stream.Position;

                        stream.Seek(dataStart + startAddress, SeekOrigin.Begin);

                        data = reader.ReadBytes(frameWidth * frameHeight);

                        stream.Seek(position, SeekOrigin.Begin);
                    }
                    else
                    {
                        data = new byte[0];
                    }

                    _frames.Add(new MPFFrame(top, left, bottom, right, xOffset, yOffset, data));
                }
            }
        }

        #endregion
    }

    public class MPFFrame
    {
        #region Fields

        private int _top;
        private int _left;
        private int _bottom;
        private int _right;
        private int _xOffset;
        private int _yOffset;
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
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <param name="data"></param>
        public MPFFrame(int top, int left, int bottom, int right, int xOffset, int yOffset, byte[] data)
        {
            _top = top;
            _left = left;
            _bottom = bottom;
            _right = right;
            _xOffset = xOffset;
            _yOffset = yOffset;
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
