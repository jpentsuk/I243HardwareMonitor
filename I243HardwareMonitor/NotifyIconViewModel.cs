using System;
using System.Windows;
using System.Windows.Input;

namespace I243HardwareMonitor
{
    public class NotifyIconViewModel
    {

        public class DelegateCommand : ICommand
        {
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

            public void Execute(object parameter)
            {
                CommandAction();
            }
        }
        
        /// <summary>
        /// Shows a window, if there isn't one open already. ICommand is mandatory to use button commands.
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow = new MainWindow();
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }

        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = () => Application.Current.MainWindow.Close(),
                    CanExecuteFunc = () => Application.Current.MainWindow != null
                };
            }
        }

        public ICommand ExitProgramCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
            }
        }
    }
}
