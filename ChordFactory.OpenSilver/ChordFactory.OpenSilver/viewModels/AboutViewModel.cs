namespace ChordFactory.OpenSilver.viewModels
{
    using System;
    using System.Windows.Browser;
    using System.Windows.Input;

    public class AboutViewModel: BaseViewModel
    {
        public AboutViewModel()
        {
            this.Title = "About";

            this.OpenWebCommand = new DelegateCommand(this.OpenUri(new Uri("http://www.openfeature.co.uk")), this.OpenUrlCanExecute);
        }

        private bool OpenUrlCanExecute(object paramList)
        {
            return true;
        }

        private Action<object> OpenUri(Uri uri)
        {
            HtmlPage.Window.Navigate(uri);
            return null;
        }

        public ICommand OpenWebCommand { get; }
    }
}