using DarkAgesLib.Data;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DarkAgesLib.Drawing
{
    public class Palette
    {
        #region Fields

        private Color[] _colors;
        private ReadOnlyCollection<Color> _colorsCollection;

        #endregion


        #region Public/Protected

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public Palette(Stream stream)
        {
            Init(stream);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        public Palette(DataArchiveEntry entry) : this(entry.Open())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public Palette(string fileName) : this(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<Color> Colors
        {
            get
            {
                return _colorsCollection;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Color this[int index]
        {
            get
            {
                return _colors[index];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Bitmap Render(byte[] imageData, int width, int height)
        {
            if (width == 0 || height == 0)
            {
                return new Bitmap(1, 1);
            }

            var bitmap = new Bitmap(width, height);

            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            IntPtr dataPointer = bitmapData.Scan0;

            byte[] pixelData = new byte[width * height * 4];

            int imageDataIndex = 0;
            int pixelDataIndex = 0;

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    int colorIndex = imageData[imageDataIndex++];

                    Color color;

                    if (colorIndex == 0)
                    {
                        color = Color.Transparent;
                    }
                    else
                    {
                        color = _colors[colorIndex];
                    }

                    pixelData[pixelDataIndex++] = color.B;
                    pixelData[pixelDataIndex++] = color.G;
                    pixelData[pixelDataIndex++] = color.R;
                    pixelData[pixelDataIndex++] = color.A;
                }
            }

            Marshal.Copy(pixelData, 0, dataPointer, pixelData.Length);

            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        #endregion Public/Protected


        #region Private/Internal

        private void Init(Stream stream)
        {
            _colors = new Color[256];
            _colorsCollection = new ReadOnlyCollection<Color>(_colors);

            using (var reader = new BinaryReader(stream, Encoding.Default, true))
            {
                stream.Seek(0, SeekOrigin.Begin);

                for (int i = 0; i < 256; ++i)
                {
                    _colors[i] = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                }
            }
        }

        #endregion Private/Internal
    }
}
