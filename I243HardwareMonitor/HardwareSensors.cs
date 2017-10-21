using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I243HardwareMonitor
{
    public class HardwareSensor
    {
		public String Name
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}
		
		public String SensorType
		{
			get
			{
				return this.SensorType;
			}
			set
			{
				this.SensorType = value;
			}
		}

		public int CurrentValue
		{
			get
			{
				return this.CurrentValue;
			}
			set
			{
				this.CurrentValue = value;
			}
		}

        public HardwareSensor(String name, String sensorType, int currentValue)
        {
			this.Name = name;
			this.SensorType = sensorType;
			this.CurrentValue = currentValue;
        }
}
