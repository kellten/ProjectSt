using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;

namespace PropertyGrid2
{
    
    class PropertyGridSimpleDemoClass2
    {
        int m_DisplayInt = 50; // some initialization
        [Browsable(true)] //this property should be visible
        [ReadOnly(true)]  //but just read only
        [Description("sample hint1")] // sample hint1
        [Category("Category1")]// Category that I want
        [DisplayName("Int for Displaying")] // I want to say more, than just DisplayInt
        public int DisplayInt
        {
            get { return m_DisplayInt; }
            set { m_DisplayInt = value; }
        }
        
        string m_DisplayString;
        [Browsable(true)] //this property should be visible
        [ReadOnly(false)]  //this property is for editing
        [Description("Example Displaying hint 2")] // sample hint2
        [Category("Category1")]// Category that I want
        [DisplayName("Name")] // and more than Display String
        public string DisplayString
        {
            get { return m_DisplayString; }
            set { m_DisplayString = value; }
        }
        
        bool m_DisplayBool;
        [Category("Category2")]// Category that I want
        [Description("To be or not to be")] // yet one hint
        [DisplayName("To drink or not to drink")] // that is a question
        public bool DisplayBool
        {
            get { return m_DisplayBool; }
            set { m_DisplayBool = value; }
        }
        
        
        Color m_DisplayColors;
        [Browsable(false)]//this property shoul be hided;
        public Color DisplayColors
        {
            get { return m_DisplayColors; }
            set { m_DisplayColors = value; }
        }

    }
}
