// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Stave.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Defines the Stave type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Openfeature.Music
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Stave class implementation
    /// </summary>
    public class Stave : Control
    {
        /// <summary>
        /// Notes DependencyProperty.
        /// </summary>
        public static readonly DependencyProperty NotesProperty
            = DependencyProperty.Register("Notes", typeof(List<Note>), typeof(Stave),
                                          new PropertyMetadata(null, NotesChangedCallback));

        /// <summary>
        /// Accidental symbols.
        /// </summary>
        private readonly List<TextBlock> accidentalSymbols = new List<TextBlock>();

        /// <summary>
        /// Note ellipses.
        /// </summary>
        private readonly List<Ellipse> noteSymbols = new List<Ellipse>();

        /// <summary>
        /// The clef symbol.
        /// </summary>
        private TextBlock clefSymbol;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stave"/> class.
        /// </summary>
        public Stave()
        {
            this.DefaultStyleKey = typeof(Stave);
        }

        /// <summary>
        /// Gets or sets the Notes collection.
        /// </summary>
        /// <value>The Notes.</value>
        public List<Note> Notes
        {
            get { return (List<Note>)this.GetValue(NotesProperty); }

            set { this.SetValue(NotesProperty, value); }
        }

        /// <summary>
        /// Notes changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void NotesChangedCallback(DependencyObject dependencyObject,
                                                DependencyPropertyChangedEventArgs args)
        {
            var theStave = dependencyObject as Stave;
            if (theStave != null)
            {
                theStave.Notes = args.NewValue as List<Note>;
                theStave.ShowNotes();
            }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.clefSymbol = (TextBlock)this.GetTemplateChild("ClefSymbol");
            this.noteSymbols.Add((Ellipse)this.GetTemplateChild("Note1"));
            this.noteSymbols.Add((Ellipse)this.GetTemplateChild("Note2"));
            this.noteSymbols.Add((Ellipse)this.GetTemplateChild("Note3"));
            this.noteSymbols.Add((Ellipse)this.GetTemplateChild("Note4"));
            this.noteSymbols.Add((Ellipse)this.GetTemplateChild("Note5"));
            this.noteSymbols.Add((Ellipse)this.GetTemplateChild("Note6"));
            this.noteSymbols.Add((Ellipse)this.GetTemplateChild("Note7"));
            this.noteSymbols.Add((Ellipse)this.GetTemplateChild("Note8"));
            this.accidentalSymbols.Add((TextBlock)this.GetTemplateChild("Accidental1"));
            this.accidentalSymbols.Add((TextBlock)this.GetTemplateChild("Accidental2"));
            this.accidentalSymbols.Add((TextBlock)this.GetTemplateChild("Accidental3"));
            this.accidentalSymbols.Add((TextBlock)this.GetTemplateChild("Accidental4"));
            this.accidentalSymbols.Add((TextBlock)this.GetTemplateChild("Accidental5"));
            this.accidentalSymbols.Add((TextBlock)this.GetTemplateChild("Accidental6"));
            this.accidentalSymbols.Add((TextBlock)this.GetTemplateChild("Accidental7"));
            this.accidentalSymbols.Add((TextBlock)this.GetTemplateChild("Accidental8"));
            var musiSyncFontStream = this.GetType().Assembly.GetManifestResourceStream("Openfeature.Music.themes.MusiSync.ttf");


            // TODO: GMM, 01-Apr-2020, load font code needed for this platform
            //this.clefSymbol.FontSource = new FontSource(musiSyncFontStream);
            //this.clefSymbol.FontFamily = new FontFamily("MusiSync");
            this.clefSymbol.FontSize = 96d;
            this.clefSymbol.Margin = new Thickness(5, 0, 0, 0);

            // foreach (TextBlock textBlock in this.accidentals)
            // {
            //    textBlock.FontSource = new FontSource(musiSyncFontStream);
            //    textBlock.FontFamily = new FontFamily("MusiSync");
            //    textBlock.FontSize = 40d;
            // }
        }

        /// <summary>
        /// Shows the notes.
        /// </summary>
        private void ShowNotes()
        {
            for (var noteIndex = 0; noteIndex < this.Notes.Count; noteIndex++)
            {
                this.noteSymbols[noteIndex].Visibility = Visibility.Visible;
                this.noteSymbols[noteIndex].SetValue(Grid.RowProperty, 0);
            }
        }
    }
}