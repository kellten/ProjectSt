using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using AnalysisSt.DataBaseFunc;

namespace AnalysisSt.Chart.Class
{
    #region Const
    //public const String CONST_CHARTAREA_PRICE = "가격";

    //public const String CONST_SERIES_CHARTAREA_PRICE = "Price";
    //public const String CONST_GAIN_QTY = "개인량";
    //public const String CONST_FORE_QTY = "외국인량";
    //public const String CONST_GIGAN_QTY = "기관량";
    //public const String CONST_GUMY_QTY = "금융량";
    //public const String CONST_BOHUM_QTY = "보험량";
    //public const String CONST_TOSIN_QTY = "투신량";
    //public const String CONST_GITA_QTY = "기타금융량";
    //public const String CONST_BANK_QTY = "은행량";
    //public const String CONST_YEONGI_QTY = "연기금량";
    //public const String CONST_SAMO_QTY = "사모량";
    //public const String CONST_NATION_QTY = "국가량";
    //public const String CONST_BUBIN_QTY = "기법량";
    //public const String CONST_IOFORE_QTY = "기외량";
    //public const String CONST_GIGAN_SUM_QTY = "기관합량";

    //public const String CONST_GAIN_PRICE = "개인가격";
    //public const String CONST_FORE_PRICE = "외국인가격";
    //public const String CONST_GIGAN_PRICE = "기관가격";
    //public const String CONST_GUMY_PRICE = "금융가격";
    //public const String CONST_BOHUM_PRICE = "보험가격";
    //public const String CONST_TOSIN_PRICE = "투신가격";
    //public const String CONST_GITA_PRICE = "기타금융가격";
    //public const String CONST_BANK_PRICE = "은행가격";
    //public const String CONST_YEONGI_PRICE = "연기금가격";
    //public const String CONST_SAMO_PRICE = "사모가격";
    //public const String CONST_NATION_PRICE = "국가가격";
    //public const String CONST_BUBIN_PRICE = "기법가격";
    //public const String CONST_IOFORE_PRICE = "기외가격";
    //public const String CONST_GIGAN_SUM_PRICE = "기관합가격";
    #endregion

    public class BaseChart
    {
        // 전역변수

        private System.Windows.Forms.DataVisualization.Charting.Chart _oChart;
        
        public System.Windows.Forms.DataVisualization.Charting.Chart OChart { get { return _oChart; } set { _oChart = value; } }

        public enum SeriesIndex
        {
            Price = 0, Curve_Price, Pr_Gain, Pr_Fore, Pr_Gigan, Pr_Gumy, Pr_Bohum, Pr_Tosin, Pr_Gita, Pr_Bank, Pr_Yeongi, Pr_Samo, Pr_Nation, Pr_Bubin, Pr_IoFore,
            Volume, 
            Gain, Fore, Gigan, Gumy, Bohum, Tosin, Gita, Bank, Yeongi, Samo,
            Nation, Bubin, IoFore
        }

        public enum MoveAvgSeriesIndex
        {
            Ma_3, Ma_5, Ma_10, Ma_20, Ma_42, Ma_60, Ma_90, Ma_120, Ma_200, Ma_480, Ma_1000
        }
        public enum AreasIndex { Price = 0, Volume = 1, TradeAnaly = 2}
        
        #region func
        private void InitData()
        {

        }

        private void InitChart()
        {
            if (_oChart == null)
            {
                return;
            }

        }
        #endregion

        #region 차트 관련
        /// <summary>
        /// 차트 영역을 생성
        /// </summary>
        /// <param name="ai"></param>
        public ChartArea CreateAreas(AreasIndex ai)
        {
            ChartArea ce = new ChartArea();
            clsChartAreaSetting arSett = new clsChartAreaSetting();

            switch (ai)
            {
                case AreasIndex.Price:
                    ce.Name = "Price";
                    arSett.PriceAreaSetting(ce);
                    break;
                case AreasIndex.Volume:
                    ce.Name = "Volume";
                    arSett.VolumeAreaSetting(ce);
                    break;
                case AreasIndex.TradeAnaly:
                    ce.Name = "TradeAnaly";
                    arSett.TradeAnlyAreaSetting(ce);
                    break;
                default:
                    break;
            }
            
            return ce;
        }
        /// <summary>
        /// 차트 시리얼을 생성
        /// </summary>
        /// <param name="si"></param>
        public Series CreateSeries(SeriesIndex si)
        {
            Series se = new Series();
            clsChartSeriesSetting seSett = new clsChartSeriesSetting();
            switch (si)
            {
                case SeriesIndex.Price:
                    se.Name = "Price";
                    seSett.PriceSeriesSetting(se);
                    break;
                case SeriesIndex.Curve_Price:
                    se.Name = "Curve_Price";
                    seSett.CurvePriceSeriesSetting(se);
                    break;
                case SeriesIndex.Pr_Gain:
                    se.Name = "Pr_Gain";
                    seSett.GainSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Fore:
                    se.Name = "Pr_Fore";
                    seSett.ForeSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Gigan:
                    se.Name = "Pr_Gigan";
                    seSett.GiganSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Gumy:
                    se.Name = "Pr_Gumy";
                    seSett.GumySeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Bohum:
                    se.Name = "Pr_Bohum";
                    seSett.BohumSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Tosin:
                    se.Name = "Pr_Tosin";
                    seSett.TosinSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Gita:
                    se.Name = "Pr_Gita";
                    seSett.GitaSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Bank:
                    se.Name = "Pr_Bank";
                    seSett.BankSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Yeongi:
                    se.Name = "Pr_Yeongi";
                    seSett.YeongiSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Samo:
                    se.Name = "Pr_Samo";
                    seSett.SamoSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Nation:
                    se.Name = "Pr_Nation";
                    seSett.NationSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_Bubin:
                    se.Name = "Pr_Bubin";
                    seSett.BubinSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Pr_IoFore:
                    se.Name = "Pr_IoFore";
                    seSett.IoForeSeriesSetting(se);
                    se.YAxisType = AxisType.Secondary;
                    break;
                case SeriesIndex.Volume:
                    se.Name = "Volume";
                    seSett.VolumeSeriesSetting(se);
                    break;
                case SeriesIndex.Gain:
                    se.Name = "Gain";
                    seSett.GainSeriesSetting(se);
                    break;
                case SeriesIndex.Fore:
                    se.Name = "Fore";
                    seSett.ForeSeriesSetting(se);
                    break;
                case SeriesIndex.Gigan:
                    se.Name = "Gigan";
                    seSett.GiganSeriesSetting(se);
                    break;
                case SeriesIndex.Gumy:
                    se.Name = "Gumy";
                    seSett.GumySeriesSetting(se);
                    break;
                case SeriesIndex.Bohum:
                    se.Name = "Bohum";
                    seSett.BohumSeriesSetting(se);
                    break;
                case SeriesIndex.Tosin:
                    se.Name = "Tosin";
                    seSett.TosinSeriesSetting(se);
                    break;
                case SeriesIndex.Gita:
                    se.Name = "Gita";
                    seSett.GitaSeriesSetting(se);
                    break;
                case SeriesIndex.Bank:
                    se.Name = "Bank";
                    seSett.BankSeriesSetting(se);
                    break;
                case SeriesIndex.Yeongi:
                    se.Name = "Yeongi";
                    seSett.YeongiSeriesSetting(se);
                    break;
                case SeriesIndex.Samo:
                    se.Name = "Samo";
                    seSett.SamoSeriesSetting(se);
                    break;
                case SeriesIndex.Nation:
                    se.Name = "Nation";
                    seSett.NationSeriesSetting(se);
                    break;
                case SeriesIndex.Bubin:
                    se.Name = "Bubin";
                    seSett.BubinSeriesSetting(se);
                    break;
                case SeriesIndex.IoFore:
                    se.Name = "IoFore";
                    seSett.IoForeSeriesSetting(se);
                    break;
                default:
                    break;
            }

        
            return se;
        }

        public Series CreateMovAvgSeries(MoveAvgSeriesIndex si)
        {
            Series se = new Series();
            clsChartSeriesSetting seSett = new clsChartSeriesSetting();
            switch (si)
            {
                case MoveAvgSeriesIndex.Ma_3:
                    se.Name = "Ma_3";
                    seSett.Ma3SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_5:
                    se.Name = "Ma_5";
                    seSett.Ma5SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_10:
                    se.Name = "Ma_10";
                    seSett.Ma10SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_20:
                    se.Name = "Ma_20";
                    seSett.Ma20SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_42:
                    se.Name = "Ma_42";
                    seSett.Ma42SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_60:
                    se.Name = "Ma_60";
                    seSett.Ma60SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_90:
                    se.Name = "Ma_90";
                    seSett.Ma90SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_120:
                    se.Name = "Ma_120";
                    seSett.Ma120SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_200:
                    se.Name = "Ma_200";
                    seSett.Ma200SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_480:
                    se.Name = "Ma_480";
                    seSett.Ma480SeriesSetting(se);
                    break;
                case MoveAvgSeriesIndex.Ma_1000:
                    se.Name = "Ma_1000";
                    seSett.Ma1000SeriesSetting(se);
                    break;
                default:
                    break;
            }

            return se;
        }
        #endregion
    }
     
}