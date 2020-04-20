// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bar.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Control representing a bar of music
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChordFactory.OpenSilver
{
    using System.Windows.Controls;

    /// <summary>
    /// Control representing a bar of music
    /// </summary>
    public class Bar : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bar"/> class.
        /// </summary>
        public Bar()
        {
            this.DefaultStyleKey = typeof(Bar);
        }
    }
}