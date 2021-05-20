using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Collections.Concurrent;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess;
using Woom.DataDefine;
using Woom.DataAccess.OptCaller.Class;
using Woom.DataAccess.PlugIn;
using Woom.DataDefine.OptData;
using SDataAccess;

namespace Woom.Volume.Forms
{
    public partial class FrmStockList : Form
    {
        private DataTable _dt;
        private Woom.DataAccess.OptCaller.Class.ClsGetKoaStudioMethod _clsGetKoaStudioMethod = new DataAccess.OptCaller.Class.ClsGetKoaStudioMethod();
        private ClsOpt10001 _clsOpt10001;
        private const string _FormId = "20";
        private int _rowIndex = 0;
        private Woom.DataDefine.Util.ClsUtil clsUtil;
        public FrmStockList()
        {
            InitializeComponent();
            _dt = new DataTable();
            /// 999 - 코스피, 코스닥
            _dt = _clsGetKoaStudioMethod.GetCodeListByMarketCallBackDataTable("0").Copy();
            //TestCode();
           // GetHighestUpRateBySector();

            clsUtil = new DataDefine.Util.ClsUtil();

            dtpStartDate.Value = clsUtil.MondayDateOnWeekTypeDateTime(dtpEndDate.Value.AddMonths(-3));

        }

        #region
        private bool GetHighestUpRateBySector()
        {

            int i = 0;

            //TaskCompletionSource<bool> tcs = null;
            //tcs = new TaskCompletionSource<bool>();

            //if (tcs == null || tcs.Task.IsCompleted)
            //{
            //    return true;
            //}

            ClsColumnSets oBasicDataType = new ClsColumnSets();

            foreach (int k in Enum.GetValues(typeof(ClsColumnSets.Column10001Index)))
            {
                int j = 0;
                j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(ClsColumnSets.Column10001Index), k));
                dgv0.Columns.Add(oBasicDataType.GetDataGridViewColumn((ClsColumnSets.ColumnNameIndex)j));
            }

            i = 0;

            foreach (DataRow dr in _dt.Rows)
            {

                if (dr["STOCK_CODE"].ToString().Trim() == "")
                {
                    continue;
                }

                dgv0.Rows.Add();
                dgv0.Rows[i].Cells["STOCK_NAME"].Value = ClsAxKH.GetMasterCodeName(dr["STOCK_CODE"].ToString());
                dgv0.Rows[i].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString();
                dgv0.Rows[i].Cells["LAST_PRICE"].Value = _clsGetKoaStudioMethod.GetMasterLastPrice(dr["STOCK_CODE"].ToString());

                i = i + 1;
            }

            _rowIndex = dgv0.RowCount - 1;

            GetOpt10001Caller(0);

            return true;
        }

        private void GetOpt10001Caller(int i)
        {
            if (dgv0.RowCount - 1 == i)
            {
                MessageBox.Show("작업이 완료되었습니다.");
                return;
            }
            if (_clsOpt10001 != null)
            {
                _clsOpt10001.Dispose();
                _clsOpt10001 = null;
            }

            _clsOpt10001 = new ClsOpt10001();
            ClsAxKH.AxKH_10001_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10001);
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            _rowIndex = i;

            _clsOpt10001.SetInit(_FormId);
            _clsOpt10001.JustRequest(dgv0.Rows[i].Cells["STOCK_CODE"].Value.ToString().Trim(), "", 0);

            tcs.SetResult(true);

        }

        private void OnReceiveTrData_Opt10001(string stockCode, DataTable dt, int sPreNext)
        {
            try
            {
                for (int i = 7; i < dgv0.Columns.Count - 1; i++)
                {
                    dgv0.Rows[_rowIndex].Cells[i].Value = dt.Rows[0][dgv0.Columns[i].Name.ToString().Trim()].ToString().Trim();
                }

                ClsAxKH.AxKH_10001_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10001);

                GetOpt10001Caller(_rowIndex + 1);
            }
            catch (Exception)
            {
                ClsAxKH.AxKH_10001_OnReceived -= new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10001);
                throw;
            }

        }
        #endregion

        #region 기간별상승종목
        private void GetOpt10015Data()
        {
            SDataAccess.KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            ClsColumnSets oBasicDataType = new ClsColumnSets();

            RemoveGridViewRow(dgvGiganUpDown);
            RemoveGridViewColumn(dgvGiganUpDown);

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            if (ChkOption1.Checked == true)
            {
                 dt = kiwoomQuery.p_Opt10015DateQuery(query: "2", stockCode: "", stockDate: "", fromDate: clsUtil.MondayDateOnWeekTypeDateTime(dtpStartDate.Value).ToString("yyyyMMdd"), toDate: dtpEndDate.Value.ToString("yyyyMMdd"), upDownRate: numUpDownRate.Value, bln3tier: false).Tables[0].Copy();
            }
            else
            {
                 dt = kiwoomQuery.p_Opt10015DateQuery(query: "1", stockCode: "", stockDate: "", fromDate: clsUtil.MondayDateOnWeekTypeDateTime(dtpStartDate.Value).ToString("yyyyMMdd"), toDate: dtpEndDate.Value.ToString("yyyyMMdd"), upDownRate: numUpDownRate.Value, bln3tier: false).Tables[0].Copy();
            }

            dt2 = kiwoomQuery.p_Opt10015DateQuery(query: "3",  stockCode: "", stockDate: "", fromDate: "", toDate: "", upDownRate: 0, bln3tier: false).Tables[0].Copy();

            if (dt.Rows.Count < 1)
            {
                dt = null;
            }
            else
            {
                int row = 0;
                dgvGiganUpDown.Columns.Add(columnName: "KIFGP_NAME", headerText: "테마명");
                dgvGiganUpDown.Columns.Add(columnName: "STOCK_NAME", headerText: "종목명");
                dgvGiganUpDown.Columns.Add(columnName: "TODAY_DAEBI", headerText: "현재가대비");

                foreach (DataColumn dc in dt.Columns)
                {
                    System.Windows.Forms.DataGridViewColumn dgvColumn = new System.Windows.Forms.DataGridViewColumn();
                    System.Windows.Forms.DataGridViewCell cell = new System.Windows.Forms.DataGridViewTextBoxCell();
                    dgvColumn.CellTemplate = cell;
                    dgvColumn.HeaderText = oBasicDataType.ColumnToKoreanOpt10015(dc.ColumnName.ToString()).ToString();
                    dgvColumn.Name = dc.ColumnName.ToString();

                    dgvGiganUpDown.Columns.Add(dgvColumn);
                }
               
                foreach (DataRow dr in dt.Rows)
                {
                    dgvGiganUpDown.Rows.Add();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName.ToString() == "STOCK_CODE")
                        {
                            dgvGiganUpDown.Rows[row].Cells["STOCK_NAME"].Value = ClsAxKH.GetMasterCodeName(dr["STOCK_CODE"].ToString());
                        }
                        dgvGiganUpDown.Rows[row].Cells[columnName: dc.ColumnName.ToString()].Value = dr[dc.ColumnName.ToString()].ToString();
                    }

                    DataTable dtSt = dt2.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == dr["STOCK_CODE"].ToString().Trim()).CopyToDataTable();

                    dgvGiganUpDown.Rows[row].Cells["TODAY_DAEBI"].Value = clsUtil.StockRate(Math.Abs(Convert.ToInt32(dtSt.Rows[0]["LAST_PRICE"])), Math.Abs(Convert.ToInt32(dr["LAST_PRICE"]))).ToString();

                    dt3 = kiwoomQuery.p_KIFSTQuery(query: "1", kifgpCode: "", stockCode: dr["STOCK_CODE"].ToString().Trim(), bln3tier: false).Tables[0].Copy();

                    if (dt3 != null)
                    {

                        string kifgp_name = "";

                        foreach (DataRow dr2th in dt3.Rows)
                        {
                            if (kifgp_name == "")
                            {
                                kifgp_name =dr2th["KIFGP_NAME"].ToString().Trim();
                            }
                            else
                            {
                                kifgp_name = kifgp_name + "," + dr2th["KIFGP_NAME"].ToString().Trim();
                            }
                        }

                        dgvGiganUpDown.Rows[row].Cells["KIFGP_NAME"].Value = kifgp_name;
                    }
                    else
                    {
                        dgvGiganUpDown.Rows[row].Cells["KIFGP_NAME"].Value = "";

                    }

                    dt3 = null;

                    row = row + 1;
                }

                tcs.SetResult(true);

            }
        }

        #endregion

        private void RemoveGridViewRow(DataGridView dgv)
        {
            do
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    try
                    {
                        dgv.Rows.Remove(row);
                    }
                    catch (Exception) { }
                }
            } while (dgv.Rows.Count > 1);

        }

        private void RemoveGridViewColumn(DataGridView dgv)
        {
            do
            {
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    try
                    {
                        dgv.Columns.Remove(column);
                    }
                    catch (Exception) { }
                }
            } while (dgv.Columns.Count > 1);

        }

        private void BtnGiganUpDowndSearch_Click(object sender, EventArgs e)
        {
            GetOpt10015Data();
        }

        
    }
}
