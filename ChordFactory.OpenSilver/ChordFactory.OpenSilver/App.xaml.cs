using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ChordFactory.OpenSilver
{
    using System.ComponentModel;
    using System.Threading.Tasks;
    using repositories;
    using viewModels;

    public sealed partial class App : Application
    {
        private SettingsViewModel settingsViewModel;

        public App(): this(null)
        {
            
        }
        
        public App(string databasePath)
        {
            this.InitializeComponent();

            var designTime = (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;

            // Enter construction logic here...
            if (designTime)
            {
                this.SettingsRepository = new FakeSettingsRepository();
            }
            else
            {
               // this.SettingsRepository = new SettingsRepository(databasePath);
            }

            //Task.Run(this.LoadSettings);
            this.ChordDataViewModel = new ChordDataViewModel { Settings = this.SettingsViewModel, MusicData = this.MusicData };
            this.FinderViewModel = new FinderViewModel { Settings = this.SettingsViewModel, MusicData =  this.MusicData};
            this.SettingsPage = new SettingsPage();
            var mainPage = new MainPage();
            Window.Current.Content = mainPage;
        }

        public MusicData MusicData => new MusicData();

        public SettingsPage SettingsPage { get; set; }

        public FinderViewModel FinderViewModel { get; set; }

        public ChordDataViewModel ChordDataViewModel { get; set; }

        private async Task LoadSettings()
        {
            var loadedSettings = await this.SettingsRepository.GetSettings();
            if (loadedSettings != null)
            {
                this.SettingsViewModel.ArpeggiateIsEnabled = loadedSettings.ArpeggiateChord;
                this.SettingsViewModel.AudioIsEnabled = loadedSettings.PlaySelection;
                this.SettingsViewModel.SetColourFromName("BlackKeySelectedChordColour", loadedSettings.BlackKeySelectedChordColour);
                this.SettingsViewModel.SetColourFromName("WhiteKeySelectedChordColour", loadedSettings.WhiteKeySelectedChordColour);
                this.SettingsViewModel.SetColourFromName("BlackKeySelectedScaleColour", loadedSettings.BlackKeySelectedScaleColour);
                this.SettingsViewModel.SetColourFromName("WhiteKeySelectedScaleColour", loadedSettings.WhiteKeySelectedScaleColour);
            }
        }

        public SettingsViewModel SettingsViewModel
        {
            get => this.settingsViewModel ?? (this.settingsViewModel = new SettingsViewModel());
            set => this.settingsViewModel = value;
        }

        public ISettingsRepository SettingsRepository { get; set; }
    }
}
