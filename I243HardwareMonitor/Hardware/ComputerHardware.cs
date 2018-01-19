using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


/* 
 * ComputerHardware class is able to identify and map all computer hardware and sensor info
 * The class is also responsible of OpenHardwareMonitor settings and initialization
*/

namespace I243HardwareMonitor.Hardware
{
    public class ComputerHardware
    {
		private OpenHardwareMonitor.Hardware.Computer computerHardware;
		private int hardwareCount;
		private List<HardwareComponent> hardwareComponents;

		public ComputerHardware()
        {
			computerHardware = new OpenHardwareMonitor.Hardware.Computer();
			UpdateComputerHardwareInitializationSettings();
			this.hardwareCount = computerHardware.Hardware.Count();
			Debug.WriteLine("Hardware count in the system: " + hardwareCount);
		}
		
		public List<HardwareComponent> GetUpdatedHardwareComponents()
		{
			UpdateHardwareComponents();
			return hardwareComponents;
		}

		private void UpdateComputerHardwareInitializationSettings()
		{
			computerHardware.MainboardEnabled = true;
			computerHardware.FanControllerEnabled = true;
			computerHardware.CPUEnabled = true;
			computerHardware.GPUEnabled = true;
			computerHardware.RAMEnabled = true;
			computerHardware.HDDEnabled = true;
			computerHardware.Open();
		}

		/* 
		 * We can use recursion here as each sensor is a part of its parent hardware
		 * We map this into a similar information structure to use it more easily for UI and info purposes
		 * Recursion happens if a sub-hardware is found during the mapping of a hardware and its sensors
		*/
		private HardwareComponent MapHardwareComponent(OpenHardwareMonitor.Hardware.IHardware hardwareComponent)
		{
			String hardwareComponentName, hardwareComponentType, hardwareComponentIdentifier;
			hardwareComponentName = hardwareComponent.Name;
			hardwareComponentType = hardwareComponent.HardwareType.ToString();
			hardwareComponentIdentifier = hardwareComponent.Identifier.ToString();
			HardwareComponent component = new HardwareComponent(hardwareComponentName, hardwareComponentType, hardwareComponentIdentifier);

			//Create a sensor for each of the sensors found under parent hardware component
			//This sensor is added to the list of sensors in the parent hardware components object
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

			//Create a sub-hardware (Hardware Component) for each of the hardware components found under parent hardware component
			//This hardwareComponent is added to the list of hardwareComponents in the parent hardware components object
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

		//We can use this to request hardware component and sensor info updates (names, values, types are all remapped)
		private void UpdateHardwareComponents()
		{
			hardwareComponents = new List<HardwareComponent>();
			for (int i = 0; i < hardwareCount; i++)
			{
				hardwareComponents.Add(MapHardwareComponent(computerHardware.Hardware[i]));
			}
		}
    }
}
