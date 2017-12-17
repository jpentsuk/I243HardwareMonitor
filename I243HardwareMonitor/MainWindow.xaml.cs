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
        //timer for saving data into database
        private DispatcherTimer timerforsavingdata;
        private List<UserControlMainView> userControls;
        private List<NotificationHandler> notificationHandlers;

        public String connectionString = ConfigurationManager.ConnectionStrings["I243HardwareMonitor.Properties.Settings.MonitoringDataConnectionString"].ConnectionString;
        public SqlConnection connection = new SqlConnection();

        private TextBox CpuNotificationSetting;

        public MainWindow()
        {
            this.hardware = new HardwareInfo();
            Debug.WriteLine(hardware.ToString());
            this.userControls = new List<UserControlMainView>();
            this.notificationHandlers = new List<NotificationHandler>();
            this.timer = new DispatcherTimer();
            this.timerforsavingdata = new DispatcherTimer();

            InitializeComponent();
            createMainUserControls();
            initNotificationHandlers();
            CpuNotificationSetting = tb_cpu_temp_warning;
            //saving user hardware data into database table Users
            SaveUserDataIntoTable();
            StartTimer();
        }

        private void initNotificationHandlers()
        {
            notificationHandlers.Add(new NotificationHandler(tb_cpu_temp_warning, HardwareType.CPU, 75));
        }

        private void updateNotificationSettings(object sender, TextChangedEventArgs args)
        {
            foreach (HardwareType type in Enum.GetValues(typeof(HardwareType)))
            {
                foreach (NotificationHandler handler in notificationHandlers)
                {
                    if (handler.type == type)
                    {
                        if (!handler.TryAndUpdateNotificationValue())
                        {
                            handler.textBox.Text = "";
                        }
                    }
                }
            }
        }
        private void createMainUserControls()
        {
            foreach (HardwareType type in Enum.GetValues(typeof(HardwareType)))
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
            HardwareType type = control.type;
            bool? isChecked = new bool?();
            switch (type)
            {
                case HardwareType.CPU:
                    isChecked = chc_cpu.IsChecked;
                    break;
                case HardwareType.GPU:
                    isChecked = chc_gpu.IsChecked;
                    break;
                case HardwareType.RAM:
                    isChecked = chc_ram.IsChecked;
                    break;
                case HardwareType.HDD:
                    isChecked = chc_hdd.IsChecked;
                    break;
            }
            if ((bool)isChecked)
            {
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

        private void btn_cleartabledata_Click(object sender, RoutedEventArgs e)
        {
            dgd_users.ItemsSource = null;
        }


        private void btn_savedata_Click(object sender, RoutedEventArgs e)
        {
            StartTimerForAddingData();
            MessageBox.Show("Saving Data Started");
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_showdata_Click(object sender, RoutedEventArgs e)
        {
            // get cpu data from CPU table into Grid2
            askcpu();
            // get Users data from Users table into Grid1
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
        private void askcpu()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            String query = "SELECT * FROM CPU";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable cpu = new DataTable("cpu");

            adapter.Fill(cpu);

            dgd_cpu.ItemsSource = cpu.DefaultView;
            adapter.Update(cpu);
        }

        private void btn_stopsavedata_Click(object sender, RoutedEventArgs e)
        {
            connection.Close();
            timerforsavingdata.Stop();
            MessageBox.Show("Saving Data Stopped");
        }

        private void SaveCPUData()
        {
            HardwareComponent CPU = hardware.CPUs[0];
            double CpuTotalLoad = double.Parse(CPU.Sensors[CPU.Sensors.Count - 1].Value);
            
            connection = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("INSERT INTO CPU (TotalClock) values( @CpuTotalLoad)", connection);
            cmd.Parameters.Add(new SqlParameter("@CpuTotalLoad", CpuTotalLoad));
            
      
            SqlDataReader datareader;
            try
            {
                connection.Open();
                datareader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveUserDataIntoTable()
        {
	        string cpuInfo, gpuInfo, hddInfo = string.Empty;
	        string ramInfo = hardware.RAM.Name;
	        foreach (HardwareComponent cpu in hardware.CPUs)
	        {
		        cpuInfo += cpu.Name;
			}
	        foreach (HardwareComponent gpu in hardware.GPUs)
	        {
		        gpuInfo += gpu.Name;
	        }
	        foreach (HardwareComponent hdd in hardware.HDDs)
	        {
		        hddInfo += hdd.Name;
	        }
	        
            connection = new SqlConnection(connectionString);

            string query = "INSERT INTO Users (CPU, GPU, HDD, RAM) values('" + cpuInfo + "','" + gpuInfo + "','" + hddInfo + "','" + ramInfo + "') ;";
            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader datareader;
            try
            {
                connection.Open();
                datareader = command.ExecuteReader();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StartTimerForAddingData()

        {
            timerforsavingdata.Interval = TimeSpan.FromSeconds(1);
            timerforsavingdata.Tick += AddDataIntoDatabase;
            timerforsavingdata.Start();
        }
        private void AddDataIntoDatabase(object sender, EventArgs e)
        {
            SaveCPUData();
        }
    }
}
