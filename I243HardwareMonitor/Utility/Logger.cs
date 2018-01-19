

using System;
using System.Diagnostics;
using System.IO;

namespace I243HardwareMonitor.Utility
{
	public static class Logger
	{
		public static void Write(String logText)
		{
			String logLine = String.Empty;
			logLine += System.DateTime.Today.ToString("yy/dd/MM") + " - ";
			logLine += logText;
			Debug.WriteLine(logLine);
			File.AppendAllText("log.txt", logLine);
		}
	}
}