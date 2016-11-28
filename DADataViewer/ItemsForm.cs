using DarkAgesLib.Data;
using DarkAgesLib.Drawing;
using DADataViewer.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DADataViewer
{
    public partial class ItemsForm : Form
    {
        private static readonly int columns = 19;
        private static readonly int rows = 14;
        private static readonly int tileWidth = 32;
        private static readonly int tileHeight = 32;
        private static readonly Color hoverPenColor = Color.CornflowerBlue;
        private static readonly Color hoverBrushColor = Color.FromArgb(140, hoverPenColor);

        private Bitmap _itemTilesImage;
        private Point _cursorLocation = new Point(-1, -1);

        private string _selectedFileName;
        private int _selectedFileNumber;

        private PaletteTable _paletteTable;

        public ItemsForm()
        {
            InitializeComponent();
            _itemTilesImage = new Bitmap(columns * tileWidth, rows * tileHeight);
            _paletteTable = new PaletteTable();
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
                        itemTilesPanel.Invalidate(new Rectangle(_cursorLocation.X * tileWidth, _cursorLocation.Y * tileHeight, tileWidth, tileHeight));
                    }
                    _cursorLocation = value;
                    itemTilesPanel.Invalidate(new Rectangle(_cursorLocation.X * tileWidth, _cursorLocation.Y * tileHeight, tileWidth, tileHeight));
                    itemTilesPanel.Update();
                    if (_cursorLocation.X != -1 && _cursorLocation.Y != -1)
                    {
                        itemTileLabel.Text = "Tile: " + (((_selectedFileNumber - 1) * 266) + (columns * _cursorLocation.Y + _cursorLocation.X) + 1);
                    }
                    else
                    {
                        itemTileLabel.Text = "No tile selected";
                    }
                }
            }
        }

        private void ItemsForm_Load(object sender, EventArgs e)
        {
            Settings.Default.VerifyLegendLocation();

            using (var archive = DataArchive.Open(Settings.Default.LegendLocation))
            {
                var entries = from entry in archive.Entries
                              where EntryIsItem(entry)
                              orderby int.Parse(entry.EntryName.Substring(4, 3))
                              select entry.EntryName;
                itemFilesListBox.Items.AddRange(entries.ToArray());
                _paletteTable.AddTable(archive.GetEntry("itempal.tbl"));
            }

            if (itemFilesListBox.Items.Count > 0)
            {
                itemFilesListBox.SelectedIndex = 0;
            }
        }

        private void itemFilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFileName = (string)itemFilesListBox.SelectedItem;
            _selectedFileNumber = int.Parse(_selectedFileName.Substring(4, 3));
            UpdateItemTilesImage();
        }

        private void itemTilesPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_itemTilesImage, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
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

        private void itemTilesPanel_MouseMove(object sender, MouseEventArgs e)
        {
            CursorLocation = new Point(e.X / tileWidth, e.Y / tileHeight);
        }

        private void itemTilesPanel_MouseLeave(object sender, EventArgs e)
        {
            CursorLocation = new Point(-1, -1);
        }

        private void UpdateItemTilesImage()
        {
            Settings.Default.VerifyLegendLocation();

            using (var archive = DataArchive.Open(Settings.Default.LegendLocation))
            using (var g = Graphics.FromImage(_itemTilesImage))
            {
                g.Clear(itemTilesPanel.BackColor);

                var epf = new EPFFile(archive.GetEntry(_selectedFileName));

                for (int col = 0; col < columns; ++col)
                {
                    for (int row = 0; row < rows; ++row)
                    {
                        int frameIndex = columns * row + col;

                        if (frameIndex < epf.Frames.Count)
                        {
                            var frame = epf.Frames[frameIndex];

                            int tileNumber = (_selectedFileNumber - 1) * 266 + frameIndex + 1;
                            int paletteNumber = _paletteTable.GetPaletteNumber(tileNumber);

                            var palette = new Palette(archive.GetEntry($"item{paletteNumber:d3}.pal"));

                            var frameImage = frame.Render(palette);

                            g.DrawImage(frameImage, col * tileWidth + frame.Left, row * tileHeight + frame.Top);
                        }
                    }
                }
            }

            itemTilesPanel.Refresh();
        }

        private bool EntryIsItem(DataArchiveEntry entry)
        {
            int itemTile;
            string name = Path.GetFileNameWithoutExtension(entry.EntryName);
            string ext = Path.GetExtension(entry.EntryName);

            return name.StartsWith("item", StringComparison.CurrentCultureIgnoreCase) && name.Length == 7
                && ext.Equals(".epf", StringComparison.CurrentCultureIgnoreCase)
                && int.TryParse(name.Substring(4, 3), out itemTile);
        }
    }
}
