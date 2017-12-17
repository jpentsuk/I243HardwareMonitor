using System;
using System.Collections.Generic;
using System.Linq;

namespace I243HardwareMonitor.Hardware
{
	public class HardwareInfo
	{
		private List<HardwareComponent> components { set; get; }
		public List<HardwareComponent> CPUs, GPUs, FanControllers, HDDs;
		public HardwareComponent Motherboard { set; get; }
		public HardwareComponent RAM { set; get; }
		private ComputerHardware computerhardware;
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
		// GetAllComponents returns the full list of Hardware Components currently stored in the Hardware Info object
		// This method can be used to retrieve all objects if caller does not care for specific type of components in the computer
		public List<HardwareComponent> GetAllComponents()
		{
			return components;
		}

		// Update method re-initializes all known Hardware components from this instance of Hardware Info and then requests new ones
		// Then the new Hardware components are stored in the public variables (GPUs, CPUs, FanControllers, HDDs, Motherboard, RAM) so they can be accessed separately by type
		// This method can be used to update all info stored in the Hardware Info object before using it or writing it out
		public void Update()
		{
			// These Hardware Component lists need to be re-initialized to avoid duplicates of same component when updating info
			CPUs = new List<HardwareComponent>();
			GPUs = new List<HardwareComponent>();
			FanControllers = new List<HardwareComponent>();
			HDDs = new List<HardwareComponent>();

			// Then we get a new list of all Hardware components and map them to be publicly accessible
			components = computerhardware.GetUpdatedHardwareComponents();
			MapComponentToIdentifier(components);
		}

		// ToString method recursively returns the name, type and value of every Hardware Component, its Hardware Sensors and sub Hardware components (and Sensors, if any) as a combined String
		// Every name, type and value ends with a newline
		// This method is intended to be used for debugging
		public override string ToString()
		{
			String fullString = "";
			for (int i = 0; i < components.Count; i++)
			{
				fullString += components[i].ToString();
			}

			return fullString;
		}

		// MapComponentsToIdentifier method finds and maps the correct Hardware components to the correct public variable based on Hardware Component public identifier
		// This method is used to make Hardware Info public variables populated so we can access specific Hardware Component (or their sensors) info
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
