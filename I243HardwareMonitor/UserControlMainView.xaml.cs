using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace I243HardwareMonitor
{
	/// <summary>
	/// Interaction logic for UserControlMainView.xaml
	/// </summary>
	public partial class UserControlMainView : UserControl
	{
		public ViewType type { get; }
		private HardwareInfo hardware;
		private List<HardwareComponent> components;
		private List<ProgressBar> progressBars;
		public UserControlMainView(HardwareInfo hardware, ViewType type)
		{
			this.components = new List<HardwareComponent>();
			this.progressBars = new List<ProgressBar>();
			this.type = type;
			this.hardware = hardware;
			setComponent();
			InitializeComponent();
			foreach (HardwareComponent component in components)
			{
				componentName.Text = type + ": " + component.Name;
			}
			ProgressBar loadProgressBar = new ProgressBar();
			loadProgressBar.Margin = new Thickness(10, 0, 10, 5);
			loadProgressBar.Visibility = Visibility.Visible;
			loadProgressBar.Height = 25;
			loadProgressBar.Foreground = System.Windows.Media.Brushes.Blue;
			loadProgressBar.Background = System.Windows.Media.Brushes.White;
			loadProgressBar.Maximum = 110;
			loadProgressBar.Value = 0;
			containerPanel.Children.Add(loadProgressBar);
			progressBars.Add(loadProgressBar);
			Debug.WriteLine(components[0].ToString());
			if (type == ViewType.RAM)
			{	
				loadProgressBar.Maximum = Double.Parse(getSensorWithType("Available").Value);
			} else if (type == ViewType.HDD)
			{
				loadProgressBar.Visibility = Visibility.Hidden;
			}
			
		}

		public void setComponent()
		{
			components = new List<HardwareComponent>();
			switch (type)
			{
				case ViewType.CPU:
					foreach (HardwareComponent cpu in hardware.CPUs)
					{
						components.Add(cpu);
					}
					break;
				case ViewType.GPU:
					foreach (HardwareComponent gpu in hardware.GPUs)
					{
						components.Add(gpu);
					}
					break;
				case ViewType.HDD:
					foreach (HardwareComponent hdd in hardware.HDDs)
					{
						components.Add(hdd);
					};
					break;
				case ViewType.RAM:
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
					case ViewType.CPU:
						if (components.Count > 0)
						{
							progressBars[0].Value = double.Parse(component.Sensors[component.Sensors.Count - 1].Value);
							labelInfo = "Total " + component.Sensors[component.Sensors.Count - 1].Type + ": " +
							            component.Sensors[component.Sensors.Count - 1].Value;
						}
						break;
					case ViewType.GPU:
						if (components.Count > 0)
						{
							mainSensor = getSensorWithType("Temperature");
							labelInfo = mainSensor.Type + ": " + mainSensor.Value;
							progressBars[0].Value = double.Parse(mainSensor.Value);
						}
						break;
					case ViewType.HDD:
						if (components.Count > 0)
						{
							mainSensor = getSensorWithType("Used");
							labelInfo = mainSensor.Type + ": " + mainSensor.Value;
							progressBars[0].Value = double.Parse(mainSensor.Value);
						}
				break;
					case ViewType.RAM:
						if (components.Count > 0)
						{
							mainSensor = getSensorWithType("Used");
							labelInfo = "In use : " + mainSensor.Value;
							progressBars[0].Value = double.Parse(mainSensor.Value);
						}
						break;
				}
			}
			lbl_main_view_info.Content = labelInfo;
		}

		private HardwareSensor getSensorWithType(String type)
		{		
			foreach (HardwareComponent comp in components)
			{
				foreach (HardwareSensor sensor in comp.Sensors)
				{
					if (sensor.Type.Contains(type))
					{
						return sensor;
					}
					if (sensor.Name.Contains(type))
					{
						return sensor;
					}
				}
			}
			return new HardwareSensor("null", "null", "null");
		}
	}
}
