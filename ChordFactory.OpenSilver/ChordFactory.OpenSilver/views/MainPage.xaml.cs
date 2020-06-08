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

    public partial class MainPage : Page, INotifyPropertyChanged
    {
        //private const string WaveString = @"ChordFactory.OpenSilver;resources/media/PIANO_MED_{0}.mp3";
        private const string WaveString = @"ms-appx:///media/PIANO_MED_{0}.mp3";
        private readonly Color chordBlackKeySelected = Colors.CadetBlue;

        private readonly Color chordKeyBorderSelected = Colors.DarkSlateBlue;

        private readonly List<Border> chordKeys = new List<Border>();
        private readonly Color chordWhiteKeySelected = Colors.SkyBlue;
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

        private Grid chordKeyboardGrid;

        private int chordRootNote;
        private ComboBox chordsCombo;
        private CheckBox finderModeCheckBox;

        private int finderRootNoteOffset;
        private ComboBox inversionCombo;
        private Grid scaleKeyboardGrid;
        private int scaleRootNote;
        private ComboBox scalesCombo;
        private Run selectedChordInversionNotesLabel;
        private Run selectedChordLabel;
        private Run selectedScaleLabel;
        private Run selectedScaleNotesLabel;

        public MainPage()
        {
            this.InitializeComponent();
            //this.Loaded += this.MainPage_Loaded;
            //this.PropertyChanged += this.MainPage_PropertyChanged;
            //this.FinderViewModel = new FinderViewModel();

        }

        private void MainPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FinderRootNoteOffset")
            {
                this.ShowIdentifiedChord();
            }
        }
        
        public FinderViewModel FinderViewModel
        {
            get => this.DataContext as FinderViewModel;
            set => this.DataContext = value;
        }

        public Chord FinderChord { get; set; }

        public MusicData MusicData { get; set; }

        public int FinderRootNoteOffset
        {
            get => this.finderRootNoteOffset;
            set => this.finderRootNoteOffset = value % 12;
        }

        public InversionEnum SelectedInversion { get; set; }

        public Chord IdentifiedChord { get; set; }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.MusicData = new MusicData();
            this.DataContext = this.MusicData;
            this.chordsCombo = this.FindName("ChordsComboBox") as ComboBox;
            this.scalesCombo = this.FindName("ScalesComboBox") as ComboBox;
            this.selectedChordLabel = this.FindName("SelectedChordLabel") as Run;
            this.selectedChordInversionNotesLabel = this.FindName("SelectedChordInversionNotesLabel") as Run;

            this.selectedScaleLabel = this.FindName("SelectedScaleLabel") as Run;
            this.selectedScaleNotesLabel = this.FindName("SelectedScaleNotesLabel") as Run;

            this.inversionCombo = this.FindName("InversionCombo") as ComboBox;
            this.finderModeCheckBox = this.FindName("FinderModeCheckBox") as CheckBox;

            if (this.inversionCombo != null)
            {
                this.inversionCombo.SelectionChanged += this.InversionCombo_SelectionChanged;
            }

            if (this.chordsCombo != null)
            {
                this.chordsCombo.SelectionChanged += this.ChordsCombo_SelectionChanged;
            }

            if (this.scalesCombo != null)
            {
                this.scalesCombo.SelectionChanged += this.ScalesCombo_SelectionChanged;
            }

            this.chordKeyboardGrid = (Grid)this.FindName("ChordKeyboardGrid");
            this.scaleKeyboardGrid = (Grid)this.FindName("ScaleKeyboardGrid");

            if (this.chordKeyboardGrid != null)
            {
                this.SizeChanged += this.MainPage_SizeChanged;
            }

            this.PopulateOctaves();
            this.PopulateKeys();

            this.FinderChord = new Chord { Description = "Mystery Chord", Notes = new List<int>() };

            this.chordsCombo.SelectedIndex = 0;
            this.scalesCombo.SelectedIndex = 0;
            this.inversionCombo.SelectedIndex = 0;
            this.SelectedInversion = 0;
            this.AdjustKeyboardAspectRatios();
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.AdjustKeyboardAspectRatios();
        }

        private void AdjustKeyboardAspectRatios()
        {
            var newScaleKeyboardHeight = this.scaleKeyboardGrid.ActualWidth * 0.45;
            this.chordKeyboardGrid.Height = this.scaleKeyboardGrid.Height = newScaleKeyboardHeight;
        }

        private void InversionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedInversion = (InversionEnum)((ComboBox)sender).SelectedIndex;
            this.ShowChord(this.chordsCombo.SelectedItem as Chord);
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
            if (this.finderModeCheckBox.IsChecked.HasValue && this.finderModeCheckBox.IsChecked == true)
            {
                this.OnFinderKeyboardTapped(this.chordKeys.IndexOf(sender as Border) % 12);
                this.ShowIdentifiedChord();
            }
            else
            {
                this.chordRootNote = this.chordKeys.IndexOf(sender as Border) % 12;
                this.ShowChord(this.chordsCombo.SelectedItem as Chord);
            }
        }

        private void FinderCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (this.finderModeCheckBox.IsChecked.HasValue && this.finderModeCheckBox.IsChecked.Value)
            {
                this.ClearKeySelection();
            }
            else
            {
                this.ShowChord(this.chordsCombo.SelectedItem as Chord);
            }
        }

        private void ScaleKey_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.scaleRootNote = this.scaleKeys.IndexOf(sender as Border) % 12;
            this.ShowScale(this.scalesCombo.SelectedItem as Scale);
        }

        private void PopulateOctaves()
        {
            //for (var octaveCount = 0; octaveCount < this.Octaves; octaveCount++)
            //{
            //    var newOctave = new Octave { Width = this.Width / this.Octaves };
            //    this.chordKeyboardGrid.Children.Add(newOctave);
            //}
        }

        private void ChordsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox)?.SelectedItem is Chord chord)
            {
                this.ShowChord(chord);
            }
        }

        private void ScalesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox)?.SelectedItem is Scale scale)
            {
                this.ShowScale(scale);
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

        private void ShowIdentifiedChord()
        {
            this.ClearKeySelection();

            //var chord = this.IdentifiedChord ?? this.FinderChord;
            //var chord = this.FinderChord;

            //if (chord.Notes.Count > 0)
            if (this.FinderViewModel.FinderChord.Notes.Count > 0)
            {
                //var actualChordNotes = chord.Notes.ToArray();
                var actualChordNotes = this.FinderViewModel.FinderChord.Notes.ToArray();

                // Cope with overflow
                for (var note = 0; note < actualChordNotes.Length; note++)
                {
                    actualChordNotes[note] = actualChordNotes[note] % this.chordKeys.Count;
                }

                foreach (var note in actualChordNotes)
                {
                    //var adjustedNoteIndex = (note + this.FinderRootNoteOffset) % 24;
                    var colourTag = (string)this.chordKeys[note].Tag;
                    this.chordKeys[note].Background =
                        new SolidColorBrush(colourTag == "Ivory" ? this.chordWhiteKeySelected : this.chordBlackKeySelected);
                    this.chordKeys[note].BorderBrush = new SolidColorBrush(this.chordKeyBorderSelected);
                }

                /*
                this.selectedChordLabel.Text = $"{this.noteNames[this.finderRootNoteOffset]} {chord.Description}";

                var inversionText = string.Empty;
                if (this.SelectedInversion != 0)
                {
                    inversionText = $" - {this.SelectedInversion.ToString().ToLower()} inversion";
                }

                this.selectedChordInversionNotesLabel.Text = $"[{this.GetChordNoteNamesText(actualChordNotes.ToList())}{inversionText}]";
                */
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

        private void ShowScale(Scale scale)
        {
            this.scaleKeys.ForEach(k => k.Background = new SolidColorBrush((string)k.Tag == "Ivory" ? Colors.Ivory : Colors.Black));

            foreach (var note in scale.Notes)
            {
                var adjustedNoteIndex = (note + this.scaleRootNote) % 24;
                var colourTag = (string)this.chordKeys[adjustedNoteIndex].Tag;
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

        private void OnFinderKeyboardTapped(int notePicked)
        {
            if (this.FinderChord.Notes.Contains(notePicked))
            {
                this.FinderChord.Notes.Remove(notePicked);
            }
            else
            {
                this.FinderChord.Notes.Add(notePicked);
            }

            this.FinderChord.Notes.Sort();
            this.FindChord();
        }

        private void FindChord(int inversion = 0)
        {
            var chordFound = false;
            Chord foundChord = null;
            this.IdentifiedChord = null;

            var inversionNotes = this.FinderChord.Notes.ToArray();
            this.SelectedInversion = 0;

            if (this.FinderChord.Notes.Count >= 3)
            {
                for (var inversionNote = 0; inversionNote < inversion; inversionNote++)
                {
                    if (inversionNotes.Length > inversionNote && inversionNotes[inversionNotes.Length - 1 - inversionNote] >= 12)
                    {
                        inversionNotes[inversionNotes.Length - 1 - inversionNote] -= 12;
                        this.SelectedInversion = (InversionEnum)inversion;
                    }
                }

                Array.Sort(inversionNotes);
            }

            var finderRootOffset = inversionNotes.Length > 0 ? inversionNotes[0] : 0;

            foreach (var chord in this.MusicData.Chords.Where(c => c.Notes.Count == inversionNotes.Length))
            {
                foreach (var chordNote in inversionNotes)
                {
                    if (!chord.Notes.Contains((chordNote - finderRootOffset) % 12))
                    {
                        chordFound = false;
                        break;
                    }

                    chordFound = true;
                    foundChord = chord;
                }

                if (chordFound)
                {
                    this.FinderRootNoteOffset = finderRootOffset;
                    this.IdentifiedChord = foundChord;
                    //this.ShowIdentifiedChord();
                    break;
                }

                this.FinderRootNoteOffset = finderRootOffset;
            }

            // Not found - look for inversions...
            if (chordFound == false && inversion < 3)
            {
                this.FindChord(++inversion);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}