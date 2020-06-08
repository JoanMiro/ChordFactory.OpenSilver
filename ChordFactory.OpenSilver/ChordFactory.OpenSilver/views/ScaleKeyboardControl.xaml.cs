namespace ChordFactory.OpenSilver.views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using models;
    using viewModels;

    public partial class ScaleKeyboardControl : UserControl, INotifyPropertyChanged
    {
        private const string WaveString = @"ms-appx:///media/PIANO_MED_{0}.mp3";
        private readonly List<string> noteNames = new List<string> { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };
        private readonly Color scaleBlackKeySelected = Colors.SeaGreen;
        private readonly Color scaleKeyBorderSelected = Colors.DarkSlateGray;
        private readonly List<Border> scaleKeys = new List<Border>();

        private readonly Color scaleWhiteKeySelected = Colors.DarkSeaGreen;
        /* ms-appx:///Audio/ */

        private readonly List<string> wavFiles = new List<string>
        {
            "C3", "DB3", "D3", "EB3", "E3", "F3", "GB3", "G3", "AB3", "A3", "BB3", "B3",
            "C4", "DB4", "D4", "EB4", "E4", "F4", "GB4", "G4", "AB4", "A4", "BB4", "B4",
            "C5", "DB5", "D5", "EB5", "E5", "F5", "GB5", "G5", "AB5", "A5", "BB5", "B5"
        };

        private Grid scaleKeyboardGrid;
        private int scaleRootNote;
        private ComboBox scalesCombo;
        private Run selectedScaleLabel;
        private Run selectedScaleNotesLabel;

        public ScaleKeyboardControl()
        {
            this.InitializeComponent();
            this.Loaded += this.MainPage_Loaded;
            this.PropertyChanged += this.MainPage_PropertyChanged;

        }

        private void MainPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        public MusicData MusicData { get; set; }


        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ((App)Application.Current)?.ScaleKeyboardViewModel;
            this.ScaleKeyboardViewModel.PropertyChanged += this.ScaleKeyboardViewModelPropertyChanged;
            this.ScaleKeyboardViewModel.Settings.PropertyChanged += this.SettingsPropertyChanged;
            
            this.scalesCombo = this.FindName("ScalesComboBox") as ComboBox;
            this.selectedScaleLabel = this.FindName("SelectedScaleLabel") as Run;
            this.selectedScaleNotesLabel = this.FindName("SelectedScaleNotesLabel") as Run;

            if (this.scalesCombo != null)
            {
                this.scalesCombo.SelectionChanged += this.ScalesCombo_SelectionChanged;
            }

            this.scaleKeyboardGrid = (Grid)this.FindName("ScaleKeyboardGrid");

            if (this.scaleKeyboardGrid != null)
            {
                this.SizeChanged += this.MainPage_SizeChanged;
            }

            this.PopulateOctaves();
            this.PopulateKeys();

            this.scalesCombo.SelectedIndex = 0;
            this.AdjustKeyboardAspectRatios();
        }

        private void SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "ChordRootNote")
            //{
            //    //this.ShowChord();
            //    // this.PlayChord();
            //}
        }

        private void ScaleKeyboardViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public ScaleKeyboardViewModel ScaleKeyboardViewModel => this.DataContext as ScaleKeyboardViewModel;

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.AdjustKeyboardAspectRatios();
        }

        private void AdjustKeyboardAspectRatios()
        {
            var newScaleKeyboardHeight = this.scaleKeyboardGrid.ActualWidth * 0.45;
            this.scaleKeyboardGrid.Height = newScaleKeyboardHeight;
        }

        private void PopulateKeys()
        {
            foreach (var octaveGrid in this.scaleKeyboardGrid.Children)
            {
                if (octaveGrid is Grid currentOctave)
                {
                    for (var keyIndex = 0; keyIndex < currentOctave.Children.Count; keyIndex++)
                    {
                        if (currentOctave.Children[keyIndex] is Border key)
                        {
                            key.Tapped += this.ScaleKey_Tapped;
                            this.scaleKeys.Add(key);
                        }
                    }
                }
            }
        }

        private void ScaleKey_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.scaleRootNote = this.scaleKeys.IndexOf(sender as Border) % 12;
            this.ShowScale(this.scalesCombo.SelectedItem as Scale);
        }

        private void PopulateOctaves()
        {
        }

        private void ScalesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox)?.SelectedItem is Scale scale)
            {
                this.ShowScale(scale);
            }
        }

        private void ShowScale(Scale scale)
        {
            this.scaleKeys.ForEach(k => k.Background = new SolidColorBrush((string)k.Tag == "Ivory" ? Colors.Ivory : Colors.Black));

            foreach (var note in scale.Notes)
            {
                var adjustedNoteIndex = (note + this.scaleRootNote) % 24;
                var colourTag = (string)this.scaleKeys[adjustedNoteIndex].Tag;
                this.scaleKeys[adjustedNoteIndex].Background =
                    new SolidColorBrush(colourTag == "Ivory" ? this.scaleWhiteKeySelected : this.scaleBlackKeySelected);
                this.scaleKeys[adjustedNoteIndex].BorderBrush = new SolidColorBrush(this.scaleKeyBorderSelected);
            }

            this.selectedScaleLabel.Text = $"{this.noteNames[this.scaleRootNote]} {(this.scalesCombo.SelectedItem as Scale)?.Description}";
            this.selectedScaleNotesLabel.Text = $"[{this.GetScaleNoteNamesText(scale.Notes)}]";
        }

        private MediaElement GetMediaElementFromResource(string resource)
        {
            try
            {
                var mediaElement = new MediaElement { Source = new Uri(resource, UriKind.RelativeOrAbsolute) };
                return mediaElement;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private string GetScaleNoteNamesText(List<int> noteSequence)
        {
            var adjustedNotes = new string[noteSequence.Count];
            for (var noteIndex = 0; noteIndex < noteSequence.Count; noteIndex++)
            {
                var note = noteSequence[noteIndex];
                adjustedNotes[noteIndex] = this.noteNames[(note + this.scaleRootNote) % 12];
            }

            return string.Join("-", adjustedNotes);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}