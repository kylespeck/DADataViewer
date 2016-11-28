using DarkAgesLib.Data;
using DarkAgesLib.Drawing;
using DADataViewer.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DADataViewer
{
    public partial class SkillsForm : Form
    {
        private static readonly int columns = 19;
        private static readonly int rows = 14;
        private static readonly int tileWidth = 32;
        private static readonly int tileHeight = 32;
        private static readonly Color hoverPenColor = Color.CornflowerBlue;
        private static readonly Color hoverBrushColor = Color.FromArgb(140, hoverPenColor);

        private Bitmap _skillTilesImage;
        private Point _cursorLocation = new Point(-1, -1);

        private string _fileName;

        public SkillsForm(string title, string fileName)
        {
            InitializeComponent();
            Text = title;
            _fileName = fileName;
            _skillTilesImage = new Bitmap(columns * tileWidth, rows * tileHeight);
        }

        public Point CursorLocation
        {
            get
            {
                return _cursorLocation;
            }

            set
            {
                if (_cursorLocation != value)
                {
                    if (_cursorLocation.X != -1 && _cursorLocation.Y != -1)
                    {
                        skillTilesPanel.Invalidate(new Rectangle(_cursorLocation.X * tileWidth, _cursorLocation.Y * tileHeight, tileWidth, tileHeight));
                    }
                    _cursorLocation = value;
                    skillTilesPanel.Invalidate(new Rectangle(_cursorLocation.X * tileWidth, _cursorLocation.Y * tileHeight, tileWidth, tileHeight));
                    skillTilesPanel.Update();
                    if (_cursorLocation.X != -1 && _cursorLocation.Y != -1)
                    {
                        skillTileLabel.Text = "Tile: " + (columns * _cursorLocation.Y + _cursorLocation.X);
                    }
                    else
                    {
                        skillTileLabel.Text = "No tile selected";
                    }
                }
            }
        }

        private void SkillsForm_Load(object sender, EventArgs e)
        {
            Settings.Default.VerifySetoaLocation();

            using (var archive = DataArchive.Open(Settings.Default.SetoaLocation))
            using (var g = Graphics.FromImage(_skillTilesImage))
            {
                var epf = new EPFFile(archive.GetEntry(_fileName));
                var pal = new Palette(archive.GetEntry("gui06.pal"));

                for (int col = 0; col < columns; ++col)
                {
                    for (int row = 0; row < rows; ++row)
                    {
                        int frameIndex = columns * row + col;
                        if (frameIndex < epf.Frames.Count)
                        {
                            var frame = epf.Frames[frameIndex];
                            var frameImage = frame.Render(pal);
                            g.DrawImage(frameImage, col * tileWidth + frame.Left, row * tileHeight + frame.Top);
                        }
                    }
                }
            }
        }

        private void skillTilesPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_skillTilesImage, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            if (_cursorLocation.X != -1 && _cursorLocation.Y != -1)
            {
                using (var brush = new SolidBrush(hoverBrushColor))
                {
                    e.Graphics.FillRectangle(brush, _cursorLocation.X * tileWidth, _cursorLocation.Y * tileHeight, tileWidth, tileHeight);
                }

                using (var pen = new Pen(hoverPenColor))
                {
                    e.Graphics.DrawRectangle(pen, _cursorLocation.X * tileWidth, _cursorLocation.Y * tileHeight, tileWidth - 1, tileHeight - 1);
                }
            }
        }

        private void skillTilesPanel_MouseMove(object sender, MouseEventArgs e)
        {
            CursorLocation = new Point(e.X / tileWidth, e.Y / tileHeight);
        }

        private void skillTilesPanel_MouseLeave(object sender, EventArgs e)
        {
            CursorLocation = new Point(-1, -1);
        }
    }
}
