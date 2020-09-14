using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using AnalysisSt.DataBaseFunc;
using System.Threading;

namespace AnalysisSt.Chart.ucChart
{
    public partial class ucPrice : UserControl
    {
        public ucPrice()
        {
            InitializeComponent();
            _oCaPrice = _oBaseChart.CreateAreas(AnalysisSt.Chart.Class.BaseChart.AreasIndex.Price);
            _oCaPrice.Position.Height = 100;
            _oCaPrice.Position.Width = 100;
            _oCaPrice.Position.Y = 5;
            _oSePrice = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Price);
            _oSePrice.ChartArea = _oCaPrice.Name;
            _oSeMa_3 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_3);
            _oSeMa_5 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_5);
            _oSeMa_10 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_10);
            _oSeMa_20 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_20);
            _oSeMa_42 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_42);
            _oSeMa_60 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_60);
            _oSeMa_90 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_90);
            _oSeMa_120 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_120);
            _oSeMa_200 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_200);
            _oSeMa_480 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_480);
            _oSeMa_1000 = _oBaseChart.CreateMovAvgSeries(Class.BaseChart.MoveAvgSeriesIndex.Ma_1000);
            _oSeMa_3.ChartArea = _oCaPrice.Name;
            _oSeMa_5.ChartArea = _oCaPrice.Name;
            _oSeMa_10.ChartArea = _oCaPrice.Name;
            _oSeMa_20.ChartArea = _oCaPrice.Name;
            _oSeMa_42.ChartArea = _oCaPrice.Name;
            _oSeMa_60.ChartArea = _oCaPrice.Name;
            _oSeMa_90.ChartArea = _oCaPrice.Name;
            _oSeMa_120.ChartArea = _oCaPrice.Name;
            _oSeMa_200.ChartArea = _oCaPrice.Name;
            _oSeMa_480.ChartArea = _oCaPrice.Name;
            _oSeMa_1000.ChartArea = _oCaPrice.Name;

            chartPrice.ChartAreas.Add(_oCaPrice);
            chartPrice.Series.Add(_oSePrice);
            chartPrice.Series.Add(_oSeMa_3);
            chartPrice.Series.Add(_oSeMa_5);
            chartPrice.Series.Add(_oSeMa_10);
            chartPrice.Series.Add(_oSeMa_20);
            chartPrice.Series.Add(_oSeMa_42);
            chartPrice.Series.Add(_oSeMa_60);
            chartPrice.Series.Add(_oSeMa_90);
            chartPrice.Series.Add(_oSeMa_120);
            chartPrice.Series.Add(_oSeMa_200);
            chartPrice.Series.Add(_oSeMa_480);
            chartPrice.Series.Add(_oSeMa_1000);

            Legend oLengend = new Legend();
            oLengend.Name = "Price";
            oLengend.Alignment = StringAlignment.Center;
            oLengend.Docking = Docking.Top;
            oLengend.LegendStyle = LegendStyle.Table;
            chartPrice.Legends.Add(oLengend);

            propertyGrid0.SelectedObject = _oPriceAttribute;
            propertyGrid0.Refresh();

            _oPriceAttribute.onChangedBaseChartProp += new ucPriceAttribute.ChangedBaseChartProp(DoChangedProp);

            chartPrice.EnableZoomAndPanControls(ChartCursorSelected, ChartCursorMoved, zoomChanged,
             new ChartOption()
             {
                 ContextMenuAllowToHideSeries = true,
                 XAxisPrecision = 0,
                 YAxisPrecision = 2
             });

        }
        private void ChartCursorSelected(System.Windows.Forms.DataVisualization.Charting.Chart sender, ChartCursor e)
        {
            PointF diff = sender.CursorsDiff();
        }
        private void ChartCursorMoved(System.Windows.Forms.DataVisualization.Charting.Chart sender, ChartCursor e)
        {

        }
        private void zoomChanged(System.Windows.Forms.DataVisualization.Charting.Chart sender)
        {
        }
        private void ucPrice_Load(object sender, EventArgs e)
        {
          
        }

        private void InitChart()
        {
            if (_stockCode == "" || _stockCode == null) { return; }
            DataSet ds;
            RichQuery oRichQuery = new RichQuery();

            ds = oRichQuery.p_Set01Query("1", _stockCode, "2", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                return;
            }

            DataSet ds2th = new DataSet();
            DataRow dr;
            System.IO.StringReader xmlSr = new System.IO.StringReader(ds.Tables[0].Rows[0]["SET_INFO"].ToString());
            ds2th.ReadXml(xmlSr);
            dr = ds2th.Tables[0].Rows[0];

            _oPriceAttribute.Ma3 = ChangeStringToBool(dr["Ma3"].ToString());
            _oPriceAttribute.Ma5 = ChangeStringToBool(dr["Ma5"].ToString());
            _oPriceAttribute.Ma10 = ChangeStringToBool(dr["Ma10"].ToString());
            _oPriceAttribute.Ma20 = ChangeStringToBool(dr["Ma20"].ToString());
            _oPriceAttribute.Ma42 = ChangeStringToBool(dr["Ma42"].ToString());
            _oPriceAttribute.Ma60 = ChangeStringToBool(dr["Ma60"].ToString());
            _oPriceAttribute.Ma90 = ChangeStringToBool(dr["Ma90"].ToString());
            _oPriceAttribute.Ma120 = ChangeStringToBool(dr["Ma120"].ToString());
            _oPriceAttribute.Ma200 = ChangeStringToBool(dr["Ma200"].ToString());
            _oPriceAttribute.Ma480 = ChangeStringToBool(dr["Ma480"].ToString());
            _oPriceAttribute.Ma1000 = ChangeStringToBool(dr["Ma1000"].ToString());

            propertyGrid0.Refresh();
         
        }

        private void btnSaveAtt_Click(object sender, EventArgs e)
        {
            if (_stockCode == "" || _stockCode == null) { return; }

            ArrayParam arrParam = new ArrayParam();
            DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "RICHDB");
            String xmlString = "";
            try
            {
                xmlString = "<Ma3>" + ChangeBoolToString(_oSeMa_3.Enabled) + "</Ma3>";
                xmlString = xmlString + "<Ma5>" + ChangeBoolToString(_oSeMa_5.Enabled) + "</Ma5>";
                xmlString = xmlString + "<Ma10>" + ChangeBoolToString(_oSeMa_10.Enabled) + "</Ma10>";
                xmlString = xmlString + "<Ma20>" + ChangeBoolToString(_oSeMa_20.Enabled) + "</Ma20>";
                xmlString = xmlString + "<Ma42>" + ChangeBoolToString(_oSeMa_42.Enabled) + "</Ma42>";
                xmlString = xmlString + "<Ma60>" + ChangeBoolToString(_oSeMa_60.Enabled) + "</Ma60>";
                xmlString = xmlString + "<Ma90>" + ChangeBoolToString(_oSeMa_90.Enabled) + "</Ma90>";
                xmlString = xmlString + "<Ma120>" + ChangeBoolToString(_oSeMa_120.Enabled) + "</Ma120>";
                xmlString = xmlString + "<Ma200>" + ChangeBoolToString(_oSeMa_200.Enabled) + "</Ma200>";
                xmlString = xmlString + "<Ma480>" + ChangeBoolToString(_oSeMa_480.Enabled) + "</Ma480>";
                xmlString = xmlString + "<Ma1000>" + ChangeBoolToString(_oSeMa_1000.Enabled) + "</Ma1000>";
                xmlString = "<SET01>" + xmlString + "</SET01>";
                arrParam.Clear();
                arrParam.Add("@ACTION_GB", "A");
                arrParam.Add("@STOCK_CODE", _stockCode);
                arrParam.Add("@CHART_GB", "2");
                arrParam.Add("@SET_INFO", xmlString);
                arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql.ExecuteNonQuery("p_Set01Add", CommandType.StoredProcedure, arrParam);

                System.Windows.Forms.MessageBox.Show("환경설정이 저장되었습니다.");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }

        }

        private bool ChangeStringToBool(String value)
        {
            switch (value)
            {
                case "0":
                    return false;
                case "1":
                    return true;
                default:
                    return false;
            }
        }
        private String ChangeBoolToString(bool value)
        {
            if (value == true)
            { return "1"; }
            else
            { return "0"; }
        }
        #region Event
        private void DoChangedProp(String CategoryName, ucPriceAttribute.ParamIndex p)
        {
            switch (p)
            {
                case ucPriceAttribute.ParamIndex.Ma_3:
                    _oSeMa_3.Enabled = _oPriceAttribute.Ma3;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_5:
                    _oSeMa_5.Enabled = _oPriceAttribute.Ma5;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_10:
                    _oSeMa_10.Enabled = _oPriceAttribute.Ma10;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_20:
                    _oSeMa_20.Enabled = _oPriceAttribute.Ma20;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_42:
                    _oSeMa_42.Enabled = _oPriceAttribute.Ma42;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_60:
                    _oSeMa_60.Enabled = _oPriceAttribute.Ma60;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_90:
                    _oSeMa_90.Enabled = _oPriceAttribute.Ma90;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_120:
                    _oSeMa_120.Enabled = _oPriceAttribute.Ma120;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_200:
                    _oSeMa_200.Enabled = _oPriceAttribute.Ma200;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_480:
                    _oSeMa_480.Enabled = _oPriceAttribute.Ma480;
                    break;
                case ucPriceAttribute.ParamIndex.Ma_1000:
                    _oSeMa_1000.Enabled = _oPriceAttribute.Ma1000;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 전역변수 및 프로퍼티
        private AnalysisSt.Chart.Class.BaseChart _oBaseChart = new AnalysisSt.Chart.Class.BaseChart();
        private string _stockCode;
        private string _FromDate;
        private string _ToDate;
        private ucPriceAttribute _oPriceAttribute = new ucPriceAttribute();
        private ChartArea _oCaPrice;
        private Series _oSePrice;
        private Series _oSeMa_3;
        private Series _oSeMa_5;
        private Series _oSeMa_10;
        private Series _oSeMa_20;
        private Series _oSeMa_42;
        private Series _oSeMa_60;
        private Series _oSeMa_90;
        private Series _oSeMa_120;
        private Series _oSeMa_200;
        private Series _oSeMa_480;
        private Series _oSeMa_1000;
        private DataTable _DtOpt10081;

        public string StockCode { get { return _stockCode; } set { _stockCode = value; DisplayChart(); InitChart(); } }
        public string FromDate { get { return _FromDate; } set { _FromDate = value; } }
        public string ToDate { get { return _ToDate; } set { _ToDate = value; } }
        #endregion

        #region Chart View
        private void DisplayChart()
        {
            if (_stockCode == "" || _stockCode == null) { return; }
            if (_FromDate == "" || _FromDate == null) { return; }
            if (_ToDate == "" || _ToDate == null) { return; }

            if (_DtOpt10081 != null)
            {
                _DtOpt10081.Reset();
            }

            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            _DtOpt10081 = oKiwoomQuery.p_Opt10081Query("2", _stockCode, _FromDate, _ToDate, false).Tables[0].Copy();

            Double[] arrP = new Double[1];
            int pt = 0;
            DataView dv = new DataView(_DtOpt10081);

            dv.Sort = "STOCK_DATE asc";

            int high = 0;
            int low = 0;

            try
            {

                for (int ix = chartPrice.Series.Count - 1; ix >= 0; ix--)
                {
                    if (chartPrice.Series[ix].Name.IndexOf(DateTime.Now.ToString("yyyyMMdd")) == -1)
                        this.chartPrice.Series[ix].Points.Clear();
                    else
                    {
                        if (chartPrice.Series[ix].Points.Count > 0)
                        {
                            arrP[arrP.Length - 1] = chartPrice.Series[ix].Points[0].YValues[0];
                            Array.Resize(ref arrP, arrP.Length + 1);
                        }
                        this.chartPrice.Series.RemoveAt(ix);
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
                if (low == 0)
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

                this.chartPrice.Series[_oSePrice.Name].Points.AddXY((object)dr["STOCK_DATE"], int.Parse(dr["HIGH_PRICE"].ToString()));
                this.chartPrice.Series[_oSePrice.Name].Points[pt].YValues[1] = int.Parse(dr["LOW_PRICE"].ToString());
                this.chartPrice.Series[_oSePrice.Name].Points[pt].YValues[2] = int.Parse(dr["START_PRICE"].ToString());
                this.chartPrice.Series[_oSePrice.Name].Points[pt].YValues[3] = int.Parse(dr["NOW_PRICE"].ToString());

                //0 or 1, 수신데이터 1:유상증자, 2:무상증자, 4:배당락, 8:액면분할, 16:액면병합, 32:기업합병, 64:감자, 256:권리락
                switch (dr["CHG_JUGA_GB"].ToString().Trim())
                {
                    case "1":
                        this.chartPrice.Series[_oSePrice.Name].Points[pt].Label = "유상증자";
                        break;
                    case "2":
                        this.chartPrice.Series[_oSePrice.Name].Points[pt].Label = "무상증자";
                        break;
                    case "4":
                        this.chartPrice.Series[_oSePrice.Name].Points[pt].Label = "배당락";
                        break;
                    case "8":
                        this.chartPrice.Series[_oSePrice.Name].Points[pt].Label = "액면분할";
                        break;
                    case "16":
                        this.chartPrice.Series[_oSePrice.Name].Points[pt].Label = "액면병합";
                        break;
                    case "32":
                        this.chartPrice.Series[_oSePrice.Name].Points[pt].Label = "기업합병";
                        break;
                    case "64":
                        this.chartPrice.Series[_oSePrice.Name].Points[pt].Label = "감자";
                        break;
                    case "256":
                        this.chartPrice.Series[_oSePrice.Name].Points[pt].Label = "권리락";
                        break;
                    default:
                        break;
                }

                if (int.Parse(dr["START_PRICE"].ToString()) > int.Parse(dr["NOW_PRICE"].ToString()))
                {
                    this.chartPrice.Series[_oSePrice.Name].Points[pt].Color = System.Drawing.Color.Blue;
                }
                else
                {
                    this.chartPrice.Series[_oSePrice.Name].Points[pt].Color = System.Drawing.Color.Red;
                }

                pt++;
            }
                this.chartPrice.ChartAreas[_oCaPrice.Name].AxisY.Minimum = low - (low * 0.1);
                this.chartPrice.ChartAreas[_oCaPrice.Name].AxisY.Maximum = high + (high * 0.1);
                this.chartPrice.ChartAreas[_oCaPrice.Name].AxisX.IsLabelAutoFit = true;

                DataCalcAll();
           
        }
        private void DataCalcAll()
        {
            var cts = new CancellationTokenSource();
            var parent = new Task(() =>
            {
                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 3)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "3", _oCaPrice.Name +":Y4", _oSeMa_3.Name), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 5)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "5", _oCaPrice.Name + ":Y4", _oSeMa_5.Name), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 10)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "10", _oCaPrice.Name + ":Y4", _oSeMa_10.Name), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 20)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "20", _oCaPrice.Name + ":Y4", _oSeMa_20.Name), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 42)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "42", _oCaPrice.Name + ":Y4", _oSeMa_42.Name), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 60)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "60", _oCaPrice.Name + ":Y4", _oSeMa_60.Name), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 90)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "90", _oCaPrice.Name + ":Y4",  _oSeMa_90.Name), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 120)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "120", _oCaPrice.Name + ":Y4", _oSeMa_120.Name), TaskCreationOptions.AttachedToParent
                    ).Start();

                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 200)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "200", _oCaPrice.Name + ":Y4", _oSeMa_200.Name), TaskCreationOptions.AttachedToParent
                    ).Start();
                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 480)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "480", _oCaPrice.Name + ":Y4", _oSeMa_480.Name), TaskCreationOptions.AttachedToParent
                    ).Start();
                if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 1000)
                    new Task(() =>
                        this.DataCalc(FinancialFormula.MovingAverage, "1000", _oCaPrice.Name + ":Y4", _oSeMa_1000.Name), TaskCreationOptions.AttachedToParent
                    ).Start();

                //if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 20)
                //    new Task(() =>
                //        this.DataCalc(FinancialFormula.Envelopes, "20,20", _oCaPrice.Name + ":Y4", "ENV:Y,ENV:Y2"), TaskCreationOptions.AttachedToParent
                //    ).Start();

                //if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 40)
                //    new Task(() =>
                //        this.DataCalc(FinancialFormula.BollingerBands, "40,2", _oCaPrice.Name + ":Y4", "볼밴:Y,볼밴:Y2"), TaskCreationOptions.AttachedToParent
                //    ).Start();

                //if (chartPrice.Series[_oCaPrice.Name].Points.Count >= 14)
                //    new Task(() =>
                //        this.DataCalc(FinancialFormula.RelativeStrengthIndex, "14", _oCaPrice.Name + ":Y4", "RSI:Y"), TaskCreationOptions.AttachedToParent
                //    ).Start();
            }, cts.Token);

            var cwt = parent.ContinueWith(// parent Task가끝나면수행할Task 연결
                parentTask =>
                {
                  //  SettingToolTip();
                });
            parent.ContinueWith(// 예외발생시
                task => Console.WriteLine("Task threw: " + task.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted);
            parent.ContinueWith(task => Console.WriteLine("Task was canceled"), TaskContinuationOptions.OnlyOnCanceled);
            parent.Start();
            try
            {
                parent.Wait();
            }
            catch
            {
                cts.Cancel();
            }
        }
        private void DataCalc(FinancialFormula formula, string period, string candle, string line)
        {
            try
            {
                this.chartPrice.DataManipulator.FinancialFormula(formula, period, candle, line);
            }
            catch (System.IndexOutOfRangeException ex) { Console.WriteLine(ex.Message); }
            catch (System.NullReferenceException ex) { Console.WriteLine(ex.Message); }
        }
        #endregion

        private void chkAttView_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAttView.Checked == true)
            { splitCon1.Panel1Collapsed = true; }
            else
            { splitCon1.Panel1Collapsed = false; }
        }

        public class ucPriceAttribute
        {
            private bool _Ma_3;
            private bool _Ma_5;
            private bool _Ma_10;
            private bool _Ma_20;
            private bool _Ma_42;
            private bool _Ma_60;
            private bool _Ma_90;
            private bool _Ma_120; 
            private bool _Ma_200;
            private bool _Ma_480; 
            private bool _Ma_1000;

            public enum ParamIndex { Ma_3, Ma_5, Ma_10, Ma_20, Ma_42, Ma_60, Ma_90, Ma_120, Ma_200, Ma_480, Ma_1000 }

            public delegate void ChangedBaseChartProp(String CategoryName, ParamIndex p);
            public event ChangedBaseChartProp onChangedBaseChartProp;

            public void DoChangedBaseChartProp(String CategoryName, ParamIndex p)
            {
                onChangedBaseChartProp(CategoryName, p);
            }

            #region Attribute
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma3 { get { return _Ma_3; } set { _Ma_3 = value; DoChangedBaseChartProp("3일이동평균", ParamIndex.Ma_3); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma5 { get { return _Ma_5; } set { _Ma_5 = value; DoChangedBaseChartProp("5일이동평균", ParamIndex.Ma_5); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma10 { get { return _Ma_10; } set { _Ma_10 = value; DoChangedBaseChartProp("10일이동평균", ParamIndex.Ma_10); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma20 { get { return _Ma_20; } set { _Ma_20 = value; DoChangedBaseChartProp("20일이동평균", ParamIndex.Ma_20); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma42 { get { return _Ma_42; } set { _Ma_42 = value; DoChangedBaseChartProp("42일이동평균", ParamIndex.Ma_42); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma60 { get { return _Ma_60; } set { _Ma_60 = value; DoChangedBaseChartProp("60일이동평균", ParamIndex.Ma_60); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma90 { get { return _Ma_90; } set { _Ma_90 = value; DoChangedBaseChartProp("90일이동평균", ParamIndex.Ma_90); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma120 { get { return _Ma_120; } set { _Ma_120 = value; DoChangedBaseChartProp("120일이동평균", ParamIndex.Ma_120); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma200 { get { return _Ma_200; } set { _Ma_200 = value; DoChangedBaseChartProp("200일이동평균", ParamIndex.Ma_200); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma480 { get { return _Ma_480; } set { _Ma_480 = value; DoChangedBaseChartProp("480일이동평균", ParamIndex.Ma_480); } }
            
            [CategoryAttribute("1. 이동평균"),
            DefaultValueAttribute("")]
            public bool Ma1000 { get { return _Ma_1000; } set { _Ma_1000 = value; DoChangedBaseChartProp("1000일이동평균", ParamIndex.Ma_1000); } }
            #endregion
        }

        private void splitCon1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void propertyGrid0_Click(object sender, EventArgs e)
        {

        }

        private void splitCon0_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void chartPrice_Click(object sender, EventArgs e)
        {

        }

    }
      
}
