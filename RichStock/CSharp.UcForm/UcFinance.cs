﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaikRichStock.Common;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace CSharp.UcForm
{
    public partial class UcFinance : UserControl
    {
        #region "전역변수"

        private DataSet _dsDay;
        private DataAccess _DataAcc = new DataAccess();
        private PaikRichStock.Common.ucMainStockVer2 _ucMainStockVer2;
        private DataSet _stockBaseInfo;
        private string _stockCode = "";
        private DataSet _dsLine;
        private DataSet _dsCredit;
        private string[] _strCredit = {"일자" , "신규" , "상환" , "잔고" , "금액" , "공여율" , "잔고율"};
        private DataSet _dsGong;
        private string[] _strGong = { "일자", "거래량", "비율", "금액", "평균가" };
        private DataSet _dsBuySell;
        private string[] _strBuySell = { "일자", "개인", "외인", "기관", "금융투자","보험"  ,"투신"	 ,   "기타금융",	"은행", "연기금등"	,"사모펀드"	,"국가"	    ,"기타법인"	,"내외국인"	};
        private DataSet _dsNews = new DataSet();
        private DataTable _dt = new DataTable("NEWS");
        private string[] _strNews = { "구분", "제목", "주소", "날짜" };
        string _htmlSource;
        string _url = "http://www.itooza.com/stock/stock_sub.htm";
        string[] _urlCode = { "01", "10", "11", "12", "13", "14", "15" };
        string[] _urlStr = { "아투-기업분석", "아투-종목정보", "아투-시장&경제분석", "아투-핫스톡/테마", "아투-V차트", "아투-미리+즉시분석", "아투-대가의선택" };
        bool _isKw = false;
        #endregion

        #region "생성자"
        public UcFinance()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            if (this.DesignMode == true)
            {
                return;
            }
            
            _stockCode = "";
            for (int i = 0; i < _strNews.Length; i++)
            {
                _dt.Columns.Add(_strNews[i]);
            }
            _dsNews.Tables.Add(_dt);
        }
        #endregion

        #region "프로퍼티"
        public PaikRichStock.Common.ucMainStockVer2 UcStockMain
        {
            get { return _ucMainStockVer2; }
            set
            {
                _ucMainStockVer2 = value;
            }
        }

        public string StockCode
        {
            get { return _stockCode; }
            set
            {
                if (value == null || value == "") return;
                _isKw = true;
                _stockCode = value;
                InitWindow();
            }
        }

        public string StockCodeNotKw
        {
            get { return _stockCode; }
            set
            {
                if (value == null || value == "") return;
                _stockCode = value;
                _isKw = false;
                InitWindow();
                SetCompanyInfo();
            }
        }
        
        public DataSet Prop_StockBaseInfo
        {
            get { return _stockBaseInfo; }
            set
            {
                if (value == null) return;
                if (_stockCode == "") return;
                _stockBaseInfo = value;
                SetCompanyInfo();
                SetTheme();
            }
        }

        public DataSet Prop_DayDs
        {
            set
            {
                if (value == null) return;
                _dsDay = value;
            }
        }

        public DataSet Prop_RealDs
        {
            set
            {
                if (value == null) return;
                if (_stockCode == "") return;
                SetRealDs(value);
            }
        }
        #endregion

        #region "FormSetting"

        private void InitWindow()
        {
            txtCompanyName.Text = "";
            txtAddress.Text = "";
            txtCeo.Text = "";
            txtSector.Text = "";
            txtHomepage.Text = "";
            txtSangJangDate.Text = "";
            txtMainProduct.Text = "";
            txtETC.Text = "";
            txtRemark.Text = "";
            txt주주.Text = "";
            dgImWon.RowCount = 0;
            txt결산월.Text = "";
            txt액면가.Text = "";
            txt자본금.Text = "";
            txt상장주식.Text = "";
            txt신용비율.Text = "";
            txt시가총액.Text = "";
            txt외인소진률.Text = "";
            txtPER.Text = "";
            txtEPS.Text = "";
            txtROE.Text = "";
            txtPBR.Text = "";
            txtEV.Text = "";
            txtBPS.Text = "";
            txt매출액.Text = "";
            txt영업이익.Text = "";
            txt당기순이익.Text = "";
            txt250최고.Text = "";
            txt250최고가대비율.Text = "";
            txt250최고가일.Text = "";
            txt연중최고.Text = "";
            txt250최저.Text = "";
            txt250최저가대비율.Text = "";
            txt250최저가일.Text = "";
            txt연중최저.Text = "";
            txt현재가.Text = "";
            txt시가.Text = "";
            txt고가.Text = "";
            txt저가.Text = "";
            txt거래량.Text = "";
            txt거래대금.Text = "";
            txtRelate.Text = "";
            dgvTheme.RowCount = 1;
            dgSimple.RowCount = 0;
            dgLine.RowCount = 0;
            dgSimple.Rows.Insert(0, 1);
            dgLine.Rows.Insert(0, 1);
            dgHistory.DataSource = null;
            dgUDBuySell.DataSource = null;
            dgUDCredit.DataSource = null;
            dgUDDart.DataSource = null;
            grpNews.Visible = true;
            webBrowser1.Visible = false;
            grp분석.Visible = true;
            grp분석.BringToFront();



            mySqlDbConn conn = new mySqlDbConn();
            ArrayParam arrParam = new ArrayParam();
            if (_stockCode == null || _stockCode == "") return;
            if (_dsDay == null)
            {
                arrParam.Clear();
                arrParam.Add("_QUERY", "6");
                arrParam.Add("_STOCK_CODE", "");
                arrParam.Add("_STOCK_DATE", "");
                _dsDay = conn.GetDataTableSpNotDisCon("p_stock_day_data_query", arrParam);
            }

            txt현재가.TextChanged -= txt현재가_TextChanged;
            txt현재가.TextChanged -= txt현재가_TextChanged;
            
            arrParam.Clear();
            arrParam.Add("_QUERY", "1");
            arrParam.Add("_STOCK_CODE", _stockCode);
            arrParam.Add("_STOCK_DATE", "");
            _dsLine = conn.GetDataTableSpNotDisCon("p_stock_day_data_line_query", arrParam);
            
            InitThema(conn);
            InitBuySell(conn);
            InitCredit(conn);
            InitGong(conn);
            InitUpDown(conn);
            //연관검색어 셋팅 - S
            InitRelate(conn);
            conn.Close();

            //var t = new Task(() => InitThema(conn), cts.Token);
            //매매동향 - S
            //var t1 = t.ContinueWith((task) => InitBuySell(conn), cts.Token);
            //신용 - S
            //var t2 = t1.ContinueWith((task) => InitCredit(conn), cts.Token);
            //공매도 - S
            //var t3 = t2.ContinueWith((task) => InitGong(conn), cts.Token);
            //급등락 - S
            //var t4 = t3.ContinueWith((task) => InitUpDown(conn), cts.Token);
            //t4.ContinueWith((task) => conn.Close(), cts.Token);
            //t.Start();
            
            if (_ucMainStockVer2 != null)
            {
                txt현재가.TextChanged += txt현재가_TextChanged;
            }
        }

        private void InitThema(mySqlDbConn conn)
        {
            ArrayParam arrParam = new ArrayParam();
            arrParam.Clear();
            arrParam.Add("_QUERY", "2");
            arrParam.Add("_STOCK_CODE", "");
            arrParam.Add("_THEME_NAME", "");
            DataSet ds = conn.GetDataTableSpNotDisCon("p_stock_theme_query", arrParam);

            테마.Items.Clear();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                테마.Items.Add(dr["theme_name"].ToString());
            }
        }

        private void InitUpDown(mySqlDbConn conn)
        {
            ArrayParam arrParam = new ArrayParam();
            arrParam.Clear();
            arrParam.Add("_QUERY", "8");
            arrParam.Add("_STOCK_CODE", _stockCode);
            arrParam.Add("_STOCK_DATE", "");

            DataSet ds = conn.GetDataTableSpNotDisCon("p_stock_day_data_query", arrParam);
            DataView dvDay = new DataView(ds.Tables[0]);
            dvDay.RowFilter = "HL_RATE > 10 OR DAY_RATE > 10 OR DAY_RATE < -10 ";
            dvDay.Sort = "STOCK_DATE DESC";
            dgUpDownDay.RowCount = 0;
            foreach (DataRowView dr in dvDay)
            {
                dgUpDownDay.RowCount++;
                dgUpDownDay.Rows[dgUpDownDay.RowCount - 1].Cells[CD일자.Index].Value = dr["STOCK_DATE"].ToString();
                dgUpDownDay.Rows[dgUpDownDay.RowCount - 1].Cells[CD종가.Index].Value = dr["END_PRICE"].ToString();
                dgUpDownDay.Rows[dgUpDownDay.RowCount - 1].Cells[CD시가.Index].Value = dr["S_PRICE"].ToString();
                dgUpDownDay.Rows[dgUpDownDay.RowCount - 1].Cells[CD고가.Index].Value = dr["H_PRICE"].ToString();
                dgUpDownDay.Rows[dgUpDownDay.RowCount - 1].Cells[CD저가.Index].Value = dr["L_PRICE"].ToString();
                dgUpDownDay.Rows[dgUpDownDay.RowCount - 1].Cells[CD대비.Index].Value = String.Format("{0:0}", Convert.ToInt32(dr["END_PRICE"].ToString()) - Convert.ToInt32(dr["PRE_E_PRICE"].ToString()));
                dgUpDownDay.Rows[dgUpDownDay.RowCount - 1].Cells[CD등락율.Index].Value = dr["DAY_RATE"].ToString();
                dgUpDownDay.Rows[dgUpDownDay.RowCount - 1].Cells[CD등락폭.Index].Value = dr["HL_RATE"].ToString();
            }
        }

        async Task DoOpt10013Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_Opt10013EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted)
                {
                    return;
                }

                if (d != null && d.Tables.Count > 0 && d.Tables[0].Rows.Count > 0)
                {
                    _dsCredit = d;
                }
                _ucMainStockVer2.OnReceiveTrData_Opt10013 -= handler; // Unsubscribe
                tcs.SetResult(true);
                // Add your one-time-only code here
            };
            _ucMainStockVer2.OnReceiveTrData_Opt10013 += handler;
            _ucMainStockVer2.Opt10013_OnReceiveTrDataNew(stockCode, stockName, DateTime.Now.ToString("yyyyMMdd"), "1");
            var t1 = tcs.Task;

            if (await Task.WhenAny(t1, Task.Delay(2000)) == t1) { }
            else { }
        }

        async Task DoOpt10014Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_Opt10014EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted)
                {
                    return;
                }

                if (d != null && d.Tables.Count > 0 && d.Tables[0].Rows.Count > 0)
                {
                   _dsGong = d;
                }
                _ucMainStockVer2.OnReceiveTrData_Opt10014 -= handler; // Unsubscribe
                tcs.SetResult(true);
                // Add your one-time-only code here
            };
            _ucMainStockVer2.OnReceiveTrData_Opt10014 += handler;
            _ucMainStockVer2.Opt10014_OnReceiveTrDataNew(stockCode, stockName, "0", DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));
            var t1 = tcs.Task;

            if (await Task.WhenAny(t1, Task.Delay(2000)) == t1) { }
            else { }
        }

        private void InitGong(mySqlDbConn conn)
        {
            var t1 = DoOpt10014Async(_stockCode, _ucMainStockVer2.GetStockInfo(_stockCode));
            t1.Wait();
            //Double sum = 0;
            //ArrayParam arrParam = new ArrayParam();
            //arrParam.Clear();
            //arrParam.Add("_QUERY", "1");
            //arrParam.Add("_STOCK_CODE", _stockCode);
            //arrParam.Add("_STOCK_DATE", "");

            //_dsGong = conn.GetDataTableSpNotDisCon("p_stock_gong_query", arrParam);

            //공매도 - S
            //if (_dsGong.Tables[0].Rows.Count > 4)
            //{
            //    sum = Convert.ToDouble(_dsGong.Tables[0].Compute("SUM(공매도량)", String.Format("일자 >= '{0}'", _dsGong.Tables[0].Rows[4]["일자"].ToString().Trim())));
            //    dgSimple.Rows[0].Cells[C공매도5.Index].Value = sum;
            //}
            //else
            //{
            //    if (_dsGong.Tables[0].Rows.Count > 0)
            //    {
            //        sum = Convert.ToDouble(_dsGong.Tables[0].Compute("SUM(공매도량)", String.Format("일자 >= '{0}'", String.Empty)));
            //        dgSimple.Rows[0].Cells[C공매도5.Index].Value = sum;
            //    }
            //}
            //if (_dsGong.Tables[0].Rows.Count > 9)
            //{
            //    sum = Convert.ToDouble(_dsGong.Tables[0].Compute("SUM(공매도량)", String.Format("일자 >= '{0}'", _dsGong.Tables[0].Rows[9]["일자"].ToString().Trim())));
            //    dgSimple.Rows[0].Cells[C공매도10.Index].Value = sum;
            //}
            //공매도 - E
        }

        private void InitCredit(mySqlDbConn conn)
        {
            var t1 = DoOpt10013Async(_stockCode, _ucMainStockVer2.GetStockInfo(_stockCode));
            t1.Wait();

            //ArrayParam arrParam = new ArrayParam();

            //arrParam.Clear();
            //arrParam.Add("_QUERY", "1");
            //arrParam.Add("_STOCK_CODE", _stockCode);
            //arrParam.Add("_STOCK_DATE", "");

            //_dsCredit = conn.GetDataTableSpNotDisCon("p_stock_credit_query", arrParam);

            ////신용잔고 - S
            //if (_dsCredit.Tables[0].Rows.Count > 4)
            //{
            //    dgSimple.Rows[0].Cells[C신용5.Index].Value = _dsCredit.Tables[0].Rows[4]["credit_jango_rate"].ToString().Trim();
            //}
            //else
            //{
            //    dgSimple.Rows[0].Cells[C신용5.Index].Value = _dsCredit.Tables[0].Rows[_dsCredit.Tables[0].Rows.Count - 1]["credit_jango_rate"].ToString().Trim();
            //}
            //if (_dsCredit.Tables[0].Rows.Count > 9)
            //{
            //    dgSimple.Rows[0].Cells[C신용10.Index].Value = _dsCredit.Tables[0].Rows[9]["credit_jango_rate"].ToString().Trim();
            //}
            ////신용잔고 - E 
        }

        private void InitBuySell(mySqlDbConn conn)
        {
            Double sum = 0;
            ArrayParam arrParam = new ArrayParam();

            arrParam.Clear();
            arrParam.Add("_QUERY", "1");
            arrParam.Add("_STOCK_CODE", _stockCode);
            arrParam.Add("_STOCK_DATE", "");

            _dsBuySell = conn.GetDataTableSpNotDisCon("p_stock_buysell_state_query", arrParam);
            //매매동향 - S
            if (_dsBuySell.Tables[0].Rows.Count > 4)
            {
                sum = Convert.ToDouble(_dsBuySell.Tables[0].Compute("SUM(p_tu)", String.Format("stock_date >= '{0}'", _dsBuySell.Tables[0].Rows[4]["stock_date"].ToString().Trim())));
                dgSimple.Rows[0].Cells[C개인5.Index].Value = sum;
                sum = Convert.ToDouble(_dsBuySell.Tables[0].Compute("SUM(f_tu)", String.Format("stock_date >= '{0}'", _dsBuySell.Tables[0].Rows[4]["stock_date"].ToString().Trim())));
                dgSimple.Rows[0].Cells[C외인5.Index].Value = sum;
                sum = Convert.ToDouble(_dsBuySell.Tables[0].Compute("SUM(k_tu)", String.Format("stock_date >= '{0}'", _dsBuySell.Tables[0].Rows[4]["stock_date"].ToString().Trim())));
                dgSimple.Rows[0].Cells[C기관5.Index].Value = sum;
            }
            else if (_dsBuySell.Tables[0].Rows.Count > 0)
            {
                sum = Convert.ToDouble(_dsBuySell.Tables[0].Compute("SUM(p_tu)", String.Empty));
                dgSimple.Rows[0].Cells[C개인5.Index].Value = sum;
                sum = Convert.ToDouble(_dsBuySell.Tables[0].Compute("SUM(f_tu)", String.Empty));
                dgSimple.Rows[0].Cells[C외인5.Index].Value = sum;
                sum = Convert.ToDouble(_dsBuySell.Tables[0].Compute("SUM(k_tu)", String.Empty));
                dgSimple.Rows[0].Cells[C기관5.Index].Value = sum;
            }
            if (_dsBuySell.Tables[0].Rows.Count > 9)
            {
                sum = Convert.ToDouble(_dsBuySell.Tables[0].Compute("SUM(p_tu)", String.Format("stock_date >= '{0}'", _dsBuySell.Tables[0].Rows[9]["stock_date"].ToString().Trim())));
                dgSimple.Rows[0].Cells[C개인10.Index].Value = sum;
                sum = Convert.ToDouble(_dsBuySell.Tables[0].Compute("SUM(f_tu)", String.Format("stock_date >= '{0}'", _dsBuySell.Tables[0].Rows[9]["stock_date"].ToString().Trim())));
                dgSimple.Rows[0].Cells[C외인10.Index].Value = sum;
                sum = Convert.ToDouble(_dsBuySell.Tables[0].Compute("SUM(k_tu)", String.Format("stock_date >= '{0}'", _dsBuySell.Tables[0].Rows[9]["stock_date"].ToString().Trim())));
                dgSimple.Rows[0].Cells[C기관10.Index].Value = sum;
            }

            if (_dsBuySell.Tables[0].Rows.Count > 0)
                Cls.ChangeColor(dgSimple.Rows[0], "A" ,  0 , C개인5.Index, C외인5.Index, C기관5.Index, C개인10.Index, C외인10.Index, C기관10.Index);
        }

        private void SetTheme()
        {
            int row = 0;
            if (_ucMainStockVer2 == null) return;
            if (_stockBaseInfo == null) return;
            DataSet ds = _DataAcc.p_stock_theme_query("1", _stockCode, "", false, null, null);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (row >= dgvTheme.RowCount - 1) dgvTheme.RowCount++;
                dgvTheme.Rows[row].Cells[CTheme.Index].Value = dr["theme_name"].ToString();
                dgvTheme.Rows[row].Cells[CEtc.Index].Value = dr["etc"].ToString();
                row += 1;
            }
        }

        private void SetCompanyInfo()
        {
            if (_isKw && _ucMainStockVer2 == null) return;
            if (_isKw && _stockBaseInfo == null) return;

            if (!_isKw)
            {
                mySqlDbConn conn = new mySqlDbConn();
                _stockBaseInfo  = conn.GetDataTableCommndText("select * from stock_finance where stock_code = '" + _stockCode + "'");
                conn.Close();
                if (_stockBaseInfo.Tables[0].Rows.Count > 0)
                {
                    int cnt = 0;
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "종목코드";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "종목명";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "결산월";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "액면가";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "자본금";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "상장주식";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "신용비율";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "연중최고";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "연중최저";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "시가총액";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "시가총액비중";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "외인소진률";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "대용가";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "PER";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "EPS";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "ROE";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "PBR";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "EV";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "BPS";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "매출액";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "영업이익";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "당기순이익";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "250최고";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "250최저";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "시가";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "고가";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "저가";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "상한가";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "하한가";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "기준가";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "250최고가일";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "250최고가대비율";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "250최저가일";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "250최저가대비율";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "현재가";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "대비기호";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "전일대비";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "등락율";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "거래량";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "거래량대비";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "액면가단위";
                    _stockBaseInfo.Tables[0].Columns[cnt++].ColumnName = "등록일자";
                }
            }

            DataRow dr = _stockBaseInfo.Tables[0].Rows[0];
            txt결산월.Text = dr["결산월"].ToString().Trim();
            txt액면가.Text = dr["액면가"].ToString().Trim();
            txt자본금.Text = dr["자본금"].ToString().Trim();
            txt상장주식.Text = dr["상장주식"].ToString().Trim();
            txt신용비율.Text = dr["신용비율"].ToString().Trim();
            txt시가총액.Text = dr["시가총액"].ToString().Trim();
            txt외인소진률.Text = dr["외인소진률"].ToString().Trim();
            txtPER.Text = dr["PER"].ToString().Trim();
            txtEPS.Text = dr["EPS"].ToString().Trim();
            txtROE.Text = dr["ROE"].ToString().Trim();
            txtPBR.Text = dr["PBR"].ToString().Trim();
            txtEV.Text = dr["EV"].ToString().Trim();
            txtBPS.Text = dr["BPS"].ToString().Trim();
            txt매출액.Text = dr["매출액"].ToString().Trim();
            txt영업이익.Text = dr["영업이익"].ToString().Trim();
            txt당기순이익.Text = dr["당기순이익"].ToString().Trim();
            txt250최고.Text = dr["250최고"].ToString().Trim();
            txt250최고가대비율.Text = dr["250최고가대비율"].ToString().Trim();
            txt250최고가일.Text = dr["250최고가일"].ToString().Trim();
            txt연중최고.Text = dr["연중최고"].ToString().Trim();
            txt250최저.Text = dr["250최저"].ToString().Trim();
            txt250최저가대비율.Text = dr["250최저가대비율"].ToString().Trim();
            txt250최저가일.Text = dr["250최저가일"].ToString().Trim();
            txt연중최저.Text = dr["연중최저"].ToString().Trim();
            txt시가.Text = dr["시가"].ToString().Trim();
            txt고가.Text = dr["고가"].ToString().Trim();
            txt저가.Text = dr["저가"].ToString().Trim();
            txt거래량.Text = Cls.ValInt64(dr["거래량"]).ToString("#,##0");
            txt현재가.Text = dr["현재가"].ToString().Trim(); //위에 시고저가 셋팅이 되어야 에러가 나지 않는다.

            
            txtCompanyName.Text = dr["종목명"].ToString().Trim();

            DataSet dsCompany = _DataAcc.p_company_info_query("1", _stockCode, false, null, null);
            if (dsCompany == null) return;
            if (dsCompany.Tables[0].Rows.Count < 1) return;
            dr = dsCompany.Tables[0].Rows[0];
            txtSearch.Text = txtCompanyName.Text;
            txtAddress.Text = dr["ADDRESS"].ToString().Trim();
            txtCeo.Text = dr["CEO"].ToString().Trim();
            txtSector.Text = dr["CLASS_GB"].ToString().Trim();
            txtHomepage.Text = dr["HOMEPAGE"].ToString().Trim();
            txtSangJangDate.Text = dr["SANGJANG_DATE"].ToString().Trim();
            txtMainProduct.Text = dr["MAIN_PRODUCT"].ToString().Trim();
            txtETC.Text = dr["ETC"].ToString().Trim();
            txtRemark.Text = dr["REMARK"].ToString().Trim().Replace("-", "\r\n\r\n-").Substring(3);
            txt주주.Text = dr["JUJU"].ToString().Trim();
            int row = 0;
            foreach (DataRow drCEO in dsCompany.Tables[0].Rows)
            {
                if (row >= dgImWon.RowCount - 1) dgImWon.RowCount += 1;
                dgImWon.Rows[row].Cells["이름"].Value = drCEO["NAME"].ToString().Trim();
                dgImWon.Rows[row].Cells["직급"].Value = drCEO["GRADE"].ToString().Trim();
                dgImWon.Rows[row].Cells["경력"].Value = drCEO["CAREER"].ToString().Trim();
                dgImWon.Rows[row].Cells["의결주식수"].Value = drCEO["POWER_STOCK"].ToString().Trim() == "" ? 0 : Decimal.Parse(drCEO["POWER_STOCK"].ToString().Trim());
                row++;
            }
            dgImWon.Sort(dgImWon.Columns["의결주식수"], ListSortDirection.Descending);
            //webBrowser1.Navigate("http://finance.naver.com/item/coinfo.nhn?code=" + _stockCode);

        }

        private void InitRelate(mySqlDbConn conn)
        {
            DataSet ds;
            int cnt = 0;
            string str = "";
            ds = conn.GetDataTableCommndText( String.Format("select * from stock_relate where stock_code = '{0}'", _stockCode));
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                str += dr["RELATE_STR"].ToString();
                if (cnt < ds.Tables[0].Rows.Count - 1) str += " , ";
                cnt++;
            }
            txtRelate.Text = str;
        }

        #endregion

        #region "실시간데이터"
        private void SetRealDs(DataSet ds)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            txt현재가.Text = dr["현재가"].ToString();
            txt고가.Text = dr["고가"].ToString();
            txt저가.Text = dr["저가"].ToString();
            txt거래량.Text = string.Format("{0:##,##0}", Convert.ToInt64(dr["누적거래량"].ToString()));
            txt거래대금.Text = string.Format("{0:##,##0}", Convert.ToInt64(dr["누적거래대금"].ToString()));
        }
        #endregion

        #region "FORM EVENT"

        #region "Button_Click"

        private void btnChart_Click(object sender, EventArgs e)
        {
            //webBrowser1.Navigate("http://finance.naver.com/item/fchart.nhn?code=" + _stockCode);
            Chart.frmChart frmChart = new Chart.frmChart();
            frmChart.MainStock = _ucMainStockVer2;
            frmChart.GetChartData(_stockCode);
            frmChart.Text = txtCompanyName.Text + "(" + _stockCode + ")";
            frmChart.Show();
        }

        private void btnFinance_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://finance.naver.com/item/coinfo.nhn?code=" + _stockCode);
            grpNews.Visible = false;
            grp분석.Visible = false;
            webBrowser1.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string themeName = "";
            string etc = "";
            _DataAcc.p_company_info_Add("UR", _stockCode, txt주주.Text.Trim(), txtSector.Text.Trim(), txtCeo.Text.Trim(), "", "", txtSangJangDate.Text.Trim(), "", "", txtHomepage.Text.Trim(), txtAddress.Text.Trim(), "", "", "", "", txtMainProduct.Text.Trim(), txtETC.Text.Trim(), txtRemark.Text.Trim().Replace("\r\n", ""), false, null, null);
            mySqlDbConn conn = new mySqlDbConn();
            //conn.Open();
            conn.ExecuteNonQuery(String.Format("delete from stock_theme where stock_code = '{0}'", _stockCode), null, CommandType.Text);
            for (int row = 0; row < dgvTheme.Rows.Count - 1; row++)
            {
                if (dgvTheme.Rows[row].Cells[CTheme.Index].Value == null) continue;
                themeName = dgvTheme.Rows[row].Cells[CTheme.Index].Value.ToString().Trim();
                if (dgvTheme.Rows[row].Cells[CEtc.Index].Value != null)
                {
                    etc = dgvTheme.Rows[row].Cells[CEtc.Index].Value.ToString().Trim();
                }
                conn.ExecuteNonQuery(String.Format("insert into stock_theme (stock_code , theme_name , deung_date , etc) values ('{0}','{1}','{2}','{3}')", _stockCode, themeName, DateTime.Now.ToString("yyyyMMdd"), etc), null, CommandType.Text);
            }
            //conn.Close();
        }

        private void btn아이투자_Click(object sender, EventArgs e)
        {
            string url = "http://search.itooza.com/index.htm";
            string post = String.Format("jl={0}&seName={1}", "k", txtCompanyName.Text.Trim());
            byte[] postData = Encoding.Default.GetBytes(post);
            webBrowser1.Navigate(url, null, postData, "Content-Type:application/x-www-form-urlencoded");

            grpNews.Visible = false;
            grp분석.Visible = false;
            webBrowser1.Visible = true;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            DataSet dsNaver;
            DataView dv;
            string temp;
            string[] splitStr;
            int sameCnt = 0;
            pgb.Value = 0;
            dgNews.DataSource = null;
            _dsNews.Tables["NEWS"].Rows.Clear();

            webBrowser1.Visible = false;
            grp분석.Visible = false;
            grpNews.Visible = false;
            pgb.Visible = true;
            pgb.Minimum = 0;
            pgb.Maximum = _urlCode.Length;

            var cts = new CancellationTokenSource(60000);
            var task = DoNavigationAsync();
            task.ContinueWith((t) =>
            {
                pgb.Visible = false;
                webBrowser1.Visible = false;
                grp분석.Visible = false;
                grpNews.Visible = true;

                if (txtSearch.Text != "")
                {
                    bool isNews = false;
                    DataRow drNew;
                    dsNaver = Cls.NaverNews(txtSearch.Text, 100);
                    foreach (DataRow dr in dsNaver.Tables["item"].Rows)
                    {
                        isNews = false;
                        temp = Cls.HtmlToPlainText(dr["title"].ToString());
                        if (temp.Replace(" ", "").IndexOf(txtSearch.Text) == -1) continue;
                        if (temp.Replace(" ", "").IndexOf("코스닥공시") > -1) continue;
                        if (temp.Replace(" ", "").IndexOf("주요공시") > -1) continue;
                        if (dr["originallink"].ToString().IndexOf("sports") > -1) continue;
                        if (_dsNews.Tables["NEWS"].Select("주소='" + dr["originallink"].ToString().Trim() + "'").Length > 0) continue;

                        string naverDate = Convert.ToDateTime(dr["pubDate"].ToString()).ToString("yyyy.MM-dd");
                        foreach (DataRow drNews in _dsNews.Tables["NEWS"].Rows)
                        {
                            //if (drNews["제목"].ToString().Replace(" ", "") == temp.Replace(" ", ""))
                            //{
                            //    isNews = true;
                            //    break;
                            //}
                            sameCnt = 0;
                            splitStr = temp.Replace("\"", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ").Replace(",", " ").ToString().Trim().Split(' ');
                            splitStr = (from str in splitStr
                                        where !string.IsNullOrEmpty(str)
                                        select str).ToArray();

                            foreach (string str in splitStr)
                            {
                                if (drNews["제목"].ToString().IndexOf(str) > -1) sameCnt++;
                            }
                            Double rate = 0.6;
                            if (naverDate == drNews["날짜"].ToString()) rate = 0.4;

                            if (sameCnt > ((Double)splitStr.Length * rate))
                            {
                                isNews = true;
                                break;
                            }
                        }
                        if (isNews == true) continue;

                        drNew = _dsNews.Tables["NEWS"].Rows.Add();
                        drNew[_strNews[0]] = "네이버";
                        drNew[_strNews[1]] = temp;
                        drNew[_strNews[2]] = dr["originallink"];
                        naverDate = naverDate.Substring(2).Replace("-", "/");
                        drNew[_strNews[3]] = naverDate;
                    }
                }

                dv = new DataView(_dsNews.Tables["NEWS"]);
                dv.Sort = _strNews[3] + " DESC";
                dgNews.DataSource = dv;
                dgNews.AutoResizeColumns();
                dgNews.Columns[2].Width = 100;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void btnTodayNews_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            btnNews.PerformClick();
        }

        private void btnDart_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://dart.fss.or.kr/html/search/SearchCompany_M2.html?textCrpNM=" + _stockCode);
            grpNews.Visible = false;
            grp분석.Visible = false;
            webBrowser1.Visible = true;
        }

        private void btn시세_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://finance.naver.com/item/sise.nhn?code=" + _stockCode);
            grpNews.Visible = false;
            grp분석.Visible = false;
            webBrowser1.Visible = true;
        }

        private void btn매동_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://finance.naver.com/item/frgn.nhn?code=" + _stockCode);
            grpNews.Visible = false;
            grp분석.Visible = false;
            webBrowser1.Visible = true;
        }

        private void btn토론_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://finance.naver.com/item/board.nhn?code=" + _stockCode);
            grpNews.Visible = false;
            grp분석.Visible = false;
            webBrowser1.Visible = true;
        }

        private void btn분석_Click(object sender, EventArgs e)
        {
            grp분석.Visible = !grp분석.Visible;
        }
        #endregion

        #region "DataGridView"

        private async void dgSimple_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgHistory.DataSource = null;
            if (e.ColumnIndex == C개인5.Index || e.ColumnIndex == C개인10.Index || e.ColumnIndex == C외인5.Index || e.ColumnIndex == C외인10.Index || e.ColumnIndex == C기관5.Index || e.ColumnIndex == C기관10.Index)
            {
                dgHistory.DataSource = _dsBuySell.Tables[0];

                if (_isKw && Cls.IsDb(_dsDay) == false && Cls.IsJangTime() == true)
                {
                    if (_dsBuySell.Tables[0].Rows[0]["STOCK_DATE"].ToString().Length == 6)
                        _dsBuySell.Tables[0].Rows.RemoveAt(0);

                    await DoOpt100064Async(_stockCode, _ucMainStockVer2.GetStockInfo(_stockCode));
                }
                else if (_isKw && Cls.IsDb(_dsDay) == false && Cls.IsJangTime() == false)
                {
                    await DoOpt100059Async(_stockCode, _ucMainStockVer2.GetStockInfo(_stockCode));
                }
                for (int ix = 0; ix < _strBuySell.Length; ix++)
                {
                    dgHistory.Columns[ix].HeaderText = _strBuySell[ix];
                    dgHistory.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (ix >= 0 && ix < 4) dgHistory.Columns[ix].Width = 65;
                    else dgHistory.Columns[ix].Width = 44;
                }
            }
            else if (e.ColumnIndex == C공매도5.Index || e.ColumnIndex == C공매도10.Index)
            {
                dgHistory.DataSource = _dsGong.Tables[0];
                for (int ix = 0; ix < _strGong.Length; ix++)
                {
                    dgHistory.Columns[ix].HeaderText = _strGong[ix];
                    dgHistory.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgHistory.Columns[ix].Width = 60;
                }
            }
            else if (e.ColumnIndex == C신용5.Index || e.ColumnIndex == C신용10.Index)
            {
                dgHistory.DataSource = _dsCredit.Tables[0];
                for (int ix = 0; ix < _strCredit.Length; ix++)
                {
                    dgHistory.Columns[ix].HeaderText = _strCredit[ix];
                    dgHistory.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgHistory.Columns[ix].Width = 60;
                }
            }

            if (dgSimple.Columns[e.ColumnIndex].HeaderText.IndexOf("개인") > -1 ||
                dgSimple.Columns[e.ColumnIndex].HeaderText.IndexOf("외인") > -1 ||
                dgSimple.Columns[e.ColumnIndex].HeaderText.IndexOf("기관") > -1)
            {
                for (int row = 0; row < dgHistory.RowCount - 1; row++)
                {
                    for (int col = 0; col < dgHistory.ColumnCount; col++)
                    {
                        dgHistory.Rows[row].Cells[col].Style.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                        if (dgHistory.Columns[col].HeaderText == "일자") continue;
                        if (dgHistory.Rows[row].Cells[col].Value.ToString() == "") continue;
                        if (Convert.ToDouble(dgHistory.Rows[row].Cells[col].Value.ToString()) < 0)
                        {
                            dgHistory.Rows[row].Cells[col].Style.ForeColor = System.Drawing.Color.Blue;
                        }
                        else if (Convert.ToDouble(dgHistory.Rows[row].Cells[col].Value.ToString()) > 0)
                        {
                            dgHistory.Rows[row].Cells[col].Style.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            dgHistory.Rows[row].Cells[col].Style.ForeColor = System.Drawing.Color.Empty;
                        }
                    }
                }
            }
            //dgHistory.AutoResizeColumns();
        }

        private void dgLine_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgHistory.DataSource = _dsLine.Tables[0];
            dgHistory.Columns[0].HeaderText = "일자";
            for (int ix = 0; ix < dgHistory.Columns.Count; ix++)
            {
                dgHistory.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgHistory.Columns[ix].Width = 60;
            }
        }

        private void dgUpDownDay_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgUDDart.DataSource = null;
            dgUDCredit.DataSource = null;
            dgUDBuySell.DataSource = null;

            DataSet ds = new DataSet();
            CSharp.Common.Func comCls = new CSharp.Common.Func();
            ds.Tables.Add(comCls.GetDartApi(_stockCode, dgUpDownDay.Rows[e.RowIndex].Cells[CD일자.Index].Value.ToString()));
            ds.Tables.Add(comCls.GetCredit(_stockCode, dgUpDownDay.Rows[e.RowIndex].Cells[CD일자.Index].Value.ToString()));
            ds.Tables.Add(comCls.GetBuySell(_stockCode, dgUpDownDay.Rows[e.RowIndex].Cells[CD일자.Index].Value.ToString()));

            dgUDDart.DataSource = ds.Tables["DART"];
            for (int ix = 0; ix < ds.Tables["DART"].Columns.Count - 1; ix++)
            {

                if (ix == 3)
                {
                    dgUDDart.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgUDDart.Columns[ix].Width = 350;
                }
                else
                {
                    dgUDDart.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgUDDart.Columns[ix].Width = 60;
                }
            }
            dgUDCredit.DataSource = ds.Tables["CREDIT"];
            for (int ix = 0; ix < _strCredit.Length; ix++)
            {
                dgUDCredit.Columns[ix].HeaderText = _strCredit[ix];
                dgUDCredit.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgUDCredit.Columns[ix].Width = 60;
            }
            dgUDBuySell.DataSource = ds.Tables["BUYSELL"];
            for (int ix = 0; ix < _strBuySell.Length; ix++)
            {
                dgUDBuySell.Columns[ix].HeaderText = _strBuySell[ix];
                dgUDBuySell.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (ix == 0) dgUDBuySell.Columns[ix].Width = 58;
                else dgUDBuySell.Columns[ix].Width = 49;
            }
        }

        private void dgUDDart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                System.Diagnostics.Process.Start("http://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + dgUDDart.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim());
            }
        }

        private void dgNews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == 2)
            {
                Process.Start(dgNews.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        #endregion

        #region "TextBox"

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (txtSearch.Text.Trim() == "") return;
            btnNews.PerformClick();
        }

        private void txt현재가_TextChanged(object sender, EventArgs e)
        {
            TextBox txtObj = (TextBox)sender;
            if (txtObj.Text.Trim() == "") return;
            if (_dsLine == null) return;

            if (txtObj.Text.IndexOf("+") < 0 && txtObj.Text.IndexOf("-") < 0) { txtObj.ForeColor = System.Drawing.Color.Empty; }
            Decimal num = Decimal.Parse(txtObj.Text.Trim());
            if (num < 0) txtObj.ForeColor = System.Drawing.Color.Blue;
            else if (num > 0) txtObj.ForeColor = System.Drawing.Color.Red;
            else txtObj.ForeColor = System.Drawing.Color.Empty;

            if (txtObj.Name != txt현재가.Name) return;
            if (txt현재가.Text == "" || txt저가.Text == "" || txt고가.Text == "" || txt시가.Text == "") return;
            int 현재가 = Math.Abs(Convert.ToInt32(txt현재가.Text));
            int 저가 = Math.Abs(Convert.ToInt32(txt저가.Text));
            int 고가 = Math.Abs(Convert.ToInt32(txt고가.Text));
            int 시가 = Math.Abs(Convert.ToInt32(txt시가.Text));
            Double stdPrice = 0;
            Double firstL = 0;
            Double firstH = 0;
            string colName = "";
            string up = "";
            string down = "";

            DataRow dr;
            DataRow drNew;
            DataTable dt = new DataTable("PRICE");
            dt.Columns.Add("PRICE", Type.GetType("System.Double"));

            if (_dsLine.Tables[0].Rows.Count > 0)
            {
                dr = _dsLine.Tables[0].Rows[0];
                dgLine.Rows[0].Cells[CRSI.Index].Value = dr["RSI"];
                for (int i = 0; i < _dsLine.Tables[0].Columns.Count; i++)
                {
                    colName = _dsLine.Tables[0].Columns[i].ToString();
                    if (colName.IndexOf("box") > -1
                        //|| (colName.IndexOf("line") > -1 && Cls.Right(colName ,1) != "3" && Cls.Right(colName,1) != "5" && Cls.Right(colName,2) != "10")
                        )
                    {
                        drNew = dt.Rows.Add();
                        drNew["PRICE"] = Cls.ValInt(dr[colName]);
                    }
                    if (colName.IndexOf("line") > -1)
                    {
                        stdPrice = Cls.Val(dr[colName]);
                        if (저가 < stdPrice && stdPrice < 현재가)
                        {
                            if (up.IndexOf(colName) == -1)
                            {
                                up += colName;
                                up += ",";
                            }
                        }
                        if (시가 > stdPrice && stdPrice > 현재가)
                        {
                            if (down.IndexOf(colName) == -1)
                            {
                                down += colName;
                                down += ",";
                            }
                        }
                    }
                }
            }

            if (dgLine.Rows[0].Cells[C돌파.Index].Value == null || up != dgLine.Rows[0].Cells[C돌파.Index].Value.ToString())
                dgLine.Rows[0].Cells[C돌파.Index].Value = up.Replace("line", "");

            if (dgLine.Rows[0].Cells[C깨짐.Index].Value == null || down != dgLine.Rows[0].Cells[C깨짐.Index].Value.ToString())
                dgLine.Rows[0].Cells[C깨짐.Index].Value = down.Replace("line", "");

            DataView dv = new DataView(dt);
            dv.Sort = "PRICE ASC";
            foreach (DataRowView drv in dv)
            {
                if (현재가 < Convert.ToDouble(drv["PRICE"].ToString()))
                {
                    if (firstH == 0)
                    {
                        //if (Double.Parse(txt현재가.Text) * 1.03 < Convert.ToDouble(drv["PRICE"].ToString()))
                        //{
                            dgLine.Rows[0].Cells[C1차저항.Index].Value = drv["PRICE"].ToString();
                            firstH = Convert.ToDouble(drv["PRICE"].ToString());
                        //}
                    }
                    else
                    {
                        if (firstH * 1.03 < Convert.ToDouble(drv["PRICE"].ToString()))
                        {
                            dgLine.Rows[0].Cells[C2차저항.Index].Value = drv["PRICE"].ToString();
                            break;
                        }
                    }
                }
            }
            dv.Sort = "PRICE DESC";
            foreach (DataRowView drv in dv)
            {
                if (현재가 > Convert.ToDouble(drv["PRICE"].ToString()))
                {
                    if (firstL == 0)
                    {
                        //if (Double.Parse(txt현재가.Text) * 0.97 > Convert.ToDouble(drv["PRICE"].ToString()))
                        //{
                            dgLine.Rows[0].Cells[C1차지지.Index].Value = drv["PRICE"].ToString();
                            firstL = Convert.ToDouble(drv["PRICE"].ToString());
                        //}
                    }
                    else
                    {
                        if (firstL * 0.97 > Convert.ToDouble(drv["PRICE"].ToString()))
                        {
                            dgLine.Rows[0].Cells[C2차지지.Index].Value = drv["PRICE"].ToString();
                            break;
                        }
                    }
                }
            }

            double stochastic = 0;
            for (int row = 0; row < _dsLine.Tables[0].Rows.Count; row++)
            {
                stochastic += Convert.ToDouble(_dsLine.Tables[0].Rows[row]["stochastic"].ToString());
                if (row == 4) break;
            }
            stochastic = stochastic / 5.0;
            dgLine.Rows[0].Cells[C스토캐스틱.Index].Value = stochastic;
            dgLine.AutoResizeColumns();
        }
        #endregion

        #region "WebBrowser"
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/coinfo.nhn?code=") > -1)
            {
                webBrowser1.Document.Window.ScrollTo(0, webBrowser1.Document.Body.ScrollRectangle.Height / 2);
            }
            else if (
                webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/fchart.nhn?code=") > -1 ||
                webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/sise.nhn?code=") > -1 ||
                webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/frgn.nhn?code=") > -1 ||
                webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/board.nhn?code=") > -1
                )
            {
                webBrowser1.Document.Window.ScrollTo(0, webBrowser1.Document.Body.ScrollRectangle.Height / 6);
            }
            else if (webBrowser1.Url.AbsoluteUri.IndexOf("http://search.itooza.com/index.htm") > -1)
            {
                webBrowser1.Document.Window.ScrollTo(0, webBrowser1.Document.Body.ScrollRectangle.Height / 8);
            }

            Stream docStream;
            StreamReader docStreamReader;

            if (e.Url.AbsoluteUri == webBrowser1.Url.AbsoluteUri)
            {
                docStream = webBrowser1.DocumentStream;
                docStreamReader = new StreamReader(docStream, System.Text.Encoding.GetEncoding(webBrowser1.Document.Encoding));
                docStream.Position = 0;
                _htmlSource = docStreamReader.ReadToEnd();
                //_htmlSource =  webBrowser1.DocumentText;
            }
        }
        #endregion

        #endregion

        #region "WEB 분석 Task"

        async Task DoNavigationAsync()
        {
            TaskCompletionSource<bool> tcsNavigation = null;
            TaskCompletionSource<bool> tcsDocument = null;

            webBrowser1.Navigated += (s, e) =>
            {
                if (tcsNavigation.Task.IsCompleted)
                    return;
                tcsNavigation.SetResult(true);
            };

            webBrowser1.DocumentCompleted += (s, e) =>
            {
                if (this.webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                    return;
                if (tcsDocument.Task.IsCompleted)
                    return;
                tcsDocument.SetResult(true);
            };

            Stream docStream;
            StreamReader docStreamReader;
            string url;
            string post;
            byte[] postData;


            if (txtSearch.Text.Trim() != "")
            {
                tcsNavigation = new TaskCompletionSource<bool>();
                tcsDocument = new TaskCompletionSource<bool>();
                url = "http://news.moneta.co.kr/Service/stock/ShellEtc.asp?LinkID=253";
                post = String.Format("kwd={0}", txtSearch.Text.Trim());
                postData = Encoding.Default.GetBytes(post);
                this.webBrowser1.Navigate(url, null, postData, "Content-Type:application/x-www-form-urlencoded");

                //await Task.WhenAny(tcsNavigation.Task , Task.Delay(5000));
                await tcsNavigation.Task;
                Debug.Print("Navigated: {0}", this.webBrowser1.Document.Url);
                // navigation completed, but the document may still be loading

                //await Task.WhenAny(tcsDocument.Task, Task.Delay(5000)); 
                await tcsDocument.Task; 
                docStream = webBrowser1.DocumentStream;
                docStreamReader = new StreamReader(docStream, System.Text.Encoding.GetEncoding(webBrowser1.Document.Encoding));
                docStream.Position = 0;
                _htmlSource = docStreamReader.ReadToEnd();
                Debug.Print("Loaded: {0}", _htmlSource);
                Moneta();
            }

            for (var i = 0; i < _urlCode.Length; i++)
            {
                tcsNavigation = new TaskCompletionSource<bool>();
                tcsDocument = new TaskCompletionSource<bool>();

                url = "";
                post = String.Format("ss={0}&qText={1}&qSearch=qTitle&qSort=", _urlCode[i], txtSearch.Text.Trim());
                postData = Encoding.Default.GetBytes(post);
                if (txtSearch.Text.Trim() == "")
                {
                    url = _url + "?ss=" + _urlCode[i];
                    this.webBrowser1.Navigate(url);
                }
                else
                {
                    this.webBrowser1.Navigate(_url, null, postData, "Content-Type:application/x-www-form-urlencoded");
                }

                //await Task.WhenAny(tcsNavigation.Task, Task.Delay(5000));
                await tcsNavigation.Task;
                Debug.Print("Navigated: {0}", this.webBrowser1.Document.Url);
                // navigation completed, but the document may still be loading

                //await Task.WhenAny(tcsDocument.Task, Task.Delay(5000));
                await tcsDocument.Task;
                docStream = webBrowser1.DocumentStream;
                docStreamReader = new StreamReader(docStream, System.Text.Encoding.GetEncoding(webBrowser1.Document.Encoding));
                docStream.Position = 0;
                _htmlSource = docStreamReader.ReadToEnd();
                Debug.Print("Loaded: {0}", _htmlSource);
                if (_htmlSource.IndexOf("검색된 DATA가 없습니다.") > -1) continue;
                IToozaToday(_urlStr[i]);
                pgb.Value = i + 1;
            }


            // the document has been fully loaded, you can access DOM here
        }

        #endregion

        #region "API 호출 , ReceiveTrData"

        async Task DoOpt100059Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_Opt10059PriceEventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted)
                    return;
                Opt10059Price_OnReceiveTrData(d);
                _ucMainStockVer2.OnReceiveTrData_Opt10059Price -= handler;
                tcs.SetResult(true);
            };
            _ucMainStockVer2.OnReceiveTrData_Opt10059Price += handler;
            _ucMainStockVer2.Opt10059_OnReceiveTrDataPrice(DateTime.Now.ToString("yyyyMMdd"), stockCode, stockName, "1", "0", "1");
            await tcs.Task;
        }

        async Task DoOpt100064Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_Opt10064EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted)
                    return;
                Opt10064_OnReceiveTrData(d);
                _ucMainStockVer2.OnReceiveTrData_Opt10064 -= handler;
                tcs.SetResult(true);
            };

            _ucMainStockVer2.OnReceiveTrData_Opt10064 += handler;
            _ucMainStockVer2.Opt10064_OnReceiveTrData(_stockCode, "", "000", "1", "0");
            await tcs.Task;
            tcs.Task.Dispose();
            tcs = null;
        }

        private void Opt10059Price_OnReceiveTrData(DataSet ds)
        {
            _dsBuySell = null;
            _dsBuySell = ds;
            _dsBuySell.Tables[0].Columns.RemoveAt(5);
            _dsBuySell.Tables[0].Columns.RemoveAt(4);
            _dsBuySell.Tables[0].Columns.RemoveAt(3);
            _dsBuySell.Tables[0].Columns.RemoveAt(2);
            _dsBuySell.Tables[0].Columns.RemoveAt(1);
            DataView dv = new DataView(ds.Tables[0]);
            //if (dv.Count > 10)
            //{
            //    dv.RowFilter = String.Format("일자 >= '{0}'", dv[10]["일자"].ToString());
            //}
            dgHistory.DataSource = dv;
            for (int ix = 0; ix < dgHistory.ColumnCount; ix++)
            {
                dgHistory.Columns[ix].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (ix >= 0 && ix < 4) dgHistory.Columns[ix].Width = 65;
                else dgHistory.Columns[ix].Width = 44;
            }

            for (int row = 0; row < dgHistory.RowCount - 1; row++)
            {
                for (int col = 0; col < dgHistory.ColumnCount; col++)
                {
                    dgHistory.Rows[row].Cells[col].Style.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                    if (dgHistory.Columns[col].HeaderText == "일자") continue;
                    if (dgHistory.Rows[row].Cells[col].Value.ToString() == "") continue;
                    if (Convert.ToDouble(dgHistory.Rows[row].Cells[col].Value.ToString()) < 0)
                    {
                        dgHistory.Rows[row].Cells[col].Style.ForeColor = System.Drawing.Color.Blue;
                    }
                    else if (Convert.ToDouble(dgHistory.Rows[row].Cells[col].Value.ToString()) > 0)
                    {
                        dgHistory.Rows[row].Cells[col].Style.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        dgHistory.Rows[row].Cells[col].Style.ForeColor = System.Drawing.Color.Empty;
                    }
                }
            }
        }

        private void Opt10064_OnReceiveTrData(DataSet ds)
        {
            if (ds == null) return;
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1) return;
            DataView dv = new DataView(ds.Tables[0]);
            dv.Sort = "시간 DESC";
            DataRowView drv = dv[0];
            DataRow dr;
            dr = _dsBuySell.Tables[0].NewRow();
            dr["stock_date"] = drv["시간"];
            dr["f_tu"] = drv["외국인투자자"];
            dr["k_tu"] = drv["기관계"];
            dr["k_2"] = drv["보험"];
            dr["k_3"] = drv["투신"];
            dr["k_5"] = drv["은행"];
            dr["k_6"] = drv["연기금등"];
            dr["k_8"] = drv["국가"];
            dr["k_9"] = drv["기타법인"];

            _dsBuySell.Tables[0].Rows.InsertAt(dr, 0);
        }

        #endregion

        #region "아이투자 , MONETA , NAVER 분석"
        
        private void IToozaToday(string iToozaGb)
        {
            string today = DateTime.Now.ToString("yyyy.MM/dd").Substring(2);
            today = today.Replace("-", "/");
            string source = _htmlSource;
            //source = System.Net.WebUtility.HtmlDecode(source);

            string text = source.Substring(source.IndexOf("<tbody>"), source.IndexOf("</tbody>") - source.IndexOf("<tbody>"));
            text += text + "</tbody>";

            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<';
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            const string tabformatSpace = @"(\r|\n|\t)";

            var tabformatSpaceRegex = new Regex(tabformatSpace, RegexOptions.Multiline);
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            ////Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            ////Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);

            String[] strs = Regex.Split(text, "</tr>");

            for (int i = strs.Length - 1; i >= 0; i--)
            {
                if (txtSearch.Text.Trim() == "")
                {
                    if (
                        strs[i].IndexOf("?no=" + DateTime.Now.ToString("yyyyMMdd")) == -1 &&
                        strs[i].IndexOf("?no=" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd")) == -1
                        )
                    {
                        strs[i] = "";
                    }
                }
                else
                {
                    if (strs[i].IndexOf(txtSearch.Text.Trim()) == -1)
                    {
                        strs[i] = "";
                    }
                }
            }

            strs = (from str in strs
                    where !string.IsNullOrEmpty(str)
                    select str).ToArray();

            string itooza_title;
            string itooza_url;
            string itooza_date;

            DataRow dr;
            for (int i = 0; i < strs.Length; i++)
            {

                itooza_url = strs[i].Substring(strs[i].IndexOf("<a href=") + 8);
                itooza_url = itooza_url.Substring(0, itooza_url.IndexOf(">"));
                itooza_url = itooza_url.Replace("\"", "");

                if (_dsNews.Tables["NEWS"].Select("주소 = '" + itooza_url + "'").Length > 0) continue;

                itooza_title = strs[i].Substring(strs[i].IndexOf("&qSort=\">") + 10);
                itooza_title = itooza_title.Substring(0, itooza_title.IndexOf("</a>"));
                itooza_title = itooza_title.Replace(">", "").Replace("<", "").Replace("\t", "");

                itooza_date = strs[i].Substring(strs[i].IndexOf("date\">") + 6);
                itooza_date = itooza_date.Substring(0, itooza_date.IndexOf("</td>"));
                dr = _dsNews.Tables["NEWS"].NewRow();
                dr[_strNews[0]] = iToozaGb;
                dr[_strNews[1]] = itooza_title;
                dr[_strNews[2]] = itooza_url;
                dr[_strNews[3]] = itooza_date;
                _dsNews.Tables["NEWS"].Rows.Add(dr);
            }
        }

        private void Moneta()
        {
            string url;
            string title;
            string date;
            string[] strs;
            string source = _htmlSource;
            if (source.IndexOf("<ul class=\"nwlist\">") == -1) return;
            string text = source.Substring(source.IndexOf("<ul class=\"nwlist\">"));
            int sameCnt;
            bool isNews;
            string[] splitStr;

            text = text.Substring(0, text.IndexOf("</ul>") + 5);
            strs = Regex.Split(text, "</li>");
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].IndexOf("</ul>") > -1) break;
                string temp = strs[i].Substring(strs[i].IndexOf("<a href=") + 9);
                url = temp.Substring(0, temp.IndexOf("\""));
                temp = strs[i].Substring(strs[i].IndexOf("<strong>") + 8);
                title = temp.Substring(0, temp.IndexOf("</strong>"));
                temp = strs[i].Substring(strs[i].IndexOf("<em>") + 4);
                date = temp.Substring(0, temp.IndexOf("</em>"));
                date = Convert.ToDateTime(date).ToString("yyyy.MM-dd");
                date = date.Substring(2).Replace("-", "/");
                temp = Cls.HtmlToPlainText(title);
                if (temp.IndexOf(txtSearch.Text.Trim()) == -1 || temp.IndexOf("코스닥 공시") > -1) continue;

                isNews = false;
                foreach (DataRow drNews in _dsNews.Tables["NEWS"].Rows)
                {
                    sameCnt = 0;
                    splitStr = temp.Replace("\"", " ").Replace("(", " ").Replace(")", " ").Replace("[", " ").Replace("]", " ").Replace(",", " ").ToString().Trim().Split(' ');
                    splitStr = (from str in splitStr
                                where !string.IsNullOrEmpty(str)
                                select str).ToArray();

                    foreach (string str in splitStr)
                    {
                        if (drNews["제목"].ToString().IndexOf(str) > -1) sameCnt++;
                    }
                    Double rate = 0.6;
                    if (date == drNews["날짜"].ToString()) rate = 0.4;

                    if (sameCnt > ((Double)splitStr.Length * rate))
                    {
                        isNews = true;
                        break;
                    }
                }
                if (isNews == true) continue;
                DataRow dr = _dsNews.Tables["NEWS"].Rows.Add();
                dr[_strNews[0]] = "팍스넷";
                dr[_strNews[1]] = temp;
                dr[_strNews[2]] = url;
                dr[_strNews[3]] = date;
            }
        }

        #endregion

        private void dgvTheme_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == 테마.Index)
            {
                dgvTheme.Rows[e.RowIndex].Cells[CTheme.Index].Value = dgvTheme.Rows[e.RowIndex].Cells[테마.Index].Value;
            }
        }

        private void dgvTheme_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dgvTheme.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dgvTheme.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

    }
}
