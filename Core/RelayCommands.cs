using System;
using System.Windows.Input;

namespace MinkVPN.Core
{
    public class RelayCommands : ICommand
    {

        private Action<object> execute;
        private Func<object, bool> canExecute;

        public RelayCommands(Action<object> execute, Func<object, bool> CanExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value;  }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
