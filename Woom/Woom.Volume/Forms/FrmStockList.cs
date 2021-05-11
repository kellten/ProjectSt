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
            GetHighestUpRateBySector();

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



    }
}
