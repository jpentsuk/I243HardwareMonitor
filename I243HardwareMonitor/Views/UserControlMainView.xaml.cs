﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using I243HardwareMonitor.Enums;
using I243HardwareMonitor.Hardware;

namespace I243HardwareMonitor.Views
{
	/// <summary>
	/// Interaction logic for UserControlMainView.xaml
	/// </summary>
	public partial class UserControlMainView : UserControl
	{
		public HardwareType type { get; }
		private HardwareInfo hardware;
		private List<HardwareComponent> components;
		private List<ProgressBar> progressBars;
		private SenType targetSensorType;

		public UserControlMainView(HardwareInfo hardware, HardwareType type, SenType sensorType = SenType.None)
		{
			this.components = new List<HardwareComponent>();
			this.progressBars = new List<ProgressBar>();
			this.type = type;
			this.hardware = hardware;
			this.targetSensorType = sensorType;
			setComponent();
			InitializeComponent();
			foreach (HardwareComponent component in components)
			{
				componentName.Text = type + ": " + component.Name;
			}
			ProgressBar loadProgressBar = initProgressBar();
			containerPanel.Children.Add(loadProgressBar);
			progressBars.Add(loadProgressBar);
			if (type == HardwareType.RAM || type == HardwareType.HDD)
			{
				loadProgressBar.Maximum = 100;
			}
		}

		public void setComponent()
		{
			components = new List<HardwareComponent>();
			switch (type)
			{
				case HardwareType.CPU:
					foreach (HardwareComponent cpu in hardware.CPUs)
					{
						components.Add(cpu);
					}
					break;
				case HardwareType.GPU:
					foreach (HardwareComponent gpu in hardware.GPUs)
					{
						components.Add(gpu);
					}
					break;
				case HardwareType.HDD:
					foreach (HardwareComponent hdd in hardware.HDDs)
					{
						components.Add(hdd);
					};
					break;
				case HardwareType.RAM:
					components.Add(hardware.RAM);
					break;
			}
		}

		public void UpdateLabelInfo()
		{
			String labelInfo = String.Empty;
			setComponent();
			foreach (HardwareComponent component in components)
			{
				HardwareSensor mainSensor;
				switch (type)
				{
					case HardwareType.CPU:
						if (components.Count > 0)
						{
							if (targetSensorType != SenType.None)
							{
								switch (targetSensorType)
								{
									case SenType.Temp:
										mainSensor = component.getSensorWithType("CPU", "Temp", true);
										progressBars[0].Value = double.Parse(mainSensor.Value);
										labelInfo = "Temperature: " + string.Format("{0:0.00}", Double.Parse(mainSensor.Value)) + "C";
										break;
									case SenType.Total:
										mainSensor = component.getSensorWithType("Total", "Load", true);
										progressBars[0].Value = double.Parse(mainSensor.Value);
										labelInfo = "Total " + mainSensor.Type + ": " + string.Format("{0:0.00}", Double.Parse(mainSensor.Value)) + "%";
										break;
								}
							}
						}
						break;
					case HardwareType.GPU:
						if (components.Count > 0)
						{
							if (targetSensorType != SenType.None)
							{
								switch (targetSensorType)
								{
									case SenType.Temp:
										mainSensor = component.getSensorWithType("Core", "Temp", true);
										labelInfo = mainSensor.Type + ": " + mainSensor.Value;
										progressBars[0].Value = double.Parse(mainSensor.Value);
										break;
									case SenType.Load:
										mainSensor = component.getSensorWithType("Memory", "Load", true);
										labelInfo = mainSensor.Type + ": " + mainSensor.Value;
										progressBars[0].Value = double.Parse(mainSensor.Value);
										break;
								}
							}

                        }
                        break;
					case HardwareType.HDD:
						if (components.Count > 0)
						{
							mainSensor = component.getSensorWithType("Used", "Load", true);
							labelInfo = "Used space: " + string.Format("{0:0.00}", Double.Parse(mainSensor.Value)) + "%";
							progressBars[0].Value = double.Parse(mainSensor.Value);
						}
						break;
					case HardwareType.RAM:
						if (components.Count > 0)
						{
							mainSensor = component.getSensorWithType("Used", "Data", true);
							HardwareSensor loadSensor = component.getSensorWithType("Memory", "Load", true);
							String availableMemory =
								string.Format("{0:0.0}", double.Parse(mainSensor.Value) / double.Parse(loadSensor.Value) * 100);
							labelInfo = "In use : " + string.Format("{0:0.00}", Double.Parse(mainSensor.Value)) + "GB/" + availableMemory +
								        "GB (" + string.Format("{0:0.00}", Double.Parse(loadSensor.Value)) + "%)";
							progressBars[0].Value = double.Parse(loadSensor.Value);	
						}
						break;
				}
			}
			lbl_main_view_info.Content = labelInfo;
			MatchProgressBarColorToLoad();
		}

		private ProgressBar initProgressBar()
		{
			ProgressBar progressBar = new ProgressBar();
			progressBar.Margin = new Thickness(10, 0, 10, 5);
			progressBar.Visibility = Visibility.Visible;
			progressBar.Height = 25;
			progressBar.Foreground = System.Windows.Media.Brushes.LawnGreen;
			progressBar.Background = System.Windows.Media.Brushes.White;
			progressBar.Maximum = 110;
			progressBar.Value = 0;
			return progressBar;
		}

		private void MatchProgressBarColorToLoad()
		{
			foreach (ProgressBar progressBar in progressBars)
			{
				double currentValueRatio = progressBar.Value / progressBar.Maximum;
				if (currentValueRatio < 0.4)
				{
					progressBar.Foreground = System.Windows.Media.Brushes.LawnGreen;
				} else if (currentValueRatio < 0.55)
				{
					progressBar.Foreground = System.Windows.Media.Brushes.Yellow;
				} else if (currentValueRatio < 0.75)
				{
					progressBar.Foreground = System.Windows.Media.Brushes.Orange;
				} else
				{
					progressBar.Foreground = System.Windows.Media.Brushes.Red;
				}
				
			}
		}
	}
}
