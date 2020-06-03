// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MusicData.cs" company="Openfeature Limited">
//   Copyright 2020 Openfeature Limited
// </copyright>
// <summary>
//   Openfeature Music Data from XML.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ChordFactory.OpenSilver
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using models;

    /// <summary>
    /// Openfeature Music Data from XML.
    /// </summary>
    public class MusicData
    {
        /// <summary>
        /// List of chords.
        /// </summary>
        private readonly List<Chord> chords;

        /// <summary>
        /// List of intervals.
        /// </summary>
        private readonly List<Interval> intervals;

        /// <summary>
        /// List of note names
        /// </summary>
        private readonly List<Note> notes;

        /// <summary>
        /// List of scales.
        /// </summary>
        private readonly List<Scale> scales;

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicData"/> class.
        /// </summary>
        public MusicData()
        {
            this.chords = new List<Chord>();
            this.intervals = new List<Interval>();
            this.scales = new List<Scale>();
            this.notes = new List<Note>();

            this.LoadData();
        }

        /// <summary>
        /// Gets the chords.
        /// </summary>
        /// <value>The chords.</value>
        public List<Chord> Chords
        {
            get { return this.chords; }
        }

        /// <summary>
        /// Gets the intervals.
        /// </summary>
        /// <value>The intervals.</value>
        public List<Interval> Intervals
        {
            get { return this.intervals; }
        }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public List<Note> Notes
        {
            get { return this.notes; }
        }

        /// <summary>
        /// Gets the scales.
        /// </summary>
        /// <value>The scales.</value>
        public List<Scale> Scales
        {
            get { return this.scales; }
        }

        public string[] Inversions => Enum.GetNames(typeof(InversionEnum));

        /// <summary>
        /// Loads the chords.
        /// </summary>
        /// <param name="chordsReader">The chords reader.</param>
        private void LoadChords(XmlReader chordsReader)
        {
            Debug.Assert(chordsReader != null, "ChordsReader should not be null");

            while (chordsReader.ReadToFollowing("Chord"))
            {
                chordsReader.ReadToFollowing("Description");
                var newChord = new Chord
                {
                    Description = chordsReader.ReadElementContentAsString()
                };

                chordsReader.ReadToFollowing("NoteList");
                newChord.Notes.AddRange((from eachNote in chordsReader.ReadElementContentAsString().Split(',')
                                         select int.Parse(eachNote)).ToArray());
                this.chords.Add(newChord);
            }
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        private void LoadData()
        {
            var stream = this.GetType().Assembly.GetManifestResourceStream("ChordFactory.xml");
            if (stream != null)
            {
                var reader = XmlReader.Create(stream);

                reader.ReadToFollowing("Chords");
                this.LoadChords(reader.ReadSubtree());

                reader.ReadToFollowing("Scales");
                this.LoadScales(reader.ReadSubtree());

                reader.ReadToFollowing("Semitones");
                this.LoadNotes(reader.ReadSubtree());

                reader.ReadToFollowing("Intervals");
                this.LoadIntervals(reader.ReadSubtree());
            }
            else
            {
                Console.WriteLine("Stream is null!");
                Debug.WriteLine("Stream is null!");
            }
        }

        /// <summary>
        /// Loads the intervals.
        /// </summary>
        /// <param name="intervalsReader">The intervals reader.</param>
        private void LoadIntervals(XmlReader intervalsReader)
        {
            while (intervalsReader.ReadToFollowing("Interval"))
            {
                intervalsReader.ReadToFollowing("Description");
                var newInterval = new Interval
                {
                    Description = intervalsReader.ReadElementContentAsString()
                };

                intervalsReader.ReadToFollowing("Abbreviation");
                newInterval.Abbreviation = intervalsReader.ReadElementContentAsString();
                this.Intervals.Add(newInterval);
            }
        }

        /// <summary>
        /// Loads the note names.
        /// </summary>
        /// <param name="notesReader">The note names reader.</param>
        private void LoadNotes(XmlReader notesReader)
        {
            notesReader.MoveToContent();

            XNamespace ns = notesReader.NamespaceURI;

            var xe = (XElement)XNode.ReadFrom(notesReader);

            IEnumerable<XElement> semitoneSet = from semitones in xe.Elements()
                                                select semitones;

            foreach (XElement semitone in semitoneSet)
            {
                Debug.WriteLine(semitone.ToString());

                var newNote = new Note
                {
                    // ReSharper disable PossibleNullReferenceException
                    Accidental = semitone.Element(ns + "Accidental").Value.AccidentalFromString(),
                    FullName = (string)semitone.Element(ns + "FullName"),
                    MidiValue = (int)semitone.Element(ns + "MidiValue"),
                    NoteName = semitone.Element(ns + "NoteName").Value.NoteNameFromString(),
                    Octave = (int)semitone.Element(ns + "Octave")
                };

                // ReSharper restore PossibleNullReferenceException
                this.Notes.Add(newNote);
            }

            // Accidental, MidiValue, FullName, NoteName, Octave

            // while (notesReader.ReadToFollowing("Semitone"))
            // {
            //     // var testNote = notesReader.ReadElementContentAs(typeof(Note), null);
            //     notesReader.ReadToFollowing("Accidental");
            //     var newNote = new Note()
            //     {
            //         Accidental = (Accidental)notesReader.ReadElementContentAs(typeof(Accidental), null)
            //     };
            //     notesReader.ReadToFollowing("MidiValue");
            //     newNote.MidiValue = notesReader.ReadContentAsInt();
            //     notesReader.ReadToFollowing("FullName");
            //     newNote.FullName = notesReader.ReadContentAsString();
            //     notesReader.ReadToFollowing("NoteName");
            //     newNote.NoteName = (NoteName)notesReader.ReadContentAs(typeof(NoteName), null);
            //     notesReader.ReadToFollowing("Octave");
            //     newNote.Octave = notesReader.ReadContentAsInt();
            //     this.Notes.Add(newNote);
            // }
        }

        /// <summary>
        /// Loads the scales.
        /// </summary>
        /// <param name="scalesReader">The scales reader.</param>
        private void LoadScales(XmlReader scalesReader)
        {
            while (scalesReader.ReadToFollowing("Scale"))
            {
                scalesReader.ReadToFollowing("Description");
                var newScale = new Scale
                {
                    Description = scalesReader.ReadElementContentAsString()
                };

                scalesReader.ReadToFollowing("NoteList");
                newScale.Notes.AddRange((from eachNote in scalesReader.ReadElementContentAsString().Split(',')
                                         select int.Parse(eachNote)).ToArray());
                this.Scales.Add(newScale);
            }
        }
    }
}