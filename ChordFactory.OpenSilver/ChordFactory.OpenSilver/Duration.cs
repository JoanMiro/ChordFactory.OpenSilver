namespace ChordFactory.OpenSilver
{
    /// <summary>
    /// Note or rest length.
    /// </summary>
    public enum Duration
    {
        /// <summary>
        /// 2 bars in four-four.
        /// </summary>
        DoubleWholeNote = 0,

        /// <summary>
        /// 1 note in a bar in four-four.
        /// </summary>
        WholeNote,

        /// <summary>
        /// 2 notes in a bar in four-four.
        /// </summary>
        HalfNote,

        /// <summary>
        /// 4 notes in a bar in four-four.
        /// </summary>
        QuarterNote,

        /// <summary>
        /// 8 notes in a bar in four-four.
        /// </summary>
        EighthNote,

        /// <summary>
        /// 16 notes in a bar in four-four.
        /// </summary>
        SixteenthNote,

        /// <summary>
        /// 32 notes in a bar in four-four.
        /// </summary>
        ThirtySecondNote,

        /// <summary>
        /// 64 notes in a bar in four-four.
        /// </summary>
        SixtyFourthNote,

        /// <summary>
        /// 128 notes in a bar in four-four.
        /// </summary>
        HundredTwentyEighthNote,

        /// <summary>
        /// 2 bars in four-four.
        /// </summary>
        Breve = DoubleWholeNote,

        /// <summary>
        /// 1 note in a bar in four-four.
        /// </summary>
        SemiBreve = WholeNote,

        /// <summary>
        /// 2 notes in a bar in four-four.
        /// </summary>
        Minim = HalfNote,

        /// <summary>
        /// 4 notes in a bar in four-four.
        /// </summary>
        Crotchet = QuarterNote,

        /// <summary>
        /// 8 notes in a bar in four-four.
        /// </summary>
        Quaver = EighthNote,

        /// <summary>
        /// 16 notes in a bar in four-four.
        /// </summary>
        SemiQuaver = SixteenthNote,

        /// <summary>
        /// 32 notes in a bar in four-four.
        /// </summary>
        DemiSemiQuaver = ThirtySecondNote,

        /// <summary>
        /// 64 notes in a bar in four-four.
        /// </summary>
        HemiDemiSemiQuaver = SixtyFourthNote,

        /// <summary>
        /// 128 notes in a bar in four-four.
        /// </summary>
        QuasiHemiDemiSemiQuaver = HundredTwentyEighthNote,

        /// <summary>
        /// 128 notes in a bar in four-four.
        /// </summary>
        SemiHemiDemiSemiQuaver = HundredTwentyEighthNote
    }
}