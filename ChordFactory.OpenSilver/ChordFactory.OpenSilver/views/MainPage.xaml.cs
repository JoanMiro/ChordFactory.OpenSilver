namespace ChordFactory.OpenSilver.views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using models;
    using viewModels;

    public partial class MainPage : Page
    {
        private TabControl mainPageTabControl;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += this.MainPageLoaded;
        }

        private void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            this.mainPageTabControl = this.FindName("MainPageTabControl") as TabControl;
            if (this.mainPageTabControl != null)
            {
                this.mainPageTabControl.SelectionChanged += this.MainPageTabControlSelectionChanged;
            }
        }

        private async void MainPageTabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((e.RemovedItems[0] as TabItem)?.Name == "SettingsControlTab")
            {
                await ((App)Application.Current).SettingsControl.SaveSettings();
            }
        }
    }
}