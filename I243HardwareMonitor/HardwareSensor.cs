using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I243HardwareMonitor
{
	public class HardwareSensor : Component
	{
		public String Value { get; }

		public HardwareSensor(String name, String type, String currentValue) : base(name, type)
		{
			this.Value = currentValue;
		}
	}
}
