using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ChordFactory.OpenSilver.controls
{
    public partial class Octave : UserControl
    {
        public Octave()
        {
            this.InitializeComponent();  
            this.Keys = new List<PianoKey>();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PopulateKeys();
        }

        private void PopulateKeys()
        {
            var childKeys = (this.GetTemplateChild("RootElement") as Grid)?.Children;

            if (childKeys != null)
            {
                foreach (var key in childKeys)
                {
                    this.Keys.Add((PianoKey)key);
                }
            }
        }

        public List<PianoKey> Keys { get; }
    }
}
