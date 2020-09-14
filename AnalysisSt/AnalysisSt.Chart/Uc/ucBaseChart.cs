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

namespace AnalysisSt.Chart.Uc
{
    public partial class ucBaseChart : UserControl
    {
        public ucBaseChart()
        {
            InitializeComponent();
        }

        private string _StockCode;
        private string _StockName;

        public string StockCode { get { return _StockCode; } set { _StockCode = value; SetStockCode(); } }
        public string StockName { get { return _StockName; } set { _StockName = value; } }

        // 전역변수
        private Parameter.ParamBaseChartAttribute _oParamBc = new Parameter.ParamBaseChartAttribute();

        #region Load
        private void ucBaseChart_Load(object sender, EventArgs e)
        {
            _oParamBc.OChart = BaseChart;
            propBasechart.SelectedObject = _oParamBc;
            _oParamBc.onChangedBaseChartProp += new Parameter.ParamBaseChartAttribute.ChangedBaseChartProp(DoChangedBaseChartProp);

            BaseChart.EnableZoomAndPanControls(ChartCursorSelected, ChartCursorMoved, zoomChanged,
                new ChartOption()
                {
                    ContextMenuAllowToHideSeries = true,
                    XAxisPrecision = 0,
                    YAxisPrecision = 2
                });

            dgvSca01Init();
        }
        #endregion

        #region Func
        private void GetSMMData()
        {
            if (_StockCode == "" || _StockCode == null) {return;}

            dgvTradeInfoA.Rows.Clear();
            dgvTradeInfoB.Rows.Clear();
            dgvTradeInfoC.Rows.Clear();

            DataSet ds;
            KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            int i = 0;

            ds = oKiwoomQuery.p_Smm01UnPivotQuery("1", _StockCode, "2", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                return;
            }
            else
            {
                
                Decimal MaSum = ds.Tables[0].AsEnumerable().Sum(x => x.Field<Decimal>("VOLUME_PRICE"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dgvTradeInfoA.Rows.Add();
                    dgvTradeInfoA.Rows[i].Cells["주체"].Value = Common.Class.clsDicDefine.GetVolumeJucheName(dr["WHO_VOLUME"].ToString());
                    dgvTradeInfoA.Rows[i].Cells["최대매입"].Value = dr["VOLUME_PRICE"].ToString();
                    dgvTradeInfoA.Rows[i].Cells["퍼센티지"].Value = PercentMima(MaSum, (Decimal)dr["VOLUME_PRICE"]);

                    i = i + 1;
                }

                ds.Reset();

                dgvTradeInfoA.Sort(dgvTradeInfoA.Columns["퍼센티지"], ListSortDirection.Descending);
                dgvTradeInfoA.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }

            i = 0;

            ds = oKiwoomQuery.p_Smm01UnPivotQuery("1", _StockCode, "1", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
                return;
            }
            else
            {

                Decimal MiSum = ds.Tables[0].AsEnumerable().Sum(x => x.Field<Decimal>("VOLUME_PRICE"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dgvTradeInfoB.Rows.Add();
                    dgvTradeInfoB.Rows[i].Cells["주체"].Value =  Common.Class.clsDicDefine.GetVolumeJucheName(dr["WHO_VOLUME"].ToString());
                    dgvTradeInfoB.Rows[i].Cells["최대매도"].Value = dr["VOLUME_PRICE"].ToString();
                    dgvTradeInfoB.Rows[i].Cells["퍼센티지"].Value = PercentMima(MiSum * - 1, (Decimal)dr["VOLUME_PRICE"] * -1);

                    i = i + 1;
                }

                ds.Reset();

                dgvTradeInfoB.Sort(dgvTradeInfoB.Columns["퍼센티지"], ListSortDirection.Descending);
                dgvTradeInfoB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
        private Decimal PercentMima(Decimal SumValue, Decimal value)
        {
            if (value == 0)
            {
                return 0;
            }

            if (value < 0)
            {
                return -1;
            }
            
            Decimal per = 0;

            per = (value / SumValue) * 100;
            return Decimal.Round(per, 2);
        }

        #endregion

        #region Extension Ver2
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

        #endregion

        #region UserEvent
        private void DoChangedBaseChartProp(String CategoryName, Parameter.ParamBaseChartAttribute.ParamIndex p)
        {
            if (_StockCode == "")
            { return; }

            switch (p)
            {
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.FromDate:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.ToDate:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Price:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Volume:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Gain:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Fore:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Gigan:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Gumy:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Bohum:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Tosin:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Gita:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Bank:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Yeongi:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Samo:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Nation:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.Bubin:
                    break;
                case AnalysisSt.Chart.Parameter.ParamBaseChartAttribute.ParamIndex.IoFore:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region SetParamBaseChartAttribute
        private void SetStockCode()
        {
            GetSMMData();
            _oParamBc.시작일자 = "20170101";
            _oParamBc.종료일자 = "20170831";
            _oParamBc.Name = _StockName;
            _oParamBc.Code = _StockCode;

        }
        #endregion

        #region DataGridView 초기화
         private void dgvSca01Init()
        {
            dgvTradeInfoA.ColumnCount = 3;
            dgvTradeInfoA.Columns[0].Name = "주체";
            dgvTradeInfoA.Columns[1].Name = "최대매입";
            dgvTradeInfoA.Columns[2].Name = "퍼센티지";

            dgvTradeInfoB.ColumnCount = 3;
            dgvTradeInfoB.Columns[0].Name = "주체";
            dgvTradeInfoB.Columns[1].Name = "최대매도";
            dgvTradeInfoB.Columns[2].Name = "퍼센티지";

            dgvTradeInfoC.ColumnCount = 2;
            dgvTradeInfoC.Columns[0].Name = "주체";
            dgvTradeInfoC.Columns[1].Name = "상관계수";
        }
        #endregion

        #region Control Event
         private void btnDisplayChart_Click(object sender, EventArgs e)
         {
             _oParamBc.DisplayChart();
             propBasechart.Refresh();
         }

         private void propBasechart_Click(object sender, EventArgs e)
         {
             MessageBox.Show(e.ToString());
         }

         private void btnReload_Click(object sender, EventArgs e)
         {
             propBasechart.SelectedObject = _oParamBc;
             propBasechart.Refresh();
         }

         private void btnStore_Click(object sender, EventArgs e)
         {
             _oParamBc.SaveChartSett();
         }
         #endregion
        
    }
}
