// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Scale.cs" company="Openfeature Limited">
//   Copyright 2010 Openfeature Limited
// </copyright>
// <summary>
//   Class representing a music scale.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Openfeature.Music
{
    using System.Collections.Generic;

    /// <summary>
    /// Class representing a music scale.
    /// </summary>
    public class Scale
    {
        #region Private Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Scale"/> class.
        /// </summary>
        public Scale()
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