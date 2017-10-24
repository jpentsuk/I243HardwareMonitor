using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I243HardwareMonitor
{
	public class HardwareInfo
	{
		public List<HardwareComponent> Components { set; get; }
		private ComputerHardware computerhardware;
		public List<HardwareComponent> CPUs, GPUs, FanControllers, HDDs;
		public HardwareComponent Motherboard { set; get; }
		public HardwareComponent RAM { set; get; }
		public HardwareInfo()
		{
			computerhardware = new ComputerHardware();
			CPUs = new List<HardwareComponent>();
			GPUs = new List<HardwareComponent>();
			FanControllers = new List<HardwareComponent>();
			HDDs = new List<HardwareComponent>();
			Components = computerhardware.GetUpdatedHardwareComponents();
			MapComponentToIdentifier(Components);
		}

		public void Update()
		{
			CPUs = new List<HardwareComponent>();
			GPUs = new List<HardwareComponent>();
			FanControllers = new List<HardwareComponent>();
			HDDs = new List<HardwareComponent>();
			Components = computerhardware.GetUpdatedHardwareComponents();
			MapComponentToIdentifier(Components);
		}

		public override string ToString()
		{
			String fullString = "";
			for (int i = 0; i < Components.Count; i++)
			{
				fullString += Components[i].ToString();
			}

			return fullString;
		}

		private void MapComponentToIdentifier(List<HardwareComponent> components)
		{
			for (int i = 0; i < components.Count(); i++)
			{
				String identifier = components[i].Identifier;
				if (identifier.Contains("mainboard"))
				{
					Motherboard = components[i];
				}
				else if (identifier.Contains("lpc"))
				{
					FanControllers.Add(components[i]);
				}
				else if (identifier.Contains("cpu"))
				{
					this.CPUs.Add(components[i]);
				}
				else if (identifier.Contains("ram"))
				{
					RAM = components[i];
				}
				else if (identifier.Contains("gpu"))
				{
					GPUs.Add(components[i]);
				}
				else if (identifier.Contains("hdd"))
				{
					HDDs.Add(components[i]);
				}
			}
		}
	}
}
