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

namespace I243HardwareMonitor
{
    /// <summary>
    /// Interaction logic for UserControlGPU.xaml
    /// </summary>
    public partial class UserControlGPU : UserControl
    {
        private HardwareInfo hardware = new HardwareInfo();
        public UserControlGPU()
        {
            InitializeComponent();
            String gpuname = hardware.ToString();
            lbl_gpuinfo.Content = gpuname;
            hardware.Update();
        }
    }
}
