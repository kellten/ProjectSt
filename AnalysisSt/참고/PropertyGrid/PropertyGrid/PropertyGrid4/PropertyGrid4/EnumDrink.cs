using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;

namespace PropertyGrid4
{
    enum DrinkDoses
    { 
        [Description("Half of litre")]
        litre,
        [Description("One litre")]
        oneLitre,
        [Description("Two litres")]
        twoLitre,
        [Description("Three litres")]
        threeLitres,
        [Description("Four litres")]
        fourLitres,
        [Description("Death dose, five litres")]
        fiveLitres
    }
}
