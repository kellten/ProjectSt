﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using PaikRichStock.Common;

namespace Chart
{
    public partial class frmChart : Form
    {
        //private clsChart _clsChart = new clsChart();
        private PaikRichStock.Common.ucMainStockVer2 _MainStock;
        private string _stockCode = "";
        System.Windows.Forms.DataVisualization.Charting.Chart _baseChart;

        //public delegate void OnDelEventReturn10081ResultDt(DataSet ds);
        //public delegate void OnDelEventReturnRealTime(DataSet ds);    

        //public delegate void AddDataDelegate();
        //public AddDataDelegate addDataDel;        
        
        public PaikRichStock.Common.ucMainStockVer2 MainStock
        {
            set
            {
                _MainStock = value;

                CheckForIllegalCrossThreadCalls = false;
                _MainStock.OnReceiveRealData_Volume -= OnEventReturnRealTime;
                _MainStock.OnReceiveRealData_Volume -= OnEventReturnRealTime;
                _MainStock.OnReceiveRealData_Volume -= OnEventReturnRealTime;
                _MainStock.OnReceiveRealData_Volume -= OnEventReturnRealTime;
                _MainStock.OnReceiveRealData_Volume += OnEventReturnRealTime;
            }
        }
        
        public frmChart()
        {
            InitializeComponent();
        }

        private async Task DoAsyncOpt100081(string stockCode )
        {
            string stdDate = DateTime.Now.ToString("yyyyMMdd");

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_opt10081NewEventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted)
                    return;

                onEventReturn10081ResultDt(d);
                _MainStock.OnReceiveTrData_opt10081New -= handler;
                tcs.SetResult(true);
            };
            _MainStock.OnReceiveTrData_opt10081New += handler;
            _MainStock.Opt10081New_OnReceiveTrData(stockCode, _MainStock.GetStockInfo(stockCode), stdDate);
            await tcs.Task;
        }

        private async Task DoAsyncOpt100082(string stockCode)
        {
            string stdDate = DateTime.Now.ToString("yyyyMMdd");

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_opt10082EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted)
                    return;
                
                onEventReturn10081ResultDt(d);
                _MainStock.OnReceiveTrData_opt10082 -= handler;
                tcs.SetResult(true);
            };

            _MainStock.OnReceiveTrData_opt10082 += handler;
            _MainStock.Opt10082_OnReceiveTrData(stockCode, _MainStock.GetStockInfo(stockCode), stdDate);
            await tcs.Task;
        }

        Task _tOpt10081;
        Task _tOpt10082;

        public void GetChartData(string stockCode)
        {
            //this._clsChart.MainStock = _MainStock;
            //this._clsChart.GetOpt10081(stockCode);
            _stockCode = stockCode;
            this.Text = _MainStock.GetStockInfo(stockCode) + "(" + stockCode + ")";
            //_tOpt10081 = DoAsyncOpt100081(stockCode);
            var tFinance = DoFinanceTask();
            tFinance.Dispose();
            tFinance = null;
            InitChart();
            rbDay.Checked = true;

            //this._clsChart.OnEventReturn10081ResultDt += new clsChart.OnEvent10081ResultDtEventHandler(this.onEventReturn10081ResultDt);
            //this._clsChart.OnEventReturnRealTime += new clsChart.OnEventReturnRealTimeEventHandler(this.OnEventReturnRealTime);
               
        }

        public void CallMdiGetChartData(string stockCode)
        {
            //this._clsChart.OnEventReturn10081ResultDt -= new clsChart.OnEvent10081ResultDtEventHandler(this.onEventReturn10081ResultDt);
            //this._clsChart.OnEventReturn10081ResultDt -= new clsChart.OnEvent10081ResultDtEventHandler(this.onEventReturn10081ResultDt);
            //this._clsChart.OnEventReturnRealTime -= new clsChart.OnEventReturnRealTimeEventHandler(this.OnEventReturnRealTime);
            //this._clsChart.OnEventReturnRealTime -= new clsChart.OnEventReturnRealTimeEventHandler(this.OnEventReturnRealTime);
            //this._clsChart.MainStock = _MainStock;
            //this._clsChart.GetOpt10081(stockCode);
            _stockCode = stockCode;
            this.Text = _MainStock.GetStockInfo(stockCode) + "(" + stockCode + ")";
            var tFinance = DoFinanceTask();
            tFinance.Dispose();
            tFinance = null;
            rbDay.Checked = true;

            //_tOpt10081 = DoAsyncOpt100081(stockCode);
            //this._clsChart.OnEventReturn10081ResultDt += new clsChart.OnEvent10081ResultDtEventHandler(this.onEventReturn10081ResultDt);
            //this._clsChart.OnEventReturnRealTime += new clsChart.OnEventReturnRealTimeEventHandler(this.OnEventReturnRealTime);

        }       

        private void InitChart()
        {

            this._baseChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            
            chartArea1.Name = "ChartArea1";
            chartArea2.Name = "Volume";
            chartArea3.Name = "RSI";
            
            chartArea2.AxisX.Enabled = AxisEnabled.False;
            chartArea3.AxisX.Enabled = AxisEnabled.False;

            this._baseChart.ChartAreas.Clear();
            this._baseChart.Series.Clear();
            this._baseChart.ChartAreas.Add(chartArea1);
            this._baseChart.ChartAreas.Add(chartArea2);
            this._baseChart.ChartAreas.Add(chartArea3);
            
            //_baseChart.ChartAreas["Volume"].AlignWithChartArea = System.Windows.Forms.DataVisualization.Charting.al

            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Name = "캔들";
            series1.YValuesPerPoint = 4;

            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = Color.Magenta  ;
            series2.Name = "20MA라인";
            
            series3.ChartArea = "Volume";
            series3.Name = "거래량";

            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = Color.Purple  ;
            series4.Name = "3MA라인";

            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = Color.YellowGreen;
            series5.Name = "5MA라인";

            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = Color.Green;
            series6.Name = "10MA라인";

            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = Color.Pink ;
            series7.Name = "60MA라인";

            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = Color.Blue;
            series8.Name = "120MA라인";

            series9.YValuesPerPoint = 4;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            series9.Color = Color.Transparent;
            series9.BorderColor = Color.Black;
            series9.Name = "볼밴";

            series10.YValuesPerPoint = 4;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            series10.Color = Color.Transparent;
            series10.BorderColor = Color.DarkGray;
            series10.Name = "ENV";

            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series11.Color = Color.DeepSkyBlue;
            series11.Name = "220MA라인";

            series12.ChartArea = "RSI";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series12.Color = Color.Red;
            series12.Name = "RSI";

            this._baseChart.ChartAreas["Volume"].AxisX.MajorGrid.Enabled = false;
            this._baseChart.ChartAreas["Volume"].AxisX.MajorTickMark.Enabled = false;
            this._baseChart.ChartAreas["Volume"].AxisX.LineWidth = 0;
            this._baseChart.ChartAreas["Volume"].AxisY.MajorGrid.Enabled = false;
            this._baseChart.ChartAreas["Volume"].AxisY.MajorTickMark.Enabled = false;
            this._baseChart.ChartAreas["Volume"].AxisY.LineWidth = 0;

            this._baseChart.ChartAreas["RSI"].AxisY.Maximum = 100;
            this._baseChart.ChartAreas["RSI"].AxisY.Minimum = 0;
            this._baseChart.ChartAreas["RSI"].AxisY.IntervalOffset = 30;
            this._baseChart.ChartAreas["RSI"].AxisY.Interval = 40;
            this._baseChart.ChartAreas["RSI"].AxisX.MajorGrid.Enabled = false;
            this._baseChart.ChartAreas["RSI"].AxisX.MajorTickMark.Enabled = false;
            this._baseChart.ChartAreas["RSI"].AxisX.LineWidth = 0;

            this._baseChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            this._baseChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Position = _baseChart.ChartAreas["ChartArea1"].AxisX.Maximum;
            this._baseChart.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            this._baseChart.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            this._baseChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            //_baseChart.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            this._baseChart.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Days;
            this._baseChart.ChartAreas["ChartArea1"].AxisX.Interval = 10;
            this._baseChart.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            this._baseChart.ChartAreas["Volume"].CursorX.IsUserEnabled = true;
            this._baseChart.ChartAreas["Volume"].CursorX.IsUserSelectionEnabled = true;
            this._baseChart.ChartAreas["Volume"].AxisX.ScaleView.Zoomable = true;

            this._baseChart.ChartAreas["RSI"].CursorX.IsUserEnabled = true;
            this._baseChart.ChartAreas["RSI"].CursorX.IsUserSelectionEnabled = true;
            this._baseChart.ChartAreas["RSI"].AxisX.ScaleView.Zoomable = true;

            this._baseChart.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            this._baseChart.ChartAreas["Volume"].InnerPlotPosition.Auto = true;
            this._baseChart.ChartAreas["RSI"].InnerPlotPosition.Auto = true;

            this._baseChart.ChartAreas["Volume"].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
            this._baseChart.ChartAreas["Volume"].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;
            this._baseChart.ChartAreas["Volume"].AlignWithChartArea = "ChartArea1";

            this._baseChart.ChartAreas["RSI"].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
            this._baseChart.ChartAreas["RSI"].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;
            this._baseChart.ChartAreas["RSI"].AlignWithChartArea = "ChartArea1";

            this._baseChart.ChartAreas["ChartArea1"].Position.Auto = false;
            this._baseChart.ChartAreas["ChartArea1"].Position.Height = 75;
            this._baseChart.ChartAreas["ChartArea1"].Position.Width = 100;
            this._baseChart.ChartAreas["ChartArea1"].Position.X = 0;
            this._baseChart.ChartAreas["ChartArea1"].Position.Y = 0;

            this._baseChart.ChartAreas["Volume"].Position.Auto = false;
            this._baseChart.ChartAreas["Volume"].Position.Height = 15;
            this._baseChart.ChartAreas["Volume"].Position.Width = 100;
            this._baseChart.ChartAreas["Volume"].Position.X = _baseChart.ChartAreas["ChartArea1"].Position.X;
            this._baseChart.ChartAreas["Volume"].Position.Y = _baseChart.ChartAreas["ChartArea1"].Position.Y + _baseChart.ChartAreas["ChartArea1"].Position.Height;

            this._baseChart.ChartAreas["RSI"].Position.Auto = false;
            this._baseChart.ChartAreas["RSI"].Position.Height = 10;
            this._baseChart.ChartAreas["RSI"].Position.Width = 100;
            this._baseChart.ChartAreas["RSI"].Position.X = _baseChart.ChartAreas["ChartArea1"].Position.X;
            this._baseChart.ChartAreas["RSI"].Position.Y = _baseChart.ChartAreas["Volume"].Position.Y + _baseChart.ChartAreas["Volume"].Position.Height;

            this._baseChart.Series.Add(series1);
            this._baseChart.Series.Add(series2);
            this._baseChart.Series.Add(series3);
            this._baseChart.Series.Add(series4);
            this._baseChart.Series.Add(series5);
            this._baseChart.Series.Add(series6);
            this._baseChart.Series.Add(series7);
            this._baseChart.Series.Add(series8);
            this._baseChart.Series.Add(series9);
            this._baseChart.Series.Add(series10);
            this._baseChart.Series.Add(series11);
            this._baseChart.Series.Add(series12);

            this._baseChart.Series["캔들"].ToolTip = "#AXISLABEL";
            this._baseChart.Series["캔들"].IsXValueIndexed = false;
            this._baseChart.Series["캔들"].XValueType = ChartValueType.DateTime;

            this._baseChart.Width = this.Width - 20;
            this._baseChart.Height = this.Height - 5;
            splitContainer1.Panel2.Controls.Add(_baseChart);
            _baseChart.Dock = DockStyle.Fill;

            this._baseChart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Chart_MouseClick);
            this._baseChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Chart_MouseMove);
            this._baseChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Chart_MouseDown);
            this._baseChart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(Chart_AxisViewChanged);
            //this._baseChart.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Chart_MouseWheel);
            this._baseChart.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Chart_MouseDoubleClick);

            splitContainer1.Panel2.Controls.Add(_baseChart);
            _baseChart.Dock = DockStyle.Fill;

            chkEnv.Checked = false;
        }
        
        private void Chart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            int start = (int)e.Axis.ScaleView.ViewMinimum;
            int end = (int)e.Axis.ScaleView.ViewMaximum;
                       
            double[] temp = _baseChart.Series["캔들"].Points.Where((x, i) => i >= start && i <= end).Select(x => x.YValues[0]).ToArray();
            double ymin = temp.Min();
            double ymax = temp.Max();

            this._baseChart.ChartAreas["ChartArea1"].AxisY.Minimum = ymin - (ymin * 0.1);
            this._baseChart.ChartAreas["ChartArea1"].AxisY.Maximum = ymax + (ymax * 0.1);           
        }

        private DataSet _ds;
        private int _month = 0;
        private double _prevLow;
        private double _prevHigh;
        private Int64 _lastVolume;

        private void DisplayChart(DataSet ds, int month)
        {
            Double[] arrP = new Double[1];
            int pt = 0;
            DataView dv = new DataView(ds.Tables[0]);

            dv.Sort = "일자 asc";
            dv.RowFilter = "일자 >=" + DateTime.Now.AddMonths(-month).ToString("yyyyMMdd");

            int high = 0;
            int low = 0;

            try {

                for (int ix = _baseChart.Series.Count - 1;   ix >= 0 ; ix--)
                {
                    if (_baseChart.Series[ix].Name.IndexOf(DateTime.Now.ToString("yyyyMMdd")) == -1)
                        this._baseChart.Series[ix].Points.Clear();
                    else
                    {
                        if (_baseChart.Series[ix].Points.Count > 0) { 
                            arrP[arrP.Length - 1] = _baseChart.Series[ix].Points[0].YValues[0];
                            Array.Resize(ref arrP, arrP.Length + 1);
                        }
                        this._baseChart.Series.RemoveAt(ix);
                    }
                }
            }
            catch (System.NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            
            //DataSet dsTmp = new DataSet();

            //using (DataAccess dataAcc = new DataAccess())
            //{
            //    dsTmp = dataAcc.p_stock_etc_query("1", _stockCode, "", null, null);
            //}           

            foreach (DataRowView dr in dv)
            {
                if (low == 0)
                {
                    high = int.Parse(dr["고가"].ToString());
                }
                else
                {
                    if (high < int.Parse(dr["고가"].ToString()))
                    {
                        high = int.Parse(dr["고가"].ToString());
                    }
                }


                if (low == 0)
                {
                    low = int.Parse(dr["저가"].ToString());
                }
                else
                {
                    if (low > int.Parse(dr["저가"].ToString()))
                    {
                        low = int.Parse(dr["저가"].ToString());
                    }
                }

                this._baseChart.Series["캔들"].Points.AddXY((object)dr["일자"], int.Parse(dr["고가"].ToString()));
                this._baseChart.Series["캔들"].Points[pt].YValues[1] = int.Parse(dr["저가"].ToString());
                this._baseChart.Series["캔들"].Points[pt].YValues[2] = int.Parse(dr["시가"].ToString());
                this._baseChart.Series["캔들"].Points[pt].YValues[3] = int.Parse(dr["현재가"].ToString());                

                //0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락
                switch (dr["수정주가구분"].ToString().Trim())
	            {
                    case "1":
                        this._baseChart.Series["캔들"].Points[pt].Label = "유상증자";
                        break;
                    case "2":
                        this._baseChart.Series["캔들"].Points[pt].Label = "무상증자";
                    break;
                    case "4":
                        this._baseChart.Series["캔들"].Points[pt].Label = "배당락";
                        break;
                    case "8":
                        this._baseChart.Series["캔들"].Points[pt].Label = "액면분할";
                        break;
                    case "16":
                        this._baseChart.Series["캔들"].Points[pt].Label = "액면병합";
                        break;
                    case "32":
                        this._baseChart.Series["캔들"].Points[pt].Label = "기업합병";
                        break;
                    case "64":
                        this._baseChart.Series["캔들"].Points[pt].Label = "감자";
                        break;
                    case "256":
                        this._baseChart.Series["캔들"].Points[pt].Label = "권리락";
                        break;                    
		            default:
                        break;
	            }

                //foreach (DataRow drTmp in dsTmp.Tables[0].Rows)
                //{
                //    if (dr["일자"].ToString().Trim() == drTmp["STOCK_DATE"].ToString().Trim())
                //    {
                //        this._baseChart.Series["캔들"].Points[pt].Label = drTmp["COMMENT"].ToString().Trim();
                //    }
                //}


                this._baseChart.Series["거래량"].Points.AddXY(dr["일자"], double.Parse(dr["거래량"].ToString()));
                int curCnt = this._baseChart.Series["거래량"].Points.Count - 1;
                this._baseChart.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Red;

                if (curCnt > 0) { 
                    Double preVolume = this._baseChart.Series["거래량"].Points[curCnt - 1].YValues[0];
                    Double CurVolume = this._baseChart.Series["거래량"].Points[curCnt].YValues[0];

                    if (preVolume < CurVolume)
                    {
                        this._baseChart.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Red;
                    }
                    else
                    {
                        this._baseChart.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Blue;
                    }
                }
                                                                
                if (int.Parse(dr["시가"].ToString()) > int.Parse(dr["현재가"].ToString()))
                {
                    this._baseChart.Series["캔들"].Points[pt].Color = System.Drawing.Color.Blue;
                }
                else
                {
                    this._baseChart.Series["캔들"].Points[pt].Color = System.Drawing.Color.Red;
                }

                pt++;
            }
            _lastVolume = Int64.Parse(_baseChart.Series["거래량"].Points[_baseChart.Series["거래량"].Points.Count - 1].YValues[0].ToString());

            this._baseChart.ChartAreas["ChartArea1"].AxisY.Minimum = low - (low * 0.1);
            this._baseChart.ChartAreas["ChartArea1"].AxisY.Maximum = high + (high * 0.1);
            this._baseChart.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = true;

            DataCalcAll();

            if (Cls.IsJangTime())
            {
                if (dv.Count > 0)
                {
                    dv.Sort = "일자 desc";

                    DataRowView dr = dv[0];
                    if (dr["전일종가"].ToString() != "" && Convert.ToDouble(dr["고가"].ToString()) < Convert.ToDouble(dr["전일종가"].ToString()))
                    {
                        dgInfo.Rows[0].Cells[고가.Index].Value = Convert.ToDouble(dr["고가"].ToString()) * -1;
                    }
                    else
                    {
                        dgInfo.Rows[0].Cells[고가.Index].Value = Convert.ToDouble(dr["고가"].ToString());
                    }

                    if (dr["전일종가"].ToString() != "" && Convert.ToDouble(dr["저가"].ToString()) < Convert.ToDouble(dr["전일종가"].ToString()))
                    {
                        dgInfo.Rows[0].Cells[저가.Index].Value = Convert.ToDouble(dr["저가"].ToString()) * -1;
                    }
                    else
                    {
                        dgInfo.Rows[0].Cells[저가.Index].Value = Convert.ToDouble(dr["저가"].ToString());
                    }

                    if (dr["전일종가"].ToString() != "" && Convert.ToDouble(dr["시가"].ToString()) < Convert.ToDouble(dr["전일종가"].ToString()))
                    {
                        dgInfo.Rows[0].Cells[시가.Index].Value = Convert.ToDouble(dr["시가"].ToString()) * -1;
                    }
                    else
                    {
                        dgInfo.Rows[0].Cells[시가.Index].Value = Convert.ToDouble(dr["시가"].ToString());
                    }

                    if (dr["전일종가"].ToString() != "" && Convert.ToDouble(dr["현재가"].ToString()) < Convert.ToDouble(dr["전일종가"].ToString()))
                    {
                        dgInfo.Rows[0].Cells[현재가.Index].Value = Convert.ToDouble(dr["현재가"].ToString()) * -1;
                    }
                    else
                    {
                        dgInfo.Rows[0].Cells[현재가.Index].Value = Convert.ToDouble(dr["현재가"].ToString());
                    }
                    if (dr["전일종가"].ToString() != "")
                    {
                        dgInfo.Rows[0].Cells[등락율.Index].Value = (Convert.ToDouble(dr["현재가"].ToString()) - Convert.ToDouble(dr["전일종가"].ToString())) / Convert.ToDouble(dr["전일종가"].ToString()) * 100;
                    }

                    dgInfo.Rows[0].Cells[거래량.Index].Value = dr["거래량"];
                    if (dr["전일종가"].ToString() != "")
                    {
                        dgInfo.Rows[0].Cells[전일대비.Index].Value = Convert.ToInt32(dr["현재가"].ToString()) - Convert.ToInt32(dr["전일종가"].ToString());
                    }
                    Cls.ChangeColor(dgInfo.Rows[0], "A", 0, 현재가.Index, 현재가.Index, 시가.Index, 고가.Index, 저가.Index, 등락율.Index, 전일대비.Index, 거래량대비.Index, 영업이익.Index, 당기순이익.Index, 외인5.Index, 기관5.Index);
                }
            }
            for (int i = 0; i < arrP.Length - 1; i++)
            {
                LineAdd(arrP[i]);
            }
            _prevLow = low - (low * 0.1);
            _prevHigh = high + (high * 0.1);
        }

        private void SettingFinance(DataSet ds)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            DataRow dr1 = ds.Tables[1].Rows[0];

            using (DataGridViewRow dgr = dgInfo.Rows[0])
            {
                dgr.Cells[종목코드.Index].Value = _stockCode;
                dgr.Cells[종목명.Index].Value = dr["STOCK_NAME"].ToString();
                dgr.Cells[현재가.Index].Value = dr["END_PRICE"].ToString();
                dgr.Cells[전일대비.Index].Value = dr["PRE_CONPARE"].ToString();
                dgr.Cells[등락율.Index].Value = dr["END_PRICE_RATE"].ToString();
                dgr.Cells[거래량.Index].Value = dr["VOLUME"].ToString();
                dgr.Cells[거래대금.Index].Value = dr["TRADING_VALUE"].ToString();
                dgr.Cells[거래량대비.Index].Value = dr["VOLUME_RATE"].ToString();
                dgr.Cells[시가총액.Index].Value = dr["STOCK_TOTAL_P"].ToString();
                dgr.Cells[시가.Index].Value = dr["S_PRICE"].ToString();
                dgr.Cells[고가.Index].Value = dr["H_PRICE"].ToString();
                dgr.Cells[저가.Index].Value = dr["L_PRICE"].ToString();
                dgr.Cells[PBR.Index].Value = dr["PBR"].ToString();
                dgr.Cells[영업이익.Index].Value = dr["O_PROFIT"].ToString();
                dgr.Cells[당기순이익.Index].Value = dr["P_PROFIT"].ToString();
                dgr.Cells[외인5.Index].Value = dr1["F_TU"].ToString();
                dgr.Cells[기관5.Index].Value = dr1["K_TU"].ToString();
                dgr.Cells[전일거래량.Index].Value = dr["VOLUME"].ToString();
                dgr.Cells[신용.Index].Value = dr["CREDIT_RATE"].ToString();
                dgr.Cells[최저MA.Index].Value = dr["LOWEST_MA"].ToString();

                for (int col = 1; col < dgInfo.ColumnCount; col++)
                {
                    Cls.ChangeColor(dgr, "A" , 0 , 현재가.Index, 현재가.Index, 시가.Index, 고가.Index, 저가.Index, 등락율.Index, 전일대비.Index, 거래량대비.Index ,영업이익.Index, 당기순이익.Index, 외인5.Index, 기관5.Index);
                    Cls.ChangeColor(dgr, "D", 1.5, PBR.Index);
                    Cls.ChangeColor(dgr, "D", 7, 신용.Index);

                    string format;
                    format = "{0:##,##0}";
                    if (col == 등락율.Index || col == PBR.Index || col == 신용.Index) format = "{0:#0.00}";
                    if (col == 전일거래량.Index || col == 최저MA.Index) format = "";
                    dgr.Cells[col].Value = Cls.ToNumber(dgr.Cells[col].Value.ToString(), Type.GetType("System.Double"), format);
                }
            }
        }

        private Task DoFinanceTask()
        {
            DataSet ds;
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            
            using (DataAccess da = new DataAccess())
            {
                ds = da.p_stock_finance_query("1", _stockCode, false);
                SettingFinance(ds);
            }
            tcs.SetResult(true);
            return tcs.Task;
        }

        private void DataCalcAll()
        {
            btnPrev.Enabled = false;
            btnNext.Enabled = false;

            var cts = new CancellationTokenSource();
            var parent = new Task(() =>
            {
                if (_baseChart.Series["캔들"].Points.Count >= 3)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "3", "캔들:Y4", "3MA라인"), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (_baseChart.Series["캔들"].Points.Count >= 5)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "5", "캔들:Y4", "5MA라인"), TaskCreationOptions.AttachedToParent
                    ).Start();
                
                if (_baseChart.Series["캔들"].Points.Count >= 10)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "10", "캔들:Y4", "10MA라인"), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (_baseChart.Series["캔들"].Points.Count >= 20)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "20", "캔들:Y4", "20MA라인"), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (_baseChart.Series["캔들"].Points.Count >= 60)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "60", "캔들:Y4", "60MA라인"), TaskCreationOptions.AttachedToParent
                    ).Start();
                
                if (_baseChart.Series["캔들"].Points.Count >= 120)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "120", "캔들:Y4", "120MA라인"), TaskCreationOptions.AttachedToParent
                    ).Start();
                
                if (_baseChart.Series["캔들"].Points.Count >= 220)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "220", "캔들:Y4", "220MA라인"), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (_baseChart.Series["캔들"].Points.Count >= 20)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.Envelopes, "20,20", "캔들:Y4", "ENV:Y,ENV:Y2"), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (_baseChart.Series["캔들"].Points.Count >= 40)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.BollingerBands, "40,2", "캔들:Y4", "볼밴:Y,볼밴:Y2"), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (_baseChart.Series["캔들"].Points.Count >= 14)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.RelativeStrengthIndex, "14", "캔들:Y4", "RSI:Y"), TaskCreationOptions.AttachedToParent
                    ).Start();
            }, cts.Token);

            var cwt = parent.ContinueWith(// parent Task가끝나면수행할Task 연결
                parentTask => {
                    SettingToolTip();
                });
            parent.ContinueWith(// 예외발생시
                task => Console.WriteLine("Task threw: " + task.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted);
            parent.ContinueWith(task => Console.WriteLine("Task was canceled"), TaskContinuationOptions.OnlyOnCanceled);
            parent.Start();
            try { 
                parent.Wait();
            }
            catch
            {
                cts.Cancel();
            }
        }

        private void SettingToolTip()
        {
            try
            {
                string pre_e_p;
                string pre_e_v;
                string tooltipStr;
                string stockDate;
                string h_p;
                string l_p;
                string s_p;
                string e_p;
                string volume;
                string line3;
                string line5;
                string line10;
                string line20;
                string line60;
                string line120;
                string line220;
                string bbup;
                string bbdown;
                string rsi;

                for (int ix = 0; ix < this._baseChart.Series["캔들"].Points.Count; ix++)
                {
                    pre_e_p = "0";
                    pre_e_v = "0";
                    if (ix > 0)
                    {
                        pre_e_p = _baseChart.Series["캔들"].Points[ix - 1].YValues[3].ToString();
                        pre_e_v = _baseChart.Series["거래량"].Points[ix - 1].YValues[0].ToString();
                    }
                    stockDate = _baseChart.Series["캔들"].Points[ix].AxisLabel;
                    h_p = _baseChart.Series["캔들"].Points[ix].YValues[0].ToString();
                    l_p = _baseChart.Series["캔들"].Points[ix].YValues[1].ToString();
                    s_p = _baseChart.Series["캔들"].Points[ix].YValues[2].ToString();
                    e_p = _baseChart.Series["캔들"].Points[ix].YValues[3].ToString();
                    volume = _baseChart.Series["거래량"].Points[ix].YValues[0].ToString();
                    line3 = "";
                    line5 = "";
                    line10 = "";
                    line20 = "";
                    line60 = "";
                    line120 = "";
                    line220 = "";
                    bbup = "";
                    bbdown = "";
                    rsi = "";

                    if (_baseChart.Series["3MA라인"].Points.Count >= 3)
                    {
                        line3 = _baseChart.Series["3MA라인"].Points[ix].YValues[0].ToString();
                    }
                    if (_baseChart.Series["5MA라인"].Points.Count >= 5)
                    {
                        line5 = _baseChart.Series["5MA라인"].Points[ix].YValues[0].ToString();
                    }
                    if (_baseChart.Series["10MA라인"].Points.Count >= 10)
                    {
                        line10 = _baseChart.Series["10MA라인"].Points[ix].YValues[0].ToString();
                    }
                    if (_baseChart.Series["20MA라인"].Points.Count >= 20)
                    {
                        line20 = _baseChart.Series["20MA라인"].Points[ix].YValues[0].ToString();
                    }
                    if (_baseChart.Series["60MA라인"].Points.Count >= 60)
                    {
                        line60 = _baseChart.Series["60MA라인"].Points[ix].YValues[0].ToString();
                    }
                    if (_baseChart.Series["120MA라인"].Points.Count >= 120)
                    {
                        line120 = _baseChart.Series["120MA라인"].Points[ix].YValues[0].ToString();
                    }
                    if (_baseChart.Series["220MA라인"].Points.Count >= 220)
                    {
                        line220 = _baseChart.Series["220MA라인"].Points[ix].YValues[0].ToString();
                    }
                    if (_baseChart.Series["볼밴"].Points.Count >= 40)
                    {
                        bbup = _baseChart.Series["볼밴"].Points[ix].YValues[0].ToString();
                        bbdown = _baseChart.Series["볼밴"].Points[ix].YValues[1].ToString();
                    }
                    if (_baseChart.Series["RSI"].Points.Count >= 14)
                    {
                        rsi = _baseChart.Series["RSI"].Points[ix].YValues[0].ToString();
                    }

                    tooltipStr = "일자: " + PaikRichStock.Common.CDateTime.FormatDate(stockDate, ".");
                    tooltipStr += "\r\n" + "시가: " + String.Format("{0:##,##0}", Convert.ToInt32(s_p)) + "   " + CalcRate(pre_e_p, s_p).ToString() + "%"
                                + "\r\n" + "고가: " + String.Format("{0:##,##0}", Convert.ToInt32(h_p)) + "   " + CalcRate(pre_e_p, h_p).ToString() + "%"
                                + "\r\n" + "저가: " + String.Format("{0:##,##0}", Convert.ToInt32(l_p)) + "   " + CalcRate(pre_e_p, l_p).ToString() + "%"
                                + "\r\n" + "종가: " + String.Format("{0:##,##0}", Convert.ToInt32(e_p)) + "   " + CalcRate(pre_e_p, e_p).ToString() + "%"
                                + "\r\n"
                                + "거래량: " + String.Format("{0:##,##0}", Convert.ToInt64(volume)) + "   " + (CalcRate(pre_e_v, volume) + 100).ToString() + "%"
                                + "\r\n" + "\r\n";
                    tooltipStr += "이평선========================";

                    if (line3 != "")
                    {
                        tooltipStr += "\r\n" + "3: " + String.Format("{0:##,##0.00}", Convert.ToDouble(line3)) + "   " + CalcRate(e_p, line3).ToString() + "%";
                    }
                    if (line5 != "")
                    {
                        tooltipStr += "\r\n" + "5: " + String.Format("{0:##,##0.00}", Convert.ToDouble(line5)) + "   " + CalcRate(e_p, line5).ToString() + "%";
                    }
                    if (line10 != "")
                    {
                        tooltipStr += "\r\n" + "10: " + String.Format("{0:##,##0.00}", Convert.ToDouble(line10)) + "   " + CalcRate(e_p, line10).ToString() + "%";
                    }
                    if (line20 != "")
                    {
                        tooltipStr += "\r\n" + "20: " + String.Format("{0:##,##0.00}", Convert.ToDouble(line20)) + "   " + CalcRate(e_p, line20).ToString() + "%";
                    }
                    if (line60 != "")
                    {
                        tooltipStr += "\r\n" + "60: " + String.Format("{0:##,##0.00}", Convert.ToDouble(line60)) + "   " + CalcRate(e_p, line60).ToString() + "%";
                    }
                    if (line120 != "")
                    {
                        tooltipStr += "\r\n" + "120: " + String.Format("{0:##,##0.00}", Convert.ToDouble(line120)) + "   " + CalcRate(e_p, line120).ToString() + "%";
                    }
                    if (line220 != "")
                    {
                        tooltipStr += "\r\n" + "220: " + String.Format("{0:##,##0.00}", Convert.ToDouble(line220)) + "   " + CalcRate(e_p, line220).ToString() + "%";
                    }

                    tooltipStr += "\r\n" + "\r\n";
                    tooltipStr += "보조지표======================";

                    if (bbup != "")
                    {
                        tooltipStr += "\r\n" + "볼밴 상한: " + String.Format("{0:##,##0.00}", Convert.ToDouble(bbup)) + "   " + CalcRate(e_p, bbup).ToString() + "%";
                        tooltipStr += "\r\n" + "볼밴 하한: " + String.Format("{0:##,##0.00}", Convert.ToDouble(bbdown)) + "   " + CalcRate(e_p, bbdown).ToString() + "%";
                    }
                    if (rsi != "")
                    {
                        tooltipStr += "\r\n" + "RSI: " + String.Format("{0:#0.00}", Convert.ToDouble(rsi));
                    }


                    for (int i = 0; i < _baseChart.Series.Count; i++)
                    {
                        if (this._baseChart.Series[i].Points.Count > ix)
                        {
                            this._baseChart.Series[i].Points[ix].ToolTip = tooltipStr;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
            }
        }

        private void DataCalc(FinancialFormula formula, string period, string candle, string line)
        {
            try { 
                this._baseChart.DataManipulator.FinancialFormula(formula, period, candle, line);
            }
            catch (System.IndexOutOfRangeException ex) { Console.WriteLine(ex.Message); }
            catch (System.NullReferenceException ex) { Console.WriteLine(ex.Message); }
        }

        private double CalcRate(string value1, string value2)
        {
            if (value1 == "" || value2 == "") return 0;

            double calcValue = 0;

            calcValue = Math.Round((double.Parse(value2) - double.Parse(value1)) / double.Parse(value1) * 100, 2);

            if(calcValue > 0)
            {
                return Math.Round(calcValue, 2);
            }            
            else
            {
                return Math.Round(calcValue , 2);
            }            
        }

        private void OnEventReturnRealTime(DataSet value)
        {
            //_baseChart.Invoke(addDataDel);
            if (this.IsDisposed) { return; }
            if (_tOpt10081 == null) { return; }
            if (!_tOpt10081.IsCompleted) { return; }
            if (this._baseChart == null) { return; }
            if (!_stockCode.Equals(value.Tables[0].Rows[0]["STOCK_CODE"].ToString())) return;
            try
            {
                this._baseChart.Series["캔들"].Points[this._baseChart.Series["캔들"].Points.Count - 1].YValues[3] = int.Parse(value.Tables[0].Rows[0]["현재가"].ToString().Replace("-", ""));
                this._baseChart.Series["캔들"].Points[this._baseChart.Series["캔들"].Points.Count - 1].Label = value.Tables[0].Rows[0]["현재가"].ToString().Replace("-", "");

                if (rbWeek.Checked == true)
                {
                    this._baseChart.Series["거래량"].Points[this._baseChart.Series["거래량"].Points.Count - 1].YValues[0] = _lastVolume  + int.Parse(value.Tables[0].Rows[0]["누적거래량"].ToString());
                }
                else
                {
                    this._baseChart.Series["거래량"].Points[this._baseChart.Series["거래량"].Points.Count - 1].YValues[0] = int.Parse(value.Tables[0].Rows[0]["누적거래량"].ToString());
                }
                

                if (this._baseChart.Series["거래량"].Points.Count > 1)
                {
                    if (int.Parse(_baseChart.Series["거래량"].Points[this._baseChart.Series["거래량"].Points.Count - 2].YValues[0].ToString()) < int.Parse(value.Tables[0].Rows[0]["누적거래량"].ToString()))
                    {
                        _baseChart.Series["거래량"].Points[this._baseChart.Series["거래량"].Points.Count - 1].Color = System.Drawing.Color.Red;
                    }
                    else {
                        _baseChart.Series["거래량"].Points[this._baseChart.Series["거래량"].Points.Count - 1].Color = System.Drawing.Color.Blue;
                    }
                }

                using (DataGridViewRow dgr = dgInfo.Rows[0])
                {
                    dgr.Cells[현재가.Index].Value = Cls.ToNumber(value.Tables[0].Rows[0]["현재가"].ToString() , null , "{0:##,##0}");
                    dgr.Cells[시가.Index].Value = Cls.ToNumber(value.Tables[0].Rows[0]["시가"].ToString(), null , "{0:##,##0}");
                    dgr.Cells[고가.Index].Value = Cls.ToNumber(value.Tables[0].Rows[0]["고가"].ToString(), null , "{0:##,##0}");
                    dgr.Cells[저가.Index].Value = Cls.ToNumber(value.Tables[0].Rows[0]["저가"].ToString(), null, "{0:##,##0}");
                    dgr.Cells[거래량.Index].Value = Cls.ToNumber(value.Tables[0].Rows[0]["누적거래량"].ToString(), null, "{0:##,##0}");
                    dgr.Cells[거래대금.Index].Value = Cls.ToNumber(value.Tables[0].Rows[0]["누적거래대금"].ToString(), null, "{0:##,##0}");
                    dgr.Cells[전일대비.Index].Value = Cls.ToNumber(value.Tables[0].Rows[0]["전일대비"].ToString(), null, "{0:##,##0}");
                    dgr.Cells[등락율.Index].Value = Cls.ToNumber(value.Tables[0].Rows[0]["등락율"].ToString(), null, "{0:0.00}");
                    dgr.Cells[거래량대비.Index].Value = Cls.ToNumber(value.Tables[0].Rows[0]["전일거래량대비(비율)"].ToString(), null, "{0:0.00}");

                    Cls.ChangeColor(dgr, "A", 0, 현재가.Index, 시가.Index, 고가.Index, 저가.Index, 등락율.Index, 전일대비.Index, 거래량대비.Index);
                }
            }
            catch (Exception)
            {
                                
            }
        }



        private void onEventReturn10081ResultDt(DataSet value)
        {
            DataSet ds = value;

            _ds = ds;

            if (rbWeek.Checked == true) _month = 6 * 7;
            else if (rbMon.Checked == true) _month = 6 * 30;
            else _month = 6;

            DisplayChart(ds, _month);
            //_baseChart.Update();
        }

        private void onEventReturn10082ResultDt(DataSet value)
        {
            DataSet ds = value;

            _ds = ds;

            if (rbWeek.Checked == true) _month = 6 * 7;
            else if (rbMon.Checked == true) _month = 6 * 30;
            else _month = 6;

            DisplayChart(ds, _month);
            //_baseChart.Update();
        }

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        Double _curPrice;
        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);
            try { 
                this._baseChart.ChartAreas["ChartArea1"].CursorX.SetCursorPixelPosition(mousePoint, true);
                this._baseChart.ChartAreas["ChartArea1"].CursorY.SetCursorPixelPosition(mousePoint, true);
            

                var pos = e.Location;
                if (prevPosition.HasValue && pos == prevPosition.Value)
                    return;
                tooltip.RemoveAll();
                prevPosition = pos;
                var results = this._baseChart.HitTest(pos.X, pos.Y, false,
                                                ChartElementType.PlottingArea);

                foreach (var result in results)
                {
                    if (result.ChartElementType == ChartElementType.PlottingArea)
                    {
                        var xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                        double yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
                        _curPrice = yVal;
                        tooltip.Show(Math.Round(yVal, 2).ToString(), this._baseChart,
                                        pos.X, pos.Y - 21);
                    }
                }
            }
            catch (System.ArgumentException) { return; }
            catch (System.Exception) { return; }
        }

        void Chart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _baseChart.MouseMove -= Chart_MouseMove;
            _baseChart.MouseMove -= Chart_MouseMove;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                _baseChart.MouseMove -= Chart_MouseMove;
                _baseChart.MouseMove -= Chart_MouseMove;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    try
                    {
                        string stockDate;
                        Point mousePoint = new Point(e.X, e.Y);

                        var pos = e.Location;
                        var results = this._baseChart.HitTest(pos.X, pos.Y, false,
                                                     ChartElementType.DataPoint);

                        foreach (var result in results)
                        {
                            if (result.ChartElementType == ChartElementType.DataPoint)
                            {
                                stockDate = result.Series.Points[result.PointIndex].AxisLabel;
                                if (stockDate == "") continue;

                                this.Width = 1600;
                                splitContainer2.Panel2Collapsed = false;
                                btnCollapse.Visible = true;
                                SettingForm(_stockCode, stockDate);
                            }
                        }

                    }
                    catch (Exception) { }
                    finally { 
                        LineRemove(); 
                    }
                }
                _baseChart.MouseMove += Chart_MouseMove;

                
            }
            _baseChart.MouseMove += Chart_MouseMove;
        }

        public void SettingForm(string stockCode, string stockDate)
        {
            DoOpt100059(stockCode, stockDate);
            DartApi(stockCode, stockDate);
            //var cts = new CancellationTokenSource(1000);
            //var t100059 = new Task(() => DoOpt100059(stockCode, stockDate), cts.Token);
            //var tDart = new Task(() => DartApi(stockCode, stockDate), cts.Token);
            //t100059.Start();
            //tDart.Start();
            //t100059.Wait();
            //tDart.Wait();
        }

        private string[] _strBuySell = { "일자", "개인", "외인", "기관", "금융투자", "보험", "투신", "기타금융", "은행", "연기금등", "사모펀드", "국가", "기타법인", "내외국인" };
        void DoOpt100059(string stockCode, string stockDate)
        {
            DataSet dsOpt10059;
            dataGridView1.DataSource = null;
            ArrayParam arrParam = new ArrayParam();

            mySqlDbConn conn = new mySqlDbConn();
            arrParam.Clear();
            if (rbWeek.Checked)
                arrParam.Add("_QUERY" , "4");
            else
                arrParam.Add("_QUERY", "3");
            arrParam.Add("_STOCK_CODE", stockCode);
            arrParam.Add("_STOCK_DATE", stockDate);

            dsOpt10059 = conn.GetDataTableSp("p_stock_buysell_state_query", arrParam);
            dataGridView1.DataSource = dsOpt10059.Tables[0];

            for (int ix = 0; ix < _strBuySell.Length; ix++)
            {
                dataGridView1.Columns[ix].HeaderText = _strBuySell[ix];
                dataGridView1.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (ix >= 0 && ix < 4) dataGridView1.Columns[ix].Width = 63;
                else dataGridView1.Columns[ix].Width = 40;
            }

            for (int row = 0; row < dataGridView1.RowCount - 1; row++)
            {
                for (int col = 0; col < dataGridView1.ColumnCount; col++)
                {
                    dataGridView1.Rows[row].Cells[col].Style.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                    if (dataGridView1.Columns[col].HeaderText == "일자") continue;
                    if (dataGridView1.Rows[row].Cells[col].Value.ToString() == "") continue;
                    if (Convert.ToDouble(dataGridView1.Rows[row].Cells[col].Value.ToString()) < 0)
                    {
                        dataGridView1.Rows[row].Cells[col].Style.ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (Convert.ToDouble(dataGridView1.Rows[row].Cells[col].Value.ToString()) > 0)
                    {
                        dataGridView1.Rows[row].Cells[col].Style.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[row].Cells[col].Style.ForeColor = System.Drawing.Color.Empty;
                    }
                }
            }
        }

        void DartApi(string stockCode, string stockDate)
        {
            DataTable dt;
            dataGridView2.DataSource = null;

            CSharp.Common.Func comCls = new CSharp.Common.Func();
            dt = comCls.GetDartApi(stockCode, stockDate);
            if (dt == null)
            {
                return;
            }
            dataGridView2.DataSource = dt;


            for (int ix = 0; ix < dt.Columns.Count - 1; ix++)
            {
                if (ix == 3)
                {
                    dataGridView2.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dataGridView2.Columns[ix].Width = 350;
                }
                else
                {
                    dataGridView2.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView2.Columns[ix].Width = 60;
                }
            }
            if (dataGridView2.Columns.Count > 0) { 
                dataGridView2.Columns.RemoveAt(5);
                dataGridView2.Columns.RemoveAt(2);
                dataGridView2.Columns.RemoveAt(1);
                dataGridView2.Columns.RemoveAt(0);
            }
        }

        private void Chart_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this._baseChart.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();

                    this._baseChart.ChartAreas["ChartArea1"].AxisY.Minimum = _prevLow;
                    this._baseChart.ChartAreas["ChartArea1"].AxisY.Maximum = _prevHigh;
                }
            }
            catch (Exception)
            {
                
                throw;
            }            
        }

        private void ucChart_SizeChanged(object sender, EventArgs e)
        {
            if (this._baseChart != null)
            {
                this._baseChart.Width = this.Width - 20;
                this._baseChart.Height = this.Height - 5;
            }            
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if(_ds != null)
            {
                
                if (rbWeek.Checked)
                {
                    _month += 3 * 7;
                }
                else
                {
                    _month += 3;
                }
                

                DisplayChart(_ds, _month);
            }
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_ds != null)
            {
                if (rbWeek.Checked)
                {
                    if (_month - 3*7 > 0)
                    {
                        _month -= 3*7;
                    }         
                }
                else
                {
                    if (_month - 3 > 0)
                    {
                        _month -= 3;
                    }         
                }

       

                DisplayChart(_ds, _month);
            }
        }

        private void chk3MA_CheckedChanged(object sender, EventArgs e)
        {
            switch (((CheckBox)sender).Name)
            {
                case "chk3MA":
                    this._baseChart.Series["3MA라인"].Enabled = ((CheckBox)sender).Checked;
                    break;
                case "chk5MA":
                    this._baseChart.Series["5MA라인"].Enabled = ((CheckBox)sender).Checked;
                    break;
                case "chk10MA":
                    this._baseChart.Series["10MA라인"].Enabled = ((CheckBox)sender).Checked;
                    break;
                case "chk20MA":
                    this._baseChart.Series["20MA라인"].Enabled = ((CheckBox)sender).Checked;
                    break;
                case "chk60MA":
                    this._baseChart.Series["60MA라인"].Enabled = ((CheckBox)sender).Checked;
                    break;
                case "chk120MA":
                    this._baseChart.Series["120MA라인"].Enabled = ((CheckBox)sender).Checked;
                    break;
                case "chk220MA":
                    this._baseChart.Series["220MA라인"].Enabled = ((CheckBox)sender).Checked;
                    break;
                case "chk41bl":
                    this._baseChart.Series["볼밴"].Enabled = ((CheckBox)sender).Checked;
                    break;
                case "chkEnv":
                    this._baseChart.Series["ENV"].Enabled = ((CheckBox)sender).Checked;
                    break;
                case "chk20LMA":
                    this._baseChart.Series["20LMA라인"].Enabled = ((CheckBox)sender).Checked;
                    break;
                default:
                    break;
            }
        }

        private void chkRSI_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = ((CheckBox)sender);
            if (chk.Checked == true)
            {
                this._baseChart.ChartAreas["Volume"].Position.Height = this._baseChart.ChartAreas["Volume"].Position.Height - 10;
                this._baseChart.ChartAreas["RSI"].Visible = true;
            }
            else
            {
                this._baseChart.ChartAreas["Volume"].Position.Height = this._baseChart.ChartAreas["Volume"].Position.Height + 10;
                this._baseChart.ChartAreas["RSI"].Visible = false;
            }
        }


        private void dgInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 최저MA.Index) return;
            LineAdd(Convert.ToDouble(String.Format("{0:0.00}", dgInfo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())));
        }

        private void Chart_MouseClick(object sender, MouseEventArgs e)
        {
            _baseChart.MouseMove -= Chart_MouseMove;
            _baseChart.MouseMove -= Chart_MouseMove;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                LineAdd(Convert.ToDouble(String.Format("{0:0.00}", Math.Round(_curPrice, 0))));
            }
            _baseChart.MouseMove += Chart_MouseMove;
        }

        private void LineAdd(Double price)
        {
            try {
                int seed = DateTime.Now.Millisecond;
                Random r = new Random(seed);

                int ranNum = r.Next();

                System.Windows.Forms.DataVisualization.Charting.Series s1 = new System.Windows.Forms.DataVisualization.Charting.Series();
                s1.ChartArea = "ChartArea1";
                s1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                s1.Color = Color.Blue;
                s1.Name = DateTime.Now.ToString("yyyyMMddHHmmss") + DateTime.Now.Millisecond.ToString() + ranNum;
            
                _baseChart.Series.Add(s1);

                for (int i = 0; i < _baseChart.Series["캔들"].Points.Count; i++)
                {
                    s1.Points.Add(price);
                }
                
                if (s1.Points.Count > 0) s1.Points[0].Label = price.ToString();
                System.Threading.Thread.Sleep(1);
            }
            catch { }
        }

        private void LineRemove()
        {
            try { 
                if (_baseChart.Series[_baseChart.Series.Count - 1].Name.IndexOf("RSI") > -1) return;
                _baseChart.Series.Remove(_baseChart.Series[_baseChart.Series.Count - 1]);
            }
            catch { }
        }

        private void frmChart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                LineRemove();
            }
            else if (e.KeyCode == Keys.Add)
            {
                if (btnNext.Enabled == true)
                {
                    btnNext.PerformClick();
                }
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                if (btnPrev.Enabled == true)
                {
                    btnPrev.PerformClick();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton obj = (RadioButton)sender;
            if (obj.Name == rbWeek.Name && rbWeek.Checked == true)
            {
                _tOpt10082 = DoAsyncOpt100082(_stockCode);
                _tOpt10082.ContinueWith((t) =>
                {
                    chk220MA.Checked = false;
                    chk41bl.Checked = false;
                    chkEnv.Checked = false;
                    chkRSI.Checked = false;
                });
            }
            else if (obj.Name == rbMon.Name && rbMon.Checked == true)
            {
               
            }
            else if (obj.Name == rbDay.Name && rbDay.Checked == true)
            {
                _tOpt10081 = null;

                _tOpt10081 = DoAsyncOpt100081(_stockCode);
                _tOpt10081.ContinueWith((t) =>
                {
                    chk220MA.Checked = true;
                    chk41bl.Checked = true;
                    chkEnv.Checked = false;
                    chkRSI.Checked = true;
                });
            }
        }

        private void frmChart_FormClosed(object sender, FormClosedEventArgs e)
        {
            _MainStock.OnReceiveRealData_Volume -= OnEventReturnRealTime;
        }

        private void frmChart_Load(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = true;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                System.Diagnostics.Process.Start("http://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim());
            }
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;
            if (splitContainer2.Panel2Collapsed) this.Width = 1130;
            else this.Width = 1600;
        }
    }
}
