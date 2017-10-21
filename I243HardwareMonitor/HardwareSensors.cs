using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I243HardwareMonitor
{
    public class HardwareSensors
    {
        private OpenHardwareMonitor.Hardware.Computer computerHardware; 

        public HardwareSensors(OpenHardwareMonitor.Hardware.Computer computerHardware)
        {
            this.computerHardware = computerHardware;
        }

        public String getHardWareSensorName()
        {
            return computerHardware.Hardware[0].Sensors[0].Name.ToString();
        }

        public String getHardWareSensorType()
        {
            return computerHardware.Hardware[0].Sensors[0].SensorType.ToString();
        }
        public String gethardWareSensorValue()
        {
            return computerHardware.Hardware[0].Sensors[0].Value.ToString();
        }
    }
}
