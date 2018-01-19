using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using I243HardwareMonitor.Enums;

namespace I243HardwareMonitor
{
	class NotificationHandler
	{
		public HardwareType type { get; }
		public TextBox textBox { get; }
		private int currentValue;

		public NotificationHandler(TextBox textbox, HardwareType type, int defaultValue)
		{
			textBox = textbox;
			this.type = type;
			currentValue = defaultValue;
		}

		public bool TryAndUpdateNotificationValue()
		{
			string value = textBox.Text;
			int result;
			bool wasValueChangeSuccessful = int.TryParse(value, out result);
			currentValue = result;
			createToast();
			Debug.WriteLine("Notification value: " + currentValue);
			return wasValueChangeSuccessful;
		}

		private void createToast()
		{
		/*	// https://msdn.microsoft.com/en-us/library/windows/desktop/hh802768(v=vs.85).aspx
			// Get a toast XML template
			XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

			// Fill in the text elements
			XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
			for (int i = 0; i < stringElements.Length; i++)
			{
				stringElements[i].AppendChild(toastXml.CreateTextNode("Line " + i));
			}

			// Specify the absolute path to an image
			String imagePath = "file:///" + Path.GetFullPath("toastImageAndText.png");
			XmlNodeList imageElements = toastXml.GetElementsByTagName("image");

			ToastNotification toast = new ToastNotification(toastXml);
		*/
		}
	}
}
