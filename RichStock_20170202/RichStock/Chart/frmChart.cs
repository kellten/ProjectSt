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

namespace Chart
{
    public partial class frmChart : Form
    {
        private clsChart _clsChart = new clsChart();
        private PaikRichStock.Common.ucMainStockVer2 _MainStock;
        private string _stockCode = "";
        System.Windows.Forms.DataVisualization.Charting.Chart _baseChart;

        public delegate void OnDelEventReturn10081ResultDt(DataSet ds);
        public delegate void OnDelEventReturnRealTime(DataSet ds);    

        public delegate void AddDataDelegate();
        public AddDataDelegate addDataDel;
        
        public PaikRichStock.Common.ucMainStockVer2 MainStock
        {
            set
            {
                _MainStock = value;

            }
        }
        
        public frmChart()
        {
            InitializeComponent();            

            //InitChart();
        }

        public void GetChartData(string stockCode)
        {
            this._clsChart.MainStock = _MainStock;
            this._clsChart.GetOpt10081(stockCode);
            _stockCode = stockCode;
                        
            this._clsChart.OnEventReturn10081ResultDt += new clsChart.OnEvent10081ResultDtEventHandler(this.onEventReturn10081ResultDt);
            this._clsChart.OnEventReturnRealTime += new clsChart.OnEventReturnRealTimeEventHandler(this.OnEventReturnRealTime);

            
            
        }       

        private void InitChart()
        {

            this._baseChart = new System.Windows.Forms.DataVisualization.Charting.Chart();

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            
            chartArea1.Name = "ChartArea1";
            chartArea2.Name = "Volume";

            this._baseChart.ChartAreas.Clear();
            this._baseChart.Series.Clear();
            this._baseChart.ChartAreas.Add(chartArea1);
            this._baseChart.ChartAreas.Add(chartArea2);
            
            //_baseChart.ChartAreas["Volume"].AlignWithChartArea = System.Windows.Forms.DataVisualization.Charting.al

            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Name = "캔들";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;            
            series2.Name = "라인";

            series3.ChartArea = "Volume";
            series3.Name = "거래량";

            this._baseChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;

            this._baseChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Position = _baseChart.ChartAreas["ChartArea1"].AxisX.Maximum;
            this._baseChart.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            this._baseChart.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            this._baseChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            //_baseChart.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            this._baseChart.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Days;
            this._baseChart.ChartAreas["ChartArea1"].AxisX.Interval = 10;
            this._baseChart.ChartAreas["Volume"].CursorX.IsUserEnabled = true;
            this._baseChart.ChartAreas["Volume"].CursorX.IsUserSelectionEnabled = true;
            this._baseChart.ChartAreas["Volume"].AxisX.ScaleView.Zoomable = true;

            this._baseChart.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            this._baseChart.ChartAreas["Volume"].InnerPlotPosition.Auto = true;

            this._baseChart.ChartAreas["Volume"].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
            this._baseChart.ChartAreas["Volume"].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;
            this._baseChart.ChartAreas["Volume"].AlignWithChartArea = "ChartArea1";

            this._baseChart.ChartAreas["ChartArea1"].Position.Auto = false;
            this._baseChart.ChartAreas["ChartArea1"].Position.Height = 70;
            this._baseChart.ChartAreas["ChartArea1"].Position.Width = 100;
            this._baseChart.ChartAreas["ChartArea1"].Position.X = 0;
            this._baseChart.ChartAreas["ChartArea1"].Position.Y = 0;

            this._baseChart.ChartAreas["Volume"].Position.Auto = false;
            this._baseChart.ChartAreas["Volume"].Position.Height = 30;
            this._baseChart.ChartAreas["Volume"].Position.Width = 100;
            this._baseChart.ChartAreas["Volume"].Position.X = _baseChart.ChartAreas["ChartArea1"].Position.X;
            this._baseChart.ChartAreas["Volume"].Position.Y = _baseChart.ChartAreas["ChartArea1"].Position.X + _baseChart.ChartAreas["ChartArea1"].Position.Height + 10;

            this._baseChart.Series.Add(series1);
            this._baseChart.Series.Add(series2);
            this._baseChart.Series.Add(series3);

            this._baseChart.Series["캔들"].ToolTip = "#AXISLABEL";
            this._baseChart.Series["캔들"].IsXValueIndexed = false;
            this._baseChart.Series["캔들"].XValueType = ChartValueType.DateTime;

            this._baseChart.Width = this.Width - 20;
            this._baseChart.Height = this.Height - 5;
            this.Controls.Add(this._baseChart);

            this._baseChart.MouseClick += new System.Windows.Forms.MouseEventHandler(this._baseChart_MouseClick);
            this._baseChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart_MouseMove);
            this._baseChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this._baseChart_MouseDown);
            this._baseChart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this._baseChart_AxisViewChanged);
            this._baseChart.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.chData_MouseWheel);

            ////ThreadStart addDataThreadStart = new ThreadStart(AddDataThreadLoop);
            ////addDataRunner = new Thread(addDataThreadStart);

            ////addDataDel += new AddDataDelegate(AddData);

            ////addDataRunner.Start();
        }
        
        #region Add new data thread

        ////private void AddDataThreadLoop()
        ////{
        ////    try
        ////    {
        ////        while (true)
        ////        {
        ////            // Invoke method must be used to interact with the chart
        ////            // control on the form!
        ////            _baseChart.Invoke(addDataDel);

        ////            // Thread is inactive for 200ms
        ////            Thread.Sleep(200);
        ////        }
        ////    }
        ////    catch
        ////    {
        ////        // Thread is aborted
        ////    }
        ////}

        ////public void AddData()
        ////{
            
        ////}
       
        #endregion

        void _baseChart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            ////DataPoint maxDataPoint = _baseChart.Series["캔들"].Points.FindMaxByValue();
            ////DataPoint minDataPoint = _baseChart.Series["캔들"].Points.FindMinByValue();
            

            ////////_baseChart.ChartAreas["ChartArea1"].CursorX.SelectionStart

            ////_baseChart.ChartAreas["ChartArea1"].AxisY.Minimum = minDataPoint.YValues[1] - (minDataPoint.YValues[1] * 0.1);
            ////_baseChart.ChartAreas["ChartArea1"].AxisY.Maximum = maxDataPoint.YValues[0] + (maxDataPoint.YValues[0] * 0.1);
            ////_baseChart.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = true;


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

        private void DisplayChart(DataSet ds, int month)
        {
            int pt = 0;

            DataView dv = new DataView(ds.Tables[0]);

            dv.Sort = "일자 asc";
            dv.RowFilter = "일자 >=" + DateTime.Now.AddMonths(-month).ToString("yyyyMMdd");

            int high = 0;
            int low = 0;
            this._baseChart.Series["캔들"].Points.Clear();
            this._baseChart.Series["거래량"].Points.Clear();

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

                this._baseChart.Series["캔들"].Points.AddXY(dr["일자"], int.Parse(dr["고가"].ToString()));
                this._baseChart.Series["캔들"].Points[pt].YValues[1] = int.Parse(dr["저가"].ToString());
                this._baseChart.Series["캔들"].Points[pt].YValues[2] = int.Parse(dr["시가"].ToString());
                this._baseChart.Series["캔들"].Points[pt].YValues[3] = int.Parse(dr["현재가"].ToString());

                this._baseChart.Series["거래량"].Points.AddXY(dr["일자"], double.Parse(dr["거래량"].ToString()));

                this._baseChart.Series["캔들"].Points[pt].ToolTip = PaikRichStock.Common.CDateTime.FormatDate(dr["일자"].ToString().Trim(), ".")
                                                                + "\r\n" + "시가:" + dr["시가"].ToString() + "   " + CalcRate(dr["전일종가"].ToString(), dr["시가"].ToString()).ToString() 
                                                                + "\r\n" + "고가:" + dr["고가"].ToString() + "   " + CalcRate(dr["전일종가"].ToString(), dr["고가"].ToString()).ToString() 
                                                                + "\r\n" + "저가:" + dr["저가"].ToString() + "   " + CalcRate(dr["전일종가"].ToString(), dr["저가"].ToString()).ToString() 
                                                                + "\r\n" + "종가:" + dr["현재가"].ToString() + "   " + CalcRate(dr["전일종가"].ToString(), dr["현재가"].ToString()).ToString()
                                                                + "\r\n" + "\r\n"
                                                                + "거래량:" + dr["거래량"].ToString();

                if (int.Parse(dr["시가"].ToString()) > int.Parse(dr["현재가"].ToString()))
                {
                    this._baseChart.Series["캔들"].Points[pt].Color = System.Drawing.Color.Blue;
                }
                else
                {
                    this._baseChart.Series["캔들"].Points[pt].Color = System.Drawing.Color.Red;
                }

                //baseChart.Series["캔들"].Points.AddXY()
                pt++;
            }

            //_baseChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(2, 3);                        
            //_baseChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.IsPositionedInside = true;

            this._baseChart.ChartAreas["ChartArea1"].AxisY.Minimum = low - (low * 0.1);
            this._baseChart.ChartAreas["ChartArea1"].AxisY.Maximum = high + (high * 0.1);
            this._baseChart.ChartAreas["ChartArea1"].AxisX.IsLabelAutoFit = true;

            _prevLow = low - (low * 0.1);
            _prevHigh = high + (high * 0.1);
        }

        private string CalcRate(string value1, string value2)
        {
            if (value1 == "" || value2 == "") return "";

            double calcValue = 0;

            calcValue = Math.Round((double.Parse(value2) - double.Parse(value1)) / double.Parse(value1) * 100, 2);

            if(calcValue>0)
            {
                return "+" + calcValue.ToString() + "%";
            }            
            else
            {
                return calcValue.ToString() + "%";
            }            
        }

        private void OnEventReturnRealTime(DataSet value)
        {
            //_baseChart.Invoke(addDataDel);
            if (this._baseChart == null) { return; }

            try
            {
                if (_stockCode == value.Tables[0].Rows[0]["STOCK_CODE"].ToString())
                { 
                    this._baseChart.Series["캔들"].Points[this._baseChart.Series["캔들"].Points.Count - 1].YValues[3] = int.Parse(value.Tables[0].Rows[0]["현재가"].ToString().Replace("-", ""));
                    this._baseChart.Series["캔들"].Points[this._baseChart.Series["캔들"].Points.Count - 1].Label = value.Tables[0].Rows[0]["현재가"].ToString().Replace("-", "");
                    _baseChart.Invalidate();
                    _baseChart.Update();

                }                
            }
            catch (Exception)
            {
                                
            }
            
            ////_baseChart.Series["캔들"].Points[_baseChart.Series["캔들"].Points.Count].YValues[2] = int.Parse(dr["시가"].ToString());
            ////_baseChart.Series["캔들"].Points[_baseChart.Series["캔들"].Points.Count].YValues[3] = int.Parse(dr["현재가"].ToString());           

        }
        private void onEventReturn10081ResultDt(DataSet value)
        {
            try
            {
                InitChart();
            }
            catch (Exception)
            {
                
            }            

            //var matplotlib = new MatplotlibCS.MatplotlibCS(@"C:\Users\eunha.BUSANPAIK\AppData\Local\Programs\Python\Python35-32\python.exe", @"D:\RichStock\Chart\MatplotlibCS\matplotlib_cs.py");
            for (int i = 0; i < this._baseChart.Series.Count; i++)
            {
                this._baseChart.Series[i].Points.Clear();
            }

            DataSet ds = value;

            _ds = ds;

            _month = 6;

            DisplayChart(ds, _month);

            ////_baseChart.ChartAreas["Volume"].Position.X = _baseChart.ChartAreas["ChartArea1"].Position.X;
            ////_baseChart.ChartAreas["Volume"].Position.Y = _baseChart.ChartAreas["ChartArea1"].Position.X + _baseChart.ChartAreas["ChartArea1"].Position.Height + 10;
        }

        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        void chart_MouseMove(object sender, MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    _baseChart.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();
            //    return;
            //}

            Point mousePoint = new Point(e.X, e.Y);

            this._baseChart.ChartAreas["ChartArea1"].CursorX.SetCursorPixelPosition(mousePoint, true);
            this._baseChart.ChartAreas["ChartArea1"].CursorY.SetCursorPixelPosition(mousePoint, true);
            
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = this._baseChart.HitTest(pos.X, pos.Y, false,
                                         ChartElementType.PlottingArea);

            try
            {
                foreach (var result in results)
                {
                    if (result.ChartElementType == ChartElementType.PlottingArea)
                    {
                        var xVal = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                        double yVal = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);

                        tooltip.Show(Math.Round(yVal, 2).ToString(), this._baseChart,
                                     pos.X, pos.Y - 15);
                    }
                    else if (result.ChartElementType == ChartElementType.DataPoint)
                    {

                    }
                }
            }
            catch (Exception)
            {
                                
            }
            
        }

        ////private void chart_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        ////{

        ////          // Check selected chart element and set tooltip text
        ////    switch(    e.HitTestResult.ChartElementType )
        ////    {
        ////        case ChartElementType.Axis:
        ////            e.Text = e.HitTestResult.Axis.Name;
        ////            break;                
        ////        case ChartElementType.DataPoint:
        ////            e.Text = "Data Point " + e.HitTestResult.PointIndex.ToString();
        ////            break;
        ////        case ChartElementType.Gridlines:
        ////            e.Text = "Grid Lines";
        ////            break;             
        ////        case ChartElementType.PlottingArea:
        ////            e.Text = e.HitTestResult;
        ////            break;
        ////        case ChartElementType.StripLines:
        ////            e.Text = "Strip Lines";
        ////            break;
        ////        case ChartElementType.TickMarks:
        ////            e.Text = "Tick Marks";
        ////            break;
        ////        case ChartElementType.Title:
        ////            e.Text = "Title";
        ////            break;
        ////    }
        ////}        


        void _baseChart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this._baseChart.ChartAreas["ChartArea1"].AxisX.ScaleView.ZoomReset();

                this._baseChart.ChartAreas["ChartArea1"].AxisY.Minimum = _prevLow;
                this._baseChart.ChartAreas["ChartArea1"].AxisY.Maximum = _prevHigh;  
            }
        }

        void _baseChart_MouseClick(object sender, MouseEventArgs e)
        {
            
            //else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    var pos = e.Location;
            //    if (prevPosition.HasValue && pos == prevPosition.Value)
            //        return;
            //    tooltip.RemoveAll();
            //    prevPosition = pos;
            //    var results = _baseChart.HitTest(pos.X, pos.Y, false,
            //                                 ChartElementType.PlottingArea);
            //    foreach (var result in results)
            //    {
            //        if (result.ChartElementType == ChartElementType.PlottingArea)
            //        {
            //            //var xVal = result.Series.Points[result.PointIndex].XValue;
            //            var yVal = Math.Round(result.ChartArea.AxisY.PixelPositionToValue(pos.Y),2);

            //            tooltip.Show(yVal.ToString(), _baseChart,
            //                         pos.X, pos.Y - 15);
            //        }
            //    }
            //}            
        }
        private void ucChart_SizeChanged(object sender, EventArgs e)
        {
            if (this._baseChart != null)
            {
                this._baseChart.Width = this.Width - 20;
                this._baseChart.Height = this.Height - 5;
            }            
        }
        private void chData_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    this._baseChart.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    this._baseChart.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = this._baseChart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = this._baseChart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = this._baseChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = this._baseChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = this._baseChart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    double posXFinish = this._baseChart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    double posYStart = this._baseChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    double posYFinish = this._baseChart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    this._baseChart.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    this._baseChart.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if(_ds != null)
            {
                _month += 3;

                DisplayChart(_ds, _month);
            }
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_ds != null)
            {
                if(_month - 3 > 0)
                {
                    _month -= 3;
                }                

                DisplayChart(_ds, _month);
            }
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {

        }

        private void frmChart_Load(object sender, EventArgs e)
        {

        }

        private void frmChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._clsChart.OnEventReturn10081ResultDt -= this.onEventReturn10081ResultDt;
            this._clsChart.OnEventReturnRealTime -= this.OnEventReturnRealTime;
        }
    }
}
