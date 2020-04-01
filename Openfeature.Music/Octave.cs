// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Octave.cs" company="Openfeature Limited">
//   Copyright 2010 Openfeature Limited
// </copyright>
// <summary>
//   Control representing an octave of piano keys.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Openfeature.Music
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Control representing an octave of piano keys.
    /// </summary>
    public class Octave : Control
    {
        #region Private Fields

        /// <summary>
        /// List of piano keys.
        /// </summary>
        private readonly List<PianoKey> keys;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Octave"/> class.
        /// </summary>
        public Octave()
        {
            this.DefaultStyleKey = typeof(Octave);
            this.keys = new List<PianoKey>();
            Loaded += OctaveLoaded;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the keys list.
        /// </summary>
        /// <value>The keys list.</value>
        public List<PianoKey> Keys
        {
            get { return this.keys; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PopulateKeys();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the Loaded event of the Octave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private static void OctaveLoaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Populates the keys.
        /// </summary>
        private void PopulateKeys()
        {
            // var buttons = (this.GetTemplateChild("RootElement") as Grid).Children;
            // var keyButtons = from eachKey in buttons
            //                 select (Button)eachKey;
            // this.keys = keyButtons.ToList<Button>();
            // this.keys = (from eachKey in ((this.GetTemplateChild("RootElement") as Grid).Children)
            //             select (Button)eachKey) as List<Button>;
            var elementKeys = ((Grid)this.GetTemplateChild("RootElement")).Children;

            foreach (PianoKey key in elementKeys)
            {
                this.keys.Add(key);
            }
        }

        #endregion
    }
}