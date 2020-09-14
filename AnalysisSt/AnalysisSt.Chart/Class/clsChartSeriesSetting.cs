using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnalysisSt.Chart.Class
{
    public class clsChartSeriesSetting
    {
        public void PriceSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Candlestick;
            se.YValuesPerPoint = 4;
        }

        public void CurvePriceSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.YValuesPerPoint = 4;
        }

        public void VolumeSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Column;
        }

        public void GainSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Red;
        }

        public void ForeSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Black;
        }

        public void GiganSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Blue;
        }

        public void GumySeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Purple;
        }

        public void BohumSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.YellowGreen;
        }

        public void TosinSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Green;
        }

        public void GitaSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Magenta;
        }

        public void BankSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Orange;
        }

        public void YeongiSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Purple;
        }

        public void SamoSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Pink;
        }

        public void NationSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Yellow;
        }

        public void BubinSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Violet;
        }

        public void IoForeSeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.FastLine;
            se.Color = Color.Orange;
        }
        public void Ma3SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.Purple;
        }
        public void Ma5SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.Pink;
        }
        public void Ma10SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.Blue;
        }
        public void Ma20SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.Orange;
        }
        public void Ma42SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.CornflowerBlue;
        }
        public void Ma60SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.Green;
        }
        public void Ma90SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.Black;
        }
        public void Ma120SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.Gray;
        }
        public void Ma200SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.Red;
        }
        public void Ma480SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.RosyBrown;
        }
        public void Ma1000SeriesSetting(Series se)
        {
            se.ChartType = SeriesChartType.Line;
            se.Color = Color.Gold;
        }

    }
}
