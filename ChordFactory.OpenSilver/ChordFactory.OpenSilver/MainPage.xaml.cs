namespace ChordFactory.OpenSilver
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Controls;

    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += this.MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DataContext = new MusicData();
        }
    }
}