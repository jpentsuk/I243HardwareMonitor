﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using I243HardwareMonitor.Enums;
using I243HardwareMonitor.Hardware;
using I243HardwareMonitor.Utility;
using HardwareType = I243HardwareMonitor.Enums.HardwareType;

namespace I243HardwareMonitor.Views
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

        public MainWindow()
        {
			Logger.Write("Initialize hardwareInfo");
            this.hardware = new HardwareInfo();
	        Logger.Write("Dump hardware info into logs");
	        Logger.Write(hardware.ToString());
			Debug.WriteLine(hardware.ToString());
            this.userControls = new List<UserControlMainView>();
            this.notificationHandlers = new List<NotificationHandler>();
            this.timer = new DispatcherTimer();
            this.timerforsavingdata = new DispatcherTimer();
	        Logger.Write("Timers started");

			InitializeComponent();
	        Logger.Write("Generate general view");
			createMainUserControls();
            initNotificationHandlers();
            //saving user hardware data into database table Users
            SaveUserDataIntoTable();
            StartTimer();
        }

        private void initNotificationHandlers()
        {
            notificationHandlers.Add(new NotificationHandler(tb_cpu_temp_warning, HardwareType.GPU, 20));
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
	            if (type != HardwareType.CPU && type != HardwareType.GPU)
	            {
		            UserControlMainView control = new UserControlMainView(hardware, type);
		            userControls.Add(control);
		            mainViewStackPanel.Children.Add(control);
	            }
	            else if (type == HardwareType.CPU)
	            {
		            foreach (SenType sensorType in Enum.GetValues(typeof(SenType)))
		            {
			            if (sensorType != SenType.None)
			            {
				            bool sensorValueExists = false;
				            foreach (HardwareComponent cpu in hardware.CPUs)
				            {
					            HardwareSensor sensor = cpu.getSensorWithType(sensorType.ToString());
					            sensorValueExists = (sensor.Value != "null");
				            }
				            if (sensorValueExists)
				            {
								UserControlMainView control = new UserControlMainView(hardware, type, sensorType);
					            userControls.Add(control);
					            mainViewStackPanel.Children.Add(control);
							}
			            }
		            }
				}
	            else if (type == HardwareType.GPU)
	            {
					foreach (SenType sensorType in Enum.GetValues(typeof(SenType)))
					{
						if (sensorType != SenType.None)
						{
							bool sensorValueExists = false;
							foreach (HardwareComponent gpu in hardware.GPUs)
							{
								HardwareSensor sensor = gpu.getSensorWithType("GPU", sensorType.ToString(), true);
								Debug.WriteLine("GPU" + sensorType.ToString());
								sensorValueExists = (sensor.Value != "null");
							}
							if (sensorValueExists)
							{
								UserControlMainView control = new UserControlMainView(hardware, type, sensorType);
								userControls.Add(control);
								mainViewStackPanel.Children.Add(control);
							}
						}
					}
				}
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
	        Logger.Write("User toggled a setting");
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

        private void btn_showdata_Click(object sender, RoutedEventArgs e)
        {
            // get cpu data from CPU table into Grid2
            askcpu();
            askgpu();
            askram();
            askhdd();
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

        private void SaveGPUData()
        {
            HardwareComponent GPU = hardware.GPUs[0];
            double GpuTotalLoad = double.Parse(GPU.Sensors[GPU.Sensors.Count - 1].Value);

            connection = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("INSERT INTO GPU (TotalClock) values( @GpuTotalLoad)", connection);
            cmd.Parameters.Add(new SqlParameter("@GpuTotalLoad", GpuTotalLoad));

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

        private void askgpu()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            String query = "SELECT * FROM GPU";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable gpu = new DataTable("gpu");

            adapter.Fill(gpu);

            dgd_gpu.ItemsSource = gpu.DefaultView;
            adapter.Update(gpu);
        }

        private void askram()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            String query = "SELECT * FROM RAM";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable ram = new DataTable("ram");

            adapter.Fill(ram);

            dgd_ram.ItemsSource = ram.DefaultView;
            adapter.Update(ram);
        }

        private void SaveRAMData()
        {
            HardwareSensor ramLoadSensor = hardware.RAM.getSensorWithType("Memory", "Load", true);
            double RamInUse = double.Parse(ramLoadSensor.Value);

            connection = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("INSERT INTO RAM (InUse) values( @RamInUse)", connection);
            cmd.Parameters.Add(new SqlParameter("@RamInUse", RamInUse));

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

        private void SaveHDDData()
        {
            HardwareSensor mainSensor = hardware.HDDs[0].getSensorWithType("Used", "Load", true);
            double Load = double.Parse(mainSensor.Value);

            connection = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("INSERT INTO HDD (Load) values( @Load)", connection);
            cmd.Parameters.Add(new SqlParameter("@Load", Load));

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

        private void askhdd()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            String query = "SELECT * FROM HDD";
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable hdd = new DataTable("hdd");

            adapter.Fill(hdd);

            dgd_hdd.ItemsSource = hdd.DefaultView;
            adapter.Update(hdd);
        }


        private void SaveUserDataIntoTable()
        {
	        string cpuInfo = string.Empty;
			string gpuInfo = string.Empty;
			string hddInfo = string.Empty;
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
            //SaveGPUData();
            SaveRAMData();
            SaveHDDData();
        }
    }
}
