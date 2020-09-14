using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	public class BoardStorageBox : StorageBox
	{
		public BoardStorageBox() { }

		protected override Equipment TakeOutEq(string type)
		{
			if (type.Equals("mango100"))
			{
				return new BoardMango100StorageBox();
			}

			else if (type.Equals("pc100"))
			{
				return new BoardPC100StorageBox();
			}

			else
			{
				return new Nothing();
			}
		}
	}
}