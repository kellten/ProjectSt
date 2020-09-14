using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AnalysisSt.DataBaseFunc;

namespace AnalysisSt.Analysis.Forms
{
    public partial class frmAnalysisTradeQty : Form
    {
        public frmAnalysisTradeQty()
        {
            InitializeComponent();
        }

        private void frmAnalysisTradeQty_Load(object sender, EventArgs e)
        {
            InitChartTradeQty();
            InitForm();
        }

        private void InitForm()
        {
            chkIoFore.Checked = false;
            chkBubin.Checked = false;
            chkSamo.Checked = false;
            chkYeon.Checked = false;
            chkBank.Checked = false;
            chkGita.Checked = false;
            chkTosin.Checked = false;
            chkBohum.Checked = false;
            chkGumy.Checked = false;
            chkGigan.Checked = false;
            chkFore.Checked = false;
            chkNation.Checked = false;

            chartTradeQty.ChartAreas[chkIoFore.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkBubin.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkSamo.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkYeon.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkBank.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkGita.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkTosin.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkBohum.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkGumy.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkGigan.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkFore.Tag.ToString()].Visible = false;
            chartTradeQty.ChartAreas[chkNation.Tag.ToString()].Visible = false;
        }

        private int _chartWidth;
        private int _chartHeight;

        private void GetTradeQtyInfo()
        {
            DataSet ds;
            DataSet ds2;
            KiwoomQuery oKiwoomQuery = new KiwoomQuery();

            ds = oKiwoomQuery.p_Opt10060_QtyQuery("2", lblStockCode.Text.Trim(), CDateTime.FormatDate(dtpFromDate.Text), CDateTime.FormatDate(dtpToDate.Text), false).Copy();
            
            ds2 = oKiwoomQuery.p_Opt1005Scare9QtyNujukQuery("1", lblStockCode.Text.Trim(), CDateTime.FormatDate(dtpFromDate.Text), CDateTime.FormatDate(dtpToDate.Text), false).Copy();

            if (ds == null || ds.Tables[0].Rows.Count < 1 || ds2 == null || ds2.Tables[0].Rows.Count < 1)
            {ds.Reset();
            ds2.Reset();
            return;}
            else
            {
                DisplayChart(ds, ds2);
            }

            ds.Reset();
            ds2.Reset();
                       
        }


        #region Chart
        private void InitChartTradeQty()
        {
            _chartHeight = chartTradeQty.Height;
            _chartWidth = chartTradeQty.Width;

            chartTradeQty.Series["개인"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["외국인"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["기관"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["금융"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["보험"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["투신"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["기타금융"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["은행"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["연기금"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["사모"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["기법"].ChartType = SeriesChartType.SplineRange;
            chartTradeQty.Series["기외"].ChartType = SeriesChartType.SplineRange;

            chartTradeQty.Series["개인매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["외국인매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["기관매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["금융매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["보험매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["투신매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["기타금융매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["은행매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["연기금매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["사모매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["기법매집"].ChartType = SeriesChartType.FastLine;
            chartTradeQty.Series["기외매집"].ChartType = SeriesChartType.FastLine;
            
            chartTradeQty.Series["Price"].ChartType = SeriesChartType.Candlestick;
            chartTradeQty.Series["거래량"].ChartType = SeriesChartType.Line;

            for (int i = 0; i < chartTradeQty.ChartAreas.Count - 1; i++)
            {
                chartTradeQty.ChartAreas[i].InnerPlotPosition.Auto = false;
                chartTradeQty.ChartAreas[i].InnerPlotPosition.Height = 90;
                chartTradeQty.ChartAreas[i].InnerPlotPosition.Width = 90;
                chartTradeQty.ChartAreas[i].InnerPlotPosition.X = 7;
                chartTradeQty.ChartAreas[i].InnerPlotPosition.Y = 3;
                chartTradeQty.ChartAreas[i].AxisY2.Enabled = AxisEnabled.True;
            }

        }
              
        private void DisplayChart(DataSet ds, DataSet ds2)
        {
            Double[] arrP = new Double[1];
            
            int pt = 0;
            DataView dv = new DataView(ds.Tables[0]);
            DataView dv2 = new DataView(ds2.Tables[0]);
            int high = 0;
            int low = 0;
            dv.Sort = "STOCK_DATE asc";
            dv2.Sort = "STOCK_DATE asc";
            try
            {

                for (int ix = chartTradeQty.Series.Count - 1; ix >= 0; ix--)
                {
                    if (chartTradeQty.Series[ix].Name.IndexOf(DateTime.Now.ToString("yyyyMMdd")) == -1)
                        chartTradeQty.Series[ix].Points.Clear();
                    else
                    {
                        if (chartTradeQty.Series[ix].Points.Count > 0)
                        {
                            arrP[arrP.Length - 1] = chartTradeQty.Series[ix].Points[0].YValues[0];
                            Array.Resize(ref arrP, arrP.Length + 1);
                        }
                        chartTradeQty.Series.RemoveAt(ix);
                    }
                }
            }
            catch (System.NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            Double Gain = 0;
            Double IoFore= 0;
            Double Bubin= 0;
            Double Samo= 0;
            Double Yeon= 0;
            Double Bank= 0;
            Double Gita= 0;
            Double Tosin= 0;
            Double Bohum= 0;
            Double Gumy= 0;
            Double Gigan= 0;
            Double Fore= 0;
            String stockDate = "";
            // Double Nation= 0;
            
            foreach (DataRowView dr in dv)
            {
                
                // Nation = 0;

                if (dr["MAEME_GB"].ToString().Trim() == "1")
                {
                    stockDate = dr["STOCK_DATE"].ToString().Trim();
                    chartTradeQty.Series["개인"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GAIN_QTY].ToString()));
                    chartTradeQty.Series["외국인"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.FORE_QTY].ToString()));
                    chartTradeQty.Series["기관"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GIGAN_QTY].ToString()));
                    chartTradeQty.Series["금융"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GUMY_QTY].ToString()));
                    chartTradeQty.Series["보험"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BOHUM_QTY].ToString()));
                    chartTradeQty.Series["투신"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.TOSIN_QTY].ToString()));
                    chartTradeQty.Series["기타금융"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GITA_QTY].ToString()));
                    chartTradeQty.Series["은행"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BANK_QTY].ToString()));
                    chartTradeQty.Series["연기금"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.YEONGI_QTY].ToString()));
                    chartTradeQty.Series["사모"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.SAMO_QTY].ToString()));
                    chartTradeQty.Series["기법"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BUBIN_QTY].ToString()));
                    chartTradeQty.Series["기외"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.IOFORE_QTY].ToString()));

                    Gain = double.Parse(dr[Common.Class.clsDicDefine.GAIN_QTY].ToString());
                    Fore = double.Parse(dr[Common.Class.clsDicDefine.FORE_QTY].ToString());
                    Bubin = double.Parse(dr[Common.Class.clsDicDefine.BUBIN_QTY].ToString()); 
                    Samo   = double.Parse(dr[Common.Class.clsDicDefine.SAMO_QTY].ToString());
                    Yeon   = double.Parse(dr[Common.Class.clsDicDefine.YEONGI_QTY].ToString());
                    Bank   = double.Parse(dr[Common.Class.clsDicDefine.BANK_QTY].ToString());
                    Gita   = double.Parse(dr[Common.Class.clsDicDefine.GITA_QTY].ToString());
                    Tosin  = double.Parse(dr[Common.Class.clsDicDefine.TOSIN_QTY].ToString());
                    Bohum  = double.Parse(dr[Common.Class.clsDicDefine.BOHUM_QTY].ToString());
                    Gumy   = double.Parse(dr[Common.Class.clsDicDefine.GUMY_QTY].ToString());
                    Gigan  = double.Parse(dr[Common.Class.clsDicDefine.GIGAN_QTY].ToString());
                    IoFore = double.Parse(dr[Common.Class.clsDicDefine.IOFORE_QTY].ToString());
                }
                else
                {
    
                    chartTradeQty.Series["개인"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.GAIN_QTY].ToString());
                    chartTradeQty.Series["외국인"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.FORE_QTY].ToString());
                    chartTradeQty.Series["기관"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.GIGAN_QTY].ToString());
                    chartTradeQty.Series["금융"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.GUMY_QTY].ToString());
                    chartTradeQty.Series["보험"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.BOHUM_QTY].ToString());
                    chartTradeQty.Series["투신"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.TOSIN_QTY].ToString());
                    chartTradeQty.Series["기타금융"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.GITA_QTY].ToString());
                    chartTradeQty.Series["은행"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.BANK_QTY].ToString());
                    chartTradeQty.Series["연기금"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.YEONGI_QTY].ToString());
                    chartTradeQty.Series["사모"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.SAMO_QTY].ToString());
                    chartTradeQty.Series["기법"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.BUBIN_QTY].ToString());
                    chartTradeQty.Series["기외"].Points[pt].YValues[1] =  double.Parse(dr[Common.Class.clsDicDefine.IOFORE_QTY].ToString());

                    if (stockDate == dr["STOCK_DATE"].ToString().Trim())
                    {
                        Gain = Gain + double.Parse(dr[Common.Class.clsDicDefine.GAIN_QTY].ToString());
                        Fore = Fore + double.Parse(dr[Common.Class.clsDicDefine.FORE_QTY].ToString());
                        Bubin = Bubin + double.Parse(dr[Common.Class.clsDicDefine.BUBIN_QTY].ToString());
                        Samo = Samo + double.Parse(dr[Common.Class.clsDicDefine.SAMO_QTY].ToString());
                        Yeon = Yeon + double.Parse(dr[Common.Class.clsDicDefine.YEONGI_QTY].ToString());
                        Bank = Bank + double.Parse(dr[Common.Class.clsDicDefine.BANK_QTY].ToString());
                        Gita = Gita + double.Parse(dr[Common.Class.clsDicDefine.GITA_QTY].ToString());
                        Tosin = Tosin + double.Parse(dr[Common.Class.clsDicDefine.TOSIN_QTY].ToString());
                        Bohum = Bohum + double.Parse(dr[Common.Class.clsDicDefine.BOHUM_QTY].ToString());
                        Gumy = Gumy + double.Parse(dr[Common.Class.clsDicDefine.GUMY_QTY].ToString());
                        Gigan = Gigan + double.Parse(dr[Common.Class.clsDicDefine.GIGAN_QTY].ToString());
                        IoFore = IoFore + double.Parse(dr[Common.Class.clsDicDefine.IOFORE_QTY].ToString());

                        if (Gain < 0) { chartTradeQty.Series["개인"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Fore < 0) { chartTradeQty.Series["외국인"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Gigan < 0) { chartTradeQty.Series["기관"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Bubin < 0) { chartTradeQty.Series["기법"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Samo < 0) { chartTradeQty.Series["사모"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Gumy < 0) { chartTradeQty.Series["금융"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Yeon < 0) { chartTradeQty.Series["연기금"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Bohum < 0) { chartTradeQty.Series["보험"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Bank < 0) { chartTradeQty.Series["은행"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Tosin < 0) { chartTradeQty.Series["투신"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (Gita < 0) { chartTradeQty.Series["기타금융"].Points[pt].Color = System.Drawing.Color.Black; }
                        if (IoFore < 0) { chartTradeQty.Series["기외"].Points[pt].Color = System.Drawing.Color.Black; }
                    }
                    else
                    {
                        Gain = 0;
                        IoFore = 0;
                        Bubin = 0;
                        Samo = 0;
                        Yeon = 0;
                        Bank = 0;
                        Gita = 0;
                        Tosin = 0;
                        Bohum = 0;
                        Gumy = 0;
                        Gigan = 0;
                        Fore = 0;
                    }
                    
                    pt++;
                }
            }

            foreach (DataRowView dr in dv2)
            {

                chartTradeQty.Series["개인매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GAIN_QTY].ToString()));
                chartTradeQty.Series["외국인매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.FORE_QTY].ToString()));
                chartTradeQty.Series["기관매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GIGAN_QTY].ToString()));
                chartTradeQty.Series["금융매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GUMY_QTY].ToString()));
                chartTradeQty.Series["보험매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BOHUM_QTY].ToString()));
                chartTradeQty.Series["투신매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.TOSIN_QTY].ToString()));
                chartTradeQty.Series["기타금융매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GITA_QTY].ToString()));
                chartTradeQty.Series["은행매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BANK_QTY].ToString()));
                chartTradeQty.Series["연기금매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.YEONGI_QTY].ToString()));
                chartTradeQty.Series["사모매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.SAMO_QTY].ToString()));
                chartTradeQty.Series["기법매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BUBIN_QTY].ToString()));
                chartTradeQty.Series["기외매집"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.IOFORE_QTY].ToString()));

            }

            pt = 0;
            
            foreach (DataRowView dr in dv)
            {
                if (dr["MAEME_GB"].ToString().Trim() == "2")
                {
                    continue;
                }

                if (high == 0)
                {
                    high = int.Parse(dr["HIGH_PRICE"].ToString());
                }
                else
                {
                    if (high < int.Parse(dr["HIGH_PRICE"].ToString()))
                    {
                        high = int.Parse(dr["HIGH_PRICE"].ToString());
                    }
                }

                if (low == 0)
                {
                    low = int.Parse(dr["LOW_PRICE"].ToString());
                }
                else
                {
                    if (low > int.Parse(dr["LOW_PRICE"].ToString()))
                    {
                        low = int.Parse(dr["LOW_PRICE"].ToString());
                    }
                }

                chartTradeQty.Series["Price"].Points.AddXY((object)dr["STOCK_DATE"], int.Parse(dr["HIGH_PRICE"].ToString()));
                chartTradeQty.Series["Price"].Points[pt].YValues[1] = int.Parse(dr["LOW_PRICE"].ToString());
                chartTradeQty.Series["Price"].Points[pt].YValues[2] = int.Parse(dr["START_PRICE"].ToString());
                chartTradeQty.Series["Price"].Points[pt].YValues[3] = int.Parse(dr["NOW_PRICE"].ToString());

                chartTradeQty.Series["거래량"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr["TRADE_QTY"].ToString()));
                int curCnt = chartTradeQty.Series["거래량"].Points.Count - 1;
                chartTradeQty.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Red;

                if (curCnt > 0)
                {
                    Double preVolume = chartTradeQty.Series["거래량"].Points[curCnt - 1].YValues[0];
                    Double CurVolume = chartTradeQty.Series["거래량"].Points[curCnt].YValues[0];

                    if (preVolume < CurVolume)
                    {
                        chartTradeQty.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Red;
                    }
                    else
                    {
                        chartTradeQty.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Blue;
                    }
                }

                if (int.Parse(dr["START_PRICE"].ToString()) > int.Parse(dr["NOW_PRICE"].ToString()))
                {
                    chartTradeQty.Series["Price"].Points[pt].Color = System.Drawing.Color.Blue;
                }
                else
                {
                    chartTradeQty.Series["Price"].Points[pt].Color = System.Drawing.Color.Red;
                }

                pt++;
            }

         //   CreateYAxis(chartTradeQty, chartTradeQty.ChartAreas["Price"], chartTradeQty.Series["Price"], 13, 8);
        //    CreateYAxis(chartTradeQty, chartTradeQty.ChartAreas["Price"], chartTradeQty.Series["거래량"], 22, 8);
            //_lastVolume = Int64.Parse(chartTradeQty.Series["거래량"].Points[chartTradeQty.Series["거래량"].Points.Count - 1].YValues[0].ToString());

            chartTradeQty.ChartAreas["Price"].AxisY.Minimum = low - (low * 0.1);
            chartTradeQty.ChartAreas["Price"].AxisY.Maximum = high + (high * 0.1);
            //chartTradeQty.ChartAreas["Trade"].AxisX.IsLabelAutoFit = true;
            
        }

        /// <summary>
        /// Creates Y axis for the specified series.
        /// </summary>
        /// <param name="chart">Chart control.</param>
        /// <param name="area">Original chart area.</param>
        /// <param name="series">Series.</param>
        /// <param name="axisOffset">New Y axis offset in relative coordinates.</param>
        /// <param name="labelsSize">Extra space for new Y axis labels in relative coordinates.</param>
        public void CreateYAxis(System.Windows.Forms.DataVisualization.Charting.Chart chart, ChartArea area, Series series, float axisOffset, float labelsSize)
        {
            // Create new chart area for original series
            ChartArea areaSeries = chart.ChartAreas.Add("ChartArea_" + series.Name);
            areaSeries.BackColor = Color.Transparent;
            areaSeries.BorderColor = Color.Transparent;
            areaSeries.Position.FromRectangleF(area.Position.ToRectangleF());
            areaSeries.InnerPlotPosition.FromRectangleF(area.InnerPlotPosition.ToRectangleF());
            areaSeries.AxisX.MajorGrid.Enabled = false;
            areaSeries.AxisX.MajorTickMark.Enabled = false;
            areaSeries.AxisX.LabelStyle.Enabled = false;
            areaSeries.AxisY.MajorGrid.Enabled = false;
            areaSeries.AxisY.MajorTickMark.Enabled = false;
            areaSeries.AxisY.LabelStyle.Enabled = false;
            areaSeries.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;

            series.ChartArea = areaSeries.Name;

            // Create new chart area for axis
            ChartArea areaAxis = chart.ChartAreas.Add("AxisY_" + series.ChartArea);
            areaAxis.BackColor = Color.Transparent;
            areaAxis.BorderColor = Color.Transparent;
            areaAxis.Position.FromRectangleF(chart.ChartAreas[series.ChartArea].Position.ToRectangleF());
            areaAxis.InnerPlotPosition.FromRectangleF(chart.ChartAreas[series.ChartArea].InnerPlotPosition.ToRectangleF());

            // Create a copy of specified series
            Series seriesCopy = chart.Series.Add(series.Name + "_Copy");
            seriesCopy.ChartType = series.ChartType;
            foreach (DataPoint point in series.Points)
            {
                seriesCopy.Points.AddXY(point.XValue, point.YValues[0]);
            }

            // Hide copied series
            seriesCopy.IsVisibleInLegend = false;
            seriesCopy.Color = Color.Transparent;
            seriesCopy.BorderColor = Color.Transparent;
            seriesCopy.ChartArea = areaAxis.Name;

            // Disable grid lines & tickmarks
            areaAxis.AxisX.LineWidth = 0;
            areaAxis.AxisX.MajorGrid.Enabled = false;
            areaAxis.AxisX.MajorTickMark.Enabled = false;
            areaAxis.AxisX.LabelStyle.Enabled = false;
            areaAxis.AxisY.MajorGrid.Enabled = false;
            areaAxis.AxisY.IsStartedFromZero = area.AxisY.IsStartedFromZero;

            // Adjust area position
            areaAxis.Position.X -= axisOffset;
            areaAxis.InnerPlotPosition.X += labelsSize;
        }
        
        #endregion
                    
        private void btnView_Click(object sender, EventArgs e)
        {
            GetTradeQtyInfo();
        }

        private void chkNation_CheckedChanged(object sender, EventArgs e)
        {
            String senderTag = ((CheckBox)sender).Tag.ToString().Trim();
            if (((CheckBox)sender).Checked == true)
            { chartTradeQty.ChartAreas[senderTag].Visible = true; }
            else
            { chartTradeQty.ChartAreas[senderTag].Visible = false; }
        }

        private void btnGetDate_Click(object sender, EventArgs e)
        {
            AnalysisSt.CallForm.Forms.frmCallFormStockWaveInfo oform = new AnalysisSt.CallForm.Forms.frmCallFormStockWaveInfo();
            AnalysisSt.CallForm.Forms.frmCallFormStockWaveInfo.ScareDate scareDate;
            AnalysisSt.CallForm.Forms.frmCallFormStockWaveInfo.StockCode stockCode;

            stockCode.STOCK_CODE = lblStockCode.Text;
            stockCode.STOCK_NAME = lblStockName.Text;
            oform.propStockCode = stockCode;

            DialogResult dr = oform.ShowDialog();

            if (dr == DialogResult.OK)
            { 
                scareDate = oform.propScareDate;
                dtpFromDate.Text = CDateTime.FormatDate(scareDate.FROM_DATE, "-");
                dtpToDate.Text = CDateTime.FormatDate(scareDate.TO_DATE, "-");
            }
        }
                             
    }
}
