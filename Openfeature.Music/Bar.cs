// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bar.cs" company="Openfeature Limited">
//   Copyright 2010 Openfeature Limited
// </copyright>
// <summary>
//   Control representing a bar of music
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Openfeature.Music
{
    using System.Windows.Controls;

    /// <summary>
    /// Control representing a bar of music
    /// </summary>
    public class Bar : Control
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bar"/> class.
        /// </summary>
        public Bar()
        {
            this.DefaultStyleKey = typeof(Bar);
        }

        #endregion
    }
}