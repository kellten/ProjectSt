using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	public abstract class StorageBox
	{
		public StorageBox() { }

		public Equipment inquiryEquipment(string type)
		{
			Equipment eq;

			eq = TakeOutEq(type);
			eq.prepare();
			return eq;
		}

		protected abstract Equipment TakeOutEq(string type);
	}
}
