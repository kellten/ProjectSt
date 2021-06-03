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
        private int _FormId = 100;
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

            RichQuery richQuery = new RichQuery();

            DataTable dt = richQuery.p_ScodeQuery(query: "1", stockCode: "", ybYongCode: "", bln3tier: false).Tables[0].Copy();

            AutoSCode.Dv = new DataView(dt);
            AutoSCode.OCon = txtStockCode;
            
            AutoSCode.AutoCompCode = "STOCK_CODE";
            AutoSCode.AutoCompString = "STOCK_NAME";

        }

        #region
        private bool GetHighestUpRateBySector()
        {

            int i = 0;

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return true;
            }

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

            tcs.SetResult(true);

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

            _clsOpt10001.SetInit(clsUtil.Right(_FormId.ToString(), 2));
            _clsOpt10001.JustRequest(dgv0.Rows[i].Cells["STOCK_CODE"].Value.ToString().Trim(), "", 0);

            tcs.SetResult(true);

            _FormId = _FormId + 1;


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

        private bool _firstCall = false;

        private void GetOpt10015Data()
        {

            SDataAccess.KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();

            int i = 0;

            string tradeDate = "";

            ClsColumnSets oBasicDataType = new ClsColumnSets();

            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }

            RemoveGridViewRow(dgvGiganUpDown);
            //RemoveGridViewColumn(dgvGiganUpDown);            

            if (ChkOption1.Checked == true)
            {
                 dt = kiwoomQuery.p_Opt10015DateQuery(query: "4", stockCode: "", stockDate: "", fromDate: clsUtil.MondayDateOnWeekTypeDateTime(dtpStartDate.Value).ToString("yyyyMMdd"), toDate: dtpEndDate.Value.ToString("yyyyMMdd"), upDownRate: numUpDownRate.Value, bln3tier: false).Tables[0].Copy();
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
                if (_firstCall == false)
                {

                    DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
                    dataGridViewButtonColumn.Name = "WEB_VIEW";
                    dataGridViewButtonColumn.HeaderText = "View";
                    dataGridViewButtonColumn.ReadOnly = false;
                    
                    dgvGiganUpDown.Columns.Add(dataGridViewButtonColumn);
                    dgvGiganUpDown.Columns.Add(columnName: "KIFGP_NAME", headerText: "테마명");
                    dgvGiganUpDown.Columns.Add(columnName: "STOCK_NAME", headerText: "종목명");
                    dgvGiganUpDown.Columns.Add(columnName: "TODAY_DAEBI", headerText: "현재가대비");
                    dgvGiganUpDown.Columns.Add(columnName: "SEQ_NO", headerText: "일수");

                    dgvGiganUpDown.Columns.Add(columnName: "SEQ_NO", headerText: "일수");

                    foreach (DataColumn dc in dt.Columns)
                    {

                        System.Windows.Forms.DataGridViewColumn dgvColumn = new System.Windows.Forms.DataGridViewColumn();
                        System.Windows.Forms.DataGridViewCell cell = new System.Windows.Forms.DataGridViewTextBoxCell();

                        if (dc.ColumnName.ToString() == "TRADE_DAEGUM")
                        {
                            dgvColumn.CellTemplate = cell;
                            dgvColumn.HeaderText = "대금대비시총";
                            dgvColumn.Name = "대금대비시총";

                            dgvGiganUpDown.Columns.Add(dgvColumn);

                            dgvColumn = null;
                            cell = null;

                            dgvColumn = new System.Windows.Forms.DataGridViewColumn();
                            cell = new System.Windows.Forms.DataGridViewTextBoxCell();
                        }
                        
                        dgvColumn.CellTemplate = cell;
                        dgvColumn.HeaderText = oBasicDataType.ColumnToKoreanOpt10015(dc.ColumnName.ToString()).ToString();
                        dgvColumn.Name = dc.ColumnName.ToString();
                  
                        dgvGiganUpDown.Columns.Add(dgvColumn);
                    }

                }
                                

                int row = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    dgvGiganUpDown.Rows.Add();

                    if (dt4 != null)
                    {
                        dt4 = null;
                    }

                    dt4 = kiwoomQuery.p_Opt10001Query(query: "1", stockCode: dr["STOCK_CODE"].ToString().Trim(), callDate: "", bln3tier: false).Tables[0].Copy();

                    if (dt4 != null)
                    {
                        if (dt4.Rows.Count > 0)
                        {

                            if (_firstCall == false)
                            { 
                                foreach (DataColumn dc in dt4.Columns)
                                {
                                    System.Windows.Forms.DataGridViewColumn dgvColumn = new System.Windows.Forms.DataGridViewColumn();
                                    System.Windows.Forms.DataGridViewCell cell = new System.Windows.Forms.DataGridViewTextBoxCell();

                                    if (dc.ColumnName.ToString() == "STOCK_CODE" || dc.ColumnName.ToString() == "CALL_TIME"
                                        || dc.ColumnName.ToString() == "상한가" || dc.ColumnName.ToString() == "하한가" || dc.ColumnName.ToString() == "기준가"
                                        || dc.ColumnName.ToString() == "시가총액비중" 
                                        || dc.ColumnName.ToString() == "예상체결가" || dc.ColumnName.ToString() == "예상체결수량" || dc.ColumnName.ToString() == "액면가단위" 
                                        || dc.ColumnName.ToString() == "전일대비" || dc.ColumnName.ToString() == "현재가" || dc.ColumnName.ToString() == "전일대비" || dc.ColumnName.ToString() == "등락율" || dc.ColumnName.ToString() == "거래량")
                                    {
                                        continue;
                                    }

                                    if (dc.ColumnName.ToString() == "CALL_DATE")
                                    {
                                        dgvColumn.CellTemplate = cell;
                                        dgvColumn.HeaderText = "기준일자";
                                        dgvColumn.Name = "OPT10001_CALL_DATE";

                                        dgvGiganUpDown.Columns.Add(dgvColumn);

                                        continue;
                                    }

                         
                                    dgvColumn.CellTemplate = cell;
                                    dgvColumn.HeaderText = dc.ColumnName.ToString();
                                    dgvColumn.Name = dc.ColumnName.ToString();

                                    dgvGiganUpDown.Columns.Add(dgvColumn);
                                }

                                _firstCall = true;
                            }
                            
                            foreach (DataColumn dc in dt4.Columns)
                            {

                                if (dc.ColumnName.ToString() == "STOCK_CODE" || dc.ColumnName.ToString() == "CALL_TIME"
                                       || dc.ColumnName.ToString() == "상한가" || dc.ColumnName.ToString() == "하한가" || dc.ColumnName.ToString() == "기준가"
                                       || dc.ColumnName.ToString() == "시가총액비중"
                                       || dc.ColumnName.ToString() == "예상체결가" || dc.ColumnName.ToString() == "예상체결수량" || dc.ColumnName.ToString() == "액면가단위"
                                       || dc.ColumnName.ToString() == "전일대비" || dc.ColumnName.ToString() == "현재가" || dc.ColumnName.ToString() == "전일대비" || dc.ColumnName.ToString() == "등락율" || dc.ColumnName.ToString() == "거래량")
                                {
                                    continue;
                                }

                                if (dc.ColumnName.ToString() == "CALL_DATE")
                                {
                                    dgvGiganUpDown.Rows[row].Cells["OPT10001_CALL_DATE"].Value = dt4.Rows[0][dc.ColumnName.ToString()].ToString();
                                }
                                else
                                { 
                                dgvGiganUpDown.Rows[row].Cells[columnName: dc.ColumnName.ToString()].Value = dt4.Rows[0][dc.ColumnName.ToString()].ToString();
                                }
                            }

                        }
                    
                    }

                    if (tradeDate == "")
                    {
                        tradeDate = dr["STOCK_DATE"].ToString().Trim();
                    }
                    else
                    {
                        if (tradeDate != dr["STOCK_DATE"].ToString().Trim())
                        {
                            tradeDate = dr["STOCK_DATE"].ToString().Trim();
                            i = i - 1;
                        }

                        if (((i * -1) % 2) == 0)
                        {
                            dgvGiganUpDown.Rows[row].DefaultCellStyle.BackColor = Color.AliceBlue;
                        }
                        
                    }

                    dgvGiganUpDown.Rows[row].Cells["SEQ_NO"].Value = i.ToString() + " 전";

                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName.ToString() == "STOCK_CODE")
                        {
                            dgvGiganUpDown.Rows[row].Cells["STOCK_NAME"].Value = ClsAxKH.GetMasterCodeName(dr["STOCK_CODE"].ToString());
                        }
                        dgvGiganUpDown.Rows[row].Cells[columnName: dc.ColumnName.ToString()].Value = dr[dc.ColumnName.ToString()].ToString();
                    }

                    DataTable dtSt = dt2.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == dr["STOCK_CODE"].ToString().Trim()).CopyToDataTable();

                    dgvGiganUpDown.Rows[row].Cells["TODAY_DAEBI"].Value = clsUtil.StockRate(Math.Abs(Convert.ToInt32(dtSt.Rows[0]["LAST_PRICE"])), Math.Abs(Convert.ToInt32(dr["LAST_PRICE"])));

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

                _firstCall = true;

                SetDataGridView();                

                tcs.SetResult(true);

            }
        }
        #endregion
              
        #region SetDataGridView
        private void SetDataGridView()
        {

            int intValue = 0;

            for (int i = 0; i < dgvGiganUpDown.Rows.Count - 1; i++)
            {
                dgvGiganUpDown.Rows[i].Cells["시가총액"].Value = Convert.ToInt32((Convert.ToInt32(dgvGiganUpDown.Rows[i].Cells["상장주식"].Value) * Math.Abs(Convert.ToInt32(dgvGiganUpDown.Rows[i].Cells["LAST_PRICE"].Value))) / 100000) ;

                if (dgvGiganUpDown.Rows[i].Cells["TODAY_DAEBI"].Value.ToString().Contains("-") == true)
                {

                    dgvGiganUpDown.Rows[i].Cells["TODAY_DAEBI"].Style.ForeColor = Color.Red;
                }

                intValue = (int)clsUtil.CalPercent((double) Convert.ToInt32(dgvGiganUpDown.Rows[i].Cells["TRADE_DAEGUM"].Value), (double) Convert.ToInt32(dgvGiganUpDown.Rows[i].Cells["시가총액"].Value));

                dgvGiganUpDown.Rows[i].Cells["대금대비시총"].Value = intValue.ToString();

                // 시총보다 거래대금이 큰 경우 옐로우
                if (intValue > 30)
                {
                    dgvGiganUpDown.Rows[i].Cells["TRADE_DAEGUM"].Style.ForeColor = Color.Green;
                }

                // 시총보다 거래대금이 큰 경우 빨간색
                if (intValue > 100)
                {
                    dgvGiganUpDown.Rows[i].Cells["TRADE_DAEGUM"].Style.ForeColor = Color.Red;
                }
                               
                // 영익이 흑자이면, 빨간색
                if (dgvGiganUpDown.Rows[i].Cells["영업이익"].Value.ToString().Contains("-") == false)
                {

                    dgvGiganUpDown.Rows[i].Cells["영업이익"].Style.ForeColor = Color.Red;
                }

                if (dgvGiganUpDown.Rows[i].Cells["당기순이익"].Value.ToString().Contains("-") == false)
                {

                    dgvGiganUpDown.Rows[i].Cells["당기순이익"].Style.ForeColor = Color.Red;
                }


            }

            dgvGiganUpDown.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }
        #endregion

        #region GetOpt10060Data
        
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

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            if (tcs == null || tcs.Task.IsCompleted)
            {
                return;
            }
            GetOpt10015Data();

            tcs.SetResult(true);
        }
        
        private void dgvGiganUpDown_MouseMove(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = dgvGiganUpDown.HitTest(e.X, e.Y);

        }

        private void BtnGetOpt_Click(object sender, EventArgs e)
        {
            ClsAxKH.SPEED_CALL = true;
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("STOCK_CODE");
                      

            for (int i = 0; i < dgvGiganUpDown.RowCount - 1; i++)
            {
                if (dgvGiganUpDown.Rows[i].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
                {
                    continue;
                }

                if (dt.Rows.Count > 0)
                {

                    var rows = dt.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == dgvGiganUpDown.Rows[i].Cells["STOCK_CODE"].Value.ToString().Trim());

                    foreach (DataRow dr2th in rows)
                    {
                        continue;
                    }
                    
                }

                dr = dt.NewRow();

                dr["STOCK_CODE"] = dgvGiganUpDown.Rows[i].Cells["STOCK_CODE"].Value.ToString().Trim();

                dt.Rows.Add(dr);
            }

            if (dt != null)
            {
                Form oform = new Woom.CallForm.Forms.FrmCallOptLastData(dt, "");
                oform.ShowDialog();
                ClsAxKH.SPEED_CALL = false;
            }
            else
            {
                MessageBox.Show("작업할 내역이 없습니다.");
            }
        }
        private void dgvGiganUpDown_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                System.Diagnostics.Process.Start("https://finance.naver.com/item/coinfo.nhn?code=" + dgvGiganUpDown.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());
                System.Diagnostics.Process.Start("https://finance.naver.com/item/fchart.nhn?code=" + dgvGiganUpDown.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());
            }

        }

        private void txtStockCode_TextChanged(object sender, EventArgs e)
        {
            if ((txtStockCode.Text.Trim()) == "")
            { return; }
            AutoSCode.SendData = txtStockCode.Text;
        }

        private void txtStockCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.Space)
            { 
              
            }

           
        }
    }
}
