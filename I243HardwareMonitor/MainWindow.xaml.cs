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
        UserControlCPU ucontrolcpu = new UserControlCPU();
        UserControlGPU ucontrolgpu = new UserControlGPU();
        UserControlRAM ucontrolram = new UserControlRAM();
        UserControlHDD ucontrolhdd = new UserControlHDD();
        

        public MainWindow()
        {
			InitializeComponent();
            stc_cpu.Children.Add(ucontrolcpu);
            stc_gpu.Children.Add(ucontrolgpu);
            stc_ram.Children.Add(ucontrolram);
            stc_hdd.Children.Add(ucontrolhdd);
        }
        private void btn_Help_Click(object sender, RoutedEventArgs e)
        {
            var helpwindow = new Help();
            helpwindow.Show();
        }

        public void chc_cpu_Checked(object sender, RoutedEventArgs e)
        {
            stc_cpu.Children.Remove(ucontrolcpu);
            stc_cpu.Children.Add(ucontrolcpu);
        }

        private void chc_cpu_Unchecked(object sender, RoutedEventArgs e)
        {
            stc_cpu.Children.Remove(ucontrolcpu);
        }

        private void chc_gpu_Checked(object sender, RoutedEventArgs e)
        {
            stc_gpu.Children.Remove(ucontrolgpu);
            stc_gpu.Children.Add(ucontrolgpu);
        }

        private void chc_gpu_Unchecked(object sender, RoutedEventArgs e)
        {
            stc_gpu.Children.Remove(ucontrolgpu);
        }

        private void chc_ram_Checked(object sender, RoutedEventArgs e)
        {
            stc_ram.Children.Remove(ucontrolram);
            stc_ram.Children.Add(ucontrolram);
        }

        private void chc_ram_Unchecked(object sender, RoutedEventArgs e)
        {
            stc_ram.Children.Remove(ucontrolram);
        }

        private void chc_hdd_Checked(object sender, RoutedEventArgs e)
        {
            stc_hdd.Children.Remove(ucontrolhdd);
            stc_hdd.Children.Add(ucontrolhdd);
        }

        private void chc_hdd_Unchecked(object sender, RoutedEventArgs e)
        {
            stc_hdd.Children.Remove(ucontrolhdd);
        }
    }


}
