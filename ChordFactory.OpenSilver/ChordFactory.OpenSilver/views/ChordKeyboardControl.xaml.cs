namespace ChordFactory.OpenSilver.views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;

    using models;

    using viewModels;

    public partial class ChordKeyboardControl : UserControl, INotifyPropertyChanged
    {
        //private const string WaveString = @"ChordFactory.OpenSilver;resources/media/PIANO_MED_{0}.mp3";
        private const string WaveString = @"ms-appx:///media/PIANO_MED_{0}.mp3";
        private readonly Color chordBlackKeySelected = Colors.CadetBlue;

        private readonly Color chordKeyBorderSelected = Colors.DarkSlateBlue;

        private readonly List<Border> chordKeys = new List<Border>();
        private readonly Color chordWhiteKeySelected = Colors.SkyBlue;
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
        private ComboBox chordsCombo;

        private ComboBox inversionCombo;
        private Run selectedChordInversionNotesLabel;
        private Run selectedChordLabel;

        public ChordKeyboardControl()
        {
            this.InitializeComponent();
            this.Loaded += this.MainPage_Loaded;
            this.PropertyChanged += this.MainPage_PropertyChanged;

        }

        private void MainPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        public InversionEnum SelectedInversion { get; set; }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ((App)Application.Current)?.ChordKeyboardViewModel;
            this.ChordKeyboardViewModel.PropertyChanged += this.ChordKeyboardViewModelPropertyChanged;
            this.ChordKeyboardViewModel.Settings.PropertyChanged += this.SettingsPropertyChanged;

            this.chordsCombo = this.FindName("ChordsComboBox") as ComboBox;
            this.selectedChordLabel = this.FindName("SelectedChordLabel") as Run;
            this.selectedChordInversionNotesLabel = this.FindName("SelectedChordInversionNotesLabel") as Run;

            this.inversionCombo = this.FindName("InversionCombo") as ComboBox;

            if (this.inversionCombo != null)
            {
                this.inversionCombo.SelectionChanged += this.InversionCombo_SelectionChanged;
            }

            if (this.chordsCombo != null)
            {
                this.chordsCombo.SelectionChanged += this.ChordsCombo_SelectionChanged;
            }

            this.chordKeyboardGrid = (Grid)this.FindName("ChordKeyboardGrid");

            if (this.chordKeyboardGrid != null)
            {
                this.SizeChanged += this.MainPage_SizeChanged;
            }

            this.PopulateKeys();

            if (this.chordsCombo.Items.Count > 0)
            {
                this.chordsCombo.SelectedIndex = 0;
            }

            if (this.inversionCombo.Items.Count > 0)
            {
                this.inversionCombo.SelectedIndex = 0;
            }

            this.SelectedInversion = 0;
            this.AdjustKeyboardAspectRatios();
        }

        private void SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void ChordKeyboardViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ChordRootNote")
            {
                //this.ShowChord();
                // this.PlayChord();
            }
        }

        public ChordKeyboardViewModel ChordKeyboardViewModel => this.DataContext as ChordKeyboardViewModel;
        
        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.AdjustKeyboardAspectRatios();
        }

        private void AdjustKeyboardAspectRatios()
        {
            var newKeyboardHeight = this.chordKeyboardGrid.ActualWidth * 0.45;
            this.chordKeyboardGrid.Height = Math.Min(400, Math.Max(newKeyboardHeight, 300));
        }

        private void InversionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedInversion = (InversionEnum)((ComboBox)sender).SelectedIndex;
            if (this.chordsCombo.SelectedItem != null)
            {
                this.ShowChord(this.chordsCombo.SelectedItem as Chord);
            }
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
            for (var inversionNote = 0;
                inversionNote < Math.Min(this.inversionCombo.SelectedIndex, chord.Notes.Count);
                inversionNote++)
            {
                actualChordNotes[inversionNote] += 12;
            }

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
            this.ShowChord(this.chordsCombo.SelectedItem as Chord);
        }

        private void ChordsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox)?.SelectedItem is Chord chord)
            {
                this.ShowChord(chord);
            }
        }

        private void ShowChord(Chord chord)
        {
            this.ClearKeySelection();

            var adjustedNotes = new int[chord.Notes.Count];
            chord.Notes.CopyTo(adjustedNotes);

            for (var inversionNote = 0;
                inversionNote < Math.Min((int)this.SelectedInversion, chord.Notes.Count);
                inversionNote++)
            {
                adjustedNotes[inversionNote] += 12;
            }

            for (var index = 0; index < adjustedNotes.Length; index++)
            {
                adjustedNotes[index] = adjustedNotes[index] % 24;
            }

            foreach (var note in adjustedNotes)
            {
                var adjustedNoteIndex = (note + this.chordRootNote) % 24;
                var colourTag = (string)this.chordKeys[adjustedNoteIndex].Tag;
                this.chordKeys[adjustedNoteIndex].Background =
                    new SolidColorBrush(colourTag == "Ivory" ? this.chordWhiteKeySelected : this.chordBlackKeySelected);
                this.chordKeys[adjustedNoteIndex].BorderBrush = new SolidColorBrush(this.chordKeyBorderSelected);
            }

            this.selectedChordLabel.Text = $"{this.noteNames[this.chordRootNote]} {chord.Description}";

            var inversionText = string.Empty;
            if (this.SelectedInversion != 0)
            {
                inversionText = $" - {this.SelectedInversion.ToString().ToLower()} inversion";
            }

            this.selectedChordInversionNotesLabel.Text = $"[{this.GetChordNoteNamesText(chord.Notes)}{inversionText}]";

            // this.PlayChord(chord);
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

            for (var inversionNote = 0;
                inversionNote < Math.Min(this.inversionCombo.SelectedIndex, noteSequence.Count);
                inversionNote++)
            {
                adjustedNotes[inversionNote] += 12;
            }

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