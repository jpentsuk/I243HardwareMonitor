using System;
using System.Collections.Generic;
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
		private HardwareInfo hardware;
		public ViewType type { get; }
		private List<HardwareComponent> components;
		public UserControlMainView(HardwareInfo hardware, ViewType type)
		{
			this.components = new List<HardwareComponent>();
			this.type = type;
			this.hardware = hardware;
			setComponent();
			InitializeComponent();
			componentName.Text = type + ": " + components[0].Name;
		}

		public void setComponent()
		{
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
			foreach (HardwareComponent component in components)
			{
				labelInfo += component + Environment.NewLine;
			}
			lbl_main_view_info.Content = labelInfo;
		}
	}
}
