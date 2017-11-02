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
using System.Windows.Threading;

namespace I243HardwareMonitor
{
    /// <summary>
    /// Interaction logic for UserControlHDD.xaml
    /// </summary>
    public partial class UserControlHDD : UserControl
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private HardwareInfo hardware = new HardwareInfo();
        public UserControlHDD()
        {
            InitializeComponent();
            startTimer();
        }
        public void timer_Tick(object sender, EventArgs e)
        {
            hardware.Update();
            lbl_hddsensors.Content = hardware.HDDs[0].ToString();
        }
        public void startTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
    }
}
