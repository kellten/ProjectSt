using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
 	public class Atmega1284PBox : Component
	{
		public Atmega1284PBox() { }

		public EIsp CreateIsp()
		{
			return new Isp10();
		}

		public ESDCard CreateSDCard()
		{
			return new SDCardN();
		}

		public EUsb CreateUsb()
		{
			return new UsbY();
		}

		public EUsart CreateUsart()
		{
			return new Usart6();
		}

		public ELcd CreateLcd()
		{
			return new Lcd8();
		}

		public EVcc CreateVcc()
		{
			return new Vcc5();
		}
	}
}