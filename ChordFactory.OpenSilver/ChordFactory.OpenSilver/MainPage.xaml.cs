namespace ChordFactory.OpenSilver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using models;

    public partial class MainPage : Page
    {
        private ComboBox chordsCombo;
        private ComboBox scalesCombo;
        private ComboBox inversionCombo;
        private Grid chordKeyboardGrid;
        private Grid scaleKeyboardGrid;
        private Run selectedChordLabel;
        private Run selectedScaleLabel;

        private readonly List<Border> chordKeys = new List<Border>();
        private readonly List<Border> scaleKeys = new List<Border>();
        private readonly Color chordWhiteKeySelected = Colors.SkyBlue;
        private readonly Color chordBlackKeySelected = Colors.CadetBlue;
        private readonly Color scaleWhiteKeySelected = Colors.DarkSeaGreen;
        private readonly Color scaleBlackKeySelected = Colors.SeaGreen;

        private readonly Color chordKeyBorderSelected = Colors.DarkSlateBlue;
        private readonly Color scaleKeyBorderSelected = Colors.DarkSlateGray;

        private int chordRootNote;
        private int scaleRootNote;
        private readonly List<string> noteNames = new List<string> { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };

        private const string WaveString = @"media\PIANO_MED_{0}.wav";

        private readonly List<string> wavFiles = new List<string>
        {
            "C3", "DB3", "D3", "EB3", "E3", "F3", "GB3", "G3", "AB3", "A3", "BB3", "B3",
            "C4", "DB4", "D4", "EB4", "E4", "F4", "GB4", "G4", "AB4", "A4", "BB4", "B4",
            "C5", "DB5", "D5", "EB5", "E5", "F5", "GB5", "G5", "AB5", "A5", "BB5", "B5"
        };

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += this.MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DataContext = new MusicData();
            this.chordsCombo = this.FindName("ChordsComboBox") as ComboBox;
            this.scalesCombo = this.FindName("ScalesComboBox") as ComboBox;
            this.selectedChordLabel = this.FindName("SelectedChordLabel") as Run;
            this.selectedScaleLabel = this.FindName("SelectedScaleLabel") as Run;
            this.inversionCombo = this.FindName("InversionCombo") as ComboBox;

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

            this.chordsCombo.SelectedIndex = 0;
            this.scalesCombo.SelectedIndex = 0;
            this.inversionCombo.SelectedIndex = 0;
            this.AdjustKeyboardAspectRatios();
        }

        private void MainPage_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
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

            var actualChordNotes = chord.Notes.Select(c => (c + (int)this.chordRootNote) % 24).ToList();
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
                mediaElements.ForEach(e =>
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
            this.chordKeys.ForEach(k =>
            {
                k.Background = new SolidColorBrush((string)k.Tag == "Ivory" ? Colors.Ivory : Colors.Black);
                k.BorderBrush = new SolidColorBrush(Colors.Black);
            });

            var adjustedNotes = new int[chord.Notes.Count];
            chord.Notes.CopyTo(adjustedNotes);

            for (var inversionNote = 0;
                 inversionNote < Math.Min(this.inversionCombo.SelectedIndex, chord.Notes.Count);
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

            this.selectedChordLabel.Text = $"{this.noteNames[this.chordRootNote]} {(this.chordsCombo.SelectedItem as Chord).Description} [{this.inversionCombo.SelectedItem}]";

            // this.PlayChord(chord);
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

            this.selectedScaleLabel.Text = $"{this.noteNames[this.scaleRootNote]} {(this.scalesCombo.SelectedItem as Scale).Description}";
        }

        private MediaElement GetMediaElementFromResource(string resource)
        {
            try
            {
                var mediaElement = new MediaElement { Source = new Uri(resource, UriKind.Relative) };
                return mediaElement;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}