using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalysisSt.Common.Class;
using AnalysisSt.DataBaseFunc;
using AnalysisSt.KiwoomVB;

namespace AnalysisSt.Common.Forms
{
    public partial class frmFavManage : Form
    {
        private clsGetRichData _oGetRichData = new clsGetRichData();
        private DataSet _dsAll;
        
        public frmFavManage()
        {
            InitializeComponent();
        }

        public void OpenForms()
        {
            DataSet ds = _oGetRichData.GetAllStock();
            _dsAll = ds.Copy();
            ds.Reset();
            dgvAllStockList.DataSource = _dsAll.Tables[0];
            dgvFCodeInit();
            dgvFsa01Init();
            dgvTodqyTradeInit();
            GetFCode();
            //RegEvent();
        }

        //private void RegEvent()
        //{
        //    KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060MaeSu += new ucMainStockVer2.OnReceiveTrData_Opt10060MaeSuEventHandler(OnReceiveTrData_Opt10060MaeSu);
        //    KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060Maedo += new ucMainStockVer2.OnReceiveTrData_Opt10060MaedoEventHandler(OnReceiveTrData_Opt10060Maedo);
        //    KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060PriceMaeSu += new ucMainStockVer2.OnReceiveTrData_Opt10060PriceMaeSuEventHandler(OnReceiveTrData_Opt10060PriceMaeSu);
        //    KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060PriceMaedo += new ucMainStockVer2.OnReceiveTrData_Opt10060PriceMaedoEventHandler(OnReceiveTrData_Opt10060PriceMaedo);
        //}

        private void frmFavManage_Load(object sender, EventArgs e)
        {
            OpenForms();
        }

        #region GetDataGridView
        private void GetFCode()
        {
            DataSet ds = _oGetRichData.GetFcodeData();
            int i = 0;

            dgvFCode.Rows.Clear();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
               dgvFCode.Rows.Add();
               dgvFCode.Rows[i].Cells["SGROUP_CODE"].Value = dr["SGROUP_CODE"].ToString();
               dgvFCode.Rows[i].Cells["SGROUP_NAME"].Value = dr["SGROUP_NAME"].ToString();
               dgvFCode.Rows[i].Cells["SGROUP_INFO"].Value = dr["SGROUP_INFO"].ToString();

               i = i + 1;
            }

            ds.Reset();
            
        }

        #endregion

        #region DataGridViewInit
        private void dgvFsa01Init()
        { 
            dgvFsa01.ColumnCount = 6;
            dgvFsa01.Columns[0].Name = "STOCK_CODE";
            dgvFsa01.Columns[1].Name = "STOCK_NAME";
            dgvFsa01.Columns[2].Name = "YBJONG_NAME";
            dgvFsa01.Columns[3].Name = "SEQ_NO";
            dgvFsa01.Columns[4].Name = "SGROUP_CODE";
            dgvFsa01.Columns[5].Name = "SGROUP_NAME";
        }

        private void dgvFCodeInit()
        {
            dgvFCode.ColumnCount = 3;
            dgvFCode.Columns[0].Name = "SGROUP_CODE";
            dgvFCode.Columns[1].Name = "SGROUP_NAME";
            dgvFCode.Columns[2].Name = "SGROUP_INFO";
        }

        private void dgvTodqyTradeInit()
        {
            dgvTodayTrade.ColumnCount = 23;
            dgvTodayTrade.Columns[0].Name = "종목";
            dgvTodayTrade.Columns[1].Name = "종목명";
            dgvTodayTrade.Columns[2].Name = "일자";
            dgvTodayTrade.Columns[3].Name = "거래량";
            dgvTodayTrade.Columns[4].Name = "거래대금";
            dgvTodayTrade.Columns[5].Name = "구분";
            dgvTodayTrade.Columns[6].Name = "시가";
            dgvTodayTrade.Columns[7].Name = "고가";
            dgvTodayTrade.Columns[8].Name = "저가";
            dgvTodayTrade.Columns[9].Name = "개인";
            dgvTodayTrade.Columns[10].Name = "외국인";
            dgvTodayTrade.Columns[11].Name = "기관";
            dgvTodayTrade.Columns[12].Name = "금융";
            dgvTodayTrade.Columns[13].Name = "보험";
            dgvTodayTrade.Columns[14].Name = "투신";
            dgvTodayTrade.Columns[15].Name = "기타금융";
            dgvTodayTrade.Columns[16].Name = "은행";
            dgvTodayTrade.Columns[17].Name = "연기금";
            dgvTodayTrade.Columns[18].Name = "사모펀드";
            dgvTodayTrade.Columns[19].Name = "국가";
            dgvTodayTrade.Columns[20].Name = "기타법인";
            dgvTodayTrade.Columns[21].Name = "기타외인";
            dgvTodayTrade.Columns[22].Name = "기관합";

            for (int i = 6; i < dgvTodayTrade.Columns.Count - 1; i++)
			{
			  dgvTodayTrade.Columns[i].ValueType = System.Type.GetType("System.Int64");
			}
                       
        }

        #endregion

        #region Func
        private void GetFsa01Data(String sGroupCode)
        {
            DataSet ds = _oGetRichData.GetFsa01Data(sGroupCode);
            int i = 0;

            dgvFsa01.Rows.Clear();

            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                //dgvFsa01.RowCount = ds.Tables[0].Rows.Count;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dgvFsa01.Rows.Add();
                    dgvFsa01.Rows[i].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"];
                    dgvFsa01.Rows[i].Cells["STOCK_NAME"].Value = dr["STOCK_NAME"];
                    dgvFsa01.Rows[i].Cells["YBJONG_NAME"].Value = dr["YBJONG_NAME"];
                    dgvFsa01.Rows[i].Cells["SEQ_NO"].Value = dr["SEQ_NO"];
                    dgvFsa01.Rows[i].Cells["SGROUP_CODE"].Value = dr["SGROUP_CODE"];
                    dgvFsa01.Rows[i].Cells["SGROUP_NAME"].Value = dr["SGROUP_NAME"];

                    i = i + 1;
                }
                dgvFsa01.SuspendLayout();
                ds.Reset();
            }
            else 
            { ds.Reset(); }
        }

        public int ReturnFsa01SeqNo(String sGroupCode, String stockCode)
        {
            DataBaseFunc.RichQuery oQuery = new DataBaseFunc.RichQuery();
            DataSet ds = oQuery.p_Fsa01Query("2", sGroupCode, stockCode, "", false);

            if (ds.Tables[0].Rows.Count < 1 || ds == null)
            { 
                ds.Reset();
                return 9999;
            }
            else
            {
                ds.Reset();
                ds = oQuery.p_Fsa01Query("1", sGroupCode, "", "", false);
                int i = (int)ds.Tables[0].Rows[0]["SEQ_NO"];
                ds.Reset();

                return i;
                                
            }


        }
        #endregion

        #region ControlEvent
        private void btnChgSGroupName_Click(object sender, EventArgs e)
        {
            
            DataBaseFunc.ArrayParam arrParam = new DataBaseFunc.ArrayParam();
            DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "RICHDB");
            String sysDate = CDateTime.FormatDate(DateTime.Now.Date.ToString());
            try
            {
                arrParam.Clear();
                arrParam.Add("@ACTION_GB", "A");
                arrParam.Add("@SGROUP_CODE", lblSGroupCode.Text.Trim());
                arrParam.Add("@SGROUP_NAME", txtSGroupName.Text.Trim());
                arrParam.Add("@SGROUP_INFO", "");
                arrParam.Add("@CREATE_DATE", sysDate);
                arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                oSql.ExecuteNonQuery("p_FCodeAdd", CommandType.StoredProcedure, arrParam);

                GetFCode();
                
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                throw;
            }
                        
        }

        private void btnNewSGroup_Click(object sender, EventArgs e)
        {
           DataSet ds = _oGetRichData.GetNewCode_FcodeData();
           lblSGroupCode.Text =ds.Tables[0].Rows[0]["NEW_SGOUP_CODE"].ToString().Trim();
           ds.Reset();
           txtSGroupName.Text = "";
        }

        private void btnChgSeqNo_Click(object sender, EventArgs e)
        {
            DataBaseFunc.ArrayParam arrParam = new DataBaseFunc.ArrayParam();
            DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "RICHDB");
            String sysDate = CDateTime.FormatDate(DateTime.Now.Date.ToString());


        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dgvAllStockList.DataSource;
            bs.Filter = string.Format("CONVERT(" + dgvAllStockList.Columns["STOCK_NAME"].DataPropertyName +
                                      ", System.String) like '%" + txtSearch.Text.Replace("'", "''") + "%'");
        }
        #endregion

        #region DataGridViewEvent
        private void dgvAllStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim() == "")
            {
                return;
            }

              if (MessageBox.Show(dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_NAME"].Value.ToString().Trim() + 
                                  "을 입력하시겠습니까?", "관심종목 입력", MessageBoxButtons.YesNo) !=  DialogResult.Yes)
            {
                return;
            }

            if (lblSGroupCode.Text.Trim() == "")
            {
                MessageBox.Show("먼저 그룹코드를 선택해주세요.");
            }
            else
            {
                int i = 0;
                i = ReturnFsa01SeqNo(lblSGroupCode.Text, dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());

                if (i == 999)
                { MessageBox.Show("이미 해당 그룹에 존재하는 종목입니다."); }
                else
                {
                    DataBaseFunc.ArrayParam arrParam = new DataBaseFunc.ArrayParam();
                    DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "RICHDB");

                    try
                    {
                        arrParam.Add("@ACTION_GB", "A");
                        arrParam.Add("@SGROUP_CODE", lblSGroupCode.Text.Trim());
                        arrParam.Add("@STOCK_CODE", dgvAllStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());
                        arrParam.Add("@SEQ_NO", i);
                        arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                        oSql.ExecuteNonQuery("p_Fsa01Add", CommandType.StoredProcedure, arrParam);

                        GetFsa01Data(lblSGroupCode.Text);

                    }
                    catch (Exception t)
                    {
                        MessageBox.Show(t.ToString());
                        throw;
                    }   
                }
            }
        }

        private void dgvFCode_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].ToString().Trim() == "")
            {
                return;
            }

            lblSGroupCode.Text = dgvFCode.Rows[e.RowIndex].Cells["SGROUP_CODE"].Value.ToString().Trim();
            txtSGroupName.Text = dgvFCode.Rows[e.RowIndex].Cells["SGROUP_NAME"].Value.ToString().Trim();
            GetFsa01Data(lblSGroupCode.Text);
        }        
        #endregion

        #region TabPage2
        private void GetTodayTradeInfo(String sGroupCode)
        {
            tbCon.SelectedIndex = 1;
            String sysDate = CDateTime.FormatDate(DateTime.Now.Date.ToString());
            DataBaseFunc.KiwoomQuery oKiwoomQuery = new DataBaseFunc.KiwoomQuery();
            DataSet ds = oKiwoomQuery.p_TodayTradeInfoQueryQuery("1", sGroupCode, "",  CDateTime.FormatDate(dtpTradeDate.Text), false);

            dgvTodayTrade.Rows.Clear();

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                ds.Reset();
            }
            else
            {
                
                int i = 0;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dgvTodayTrade.Rows.Add();
                    dgvTodayTrade.Rows[i].Cells["종목"].Value = dr["종목"];
                    dgvTodayTrade.Rows[i].Cells["종목명"].Value = dr["종목명"];
                    dgvTodayTrade.Rows[i].Cells["일자"].Value = dr["일자"];
                    dgvTodayTrade.Rows[i].Cells["거래량"].Value = dr["거래량"];
                    dgvTodayTrade.Rows[i].Cells["거래대금"].Value = dr["거래대금"];
                    dgvTodayTrade.Rows[i].Cells["구분"].Value = dr["구분"];
                    dgvTodayTrade.Rows[i].Cells["시가"].Value = dr["시가"];
                    dgvTodayTrade.Rows[i].Cells["고가"].Value = dr["고가"];
                    dgvTodayTrade.Rows[i].Cells["저가"].Value = dr["저가"];
                    dgvTodayTrade.Rows[i].Cells["개인"].Value = dr["개인"];
                    dgvTodayTrade.Rows[i].Cells["외국인"].Value = dr["외국인"];
                    dgvTodayTrade.Rows[i].Cells["기관"].Value = dr["기관"];
                    dgvTodayTrade.Rows[i].Cells["금융"].Value = dr["금융"];
                    dgvTodayTrade.Rows[i].Cells["보험"].Value = dr["보험"];
                    dgvTodayTrade.Rows[i].Cells["투신"].Value = dr["투신"];
                    dgvTodayTrade.Rows[i].Cells["기타금융"].Value = dr["기타금융"];
                    dgvTodayTrade.Rows[i].Cells["은행"].Value = dr["은행"];
                    dgvTodayTrade.Rows[i].Cells["연기금"].Value = dr["연기금"];
                    dgvTodayTrade.Rows[i].Cells["사모펀드"].Value = dr["사모펀드"];
                    dgvTodayTrade.Rows[i].Cells["국가"].Value = dr["국가"];
                    dgvTodayTrade.Rows[i].Cells["기타법인"].Value = dr["기타법인"];
                    dgvTodayTrade.Rows[i].Cells["기타외인"].Value = dr["기타외인"];
                    dgvTodayTrade.Rows[i].Cells["기관합"].Value = dr["기관합"];

                    for (int j = 8; j < dgvTodayTrade.ColumnCount - 1; j++)
                    {
                        if (Convert.ToInt64(dgvTodayTrade.Rows[i].Cells[j].Value) > 0)
                        {
                            //  System.Drawing.SystemColors.
                            dgvTodayTrade.Rows[i].Cells[j].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            dgvTodayTrade.Rows[i].Cells[j].Style.ForeColor = Color.Blue;
                        }
                        
                    }

                    i = i + 1;
                }
                                
                for (int k = 0; k < dgvTodayTrade.Columns.Count; k++)
                {
                    int colw = dgvTodayTrade.Columns[k].Width;
                    dgvTodayTrade.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgvTodayTrade.Columns[k].Width = colw;
                }

            }
        }

        private IEnumerable<DataRow> GetNumber(String sGroupCode)
        {
            DataSet ds = _oGetRichData.GetFsa01Data(sGroupCode);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                yield return dr;
            }
        }

        
        #endregion
        private DataSet _dsSGroupCode;
        private int _iSGroupcode;
        private void btnViewToday_Click(object sender, EventArgs e)
        {

            if (Check10059(lblSGroupCode.Text) == true)
            {
                if (Check10060(lblSGroupCode.Text) == false)
                {
                    if (MessageBox.Show("해당일의 10060자료가 없습니다.자료생성하시겠습니까?", "자료생성", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        GetOpt10060();
                    }
                }
                else
                {
                    GetTodayTradeInfo(lblSGroupCode.Text);
                }
            }
            else 
            {
                MessageBox.Show("해당일의 10059자료가 없습니다.");
            }
        }

        private Boolean Check10059(String sGroupCode)
        {
            DataBaseFunc.KiwoomQuery oKiwoomQuery = new DataBaseFunc.KiwoomQuery();
            DataSet ds = oKiwoomQuery.p_TodayTraderInfoExists("1", sGroupCode, "", CDateTime.FormatDate(dtpTradeDate.Text), false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {return true;
            }
            else { return false; }
            
        }

        private Boolean Check10060(String sGroupCode)
        {
            DataBaseFunc.KiwoomQuery oKiwoomQuery = new DataBaseFunc.KiwoomQuery();
            DataSet ds = oKiwoomQuery.p_TodayTraderInfoExists("2", sGroupCode, "", CDateTime.FormatDate(dtpTradeDate.Text), false);

            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                return true;
            }
            else {

                if (_dsSGroupCode != null)
                {
                    _dsSGroupCode.Reset();
                }
                _iSGroupcode = 0;
                _dsSGroupCode = ds.Copy();

                return false; }
        }
        
        private DataRow SGroupCodeData()
        {
            DataRow dr;
            
            if (_iSGroupcode > _dsSGroupCode.Tables[0].Rows.Count - 1)
            {
                return null;
            }

            dr = _dsSGroupCode.Tables[0].Rows[_iSGroupcode];

            _iSGroupcode = _iSGroupcode + 1;

            return dr;
        }

        private async Task DoGetOpt10060(String stockCode, String stockName, String startDate)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            TaskCompletionSource<bool> tcs2 = null;
            tcs2 = new TaskCompletionSource<bool>();
            TaskCompletionSource<bool> tcs3 = null;
            tcs3 = new TaskCompletionSource<bool>();
            TaskCompletionSource<bool> tcs4 = null;
            tcs4 = new TaskCompletionSource<bool>();

            KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060MaeSu += (d) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                    return;
                OnReceiveTrData_Opt10060MaeSu(d);
                tcs.SetResult(true);
                //System.Threading.Thread.Sleep(1000);
            };
            KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060Maedo += (d) =>
            {
                if (tcs2 == null || tcs2.Task.IsCompleted)
                    return;
                OnReceiveTrData_Opt10060Maedo(d);
                tcs2.SetResult(true);
                //System.Threading.Thread.Sleep(1000);
            };
            KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060PriceMaeSu += (d) =>
            {
                if (tcs3 == null || tcs3.Task.IsCompleted)
                    return;
                OnReceiveTrData_Opt10060PriceMaeSu(d);
                tcs3.SetResult(true);
                //System.Threading.Thread.Sleep(1000);
            };
            KiwoomVB.ModStatus._ModMainStock.OnReceiveTrData_Opt10060PriceMaedo += (d) =>
            {
                if (tcs4 == null || tcs4.Task.IsCompleted)
                    return;
                OnReceiveTrData_Opt10060PriceMaedo(d);
                tcs4.SetResult(true);
                //System.Threading.Thread.Sleep(1000);
            };
            GetOpt10060QtyMaeSu(stockCode, stockName, startDate);
            await tcs.Task;
            tcs.Task.Dispose();
            tcs = null;
            //System.Threading.Thread.Sleep(5000);
            GetOpt10060QtyMaeDo(stockCode, stockName, startDate);
            await tcs2.Task;
            tcs2.Task.Dispose();
            tcs2 = null;
            //System.Threading.Thread.Sleep(5000);
            GetOpt10060PriceMaeSu(stockCode, stockName, startDate);
            await tcs3.Task;
            tcs3.Task.Dispose();
            tcs3 = null;
            //System.Threading.Thread.Sleep(5000);
            GetOpt10060PriceMaeDo(stockCode, stockName, startDate);
            await tcs4.Task;         
            tcs4.Task.Dispose();         
            tcs4 = null;
            System.Threading.Thread.Sleep(5000);
            GetOpt10060();
        }

        private async void GetOpt10060()
        {
            DataRow dr = SGroupCodeData();
            if (dr == null)
            {
                MessageBox.Show("해당일의 거래내역을 모두 가져왔습니다.");
                GetTodayTradeInfo(lblSGroupCode.Text);
                return;
            }
            String stockCode = dr["STOCK_CODE"].ToString().Trim();
            String stockName = dr["STOCK_NAME"].ToString().Trim();
            String startDate = CDateTime.FormatDate(dtpTradeDate.Text);
            await DoGetOpt10060(stockCode, stockName, startDate);
        }
        String _stockCode_Opt10060MaeSu;
        String _stockCode_Opt10060MaeDo;
        String _stockCode_Opt10060PriceMaeSu;
        String _stockCode_Opt10060PriceMaeDo;

        private void GetOpt10060QtyMaeSu(String stockCode, String stockName, String startDate)
        {
            _stockCode_Opt10060MaeSu = stockCode;
            KiwoomVB.ModStatus._ModMainStock.Opt10060MaeSu_OnReceiveTrData(startDate, stockCode, stockName, "", "", "");
        }
        private void GetOpt10060QtyMaeDo(String stockCode, String stockName, String startDate)
        {
            _stockCode_Opt10060MaeDo = stockCode;
            KiwoomVB.ModStatus._ModMainStock.Opt10060MaeDo_OnReceiveTrData(startDate, stockCode, stockName, "", "", "");
        }
        private void GetOpt10060PriceMaeSu(String stockCode, String stockName, String startDate)
        {
            _stockCode_Opt10060PriceMaeSu = stockCode;
            KiwoomVB.ModStatus._ModMainStock.Opt10060PriceMaeSu_OnReceiveTrData(startDate, stockCode, stockName, "", "", "");
        }
        private void GetOpt10060PriceMaeDo(String stockCode, String stockName, String startDate)
        {
            _stockCode_Opt10060PriceMaeDo = stockCode;
            KiwoomVB.ModStatus._ModMainStock.Opt10060PriceMaedo_OnReceiveTrData(startDate, stockCode, stockName, "", "", "");
        }

        private void OnReceiveTrData_Opt10060MaeSu(DataSet ds)
        {
            if (ds == null) { return; }

            try
            {
                ArrayParam arrParam = new ArrayParam();
                DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _stockCode_Opt10060MaeSu);
                    arrParam.Add("@STOCK_DATE", dr["일자"]);
                    arrParam.Add("@MAEME_GB", "1");
                    arrParam.Add("@DATE_SEQNO", 0);
                    arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                    arrParam.Add("@GAIN_QTY", dr["개인투자자"]);
                    arrParam.Add("@FORE_QTY", dr["외국인투자자"]);
                    arrParam.Add("@GIGAN_QTY", dr["기관계"]);
                    arrParam.Add("@GUMY_QTY", dr["금융투자"]);
                    arrParam.Add("@BOHUM_QTY", dr["보험"]);
                    arrParam.Add("@TOSIN_QTY", dr["투신"]);
                    arrParam.Add("@GITA_QTY", dr["기타금융"]);
                    arrParam.Add("@BANK_QTY", dr["은행"]);
                    arrParam.Add("@YEONGI_QTY", dr["연기금등"]);
                    arrParam.Add("@SAMO_QTY", dr["사모펀드"]);
                    arrParam.Add("@NATION_QTY", dr["국가"]);
                    arrParam.Add("@BUBIN_QTY", dr["기타법인"]);
                    arrParam.Add("@IOFORE_QTY", dr["내외국인"]);
                    arrParam.Add("@GIGAN_SUM_QTY", (int)dr["금융투자"] + (int)dr["보험"] + (int)dr["투신"] +
                                                   (int)dr["기타금융"] + (int)dr["은행"] + (int)dr["연기금등"] + (int)dr["사모펀드"] + (int)dr["국가"]);
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060QtyAdd", CommandType.StoredProcedure, arrParam);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }

        }
        private void OnReceiveTrData_Opt10060Maedo(DataSet ds)
        {
            if (ds == null) { return; }

            try
            {
                ArrayParam arrParam = new ArrayParam();
                DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _stockCode_Opt10060MaeDo);
                    arrParam.Add("@STOCK_DATE", dr["일자"]);
                    arrParam.Add("@MAEME_GB", "2");
                    arrParam.Add("@DATE_SEQNO", 0);
                    arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                    arrParam.Add("@GAIN_QTY", dr["개인투자자"]);
                    arrParam.Add("@FORE_QTY", dr["외국인투자자"]);
                    arrParam.Add("@GIGAN_QTY", dr["기관계"]);
                    arrParam.Add("@GUMY_QTY", dr["금융투자"]);
                    arrParam.Add("@BOHUM_QTY", dr["보험"]);
                    arrParam.Add("@TOSIN_QTY", dr["투신"]);
                    arrParam.Add("@GITA_QTY", dr["기타금융"]);
                    arrParam.Add("@BANK_QTY", dr["은행"]);
                    arrParam.Add("@YEONGI_QTY", dr["연기금등"]);
                    arrParam.Add("@SAMO_QTY", dr["사모펀드"]);
                    arrParam.Add("@NATION_QTY", dr["국가"]);
                    arrParam.Add("@BUBIN_QTY", dr["기타법인"]);
                    arrParam.Add("@IOFORE_QTY", dr["내외국인"]);
                    arrParam.Add("@GIGAN_SUM_QTY", (int)dr["금융투자"] + (int)dr["보험"] + (int)dr["투신"] +
                                                   (int)dr["기타금융"] + (int)dr["은행"] + (int)dr["연기금등"] + (int)dr["사모펀드"] + (int)dr["국가"]);
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060QtyAdd", CommandType.StoredProcedure, arrParam);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        private void OnReceiveTrData_Opt10060PriceMaeSu(DataSet ds)
        {
            if (ds == null) { return; }

            try
            {
                ArrayParam arrParam = new ArrayParam();
                DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _stockCode_Opt10060PriceMaeSu);
                    arrParam.Add("@STOCK_DATE", dr["일자"]);
                    arrParam.Add("@MAEME_GB", "1");
                    arrParam.Add("@DATE_SEQNO", 0);
                    arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                    arrParam.Add("@GAIN_PRICE", dr["개인투자자"]);
                    arrParam.Add("@FORE_PRICE", dr["외국인투자자"]);
                    arrParam.Add("@GIGAN_PRICE", dr["기관계"]);
                    arrParam.Add("@GUMY_PRICE", dr["금융투자"]);
                    arrParam.Add("@BOHUM_PRICE", dr["보험"]);
                    arrParam.Add("@TOSIN_PRICE", dr["투신"]);
                    arrParam.Add("@GITA_PRICE", dr["기타금융"]);
                    arrParam.Add("@BANK_PRICE", dr["은행"]);
                    arrParam.Add("@YEONGI_PRICE", dr["연기금등"]);
                    arrParam.Add("@SAMO_PRICE", dr["사모펀드"]);
                    arrParam.Add("@NATION_PRICE", dr["국가"]);
                    arrParam.Add("@BUBIN_PRICE", dr["기타법인"]);
                    arrParam.Add("@IOFORE_PRICE", dr["내외국인"]);
                    arrParam.Add("@GIGAN_SUM_PRICE", (int)dr["금융투자"] + (int)dr["보험"] + (int)dr["투신"] +
                                                   (int)dr["기타금융"] + (int)dr["은행"] + (int)dr["연기금등"] + (int)dr["사모펀드"] + (int)dr["국가"]);
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060PriceAdd", CommandType.StoredProcedure, arrParam);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }
        private void OnReceiveTrData_Opt10060PriceMaedo(DataSet ds)
        {
            if (ds == null) { return; }

            try
            {
                ArrayParam arrParam = new ArrayParam();
                DataBaseFunc.Sql oSql = new DataBaseFunc.Sql("EDPB2F011\\VADIS", "KIWOOMDB");

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "A");
                    arrParam.Add("@STOCK_CODE", _stockCode_Opt10060PriceMaeDo);
                    arrParam.Add("@STOCK_DATE", dr["일자"]);
                    arrParam.Add("@MAEME_GB", "2");
                    arrParam.Add("@DATE_SEQNO", 0);
                    arrParam.Add("@NUJUK_TRDAEGUM", dr["누적거래대금"]);
                    arrParam.Add("@GAIN_PRICE", dr["개인투자자"]);
                    arrParam.Add("@FORE_PRICE", dr["외국인투자자"]);
                    arrParam.Add("@GIGAN_PRICE", dr["기관계"]);
                    arrParam.Add("@GUMY_PRICE", dr["금융투자"]);
                    arrParam.Add("@BOHUM_PRICE", dr["보험"]);
                    arrParam.Add("@TOSIN_PRICE", dr["투신"]);
                    arrParam.Add("@GITA_PRICE", dr["기타금융"]);
                    arrParam.Add("@BANK_PRICE", dr["은행"]);
                    arrParam.Add("@YEONGI_PRICE", dr["연기금등"]);
                    arrParam.Add("@SAMO_PRICE", dr["사모펀드"]);
                    arrParam.Add("@NATION_PRICE", dr["국가"]);
                    arrParam.Add("@BUBIN_PRICE", dr["기타법인"]);
                    arrParam.Add("@IOFORE_PRICE", dr["내외국인"]);
                    arrParam.Add("@GIGAN_SUM_PRICE", (int)dr["금융투자"] + (int)dr["보험"] + (int)dr["투신"] +
                                                   (int)dr["기타금융"] + (int)dr["은행"] + (int)dr["연기금등"] + (int)dr["사모펀드"] + (int)dr["국가"]);
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_Opt10060PriceAdd", CommandType.StoredProcedure, arrParam);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
        }

        private void btnViewTodayVer2_Click(object sender, EventArgs e)
        {
           
        }

    }
}
