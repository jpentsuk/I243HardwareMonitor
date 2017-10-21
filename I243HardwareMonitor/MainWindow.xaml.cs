﻿using System;
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
        ComputerHardware computerhardware;

        public MainWindow()
        {
			computerhardware = new ComputerHardware();
			InitializeComponent();
		}

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
			List<String> dataList = new List<String>();
			computerhardware.UpdateHardwareSensors();
			List<HardwareSensor> hardwareSensors = computerhardware.GetHardwareSensors();
			for (int i = 0; i < hardwareSensors.Count; i++)
			{
				dataList.Add(hardwareSensors[i].Name);
				dataList.Add(hardwareSensors[i].Type);
				dataList.Add(hardwareSensors[i].Value);
			}
			var message = string.Join(Environment.NewLine, dataList);
			Debug.WriteLine(message);
			// adding text into textbox
			txtbOutput.Text = message;
        }

        // application close
        private void File_Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
           
            Window helpwindow = new Window();
            helpwindow.Title = "Help";
            helpwindow.Height = 300;
            helpwindow.Width = 200;
            helpwindow.Show();

        }
    }
}
