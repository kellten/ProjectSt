using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms.Design;

namespace AnalysisSt.Chart.Parameter
{
    public class ParamChartAttribute
    {
        // Enum
        public enum ParamIndex
        { Name = 0, ChartAreaPosition = 1, PlottingAreaPosition = 2, Visible = 3, Enable = 4 }

        #region Delegate & Event
        public delegate void ChangedCAreaProp(String CategoryName, ParamIndex p);
        public event ChangedCAreaProp onChangedCAreaProp;

        public void DoChangedCAreaProp(String CategoryName, ParamIndex p)
        {
            onChangedCAreaProp(CategoryName, p);
        }
        #endregion

        #region struct
        [TypeConverter(typeof(ChartAreaPositionConvert))]
        public struct stChartAreaPosition
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
        }
        [TypeConverter(typeof(PlottingAreaPositionConvert))]
        public struct stPlottingAreaPosition
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Width { get; set; }
            public float Height { get; set; }
        }
        #endregion

        // 전역변수
        private String _Name;
        private stChartAreaPosition _ChartAreaPostion;
        private stPlottingAreaPosition _PlottingAreaPosition;
        private Boolean _Visible;
        private Boolean _Enalbe;

        // Name
        [CategoryAttribute("Name"),
        ReadOnlyAttribute(true),
        DefaultValueAttribute("")]
        public String Name { get { return _Name; } set { _Name = value; DoChangedCAreaProp("Name", ParamIndex.Name); } }

        // ChartAreaPosition
        [CategoryAttribute("Chart Area Position"),
        DefaultValueAttribute(""),
        DescriptionAttribute("Enter Area Position Location")]
        public stChartAreaPosition ChartAreaPosition { get { return _ChartAreaPostion; } set { _ChartAreaPostion = value; DoChangedCAreaProp("ChartAreaPosition", ParamIndex.ChartAreaPosition); } }

        // PlottingAreaPosition
        [CategoryAttribute("Plotting Area Position"),
        DefaultValueAttribute(""),
        DescriptionAttribute("Plotting Area Position")]
        public stPlottingAreaPosition PlottingAreaPosition { get { return _PlottingAreaPosition; } set { _PlottingAreaPosition = value; DoChangedCAreaProp("ChartAreaPosition", ParamIndex.PlottingAreaPosition); } }

        // Visible
        [CategoryAttribute("Chart Area Visible"),
        DefaultValueAttribute(""),
        DescriptionAttribute("Select Area Visible")]
        public Boolean Visible { get { return _Visible; } set { _Visible = value; DoChangedCAreaProp("Visible", ParamIndex.Visible); } }

        // Enable
        [CategoryAttribute("Chart Area Enable"),
        DefaultValueAttribute(""),
        DescriptionAttribute("Select Area Enable")]
        public Boolean Enable { get { return _Enalbe; } set { _Enalbe = value; DoChangedCAreaProp("Enable", ParamIndex.Enable); } }
               
        public class ChartAreaPositionConvert : ExpandableObjectConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                { return true; }

                return base.CanConvertFrom(context, sourceType);
            }
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(stChartAreaPosition))
                {
                    return true;
                }
               return base.CanConvertTo(context, destinationType);
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(System.String) && value is stChartAreaPosition)
                {
                    stChartAreaPosition stChartAp = (stChartAreaPosition)value;
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                 if (value is string) {
                    try {
                        string s = (string) value;
                        int colon = s.IndexOf(':');
                        int comma = s.IndexOf(',');
 
                        if (colon != -1 && comma != -1) 
                        {
                            string x = s.Substring(colon + 1 ,
                                                            (comma - colon - 1));
 
                            colon = s.IndexOf(':', comma + 1);
                            comma = s.IndexOf(',', comma + 1);
 
                            string y = s.Substring(colon + 1 , 
                                                            (comma - colon -1));
 
                            colon = s.IndexOf(':', comma + 1);

                            string width = s.Substring(colon + 1 , 
                                                            (comma - colon -1));
 
                            colon = s.IndexOf(':', comma + 1);
 
                            string height = s.Substring(colon + 1);
 
                            stChartAreaPosition st = new stChartAreaPosition();

                            st.X = float.Parse(x);
                            st.Y = float.Parse(y);
                            st.Width = float.Parse(width);
                            st.Height = float.Parse(height);

                            return st;
                            }
                        }
                        catch {throw new ArgumentException("변환할 수 없습니다");}
                    }  

                return base.ConvertFrom(context, culture, value);
            }
        }

        public class PlottingAreaPositionConvert : ExpandableObjectConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                { return true; }

                return base.CanConvertFrom(context, sourceType);
            }
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(stPlottingAreaPosition))
                {
                    return true;
                }
               return base.CanConvertTo(context, destinationType);
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(System.String) && value is stPlottingAreaPosition)
                {
                    stPlottingAreaPosition stPlottAp = (stPlottingAreaPosition)value;
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                 if (value is string) {
                    try {
                        string s = (string) value;
                        int colon = s.IndexOf(':');
                        int comma = s.IndexOf(',');
 
                        if (colon != -1 && comma != -1) 
                        {
                            string x = s.Substring(colon + 1 ,
                                                            (comma - colon - 1));
 
                            colon = s.IndexOf(':', comma + 1);
                            comma = s.IndexOf(',', comma + 1);
 
                            string y = s.Substring(colon + 1 , 
                                                            (comma - colon -1));
 
                            colon = s.IndexOf(':', comma + 1);

                            string width = s.Substring(colon + 1 , 
                                                            (comma - colon -1));
 
                            colon = s.IndexOf(':', comma + 1);
 
                            string height = s.Substring(colon + 1);
 
                            stPlottingAreaPosition st = new stPlottingAreaPosition();

                            st.X = float.Parse(x);
                            st.Y = float.Parse(y);
                            st.Width = float.Parse(width);
                            st.Height = float.Parse(height);

                            return st;
                            }
                        }
                        catch {throw new ArgumentException("변환할 수 없습니다");}
                    }  

                return base.ConvertFrom(context, culture, value);
            }
        }
    }
  
}
