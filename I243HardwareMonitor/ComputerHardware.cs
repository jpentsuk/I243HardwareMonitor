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
		private int hardwareCount;
		private List<HardwareSensor> sensorsList;

		public ComputerHardware()
        {
			computerHardware = new OpenHardwareMonitor.Hardware.Computer();
			UpdateComputerHardwareInfo();
			this.hardwareCount = computerHardware.Hardware.Count();
			Debug.WriteLine("Hardware count in the system: " + hardwareCount);
		}
		
		public List<HardwareSensor> GetHardwareSensors()
		{
			return sensorsList;
		}

		public void UpdateHardwareSensors()
		{
			UpdateHardwareSensorsList();
		}

		private void UpdateComputerHardwareInfo()
		{
			computerHardware.MainboardEnabled = true;
			computerHardware.FanControllerEnabled = true;
			computerHardware.CPUEnabled = true;
			computerHardware.GPUEnabled = true;
			computerHardware.RAMEnabled = true;
			computerHardware.HDDEnabled = true;
			computerHardware.Open();
		}

		private void UpdateHardwareSensorsList()
		{
			computerHardware.Open();
			sensorsList = new List<HardwareSensor>();
			String currentSensorName, currentSensorType, currentSensorValue;
			for (int i = 0; i < hardwareCount; i++)
			{
				//Add hardware component itself as a sensor to the sensors list
				//Value is "" due to hardware component itself having no sensor value
				currentSensorName = computerHardware.Hardware[i].Name.ToString();
				currentSensorType = computerHardware.Hardware[i].HardwareType.ToString();
				currentSensorValue = "";
				sensorsList.Add(new HardwareSensor(currentSensorName, currentSensorType, currentSensorValue));
				Debug.WriteLine(currentSensorName);
				Debug.WriteLine(currentSensorType);
				Debug.WriteLine(currentSensorValue);

				//Add hardware component sensors to the sensors list
				int hardwareSensorCount = computerHardware.Hardware[i].Sensors.Count();
				if (hardwareSensorCount > 0)
				{
					for (int u = 0; u < hardwareSensorCount; u++)
					{
						currentSensorName = computerHardware.Hardware[i].Sensors[u].Name;
						currentSensorType = computerHardware.Hardware[i].Sensors[u].SensorType.ToString();
						currentSensorValue = computerHardware.Hardware[i].Sensors[u].Value.ToString();
						sensorsList.Add(new HardwareSensor(currentSensorName, currentSensorType, currentSensorValue));
					}
				}
				
				//Update SubHardware info so it can be aqcuired
				int subHardwareCount = computerHardware.Hardware[i].SubHardware.Count();
				if (subHardwareCount > 0)
				{
					for (int u = 0; u < subHardwareCount; u++)
					{
						computerHardware.Hardware[i].SubHardware[u].Update();
					}

					//Add hardware component subhardware itself as a sensor to the sensors list
					//Value is "" due to hardware component itself having no sensor value
					for (int u = 0; u < subHardwareCount; u++)
					{
						currentSensorName = computerHardware.Hardware[i].SubHardware[u].Name;
						currentSensorType = computerHardware.Hardware[i].SubHardware[u].HardwareType.ToString();
						currentSensorValue = "";
						sensorsList.Add(new HardwareSensor(currentSensorName, currentSensorType, currentSensorValue));

						//Add hardware component subhardware sensors to the sensors list
						int subHardwareSensorCount = computerHardware.Hardware[i].SubHardware[u].Sensors.Count();
						for (int a = 0; a < subHardwareSensorCount; a++)
						{
							currentSensorName = computerHardware.Hardware[i].SubHardware[u].Sensors[a].Name;
							currentSensorType = computerHardware.Hardware[i].SubHardware[u].Sensors[a].SensorType.ToString();
							currentSensorValue = computerHardware.Hardware[i].SubHardware[u].Sensors[a].Value.ToString();
							sensorsList.Add(new HardwareSensor(currentSensorName, currentSensorType, currentSensorValue));
						}
					}
				}


			}
			computerHardware.Close();
		}
    }
}
