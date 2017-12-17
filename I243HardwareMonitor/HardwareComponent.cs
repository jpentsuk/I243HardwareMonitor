using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I243HardwareMonitor
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

		public HardwareSensor getSensorWithType(String type)
		{
				foreach (HardwareSensor sensor in Sensors)
				{
					if (sensor.Type.Contains(type))
					{
						return sensor;
					}
					if (sensor.Name.Contains(type))
					{
						return sensor;
					}
				}
			return new HardwareSensor("null", "null", "null");
		}

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
