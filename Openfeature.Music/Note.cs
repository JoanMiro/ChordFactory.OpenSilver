// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Note.cs" company="Openfeature Limited">
//   Copyright 2010 Openfeature Limited
// </copyright>
// <summary>
//   Structure representing a musical note.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Openfeature.Music
{
    using System;
    using System.Text;
    using System.Windows;

    /// <summary>
    /// Structure representing a musical note.
    /// </summary>
    public class Note : DependencyObject
    {
        #region Private Fields

        /// <summary>
        /// The raw note name i.e. just the note letter.
        /// </summary>
        private NoteName noteName;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the accidental.
        /// </summary>
        /// <value>The accidental.</value>
        public Accidental Accidental { get; set; }

        /// <summary>
        /// Gets or sets the midi value.
        /// </summary>
        /// <value>The midi value.</value>
        public int MidiValue { get; set; }

        /// <summary>
        /// Gets or sets the full note name i.e. the note letter plus any sharp or flat.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the octave.
        /// </summary>
        /// <value>The octave.</value>
        public int Octave { get; set; }

        /// <summary>
        /// Gets or sets the raw note.
        /// </summary>
        /// <value>The raw note.</value>
        public NoteName NoteName
        {
            get
            {
                return this.noteName;
            }

            set
            {
                if (this.noteName == value)
                {
                    return;
                }

                this.noteName = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Duration changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void DurationChangedCallback(DependencyObject dependencyObject,
                                                   DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A formatted string representing a note</returns>
        public string Parse(string value)
        {
            value = value.Trim().ToLower().Replace(" ", string.Empty);
            var parsedNote = new StringBuilder();

            string rawNote = value[0].ToString();
            this.NoteName = (NoteName)Enum.Parse(typeof(NoteName), rawNote, true);
            parsedNote.Append(rawNote);

            if (value.Length > 1)
            {
                char rawAccidentalChar = value[1];
                switch (rawAccidentalChar)
                {
                    case 'b':
                    case 'f':
                        this.Accidental = Accidental.Flat;
                        break;
                    case '#':
                    case 's':
                        this.Accidental = Accidental.Sharp;
                        break;
                    default:
                        this.Accidental = Accidental.Natural;
                        break;
                }

                parsedNote.Append(rawAccidentalChar.ToString());
            }

            return parsedNote.ToString();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}