using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	class BoardMango100StorageBox : Equipment
	{
		public BoardMango100StorageBox()
		{
			name = "Mango100";
			type = "Test Board";
			use = "Android / WinCE TEST";
			manufacture = "���� ��ũ�����";
			price = "56,000";
			number = "1";
			dateOfPurchase = "2010/6/20";
			Standard.Add("Samsung Process");
			Standard.Add("128RAM Size");
			Standard.Add("4 Inch TFT LCD");
			Standard.Add("���� ���� ��");
		}
	}
}
