// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enums.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Accidental markers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Openfeature.Music
{
    using System;

    /// <summary>
    /// Accidental markers.
    /// </summary>
    public enum Accidental
    {
        /// <summary>
        /// Flat accidental marker.
        /// </summary>
        Flat = 0,

        /// <summary>
        /// Natural/no accidental marker.
        /// </summary>
        Natural,

        /// <summary>
        /// Sharp accidental marker.
        /// </summary>
        Sharp
    }

    /// <summary>
    /// Accidental marks.
    /// </summary>
    public enum AccidentalASCIICode
    {
        /// <summary>
        /// Sharp ASCII code.
        /// </summary>
        Sharp = 128,

        /// <summary>
        /// Flat ASCII code.
        /// </summary>
        Flat = 144,

        /// <summary>
        /// Natural ASCII code.
        /// </summary>
        Natural = 160
    }

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

    /// <summary>
    /// Full scale of notes.
    /// </summary>
    public enum FullScale
    {
        /// <summary>
        /// The note C.
        /// </summary>
        C,

        /// <summary>
        /// The note C#.
        /// </summary>
        CSharp,

        /// <summary>
        /// The note D.
        /// </summary>
        D,

        /// <summary>
        /// The note Eb.
        /// </summary>
        EFlat,

        /// <summary>
        /// The note E.
        /// </summary>
        E,

        /// <summary>
        /// The note F.
        /// </summary>
        F,

        /// <summary>
        /// The note F#.
        /// </summary>
        FSharp,

        /// <summary>
        /// The note G.
        /// </summary>
        G,

        /// <summary>
        /// The note G#.
        /// </summary>
        GSharp,

        /// <summary>
        /// The note A.
        /// </summary>
        A,

        /// <summary>
        /// The note Bb.
        /// </summary>
        BFlat,

        /// <summary>
        /// The note B.
        /// </summary>
        B
    }

    /// <summary>
    /// Key signature.
    /// </summary>
    public enum KeySignature
    {
        /// <summary>
        /// Key of C KeySignature.
        /// </summary>
        C = 0,

        /// <summary>
        /// Key of G KeySignature.
        /// </summary>
        G = 161,

        /// <summary>
        /// Key of D KeySignature.
        /// </summary>
        D,

        /// <summary>
        /// Key of A KeySignature.
        /// </summary>
        A,

        /// <summary>
        /// Key of E KeySignature.
        /// </summary>
        E,

        /// <summary>
        /// Key of B KeySignature.
        /// </summary>
        B,

        /// <summary>
        /// Key of F# KeySignature.
        /// </summary>
        FSharp,

        /// <summary>
        /// Key of C# KeySignature.
        /// </summary>
        CSharp,

        /// <summary>
        /// Key of F KeySignature.
        /// </summary>
        F,

        /// <summary>
        /// Key of Bb KeySignature.
        /// </summary>
        BFlat,

        /// <summary>
        /// Key of Eb KeySignature.
        /// </summary>
        EFlat,

        /// <summary>
        /// Key of Ab KeySignature.
        /// </summary>
        AFlat,

        /// <summary>
        /// Key of Db KeySignature.
        /// </summary>
        DFlat,

        /// <summary>
        /// Key of Gb KeySignature.
        /// </summary>
        GFlat,

        /// <summary>
        /// Key of Cb KeySignature.
        /// </summary>
        CFlat
    }

    /// <summary>
    /// Note ASCIICode.
    /// </summary>
    public enum NoteASCIICode
    {
        /// <summary>
        /// A below middle C.
        /// </summary>
        A0 = 80,

        /// <summary>
        /// B below middle C.
        /// </summary>
        B0,

        /// <summary>
        /// Middle C base.
        /// </summary>
        C,

        /// <summary>
        /// D above middle C.
        /// </summary>
        D,

        /// <summary>
        /// E above middle C.
        /// </summary>
        E,

        /// <summary>
        /// F above middle C.
        /// </summary>
        F,

        /// <summary>
        /// G above middle C.
        /// </summary>
        G,

        /// <summary>
        /// A above middle C.
        /// </summary>
        A,

        /// <summary>
        /// B above middle C.
        /// </summary>
        B,

        /// <summary>
        /// Upper C base.
        /// </summary>
        C1,

        /// <summary>
        /// Upper D above upper C.
        /// </summary>
        D1,

        /// <summary>
        /// Upper E above upper C.
        /// </summary>
        E1,

        /// <summary>
        /// Upper F above upper C.
        /// </summary>
        F1,

        /// <summary>
        /// Upper G above upper C.
        /// </summary>
        G1,

        /// <summary>
        /// Upper A above upper C.
        /// </summary>
        A1
    }

    /// <summary>
    /// Note names only.
    /// </summary>
    public enum NoteName
    {
        /// <summary>
        /// The note C.
        /// </summary>
        C,

        /// <summary>
        /// The note D.
        /// </summary>
        D,

        /// <summary>
        /// The note E.
        /// </summary>
        E,

        /// <summary>
        /// The note F.
        /// </summary>
        F,

        /// <summary>
        /// The note G.
        /// </summary>
        G,

        /// <summary>
        /// The note A.
        /// </summary>
        A,

        /// <summary>
        /// The note B.
        /// </summary>
        B
    }

    /// <summary>
    /// Stave type.
    /// </summary>
    public enum Clef
    {
        /// <summary>
        /// Treble Clef stave.
        /// </summary>
        TrebleClef = 38,

        /// <summary>
        /// Bass Clef stave.
        /// </summary>
        BassClef = 175,
    }

    /// <summary>
    /// Incidental parts of the stave.
    /// </summary>
    public enum StavePart
    {
        /// <summary>
        /// Left hand bar end.
        /// </summary>
        BarEndLeft = 39,

        /// <summary>
        /// Right hand bar end.
        /// </summary>
        BarEndRight = 33,

        /// <summary>
        /// Empty bar section i.e. just stave lines.
        /// </summary>
        EmptyBarSection = 61
    }

    /// <summary>
    /// Time signature.
    /// </summary>
    public enum TimeSignature
    {
        /// <summary>
        /// No TimeSignature
        /// </summary>
        None = 0,

        /// <summary>
        /// Two-Two - March?
        /// </summary>
        TwoTwo = 49,

        /// <summary>
        /// Two-Four - March?
        /// </summary>
        TwoFour,

        /// <summary>
        /// Three-Four - Waltz.
        /// </summary>
        ThreeFour,

        /// <summary>
        /// Four-Four time.
        /// </summary>
        FourFour,

        /// <summary>
        /// ThreeTwo time.
        /// </summary>
        ThreeTwo,

        /// <summary>
        /// SixEight time.
        /// </summary>
        SixEight
    }

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