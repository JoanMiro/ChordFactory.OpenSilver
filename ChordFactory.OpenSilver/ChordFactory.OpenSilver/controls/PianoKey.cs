// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PianoKey.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Defines the PianoKey type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChordFactory.OpenSilver.controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// PianoKey class implementation
    /// </summary>
    public partial class PianoKey : Button
    {
        /// <summary>
        /// The is scale key property.
        /// </summary>
        public static readonly DependencyProperty IsScaleKeyProperty =
            DependencyProperty.Register("IsScaleKey", typeof(bool), typeof(PianoKey),
                new PropertyMetadata(false, OnIsScaleKeyChanged));

        /// <summary>
        /// The is chord key property.
        /// </summary>
        public static readonly DependencyProperty IsChordKeyProperty =
            DependencyProperty.Register("IsChordKey", typeof(bool), typeof(PianoKey),
                new PropertyMetadata(false, OnIsChordKeyChanged));

        /// <summary>
        /// The is chord key property.
        /// </summary>
        public static readonly DependencyProperty IsRootKeyProperty =
            DependencyProperty.Register("IsRootKey", typeof(bool), typeof(PianoKey),
                new PropertyMetadata(false, OnIsRootKeyChanged));
        
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

        /// <summary>
        /// Initializes a new instance of the <see cref="PianoKey"/> class.
        /// </summary>
        public PianoKey()
        {
            this.InitializeComponent();
            this.DefaultStyleKey = typeof(PianoKey);
        }

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
        /// Gets or sets a value indicating whether IsChordKey.
        /// </summary>
        public bool IsChordKey
        {
            get { return (bool)this.GetValue(IsChordKeyProperty); }
            set { this.SetValue(IsChordKeyProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsScaleKey.
        /// </summary>
        public bool IsScaleKey
        {
            get { return (bool)this.GetValue(IsScaleKeyProperty); }
            set { this.SetValue(IsScaleKeyProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether key is a root note.
        /// </summary>
        /// <value>Will be <c>true</c> if [root note]; otherwise, <c>false</c>.</value>
        public bool IsRootKey
        {
            get { return (bool)this.GetValue(IsRootKeyProperty); }

            set { this.SetValue(IsRootKeyProperty, value); }
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
        /// <summary>
        /// Called when [is chord key changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsChordKeyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var source = (PianoKey)sender;

            var newValue = (bool)args.NewValue;
            VisualStateManager.GoToState(source, newValue ? "IsChordKey" : "IsNotChordKey", true);
        }

        /// <summary>
        /// Called when [is root key changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsRootKeyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var source = (PianoKey)sender;

            var newValue = (bool)args.NewValue;
            VisualStateManager.GoToState(source, newValue ? "IsRootKey" : "IsNotRootKey", true);
        }

        /// <summary>
        /// Called when [is scale key changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnIsScaleKeyChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var source = (PianoKey)sender;

            var newValue = (bool)args.NewValue;
            VisualStateManager.GoToState(source, newValue ? "IsScaleKey" : "IsNotScaleKey", true);
        }
    }
}