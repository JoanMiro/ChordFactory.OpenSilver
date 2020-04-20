namespace ChordFactory.OpenSilver
{
    using System;
    using System.Diagnostics;
    using System.Windows.Controls;

    public partial class MainPage : Page
    {
        private MusicData musicData;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += this.MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.musicData = new MusicData();
            this.DataContext = this.musicData;
            this.ShowDiagnostics();
        }

        private void ShowDiagnostics()
        {
            Console.WriteLine($"Chords Count [{this.musicData.Chords.Count}]");
            Console.WriteLine($"Scales Count [{this.musicData.Scales.Count}]");

            Debug.WriteLine($"Chords Count [{this.musicData.Chords.Count}]");
            Debug.WriteLine($"Scales Count [{this.musicData.Scales.Count}]");

            //if (this.FindName("chordName") is TextBlock chordName)
            //{
            //    chordName.Text = $"Chords Count [{this.musicData.Chords.Count}]";
            //}

            //if (this.FindName("scaleName") is TextBlock scaleName)
            //{
            //    scaleName.Text = $"Scales Count [{this.musicData.Scales.Count}]";
            //}
        }
    }
}