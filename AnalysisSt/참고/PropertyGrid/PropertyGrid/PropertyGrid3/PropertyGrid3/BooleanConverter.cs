using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace PropertyGrid3
{
    class DrinkerClassConverter : BooleanConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destType)
        {
            return (bool)value ?
              "Yes" : "Yes, of course";
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
          CultureInfo culture,
          object value)
        {
            return (string)value == "Yes";
        }

    }
}
