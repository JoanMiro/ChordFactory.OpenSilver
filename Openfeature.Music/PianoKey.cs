// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PianoKey.cs" company="Openfeature Limited">
//   Copyright 2010 Openfeature Limited
// </copyright>
// <summary>
//   Defines the PianoKey type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Openfeature.Music
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// PianoKey class implementation
    /// </summary>
    public class PianoKey : Button
    {
        #region Dependency Properties

        /// <summary>
        /// ChordNote DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ChordNoteProperty
            = DependencyProperty.Register("ChordNote", typeof(bool), typeof(PianoKey),
                                          new PropertyMetadata(new PropertyChangedCallback(ChordNoteChangedCallback)));

        /// <summary>
        /// ChordSymbol DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ChordSymbolProperty
            = DependencyProperty.Register("ChordSymbol", typeof(string), typeof(PianoKey),
                                          new PropertyMetadata("1", ChordSymbolChangedCallback));

        /// <summary>
        /// RootNote DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty RootNoteProperty
            = DependencyProperty.Register("RootNote", typeof(bool), typeof(PianoKey),
                                          new PropertyMetadata(false, RootNoteChangedCallback));

        /// <summary>
        /// RootSymbol DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty RootSymbolProperty
            = DependencyProperty.Register("RootSymbol", typeof(string), typeof(PianoKey),
                                          new PropertyMetadata("R", RootSymbolChangedCallback));

        /// <summary>
        /// ScaleNote DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty ScaleNoteProperty
            = DependencyProperty.Register("ScaleNote", typeof(bool), typeof(PianoKey),
                                          new PropertyMetadata(false, ScaleNoteChangedCallback));

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PianoKey"/> class.
        /// </summary>
        public PianoKey()
        {
            this.DefaultStyleKey = typeof(PianoKey);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether key is a chord note.
        /// </summary>
        /// <value>Will be <c>true</c> if [hord note; otherwise, <c>false</c>.</value>
        public bool ChordNote
        {
            get { return (bool)this.GetValue(ChordNoteProperty); }

            set { this.SetValue(ChordNoteProperty, value); }
        }

        /// <summary>
        /// Gets or sets the chord symbol.
        /// </summary>
        /// <value>The chord symbol.</value>
        public string ChordSymbol
        {
            get { return (string)this.GetValue(ChordSymbolProperty); }

            set { this.SetValue(ChordSymbolProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether key is a root note.
        /// </summary>
        /// <value>Will be <c>true</c> if [root note]; otherwise, <c>false</c>.</value>
        public bool RootNote
        {
            get { return (bool)this.GetValue(RootNoteProperty); }

            set { this.SetValue(RootNoteProperty, value); }
        }

        /// <summary>
        /// Gets or sets the root symbol.
        /// </summary>
        /// <value>The root symbol.</value>
        public string RootSymbol
        {
            get { return (string)this.GetValue(RootSymbolProperty); }

            set { this.SetValue(RootSymbolProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether scale note.
        /// </summary>
        /// <value>Will be <c>true</c> if [scale note]; otherwise, <c>false</c>.</value>
        public bool ScaleNote
        {
            get { return (bool)this.GetValue(ScaleNoteProperty); }

            set { this.SetValue(ScaleNoteProperty, value); }
        }

        #endregion

        #region private Static Methods

        /// <summary>
        /// ChordNote changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ChordNoteChangedCallback(DependencyObject dependencyObject,
                                                     DependencyPropertyChangedEventArgs args)
        {
            var newValue = (bool)args.NewValue;
            var theKey = (PianoKey)dependencyObject;
            theKey.ChordNote = newValue;

            VisualStateManager.GoToState(theKey, newValue ? "ChordMember" : "NotChordMember", true);
        }

        /// <summary>
        /// RootNote changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void RootNoteChangedCallback(DependencyObject dependencyObject,
                                                    DependencyPropertyChangedEventArgs args)
        {
            var newValue = (bool)args.NewValue;
            var theKey = (PianoKey)dependencyObject;
            theKey.RootNote = newValue;

            VisualStateManager.GoToState(theKey, newValue ? "RootNote" : "NotRootNote", true);
        }

        /// <summary>
        /// RootSymbol changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void RootSymbolChangedCallback(DependencyObject dependencyObject,
                                                      DependencyPropertyChangedEventArgs args)
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// ChordSymbol changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ChordSymbolChangedCallback(DependencyObject dependencyObject,
                                                       DependencyPropertyChangedEventArgs args)
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// ScaleNote changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ScaleNoteChangedCallback(DependencyObject dependencyObject,
                                                     DependencyPropertyChangedEventArgs args)
        {
            var newValue = (bool)args.NewValue;
            var theKey = (PianoKey)dependencyObject;
            theKey.ScaleNote = newValue;

            VisualStateManager.GoToState(theKey, newValue ? "ScaleMember" : "NotScaleMember", true);
        }

        #endregion
    }
}