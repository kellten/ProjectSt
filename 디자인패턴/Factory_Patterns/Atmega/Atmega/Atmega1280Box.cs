using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
 	public class Atmega1280Box : Component
	{
		public Atmega1280Box() { }

		public EIsp CreateIsp()
		{
			return new Isp610();
		}

		public ESDCard CreateSDCard()
		{
			return new SDCardN();
		}

		public EUsb CreateUsb()
		{
			return new UsbN();
		}

		public EUsart CreateUsart()
		{
			return new Usart6();
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