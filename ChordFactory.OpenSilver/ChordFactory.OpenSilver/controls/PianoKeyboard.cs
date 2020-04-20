// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PianoKeyboard.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   PianoKeyboard control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChordFactory.OpenSilver.controls
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using models;

    /// <summary>
    /// PianoKeyboard control.
    /// </summary>
    public partial class PianoKeyboard : UserControl
    {
        /// <summary>
        /// Octaves DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty OctavesProperty
            = DependencyProperty.Register("Octaves", typeof(int), typeof(PianoKeyboard),
                                          new PropertyMetadata(new PropertyChangedCallback(OctavesChangedCallback)));

        /// <summary>
        /// PianoKeys List.
        /// </summary>
        private readonly List<PianoKey> keys;

        /// <summary>
        /// The container for the octaves.
        /// </summary>
        private StackPanel keyboardStackPanel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChordFactory.OpenSilver.controls.PianoKeyboard"/> class.
        /// </summary>
        public PianoKeyboard()
        {
            this.DefaultStyleKey = typeof(PianoKeyboard);
            this.keys = new List<PianoKey>();
        }

        /// <summary>
        /// Gets the music data.
        /// </summary>
        /// <value>The music data.</value>
        public MusicData MusicData { get; } = new MusicData();

        /// <summary>
        /// Gets the keys list.
        /// </summary>
        /// <value>The keys list.</value>
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

        /// <summary>
        /// Gets or sets the octaves.
        /// </summary>
        /// <value>The octaves.</value>
        public int Octaves
        {
            get { return (int)this.GetValue(OctavesProperty); }

            set { this.SetValue(OctavesProperty, value); }
        }

        /// <summary>
        /// Octaveses the changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void OctavesChangedCallback(DependencyObject dependencyObject,
                                                  DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.keyboardStackPanel = (StackPanel)this.GetTemplateChild("KeyboardStackPanel");

            this.PopulateOctaves();
            this.PopulateKeys();
        }

        /// <summary>
        /// Shows the chord.
        /// </summary>
        /// <param name="chord">The chord.</param>
        /// <param name="rootNote">The rootnote.</param>
        public void ShowChord(Chord chord, int rootNote)
        {
            foreach (var pianoKey in this.Keys)
            {
                pianoKey.Content = null;
                pianoKey.ChordNote = false;
                pianoKey.RootNote = false;
            }

            foreach (int noteIndex in chord.Notes)
            {
                int noteValue = (rootNote + noteIndex) % this.Keys.Count;
                this.Keys[noteValue].ChordSymbol = this.MusicData.Intervals[noteIndex].Abbreviation;
                this.Keys[noteValue].ChordNote = true;
                this.Keys[rootNote].RootNote = true;
            }
        }

        /// <summary>
        /// Shows the scale.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="rootNote">The rootnote.</param>
        public void ShowScale(Scale scale, int rootNote)
        {
            for (int keyIndex = 0; keyIndex < this.Keys.Count; keyIndex++)
            {
                this.Keys[keyIndex].RootNote = false;

                int adjustedNote = (keyIndex - rootNote) % 12;
                adjustedNote = adjustedNote >= 0 ? adjustedNote : adjustedNote + 12;

                if (scale.Notes.Contains(adjustedNote))
                {
                    this.Keys[keyIndex].ScaleNote = true;
                    this.Keys[keyIndex].Content = this.MusicData.Notes[keyIndex % 12].FullName;
                }
                else
                {
                    this.Keys[keyIndex].ScaleNote = false;
                    this.Keys[keyIndex].Content = string.Empty;
                }

                this.Keys[rootNote].RootNote = true;
            }
        }

        /// <summary>
        /// Populates the keys.
        /// </summary>
        private void PopulateKeys()
        {
            foreach (var uiElement in this.keyboardStackPanel.Children)
            {
                var currentOctave = uiElement as OpenSilver.Octave;
                if (currentOctave != null)
                {
                    foreach (var pianoKey in currentOctave.Keys)
                    {
                        this.keys.Add(pianoKey);
                    }
                }
            }
        }

        /// <summary>
        /// Populates the octaves.
        /// </summary>
        private void PopulateOctaves()
        {
            for (var octaveCount = 0; octaveCount < this.Octaves; octaveCount++)
            {
                var newOctave = new OpenSilver.Octave { Width = this.Width / this.Octaves };
                this.keyboardStackPanel.Children.Add(newOctave);
            }
        }
    }
}