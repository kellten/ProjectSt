using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalysisSt.DataBaseFunc;
using AnalysisSt.KiwoomVB;
using AnalysisSt.Common;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnalysisSt.Chart.Uc
{
    public partial class ucTradeAnalyChart : UserControl
    {
        public ucTradeAnalyChart()
        {
            InitializeComponent();
            // ChartInit();
        }
        
        public struct StockCode
	    {
		    public String STOCK_CODE;
            public String STOCK_NAME;
	    }
        
        private StockCode _stockCode;
        public StockCode propStockCode 
        { get {return _stockCode;}
          set {_stockCode = value;
               SetStockCode();}
        }

        private int _highTrade;
        private int _lowTrade;

        #region ChartControl
        private void ChartInit()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();

            chartArea1.Name = "ChartArea1";
            chartArea2.Name = "Volume";
            chartArea3.Name = "Price";

            chartArea2.AxisX.Enabled = AxisEnabled.False;
            chartArea3.AxisX.Enabled = AxisEnabled.False;

            chartTradeAnaly.ChartAreas.Clear();
            chartTradeAnaly.Series.Clear();
            chartTradeAnaly.ChartAreas.Add(chartArea1);
            chartTradeAnaly.ChartAreas.Add(chartArea2);
            chartTradeAnaly.ChartAreas.Add(chartArea3);

            series1.ChartArea = "Price";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Name = "캔들";
            series1.YValuesPerPoint = 4;

            series3.ChartArea = "Volume";
            series3.Name = "거래량";

            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = Color.Purple;
            series4.Name = "개인";

            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = Color.YellowGreen;
            series5.Name = "외국인";

            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = Color.Green;
            series6.Name = "기관";

            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = Color.Magenta;
            series2.Name = "금융";

            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series13.Color = Color.Orange;
            series13.Name = "보험";
            
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = Color.Pink;
            series7.Name = "투신";

            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = Color.Blue;
            series8.Name = "기타금융";

            //series9.YValuesPerPoint = 4;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            series9.Color = Color.Transparent;
            series9.BorderColor = Color.Black;
            series9.Name = "은행";

            //series10.YValuesPerPoint = 4;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            series10.Color = Color.Transparent;
            series10.BorderColor = Color.DarkGray;
            series10.Name = "연기금";

            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series11.Color = Color.DeepSkyBlue;
            series11.Name = "사모펀드";

            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series12.Color = Color.Red;
            series12.Name = "국가";

            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series14.Color = Color.Black;
            series14.Name = "기타법인";

            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series15.Color = Color.Gold;
            series15.Name = "기타외인";


            chartTradeAnaly.ChartAreas["Volume"].AxisX.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["Volume"].AxisX.MajorTickMark.Enabled = false;
            chartTradeAnaly.ChartAreas["Volume"].AxisX.LineWidth = 0;
            chartTradeAnaly.ChartAreas["Volume"].AxisY.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["Volume"].AxisY.MajorTickMark.Enabled = false;
            chartTradeAnaly.ChartAreas["Volume"].AxisY.LineWidth = 0;

            chartTradeAnaly.ChartAreas["Price"].AxisX.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["Price"].AxisX.MajorTickMark.Enabled = false;
            chartTradeAnaly.ChartAreas["Price"].AxisX.LineWidth = 0;
            chartTradeAnaly.ChartAreas["Price"].AxisY.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["Price"].AxisY.MajorTickMark.Enabled = false;
            chartTradeAnaly.ChartAreas["Price"].AxisY.LineWidth = 0;


            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.ScaleView.Position = chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.Maximum;
            chartTradeAnaly.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chartTradeAnaly.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            //_baseChart.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Days;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.Interval = 10;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            chartTradeAnaly.ChartAreas["Volume"].CursorX.IsUserEnabled = true;
            chartTradeAnaly.ChartAreas["Volume"].CursorX.IsUserSelectionEnabled = true;
            chartTradeAnaly.ChartAreas["Volume"].AxisX.ScaleView.Zoomable = true;

            chartTradeAnaly.ChartAreas["Price"].CursorX.IsUserEnabled = true;
            chartTradeAnaly.ChartAreas["Price"].CursorX.IsUserSelectionEnabled = true;
            chartTradeAnaly.ChartAreas["Price"].AxisX.ScaleView.Zoomable = true;
            
            chartTradeAnaly.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            chartTradeAnaly.ChartAreas["Volume"].InnerPlotPosition.Auto = true;
            chartTradeAnaly.ChartAreas["Price"].InnerPlotPosition.Auto = true;

            chartTradeAnaly.ChartAreas["Volume"].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
            chartTradeAnaly.ChartAreas["Volume"].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;
            chartTradeAnaly.ChartAreas["Volume"].AlignWithChartArea = "ChartArea1";

            chartTradeAnaly.ChartAreas["Price"].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
            chartTradeAnaly.ChartAreas["Price"].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;
            chartTradeAnaly.ChartAreas["Price"].AlignWithChartArea = "ChartArea1";

            chartTradeAnaly.ChartAreas["ChartArea1"].Position.Auto = false;
            chartTradeAnaly.ChartAreas["ChartArea1"].Position.Height = 75;
            chartTradeAnaly.ChartAreas["ChartArea1"].Position.Width = 100;
            chartTradeAnaly.ChartAreas["ChartArea1"].Position.X = 0;
            chartTradeAnaly.ChartAreas["ChartArea1"].Position.Y = 0;

            chartTradeAnaly.ChartAreas["Price"].Position.Auto = false;
            chartTradeAnaly.ChartAreas["Price"].Position.Height = 15;
            chartTradeAnaly.ChartAreas["Price"].Position.Width = 100;
            chartTradeAnaly.ChartAreas["Price"].Position.X = chartTradeAnaly.ChartAreas["ChartArea1"].Position.X;
            chartTradeAnaly.ChartAreas["Price"].Position.Y = chartTradeAnaly.ChartAreas["ChartArea1"].Position.Y + chartTradeAnaly.ChartAreas["ChartArea1"].Position.Height;

            chartTradeAnaly.ChartAreas["Volume"].Position.Auto = false;
            chartTradeAnaly.ChartAreas["Volume"].Position.Height = 15;
            chartTradeAnaly.ChartAreas["Volume"].Position.Width = 100;
            chartTradeAnaly.ChartAreas["Volume"].Position.X = chartTradeAnaly.ChartAreas["ChartArea1"].Position.X;
            chartTradeAnaly.ChartAreas["Volume"].Position.Y = chartTradeAnaly.ChartAreas["ChartArea1"].Position.Y + chartTradeAnaly.ChartAreas["ChartArea1"].Position.Height;

            chartTradeAnaly.Series.Add(series1);
            chartTradeAnaly.Series.Add(series2);
            chartTradeAnaly.Series.Add(series3);
            chartTradeAnaly.Series.Add(series4);
            chartTradeAnaly.Series.Add(series5);
            chartTradeAnaly.Series.Add(series6);
            chartTradeAnaly.Series.Add(series7);
            chartTradeAnaly.Series.Add(series8);
            chartTradeAnaly.Series.Add(series9);
            chartTradeAnaly.Series.Add(series10);
            chartTradeAnaly.Series.Add(series11);
            chartTradeAnaly.Series.Add(series12);
            chartTradeAnaly.Series.Add(series13);
            chartTradeAnaly.Series.Add(series14);
            chartTradeAnaly.Series.Add(series15);

            chartTradeAnaly.Series["캔들"].ToolTip = "#AXISLABEL";
            chartTradeAnaly.Series["캔들"].IsXValueIndexed = false;
            chartTradeAnaly.Series["캔들"].XValueType = ChartValueType.DateTime;

            chartTradeAnaly.Width = this.Width - 20;
            chartTradeAnaly.Height = this.Height - 5;
      
        }

        private void DisplayChart(DataSet ds)
        {
            Double[] arrP = new Double[1];
            int pt = 0;
            DataView dv = new DataView(ds.Tables[0]);
            int high = 0;
            int low = 0;

            dv.Sort = "STOCK_DATE asc";
    
            try
            {

                for (int ix = chartTradeAnaly.Series.Count - 1; ix >= 0; ix--)
                {
                    if (chartTradeAnaly.Series[ix].Name.IndexOf(DateTime.Now.ToString("yyyyMMdd")) == -1)
                        chartTradeAnaly.Series[ix].Points.Clear();
                    else
                    {
                        if (chartTradeAnaly.Series[ix].Points.Count > 0)
                        {
                            arrP[arrP.Length - 1] = chartTradeAnaly.Series[ix].Points[0].YValues[0];
                            Array.Resize(ref arrP, arrP.Length + 1);
                        }
                        chartTradeAnaly.Series.RemoveAt(ix);
                    }
                }
            }
            catch (System.NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
     
            foreach (DataRowView dr in dv)
            {
                //if (low == 0)
                //{
                //    high = int.Parse(dr["HIGH_PRICE"].ToString());
                //}
                //else
                //{
                //    if (high < int.Parse(dr["HIGH_PRICE"].ToString()))
                //    {
                //        high = int.Parse(dr["HIGH_PRICE"].ToString());
                //    }
                //}


                //if (low == 0)
                //{
                //    low = int.Parse(dr["LOW_PRICE"].ToString());
                //}
                //else
                //{
                //    if (low > int.Parse(dr["LOW_PRICE"].ToString()))
                //    {
                //        low = int.Parse(dr["LOW_PRICE"].ToString());
                //    }
                //}

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

                chartTradeAnaly.Series["Price"].Points.AddXY((object)dr["STOCK_DATE"], int.Parse(dr["HIGH_PRICE"].ToString()));
                chartTradeAnaly.Series["Price"].Points[pt].YValues[1] = int.Parse(dr["LOW_PRICE"].ToString());
                chartTradeAnaly.Series["Price"].Points[pt].YValues[2] = int.Parse(dr["START_PRICE"].ToString());
                chartTradeAnaly.Series["Price"].Points[pt].YValues[3] = int.Parse(dr["NOW_PRICE"].ToString());
                                
                chartTradeAnaly.Series["거래량"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr["TRADE_QTY"].ToString()));
                int curCnt = chartTradeAnaly.Series["거래량"].Points.Count - 1;
                chartTradeAnaly.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Red;

                if (curCnt > 0) { 
                    Double preVolume = chartTradeAnaly.Series["거래량"].Points[curCnt - 1].YValues[0];
                    Double CurVolume = chartTradeAnaly.Series["거래량"].Points[curCnt].YValues[0];

                    if (preVolume < CurVolume)
                    {
                        chartTradeAnaly.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Red;
                    }
                    else
                    {
                        chartTradeAnaly.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Blue;
                    }
                }
                                                                
                if (int.Parse(dr["START_PRICE"].ToString()) > int.Parse(dr["NOW_PRICE"].ToString()))
                {
                    chartTradeAnaly.Series["Price"].Points[pt].Color = System.Drawing.Color.Blue;
                }
                else
                {
                    chartTradeAnaly.Series["Price"].Points[pt].Color = System.Drawing.Color.Red;
                }

                chartTradeAnaly.Series["개인"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GAIN_PRICE].ToString()));
                chartTradeAnaly.Series["외국인"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.FORE_PRICE].ToString()));
                chartTradeAnaly.Series["기관"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GIGAN_PRICE].ToString()));
                chartTradeAnaly.Series["금융"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GUMY_PRICE].ToString()));
                chartTradeAnaly.Series["보험"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BOHUM_PRICE].ToString()));
                chartTradeAnaly.Series["투신"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.TOSIN_PRICE].ToString()));
                chartTradeAnaly.Series["기타금융"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GITA_PRICE].ToString()));
                chartTradeAnaly.Series["은행"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BANK_PRICE].ToString()));
                chartTradeAnaly.Series["연기금"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.YEONGI_PRICE].ToString()));
                chartTradeAnaly.Series["사모"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.SAMO_PRICE].ToString()));
                chartTradeAnaly.Series["기법"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BUBIN_PRICE].ToString()));
                chartTradeAnaly.Series["기외"].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.IOFORE_PRICE].ToString()));

                pt++;
            }
            //_lastVolume = Int64.Parse(chartTradeAnaly.Series["거래량"].Points[chartTradeAnaly.Series["거래량"].Points.Count - 1].YValues[0].ToString());

            chartTradeAnaly.ChartAreas["Price"].AxisY.Minimum = low - (low * 0.1);
            chartTradeAnaly.ChartAreas["Price"].AxisY.Maximum = high + (high * 0.1);
            chartTradeAnaly.ChartAreas["Trade"].AxisX.IsLabelAutoFit = true;

            }
   
        #endregion

        #region Func
        private void SetStockCode()
        {
            lblStockCode.Text = _stockCode.STOCK_CODE;
            lblStockName.Text = _stockCode.STOCK_NAME;
        }

        private void GetAnalyOpt10059(String stockCode)
        {
            DataSet ds;
            KiwoomQuery oKiwoomQuery = new KiwoomQuery();

            ds = oKiwoomQuery.p_NuOPT10059PriceQuery("1", stockCode, CDateTime.FormatDate(dtpFromDate.Text), CDateTime.FormatDate(dtpToDate.Text), false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                MessageBox.Show("내역이 존재하지 않습니다.");
                return;
            }

            DisplayChart(ds);

            ds.Reset();
        }
        #endregion
        

        private void btnView_Click(object sender, EventArgs e)
        {
            GetAnalyOpt10059(lblStockCode.Text);
          //  GetAnalyOpt10059("088910");
        }

        private void chkIoFore_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            { chartTradeAnaly.Series[((CheckBox)sender).Text].Enabled = true; }
            else
            { chartTradeAnaly.Series[((CheckBox)sender).Text].Enabled = false; }
        }
  

    }
}
