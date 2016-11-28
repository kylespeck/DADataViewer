using System;
using System.IO;
using System.Windows.Forms;

namespace DADataViewer.Properties
{
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings
    {
        public Settings()
        {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }

        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            // Add code to handle the SettingChangingEvent event here.
        }

        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Add code to handle the SettingsSaving event here.
        }

        internal void SetDefaultValues()
        {
            string programFilesLocation;

            if (Environment.Is64BitOperatingSystem)
            {
                programFilesLocation = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
            }
            else
            {
                programFilesLocation = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            }

            string darkAgesLocation = Path.Combine(programFilesLocation, "KRU", "Dark Ages");

            if (string.IsNullOrEmpty(HadesLocation))
            {
                HadesLocation = Path.Combine(darkAgesLocation, "hades.dat");
            }

            if (string.IsNullOrEmpty(LegendLocation))
            {
                LegendLocation = Path.Combine(darkAgesLocation, "Legend.dat");
            }

            if (string.IsNullOrEmpty(RohLocation))
            {
                RohLocation = Path.Combine(darkAgesLocation, "roh.dat");
            }

            if (string.IsNullOrEmpty(SetoaLocation))
            {
                SetoaLocation = Path.Combine(darkAgesLocation, "setoa.dat");
            }

            if (string.IsNullOrEmpty(KhanmadLocation))
            {
                KhanmadLocation = Path.Combine(darkAgesLocation, "khanmad.dat");
            }

            if (string.IsNullOrEmpty(KhanwadLocation))
            {
                KhanwadLocation = Path.Combine(darkAgesLocation, "khanwad.dat");
            }

            if (string.IsNullOrEmpty(KhanpalLocation))
            {
                KhanpalLocation = Path.Combine(darkAgesLocation, "khanpal.dat");
            }
        }

        internal void VerifyHadesLocation()
        {
            if (string.IsNullOrEmpty(HadesLocation) || !File.Exists(HadesLocation))
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "hades.dat|hades.dat";
                    dialog.Title = "Please locate your hades.dat file...";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        HadesLocation = dialog.FileName;
                        Save();
                    }
                }
            }
        }

        internal void VerifyLegendLocation()
        {
            if (string.IsNullOrEmpty(LegendLocation) || !File.Exists(LegendLocation))
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Legend.dat|Legend.dat";
                    dialog.Title = "Please locate your Legend.dat file...";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        LegendLocation = dialog.FileName;
                        Save();
                    }
                }
            }
        }

        internal void VerifyRohLocation()
        {
            if (string.IsNullOrEmpty(RohLocation) || !File.Exists(RohLocation))
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "roh.dat|roh.dat";
                    dialog.Title = "Please locate your roh.dat file...";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        RohLocation = dialog.FileName;
                        Save();
                    }
                }
            }
        }

        internal void VerifySetoaLocation()
        {
            if (string.IsNullOrEmpty(SetoaLocation) || !File.Exists(SetoaLocation))
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "setoa.dat|setoa.dat";
                    dialog.Title = "Please locate your setoa.dat file...";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        SetoaLocation = dialog.FileName;
                        Save();
                    }
                }
            }
        }

        internal void VerifyKhanmadLocation()
        {
            if (string.IsNullOrEmpty(KhanmadLocation) || !File.Exists(KhanmadLocation))
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "khanmad.dat|khanmad.dat";
                    dialog.Title = "Please locate your khanmad.dat file...";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        KhanmadLocation = dialog.FileName;
                        Save();
                    }
                }
            }
        }

        internal void VerifyKhanwadLocation()
        {
            if (string.IsNullOrEmpty(KhanwadLocation) || !File.Exists(KhanwadLocation))
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "khanwad.dat|khanwad.dat";
                    dialog.Title = "Please locate your khanwad.dat file...";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        KhanwadLocation = dialog.FileName;
                        Save();
                    }
                }
            }
        }

        internal void VerifyKhanpalLocation()
        {
            if (string.IsNullOrEmpty(KhanpalLocation) || !File.Exists(KhanpalLocation))
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = "khanpal.dat|khanpal.dat";
                    dialog.Title = "Please locate your khanpal.dat file...";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        KhanpalLocation = dialog.FileName;
                        Save();
                    }
                }
            }
        }
    }
}
