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
        

        public CPUTemp()
        {

            computerHardware = InitializeHardwareMonitor();
            Debug.WriteLine(computerHardware.Hardware[0].Name);
            for (int i = 0; i < 5; i++)
            {
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[i].Name);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[i].SensorType.ToString());
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[i].Value);
            }
            
                /*
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[0].Name);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[0].SensorType.ToString());
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[0].Value);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[1].Name);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[1].SensorType.ToString());
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[1].Value);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[2].Name);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[2].SensorType.ToString());
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[2].Value);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[3].Name);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[3].SensorType.ToString());
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[3].Value);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[4].Name);
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[4].SensorType.ToString());
                Debug.WriteLine(computerHardware.Hardware[0].Sensors[4].Value);
        */


            
        }

        public OpenHardwareMonitor.Hardware.Computer InitializeHardwareMonitor()
        {
            OpenHardwareMonitor.Hardware.Computer computerHardware = new OpenHardwareMonitor.Hardware.Computer();
            computerHardware.CPUEnabled = true;
            computerHardware.Open();
            return computerHardware;
        }

        

    }
}
