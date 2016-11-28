using DarkAgesLib.Data;
using DarkAgesLib.Drawing;
using DADataViewer.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DADataViewer
{
    public partial class MotionsForm : Form
    {
        private Bitmap _motionTilesImage;

        private MotionInfo _selectedMotion;

        private int _animationFrame = -1;

        public MotionsForm()
        {
            InitializeComponent();

            _motionTilesImage = new Bitmap(100, 100);

            genderListBox.SelectedIndex = 0;
            genderListBox.SelectedIndexChanged += genderListBox_SelectedIndexChanged;

            motionTilesListBox.Items.Add(new MotionInfo(1, 0, 0, 2));
            motionTilesListBox.Items.Add(new MotionInfo(6, 0, 0, 1));
            motionTilesListBox.Items.Add(new MotionInfo(128, 1, 0, 3));
            motionTilesListBox.Items.Add(new MotionInfo(129, 2, 0, 4));
            motionTilesListBox.Items.Add(new MotionInfo(130, 2, 8, 3));
            motionTilesListBox.Items.Add(new MotionInfo(131, 3, 0, 3));
            motionTilesListBox.Items.Add(new MotionInfo(132, 3, 6, 2));
            motionTilesListBox.Items.Add(new MotionInfo(133, 3, 10, 4));
            motionTilesListBox.Items.Add(new MotionInfo(134, 4, 0, 2));
            motionTilesListBox.Items.Add(new MotionInfo(135, 4, 4, 2));
            motionTilesListBox.Items.Add(new MotionInfo(136, 5, 0, 2));
            motionTilesListBox.Items.Add(new MotionInfo(137, 1, 6, 3));
            motionTilesListBox.Items.Add(new MotionInfo(138, 1, 12, 1));
            motionTilesListBox.Items.Add(new MotionInfo(139, 2, 14, 2));
            motionTilesListBox.Items.Add(new MotionInfo(140, 2, 18, 3));
            motionTilesListBox.Items.Add(new MotionInfo(141, 2, 24, 3));
            motionTilesListBox.Items.Add(new MotionInfo(142, 4, 8, 4));
            motionTilesListBox.Items.Add(new MotionInfo(143, 4, 16, 6));
            motionTilesListBox.Items.Add(new MotionInfo(144, 4, 28, 4));
            motionTilesListBox.Items.Add(new MotionInfo(145, 5, 4, 4));

            motionTilesListBox.SelectedIndexChanged += motionTilesListBox_SelectedIndexChanged;
            motionTilesListBox.SelectedIndex = 0;
        }

        private void MotionsForm_Load(object sender, EventArgs e)
        {
        }

        private void MotionsForm_VisibleChanged(object sender, EventArgs e)
        {
            animationTimer.Enabled = Visible;
        }

        private void genderListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _animationFrame = -1;
            UpdateMotionTilesImage();
        }

        private void motionTilesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedMotion = motionTilesListBox.SelectedItem as MotionInfo;
            _animationFrame = -1;
            UpdateMotionTilesImage();
            motionTileLabel.Text = "Motion: " + _selectedMotion.ToString();
        }

        private void motionTilesPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_motionTilesImage, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            _animationFrame++;
            UpdateMotionTilesImage();
        }

        private void UpdateMotionTilesImage()
        {
            string archiveFileName, idleFileName, motionFileName;

            if (genderListBox.SelectedIndex == 0)
            {
                animationTimer.Stop();
                Settings.Default.VerifyKhanmadLocation();
                animationTimer.Start();
                archiveFileName = Settings.Default.KhanmadLocation;

                idleFileName = "mb00101.epf";

                if (_selectedMotion.Number == 1)
                {
                    motionFileName = "mb00102.epf";
                }
                else if (_selectedMotion.Number == 6)
                {
                    motionFileName = "mb00103.epf";
                }
                else
                {
                    motionFileName = $"mb001{(char)('a' + _selectedMotion.FileNumber)}.epf";
                }
            }
            else
            {
                animationTimer.Stop();
                Settings.Default.VerifyKhanwadLocation();
                animationTimer.Start();
                archiveFileName = Settings.Default.KhanwadLocation;

                idleFileName = "wb00101.epf";

                if (_selectedMotion.Number == 1)
                {
                    motionFileName = "wb00102.epf";
                }
                else if (_selectedMotion.Number == 6)
                {
                    motionFileName = "wb00103.epf";
                }
                else
                {
                    motionFileName = $"wb001{(char)('a' + _selectedMotion.FileNumber)}.epf";
                }
            }

            animationTimer.Stop();
            Settings.Default.VerifyKhanpalLocation();
            animationTimer.Start();

            using (var epfArchive = DataArchive.Open(archiveFileName))
            using (var palArchive = DataArchive.Open(Settings.Default.KhanpalLocation))
            {
                var idleEpf = new EPFFile(epfArchive.GetEntry(idleFileName));
                var motionEpf = new EPFFile(epfArchive.GetEntry(motionFileName));
                var pal = new Palette(palArchive.GetEntry("palb000.pal"));

                EPFFrame frame;

                if (_animationFrame >= _selectedMotion.FrameCount)
                {
                    _animationFrame = -1;
                }

                if (_animationFrame == -1)
                {
                    frame = idleEpf.Frames[5];
                }
                else
                {
                    frame = motionEpf.Frames[_selectedMotion.StartIndex + _selectedMotion.FrameCount + _animationFrame];
                }

                using (var g = Graphics.FromImage(_motionTilesImage))
                {
                    g.Clear(motionTilesPanel.BackColor);
                    g.DrawImage(frame.Render(pal), frame.Left, frame.Top);
                }
            }

            motionTilesPanel.Refresh();
        }
    }

    public class MotionInfo
    {
        private int _number;
        private int _fileNumber;
        private int _startIndex;
        private int _frameCount;

        public MotionInfo(int number, int fileNumber, int startIndex, int frameCount)
        {
            _number = number;
            _fileNumber = fileNumber;
            _startIndex = startIndex;
            _frameCount = frameCount;
        }

        public int Number
        {
            get
            {
                return _number;
            }

            set
            {
                _number = value;
            }
        }

        public int FileNumber
        {
            get
            {
                return _fileNumber;
            }

            set
            {
                _fileNumber = value;
            }
        }

        public int StartIndex
        {
            get
            {
                return _startIndex;
            }

            set
            {
                _startIndex = value;
            }
        }

        public int FrameCount
        {
            get
            {
                return _frameCount;
            }

            set
            {
                _frameCount = value;
            }
        }

        public override string ToString()
        {
            return _number.ToString();
        }
    }
}
