using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	public class PCBAtmega128StorageBox : Equipment
	{
		public PCBAtmega128StorageBox()
		{
			name = "ezMCU AT128A";
			type = "ATMEGA128";
			use = "LCD 제어용";
			manufacture = "이지써킷";
			number = "25";
			price = "33,000";
			dateOfPurchase = "2009/07/22";
			Standard.Add("3V/5V");
			Standard.Add("2.4inch");
			Standard.Add("320 x 240");
			Standard.Add("QVGA TFT LCD(16bit interface");
			Standard.Add("2.54mm pitch 25 pin single Header PIN");
		}
	}
}
