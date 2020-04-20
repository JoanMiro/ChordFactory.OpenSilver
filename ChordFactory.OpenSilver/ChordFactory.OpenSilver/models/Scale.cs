// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scale.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Class representing a music scale.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChordFactory.OpenSilver.models
{
    using System.Collections.Generic;

    /// <summary>
    /// Class representing a music scale.
    /// </summary>
    public class Scale
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Scale"/> class.
        /// </summary>
        public Scale()
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
    }
}