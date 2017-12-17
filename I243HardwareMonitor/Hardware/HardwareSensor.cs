using System;

namespace I243HardwareMonitor.Hardware
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
