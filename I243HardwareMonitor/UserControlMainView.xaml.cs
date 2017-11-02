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
		private ViewType type;
		public UserControlMainView(HardwareInfo hardware, ViewType type)
		{
			this.type = type;
			this.hardware = hardware;
			InitializeComponent();
		}
		public void UpdateLabelInfo()
		{
			String labelInfo = String.Empty;
			switch (type)
			{
				case ViewType.CPU:
					foreach (HardwareComponent cpu in hardware.CPUs)
					{
						labelInfo += cpu + Environment.NewLine;
					}
					break;
				case ViewType.GPU:
					foreach (HardwareComponent gpu in hardware.GPUs)
					{
						labelInfo += gpu + Environment.NewLine;
					}
					break;
				case ViewType.HDD:
					foreach (HardwareComponent hdd in hardware.HDDs)
					{
						labelInfo += hdd + Environment.NewLine;
					};
					break;
				case ViewType.RAM:
					labelInfo = hardware.RAM.ToString();
					break;
			}
			lbl_main_view_info.Content = labelInfo;
		}
	}
}
