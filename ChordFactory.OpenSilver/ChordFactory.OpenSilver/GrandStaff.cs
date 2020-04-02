// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GrandStaff.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   GrandStaff control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Openfeature.Music
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    /// GrandStaff control.
    /// </summary>
    public class GrandStaff : ContentControl
    {
        /// <summary>
        /// Bar DependencyProperty.
        /// </summary>
        private static readonly DependencyProperty BarsProperty =
            DependencyProperty.Register("Bars", typeof(int), typeof(GrandStaff), new PropertyMetadata(4, BarsChangedCallback));

        /// <summary>
        /// KeySignature DependencyProperty
        /// </summary>
        private static readonly DependencyProperty KeySignatureProperty =
            DependencyProperty.Register("KeySignature", typeof(KeySignature), typeof(GrandStaff),
                                        new PropertyMetadata(KeySignature.C, KeySignatureChangedCallback));

        /// <summary>
        /// TimeSignature DependencyProperty
        /// </summary>
        private static readonly DependencyProperty TimeSignatureProperty
            = DependencyProperty.Register("TimeSignature", typeof(TimeSignature), typeof(GrandStaff),
                                          new PropertyMetadata(TimeSignature.FourFour, TimeSignatureChangedCallback));

        /// <summary>
        /// VisibleStave DependencyProperty
        /// </summary>
        private static readonly DependencyProperty VisibleStaveProperty
            = DependencyProperty.Register("VisibleStave", typeof(VisibleStave), typeof(GrandStaff),
                                          new PropertyMetadata(VisibleStave.Both, VisibleStaveChangedCallback));

        /// <summary>
        /// The bass cleft TextBlock
        /// </summary>
        private TextBlock bassClef;

        /// <summary>
        /// Bass note position grid
        /// </summary>
        private Grid bassNotePositionGrid;

        /// <summary>
        /// The treble cleft TextBlock
        /// </summary>
        private TextBlock trebleClef;

        /// <summary>
        /// Treble note position grid
        /// </summary>
        private Grid trebleNotePositionGrid;

        /// <summary>
        /// Initializes a new instance of the <see cref="GrandStaff"/> class.
        /// </summary>
        public GrandStaff()
        {
            this.DefaultStyleKey = typeof(GrandStaff);
        }

        /// <summary>
        /// Gets or sets the bars.
        /// </summary>
        /// <value>The bars count.</value>
        public int Bars
        {
            private get { return (int)this.GetValue(BarsProperty); }

            set { this.SetValue(BarsProperty, value); }
        }

        /// <summary>
        /// Gets the KeySignature.
        /// </summary>
        /// <value>The KeySignature.</value>
        private KeySignature KeySignature
        {
            get { return (KeySignature)this.GetValue(KeySignatureProperty); }
        }

        /// <summary>
        /// Gets the TimeSignature.
        /// </summary>
        /// <value>The TimeSignature.</value>
        private TimeSignature TimeSignature
        {
            get { return (TimeSignature)this.GetValue(TimeSignatureProperty); }
        }

        /// <summary>
        /// Gets the visible staves.
        /// </summary>
        private VisibleStave VisibleStave
        {
            get { return (VisibleStave)this.GetValue(VisibleStaveProperty); }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // barsGrid = (Grid)GetTemplateChild("GrandStaffGrid");
            this.trebleClef = (TextBlock)this.GetTemplateChild("TrebleClef");
            this.bassClef = (TextBlock)this.GetTemplateChild("BassClef");
            
            // trebleClefGrid = (Grid)GetTemplateChild("TrebleClefGrid");
            // bassClefGrid = (Grid)GetTemplateChild("BassClefGrid");
            this.bassNotePositionGrid = (Grid)this.GetTemplateChild("BassNotePositionGrid");
            this.trebleNotePositionGrid = (Grid)this.GetTemplateChild("TrebleNotePositionGrid");

            // TODO: GMM, 01-Apr-2020, load font code needed for this platform
            // Stream fontStream = GetType().Assembly.GetManifestResourceStream("Openfeature.Music.themes.MusiQwik.ttf");
            // this.trebleClef.FontSource = new FontSource(fontStream);
            // this.trebleClef.FontFamily = new FontFamily("MusiQwik");
            // this.bassClef.FontSource = new FontSource(fontStream);
            // this.bassClef.FontFamily = new FontFamily("MusiQwik");

            var trebleString = new StringBuilder();
            var bassString = new StringBuilder();

            trebleString.Append(MakeStaveString(Clef.TrebleClef, this.KeySignature, this.TimeSignature));

            bassString.Append(MakeStaveString(Clef.BassClef, this.KeySignature, this.TimeSignature));

            // bassNoteStartPosition = bassString.Length + 1;
            // trebleNoteStartPosition = trebleString.Length + 1;
            trebleString.Append(MakeEmptyBarString(true));
            bassString.Append(MakeEmptyBarString(true));

            for (int barIndex = 1; barIndex < this.Bars; barIndex++)
            {
                trebleString.Append(MakeEmptyBarString(false));
                bassString.Append(MakeEmptyBarString(false));
            }

            this.trebleClef.Text = trebleString.ToString();
            this.bassClef.Text = bassString.ToString();

            this.AddNoteSymbols();
            this.SetStaveVisibility();
        }

        /// <summary>
        /// Shows the scale.
        /// </summary>
        /// <param name="scale">The scale.</param>
        /// <param name="rootNote">The root note.</param>
        public void ShowScale(Scale scale, int rootNote)
        {
            // trebleClefCanvas.Height = trebleClef.ActualHeight;
            // trebleClefCanvas.Width = trebleClef.ActualWidth;
            // bassClefCanvas.Height = bassClef.ActualHeight;
            // bassClefCanvas.Width = bassClef.ActualWidth;
               
            // for (int positionIndex = 0; positionIndex < trebleNotes.Count; positionIndex++)
            // {
            //     trebleNotes[positionIndex].Visibility = Visibility.Collapsed;
            //     trebleSharps[positionIndex].Visibility = Visibility.Collapsed;
            //     trebleFlats[positionIndex].Visibility = Visibility.Collapsed;
            //     bassNotes[positionIndex].Visibility = Visibility.Collapsed;
            //     bassSharps[positionIndex].Visibility = Visibility.Collapsed;
            //     bassFlats[positionIndex].Visibility = Visibility.Collapsed;
            // }
               
            // for (int noteIndex = 0; noteIndex < scale.Notes.Count; noteIndex++)
            // {
            //     trebleNotes[noteIndex].Visibility = Visibility.Visible;
            //     double noteLeft = (double)(noteIndex * 20) + 100;
            //     double noteTop = (trebleClef.ActualHeight - 30) - (scale.Notes[noteIndex] + rootNote * NoteHeight / 2 + 2.4);
            //     trebleNotes[noteIndex].SetValue(Canvas.LeftProperty, noteLeft);
            //     trebleNotes[noteIndex].SetValue(Canvas.TopProperty, noteTop);
            //     trebleFlats[noteIndex].SetValue(Canvas.LeftProperty, noteLeft - 8d);
            //     trebleFlats[noteIndex].SetValue(Canvas.TopProperty, noteTop - 35d);
            //     trebleSharps[noteIndex].SetValue(Canvas.LeftProperty, noteLeft - 8d);
            //     trebleSharps[noteIndex].SetValue(Canvas.TopProperty, noteTop - 35d);
            // }
            // for (int noteIndex = 0; noteIndex < scale.Notes.Count; noteIndex++)
            // {
            //     double noteLeft = (double)(noteIndex * 20) + 100;
            //     double noteTop = (trebleClef.ActualHeight - 30) - (noteIndex * NoteHeight / 2 + 2.4);
            //     trebleNotes[noteIndex].SetValue(Canvas.LeftProperty, noteLeft);
            //     trebleNotes[noteIndex].SetValue(Canvas.TopProperty, noteTop);
            //     trebleFlats[noteIndex].SetValue(Canvas.LeftProperty, noteLeft - 8d);
            //     trebleFlats[noteIndex].SetValue(Canvas.TopProperty, noteTop - 35d);
            //     trebleSharps[noteIndex].SetValue(Canvas.LeftProperty, noteLeft - 8d);
            //     trebleSharps[noteIndex].SetValue(Canvas.TopProperty, noteTop - 35d);
            //     bassNotes[noteIndex].SetValue(Canvas.LeftProperty, noteLeft);
            //     bassNotes[noteIndex].SetValue(Canvas.TopProperty, noteTop);
            //     bassFlats[noteIndex].SetValue(Canvas.LeftProperty, noteLeft - 8d);
            //     bassFlats[noteIndex].SetValue(Canvas.TopProperty, noteTop - 35d);
            //     bassSharps[noteIndex].SetValue(Canvas.LeftProperty, noteLeft - 8d);
            //     bassSharps[noteIndex].SetValue(Canvas.TopProperty, noteTop - 35d);
            // }
        }

        /// <summary>
        /// Bars changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void BarsChangedCallback(DependencyObject dependencyObject,
                                                DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// KeySignature changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void KeySignatureChangedCallback(DependencyObject dependencyObject,
                                                        DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// TimeSignature changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void TimeSignatureChangedCallback(DependencyObject dependencyObject,
                                                         DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// VisibleStave changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void VisibleStaveChangedCallback(DependencyObject dependencyObject,
                                                        DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Gets the music symbol.
        /// </summary>
        /// <typeparam name="T">The required type</typeparam>
        /// <param name="symbolCode">The symbol code.</param>
        /// <returns>The relevant musical symbol</returns>
        private static string GetMusicSymbol<T>(T symbolCode) where T : struct
        {
            if (Convert.ToInt32(symbolCode) == 0)
            {
                return string.Empty;
            }

            return ((char)Convert.ToInt32(symbolCode)).ToString();
        }

        // private static string GetMusicSymbol<T, TV>(T symbolCode, TV accidentalCode)
        //     where T : struct
        //     where TV : struct
        // {
        //     int accidentalValue = Convert.ToInt32(symbolCode) + Convert.ToInt32(accidentalCode);
        //     return ((char)Convert.ToInt32(accidentalValue)).ToString();
        // }
           
        // private static string MakeBarString(IList<NoteASCIICode> notes)
        // {
        //     StringBuilder barStringBuilder = new StringBuilder();
        //     for (int positionIndex = 0; positionIndex < notes.Count; positionIndex++)
        //     {
        //         barStringBuilder.Append(GetMusicSymbol(StavePart.EmptyBarSection));
        //         barStringBuilder.Append(GetMusicSymbol(notes[positionIndex]));
        //     }
        //     barStringBuilder.Append(GetMusicSymbol(StavePart.BarEndRight));
        //     return barStringBuilder.ToString();
        // }

        /// <summary>
        /// Makes the empty bar string.
        /// </summary>
        /// <param name="partial">if set to <c>true</c> [partial].</param>
        /// <returns>String representing an empty bar.</returns>
        private static string MakeEmptyBarString(bool partial)
        {
            var barStringBuilder = new StringBuilder();

            if (!partial)
            {
                barStringBuilder.Append(GetMusicSymbol(StavePart.BarEndLeft));
            }

            for (int positionIndex = 0; positionIndex < 8; positionIndex++)
            {
                barStringBuilder.Append(GetMusicSymbol(StavePart.EmptyBarSection));
            }

            barStringBuilder.Append(GetMusicSymbol(StavePart.BarEndRight));

            return barStringBuilder.ToString();
        }

        /// <summary>
        /// Makes the stave string.
        /// </summary>
        /// <param name="stave">The stave.</param>
        /// <param name="keySignature">The key signature.</param>
        /// <param name="timeSignature">The time signature.</param>
        /// <returns>A string representing a stave</returns>
        private static string MakeStaveString(Clef stave, KeySignature keySignature, TimeSignature timeSignature)
        {
            var staveStringBuilder = new StringBuilder();

            staveStringBuilder.Append(GetMusicSymbol(StavePart.BarEndLeft));
            staveStringBuilder.Append(GetMusicSymbol(stave));
            staveStringBuilder.Append(GetMusicSymbol(keySignature));
            staveStringBuilder.Append(GetMusicSymbol(StavePart.EmptyBarSection));
            staveStringBuilder.Append(GetMusicSymbol(timeSignature));
            
            // staveStringBuilder.Append(GetMusicSymbol(StavePart.EmptyBarSection));
            return staveStringBuilder.ToString();
        }

        /// <summary>
        /// Adds the note symbols.
        /// </summary>
        private void AddNoteSymbols()
        {
            for (int notePosition = 0; notePosition < 19; notePosition++)
            {
                this.bassNotePositionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5) });
                this.trebleNotePositionGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5) });

                for (int noteCount = 0; noteCount < 8; noteCount++)
                {
                    this.bassNotePositionGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    this.trebleNotePositionGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    // Ellipse newTrebleNote = new Ellipse
                    // {
                    //     Width = NoteWidth,
                    //     Height = NoteHeight,
                    //     HorizontalAlignment = HorizontalAlignment.Left,
                    //     VerticalAlignment = VerticalAlignment.Top,
                    //     Fill = new SolidColorBrush(Colors.Black)
                    // };
                    var newTrebleNote = new MusicalNote();

                    // trebleClefCanvas.Children.Add(newTrebleNote);
                    // trebleNotes.Add(newTrebleNote);
                    newTrebleNote.SetValue(Grid.RowProperty, notePosition);
                    newTrebleNote.SetValue(Grid.ColumnProperty, noteCount);
                    this.trebleNotePositionGrid.Children.Add(newTrebleNote);

                    // TextBlock newTrebleSharp = new TextBlock
                    // {
                    //     FontSize = accidentalFontSize,
                    //     Visibility = Visibility.Collapsed,
                    //     Text = "B",
                    //     FontSource = new FontSource(fontStream),
                    //     FontFamily = new FontFamily("MusiSync")
                    // };
                    // trebleClefCanvas.Children.Add(newTrebleSharp);
                    // trebleSharps.Add(newTrebleSharp);

                    // TextBlock newTrebleFlat = new TextBlock
                    //   {
                    //       FontSize = accidentalFontSize,
                    //       Text = "b",
                    //       FontSource = new FontSource(fontStream),
                    //       FontFamily = new FontFamily("MusiSync")
                    //   };
                    // trebleClefCanvas.Children.Add(newTrebleFlat);
                    // trebleFlats.Add(newTrebleFlat);
                    var newBassNote = new MusicalNote();
                    newBassNote.SetValue(Grid.RowProperty, notePosition);
                    newBassNote.SetValue(Grid.ColumnProperty, noteCount);
                    this.bassNotePositionGrid.Children.Add(newBassNote);

                    // Ellipse newBassNote = new Ellipse
                    // {
                    //     Width = NoteWidth,
                    //     Height = NoteHeight,
                    //     HorizontalAlignment = HorizontalAlignment.Left,
                    //     VerticalAlignment = VerticalAlignment.Top,
                    //     Fill = new SolidColorBrush(Colors.Black)
                    // };
                    //  bassClefCanvas.Children.Add(newBassNote);
                    //  bassNotes.Add(newBassNote);

                    // TextBlock newBassSharp = new TextBlock
                    // {
                    //     FontSize = accidentalFontSize,
                    //     Text = "B",
                    //     FontSource = new FontSource(fontStream),
                    //     FontFamily = new FontFamily("MusiSync")
                    // };
                    // bassClefCanvas.Children.Add(newBassSharp);
                    // bassSharps.Add(newBassSharp);
                    // TextBlock newBassFlat = new TextBlock
                    // {
                    //     FontSize = accidentalFontSize,
                    //     Visibility = Visibility.Collapsed,
                    //     Text = "b",
                    //     FontSource = new FontSource(fontStream),
                    //     FontFamily = new FontFamily("MusiSync")
                    // };
                    // bassClefCanvas.Children.Add(newBassFlat);
                    // bassFlats.Add(newBassFlat);
                }
            }
        }

        /// <summary>
        /// Sets the stave visibility.
        /// </summary>
        private void SetStaveVisibility()
        {
            if (this.bassClef == null || this.trebleClef == null)
            {
                return;
            }

            this.bassClef.Visibility = (this.VisibleStave & VisibleStave.Bass) == VisibleStave.Bass
                                      ? Visibility.Visible
                                      : Visibility.Collapsed;
            this.trebleClef.Visibility = (this.VisibleStave & VisibleStave.Treble) == VisibleStave.Treble
                                        ? Visibility.Visible
                                        : Visibility.Collapsed;
        }
    }
}