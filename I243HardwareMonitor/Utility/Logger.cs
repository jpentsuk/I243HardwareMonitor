

using System;
using System.Diagnostics;
using System.IO;

namespace I243HardwareMonitor.Utility
{
	public static class Logger
	{
		public static void Write(String logText)
		{
			String currentDate = System.DateTime.Today.ToString("yy_dd_MM");
			String logLine = String.Empty;
			String fileName = "log_" + currentDate + ".txt";
			logLine += currentDate + " - ";
			logLine += logText;
			Debug.WriteLine(logLine);
			File.AppendAllText(fileName, logLine);
		}
	}
}