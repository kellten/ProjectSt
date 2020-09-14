using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace PropertyGrid3
{
    class PropertyGridSimpleDemoClass3
    {
        bool m_DrinkOrNot;

        [DisplayName("Drink or not")]
        [Description("Drink or not")]
        [Category("Make right decision")]
        [TypeConverter(typeof(DrinkerClassConverter))]
        public bool DrinkOrNot
        {
            get { return m_DrinkOrNot; }
            set { m_DrinkOrNot = value; }
        }

    }
}
