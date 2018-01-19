using System;
using System.Collections.Generic;
using System.Linq;

/* 
 * HardwareComponent object is created for each hardware and sub-hardware found during mapping of the hardware in ComputerHardware class
 * The class keeps information on the mapped component such as name, type and identifier
 * This object has HardwareSensors and HardwareComponents added to a list in it for every sensor and sub-hardware this hardware unit has
*/

namespace I243HardwareMonitor.Hardware
{
	public class HardwareComponent : Component
	{
		public String Identifier { get; }
		public List<HardwareSensor> Sensors { get; }
		private List<HardwareComponent> SubHardware { get; }
		private int sensorCount, subHardwareCount;

		public HardwareComponent(String name, String type, String identifier) : base(name, type)
		{
			this.Identifier = identifier;
			this.SubHardware = new List<HardwareComponent>();
			this.Sensors = new List<HardwareSensor>();
		}

		public void AddSensor(HardwareSensor sensor)
		{
			Sensors.Add(sensor);
		}

		public void AddSubHardware(HardwareComponent component)
		{
			SubHardware.Add(component);
		}

		public HardwareSensor getSensorWithType(String name, String sensorType = "null", bool typeNeeded = false)
		{
			//We need to search both name & sensor type here due to very different sensor information based on hardware
			//The identifier we are looking for can be both in Name and type
			foreach (HardwareSensor sensor in Sensors)
			{
				if (typeNeeded && sensor.Name.Contains(name) && sensor.Type.Contains(sensorType))
				{
					return sensor;
				}
				if (!typeNeeded && sensor.Name.Contains(name))
				{
					return sensor;
				}
			}
			return new HardwareSensor("null", "null", "null");
		}

		//With this override we can more easily debug hardware and hardware sensor info
		//This class is not intended to be used for UI purposes
		//To access component info use the name, type and identifier properties instead
		public override string ToString()
		{
			String combinedInfo;
			sensorCount = Sensors.Count();
			subHardwareCount = SubHardware.Count();
			combinedInfo = "HardwareComponent: " + this.Name + " - " + this.Type + " - " + this.Identifier + Environment.NewLine;
			if (sensorCount > 0)
			{
				for (int i = 0; i < sensorCount; i++)
				{
					combinedInfo += "Sensor: " + Sensors[i].Name + " - " + Sensors[i].Type + " - " + Sensors[i].Value + Environment.NewLine;
				}
			}
			if (subHardwareCount > 0)
			{
				for (int u = 0; u < subHardwareCount; u++)
				{
					combinedInfo += SubHardware[u].ToString();
				}
			}
			return combinedInfo;
		}
	}
}
