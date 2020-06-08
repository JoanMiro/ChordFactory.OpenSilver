namespace ChordFactory.OpenSilver.controls
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public partial class ColourPicker : UserControl
    {
        public static readonly DependencyProperty ColourProperty = DependencyProperty.Register(
            "Colour",
            typeof(Color),
            typeof(ColourPicker),
            new PropertyMetadata(Colors.Gray));

        private static readonly Dictionary<string, Color> ColourDictionary = new Dictionary<string, Color>();
        private ComboBox selectionPicker;
        private Border colourPickerBoxView;

        public ColourPicker()
        {
            this.DataContext = this;
            this.InitializeComponent();
            this.Loaded += this.ColourPicker_Loaded;
        }

        public Color Colour
        {
            get => (Color)this.GetValue(ColourProperty);
            set => this.SetValue(ColourProperty, value);
        }

        //public ComboBox SelectionPicker { get; private set; }


        public static Dictionary<string, Color> Colours
        {
            get
            {
                if (ColourDictionary.Count == 0)
                {
                    foreach (var field in typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public))
                    {
                        if (field.PropertyType == typeof(Color))
                        {
                            ColourDictionary.Add(field.Name, (Color)field.GetValue(Application.Current));
                        }
                    }
                }

                return ColourDictionary;
            }
        }

        private void ColourPicker_Loaded(object sender, RoutedEventArgs e)
        {
            this.selectionPicker = this.FindName("SelectionPicker") as ComboBox;
            this.colourPickerBoxView = this.FindName("ColourPickerBoxView") as Border;
            if (this.selectionPicker != null)
            {
                foreach (var colorName in Colours.Keys)
                {
                    this.selectionPicker.Items.Add(colorName);
                }
            }
        }

        private void PickerSelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetColourPickerColour();
        }

        private void SetColourPickerColour()
        {
            var colorName = this.selectionPicker.Items[this.selectionPicker.SelectedIndex].ToString();
            this.Colour = Colours[colorName];
            this.ColourPickerBoxView.Background = new SolidColorBrush(this.Colour);
        }
    }
}