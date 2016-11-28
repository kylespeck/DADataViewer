using DarkAgesLib.Data;
using DADataViewer.Properties;
using NAudio.Wave;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DADataViewer
{
    public partial class SoundsForm : Form
    {
        private Thread _soundThread;

        public SoundsForm()
        {
            InitializeComponent();
        }

        private void SoundsForm_Load(object sender, EventArgs e)
        {
            Settings.Default.VerifyLegendLocation();

            using (var archive = DataArchive.Open(Settings.Default.LegendLocation))
            {
                int soundNumber;
                var entries = from entry in archive.Entries
                              let name = Path.GetFileNameWithoutExtension(entry.EntryName)
                              let ext = Path.GetExtension(entry.EntryName)
                              where ext.Equals(".mp3", StringComparison.CurrentCultureIgnoreCase) && int.TryParse(name, out soundNumber)
                              orderby int.Parse(name)
                              select entry.EntryName;
                soundFilesListBox.Items.AddRange(entries.ToArray());
            }

            if (soundFilesListBox.Items.Count > 0)
            {
                soundFilesListBox.SelectedIndex = 0;
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (soundFilesListBox.SelectedIndex < 0)
            {
                return;
            }

            Settings.Default.VerifyLegendLocation();

            TogglePlayButtonEnabled(false);

            string fileName = (string)soundFilesListBox.SelectedItem;

            _soundThread = new Thread(PlaySoundAsync);
            _soundThread.Start(soundFilesListBox.SelectedItem);
        }

        private void TogglePlayButtonEnabled(bool enabled)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(TogglePlayButtonEnabled), enabled);
                return;
            }

            playButton.Enabled = enabled;
        }

        private void PlaySoundAsync(object parameter)
        {
            string fileName = (string)parameter;

            using (var archive = DataArchive.Open(Settings.Default.LegendLocation))
            using (var stream = archive.GetEntry(fileName).Open())
            {
                stream.Position = 0;
                using (var reader = new Mp3FileReader(stream))
                using (var conversionStream = WaveFormatConversionStream.CreatePcmStream(reader))
                using (var provider = new BlockAlignReductionStream(conversionStream))
                using (var waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                {
                    waveOut.Init(provider);
                    waveOut.Play();

                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }

            TogglePlayButtonEnabled(true);
        }
    }
}
