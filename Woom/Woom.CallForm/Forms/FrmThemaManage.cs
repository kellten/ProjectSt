using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SDataAccess;
using Woom.DataDefine.Util;
using Woom.DataAccess.PlugIn;
using Woom.DataAccess.OptCaller.Class;

namespace Woom.CallForm.Forms
{
    public partial class FrmThemaManage : Form
    {
        private ClsDataGridViewUtil _clsDataGridViewUtil;

        public FrmThemaManage()
        {
            InitializeComponent();

            _clsDataGridViewUtil = new ClsDataGridViewUtil();

            ucStockList1.OnSelectedStockCode += new Uc.UcStockList.OnSelectedStockCodeEventHandler(OnSelectedStockCode);

            GetThemaGroupData();
        }

        #region" 저장 "
        private void ThemaStoreRecord(string actionGb)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

            arrParam.Add("@ACTION_GB", "A");
            arrParam.Add("@THEMA_CODE", lblThema.Text);
            arrParam.Add("@TGPSEQ_NO", lblThemaGroup.Text);
            arrParam.Add("@THEMA_NAME", txtThema.Text);
            arrParam.Add("@THEMA_DESC", txtThemaDesc.Text);
            arrParam.Add("@WORK_ID", "ADMIN00000");
            arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

            oSql.ExecuteNonQuery("p_ThemaAdd", CommandType.StoredProcedure, arrParam);

            GetThemaData();

        }
        private void ThemaGroupStoreRecord(string actionGb)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

            arrParam.Add("@ACTION_GB", actionGb);
            arrParam.Add("@TGPSEQ_NO", 0);
            arrParam.Add("@THGROUP_NAME", txtThemaGroup.Text.Trim());
            arrParam.Add("@THGROUP_DESC", txtThemaGroupDesc.Text.Trim());
            arrParam.Add("@WORK_ID", "ADMIN00000");
            arrParam.Add("@R_ErrorCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

            oSql.ExecuteNonQuery("p_THEMA_GROUPAdd", CommandType.StoredProcedure, arrParam);

            MessageBox.Show("입력되었습니다.");

            GetThemaGroupData();
        }

        private void ThemstStoreRecord(string actionGb, string stockCode)
        {
            ArrayParam arrParam = new ArrayParam();
            Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

            arrParam.Add("@ACTION_GB", actionGb);
            arrParam.Add("@STOCK_CODE", stockCode);
            arrParam.Add("@THEMA_CODE", lblThema.Text);
            arrParam.Add("@DESC_TEXT", "");
            arrParam.Add("@WORK_ID", "ADMIN00000");
            arrParam.Add("@R_ErrorCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

            oSql.ExecuteNonQuery("p_ThestAdd", CommandType.StoredProcedure, arrParam);

            //MessageBox.Show("입력되었습니다.");

            GetThemaByStockCode();

        }
        #endregion

        #region 조회
        private void GetThemaGroupData()
        {
            RichQuery richQuery = new RichQuery();
            DataTable dt = richQuery.p_THEMA_GroupQuery(query: "1", tgpseqNo: "", bln3tier: false).Tables[0].Copy();
            int row = 0;

            _clsDataGridViewUtil.RemoveGridViewRow(dgvThemaGroup);

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgvThemaGroup.Rows.Add();
                    dgvThemaGroup.Rows[row].Cells["THGROUP_NAME"].Value = dr["THGROUP_NAME"].ToString().Trim();
                    dgvThemaGroup.Rows[row].Cells["TGPSEQ_NO"].Value = dr["TGPSEQ_NO"].ToString().Trim();
                    dgvThemaGroup.Rows[row].Cells["THGROUP_DESC"].Value = dr["THGROUP_DESC"].ToString().Trim();

                    row = row + 1;
                }
            }
        }

        private void GetThemaData()
        {
            RichQuery richQuery = new RichQuery();
            DataTable dt = richQuery.p_Thema_GroupQuery2(query: "1", themaCode: "", tgpseqNo: lblThemaGroup.Text.Trim(), bln3tier: false).Tables[0].Copy();
            int row = 0;

            _clsDataGridViewUtil.RemoveGridViewRow(dgvThema);

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgvThema.Rows.Add();
                    dgvThema.Rows[row].Cells["THEMA_CODE"].Value = dr["THEMA_CODE"].ToString().Trim();
                    dgvThema.Rows[row].Cells["THEMA_NAME"].Value = dr["THEMA_NAME"].ToString().Trim();
                    dgvThema.Rows[row].Cells["THEMA_DESC"].Value = dr["THEMA_DESC"].ToString().Trim();
                    dgvThema.Rows[row].Cells["THGROUP_NAME2"].Value = dr["THGROUP_NAME"].ToString().Trim();
                    dgvThema.Rows[row].Cells["TGPSEQ_NO2"].Value = dr["THGROUP_NAME"].ToString().Trim();

                    row = row + 1;
                }

            }
        }
        private bool _firstCall = false;

        private void GetThemaByStockCode()
        {
            RichQuery richQuery = new RichQuery();
            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt4 = new DataTable();
            string strMaxDate = richQuery.p_TDATEQuery(query: "2", tradeDate: "", fromDate: "", toDate: "", bln3tier: false).Tables[0].Rows[0]["MAX_TRADE_DATE"].ToString();

            richQuery = null;
            richQuery = new RichQuery();

            DataTable dt = richQuery.p_ThemaQuery(query: "1", THEMA_CODE: lblThema.Text, TGPSEQ_NO: lblThemaGroup.Text, THEMA_NAME: "", WORK_ID: "", bln3tier: false).Tables[0].Copy();
            int row = 0;

            _clsDataGridViewUtil.RemoveGridViewRow(dgvThemaPerStock);

            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dgvThemaPerStock.Rows.Add();
                    
                    dgvThemaPerStock.Rows[row].Cells["ThemaByStock_THEMA_NAME"].Value = dr["THEMA_NAME"].ToString().Trim();
                    dgvThemaPerStock.Rows[row].Cells["ThemaByStock_STOCK_NAME"].Value = dr["STOCK_NAME"].ToString().Trim();
                    dgvThemaPerStock.Rows[row].Cells["ThemaByStock_STOCK_CODE"].Value = dr["STOCK_CODE"].ToString().Trim();

                    dt4 = kiwoomQuery.p_StockCodeOptInfoQuery(query: "1", stockCode: dr["STOCK_CODE"].ToString().Trim(), stockDate: "", bln3tier: false).Tables[0].Copy();

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

                                    if (dc.ColumnName.ToString() == "STOCK_CODE" || dc.ColumnName.ToString() == "CALL_TIME")
                                    {
                                        continue;
                                    }

                                    if (dc.ColumnName.ToString() == "CALL_DATE")
                                    {
                                        dgvColumn.CellTemplate = cell;
                                        dgvColumn.HeaderText = "기준일자";
                                        dgvColumn.Name = "OPT10001_CALL_DATE";

                                        dgvThemaPerStock.Columns.Add(dgvColumn);

                                        dgvColumn = null;

                                        dgvColumn = new DataGridViewColumn();

                                        dgvColumn.CellTemplate = cell;
                                        dgvColumn.HeaderText = "대금대비시총";
                                        dgvColumn.Name = "대금대비시총";

                                        dgvThemaPerStock.Columns.Add(dgvColumn);

                                        continue;
                                    }


                                    dgvColumn.CellTemplate = cell;
                                    dgvColumn.HeaderText = dc.ColumnName.ToString();
                                    dgvColumn.Name = dc.ColumnName.ToString();

                                    dgvThemaPerStock.Columns.Add(dgvColumn);
                                }

                                _firstCall = true;
                            }                            

                            foreach (DataColumn dc in dt4.Columns)
                            {

                                if (dc.ColumnName.ToString() == "STOCK_CODE" || dc.ColumnName.ToString() == "CALL_TIME")
                                {
                                    continue;
                                }

                                if (dc.ColumnName.ToString() == "CALL_DATE")
                                {
                                    dgvThemaPerStock.Rows[row].Cells["OPT10001_CALL_DATE"].Value = dt4.Rows[0][dc.ColumnName.ToString()].ToString();
                                }
                                else
                                {
                                    dgvThemaPerStock.Rows[row].Cells[columnName: dc.ColumnName.ToString()].Value = dt4.Rows[0][dc.ColumnName.ToString()].ToString();
                                }
                            }

                        }

                    }

                    dt = null;
                    dt = new DataTable();

                    row = row + 1;
                }
                SetDataGridView();
            }            

        }
        #endregion


        #region SetDataGridView
        private void SetDataGridView()
        {
            ClsUtil clsUtil = new ClsUtil();
            int intValue = 0;

            for (int i = 0; i < dgvThemaPerStock.Rows.Count - 1; i++)
            {
                dgvThemaPerStock.Rows[i].Cells["시가총액"].Value = Convert.ToInt32((Convert.ToInt32(dgvThemaPerStock.Rows[i].Cells["상장주식"].Value) * Math.Abs(Convert.ToInt32(dgvThemaPerStock.Rows[i].Cells["현재가"].Value))) / 100000);

                if (dgvThemaPerStock.Rows[i].Cells["전일대비"].Value.ToString().Contains("-") == true)
                {

                    dgvThemaPerStock.Rows[i].Cells["전일대비"].Style.ForeColor = Color.Red;
                }

                intValue = (int)clsUtil.CalPercent((double)Convert.ToInt32(dgvThemaPerStock.Rows[i].Cells["거래대금"].Value), (double)Convert.ToInt32(dgvThemaPerStock.Rows[i].Cells["시가총액"].Value));

                dgvThemaPerStock.Rows[i].Cells["대금대비시총"].Value = intValue.ToString();

                // 시총보다 거래대금이 큰 경우 옐로우
                if (intValue > 30)
                {
                    dgvThemaPerStock.Rows[i].Cells["거래대금"].Style.ForeColor = Color.Green;
                }

                // 시총보다 거래대금이 큰 경우 빨간색
                if (intValue > 100)
                {
                    dgvThemaPerStock.Rows[i].Cells["거래대금"].Style.ForeColor = Color.Red;
                }

                // 영익이 흑자이면, 빨간색
                if (dgvThemaPerStock.Rows[i].Cells["영업이익"].Value.ToString().Contains("-") == false)
                {

                    dgvThemaPerStock.Rows[i].Cells["영업이익"].Style.ForeColor = Color.Red;
                }

                if (dgvThemaPerStock.Rows[i].Cells["당기순이익"].Value.ToString().Contains("-") == false)
                {

                    dgvThemaPerStock.Rows[i].Cells["당기순이익"].Style.ForeColor = Color.Red;
                }


            }

            dgvThemaPerStock.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

        }
        #endregion

        private void OnSelectedStockCode(string stockCode)
        {
            if (stockCode == "")
            {
                return;
            }

            if (lblThema.Text == "")
            {
                return;
            }

            ThemstStoreRecord("A", stockCode);
        }
            
        private void btnThemaGroupStore_Click(object sender, EventArgs e)
        {
            if (lblThemaGroup.Text.Trim() == "")
            {
                ThemaGroupStoreRecord("A");
            }
            else
            {
                ThemaGroupStoreRecord("C");
            }
        }

        private void btnClearThemaGroup_Click(object sender, EventArgs e)
        {
            txtThemaGroup.Text = "";
            lblThemaGroup.Text = "";
        }
        private void btnThemaStore_Click(object sender, EventArgs e)
        {
            if (lblThema.Text.Trim() == "")
            {
                ThemaStoreRecord("A");
            }
            else
            {
                ThemaStoreRecord("C");
            }
        }
        private void btnThemaClear_Click(object sender, EventArgs e)
        {
            txtThema.Text = "";
            lblThema.Text = "";
        }

        private void dgvThemaGroup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvThemaGroup.Rows[e.RowIndex].Cells["TGPSEQ_NO"].Value.ToString().Trim() != "")
            {
                txtThemaGroup.Text = dgvThemaGroup.Rows[e.RowIndex].Cells["THGROUP_NAME"].Value.ToString().Trim();
                lblThemaGroup.Text = dgvThemaGroup.Rows[e.RowIndex].Cells["TGPSEQ_NO"].Value.ToString().Trim();
                txtThemaGroupDesc.Text = dgvThemaGroup.Rows[e.RowIndex].Cells["THGROUP_DESC"].Value.ToString().Trim();

                GetThemaData();
            }
        }

        private void dgvThema_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvThema.Rows[e.RowIndex].Cells["THEMA_CODE"].Value.ToString().Trim() != "")
            {
                txtThema.Text = dgvThema.Rows[e.RowIndex].Cells["THEMA_NAME"].Value.ToString().Trim();
                lblThema.Text = dgvThema.Rows[e.RowIndex].Cells["THEMA_CODE"].Value.ToString().Trim();
                txtThemaDesc.Text = dgvThema.Rows[e.RowIndex].Cells["THEMA_DESC"].Value.ToString().Trim();

                GetThemaByStockCode();
            }
        }

        private void dgvThemaPerStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvThemaPerStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control)
            {
                //if (MessageBox.Show("Delete row?", "Confirmation", MessageBoxButtons.YesNo) !=
                //  DialogResult.Yes)
                //    return;

                ThemstStoreRecord("D", dgvThemaPerStock.Rows[dgvThemaPerStock.CurrentRow.Index].Cells["STOCK_CODE"].ToString());
            }
        }

        private void BtnStoreThemaFromMemo_Click(object sender, EventArgs e)
        {
            if (txtThema.Text == "")
            {
                MessageBox.Show("테마부터 지정하여 주십시요.");
                return;
            }

            if (txtStockMemo.Text.Trim() == "")
            {
                MessageBox.Show("내역이 없습니다.");
                return;
            }

            GetStockCodeFromStockMemo();

        }

        private DataTable _dtStockList;

        private void GetStockCodeFromStockMemo()
        {

            if (_dtStockList == null)
            {
                ClsGetKoaStudioMethod clsGetKoaStudioMethod = new ClsGetKoaStudioMethod();

                _dtStockList = new DataTable();

                _dtStockList = clsGetKoaStudioMethod.GetCodeListByMarketCallBackDataTable("999").Copy();
            }
            
            string stockCode = "";
            for (int i = 0; i < txtStockMemo.Lines.Length - 1; i++)
            {

                if (txtStockMemo.Lines[i].ToString().Trim() == "")
                {
                    continue;
                }
                stockCode = "";

                foreach (DataRow dr in _dtStockList.AsEnumerable().Where(Row => Row.Field<string>("STOCK_NAME") == txtStockMemo.Lines[i].ToString().Trim()).CopyToDataTable().Rows)
                {
                    stockCode = dr["STOCK_CODE"].ToString();
                    break;
                }

                if (stockCode == "")
                {
                    foreach (DataRow dr in _dtStockList.AsEnumerable().Where(Row => Row.Field<string>("STOCK_CODE") == txtStockMemo.Lines[i].ToString().Trim()).CopyToDataTable().Rows)
                    {
                        stockCode = dr["STOCK_CODE"].ToString();
                        break;
                    }
                }

                if (stockCode != "")
                {
                    ThemstStoreRecord(actionGb: "A", stockCode:stockCode);
                }
            }
        
        }

        
    }
}
