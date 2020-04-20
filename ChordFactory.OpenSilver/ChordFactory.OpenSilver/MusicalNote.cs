// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MusicalNote.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Defines the MusicalNote type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChordFactory.OpenSilver
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Shapes;

    /// <summary>
    /// MusicalNote class implementation
    /// </summary>
    public class MusicalNote : ContentControl
    {
        /// <summary>
        /// Accidental Property
        /// </summary>
        public static readonly DependencyProperty AccidentalPropertyProperty =
            DependencyProperty.Register("AccidentalProperty", typeof(string), typeof(MusicalNote),
                                        new PropertyMetadata(string.Empty, OnAccidentalPropertyChanged));

        /// <summary>
        /// Accidental TextBlock
        /// </summary>
        private TextBlock accidentalTextBlock;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicalNote"/> class.
        /// </summary>
        public MusicalNote()
        {
            this.DefaultStyleKey = typeof(MusicalNote);
        }

        /// <summary>
        /// Gets the note ellipse.
        /// </summary>
        /// <value>The note ellipse.</value>
        public Ellipse NoteEllipse { get; private set; }

        /// <summary>
        /// Gets or sets the accidental property.
        /// </summary>
        /// <value>The accidental property.</value>
        public string AccidentalProperty
        {
            get { return (string)this.GetValue(AccidentalPropertyProperty); }
            set { this.SetValue(AccidentalPropertyProperty, value); }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.accidentalTextBlock = (TextBlock)this.GetTemplateChild("AccidentalTextBlock");
            this.NoteEllipse = (Ellipse)this.GetTemplateChild("NoteEllipse");
        }

        /// <summary>
        /// Called when [accidental property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnAccidentalPropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            // Get reference to self
            var source = (MusicalNote)sender;

            // Add Handling Code
            var newValue = (string)args.NewValue;
            source.accidentalTextBlock.Text = newValue;
        }
    }
}