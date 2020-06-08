namespace ChordFactory.OpenSilver.viewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using models;

    public class ScaleKeyboardViewModel: BaseViewModel
    {
        public ScaleKeyboardViewModel()
        {
            this.Title = "Keyboard";
            this.Items = new ObservableCollection<Item>();
            this.LoadItemsCommand = new DelegateCommand(this.ExecuteLoadItemsCommand(), this.ExecuteLoadItemsCanExecute);
        }

        private bool ExecuteLoadItemsCanExecute(object paramList)
        {
            return true;
        }

        public List<Scale> Scales => this.MusicData.Scales;

        public ObservableCollection<Item> Items { get; set; }

        public DelegateCommand LoadItemsCommand { get; set; }

        private Action<object> ExecuteLoadItemsCommand()
        {
            if (this.IsBusy)
                return null;

            this.IsBusy = true;

            try
            {
                this.Items.Clear();
                //var items = await this.DataStore.GetItemsAsync(true);
                //foreach (var item in items)
                //{
                //    this.Items.Add(item);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                this.IsBusy = false;
            }

            return null;
        }
    }
}