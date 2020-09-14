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
using AnalysisSt.TechnicalFunc;

namespace AnalysisSt.Chart.ucChart
{
    public partial class ucVolume : UserControl
    {
        public ucVolume()
        {
            InitializeComponent();
            _oCaVolume = _oBaseChart.CreateAreas(AnalysisSt.Chart.Class.BaseChart.AreasIndex.Volume);
            _oCaVolume.Position.Height = 100;
            _oCaVolume.Position.Width = 100;
            _oCaVolume.Position.Y = 5;
            _oSeVolume = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Volume);
            _Pr_Gain_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Gain);
            _Pr_Fore_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Fore);
            _Pr_Gigan_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Gigan);
            _Pr_Gumy_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Gumy);
            _Pr_Bohum_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Bohum);
            _Pr_Tosin_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Tosin);
            _Pr_Gita_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Gita);
            _Pr_Bank_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Bank);
            _Pr_Yeongi_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Yeongi);
            _Pr_Samo_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Samo);
            _Pr_Nation_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Nation);
            _Pr_Bubin_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_Bubin);
            _Pr_IoFore_Series = _oBaseChart.CreateSeries(Class.BaseChart.SeriesIndex.Pr_IoFore);
            _Pr_Gain_Series.ChartArea = _oCaVolume.Name;
            _Pr_Fore_Series.ChartArea = _oCaVolume.Name;
            _Pr_Gigan_Series.ChartArea = _oCaVolume.Name;
            _Pr_Gumy_Series.ChartArea = _oCaVolume.Name;
            _Pr_Bohum_Series.ChartArea = _oCaVolume.Name;
            _Pr_Tosin_Series.ChartArea = _oCaVolume.Name;
            _Pr_Gita_Series.ChartArea = _oCaVolume.Name;
            _Pr_Bank_Series.ChartArea = _oCaVolume.Name;
            _Pr_Yeongi_Series.ChartArea = _oCaVolume.Name;
            _Pr_Samo_Series.ChartArea = _oCaVolume.Name;
            _Pr_Nation_Series.ChartArea = _oCaVolume.Name;
            _Pr_Bubin_Series.ChartArea = _oCaVolume.Name;
            _Pr_IoFore_Series.ChartArea = _oCaVolume.Name;

            chartVolume.ChartAreas.Add(_oCaVolume);
            chartVolume.Series.Add(_oSeVolume);
            chartVolume.Series.Add(_Pr_Gain_Series);
            chartVolume.Series.Add(_Pr_Fore_Series);
            chartVolume.Series.Add(_Pr_Gigan_Series);
            chartVolume.Series.Add(_Pr_Gumy_Series);
            chartVolume.Series.Add(_Pr_Bohum_Series);
            chartVolume.Series.Add(_Pr_Tosin_Series);
            chartVolume.Series.Add(_Pr_Gita_Series);
            chartVolume.Series.Add(_Pr_Bank_Series);
            chartVolume.Series.Add(_Pr_Yeongi_Series);
            chartVolume.Series.Add(_Pr_Samo_Series);
            chartVolume.Series.Add(_Pr_Nation_Series);
            chartVolume.Series.Add(_Pr_Bubin_Series);
            chartVolume.Series.Add(_Pr_IoFore_Series);

            Legend oLengend = new Legend();
            oLengend.Name = "Volume";
            oLengend.Alignment = StringAlignment.Center;
            oLengend.Docking = Docking.Top;
            oLengend.LegendStyle = LegendStyle.Table;
            chartVolume.Legends.Add(oLengend);

            propertyGrid0.SelectedObject = _oVolumeAttribute;
            propertyGrid0.Refresh();

            _oVolumeAttribute.onChangedBaseChartProp += new ucVolumeAttribute.ChangedBaseChartProp(DoChangedProp);
            chartVolume.EnableZoomAndPanControls(ChartCursorSelected, ChartCursorMoved, zoomChanged,
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
        private void ucVolume_Load(object sender, EventArgs e)
        {
          
        }
        #region DataTable 전역변수
        private DataTable _DtOpt10060_Qty;
        private DataTable _DtOpt10081;
        private DataTable _DtNuOpt10059Qty;
        private DataTable _DtNuOpt10059Price;
        private DataTable _DtSmm01;
        private DataTable _DtSmm02;
        #endregion

        #region 전역변수 및 프로퍼티
        private ucVolumeAttribute _oVolumeAttribute = new ucVolumeAttribute();
        private AnalysisSt.Chart.Class.BaseChart _oBaseChart = new AnalysisSt.Chart.Class.BaseChart();

        private string _stockCode;
        private string _FromDate;
        private string _ToDate;

        private ChartArea _oCaVolume;
        private Series _Pr_Gain_Series;
        private Series _Pr_Fore_Series;
        private Series _Pr_Gigan_Series;
        private Series _Pr_Gumy_Series;
        private Series _Pr_Bohum_Series;
        private Series _Pr_Tosin_Series;
        private Series _Pr_Gita_Series;
        private Series _Pr_Bank_Series;
        private Series _Pr_Yeongi_Series;
        private Series _Pr_Samo_Series;
        private Series _Pr_Nation_Series;
        private Series _Pr_Bubin_Series;
        private Series _Pr_IoFore_Series;
        private Series _oSeVolume;

        public string StockCode { get { return _stockCode; } set { _stockCode = value; InitChart(); DisplayChart(); } }
        public string FromDate { get { return _FromDate; } set { _FromDate = value; } }
        public string ToDate { get { return _ToDate; } set { _ToDate = value; } }

        #endregion

        private void InitChart()
        {
            if (_stockCode == "" || _stockCode == null) { return; }
            DataSet ds;
            RichQuery oRichQuery = new RichQuery();

            ds = oRichQuery.p_Set01Query("1", _stockCode, "3", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();

                ds = oRichQuery.p_Set01Query("1", _stockCode, "1", false);

                if (ds == null || ds.Tables[0].Rows.Count < 1)
                {
                    ds.Reset();
                    return;
                }
            }
            
           
            DataSet ds2th = new DataSet();
            DataRow dr;
            System.IO.StringReader xmlSr = new System.IO.StringReader(ds.Tables[0].Rows[0]["SET_INFO"].ToString());
            ds2th.ReadXml(xmlSr);
            dr = ds2th.Tables[0].Rows[0];

            _oVolumeAttribute.개인평균 = ChangeStringToBool(dr["개인평균"].ToString());
            _oVolumeAttribute.외국인평균 = ChangeStringToBool(dr["외국인평균"].ToString());
            _oVolumeAttribute.기관평균 = ChangeStringToBool(dr["기관평균"].ToString());
            _oVolumeAttribute.금융평균 = ChangeStringToBool(dr["금융평균"].ToString());
            _oVolumeAttribute.보험평균 = ChangeStringToBool(dr["보험평균"].ToString());
            _oVolumeAttribute.투신평균 = ChangeStringToBool(dr["투신평균"].ToString());
            _oVolumeAttribute.기타평균 = ChangeStringToBool(dr["기타평균"].ToString());
            _oVolumeAttribute.은행평균 = ChangeStringToBool(dr["은행평균"].ToString());
            _oVolumeAttribute.연기금평균 = ChangeStringToBool(dr["연기금평균"].ToString());
            _oVolumeAttribute.사모평균 = ChangeStringToBool(dr["사모평균"].ToString());
            _oVolumeAttribute.국가평균 = ChangeStringToBool(dr["국가평균"].ToString());
            _oVolumeAttribute.법인평균 = ChangeStringToBool(dr["법인평균"].ToString());
            _oVolumeAttribute.기외평균 = ChangeStringToBool(dr["기외평균"].ToString());

            propertyGrid0.Refresh();

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
        private void DoChangedProp(String CategoryName, ucVolumeAttribute.ParamIndex p)
        {
            switch (p)
            {
                case ucVolumeAttribute.ParamIndex.Pr_Gain:
                    _Pr_Gain_Series.Enabled = _oVolumeAttribute.개인평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Fore:
                    _Pr_Fore_Series.Enabled = _oVolumeAttribute.외국인평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Gigan:
                    _Pr_Gigan_Series.Enabled = _oVolumeAttribute.기관평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Gumy:
                    _Pr_Gumy_Series.Enabled = _oVolumeAttribute.금융평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Bohum:
                    _Pr_Bohum_Series.Enabled = _oVolumeAttribute.보험평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Tosin:
                    _Pr_Tosin_Series.Enabled = _oVolumeAttribute.투신평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Gita:
                    _Pr_Gita_Series.Enabled = _oVolumeAttribute.기타평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Bank:
                    _Pr_Bank_Series.Enabled = _oVolumeAttribute.은행평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Yeongi:
                    _Pr_Yeongi_Series.Enabled = _oVolumeAttribute.연기금평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Samo:
                    _Pr_Samo_Series.Enabled = _oVolumeAttribute.사모평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Nation:
                    _Pr_Nation_Series.Enabled = _oVolumeAttribute.국가평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Bubin:
                    _Pr_Bubin_Series.Enabled = _oVolumeAttribute.법인평균;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_IoFore:
                    _Pr_IoFore_Series.Enabled = _oVolumeAttribute.기외평균;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region DisplayChart
        private void DisplayChart()
        {
            if (_stockCode == null || _stockCode.Trim() == "")
            {
                return;
            }

            if (_oSeVolume == null)
            {
                return;
            }

            GetOpt10081Qty();
            GetOpt1005ScarePriceNujuk();

            int pt = 0;

            try
            {

                for (int ix = chartVolume.Series.Count - 1; ix >= 0; ix--)
                {
                    if (chartVolume.Series[ix].Name.IndexOf(DateTime.Now.ToString("yyyyMMdd")) == -1)
                        this.chartVolume.Series[ix].Points.Clear(); 
                }
            }
            catch (System.NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            DataView dv = new DataView(_DtOpt10081);
            dv.Sort = "STOCK_DATE asc";

            foreach (DataRowView dr in dv)
            {
         
                chartVolume.Series[_oSeVolume.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr["TRADE_QTY"].ToString()));
                int curCnt = chartVolume.Series[_oSeVolume.Name].Points.Count - 1;
                chartVolume.Series[_oSeVolume.Name].Points[curCnt].Color = System.Drawing.Color.Red;

                if (curCnt > 0)
                {
                    Double preVolume = chartVolume.Series[_oSeVolume.Name].Points[curCnt - 1].YValues[0];
                    Double CurVolume = chartVolume.Series[_oSeVolume.Name].Points[curCnt].YValues[0];

                    if (preVolume < CurVolume)
                    {
                        chartVolume.Series[_oSeVolume.Name].Points[curCnt].Color = System.Drawing.Color.Red;
                    }
                    else
                    {
                        chartVolume.Series[_oSeVolume.Name].Points[curCnt].Color = System.Drawing.Color.Blue;
                    }
                }

                Parallel.For((int)ucVolumeAttribute.ParamIndex.Pr_Gain, (int)ucVolumeAttribute.ParamIndex.Pr_IoFore, (int i) =>
                {
                    
                        switch ((ucVolumeAttribute.ParamIndex)i)
                        {
                            case ucVolumeAttribute.ParamIndex.Pr_Gain:
                                if (_Pr_Gain_Series != null)
                                {
                                    float avgPrice0 = 0;
                                    
                                    avgPrice0 = 0;
                                    avgPrice0 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Gain);

                                    if (avgPrice0 != -999)
                                    {
                                        chartVolume.Series[_Pr_Gain_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice0);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Fore:
                                if (_Pr_Fore_Series != null)
                                {
                                    float avgPrice1 = 0;
                                    
                                    avgPrice1 = 0;
                                    avgPrice1 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Fore);

                                    if (avgPrice1 != -999)
                                    {
                                        chartVolume.Series[_Pr_Fore_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice1);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Gigan:
                                if (_Pr_Gigan_Series != null)
                                {
                                    float avgPrice2 = 0;
                                   
                                    avgPrice2 = 0;
                                    avgPrice2 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Gigan);

                                    if (avgPrice2 != -999)
                                    {
                                        chartVolume.Series[_Pr_Gigan_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice2);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Gumy:
                                if (_Pr_Gumy_Series != null)
                                {
                                    float avgPrice3 = 0;
                                  
                                    avgPrice3 = 0;
                                    avgPrice3 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Gumy);

                                    if (avgPrice3 != -999)
                                    {
                                        chartVolume.Series[_Pr_Gumy_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice3);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Bohum:
                                if (_Pr_Bohum_Series != null)
                                {
                                    float avgPrice4 = 0;
                                   
                                    avgPrice4 = 0;
                                    avgPrice4 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Bohum);

                                    if (avgPrice4 != -999)
                                    {
                                        chartVolume.Series[_Pr_Bohum_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice4);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Tosin:
                                if (_Pr_Tosin_Series != null)
                                {
                                    float avgPrice5 = 0;
                                   
                                    avgPrice5 = 0;
                                    avgPrice5 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Tosin);

                                    if (avgPrice5 != -999)
                                    {
                                        chartVolume.Series[_Pr_Tosin_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice5);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Gita:
                                if (_Pr_Gita_Series != null)
                                {
                                    float avgPrice6 = 0;
                                    
                                    avgPrice6 = 0;
                                    avgPrice6 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Gita);

                                    if (avgPrice6 != -999)
                                    {
                                        chartVolume.Series[_Pr_Gita_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice6);
                                    }
                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Bank:

                                if (_Pr_Bank_Series != null)
                                {
                                    float avgPrice7 = 0;
                                    
                                    avgPrice7 = 0;
                                    avgPrice7 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Bank);

                                    if (avgPrice7 != -999)
                                    {
                                        chartVolume.Series[_Pr_Bank_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice7);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Yeongi:
                                if (_Pr_Yeongi_Series != null)
                                {
                                    float avgPrice8 = 0;
                                    
                                    avgPrice8 = 0;
                                    avgPrice8 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Yeongi);

                                    if (avgPrice8 != -999)
                                    {
                                        chartVolume.Series[_Pr_Yeongi_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice8);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Samo:
                                if (_Pr_Samo_Series != null)
                                {
                                    float avgPrice9 = 0;
                                   
                                    avgPrice9 = 0;
                                    avgPrice9 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Samo);

                                    if (avgPrice9 != -999)
                                    {
                                        chartVolume.Series[_Pr_Samo_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice9);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Nation:

                                if (_Pr_Nation_Series != null)
                                {
                                    float avgPrice10 = 0;
                                   
                                    avgPrice10 = 0;
                                    avgPrice10 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Nation);

                                    if (avgPrice10 != -999)
                                    {
                                        chartVolume.Series[_Pr_Nation_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice10);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_Bubin:

                                if (_Pr_Bubin_Series != null)
                                {
                                    float avgPrice11 = 0;
                                  
                                    avgPrice11 = 0;
                                    avgPrice11 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_Bubin);

                                    if (avgPrice11 != -999)
                                    {
                                        chartVolume.Series[_Pr_Bubin_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice11);
                                    }

                                }
                                break;
                            case ucVolumeAttribute.ParamIndex.Pr_IoFore:

                                if (_Pr_IoFore_Series != null)
                                {
                                    float avgPrice12 = 0;

                                    avgPrice12 = 0;
                                    avgPrice12 = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ucVolumeAttribute.ParamIndex.Pr_IoFore);

                                    if (avgPrice12 != -999)
                                    {
                                        chartVolume.Series[_Pr_IoFore_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice12);
                                    }

                                }
                                break;
            
                            }
                });
                
                pt++;
            }

            chartVolume.ChartAreas[_oCaVolume.Name].AxisY2.Minimum = 0;
            chartVolume.ChartAreas[_oCaVolume.Name].AxisY2.Maximum = 100;
            chartVolume.ChartAreas[_oCaVolume.Name].AxisX.IsLabelAutoFit = true;

         
        }
       
        private float CalPercentVolume(String stdDate, ucVolumeAttribute.ParamIndex pi)
        {
            float minPrice = 0;
            float maxPrice = 0;
            float scaPrice = 0;
            int iPrice = 0;
            float perPrice = 0;
            String QtyColumnName = "";
            String PriceColumnName = "";
            DataView dv2 = new DataView(_DtSmm01);
            DataView dv3 = new DataView(_DtNuOpt10059Price);
            dv3.RowFilter = String.Format("STOCK_DATE = '{0}'", stdDate);

            switch (pi)
            {
                case ucVolumeAttribute.ParamIndex.Pr_Gain:
                    QtyColumnName = Common.Class.clsDicDefine.GAIN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GAIN_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Fore:
                    QtyColumnName = Common.Class.clsDicDefine.FORE_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.FORE_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Gigan:
                    QtyColumnName = Common.Class.clsDicDefine.GIGAN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GIGAN_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Gumy:
                    QtyColumnName = Common.Class.clsDicDefine.GUMY_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GUMY_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Bohum:
                    QtyColumnName = Common.Class.clsDicDefine.BOHUM_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.BOHUM_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Tosin:
                    QtyColumnName = Common.Class.clsDicDefine.TOSIN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.TOSIN_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Gita:
                    QtyColumnName = Common.Class.clsDicDefine.GITA_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GITA_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Bank:
                    QtyColumnName = Common.Class.clsDicDefine.BANK_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.BANK_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Yeongi:
                    QtyColumnName = Common.Class.clsDicDefine.YEONGI_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.YEONGI_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Samo:
                    QtyColumnName = Common.Class.clsDicDefine.SAMO_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.SAMO_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Nation:
                    QtyColumnName = Common.Class.clsDicDefine.NATION_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.NATION_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_Bubin:
                    QtyColumnName = Common.Class.clsDicDefine.BUBIN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.BUBIN_PRICE;
                    break;
                case ucVolumeAttribute.ParamIndex.Pr_IoFore:
                    QtyColumnName = Common.Class.clsDicDefine.IOFORE_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.IOFORE_PRICE;
                    break;
            }
            if (QtyColumnName == "" || PriceColumnName == "")
            {
                return -1;
            }

            bool dv2Check = false;
            bool dv3Check = false;

            foreach (DataRowView drv in dv2)
            {
                dv2Check = true;
                switch (drv["MIMA_GB"].ToString().Trim())
                {
                    case "1":
                        minPrice = Convert.ToInt32(drv[PriceColumnName]);
                        break;
                    case "2":
                        maxPrice = Convert.ToInt32(drv[PriceColumnName]);
                        break;
                    case "3":
                        scaPrice = Convert.ToInt32(drv[PriceColumnName]);
                        break;
                }
            }
            foreach (DataRowView drv in dv3)
            {
                dv3Check = true;
                iPrice = Convert.ToInt32(drv[PriceColumnName]);
            }

            if (dv2Check == false || dv3Check == false)
            {
                return 0;
            }

            perPrice = (float)(((iPrice - minPrice) / scaPrice) * 100);

            return perPrice;

        }
        #endregion

        #region GetData
        private void GetOpt10081Qty()
        {
            if (_stockCode == null || _stockCode.Trim() == "")
            {
                return;
            }
            if (_DtOpt10081 != null)
            {
                _DtOpt10081.Reset();
            }

            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            _DtOpt10081 = oKiwoomQuery.p_Opt10081Query("2", _stockCode, _FromDate, _ToDate, false).Tables[0].Copy();

        }
        private void GetOpt10060Qty()
        {
            if (_stockCode == null || _stockCode.Trim() == "")
            {
                return;
            }
            if (_DtOpt10060_Qty != null)
            {
                _DtOpt10060_Qty.Reset();
            }

            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            _DtOpt10060_Qty = oKiwoomQuery.p_Opt10060_QtyQuery("2", _stockCode, _FromDate, _ToDate, false).Tables[0].Copy();

        }
        private void GetOpt1005Scare9QtyNujuk()
        {
            if (_stockCode == null || _stockCode.Trim() == "")
            {
                return;
            }
            if (_DtNuOpt10059Qty != null)
            {
                _DtNuOpt10059Qty.Reset();
            }

            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            _DtNuOpt10059Qty = oKiwoomQuery.p_NuOPT10059QtyQuery("1", _stockCode, _FromDate, _ToDate, false).Tables[0].Copy();
            _DtSmm02 = oKiwoomQuery.p_Smm02Query("1", _stockCode, false).Tables[0].Copy();


        }
        private void GetOpt1005ScarePriceNujuk()
        {
            if (_stockCode == null || _stockCode.Trim() == "")
            {
                return;
            }
            if (_DtNuOpt10059Price != null)
            {
                _DtNuOpt10059Price.Reset();
            }

            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            DataSet ds;

            ds = oKiwoomQuery.p_NuOPT10059PriceQuery("2", _stockCode, "", "", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                if (System.Windows.Forms.MessageBox.Show("NU_OPT10059Price의 최종내역이 없습니다. 작업을 하시겠습니까?", "생성작업", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ds = oKiwoomQuery.p_Opt10059PriceNujukQuery("1", _stockCode, "", false);
                    System.Windows.Forms.MessageBox.Show("NU_OPT10059Price 작업 완료");
                    ds.Reset();
                }
            }
            else
            { ds.Reset(); }


            ds = oKiwoomQuery.p_Smm01Query("1", _stockCode, false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                if (System.Windows.Forms.MessageBox.Show("Smm01의 최종내역이 없습니다. 작업을 하시겠습니까?", "생성작업", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ArrayParam arrParam = new ArrayParam();
                    DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _stockCode);
                    arrParam.Add("@MIMA_GB", "");
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_SMM01Add", CommandType.StoredProcedure, arrParam);
                }
            }
            else
            { ds.Reset(); }

            _DtNuOpt10059Price = oKiwoomQuery.p_NuOPT10059PriceQuery("1", _stockCode, _FromDate, _ToDate, false).Tables[0].Copy();
            _DtSmm01 = oKiwoomQuery.p_Smm01Query("1", _stockCode, false).Tables[0].Copy();


        }
        #endregion

        private void SeriesVisibleProcess(Boolean value, ref ChartArea ca, ref Series se, AnalysisSt.Chart.Class.BaseChart.SeriesIndex si)
        {
            if (se == null)
            {

                if ((Boolean)value == true)
                { se = _oBaseChart.CreateSeries(si); se.ChartArea = ca.Name; chartVolume.Series.Add(se); }

            }
            else
            {
                se.Enabled = (Boolean)value;

            }
        }

        private void btnSaveAtt_Click(object sender, EventArgs e)
        {
            if (_stockCode == "" || _stockCode == null) { return; }

            ArrayParam arrParam = new ArrayParam();
            DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "RICHDB");
            String xmlString = "";
            try
            {
                xmlString = "<개인평균>" + ChangeBoolToString(_Pr_Gain_Series.Enabled) + "</개인평균>";
                xmlString = xmlString + "<외국인평균>" + ChangeBoolToString(_Pr_Fore_Series.Enabled) + "</외국인평균>";
                xmlString = xmlString + "<기관평균>" + ChangeBoolToString(_Pr_Gigan_Series.Enabled) + "</기관평균>";
                xmlString = xmlString + "<금융평균>" + ChangeBoolToString(_Pr_Gumy_Series.Enabled) + "</금융평균>";
                xmlString = xmlString + "<보험평균>" + ChangeBoolToString(_Pr_Bohum_Series.Enabled) + "</보험평균>";
                xmlString = xmlString + "<투신평균>" + ChangeBoolToString(_Pr_Tosin_Series.Enabled) + "</투신평균>";
                xmlString = xmlString + "<기타평균>" + ChangeBoolToString(_Pr_Gita_Series.Enabled) + "</기타평균>";
                xmlString = xmlString + "<은행평균>" + ChangeBoolToString(_Pr_Bank_Series.Enabled) + "</은행평균>";
                xmlString = xmlString + "<연기금평균>" + ChangeBoolToString(_Pr_Yeongi_Series.Enabled) + "</연기금평균>";
                xmlString = xmlString + "<사모평균>" + ChangeBoolToString(_Pr_Samo_Series.Enabled) + "</사모평균>";
                xmlString = xmlString + "<국가평균>" + ChangeBoolToString(_Pr_Nation_Series.Enabled) + "</국가평균>";
                xmlString = xmlString + "<법인평균>" + ChangeBoolToString(_Pr_Bubin_Series.Enabled) + "</법인평균>";
                xmlString = xmlString + "<기외평균>" + ChangeBoolToString(_Pr_IoFore_Series.Enabled) + "</기외평균>";
                xmlString = "<SET01>" + xmlString + "</SET01>";
                arrParam.Clear();
                arrParam.Add("@ACTION_GB", "A");
                arrParam.Add("@STOCK_CODE", _stockCode);
                arrParam.Add("@CHART_GB", "3");
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

        public class ucVolumeAttribute
        {
            private bool _Pr_Gain;
            private bool _Pr_Fore;
            private bool _Pr_Gigan;
            private bool _Pr_Gumy;
            private bool _Pr_Bohum;
            private bool _Pr_Tosin;
            private bool _Pr_Gita;
            private bool _Pr_Bank;
            private bool _Pr_Yeongi;
            private bool _Pr_Samo;
            private bool _Pr_Nation;
            private bool _Pr_Bubin;
            private bool _Pr_IoFore;

            public enum ParamIndex { Pr_Gain = 0, Pr_Fore, Pr_Gigan, Pr_Gumy, Pr_Bohum, Pr_Tosin, Pr_Gita, Pr_Bank, Pr_Yeongi, Pr_Samo, Pr_Nation, Pr_Bubin, Pr_IoFore }

            public delegate void ChangedBaseChartProp(String CategoryName, ParamIndex p);
            public event ChangedBaseChartProp onChangedBaseChartProp;

            public void DoChangedBaseChartProp(String CategoryName, ParamIndex p)
            {
                onChangedBaseChartProp(CategoryName, p);
            }

            #region Attribute
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 개인평균 { get { return _Pr_Gain; } set { _Pr_Gain = value; DoChangedBaseChartProp("Pr_Gain", ParamIndex.Pr_Gain); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 외국인평균 { get { return _Pr_Fore; } set { _Pr_Fore = value; DoChangedBaseChartProp("Pr_Fore", ParamIndex.Pr_Fore); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 기관평균 { get { return _Pr_Gigan; } set { _Pr_Gigan = value; DoChangedBaseChartProp("Pr_Gigan", ParamIndex.Pr_Gigan); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 금융평균 { get { return _Pr_Gumy; } set { _Pr_Gumy = value; DoChangedBaseChartProp("Pr_Gumy", ParamIndex.Pr_Gumy); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 보험평균 { get { return _Pr_Bohum; } set { _Pr_Bohum = value; DoChangedBaseChartProp("Pr_Bohum", ParamIndex.Pr_Bohum); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 투신평균 { get { return _Pr_Tosin; } set { _Pr_Tosin = value; DoChangedBaseChartProp("Pr_Tosin", ParamIndex.Pr_Tosin); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 기타평균 { get { return _Pr_Gita; } set { _Pr_Gita = value; DoChangedBaseChartProp("Pr_Gita", ParamIndex.Pr_Gita); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 은행평균 { get { return _Pr_Bank; } set { _Pr_Bank = value; DoChangedBaseChartProp("Pr_Bank", ParamIndex.Pr_Bank); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 연기금평균 { get { return _Pr_Yeongi; } set { _Pr_Yeongi = value; DoChangedBaseChartProp("Pr_Yeongi", ParamIndex.Pr_Yeongi); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 사모평균 { get { return _Pr_Samo; } set { _Pr_Samo = value; DoChangedBaseChartProp("Pr_Samo", ParamIndex.Pr_Samo); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 국가평균 { get { return _Pr_Nation; } set { _Pr_Nation = value; DoChangedBaseChartProp("Pr_Nation", ParamIndex.Pr_Nation); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 법인평균 { get { return _Pr_Bubin; } set { _Pr_Bubin = value; DoChangedBaseChartProp("Pr_Bubin", ParamIndex.Pr_Bubin); } }
            [CategoryAttribute("1. 평균"),
            DefaultValueAttribute(true)]
            public Boolean 기외평균 { get { return _Pr_IoFore; } set { _Pr_IoFore = value; DoChangedBaseChartProp("Pr_IoFore", ParamIndex.Pr_IoFore); } }
            #endregion
        }

        private void chkAttView_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAttView.Checked == true)
            { splitCon1.Panel1Collapsed = true; }
            else
            { splitCon1.Panel1Collapsed = false; }
        }

        private void chkPar_CheckedChanged(object sender, EventArgs e)
        {

        }

    }


}
