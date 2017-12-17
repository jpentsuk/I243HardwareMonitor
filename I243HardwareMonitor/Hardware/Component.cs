using System;

namespace I243HardwareMonitor.Hardware
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
