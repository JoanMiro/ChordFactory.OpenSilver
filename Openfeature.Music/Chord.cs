// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Chord.cs" company="Openfeature Limited">
//   Copyright 2010 Openfeature Limited
// </copyright>
// <summary>
//   Class representing a piano chord.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Openfeature.Music
{
    using System.Collections.Generic;

    /// <summary>
    /// Class representing a piano chord.
    /// </summary>
    public class Chord
    {
        #region Private Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Chord"/> class.
        /// </summary>
        public Chord()
        {
            this.Notes = new List<int>();
        }

        #endregion

        #region Public Properties

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

        #endregion
    }
}