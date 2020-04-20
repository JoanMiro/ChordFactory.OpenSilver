// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Defines the Extensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChordFactory.OpenSilver
{
    using System.Text;

    /// <summary>
    /// Extensions class implementation.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Swaps the string part.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="newString">The new string.</param>
        /// <param name="position">The position.</param>
        /// <returns>The swapped string</returns>
        public static string SwapStringPart(this string source, string newString, int position)
        {
            var returnString = new StringBuilder(source.Substring(0, position));
            returnString.Append(newString);
            returnString.Append(source.Substring(position + newString.Length));

            return returnString.ToString();
        }

        /// <summary>
        /// Gets a note name from a string.
        /// </summary>
        /// <param name="noteNameString">The note name string.</param>
        /// <returns>The note name.</returns>
        public static NoteName NoteNameFromString(this string noteNameString)
        {
            NoteName returnValue = NoteName.A;
            switch (noteNameString.ToUpper())
            {
                case "A":
                    returnValue = NoteName.A;
                    break;
                case "B":
                    returnValue = NoteName.B;
                    break;
                case "C":
                    returnValue = NoteName.C;
                    break;
                case "D":
                    returnValue = NoteName.D;
                    break;
                case "E":
                    returnValue = NoteName.E;
                    break;
                case "F":
                    returnValue = NoteName.F;
                    break;
                case "G":
                    returnValue = NoteName.G;
                    break;
            }

            return returnValue;
        }

        /// <summary>
        /// Gets an accidentals from a string.
        /// </summary>
        /// <param name="accidentalString">The accidental string.</param>
        /// <returns>The accidental string</returns>
        public static Accidental AccidentalFromString(this string accidentalString)
        {
            Accidental returnValue = Accidental.Natural;
            switch (accidentalString.ToUpper())
            {
                case "NATURAL":
                    returnValue = Accidental.Natural;
                    break;
                case "FLAT":
                    returnValue = Accidental.Flat;
                    break;
                case "SHARP":
                    returnValue = Accidental.Sharp;
                    break;
            }

            return returnValue;
        }
    }
}