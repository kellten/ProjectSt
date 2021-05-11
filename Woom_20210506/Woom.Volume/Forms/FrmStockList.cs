using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace Woom.Volume.Forms
{
    public partial class FrmStockList : Form
    {
        private DataTable _dt;
        private Woom.DataAccess.OptCaller.Class.ClsGetKoaStudioMethod _clsGetKoaStudioMethod = new DataAccess.OptCaller.Class.ClsGetKoaStudioMethod();
        private ClsOpt10001 _clsOpt10001;
        public FrmStockList()
        {
            InitializeComponent();
            _dt = new DataTable();
            /// 999 - 코스피, 코스닥
            _dt = _clsGetKoaStudioMethod.GetCodeListByMarketCallBackDataTable("999").Copy();
            ClsAxKH.AxKH_10001_OnReceived += new ClsAxKH.OnReceivedEventHandler(OnReceiveTrData_Opt10001);
            GetHighestUpRateBySector();
        }       

        private  bool GetHighestUpRateBySector()
        {

            int i = 0;

            _clsOpt10001 = new ClsOpt10001();
            _clsOpt10001.SetInit("01");

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return true;
            }

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

                _clsOpt10001.JustRequest(StockCode:dr["STOCK_CODE"].ToString(), StockName:dgv0.Rows[i].Cells["STOCK_NAME"].Value.ToString(), nPrevNext:0);
                                                
                i = i + 1;
            }

            tcs.SetResult(true);



            return true;
        }

    

        private void OnReceiveTrData_Opt10001(string stockCode, DataTable dt, int sPreNext)
        {

        }
    }
}
