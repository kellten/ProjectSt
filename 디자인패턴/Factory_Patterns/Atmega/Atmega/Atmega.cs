using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
	public abstract class Atmega
	{
		private string name;
		protected EIsp isp;
		protected ESDCard sdcard;
		protected EUsb usb;
		protected EUsart usart;
		protected ELcd lcd;
		protected EVcc vcc;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public Atmega() { }

		public abstract string Prepare();
	}
}
