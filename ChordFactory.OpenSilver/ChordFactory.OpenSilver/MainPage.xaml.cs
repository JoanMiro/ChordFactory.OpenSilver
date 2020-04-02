namespace ChordFactory.OpenSilver
{
    using System.Windows.Controls;
    using Openfeature.Music;

    public partial class MainPage : Page
    {
        private readonly MusicData musicData = new MusicData();

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = this.musicData;
        }
    }
}