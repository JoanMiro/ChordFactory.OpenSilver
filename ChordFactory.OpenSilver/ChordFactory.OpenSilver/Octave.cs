// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Octave.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Control representing an octave of piano keys.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChordFactory.OpenSilver
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using controls;

    /// <summary>
    /// Control representing an octave of piano keys.
    /// </summary>
    public class Octave : Control
    {
        /// <summary>
        /// List of piano keys.
        /// </summary>
        private readonly List<PianoKey> keys;

        /// <summary>
        /// Initializes a new instance of the <see cref="Octave"/> class.
        /// </summary>
        public Octave()
        {
            this.DefaultStyleKey = typeof(Octave);
            this.keys = new List<PianoKey>();
            this.Loaded += OctaveLoaded;
        }

        /// <summary>
        /// Gets the keys list.
        /// </summary>
        /// <value>The keys list.</value>
        public List<PianoKey> Keys
        {
            get { return this.keys; }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PopulateKeys();
        }

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
    }
}