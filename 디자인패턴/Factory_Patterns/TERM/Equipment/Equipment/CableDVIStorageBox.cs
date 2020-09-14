using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	class CableDVIStorageBox : Equipment
	{
		public CableDVIStorageBox()
		{
			name = "Monitor Cable";
			type = "DVI";
			use = "모니터 사용";
			manufacture = "Apple";
			price = "12,000";
			number = "2";
			dateOfPurchase = "2010/3/12";
			Standard.Add("1M");
		}
	}
}