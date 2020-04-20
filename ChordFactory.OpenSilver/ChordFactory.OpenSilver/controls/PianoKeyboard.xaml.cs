namespace ChordFactory.OpenSilver.controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using models;

    public partial class PianoKeyboard : UserControl
    {
        /*
        public static readonly DependencyProperty OctavesProperty = DependencyProperty.Register(
            "Octaves", typeof(int), typeof(PianoKeyboard), new PropertyMetadata(default(int)));

        private StackPanel keyboardStackPanel;
        private readonly List<PianoKey> keys;

        public PianoKeyboard()
        {
            this.InitializeComponent();
            this.keys = new List<PianoKey>();
        }

        public int Octaves
        {
            get => (int)this.GetValue(OctavesProperty);
            set => this.SetValue(OctavesProperty, value);
        }

        public List<PianoKey> Keys
        {
            get
            {
                if (this.keys.Count == 0)
                {
                    this.PopulateKeys();
                }

                return this.keys;
            }
        }

        // public MusicData MusicData { get; } = new MusicData();
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.keyboardStackPanel = (StackPanel)this.GetTemplateChild("KeyboardStackPanel");
            this.PopulateOctaves();
            this.PopulateKeys();
        }

        private void PopulateKeys()
        {
            for (var octaveIndex = 0; octaveIndex < this.keyboardStackPanel.Children.Count; octaveIndex++)
            {
                var currentOctave = this.keyboardStackPanel.Children[octaveIndex] as Octave;
                for (var keyIndex = 0; keyIndex < currentOctave.Keys.Count; keyIndex++)
                {
                    this.keys.Add(currentOctave.Keys[keyIndex]);
                }
            }
        }

        private void PopulateOctaves()
        {
            for (var octaveCount = 0; octaveCount < this.Octaves; octaveCount++)
            {
                var newOctave = new Octave { Width = this.Width / this.Octaves };
                this.keyboardStackPanel.Children.Add(newOctave);
            }
        }
        */
        /// <summary>
        /// The inversion property.
        /// </summary>
        public static readonly DependencyProperty InversionProperty =
            DependencyProperty.Register("Inversion", typeof(Inversion), typeof(PianoKeyboard),
                                        new PropertyMetadata(Inversion.First, OnInversionChanged));

        /// <summary>
        /// The root note property.
        /// </summary>
        public static readonly DependencyProperty RootNoteProperty =
            DependencyProperty.Register("RootNote", typeof(RootNotes), typeof(PianoKeyboard),
                                        new PropertyMetadata(RootNotes.C, OnRootNotePropertyChanged));

        /// <summary>
        /// The selected chord property.
        /// </summary>
        public static readonly DependencyProperty SelectedChordProperty =
            DependencyProperty.Register("SelectedChord", typeof(List<int>), typeof(PianoKeyboard),
                                        new PropertyMetadata(new List<int>(), OnSelectedChordChanged));

        /// <summary>
        /// The selected scale property.
        /// </summary>
        public static readonly DependencyProperty SelectedScaleProperty =
            DependencyProperty.Register("SelectedScale", typeof(List<int>), typeof(PianoKeyboard),
                                        new PropertyMetadata(new List<int>(), OnSelectedScaleChanged));


        /// <summary>
        /// The note buttons.
        /// </summary>
        private readonly List<PianoKey> noteButtons = new List<PianoKey>();

        /// <summary>
        /// The note names.
        /// </summary>
        private readonly List<string> noteNames = new List<string> { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B" };

        /// <summary>
        /// The intervals
        /// </summary>
        private readonly List<string> intervals = new List<string> 
        {
            "R", "2b", "2", "3b", "3", "4", "5b", "5", "5+", "6", "7", "M7", 
            "R", "9b", "9", "9#", "10", "11", "11#", "12", "13b", "13", "13#", "14", 
            "R", "2b", "2", "3b", "3", "4", "5b", "5", "5+", "6", "7", "M7", 
            "R", "9b", "9", "9#", "10", "11", "11#", "12", "13b", "13", "13#", "14" 
        };

        /// <summary>
        /// List of NoteKeyInfo for the chord
        /// </summary>
        private readonly List<NoteKeyInfo> chordNoteKeyInfo = new List<NoteKeyInfo>();

        /// <summary>
        /// Gets or sets Inversion.
        /// </summary>
        public Inversion Inversion
        {
            get { return (Inversion)this.GetValue(InversionProperty); }
            set { this.SetValue(InversionProperty, value); }
        }

        /// <summary>
        /// Gets NoteButtons.
        /// </summary>
        public List<PianoKey> NoteButtons
        {
            get
            {
                if (this.noteButtons.Count == 0)
                {
                    this.PopulateNotes();
                }

                return this.noteButtons;
            }
        }

        /// <summary>
        /// Gets or sets RootNote.
        /// </summary>
        public RootNotes RootNote
        {
            get { return (RootNotes)this.GetValue(RootNoteProperty); }
            set { this.SetValue(RootNoteProperty, value); }
        }

        /// <summary>
        /// Gets or sets SelectedChord.
        /// </summary>
        public List<int> SelectedChord
        {
            get { return (List<int>)this.GetValue(SelectedChordProperty); }
            set { this.SetValue(SelectedChordProperty, value); }
        }

        /// <summary>
        /// Gets or sets SelectedScale.
        /// </summary>
        public List<int> SelectedScale
        {
            get { return (List<int>)this.GetValue(SelectedScaleProperty); }
            set { this.SetValue(SelectedScaleProperty, value); }
        }

        /// <summary>
        /// Called when [inversion changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnInversionChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var pianoKeyboard = (PianoKeyboard)sender;
            pianoKeyboard.DisplayNotes();
        }

        /// <summary>
        /// Called when [selected chord changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSelectedChordChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var pianoKeyboard = (PianoKeyboard)sender;
            pianoKeyboard.DisplayNotes();
        }

        /// <summary>
        /// Called when [selected scale changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSelectedScaleChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var pianoKeyboard = (PianoKeyboard)sender;
            pianoKeyboard.DisplayNotes();
        }

        /// <summary>
        /// Called when [root note property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnRootNotePropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var pianoKeyboard = (PianoKeyboard)sender;
            pianoKeyboard.DisplayNotes();
        }

        /// <summary>
        /// The display notes.
        /// </summary>
        private void DisplayNotes()
        {
            if (this.SelectedChord != null && this.NoteButtons != null && this.NoteButtons.Count > 0)
            {
                this.chordNoteKeyInfo.Clear();

                // Adjust from root note
                var actualChordNotes = this.SelectedChord.Select(c => (int)this.RootNote + c).ToList();

                actualChordNotes.ForEach(n => this.chordNoteKeyInfo.Add(new NoteKeyInfo
                    {
                        Index = this.chordNoteKeyInfo.Count,
                        NoteNumber = n,
                        AdjustedNoteNumber = n,
                        IntervalIndex = n - (int)this.RootNote
                    }));

                // Adjust for inversions
                for (var inversionNote = 0;
                     inversionNote < Math.Min((int)this.Inversion, this.SelectedChord.Count);
                     inversionNote++)
                {
                    this.chordNoteKeyInfo[inversionNote].AdjustedNoteNumber += 12;
                }

                // Cope with overflow
                this.chordNoteKeyInfo.ForEach(n => n.AdjustedNoteNumber = n.AdjustedNoteNumber % this.NoteButtons.Count);

                var chordButtons = new List<PianoKey>();

                this.chordNoteKeyInfo.ForEach(n => chordButtons.Add(this.NoteButtons[n.AdjustedNoteNumber]));

                var nonChordButtons = this.NoteButtons.Where(n => chordButtons.Contains(n) == false).ToList();
                chordButtons.ForEach(n => n.IsChordKey = true);
                nonChordButtons.ForEach(n => n.IsChordKey = false);

                for (var noteIndex = 0; noteIndex < chordButtons.Count; noteIndex++)
                {
                    chordButtons[this.chordNoteKeyInfo[noteIndex].Index].ChordSymbol = this.intervals[this.chordNoteKeyInfo[noteIndex].IntervalIndex];
                }

                this.NoteButtons.ForEach(n => n.IsRootKey = (int)this.RootNote == this.NoteButtons.IndexOf(n) ? true : false);
            }

            if (this.SelectedScale != null && this.NoteButtons != null && this.NoteButtons.Count > 0)
            {
                var actualScaleNotes = this.SelectedScale.Select(s => (s + (int)this.RootNote) % this.NoteButtons.Count).ToList();
                var scaleButtons =
                    this.NoteButtons.Where(n => actualScaleNotes.Contains(this.NoteButtons.IndexOf(n))).ToList();
                var nonScaleButtons = this.NoteButtons.Where(n => scaleButtons.Contains(n) == false).ToList();
                scaleButtons.ForEach(n => n.IsScaleKey = true);
                nonScaleButtons.ForEach(n => n.IsScaleKey = false);

                scaleButtons.ForEach(n => n.Content = this.noteNames[this.NoteButtons.IndexOf(n) % 12]);
            }

            //this.UpdateLayout();
        }

        /// <summary>
        /// The populate notes.
        /// </summary>
        private void PopulateNotes()
        {
            var octavesStackPanel = (StackPanel)this.GetTemplateChild("KeyboardStackPanel");
            if (octavesStackPanel == null)
            {
                return;
            }

            foreach (var octave in octavesStackPanel.Children.Cast<Octave>())
            {
                this.noteButtons.AddRange(octave.Keys);
            }

            this.noteButtons.ForEach(b => b.Click += (s, e) => { this.RootNote = (RootNotes)(this.noteButtons.IndexOf(s as PianoKey) % 12); });

            this.noteButtons.ForEach(n => n.Tag = this.noteButtons.IndexOf(n));
        }
    }
}