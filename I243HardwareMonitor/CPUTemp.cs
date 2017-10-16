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

namespace I243HardwareMonitor
{
    public class CPUTemp
    {
        private OpenHardwareMonitor.Hardware.Computer computerHardware;
        private List<String> cpudatalist = new List<String>();
        

        public CPUTemp()
        {

            computerHardware = InitializeHardwareMonitor();

            // adding all data into list
            Setcpudatalist(computerHardware);
          
        }

        public OpenHardwareMonitor.Hardware.Computer InitializeHardwareMonitor()
        {
            OpenHardwareMonitor.Hardware.Computer computerHardware = new OpenHardwareMonitor.Hardware.Computer();
            computerHardware.CPUEnabled = true;
            computerHardware.Open();
            return computerHardware;
        }

        public void Setcpudatalist(OpenHardwareMonitor.Hardware.Computer computerHardware)
        {
            cpudatalist.Add(computerHardware.Hardware[0].Name);

            for (int i = 0; i < 5; i++)
            {
                cpudatalist.Add(computerHardware.Hardware[0].Sensors[i].Name);
                cpudatalist.Add(computerHardware.Hardware[0].Sensors[i].SensorType.ToString());
                cpudatalist.Add(computerHardware.Hardware[0].Sensors[i].Value.ToString());
            }
            // just printing into console
            foreach (object o in cpudatalist)
            {
                Debug.WriteLine(o);
            }
        }
        public List<String> Getcpudatalist()
        {
            return cpudatalist;
        }



    }
}
