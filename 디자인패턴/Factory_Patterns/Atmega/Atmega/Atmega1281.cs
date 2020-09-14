using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
	public class Atmega1281 : Atmega
	{
		Component component;

		public Atmega1281(Component component)
		{
			this.component = component;
		}

		public override string Prepare()
		{
			isp = component.CreateIsp();
			sdcard = component.CreateSDCard();
			usb = component.CreateUsb();
			usart = component.CreateUsart();
			lcd = component.CreateLcd();
			vcc = component.CreateVcc();
			
			StringBuilder sb = new StringBuilder();
			sb.Append(Name + " 기자재 조회 결과" + "\n");
			sb.Append(isp.toString() + "\n");
			sb.Append(sdcard.toString() + "\n");
			sb.Append(usb.toString() + "\n");
			sb.Append(usart.toString() + "\n");
			sb.Append(lcd.toString() + "\n");
			sb.Append(vcc.toString() + "\n");

			return sb.ToString();
		}
	}
}
