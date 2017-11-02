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
using System.Diagnostics;


namespace I243HardwareMonitor
{
    /// <summary>
    /// Interaction logic for UserControlRAM.xaml
    /// </summary>
    public partial class UserControlRAM : UserControl
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private HardwareInfo hardware = new HardwareInfo();

        public UserControlRAM()
        {
            InitializeComponent();
            starttimer();
        }
        public void timer_Tick(object sender, EventArgs e)
        {
            hardware.Update();
            lbl_ramsensors.Content = hardware.RAM.ToString();
        }
        public void starttimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
    }
}
