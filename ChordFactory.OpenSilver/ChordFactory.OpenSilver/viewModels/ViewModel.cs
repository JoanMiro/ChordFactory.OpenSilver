namespace ChordFactory.OpenSilver.viewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows.Input;
    using models;

    public class ChordDataViewModel:BaseViewModel
    {
        /// <summary>
        /// The note names.
        /// </summary>
        private readonly string[] noteNames =
        {
            "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "Bb", "B"
        };

        /// <summary>
        /// The chord root note offset
        /// </summary>
        private int chordRootNoteOffset;

        /// <summary>
        /// The finder root note offset
        /// </summary>
        private int finderRootNoteOffset;

        /// <summary>
        /// The finder chord
        /// </summary>
        private Chord finderChord;

        /// <summary>
        /// The found chord
        /// </summary>
        private Chord identifiedChord;

        /// <summary>
        /// The scale root note offset
        /// </summary>
        private int scaleRootNoteOffset;

        /// <summary>
        /// The selected chord
        /// </summary>
        private Chord selectedChord;

        /// <summary>
        /// The selected inversion
        /// </summary>
        private Inversion selectedInversion;

        /// <summary>
        /// The selected scale
        /// </summary>
        private Scale selectedScale;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChordDataViewModel" /> class.
        /// </summary>
        public ChordDataViewModel()
        {
            this.ChordKeyboardTappedCommand = new DelegateCommand(this.OnChordKeyboardTapped, this.ChordKeyboardTappedCanExecute);
            this.FinderKeyboardTappedCommand = new DelegateCommand(this.OnFinderKeyboardTapped, this.FinderKeyboardTappedCanExecute);
            this.ScaleKeyboardTappedCommand = new DelegateCommand(this.OnScaleKeyboardTapped, this.ScaleKeyboardTappedCanExecute);
            this.selectedInversion = this.Inversions[0];
            this.FinderChord = Chord.Create("Mystery Chord", new List<int>());
        }

        public MusicData MusicData { get; set; }

        private bool ChordKeyboardTappedCanExecute(object paramList)
        {
            return true;
        }
        private bool FinderKeyboardTappedCanExecute(object paramList)
        {
            return true;
        }
        private bool ScaleKeyboardTappedCanExecute(object paramList)
        {
            return true;
        }

        /// <summary>
        /// Gets or sets the selected inversion.
        /// </summary>
        /// <value>The selected inversion.</value>
        public Inversion SelectedInversion
        {
            get => this.selectedInversion;
            set
            {
                this.selectedInversion = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the name of the chord root note.
        /// </summary>
        /// <value>The name of the chord root note.</value>
        public string ChordRootNoteName => this.noteNames[this.ChordRootNoteOffset % 12];

        public string FinderRootNoteName => this.noteNames[this.FinderRootNoteOffset % 12];

        /// <summary>
        /// Gets the name of the scale root note.
        /// </summary>
        /// <value>The name of the scale root note.</value>
        public string ScaleRootNoteName => this.noteNames[this.ScaleRootNoteOffset % 12];

        /// <summary>
        /// The chords instance.
        /// </summary>
        /// <value>The chords.</value>
        public List<Chord> Chords => this.MusicData.Chords;

        /// <summary>
        /// The scales instance.
        /// </summary>
        /// <value>The scales.</value>
        public List<Scale> Scales => this.MusicData.Scales;

        /// <summary>
        /// Gets the inversions.
        /// </summary>
        /// <value>The inversions.</value>
        public List<Inversion> Inversions { get; } = new List<Inversion>
        {
            new Inversion { Value = 0, Description = "Root" },
            new Inversion { Value = 1, Description = "First inversion" },
            new Inversion { Value = 2, Description = "Second inversion" },
            new Inversion { Value = 3, Description = "Third inversion" }
        };

        /// <summary>
        /// The selected chord.
        /// </summary>
        /// <value>The selected chord.</value>
        public Chord SelectedChord
        {
            get => this.selectedChord;
            set
            {
                this.selectedChord = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.SelectedChordFullName));
            }
        }

        /// <summary>
        /// Gets or sets the found chord.
        /// </summary>
        /// <value>The found chord.</value>
        public Chord IdentifiedChord
        {
            get => this.identifiedChord;
            set
            {
                this.identifiedChord = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.FoundChordFullName));
            }
        }

        /// <summary>
        /// Gets or sets the finder chord.
        /// </summary>
        /// <value>The finder chord.</value>
        public Chord FinderChord
        {
            get => this.finderChord;
            set
            {
                this.finderChord = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.FinderChordFullName));
            }
        }

        /// <summary>
        /// Gets the full name of the selected chord.
        /// </summary>
        /// <value>The full name of the selected chord.</value>
        public string SelectedChordNoteNames
        {
            get
            {
                if (this.SelectedChord != null)
                {
                    var chordNoteNames = new StringBuilder();
                    foreach (var note in this.SelectedChord.Notes)
                    {
                        chordNoteNames.Append(this.noteNames[(note + this.ChordRootNoteOffset) % 12] + " ");
                    }

                    return $"[{chordNoteNames.ToString().Trim()}]";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the full name of the selected chord.
        /// </summary>
        /// <value>The full name of the selected chord.</value>
        public string SelectedChordFullName
        {
            get
            {
                if (this.SelectedChord != null)
                {
                    var chordNoteNames = new StringBuilder();
                    foreach (var note in this.SelectedChord.Notes)
                    {
                        chordNoteNames.Append(this.noteNames[(note + this.ChordRootNoteOffset) % 12] + " ");
                    }

                    return $"{this.noteNames[this.ChordRootNoteOffset]} {this.SelectedChord.Description} [{chordNoteNames.ToString().TrimEnd('+')}]";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the full name of the found chord.
        /// </summary>
        /// <value>The full name of the found chord.</value>
        public string FoundChordFullName
        {
            get
            {
                if (this.IdentifiedChord != null)
                {
                    var chordNoteNames = new StringBuilder();
                    foreach (var note in this.IdentifiedChord.Notes)
                    {
                        chordNoteNames.Append(this.noteNames[(note + this.FinderRootNoteOffset) % 12] + " ");
                    }

                    return $"{this.noteNames[this.FinderRootNoteOffset]} {this.IdentifiedChord.Description} [{chordNoteNames.ToString().TrimEnd('+')}]";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the full name of the finder chord.
        /// </summary>
        /// <value>The full name of the finder chord.</value>
        public string FinderChordFullName
        {
            get
            {
                if (this.FinderChord != null)
                {
                    var chordNoteNames = new StringBuilder();
                    foreach (var note in this.FinderChord.Notes)
                    {
                        chordNoteNames.Append(this.noteNames[(note + this.FinderRootNoteOffset) % 12] + " ");
                    }

                    ////return $"{this.noteNames[this.ChordRootNoteOffset]} {this.FinderChord.Description} [{chordNoteNames.ToString().TrimEnd('+')}]";
                    return $"[{chordNoteNames.ToString().Trim()}]";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the full name of the selected scale.
        /// </summary>
        /// <value>The full name of the selected scale.</value>
        public string SelectedScaleFullName
        {
            get
            {
                if (this.SelectedScale != null)
                {
                    var scaleNoteNames = new StringBuilder();
                    foreach (var note in this.SelectedScale.Notes)
                    {
                        scaleNoteNames.Append(this.noteNames[(note + this.ScaleRootNoteOffset) % 12] + " ");
                    }

                    ////return $"{this.noteNames[this.ScaleRootNoteOffset]} {this.SelectedScale.Description} [{scaleNoteNames.ToString().TrimEnd('+')}]";
                    return $"[{scaleNoteNames.ToString().Trim()}]";
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// The selected scale.
        /// </summary>
        /// <value>The selected scale.</value>
        public Scale SelectedScale
        {
            get => this.selectedScale;
            set
            {
                this.selectedScale = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.SelectedScaleFullName));
            }
        }

        /// <summary>
        /// Gets the chord keyboard tapped command.
        /// </summary>
        /// <value>The chord keyboard tapped command.</value>
        public ICommand ChordKeyboardTappedCommand { get; }

        /// <summary>
        /// Gets the finder keyboard tapped command.
        /// </summary>
        /// <value>The finder keyboard tapped command.</value>
        public ICommand FinderKeyboardTappedCommand { get; }

        /// <summary>
        /// Gets the scale keyboard tapped command.
        /// </summary>
        /// <value>The scale keyboard tapped command.</value>
        public ICommand ScaleKeyboardTappedCommand { get; }

        /// <summary>
        /// Gets or sets the chord root note offset.
        /// </summary>
        /// <value>The chord root note offset.</value>
        public int ChordRootNoteOffset
        {
            get => this.chordRootNoteOffset;
            set
            {
                this.chordRootNoteOffset = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ChordRootNoteName));
                this.OnPropertyChanged(nameof(this.SelectedChordFullName));
            }
        }

        public int FinderRootNoteOffset
        {
            get => this.finderRootNoteOffset;
            set
            {
                this.finderRootNoteOffset = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.FinderRootNoteName));
                this.OnPropertyChanged(nameof(this.FinderChordFullName));
            }
        }
        /// <summary>
        /// Gets or sets the scale root note offset.
        /// </summary>
        /// <value>The scale root note offset.</value>
        public int ScaleRootNoteOffset
        {
            get => this.scaleRootNoteOffset;
            set
            {
                this.scaleRootNoteOffset = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ScaleRootNoteName));
                this.OnPropertyChanged(nameof(this.SelectedScaleFullName));
            }
        }

        /// <summary>
        /// Gets or sets the settings page.
        /// </summary>
        /// <value>The settings page.</value>
        public Page SettingsPage { get; set; }

        /// <summary>
        /// Gets or sets the chord page.
        /// </summary>
        /// <value>The chord page.</value>
        public Page ChordPage { get; set; }

        public Page FinderPage { get; set; }

        /// <summary>
        /// Gets or sets the scale page.
        /// </summary>
        /// <value>The scale page.</value>
        public Page ScalePage { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public SettingsViewModel Settings { get; set; }

        /// <summary>
        /// Called when [chord keyboard tapped].
        /// </summary>
        /// <param name="key">The key.</param>
        private void OnChordKeyboardTapped(object key)
        {
            this.ChordRootNoteOffset = int.Parse(key.ToString().Substring(3)) % 12;
        }

        /// <summary>
        /// Called when [finder keyboard tapped].
        /// </summary>
        /// <param name="key">The key.</param>
        private void OnFinderKeyboardTapped(object key)
        {
            ////var notePicked = int.Parse(key.ToString().Substring(3)) % 12;
            var notePicked = int.Parse(key.ToString().Substring(3));

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

        /// <summary>
        /// Finds the chord.
        /// </summary>
        private void FindChord()
        {
            var chordFound = false;
            Chord foundChord = null;
            this.IdentifiedChord = null;

            var finderRootOffset = this.FinderChord.Notes.Count > 0 ? this.FinderChord.Notes[0] : 0;
   
            foreach (var chord in this.Chords.Where(c => c.Notes.Count == this.FinderChord.Notes.Count))
            {
                foreach (var chordNote in this.FinderChord.Notes)
                {
                    if (!chord.Notes.Contains(chordNote - finderRootOffset))
                    {
                        chordFound = false;
                        break;
                    }
                    else
                    {
                        chordFound = true;
                        foundChord = chord;
                    }
                }

                if (chordFound)
                {
                    this.FinderRootNoteOffset = finderRootOffset;
                    this.IdentifiedChord = foundChord;
                    break;
                }

                this.FinderRootNoteOffset = finderRootOffset;
            }
        }

        /// <summary>
        /// Called when [scale keyboard tapped].
        /// </summary>
        /// <param name="key">The key.</param>
        private void OnScaleKeyboardTapped(object key)
        {
            this.ScaleRootNoteOffset = int.Parse(key.ToString().Substring(3)) % 12;
        }
    }
}