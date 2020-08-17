namespace ChordFactory.OpenSilver.views
{
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using controls;
    using extensions;
    using models;

    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            this.InitializeComponent();
            if (this.DataContext == null)
            {
                this.DataContext = this.CurrentApp.SettingsViewModel;
            }

            //this.ExplicitPickerControlBinding();
            //this.ExplicitPickerControlDirectBinding();
        }

        private void ExplicitPickerControlDirectBinding()
        {
            this.WhiteChordColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.WhiteKeySelectedChordColour;
            this.BlackScaleColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.BlackKeySelectedScaleColour;
            this.WhiteScaleColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.WhiteKeySelectedScaleColour;
            this.BlackFinderColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.BlackKeySelectedFinderColour;
            this.WhiteFinderColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.WhiteKeySelectedFinderColour;

            this.WhiteChordColourPickerControl.PropertyChanged += this.WhiteChordColourPickerPropertyChanged;
            this.BlackScaleColourPickerControl.PropertyChanged += this.BlackScaleColourPickerPropertyChanged;
            this.WhiteScaleColourPickerControl.PropertyChanged += this.WhiteScaleColourPickerPropertyChanged;
            this.BlackFinderColourPickerControl.PropertyChanged += this.BlackFinderColourPickerPropertyChanged;
            this.WhiteFinderColourPickerControl.PropertyChanged += this.WhiteFinderColourPickerPropertyChanged;
        }

        private void ExplicitPickerControlBinding()
        {
            this.BlackChordColourPickerControl = this.FindName("BlackChordColourPicker") as ColourPicker;
            if (this.BlackChordColourPickerControl != null)
            {
                this.BlackChordColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.BlackKeySelectedChordColour;
                this.BlackChordColourPickerControl.PropertyChanged += this.BlackChordColourPickerPropertyChanged;
            }

            this.WhiteChordColourPickerControl = this.FindName("WhiteChordColourPicker") as ColourPicker;
            if (this.WhiteChordColourPickerControl != null)
            {
                this.WhiteChordColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.WhiteKeySelectedChordColour;
                this.WhiteChordColourPickerControl.PropertyChanged += this.WhiteChordColourPickerPropertyChanged;
            }

            this.BlackScaleColourPickerControl = this.FindName("BlackScaleColourPicker") as ColourPicker;
            if (this.BlackScaleColourPickerControl != null)
            {
                this.BlackScaleColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.BlackKeySelectedScaleColour;
                this.BlackScaleColourPickerControl.PropertyChanged += this.BlackScaleColourPickerPropertyChanged;
            }

            this.WhiteScaleColourPickerControl = this.FindName("WhiteScaleColourPicker") as ColourPicker;
            if (this.WhiteScaleColourPickerControl != null)
            {
                this.WhiteScaleColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.WhiteKeySelectedScaleColour;
                this.WhiteScaleColourPickerControl.PropertyChanged += this.WhiteScaleColourPickerPropertyChanged;
            }

            this.BlackFinderColourPickerControl = this.FindName("BlackFinderColourPicker") as ColourPicker;
            if (this.BlackFinderColourPickerControl != null)
            {
                this.BlackFinderColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.BlackKeySelectedFinderColour;
                this.BlackFinderColourPickerControl.PropertyChanged += this.BlackFinderColourPickerPropertyChanged;
            }

            this.WhiteFinderColourPickerControl = this.FindName("WhiteFinderColourPicker") as ColourPicker;
            if (this.WhiteFinderColourPickerControl != null)
            {
                this.WhiteFinderColourPickerControl.Colour = this.CurrentApp.SettingsViewModel.WhiteKeySelectedFinderColour;
                this.WhiteFinderColourPickerControl.PropertyChanged += this.WhiteFinderColourPickerPropertyChanged;
            }
        }

        public ColourPicker WhiteFinderColourPickerControl { get; set; }

        public ColourPicker BlackFinderColourPickerControl { get; set; }

        public ColourPicker WhiteScaleColourPickerControl { get; set; }

        public ColourPicker BlackScaleColourPickerControl { get; set; }

        public ColourPicker WhiteChordColourPickerControl { get; set; }

        public ColourPicker BlackChordColourPickerControl { get; set; }

        private void WhiteFinderColourPickerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CurrentApp.SettingsViewModel.WhiteKeySelectedFinderColour = this.WhiteFinderColourPickerControl.Colour;
        }

        private void BlackFinderColourPickerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CurrentApp.SettingsViewModel.BlackKeySelectedFinderColour = this.BlackFinderColourPickerControl.Colour;
        }

        private void WhiteScaleColourPickerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CurrentApp.SettingsViewModel.WhiteKeySelectedScaleColour = this.WhiteScaleColourPickerControl.Colour;
        }

        private void BlackScaleColourPickerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CurrentApp.SettingsViewModel.BlackKeySelectedScaleColour = this.BlackScaleColourPickerControl.Colour;
        }

        private void WhiteChordColourPickerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CurrentApp.SettingsViewModel.WhiteKeySelectedChordColour = this.WhiteChordColourPickerControl.Colour;
        }

        private void BlackChordColourPickerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.CurrentApp.SettingsViewModel.BlackKeySelectedChordColour = this.BlackChordColourPickerControl.Colour;
        }

        public App CurrentApp => Application.Current as App;

        public async Task SaveSettings()
        {
            var update = true;
            var settingsToSave = await this.CurrentApp.SettingsRepository.GetSettings();
            if (settingsToSave == null)
            {
                settingsToSave = new Settings();
                update = false;
            }

            settingsToSave.ArpeggiateChord = this.CurrentApp.SettingsViewModel.ArpeggiateIsEnabled;
            settingsToSave.PlaySelection = this.CurrentApp.SettingsViewModel.AudioIsEnabled;

            settingsToSave.BlackKeySelectedChordColour = ColourUtilities.Colours.First(c => c.Value == this.CurrentApp.SettingsViewModel.BlackKeySelectedChordColour).Key;
            settingsToSave.WhiteKeySelectedChordColour = ColourUtilities.Colours.First(c => c.Value == this.CurrentApp.SettingsViewModel.WhiteKeySelectedChordColour).Key;
            settingsToSave.BlackKeySelectedScaleColour = ColourUtilities.Colours.First(c => c.Value == this.CurrentApp.SettingsViewModel.BlackKeySelectedScaleColour).Key;
            settingsToSave.WhiteKeySelectedScaleColour = ColourUtilities.Colours.First(c => c.Value == this.CurrentApp.SettingsViewModel.WhiteKeySelectedScaleColour).Key;
            settingsToSave.BlackKeySelectedFinderColour = ColourUtilities.Colours.First(c => c.Value == this.CurrentApp.SettingsViewModel.BlackKeySelectedFinderColour).Key;
            settingsToSave.WhiteKeySelectedFinderColour = ColourUtilities.Colours.First(c => c.Value == this.CurrentApp.SettingsViewModel.WhiteKeySelectedFinderColour).Key;

            if (update)
            {
                await this.CurrentApp.SettingsRepository.UpdateSettings(settingsToSave);
            }
            else
            {
                await this.CurrentApp.SettingsRepository.AddSettings(settingsToSave);
            }
        }
    }
}
