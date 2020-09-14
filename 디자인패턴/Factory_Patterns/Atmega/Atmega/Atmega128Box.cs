using System;
using System.Collections.Generic;
using System.Text;

namespace Atmega
{
	public class Atmega128Box : AtmegaBox
	{
		public Atmega128Box() { }

		protected override Atmega TakeOutAtmega(string type)
		{
			Atmega atmega = null;
			Component component = null;

			switch (type)
			{
				case "0" :
					component = new Atmega1280Box();
					atmega = new Atmega1280(component);
					atmega.Name = "ATMEGA1280";
					break;
				case "1":
					component = new Atmega1281Box();
					atmega = new Atmega1281(component);
					atmega.Name = "ATMEGA1281";
					break;
				case "4":
					component = new Atmega1284Box();
					atmega = new Atmega1284(component);
					atmega.Name = "ATMEGA1284";
					break;
				case "4P":
					component = new Atmega1284PBox();
					atmega = new Atmega1284P(component);
					atmega.Name = "ATMEGA1284P";
					break;
				case "A":
					component = new Atmega128ABox();
					atmega = new Atmega128A(component);
					atmega.Name = "ATMEGA128A";
					break;
			}

			return atmega;
		}
	}
}
