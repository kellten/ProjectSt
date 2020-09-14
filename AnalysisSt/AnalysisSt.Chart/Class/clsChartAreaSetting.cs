using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnalysisSt.Chart.Class
{
    public class clsChartAreaSetting
    {
        public void PriceAreaSetting(ChartArea ca)
        {
            ca.Position.Auto = false;
            ca.Position.Height = 50;
            ca.Position.Width = 100;
            ca.Position.X = 3;
            ca.Position.Y = 0;

        }

        public void VolumeAreaSetting(ChartArea ca)
        {
            ca.Position.Auto = false;
            ca.Position.Height = 25;
            ca.Position.Width = 100;
            ca.Position.X = 3;
            ca.Position.Y = 50;

        }

        public void TradeAnlyAreaSetting(ChartArea ca)
        {
            ca.Position.Auto = false;
            ca.Position.Height = 25;
            ca.Position.Width = 100;
            ca.Position.X = 3;
            ca.Position.Y = 75;

        }
    }
}
