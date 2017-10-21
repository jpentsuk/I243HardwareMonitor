using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I243HardwareMonitor
{
	public class HardwareComponent : Component
	{
		private List<HardwareComponent> SubHardware { get; }
		private List<HardwareSensor> Sensors { get; }
		public HardwareComponent(String name, String type) : base(name, type)
		{
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
	}
}
