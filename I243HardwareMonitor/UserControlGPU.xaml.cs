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
using System.Windows.Threading;

namespace I243HardwareMonitor
{
    /// <summary>
    /// Interaction logic for UserControlGPU.xaml
    /// </summary>
    public partial class UserControlGPU : UserControl
    {
        private HardwareInfo hardware = new HardwareInfo();
        private DispatcherTimer timer = new DispatcherTimer();
        public UserControlGPU()
        {
            InitializeComponent();
            startTimer();
        }
        public void timer_Tick(object sender, EventArgs e)
        {
            hardware.Update();
            lbl_gpusensors.Content = hardware.CPUs[0].ToString();
        }
        public void startTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
    }
}
