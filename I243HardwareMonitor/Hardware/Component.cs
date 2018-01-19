using System;

/* 
 * Base class for all mapped hardware (and sub-hardware) components and hardware sensors
*/

namespace I243HardwareMonitor.Hardware
{
	public abstract class Component
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
