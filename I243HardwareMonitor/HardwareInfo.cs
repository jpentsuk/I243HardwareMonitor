using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I243HardwareMonitor
{
	public class HardwareInfo
	{
		private List<HardwareComponent> components;
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
			components = computerhardware.GetUpdatedHardwareComponents();
			MapComponentToIdentifier(components);
		}

		public void Update()
		{
			CPUs = new List<HardwareComponent>();
			GPUs = new List<HardwareComponent>();
			FanControllers = new List<HardwareComponent>();
			HDDs = new List<HardwareComponent>();
			components = computerhardware.GetUpdatedHardwareComponents();
			MapComponentToIdentifier(components);
		}

		public List<HardwareComponent> GetAllComponents()
		{
			return components;
		}

		public override string ToString()
		{
			String fullString = "";
			for (int i = 0; i < components.Count; i++)
			{
				fullString += components[i].ToString();
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
