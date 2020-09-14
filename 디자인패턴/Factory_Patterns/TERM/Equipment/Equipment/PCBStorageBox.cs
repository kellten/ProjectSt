using System;
using System.Collections.Generic;
using System.Text;

namespace Equipment
{
	public class PCBStorageBox : StorageBox
	{
		public PCBStorageBox() { }

		protected override Equipment TakeOutEq(string type)
		{
			if (type.Equals("atmega128"))
			{
				return new PCBAtmega128StorageBox();
			}

			else if (type.Equals("lqfp64l"))
			{
				return new PCBLQFP64LStorageBox();
			}

			else
			{
				return new Nothing();
			}
		}
	}
}
