using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	class BoardPC100StorageBox : Equipment
	{
		public BoardPC100StorageBox()
		{
			name = "PC100";
			type = "Test Board";
			use = "Atmega128 TEST Board";
			manufacture = "Chungpa System";
			price = "1,800,000";
			number = "20";
			dateOfPurchase = "2001/12/30";
			Standard.Add("ATMEGA Process");
			Standard.Add("각종 센서 등");
		}
	}
}