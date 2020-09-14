using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;

namespace PropertyGrid5
{
    class GameClassDisplayer
    {
        GameValues m_GameValues;
        [DisplayName("Choose your variant")]
        [Description("You can choose between Stone, scissors, paper")]
        [Category("Choosing")]
        [Editor(typeof(GameEditor), typeof(UITypeEditor))]
        public GameValues DisplayGameValues
        {
            get 
            {
                return m_GameValues;
            }
            set
            {
                m_GameValues = value;
            }
        }
    }
}
