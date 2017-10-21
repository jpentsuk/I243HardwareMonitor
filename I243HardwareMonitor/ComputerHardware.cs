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
    public class ComputerHardware
    {
        private OpenHardwareMonitor.Hardware.Computer computerHardware;
        private String name;
        private String type;
        private String value;
        private List<String> dataList = new List<String>();

        public ComputerHardware()
        {
            computerHardware = InitializeHardwareMonitor();
            HardwareSensors sensorInfo = new HardwareSensors(computerHardware);
            name = sensorInfo.getHardWareSensorName();
            type = sensorInfo.getHardWareSensorType();
            value = sensorInfo.gethardWareSensorValue();
            printIntoConsole();
        }

        public OpenHardwareMonitor.Hardware.Computer getComputerHardware(OpenHardwareMonitor.Hardware.Computer computerHardware)
        {
            return computerHardware;
        }

        public OpenHardwareMonitor.Hardware.Computer InitializeHardwareMonitor()
        {
            OpenHardwareMonitor.Hardware.Computer computerHardware = new OpenHardwareMonitor.Hardware.Computer();
            computerHardware.CPUEnabled = true;
            computerHardware.Open();
            return computerHardware;
        }

        public String getComputerHardwareName()
        {
            return computerHardware.Hardware[0].Name.ToString();
        }

        public List<String> getDataList()
        {
            dataList.Add(getComputerHardwareName());
            dataList.Add(name);
            dataList.Add(type);
            dataList.Add(value);
            return dataList;
        }

        //just print out to console
        public void printIntoConsole()
        {
            Debug.WriteLine(getComputerHardwareName());
            Debug.WriteLine(name);
            Debug.WriteLine(type);
            Debug.WriteLine(value);
        }
    }
}
