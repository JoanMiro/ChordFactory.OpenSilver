namespace ChordFactory.OpenSilver.controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Annotations;

    public class ColourPicker : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty ColourProperty = DependencyProperty.Register(
            "Colour",
            typeof(Color),
            typeof(ColourPicker),
            new PropertyMetadata(Colors.Gray));

        public static readonly DependencyProperty SelectedColourProperty = DependencyProperty.Register(
            "SelectedColour",
            typeof(SolidColorBrush),
            typeof(ColourPicker),
            new PropertyMetadata(default(SolidColorBrush)));

        public static readonly DependencyProperty ColourNamesProperty = DependencyProperty.Register(
            "ColourNames",
            typeof(List<string>),
            typeof(ColourPicker),
            new PropertyMetadata(default(List<string>)));

        public static readonly DependencyProperty SelectedColourNameProperty = DependencyProperty.Register(
            "SelectedColourName", typeof(string), typeof(ColourPicker), new PropertyMetadata(default(string)));

        private static readonly Dictionary<string, Color> ColourDictionary = new Dictionary<string, Color>();
        private Border colourPickerBoxView;
        private ComboBox selectionPicker;

        public ColourPicker()
        {
            this.DefaultStyleKey = typeof(ColourPicker);
        }

        public string SelectedColourName
        {
            get => (string)this.GetValue(SelectedColourNameProperty);
            set => this.SetValue(SelectedColourNameProperty, value);
        }

        public List<string> ColourNames
        {
            get => (List<string>)this.GetValue(ColourNamesProperty);
            set => this.SetValue(ColourNamesProperty, value);
        }

        public SolidColorBrush SelectedColour
        {
            get => (SolidColorBrush)this.GetValue(SelectedColourProperty);
            set => this.SetValue(SelectedColourProperty, value);
        }

        public Color Colour
        {
            get => (Color)this.GetValue(ColourProperty);
            set => this.SetValue(ColourProperty, value);
            //this.SelectedColour = new SolidColorBrush(value);
            //this.OnPropertyChanged(nameof(this.Colour));
            //this.OnPropertyChanged(nameof(this.SelectedColour));
        }

        public Dictionary<string, Color> Colours => ColourDictionary;

        public event PropertyChangedEventHandler PropertyChanged;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.selectionPicker = this.GetTemplateChild("SelectionPicker") as ComboBox;
            this.colourPickerBoxView = this.GetTemplateChild("ColourPickerBoxView") as Border;
            if (this.selectionPicker != null)
            {
                this.selectionPicker.SelectionChanged += this.PickerSelectedIndexChanged;
            }

            this.LoadColours();
        }

        private void LoadColours()
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

            this.ColourNames = ColourDictionary.Select(c => c.Key).ToList();
        }

        private void ColourPicker_Loaded(object sender, RoutedEventArgs e)
        {
            this.selectionPicker = this.FindName("SelectionPicker") as ComboBox;
            this.colourPickerBoxView = this.FindName("ColourPickerBoxView") as Border;
            if (this.selectionPicker != null)
            {
                foreach (var colorName in this.Colours.Keys)
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
            this.Colour = this.Colours[colorName];
            this.SelectedColour = new SolidColorBrush(this.Colour);
            this.SelectedColourName = colorName;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}