namespace ChordFactory.OpenSilver.models
{
    public class NoteKeyInfo
    {
        /// <summary>
        /// Gets or sets the note number.
        /// </summary>
        /// <value>The note number.</value>
        public int NoteNumber { get; set; }

        /// <summary>
        /// Gets or sets the adjusted note number.
        /// </summary>
        /// <value>The adjusted note number.</value>
        public int AdjustedNoteNumber { get; set; }

        /// <summary>
        /// Gets or sets the index of the interval.
        /// </summary>
        /// <value>The index of the interval.</value>
        public int IntervalIndex { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the root note.
        /// </summary>
        /// <value>The root note.</value>
        public RootNotes RootNote { get; set; }

    }
}