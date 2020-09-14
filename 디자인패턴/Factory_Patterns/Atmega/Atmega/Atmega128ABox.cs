using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
 	public class Atmega128ABox : Component
	{
		public Atmega128ABox() { }

		public EIsp CreateIsp()
		{
			return new Isp610();
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
			return new Lcd18();
		}

		public EVcc CreateVcc()
		{
			return new Vcc5();
		}
	}
}