using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I243HardwareMonitor
{
	public class HardwareSensor
	{
		public String Name { get; set; }

		public String Type { get; set; }

		public String Value { get; set; }

		public HardwareSensor(String name, String sensorType, String currentValue)
		{
			this.Name = name;
			this.Type = sensorType;
			this.Value = currentValue;
		}
	}
}
