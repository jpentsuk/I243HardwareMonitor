using System.Windows;
using System.Windows.Input;

namespace I243HardwareMonitor.ViewModels
{
    public class ButtonCommands
    {
        public ICommand ShowHelpWindow
        {
            get
            {
                return new DelegateCommand
                {
                    CanExecuteFunc = () => Application.Current.MainWindow != null,
                    CommandAction = () =>
                    {
                        var helpwindow = new Views.Help();
                        helpwindow.Show();
                    }
                };
            }
        }
    }
}
