namespace ChordFactory.OpenSilver.viewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;

    public class BaseViewModel : INotifyPropertyChanged
    {
        private string title;
        private bool isBusy;
        public bool IsBusy
        {
            get => this.isBusy;
            set => this.SetProperty(ref this.isBusy, value);
        }

        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
                                      [CallerMemberName] string propertyName = "",
                                      Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}