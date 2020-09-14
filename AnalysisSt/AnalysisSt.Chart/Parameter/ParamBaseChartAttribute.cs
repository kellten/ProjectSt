using System;
using System.Data;  
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms.DataVisualization.Charting;
using AnalysisSt.DataBaseFunc;
using AnalysisSt.TechnicalFunc;

namespace AnalysisSt.Chart.Parameter
{
    public class ParamBaseChartAttribute
    {
        // Enum
        public enum ParamIndex
        { Code = 0, Name, FromDate, ToDate,
          AreaA, Price, Curve_Price, Pr_Gain, Pr_Fore, Pr_Gigan, Pr_Gumy, Pr_Bohum, Pr_Tosin, Pr_Gita, Pr_Bank, Pr_Yeongi, Pr_Samo, Pr_Nation, Pr_Bubin, Pr_IoFore,
          AreaB, Volume, 
          AreaC, Gain, Fore, Gigan, Gumy, Bohum, Tosin, Gita, Bank, Yeongi, Samo,
          Nation, Bubin, IoFore
        }

        private System.Windows.Forms.DataVisualization.Charting.Chart _oChart;
        public System.Windows.Forms.DataVisualization.Charting.Chart OChart { get { return _oChart; } set { _oChart = value; } }
        private AnalysisSt.Chart.Class.BaseChart _oBasechart = new AnalysisSt.Chart.Class.BaseChart();
        private TechnicalFunc.TradeAnalysis.clsTechStats oTechStats = new TechnicalFunc.TradeAnalysis.clsTechStats();

        public void InitLoad()
        {
            if (_oChart == null) { return; }

            _oBasechart.OChart = _oChart;

        }

        #region Delegate & Event
        public delegate void ChangedBaseChartProp(String CategoryName, ParamIndex p);
        public event ChangedBaseChartProp onChangedBaseChartProp;

        public void DoChangedBaseChartProp(String CategoryName, ParamIndex p)
        {
            if (_StockCode == null || _StockCode == "") {return;}
            switch (p)
            {
                case ParamIndex.Code:
                    InitChartSetting();
                    InitAttribute();
                    break;
                case ParamIndex.Name:
                    break;
                case ParamIndex.FromDate:
                    break;
                case ParamIndex.ToDate:
                    break;
                case ParamIndex.AreaA:
                    AreaVisibleProcess(_AreaA, ref _AreaA_ChartArea, Class.BaseChart.AreasIndex.Price);
                    break;
                case ParamIndex.AreaB:
                    AreaVisibleProcess(_AreaB, ref _AreaB_ChartArea, Class.BaseChart.AreasIndex.Volume);
                    break;
                case ParamIndex.Price:
                    SeriesVisibleProcess(_Price, ref _AreaA_ChartArea, ref _Price_Series, Class.BaseChart.SeriesIndex.Price);
                    break;
                case ParamIndex.Curve_Price:
                    SeriesVisibleProcess(_Curve_Price, ref _AreaA_ChartArea, ref _Curve_Price_Series, Class.BaseChart.SeriesIndex.Price);
                    break;
                case ParamIndex.Pr_Gain:
                    SeriesVisibleProcess(_Pr_Gain, ref _AreaA_ChartArea, ref _Pr_Gain_Series, Class.BaseChart.SeriesIndex.Pr_Gain);
                    break;
                case ParamIndex.Pr_Fore:
                    SeriesVisibleProcess(_Pr_Fore, ref _AreaA_ChartArea, ref _Pr_Fore_Series, Class.BaseChart.SeriesIndex.Pr_Fore);
                    break;
                case ParamIndex.Pr_Gigan:
                    SeriesVisibleProcess(_Pr_Gigan, ref _AreaA_ChartArea, ref _Pr_Gigan_Series, Class.BaseChart.SeriesIndex.Pr_Gigan);
                    break;
                case ParamIndex.Pr_Gumy:
                    SeriesVisibleProcess(_Pr_Gumy, ref _AreaA_ChartArea, ref _Pr_Gumy_Series, Class.BaseChart.SeriesIndex.Pr_Gumy);
                    break;
                case ParamIndex.Pr_Bohum:
                    SeriesVisibleProcess(_Pr_Bohum, ref _AreaA_ChartArea, ref _Pr_Bohum_Series, Class.BaseChart.SeriesIndex.Pr_Bohum);
                    break;
                case ParamIndex.Pr_Tosin:
                    SeriesVisibleProcess(_Pr_Tosin, ref _AreaA_ChartArea, ref _Pr_Tosin_Series, Class.BaseChart.SeriesIndex.Pr_Tosin);
                    break;
                case ParamIndex.Pr_Gita:
                    SeriesVisibleProcess(_Pr_Gita, ref _AreaA_ChartArea, ref _Pr_Gita_Series, Class.BaseChart.SeriesIndex.Pr_Gita);
                    break;
                case ParamIndex.Pr_Bank:
                    SeriesVisibleProcess(_Pr_Bank, ref _AreaA_ChartArea, ref _Pr_Bank_Series, Class.BaseChart.SeriesIndex.Pr_Bank);
                    break;
                case ParamIndex.Pr_Yeongi:
                    SeriesVisibleProcess(_Pr_Yeongi, ref _AreaA_ChartArea, ref _Pr_Yeongi_Series, Class.BaseChart.SeriesIndex.Pr_Yeongi);
                    break;
                case ParamIndex.Pr_Samo:
                    SeriesVisibleProcess(_Pr_Samo, ref _AreaA_ChartArea, ref _Pr_Samo_Series, Class.BaseChart.SeriesIndex.Pr_Samo);
                    break;
                case ParamIndex.Pr_Nation:
                    SeriesVisibleProcess(_Pr_Nation, ref _AreaA_ChartArea, ref _Pr_Nation_Series, Class.BaseChart.SeriesIndex.Pr_Nation);
                    break;
                case ParamIndex.Pr_Bubin:
                    SeriesVisibleProcess(_Pr_Bubin, ref _AreaA_ChartArea, ref _Pr_Bubin_Series, Class.BaseChart.SeriesIndex.Pr_Bubin);
                    break;
                case ParamIndex.Pr_IoFore:
                    SeriesVisibleProcess(_Pr_IoFore, ref _AreaA_ChartArea, ref _Pr_IoFore_Series, Class.BaseChart.SeriesIndex.Pr_IoFore);
                    break;
                case ParamIndex.Volume:
                    SeriesVisibleProcess(_Volume, ref  _AreaB_ChartArea, ref  _Volume_Series, Class.BaseChart.SeriesIndex.Volume);
                    break;
                case ParamIndex.AreaC:
                    AreaVisibleProcess(_AreaC, ref _AreaC_ChartArea, Class.BaseChart.AreasIndex.TradeAnaly);
                    break;
                case ParamIndex.Gain:
                    SeriesVisibleProcess(_Gain, ref  _AreaC_ChartArea, ref  _Gain_Series, Class.BaseChart.SeriesIndex.Gain);
                    break;
                case ParamIndex.Fore:
                    SeriesVisibleProcess(_Fore, ref  _AreaC_ChartArea, ref  _Fore_Series, Class.BaseChart.SeriesIndex.Fore);
                    break;
                case ParamIndex.Gigan:
                    SeriesVisibleProcess(_Gigan, ref   _AreaC_ChartArea, ref _Gigan_Series, Class.BaseChart.SeriesIndex.Gigan);
                    break;
                case ParamIndex.Gumy:
                    SeriesVisibleProcess(_Gumy, ref  _AreaC_ChartArea, ref _Gumy_Series, Class.BaseChart.SeriesIndex.Gumy);
                    break;
                case ParamIndex.Bohum:
                    SeriesVisibleProcess(_Bohum, ref  _AreaC_ChartArea, ref  _Bohum_Series, Class.BaseChart.SeriesIndex.Bohum);
                    break;
                case ParamIndex.Tosin:
                    SeriesVisibleProcess(_Tosin, ref  _AreaC_ChartArea, ref  _Tosin_Series, Class.BaseChart.SeriesIndex.Tosin);
                    break;
                case ParamIndex.Gita:
                    SeriesVisibleProcess(_Gita, ref _AreaC_ChartArea, ref  _Gita_Series, Class.BaseChart.SeriesIndex.Gita);
                    break;
                case ParamIndex.Bank:
                    SeriesVisibleProcess(_Bank, ref _AreaC_ChartArea, ref _Bank_Series, Class.BaseChart.SeriesIndex.Bank);
                    break;
                case ParamIndex.Yeongi:
                    SeriesVisibleProcess(_Yeongi, ref  _AreaC_ChartArea, ref _Yeongi_Series, Class.BaseChart.SeriesIndex.Yeongi);
                    break;
                case ParamIndex.Samo:
                    SeriesVisibleProcess(_Samo, ref _AreaC_ChartArea, ref _Samo_Series, Class.BaseChart.SeriesIndex.Samo);
                    break;
                case ParamIndex.Nation:
                    SeriesVisibleProcess(_Nation, ref _AreaC_ChartArea, ref  _Nation_Series, Class.BaseChart.SeriesIndex.Nation);
                    break;
                case ParamIndex.Bubin:
                    SeriesVisibleProcess(_Bubin, ref  _AreaC_ChartArea, ref  _Bubin_Series, Class.BaseChart.SeriesIndex.Bubin);
                    break;
                case ParamIndex.IoFore:
                    SeriesVisibleProcess(_IoFore, ref  _AreaC_ChartArea, ref  _IoFore_Series, Class.BaseChart.SeriesIndex.IoFore);
                    break;
                default:
                    break;
            }


            onChangedBaseChartProp(CategoryName, p);
        }
        private void AreaVisibleProcess(Boolean value,ref ChartArea oc, AnalysisSt.Chart.Class.BaseChart.AreasIndex ai)
        {
            if (oc == null)
            {
                    if ((Boolean)value == true)
                    { oc = _oBasechart.CreateAreas(ai);
                    _oChart.ChartAreas.Add(oc);
                    if (ai == AnalysisSt.Chart.Class.BaseChart.AreasIndex.TradeAnaly)
                    {
                        if (_AreaA_ChartArea != null)
                        {
                            _AreaC_ChartArea.AlignWithChartArea = _AreaA_ChartArea.Name;
                            _AreaC_ChartArea.AlignmentStyle = AreaAlignmentStyles.AxesView;
                            _AreaC_ChartArea.AlignmentStyle = AreaAlignmentStyles.Cursor;
                            _AreaC_ChartArea.AlignmentStyle = AreaAlignmentStyles.PlotPosition;
                        }
                    }
                    if (ai == AnalysisSt.Chart.Class.BaseChart.AreasIndex.Volume)
                    {
                        if (_AreaA_ChartArea != null)
                        {
                            _AreaB_ChartArea.AlignWithChartArea = _AreaA_ChartArea.Name;
                            _AreaB_ChartArea.AlignmentStyle = AreaAlignmentStyles.AxesView;
                            _AreaB_ChartArea.AlignmentStyle = AreaAlignmentStyles.Cursor;
                            _AreaB_ChartArea.AlignmentStyle = AreaAlignmentStyles.PlotPosition;
                        }
                    }
                    }
                    ChareAreaPositionHeight();
            }
            else 
            {  
                    oc.Visible = (Boolean)value;
                    ChareAreaPositionHeight();
            }
        }
        private void SeriesVisibleProcess(Boolean value, ref ChartArea ca, ref Series se, AnalysisSt.Chart.Class.BaseChart.SeriesIndex si)
        {
            if (se == null)
            {
             
                    if ((Boolean)value == true)
                    { se = _oBasechart.CreateSeries(si); se.ChartArea = ca.Name; _oChart.Series.Add(se); }
                
            }
            else
            { 
                    se.Enabled = (Boolean)value;
            
            }
        }
        private void ChareAreaPositionHeight()
        {
            bool vAreaA = false;
            bool vAreaB = false;
            bool vAreaC = false;
            if (_AreaA_ChartArea != null)
            {
                if (_AreaA_ChartArea.Visible == true)
                {
                    vAreaA = true;
                }
            }
            if (_AreaB_ChartArea != null)
            {
                if (_AreaB_ChartArea.Visible == true)
                {
                    vAreaB = true;
                }
            }
            if (_AreaC_ChartArea != null)
            {
                if (_AreaC_ChartArea.Visible == true)
                {
                    vAreaC = true;
                }
            }

            if (vAreaA == true && vAreaB == true && vAreaC == true)
            { 
                _AreaA_ChartArea.Position.Height = 50;
                _AreaB_ChartArea.Position.Height = 25;
                _AreaC_ChartArea.Position.Height = 25;
                _AreaA_ChartArea.Position.Y = 0;
                _AreaB_ChartArea.Position.Y = 50;
                _AreaC_ChartArea.Position.Y = 75;
            }
            else if (vAreaA == true && vAreaB == false && vAreaC == false)
            {
                _AreaA_ChartArea.Position.Height = 100;
                if (_AreaB_ChartArea != null)
                {
                    _AreaB_ChartArea.Position.Height = 0;
                }

                if (_AreaC_ChartArea != null)
                {
                    _AreaC_ChartArea.Position.Height = 0;
                }
               
            }
            else if (vAreaA == true && vAreaB == true && vAreaC == false)
            {
                _AreaA_ChartArea.Position.Height = 75;
                if (_AreaB_ChartArea != null)
                {
                    _AreaB_ChartArea.Position.Height = 25;
                    _AreaB_ChartArea.Position.Y = 75;
                }

                if (_AreaC_ChartArea != null)
                {
                    _AreaC_ChartArea.Position.Height = 0;
                }
            }
            else if (vAreaA == true && vAreaB == false && vAreaC == true)
            {
                _AreaA_ChartArea.Position.Height = 75;
                if (_AreaB_ChartArea != null)
                {
                    _AreaB_ChartArea.Position.Height = 0;
                }

                if (_AreaC_ChartArea != null)
                {
                    _AreaC_ChartArea.Position.Height = 25;
                    _AreaB_ChartArea.Position.Y = 75;
                }
            }
        }
        #endregion

        #region 전역변수
        // 전역변수
        private String _StockCode;
        private String _StockName;
        private String _FromDate;
        private String _ToDate;

        private Boolean _AreaA;
        private Boolean _Price;
        private Boolean _Curve_Price;
        private Boolean _Pr_Gain;
        private Boolean _Pr_Fore;
        private Boolean _Pr_Gigan;
        private Boolean _Pr_Gumy;
        private Boolean _Pr_Bohum;
        private Boolean _Pr_Tosin;
        private Boolean _Pr_Gita;
        private Boolean _Pr_Bank;
        private Boolean _Pr_Yeongi;
        private Boolean _Pr_Samo;
        private Boolean _Pr_Nation;
        private Boolean _Pr_Bubin;
        private Boolean _Pr_IoFore;

        private Double _Col_Pr_Gain;
        private Double _Col_Pr_Fore;
        private Double _Col_Pr_Gigan;
        private Double _Col_Gumy;
        private Double _Col_Bohum;
        private Double _Col_Tosin;
        private Double _Col_Gita;
        private Double _Col_Bank;
        private Double _Col_Yeongi;
        private Double _Col_Samo;
        private Double _Col_Nation;
        private Double _Col_Bubin;
        private Double _Col_IoFore;

        private Boolean _AreaB;
        private Boolean _Volume;

        private Boolean _AreaC;
        private Boolean _Gain;
        private Boolean _Fore;
        private Boolean _Gigan;
        private Boolean _Gumy;
        private Boolean _Bohum;
        private Boolean _Tosin;
        private Boolean _Gita;
        private Boolean _Bank;
        private Boolean _Yeongi;
        private Boolean _Samo;
        private Boolean _Nation;
        private Boolean _Bubin;
        private Boolean _IoFore;   
    
        #endregion

        #region DataTable 전역변수
        private DataTable _DtOpt10060_Qty;
        private DataTable _DtOpt10081;
        private DataTable _DtNuOpt10059Qty;
        private DataTable _DtNuOpt10059Price;
        private DataTable _DtSmm01;
        private DataTable _DtSmm02;
        #endregion

        #region ChartArea & Series 전역변수
        [TypeConverter(typeof(ChartAreaPositionConvert))]
        public ChartArea AreaA_ChartArea { get { return _AreaA_ChartArea; } set { _AreaA_ChartArea = value; } }
        [TypeConverter(typeof(ChartAreaPositionConvert))]
        public ChartArea AreaB_ChartArea { get { return _AreaB_ChartArea; } set { _AreaB_ChartArea = value; } }
        [TypeConverter(typeof(ChartAreaPositionConvert))]
        public ChartArea AreaC_ChartArea { get { return _AreaC_ChartArea; } set { _AreaC_ChartArea = value; } }

        private ChartArea _AreaA_ChartArea;
        private Series _Price_Series;
        private Series _Curve_Price_Series;
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
        private ChartArea _AreaB_ChartArea;
        private Series _Volume_Series;
        
        private ChartArea _AreaC_ChartArea;
        private Series _Gain_Series;
        private Series _Fore_Series;
        private Series _Gigan_Series;
        private Series _Gumy_Series;
        private Series _Bohum_Series;
        private Series _Tosin_Series;
        private Series _Gita_Series;
        private Series _Bank_Series;
        private Series _Yeongi_Series;
        private Series _Samo_Series;
        private Series _Nation_Series;
        private Series _Bubin_Series;
        private Series _IoFore_Series;
        #endregion

        #region Attribute
        #region 기본 Attribute
        // Code
        [CategoryAttribute("1. 기본정보"),
        ReadOnlyAttribute(true),
        DefaultValueAttribute("")]
        public String Code { get { return _StockCode; } set { _StockCode = value; DoChangedBaseChartProp("기본정보", ParamIndex.Code); } }

        // Name
        [CategoryAttribute("1. 기본정보"),
        ReadOnlyAttribute(true),
        DefaultValueAttribute("")]
        public String Name { get { return _StockName; } set { _StockName = value; } }

        // 기간
        [CategoryAttribute("1. 기본정보"),
        DefaultValueAttribute("")]
        public String 시작일자 { get { return _FromDate; } set { _FromDate = value; DoChangedBaseChartProp("시작일자", ParamIndex.FromDate); } }

        // 기간
        [CategoryAttribute("1. 기본정보"),
        DefaultValueAttribute("")]
        public String 종료일자 { get { return _ToDate; } set { _ToDate = value; DoChangedBaseChartProp("종료일자", ParamIndex.ToDate); } }
        #endregion
        
        #region 가격 Attribute
        // AreaA
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean AreaA { get { return _AreaA; } set { _AreaA = value; DoChangedBaseChartProp("AreaA", ParamIndex.AreaA); } }

        // Price
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean Price { get { return _Price; } set { _Price = value; DoChangedBaseChartProp("Price", ParamIndex.Price);} }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean CurvePrice { get { return _Curve_Price; } set { _Curve_Price = value; DoChangedBaseChartProp("Price", ParamIndex.Price); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 개인평균 { get { return _Pr_Gain; } set { _Pr_Gain = value; DoChangedBaseChartProp("Pr_Gain", ParamIndex.Pr_Gain); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 외국인평균 { get { return _Pr_Fore; } set { _Pr_Fore = value; DoChangedBaseChartProp("Pr_Fore", ParamIndex.Pr_Fore); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 기관평균 { get { return _Pr_Gigan; } set { _Pr_Gigan = value; DoChangedBaseChartProp("Pr_Gigan", ParamIndex.Pr_Gigan); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 금융평균 { get { return _Pr_Gumy; } set { _Pr_Gumy = value; DoChangedBaseChartProp("Pr_Gumy", ParamIndex.Pr_Gumy); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 보험평균 { get { return _Pr_Bohum; } set { _Pr_Bohum = value; DoChangedBaseChartProp("Pr_Bohum", ParamIndex.Pr_Bohum); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 투신평균 { get { return _Pr_Tosin; } set { _Pr_Tosin = value; DoChangedBaseChartProp("Pr_Tosin", ParamIndex.Pr_Tosin); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 기타평균 { get { return _Pr_Gita; } set { _Pr_Gita = value; DoChangedBaseChartProp("Pr_Gita", ParamIndex.Pr_Gita); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 은행평균 { get { return _Pr_Bank; } set { _Pr_Bank = value; DoChangedBaseChartProp("Pr_Bank", ParamIndex.Pr_Bank); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 연기금평균 { get { return _Pr_Yeongi; } set { _Pr_Yeongi = value; DoChangedBaseChartProp("Pr_Yeongi", ParamIndex.Pr_Yeongi); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 사모평균 { get { return _Pr_Samo; } set { _Pr_Samo = value; DoChangedBaseChartProp("Pr_Samo", ParamIndex.Pr_Samo); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 국가평균 { get { return _Pr_Nation; } set { _Pr_Nation = value; DoChangedBaseChartProp("Pr_Nation", ParamIndex.Pr_Nation); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 법인평균 { get { return _Pr_Bubin; } set { _Pr_Bubin = value; DoChangedBaseChartProp("Pr_Bubin", ParamIndex.Pr_Bubin); } }
        [CategoryAttribute("2. 가격"),
        DefaultValueAttribute(true)]
        public Boolean 기외평균 { get { return _Pr_IoFore; } set { _Pr_IoFore = value; DoChangedBaseChartProp("Pr_IoFore", ParamIndex.Pr_IoFore); } }

        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 개인상관계수 { get { return _Col_Pr_Gain; } set { _Col_Pr_Gain = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 외국인상관계수 { get { return _Col_Pr_Fore; } set { _Col_Pr_Fore = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 기관상관계수 { get { return _Col_Pr_Gigan; } set { _Col_Pr_Gigan = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 금융상관계수 { get { return _Col_Gumy; } set { _Col_Gumy = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 보험상관계수 { get { return _Col_Bohum; } set { _Col_Bohum = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 투신상관계수 { get { return _Col_Tosin; } set { _Col_Tosin = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 기타금융상관계수 { get { return _Col_Gita; } set { _Col_Gita = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 은행상관계수 { get { return _Col_Bank; } set { _Col_Bank = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 연기금상관계수 { get { return _Col_Yeongi; } set { _Col_Yeongi = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 사모상관계수 { get { return _Col_Samo; } set { _Col_Samo = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 국가상관계수 { get { return _Col_Nation; } set { _Col_Nation = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 법인상관계수 { get { return _Col_Bubin; } set { _Col_Bubin = value; } }
        [CategoryAttribute("2-1. 상관계수"),
         DefaultValueAttribute(true)]
        public Double 기외상관계수 { get { return _Col_IoFore; } set { _Col_IoFore = value; } }

        // Volume
        [CategoryAttribute("3. 거래량"),
        DefaultValueAttribute(true)]
        public Boolean AreaB { get { return _AreaB; } set { _AreaB = value; DoChangedBaseChartProp("AreaB", ParamIndex.AreaB); } }

        // Volume
        [CategoryAttribute("3. 거래량"),
        DefaultValueAttribute(true)]
        public Boolean Volume { get { return _Volume; } set { _Volume = value; DoChangedBaseChartProp("Volume", ParamIndex.Volume);} }
        #endregion

        #region 거래량분석 Attribute
        // AreaC
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(true)]
        public Boolean AreaC { get { return _AreaC; } set { _AreaC = value; DoChangedBaseChartProp("AreaC", ParamIndex.AreaC); } }
        // 개인
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(true)]
        public Boolean 개인 { get { return _Gain; } set { _Gain = value; DoChangedBaseChartProp("개인", ParamIndex.Gain);} }
        // 외국인
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(true)]
        public Boolean 외국인 { get { return _Fore; } set { _Fore = value; DoChangedBaseChartProp("외국인", ParamIndex.Fore); } }
        // 기관
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(true)]
        public Boolean 기관 { get { return _Gigan; } set { _Gigan = value; DoChangedBaseChartProp("기관", ParamIndex.Gigan); } }
        // 금융
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 금융 { get { return _Gumy; } set { _Gumy = value; DoChangedBaseChartProp("금융", ParamIndex.Gumy); } }
        // 보험
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 보험 { get { return _Bohum; } set { _Bohum = value; DoChangedBaseChartProp("보험", ParamIndex.Bohum); } }
        // 투신
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 투신 { get { return _Tosin; } set { _Tosin = value; DoChangedBaseChartProp("투신", ParamIndex.Tosin); } }
        // 기타
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 기타 { get { return _Gita; } set { _Gita = value; DoChangedBaseChartProp("기타", ParamIndex.Gita); } }
        // 은행
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 은행 { get { return _Bank; } set { _Bank = value; DoChangedBaseChartProp("은행", ParamIndex.Bank); } }
        // 연기금
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 연기금 { get { return _Yeongi; } set { _Yeongi = value; DoChangedBaseChartProp("연기금", ParamIndex.Yeongi); } }
        // 사모
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 사모 { get { return _Samo; } set { _Samo = value; DoChangedBaseChartProp("사모", ParamIndex.Samo); } }
        // 국가
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 국가 { get { return _Nation; } set { _Nation = value; DoChangedBaseChartProp("국가", ParamIndex.Nation); } }
        // 법인
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 법인 { get { return _Bubin; } set { _Bubin = value; DoChangedBaseChartProp("법인", ParamIndex.Bubin); } }
        // 기타외인
        [CategoryAttribute("4. 거래량분석"),
        DefaultValueAttribute(false)]
        public Boolean 기타외인 { get { return _IoFore; } set { _IoFore = value; DoChangedBaseChartProp("기타외인", ParamIndex.IoFore); } }
        #endregion      
        
        #region Series 속성
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Price_Series { get { return _Price_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Gain_Series { get { return _Pr_Gain_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Fore_Series { get { return _Pr_Fore_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Gigan_Series { get { return _Pr_Gigan_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Gumy_Series { get { return _Pr_Gumy_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Bohum_Series { get { return _Pr_Bohum_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Tosin_Series { get { return _Pr_Tosin_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Gita_Series { get { return _Pr_Gita_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Bank_Series { get { return _Pr_Bank_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Yeongi_Series { get { return _Pr_Yeongi_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Samo_Series { get { return _Pr_Samo_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Nation_Series { get { return _Pr_Nation_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_Bubin_Series { get { return _Pr_Bubin_Series; } }
        [CategoryAttribute("AreaA Series"),
        DefaultValueAttribute(false)]
        public Series Pr_IoFore_Series { get { return _Pr_IoFore_Series; } }
                                                  
        [CategoryAttribute("AreaB Series"),
        DefaultValueAttribute(false)]
        public Series Volume_Series { get { return _Volume_Series; } }

        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Gain_Series{ get { return _Gain_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Fore_Series{ get { return _Fore_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Gigan_Series{ get { return _Gigan_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Gumy_Series{ get { return _Gumy_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Bohum_Series{ get { return _Bohum_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Tosin_Series{ get { return _Tosin_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Gita_Series{ get { return _Gita_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Bank_Series{ get { return _Bank_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Yeongi_Series{ get { return _Yeongi_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Samo_Series{ get { return _Samo_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Nation_Series{ get { return _Nation_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series Bubin_Series{ get { return _Bubin_Series; } }
        [CategoryAttribute("AreaC Series"),
        DefaultValueAttribute(false)]
        public Series IoFore_Series { get { return _IoFore_Series; } }
        #endregion
        #endregion

        #region Attribute 초기 세팅
        private void InitAttribute()
        {
            DataSet ds;
            RichQuery oRichQuery = new RichQuery();

            ds = oRichQuery.p_Set01Query("1", _StockCode, "1", false);

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

            AreaA = ChangeStringToBool(dr["AreaA"].ToString());
            Price = ChangeStringToBool(dr["Price"].ToString());
            개인평균 = ChangeStringToBool(dr["개인평균"].ToString());
            외국인평균 = ChangeStringToBool(dr["외국인평균"].ToString());
            기관평균 = ChangeStringToBool(dr["기관평균"].ToString());
            금융평균 = ChangeStringToBool(dr["금융평균"].ToString());
            보험평균 = ChangeStringToBool(dr["보험평균"].ToString());
            투신평균 = ChangeStringToBool(dr["투신평균"].ToString());
            기타평균 = ChangeStringToBool(dr["기타평균"].ToString());
            은행평균 = ChangeStringToBool(dr["은행평균"].ToString());
            연기금평균 = ChangeStringToBool(dr["연기금평균"].ToString());
            사모평균 = ChangeStringToBool(dr["사모평균"].ToString());
            국가평균 = ChangeStringToBool(dr["국가평균"].ToString());
            법인평균 = ChangeStringToBool(dr["법인평균"].ToString());
            기외평균 = ChangeStringToBool(dr["기외평균"].ToString());
            AreaB = ChangeStringToBool(dr["AreaB"].ToString());
            Volume = ChangeStringToBool(dr["Volume"].ToString());
            AreaC = ChangeStringToBool(dr["AreaC"].ToString());
            개인 = ChangeStringToBool(dr["개인"].ToString());
            외국인 = ChangeStringToBool(dr["외국인"].ToString());
            기관 = ChangeStringToBool(dr["기관"].ToString());
            금융 = ChangeStringToBool(dr["금융"].ToString());
            보험 = ChangeStringToBool(dr["보험"].ToString());
            투신 = ChangeStringToBool(dr["투신"].ToString());
            기타 = ChangeStringToBool(dr["기타"].ToString());
            은행 = ChangeStringToBool(dr["은행"].ToString());
            연기금 = ChangeStringToBool(dr["연기금"].ToString());
            사모 = ChangeStringToBool(dr["사모"].ToString());
            국가 = ChangeStringToBool(dr["국가"].ToString());
            법인 = ChangeStringToBool(dr["법인"].ToString());
            기타외인 = ChangeStringToBool(dr["기타외인"].ToString());
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
            { return "1";}
            else
            { return "0"; }
        }
        public void SaveChartSett()
        {
            if (_StockCode == "" || _StockCode == null) { return; }

            ArrayParam arrParam = new ArrayParam();
            DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "RICHDB");
            String xmlString = "";
            try
            {
                xmlString = "<AreaA>" + ChangeBoolToString(AreaA) + "</AreaA>";
                xmlString = xmlString + "<Price>"  + ChangeBoolToString(Price) + "</Price>";
                xmlString = xmlString + "<개인평균>"  + ChangeBoolToString(개인평균) + "</개인평균>";
                xmlString = xmlString + "<외국인평균>"  + ChangeBoolToString(외국인평균) + "</외국인평균>";
                xmlString = xmlString + "<기관평균>"  + ChangeBoolToString(기관평균) + "</기관평균>";
                xmlString = xmlString + "<금융평균>"  + ChangeBoolToString(금융평균) + "</금융평균>";
                xmlString = xmlString + "<보험평균>"  + ChangeBoolToString(보험평균) + "</보험평균>";
                xmlString = xmlString + "<투신평균>"  + ChangeBoolToString(투신평균) + "</투신평균>";
                xmlString = xmlString + "<기타평균>"  + ChangeBoolToString(기타평균) + "</기타평균>";
                xmlString = xmlString + "<은행평균>"  + ChangeBoolToString(은행평균) + "</은행평균>";
                xmlString = xmlString + "<연기금평균>" + ChangeBoolToString(연기금평균) + "</연기금평균>";
                xmlString = xmlString + "<사모평균>"  + ChangeBoolToString(사모평균)  + "</사모평균>";
                xmlString = xmlString + "<국가평균>"  + ChangeBoolToString(국가평균)  + "</국가평균>";
                xmlString = xmlString + "<법인평균>"  + ChangeBoolToString(법인평균)  + "</법인평균>";
                xmlString = xmlString + "<기외평균>"  + ChangeBoolToString(기외평균)  + "</기외평균>";
                xmlString = xmlString + "<AreaB>"  + ChangeBoolToString(AreaB) + "</AreaB>";
                xmlString = xmlString + "<Volume>"  + ChangeBoolToString(Volume) + "</Volume>";
                xmlString = xmlString + "<AreaC>"  + ChangeBoolToString(AreaC) + "</AreaC>";
                xmlString = xmlString + "<개인>"  + ChangeBoolToString(개인) + "</개인>";
                xmlString = xmlString + "<외국인>"  + ChangeBoolToString(외국인) + "</외국인>";
                xmlString = xmlString + "<기관>"  + ChangeBoolToString(기관) + "</기관>";
                xmlString = xmlString + "<금융>"  + ChangeBoolToString(금융) + "</금융>";
                xmlString = xmlString + "<보험>"  + ChangeBoolToString(보험) + "</보험>";
                xmlString = xmlString + "<투신>"  + ChangeBoolToString(투신) + "</투신>";
                xmlString = xmlString + "<기타>"  + ChangeBoolToString(기타) + "</기타>";
                xmlString = xmlString + "<은행>"  + ChangeBoolToString(은행) + "</은행>";
                xmlString = xmlString + "<연기금>"  + ChangeBoolToString(연기금) + "</연기금>";
                xmlString = xmlString + "<사모>"  + ChangeBoolToString(사모) + "</사모>";
                xmlString = xmlString + "<국가>"  + ChangeBoolToString(국가) + "</국가>";
                xmlString = xmlString + "<법인>"  + ChangeBoolToString(법인) + "</법인>";
                xmlString = xmlString + "<기타외인>"  + ChangeBoolToString(기타외인) + "</기타외인>";
                xmlString = "<SET01>" + xmlString + "</SET01>";
                arrParam.Clear();
                arrParam.Add("@ACTION_GB", "A");
                arrParam.Add("@STOCK_CODE", _StockCode);
                arrParam.Add("@CHART_GB", "1");
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

        #endregion

        #region 차트 초기 세팅
        private void InitChartSetting()
        {
            if (_StockCode == "") {return;}

            for (int ix = _oChart.Series.Count - 1; ix >= 0; ix--)
            {
                _oChart.Series[ix].Points.Clear();  
            }

            //if (_Price_Series != null) { InitSeries(_Price_Series); }
            //if (_Volume_Series != null) { InitSeries(_Volume_Series); }
            //if (_Gain_Series != null) { InitSeries(_Gain_Series); }
            //if (_Fore_Series != null) { InitSeries(_Fore_Series); }
            //if (_Gigan_Series != null) { InitSeries(_Gigan_Series); }
            //if (_Gumy_Series != null) { InitSeries(_Gumy_Series); }
            //if (_Bohum_Series != null) { InitSeries(_Bohum_Series); }
            //if (_Tosin_Series != null) { InitSeries(_Tosin_Series); }
            //if (_Gita_Series != null) { InitSeries(_Gita_Series); }
            //if (_Bank_Series != null) { InitSeries(_Bank_Series); }
            //if (_Yeongi_Series != null) { InitSeries(_Yeongi_Series); }
            //if (_Samo_Series != null) { InitSeries(_Samo_Series); }
            //if (_Nation_Series != null) { InitSeries(_Nation_Series); }
            //if (_Bubin_Series != null) { InitSeries(_Bubin_Series); }
            //if (_IoFore_Series != null) { InitSeries(_IoFore_Series); }          
        }

        private void InitSeries(Series oSeries)
        {
            oSeries.ClearPoints();
        }
        #endregion

        #region 차트 Display
        public void DisplayChart()
        {
            InitChartSetting();
            GetOpt10060Qty();
            GetOpt10081Qty();
            GetOpt1005Scare9QtyNujuk();
            GetOpt1005ScarePriceNujuk();
            VolumeSeries();
            PriceSeries();
            TradeAnaly();
            Correlation();
        }

        #region DisplaySeries
        private void PriceSeries()
        {
            if (_Price_Series == null) { return; }
            Double[] arrP = new Double[1];

            int pt = 0;
            DataView dv = new DataView(_DtOpt10081);
            int high = 0;
            int low = 0;
            float avgPrice = 0;
            dv.Sort = "STOCK_DATE asc";
                        
            foreach (DataRowView dr in dv)
            {
                //if (dr["MAEME_GB"].ToString().Trim() == "2")
                //{
                //    continue;
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

                _oChart.Series[_Price_Series.Name].Points.AddXY((object)dr["STOCK_DATE"], int.Parse(dr["HIGH_PRICE"].ToString()));
                _oChart.Series[_Price_Series.Name].Points[pt].YValues[1] = int.Parse(dr["LOW_PRICE"].ToString());
                _oChart.Series[_Price_Series.Name].Points[pt].YValues[2] = int.Parse(dr["START_PRICE"].ToString());
                _oChart.Series[_Price_Series.Name].Points[pt].YValues[3] = int.Parse(dr["NOW_PRICE"].ToString());
                             
                if (int.Parse(dr["START_PRICE"].ToString()) > int.Parse(dr["NOW_PRICE"].ToString()))
                {
                    _oChart.Series[_Price_Series.Name].Points[pt].Color = System.Drawing.Color.Blue;
                }
                else
                {
                    _oChart.Series[_Price_Series.Name].Points[pt].Color = System.Drawing.Color.Red;
                }

                if (_Pr_Gain_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Gain);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Gain_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice); 
                    }
                }
                if (_Pr_Fore_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Fore);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Fore_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Gigan_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Gigan);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Gigan_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Gumy_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Gumy);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Gumy_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Bohum_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Bohum);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Bohum_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Tosin_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Tosin);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Tosin_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Gita_Series != null)
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Gita);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Gita_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Bank_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Bank);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Bank_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Yeongi_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Yeongi);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Yeongi_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Samo_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Samo);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Samo_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Nation_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Nation);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Nation_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_Bubin_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_Bubin);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_Bubin_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }
                if (_Pr_IoFore_Series != null) 
                {
                    avgPrice = CalPercentVolume(dr["STOCK_DATE"].ToString().Trim(), ParamIndex.Pr_IoFore);

                    if (avgPrice != -999)
                    {
                        _oChart.Series[_Pr_IoFore_Series.Name].Points.AddXY(dr["STOCK_DATE"], avgPrice);
                    }
                }

                pt++;
            }

            _oChart.ChartAreas[_AreaA_ChartArea.Name].AxisY.Minimum = low - (low * 0.1);
            _oChart.ChartAreas[_AreaA_ChartArea.Name].AxisY.Maximum = high + (high * 0.1);
                
        }
        private Int64 CalAvgPrice(String stdDate, ParamIndex pi)
        {
            DataView dv2 = new DataView(_DtNuOpt10059Qty);
            DataView dv3 = new DataView(_DtNuOpt10059Price);

            Int64 iPrice = 0;
            int iQty = 0;
            Int64 avgPrice = 0;
            bool dv2Check = false;
            bool dv3Check = false;
            String QtyColumnName = "";
            String PriceColumnName = "";
   
            dv2.RowFilter = String.Format("STOCK_DATE = '{0}'", stdDate);
            dv3.RowFilter = String.Format("STOCK_DATE = '{0}'", stdDate);

            switch (pi)
	        {
            case ParamIndex.Pr_Gain:
                    QtyColumnName = Common.Class.clsDicDefine.GAIN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GAIN_PRICE;
             break;
            case ParamIndex.Pr_Fore:
                    QtyColumnName = Common.Class.clsDicDefine.FORE_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.FORE_PRICE;
             break;
            case ParamIndex.Pr_Gigan:
                    QtyColumnName = Common.Class.clsDicDefine.GIGAN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GIGAN_PRICE;
             break;
            case ParamIndex.Pr_Gumy:
                    QtyColumnName = Common.Class.clsDicDefine.GUMY_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GUMY_PRICE;
             break;
            case ParamIndex.Pr_Bohum:
                    QtyColumnName = Common.Class.clsDicDefine.BOHUM_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.BOHUM_PRICE;
             break;
            case ParamIndex.Pr_Tosin:
                    QtyColumnName = Common.Class.clsDicDefine.TOSIN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.TOSIN_PRICE;
             break;
            case ParamIndex.Pr_Gita:
                    QtyColumnName = Common.Class.clsDicDefine.GITA_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GITA_PRICE;
             break;
            case ParamIndex.Pr_Bank:
                    QtyColumnName = Common.Class.clsDicDefine.BANK_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.BANK_PRICE;
             break;
            case ParamIndex.Pr_Yeongi:
                    QtyColumnName = Common.Class.clsDicDefine.YEONGI_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.YEONGI_PRICE;
             break;
            case ParamIndex.Pr_Samo:
                    QtyColumnName = Common.Class.clsDicDefine.SAMO_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.SAMO_PRICE;
             break;
            case ParamIndex.Pr_Nation:
                    QtyColumnName = Common.Class.clsDicDefine.NATION_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.NATION_PRICE;
             break;
            case ParamIndex.Pr_Bubin:
                    QtyColumnName = Common.Class.clsDicDefine.BUBIN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.BUBIN_PRICE;
             break;
            case ParamIndex.Pr_IoFore:
                    QtyColumnName = Common.Class.clsDicDefine.IOFORE_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.IOFORE_PRICE;
             break;
	        }
            if (QtyColumnName == "" || PriceColumnName == "")
            {
                return -1;
            }

            foreach (DataRowView drv in dv2)
	        {
                dv2Check = true;
                iQty = (int)drv[QtyColumnName];
	        }
            foreach (DataRowView drv in dv3)
	        {
                dv3Check = true;
		        iPrice = Convert.ToInt32(drv[PriceColumnName]) * 1000000;
	        }

            if (dv2Check == false || dv3Check == false)
            {
                return -1;
            }

             avgPrice = (Int64)(iPrice / iQty);


            if (avgPrice <= 0)
            {
            return -1;
            }
            else
            {
            return avgPrice;
            }
        }

        private float CalPercentVolume(String stdDate, ParamIndex pi)
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
                case ParamIndex.Pr_Gain:
                    QtyColumnName = Common.Class.clsDicDefine.GAIN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GAIN_PRICE;
                    break;
                case ParamIndex.Pr_Fore:
                    QtyColumnName = Common.Class.clsDicDefine.FORE_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.FORE_PRICE;
                    break;
                case ParamIndex.Pr_Gigan:
                    QtyColumnName = Common.Class.clsDicDefine.GIGAN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GIGAN_PRICE;
                    break;
                case ParamIndex.Pr_Gumy:
                    QtyColumnName = Common.Class.clsDicDefine.GUMY_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GUMY_PRICE;
                    break;
                case ParamIndex.Pr_Bohum:
                    QtyColumnName = Common.Class.clsDicDefine.BOHUM_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.BOHUM_PRICE;
                    break;
                case ParamIndex.Pr_Tosin:
                    QtyColumnName = Common.Class.clsDicDefine.TOSIN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.TOSIN_PRICE;
                    break;
                case ParamIndex.Pr_Gita:
                    QtyColumnName = Common.Class.clsDicDefine.GITA_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.GITA_PRICE;
                    break;
                case ParamIndex.Pr_Bank:
                    QtyColumnName = Common.Class.clsDicDefine.BANK_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.BANK_PRICE;
                    break;
                case ParamIndex.Pr_Yeongi:
                    QtyColumnName = Common.Class.clsDicDefine.YEONGI_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.YEONGI_PRICE;
                    break;
                case ParamIndex.Pr_Samo:
                    QtyColumnName = Common.Class.clsDicDefine.SAMO_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.SAMO_PRICE;
                    break;
                case ParamIndex.Pr_Nation:
                    QtyColumnName = Common.Class.clsDicDefine.NATION_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.NATION_PRICE;
                    break;
                case ParamIndex.Pr_Bubin:
                    QtyColumnName = Common.Class.clsDicDefine.BUBIN_QTY;
                    PriceColumnName = Common.Class.clsDicDefine.BUBIN_PRICE;
                    break;
                case ParamIndex.Pr_IoFore:
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

        private void VolumeSeries()
        {
            if (_Volume_Series == null)
            {
                return;
            }

            int pt = 0;
            DataView dv = new DataView(_DtOpt10060_Qty);
            dv.Sort = "STOCK_DATE asc";

            foreach (DataRowView dr in dv)
            {
                if (dr["MAEME_GB"].ToString().Trim() == "2")
                {
                    continue;
                }


                _oChart.Series[_Volume_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr["TRADE_QTY"].ToString()));
                int curCnt = _oChart.Series[_Volume_Series.Name].Points.Count - 1;
                _oChart.Series[_Volume_Series.Name].Points[curCnt].Color = System.Drawing.Color.Red;

                if (curCnt > 0)
                {
                    Double preVolume = _oChart.Series[_Volume_Series.Name].Points[curCnt - 1].YValues[0];
                    Double CurVolume = _oChart.Series[_Volume_Series.Name].Points[curCnt].YValues[0];

                    if (preVolume < CurVolume)
                    {
                        _oChart.Series[_Volume_Series.Name].Points[curCnt].Color = System.Drawing.Color.Red;
                    }
                    else
                    {
                        _oChart.Series[_Volume_Series.Name].Points[curCnt].Color = System.Drawing.Color.Blue;
                    }
                }

                pt++;
            }

            //_oChart.Series[_Volume_Series.Name].YAxisType = AxisType.Secondary;

        }
        private void TradeAnaly()
        {
            DataView dv = new DataView(_DtNuOpt10059Qty);
            dv.Sort = "STOCK_DATE asc";
            foreach (DataRowView dr in dv)
            {
                if (_Gain_Series != null) { _oChart.Series[_Gain_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GAIN_QTY].ToString())); }
                if (_Fore_Series != null) { _oChart.Series[_Fore_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.FORE_QTY].ToString())); }
                if (_Gigan_Series != null) { _oChart.Series[_Gigan_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GIGAN_QTY].ToString())); }
                if (_Gumy_Series != null) { _oChart.Series[_Gumy_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GUMY_QTY].ToString())); }
                if (_Bohum_Series != null) { _oChart.Series[_Bohum_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BOHUM_QTY].ToString())); }
                if (_Tosin_Series != null) { _oChart.Series[_Tosin_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.TOSIN_QTY].ToString())); }
                if (_Gita_Series != null) { _oChart.Series[_Gita_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.GITA_QTY].ToString())); }
                if (_Bank_Series != null) { _oChart.Series[_Bank_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BANK_QTY].ToString())); }
                if (_Yeongi_Series != null) { _oChart.Series[_Yeongi_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.YEONGI_QTY].ToString())); }
                if (_Samo_Series != null) { _oChart.Series[_Samo_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.SAMO_QTY].ToString())); }
                if (_Nation_Series != null) { _oChart.Series[_Nation_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.NATION_QTY].ToString())); }
                if (_Bubin_Series != null) { _oChart.Series[_Bubin_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.BUBIN_QTY].ToString())); }
                if (_IoFore_Series != null) { _oChart.Series[_IoFore_Series.Name].Points.AddXY(dr["STOCK_DATE"], double.Parse(dr[Common.Class.clsDicDefine.IOFORE_QTY].ToString())); }
                
            }

        }
        #endregion

        #region 상관계수
        private void Correlation()
        {

            //double[] arrStockDate = _DtOpt10081.AsEnumerable().Select(row => Convert.ToDouble(row.Field<String>("STOCK_DATE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrPrice = _DtOpt10081.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("NOW_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrGainPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.GAIN_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrForePrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.FORE_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrGiganPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.GIGAN_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrGumyPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.GUMY_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrBohumPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.BOHUM_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrTosinPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.TOSIN_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrGitaPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.GITA_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrBankPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.BANK_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrYeongiPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.YEONGI_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrSamoPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.SAMO_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrNationPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.NATION_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrBubinPrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.BUBIN_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            double[] arrIoForePrice = _DtNuOpt10059Price.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>(Common.Class.clsDicDefine.IOFORE_PRICE), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            
            개인상관계수 = oTechStats.Tech_Correlation(arrPrice, arrGainPrice);
            arrGainPrice  = null;
            외국인상관계수 = oTechStats.Tech_Correlation(arrPrice, arrForePrice);
            arrForePrice = null;
            기관상관계수 = oTechStats.Tech_Correlation(arrPrice, arrGiganPrice);
            arrGiganPrice = null;            
            금융상관계수 = oTechStats.Tech_Correlation(arrPrice, arrGumyPrice);
            arrGumyPrice = null;
            보험상관계수 = oTechStats.Tech_Correlation(arrPrice, arrBohumPrice);
            arrBohumPrice = null;
            투신상관계수 = oTechStats.Tech_Correlation(arrPrice, arrTosinPrice);
            arrTosinPrice = null;
            기타금융상관계수 = oTechStats.Tech_Correlation(arrPrice, arrGitaPrice);
            arrGitaPrice = null;
            은행상관계수 = oTechStats.Tech_Correlation(arrPrice, arrBankPrice);
            arrBankPrice = null;
            연기금상관계수 = oTechStats.Tech_Correlation(arrPrice, arrYeongiPrice);
            arrYeongiPrice = null;
            사모상관계수 = oTechStats.Tech_Correlation(arrPrice, arrSamoPrice);
            arrSamoPrice = null;
            국가상관계수 = oTechStats.Tech_Correlation(arrPrice, arrNationPrice);
            arrNationPrice = null;
            법인상관계수 = oTechStats.Tech_Correlation(arrPrice, arrBubinPrice);
            arrBubinPrice = null;
            기외상관계수 = oTechStats.Tech_Correlation(arrPrice, arrIoForePrice);
            arrIoForePrice = null;

            // Tuple<double, double> p = oTechStats.Tech_FitLine(arrStockDate, arrPrice);
        }
        #endregion

        #endregion

        #region GetData
        private void GetOpt10081Qty()
        {
            if (_StockCode == null || _StockCode.Trim() == "")
            {
                return;
            }
            if (_DtOpt10081 != null)
            {
                _DtOpt10081.Reset();
            }

            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            _DtOpt10081 = oKiwoomQuery.p_Opt10081Query("2", _StockCode, _FromDate, _ToDate, false).Tables[0].Copy();

        }
        private void GetOpt10060Qty()
        {
            if (_StockCode == null || _StockCode.Trim() == "")
            {
                return;
            }
            if (_DtOpt10060_Qty != null)
            {
                _DtOpt10060_Qty.Reset();
            }

            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            _DtOpt10060_Qty = oKiwoomQuery.p_Opt10060_QtyQuery("2", _StockCode, _FromDate, _ToDate, false).Tables[0].Copy();

        }
        private void GetOpt1005Scare9QtyNujuk()
        {
            if (_StockCode == null || _StockCode.Trim() == "")
            {
                return;
            }
            if (_DtNuOpt10059Qty != null)
            {
                _DtNuOpt10059Qty.Reset();
            }

            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            _DtNuOpt10059Qty = oKiwoomQuery.p_NuOPT10059QtyQuery("1", _StockCode, _FromDate, _ToDate, false).Tables[0].Copy();
            _DtSmm02 = oKiwoomQuery.p_Smm02Query("1", _StockCode, false).Tables[0].Copy();

          
        }
        private void GetOpt1005ScarePriceNujuk()
        {
            if (_StockCode == null || _StockCode.Trim() == "")
            {
                return;
            }
            if (_DtNuOpt10059Price != null)
            {
                _DtNuOpt10059Price.Reset();
            }

            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            DataSet ds;

            ds = oKiwoomQuery.p_NuOPT10059PriceQuery("2", _StockCode, "", "", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                if (System.Windows.Forms.MessageBox.Show("NU_OPT10059Price의 최종내역이 없습니다. 작업을 하시겠습니까?", "생성작업", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ds = oKiwoomQuery.p_Opt10059PriceNujukQuery("1", _StockCode, "", false);
                    System.Windows.Forms.MessageBox.Show("NU_OPT10059Price 작업 완료");
                    ds.Reset();
                }
            }
            else
            { ds.Reset(); }
            

            ds = oKiwoomQuery.p_Smm01Query("1", _StockCode, false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                if (System.Windows.Forms.MessageBox.Show("Smm01의 최종내역이 없습니다. 작업을 하시겠습니까?", "생성작업", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ArrayParam arrParam = new ArrayParam();
                    DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _StockCode);
                    arrParam.Add("@MIMA_GB", "");
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_SMM01Add", CommandType.StoredProcedure, arrParam);
                }
            }
            else
            { ds.Reset(); }
                        
            _DtNuOpt10059Price = oKiwoomQuery.p_NuOPT10059PriceQuery("1", _StockCode, _FromDate, _ToDate, false).Tables[0].Copy();
            _DtSmm01 = oKiwoomQuery.p_Smm01Query("1", _StockCode, false).Tables[0].Copy();

       
        }
        #endregion

        public class ChartAreaPositionConvert : ExpandableObjectConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                { return true; }

                return base.CanConvertFrom(context, sourceType);
            }
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(ChartArea))
                {
                    return true;
                }
                return base.CanConvertTo(context, destinationType);
            }
            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(System.String) && value is ChartArea)
                {
                    ChartArea stChartAp = (ChartArea)value;
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                if (value is string)
                {
                    try
                    {
                        
                            ChartArea st = new ChartArea();

                            return st;
           
                    }
                    catch { throw new ArgumentException("변환할 수 없습니다"); }
                }

                return base.ConvertFrom(context, culture, value);
            }
        }

    }
}
