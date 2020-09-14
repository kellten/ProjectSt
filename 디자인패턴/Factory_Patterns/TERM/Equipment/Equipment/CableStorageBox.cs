using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	public class CableStorageBox : StorageBox
	{
		public CableStorageBox() { }

		protected override Equipment TakeOutEq(string type)
		{
			if (type.Equals("rgb"))
			{
				return new CableRGBStrogeBox();
			}

			else if (type.Equals("dvi"))
			{
				return new CableDVIStorageBox();
			}

			else
			{
				return new Nothing();
			}
		}
	}
}