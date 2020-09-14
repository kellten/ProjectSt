using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	public class CableRGBStrogeBox : Equipment
	{
		public CableRGBStrogeBox()
		{
			name = "Monitor Cable";
			type = "RGB";
			use = "모니터 사용";
			manufacture = "MS";
			price = "5,000";
			number = "4";
			dateOfPurchase = "2003/11/9";
			Standard.Add("1M");
		}
	}
}