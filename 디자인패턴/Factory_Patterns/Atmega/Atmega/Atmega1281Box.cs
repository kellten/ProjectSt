using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
 	public class Atmega1281Box : Component
	{
		public Atmega1281Box() { }

		public EIsp CreateIsp()
		{
			return new Isp10();
		}

		public ESDCard CreateSDCard()
		{
			return new SDCardY();
		}

		public EUsb CreateUsb()
		{
			return new UsbN();
		}

		public EUsart CreateUsart()
		{
			return new Usart10();
		}

		public ELcd CreateLcd()
		{
			return new LcdN();
		}

		public EVcc CreateVcc()
		{
			return new Vcc3();
		}
	}
}