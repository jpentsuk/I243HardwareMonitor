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
using System.Windows.Threading;

namespace I243HardwareMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControlCPU userControlCpu;
        UserControlGPU userControlGpu;
        UserControlRAM UserControlRam;
        UserControlHDD userControlHdd;
        
        public MainWindow()
        {
	        this.userControlCpu = new UserControlCPU();
	        this.userControlGpu = new UserControlGPU();
	        this.UserControlRam = new UserControlRAM();
	        this.userControlHdd = new UserControlHDD();
			InitializeComponent();
        }
        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            var helpwindow = new Help();
            helpwindow.Show();
        }

        public void chc_cpu_Checked(object sender, RoutedEventArgs e)
        {
            stc_cpu.Children.Add(userControlCpu);
        }

        private void chc_cpu_Unchecked(object sender, RoutedEventArgs e)
        {
            stc_cpu.Children.Remove(userControlCpu);
        }

        private void chc_gpu_Checked(object sender, RoutedEventArgs e)
        {
            stc_gpu.Children.Add(userControlGpu);
        }

        private void chc_gpu_Unchecked(object sender, RoutedEventArgs e)
        {
            stc_gpu.Children.Remove(userControlGpu);
        }

        private void chc_ram_Checked(object sender, RoutedEventArgs e)
        {
            stc_ram.Children.Add(UserControlRam);
        }

        private void chc_ram_Unchecked(object sender, RoutedEventArgs e)
        {
            stc_ram.Children.Remove(UserControlRam);
        }

        private void chc_hdd_Checked(object sender, RoutedEventArgs e)
        {
            stc_hdd.Children.Add(userControlHdd);
        }

        private void chc_hdd_Unchecked(object sender, RoutedEventArgs e)
        {
            stc_hdd.Children.Remove(userControlHdd);
        }
    }
}
