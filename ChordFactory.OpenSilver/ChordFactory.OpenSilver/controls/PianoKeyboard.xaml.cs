using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace ChordFactory.OpenSilver.controls
{
    using Openfeature.Music;

    public partial class PianoKeyboard : UserControl
    {
        private StackPanel keyboardStackPanel;
        private List<PianoKey> keys;
        public static readonly DependencyProperty OctavesProperty = DependencyProperty.Register(
            "Octaves", typeof(int), typeof(PianoKeyboard), new PropertyMetadata(default(int)));

        public int Octaves
        {
            get => (int)this.GetValue(OctavesProperty);
            set => this.SetValue(OctavesProperty, value);
        }
        public PianoKeyboard()
        {
            this.InitializeComponent();
            this.keys = new List<PianoKey>();
        }

        public MusicData MusicData { get; } = new MusicData();
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();           
            
            this.keyboardStackPanel = (StackPanel)this.GetTemplateChild("KeyboardStackPanel");
            this.PopulateOctaves();
            this.PopulateKeys();
        }

        public List<PianoKey> Keys
        {
            get
            {
                if (this.keys.Count == 0)
                {
                    this.PopulateKeys();
                }

                return this.keys;
            }
        }
        private void PopulateKeys()
        {
            for (int octaveIndex = 0; octaveIndex < this.keyboardStackPanel.Children.Count; octaveIndex++)
            {
                var currentOctave = this.keyboardStackPanel.Children[octaveIndex] as Octave;
                for (int keyIndex = 0; keyIndex < currentOctave.Keys.Count; keyIndex++)
                {
                    this.keys.Add(currentOctave.Keys[keyIndex]);
                }
            }
        }

        private void PopulateOctaves()
        {
            for (var octaveCount = 0; octaveCount < this.Octaves; octaveCount++)
            {
                var newOctave = new Octave { Width = this.Width / this.Octaves };
                this.keyboardStackPanel.Children.Add(newOctave);
            }
        }
    }
}
