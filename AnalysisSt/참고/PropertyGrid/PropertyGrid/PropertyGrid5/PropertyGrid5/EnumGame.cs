using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;

namespace PropertyGrid5
{
   enum GameValues
   {
       [Description("Stone")]
       Stone, 
       [Description("Scissors")]
       Scissors, 
       [Description("Paper")]
       Paper
   }
}
