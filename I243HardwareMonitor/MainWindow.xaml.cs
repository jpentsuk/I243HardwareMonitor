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
	    private HardwareInfo hardware = new HardwareInfo();

        public MainWindow()
        {
			InitializeComponent();
		}

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            String selection = cmb_makedecision.Text;
            if (selection == "")
            {
                MessageBox.Show("You have to choose something!");
            }
            else if (selection == "Store in textbox")
            {
                btn_clear.IsEnabled = true;
                cmb_makedecision.Text = "";
                txtbOutput.Text = hardware.ToString();
				hardware.Update();
	            Debug.WriteLine("Test");
				Debug.WriteLine(hardware.CPUs[0].ToString());
            }
            else
            {
                MessageBox.Show("That does not work yet");
            }
        }

        // application close
        

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_Start.IsEnabled = true;
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txtbOutput.Clear();
            btn_clear.IsEnabled = false;
        }

        private void btn_exit1_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_help_Click(object sender, RoutedEventArgs e)
        {
            var helpwindow = new Help();
            helpwindow.Show();
        }
    }
}
