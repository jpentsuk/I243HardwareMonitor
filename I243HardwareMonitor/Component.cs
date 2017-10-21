using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I243HardwareMonitor
{
	public class Component
	{
		public String Name { get; }
		public String Type { get; }
		public Component(String name, String type)
		{
			this.Name = name;
			this.Type = type;
		}
	}
}
