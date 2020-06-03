namespace ChordFactory.OpenSilver.viewModels
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        private readonly Func<object, bool> canExecute;
        private readonly Action<object> executeAction;
        private bool canExecuteCache;
 
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }
 
 
        public bool CanExecute(object parameter)
        {
            var temp = this.canExecute(parameter);
 
            if (this.canExecuteCache != temp)
            {
                this.canExecuteCache = temp;
                this.CanExecuteChanged?.Invoke(this, new EventArgs());
            }
 
            return this.canExecuteCache;
        }
 
        public event EventHandler CanExecuteChanged;
 
        public void Execute(object parameter)
        {
            this.executeAction(parameter);
        }
 
    }
}