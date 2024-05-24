using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SNet3.Monitor.Core
{
    public class CommandHandler : ICommand
    {
        private readonly Action command;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public CommandHandler(Action command, Func<bool> canExecute = null)
        {
            this.canExecute = canExecute;
            this.command = command ?? throw new ArgumentNullException();
        }

        public void Execute(object parameter)
        {
            command();
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }
    }
    public class ParameterCommandHandler : ICommand
    {
        private readonly Action<object> command;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ParameterCommandHandler(Action<object> command, Func<bool> canExecute = null)
        {
            this.canExecute = canExecute;
            this.command = command ?? throw new ArgumentNullException();
        }

        public void Execute(object parameter)
        {
            command(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }
    }
}
