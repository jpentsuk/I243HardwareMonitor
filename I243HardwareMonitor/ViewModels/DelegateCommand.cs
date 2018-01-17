using System;
using System.Windows.Input;

namespace I243HardwareMonitor.ViewModels
{
    //This class was made according to the example given here: https://www.codeproject.com/Articles/36468/WPF-NotifyIcon

    /// <summary>
    /// Enables the use of commands for buttons
    /// </summary>
    public class DelegateCommand : ICommand
    {   
        //Button commands require CanExecuteFunc to know, if the command is executable, and
        //CommandAction to specify the command to execute.
        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        //Command will be executed only if CanExecute == true.
        public void Execute(object parameter)
        {
            CommandAction();
        }
    }
}
