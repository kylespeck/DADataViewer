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
    public partial class EffectsForm : Form
    {
        private Bitmap _effectImage;

        private string _selectedFileName;
        private int _selectedFileNumber;

        private PaletteTable _paletteTable;

        private int _animationFrame;

        public EffectsForm()
        {
            InitializeComponent();
            _effectImage = new Bitmap(1, 1);
            _paletteTable = new PaletteTable();
        }

        private void EffectsForm_Load(object sender, EventArgs e)
        {
            Settings.Default.VerifyRohLocation();

            using (var archive = DataArchive.Open(Settings.Default.RohLocation))
            {
                var entries = from entry in archive.Entries
                              where EntryIsEffect(entry)
                              orderby int.Parse(entry.EntryName.Substring(4, 3))
                              select entry.EntryName;
                effectFilesListBox.Items.AddRange(entries.ToArray());
                _paletteTable.AddTable(archive.GetEntry("effpal.tbl"));
            }

            if (effectFilesListBox.Items.Count > 0)
            {
                effectFilesListBox.SelectedIndex = 0;
            }
        }

        private void EffectsForm_VisibleChanged(object sender, EventArgs e)
        {
            animationTimer.Enabled = Visible;
        }

        private void effectFilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFileName = (string)effectFilesListBox.SelectedItem;
            _selectedFileNumber = int.Parse(_selectedFileName.Substring(4, 3));
            _animationFrame = 0;
            UpdateEffectImage();
            effectNumberLabel.Text = "Effect: " + _selectedFileNumber;
        }

        private void effectPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_effectImage, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            _animationFrame++;
            UpdateEffectImage();
        }

        private void UpdateEffectImage()
        {
            if (_selectedFileName.EndsWith(".efa", StringComparison.CurrentCultureIgnoreCase))
            {
                _effectImage = new Bitmap(300, 100);
                using (var g = Graphics.FromImage(_effectImage))
                {
                    g.DrawString("EFA is not currently implemented.", new Font("GulimChe", 9, FontStyle.Regular), Brushes.White, 10, 10);
                }
                effectPanel.Refresh();
                return;
            }

            animationTimer.Stop();
            Settings.Default.VerifyRohLocation();
            animationTimer.Start();

            using (var archive = DataArchive.Open(Settings.Default.RohLocation))
            {
                var epf = new EPFFile(archive.GetEntry(_selectedFileName));

                if (_animationFrame >= epf.Frames.Count)
                {
                    _animationFrame = 0;
                }

                var frame = epf.Frames[_animationFrame];

                int paletteNumber = _paletteTable.GetPaletteNumber(_selectedFileNumber);

                bool useBlending = false;

                if (paletteNumber >= 1000)
                {
                    paletteNumber -= 1000;
                    useBlending = true;
                }

                var palette = new Palette(archive.GetEntry($"eff{paletteNumber:d3}.pal"));

                _effectImage = new Bitmap(epf.Width, epf.Height);

                using (var g = Graphics.FromImage(_effectImage))
                {
                    if (useBlending)
                    {
                        // temporary solution
                        g.Clear(Color.Black);
                    }
                    g.DrawImage(frame.Render(palette), frame.Left, frame.Top);
                }
            }

            effectPanel.Refresh();
        }

        private bool EntryIsEffect(DataArchiveEntry entry)
        {
            int effectNumber;
            string name = Path.GetFileNameWithoutExtension(entry.EntryName);
            string ext = Path.GetExtension(entry.EntryName);

            return name.StartsWith("efct", StringComparison.CurrentCultureIgnoreCase) && name.Length == 7
                && (ext.Equals(".epf", StringComparison.CurrentCultureIgnoreCase) || ext.Equals(".efa", StringComparison.CurrentCultureIgnoreCase))
                && int.TryParse(name.Substring(4, 3), out effectNumber);
        }
    }
}
