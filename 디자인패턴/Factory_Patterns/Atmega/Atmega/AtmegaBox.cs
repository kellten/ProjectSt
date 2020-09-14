using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
	public abstract class AtmegaBox
	{
		public AtmegaBox() { }

		public Atmega inquiryAtmega(string type)
		{
			Atmega atmega;

			atmega = TakeOutAtmega(type);

			atmega.Prepare();

			return atmega;
		}

		protected abstract Atmega TakeOutAtmega(string type);
	}
}
