using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnSt.Chart.DefineSeries
{
    public class ClsDefinePriceSeries
    {

        public void PriceSeries(ref Series se, string seriesName, string vLegendText)
        {
            if (seriesName == "")
            {
                se.Name = "Price";

            }
            else
            {
                se.Name = seriesName;
            }
            if (vLegendText == "")
            {
                se.LegendText = "가격";
            }
            else
            {
                se.LegendText = vLegendText;
            }

            se.ChartType = SeriesChartType.Candlestick;
            se.YValuesPerPoint = 4;
        }

        public void MaSeries(ref Series se, string seriesName, string vLegentText)
        {
            se.ChartType = SeriesChartType.Line;
            se.LegendText = vLegentText;
            se.Name = seriesName;

            int period = System.Convert.ToInt32(seriesName.Replace("Ma", ""));

            switch (period)
            {
                case 3:
                    se.Color = Color.Purple;
                    break;
                case 5:
                    se.Color = Color.Pink;
                    break;
                case 10:
                    se.Color = Color.Blue;
                    break;
                case 20:
                    se.Color = Color.Orange;
                    break;
                case 42:
                    se.Color = Color.CornflowerBlue;
                    break;
                case 60:
                    se.Color = Color.Green;
                    break;
                case 90:
                    se.Color = Color.Black;
                    break;
                case 120:
                    se.Color = Color.Gray;
                    break;
                case 200:
                    se.Color = Color.Red;
                    break;
                case 480:
                    se.Color = Color.RosyBrown;
                    break;
                case 1000:
                    se.Color = Color.Gold;
                    break;
                default:
                    break;
            }

        }
    }
}