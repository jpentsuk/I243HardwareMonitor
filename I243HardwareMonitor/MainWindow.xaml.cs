using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.Collections;
using System.Diagnostics;

namespace I243HardwareMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CPUTemp cputemp = new CPUTemp();

        public MainWindow()
        {
			
			InitializeComponent();

            
            cputemp.InitializeHardwareMonitor();
		}

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            // making string from list
            var message = string.Join(Environment.NewLine, cputemp.Getcpudatalist());

            // adding text into textbox
            txtbOutput.Text = message;
            
        }
    }
}
