namespace ChordFactory.OpenSilver.viewModels
{
    using System.Windows.Media;

    using extensions;

    public class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// Arpeggiate is enabled
        /// </summary>
        private bool arpeggiateIsEnabled;

        /// <summary>
        /// Audio is enabled
        /// </summary>
        private bool audioIsEnabled;

        /// <summary>
        /// The black key colour
        /// </summary>
        private Color blackKeyColour = Color.FromArgb(0, 0, 0, 0);

        /// <summary>
        /// The black key selected chord colour
        /// </summary>
        private Color blackKeySelectedChordColour = Colors.DarkOrange;

        /// <summary>
        /// The black key selected finder colour
        /// </summary>
        private Color blackKeySelectedFinderColour = Colors.DarkGoldenrod;

        /// <summary>
        /// The black key selected scale colour
        /// </summary>
        private Color blackKeySelectedScaleColour = Colors.DarkGreen;

        /// <summary>
        /// The white key colour
        /// </summary>
        private Color whiteKeyColour = Color.FromArgb(byte.MaxValue, byte.MaxValue, byte.MaxValue, 240);

        /// <summary>
        /// The white key selected chord colour
        /// </summary>
        private Color whiteKeySelectedChordColour = Colors.LightSalmon;

        /// <summary>
        /// The white key selected finder colour
        /// </summary>
        private Color whiteKeySelectedFinderColour = Colors.PaleGoldenrod;

        /// <summary>
        /// The white key selected scale colour
        /// </summary>
        private Color whiteKeySelectedScaleColour = Colors.DarkSeaGreen;

        /// <summary>
        /// The black key colour
        /// </summary>
        /// <value>The black key colour.</value>
        public Color BlackKeyColour
        {
            get => this.blackKeyColour;
            set
            {
                this.blackKeyColour = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The black key selected chord colour
        /// </summary>
        /// <value>The black key selected chord colour.</value>
        public Color BlackKeySelectedChordColour
        {
            get => this.blackKeySelectedChordColour;
            set
            {
                this.blackKeySelectedChordColour = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The black key selected scale colour
        /// </summary>
        /// <value>The black key selected scale colour.</value>
        public Color BlackKeySelectedScaleColour
        {
            get => this.blackKeySelectedScaleColour;
            set
            {
                this.blackKeySelectedScaleColour = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The black key selected finder colour
        /// </summary>
        /// <value>The black key selected finder colour.</value>
        public Color BlackKeySelectedFinderColour
        {
            get => this.blackKeySelectedFinderColour;
            set
            {
                this.blackKeySelectedFinderColour = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The white key colour
        /// </summary>
        /// <value>The white key colour.</value>
        public Color WhiteKeyColour
        {
            get => this.whiteKeyColour;
            set
            {
                this.whiteKeyColour = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The white key selected chord colour
        /// </summary>
        /// <value>The white key selected chord colour.</value>
        public Color WhiteKeySelectedChordColour
        {
            get => this.whiteKeySelectedChordColour;
            set
            {
                this.whiteKeySelectedChordColour = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The white key selected scale colour
        /// </summary>
        /// <value>The white key selected scale colour.</value>
        public Color WhiteKeySelectedScaleColour
        {
            get => this.whiteKeySelectedScaleColour;
            set
            {
                this.whiteKeySelectedScaleColour = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// The white key selected finder colour
        /// </summary>
        /// <value>The white key selected finder colour.</value>
        public Color WhiteKeySelectedFinderColour
        {
            get => this.whiteKeySelectedFinderColour;
            set
            {
                this.whiteKeySelectedFinderColour = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [audio is enabled].
        /// </summary>
        /// <value><c>true</c> if [audio is enabled]; otherwise, <c>false</c>.</value>
        public bool AudioIsEnabled
        {
            get => this.audioIsEnabled;
            set
            {
                this.audioIsEnabled = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SettingsViewModel" /> is arpeggiateIsEnabled.
        /// </summary>
        /// <value><c>true</c> if arpeggiateIsEnabled; otherwise, <c>false</c>.</value>
        public bool ArpeggiateIsEnabled
        {
            get => this.arpeggiateIsEnabled;
            set
            {
                this.arpeggiateIsEnabled = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Sets the name of the colour from.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="colourName">Name of the colour.</param>
        public void SetColourFromName(string propertyName, string colourName)
        {
            switch (propertyName)
            {
                case "BlackKeySelectedChordColour":
                    this.BlackKeySelectedChordColour = ColourUtilities.Colours[colourName];
                    break;
                case "WhiteKeySelectedChordColour":
                    this.WhiteKeySelectedChordColour = ColourUtilities.Colours[colourName];
                    break;
                case "BlackKeySelectedScaleColour":
                    this.BlackKeySelectedScaleColour = ColourUtilities.Colours[colourName];
                    break;
                case "WhiteKeySelectedScaleColour":
                    this.WhiteKeySelectedScaleColour = ColourUtilities.Colours[colourName];
                    break;
                case "BlackKeySelectedFinderColour":
                    this.BlackKeySelectedFinderColour = ColourUtilities.Colours[colourName];
                    break;
                case "WhiteKeySelectedFinderColour":
                    this.WhiteKeySelectedFinderColour = ColourUtilities.Colours[colourName];
                    break;
            }
        }
    }

}