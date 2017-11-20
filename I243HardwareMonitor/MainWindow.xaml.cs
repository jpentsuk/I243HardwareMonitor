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
using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.Collections;
using System.Diagnostics;
using System.Windows.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace I243HardwareMonitor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private HardwareInfo hardware;
		private DispatcherTimer timer;
		private List<UserControlMainView> userControls;

        public String connectionString;
        public SqlConnection connection = new SqlConnection();

		public MainWindow()
		{
            connectionString = ConfigurationManager.ConnectionStrings["I243HardwareMonitor.Properties.Settings.MonitoringDataConnectionString"].ConnectionString;
			this.hardware = new HardwareInfo();
			Debug.WriteLine(hardware.ToString());
			this.userControls = new List<UserControlMainView>();
			this.timer = new DispatcherTimer();
			InitializeComponent();
			createMainUserControls();
			StartTimer();
		}

		private void createMainUserControls()
		{
			foreach (ViewType type in Enum.GetValues(typeof(ViewType)))
			{
				UserControlMainView control = new UserControlMainView(hardware, type);
				userControls.Add(control);
				mainViewStackPanel.Children.Add(control);
			}
		}

		private void StartTimer()
		{
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += UpdateInfoOnMainViewComponents;
			timer.Start();
		}

		private void UpdateInfoOnMainViewComponents(object sender, EventArgs e)
		{
			hardware.Update();
			foreach (UserControlMainView control in userControls)
			{
				control.UpdateLabelInfo();
			}
		}

		private void chc_viewToggle_Changed(object sender, RoutedEventArgs e)
		{
			foreach (UserControlMainView control in userControls)
			{
				updateMainViewChildVisibility(control);
			}
		}

		private void updateMainViewChildVisibility(UserControlMainView control)
		{
			mainViewStackPanel.Children.Remove(control);
			ViewType type = control.type;
			bool? isChecked = new bool?();
			switch (type)
			{
				case ViewType.CPU:
					isChecked = chc_cpu.IsChecked;
					break;
				case ViewType.GPU:
					isChecked = chc_gpu.IsChecked;
					break;
				case ViewType.RAM:
					isChecked = chc_ram.IsChecked;
					break;
				case ViewType.HDD:
					isChecked = chc_hdd.IsChecked;
					break;
			}
			if ((bool)isChecked) { 
				mainViewStackPanel.Children.Add(control);
			}
			else
			{
				mainViewStackPanel.Children.Remove(control);
			}
		}

		private void btn_Help_Click(object sender, RoutedEventArgs e)
		{
			var helpwindow = new Help();
			helpwindow.Show();
		}

        private void btn_showusers_Click(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            String query = "SELECT * FROM Users";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable users = new DataTable("Users");

            adapter.Fill(users);

            dgd_users.ItemsSource = users.DefaultView;
            adapter.Update(users);
        }
    }
}
