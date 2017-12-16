﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
			Debug.WriteLine("Notification value: " + currentValue);
		    return wasValueChangeSuccessful;
	    }
    }
}
