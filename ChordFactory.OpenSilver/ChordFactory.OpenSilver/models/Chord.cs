// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Chord.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Class representing a piano chord.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChordFactory.OpenSilver.models
{
    using System.Collections.Generic;

    /// <summary>
    /// Class representing a piano chord.
    /// </summary>
    public class Chord : INoteSequence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Chord"/> class.
        /// </summary>
        public Chord()
        {
            this.Notes = new List<int>();
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public List<int> Notes { get; set; }

        public static Chord Create(string description, List<int> notes)
        {
            return new Chord
            {
                Notes = notes,
                Description = description
            };
        }
    }
}