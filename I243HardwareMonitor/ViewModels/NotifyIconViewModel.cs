using System;
using System.Windows;
using System.Windows.Input;

namespace I243HardwareMonitor.ViewModels
{
    //Solution for this class is largely based on example given in https://www.codeproject.com/Articles/36468/WPF-NotifyIcon
    //This is the suggested method by the maker of the NotifyIcon add-on used in this project.
    public class NotifyIconViewModel
    {   
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
                        Application.Current.MainWindow = new Views.MainWindow();
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
