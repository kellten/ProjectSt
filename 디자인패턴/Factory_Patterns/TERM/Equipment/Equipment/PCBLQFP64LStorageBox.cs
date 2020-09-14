using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	class PCBLQFP64LStorageBox : Equipment
	{
		public PCBLQFP64LStorageBox()
		{
			name = "BLQFP64L";
			type = "Decal";
			use = "�а� �ǽ���";
			manufacture = "BLQ";
			price = "52,000";
			number = "20";
			dateOfPurchase = "2007/06/10";
			Standard.Add("3V/5V");
			Standard.Add("2.54mm pitch 10 pin single Header PIN");
		}
	}
}