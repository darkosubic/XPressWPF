using System;
using System.Windows.Input;

namespace XPressWPF.Shared
{
    public class CustomCommand : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        public CustomCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            bool b = execute == null || canExecute(parameter);
            return b;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
