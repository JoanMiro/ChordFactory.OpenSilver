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
    public partial class GrandStaff : UserControl
    {
        public static readonly DependencyProperty BarsProperty = DependencyProperty.Register(
            "Bars", typeof(int), typeof(GrandStaff), new PropertyMetadata(default(int)));

        public int Bars
        {
            get => (int)this.GetValue(BarsProperty);
            set => this.SetValue(BarsProperty, value);
        }
        public GrandStaff()
        {
            this.InitializeComponent();
        }
    }
}
