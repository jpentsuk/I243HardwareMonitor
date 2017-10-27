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
		private List<HardwareComponent> hardwareComponents;

		public ComputerHardware()
        {
			computerHardware = new OpenHardwareMonitor.Hardware.Computer();
			UpdateComputerHardwareInfo();
			this.hardwareCount = computerHardware.Hardware.Count();
			Debug.WriteLine("Hardware count in the system: " + hardwareCount);
		}
		
		public List<HardwareComponent> GetUpdatedHardwareComponents()
		{
			UpdateHardwareComponents();
			return hardwareComponents;
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

		private HardwareComponent MapHardwareComponent(OpenHardwareMonitor.Hardware.IHardware hardwareComponent)
		{
			String hardwareComponentName, hardwareComponentType, hardwareComponentIdentifier;
			hardwareComponentName = hardwareComponent.Name.ToString();
			hardwareComponentType = hardwareComponent.HardwareType.ToString();
			hardwareComponentIdentifier = hardwareComponent.Identifier.ToString();
			HardwareComponent component = new HardwareComponent(hardwareComponentName, hardwareComponentType, hardwareComponentIdentifier);

			int sensorCount = hardwareComponent.Sensors.Count();
			hardwareComponent.Update();
			for (int i = 0; i < sensorCount; i++)
			{
				String sensorName, sensorType, sensorValue;
				sensorName = hardwareComponent.Sensors[i].Name;
				sensorType = hardwareComponent.Sensors[i].SensorType.ToString();
				sensorValue = hardwareComponent.Sensors[i].Value.ToString();
				HardwareSensor sensor = new HardwareSensor(sensorName, sensorType, sensorValue);
				component.AddSensor(sensor);
			}

			int subHardwareCount = hardwareComponent.SubHardware.Count();
			if (subHardwareCount > 0)
			{
				for (int u = 0; u < subHardwareCount; u++)
				{
					hardwareComponent.SubHardware[u].Update();
				}

				for (int a = 0; a < subHardwareCount; a++)
				{
					component.AddSubHardware(MapHardwareComponent(hardwareComponent.SubHardware[a]));
				}
			}
			return component;
		}

		private void UpdateHardwareComponents()
		{
			//computerHardware.Open();
			hardwareComponents = new List<HardwareComponent>();
			for (int i = 0; i < hardwareCount; i++)
			{
				hardwareComponents.Add(MapHardwareComponent(computerHardware.Hardware[i]));
			}
			//computerHardware.Close();
		}
    }
}
