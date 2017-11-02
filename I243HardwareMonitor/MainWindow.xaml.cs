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
		private HardwareInfo hardware;
		private DispatcherTimer timer;
		private UserControlMainView userControlCpu;
		private UserControlMainView userControlGpu;
		private UserControlMainView userControlRam;
		private UserControlMainView userControlHdd;

		public MainWindow()
		{
			this.hardware = new HardwareInfo();
			this.timer = new DispatcherTimer();
			this.userControlCpu = new UserControlMainView(hardware, ViewType.CPU);
			this.userControlGpu = new UserControlMainView(hardware, ViewType.GPU);
			this.userControlRam = new UserControlMainView(hardware, ViewType.RAM);
			this.userControlHdd = new UserControlMainView(hardware, ViewType.HDD);
			InitializeComponent();
			stc_cpu.Children.Add(userControlCpu);
			stc_gpu.Children.Add(userControlGpu);
			stc_hdd.Children.Add(userControlHdd);
			stc_ram.Children.Add(userControlRam);
			StartTimer();
		}

		private void StartTimer()
		{
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += UpdateInfoOnMainViewComponents;
			timer.Start();
		}

		private void UpdateInfoOnMainViewComponents(object sender, EventArgs e)
		{
			hardware.Update();
			userControlCpu.UpdateLabelInfo();
			userControlGpu.UpdateLabelInfo();
			userControlRam.UpdateLabelInfo();
			userControlHdd.UpdateLabelInfo();
		}

		private void chc_viewToggle_Changed(object sender, RoutedEventArgs e)
		{
			bool? cpuChecked = chc_cpu.IsChecked;
			bool? gpuChecked = chc_gpu.IsChecked;
			bool? ramChecked = chc_ram.IsChecked;
			bool? hddChecked = chc_hdd.IsChecked;

			if (cpuChecked == true)
			{
				stc_cpu.Children[0].Visibility = Visibility.Visible;
			}
			else
			{
				stc_cpu.Children[0].Visibility = Visibility.Hidden;
			}
			if (gpuChecked == true)
			{
				stc_gpu.Children[0].Visibility = Visibility.Visible;
			}
			else
			{
				stc_gpu.Children[0].Visibility = Visibility.Hidden;
			}
			if (ramChecked == true)
			{
				stc_ram.Children[0].Visibility = Visibility.Visible;
			}
			else
			{
				stc_ram.Children[0].Visibility = Visibility.Hidden;
			}
			if (hddChecked == true)
			{
				stc_hdd.Children[0].Visibility = Visibility.Visible;
			}
			else
			{
				stc_hdd.Children[0].Visibility = Visibility.Hidden;
			}
		}

		private void btn_Help_Click(object sender, RoutedEventArgs e)
		{
			var helpwindow = new Help();
			helpwindow.Show();
		}
	}
}
