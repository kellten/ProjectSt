﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic;
using System.Globalization;
using PaikRichStock.Common;
using System.Text.RegularExpressions;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.Internals;
using System.Net;
using System.IO;
using PaikRichStock.UcForm;
using System.Collections;
using System.Threading;
namespace StockDayDataSaver
{
    public partial class frmSaverNew : Form
    {
        #region "전역변수"
        private int _rowCnt = 0;
        private bool _IsReady = false;
        private ChromiumWebBrowser _browser;
        private string _htmlSource = "";
        private DataTable _dt = new DataTable("DAYINFO");
        private DataTable _dtFinance = new DataTable("FINANCE");
        private string _FILTERSTR = "STOCK_CODE <> '' AND STOCK_NAME NOT LIKE '%ETN%' AND STOCK_NAME NOT LIKE '%선물%'  AND STOCK_NAME NOT LIKE '트러스%'  AND STOCK_NAME NOT LIKE 'KBSTAR%' AND STOCK_NAME NOT LIKE '하이골드%' AND STOCK_NAME NOT LIKE 'KINDEX%'  AND STOCK_NAME NOT LIKE 'KOSEF%'   AND STOCK_NAME NOT LIKE '하나니켈%'   AND STOCK_NAME NOT LIKE 'TREX%' AND STOCK_NAME NOT LIKE '코리아0%' AND STOCK_NAME NOT LIKE '동북아%'  AND STOCK_NAME NOT LIKE '아시아 %' AND STOCK_NAME NOT LIKE '%우B' AND STOCK_NAME NOT LIKE '%1호' AND STOCK_NAME NOT LIKE '%2호' AND STOCK_NAME NOT LIKE '%K' AND STOCK_NAME NOT LIKE '%채권%' AND STOCK_NAME NOT LIKE '%신탁%' AND STOCK_NAME NOT LIKE '%TIGER%' AND STOCK_NAME NOT LIKE '%KODEX%' AND STOCK_NAME NOT LIKE '%KINDEX%' AND STOCK_NAME NOT LIKE '%ARIRANG%' AND STOCK_NAME NOT LIKE '%(합성%'";
        private DataAccess _DataAcc;
        private string _mode = "";
        int _workerCnt = 0;
        ArrayParam _arr = new ArrayParam();
        ArrayParams _arrs = new ArrayParams();
        ArrayParams _arrsDelete = new ArrayParams();
        #endregion

        #region "생성자"
        public frmSaverNew()
        {
            InitializeComponent();
            _DataAcc = new DataAccess();
        }

        public frmSaverNew(string mode)
        {
            InitializeComponent();
            _DataAcc = new DataAccess();
            _mode = mode;
        }
#endregion

        #region "실행함수"
        public void ExcuteStockMaster()
        {
            _rowCnt = 0;
            _arrs.Clear();
            _arrsDelete.Clear();
            DataView dv;
            try
            {
                if (ucMainStockVer2._KospiStockDataset == null) { return; }
                
                mySqlDbConn conn = new mySqlDbConn();
                conn.ExecuteNonQuery("truncate table stock_master", null, CommandType.Text);

                dv = new DataView(ucMainStockVer2._KospiStockDataset.Tables[0]);
                dv.RowFilter = _FILTERSTR;
                foreach (DataRowView dr in dv)
                {
                    if (dr["STOCK_CODE"].ToString().Trim() == "") continue;
                    _arr.Clear();
                    _arr.Add("STOCK_CODE", dr["STOCK_CODE"].ToString().Trim());
                    _arr.Add("STOCK_NAME", dr["STOCK_NAME"].ToString().Trim());
                    _arr.Add("GUBUN","KOSPI");
                    _arrs.Add(_arr);
                }


                if (ucMainStockVer2._KosDakStockDataset == null) { return; }
                dv = new DataView(ucMainStockVer2._KosDakStockDataset.Tables[0]);
                dv.RowFilter = _FILTERSTR;
                foreach (DataRowView dr in dv)
                {
                    if (dr["STOCK_CODE"].ToString().Trim() == "") continue;
                    _arr.Clear();
                    _arr.Add("STOCK_CODE", dr["STOCK_CODE"].ToString().Trim());
                    _arr.Add("STOCK_NAME", dr["STOCK_NAME"].ToString().Trim());
                    _arr.Add("GUBUN", "KOSDAQ");
                    _arrs.Add(_arr);
                }
                conn.ExecuteNonMultiInsert("stock_master", _arrs);
            }
            finally
            {
                _browser.Dispose();
                ucMainStockVer2.DisConnection();
                Application.Exit();
            }
        }

        public void ExcuteCompanyCEO()
        {
            DataView dv;
            DataSet ds = new DataSet();
            DataTable dt;


            if (ucMainStockVer2._allStockDataset == null) { return; }

            try
            {
                dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
                if (txtSearch.Text.Trim() != "")
                {
                    dv.RowFilter = "STOCK_CODE = '" + txtSearch.Text.Trim() + "' AND " + _FILTERSTR;
                }
                else
                {
                    dv.RowFilter = _FILTERSTR;
                }
                dv.Sort = "STOCK_CODE ASC";
                dataGridView1.DataSource = dv;

                foreach (DataRowView dr in dv)
                {
                    tslStatus.Text = dr["STOCK_CODE"].ToString().Trim() + "-" + _workerCnt;
                    try
                    {
                        dt = GetCEOAndETC(dr["STOCK_CODE"].ToString().Trim());
                        if (dt == null)
                        {
                            dt = GetCEOAndETC(dr["STOCK_CODE"].ToString().Trim());
                            if (dt == null)
                            {
                                dt = GetCEOAndETC(dr["STOCK_CODE"].ToString().Trim());
                                if (dt == null)
                                {
                                    dt = GetCEOAndETC(dr["STOCK_CODE"].ToString().Trim());
                                    if (dt == null)
                                    {
                                        dt = GetCEOAndETC(dr["STOCK_CODE"].ToString().Trim());
                                        if (dt == null)
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch { continue; }
                    if (dt == null) continue;
                    if (dt.Rows.Count < 1) continue;

                    foreach (DataRow drv in dt.Rows)
                    {
                        _DataAcc.p_company_ceo_Add(
                            "A",
                            dr["STOCK_CODE"].ToString().Trim(),
                            drv["PUB_DATE"].ToString().Trim(),
                            drv["성명"].ToString().Trim(),
                            drv["성별"].ToString().Trim(),
                            drv["출생년월"].ToString().Trim(),
                            drv["직위"].ToString().Trim(),
                            drv["등기임원여부"].ToString().Trim(),
                            drv["상근여부"].ToString().Trim(),
                            drv["담당업무"].ToString().Trim(),
                            drv["주요경력"].ToString().Trim(),
                            drv["의결권있는주식"].ToString().Trim(),
                            drv["의결권없는주식"].ToString().Trim(),
                            drv["재직기간"].ToString().Trim(),
                            drv["임기만료일"].ToString().Trim(),
                            true,
                            null,
                            null
                            );
                    }
                    _workerCnt++;
                    Application.DoEvents();
                }
            }
            finally
            {
            }
        }

        public void ExcuteCompanyData()
        {
            DataSet ds = new DataSet();
            DataRow drCompany;
            DataView dv, dvCompany;
            DataTable dt;


            ds = _DataAcc.p_company_info_query("2", "", false);
            dvCompany = new DataView(ds.Tables[0]);

            if (ucMainStockVer2._allStockDataset == null) { return; }

            try
            {
                dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
                if (txtSearch.Text.Trim() != "")
                {
                    dv.RowFilter = "STOCK_CODE = '" + txtSearch.Text.Trim() + "' AND " + _FILTERSTR;
                }
                else
                {
                    dv.RowFilter = _FILTERSTR;
                }
                dv.Sort = "STOCK_CODE ASC";
                dataGridView1.DataSource = dv;

                foreach (DataRowView dr in dv)
                {
                    dt = GetCompanyInfo(dr["STOCK_CODE"].ToString().Trim());
                    if (dt == null)
                    {
                        dt = GetCompanyInfo(dr["STOCK_CODE"].ToString().Trim());
                        if (dt == null)
                        {
                            dt = GetCompanyInfo(dr["STOCK_CODE"].ToString().Trim());
                            if (dt == null)
                            {
                                dt = GetCompanyInfo(dr["STOCK_CODE"].ToString().Trim());
                                if (dt == null)
                                {
                                    dt = GetCompanyInfo(dr["STOCK_CODE"].ToString().Trim());
                                    if (dt == null)
                                    {
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                    if (dt.Rows.Count < 1) continue;
                    drCompany = dt.Rows[0];
                    _DataAcc.p_company_info_Add("A",
                        drCompany["STOCK_CODE"].ToString().Trim(),
                        drCompany["JUJU"].ToString().Trim(),
                        drCompany["CLASS_GB"].ToString().Trim(),
                        drCompany["CEO"].ToString().Trim(),
                        drCompany["PRE_CNAME"].ToString().Trim(),
                        drCompany["CREATE_DATE"].ToString().Trim(),
                        drCompany["SANGJANG_DATE"].ToString().Trim(),
                        drCompany["WORKER_CNT"].ToString().Trim(),
                        drCompany["GROUP_NAME"].ToString().Trim(),
                        drCompany["HOMEPAGE"].ToString().Trim(),
                        drCompany["ADDRESS"].ToString().Trim(),
                        drCompany["COMPANY_TEL"].ToString().Trim(),
                        drCompany["JUDAM_TEL"].ToString().Trim(),
                        drCompany["GAMSA_TEXT"].ToString().Trim(),
                        drCompany["MAIN_BANK"].ToString().Trim(),
                        drCompany["MAIN_PRODUCT"].ToString().Trim(),
                        drCompany["ETC"].ToString().Trim(),
                        drCompany["REMARK"].ToString().Trim(),
                        true,
                        null,
                        null
                        );
                }
            }
            finally
            {
            }
        }

        public async void ExecuteEvening(int procNum = 0)
        {
            DataView dv;
            if (ucMainStockVer2._allStockDataset == null) { return; }
            dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
            if (txtSearch.Text.Trim() != "")
            {
                dv.RowFilter = "STOCK_CODE = '" + txtSearch.Text.Trim() + "' AND " + _FILTERSTR;
            }
            else
            {
                dv.RowFilter = _FILTERSTR;
            }

            //dv.RowFilter = "STOCK_CODE > '104480' AND " + _FILTERSTR;
            dv.Sort = "STOCK_CODE ASC";
            dataGridView1.DataSource = dv;

            mySqlDbConn conn = new mySqlDbConn();

            if (procNum.Equals(1)) goto skip1;
            else if (procNum.Equals(2)) goto skip2;
            else if (procNum.Equals(3)) goto skip3;
            else if (procNum.Equals(4)) goto skip4;
            //else if (procNum.Equals(5)) goto skip5;


        skip1 :

            _rowCnt = 0;
            foreach (DataRowView dr in dv)
            {
                _arrsDelete.Clear();
                _arrs.Clear();
                tslStatus.Text = dr["STOCK_CODE"].ToString() + "-" + _rowCnt.ToString();
                await DoOpt10081Async(dr["STOCK_CODE"].ToString(), dr["STOCK_NAME"].ToString());
                if (_arrsDelete.Count > 0)
                {
                    conn.ExecuteNonMultiDelete("stock_day_data", _arrsDelete);
                    conn.ExecuteNonMultiInsert("stock_day_data", _arrs);
                }
                System.Threading.Thread.Sleep(300);
                _rowCnt++;
            }

        skip2:

            _rowCnt = 0;
            foreach (DataRowView dr in dv)
            {
                _arrsDelete.Clear();
                _arrs.Clear();
                tslStatus.Text = dr["STOCK_CODE"].ToString() + "-" + _rowCnt.ToString();
                await DoOpt10059Async(dr["STOCK_CODE"].ToString(), dr["STOCK_NAME"].ToString());
                if (_arrsDelete.Count > 0)
                {
                    conn.ExecuteNonMultiDelete("stock_buysell_state", _arrsDelete);
                    conn.ExecuteNonMultiInsert("stock_buysell_state", _arrs);
                }
                System.Threading.Thread.Sleep(300);
                _rowCnt++;
            }



        skip3:
            _rowCnt = 0;
            foreach (DataRowView dr in dv)
            {
                _arrsDelete.Clear();
                _arrs.Clear();
                tslStatus.Text = dr["STOCK_CODE"].ToString() + "-" + _rowCnt.ToString();
                await DoOpt10001Async(dr["STOCK_CODE"].ToString(), dr["STOCK_NAME"].ToString());
                if (_arrsDelete.Count > 0)
                {
                    conn.ExecuteNonMultiDelete("stock_finance", _arrsDelete);
                    conn.ExecuteNonMultiInsert("stock_finance", _arrs);
                }
                System.Threading.Thread.Sleep(300);
                _rowCnt++;
            }

        skip4:
            _rowCnt = 0;
            foreach (DataRowView dr in dv)
            {
                _arrsDelete.Clear();
                _arrs.Clear();
                tslStatus.Text = dr["STOCK_CODE"].ToString() + "-" + _rowCnt.ToString();
                await DoOpt10082Async(dr["STOCK_CODE"].ToString(), dr["STOCK_NAME"].ToString());
                if (_arrsDelete.Count > 0)
                {
                    conn.ExecuteNonMultiDelete("stock_week_data", _arrsDelete);
                    conn.ExecuteNonMultiInsert("stock_week_data", _arrs);
                }
                System.Threading.Thread.Sleep(300);
                _rowCnt++;
            }

        //skip5:
        //    _rowCnt = 0;
        //    foreach (DataRowView dr in dv)
        //    {
        //        _arrsDelete.Clear();
        //        _arrs.Clear();
        //        tslStatus.Text = dr["STOCK_CODE"].ToString() + "-" + _rowCnt.ToString();
        //        await DoOpt10014Async(dr["STOCK_CODE"].ToString(), dr["STOCK_NAME"].ToString());
        //        if (_arrsDelete.Count > 0)
        //        {
        //            conn.ExecuteNonMultiDelete("stock_gong", _arrsDelete);
        //            conn.ExecuteNonMultiInsert("stock_gong", _arrs);
        //        }
        //        System.Threading.Thread.Sleep(300);
        //        _rowCnt++;
        //    }
            Application.Exit();
        }

        public async void ExecuteMorning()
        {
            DataView dv;
            if (ucMainStockVer2._allStockDataset == null) { return; }
            dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
            if (txtSearch.Text.Trim() != "")
            {
                dv.RowFilter = "STOCK_CODE = '" + txtSearch.Text.Trim() + "' AND " + _FILTERSTR;
            }
            else
            {
                dv.RowFilter = _FILTERSTR;
            }
            dv.Sort = "STOCK_CODE ASC";
            dataGridView1.DataSource = dv;


            mySqlDbConn conn = new mySqlDbConn();

            _rowCnt = 0;
            foreach (DataRowView dr in dv)
            {
                _arrsDelete.Clear();
                _arrs.Clear();
                tslStatus.Text = dr["STOCK_CODE"].ToString() + "-" + _rowCnt.ToString();
                await DoOpt10014Async(dr["STOCK_CODE"].ToString(), dr["STOCK_NAME"].ToString());
                if (_arrsDelete.Count > 0)
                {
                    conn.ExecuteNonMultiDelete("stock_gong", _arrsDelete);
                    conn.ExecuteNonMultiInsert("stock_gong", _arrs);
                }
                System.Threading.Thread.Sleep(300);
                _rowCnt++;
            }

            _rowCnt = 0;
            foreach (DataRowView dr in dv)
            {
                _arrsDelete.Clear();
                _arrs.Clear();
                tslStatus.Text = dr["STOCK_CODE"].ToString() + "-" + _rowCnt.ToString();
                await DoOpt10013Async(dr["STOCK_CODE"].ToString(), dr["STOCK_NAME"].ToString());
                if (_arrsDelete.Count > 0) { 
                    conn.ExecuteNonMultiDelete("stock_credit", _arrsDelete);
                    conn.ExecuteNonMultiInsert("stock_credit", _arrs);
                }
                _rowCnt++;
            }



            _arrsDelete.Clear();
            _arrs.Clear();
            _rowCnt = 0;
            conn.ExecuteNonQuery("truncate table stock_master", null, CommandType.Text);
            dv = new DataView(ucMainStockVer2._KospiStockDataset.Tables[0]);
            dv.RowFilter = _FILTERSTR;
            foreach (DataRowView dr in dv)
            {
                if (dr["STOCK_CODE"].ToString().Trim() == "") continue;
                _arr.Clear();
                _arr.Add("STOCK_CODE", dr["STOCK_CODE"].ToString().Trim());
                _arr.Add("STOCK_NAME", dr["STOCK_NAME"].ToString().Trim());
                _arr.Add("GUBUN", "KOSPI");
                _arrs.Add(_arr);
            }

            if (ucMainStockVer2._KosDakStockDataset == null) { return; }
            dv = new DataView(ucMainStockVer2._KosDakStockDataset.Tables[0]);
            dv.RowFilter = _FILTERSTR;
            foreach (DataRowView dr in dv)
            {
                if (dr["STOCK_CODE"].ToString().Trim() == "") continue;
                _arr.Clear();
                _arr.Add("STOCK_CODE", dr["STOCK_CODE"].ToString().Trim());
                _arr.Add("STOCK_NAME", dr["STOCK_NAME"].ToString().Trim());
                _arr.Add("GUBUN", "KOSDAQ");
                _arrs.Add(_arr);
            }
            conn.ExecuteNonMultiInsert("stock_master", _arrs);
            Application.Exit();
        }
        #endregion

        #region "Chrome"
        private void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            _browser = new ChromiumWebBrowser("");
            _browser.FrameLoadEnd += new EventHandler<CefSharp.FrameLoadEndEventArgs>(browser_FrameLoadEnd);
            this.Controls.Add(_browser);
            _browser.Dock = DockStyle.Fill;
        }

        private void DisposeBrowser()
        {
            _browser.Dispose();
            Cef.Shutdown();
        }

        private void browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                _browser.GetSourceAsync().ContinueWith(taskHtml =>
                {
                    _IsReady = true;
                    _htmlSource = taskHtml.Result;
                });
            }
        }

        private DataTable GetCEOAndETC(string stockCode)
        {

            DataSet ds = Dart(stockCode);
            DataTable dt = new DataTable("STOCK_CEO");
            DataRow dr;

            if (ds.Tables["list"] == null) return dt;
            string pubDate = ds.Tables["list"].Rows[0]["rcp_dt"].ToString().Trim();
            string url = "http://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + ds.Tables["list"].Rows[0]["rcp_no"].ToString().Trim();
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                _browser.Load(url);
            }
            //while (browser.IsBusy || browser.DocumentText.Length < 5) { Application.DoEvents(); }
            _IsReady = false;

            DateTime startTime = DateTime.Now;
            TimeSpan ts;
            DateTime excuteTime;
            int diffSecond;
            do
            {
                try
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); //스택 오버플로워 오류 가능성있음
                }

                excuteTime = DateTime.Now;

                ts = excuteTime - startTime;
                diffSecond = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
                if (diffSecond > 5) return null;
            } while (!_IsReady);

            //임원 및 직원의 현황
            string source = _htmlSource;
            source = System.Net.WebUtility.HtmlDecode(source);
            string text = "";

            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<';
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            const string tabformatSpace = @"(\r|\n|\t)";
            const string trformatSpace = @"</(tr|TR)>";
            const string tdformatSpace = @"</(td|TD)>";

            var tabformatSpaceRegex = new Regex(tabformatSpace, RegexOptions.Multiline);
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);
            var trformatSpaceRegex = new Regex(trformatSpace, RegexOptions.Multiline);
            var tdformatSpaceRegex = new Regex(tdformatSpace, RegexOptions.Multiline);


            if (source.IndexOf("임원 및 직원의 현황") < 0)
            {
                //dr["JUJU"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("임원 및 직원의 현황"));
                text = text.Substring(text.IndexOf("viewDoc("), text.IndexOf("'dart3.xsd');}") - text.IndexOf("viewDoc("));
                text = text.Replace("viewDoc(", "").Replace("'", "");
                string[] str = text.Split(',');
                url = String.Format("http://dart.fss.or.kr/report/viewer.do?rcpNo={0}&dcmNo={1}&eleId={2}&offset={3}&length={4}&dtd=dart3.xsd", str[0].Trim(), str[1].Trim(), str[2].Trim(), str[3].Trim(), str[4].Trim());
            }


            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                _browser.Load(url);
            }
            //while (browser.IsBusy || browser.DocumentText.Length < 5) { Application.DoEvents(); }
            _IsReady = false;

            startTime = DateTime.Now;
            do
            {
                try
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); //스택 오버플로워 오류 가능성있음
                }

                excuteTime = DateTime.Now;

                ts = excuteTime - startTime;
                diffSecond = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
                if (diffSecond > 5) return null;
            } while (!_IsReady);

            //임원 및 직원의 현황

            source = _htmlSource;
            source = System.Net.WebUtility.HtmlDecode(source);

            text = tabformatSpaceRegex.Replace(source, string.Empty);
            text = tagWhiteSpaceRegex.Replace(text, "><");

            text = text.Substring(text.IndexOf("</thead><tbody><tr>") + 15);
            text = text.Substring(0, text.IndexOf("</tbody>"));
            text = text.Replace("</p>", ":").Replace("<br>", ":");
            text = trformatSpaceRegex.Replace(text, "^");
            text = tdformatSpaceRegex.Replace(text, "|");
            text = stripFormattingRegex.Replace(text, "");

            string[] strTr = text.Split('^');
            string[] strTd;

            dt.Columns.Add("PUB_DATE");
            dt.Columns.Add("성명");
            dt.Columns.Add("성별");
            dt.Columns.Add("출생년월");
            dt.Columns.Add("직위");
            dt.Columns.Add("등기임원여부");
            dt.Columns.Add("상근여부");
            dt.Columns.Add("담당업무");
            dt.Columns.Add("주요경력");
            dt.Columns.Add("의결권있는주식");
            dt.Columns.Add("의결권없는주식");
            dt.Columns.Add("재직기간");
            dt.Columns.Add("임기만료일");
            int ix = 0;
            foreach (string str in strTr)
            {
                ix = 0;
                if (str == "") continue;
                dr = dt.NewRow();
                strTd = str.Split('|');
                dr["PUB_DATE"] = pubDate;
                dr["성명"] = strTd[ix++].Trim().Replace(":", "");
                if (strTd.Length == 13)
                {
                    dr["성별"] = strTd[ix++].Trim().Replace(":", "");
                }
                dr["출생년월"] = strTd[ix++].Trim().Replace(":", "");
                dr["직위"] = strTd[ix++].Trim().Replace(":", "");
                dr["등기임원여부"] = strTd[ix++].Trim().Replace(":", "");
                dr["상근여부"] = strTd[ix++].Trim().Replace(":", "");
                dr["담당업무"] = strTd[ix++].Trim().Replace(":", "");
                dr["주요경력"] = strTd[ix++].Trim().Replace(":", Environment.NewLine);
                dr["의결권있는주식"] = strTd[ix++].Trim().Replace(":", "");
                dr["의결권없는주식"] = strTd[ix++].Trim().Replace(":", "");
                dr["재직기간"] = strTd[ix++].Trim().Replace(":", "");
                dr["임기만료일"] = strTd[ix++].Trim().Replace(":", "");
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataSet Dart(string stockCode)
        {
            string authKey = "";
            int seed = DateTime.Now.Millisecond;
            Random r = new Random(seed);

            int ranNum = r.Next();


            /*
            sundown99@hanafos.com
            인증키 : 2871266923d9676db1ebde770fddbc27af172b99
            발급일 : 2016-11-23

            sundown99@nate.com
            인증키 : 65c65a332282daf3dcf8b593111001ea05edb8d3
            발급일 : 2016-12-09

            kojedevil@gmail.com
            인증키 : 111688347f9a0c9363962e559b49db99ebf4f7e0
            발급일 : 2016-12-09

            kojedevil@gmail.com
            인증키 : 111688347f9a0c9363962e559b49db99ebf4f7e0
            발급일 : 2016-12-09


            ydk3119@hanmail.net
            인증키 : 751d353e9651c526800b4a5f25232d8ce826a486
            발급일 : 2016-12-09
             * 

            kojeyangari@naver.com
            인증키 : 1738de9acf96edf110619de6573ded998c98a17b
            발급일 : 2016-12-09
             * 
              //인증키 : da676408b4af06f3bec0d0200edf6f2d731cd61b   //sundown99@daum.net
            //발급일 : 2016-12-12
             */
            if (ranNum % 9 == 0)
            {
                authKey = "2871266923d9676db1ebde770fddbc27af172b99";
            }
            else if (ranNum % 9 == 1)
            {
                authKey = "65c65a332282daf3dcf8b593111001ea05edb8d3";
            }
            else if (ranNum % 9 == 2)
            {
                authKey = "111688347f9a0c9363962e559b49db99ebf4f7e0";
            }
            else if (ranNum % 9 == 3)
            {
                authKey = "111688347f9a0c9363962e559b49db99ebf4f7e0";
            }
            else if (ranNum % 9 == 4)
            {
                authKey = "751d353e9651c526800b4a5f25232d8ce826a486";
            }
            else if (ranNum % 9 == 5)
            {
                authKey = "dabf505d5d3326b779cb6d7b523c8a4766bfeb8d";
            }
            else if (ranNum % 9 == 6)
            {
                authKey = "a241a5f9b253c93b17f7a9d51768ff58131fb5dd";
            }
            else if (ranNum % 9 == 7)
            {
                authKey = "1738de9acf96edf110619de6573ded998c98a17b";
            }
            else if (ranNum % 9 == 8)
            {
                authKey = "da676408b4af06f3bec0d0200edf6f2d731cd61b";
            }
            else
            {
                authKey = "db3e22ead89d0449f95fbbf966969cc41e6cf943"; // 종근
            }

            authKey = "da676408b4af06f3bec0d0200edf6f2d731cd61b";

            String apiURL = "http://dart.fss.or.kr/api/search.xml?auth=" + authKey + "&crp_cd=" + stockCode + "&start_dt=19990101&bsn_tp=A003&page_set=1";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader1 = new StreamReader(response.GetResponseStream());

            string page = reader1.ReadToEnd();
            System.Data.DataSet ds = new System.Data.DataSet();

            StringReader sReader = new StringReader(page);

            System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

            ds.ReadXml(reader);
            return ds;
        }

        public DataTable GetCompanyInfo(string stockCode)
        {
            DataRow dr;
            DataTable dt = new DataTable();
            dt.Columns.Add("STOCK_CODE");
            dt.Columns.Add("JUJU");
            dt.Columns.Add("CLASS_GB");
            dt.Columns.Add("CEO");
            dt.Columns.Add("PRE_CNAME");
            dt.Columns.Add("CREATE_DATE");
            dt.Columns.Add("SANGJANG_DATE");
            dt.Columns.Add("WORKER_CNT");
            dt.Columns.Add("GROUP_NAME");
            dt.Columns.Add("HOMEPAGE");
            dt.Columns.Add("ADDRESS");
            dt.Columns.Add("COMPANY_TEL");
            dt.Columns.Add("JUDAM_TEL");
            dt.Columns.Add("GAMSA_TEXT");
            dt.Columns.Add("MAIN_BANK");
            dt.Columns.Add("MAIN_PRODUCT");
            dt.Columns.Add("ETC");
            dt.Columns.Add("REMARK");
            dr = dt.NewRow();

            dr["STOCK_CODE"] = stockCode;

            //WebBrowser browser = new WebBrowser();
            //browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            string url = String.Format("http://media.kisline.com/highlight/mainHighlight.nice?paper_stock={0}&nav=1&header=N&comp=daishin", stockCode);
            //browser.ScriptErrorsSuppressed = true;
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                _browser.Load(url);
            }
            //while (browser.IsBusy || browser.DocumentText.Length < 5) { Application.DoEvents(); }
            _IsReady = false;
            DateTime startTime = DateTime.Now;
            TimeSpan ts;
            DateTime excuteTime;
            int diffSecond;
            do
            {
                try
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); //스택 오버플로워 오류 가능성있음
                }

                excuteTime = DateTime.Now;

                ts = excuteTime - startTime;
                diffSecond = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
                if (diffSecond > 5) return null;
            } while (!_IsReady);
            //browser.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);

            string source = _htmlSource;
            source = System.Net.WebUtility.HtmlDecode(source);
            string text = "";

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

            //if (source.Length < 3000) { return dt; }
            if (source.IndexOf("<p><img src=\"http://cdn-image.kisline.com/media/common/images/search/txt_sea1.gif\" alt=\"기업분석의 최강자! 국내 최대/최고의 기업정보 보유 기관! 여러분의 성공투자를 NICE신용평가정보와 함께 하세요.\" /></p>") > -1) return dt;


            if (source.IndexOf("series: [{") < 0)
            {
                dr["JUJU"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("series: [{"), source.IndexOf("}]") - source.IndexOf("series: [{"));
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = text.Substring(text.IndexOf("[[") + 1, text.IndexOf("],{") - text.IndexOf("[["));
                dr["JUJU"] = text.Replace("'", string.Empty);//주주현황
            }


            if (source.IndexOf("<p class=\"cot_tx\" title=\"회사정보\">") < 0)
            {
                dr["CLASS_GB"] = "";//업종
            }
            else
            {
                text = source.Substring(source.IndexOf("<p class=\"cot_tx\" title=\"회사정보\">"));
                text = text.Substring(0, text.IndexOf("</p>") + 4);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["CLASS_GB"] = text;//업종
            }

            if (source.IndexOf("<th scope=\"row\">대표이사</th>") < 0)
            {
                dr["CEO"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">대표이사</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["CEO"] = text;//대표이사
            }

            if (source.IndexOf("<th scope=\"row\">구상호</th>") < 0)
            {
                dr["PRE_CNAME"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">구상호</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["PRE_CNAME"] = text;//(구)상호
            }

            if (source.IndexOf("<th scope=\"row\">설립일</th>") < 0) return dt;
            text = source.Substring(source.IndexOf("<th scope=\"row\">설립일</th>"));
            text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
            text = tabformatSpaceRegex.Replace(text, string.Empty);
            text = tagWhiteSpaceRegex.Replace(text, "><");
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            text = stripFormattingRegex.Replace(text, string.Empty);
            dr["CREATE_DATE"] = text;//설립일


            if (source.IndexOf("<th scope=\"row\">상장일</th>") < 0)
            {
                dr["SANGJANG_DATE"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">상장일</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["SANGJANG_DATE"] = text;//상장일
            }


            if (source.IndexOf("<th scope=\"row\">종업원수</th>") < 0)
            {
                dr["WORKER_CNT"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">종업원수</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["WORKER_CNT"] = text;//종업원수
            }

            if (source.IndexOf("<th scope=\"row\">그룹명</th>") < 0)
            {
                dr["GROUP_NAME"] = "";//그룹명
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">그룹명</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["GROUP_NAME"] = text;//그룹명
            }


            if (source.IndexOf("<th scope=\"row\">홈페이지</th>") < 0)
            {
                dr["HOMEPAGE"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">홈페이지</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["HOMEPAGE"] = text;//홈페이지
            }

            if (source.IndexOf("<th scope=\"row\">주소</th>") < 0)
            {
                dr["ADDRESS"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">주소</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["ADDRESS"] = text;//주소
            }


            if (source.IndexOf("<th scope=\"row\">대표전화</th>") < 0)
            {
                dr["COMPANY_TEL"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">대표전화</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["COMPANY_TEL"] = text;//대표전화
            }


            if (source.IndexOf("<th scope=\"row\">주식담당자</th>") < 0)
            {
                dr["JUDAM_TEL"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">주식담당자</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["JUDAM_TEL"] = text;//주식담당자
            }



            if (source.IndexOf("<th scope=\"row\">감사의견</th>") < 0)
            {
                dr["GAMSA_TEXT"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">감사의견</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["GAMSA_TEXT"] = text;//감사의견
            }

            if (source.IndexOf("<th scope=\"row\">주거래은행</th>") < 0)
            {
                dr["MAIN_BANK"] = "";//주거래은행
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">주거래은행</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["MAIN_BANK"] = text;//주거래은행
            }


            if (source.IndexOf("<th scope=\"row\">주요품목</th>") < 0)
            {
                dr["MAIN_PRODUCT"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">주요품목</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["MAIN_PRODUCT"] = text;//주요품목
            }

            if (source.IndexOf("<th scope=\"row\">특기사항</th>") < 0)
            {
                dr["ETC"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">특기사항</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["ETC"] = text;//특기사항
            }

            if (source.IndexOf("<!-- 현황 및 전망 -->") < 0)
            {
                dr["REMARK"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<!-- 현황 및 전망 -->"));
                text = text.Substring(text.IndexOf("<tbody>"), text.IndexOf("</tbody>") - text.IndexOf("<tbody>") + 8);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["REMARK"] = text;//현황과 전망
            }
            dt.Rows.Add(dr);

            GC.Collect();
            return dt;
        }
        #endregion

        #region "OPT Task 함수"
        async Task DoOpt10001Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_Opt10001EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted) {
                    ucMainStockVer2.OnReceiveTrData_Opt10001 -= handler; // Unsubscribe
                    return;
                }

                if (d != null && d.Tables.Count > 0 && d.Tables[0].Rows.Count > 0)
                {
                    StoredOpt10001(d);
                }
                ucMainStockVer2.OnReceiveTrData_Opt10001 -= handler; // Unsubscribe
                tcs.SetResult(true);
                // Add your one-time-only code here
            };
            ucMainStockVer2.OnReceiveTrData_Opt10001 += handler;

            ucMainStockVer2.Opt10001_OnReceiveTrData(stockCode, stockName);
            await tcs.Task;
            tcs = null;
        }

        async Task DoOpt10013Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_Opt10013EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted) {
                    return;
                }

                if (d != null && d.Tables.Count > 0 && d.Tables[0].Rows.Count > 0)
                {
                    StoredOpt10013(d);
                }
                ucMainStockVer2.OnReceiveTrData_Opt10013 -= handler; // Unsubscribe
                tcs.SetResult(true);
                // Add your one-time-only code here
            };
            ucMainStockVer2.OnReceiveTrData_Opt10013 += handler;

            ucMainStockVer2.Opt10013_OnReceiveTrDataNew(stockCode, stockName, dtpStdDate.Value.ToString("yyyyMMdd"), "1");
            await tcs.Task;
            tcs = null;
        }

        async Task DoOpt10014Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_Opt10014EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted) {
                    return;
                }

                if (d != null && d.Tables.Count > 0 && d.Tables[0].Rows.Count > 0)
                {
                    StoredOpt10014(d);
                }
                ucMainStockVer2.OnReceiveTrData_Opt10014 -= handler; // Unsubscribe
                tcs.SetResult(true);
                // Add your one-time-only code here
            };
            ucMainStockVer2.OnReceiveTrData_Opt10014 += handler;

            ucMainStockVer2.Opt10014_OnReceiveTrDataNew(stockCode, stockName, "0", dtpStdDate.Value.ToString("yyyyMMdd"), dtpStdDate.Value.ToString("yyyyMMdd"));
            await tcs.Task;
        }

        async Task DoOpt10059Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_Opt10059PriceEventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted) {
                    return;
                }

                if (d != null && d.Tables.Count > 0 && d.Tables[0].Rows.Count > 0)
                {
                    StoredOpt10059Price(d);
                }
                ucMainStockVer2.OnReceiveTrData_Opt10059Price -= handler; // Unsubscribe
                tcs.SetResult(true);
                // Add your one-time-only code here
            };
            ucMainStockVer2.OnReceiveTrData_Opt10059Price += handler;

            ucMainStockVer2.Opt10059_OnReceiveTrDataPriceNew(dtpStdDate.Value.ToString("yyyyMMdd"), stockCode, stockName, "1", "0", "1");
            await tcs.Task;
        }

        async Task DoOpt10081Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_opt10081EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted) {
                    return;
                }

                if (d != null && d.Tables.Count > 0 && d.Tables[0].Rows.Count > 0)
                {
                    StoredOpt10081(d);
                }
                ucMainStockVer2.OnReceiveTrData_opt10081 -= handler; // Unsubscribe
                tcs.SetResult(true);
                // Add your one-time-only code here
            };
            ucMainStockVer2.OnReceiveTrData_opt10081 += handler;

            ucMainStockVer2.Opt10081_OnReceiveTrData(stockCode, stockName, dtpStdDate.Value.ToString("yyyyMMdd"));
            await tcs.Task;
        }

        async Task DoOpt10082Async(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_opt10082EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted) {
                    return;
                }

                if (d != null && d.Tables.Count > 0 && d.Tables[0].Rows.Count > 0)
                {
                    StoredOpt10082(d);
                }
                ucMainStockVer2.OnReceiveTrData_opt10082 -= handler; // Unsubscribe
                tcs.SetResult(true);
                // Add your one-time-only code here
            };
            ucMainStockVer2.OnReceiveTrData_opt10082 += handler;

            ucMainStockVer2.Opt10082_OnReceiveTrData(stockCode, stockName, dtpStdDate.Value.ToString("yyyyMMdd"));
            await tcs.Task;
        }
        #endregion

        #region "OPT STORE"
        private void StoredOpt10001(DataSet ds)
        {

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string 종목코드 = dr["종목코드"].ToString().Trim();
                string 종목명 = dr["종목명"].ToString().Trim();
                string 결산월 = dr["결산월"].ToString().Trim();
                string 액면가 = dr["액면가"].ToString().Trim();
                string 자본금 = dr["자본금"].ToString().Trim();
                string 상장주식 = dr["상장주식"].ToString().Trim();
                string 신용비율 = dr["신용비율"].ToString().Trim();
                string 연중최고 = dr["연중최고"].ToString().Trim();
                string 연중최저 = dr["연중최저"].ToString().Trim();
                string 시가총액 = dr["시가총액"].ToString().Trim();
                string 시가총액비중 = dr["시가총액비중"].ToString().Trim();
                string 외인소진률 = dr["외인소진률"].ToString().Trim();
                string 대용가 = dr["대용가"].ToString().Trim();
                string PER = dr["PER"].ToString().Trim();
                string EPS = dr["EPS"].ToString().Trim();
                string ROE = dr["ROE"].ToString().Trim();
                string PBR = dr["PBR"].ToString().Trim();
                string EV = dr["EV"].ToString().Trim();
                string BPS = dr["BPS"].ToString().Trim();
                string 매출액 = dr["매출액"].ToString().Trim();
                string 영업이익 = dr["영업이익"].ToString().Trim();
                string 당기순이익 = dr["당기순이익"].ToString().Trim();
                string 최고250 = dr["250최고"].ToString().Trim();
                string 최저250 = dr["250최저"].ToString().Trim();
                string 시가 = dr["시가"].ToString().Trim();
                string 고가 = dr["고가"].ToString().Trim();
                string 저가 = dr["저가"].ToString().Trim();
                string 상한가 = dr["상한가"].ToString().Trim();
                string 하한가 = dr["하한가"].ToString().Trim();
                string 기준가 = dr["기준가"].ToString().Trim();
                string 최고가일250 = dr["250최고가일"].ToString().Trim();
                string 최고가대비율250 = dr["250최고가대비율"].ToString().Trim();
                string 최저가일250 = dr["250최저가일"].ToString().Trim();
                string 최저가대비율250 = dr["250최저가대비율"].ToString().Trim();
                string 현재가 = dr["현재가"].ToString().Trim();
                string 대비기호 = dr["대비기호"].ToString().Trim();
                string 전일대비 = dr["전일대비"].ToString().Trim();
                string 등락율 = dr["등락율"].ToString().Trim();
                string 거래량 = dr["거래량"].ToString().Trim();
                string 거래대비 = dr["거래대비"].ToString().Trim();
                string 액면가단위 = dr["액면가단위"].ToString().Trim();


                _arr.Clear();
                _arr.Add("STOCK_CODE", 종목코드);
                _arrsDelete.Add(_arr);

                _arr.Clear();
                _arr.Add("STOCK_CODE", 종목코드);
                _arr.Add("STOCK_NAME", 종목명);
                _arr.Add("RESULT_MONTH", 결산월);
                _arr.Add("BASE_P", 액면가);
                _arr.Add("CAPITAL_P", 자본금);
                _arr.Add("STOCK_QTY", 상장주식);
                _arr.Add("CREDIT_RATE", 신용비율);
                _arr.Add("YEAR_HIGH_P", 연중최고);
                _arr.Add("YEAR_LOW_P", 연중최저);
                _arr.Add("STOCK_TOTAL_P", 시가총액);
                _arr.Add("STOCK_TOTAL_P_RATE", 시가총액비중);
                _arr.Add("F_REDUCE_RATE", 외인소진률);
                _arr.Add("RENTAL_P", 대용가);
                _arr.Add("PER", PER);
                _arr.Add("EPS", EPS);
                _arr.Add("ROE", ROE);
                _arr.Add("PBR", PBR);
                _arr.Add("EV", EV);
                _arr.Add("BPS", BPS);
                _arr.Add("SALES_P", 매출액);
                _arr.Add("O_PROFIT", 영업이익);
                _arr.Add("P_PROFIT", 당기순이익);
                _arr.Add("HIGH_250", 최고250);
                _arr.Add("LOW_250", 최저250);
                _arr.Add("S_PRICE", 시가);
                _arr.Add("H_PRICE", 고가);
                _arr.Add("L_PRICE", 저가);
                _arr.Add("UP_PRICE", 상한가);
                _arr.Add("DOWN_PRICE", 하한가);
                _arr.Add("STD_PRICE", 기준가);
                _arr.Add("HIGH_250_DAY", 최고가일250);
                _arr.Add("HIGH_250_RATE", 최고가대비율250);
                _arr.Add("LOW_250_DAY", 최저가일250);
                _arr.Add("LOW_250_RATE", 최저가대비율250);
                _arr.Add("END_PRICE", 현재가);
                _arr.Add("SYMBOL", 대비기호);
                _arr.Add("PRE_CONPARE", 전일대비);
                _arr.Add("END_PRICE_RATE", 등락율);
                _arr.Add("VOLUME", 거래량);
                _arr.Add("VOLUME_RATE", 거래대비);
                _arr.Add("PRICE_UNIT", 액면가단위);
                _arr.Add("DEUNG_DATE", DateTime.Now.ToString("yyyyMMdd"));
                _arrs.Add(_arr);
            }
        }

        private void StoredOpt10013(DataSet ds)
        {
            if (ds == null) return;
            if (ds.Tables.Count == 0) return;
            if (ds.Tables[0].Rows.Count == 0) return;
            string preDate = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (preDate.Equals(dr["일자"].ToString().Trim())) continue;
                _arr.Clear();
                _arr.Add("stock_code", ds.Tables[0].Rows[0]["종목코드"].ToString().Trim());
                _arr.Add("stock_date", dr["일자"].ToString().Trim());
                _arrsDelete.Add(_arr);

                _arr.Clear();
                _arr.Add("stock_code", ds.Tables[0].Rows[0]["종목코드"].ToString().Trim());
                _arr.Add("stock_date", dr["일자"].ToString().Trim());
                _arr.Add("end_price", dr["현재가"].ToString().Trim());
                _arr.Add("daebi_gb", dr["전일대비기호"].ToString().Trim());
                _arr.Add("pre_daebi", dr["전일대비"].ToString().Trim());
                _arr.Add("volume", dr["거래량"].ToString().Trim());
                _arr.Add("credit_new", dr["신규"].ToString().Trim());
                _arr.Add("credit_return", dr["상환"].ToString().Trim());
                _arr.Add("credit_jango", dr["잔고"].ToString().Trim());
                _arr.Add("credit_price", dr["금액"].ToString().Trim());
                _arr.Add("credit_daebi", dr["대비"].ToString().Trim());
                _arr.Add("credit_gong_rate", dr["공여율"].ToString().Trim());
                _arr.Add("credit_jango_rate", dr["잔고율"].ToString().Trim());
                _arrs.Add(_arr);
                if (comboBox1.Text.IndexOf("전체") == -1)
                {
                    break;
                }
                preDate = dr["일자"].ToString().Trim();
            }
        }

        private void StoredOpt10014(DataSet ds)
        {
            if (ds == null) return;
            if (ds.Tables.Count == 0) return;
            if (ds.Tables[0].Rows.Count == 0) return;
            string preDate = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (preDate.Equals(dr["일자"].ToString().Trim())) continue;
                _arr.Clear();
                _arr.Add("stock_code", ds.Tables[0].Rows[0]["종목코드"].ToString().Trim());
                _arr.Add("stock_date", dr["일자"].ToString().Trim());
                _arrsDelete.Add(_arr);

                _arr.Clear();
                _arr.Add("stock_code", ds.Tables[0].Rows[0]["종목코드"].ToString().Trim());
                _arr.Add("stock_date", dr["일자"].ToString().Trim());
                _arr.Add("end_price", dr["종가"].ToString().Trim());
                _arr.Add("daebi_gb", dr["전일대비기호"].ToString().Trim());
                _arr.Add("pre_daebi", dr["전일대비"].ToString().Trim());
                _arr.Add("rate", dr["등락율"].ToString().Trim());
                _arr.Add("volume", dr["거래량"].ToString().Trim());
                _arr.Add("gong_volume", dr["공매도량"].ToString().Trim());
                _arr.Add("gong_rate", dr["매매비중"].ToString().Trim());
                _arr.Add("gong_price", dr["공매도거래대금"].ToString().Trim());
                _arr.Add("gong_avg_price", dr["공매도평균가"].ToString().Trim());
                _arrs.Add(_arr);
                if (comboBox1.Text.IndexOf("전체") == -1)
                {
                    break;
                }
                preDate = dr["일자"].ToString().Trim();
            }
        }

        private void StoredOpt10059Price(DataSet ds)
        {
            if (ds == null) return;
            if (ds.Tables.Count == 0) return;
            if (ds.Tables[0].Rows.Count == 0) return;
            string preDate = "";

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (preDate.Equals(dr["일자"].ToString().Trim())) continue;
                _arr.Clear();
                _arr.Add("stock_code", ds.Tables[0].Rows[0]["종목코드"].ToString().Trim());
                _arr.Add("stock_date", dr["일자"].ToString().Trim());
                _arrsDelete.Add(_arr);

                _arr.Clear();
                _arr.Add("stock_code", ds.Tables[0].Rows[0]["종목코드"].ToString().Trim());
                _arr.Add("stock_date", dr["일자"].ToString().Trim());
                _arr.Add("end_price", dr["현재가"].ToString().Trim());
                _arr.Add("daebi_gb", dr["대비기호"].ToString().Trim());
                _arr.Add("pre_daebi", dr["전일대비"].ToString().Trim());
                _arr.Add("rate", dr["등락율"].ToString().Trim());
                _arr.Add("volume_price", dr["누적거래대금"].ToString().Trim());
                _arr.Add("p_tu", dr["개인투자자"].ToString().Trim());
                _arr.Add("f_tu", dr["외국인투자자"].ToString().Trim());
                _arr.Add("k_tu", dr["기관계"].ToString().Trim());
                _arr.Add("k_1", dr["금융투자"].ToString().Trim());
                _arr.Add("k_2", dr["보험"].ToString().Trim());
                _arr.Add("k_3", dr["투신"].ToString().Trim());
                _arr.Add("k_4", dr["기타금융"].ToString().Trim());
                _arr.Add("k_5", dr["은행"].ToString().Trim());
                _arr.Add("k_6", dr["연기금등"].ToString().Trim());
                _arr.Add("k_7", dr["사모펀드"].ToString().Trim());
                _arr.Add("k_8", dr["국가"].ToString().Trim());
                _arr.Add("k_9", dr["기타법인"].ToString().Trim());
                _arr.Add("k_f", dr["내외국인"].ToString().Trim());
                _arrs.Add(_arr);
                if (comboBox1.Text.IndexOf("전체") == -1) { 
                    break;
                }
                preDate = dr["일자"].ToString().Trim();
            }
        }

        private void StoredOpt10081(DataSet ds)
        {
            ArrayParam arr = new ArrayParam();
            ArrayParams arrs = new ArrayParams();
            if (ds.Tables.Count < 1) { return; }
            if (ds.Tables[0].Rows.Count < 1) { return; }

            string 종목코드;
            int 현재가;
            int 거래량;
            int 거래대금;
            string 일자;
            int 시가;
            int 고가;
            int 저가;
            string 수정주가구분;
            string 수정비율;
            string 대업종구분;
            string 소업종구분;
            string 종목정보;
            string 수정주가이벤트;
            string 전일종가;
            string 저가MA;
            string 기간최저가;
            string 기간종가최저가;
            string 최저가MA;
            string 최저가종가MA;
            string 최고저가종가MA;
            DataView dv;
            mySqlDbConn conn = new mySqlDbConn();

            if (ds.Tables[0].Rows[0]["수정주가구분"].ToString().Trim() != "" || comboBox1.Text == "2.전체")
            {
                dv = new DataView(ds.Tables[0]);
                dv.RowFilter = "일자 >= '20160101'";
                dv.Sort = "일자 ASC";
                종목코드 = ds.Tables[0].Rows[0]["종목코드"].ToString().Trim();

                conn.ExecuteNonQuery("delete from stock_day_data_line where stock_code = '" + 종목코드 + "'", null, CommandType.Text);
                conn.ExecuteNonQuery("delete from stock_day_data where stock_code = '" + 종목코드 + "'", null, CommandType.Text);

                arrs.Clear();
                foreach (DataRowView dr in dv)
                {
                    현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                    거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
                    거래대금 = Convert.ToInt32(dr["거래대금"].ToString().Trim());
                    일자 = dr["일자"].ToString().Trim();
                    시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                    고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                    저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                    수정주가구분 = dr["수정주가구분"].ToString().Trim();
                    수정비율 = dr["수정비율"].ToString().Trim();
                    대업종구분 = dr["대업종구분"].ToString().Trim();
                    소업종구분 = dr["소업종구분"].ToString().Trim();
                    종목정보 = dr["종목정보"].ToString().Trim();
                    수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                    전일종가 = dr["전일종가"].ToString().Trim();


                    저가MA = dr["저가MA"].ToString().Trim();
                    기간최저가 = dr["기간최저가"].ToString().Trim();
                    기간종가최저가 = dr["기간종가최저가"].ToString().Trim();
                    최저가MA = dr["최저가MA"].ToString().Trim();
                    최저가종가MA = dr["최저가종가MA"].ToString().Trim();
                    최고저가종가MA = dr["최고저가종가MA"].ToString().Trim();

                    arr.Clear();
                    arr.Add("STOCK_CODE", 종목코드);
                    arr.Add("STOCK_DATE", 일자);
                    arr.Add("END_PRICE", 현재가.ToString().Trim());
                    arr.Add("VOLUME", 거래량.ToString().Trim());
                    arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                    arr.Add("S_PRICE", 시가.ToString().Trim());
                    arr.Add("H_PRICE", 고가.ToString().Trim());
                    arr.Add("L_PRICE", 저가.ToString().Trim());
                    arr.Add("MOD_GUBUN", 수정주가구분);
                    arr.Add("MOD_RATE", 수정비율.ToString().Trim() == "" ? "0.00" : 수정비율.ToString().Trim());
                    arr.Add("CATE_GUBUN1", 대업종구분);
                    arr.Add("CATE_GUBUN2", 소업종구분);
                    arr.Add("STOCK_INFO", 종목정보);
                    arr.Add("MOD_EVENT", 수정주가이벤트);
                    arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                    arr.Add("LOW_MA", 저가MA.ToString().Trim());
                    arr.Add("LOWEST_PERIOD", 기간최저가.ToString().Trim());
                    arr.Add("ENDLOW_PERIOD", 기간종가최저가.ToString().Trim());
                    arr.Add("LOWEST_MA", 최저가MA.ToString().Trim());
                    arr.Add("LOWEND_MA", 최저가종가MA.ToString().Trim());
                    arr.Add("H_LOWEND_MA", 최고저가종가MA.ToString().Trim());
                    arrs.Add(arr);
                }
                conn.ExecuteNonMultiInsert("stock_day_data", arrs);

                return;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                종목코드 = dr["종목코드"].ToString().Trim();
                현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
                거래대금 = Convert.ToInt32(dr["거래대금"].ToString().Trim());
                일자 = dr["일자"].ToString().Trim();
                시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                수정주가구분 = dr["수정주가구분"].ToString().Trim();
                수정비율 = dr["수정비율"].ToString().Trim();
                대업종구분 = dr["대업종구분"].ToString().Trim();
                소업종구분 = dr["소업종구분"].ToString().Trim();
                종목정보 = dr["종목정보"].ToString().Trim();
                수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                전일종가 = dr["전일종가"].ToString().Trim();


                저가MA = dr["저가MA"].ToString().Trim();
                기간최저가 = dr["기간최저가"].ToString().Trim();
                기간종가최저가 = dr["기간종가최저가"].ToString().Trim();
                최저가MA = dr["최저가MA"].ToString().Trim();
                최저가종가MA = dr["최저가종가MA"].ToString().Trim();
                최고저가종가MA = dr["최고저가종가MA"].ToString().Trim();

                _arr.Clear();
                _arr.Add("STOCK_CODE", 종목코드);
                _arr.Add("STOCK_DATE", 일자);
                _arrsDelete.Add(_arr);

                _arr.Clear();
                _arr.Add("STOCK_CODE", 종목코드);
                _arr.Add("STOCK_DATE", 일자);
                _arr.Add("END_PRICE", 현재가.ToString().Trim());
                _arr.Add("VOLUME", 거래량.ToString().Trim());
                _arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                _arr.Add("S_PRICE", 시가.ToString().Trim());
                _arr.Add("H_PRICE", 고가.ToString().Trim());
                _arr.Add("L_PRICE", 저가.ToString().Trim());
                _arr.Add("MOD_GUBUN", 수정주가구분);
                _arr.Add("MOD_RATE", 수정비율.ToString().Trim());
                _arr.Add("CATE_GUBUN1", 대업종구분);
                _arr.Add("CATE_GUBUN2", 소업종구분);
                _arr.Add("STOCK_INFO", 종목정보);
                _arr.Add("MOD_EVENT", 수정주가이벤트);
                _arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                _arr.Add("LOW_MA", 저가MA.ToString().Trim());
                _arr.Add("LOWEST_PERIOD", 기간최저가.ToString().Trim());
                _arr.Add("ENDLOW_PERIOD", 기간종가최저가.ToString().Trim());
                _arr.Add("LOWEST_MA", 최저가MA.ToString().Trim());
                _arr.Add("LOWEND_MA", 최저가종가MA.ToString().Trim());
                _arr.Add("H_LOWEND_MA", 최고저가종가MA.ToString().Trim());
                _arrs.Add(_arr);
                if (comboBox1.Text.IndexOf("전체") == -1)
                {
                    break;
                }
            }
        }

        private void StoredOpt10082(DataSet ds)
        {
            ArrayParam arr = new ArrayParam();
            ArrayParams arrs = new ArrayParams();
            if (ds.Tables.Count < 1) { return; }
            if (ds.Tables[0].Rows.Count < 1) { return; }

            string 종목코드;
            int 현재가;
            Int64 거래량;
            Int64 거래대금;
            string 일자;
            int 시가;
            int 고가;
            int 저가;
            string 수정주가구분;
            string 수정비율;
            string 대업종구분;
            string 소업종구분;
            string 종목정보;
            string 수정주가이벤트;
            string 전일종가;
            DataView dv;
            mySqlDbConn conn = new mySqlDbConn();

            if (ds.Tables[0].Rows[0]["수정주가구분"].ToString().Trim() != "" || comboBox1.Text == "2.전체")
            {
                dv = new DataView(ds.Tables[0]);
                dv.RowFilter = "일자 >= '20160101'";
                dv.Sort = "일자 DESC";
                종목코드 = ds.Tables[0].Rows[0]["종목코드"].ToString().Trim();

                conn.ExecuteNonQuery("delete from stock_week_data where stock_code = '" + 종목코드 + "'", null, CommandType.Text);

                arrs.Clear();
                foreach (DataRowView dr in dv)
                {
                    현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                    거래량 = Convert.ToInt64(dr["거래량"].ToString().Trim());
                    거래대금 = Convert.ToInt64(dr["거래대금"].ToString().Trim());
                    일자 = dr["일자"].ToString().Trim();
                    시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                    고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                    저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                    수정주가구분 = dr["수정주가구분"].ToString().Trim();
                    수정비율 = dr["수정비율"].ToString().Trim();
                    대업종구분 = dr["대업종구분"].ToString().Trim();
                    소업종구분 = dr["소업종구분"].ToString().Trim();
                    종목정보 = dr["종목정보"].ToString().Trim();
                    수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                    전일종가 = dr["전일종가"].ToString().Trim();
                    
                    arr.Clear();
                    arr.Add("STOCK_CODE", 종목코드);
                    arr.Add("STOCK_DATE", 일자);
                    arr.Add("END_PRICE", 현재가.ToString().Trim());
                    arr.Add("VOLUME", 거래량.ToString().Trim());
                    arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                    arr.Add("S_PRICE", 시가.ToString().Trim());
                    arr.Add("H_PRICE", 고가.ToString().Trim());
                    arr.Add("L_PRICE", 저가.ToString().Trim());
                    arr.Add("MOD_GUBUN", 수정주가구분);
                    arr.Add("MOD_RATE", 수정비율.ToString().Trim() == "" ? "0.00" : 수정비율.ToString().Trim());
                    arr.Add("CATE_GUBUN1", 대업종구분);
                    arr.Add("CATE_GUBUN2", 소업종구분);
                    arr.Add("STOCK_INFO", 종목정보);
                    arr.Add("MOD_EVENT", 수정주가이벤트);
                    arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                    arr.Add("DEUNG_DATE", DateTime.Now.ToString("yyyyMMdd"));
                    arrs.Add(arr);
                }
                conn.ExecuteNonMultiInsert("stock_week_data", arrs);
                return;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                종목코드 = dr["종목코드"].ToString().Trim();
                현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                거래량 = Convert.ToInt64(dr["거래량"].ToString().Trim());
                거래대금 = Convert.ToInt64(dr["거래대금"].ToString().Trim());
                일자 = dr["일자"].ToString().Trim();
                시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                수정주가구분 = dr["수정주가구분"].ToString().Trim();
                수정비율 = dr["수정비율"].ToString().Trim();
                대업종구분 = dr["대업종구분"].ToString().Trim();
                소업종구분 = dr["소업종구분"].ToString().Trim();
                종목정보 = dr["종목정보"].ToString().Trim();
                수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                전일종가 = dr["전일종가"].ToString().Trim();

                _arr.Clear();
                _arr.Add("STOCK_CODE", 종목코드);
                _arr.Add("STOCK_DATE", 일자);
                _arrsDelete.Add(_arr);

                _arr.Clear();
                _arr.Add("STOCK_CODE", 종목코드);
                _arr.Add("STOCK_DATE", 일자);
                _arr.Add("END_PRICE", 현재가.ToString().Trim());
                _arr.Add("VOLUME", 거래량.ToString().Trim());
                _arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                _arr.Add("S_PRICE", 시가.ToString().Trim());
                _arr.Add("H_PRICE", 고가.ToString().Trim());
                _arr.Add("L_PRICE", 저가.ToString().Trim());
                _arr.Add("MOD_GUBUN", 수정주가구분);
                _arr.Add("MOD_RATE", 수정비율.ToString().Trim());
                _arr.Add("CATE_GUBUN1", 대업종구분);
                _arr.Add("CATE_GUBUN2", 소업종구분);
                _arr.Add("STOCK_INFO", 종목정보);
                _arr.Add("MOD_EVENT", 수정주가이벤트);
                _arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                _arr.Add("DEUNG_DATE", DateTime.Now.ToString("yyyyMMdd"));
                _arrs.Add(_arr);
                if (comboBox1.Text.IndexOf("전체") == -1)
                {
                    break;
                }
            }
        }
        #endregion

        #region "Form Event"
        private void frmSaver_Load(object sender, EventArgs e)
        {
            InitBrowser();
            tslStatus.Text = "";
            ucMainStockVer2.Connection();
            comboBox1.SelectedIndex = 0;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ucMainStockVer2._allStockDataset == null) return;
                DataView dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
                dv.RowFilter = "STOCK_NAME = '" + txtSearch.Text.Trim() + "'";
                if (dv.Count < 1) return;
                txtSearch.Text = dv[0]["STOCK_CODE"].ToString();
                label1.Text = dv[0]["STOCK_NAME"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteCompanyCEO();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteStockMaster();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteCompanyData();
        }

        private void ucMainStockVer2_OnEventConnect(string status)
        {
            if (_mode == "evening") ExecuteEvening();
            else if (_mode == "morning") ExecuteMorning();
            else if (_mode == "gong") ExecuteEvening(5);
            else if (_mode == "company")
            {
                ExcuteCompanyData();
                ExcuteCompanyCEO();
                Application.Exit();
            }
            else if (_mode == "relate") ExecuteRelateWord();
        }

        private void frmSaverNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeBrowser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecuteEvening();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ExecuteMorning();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ExecuteRelateWord();
        }
        #endregion

        #region "NAVER 연관검색어"
        private async void ExecuteRelateWord()
        {
            DataView dv;

            if (ucMainStockVer2._allStockDataset == null) { return; }
            _browser.FrameLoadEnd -= new EventHandler<CefSharp.FrameLoadEndEventArgs>(browser_FrameLoadEnd);
            _browser.FrameLoadEnd -= new EventHandler<CefSharp.FrameLoadEndEventArgs>(browser_FrameLoadEnd);

            mySqlDbConn conn = new mySqlDbConn();
            //conn.Open();
            //DataSet ds = conn.GetDataTableCommndText("select a.stock_code , a.stock_name from stock_finance a left join stock_relate b on a.stock_code = b.stock_code where b.stock_code is null");
            //conn.Close();
            //dv = new DataView(ds.Tables[0]);
            dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
            if (txtSearch.Text.Trim() != "")
            {
                dv.RowFilter = "STOCK_CODE = '" + txtSearch.Text.Trim() + "' AND " + _FILTERSTR;
            }
            else
            {
                dv.RowFilter = _FILTERSTR;
            }
            dv.Sort = "STOCK_CODE ASC";
            dataGridView1.DataSource = dv;

            for(int i = 0 ; i < dv.Count ; i++) 
            {
                var t = DoAsyncNaverRelation(dv[i]["STOCK_CODE"].ToString(), dv[i]["STOCK_NAME"].ToString());
                if (await Task.WhenAny(t, Task.Delay(5000)) == t){}
                else
                {
                    i--;
                }
            }
            Application.Exit();
        }

        private async Task DoAsyncNaverRelation(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = null;
            DataTable dt = null;
            _browser.FrameLoadEnd += (s, e) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                {
                    return;
                }
                
                if (e.Frame.IsMain)
                {
                    _browser.GetSourceAsync().ContinueWith(taskHtml =>
                    {
                        dt = Cls.ConvertRelateHtml(taskHtml.Result);
                        tcs.SetResult(true);
                    });
                }
            };

            //foreach(DataRowView dr in dv) {
                tcs = new TaskCompletionSource<bool>();
            
                dt = new DataTable();
                string str = System.Web.HttpUtility.UrlEncode(stockName, System.Text.Encoding.UTF8);
                string url = "https://search.naver.com/search.naver?where=nexearch&query=" + str + "&sm=top_hty&fbm=1&ie=utf8";
                if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                {
                    _browser.Load(url);
                }

                await tcs.Task;
                tcs.Task.Dispose();
                tcs = null;
                StoredRelated(stockCode, stockName, dt);

                //if (await Task.WhenAny(tcs.Task, Task.Delay(5000)) == tcs.Task)
                //{
                //    tcs.Task.Dispose();
                //    tcs = null;
                //    StoredRelated(dr["STOCK_CODE"].ToString(), dr["STOCK_NAME"].ToString(), dt);
                //}
                //else
                //{
                //    _browser.Reload();
                //}
            //}
        }

        private void StoredRelated(string stockCode, string stockName, DataTable dt)
        {
            if (dt == null) return;
            if (dt.Rows.Count == 0) return;

            DataTable dtConvert = RemoveDuplicateRows(dt , "RELATE_STR");
            _arrsDelete.Clear();
            _arrs.Clear();

            foreach (DataRow dr in dtConvert.Rows)
            {
                _arr.Clear();
                _arr.Add("STOCK_CODE", stockCode);
                _arr.Add("RELATE_STR", dr["RELATE_STR"].ToString().Trim());
                _arrsDelete.Add(_arr);

                _arr.Clear();
                _arr.Add("STOCK_CODE", stockCode);
                _arr.Add("STOCK_NAME", stockName);
                _arr.Add("RELATE_STR", dr["RELATE_STR"].ToString().Trim());
                _arr.Add("deung_date", DateTime.Now.ToString("yyyyMMdd"));
                _arrs.Add(_arr);
            }
            mySqlDbConn conn = new mySqlDbConn();
            conn.ExecuteNonMultiDelete("stock_relate" ,_arrsDelete);
            conn.ExecuteNonMultiInsert("stock_relate" , _arrs);
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }
        #endregion

    }
}
