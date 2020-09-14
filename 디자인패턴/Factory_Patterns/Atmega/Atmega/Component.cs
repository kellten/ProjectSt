using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
	public interface Component
	{
		EIsp CreateIsp();
		ESDCard CreateSDCard();
		EUsb CreateUsb();
		EUsart CreateUsart();
		ELcd CreateLcd();
		EVcc CreateVcc();
	}
}
