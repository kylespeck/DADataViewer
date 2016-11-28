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
    public partial class MonstersForm : Form
    {
        private Bitmap _monsterTileImage;

        private string _selectedFileName;
        private int _selectedFileNumber;

        public MonstersForm()
        {
            InitializeComponent();
            _monsterTileImage = new Bitmap(1, 1);
        }

        private void MonstersForm_Load(object sender, EventArgs e)
        {
            Settings.Default.VerifyHadesLocation();

            using (var archive = DataArchive.Open(Settings.Default.HadesLocation))
            {
                var entries = from entry in archive.Entries
                              where EntryIsMonster(entry)
                              orderby int.Parse(entry.EntryName.Substring(3, 3))
                              select entry.EntryName;
                monsterFilesListBox.Items.AddRange(entries.ToArray());
            }

            if (monsterFilesListBox.Items.Count > 0)
            {
                monsterFilesListBox.SelectedIndex = 0;
            }
        }

        private void monsterFilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFileName = (string)monsterFilesListBox.SelectedItem;
            _selectedFileNumber = int.Parse(_selectedFileName.Substring(3, 3));
            UpdateMonsterTileImage();
            monsterTileLabel.Text = "Tile: " + _selectedFileNumber;
        }

        private void monsterTilePanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_monsterTileImage, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
        }

        private void UpdateMonsterTileImage()
        {
            Settings.Default.VerifyHadesLocation();

            using (var archive = DataArchive.Open(Settings.Default.HadesLocation))
            {
                var mpf = new MPFFile(archive.GetEntry(_selectedFileName));
                var pal = new Palette(archive.GetEntry($"mns{mpf.PaletteNumber:d3}.pal"));

                int frontStopFrameIndex;

                int stopFrameCount = Math.Max(mpf.StopFrameCount, mpf.StopMotionFrameCount);

                if (mpf.WalkFrameIndex == 0 && mpf.WalkFrameCount > stopFrameCount)
                {
                    frontStopFrameIndex = mpf.WalkFrameCount;
                }
                else
                {
                    frontStopFrameIndex = stopFrameCount;
                }

                if (frontStopFrameIndex >= mpf.Frames.Count)
                {
                    frontStopFrameIndex = 0;
                }

                var frame = mpf.Frames[frontStopFrameIndex];

                _monsterTileImage = new Bitmap(frame.Width, frame.Height);

                using (var g = Graphics.FromImage(_monsterTileImage))
                {
                    g.DrawImage(frame.Render(pal), 0, 0);
                }
            }

            monsterTilePanel.Refresh();
        }

        private bool EntryIsMonster(DataArchiveEntry entry)
        {
            int monsterTile;
            string name = Path.GetFileNameWithoutExtension(entry.EntryName);
            string ext = Path.GetExtension(entry.EntryName);

            return name.StartsWith("mns", StringComparison.CurrentCultureIgnoreCase) && name.Length == 6
                && ext.Equals(".mpf", StringComparison.CurrentCultureIgnoreCase)
                && int.TryParse(name.Substring(3, 3), out monsterTile);
        }
    }
}
