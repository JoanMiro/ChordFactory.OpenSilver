namespace ChordFactory.OpenSilver
{
    using System;

    /// <summary>
    /// Staves to show in the GrandStaff.
    /// </summary>
    [Flags]
    public enum VisibleStave
    {
        /// <summary>
        /// Treble staff.
        /// </summary>
        Treble = 1,

        /// <summary>
        /// Bass staff.
        /// </summary>
        Bass = 2,

        /// <summary>
        /// Both treble and bass.
        /// </summary>
        Both = Treble | Bass
    }
}