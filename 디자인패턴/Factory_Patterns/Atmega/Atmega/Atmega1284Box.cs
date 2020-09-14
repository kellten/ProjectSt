using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
 	public class Atmega1284Box : Component
	{
		public Atmega1284Box() { }

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
			return new UsbY();
		}

		public EUsart CreateUsart()
		{
			return new Usart610();
		}

		public ELcd CreateLcd()
		{
			return new Lcd8();
		}

		public EVcc CreateVcc()
		{
			return new Vcc3();
		}
	}
}