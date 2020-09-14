using AnSt.Chart.DefineSeries;
using AnSt.Chart.SetSeriesData;
using AnSt.Define.Attribute;
using AnSt.Define.ChartAttribute;
using System;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnSt.Chart.Uc
{
    public partial class UcPriceChart : UserControl
    {

        #region 전역변수
        public ClsPriceAttribute clsPriceAttribute;
        private ClsChartMenuAttirbute clsChartMenuAttirbute;
        private const string ChartAreaName = "PriceArea";
        private const string SeriesPriceName = "Price";
        private const string SeriesPriceLegend = "가격";
        private const string SeriesMa3Name = "Ma3";
        private const string SeriesMa3Legend = "M3";
        private const string SeriesMa5Name = "Ma5";
        private const string SeriesMa5Legend = "M5";
        private const string SeriesMa10Name = "Ma10";
        private const string SeriesMa10Legend = "M10";
        private const string SeriesMa20Name = "Ma20";
        private const string SeriesMa20Legend = "M20";
        private const string SeriesMa42Name = "Ma42";
        private const string SeriesMa42Legend = "M42";
        private const string SeriesMa60Name = "Ma60";
        private const string SeriesMa60Legend = "M60";
        private const string SeriesMa90Name = "Ma90";
        private const string SeriesMa90Legend = "M90";
        private const string SeriesMa120Name = "Ma120";
        private const string SeriesMa120Legend = "M120";
        private const string SeriesMa200Name = "Ma200";
        private const string SeriesMa200Legend = "Ma200";
        private const string SeriesMa480Name = "Ma480";
        private const string SeriesMa480Legend = "M480";
        private const string SeriesMa1000Name = "Ma1000";
        private const string SeriesMa1000Legend = "M1000";
        #endregion

        #region Event

        public delegate void PriceChartWaveOnSelectedHandler(object sender, string fromDate, string toDate);
        public event PriceChartWaveOnSelectedHandler PriceChartWaveOnSelected;
        #endregion

        public UcPriceChart()
        {
            InitializeComponent();
            // Code, Name, fromDate, toDate Attribute
            clsPriceAttribute = new ClsPriceAttribute();

            clsChartMenuAttirbute = new ClsChartMenuAttirbute();

            ppGridMenu.SelectedObject = clsChartMenuAttirbute;

            clsPriceAttribute.PricePropertyChanged += OnPropertyChanged;
            clsChartMenuAttirbute.ChartMenuAttributeChanged += ChartMenuAttributeChanged;

            ChartArea ca = new ChartArea();
            ca.Name = ChartAreaName;

            ChartPrice.ChartAreas.Add(ca);
        }

        #region SeriesAdd
        public async void SeriesAdd(string seriesName, string seriesLegendText)
        {
            Series se = new Series();
            ClsDefinePriceSeries clsDefinePriceSeries = new ClsDefinePriceSeries();

            if (seriesName == SeriesPriceName)
            {
                clsDefinePriceSeries.PriceSeries(ref se, SeriesPriceName, seriesLegendText);
                if (clsPriceAttribute.clsStockAttribute.StockCode != "")
                {
                    ClsPriceSetSeriesData clsPriceSetSeriesData = new ClsPriceSetSeriesData();
                    DataTable dt = new DataTable();
                    dt = clsPriceSetSeriesData.GetOpt10081("2", clsPriceAttribute.clsStockAttribute.StockCode, clsPriceAttribute.FromDate, clsPriceAttribute.ToDate);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {

                            InitChart();
                            clsDefinePriceSeries.PriceSeries(ref se, SeriesPriceName, SeriesPriceLegend);
                            clsPriceSetSeriesData.PriceSetSeriesData(ref se, dt);
                            se.ChartArea = ChartPrice.ChartAreas[ChartAreaName].Name;
                            ChartPrice.Series.Add(se);

                            int minLavel = Convert.ToInt32(dt.Compute("min([LOW_PRICE])", string.Empty));
                            int maxLavel = Convert.ToInt32(dt.Compute("max([HIGH_PRICE])", string.Empty));

                            ChartPrice.ChartAreas["PriceArea"].AxisY.Minimum = minLavel - (minLavel * 0.1);
                            ChartPrice.ChartAreas["PriceArea"].AxisY.Maximum = maxLavel + (maxLavel * 0.1);

                        }
                    }
                }
            }
            else
            {
                clsDefinePriceSeries.MaSeries(ref se, seriesName, seriesLegendText);
                se.ChartArea = ChartPrice.ChartAreas[ChartAreaName].Name;
                ChartPrice.Series.Add(se);

                //  DataCalcAll(seriesName, seriesLegendText,  Convert.ToInt32(seriesName.Replace("Ma", "")));
                if (ChartPrice.Series[SeriesPriceName].Points.Count >= Convert.ToInt32(seriesName.Replace("Ma", "")))
                {
                    await DataCalc(FinancialFormula.MovingAverage, seriesName.Replace("Ma", ""), SeriesPriceName + ":Y4", seriesName);
                }

            }
        }

        public void InitChart()
        {
            Double[] arrP = new Double[1];

            for (int ix = ChartPrice.Series.Count - 1; ix >= 0; ix--)
            {
                if (ChartPrice.Series[ix].Name.IndexOf(DateTime.Now.ToString("yyyyMMdd")) == -1)
                    this.ChartPrice.Series[ix].Points.Clear();
                else
                {
                    if (ChartPrice.Series[ix].Points.Count > 0)
                    {
                        arrP[arrP.Length - 1] = ChartPrice.Series[ix].Points[0].YValues[0];
                        Array.Resize(ref arrP, arrP.Length + 1);
                    }
                    this.ChartPrice.Series.RemoveAt(ix);
                }
            }
        }

        public void SeriesStateChange(string seriesName, string seriesLegendText, bool blnView)
        {
            if (blnView == true)
            {
                if (CheckSeriesInChart(seriesName) == true)
                {
                    ChartPrice.Series[seriesName].Enabled = true;
                }
                else
                {
                    SeriesAdd(seriesName, seriesLegendText);
                }
            }
            else
            {
                if (CheckSeriesInChart(seriesName) == true)
                {
                    ChartPrice.Series[seriesName].Enabled = false;

                    return;
                }
            }
        }

        private void PriceSeries(bool View)
        {
            bool chkSeries = CheckSeriesInChart(SeriesPriceName);

            if (View == true)
            {
                if (chkSeries == true)
                {
                    ChartPrice.Series[SeriesPriceName].Enabled = true;
                }
                else
                {
                    if (clsPriceAttribute.clsStockAttribute.StockCode != "" ||
                        clsPriceAttribute.FromDate != "" ||
                        clsPriceAttribute.ToDate != "")
                    {
                        Series se = new Series();
                        ClsDefinePriceSeries clsDefinePriceSeries = new ClsDefinePriceSeries();
                        ClsPriceSetSeriesData clsPriceSetSeriesData = new ClsPriceSetSeriesData();
                        DataTable dt = new DataTable();
                        dt = clsPriceSetSeriesData.GetOpt10081("2", clsPriceAttribute.clsStockAttribute.StockCode, clsPriceAttribute.FromDate, clsPriceAttribute.ToDate);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                clsDefinePriceSeries.PriceSeries(ref se, SeriesPriceName, SeriesPriceLegend);
                                clsPriceSetSeriesData.PriceSetSeriesData(ref se, dt);
                                se.ChartArea = ChartPrice.ChartAreas[ChartAreaName].Name;
                                ChartPrice.Series.Add(se);

                                int minLavel = Convert.ToInt32(dt.Compute("min([LOW_PRICE])", string.Empty));
                                int maxLavel = Convert.ToInt32(dt.Compute("max([HIGH_PRICE])", string.Empty));

                                ChartPrice.ChartAreas["PriceArea"].AxisY.Minimum = minLavel - (minLavel * 0.1);
                                ChartPrice.ChartAreas["PriceArea"].AxisY.Maximum = maxLavel + (maxLavel * 0.1);

                            }
                        }

                    }
                }

            }
            else
            {
                if (chkSeries == true)
                {
                    ChartPrice.Series[SeriesPriceName].Enabled = false;
                }
            }
        }

        private bool CheckSeriesInChart(string name)
        {
            bool blnChk = false;
            for (int i = 0; i < ChartPrice.Series.Count; i++)
            {
                if (ChartPrice.Series[i].Name == name)
                {
                    return true;
                }
            }

            return blnChk;
        }
        #endregion       

        private async Task DataCalc(FinancialFormula formula, string period, string candle, string line)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            {
                if (tcs == null || tcs.Task.IsCompleted)
                { return; }

                tcs.SetResult(true);
            };
            this.ChartPrice.DataManipulator.FinancialFormula(formula, period, candle, line);
            await tcs.Task;
            tcs.Task.Dispose();

        }
        public void ChartRefresh()
        {
            InitChart();
            SeriesStateChange(SeriesPriceName, SeriesPriceLegend, clsChartMenuAttirbute.Price);
            SeriesStateChange(SeriesMa3Name, SeriesMa3Legend, clsChartMenuAttirbute.Ma3);
            SeriesStateChange(SeriesMa5Name, SeriesMa5Legend, clsChartMenuAttirbute.Ma5);
            SeriesStateChange(SeriesMa10Name, SeriesMa10Legend, clsChartMenuAttirbute.Ma10);
            SeriesStateChange(SeriesMa20Name, SeriesMa20Legend, clsChartMenuAttirbute.Ma20);
            SeriesStateChange(SeriesMa42Name, SeriesMa42Legend, clsChartMenuAttirbute.Ma42);
            SeriesStateChange(SeriesMa60Name, SeriesMa60Legend, clsChartMenuAttirbute.Ma60);
            SeriesStateChange(SeriesMa90Name, SeriesMa90Legend, clsChartMenuAttirbute.Ma90);
            SeriesStateChange(SeriesMa120Name, SeriesMa120Legend, clsChartMenuAttirbute.Ma120);
            SeriesStateChange(SeriesMa200Name, SeriesMa200Legend, clsChartMenuAttirbute.Ma200);
            SeriesStateChange(SeriesMa480Name, SeriesMa480Legend, clsChartMenuAttirbute.Ma480);
            SeriesStateChange(SeriesMa1000Name, SeriesMa1000Legend, clsChartMenuAttirbute.Ma1000);
        }
        private void WaveOnSelected(object sender, string bifFlow)
        {
            clsPriceAttribute.FromDate = ucWaveInfo1.StartDate;
            clsPriceAttribute.ToDate = ucWaveInfo1.EndDate;
            ChartRefresh();
            var handler = PriceChartWaveOnSelected;
            if (handler != null)
            {
                this.PriceChartWaveOnSelected(this, clsPriceAttribute.FromDate, clsPriceAttribute.ToDate);
            }
        }


        #region Event
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(clsPriceAttribute.clsStockAttribute.StockCode))
            {
                if (clsPriceAttribute.clsStockAttribute.StockCode == "")
                {
                    return;
                }
                ucWaveInfo1.clsStockAttribute.StockName = clsPriceAttribute.clsStockAttribute.StockName;
                ucWaveInfo1.clsStockAttribute.StockCode = clsPriceAttribute.clsStockAttribute.StockCode;
                InitChart();
            }
            //PriceSeries(clsChartMenuAttirbute.Price);
        }

        private void ChartMenuAttributeChanged(object sender, PropertyChangedEventArgs e)
        {
            if (clsPriceAttribute.clsStockAttribute.StockCode == "")
            { return; }

            switch (e.PropertyName)
            {
                case SeriesPriceName:
                    SeriesStateChange(SeriesPriceName, SeriesPriceLegend, clsChartMenuAttirbute.Price);
                    break;
                case SeriesMa3Name:
                    SeriesStateChange(SeriesMa3Name, SeriesMa3Legend, clsChartMenuAttirbute.Ma3);
                    break;
                case SeriesMa5Name:
                    SeriesStateChange(SeriesMa5Name, SeriesMa5Legend, clsChartMenuAttirbute.Ma5);
                    break;
                case SeriesMa10Name:
                    SeriesStateChange(SeriesMa10Name, SeriesMa10Legend, clsChartMenuAttirbute.Ma10);
                    break;
                case SeriesMa20Name:
                    SeriesStateChange(SeriesMa20Name, SeriesMa20Legend, clsChartMenuAttirbute.Ma20);
                    break;
                case SeriesMa42Name:
                    SeriesStateChange(SeriesMa42Name, SeriesMa42Legend, clsChartMenuAttirbute.Ma42);
                    break;
                case SeriesMa60Name:
                    SeriesStateChange(SeriesMa60Name, SeriesMa60Legend, clsChartMenuAttirbute.Ma60);
                    break;
                case SeriesMa90Name:
                    SeriesStateChange(SeriesMa90Name, SeriesMa90Legend, clsChartMenuAttirbute.Ma90);
                    break;
                case SeriesMa120Name:
                    SeriesStateChange(SeriesMa120Name, SeriesMa120Legend, clsChartMenuAttirbute.Ma120);
                    break;
                case SeriesMa200Name:
                    SeriesStateChange(SeriesMa120Name, SeriesMa120Legend, clsChartMenuAttirbute.Ma120);
                    break;
                case SeriesMa480Name:
                    SeriesStateChange(SeriesMa480Name, SeriesMa480Legend, clsChartMenuAttirbute.Ma480);
                    break;
                case SeriesMa1000Name:
                    SeriesStateChange(SeriesMa1000Name, SeriesMa1000Legend, clsChartMenuAttirbute.Ma1000);
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
