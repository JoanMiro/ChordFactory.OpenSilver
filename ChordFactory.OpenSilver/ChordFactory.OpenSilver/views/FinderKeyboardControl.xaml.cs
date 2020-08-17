namespace ChordFactory.OpenSilver.views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using models;
    using viewModels;

    public partial class FinderKeyboardControl : UserControl, INotifyPropertyChanged
    {
        //private const string WaveString = @"ChordFactory.OpenSilver;resources/media/PIANO_MED_{0}.mp3";
        private const string WaveString = @"ms-appx:///media/PIANO_MED_{0}.mp3";
        // private readonly Color chordBlackKeySelected = Colors.CadetBlue;

        private readonly Color chordKeyBorderSelected = Colors.DarkSlateBlue;

        private readonly List<Border> chordKeys = new List<Border>();
        // private readonly Color chordWhiteKeySelected = Colors.SkyBlue;
        private readonly List<string> noteNames = new List<string> { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };
        /* ms-appx:///Audio/ */

        private readonly List<string> wavFiles = new List<string>
        {
            "C3", "DB3", "D3", "EB3", "E3", "F3", "GB3", "G3", "AB3", "A3", "BB3", "B3",
            "C4", "DB4", "D4", "EB4", "E4", "F4", "GB4", "G4", "AB4", "A4", "BB4", "B4",
            "C5", "DB5", "D5", "EB5", "E5", "F5", "GB5", "G5", "AB5", "A5", "BB5", "B5"
        };

        private Grid chordKeyboardGrid;
        private int chordRootNote;

        public FinderKeyboardControl()
        {
            this.InitializeComponent();
            this.Loaded += this.MainPage_Loaded;
        }

        public FinderViewModel FinderViewModel => this.DataContext as FinderViewModel;

        public InversionEnum SelectedInversion { get; set; }

        public Chord IdentifiedChord { get; set; }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ((App)Application.Current)?.FinderViewModel;
            this.FinderViewModel.PropertyChanged += this.FinderViewModelPropertyChanged;
            this.FinderViewModel.Settings.PropertyChanged += this.SettingsPropertyChanged;

            this.chordKeyboardGrid = (Grid)this.FindName("ChordKeyboardGrid");

            if (this.chordKeyboardGrid != null)
            {
                this.SizeChanged += this.MainPage_SizeChanged;
            }

            this.PopulateKeys();

            this.SelectedInversion = 0;
            this.AdjustKeyboardAspectRatios();
        }

        private void SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.ShowChord();
        }

        private void FinderViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FinderRootNoteOffset")
            {
                this.ShowChord();
                // this.PlayChord();
            }
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.AdjustKeyboardAspectRatios();
        }

        private void AdjustKeyboardAspectRatios()
        {
            var newKeyboardHeight = this.chordKeyboardGrid.ActualWidth * 0.45;
            this.chordKeyboardGrid.Height = newKeyboardHeight;
        }

        private void PopulateKeys()
        {
            foreach (var octaveGrid in this.chordKeyboardGrid.Children)
            {
                if (octaveGrid is Grid currentOctave)
                {
                    for (var keyIndex = 0; keyIndex < currentOctave.Children.Count; keyIndex++)
                    {
                        if (currentOctave.Children[keyIndex] is Border key)
                        {
                            key.Tapped += this.ChordKey_Tapped;
                            this.chordKeys.Add(key);
                        }
                    }
                }
            }
        }

        public void PlayChord(Chord chord)
        {
            var mediaElements = new List<MediaElement>();
            var noteLength = new TimeSpan(0, 0, 0, 0, 400);
            var pauseLength = new TimeSpan(0, 0, 0, 1, 500);

            var actualChordNotes = chord.Notes.Select(c => (c + this.chordRootNote) % 24).ToList();

            actualChordNotes.Sort();

            actualChordNotes.ForEach(
                n => mediaElements.Add(this.GetMediaElementFromResource(string.Format(WaveString, this.wavFiles[n % this.wavFiles.Count]))));

            // Play chord
            mediaElements.ForEach(e => e.Play());

            var pauseEndTime = DateTime.Now + pauseLength;

            {
                // Wait
                while (DateTime.Now < pauseEndTime)
                {
                }

                // Play arpeggio
                mediaElements.ForEach(
                    e =>
                    {
                        e.Play();
                        var endTime = DateTime.Now + noteLength;
                        while (DateTime.Now < endTime)
                        {
                        }
                    });
            }
        }

        private void ChordKey_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.chordRootNote = this.chordKeys.IndexOf(sender as Border) % 12;
            this.FinderViewModel.FinderKeyboardTappedCommand.Execute($"Key{this.chordKeys.IndexOf(sender as Border)}");
            this.ShowChord();
        }

        private void ShowChord()
        {
            this.ClearKeySelection();

            if (this.FinderViewModel.FinderChord.Notes.Count > 0)
            {
                var actualChordNotes = this.FinderViewModel.FinderChord.Notes.ToArray();

                // Cope with overflow
                for (var note = 0; note < actualChordNotes.Length; note++)
                {
                    actualChordNotes[note] = actualChordNotes[note] % this.chordKeys.Count;
                }

                foreach (var note in actualChordNotes)
                {
                    var colourTag = (string)this.chordKeys[note].Tag;
                    this.chordKeys[note].Background =
                        new SolidColorBrush(
                            colourTag == "Ivory"
                                ? this.FinderViewModel.Settings.WhiteKeySelectedFinderColour
                                : this.FinderViewModel.Settings.BlackKeySelectedFinderColour);

                    this.chordKeys[note].BorderBrush = new SolidColorBrush(this.chordKeyBorderSelected);
                }
            }
        }

        private void ClearKeySelection()
        {
            this.chordKeys.ForEach(
                k =>
                {
                    k.Background = new SolidColorBrush((string)k.Tag == "Ivory" ? Colors.Ivory : Colors.Black);
                    k.BorderBrush = new SolidColorBrush(Colors.Black);
                });
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

        private string GetChordNoteNamesText(List<int> noteSequence)
        {
            var adjustedNotes = new List<int>(noteSequence);
            var adjustedNoteNames = new string[noteSequence.Count];

            adjustedNotes.Sort();

            for (var noteIndex = 0; noteIndex < adjustedNotes.Count; noteIndex++)
            {
                adjustedNoteNames[noteIndex] = this.noteNames[(adjustedNotes[noteIndex] + this.chordRootNote) % 12];
            }

            return string.Join("-", adjustedNoteNames);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}