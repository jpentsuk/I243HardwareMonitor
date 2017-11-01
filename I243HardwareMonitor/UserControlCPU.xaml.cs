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
    /// Interaction logic for UserControlCPU.xaml
    /// </summary>
    public partial class UserControlCPU : UserControl
    {
        private HardwareInfo hardware = new HardwareInfo();
        private DispatcherTimer timer = new DispatcherTimer();
        


        public UserControlCPU()
        {
            InitializeComponent();
            Debug.WriteLine(hardware.ToString());
            starttimer();

        }
        public void timer_Tick(object sender, EventArgs e)
        {
            hardware.Update();
            lbl_cpusensor0.Content = hardware.CPUs[0].Sensors[0].Name +
            " " + hardware.CPUs[0].Sensors[0].Type + " " +
            hardware.CPUs[0].Sensors[0].Value; 
            lbl_cpusensor1.Content = hardware.CPUs[0].Sensors[1].Name +
            " " + hardware.CPUs[0].Sensors[1].Type + " " +
            hardware.CPUs[0].Sensors[1].Value;
        }
        public void starttimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        

        

        

        


        
    }
}
