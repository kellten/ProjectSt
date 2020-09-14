﻿// Copyright © 2010-2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Text;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using VB6 = Microsoft.VisualBasic;
using System.Globalization;
using PaikRichStock.Common;
using System.Collections;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.Internals;
using System.Threading.Tasks;
using System.Threading;

namespace NewsFinder
{
    public partial class frmMainVer2 : Form
    {
        private bool _isInit = false;
        //private DataSet _ds뉴스 = new DataSet();
        private DataSet _ds조건체결 = new DataSet();
        //private DataTable _dt보유잔고 = new DataTable();
        //private DataTable _dt미체결 = new DataTable();
        private DataSet _dsTick60 = new DataSet();
        private DataSet _ds전체일봉 = new DataSet();
        private DataSet _ds전체재정 = new DataSet();
        private DataSet _ds전체매동 = new DataSet();
        private DataSet _ds익절 = new DataSet();

        //private string[] _뉴스 = { "현재가", "등락율", "체결강도", "매수거래량", "매도거래량", "매수거래비용", "매도거래비용", "거래시간" };
        private string[] _체결 = { "체결시간", "현재가", "전일대비", "등락율", "거래량", "누적거래량", "누적거래대금", "시가", "고가", "저가", "전일거래량대비", "거래비용", "체결강도", "상한가발생시간", "최고거래량", "최저거래량", "최고체결강도", "최저체결강도" };
        private string[,] _Tick60 = { 
                                    { "일자", "System.String" }, { "현재가", "System.Int32" }, 
                                    { "BBUP", "System.Double" }, { "BBDOWN", "System.Double" }, 
                                    { "COUNT", "System.Int32" }, 
                                    { "매수유무", "System.Int32" }, { "매도유무", "System.Int32" },
                                    { "매수신호", "System.Int32" }, { "매도신호", "System.Int32" }, 
                                    { "시가", "System.Int32" }, { "고가", "System.Int32" }, { "저가", "System.Int32" }, 
                                    { "등락율", "System.Decimal" }, { "체결강도", "System.Decimal" }, { "매수거래량", "System.Int32" }, { "매도거래량", "System.Int32" }, 
                                    { "매수거래비용", "System.Int32" }, { "매도거래비용", "System.Int32" }, 
                                    { "시작시간", "System.String" }, { "종료시간", "System.String" }, 
                                    { "LINE5", "System.Decimal" }, { "LINE10", "System.Decimal" }, { "LINE20", "System.Decimal" }, { "LINE40", "System.Decimal" }, { "LINE60", "System.Decimal" } };
        private DataAccess _DataAcc;
        private DataTable _dt화면관리 = new DataTable();
        private DataTable _dtDart = new DataTable();

        private clsKiwoomBaseInfo _clsKiwoomBaseInfo = new clsKiwoomBaseInfo();
        private int _SLEEP_TIME = 200;

        public frmMainVer2()
        {

            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            WindowState = FormWindowState.Maximized;
            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            ResizeBegin += (s, e) => SuspendLayout();
            ResizeEnd += (s, e) => ResumeLayout(true);

            _dt화면관리.Columns.Add("화면구분"); //1.뉴스, 2.잔고 , 3.조건
            _dt화면관리.Columns.Add("실시간구분"); //1.실시간 2.정적
            _dt화면관리.Columns.Add("종목코드"); //종목코드
            //_dt화면관리.Columns.Add("화면번호"); //화면번호

            _dtDart.Columns.Add("시간");
            _dtDart.Columns.Add("종목코드");
            _dtDart.Columns.Add("종목명");
            _dtDart.Columns.Add("제목");
            _dtDart.Columns.Add("주소");

            //for (int i = 0; i < _미체결.Length / 2; i++)
            //{
            //    _dt미체결.Columns.Add(_미체결[i, 0], Type.GetType(_미체결[i, 1]));
            //}

            _DataAcc = new DataAccess();
        }

        // 화면번호 생산
        private int _scrNum = 7000;
        private int _scrNumOrder = 8000;
        private string GetScrNum()
        {
            if (_scrNum < 8000)
                _scrNum++;
            else
                _scrNum = 7000;

            return _scrNum.ToString();
        }

        private string GetScrNumOrder()
        {
            if (_scrNumOrder < 8999)
                _scrNumOrder++;
            else
                _scrNumOrder = 8000;

            return _scrNumOrder.ToString();
        }


        ////private void button5_Click(object sender, EventArgs e)
        ////{
        ////    browser.Load("https://www.cowboom.com/checkout/cart.cfm?uiID=16");
        ////}

        private void btnKeyWord_Click(object sender, EventArgs e)
        {
            lst검색어.Items.Add(txtKeyWord.Text.Trim());
        }

        private int _idx1;
        private int _idx2;

        private void tmrDaum_Tick(object sender, EventArgs e)
        {
            try
            {
                GetDaNews();
            }
            catch (Exception ex) { Logger("에러 (tmrDaum)", ex.Message); }
            finally
            {
                if (tmrDaum.Enabled == false)
                    tmrDaum.Enabled = true;
            }
            return;
        }

        private void lst검색어_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lst검색어.SelectedIndex == -1) return;
                lst검색어.Items.RemoveAt(lst검색어.SelectedIndex);
            }
        }

        private void txtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lst검색어.Items.Add(txtKeyWord.Text.Trim());
                txtKeyWord.Text = "";
                txtKeyWord.Focus();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                System.Diagnostics.Process.Start(dgNews.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());
            }
        }

        private void GetNaBlogNews(string keyword)
        {
            DataTable dt = new DataTable();

            if (keyword.IndexOf("시황") > -1 ||
                keyword.IndexOf("특징") > -1 ||
                keyword.IndexOf("마감") > -1 ||
                keyword.IndexOf("관심") > -1 ||
                keyword.IndexOf("추천") > -1 ||
                keyword.IndexOf("분석") > -1 ||
                keyword.IndexOf("아침") > -1 ||
                keyword.IndexOf("장전") > -1 ||
                keyword.IndexOf("장후") > -1 ||
                keyword.IndexOf("전략") > -1)
            {
            }
            else
            {
                keyword = keyword + " 종목분석";
            }

            string query = System.Web.HttpUtility.UrlEncode(keyword);

            for (int i = 1; i <= 10; i++)
            {
                String apiURL = "https://openapi.naver.com/v1/search/cafearticle.xml?query=" + query + "&start=" + i.ToString() + "&display=100&sort=date";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);

                request.Method = "GET";
                request.Headers.Add("X-Naver-Client-Id", "MbZd5RTwx737EUZkTS4R"); //시영
                request.Headers.Add("X-Naver-Client-Secret", "E7u5wgKVW2"); //시영

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader1 = new StreamReader(response.GetResponseStream());

                string page = reader1.ReadToEnd();
                System.Data.DataSet ds = new System.Data.DataSet();

                StringReader sReader = new StringReader(page);

                System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

                ds.ReadXml(reader);

                dt = ds.Tables["ITEM"].Clone();
                foreach (System.Data.DataRow dr in ds.Tables["ITEM"].Rows)
                {
                    dt.Rows.Add(dr.ItemArray);
                }
            }

            for (int i = 1; i <= 10; i++)
            {
                String apiURL = "https://openapi.naver.com/v1/search/blog.xml?query=" + query + "&start=" + i.ToString() + "&display=100&sort=date";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);

                request.Method = "GET";
                request.Headers.Add("X-Naver-Client-Id", "MbZd5RTwx737EUZkTS4R"); //시영
                request.Headers.Add("X-Naver-Client-Secret", "E7u5wgKVW2"); //시영
                //request.Headers.Add("X-Naver-Client-Id", "lLk8wyiPEClhz4iSE5uS");
                //request.Headers.Add("X-Naver-Client-Secret", "AO5aMsIpja");

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader1 = new StreamReader(response.GetResponseStream());

                string page = reader1.ReadToEnd();
                System.Data.DataSet ds = new System.Data.DataSet();

                StringReader sReader = new StringReader(page);

                System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

                ds.ReadXml(reader);

                foreach (System.Data.DataRow dr in ds.Tables["ITEM"].Rows)
                {
                    dt.Rows.Add(dr.ItemArray);
                }
            }
        }

        private void GetNaNews()
        {
            DataSet ds;
            if (lst검색어.Items.Count == 0) return;

            string itemText = "";
            int idx = _idx1 % lst검색어.Items.Count;
            try
            {
                //itemText = System.Web.HttpUtility.UrlEncode(lst검색어.Items[_idx1].ToString().Trim(), System.Text.Encoding.UTF8);
                itemText = lst검색어.Items[idx].ToString().Trim();
            }
            catch
            {
                return;
            }
            finally
            {
                //this.Text = "RichStock - " + itemText;
                _idx1++;
                if (_idx1 > 1000) _idx1 = 0;
            }

            ds = Cls.NaverNews(itemText , 1);
            if (ds == null) return;
            DataView dv = new DataView(ds.Tables["item"]);
            dv.RowFilter = "ORIGINALLINK NOT LIKE '%star.mt.co.kr%'";
            bool blnTrue = false;
            foreach (System.Data.DataRowView dr in dv)
            {
                blnTrue = false;
                for (int row = 0; row < dgTotalNews.Rows.Count - 1; row++)
                {
                    if (dgTotalNews.Rows[row].Cells[T주소.Index].Value == null) { blnTrue = false; break; }
                    if (dgTotalNews.Rows[row].Cells[T주소.Index].Value.Equals(dr["ORIGINALLINK"].ToString().Trim()) ||
                        dgTotalNews.Rows[row].Cells[T제목.Index].Value.Equals(Cls.HtmlToPlainText(dr["TITLE"].ToString().Trim()))
                        )
                    {
                        blnTrue = true;
                        break;
                    }
                }

                if (blnTrue) continue;
                if (dgTotalNews.Rows.Count >= 100) dgTotalNews.Rows.RemoveAt(dgTotalNews.Rows.Count - 1);
                dgTotalNews.Rows.Insert(0, 1);
                dgTotalNews.Rows[0].Cells[T언론사.Index].Value = "네이버";
                dgTotalNews.Rows[0].Cells[T시간.Index].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //dgTotalNews.Rows[0].Cells["N키워드"].Value = lst검색어.Items[idx].ToString().Trim();
                dgTotalNews.Rows[0].Cells[T제목.Index].Value = Cls.HtmlToPlainText(dr["TITLE"].ToString().Trim());
                dgTotalNews.Rows[0].Cells[T주소.Index].Value = dr["ORIGINALLINK"].ToString().Trim();
                dgTotalNews.Rows[0].Cells[T종목.Index].Value = GetNewsStockInfo(dgTotalNews.Rows[0].Cells[T제목.Index].Value.ToString().Replace("[", " ").Replace("]", " ").Replace("(", " ").Replace(")", " ").Replace(",", " ").Replace("'", " ").Replace("\"", " "));
                break;
            }
        }

        private void GetDaNews()
        {
            tmrDaum.Enabled = false;
            if (lst검색어.Items.Count == 0) return;

            DataSet ds;

            string itemText = "";
            int idx = _idx2 % lst검색어.Items.Count;
            try
            {
                //itemText = System.Web.HttpUtility.UrlEncode(lst검색어.Items[_idx1].ToString().Trim(), System.Text.Encoding.UTF8);
                itemText = lst검색어.Items[idx].ToString().Trim();
            }
            catch
            {
                return;
            }
            finally
            {
                _idx2++;
                if (_idx1 > 1000) _idx2 = 0;
            }

            ds = DaumNews(itemText);

            try
            {
                DataView dv = new DataView(ds.Tables["item"]);
                bool blnTrue = false;
                foreach (System.Data.DataRowView dr in dv)
                {
                    blnTrue = false;
                    for (int row = 0; row < dgNews.Rows.Count - 1; row++)
                    {
                        if (dgNews.Rows[row].Cells["N제목"].Value.ToString().Trim() == Cls.HtmlToPlainText(dr["제목"].ToString().Trim()))
                        {
                            blnTrue = true;
                            break;
                        }
                    }

                    if (blnTrue == true) continue;

                    if (dgNews.Columns.Count < 1) return;
                    dgNews.Rows.Insert(0, 1);

                    dgNews.Rows[0].Cells["N시간"].Value = DateTime.Now;
                    dgNews.Rows[0].Cells["N키워드"].Value = dr["키워드"].ToString().Trim();
                    dgNews.Rows[0].Cells["N제목"].Value = Cls.HtmlToPlainText(dr["제목"].ToString().Trim());
                    dgNews.Rows[0].Cells["N주소"].Value = dr["주소"].ToString().Trim();
                }
            }
            finally
            {
                tmrDaum.Enabled = true;
            }
        }

        private void GetDart()
        {
            bool 팝업필터 = false;
            DataView dv;
            DataSet ds = Cls.Dart();
            dv = new DataView(ds.Tables["list"]);
            dv.RowFilter = "crp_cls in ('Y' , 'K')";
            foreach (System.Data.DataRowView dr in dv)
            {
                Boolean blnTrue = false;
                for (int i = 0; i < dgDart.Rows.Count; i++)
                {

                    if (dgDart.Rows[i].Cells["제목"].Value == null) break;
                    string tmpStr = dgDart.Rows[i].Cells["제목"].Value.ToString();
                    if (tmpStr == "") break;
                    if (tmpStr.IndexOf("매출액 대비") > -1)
                    {
                        tmpStr = dgDart.Rows[i].Cells["제목"].Value.ToString().Replace("-자동매수", "").Substring(0, dgDart.Rows[i].Cells["제목"].Value.ToString().Replace("-자동매수", "").Length - 7);
                    }
                    else
                    {
                        tmpStr = dgDart.Rows[i].Cells["제목"].Value.ToString().Replace("-자동매수", "");
                    }

                    if (tmpStr.Replace("(3자배정)", "").Replace("-매출액 대비:", "") == dr["rpt_nm"].ToString().Trim())
                    {
                        blnTrue = true;
                        break;
                    }
                }

                if (blnTrue == false)
                {
                    dgDart.Rows.Insert(0, 1);

                    dgDart.Rows[0].Cells["시간"].Value = dr["rcp_dt"].ToString().Trim() + " " + DateTime.Now.ToString("HH:mm:ss.") + DateTime.Now.Millisecond;
                    dgDart.Rows[0].Cells["종목"].Value = dr["crp_nm"].ToString().Trim();
                    dgDart.Rows[0].Cells["제목"].Value = dr["rpt_nm"].ToString().Trim();
                    dgDart.Rows[0].Cells["주소"].Value = "http://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + dr["rcp_no"].ToString().Trim();

                    ////if (dgDart.Rows[0].Cells["제목"].Value.ToString().Trim().IndexOf("유상증자") > -1 && dgDart.Rows[0].Cells["제목"].Value.ToString().Trim().IndexOf("자율공시") < 0)
                    ////{
                    ////    string html = "";

                    ////    html = GetReport(dgDart.Rows[0].Cells["주소"].Value.ToString().Trim());

                    ////    try
                    ////    {
                    ////        if (html.IndexOf("3자배정") > -1 && html.IndexOf("자율") < 0)
                    ////        {
                    ////            dgDart.Rows[0].Cells["제목"].Value = dgDart.Rows[0].Cells["제목"].Value.ToString().Trim() + "(3자배정)" + "-자동매수";
                    ////        }
                    ////    }
                    ////    catch
                    ////    {
                    ////    }
                    ////}
                    ////else if (dgDart.Rows[0].Cells["제목"].Value.ToString().Trim().IndexOf("단일판매ㆍ공급계약체결") > -1 && dgDart.Rows[0].Cells["제목"].Value.ToString().Trim().IndexOf("기재정정") < 0)
                    ////{
                    ////    string html = GetReport(dgDart.Rows[0].Cells["주소"].Value.ToString().Trim());
                    ////    try
                    ////    {

                    ////        if (html.IndexOf("매출액 대비") > -1 || html.IndexOf("매출액대비") > -1)
                    ////        {
                    ////            int index1 = 0;
                    ////            int index2 = 0;
                    ////            int index3 = 0;
                    ////            int index4 = 0;
                    ////            if (html.IndexOf("매출액대비") > -1)
                    ////            {
                    ////                index1 = html.IndexOf("매출액대비");
                    ////            }
                    ////            else if (html.IndexOf("매출액 대비") > -1)
                    ////            {
                    ////                index1 = html.IndexOf("매출액 대비");
                    ////            }

                    ////            index2 = html.IndexOf("xforms_input", index1);
                    ////            index3 = html.IndexOf(">", index2);
                    ////            index4 = html.IndexOf("<", index3);

                    ////            int leng = 7 - html.Substring(index3 + 1, index4 - index3 - 1).Length;
                    ////            string tmpStr1 = html.Substring(index3 + 1, index4 - index3 - 1);
                    ////            double tmpCnt = Cls.Val(html.Substring(index3 + 1, index4 - index3 - 1));
                    ////            for (int i = 0; i < leng; i++)
                    ////            {
                    ////                tmpStr1 = " " + tmpStr1;
                    ////            }

                    ////            if (tmpCnt > 15)
                    ////            {
                    ////                tmpStr1 = tmpStr1 + "-자동매수";
                    ////            }

                    ////            dgDart.Rows[0].Cells["제목"].Value = dgDart.Rows[0].Cells["제목"].Value + "-매출액 대비:" + tmpStr1;

                    ////        }
                    ////    }
                    ////    catch
                    ////    {
                    ////    }
                    ////}
                    팝업필터 = false;
                    foreach (string str2 in lst팝업필터.Items)
                    {
                        if (dgDart.Rows[0].Cells["제목"].Value.ToString().IndexOf(str2) > -1)
                        {
                            팝업필터 = true;
                            break;
                        }
                    }

                    if (팝업필터 == true) { break; }

                    bool is자동매수 = false;
                    foreach (string str1 in lsb자동매수단어.Items)
                    {
                        if (dgDart.Rows[0].Cells["제목"].Value.ToString().IndexOf(str1) > -1)
                        {

                            AutoBuy(dgDart.Rows[0].Cells["종목"].Value.ToString().Trim(), dgDart.Rows[0].Cells["제목"].Value.ToString().Trim());
                            is자동매수 = true;
                            break;
                        }
                    }

                    if (is자동매수 == true)
                    {
                        string oStr = "";
                        oStr = dgDart.Rows[0].Cells["종목"].Value.ToString() + "-" + dgDart.Rows[0].Cells["제목"].Value.ToString() + " " + dgDart.Rows[0].Cells["주소"].Value.ToString();
                        //_frm.UpdateNoti(oStr);
                        //_frm.Visible = true;
                        //_frm.TopMost = true;
                        //System.Windows.Forms.Clipboard.SetText(dgDart.Rows[0].Cells["종목"].Value.ToString().Trim());
                        break;
                    }
                }
            }
        }

        private void tmrNaver_Tick(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) return;
            GetNews1(); 
            GetNews2(); 
            GetNews3(); 
            GetNews4();
            GetNews5();
            GetNews6();
            GetNews7();
            GetNews8();
            GetNews9();
            GetNews10();
            GetNews11();
            GetNews12();
            GetNews13();
            GetNaNews();
            //try
            //{
            //    GetNaNews();
            //}
            //catch (Exception ex) { Logger("에러 (tmrNaver)", ex.Message); }
            //finally
            //{
            //    if (tmrNaver.Enabled == false)
            //        tmrNaver.Enabled = true;
            //}
            //return;
        }


        private void GetGong()
        {
            GetGongData();
            return;
        }

        private void Logger(string gubun, string msg)
        {
            //if (btn로그실행.Text == "▶") return;
            //if (dgLog.Rows.Count > 50)
            //{
            //    dgLog.Rows.RemoveAt(dgLog.Rows.Count - 1);
            //}
            //dgLog.Rows.Insert(0, 1);
            //dgLog.Rows[0].Cells[0].Value = gubun;
            //dgLog.Rows[0].Cells[1].Value = msg;
        }

        //자동단어에 등록된 공시가 났을경우 타는 함수 - S
        public void AutoBuy(string StockName, string title)
        {
            //Decimal 매출액대비Per = 0;
            //if (title.IndexOf("매출액 대비") > -1)
            //{
            //    매출액대비Per = Cls.Val(title.Replace("-자동매수", "").Substring(title.IndexOf("대비:") + 3));
            //}

            if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:20:00")))
            {
                if (title.IndexOf("3자배정") < 0) return;
            }

            if (ucMainStockVer2._allStockDataset == null) { return; }
            if (ucMainStockVer2._allStockDataset.Tables.Count == 0) { return; }
            DataView dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
            dv.RowFilter = "STOCK_NAME = '" + StockName + "'";

            if (dv.Count == 0) { return; }

            DataRow drDart = _dtDart.NewRow();

            foreach (DataRowView dr in dv)
            {
                if (chk자동매매.Checked == true)
                {
                    //drDart["시간"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //drDart["종목코드"] = dr["STOCK_CODE"].ToString().Trim();
                    //drDart["종목명"] = dr["STOCK_NAME"].ToString().Trim();
                    //drDart["제목"] = title;
                    //_dtDart.Rows.Add(drDart);

                    if (chk공.Checked == true)
                    {
                        NewsFavAdd( dr["STOCK_CODE"].ToString().Trim() , "" , "" , "공" , 0 , 0 , 0 , 0);
                        SetDsScreenNo("A", "1", "1", dr["STOCK_CODE"].ToString().Trim(), dr["STOCK_NAME"].ToString().Trim(), true);

                        //if (_ds뉴스.Tables[dr["STOCK_CODE"].ToString().Trim()] == null)
                        //{
                        //    DataTable dt = _ds뉴스.Tables.Add(dr["STOCK_CODE"].ToString().Trim());
                        //    foreach (string str in _뉴스)
                        //    {
                        //        dt.Columns.Add(str);
                        //    }
                        //}
                    }
                }
                //뉴스 관종에 등록 - E
            }
        }
        //자동단어에 등록된 공시가 났을경우 타는 함수 - S

        //모든 주문이 이루어지는 함수 - S
        private void SendBuySellMsg(string 종목코드, string 거래구분, int 매매구분, int 호가단위, int 현재가, int 수량, string 화면구분)
        {
            SendBuySellMsg(종목코드, 거래구분, 매매구분, 호가단위, 현재가, 수량, 화면구분, "");
        }

        private void SendBuySellMsg(string 종목코드, string 거래구분, int 매매구분, int 호가단위, int 현재가, int 수량, string 화면구분, string 주문번호)
        {
            // =================================================
            // 거래구분 취득
            // 00:지정가, 03:시장가, 05:조건부지정가, 06:최유리지정가, 07:최우선지정가,
            // 10:지정가IOC, 13:시장가IOC, 16:최유리IOC, 20:지정가FOK, 23:시장가FOK,
            // 26:최유리FOK, 61:장개시전시간외, 62:시간외단일가매매, 81:시간외종가

            // =================================================
            // 매매구분 취득
            // (1:신규매수, 2:신규매도 3:매수취소, 
            // 4:매도취소, 5:매수정정, 6:매도정정)

            string stockName = ucMainStockVer2.GetStockInfo(종목코드);
            ucMainStock.OrderType 매매Type = (ucMainStock.OrderType)매매구분;

            int 호가단위금액 = 0;
            int 주문금액 = 현재가;

            if (거래구분 != "03" && 현재가 != 0)
            {
                if (현재가 < 1000) 호가단위금액 = 1;
                else if (현재가 >= 1000 && 현재가 < 5000) 호가단위금액 = 5;
                else if (현재가 >= 5000 && 현재가 < 10000) 호가단위금액 = 10;
                else if (현재가 >= 10000 && 현재가 < 50000) 호가단위금액 = 50;
                else if (현재가 >= 50000 && 현재가 < 100000) 호가단위금액 = 100;
                else if (현재가 >= 100000 && 현재가 < 500000) 호가단위금액 = 500;
                else if (현재가 >= 500000) 호가단위금액 = 1000;

                if (매매구분 == 1)
                {
                    주문금액 = 현재가 - (호가단위 * 호가단위금액);
                }
                else if (매매구분 == 2)
                {
                    주문금액 = 현재가 + (호가단위 * 호가단위금액);
                }
            }

            string scrNum = GetScrNumOrder();
            int tickRow = -1;
            if (_dsTick60.Tables[종목코드] != null) tickRow = _dsTick60.Tables[종목코드].Rows.Count - 1;

            var objMi = (from DataGridViewRow dgr in dg미체결.Rows
                     where dgr.Cells[M종목코드.Index].Value != null && dgr.Cells[M종목코드.Index].Value.Equals(종목코드)
                     select dgr).FirstOrDefault();

            if (매매Type == ucMainStock.OrderType.신규매수)
            {
                if (tickRow != -1)
                {
                    _dsTick60.Tables[종목코드].Rows[tickRow]["매수유무"] = "1";
                }
                ucMainStockVer2.SendOrder_OnReceiveChejanData("매수", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.신규매수,
                   stockName, 종목코드, 수량, 거래구분 == "03" || 거래구분 == "61" || 거래구분 == "81" ? 0 : 주문금액, 거래구분, "");
            }
            else if (매매Type == ucMainStock.OrderType.신규매도)
            {
                if (tickRow != -1)
                {
                    _dsTick60.Tables[종목코드].Rows[tickRow]["매도유무"] = "1";
                }
                ucMainStockVer2.SendOrder_OnReceiveChejanData("매도", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.신규매도,
                   stockName, 종목코드, 수량, 거래구분 == "03" || 거래구분 == "61" || 거래구분 == "81" ? 0 : 주문금액, 거래구분, "");
            }
            else if (매매Type == ucMainStock.OrderType.매수취소)
            {
                if (objMi != null)
                {
                    ucMainStockVer2.SendOrder_OnReceiveChejanData("매수취소", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.매수취소,
                        stockName, 종목코드, 수량, 0, 거래구분, 주문번호);
                }

            }
            else if (매매Type == ucMainStock.OrderType.매도취소)
            {
                if (objMi != null)
                {
                    ucMainStockVer2.SendOrder_OnReceiveChejanData("매도취소", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.매도취소,
                       stockName, 종목코드, 수량, 0, 거래구분, 주문번호);
                }
            }
            else if (매매Type == ucMainStock.OrderType.매수정정)
            {
                if (objMi != null)
                {
                    ucMainStockVer2.SendOrder_OnReceiveChejanData("매수정정", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.매수정정,
                       stockName, 종목코드, 수량, 거래구분 == "03" || 거래구분 == "61" || 거래구분 == "81" ? 0 : 주문금액, 거래구분, 주문번호);
                }
            }
            else if (매매Type == ucMainStock.OrderType.매도정정)
            {
                if (objMi != null)
                {
                    ucMainStockVer2.SendOrder_OnReceiveChejanData("매도정정", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.매도정정,
                       stockName, 종목코드, 수량, 거래구분 == "03" || 거래구분 == "61" || 거래구분 == "81" ? 0 : 주문금액, 거래구분, 주문번호);
                }
            }
        }
        //모든 주문이 이루어지는 함수 - E

        public static string GetResponse(string url)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

            HttpWebResponse response = (request.GetResponse() as HttpWebResponse);

            StreamReader reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }

        public static string GetResponse(string url, Encoding Encode)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;

            HttpWebResponse response = (request.GetResponse() as HttpWebResponse);

            StreamReader reader = new StreamReader(response.GetResponseStream(), Encode);
            return reader.ReadToEnd();
        }

        private void tmrDart_Tick(object sender, EventArgs e)
        {
            if (!_isInit) return;
            if (Cls.Val(DateTime.Now.ToString("HHmmss")) < 093000 && !cboAccount.Text.Equals("5116998410") && !System.Environment.MachineName.Equals("EDPB2F012")) return;
            try
            {
                GetGong();

                if (dgDart.RowCount > 50) { dgDart.Rows.RemoveAt(dgDart.Rows.Count - 1); }
            }
            catch (Exception ex) { Logger("에러 (tmrDart)", ex.Message); }
        }

        private void tmrDartKrx_Tick(object sender, EventArgs e)
        {
            if (!_isInit) return;
            if (Cls.Val(DateTime.Now.ToString("HHmmss")) < 093000 && !cboAccount.Text.Equals("5116998410") && !System.Environment.MachineName.Equals("EDPB2F012")) return;
            try
            {
                GetGongDataKrx();

                if (dgDartNew.RowCount > 50) { dgDartNew.Rows.RemoveAt(dgDartNew.Rows.Count - 1); }
            }
            catch (Exception ex) { Logger("에러 (tmrDart)", ex.Message); }
        }

        public async void GetNews1() //매일경제
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("euc-kr");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://file.mk.co.kr/news/rss/rss_40300001.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews2() //파이낸셜뉴스
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://www.fnnews.com/rss/new/fn_realnews_all.xml;jsessionid=6E8E7F1A12AF8EFE944C5A9964D5B93D");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews3() //이데일리
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://rss.edaily.co.kr/edaily_news.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews4() //경향신문
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://www.khan.co.kr/rss/rssdata/total_news.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews5() //동아일보
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://rss.donga.com/total.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews6() //세계일보
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("euc-kr");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://rss.segye.com/segye_recent.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews7() //아이뉴스
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("euc-kr");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://www.inews24.com/rss/news_inews.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews8() //오마이뉴스
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://rss.ohmynews.com/rss/ohmynews.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews9() //한국일보
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("euc-kr");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://rss.hankooki.com/daily/dh_main.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews10() //헤럴드경제
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://biz.heraldcorp.com/common_prog/rssdisp.php?ct=010000000000.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }
        
        public async void GetNews11() //노컷뉴스
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://rss.nocutnews.co.kr/nocutnews.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews12() //머니투데이
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("euc-kr");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://rss.mt.co.kr/mt_news.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }

        public async void GetNews13() //뉴스토마토-증권
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.Encoding.GetEncoding("utf-8");
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompNews);
                    await wc.DownloadStringTaskAsync("http://www.newstomato.com/rss/?cate=12 ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (WcDownLoadCompNews1)", ex.ToString());
            }
        }
        
        public async void GetGongData()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.UTF8Encoding.UTF8;
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadComp);
                    await wc.DownloadStringTaskAsync("http://dart.fss.or.kr/api/todayRSS.xml");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (GetGongData)", ex.ToString());
            }

        }

        public async void GetGongDataKrx()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.UTF8Encoding.UTF8;
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompKrx);
                    await wc.DownloadStringTaskAsync("http://kind.krx.co.kr/disclosure/rsstodaydistribute.do?method=searchRssTodayDistribute&repIsuSrtCd=&mktTpCd=0&searchCorpName=&currentPageSize=1");
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (GetGongDataKrx)", ex.ToString());
            }

        }

        public async void GetDartDataApi()
        {
            try
            {
                using (WebClient wc = new WebClient())
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
                    else
                    {
                        authKey = "db3e22ead89d0449f95fbbf966969cc41e6cf943"; // 종근
                    }

                    String apiURL = "http://dart.fss.or.kr/api/search.xml?auth=" + authKey + "&page_set=10";

                    wc.Encoding = System.Text.UTF8Encoding.UTF8;
                    await wc.DownloadStringTaskAsync(apiURL);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (GetGongDataApi)", ex.ToString());
            }

        }

        void WcDownLoadCompNews(object sender, DownloadStringCompletedEventArgs e) 
        {
            try
            {
                StringReader sr = new StringReader(e.Result);
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(sr);
                sr.Close();
                System.Xml.XmlNodeList forecastNodes = doc.SelectNodes("rss/channel/item");
                foreach (System.Xml.XmlNode node in forecastNodes)
                {
                    Boolean blnTrue = false;
                    for (int i = 0; i < dgTotalNews.Rows.Count; i++)
                    {
                        string tmpStr = dgTotalNews.Rows[i].Cells[T제목.Index].Value.ToString().Trim();
                        if (tmpStr.Equals(node["title"].InnerText.Trim()))
                        {
                            blnTrue = true;
                            break;
                        }
                    }
                    if (!blnTrue)
                    {
                        //if (dgTotalNews.Rows.Count >= 100) dgTotalNews.Rows.RemoveAt(dgTotalNews.Rows.Count - 1);
                        string company = "";
                        string date = "";
                        if (node["link"].InnerText.IndexOf("mk.co.kr") > -1) { company = "매일경제"; date = node["pubDate"].InnerText;}
                        if (node["link"].InnerText.IndexOf("fnnews.com") > -1) { company = "파이낸셜"; date = node["atom:updated"].InnerText; }
                        if (node["link"].InnerText.IndexOf("edaily.co.kr") > -1) { company = "이데일리"; date = node["pubDate"].InnerText; }
                        if (node["link"].InnerText.IndexOf("khan.co.kr") > -1) { company = "경향신문"; date = node["dc:date"].InnerText; }
                        if (node["link"].InnerText.IndexOf("donga.com") > -1) { company = "동아일보"; date = node["pubDate"].InnerText; }
                        if (node["link"].InnerText.IndexOf("segye.com") > -1) { company = "세계일보"; date = node["pubDate"].InnerText; }
                        if (node["link"].InnerText.IndexOf("inews24.com") > -1) { company = "아이뉴스"; date = node["pubDate"].InnerText; }
                        if (node["link"].InnerText.IndexOf("ohmynews.com") > -1) { company = "오마이뉴스"; date = node["pubDate"].InnerText; }
                        if (node["link"].InnerText.IndexOf("hankooki.com") > -1) { company = "한국일보"; date = node["pubDate"].InnerText; }
                        if (node["link"].InnerText.IndexOf("koreaherald.com") > -1) { company = "헤럴드경제"; date = node["dc:date"].InnerText; }
                        if (node["link"].InnerText.IndexOf("nocutnews.co.kr") > -1) { company = "노컷뉴스"; date = node["dc:date"].InnerText.Split(',')[1].Trim().Replace("UTC", "").Replace("GMT", ""); }
                        if (node["link"].InnerText.IndexOf("mt.co.kr") > -1) { company = "머니투데이"; date = CDateTime.FormatDate(Cls.Left(node["pubDate"].InnerText.Split('+')[0].Trim(), 8) , "-") + " " + CDateTime.FormatTime(node["pubDate"].InnerText.Split('+')[0].Substring(8), ":"); }
                        if (node["link"].InnerText.IndexOf("newstomato.com") > -1) { company = "뉴스토마토"; date = node["pubDate"].InnerText; }
                        
                        string stockName = GetNewsStockInfo(node["title"].InnerText.Replace("[", " ").Replace("]", " ").Replace("(", " ").Replace(")", " ").Replace(",", " ").Replace("'", " ").Replace("\"", " ")).Trim();
                        if (stockName.Equals("")) break;

                        dgTotalNews.Rows.Insert(0, 1);
                        DataGridViewRow dgr = dgTotalNews.Rows[0];

                        dgr.Cells[T언론사.Index].Value = company.Trim();
                        //dgr.Cells[T시간.Index].Value = DateTime.Parse(date , IFormatProvider "yyyy-MM-dd HH:mm:ss");
                        try
                        {
                            dgr.Cells[T시간.Index].Value = DateTime.Parse(date).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        catch {
                            dgr.Cells[T시간.Index].Value = date.ToString();
                        }
                        dgr.Cells[T제목.Index].Value = node["title"].InnerText.Trim();
                        dgr.Cells[T주소.Index].Value = node["link"].InnerText.Trim();
                        dgr.Cells[T종목.Index].Value = stockName;
                        if (!dgr.Cells[T종목.Index].Value.Equals(""))
                        {
                            string stockCode = Cls.Right(dgr.Cells[T종목.Index].Value.ToString().Trim(),6);
                            NewsFavAdd(stockCode, "", "", "공", 0, 0, 0, 0);
                        }

                    }
                    break;
                }
                dgTotalNews.Sort(dgTotalNews.Columns[1], System.ComponentModel.ListSortDirection.Descending);
            }
            catch (Exception ex)
            {

                Logger("에러 (WcDownLoadComp)", ex.ToString());
            }
        }
        public string GetNewsStockInfo(string title)
        {
            string str = "";
            string[] words = title.Split(' ');

            foreach (string objStr in words)
            {
                if (objStr.Trim().Equals("")) continue;
                DataRow[] arr = ucMainStockVer2._allStockDataset.Tables[0].Select("STOCK_NAME = '" + objStr +"'");
                
                if (arr.Length > 0)
                {
                    str = arr[0]["STOCK_NAME"].ToString() + "-" + arr[0]["STOCK_CODE"].ToString();
                    break;
                }
            }
            return str;
        }
        void WcDownLoadComp(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                bool 팝업필터 = false;

                StringReader sr = new StringReader(e.Result);
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(sr);
                sr.Close();
                //XmlNodeList forecastNodes = doc.SelectNodes("xml_api_reply/news/news_entry");
                System.Xml.XmlNodeList forecastNodes = doc.SelectNodes("rss/channel/item");
                foreach (System.Xml.XmlNode node in forecastNodes)
                {
                    if (node["title"].InnerText.IndexOf("(기타)") > -1
                        || node["title"].InnerText.IndexOf("(코넥스)") > -1
                        ) continue;

                    //if (datagridview2.Rows.Count > 1)
                    //{
                    Boolean blnTrue = false;

                    for (int i = 0; i < dgDart.Rows.Count; i++)
                    {
                        if (i + 1 == dgDart.Rows.Count)
                        {
                            break;
                        }


                        string tmpStr = dgDart.Rows[i].Cells["제목"].Value.ToString();

                        if (tmpStr.IndexOf("매출액 대비") > -1)
                        {
                            tmpStr = dgDart.Rows[i].Cells["제목"].Value.ToString().Replace("-자동매수", "").Substring(0, dgDart.Rows[i].Cells[2].Value.ToString().Replace("-자동매수", "").Length - 7);
                        }
                        else
                        {
                            tmpStr = dgDart.Rows[i].Cells["제목"].Value.ToString().Replace("-자동매수", "");
                        }

                        if (tmpStr.Replace("(3자배정)", "").Replace("-매출액 대비:", "") == node["title"].InnerText)
                        {
                            blnTrue = true;
                            break;
                        }

                        //if (tmpStr == node["title"].InnerText)
                        //{
                        //    blnTrue = true;
                        //    break;
                        //}
                    }
                    if (blnTrue == false)
                    {
                        if (dgDart.ColumnCount == 0) return;
                        dgDart.Rows.Insert(0, 1);

                        dgDart.Rows[0].Cells["시간"].Value = DateTime.Parse(node["pubDate"].InnerText).ToString("yyyy-MM-dd HH:mm") + DateTime.Now.ToString(":ss.") + DateTime.Now.Millisecond; ;
                        dgDart.Rows[0].Cells["종목"].Value = node["dc:creator"].InnerText;
                        dgDart.Rows[0].Cells["제목"].Value = node["title"].InnerText;
                        dgDart.Rows[0].Cells["주소"].Value = node["link"].InnerText;

                        if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:20:00")))
                        {
                            if (node["title"].InnerText.IndexOf("유상증자") > -1 && node["title"].InnerText.IndexOf("자율공시") < 0)
                            {
                                string html = "";

                                html = GetReport(node["link"].InnerText);

                                try
                                {
                                    if (html.IndexOf("3자배정") > -1 && html.IndexOf("자율") < 0)
                                    {
                                        dgDart.Rows[0].Cells["제목"].Value = node["title"].InnerText.Trim() + "(3자배정)";// +"-자동매수";
                                    }
                                }
                                catch
                                {
                                }
                            }
                            //else if (node["title"].InnerText.IndexOf("단일판매ㆍ공급계약체결") > -1 && node["title"].InnerText.IndexOf("기재정정") < 0)
                            //{
                            //    string html = GetReport(node["link"].InnerText);
                            //    try
                            //    {

                            //        if (html.IndexOf("매출액 대비") > -1 || html.IndexOf("매출액대비") > -1)
                            //        {
                            //            int index1 = 0;
                            //            int index2 = 0;
                            //            int index3 = 0;
                            //            int index4 = 0;
                            //            if (html.IndexOf("매출액대비") > -1)
                            //            {
                            //                index1 = html.IndexOf("매출액대비");
                            //            }
                            //            else if (html.IndexOf("매출액 대비") > -1)
                            //            {
                            //                index1 = html.IndexOf("매출액 대비");
                            //            }

                            //            index2 = html.IndexOf("xforms_input", index1);
                            //            index3 = html.IndexOf(">", index2);
                            //            index4 = html.IndexOf("<", index3);

                            //            int leng = 7 - html.Substring(index3 + 1, index4 - index3 - 1).Length;
                            //            string tmpStr1 = html.Substring(index3 + 1, index4 - index3 - 1);
                            //            double tmpCnt = Cls.Val(html.Substring(index3 + 1, index4 - index3 - 1));
                            //            for (int i = 0; i < leng; i++)
                            //            {
                            //                tmpStr1 = " " + tmpStr1;
                            //            }

                            //            if (tmpCnt > 15)
                            //            {
                            //                tmpStr1 = tmpStr1 + "-자동매수";
                            //            }

                            //            dgDart.Rows[0].Cells["제목"].Value = node["title"].InnerText + "-매출액 대비:" + tmpStr1;

                            //        }
                            //    }
                            //    catch
                            //    {
                            //    }
                            //}

                            //else if (node["title"].InnerText.IndexOf("(자율공시)") > -1
                            //    && (node["title"].InnerText.IndexOf("유상증자") > -1 || node["title"].InnerText.IndexOf("사채") > -1)
                            //    && node["title"].InnerText.IndexOf("발행결과") > -1
                            //    && node["title"].InnerText.IndexOf("기재정정") < 0
                            //    )
                            //{
                            //    string html = GetReport(node["link"].InnerText);

                            //    try
                            //    {
                            //        int index1 = 0;
                            //        int index2 = 0;
                            //        int index3 = 0;
                            //        int index4 = 0;
                            //        if (html.IndexOf("발행예정금액") > -1)
                            //        {
                            //            index1 = html.IndexOf("발행예정금액");
                            //        }

                            //        index2 = html.IndexOf("xforms_input", index1);
                            //        index3 = html.IndexOf(">", index2);
                            //        index4 = html.IndexOf("<", index3);

                            //        string tmpStr1 = html.Substring(index3 + 1, index4 - index3 - 1);

                            //        if (html.IndexOf("실제발행금액") > -1)
                            //        {
                            //            index1 = html.IndexOf("실제발행금액");
                            //        }

                            //        index2 = html.IndexOf("xforms_input", index1);
                            //        index3 = html.IndexOf(">", index2);
                            //        index4 = html.IndexOf("<", index3);

                            //        string tmpStr2 = html.Substring(index3 + 1, index4 - index3 - 1);

                            //        if (tmpStr1.Trim() == tmpStr2.Trim())
                            //        {
                            //            dgDart.Rows[0].Cells["제목"].Value = node["title"].InnerText + "-자동매수";
                            //        }
                            //    }
                            //    catch
                            //    {
                            //    }
                            //}
                        }
                        팝업필터 = false;

                        foreach (string str2 in lst팝업필터.Items)
                        {
                            if (dgDart.Rows[0].Cells["제목"].Value.ToString().IndexOf(str2) > -1)
                            {
                                팝업필터 = true;
                                break;
                            }
                        }

                        if (팝업필터 == true) { break; }

                        bool is자동매수 = false;
                        foreach (string str1 in lsb자동매수단어.Items)
                        {
                            if (dgDart.Rows[0].Cells["제목"].Value.ToString().IndexOf(str1) > -1)
                            {
                                AutoBuy(dgDart.Rows[0].Cells["종목"].Value.ToString().Trim(), dgDart.Rows[0].Cells["제목"].Value.ToString().Trim());
                                is자동매수 = true;
                                break;
                            }
                        }

                        if (is자동매수 == true)
                        {
                            string oStr = "";
                            oStr = dgDart.Rows[0].Cells["종목"].Value.ToString() + "-" + dgDart.Rows[0].Cells["제목"].Value.ToString() + " " + dgDart.Rows[0].Cells["주소"].Value.ToString();
                            //_frm.UpdateNoti(oStr);
                            //_frm.Visible = true;
                            //_frm.TopMost = true;
                            //System.Windows.Forms.Clipboard.SetText(dgDart.Rows[0].Cells["종목"].Value.ToString().Trim());
                            break;
                        }
                    }
                }
                dgDart.Sort(dgDart.Columns[0], System.ComponentModel.ListSortDirection.Descending);
            }
            catch (Exception ex)
            {

                Logger("에러 (WcDownLoadComp)", ex.ToString());
            }
        }

        void WcDownLoadCompKrx(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                bool 팝업필터 = false;

                StringReader sr = new StringReader(e.Result);
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(sr);
                sr.Close();
                //XmlNodeList forecastNodes = doc.SelectNodes("xml_api_reply/news/news_entry");
                System.Xml.XmlNodeList forecastNodes = doc.SelectNodes("rss/channel/item");
                foreach (System.Xml.XmlNode node in forecastNodes)
                {
                    if (node["author"].InnerText.IndexOf("[") < 0) continue;

                    //if (datagridview2.Rows.Count > 1)
                    //{
                    Boolean blnTrue = false;

                    for (int i = 0; i < dgDartNew.Rows.Count; i++)
                    {
                        if (i + 1 == dgDartNew.Rows.Count)
                        {
                            break;
                        }


                        string tmpStr = dgDartNew.Rows[i].Cells[2].Value.ToString();

                        ////if (tmpStr.IndexOf("매출액 대비") > -1)
                        ////{
                        ////    tmpStr = dgDartNew.Rows[i].Cells[2].Value.ToString().Replace("-자동매수", "").Substring(0, dgDartNew.Rows[i].Cells[2].Value.ToString().Replace("-자동매수", "").Length - 7);
                        ////}
                        ////else
                        ////{
                        ////    tmpStr = dgDartNew.Rows[i].Cells[2].Value.ToString().Replace("-자동매수", "");
                        ////}

                        if (tmpStr == node["title"].InnerText)
                        {
                            blnTrue = true;
                            break;
                        }
                    }
                    if (blnTrue == false)
                    {
                        if (dgDartNew.Columns.Count == 0) return;
                        dgDartNew.Rows.Insert(0, 1);

                        dgDartNew.Rows[0].Cells[0].Value = DateTime.Parse(node["pubDate"].InnerText).ToString("yyyy-MM-dd HH:mm:ss.") + DateTime.Now.Millisecond; ;
                        dgDartNew.Rows[0].Cells[1].Value = node["author"].InnerText;
                        dgDartNew.Rows[0].Cells[2].Value = node["title"].InnerText;
                        dgDartNew.Rows[0].Cells[3].Value = node["link"].InnerText;

                        if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:20:00")))
                        {
                            if (node["title"].InnerText.IndexOf("유상증자") > -1 && node["title"].InnerText.IndexOf("자율공시") < 0)
                            {
                                string html = "";

                                html = GetReport(node["link"].InnerText);

                                try
                                {
                                    if (html.IndexOf("3자배정") > -1 && html.IndexOf("자율") < 0)
                                    {
                                        dgDartNew.Rows[0].Cells[2].Value = node["title"].InnerText.Trim() + "(3자배정)";// +"-자동매수";
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }

                        팝업필터 = false;

                        foreach (string str2 in lst팝업필터.Items)
                        {
                            if (dgDartNew.Rows[0].Cells[2].Value.ToString().IndexOf(str2) > -1)
                            {
                                팝업필터 = true;
                                break;
                            }
                        }

                        if (팝업필터 == true) { break; }

                        bool is자동매수 = false;
                        foreach (string str1 in lsb자동매수단어.Items)
                        {
                            if (dgDartNew.Rows[0].Cells[2].Value.ToString().IndexOf(str1) > -1)
                            {
                                AutoBuy(dgDartNew.Rows[0].Cells[1].Value.ToString().Trim().Substring(3, dgDartNew.Rows[0].Cells[1].Value.ToString().Trim().Length - 3), dgDartNew.Rows[0].Cells[2].Value.ToString().Trim());
                                is자동매수 = true;
                                break;
                            }
                        }

                        if (is자동매수 == true)
                        {
                            string oStr = "";
                            oStr = dgDartNew.Rows[0].Cells[1].Value.ToString() + "-" + dgDartNew.Rows[0].Cells[2].Value.ToString() + " " + dgDartNew.Rows[0].Cells[3].Value.ToString();
                            //_frm.UpdateNoti(oStr);
                            //_frm.Visible = true;
                            //_frm.TopMost = true;
                            //System.Windows.Forms.Clipboard.SetText(dgDartNew.Rows[0].Cells[1].Value.ToString().Trim());
                            break;
                        }
                    }
                }
                dgDartNew.Sort(dgDartNew.Columns[0], System.ComponentModel.ListSortDirection.Descending);
            }
            catch (Exception ex)
            {

                Logger("에러 (WcDownLoadCompKrx)", ex.ToString());
            }
        }

        private void datagridview2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                System.Diagnostics.Process.Start(dgDart.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());
            }
        }

        private void SimpleBrowserForm_Load(object sender, EventArgs e)
        {
            //InitBrowser();
            //_frm = new frmNotice();
            dgTick.DataSource = _dsTick60;
            cboTickStd.SelectedIndex = 0;
            ucMainStockVer2.Connection();
            
            dg조건종목2.Columns.Clear();
            dg조건종목3.Columns.Clear();
            dg조건종목4.Columns.Clear();
            dg조건종목5.Columns.Clear();

            for (int j = 0; j < this.dg조건종목1.Columns.Count; j++)
            {
                dg조건종목2.Columns.Add(this.dg조건종목1.Columns[j].Clone() as DataGridViewColumn);
                dg조건종목3.Columns.Add(this.dg조건종목1.Columns[j].Clone() as DataGridViewColumn);
                dg조건종목4.Columns.Add(this.dg조건종목1.Columns[j].Clone() as DataGridViewColumn);
                dg조건종목5.Columns.Add(this.dg조건종목1.Columns[j].Clone() as DataGridViewColumn);
            }

            dgDart.DoubleBuffered(true);
            dgDartNew.DoubleBuffered(true);
            dgNews.DoubleBuffered(true);
            dgTotalNews.DoubleBuffered(true);
            dgN관종.DoubleBuffered(true);
            dgOrderList.DoubleBuffered(true);
            dg관종.DoubleBuffered(true);
            dg뉴스잔고.DoubleBuffered(true);
            dg뉴스체결.DoubleBuffered(true);
            dg미체결.DoubleBuffered(true);
            dg조건리스트.DoubleBuffered(true);
            dg조건종목1.DoubleBuffered(true);
            dg조건종목2.DoubleBuffered(true);
            dg조건종목3.DoubleBuffered(true);
            dg조건종목4.DoubleBuffered(true);
            dg조건종목5.DoubleBuffered(true);
            dg화면관리.DoubleBuffered(true);
            dgTick.DoubleBuffered(true);
            cboBuySellDay.SelectedIndex = 1;
        }

        private string _stockId = "";

        private void SimpleBrowserForm_Shown(object sender, EventArgs e)
        {
            lst검색어.Items.Clear();
            int counter = 0;
            string line;
            string curFile = Application.StartupPath + @"\keyword.txt";
            if (File.Exists(curFile) == true)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(curFile, Encoding.Default);
                while ((line = file.ReadLine()) != null)
                {
                    if (lst검색어.Items.Contains(line) == true) continue;

                    txtKeyWord.Text = line;
                    txtKeyWord_KeyDown(txtKeyWord, new KeyEventArgs(Keys.Enter));
                    counter++;
                }

                file.Close();
            }

            counter = 0;
            curFile = Application.StartupPath + @"\자동매수단어.txt";
            if (File.Exists(curFile) == true)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(curFile, Encoding.Default);
                while ((line = file.ReadLine()) != null)
                {
                    if (lsb자동매수단어.Items.Contains(line) == true) continue;

                    txt자동매수단어.Text = line;
                    txt자동매수단어_KeyDown(txt자동매수단어, new KeyEventArgs(Keys.Enter));
                    counter++;
                }

                file.Close();
            }
            else
            {
                lsb자동매수단어.Items.Clear();
                lsb자동매수단어.Items.Add("(3자배정)");
            }

            counter = 0;
            curFile = Application.StartupPath + @"\팝업필터.txt";
            if (File.Exists(curFile) == true)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(curFile, Encoding.Default);
                while ((line = file.ReadLine()) != null)
                {
                    if (lst팝업필터.Items.Contains(line) == true) continue;

                    txt팝업필터.Text = line;
                    txt팝업필터_KeyDown(txt팝업필터, new KeyEventArgs(Keys.Enter));
                    counter++;
                }

                file.Close();
            }
            else
            {
                lst팝업필터.Items.Clear();
                lst팝업필터.Items.Add("[기재정정]");
                lst팝업필터.Items.Add("[첨부정정]");
            }

            counter = 0;
            curFile = Application.StartupPath + @"\Stoploss제외항목.txt";

            lsb제외항목.Items.Clear();
            if (File.Exists(curFile) == true)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(curFile, Encoding.Default);
                try
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        if (lsb제외항목.Items.Contains(line) == true) continue;
                        lsb제외항목.Items.Add(line);
                        counter++;
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                finally { file.Close(); }
            }
            txt자동매수단어.Text = "";
            txt팝업필터.Text = "";
            txt제외항목.Text = "";

            txtKeyWord.Focus();
        }

        private void SimpleBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrNews.Stop();
            tmrDart.Stop();
            tmrDartKrx.Stop();

            Task.WaitAll();
            if (MessageBox.Show("TICK 테이블 저장하시겠습니까?", "알림!!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                tbStockList.SelectedIndex = 7;
                foreach (DataTable dt in _dsTick60.Tables)
                {
                    TickSaveNew(dt);
                }
            }

            //_frm.Dispose();
            string fileName = Application.StartupPath + @"\keyword.txt";
            FileStream fs = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                using (StreamWriter writer = new StreamWriter(fs, Encoding.Default))
                {
                    foreach (string str in lst검색어.Items)
                    {
                        writer.WriteLine(str);
                    }
                }
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }

            fileName = Application.StartupPath + @"\자동매수단어.txt";
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                using (StreamWriter writer = new StreamWriter(fs, Encoding.Default))
                {
                    foreach (string str in lsb자동매수단어.Items)
                    {
                        writer.WriteLine(str);
                    }
                }
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }

            fileName = Application.StartupPath + @"\팝업필터.txt";
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                using (StreamWriter writer = new StreamWriter(fs, Encoding.Default))
                {
                    foreach (string str in lst팝업필터.Items)
                    {
                        writer.WriteLine(str);
                    }
                }
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }


            fileName = Application.StartupPath + @"\Stoploss제외항목.txt";
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                using (StreamWriter writer = new StreamWriter(fs, Encoding.Default))
                {
                    foreach (string str in lsb제외항목.Items)
                    {
                        writer.WriteLine(str);
                    }
                }
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
        }

        private void toolStripContainer_ContentPanel_Load(object sender, EventArgs e)
        {

        }

        private string GetReport(string url)
        {
            string html = GetResponse(url, Encoding.UTF8);
            string[] arrPattern = new string[0];


            string urlPart = "";
            // Regex 와 MatchCollection 를 이용해서 패턴으로 분류합니다.             
            int index1 = 0;
            int index2 = 0;

            string tmpStr = "";

            string strPatternOne = "click: function()";
            //string strPatternOne = @"click:*\r";
            string strPatternTwo = "javascript:";

            urlPart = "http://dart.fss.or.kr/report/viewer.do?";
            int count = 0;
            string html1 = "";

            if (html.IndexOf("단일판매ㆍ공급계약체결") > -1 || html.IndexOf("자율공시") > -1)
            {

                Regex regex = new Regex(strPatternTwo);
                MatchCollection mc = regex.Matches(html);
                System.Collections.Hashtable ht = new System.Collections.Hashtable();

                foreach (Match m in mc)
                {
                    // 첫번째 패턴으로 뽑아낸 데이터를 저장합니다..
                    if (count == 0)
                    {
                        ////Array.Resize(ref arrPattern, count + 1);
                        ////arrPattern[count] = m.ToString();
                        index1 = html.IndexOf("viewDoc(", m.Index);
                        index2 = html.IndexOf(")", index1);
                        tmpStr = html.Substring(index1 + 9, index2 - 10 - index1);
                        string[] tmpSp = tmpStr.Split(',');

                        urlPart += "rcpNo=" + tmpSp[0].Replace("'", "").Trim();
                        urlPart += "&dcmNo=" + tmpSp[1].Replace("'", "").Trim();
                        urlPart += "&eleId=0";
                        urlPart += "&offset=0";
                        urlPart += "&length=0";
                        urlPart += "&dtd=HTML";
                    }

                    count++;
                }

                html1 = GetResponse(urlPart, Encoding.Default);
            }
            else
            {
                Regex regex = new Regex(strPatternOne);
                MatchCollection mc = regex.Matches(html);
                System.Collections.Hashtable ht = new System.Collections.Hashtable();

                foreach (Match m in mc)
                {
                    // 첫번째 패턴으로 뽑아낸 데이터를 저장합니다..
                    if (count == 1)
                    {
                        ////Array.Resize(ref arrPattern, count + 1);
                        ////arrPattern[count] = m.ToString();
                        index1 = html.IndexOf("viewDoc(", m.Index);
                        index2 = html.IndexOf(")", index1);
                        tmpStr = html.Substring(index1 + 9, index2 - 10 - index1);
                        string[] tmpSp = tmpStr.Split(',');

                        urlPart += "rcpNo=" + tmpSp[0].Replace("'", "").Trim();
                        urlPart += "&dcmNo=" + tmpSp[1].Replace("'", "").Trim();
                        urlPart += "&eleId=" + tmpSp[2].Replace("'", "").Trim();
                        urlPart += "&offset=" + tmpSp[3].Replace("'", "").Trim();
                        urlPart += "&length=" + tmpSp[4].Replace("'", "").Trim();
                        urlPart += "&dtd=" + tmpSp[5].Replace("'", "").Trim();
                    }

                    count++;
                }

                html1 = GetResponse(urlPart, Encoding.UTF8);
            }

            return html1;
        }

        private void btn공시재로드_Click(object sender, EventArgs e)
        {
            dgDart.RowCount = 0;
            dgDartNew.RowCount = 0;

            tmrDart.Start();
            tmrDartKrx.Start();
        }

        private void txt자동매수단어_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lsb자동매수단어.Items.Add(txt자동매수단어.Text.Trim());
                txt자동매수단어.Text = "";
                txt자동매수단어.Focus();
            }
        }

        private void lsb자동매수단어_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lsb자동매수단어.SelectedIndex == -1) return;
                lsb자동매수단어.Items.RemoveAt(lsb자동매수단어.SelectedIndex);
            }
        }

        private void txt팝업필터_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lst팝업필터.Items.Add(txt팝업필터.Text.Trim());
                txt팝업필터.Text = "";
                txt팝업필터.Focus();
            }
        }

        private void lst팝업필터_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lst팝업필터.SelectedIndex == -1) return;
                lst팝업필터.Items.RemoveAt(lst팝업필터.SelectedIndex);
            }
        }

        private void txt주문종목코드_Enter(object sender, EventArgs e)
        {
            TextBox obj = (TextBox)sender;
            obj.SelectAll();
        }

        private void btn실행_Click(object sender, EventArgs e)
        {
            if (chk이익실현.Checked == false && chk손절.Checked == false) { MessageBox.Show("이익실현 or 손절 둘 중 하나는 체크 하셔야 StopLoss 가 실행가능합니다.", "경고!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if (btnStopLoss.Text == "▶")
            {
                btnStopLoss.Text = "||";
                pnlStopChk.Enabled = false;
            }
            else
            {
                btnStopLoss.Text = "▶";
                pnlStopChk.Enabled = true;
            }
        }

        private void lsb제외항목_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                lsb제외항목.Items.RemoveAt(lsb제외항목.SelectedIndex);
            }
        }

        private void txt제외항목_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ucMainStockVer2._allStockDataset == null) return;
                if (lsb제외다수.Visible == true)
                {
                    lsb제외항목.Items.Add(lsb제외다수.Items[0].ToString());
                    lsb제외다수.Visible = false;
                    txt제외항목.Text = "";
                    txt제외항목.Focus();
                }
                else
                {
                    lsb제외다수.Items.Clear();
                    DataView dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
                    dv.RowFilter = "STOCK_NAME = '" + txt제외항목.Text + "'";

                    if (dv.Count == 0) return;

                    foreach (DataRowView dr in dv)
                    {
                        lsb제외다수.Items.Add(dr["STOCK_NAME"].ToString().Trim() + "|" + dr["STOCK_CODE"].ToString().Trim());
                    }

                    if (lsb제외다수.Items.Count == 1)
                    {
                        lsb제외항목.Items.Add(lsb제외다수.Items[0].ToString());
                        lsb제외다수.Visible = false;
                        txt제외항목.Text = "";
                    }
                    else
                    {
                        lsb제외다수.Visible = true;
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tbStockList.SelectedIndex == 1)
            {
                //UcMainStockVer2.Opw00018_OnReceiveTrData(cboAccount.Text.Trim());
            }
            else if (tbStockList.SelectedIndex == 2)
            {
                if (cbo조건1.Items.Count < 1)
                {
                    ucMainStockVer2.GetConditionLoad_OnReceiveTrCondition();
                }
            }
            else if (tbStockList.SelectedIndex == 3)
            {
            }
        }


        private void btn로그클리어_Click(object sender, EventArgs e)
        {
            dgLog.RowCount = 0;
        }


        private void dg조건리스트_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value.ToString().Trim() == "||") return;
            //dg조건종목.RowCount = 0;
            //UcMainStockVer2.SendCondition_OnReceiveConditionVer("9" + dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim()
            //    , dg조건리스트.Rows[e.RowIndex].Cells["조건명"].Value.ToString().Trim()
            //    , dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim(), 0);

        }


        private void btn조건식_Click(object sender, EventArgs e)
        {

        }

        private void btn일괄매수_Click(object sender, EventArgs e)
        {
            
        }

        public int MakeOrderPrice(int 금액)
        {
            int 주문가 = 0;
            if (금액 < 1000) 주문가 = 금액;
            else if (금액 >= 1000 && 금액 < 10000) 주문가 = Cls.ValInt(Cls.Left(금액.ToString(), 3) + "0");
            else if (금액 >= 10000 && 금액 < 100000) 주문가 = Cls.ValInt(Cls.Left(금액.ToString(), 3) + "00");
            else if (금액 >= 100000 && 금액 < 1000000) 주문가 = Cls.ValInt(Cls.Left(금액.ToString(), 3) + "000");
            else if (금액 >= 1000000 && 금액 < 10000000) 주문가 = Cls.ValInt(Cls.Left(금액.ToString(), 3) + "0000");

            return 주문가;
        }

        private bool _실시간실행여부;
        private void dg조건리스트_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value.ToString().Trim() == "▶")
                {
                    if (_실시간실행여부 == true)
                    {
                        MessageBox.Show("실행중인 조건검색이 있습니다. 종료하고 실행해 주십시요", "경고!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dg조건종목1.RowCount = 0;
                    ucMainStockVer2.SendCondition_OnReceiveConditionVer("9" + dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim()
                        , dg조건리스트.Rows[e.RowIndex].Cells["조건명"].Value.ToString().Trim()
                        , dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim(), 1);

                    dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value = "||";
                    _실시간실행여부 = true;
                }
                else
                {
                    dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value = "▶";
                    ucMainStockVer2.SendConditionStop("9" + dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim()
                        , dg조건리스트.Rows[e.RowIndex].Cells["조건명"].Value.ToString().Trim(),
                        dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim());
                    _실시간실행여부 = false;
                }
            }
        }


        private void  SettingAccountData(DataSet ds)
        {
            int 이익실현차수 = 0;
            Double 매입가 = 0;
            int 현재가 = 0;
            int 보유수량 = 0;
            int 매매가능수량 = 0;
            string 종목코드 = "";
            string 종목명 = "";
            
            if (ds.Tables[0].Rows.Count == 0) { return; }
            DataRow drReal = ds.Tables[0].Rows[0];
            Double temp = 0;
            Double 갭체결강도 = Cls.Val(nmStopPwRate.Value.ToString());

            종목코드 = drReal["STOCK_CODE"].ToString();
            DataGridViewRow dvr;
            var obj = (from DataGridViewRow dgr in dg뉴스잔고.Rows
                        where dgr.Cells[J종목코드.Index].Value != null && dgr.Cells[J종목코드.Index].Value.Equals(종목코드)
                        select dgr).FirstOrDefault();
            if (obj == null) return;
            dvr = obj;

            현재가 = Math.Abs(Cls.ValInt(drReal["현재가"].ToString().Trim()));
            매입가 = Cls.Val(dvr.Cells[J매입가.Index].Value.ToString());
            if (dvr.Cells[J보유수량.Index].Value != null)
            {
                보유수량 = Cls.ValInt(dvr.Cells[J보유수량.Index].Value.ToString());
            }
            if (dvr.Cells[J매매가능수량.Index].Value != null) { 
                매매가능수량 = Cls.ValInt(dvr.Cells[J매매가능수량.Index].Value.ToString());
            }

            dvr.Cells[J현재가.Index].Value = 현재가;
            dvr.Cells[J수익률.Index].Value = String.Format("{0:0.00}" , ((Double)현재가 - (Double)매입가) / (Double)매입가 * 100);

            dvr.Cells[J평가금액.Index].Value = 현재가 *  보유수량;
            dvr.Cells[J등락율.Index].Value = drReal["등락율"].ToString();
            dvr.Cells[J대비.Index].Value = drReal["전일대비"].ToString();

            //최고체결강도를 UPDATE 해준다. - S

            if (_ht최고체결강도[종목코드] != null)
            {
                temp = Cls.Val(_ht최고체결강도[종목코드].ToString().Trim());
                if (temp < Cls.Val(drReal["체결강도"].ToString().Trim()))
                {
                    _ht최고체결강도[종목코드] = Cls.Val(drReal["체결강도"].ToString().Trim());
                }
                else
                {
                    //if (Cls.Val(drReal["체결강도"].ToString().Trim()) > 100) 갭체결강도 = 갭체결강도 * 2;
                    //if (
                    //    Cls.Val(_ht최고체결강도[종목코드].ToString().Trim()) - Cls.Val(drReal["체결강도"].ToString().Trim()) > 갭체결강도
                    //)
                    //{
                        bool isSell = true;
                        if (dvr.Cells[J체크.Index].Value != null &&  Convert.ToBoolean(dvr.Cells[J체크.Index].Value) == true) isSell = false;
                        else if (매매가능수량 == 0) { isSell = false; }
                        else
                        {
                            if (_dsTick60.Tables[종목코드] != null) //잔고 실시간 송신할때 _dsTick60 에 테이블을 만듬 (SetDsScreenNo)
                            {
                                if (Cls.Val(_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 1]["COUNT"]) < 5) { isSell = false; }
                                if (Math.Abs(Cls.ValInt(_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 1]["매수거래비용"])) > Math.Abs(Cls.ValInt(_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 1]["매도거래비용"]))) { isSell = false; } //매수량이 더 많은데 구지 매도할 필요없음 매도량의 2% 까지는 본다.
                            }
                        }
                        if (isSell)
                        {
                            if (dvr.Cells[J이익실현1.Index].Value == null || dvr.Cells[J이익실현1.Index].Value.Equals("") || dvr.Cells[J이익실현1.Index].Value.Equals("0"))
                                이익실현차수 = 1;
                            else if (dvr.Cells[J이익실현1.Index].Value.Equals("1") &&
                                (dvr.Cells[J이익실현2.Index].Value == null || dvr.Cells[J이익실현2.Index].Value.Equals("") || dvr.Cells[J이익실현2.Index].Value.Equals("0")))
                                이익실현차수 = 2;
                            else if (dvr.Cells[J이익실현2.Index].Value.Equals("1") &&
                                (dvr.Cells[J이익실현3.Index].Value == null || dvr.Cells[J이익실현3.Index].Value.Equals("") || dvr.Cells[J이익실현3.Index].Value.Equals("0")))
                                이익실현차수 = 3;

                            ProcessStopLoss(
                                Cls.Val(dvr.Cells[J수익률.Index].Value.ToString()),
                                ds,
                                종목코드,
                                종목명,
                                매매가능수량,
                                이익실현차수,
                                dvr
                            );
                        }
                    //}
                }
            }
            else
            {
                _ht최고체결강도.Add(종목코드, Cls.Val(drReal["체결강도"].ToString().Trim()));
            }
            //최고체결강도를 UPDATE 해준다. - E
        }

        private void ProcessStopLoss(Double 수익률, DataSet ds, string 종목코드, string 종목명, int 매매가능수량, int 이익실현차수, DataGridViewRow dgrJan)
        {
            if (종목코드 == ""  || 매매가능수량 == 0) return;
            if (btnStopLoss.Text.Trim() == "▶") return;
            foreach (string str in lsb제외항목.Items)
            {
                if (str.IndexOf(종목코드) > -1) return;
            }

            ArrayParam arr = new ArrayParam();
            bool isDart = false;
            bool is익절 = false;
            bool is손절 = false;
            Double stoplossPer = 0;
            Double fitPer = 0;
            Double lossPer = 0;
            int 매매수량 = 0;
            string 거래구분 = "00";

            DataRow drReal = ds.Tables[0].Rows[0];
            int 현재가 = Cls.ValInt(drReal["현재가"]);
            int 최우선매도호가 = Cls.ValInt(drReal["(최우선)매도호가"]);
            int 최우선매수호가 = Cls.ValInt(drReal["(최우선)매수호가"]);

            var obj = (from DataGridViewRow dgr in dg뉴스체결.Rows
                       where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(종목코드) && dgr.Cells[N모니터링구분.Index].Value.Equals("공")
                       select dgr).FirstOrDefault();

            if (obj != null || (dgrJan.Cells[J구분.Index].Value != null && dgrJan.Cells[J구분.Index].Value.Equals("공")))
            {
                isDart = true;
            }


            stoplossPer = 수익률;
            
            if (isDart)
            {
                is익절 = chk이익실현공.Checked;
                is손절 = chk손절공.Checked;
                if (이익실현차수.Equals(1))
                {
                    fitPer = Cls.Val(nm이익실현공1.Value.ToString());
                    매매수량 = Cls.ValInt((매매가능수량 * (nm보유비율공1.Value / 100)).ToString("#,##0"));
                }
                else if (이익실현차수.Equals(2))
                {
                    fitPer = Cls.Val(nm이익실현공2.Value.ToString());
                    매매수량 = Cls.ValInt((매매가능수량 * (nm보유비율공2.Value / 100)).ToString("#,##0"));
                }
                else if (이익실현차수.Equals(3))
                {
                    fitPer = Cls.Val(nm이익실현공3.Value.ToString());
                    매매수량 = Cls.ValInt((매매가능수량 * (nm보유비율공3.Value / 100)).ToString("#,##0"));
                }
                lossPer = Cls.Val(nm손절공.Value.ToString());
                거래구분 = "03";
            }
            else
            {
                is익절 = chk이익실현.Checked;
                is손절 = chk손절.Checked;
                if (이익실현차수.Equals(1))
                {
                    fitPer = Cls.Val(nm이익실현1.Value.ToString());
                    매매수량 = Cls.ValInt((매매가능수량 * (nm보유비율1.Value / 100)).ToString("#,##0"));
                }
                else if (이익실현차수.Equals(2))
                {
                    fitPer = Cls.Val(nm이익실현2.Value.ToString());
                    매매수량 = Cls.ValInt((매매가능수량 * (nm보유비율2.Value / 100)).ToString("#,##0"));
                }
                else if (이익실현차수.Equals(3))
                {
                    fitPer = Cls.Val(nm이익실현3.Value.ToString());
                    매매수량 = Cls.ValInt((매매가능수량 * (nm보유비율3.Value / 100)).ToString("#,##0"));
                }
                lossPer = Cls.Val(nm손절.Value.ToString());
            }

            if (매매수량 == 0)
            {
                return;
            }

            if (is익절)
            {
                if (stoplossPer >= fitPer)
                {
                    SendBuySellMsg(종목코드, 거래구분, (int)ucMainStock.OrderType.신규매도, 0, 최우선매수호가, 매매수량, "2"); //공시 : 시장가 , 그냥 : 현재가
                    dg뉴스잔고.CellValueChanged -= dg뉴스잔고_CellValueChanged;
                    if (이익실현차수.Equals(1))
                    {
                        dgrJan.Cells[J이익실현1.Index].Value = "1";
                        _DataAcc.p_stock_profit_Add("A", cboAccount.Text, 종목코드, 최우선매수호가.ToString(), "", "", "", "", "");
                        //이익실현 부분 셋팅 - S
                        UpdateDsProfit("U" , 종목코드, null, "1", "-1", "-1", "-1", "-1");
                        //이익실현 부분 셋팅 - E
                    }
                    else if (이익실현차수.Equals(2))
                    {
                        dgrJan.Cells[J이익실현2.Index].Value = "1";
                        _DataAcc.p_stock_profit_Add("A", cboAccount.Text, 종목코드, "", 최우선매수호가.ToString(), "", "", "", "");
                        //이익실현 부분 셋팅 - S
                        UpdateDsProfit("U", 종목코드, null, "-1", "1", "-1", "-1", "-1");
                        //이익실현 부분 셋팅 - E
                    }
                    else if (이익실현차수.Equals(3))
                    {
                        dgrJan.Cells[J이익실현3.Index].Value = "1";
                        _DataAcc.p_stock_profit_Add("A", cboAccount.Text, 종목코드, "", "", 최우선매수호가.ToString(), "", "", "");
                        //이익실현 부분 셋팅 - S
                        UpdateDsProfit("U", 종목코드, null, "-1", "-1", "1", "-1", "-1");
                        //이익실현 부분 셋팅 - E
                    }
                    dg뉴스잔고.CellValueChanged += dg뉴스잔고_CellValueChanged;
                }
            }

            if (is손절)
            {
                if (stoplossPer <= lossPer)
                {
                    SendBuySellMsg(종목코드, "00", (int)ucMainStock.OrderType.신규매도, 0, 최우선매수호가, 매매가능수량, "2"); //지정가
                }
            }
        }

        private Task SettingConditionStockListDetailData(DataSet ds)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            DataRow dr;
            int row = -1;
            if (ds == null) { }
            dr = ds.Tables[0].Rows[0];
            string 종목코드 = dr["STOCK_CODE"].ToString().Trim();

            var obj = (from DataGridViewRow dgr in dg조건종목1.Rows
                       where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;

                
                dg조건종목1.Rows[row].Cells[C등락률.Index].Value = dr["등락율"].ToString().Trim();
                dg조건종목1.Rows[row].Cells[C거래량.Index].Value = Cls.Val(dr["누적거래량"].ToString().Trim()).ToString("#,##0");
                dg조건종목1.Rows[row].Cells[C시가.Index].Value = Math.Abs(Cls.Val(dr["시가"].ToString().Trim())).ToString("#,##0");
                dg조건종목1.Rows[row].Cells[C고가.Index].Value = Math.Abs(Cls.Val(dr["고가"].ToString().Trim())).ToString("#,##0");
                dg조건종목1.Rows[row].Cells[C저가.Index].Value = Math.Abs(Cls.Val(dr["저가"].ToString().Trim())).ToString("#,##0");
                dg조건종목1.Rows[row].Cells[C현재가.Index].Value = Math.Abs(Cls.Val(dr["현재가"].ToString().Trim())).ToString("#,##0");
                dg조건종목1.Rows[row].Cells[C대비.Index].Value = dr["전일대비"].ToString().Trim();
            }


            obj = (from DataGridViewRow dgr in dg조건종목2.Rows
                   where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                   select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;

                dg조건종목2.Rows[row].Cells[C현재가.Index].Value = Math.Abs(Cls.Val(dr["현재가"].ToString().Trim())).ToString("#,##0");
                dg조건종목2.Rows[row].Cells[C등락률.Index].Value = dr["등락율"].ToString().Trim();
                dg조건종목2.Rows[row].Cells[C대비.Index].Value = dr["전일대비"].ToString().Trim();
                dg조건종목2.Rows[row].Cells[C거래량.Index].Value = Cls.Val(dr["누적거래량"].ToString().Trim()).ToString("#,##0");
                dg조건종목2.Rows[row].Cells[C시가.Index].Value = Math.Abs(Cls.Val(dr["시가"].ToString().Trim())).ToString("#,##0");
                dg조건종목2.Rows[row].Cells[C고가.Index].Value = Math.Abs(Cls.Val(dr["고가"].ToString().Trim())).ToString("#,##0");
                dg조건종목2.Rows[row].Cells[C저가.Index].Value = Math.Abs(Cls.Val(dr["저가"].ToString().Trim())).ToString("#,##0");
            }


            obj = (from DataGridViewRow dgr in dg조건종목3.Rows
                   where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                   select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;

                dg조건종목3.Rows[row].Cells[C현재가.Index].Value = Math.Abs(Cls.Val(dr["현재가"].ToString().Trim())).ToString("#,##0");
                dg조건종목3.Rows[row].Cells[C등락률.Index].Value = dr["등락율"].ToString().Trim();
                dg조건종목3.Rows[row].Cells[C대비.Index].Value = dr["전일대비"].ToString().Trim();
                dg조건종목3.Rows[row].Cells[C거래량.Index].Value = Cls.Val(dr["누적거래량"].ToString().Trim()).ToString("#,##0");
                dg조건종목3.Rows[row].Cells[C시가.Index].Value = Math.Abs(Cls.Val(dr["시가"].ToString().Trim())).ToString("#,##0");
                dg조건종목3.Rows[row].Cells[C고가.Index].Value = Math.Abs(Cls.Val(dr["고가"].ToString().Trim())).ToString("#,##0");
                dg조건종목3.Rows[row].Cells[C저가.Index].Value = Math.Abs(Cls.Val(dr["저가"].ToString().Trim())).ToString("#,##0");
            }


            obj = (from DataGridViewRow dgr in dg조건종목4.Rows
                   where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                   select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;

                dg조건종목4.Rows[row].Cells[C현재가.Index].Value = Math.Abs(Cls.Val(dr["현재가"].ToString().Trim())).ToString("#,##0");
                dg조건종목4.Rows[row].Cells[C등락률.Index].Value = dr["등락율"].ToString().Trim();
                dg조건종목4.Rows[row].Cells[C대비.Index].Value = dr["전일대비"].ToString().Trim();
                dg조건종목4.Rows[row].Cells[C거래량.Index].Value = Cls.Val(dr["누적거래량"].ToString().Trim()).ToString("#,##0");
                dg조건종목4.Rows[row].Cells[C시가.Index].Value = Math.Abs(Cls.Val(dr["시가"].ToString().Trim())).ToString("#,##0");
                dg조건종목4.Rows[row].Cells[C고가.Index].Value = Math.Abs(Cls.Val(dr["고가"].ToString().Trim())).ToString("#,##0");
                dg조건종목4.Rows[row].Cells[C저가.Index].Value = Math.Abs(Cls.Val(dr["저가"].ToString().Trim())).ToString("#,##0");
            }

            obj = (from DataGridViewRow dgr in dg조건종목5.Rows
                   where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                   select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;

                dg조건종목5.Rows[row].Cells[C현재가.Index].Value = Math.Abs(Cls.Val(dr["현재가"].ToString().Trim())).ToString("#,##0");
                dg조건종목5.Rows[row].Cells[C등락률.Index].Value = dr["등락율"].ToString().Trim();
                dg조건종목5.Rows[row].Cells[C대비.Index].Value = dr["전일대비"].ToString().Trim();
                dg조건종목5.Rows[row].Cells[C거래량.Index].Value = Cls.Val(dr["누적거래량"].ToString().Trim()).ToString("#,##0");
                dg조건종목5.Rows[row].Cells[C시가.Index].Value = Math.Abs(Cls.Val(dr["시가"].ToString().Trim())).ToString("#,##0");
                dg조건종목5.Rows[row].Cells[C고가.Index].Value = Math.Abs(Cls.Val(dr["고가"].ToString().Trim())).ToString("#,##0");
                dg조건종목5.Rows[row].Cells[C저가.Index].Value = Math.Abs(Cls.Val(dr["저가"].ToString().Trim())).ToString("#,##0");
            }

            tcs.SetResult(true);
            return tcs.Task;
        }

        private void SetConditionListVer(DataSet ds)
        {
            //ArrayList arr = new ArrayList();
            int row = 0;
            if (ds == null) { return; }

            try
            {
                //dg조건리스트.RowCount = 0;
                //arr.Clear();
                cbo조건1.Items.Clear();
                cbo조건2.Items.Clear();
                cbo조건3.Items.Clear();
                cbo조건4.Items.Clear();
                cbo조건5.Items.Clear();
                foreach (DataRow dr in ds.Tables["CondiList"].Select("CONDI_NAME LIKE 'API%'"))
                {
                    //if (dg조건리스트.RowCount - 1 <= row) dg조건리스트.RowCount = dg조건리스트.RowCount + 1;
                    //dg조건리스트.Rows[row].Cells["조건명"].Value = dr["CONDI_NAME"].ToString().Trim();
                    //dg조건리스트.Rows[row].Cells["번호"].Value = dr["CONDI_SEQ"].ToString().Trim();
                    //dg조건리스트.Rows[row].Cells["실행"].Value = "▶";
                    //arr.Add(dr["CONDI_SEQ"].ToString().Trim() + "^" + dr["CONDI_NAME"].ToString().Trim());
                    cbo조건1.Items.Add(dr["CONDI_SEQ"].ToString().Trim() + "^" + dr["CONDI_NAME"].ToString().Trim());
                    cbo조건2.Items.Add(dr["CONDI_SEQ"].ToString().Trim() + "^" + dr["CONDI_NAME"].ToString().Trim());
                    cbo조건3.Items.Add(dr["CONDI_SEQ"].ToString().Trim() + "^" + dr["CONDI_NAME"].ToString().Trim());
                    cbo조건4.Items.Add(dr["CONDI_SEQ"].ToString().Trim() + "^" + dr["CONDI_NAME"].ToString().Trim());
                    cbo조건5.Items.Add(dr["CONDI_SEQ"].ToString().Trim() + "^" + dr["CONDI_NAME"].ToString().Trim());
                    row = row + 1;
                }

                //cbo조건1.Items.Clear();
                //cbo조건2.Items.Clear();
                //cbo조건3.Items.Clear();
                //cbo조건4.Items.Clear();
                //cbo조건5.Items.Clear();
                //for (int i = 0; i < arr.Count; i++)
                //{
                //    cbo조건1.Items.Add(arr[i].ToString());
                //    cbo조건2.Items.Add(arr[i].ToString());
                //    cbo조건3.Items.Add(arr[i].ToString());
                //    cbo조건4.Items.Add(arr[i].ToString());
                //    cbo조건5.Items.Add(arr[i].ToString());
                //}
            }

            catch (Exception ex)
            {
                Logger("에러 (OnDsGetConditionList)", ex.ToString());
            }
        }

        private DataSet ConvertDsNumber(DataSet ds, int startCol)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int col = startCol; col < ds.Tables[0].Columns.Count - 1; col++)
                {

                    if (VB6.Information.IsNumeric(dr[col].ToString().Trim().Replace(",", "")) == true)
                    {
                        if (dr[col].ToString().Trim().IndexOf(".") > -1)
                        {
                            dr[col] = Cls.Val(dr[col].ToString().Trim()).ToString("#,##0.00");
                        }
                        else
                        {
                            if (col == 3) //수익률이 값이 이상하게 넘어온다.
                            {
                                dr[col] = (Cls.Val(dr[col].ToString().Trim()) / 100).ToString("#,##0.00");
                            }
                            else
                            {
                                dr[col] = Cls.ValInt(dr[col].ToString().Trim()).ToString("#,##0");
                            }
                        }
                    }
                }
            }
            return ds;
        }

        private void SettingNewsOrderPrice(string 종목코드, string 체결가)
        {
            var obj = (from DataGridViewRow dgr in dg뉴스체결.Rows
                       where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();

            if (obj != null)
            {
                using (DataGridViewRow dgr = (DataGridViewRow)obj)
                {
                    dgr.Cells["N주문가"].Value = Cls.Val(체결가).ToString("#,##0");
                }
            }
            //뉴스체결로 인해 시장가로 주문이 들어감 체결된 가격으로 UPDATE한다.
        }

        private void SetRealDataVolume(DataSet ds)
        {
            string str = "";
            DataRow dr;
            DataView dv;
            string 종목코드 = "";
            dv = new DataView(_dt화면관리);

            Logger(DateTime.Now.ToString("HH:mm:ss"), str);

            if (ds == null) return;
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1) return;

            dr = ds.Tables[0].Rows[0];

            for (int col = 0; col < ds.Tables[0].Columns.Count - 1; col++)
            {
                str += ds.Tables[0].Columns[col].ColumnName + " : " + dr[col].ToString().Trim() + " | ";
            }

            if (ds.Tables[0].Rows[0]["sRealType"].ToString().Trim() == "주식체결")
            {

                종목코드 = ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim();

                var cts = new CancellationTokenSource();

                if (_dsTick60.Tables[종목코드] != null)
                {
                    var t1 = new Task(() => SettingTick(ds, _dsTick60.Tables[종목코드]), cts.Token); //Tick 저장
                    t1.Start();
                    try
                    {
                        t1.Wait();
                    }
                    catch (AggregateException ex)
                    {
                        Console.WriteLine(ex.TargetSite.Name);
                        Console.WriteLine("TickSetting Exception : " + 종목코드);
                        cts.Cancel();
                    }
                }

                try
                {
                    var tParent1 = new Task(() =>
                    {
                        new Task(() => SettingNewsFav(ds), TaskCreationOptions.AttachedToParent).Start(); //뉴스관종
                        new Task(() => SettingAccountData(ds), TaskCreationOptions.AttachedToParent).Start(); //잔고
                    }
                    , cts.Token);

                    tParent1.Start();
                    tParent1.Wait();
                
                    var tParent2 = new Task(() =>
                    {
                        new Task(() => SettingNewsOrder(ds), TaskCreationOptions.AttachedToParent).Start(); //뉴스체결
                        new Task(() => SettingConditionStockListDetailData(ds), TaskCreationOptions.AttachedToParent).Start(); //조건검색
                        new Task(() => SettingFavData(ds), TaskCreationOptions.AttachedToParent).Start(); //관심종목

                        //if (ucFinance1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
                        //{
                        //    new Task(() => ucFinance1.Prop_RealDs = ds, TaskCreationOptions.AttachedToParent).Start(); //기업정보
                        //}

                        if (ucHogaWindowNew1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
                        {
                            new Task(() => ucHogaWindowNew1.Property_GetStockTrade = ds, TaskCreationOptions.AttachedToParent).Start(); //기업정보
                        }
                    }
                    , cts.Token);

                    tParent2.Start();
                    tParent2.Wait();
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine(ex.TargetSite.Name);
                    Console.WriteLine("RealData Exception : " + 종목코드);
                    cts.Cancel();
                }


                //try
                //{
                //    _tcs1 = SettingNewsFav(ds);
                //    if (await Task.WhenAny(_tcs1, Task.Delay(2000)) == _tcs1) { }
                //    else { }
                        
                //}
                //catch (ArgumentOutOfRangeException ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                //try
                //{
                //    _tcs8 = SettingNewsOrder(ds);
                //    if (await Task.WhenAny(_tcs8, Task.Delay(2000)) == _tcs8) { }
                //    else { }

                //    //_tcs8 = new Task(() => SettingNewsOrder(ds));
                //    //_tcs8.Start();
                //}

                //catch (ArgumentOutOfRangeException ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

            }
        }

        private void SettingTick(DataSet ds, DataTable dt)
        {
            if (ds == null) { return; }
            if (ds.Tables.Count < 1) { return; }
            if (ds.Tables[0].Rows.Count < 1) { return; }
            DataRow dr;
            DataRow drReal = ds.Tables[0].Rows[0];

            bool isBuy = true ;
            if (Cls.ValInt(drReal["거래량"]) < 0) isBuy = false;
            else isBuy = true;

            int price = Math.Abs(Cls.ValInt(drReal["현재가"]));
            int volume = Math.Abs(Cls.ValInt(drReal["거래량"]));
            int tradingprice = price * volume;
            

            if (volume <= 20) { return; }

            if (dt.Rows.Count == 0)
            {
                dr = dt.Rows.Add();
                dr["일자"] = DateTime.Now.ToString("yyyyMMdd");
                dr["현재가"] = price;
                dr["시가"] = price;
                dr["고가"] = price;
                dr["저가"] = price;
                dr["등락율"] = drReal["등락율"];
                dr["체결강도"] = drReal["체결강도"];
                if (!isBuy)
                {
                    dr["매수거래량"] = 0;
                    dr["매도거래량"] = volume * -1;
                    dr["매수거래비용"] = 0;
                    dr["매도거래비용"] = tradingprice * -1;
                }
                else
                {
                    dr["매수거래량"] = volume;
                    dr["매도거래량"] = 0;
                    dr["매수거래비용"] = tradingprice;
                    dr["매도거래비용"] = 0;
                }
                dr["시작시간"] = drReal["체결시간"];
                dr["종료시간"] = drReal["체결시간"];
                dr["COUNT"] = 1;
                dr["매수유무"] = 0;
                dr["매도유무"] = 0;
                dr["매수신호"] = 0;
                dr["매도신호"] = 0;

            }
            else
            {
                DataRow[] drArr = dt.Select("Convert(COUNT , 'System.Int32') < 30");
                if (drArr.Length < 1)
                {
                    if (dt.Rows.Count >= 48) {
                        DataRow drR = dt.Rows[0];
                        if(drR["매수신호"].Equals(1)) {
                            var obj = (from DataGridViewRow dgr in dg뉴스체결.Rows
                                       where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(dt.TableName)
                                       select dgr).FirstOrDefault();
                            var objMi = (from DataGridViewRow dgr in dg미체결.Rows
                                       where dgr.Cells[M종목코드.Index].Value != null && dgr.Cells[M종목코드.Index].Value.Equals(dt.TableName)
                                       select dgr).FirstOrDefault();
                            if (obj != null && objMi != null)
                            {
                                try { 
                                    string stockName = obj.Cells[N종목명.Index].Value.ToString();
                                    string monPrice = obj.Cells[N모니터링가격.Index].Value.ToString();
                                    string monGb = obj.Cells[N모니터링구분.Index].Value.ToString();
                                    CancelAll(obj.Cells[N종목코드.Index].Value.ToString());
                                    dg뉴스체결.Rows.Remove(obj);
                                    NewsFavAdd(dt.TableName, "Y" , monPrice, monGb , 0 , 0 , 0 ,0);
                                }
                                catch { }
                            }
                        }
                        dt.Rows.Remove(drR); 
                    }

                    dr = dt.Rows.Add();
                    dr["일자"] = DateTime.Now.ToString("yyyyMMdd");
                    dr["현재가"] = price;
                    dr["시가"] = price;
                    dr["고가"] = price;
                    dr["저가"] = price;
                    dr["등락율"] = drReal["등락율"];
                    dr["체결강도"] = drReal["체결강도"];
                    if (!isBuy)
                    {
                        dr["매수거래량"] = 0;
                        dr["매도거래량"] = volume * -1;
                        dr["매수거래비용"] = 0;
                        dr["매도거래비용"] = tradingprice * -1;
                    }
                    else
                    {
                        dr["매수거래량"] = volume;
                        dr["매도거래량"] = 0;
                        dr["매수거래비용"] = tradingprice;
                        dr["매도거래비용"] = 0;
                    }
                    dr["시작시간"] = drReal["체결시간"];
                    dr["종료시간"] = drReal["체결시간"];
                    dr["COUNT"] = 1;
                    dr["매수유무"] = 0;
                    dr["매도유무"] = 0;
                    dr["매수신호"] = 0;
                    dr["매도신호"] = 0;
                }
                else
                {
                    dr = drArr[0];
                    dr["현재가"] = price;
                    if (price > Cls.ValInt(dr["고가"]))
                    {
                        dr["고가"] = price;
                    }
                    if (price < Cls.ValInt(dr["저가"]))
                    {
                        dr["저가"] = price;
                    }

                    dr["등락율"] = drReal["등락율"];
                    dr["체결강도"] = drReal["체결강도"];
                    if (!isBuy)
                    {
                        dr["매도거래량"] = Cls.ValInt(dr["매도거래량"]) - volume;
                        dr["매도거래비용"] = Cls.ValInt(dr["매도거래비용"]) - tradingprice;
                    }
                    else
                    {
                        dr["매수거래량"] = Cls.ValInt(dr["매수거래량"]) + volume;
                        dr["매수거래비용"] = Cls.ValInt(dr["매수거래비용"]) + tradingprice;
                    }
                    dr["종료시간"] = drReal["체결시간"];
                    dr["COUNT"] = Cls.ValInt(dr["COUNT"]) + 1;

                    if (dr["COUNT"].Equals(30))
                    {
                        if (Cls.ValInt(dr["매수거래비용"]) + Cls.ValInt(dr["매도거래비용"]) < Cls.ValInt(nm조건거래대금.Value) * 1000000 / 3)
                        {
                            dr["매수신호"] = 0;
                        }
                        else if (Cls.ValInt(dr["매수거래비용"]) + Cls.ValInt(dr["매도거래비용"]) < Cls.ValInt(nm조건거래대금.Value) * 1000000 * -1)
                        {
                            dr["매도신호"] = 1;
                        }
                    } 
                }
                SettingTickLine(dr, dt);
            }
        }

        private void SettingTickLine(DataRow dr, DataTable dt)
        {
            Double[] sum = { 0, 0, 0, 0, 0 }; // 5 , 10  , 20 , 40 , 60
            Double[] avg = { 0, 0, 0, 0, 0 };// 5 , 10  , 20 , 40 , 60

            DataView dv = new DataView(dt);
            dv.Sort = "일자 DESC , 시작시간 DESC";
            Double pwSum = 0;
            if (dv.Count >= 5)
            {
                for (int row = 0; row < 5; row++)
                {

                    sum[0] += Cls.Val(dv[row]["현재가"].ToString());
                }
                dr["LINE5"] = (sum[0] / 5).ToString("#,##0.00");
            }
            //if (dv.Count >= 10)
            //{
            //    for (int row = 0; row < 10; row++)
            //    {

            //        sum[1] += Cls.Val(dv[row]["현재가"].ToString());
            //    }
            //    dr["LINE10"] = (sum[1] / 10).ToString("#,##0.00");
            //}
            //if (dv.Count >= 20)
            //{
            //    for (int row = 0; row < 20; row++)
            //    {

            //        sum[2] += Cls.Val(dv[row]["현재가"].ToString());
            //    }
            //    dr["LINE20"] = (sum[2] / 20).ToString("#,##0.00");
            //}

            if (dv.Count >= 40)
            {
                for (int row = 0; row < 40; row++)
                {
                    sum[3] += Cls.Val(dv[row]["현재가"].ToString());
                }
                dr["LINE40"] = (sum[3] / 40).ToString("#,##0.00");

                pwSum = 0;
                for (int row = 0; row < 40; row++)
                {
                    pwSum += Math.Pow(Cls.Val((sum[3] / 40).ToString("0.00")) - Cls.Val(dv[row]["현재가"].ToString()), 2);
                }
                dr["BBUP"] = (Cls.Val((sum[3] / 40).ToString("0.00")) + 2 * Math.Sqrt(pwSum / 40.0)).ToString("0.00");
                dr["BBDOWN"] = (Cls.Val((sum[3] / 40).ToString("0.00")) - 2 * Math.Sqrt(pwSum / 40.0)).ToString("0.00");
            }
            else
            {
                dr["LINE40"] = 0;
                dr["BBUP"] = 0;
                dr["BBDOWN"] = 0;
            }

            //if (dv.Count >= 60)
            //{
            //    for (int row = 0; row < 60; row++)
            //    {

            //        sum[4] += Cls.Val(dv[row]["현재가"].ToString());
            //    }
            //    dr["LINE60"] = (sum[4] / 60).ToString("0.00");
            //}


        }

        private void SettingFavData(DataSet ds)
        {
            string 현재가;
            string 시가;
            string 저가;
            string 고가;
            string 종목코드;
            int row = -1;

            if (ds == null) {  }
            if (ds.Tables.Count < 1) {  }
            if (ds.Tables[0].Rows.Count < 1) {  }

            DataRow dr = ds.Tables[0].Rows[0];
            종목코드 = dr["STOCK_CODE"].ToString().Trim();

            var obj = (from DataGridViewRow dgr in dg관종.Rows
                       where dgr.Cells[F_종목코드.Index].Value != null && dgr.Cells[F_종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;
                현재가 = Math.Abs(Cls.Val(dr["현재가"].ToString())).ToString("#,##0").Trim();
                시가 = Math.Abs(Cls.Val(dr["시가"].ToString())).ToString("#,##0").Trim();
                저가 = Math.Abs(Cls.Val(dr["저가"].ToString())).ToString("#,##0").Trim();
                고가 = Math.Abs(Cls.Val(dr["고가"].ToString())).ToString("#,##0").Trim();
                dg관종.Rows[row].Cells["F_거래량"].Value = Cls.Val(dr["누적거래량"].ToString()).ToString("#,##0").Trim();
                dg관종.Rows[row].Cells["F_거래대금"].Value = (Int64.Parse(dr["누적거래대금"].ToString()) * 1000000).ToString("#,##0").Trim();
                dg관종.Rows[row].Cells["F_체결강도"].Value = Cls.Val(dr["체결강도"].ToString()).ToString("0.00").Trim();
                dg관종.Rows[row].Cells["F_시가"].Value = 시가;
                dg관종.Rows[row].Cells["F_저가"].Value = 저가;
                dg관종.Rows[row].Cells["F_고가"].Value = 고가;
                dg관종.Rows[row].Cells["F_시간"].Value = dr["체결시간"].ToString();

                if (DateTime.Now.Hour >= 8 && DateTime.Now.Minute >= 10 && DateTime.Now.Hour < 9)
                {

                }
                else
                {

                }
                dg관종.Rows[row].Cells["F_등락율"].Value = Cls.Val(dr["등락율"].ToString()).ToString("0.00").Trim();
                dg관종.Rows[row].Cells["F_현재가"].Value = 현재가;
                dg관종.Rows[row].Cells["F_대비"].Value = Cls.Val(dr["전일대비"].ToString()).ToString("#,##0").Trim();
            }


        }

        private void SettingOPT10001(DataSet ds)
        {
            DataView dv = new DataView(ds.Tables[0]);
            if (dv.Count < 1) return;
            DataRowView dr = dv[0];

            string 종목코드 = dr["종목코드"].ToString().Trim();
            Double 등락률 = Cls.Val(dr["등락율"].ToString().Trim());
            int 거래량 = Cls.ValInt(dr["거래량"].ToString().Trim());
            int 현재가 = Math.Abs(Cls.ValInt(dr["현재가"].ToString().Trim()));
            int 고가 = Math.Abs(Cls.ValInt(dr["고가"].ToString().Trim()));
            int 시가 = Math.Abs(Cls.ValInt(dr["시가"].ToString().Trim()));
            int 저가 = Math.Abs(Cls.ValInt(dr["저가"].ToString().Trim()));
            int 대비 = Cls.ValInt(dr["전일대비"].ToString().Trim());
            int 주문가 = 현재가;
            int 매수금액 = Cls.ValInt(nm공시매수금액.Value.ToString());
            int 매수수량 = 매수금액 / 주문가;

            //TAB 0 처리 - S
            for (int row = 0; row < dgN관종.RowCount - 1; row++)
            {
                if (dgN관종.Rows[row].Cells[P종목코드.Index].Value.ToString().Trim() == 종목코드)
                {
                    dgN관종.Rows[row].Cells["P현재가"].Value = 현재가.ToString("#,##0");
                    dgN관종.Rows[row].Cells["P등락률"].Value = 등락률;
                    dgN관종.Rows[row].Cells["P체결강도"].Value = ""; //주식시세표요청에서는 체결강도를 넘겨주지 않는다.
                    dgN관종.Rows[row].Cells["P거래량"].Value = 거래량.ToString("#,##0");
                    //dgN관종.Rows[row].Cells["P거래대금"].Value = 거래대금.ToString("#,##0");
                    dgN관종.Rows[row].Cells["P최초등락률"].Value = 등락률;
                    dgN관종.Rows[row].Cells["P최초체결강도"].Value = "";//주식시세표요청에서는 체결강도를 넘겨주지 않는다.
                    dgN관종.Rows[row].Cells["P최초거래량"].Value = 거래량.ToString("#,##0");
                    //dgN관종.Rows[row].Cells["P최초거래대금"].Value = 거래대금.ToString("#,##0");
                    //dgN관종.Rows[row].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            //TAB 0 처리 - E

            //TAB 2 처리 - S
            if (tbStockList.SelectedIndex == 2)
            {
                for (int row = 0; row < dg조건종목1.RowCount - 1; row++)
                {
                    if (dg조건종목1.Rows[row].Cells[C종목코드.Index].Value.ToString().Trim() == 종목코드)
                    {
                        dg조건종목1.Rows[row].Cells[C현재가.Index].Value = 현재가.ToString("#,##0");
                        dg조건종목1.Rows[row].Cells[C대비.Index].Value = 대비;
                        dg조건종목1.Rows[row].Cells[C등락률.Index].Value = 등락률;
                        dg조건종목1.Rows[row].Cells[C거래량.Index].Value = 거래량.ToString("#,##0");
                        dg조건종목1.Rows[row].Cells[C시가.Index].Value = 시가.ToString("#,##0");
                        dg조건종목1.Rows[row].Cells[C고가.Index].Value = 고가.ToString("#,##0");
                        dg조건종목1.Rows[row].Cells[C저가.Index].Value = 저가.ToString("#,##0");
                    }
                }
            }
            //TAB 2 처리 - E

            //TAB 3 관심종목 처리 -S
            if (tbStockList.SelectedIndex == 3)
            {
                for (int row = 0; row < dg관종.RowCount - 1; row++)
                {
                    if (dg관종.Rows[row].Cells[F_종목코드.Index].Value.ToString() == 종목코드)
                    {
                        
                        dg관종.Rows[row].Cells["F_등락율"].Value = 등락률;
                        dg관종.Rows[row].Cells["F_거래량"].Value = 거래량.ToString("#,##0");
                        dg관종.Rows[row].Cells["F_시가"].Value = 시가.ToString("#,##0");
                        dg관종.Rows[row].Cells["F_고가"].Value = 고가.ToString("#,##0");
                        dg관종.Rows[row].Cells["F_저가"].Value = 저가.ToString("#,##0");
                        dg관종.Rows[row].Cells["F_체결강도"].Value = "0";
                        dg관종.Rows[row].Cells["F_시간"].Value = DateTime.Now.ToString("HH:mm:ss");
                        dg관종.Rows[row].Cells["F_현재가"].Value = 현재가.ToString("#,##0");
                        dg관종.Rows[row].Cells["F_대비"].Value = 대비.ToString("#,##0");
                    }
                }
            }
            //TAB 3 관심종목 처리 -E
        }

        private void SettingOPT10007(DataSet ds)
        {
            int row = -1;
            DataView dv = new DataView(ds.Tables[0]);
            if (dv.Count < 1) return;
            DataRowView dr = dv[0];

            string 종목코드 = dr["종목코드"].ToString().Trim();
            if (종목코드 == "") return;

            Double 등락률 = Cls.Val(dr["등락률"].ToString().Trim());
            int 거래량 = Cls.ValInt(dr["거래량"].ToString().Trim());
            int 거래대금 = Cls.ValInt(dr["거래대금"].ToString().Trim());  //단위 백만원
            int 현재가 = Math.Abs(Cls.ValInt(dr["현재가"].ToString().Trim()));
            int 고가 = Math.Abs(Cls.ValInt(dr["고가"].ToString().Trim()));
            int 시가 = Math.Abs(Cls.ValInt(dr["시가"].ToString().Trim()));
            int 저가 = Math.Abs(Cls.ValInt(dr["저가"].ToString().Trim()));
            int 대비 = 현재가 - Cls.ValInt(dr["전일종가"].ToString().Trim());
            int 주문가 = 현재가;
            int 매수금액 = Cls.ValInt(nm공시매수금액.Value.ToString());
            int 매수수량 = 매수금액 / 주문가;
            int 예상대비 = 0;
            int 예상체결가 = 0;
            int 전일종가 = Math.Abs(Cls.ValInt(dr["전일종가"].ToString()));
            Double 예상등락률 = 0.0;

            예상체결가 = Math.Abs(Cls.ValInt(dr["예상체결가"].ToString()));
            if (((DateTime.Now.Hour >= 8 && DateTime.Now.Minute >= 10 && DateTime.Now.Hour < 9) ||
                (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 30))
                && 예상체결가 != 0)
            {
                예상대비 = 예상체결가 - 전일종가;
                예상등락률 = Cls.Val(예상대비) / Cls.Val(전일종가) * 100.00;
                현재가 = 예상체결가;
            }

            //장전후 거래 - S            
            //string 거래구분 = "";
            //if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:20:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:40:00")))
            //{
            //    거래구분 = "61";
            //}
            //else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:00:00")))
            //{
            //    거래구분 = "81";
            //}
            ////else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:00:00")) && DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 18:00:00")))
            ////{
            ////    거래구분 = "62";
            ////}
            //else
            //{
            //    거래구분 = "03";
            //}

            ////시장가 매수 체결된 정보의 주문가랑 조금은 틀릴수 있음 -- 정보요청을 덜하기 위한 수단임
            //DataView dvDart = new DataView(_dtDart);
            //dvDart.RowFilter = String.Format("종목코드 = '{0}' AND 제목 LIKE '%3자배정%'", 종목코드);
            //dvDart.Sort = "시간 DESC";
            //if (거래구분 != "")
            //{
            //    if (dvDart.Count > 0)
            //    {
            //        if (dvDart[0]["제목"].ToString().IndexOf("자율") < 0 && dvDart[0]["제목"].ToString().IndexOf("정정") < 0 && dvDart[0]["제목"].ToString().IndexOf("추가상장") < 0)
            //        {
            //            SendBuySellMsg(종목코드, 거래구분, 1, 0, 주문가, 매수수량, "1");
            //        }
            //    }
            //}
            //장전후 거래 - E

            //TAB 0 처리 - S
            var obj = (from DataGridViewRow dgr in dg뉴스잔고.Rows
                       where dgr.Cells[J종목코드.Index].Value != null && dgr.Cells[J종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();

            if (obj != null)
            {
                var dgr = obj;
                dgr.Cells[J현재가.Index].Value = 현재가;
                dgr.Cells[J등락율.Index].Value = 등락률;
                dgr.Cells[J대비.Index].Value = 대비;
            }
            //TAB 0 처리 - E

            //TAB 2 처리 - S
            if (tbStockList.SelectedIndex == 2)
            {
                obj = (from DataGridViewRow dgr in dg조건종목1.Rows
                       where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();
                if (obj != null)
                {
                    row = ((DataGridViewRow)obj).Index;
                    dg조건종목1.Rows[row].Cells[C현재가.Index].Value = 현재가.ToString("#,##0");
                    if (((DateTime.Now.Hour >= 8 && DateTime.Now.Minute >= 10 && DateTime.Now.Hour < 9) ||
                        (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 30))
                        && 예상체결가 != 0)
                    {
                        dg조건종목1.Rows[row].Cells[C대비.Index].Value = 예상대비;
                        dg조건종목1.Rows[row].Cells[C등락률.Index].Value = 예상등락률.ToString("0.00");
                    }
                    else
                    {
                        dg조건종목1.Rows[row].Cells[C대비.Index].Value = 대비;
                        dg조건종목1.Rows[row].Cells[C등락률.Index].Value = 등락률.ToString("0.00");
                    }
                    
                    dg조건종목1.Rows[row].Cells[C거래량.Index].Value = 거래량.ToString("#,##0");
                    dg조건종목1.Rows[row].Cells[C시가.Index].Value = 시가.ToString("#,##0");
                    dg조건종목1.Rows[row].Cells[C고가.Index].Value = 고가.ToString("#,##0");
                    dg조건종목1.Rows[row].Cells[C저가.Index].Value = 저가.ToString("#,##0");
                }

                obj = (from DataGridViewRow dgr in dg조건종목2.Rows
                       where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();
                if (obj != null)
                {
                    row = ((DataGridViewRow)obj).Index;
                    dg조건종목2.Rows[row].Cells[C현재가.Index].Value = 현재가.ToString("#,##0");
                    if (((DateTime.Now.Hour >= 8 && DateTime.Now.Minute >= 10 && DateTime.Now.Hour < 9) ||
                        (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 30))
                        && 예상체결가 != 0)
                    {
                        dg조건종목2.Rows[row].Cells[C대비.Index].Value = 예상대비;
                        dg조건종목2.Rows[row].Cells[C등락률.Index].Value = 예상등락률;
                    }
                    else { 
                        dg조건종목2.Rows[row].Cells[C대비.Index].Value = 대비;
                        dg조건종목2.Rows[row].Cells[C등락률.Index].Value = 등락률;
                    }
                   
                    dg조건종목2.Rows[row].Cells[C거래량.Index].Value = 거래량.ToString("#,##0");
                    dg조건종목2.Rows[row].Cells[C시가.Index].Value = 시가.ToString("#,##0");
                    dg조건종목2.Rows[row].Cells[C고가.Index].Value = 고가.ToString("#,##0");
                    dg조건종목2.Rows[row].Cells[C저가.Index].Value = 저가.ToString("#,##0");
                }

                obj = (from DataGridViewRow dgr in dg조건종목3.Rows
                       where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();
                if (obj != null)
                {
                    row = ((DataGridViewRow)obj).Index;
                    dg조건종목3.Rows[row].Cells[C현재가.Index].Value = 현재가.ToString("#,##0");
                    if (((DateTime.Now.Hour >= 8 && DateTime.Now.Minute >= 10 && DateTime.Now.Hour < 9) ||
                        (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 30))
                        && 예상체결가 != 0)
                    {
                        dg조건종목3.Rows[row].Cells[C대비.Index].Value = 예상대비;
                        dg조건종목3.Rows[row].Cells[C등락률.Index].Value = 예상등락률.ToString("0.00");
                    }
                    else
                    {
                        dg조건종목3.Rows[row].Cells[C대비.Index].Value = 대비;
                        dg조건종목3.Rows[row].Cells[C등락률.Index].Value = 등락률.ToString("0.00");
                    }
                    
                    dg조건종목3.Rows[row].Cells[C거래량.Index].Value = 거래량.ToString("#,##0");
                    dg조건종목3.Rows[row].Cells[C시가.Index].Value = 시가.ToString("#,##0");
                    dg조건종목3.Rows[row].Cells[C고가.Index].Value = 고가.ToString("#,##0");
                    dg조건종목3.Rows[row].Cells[C저가.Index].Value = 저가.ToString("#,##0");
                }

                obj = (from DataGridViewRow dgr in dg조건종목4.Rows
                       where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();
                if (obj != null)
                {
                    row = ((DataGridViewRow)obj).Index;
                    dg조건종목4.Rows[row].Cells[C현재가.Index].Value = 현재가.ToString("#,##0");
                    if (((DateTime.Now.Hour >= 8 && DateTime.Now.Minute >= 10 && DateTime.Now.Hour < 9) ||
                        (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 30))
                        && 예상체결가 != 0)
                    {
                        dg조건종목4.Rows[row].Cells[C대비.Index].Value = 예상대비;
                        dg조건종목4.Rows[row].Cells[C등락률.Index].Value = 예상등락률.ToString("0.00");
                    }
                    else
                    {
                        dg조건종목4.Rows[row].Cells[C대비.Index].Value = 대비;
                        dg조건종목4.Rows[row].Cells[C등락률.Index].Value = 등락률.ToString("0.00");
                    }
                    
                    dg조건종목4.Rows[row].Cells[C거래량.Index].Value = 거래량.ToString("#,##0");
                    dg조건종목4.Rows[row].Cells[C시가.Index].Value = 시가.ToString("#,##0");
                    dg조건종목4.Rows[row].Cells[C고가.Index].Value = 고가.ToString("#,##0");
                    dg조건종목4.Rows[row].Cells[C저가.Index].Value = 저가.ToString("#,##0");
                }

                obj = (from DataGridViewRow dgr in dg조건종목5.Rows
                       where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();
                if (obj != null)
                {
                    row = ((DataGridViewRow)obj).Index;
                    dg조건종목5.Rows[row].Cells[C현재가.Index].Value = 현재가.ToString("#,##0");
                    if (((DateTime.Now.Hour >= 8 && DateTime.Now.Minute >= 10 && DateTime.Now.Hour < 9) ||
                        (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 30))
                        && 예상체결가 != 0)
                    {
                        dg조건종목5.Rows[row].Cells[C대비.Index].Value = 예상대비;
                        dg조건종목5.Rows[row].Cells[C등락률.Index].Value = 예상등락률.ToString("0.00");
                    }
                    else
                    {
                        dg조건종목5.Rows[row].Cells[C대비.Index].Value = 대비;
                        dg조건종목5.Rows[row].Cells[C등락률.Index].Value = 등락률.ToString("0.00");
                    }
                    dg조건종목5.Rows[row].Cells[C거래량.Index].Value = 거래량.ToString("#,##0");
                    dg조건종목5.Rows[row].Cells[C시가.Index].Value = 시가.ToString("#,##0");
                    dg조건종목5.Rows[row].Cells[C고가.Index].Value = 고가.ToString("#,##0");
                    dg조건종목5.Rows[row].Cells[C저가.Index].Value = 저가.ToString("#,##0");
                }
            }
            //TAB 2 처리 - E

            //TAB 3 관심종목 처리 -S
            if (tbStockList.SelectedIndex == 3)
            {
                obj = (from DataGridViewRow dgr in dg관종.Rows
                       where dgr.Cells[F_종목코드.Index].Value != null && dgr.Cells[F_종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();
                if (obj != null)
                {
                    row = ((DataGridViewRow)obj).Index;

                    if (dg관종.Rows[row].Cells["F_체결강도"].Value != null) return;

                    if (((DateTime.Now.Hour >= 8 && DateTime.Now.Minute >= 10 && DateTime.Now.Hour < 9) ||
                        (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 30))
                        && 예상체결가 != 0)
                    {
                        dg관종.Rows[row].Cells["F_등락율"].Value = 예상등락률.ToString("0.00");
                    }
                    else
                    {
                        dg관종.Rows[row].Cells["F_등락율"].Value = 등락률.ToString("0.00");
                    }
                    
                    dg관종.Rows[row].Cells["F_거래량"].Value = 거래량.ToString("#,##0");
                    dg관종.Rows[row].Cells["F_거래대금"].Value = (Convert.ToInt64(거래대금) * 1000000).ToString("#,##0");
                    dg관종.Rows[row].Cells["F_시가"].Value = 시가.ToString("#,##0");
                    dg관종.Rows[row].Cells["F_고가"].Value = 고가.ToString("#,##0");
                    dg관종.Rows[row].Cells["F_저가"].Value = 저가.ToString("#,##0");
                    dg관종.Rows[row].Cells["F_체결강도"].Value = "0";
                    dg관종.Rows[row].Cells["F_시간"].Value = DateTime.Now.ToString("HH:mm:ss");
                    dg관종.Rows[row].Cells["F_현재가"].Value = 현재가.ToString("#,##0");
                    if (((DateTime.Now.Hour >= 8 && DateTime.Now.Minute >= 10 && DateTime.Now.Hour < 9) ||
                        (DateTime.Now.Hour >= 15 && DateTime.Now.Minute >= 20 && DateTime.Now.Minute < 30))
                        && 예상체결가 != 0)
                    {
                        dg관종.Rows[row].Cells["F_대비"].Value = 예상대비.ToString("#,##0");
                    }
                    else
                    {
                        dg관종.Rows[row].Cells["F_대비"].Value = 대비.ToString("#,##0");
                    }
                    
                }
            }
            //TAB 3 관심종목 처리 -E
        }

        private DataTable SetLine(string 종목코드)
        {
            DataSet ds;
            DataTable dt = new DataTable();
            DataRow dr;
            DataRow[] drArr;
            drArr = _ds전체일봉.Tables[0].Select(String.Format("STOCK_CODE = '{0}'", 종목코드));
            //if (drArr.Length < 1)
            //{
            //    ds = _DataAcc.p_stock_day_data_query_Line2("1", 종목코드, true, null, null);
            //    ds.Tables[0].Columns.Add("B_UP");
            //    ds.Tables[0].Columns.Add("B_DOWN");

            //    if (ds.Tables.Count > 1)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            ds.Tables[0].Rows[0]["B_UP"] = ds.Tables[1].Rows[0]["B_LINEUP"].ToString().Trim();
            //            ds.Tables[0].Rows[0]["B_DOWN"] = ds.Tables[1].Rows[0]["B_LINEDOWN"].ToString().Trim();
            //        }
            //    }
            //}
            //else
            //{
                ds = new DataSet();
                dt.Columns.Add("end_price", Type.GetType("System.Int32"));
                dt.Columns.Add("rate", Type.GetType("System.Double"));
                dt.Columns.Add("daebi", Type.GetType("System.Int32"));
                dt.Columns.Add("s_price", Type.GetType("System.Int32"));
                dt.Columns.Add("h_price", Type.GetType("System.Int32"));
                dt.Columns.Add("l_price", Type.GetType("System.Int32"));
                dt.Columns.Add("volume", Type.GetType("System.Int32"));
                dt.Columns.Add("trading_value", Type.GetType("System.Int64"));
                dt.Columns.Add("line3", Type.GetType("System.String"));
                dt.Columns.Add("line5", Type.GetType("System.String"));
                dt.Columns.Add("line10", Type.GetType("System.String"));
                dt.Columns.Add("line15", Type.GetType("System.String"));
                dt.Columns.Add("line20", Type.GetType("System.String"));
                dt.Columns.Add("line40", Type.GetType("System.String"));
                dt.Columns.Add("line60", Type.GetType("System.String"));
                dt.Columns.Add("line120", Type.GetType("System.String"));
                dt.Columns.Add("line220", Type.GetType("System.String"));
                dt.Columns.Add("line240", Type.GetType("System.String"));
                dt.Columns.Add("MA_H", Type.GetType("System.String"));
                dt.Columns.Add("MA_C", Type.GetType("System.String"));
                dt.Columns.Add("MA_L", Type.GetType("System.String"));
                dt.Columns.Add("B_UP", Type.GetType("System.String"));
                dt.Columns.Add("B_DOWN", Type.GetType("System.String"));

                dr = dt.Rows.Add();
                if(drArr.Length < 1 ) {
                    for(int col = 0 ; col < dt.Columns.Count ; col++) {
                        dr[col] = "0";
                    }
                }else {
                    dr["end_price"] = drArr[0]["end_price"];
                    dr["rate"] = drArr[0]["rate"];
                    dr["daebi"] = drArr[0]["daebi"];
                    dr["s_price"] = drArr[0]["s_price"];
                    dr["h_price"] = drArr[0]["h_price"];
                    dr["l_price"] = drArr[0]["l_price"];
                    dr["volume"] = drArr[0]["volume"];
                    dr["trading_value"] = Convert.ToInt64(drArr[0]["trading_value"].ToString()) * 1000000;
                    dr["line3"] = drArr[0]["line3s"];
                    dr["line5"] = drArr[0]["line5s"];
                    dr["line10"] = drArr[0]["line10s"];
                    dr["line15"] = drArr[0]["line15s"];
                    dr["line20"] = drArr[0]["line20s"];
                    dr["line40"] = drArr[0]["line40s"];
                    dr["line60"] = drArr[0]["line60s"];
                    dr["line120"] = drArr[0]["line120s"];
                    dr["line220"] = drArr[0]["line220s"];
                    dr["line240"] = drArr[0]["line240s"];
                    dr["MA_H"] = drArr[0]["H_LOWEND_MA"];
                    dr["MA_L"] = drArr[0]["LOWEND_MA"];
                    if (Cls.IsNumeric(dr["MA_H"].ToString()) == true)
                        dr["MA_C"] = (Cls.Val(dr["MA_H"].ToString()) + Cls.Val(dr["MA_L"].ToString())) / 2.0;
                    else
                        dr["MA_C"] = "";
                    dr["B_UP"] = drArr[0]["bbup"];
                    dr["B_DOWN"] = drArr[0]["bbdown"];
                }
                ds.Tables.Add(dt);
                return ds.Tables[0];

                //if (drArr.Length > 2)
                //    dr["line3"] = _ds전체일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[1]["STOCK_DATE"].ToString()));
                //else
                //    dr["line3"] = "";
                //if (drArr.Length > 4)
                //    dr["line5"] = _ds전체일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[3]["STOCK_DATE"].ToString()));
                //else
                //    dr["line5"] = "";

                //if (drArr.Length > 9)
                //    dr["line10"] = _ds전체일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[8]["STOCK_DATE"].ToString()));
                //else
                //    dr["line10"] = "";

                //if (drArr.Length > 14)
                //    dr["line15"] = _ds전체일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[13]["STOCK_DATE"].ToString()));
                //else
                //    dr["line15"] = "";

                //if (drArr.Length > 19)
                //    dr["line20"] = _ds전체일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[18]["STOCK_DATE"].ToString()));
                //else
                //    dr["line20"] = "";

                //if (drArr.Length > 39)
                //    dr["line40"] = _ds전체일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[38]["STOCK_DATE"].ToString()));
                //else
                //    dr["line40"] = "";

                //if (drArr.Length > 59)
                //    dr["line60"] = _ds전체일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[58]["STOCK_DATE"].ToString()));
                //else
                //    dr["line60"] = "";

                //dr["MA_H"] = _ds전체일봉.Tables[0].Compute("SUM(H_LOWEND_MA)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[0]["STOCK_DATE"].ToString()));
                //dr["MA_L"] = _ds전체일봉.Tables[0].Compute("SUM(LOWEND_MA)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[0]["STOCK_DATE"].ToString()));
                //if (Cls.IsNumeric(dr["MA_H"].ToString()) == true)
                //{
                //    dr["MA_C"] = (Cls.Val(dr["MA_H"].ToString()) + Cls.Val(dr["MA_L"].ToString())) / 2.0;
                //}
                //else
                //{
                //    dr["MA_C"] = "";
                //}


                //if (drArr.Length >= 41)
                //{
                //    Double tempBB = 0;
                //    Double ma = Convert.ToDouble(_ds전체일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[40]["STOCK_DATE"].ToString())).ToString()) / 41;
                //    DataRow drBB;
                //    for (int row = 0; row < 41; row++)
                //    {
                //        drBB = drArr[row];
                //        tempBB += Math.Pow(ma - Cls.Val(drBB["END_PRICE"].ToString()), 2);
                //    }
                //    dr["B_UP"] = ma + 2 * Math.Sqrt(tempBB / 41);
                //    dr["B_DOWN"] = ma - 2 * Math.Sqrt(tempBB / 41);
                //}
                //else
                //{
                //    dr["B_UP"] = "";
                //    dr["B_DOWN"] = "";
                //}

                //ds.Tables.Add(dt);
            //}
            //return ds.Tables[0];
        }

        private DataTable SetBuySellState(string 종목코드)
        {
            DataSet ds;
            DataTable dt = new DataTable();
            DataRow dr, dr1, drBuy;

            ds = new DataSet();
            dt.Columns.Add("F_TU", Type.GetType("System.Int32"));
            dt.Columns.Add("K_TU", Type.GetType("System.Int32"));
            
            dr = dt.Rows.Add();
            DataRow[] drArr = _ds전체매동.Tables[0].Select(String.Format("STOCK_CODE = '{0}'", 종목코드));
            if (drArr.Length < 1)
            {
                dr["F_TU"] = 0;
                dr["K_TU"] = 0;
            }
            else { 
                drBuy = drArr[0];
                dr["F_TU"] = drBuy["F_TU"];
                dr["K_TU"] = drBuy["K_TU"];
            }

            dr1 = dt.Rows.Add();
            DataRow[] drArr1 = _ds전체매동.Tables[1].Select(String.Format("STOCK_CODE = '{0}'", 종목코드));
            if (drArr1.Length < 1)
            {
                dr1["F_TU"] = 0;
                dr1["K_TU"] = 0;
            }
            else
            {
                drBuy = drArr1[0];
                dr1["F_TU"] = drBuy["F_TU"];
                dr1["K_TU"] = drBuy["K_TU"];
            }

            ds.Tables.Add(dt);
            return ds.Tables[0];
        }

        private DataTable SetFinance(string 종목코드)
        {
            DataSet ds;
            DataTable dt = new DataTable();
            DataRow dr, drFinance;

            //if (_ds관종재정.Tables[0].Select(String.Format("STOCK_CODE = '{0}'", 종목코드)).Length < 1)
            //{
            //    ds = _DataAcc.p_stock_finance_query("1", 종목코드, true, null, null);
            //}
            //else
            //{
            ds = new DataSet();
            dt.Columns.Add("CLASS_GB", Type.GetType("System.String"));
            dt.Columns.Add("CREDIT_RATE", Type.GetType("System.String"));
            dt.Columns.Add("STOCK_TOTAL_P", Type.GetType("System.String"));
            dt.Columns.Add("PER", Type.GetType("System.String"));
            dt.Columns.Add("ROE", Type.GetType("System.String"));
            dt.Columns.Add("PBR", Type.GetType("System.String"));
            dt.Columns.Add("EV", Type.GetType("System.String"));
            dt.Columns.Add("EPS", Type.GetType("System.String"));
            dt.Columns.Add("BPS", Type.GetType("System.String"));
            dt.Columns.Add("O_PROFIT", Type.GetType("System.String"));
            dt.Columns.Add("P_PROFIT", Type.GetType("System.String"));
            dt.Columns.Add("HIGH_250", Type.GetType("System.String"));
            dt.Columns.Add("LOW_250", Type.GetType("System.String"));
            dt.Columns.Add("THEME_NAME", Type.GetType("System.String"));

            dr = dt.Rows.Add();

            DataRow[] drArr = _ds전체재정.Tables[0].Select(String.Format("STOCK_CODE = '{0}'", 종목코드));
            if (drArr.Length < 1) return null;
            drFinance = drArr[0];

            dr["CLASS_GB"] = drFinance["class_gb"];
            dr["CREDIT_RATE"] = drFinance["CREDIT_RATE"];
            dr["STOCK_TOTAL_P"] = drFinance["STOCK_TOTAL_P"];
            dr["PER"] = drFinance["PER"];
            dr["ROE"] = drFinance["ROE"];
            dr["PBR"] = drFinance["PBR"];
            dr["EV"] = drFinance["EV"];
            dr["EPS"] = drFinance["EPS"];
            dr["BPS"] = drFinance["BPS"];
            dr["O_PROFIT"] = drFinance["O_PROFIT"];
            dr["P_PROFIT"] = drFinance["P_PROFIT"];
            dr["HIGH_250"] = drFinance["HIGH_250"];
            dr["LOW_250"] = drFinance["LOW_250"];
            dr["THEME_NAME"] = drFinance["theme_name"];

            ds.Tables.Add(dt);
            //}
            return ds.Tables[0];
        }

        //DataTable 사용시 int minLavel = Cls.ValInt(dt.Compute("min(AccountLevel)", string.Empty));
        Hashtable _ht최고체결강도 = new Hashtable();

        //TAB 0 실시간데이터 처리 부분 - S
        //private void SetNewsDs(DataRow RealDr)
        //{
        //    DataTable dt = _ds뉴스.Tables[RealDr["STOCK_CODE"].ToString().Trim()];
        //    DataRow dr;

        //    if (dt == null) return;
        //    if (dt.Rows.Count < 1)
        //    {
        //        dr = dt.Rows.Add();
        //        if (Cls.ValInt(RealDr["거래량"].ToString().Trim()) > 0)
        //        {
        //            dr["매수거래량"] = RealDr["거래량"];
        //            dr["매수거래비용"] = Math.Abs(Cls.ValInt(RealDr["현재가"].ToString().Trim())) * Cls.ValInt(RealDr["거래량"].ToString().Trim());
        //            dr["매도거래량"] = 0;
        //            dr["매도거래비용"] = 0;
        //        }
        //        else
        //        {
        //            dr["매수거래량"] = 0;
        //            dr["매수거래비용"] = 0;
        //            dr["매도거래량"] = RealDr["거래량"];
        //            dr["매도거래비용"] = Math.Abs(Cls.ValInt(RealDr["현재가"].ToString().Trim())) * Cls.ValInt(RealDr["거래량"].ToString().Trim());
        //        }
        //    }
        //    else
        //    {
        //        dr = dt.Rows[0];
        //        if (Cls.ValInt(RealDr["거래량"].ToString().Trim()) > 0)
        //        {
        //            dr["매수거래량"] = Cls.ValInt(dr["매수거래량"].ToString().Trim()) + Cls.ValInt(RealDr["거래량"].ToString().Trim());
        //            dr["매수거래비용"] = Cls.ValInt(dr["매수거래비용"].ToString().Trim()) + (
        //                Math.Abs(Cls.ValInt(RealDr["현재가"].ToString().Trim())) * Cls.ValInt(RealDr["거래량"].ToString().Trim())
        //                );
        //        }
        //        else
        //        {
        //            dr["매도거래량"] = Cls.ValInt(dr["매도거래량"].ToString().Trim()) + Cls.ValInt(RealDr["거래량"].ToString().Trim());
        //            dr["매도거래비용"] = Cls.ValInt(dr["매도거래비용"].ToString().Trim()) + (
        //                 Math.Abs(Cls.ValInt(RealDr["현재가"].ToString().Trim())) * Cls.ValInt(RealDr["거래량"].ToString().Trim())
        //                );
        //        }
        //    }

        //    dr["현재가"] = RealDr["현재가"];
        //    dr["등락율"] = RealDr["등락율"];
        //    dr["체결강도"] = RealDr["체결강도"];
        //    dr["거래시간"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //}

        private void SettingNewsFav(DataSet ds)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            string 종목코드 = dr["STOCK_CODE"].ToString().Trim();
            string 종목명 = ucMainStockVer2.GetStockInfo(종목코드);

            Double avgPwRate;
            //초기값 셋팅 - S
            int 매수수량 = 0;
            int 매수금액 = 0;
            bool isBuy = false;
            
            string 거래구분 = "00";
            int 현재가 = Math.Abs(Cls.ValInt(dr["현재가"]));
            int 시가 = Math.Abs(Cls.ValInt(dr["시가"]));
            int 고가 = Math.Abs(Cls.ValInt(dr["고가"]));
            int 저가 = Math.Abs(Cls.ValInt(dr["저가"]));
            int 현재거래대금 = Cls.ValInt(dr["누적거래대금"]);
            int 현재거래량 = Cls.ValInt(dr["누적거래량"]);
            int 최우선매도호가 = Math.Abs(Cls.ValInt(dr["(최우선)매도호가"]));
            int 최우선매수호가 = Math.Abs(Cls.ValInt(dr["(최우선)매수호가"]));
            int 주문가 = 최우선매수호가;
            int 조건시간 = Cls.ValInt(nm조건시간.Value);
            int 조건거래대금 = Cls.ValInt(nm조건거래대금.Value);
            int 조건TickCount = Cls.ValInt(nmTickCount.Value);
            double 현재체결강도 = Cls.Val(dr["체결강도"]);
            double 조건체결강도 = Cls.Val(nm조건체결강도.Value);
            int 조건신호 = Cls.ValInt(nm매수신호.Value);
            //if (현재체결강도 > 100)
            //{
            //    조건체결강도 = 조건체결강도 * 2;
            //}
            //초기값 셋팅 - E

            //뉴스관종에 데이터를 Check. - S
            if (현재가 * 시가 * 고가 * 저가 == 0) { return; }
            double 매수신호가폭 = (고가 - 저가) / 저가 * 100;
            double 매수신호기준비율 = (1.00 - (매수신호가폭 * 0.1));
            if (매수신호가폭 > 5.0){매수신호기준비율 = 0.50;}
            
            //뉴스관종에 데이터를 Check. - E

            //뉴스관종여부 체크 - S
            var obj = (from DataGridViewRow dgr in dgN관종.Rows
                       where dgr.Cells[P종목코드.Index].Value != null && dgr.Cells[P종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();
            if (obj == null) return;
            //뉴스관종여부 체크 - E


            //금일 캔들 분석 - S
            if (obj.Cells[P모니터링구분.Index].Value.Equals("공"))
            {
                매수신호기준비율 = 매수신호기준비율 * 0.75;
            }
            //금일 캔들 분석 - E


            //SetNewsDs(dr);
            obj.Cells[P현재가.Index].Value = Math.Abs(현재가).ToString("#,##0");
            obj.Cells[P등락률.Index].Value = dr["등락율"].ToString().Trim();
            obj.Cells[P체결강도.Index].Value = Cls.Val(dr["체결강도"]).ToString("0.00");
            obj.Cells[P거래량.Index].Value = Cls.ValInt(dr["누적거래량"]).ToString("#,##0");
            obj.Cells[P거래대금.Index].Value = Cls.ValInt64(dr["누적거래대금"]).ToString("#,##0");

            if (Cls.Val(obj.Cells[P최초체결강도.Index].Value).Equals(0))
            {
                obj.Cells[P최초등락률.Index].Value = dr["등락율"].ToString().Trim();
                obj.Cells[P최초체결강도.Index].Value = Cls.Val(dr["체결강도"]).ToString("0.00");
                obj.Cells[P최초거래량.Index].Value = Cls.ValInt(dr["누적거래량"]).ToString("#,##0");
                obj.Cells[P최초거래대금.Index].Value = Cls.ValInt64(dr["누적거래대금"]).ToString("#,##0");
                //dgN관종.Rows[row].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return;
            }
            string 모니터링구분 = obj.Cells[P모니터링구분.Index].Value.ToString();
            if (!chk자동매매.Checked) { return; }



            if (obj.Cells[P모니터링구분.Index].Value == null)
            {
                return;
            }

            //공시외 3시 이후 체크 - S
            if (!obj.Cells[P모니터링구분.Index].Value.Equals("공") && (Cls.ValInt(DateTime.Now.ToString("HHmmss")) > 150000 || Cls.ValInt(DateTime.Now.ToString("HHmmss")) < 090000))
            {
                return;
            }
            //공시외 3시 이후 체크 - E



            double 최초체결강도 = Cls.Val(obj.Cells[P최초체결강도.Index].Value);
            int 최초거래대금 = Cls.ValInt(obj.Cells[P최초거래대금.Index].Value);
            int 최초거래량 = Cls.ValInt(obj.Cells[P최초거래량.Index].Value);
            DateTime 최초거래시간 = DateTime.Parse(obj.Cells[P최초거래시간.Index].Value.ToString().Trim());
            TimeSpan ts = DateTime.Now - 최초거래시간;
            int diffSecond = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;

            if (Cls.Val(저가) + ((Cls.Val(고가) - Cls.Val(저가)) * 매수신호기준비율) <= Cls.Val(현재가))
            {
                if (!모니터링구분.Equals("수"))
                {
                    if (diffSecond > 60)
                    {
                        if (모니터링구분.Equals("공"))
                        {
                            SetDsScreenNo("D", "1", "1", 종목코드, 종목명, false);
                        }
                        dgN관종.Rows.Remove(obj);
                    }
                }
                return;
            }

            if (모니터링구분.Equals("공"))
            {
                if (diffSecond > 조건시간)
                {
                    var tNagari = new Task(() =>
                    {
                        if (_ds전체재정.Tables[0].Select(String.Format("STOCK_CODE = '{0}' AND VOLUME > 50000", 종목코드)).Length > 0)
                            _DataAcc.p_Psi02Add("A", _stockId, 3, 종목코드, "00", "", "", null, null);
                    });
                    tNagari.Start();
                    tNagari.Wait();

                    SetDsScreenNo("D", "1", "1", 종목코드, 종목명, false);
                    dgN관종.Rows.Remove(obj);

                    return;
                }

                int rowCnt = _dsTick60.Tables[종목코드].Rows.Count;
                if (rowCnt < 1) { return; }
                if (rowCnt > 3) rowCnt = 3;

                if (현재체결강도 - 최초체결강도 <= 조건체결강도) { return; }
                if (최초체결강도 <= Cls.Val(nm최소체결강도.Value)) { return; }
                if (최초체결강도 >= Cls.Val(nm최대체결강도.Value.ToString())) { return; }
                
                int 매수거래비용 = 0;
                int 매도거래비용 = 0;

                for (int i = 1; i <= rowCnt; i++)
                {
                    매수거래비용 += Math.Abs(Cls.ValInt(_dsTick60.Tables[종목코드].Rows[rowCnt - i]["매수거래비용"]));
                    매도거래비용 += Math.Abs(Cls.ValInt(_dsTick60.Tables[종목코드].Rows[rowCnt - i]["매도거래비용"]));
                }

                int 매수매도거래비용 = 매수거래비용 - 매도거래비용;
                if (매수매도거래비용 <= (조건거래대금 * 1000000)) { return; }
                isBuy = true;
            }

            else if (obj.Cells[P강제추가여부.Index].Value.Equals("Y"))
            {
                //모니터링가격이 있을시에 설정 이상 이격이 벌어지면 매수주문들어가지 않음  - S
                try
                {
                    if (obj.Cells[P모니터링가격.Index].Value != null && Cls.Val(obj.Cells[P모니터링가격.Index].Value) > 0)
                    {
                        if (Cls.Val(현재가) >= Cls.Val(obj.Cells[P모니터링가격.Index].Value) * ((Cls.Val(nm이격.Value) * 0.01) + 1.00))
                        {
                            if (obj.Cells[P모니터링구분.Index].Value.Equals("수"))
                            {
                                return;
                            }
                            else
                            {
                                ResetSignal(종목코드, 2);
                                dgN관종.Rows.Remove(obj);
                                return;
                            }
                        }
                    }


                    if (_dsTick60.Tables[종목코드].Rows.Count > 0)
                    {
                        if (_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 1]["매수유무"].Equals(1))
                        {
                            return;
                        }
                    }
                }
                catch {
                    return;
                }
                //모니터링가격이 있을시에 설정 이상 이격이 벌어지면 매수주문들어가지 않음  - E

                if (_dsTick60.Tables[종목코드] == null && _dsTick60.Tables[종목코드].Rows.Count < 20) { return; }
                                
                //dvTick = new DataView(_dsTick60.Tables[종목코드]);
                //dvTick.Sort = "일자 DESC  , 시작시간 DESC  , 종료시간 DESC";

                try
                {
                    avgPwRate = Cls.Val(_dsTick60.Tables[종목코드].Compute("MIN(체결강도)", "일자 = '" + DateTime.Now.ToString("yyyyMMdd") + "'").ToString());

                    //개별 셋팅일경우 개별 셋팅에 맞춰 조건 셋팅한다. - S
                    try
                    {
                        if (obj.Cells[P대금.Index].Value != null && obj.Cells[P대금.Index].Value.ToString().Trim() != "")
                        {
                            조건거래대금 = Cls.ValInt(obj.Cells[P대금.Index].Value.ToString());
                            조건체결강도 = 0;
                        }
                        if (obj.Cells[PTC.Index].Value != null && obj.Cells[PTC.Index].Value.ToString().Trim() != "")
                        {
                            조건TickCount = Cls.ValInt(obj.Cells[PTC.Index].Value.ToString());
                        }

                        if (obj.Cells[P신호.Index].Value != null && obj.Cells[P신호.Index].Value.ToString().Trim() != "")
                        {
                            조건신호 = Cls.ValInt(obj.Cells[P신호.Index].Value);
                        }
                    }
                    catch { }
                    //개별 셋팅일경우 개별 셋팅에 맞춰 조건 셋팅한다. - E

                    DataRow drTickLast = _dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 1];
                    if (Cls.Val(drTickLast["COUNT"]) < 조건TickCount) { goto skip; }
                    if (현재체결강도 - avgPwRate <= 조건체결강도) { goto skip; }
                    if (Cls.Val(drTickLast["매수거래비용"]) + Cls.Val(drTickLast["매도거래비용"]) <= (조건거래대금 * 1000000)) { return; }

                    bool isBuyAlarm = false;
                    if (_dsTick60.Tables[종목코드].Rows.Count >= 3 &&
                        !_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 3]["매수신호"].Equals(1) &&
                        !_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 2]["매수신호"].Equals(1))
                    {
                        isBuyAlarm = true;
                    }
                    else if (_dsTick60.Tables[종목코드].Rows.Count == 2 &&
                        !_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 2]["매수신호"].Equals(1))
                    {
                        isBuyAlarm = true;
                    }
                    else if (_dsTick60.Tables[종목코드].Rows.Count == 1) 
                    {
                        isBuyAlarm = true;
                    }

                    if(isBuyAlarm) {
                        _dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 1]["매수신호"] = 1;
                    }

                    DataRow[] 매수신호 = _dsTick60.Tables[종목코드].Select(String.Format("매수신호 = 1"));
                    if (매수신호.Length <= 조건신호) { return; }

                    isBuy = true;
                }
                catch { return; }
            }

        skip :
            if (isBuy)
            {
                매수금액 = 0;
                int 강제호가 = Cls.ValInt(nmYHoga.Value);
                int 공시호가 = Cls.ValInt(nmDHoga.Value);

                if (obj.Cells[P모니터링구분.Index].Value.Equals("조")) 
                    매수금액 = Cls.ValInt(nm조건매수금액.Value);
                else if (obj.Cells[P모니터링구분.Index].Value.Equals("공")) 
                    매수금액 = Cls.ValInt(nm공시매수금액.Value);
                else 
                    매수금액 = Cls.ValInt(nm관종매수금액.Value);

                매수수량 = Math.Abs(매수금액 / 현재가);

                if (매수수량.Equals(0)) return;
                try
                {
                    if (obj.Cells[P강제추가여부.Index].Value.Equals("Y"))
                    {
                        DataRow drBB = _dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 1];
                        if (!cboTickStd.Text.Equals("실시간"))
                        {
                            주문가 = Cls.ValInt(_dsTick60.Tables[종목코드].Compute("MIN(" + cboTickStd.Text.Trim() + ")", "일자 = '" + DateTime.Now.ToString("yyyyMMdd") + "'").ToString()); //강제 수동 추가일경우에는 그날 급등할껄 예상하고 등록하기 때문에 그날 저점에 주문금액을 등록한다.
                        }else {
                            if (drBB["BBDOWN"] != null && !drBB["BBDOWN"].Equals(""))
                            {
                                주문가 = Cls.ValInt(drBB["BBDOWN"]);
                            }
                        }

                        //개별 셋팅일경우 개별 셋팅에 맞춰 조건 셋팅한다. - S
                        if (obj.Cells[P가격.Index].Value != null && !obj.Cells[P가격.Index].Value.Equals(""))
                        {
                            if (!obj.Cells[P가격.Index].Value.Equals("실시간"))
                            {
                                주문가 = Cls.ValInt(_dsTick60.Tables[종목코드].Compute("MIN(" + cboTickStd.Text.Trim() + ")", "일자 = '" + DateTime.Now.ToString("yyyyMMdd") + "'").ToString()); //강제 수동 추가일경우에는 그날 급등할껄 예상하고 등록하기 때문에 그날 저점에 주문금액을 등록한다.
                            }
                            else
                            {
                                if (drBB["BBDOWN"] != null && !drBB["BBDOWN"].Equals(""))
                                {
                                    주문가 = Cls.ValInt(drBB["BBDOWN"]);
                                }
                            }
                        }

                        if (obj.Cells[P호가.Index].Value != null && !obj.Cells[P호가.Index].Value.Equals(""))
                        {
                            강제호가 = Cls.ValInt(obj.Cells[P호가.Index].Value);
                        }
                        //개별 셋팅일경우 개별 셋팅에 맞춰 조건 셋팅한다. - E

                        if (Cls.Val(주문가).Equals(0)) return;
                        if (cboTickStd.Text.Equals("실시간"))
                        {
                            강제호가 = GetHoga(주문가);
                        }

                        주문가 = MakeOrderPrice(주문가);
                        SendBuySellMsg(종목코드, 거래구분, 1, 강제호가, 주문가, 매수수량, "1");
                    }
                    else
                    {
                        거래구분 = "00";

                        if (Cls.isExistsRow(dg뉴스체결, 0, obj.Cells[0].Value.ToString().Trim()) == -1)
                        {
                            if (현재체결강도 - 최초체결강도 > Cls.Val(nm조건시장가강도.Value.ToString()) &&
                                최초체결강도 > Cls.Val(nm최소체결강도.Value.ToString()) && 최초체결강도 < Cls.Val(nm최대체결강도.Value.ToString()))
                            {
                                거래구분 = "03";
                                obj.Cells[P최초거래시간.Index].Value = "2010-01-01 00:00:00";
                            }
                            SendBuySellMsg(종목코드, 거래구분, 1, 0, 주문가, 매수수량, "1");
                        }
                    }
                }
                catch { return; }

                var objOrder = (from DataGridViewRow dgr in dg뉴스체결.Rows
                                where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(종목코드)
                                select dgr).FirstOrDefault();

                if (objOrder != null) return;

                dg뉴스체결.Rows.Insert(0, 1);

                objOrder = dg뉴스체결.Rows[0];
                objOrder.Cells[N종목코드.Index].Value = obj.Cells[P종목코드.Index].Value.ToString().Trim();
                objOrder.Cells[N종목명.Index].Value = obj.Cells[P종목명.Index].Value.ToString().Trim();
                objOrder.Cells[N주문가.Index].Value = Math.Abs(주문가);
                objOrder.Cells[N현재가.Index].Value = Cls.Val(obj.Cells[P현재가.Index].Value);

                if (!dg뉴스체결.Rows[0].Cells[N주문가.Index].Value.Equals("") && !dg뉴스체결.Rows[0].Cells[N현재가.Index].Value.Equals(""))
                {
                    Double temp1 = Cls.Val(objOrder.Cells[N주문가.Index].Value);
                    Double temp2 = Cls.Val(objOrder.Cells[N현재가.Index].Value);
                    if (temp1 > 0) {
                        objOrder.Cells[N수익률.Index].Value = ((temp2 - temp1) / temp1 * 100).ToString("0.00");
                    }
                }

                objOrder.Cells[N거래량.Index].Value = obj.Cells[P거래량.Index].Value;
                objOrder.Cells[N거래대금.Index].Value = obj.Cells[P거래대금.Index].Value;
                objOrder.Cells[N체결강도.Index].Value = obj.Cells[P체결강도.Index].Value;
                objOrder.Cells[N강제추가여부.Index].Value = obj.Cells[P강제추가여부.Index].Value;
                objOrder.Cells[N매수수량.Index].Value = 매수수량;
                objOrder.Cells[N최초체결강도.Index].Value = obj.Cells[P최초체결강도.Index].Value;
                objOrder.Cells[N최초거래시간.Index].Value = obj.Cells[P최초거래시간.Index].Value;
                objOrder.Cells[N모니터링가격.Index].Value = obj.Cells[P모니터링가격.Index].Value;
                objOrder.Cells[N모니터링구분.Index].Value = obj.Cells[P모니터링구분.Index].Value;

                //if (_ds뉴스.Tables[종목코드] != null)
                //{
                //    _ds뉴스.Tables.Remove(_ds뉴스.Tables[종목코드]);
                //}
                dgN관종.Rows.Remove(obj);
            }
            //체결쪽으로 넘어간 데이터를 검수하고 RealData를 업데이트 해준다. - E
        }

        private int GetHoga(int 주문가)
        {
            int 강제호가 = 0;
            if (주문가 < 1000) 강제호가 = 2;
            else if (주문가 < 2000) 강제호가 = 0;
            else if (주문가 < 6000) 강제호가 = 1;
            else if (주문가 < 10000) 강제호가 = 2;
            else if (주문가 < 20000) 강제호가 = 0;
            else if (주문가 < 30000) 강제호가 = 1;
            else if (주문가 < 40000) 강제호가 = 2;
            else if (주문가 < 50000) 강제호가 = 2;
            else if (주문가 < 60000) 강제호가 = 0;
            else if (주문가 < 70000) 강제호가 = 1;
            else if (주문가 < 80000) 강제호가 = 2;
            else if (주문가 < 90000) 강제호가 = 2;
            else if (주문가 < 100000) 강제호가 = 2;
            else 강제호가 = 2;
            return 강제호가;
        }
        private void RemoveTickTable(string stockCode)
        {
            //if (_dsTick60.Tables[stockCode].Rows.Count < 10)
            //{
            int index = cboTickDataMember.FindString(stockCode + "-" + ucMainStockVer2.GetStockInfo(stockCode));
            if (index == -1) return;
            cboTickDataMember.Items.RemoveAt(cboTickDataMember.FindString(stockCode + "-" + ucMainStockVer2.GetStockInfo(stockCode)));
            _dsTick60.Tables.Remove(_dsTick60.Tables[stockCode]);
            //}
        }

        private void SettingNewsOrder(DataSet ds)
        {
            DataRow dr = ds.Tables[0].Rows[0];

            string 종목코드 = dr["STOCK_CODE"].ToString().Trim();
            string 종목명 = ucMainStockVer2.GetStockInfo(종목코드);

            int 현재가 = Math.Abs(Cls.ValInt(dr["현재가"].ToString().Trim()));
            
            Double 현재체결강도;
            int 현재거래대금;
            int 현재거래량;
            int 매수수량;
            int 매수금액;
            int 최우선매도호가 = Math.Abs(Cls.ValInt(dr["(최우선)매도호가"]));
            int 최우선매수호가 = Math.Abs(Cls.ValInt(dr["(최우선)매수호가"]));
            int 주문가 = 최우선매수호가;

            현재체결강도 = Cls.Val(dr["체결강도"]);
            현재거래대금 = Cls.ValInt(dr["누적거래대금"]);
            현재거래량 = Cls.ValInt(dr["누적거래량"]);

            //체결쪽으로 넘어간 데이터를 검수하고 RealData를 업데이트 해준다. - S
            var dgr = (from DataGridViewRow oDgr in dg뉴스체결.Rows
                       where oDgr.Cells[N종목코드.Index].Value != null && oDgr.Cells[N종목코드.Index].Value.Equals(종목코드)
                       select oDgr).FirstOrDefault();
            if (dgr == null) return;
            
            dgr.Cells[N현재가.Index].Value = 현재가.ToString("#,##0");
            dgr.Cells[N등락률.Index].Value = Cls.Val(dr["등락율"]).ToString("0.00");
            dgr.Cells[N체결강도.Index].Value = Cls.Val(dr["체결강도"]).ToString("0.00");
            dgr.Cells[N거래량.Index].Value = Cls.ValInt(dr["누적거래량"]).ToString("#,##0");
            dgr.Cells[N거래대금.Index].Value = Cls.ValInt(dr["누적거래대금"]).ToString("#,##0");

            double 이전주문가 = Cls.Val(dgr.Cells[N주문가.Index].Value);
            if (dgr.Cells[N주문가.Index].Value == null) return;
            if (!dgr.Cells[N주문가.Index].Value.Equals(""))
            {
                if (!이전주문가.Equals(0))
                {
                    dgr.Cells[N수익률.Index].Value = ((현재가 - 이전주문가) / 이전주문가 * 100).ToString("0.00");
                }
            }

            if (dgr.Cells[N주문번호.Index].Value == null) return;
            if (dgr.Cells[N매수수량.Index].Value == null) return;
            
            //초기값 셋팅 - S
            string 모니터링구분 = "";
            if (dgr.Cells[N모니터링구분.Index].Value != null)
                모니터링구분 = dgr.Cells[N모니터링구분.Index].Value.ToString();

            double 최초체결강도 = 999;
            if (dgr.Cells[N최초체결강도.Index].Value != null)
                최초체결강도 = Cls.Val(dgr.Cells[N최초체결강도.Index].Value);

            DateTime 최초거래시간 = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")); ;
            if (dgr.Cells[N최초거래시간.Index].Value != null) 
                최초거래시간 = DateTime.Parse(dgr.Cells[N최초거래시간.Index].Value.ToString().Trim());

            TimeSpan ts = DateTime.Now - 최초거래시간;
            int diffSecond = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
            int 조건시간 = Cls.ValInt(nm조건시간.Value);
            string 주문번호 = dgr.Cells[N주문번호.Index].Value.ToString();
            bool isNagari = false;
            //초기값 셋팅 - E

            var objMi = (from DataGridViewRow oDgr in dg미체결.Rows
                         where oDgr.Cells[M종목코드.Index].Value != null && oDgr.Cells[M종목코드.Index].Value.Equals(종목코드)
                         select oDgr).FirstOrDefault();

            if (모니터링구분.Equals("공")) {
                if (objMi != null)
                {
                    if (diffSecond > 조건시간 * 2)
                    {
                        isNagari = true;
                        goto skip;
                    }

                    if(현재체결강도 - 최초체결강도 <= Cls.Val(nm조건시장가강도.Value)) return;
                    if(최초체결강도 <= Cls.Val(nm최소체결강도.Value) && 최초체결강도 < Cls.Val(nm최대체결강도.Value)) return;
                    try
                    {
                        if (!dgr.Cells[N최초거래시간.Index].Value.Equals("2010-01-01 00:00:00"))
                        {
                            if (현재가 >= 이전주문가 * (1 + Cls.Val(nmNDRate.Value) * 0.01))
                            {
                                isNagari = true;
                                goto skip;
                            }

                            매수금액 = Cls.ValInt(nm공시매수금액.Value);
                            매수수량 = 매수금액 / 현재가;
                            SendBuySellMsg(종목코드, "00", (int)ucMainStock.OrderType.매수취소, 0, 0, Cls.ValInt(dgr.Cells[N매수수량.Index].Value), "2", 주문번호);
                            SendBuySellMsg(종목코드, "03", (int)ucMainStock.OrderType.신규매수, 0, 주문가, 매수수량, "2", "");
                            dgr.Cells[N최초거래시간.Index].Value = "2010-01-01 00:00:00";
                        }
                    }
                    catch { return ;}
                }
            }
            else
            {
                if (현재가 >= 이전주문가 * (1 + Cls.Val(nmNYRate.Value) * 0.01))
                {
                    if (objMi != null)
                    {
                        isNagari = true;
                        goto skip;
                    }
                }
                else
                {
                    if (objMi == null) return;
                    if (주문번호.Equals("")) return;
                    if (Cls.Val(dgr.Cells[N모니터링가격.Index].Value).Equals(0)) return;
                    if (Cls.Val(dgr.Cells[N현재가.Index].Value).Equals(0)) return;
                    if (_dsTick60.Tables[종목코드].Rows.Count < 3) return;
                    if (_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 3]["매수유무"].Equals(1)) return;
                    if (_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 2]["매수유무"].Equals(1)) return;
                    if (_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 1]["매수유무"].Equals(1)) return;

                    double BBDOWN = Cls.Val(_dsTick60.Tables[종목코드].Rows[_dsTick60.Tables[종목코드].Rows.Count - 1]["BBDOWN"]);
                    if (BBDOWN > 0)
                    {
                        매수수량 = Cls.ValInt(dgr.Cells[N매수수량.Index].Value);
                        if (BBDOWN < 이전주문가 && Cls.Val(주문번호) > 0)
                        {
                            주문가 = MakeOrderPrice(Cls.ValInt(BBDOWN));
                            int 강제호가 = GetHoga(주문가);

                            CancelAll(종목코드);
                            SendBuySellMsg(종목코드, "00", (int)ucMainStock.OrderType.신규매수, 강제호가, 주문가, 매수수량, "2", "");
                        }
                    }
                }
            }

    skip :
            if (isNagari)
            {
                매수수량 = Cls.ValInt(dgr.Cells[N매수수량.Index].Value);
                SendBuySellMsg(종목코드, "00", (int)ucMainStock.OrderType.매수취소, 0, 0, 매수수량, "2", 주문번호);

                string monGb = dgr.Cells[N모니터링구분.Index].Value.ToString();
                string monprice = dgr.Cells[N모니터링가격.Index].Value.ToString();
                CancelAll(종목코드);
                dg뉴스체결.Rows.Remove(dgr);

                if (!monGb.Equals("공")) { 
                    ResetSignal(종목코드, 2);
                    NewsFavAdd(종목코드, "Y", monprice, monGb , 0 , 0 ,0 , 0 , false);
                }
            }
            //체결쪽으로 넘어간 데이터를 검수하고 RealData를 업데이트 해준다. - E
        }

        //TAB 0 실시간데이터 처리 부분 - E


        //뉴스 Tab 에 리얼데이터 를 요청 하겠다는 함수 - 참고.OPT10003 은 최초 시간을 빠르게 가져 오기 위해 삽입 계속 바꾸어 가며 테스트중 - S
        private void RequestRealData(string 종목코드)
        {
            string tmpStr = "";
            int cnt = 1;
            if (종목코드.Length != 6)
            {
                for (int ix = 0; ix < 종목코드.Split(';').Length - 1; ix++)
                {
                    tmpStr += 종목코드.Split(';')[ix] + ";";
                    if (ix % 100 == 99)
                    {
                        ucMainStockVer2.OptKWFid_OnReceiveRealData(tmpStr, cnt);
                        SystemSleep();
                        tmpStr = "";
                        cnt = 0;
                    }
                    cnt += 1;
                }
                ucMainStockVer2.OptKWFid_OnReceiveRealData(tmpStr, cnt);
                SystemSleep();
            }
            else
            {
                ucMainStockVer2.OptKWFid_OnReceiveRealData(종목코드, cnt);
                SystemSleep();
            }

            //dgUcReal.DataSource = UcMainStockVer2._DtScreenNoManage;
            //return UcMainStockVer2.ScreenNoManage(ucMainStockVer2.Enum_ScreenNo.TR_OPTK, "OptKWFid_OnReceiveRealData", 종목코드);
        }
        //뉴스 Tab 에 RealData를 요청 하겠다는 함수 OPT10003 은 최초 시간을 빠르게 가져 오기 위해 삽입 계속 바꾸어 가며 테스트중 - E


        private async Task DoOpt10007(string stockCode, string stockName)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_Opt10007EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted)
                    return;

                SettingOPT10007(d);
                SystemSleep();
                ucMainStockVer2.OnReceiveTrData_Opt10007 -= handler;
                tcs.SetResult(true);
            };

            ucMainStockVer2.OnReceiveTrData_Opt10007 += handler;
            ucMainStockVer2.Opt10007_OnReceiveTrData(stockCode, stockName);

            await tcs.Task;
        }

        //private Task DoOpt10007(string stockCode, string stockName)
        //{
        //    TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
        //    return tcs.Task;
        //}

        private void SetData(string stockCode , DataSet ds)
        {
            if(ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1) return;

            DataRow dr = ds.Tables[0].Rows[0];
            string 종목코드 = dr["종목코드"].ToString();
            if (종목코드 == "") return;

            var obj = (from DataGridViewRow dgr in dg관종.Rows
                        where dgr.Cells[F_종목코드.Index].Value != null && dgr.Cells[F_종목코드.Index].Value.Equals(종목코드)
                    select dgr).FirstOrDefault();
            if (obj != null)
            {
                using (DataGridViewRow dgr = (DataGridViewRow)obj)
                {
                    dgr.Cells[F_등락율.Index].Value = Cls.Val(dr["등락률"]).ToString("0.00");
                    dgr.Cells[F_거래량.Index].Value = Cls.ValInt(dr["거래량"]).ToString("#,##0");
                    dgr.Cells[F_거래대금.Index].Value = Cls.ValInt((Cls.ValInt(dr["거래대금"]) * 1000000)).ToString("#,##0");
                    dgr.Cells[F_시가.Index].Value = Cls.ValInt(dr["시가"]).ToString("#,##0");
                    dgr.Cells[F_고가.Index].Value = Cls.ValInt(dr["고가"]).ToString("#,##0");
                    dgr.Cells[F_저가.Index].Value = Cls.ValInt(dr["저가"]).ToString("#,##0");
                    dgr.Cells[F_현재가.Index].Value = Cls.ValInt(Math.Abs(Cls.ValInt(dr["현재가"]))).ToString("#,##0");
                    dgr.Cells[F_대비.Index].Value = Math.Abs(Cls.ValInt(dr["현재가"].ToString())) - Math.Abs(Cls.ValInt(dr["전일종가"].ToString()));
                }
            }
        }

        private async void SetDsScreenNo(string actGb, string 화면구분, string 실시간구분, string 종목코드, string 종목명, bool isOPT10007)
        {
            DataRow drAdd;
            DataRow[] drArr;

            if (actGb == "A")
            {
                if (실시간구분 == "1")
                {
                    DataTable tickDt;
                    if (종목코드.Length == 6)
                    {
                        if (_dsTick60.Tables[종목코드] == null)
                        {
                            cboTickDataMember.Items.Add(종목코드 + "-" + 종목명);
                            tickDt = _dsTick60.Tables.Add(종목코드);
                            for (int i = 0; i < _Tick60.Length / 2; i++)
                            {
                                tickDt.Columns.Add(_Tick60[i, 0], Type.GetType(_Tick60[i, 1]));
                            }
                        }
                    }
                    else
                    {
                        foreach (string str in 종목코드.Split(';'))
                        {
                            if (str == "") break;
                            if (_dsTick60.Tables[str] == null)
                            {
                                if (ucMainStockVer2._allStockDataset.Tables["STOCKLIST"].Select("STOCK_CODE = '" + str + "'").Length > 0)
                                {
                                    cboTickDataMember.Items.Add(str + "-" + ucMainStockVer2._allStockDataset.Tables["STOCKLIST"].Select("STOCK_CODE = '" + str + "'")[0]["STOCK_NAME"].ToString());
                                    tickDt = _dsTick60.Tables.Add(str);
                                    for (int i = 0; i < _Tick60.Length / 2; i++)
                                    {
                                        tickDt.Columns.Add(_Tick60[i, 0], Type.GetType(_Tick60[i, 1]));
                                    }
                                }

                            }
                        }
                    }

                    if (ucMainStockVer2._DtScreenNoManage != null)
                    {
                        if (ucMainStockVer2._DtScreenNoManage.Rows.Count < 1)
                        {
                            RequestRealData(종목코드);
                        }
                        else
                        {
                            if (ucMainStockVer2._DtScreenNoManage.Select("STOCK_CODE LIKE '%" + 종목코드 + "%'").Length < 1)
                            {
                                RequestRealData(종목코드);
                            }
                        }
                    }
                }

                if (isOPT10007 == true && !Cls.IsDb(_ds전체일봉))
                {
                    foreach (string str in 종목코드.Split(';'))
                    {
                        if (str == "") break;
                        await DoOpt10007(str, ucMainStockVer2.GetStockInfo(str));
                    }
                }

                drArr = _dt화면관리.Select(String.Format("화면구분 = '{0}' AND 실시간구분 = '{1}' AND 종목코드 LIKE '%{2}%'", 화면구분, 실시간구분, 종목코드));
                if (drArr.Length > 0) return;

                string tmpStr = "";
                int cnt = 1;
                if (종목코드.Length != 6)
                {
                    for (int ix = 0; ix < 종목코드.Split(';').Length - 1; ix++)
                    {
                        tmpStr += 종목코드.Split(';')[ix] + ";";
                        if (ix % 100 == 99)
                        {
                            drAdd = _dt화면관리.NewRow();
                            drAdd["화면구분"] = 화면구분;
                            drAdd["실시간구분"] = 실시간구분;
                            drAdd["종목코드"] = tmpStr;
                            _dt화면관리.Rows.Add(drAdd);
                            tmpStr = "";
                            cnt = 0;
                        }
                        cnt += 1;
                    }
                    drAdd = _dt화면관리.NewRow();
                    drAdd["화면구분"] = 화면구분;
                    drAdd["실시간구분"] = 실시간구분;
                    drAdd["종목코드"] = tmpStr;
                    _dt화면관리.Rows.Add(drAdd);
                    SystemSleep();
                }
                else { 
                    drAdd = _dt화면관리.NewRow();
                    drAdd["화면구분"] = 화면구분;
                    drAdd["실시간구분"] = 실시간구분;
                    drAdd["종목코드"] = 종목코드;
                    _dt화면관리.Rows.Add(drAdd);
                }
            }
            else if (actGb == "D")
            {
                drArr = _dt화면관리.Select(String.Format("화면구분 <> '{0}' AND 실시간구분 = '{1}' AND 종목코드 LIKE '%{2}%'", 화면구분, 실시간구분, 종목코드));
                if (drArr.Length < 1)
                {  // 다른 화면에서 사용할 가능성이 있기 때문에 끊지 않음
                    ucMainStockVer2.DisconnectRealDataStockCode(종목코드); //실시간 데이터가 필요 없을 경우 실시간 데이터 요청을 끊어준다. - S
                    RemoveTickTable(종목코드);
                }
                drArr = _dt화면관리.Select(String.Format("화면구분 = '{0}' AND 실시간구분 = '{1}' AND 종목코드 LIKE '%{2}%'", 화면구분, 실시간구분, 종목코드));
                if (drArr.Length > 0)
                {  // 다른 화면에서 사용할 가능성이 있기 때문에 끊지 않음
                    _dt화면관리.Rows.Remove(drArr[0]);
                }
            }
        }


        //조건검색 받는쪽 - S
        private void SetConditionList(DataSet ds, string index, string scrNo, string conName)
        {
            DataGridView dg = null;
            string ind = Cls.ValInt(index).ToString("000");
            if (cbo조건1.SelectedIndex != -1 && cbo조건1.Items[cbo조건1.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목1;
            }
            else if (cbo조건2.SelectedIndex != -1 && cbo조건2.Items[cbo조건2.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목2;
            }
            else if (cbo조건3.SelectedIndex != -1 && cbo조건3.Items[cbo조건3.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목3;
            }
            else if (cbo조건4.SelectedIndex != -1 && cbo조건4.Items[cbo조건4.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목4;
            }
            else if (cbo조건5.SelectedIndex != -1 && cbo조건5.Items[cbo조건5.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목5;
            }
            if (dg == null) return;
            string strCodes = "";
            if (ds == null) return;
            try
            {
                int row = 0;
                dg.RowCount = 0;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["STOCK_CODE"].ToString().Trim() == "") continue;

                    dg.RowCount++;
                    DataGridViewRow dgr = dg.Rows[row];
                    dgr.Cells[C종목코드.Index].Value = dr["STOCK_CODE"].ToString().Trim();
                    dgr.Cells[C종목명.Index].Value = dr["STOCK_NAME"].ToString().Trim();

                    DataTable dt = SetLine(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgr.Cells[CH3일선.Index].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                        dgr.Cells[CH5일선.Index].Value = dt.Rows[0]["line5"].ToString();//H_5일선
                        dgr.Cells[CH10일선.Index].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                        dgr.Cells[CH15일선.Index].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                        dgr.Cells[CH20일선.Index].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                        dgr.Cells[CH40일선.Index].Value = dt.Rows[0]["line40"].ToString();//H_40일선
                        dgr.Cells[CH60일선.Index].Value = dt.Rows[0]["line60"].ToString();//H_60일선
                        dgr.Cells[C세력선_H.Index].Value = Cls.Val(dt.Rows[0]["MA_H"].ToString()).ToString("#,##0");//MA_H
                        dgr.Cells[C세력선_C.Index].Value = Cls.Val(dt.Rows[0]["MA_C"].ToString()).ToString("#,##0");//MA_C
                        dgr.Cells[C세력선_L.Index].Value = Cls.Val(dt.Rows[0]["MA_L"].ToString()).ToString("#,##0");//MA_L
                        dgr.Cells[CB상한.Index].Value = Cls.Val(dt.Rows[0]["B_UP"].ToString()).ToString("#,##0.00");//B_UP
                        dgr.Cells[CB하한.Index].Value = Cls.Val(dt.Rows[0]["B_DOWN"].ToString()).ToString("#,##0.00");//B_DOWN

                        SetConditionPrice123(row, dg, conName , 0);
                        //SetConditionPrice(-1, dg, conName);
                        if (Cls.IsDb(_ds전체일봉) == true)
                        {
                            dgr.Cells[C시가.Index].Value = Cls.ValInt(dt.Rows[0]["s_price"]).ToString("#,##0");
                            dgr.Cells[C저가.Index].Value = Cls.ValInt(dt.Rows[0]["l_price"]).ToString("#,##0");
                            dgr.Cells[C고가.Index].Value = Cls.ValInt(dt.Rows[0]["h_price"]).ToString("#,##0");
                            dgr.Cells[C거래량.Index].Value = Cls.ValInt(dt.Rows[0]["volume"]).ToString("#,##0");
                            //dg.Rows[row].Cells[C거래대금.Index].Value = dt.Rows[0]["trading_value"].ToString();
                            dgr.Cells[C등락률.Index].Value = dt.Rows[0]["rate"].ToString();
                            dgr.Cells[C현재가.Index].Value = Cls.ValInt(dt.Rows[0]["end_price"]).ToString("#,##0");
                            dgr.Cells[C대비.Index].Value = dt.Rows[0]["daebi"].ToString();
                        }
                        dt = SetFinance(dr["STOCK_CODE"].ToString().Trim());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dgr.Cells["C업종"].Value = dt.Rows[0]["CLASS_GB"].ToString();
                            dgr.Cells["C신용비율"].Value = dt.Rows[0]["CREDIT_RATE"].ToString();
                            dgr.Cells["C시가총액"].Value = dt.Rows[0]["STOCK_TOTAL_P"].ToString();
                            dgr.Cells["CPER"].Value = dt.Rows[0]["PER"].ToString();
                            dgr.Cells["CROE"].Value = dt.Rows[0]["ROE"].ToString();
                            dgr.Cells["CPBR"].Value = dt.Rows[0]["PBR"].ToString();
                            dgr.Cells["CEV"].Value = dt.Rows[0]["EV"].ToString();
                            dgr.Cells["CEPS"].Value = dt.Rows[0]["EPS"].ToString();
                            dgr.Cells["CBPS"].Value = dt.Rows[0]["BPS"].ToString();
                            dgr.Cells["C영업이익"].Value = dt.Rows[0]["O_PROFIT"].ToString();
                            dgr.Cells["C당기순이익"].Value = dt.Rows[0]["P_PROFIT"].ToString();
                            dgr.Cells["C250최고가"].Value = dt.Rows[0]["HIGH_250"].ToString();
                            dgr.Cells["C250최저가"].Value = dt.Rows[0]["LOW_250"].ToString();
                            dgr.Cells["C테마"].Value = dt.Rows[0]["THEME_NAME"].ToString();

                            try
                            {
                                Cls.ChangeColor(dgr, "D", 7, F_신용비율.Index);
                                Cls.ChangeColor(dgr, "D", 3, F_PBR.Index);
                                Cls.ChangeColor(dgr, "A", 0, F_영업이익.Index, F_당기순이익.Index);

                                //if (Cls.Val(dg.Rows[row].Cells["C신용비율"].Value.ToString()) > 5) dg.Rows[row].Cells["C신용비율"].Style.ForeColor = System.Drawing.Color.Red;
                                //else dg.Rows[row].Cells["C신용비율"].Style.ForeColor = System.Drawing.Color.Empty;
                                //if (Cls.Val(dg.Rows[row].Cells["CPBR"].Value.ToString()) > 3) dg.Rows[row].Cells["CPBR"].Style.ForeColor = System.Drawing.Color.Red;
                                //else dg.Rows[row].Cells["CPBR"].Style.ForeColor = System.Drawing.Color.Empty;
                                //if (Cls.Val(dg.Rows[row].Cells["C영업이익"].Value.ToString()) < 0) dg.Rows[row].Cells["C영업이익"].Style.ForeColor = System.Drawing.Color.Red;
                                //else dg.Rows[row].Cells["C영업이익"].Style.ForeColor = System.Drawing.Color.Empty;
                                //if (Cls.Val(dg.Rows[row].Cells["C당기순이익"].Value.ToString()) < 0) dg.Rows[row].Cells["C당기순이익"].Style.ForeColor = System.Drawing.Color.Red;
                                //else dg.Rows[row].Cells["C당기순이익"].Style.ForeColor = System.Drawing.Color.Empty;
                            }
                            catch { }
                        }
                        dt = SetBuySellState(dr["STOCK_CODE"].ToString().Trim());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dgr.Cells[C외국인.Index].Value = dt.Rows[0]["F_TU"].ToString();
                            dgr.Cells[C기관.Index].Value = dt.Rows[0]["K_TU"].ToString();
                            dgr.Cells[C외국인10.Index].Value = dt.Rows[1]["F_TU"].ToString();
                            dgr.Cells[C기관10.Index].Value = dt.Rows[1]["K_TU"].ToString();

                            Cls.ChangeColor(dgr, "A", 0, C외국인.Index, C기관.Index, C외국인10.Index, C기관10.Index);

                            //if (Cls.Val(dg.Rows[row].Cells[C외국인.Index].Value.ToString()) > 0) dg.Rows[row].Cells[C외국인.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dg.Rows[row].Cells[C외국인.Index].Style.ForeColor = System.Drawing.Color.Blue;
                            //if (Cls.Val(dg.Rows[row].Cells[C기관.Index].Value.ToString()) > 0) dg.Rows[row].Cells[C기관.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dg.Rows[row].Cells[C기관.Index].Style.ForeColor = System.Drawing.Color.Blue;
                        }
                    }

                    strCodes += dr["STOCK_CODE"].ToString().Trim() + ";";
                    row++;
                }
                if (Cls.IsDb(_ds전체일봉) == true){}
                else if (Cls.IsDb(_ds전체일봉) == false && DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:00:00")) && DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:20:00"))
                    )
                {
                    SetDsScreenNo("A", "3", "1", strCodes, "", false);
                    SystemSleep();
                }
                else
                {
                    //ucStockAvgMagipPrice1.PropWriteStockList10007 = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dg);
                }
                
            }
            catch (Exception ex)
            {
                Logger("에러 (OnDsSetConditionList)", ex.ToString());
            }
            finally
            {
            }
        }
        //조건검색 받는쪽 - E

        private void UcMainStockVer2_OnDsStockByTradePortNumer(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnDsTradePortInfo(DataRow dr)
        {

        }

        private void SetConditionPrice123(int rowIndex, DataGridView dg, string conName , int price)
        {
            if (dg == null) return;

            int row = 0;
            string stockCode = dg.Rows[rowIndex].Cells[C종목코드.Index].Value.ToString();
            DataView dv = GetPrices(price, stockCode);
            dv.RowFilter = "GB = 'C'";
            
            int prePrice = Cls.ValInt(dv[0]["PRICE"]);
            dv.RowFilter = "GB = 'B'";
            dv.Sort = "PRICE DESC";

            dg.Rows[rowIndex].Cells[C1차매수가.Index].Value = "";
            dg.Rows[rowIndex].Cells[C2차매수가.Index].Value = "";
            dg.Rows[rowIndex].Cells[C3차매수가.Index].Value = "";
            foreach (DataRowView drv in dv)
            {
                if ((Cls.Val(prePrice) * 0.97) >= Cls.Val(drv["PRICE"]))
                {
                    dg.Rows[rowIndex].Cells["C" + (row + 1).ToString() + "차매수가"].Value = drv["PRICE"];
                    prePrice = Cls.ValInt(drv["PRICE"]);
                }
                else
                {
                    continue;
                }
                row++;
                if (row >= 3) break;
            }
        }

        private DataView GetPrices(int price, string stockCode)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GB");
            dt.Columns.Add("PRICE" , Type.GetType("System.Int32"));
            

            DataRow[] drs = _ds전체일봉.Tables[0].Select("STOCK_CODE = '" + stockCode + "'");
            if (drs.Length == 0) return null;

            DataRow dr = drs[0];
            DataRow drNew;
            if (price.Equals(0)) price = Cls.ValInt(dr["END_PRICE"]);

            dt.Rows.Clear();
            foreach(DataColumn dc in _ds전체일봉.Tables[0].Columns)
            {
                if (dc.ColumnName.IndexOf("box") > -1)
                {
                    drNew = dt.NewRow();
                    if (price == Cls.ValInt(dr[dc.ColumnName])) continue;
                    else if (price > Cls.ValInt(dr[dc.ColumnName])) drNew["GB"] = "B";
                    else drNew["GB"] = "S";
                    drNew["PRICE"] = Cls.ValInt(dr[dc.ColumnName]);
                    dt.Rows.Add(drNew);
                }
            }
            drNew = dt.NewRow();
            dt.Rows.Add(drNew);
            drNew["GB"] = "C";
            drNew["PRICE"] = price;

            DataView dv = new DataView(dt);
            return dv;
        }

        //조건 검색 API 25% , 50% , 75% , 0% 계산법 - S
        private void SetConditionPrice(int rowIndex, DataGridView dg, string conName)
        {
            if (dg == null) return;
            DataSet ds;
            DataView dv;
            string 종목코드, 종목명;
            int 시가;
            int 저가;
            Double 등락률;
            int 종가;
            int 고가;
            int 전일종가;
            Double 저종MA;
            Double 최고저종MA;
            int sRow = 0;
            int eRow = 0;
            if (rowIndex != -1)
            {
                sRow = rowIndex;
                eRow = rowIndex;
            }
            else
            {
                sRow = 0;
                eRow = dg.Rows.Count - 1;
            }

            for (int row = sRow; row <= eRow; row++)
            {
                dg.Rows[row].Cells[C1차매수가.Index].Value = "";
                dg.Rows[row].Cells[C2차매수가.Index].Value = "";
                dg.Rows[row].Cells[C3차매수가.Index].Value = "";

                종목코드 = dg.Rows[row].Cells[C종목코드.Index].Value.ToString().Trim();
                종목명 = dg.Rows[row].Cells[C종목명.Index].Value.ToString().Trim();
                ds = _DataAcc.p_stock_day_data_query("4", 종목코드, "", true, null, null);
                dv = new DataView(ds.Tables[0]);

                dv.RowFilter = "CANDLE_RATE >= 5.00 ";
                
                dv.Sort = "L_PRICE ASC";
                if (dv.Count < 1) { continue; }

                종가 = Cls.ValInt(dv[0]["END_PRICE"].ToString().Trim());
                전일종가 = Cls.ValInt(dv[0]["PRE_E_PRICE"].ToString().Trim());
                시가 = Cls.ValInt(dv[0]["S_PRICE"].ToString().Trim());
                저가 = Cls.ValInt(dv[0]["L_PRICE"].ToString().Trim());
                고가 = Cls.ValInt(dv[0]["H_PRICE"].ToString().Trim());

                저종MA = Cls.Val(dv[0]["LOWEND_MA"].ToString().Trim());
                최고저종MA = Cls.Val(dv[0]["H_LOWEND_MA"].ToString().Trim());
                등락률 = Cls.Val(dv[0]["DAY_RATE"].ToString().Trim());


                if (conName.IndexOf("갭") > -1)
                {
                    dg.Rows[row].Cells[C1차매수가.Index].Value = String.Format("{0:0}" , 종가);
                    dg.Rows[row].Cells[C2차매수가.Index].Value = String.Format("{0:0}" , 시가);
                }
                else if (conName.IndexOf("신고가") > -1 || conName.IndexOf("연속음봉") > -1)
                {
                    dg.Rows[row].Cells[C1차매수가.Index].Value = String.Format("{0:0}" , (종가 + 저가) / 2);
                    dg.Rows[row].Cells[C2차매수가.Index].Value = String.Format("{0:0}" , 시가);
                }

                //if (등락률 < 10)
                //{
                //    지지선1 = (종가 + 저가) / 2;
                //    dg.Rows[row].Cells[C1차매수가.Index].Value = 지지선1.ToString("#,##0").Trim();
                //    dg.Rows[row].Cells[C2차매수가.Index].Value = 저가.ToString("#,##0").Trim();

                //    if (저가 > 최고저종MA) dg.Rows[row].Cells[C3차매수가.Index].Value = 최고저종MA.ToString("#,##0").Trim();
                //    if (저가 > (최고저종MA + 저종MA) / 2) dg.Rows[row].Cells[C3차매수가.Index].Value = (최고저종MA + 저종MA).ToString("#,##0").Trim();
                //    if (저가 > 저종MA) dg.Rows[row].Cells[C3차매수가.Index].Value = 저종MA.ToString("#,##0").Trim();

                //    temp = (시가 - 저가) / 저가 * 100;

                //}
                //else
                //{
                //    지지선1 = 종가 - ((종가 - 저가) / 3);
                //    지지선2 = (종가 + 저가) / 2;
                //    지지선3 = 저가;

                //    dg.Rows[row].Cells[C1차매수가.Index].Value = 지지선1.ToString("#,##0").Trim(); ;
                //    dg.Rows[row].Cells[C2차매수가.Index].Value = 지지선2.ToString("#,##0").Trim(); ;
                //    dg.Rows[row].Cells[C3차매수가.Index].Value = 지지선3.ToString("#,##0").Trim(); ;
                //}

                dv.RowFilter = "";
            }
        }
        //조건 검색 API 25% , 50% , 75% , 0% 계산법 - E

        //관종 실시간 리스트 리셋 - S
        private void btnReset_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dgN관종.RowCount - 1; row++)
            {
                SetDsScreenNo("D", "1", "1", dgN관종.Rows[row].Cells[P종목코드.Index].Value.ToString().Trim(), dgN관종.Rows[row].Cells["P종목명"].Value.ToString().Trim(), false);
            }
            //_ds뉴스.Tables.Clear();
            dgN관종.RowCount = 0;
        }
        //관종 실시간 리스트 리셋 - E

        //체결된 실시간 리스트 리셋 - S
        private void btn뉴스체결_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dg뉴스체결.RowCount - 1; row++)
            {
                SetDsScreenNo("D", "1", "1", dg뉴스체결.Rows[row].Cells[N종목코드.Index].Value.ToString().Trim(), dg뉴스체결.Rows[row].Cells["N종목명"].Value.ToString().Trim(), false);
            }
            dg뉴스체결.RowCount = 0;
        }
        //체결된 실시간 리스트 리셋 - E

        private void txtTmDaum_Leave(object sender, EventArgs e)
        {
            //tmrDaum.Start();
            //tmrDaum.Interval = Cls.ValInt(txtTmDaum1.Text.Trim());
        }

        private void txtTmNaver_Leave(object sender, EventArgs e)
        {
            tmrNews.Start();
            tmrNews.Interval = Cls.ValInt(txtTmNaver2.Text.Trim());
        }

        private void txtTmDart_Leave(object sender, EventArgs e)
        {
            tmrDart.Start();
            tmrDartKrx.Start();
            tmrDart.Interval = Cls.ValInt(txtTmDart3.Text.Trim());
            tmrDartKrx.Interval = Cls.ValInt(txtTmDart3.Text.Trim());
        }

        private void dgN관종_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 1)
            {
                SetDsScreenNo("A", "1", "1", dgN관종.Rows[e.RowIndex].Cells[P종목코드.Index].ToString().Trim(), dgN관종.Rows[e.RowIndex].Cells["P종목명"].ToString().Trim(), true);
            }
        }

        private void txt주문종목코드_Enter_1(object sender, EventArgs e)
        {
            TextBox txtObj = (TextBox)sender;
            txtObj.SelectAll();
        }

        private void btnNewsStop_Click(object sender, EventArgs e)
        {
            if (btnNewsStop.Text.IndexOf("실행") > -1)
            {
                //tmrDaum.Start();
                tmrNews.Start();
                btnNewsStop.Text = "뉴스타이머중지(■)";
            }
            else
            {
                //tmrDaum.Stop();
                tmrNews.Stop();
                btnNewsStop.Text = "뉴스타이머실행(▶)";
            }
        }

        private void btn로그실행_Click(object sender, EventArgs e)
        {

        }

        private void btn잔고_Click(object sender, EventArgs e)
        {
            ucMainStockVer2.Opw00018_OnReceiveTrData(cboAccount.Items[cboAccount.SelectedIndex].ToString());
            var tMi = GetMiOrder();
        }

        private async Task GetMiOrder()
        {
            dg미체결.RowCount = 0;
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            ucMainStockVer2.OnReceiveTrData_opt10075EventHandler handler = null;

            handler = (d) =>
            {
                if (tcs.Task.IsCompleted)
                    return;

                DataView dv = new DataView(d.Tables[0]);
                dv.RowFilter = "미체결수량 > 0";
                foreach (DataRowView dr in dv)
                {
                    dg미체결.RowCount++;
                    DataGridViewRow dgr = dg미체결.Rows[dg미체결.Rows.Count - 1];
                    dgr.Cells[M주문번호.Index].Value = dr["주문번호"];
                    dgr.Cells[M종목코드.Index].Value = dr["종목코드"];
                    dgr.Cells[M주문상태.Index].Value = dr["주문상태"].Equals("체결") ? "접수" : dr["주문상태"];
                    dgr.Cells[M종목명.Index].Value = dr["종목명"];
                    dgr.Cells[M주문수량.Index].Value = dr["주문수량"];
                    dgr.Cells[M주문가격.Index].Value = dr["주문가격"];
                    dgr.Cells[M미체결수량.Index].Value = dr["미체결수량"];
                    dgr.Cells[M원주문번호.Index].Value = dr["원주문번호"];
                    dgr.Cells[M주문구분.Index].Value = dr["주문구분"];
                    dgr.Cells[M매매구분.Index].Value = dr["매매구분"];
                    dgr.Cells[M시간.Index].Value = dr["시간"];
                }
                ucMainStockVer2.OnReceiveTrData_opt10075 -= handler;
                tcs.SetResult(true);
            };

            ucMainStockVer2.OnReceiveTrData_opt10075 += handler;
            ucMainStockVer2.Opt10075_OnReceiveChejanData(cboAccount.Items[cboAccount.SelectedIndex].ToString());
            await tcs.Task;
        }

        private void dg관종_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtF종목코드_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewRow dgv;
            if (e.KeyCode == Keys.Enter)
            {
                if (ucMainStockVer2._allStockDataset == null) return;
                DataView dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);

                if (txtF종목코드.Text.Trim().Length == 6 && Cls.IsNumeric(txtF종목코드.Text.Trim()))
                {
                    dv.RowFilter = "STOCK_CODE = '" + txtF종목코드.Text.Trim() + "'";
                }
                else
                {
                    dv.RowFilter = "STOCK_NAME = '" + txtF종목코드.Text.Trim() + "'";
                }
                

                bool blnTrue = false;
                Decimal ret;
                if (dv.Count < 1) return;

                blnTrue = false;
                for (int row = 0; row < dg관종.Rows.Count - 1; row++)
                {
                    if (dg관종.Rows[row].Cells[F_종목코드.Index].Value.ToString().Trim() == dv[0]["STOCK_CODE"].ToString())
                    {
                        blnTrue = true;
                        break;
                    }
                }

                if (blnTrue == false)
                {
                    try
                    {
                        //dgv = (DataGridViewRow)dg관종.Rows[0].Clone();
                        dg관종.RowCount += 1;
                        dgv = dg관종.Rows[dg관종.RowCount - 1];
                        dgv.Cells["F_삭제"].Value = "D";
                        dgv.Cells[F_종목코드.Index].Value = dv[0]["STOCK_CODE"].ToString().Trim();
                        dgv.Cells["F_종목명"].Value = dv[0]["STOCK_NAME"].ToString().Trim();

                        DataTable dt = SetLine(dv[0]["STOCK_CODE"].ToString().Trim());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dgv.Cells[H_3일선.Index].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                            dgv.Cells[H_5일선.Index].Value = dt.Rows[0]["line5"].ToString(); //H_5일선
                            dgv.Cells[H_10일선.Index].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                            dgv.Cells[H_15일선.Index].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                            dgv.Cells[H_20일선.Index].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                            dgv.Cells[H_40일선.Index].Value = dt.Rows[0]["line40"].ToString();//H_40일선
                            dgv.Cells[H_60일선.Index].Value = dt.Rows[0]["line60"].ToString();//H_60일선
                            dgv.Cells[H_120일선.Index].Value = dt.Rows[0]["line120"].ToString();//H_120일선
                            dgv.Cells[H_220일선.Index].Value = dt.Rows[0]["line220"].ToString();//H_220일선
                            dgv.Cells[F_세력선_H.Index].Value = Cls.Val(dt.Rows[0]["MA_H"].ToString()).ToString("#,##0");//MA_H
                            dgv.Cells[F_세력선_C.Index].Value = Cls.Val(dt.Rows[0]["MA_C"].ToString()).ToString("#,##0");//MA_C
                            dgv.Cells[F_세력선_L.Index].Value = Cls.Val(dt.Rows[0]["MA_L"].ToString()).ToString("#,##0");//MA_L
                            dgv.Cells[F_B상한.Index].Value = Cls.Val(dt.Rows[0]["B_UP"].ToString()).ToString("#,##0.00");//B_UP
                            dgv.Cells[F_B하한.Index].Value = Cls.Val(dt.Rows[0]["B_DOWN"].ToString()).ToString("#,##0.00");//B_DOWN

                            if (Decimal.TryParse(dgv.Cells[F_B상한.Index].Value.ToString(), out ret) != false && Decimal.TryParse(dgv.Cells[F_B하한.Index].Value.ToString(), out ret) != false && Cls.Val(dgv.Cells[F_B하한.Index].Value.ToString()) != 0)
                            {
                                dgv.Cells[F_BB이격.Index].Value = ((Cls.Val(dgv.Cells[F_B상한.Index].Value.ToString()) - Cls.Val(dgv.Cells[F_B하한.Index].Value.ToString())) / Cls.Val(dgv.Cells[F_B하한.Index].Value.ToString()) * 100).ToString("#,##0.0");
                            }

                            if (Cls.IsDb(_ds전체일봉) == true)
                            {
                                dgv.Cells[F_시가.Index].Value = Cls.ValInt(dt.Rows[0]["s_price"]);
                                dgv.Cells[F_저가.Index].Value = Cls.ValInt(dt.Rows[0]["l_price"]);
                                dgv.Cells[F_고가.Index].Value = Cls.ValInt(dt.Rows[0]["h_price"]);
                                dgv.Cells[F_거래량.Index].Value = Cls.ValInt(dt.Rows[0]["volume"]);
                                dgv.Cells[F_거래대금.Index].Value = Cls.ValInt64(dt.Rows[0]["trading_value"]);
                                dgv.Cells[F_등락율.Index].Value = dt.Rows[0]["rate"].ToString();
                                dgv.Cells[F_현재가.Index].Value = Cls.ValInt(dt.Rows[0]["end_price"]);
                                dgv.Cells[F_대비.Index].Value = dt.Rows[0]["daebi"].ToString();
                            }
                        }

                        dt = SetFinance(dv[0]["STOCK_CODE"].ToString().Trim());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dgv.Cells[F_업종.Index].Value = dt.Rows[0]["CLASS_GB"].ToString();
                            dgv.Cells[F_신용비율.Index].Value = dt.Rows[0]["CREDIT_RATE"].ToString();
                            dgv.Cells[F_시가총액.Index].Value = dt.Rows[0]["STOCK_TOTAL_P"].ToString();
                            dgv.Cells[F_PER.Index].Value = dt.Rows[0]["PER"].ToString();
                            dgv.Cells[F_ROE.Index].Value = dt.Rows[0]["ROE"].ToString();
                            dgv.Cells[F_PBR.Index].Value = dt.Rows[0]["PBR"].ToString();
                            dgv.Cells[F_EV.Index].Value = dt.Rows[0]["EV"].ToString();
                            dgv.Cells[F_EPS.Index].Value = dt.Rows[0]["EPS"].ToString();
                            dgv.Cells[F_BPS.Index].Value = dt.Rows[0]["BPS"].ToString();
                            dgv.Cells[F_영업이익.Index].Value = dt.Rows[0]["O_PROFIT"].ToString();
                            dgv.Cells[F_당기순이익.Index].Value = dt.Rows[0]["P_PROFIT"].ToString();
                            dgv.Cells[F_250최고가.Index].Value = dt.Rows[0]["HIGH_250"].ToString();
                            dgv.Cells[F_250최저가.Index].Value = dt.Rows[0]["LOW_250"].ToString();
                            dgv.Cells[F_테마.Index].Value = dt.Rows[0]["THEME_NAME"].ToString();

                            try
                            {
                                Cls.ChangeColor(dgv, "D", 7, F_신용비율.Index);
                                Cls.ChangeColor(dgv, "D", 3, F_PBR.Index);
                                Cls.ChangeColor(dgv, "A", 0, F_영업이익.Index, F_당기순이익.Index);

                                //if (Cls.Val(dgv.Cells[F_신용비율.Index].Value.ToString()) > 5) dgv.Cells[F_신용비율.Index].Style.ForeColor = System.Drawing.Color.Red;
                                //else dgv.Cells[F_신용비율.Index].Style.ForeColor = System.Drawing.Color.Empty;
                                //if (Cls.Val(dgv.Cells[F_PBR.Index].Value.ToString()) > 3) dgv.Cells[F_PBR.Index].Style.ForeColor = System.Drawing.Color.Red;
                                //else dgv.Cells[F_PBR.Index].Style.ForeColor = System.Drawing.Color.Empty;
                                //if (Cls.Val(dgv.Cells[F_영업이익.Index].Value.ToString()) < 0) dgv.Cells[F_영업이익.Index].Style.ForeColor = System.Drawing.Color.Red;
                                //else dgv.Cells[F_영업이익.Index].Style.ForeColor = System.Drawing.Color.Empty;
                                //if (Cls.Val(dgv.Cells[F_당기순이익.Index].Value.ToString()) < 0) dgv.Cells[F_당기순이익.Index].Style.ForeColor = System.Drawing.Color.Red;
                                //else dgv.Cells[F_당기순이익.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            }
                            catch { }
                        }

                        dt = SetBuySellState(dv[0]["STOCK_CODE"].ToString().Trim());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dgv.Cells[F_외국인.Index].Value = dt.Rows[0]["F_TU"].ToString();
                            dgv.Cells[F_기관.Index].Value = dt.Rows[0]["K_TU"].ToString();
                            dgv.Cells[F_외국인10.Index].Value = dt.Rows[1]["F_TU"].ToString();
                            dgv.Cells[F_기관10.Index].Value = dt.Rows[1]["K_TU"].ToString();

                            Cls.ChangeColor(dgv, "A", 0, F_외국인.Index, F_기관.Index, F_외국인10.Index, F_기관10.Index);
                        }

                        //dg관종.Rows.Add(dgv);
                        if (cboTheme.SelectedIndex < 1) { 
                            int interId = 0;
                            if (rb1.Checked == true) interId = 1;
                            else if (rb2.Checked == true) interId = 2;
                            else if (rb3.Checked == true) interId = 3;
                            else if (rb4.Checked == true) interId = 4;
                            else if (rb5.Checked == true) interId = 5;

                            _DataAcc.p_Psi02Add("A", _stockId, interId, dv[0]["STOCK_CODE"].ToString().Trim(), "00", "", "", null, null);
                        }
                        else
                        {
                            mySqlDbConn conn = new mySqlDbConn();

                            conn.ExecuteNonQuery(String.Format("insert into stock_theme(stock_code , theme_name) values ('{0}' , '{1}')" , dv[0]["STOCK_CODE"].ToString().Trim() , cboTheme.Text.Trim()) , null , CommandType.Text);
                        }
                        SetDsScreenNo("A", "4", "1", dv[0]["STOCK_CODE"].ToString().Trim(), dv[0]["STOCK_NAME"].ToString().Trim(), true);
                    }
                    finally
                    {
                    }
                }

                txtF종목코드.Text = "";
            }

        }

        private void dg관종_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value == null) return;
            if (e.ColumnIndex == 0) //삭제버튼클릭시
            {
                if (cboTheme.SelectedIndex < 1) { 
                    int interId = 0;
                    if (rb1.Checked == true) interId = 1;
                    else if (rb2.Checked == true) interId = 2;
                    else if (rb3.Checked == true) interId = 3;
                    else if (rb4.Checked == true) interId = 4;
                    else if (rb5.Checked == true) interId = 5;

                    _DataAcc.p_Psi02Add("D", _stockId, interId, dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value.ToString().Trim(), "00", "", "", null, null);
                }
                else
                {
                    mySqlDbConn conn = new mySqlDbConn();
                    conn.ExecuteNonQuery(String.Format("delete from stock_theme where stock_code = '{0}' and theme_name = '{1}'", dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value.ToString().Trim(), cboTheme.Text.Trim()), null, CommandType.Text);
                }

                if (dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value != null) {
                    SetDsScreenNo("D", "4", "1", dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value.ToString().Trim(), dg관종.Rows[e.RowIndex].Cells["F_종목명"].Value.ToString().Trim(), false);
                }
                
                dg관종.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void txtF종목코드_Enter(object sender, EventArgs e)
        {
            txtF종목코드.SelectAll();
        }

        private void dg관종_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value == null) return;
            ucHogaWindowNew1.StockCode = dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value.ToString().Trim();
        }

        private DataSet DaumNews(string SearchStr)
        {
            DataSet ds = new DataSet();
            string url = "http://search.daum.net/search?w=news&cluster=n&q=" + SearchStr + "&sort=1";

            //LoadPageAsync(_browser, url);
            //_browser.Load(url);
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                ////_browser.Load(url);
            }
            //      CopySourceToClipBoardAsync(_browser);

            DataTable dt = new DataTable("item");
            DataRow dr;

            ds.Tables.Add(dt);

            dt.Columns.Add("시간");
            dt.Columns.Add("키워드");
            dt.Columns.Add("제목");
            dt.Columns.Add("주소");


            if (_htmlSource == "") { return ds; }
            if (_htmlSource.Contains(">뉴스</h2>"))
            {
                int idxName;
                int idxNameLast;
                idxName = _htmlSource.IndexOf("topQuery");
                idxNameLast = _htmlSource.IndexOf(";", idxName);

                int idxFirst;
                int idxNews;
                int idxLast;
                idxFirst = _htmlSource.IndexOf("뉴스</h2>");

                if (idxFirst == -1)
                {
                    return ds;
                }

                if (_htmlSource.IndexOf("스타뉴스", idxFirst) > -1)
                {
                    return ds;
                }

                idxNews = _htmlSource.IndexOf("f_eb desc", idxFirst);
                idxLast = _htmlSource.IndexOf("</p>", idxNews);

                int idxTitle;
                int idxTitle1;
                int idxidxTitleLast;
                idxTitle = _htmlSource.IndexOf("wrap_tit mg_tit", idxFirst);
                idxTitle1 = _htmlSource.IndexOf("_blank", idxTitle);
                idxidxTitleLast = _htmlSource.IndexOf("</a>", idxTitle1);

                int idxNewsTime;
                int idxLastTime;
                idxNewsTime = _htmlSource.IndexOf("f_nb date", idxFirst);
                idxLastTime = _htmlSource.IndexOf("전", idxNewsTime);

                int idxLink;
                int idxLastLink;
                idxLink = _htmlSource.IndexOf("a href=", idxTitle);
                idxLastLink = _htmlSource.IndexOf("class", idxLink);

                DataView dv = new DataView(ds.Tables["item"]);

                dv.RowFilter = "제목 = '" + Cls.HtmlToPlainText(_htmlSource.Substring(idxTitle1 + 8, idxidxTitleLast - idxTitle1 - 8)).Replace("'", "''") + "'";
                if (dv.Count > 0)
                {
                    return ds;
                }

                dr = dt.NewRow();
                dr["시간"] = DateTime.Now;
                dr["키워드"] = _htmlSource.Substring(idxName + 12, idxNameLast - idxName - 13);
                dr["제목"] = Cls.HtmlToPlainText(_htmlSource.Substring(idxTitle1 + 8, idxidxTitleLast - idxTitle1 - 8));
                dr["주소"] = _htmlSource.Substring(idxLink + 8, idxLastLink - idxLink - 10);

                dt.Rows.Add(dr);
            }
            return ds;
        }

        ////private ChromiumWebBrowser _browser;
        private string _htmlSource = "";
        private void InitBrowser()
        {
            ////Cef.Initialize(new CefSettings());
            ////_browser = new ChromiumWebBrowser("");
            ////_browser.FrameLoadEnd += new EventHandler<CefSharp.FrameLoadEndEventArgs>(browser_FrameLoadEnd);
            ////this.Controls.Add(_browser);
            ////_browser.Dock = DockStyle.Fill;
        }

        ////private void browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        ////{
        ////    if (e.Frame.IsMain)
        ////    {
        ////        _browser.GetSourceAsync().ContinueWith(taskHtml =>
        ////        {
        ////            _htmlSource = taskHtml.Result;
        ////        });
        ////    }
        ////}

        private void dg조건종목_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            if (e.RowIndex == -1) return;

            if (dg.Rows[e.RowIndex].Cells[C종목코드.Index].Value == null) return;
            if (dg.Rows[e.RowIndex].Cells[C종목명.Index].Value == null) return;

            string 종목코드 = dg.Rows[e.RowIndex].Cells[C종목코드.Index].Value.ToString().Trim();
            string 종목명 = dg.Rows[e.RowIndex].Cells[C종목명.Index].Value.ToString().Trim();

            if (dg.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
            if (dg.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "") return;
            if (Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == false) return;


            if (dg.Rows[e.RowIndex].Cells[C현재가.Index].Value == null) return;
            if (dg.Rows[e.RowIndex].Cells[C시가.Index].Value == null) return;
            if (dg.Rows[e.RowIndex].Cells[C저가.Index].Value == null) return;
            if (dg.Rows[e.RowIndex].Cells[C고가.Index].Value == null) return;
            if (dg.Rows[e.RowIndex].Cells[C거래량.Index].Value == null) return;
            if (dg.Rows[e.RowIndex].Cells[C등락률.Index].Value == null) return;

            if (e.ColumnIndex == C대비.Index) //대비
            {
                if (Cls.Val(dg.Rows[e.RowIndex].Cells[C대비.Index].Value.ToString()) < 0)
                {
                    dg.Rows[e.RowIndex].Cells[C현재가.Index].Style.ForeColor = System.Drawing.Color.Blue;
                    dg.Rows[e.RowIndex].Cells[C대비.Index].Style.ForeColor = System.Drawing.Color.Blue;
                    dg.Rows[e.RowIndex].Cells[C등락률.Index].Style.ForeColor = System.Drawing.Color.Blue;
                }
                else if (Cls.Val(dg.Rows[e.RowIndex].Cells[C대비.Index].Value.ToString()) > 0)
                {
                    dg.Rows[e.RowIndex].Cells[C현재가.Index].Style.ForeColor = System.Drawing.Color.Red;
                    dg.Rows[e.RowIndex].Cells[C대비.Index].Style.ForeColor = System.Drawing.Color.Red;
                    dg.Rows[e.RowIndex].Cells[C등락률.Index].Style.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    dg.Rows[e.RowIndex].Cells[C현재가.Index].Style.ForeColor = System.Drawing.Color.Empty;
                    dg.Rows[e.RowIndex].Cells[C대비.Index].Style.ForeColor = System.Drawing.Color.Empty;
                    dg.Rows[e.RowIndex].Cells[C등락률.Index].Style.ForeColor = System.Drawing.Color.Empty;
                }
            }

            if (e.ColumnIndex == C현재가.Index )
            {
                try
                {
                    Double 현재가 = Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString().Trim());

                    if (dg.Rows[e.RowIndex].Cells[CH3일선.Index].Value == null) return;
                    //가격변경 - S
                    if (Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[CH3일선.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C3일선.Index].Value =
                            ((Cls.Val(dg.Rows[e.RowIndex].Cells[CH3일선.Index].Value.ToString()) + Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString())) / 3).ToString("#,##0.00")
                            ;
                    }
                    if (Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[CH5일선.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C5일선.Index].Value =
                            ((Cls.Val(dg.Rows[e.RowIndex].Cells[CH5일선.Index].Value.ToString()) + Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString())) / 5).ToString("#,##0.00")
                            ;
                    }
                    if (Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[CH10일선.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C10일선.Index].Value =
                            ((Cls.Val(dg.Rows[e.RowIndex].Cells[CH10일선.Index].Value.ToString()) + Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString())) / 10).ToString("#,##0.00")
                            ;
                    }
                    if (Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[CH15일선.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C15일선.Index].Value =
                            ((Cls.Val(dg.Rows[e.RowIndex].Cells[CH15일선.Index].Value.ToString()) + Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString())) / 15).ToString("#,##0.00")
                            ;
                    }
                    if (Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[CH20일선.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C20일선.Index].Value =
                            ((Cls.Val(dg.Rows[e.RowIndex].Cells[CH20일선.Index].Value.ToString()) + Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString())) / 20).ToString("#,##0.00")
                            ;
                    }

                    if (Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[CH40일선.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C40일선.Index].Value =
                            ((Cls.Val(dg.Rows[e.RowIndex].Cells[CH40일선.Index].Value.ToString()) + Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString())) / 40).ToString("#,##0.00")
                            ;
                    }

                    if (Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[CH60일선.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C60일선.Index].Value =
                            ((Cls.Val(dg.Rows[e.RowIndex].Cells[CH60일선.Index].Value.ToString()) + Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString())) / 60).ToString("#,##0.00")
                            ;
                    }

                    if (dg.Rows[e.RowIndex].Cells[C20일선.Index].Value != null && Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[C20일선.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[CENV상한.Index].Value =
                            (
                            Cls.Val(dg.Rows[e.RowIndex].Cells[C20일선.Index].Value.ToString()) * (1 + (Cls.Val(envRate.Value.ToString()) / 100))
                            ).ToString("#,##0.00");

                    }

                    if (dg.Rows[e.RowIndex].Cells[C20일선.Index].Value != null && Cls.IsNumeric(dg.Rows[e.RowIndex].Cells[C20일선.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[CENV하한.Index].Value =
                            (
                            Cls.Val(dg.Rows[e.RowIndex].Cells[C20일선.Index].Value.ToString()) * (1 - (Cls.Val(envRate.Value.ToString()) / 100))
                            ).ToString("#,##0.00");
                    }
                    //가격변경 - E

                    ComboBox cbo;
                    cbo = (ComboBox)tabPage3.Controls.Find("cbo조건" + Cls.Right(dg.Name, 1), true)[0];
                    
                    //120신고가-S
                    if (cbo.Text.IndexOf("신고가") > -1)
                    {
                        dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value = dg.Rows[e.RowIndex].Cells[C3일선.Index].Value;
                        dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value = dg.Rows[e.RowIndex].Cells[C5일선.Index].Value;
                        dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value = dg.Rows[e.RowIndex].Cells[C10일선.Index].Value;
                    }
                    else if (cbo.Text.IndexOf("일목균형표") > -1)
                    {
                        dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value = dg.Rows[e.RowIndex].Cells[C5일선.Index].Value;
                        dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value = dg.Rows[e.RowIndex].Cells[C10일선.Index].Value;
                        dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value = dg.Rows[e.RowIndex].Cells[C20일선.Index].Value;
                    }
                    else if (cbo.Text.IndexOf("연속음봉") > -1)
                    {
                        dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value = dg.Rows[e.RowIndex].Cells[C10일선.Index].Value;
                        dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value = dg.Rows[e.RowIndex].Cells[C20일선.Index].Value;
                        dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value = dg.Rows[e.RowIndex].Cells[C40일선.Index].Value;
                    }
                    //120신고가-E


                    //색깔변경 - S
                    if (dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value != null && dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value.ToString().Trim() != "")
                    {
                        if (Cls.Val(dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                        {
                            dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                        }
                        else
                        {
                            dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Style.BackColor = System.Drawing.Color.Empty;
                        }
                    }

                    if (dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value != null && dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value.ToString().Trim() != "")
                    {
                        if (Cls.Val(dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                        {
                            dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                        }
                        else
                        {
                            dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Style.BackColor = System.Drawing.Color.Empty;
                        }
                    }

                    if (dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value != null && dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value.ToString().Trim() != "")
                    {
                        if (Cls.Val(dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                        {
                            dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                        }
                        else
                        {
                            dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Style.BackColor = System.Drawing.Color.Empty;
                        }
                    }

                    if (Cls.Val(dg.Rows[e.RowIndex].Cells[C3일선.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C3일선.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C3일선.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.Val(dg.Rows[e.RowIndex].Cells[C5일선.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C5일선.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C5일선.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.Val(dg.Rows[e.RowIndex].Cells[C10일선.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C10일선.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C10일선.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (dg.Rows[e.RowIndex].Cells[C15일선.Index].Value != null && Cls.Val(dg.Rows[e.RowIndex].Cells[C15일선.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C15일선.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C15일선.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (dg.Rows[e.RowIndex].Cells[C20일선.Index].Value != null && Cls.Val(dg.Rows[e.RowIndex].Cells[C20일선.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C20일선.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C20일선.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (dg.Rows[e.RowIndex].Cells[C40일선.Index].Value != null && Cls.Val(dg.Rows[e.RowIndex].Cells[C40일선.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C40일선.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C40일선.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (dg.Rows[e.RowIndex].Cells[C60일선.Index].Value != null && Cls.Val(dg.Rows[e.RowIndex].Cells[C60일선.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C60일선.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C60일선.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (Cls.Val(dg.Rows[e.RowIndex].Cells[C세력선_L.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C세력선_L.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C세력선_L.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.Val(dg.Rows[e.RowIndex].Cells[C세력선_C.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C세력선_C.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C세력선_C.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.Val(dg.Rows[e.RowIndex].Cells[C세력선_H.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[C세력선_H.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[C세력선_H.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (dg.Rows[e.RowIndex].Cells[C20일선.Index].Value != null && Cls.Val(dg.Rows[e.RowIndex].Cells[CENV상한.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[CENV상한.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[CENV상한.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (dg.Rows[e.RowIndex].Cells[C20일선.Index].Value != null && Cls.Val(dg.Rows[e.RowIndex].Cells[CENV하한.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[CENV하한.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[CENV하한.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (dg.Rows[e.RowIndex].Cells[CB상한.Index].Value.ToString() != "" && Cls.Val(dg.Rows[e.RowIndex].Cells[CB상한.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[CB상한.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[CB상한.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (dg.Rows[e.RowIndex].Cells[CB하한.Index].Value.ToString() != "" && Cls.Val(dg.Rows[e.RowIndex].Cells[CB하한.Index].Value.ToString()) <= Cls.Val(dg.Rows[e.RowIndex].Cells[C현재가.Index].Value.ToString()))
                    {
                        dg.Rows[e.RowIndex].Cells[CB하한.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg.Rows[e.RowIndex].Cells[CB하한.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }
                    //색깔변경 - S


                    
                    if (dg.Rows[e.RowIndex].Cells[C시가.Index].Value == null) return;
                    int 시가 = Cls.ValInt(dg.Rows[e.RowIndex].Cells[C시가.Index].Value);
                    int 고가 = Cls.ValInt(dg.Rows[e.RowIndex].Cells[C고가.Index].Value);
                    int 저가 = Cls.ValInt(dg.Rows[e.RowIndex].Cells[C저가.Index].Value);

                    if (cbo.Text.IndexOf("시가베팅") > -1)
                    {
                        if (chk조.Checked == true && chk자동매매.Checked == true)
                        {
                            NewsFavAdd(종목코드, "Y", "0", "조" , 시가 , 고가 , 저가 , Cls.ValInt(현재가));
                        }
                    }

                    if (dg.Rows[e.RowIndex].Cells[C시가.Index].Value != null
                        && dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value != null
                        && dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value.ToString().Trim() != ""
                        && dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value.ToString().Trim() != "0"
                        && dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value != null
                        && dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value.ToString().Trim() != ""
                        && dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value.ToString().Trim() != "0"
                        && Convert.ToBoolean(dg.Rows[e.RowIndex].Cells[C체크.Index].Value) != true
                        )
                    {

                        Double 매수가1차 = Cls.Val(dg.Rows[e.RowIndex].Cells[C1차매수가.Index].Value.ToString().Trim());
                        Double 매수가2차 = Cls.Val(dg.Rows[e.RowIndex].Cells[C2차매수가.Index].Value.ToString().Trim());
                        Double percent = 100;
                        Double mPrice = 0;
                        DataRow[] drFinance;
                        //string lowPDate = "";

                        drFinance = _ds전체재정.Tables[0].Select(String.Format("STOCK_CODE = '{0}' AND VOLUME > 50000", 종목코드));
                        if (drFinance.Length < 1) return;
                        DataRow dr = drFinance[0];
                        //시가 갭 하락이 일어 날 경우 개미털기일 가능성이 높기 때문에 상승 확률이 높다
                        Double gabRate;
                        int preEndPrice;
                        int preSPrice;
                        preEndPrice = Math.Abs(Cls.ValInt(dr["END_PRICE"].ToString()));
                        preSPrice = Math.Abs(Cls.ValInt(dr["S_PRICE"].ToString()));
                        gabRate = (double)(시가 - preEndPrice) / preEndPrice * 100;

                        if (preEndPrice < preSPrice) dg.Rows[e.RowIndex].Cells[C저가.Index].Style.ForeColor = System.Drawing.Color.Red; //전일음봉
                        if (gabRate < -1) dg.Rows[e.RowIndex].Cells[C시가.Index].Style.ForeColor = System.Drawing.Color.Red; // 전일대비 갭하락

                        if (시가 > 매수가1차 && 현재가 > 매수가1차) { percent = (현재가 - 매수가1차) / 매수가1차 * 100; mPrice = 매수가1차; }
                        else if (시가 > 매수가2차 && 현재가 > 매수가2차) { percent = (현재가 - 매수가2차) / 매수가2차 * 100; mPrice = 매수가2차; }
                        else
                        {
                            if (
                                dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value != null
                                && dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value.ToString().Trim() != ""
                                && dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value.ToString().Trim() != "0")
                            {
                                Double 매수가3차 = Cls.Val(dg.Rows[e.RowIndex].Cells[C3차매수가.Index].Value.ToString().Trim());
                                percent = (현재가 - 매수가3차) / 매수가3차 * 100;
                                mPrice = 매수가3차;
                            }
                        }

                        if (percent < Cls.Val(nmCMonitor.Value))
                        {
                            if (chk조.Checked && chk자동매매.Checked)
                            {
                                NewsFavAdd(종목코드 , "Y" , mPrice.ToString() , "조" , 시가 , 고가 , 저가 , Cls.ValInt(현재가));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger("에러 (dg조건종목_CellValueChanged)", ex.Message);
                }
                finally
                {
                }
            }
        }

        private void btnDiscon_Click(object sender, EventArgs e)
        {

        }


        private void UcMainStockVer2_OnEventConnect(string status)
        {
            if (status == "로그인 실패") return;

            ucMainStockVer2.GetAccount();

            foreach (DataRow dr in ucMainStockVer2._AccNo.Tables["ACCNO"].Rows)
            {
                cboAccount.Items.Add(dr["ACCNO"].ToString().Trim());
            }
            cboAccount.SelectedIndex = 0;

            if (cboAccount.Text == "3228538611" || cboAccount.Text == "8090913611" || cboAccount.Text == "5116998410" || System.Environment.MachineName.Equals("EDPB2F012"))
            {
                _stockId = "000003";
            }
            else if (cboAccount.Text == "3242394711" || cboAccount.Text == "5095390910" || cboAccount.Text == "8086172111")
            {
                _stockId = "000005";
            }
            else if (cboAccount.Text == "8087645611")
            {
                _stockId = "000002";
            }

            cboTickStd.SelectedIndex = 2;
            txtTmNaver2.Text = tmrNews.Interval.ToString();
            txtTmDart3.Text = tmrDart.Interval.ToString();
            dgUcReal.DataSource = ucMainStockVer2._DtScreenNoManage;
            dgLog.DataSource = ucMainStockVer2._sLogger;
            dg화면관리.DataSource = _dt화면관리;

            ucHogaWindowNew1.MainStock = ucMainStockVer2;
            ucFinance1.UcStockMain = ucMainStockVer2;

            btnStopLoss.PerformClick();
            _pgr.Minimum = 0;
            _pgr.Maximum = 100;
            _pgr.Location = tbStockList.Location;
            _pgr.Size = new System.Drawing.Size(this.Width, 40);
            this.Controls.Add(_pgr);
            _pgr.BringToFront();

            tmrNews.Enabled = false;
            tmrDart.Enabled = false;
            tmrDartKrx.Enabled = false;

            var tMi = GetMiOrder();
            var t1 = new Task(() => GetStockDayDataQuery());
            t1.Start();

            if (Cls.GetWeekOfDay(DateTime.Now) != "토" && Cls.GetWeekOfDay(DateTime.Now) != "일")
            {
                if (Cls.ValInt(DateTime.Now.ToString("HH")) > 18 || Cls.ValInt(DateTime.Now.ToString("HH")) < 7)
                {
                    //tmrDart.Interval = 60000;
                    //tmrDartKrx.Interval = 60000;
                    tmrNews.Interval = 300;
                    tmrDart.Interval = 300;
                    tmrDartKrx.Interval = 300;
                }

                if ((cboAccount.Text.Equals("3228538611") || cboAccount.Text.Equals("8087774611")) && !System.Environment.MachineName.Equals("EDPB2F012"))
                {
                    tmrNews.Enabled = false;
                    tmrDart.Enabled = false;
                    tmrDartKrx.Enabled = false;
                    btnDartOnOff.Text = "▶";
                }
                else {
                    tmrNews.Enabled = true;
                    tmrDart.Enabled = true;
                    tmrDartKrx.Enabled = true;
                    btnDartOnOff.Text = "||";
                }

                if (cboAccount.Text.Equals("5116998410"))
                {
                    tmrDart.Interval = 300;
                    tmrDartKrx.Interval = 300;
                    splitContainer1.Panel1Collapsed = false;
                    txtTmDart3.Text = "300";
                }
                else
                {
                    splitContainer1.Panel1Collapsed = true;
                }

                if(_stockId.Equals("000003"))
                    ucMainStockVer2.btnLoggerStart.PerformClick();
                else
                    tmrNews.Enabled = false;
            }
        }

        ProgressBar _pgr = new ProgressBar();
        private void GetStockDayDataQuery()
        {
            //TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            DataAccess dataAcc = new DataAccess();
            
            ArrayParam arrParam = new ArrayParam();
            
            arrParam.Clear();
            arrParam.Add("_QUERY" , "6");
            arrParam.Add("_STOCK_CODE", "");
            arrParam.Add("_STOCK_DATE", "");

            mySqlDbConn conn1 = new mySqlDbConn();
            _ds전체일봉 = conn1.GetDataTableSp("p_stock_day_data_query", arrParam);
            _pgr.Value = 25;

            arrParam.Clear();
            arrParam.Add("_QUERY", "2");
            arrParam.Add("_STOCK_CODE", _stockId);
            
            mySqlDbConn conn2 = new mySqlDbConn();
            _ds전체재정 = conn2.GetDataTableSp("p_stock_finance_query", arrParam);
            _pgr.Value = 40;

            arrParam.Clear();
            arrParam.Add("_QUERY", "5");
            arrParam.Add("_STOCK_CODE", "");
            arrParam.Add("_STOCK_DATE", "");

            mySqlDbConn conn3 = new mySqlDbConn();
            _ds전체매동 = conn3.GetDataTableSp("p_stock_buysell_state_query", arrParam);
            _pgr.Value = 55;

            arrParam.Clear();
            arrParam.Add("_QUERY", "1");
            arrParam.Add("_ACCOUNT_NO", cboAccount.Text);
            arrParam.Add("_STOCK_CODE", "");

            mySqlDbConn conn4 = new mySqlDbConn();
            _ds익절 = conn4.GetDataTableSp("p_stock_profit_query", arrParam);
            _pgr.Value = 70;

            //conn.Close();
            
            ucFinance1.Prop_DayDs = _ds전체일봉;
            SetTheme();
            if (!cboAccount.Text.Equals("5116998410") && !System.Environment.MachineName.Equals("EDPB2F012") && Cls.ValInt(DateTime.Now.ToString("HHmmss")) <= 133000)
            { 
                SetTickInit();
            }
            _pgr.Value = 85;


            ucMainStockVer2.Opw00018_OnReceiveTrData(cboAccount.Text.Trim());
            _pgr.Value = 100;
            _pgr.Visible = false;
            _isInit = true;
        }

        private void SetTheme()
        {
            DataSet ds;

            ds = _DataAcc.p_stock_theme_query("2", "", "", false);
            cboTheme.Items.Clear();
            cboTheme.Items.Add("[테마]");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                cboTheme.Items.Add(dr["theme_name"].ToString().Trim());
            }
            cboTheme.SelectedIndex = 0;
        }

        private void OnSelectVolumeC(PaikRichStock.UcForm.ucVolumeChartBase.Struture_ColumnName columnName, DataSet ds, DataSet ds2th)
        {
            PaikRichStock.MenuItem1.frmVolumeChart oForm = new PaikRichStock.MenuItem1.frmVolumeChart();
            oForm.ds = ds.Copy();
            oForm.ColumnName = columnName;

            oForm.Show();
        }

        DataSet _dsTickInit;
        private void SetTickInit()
        {
            string stockCodes = "";
            DataRow tickRow;
            string tempStr = "";
            _dsTickInit = _DataAcc.p_stock_tick_query("1", _stockId, "", "", "", null, null);
            if (_dsTickInit.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drTick in _dsTickInit.Tables[0].Rows)
                {
                    if (tempStr != drTick["STOCK_CODE"].ToString().Trim())
                    {
                        stockCodes += drTick["STOCK_CODE"].ToString().Trim() + ";";
                    }
                    tempStr = drTick["STOCK_CODE"].ToString().Trim();
                }

                SetDsScreenNo("A", "4", "1", stockCodes, "", false);

                DataView dv = new DataView(_dsTickInit.Tables[0]);
                dv.Sort = "STOCK_DATE DESC , S_TIME DESC";
                foreach (DataRowView drTick in dv)
                {
                    if (drTick["STOCK_CODE"].Equals("")) continue;
                    if (_dsTick60.Tables[drTick["STOCK_CODE"].ToString().Trim()].Rows.Count >= 48) continue;

                    tickRow = _dsTick60.Tables[drTick["STOCK_CODE"].ToString().Trim()].NewRow();
                    tickRow["일자"] = drTick["STOCK_DATE"];
                    tickRow["현재가"] = drTick["C_PRICE"];
                    tickRow["시가"] = drTick["S_PRICE"];
                    tickRow["고가"] = drTick["H_PRICE"];
                    tickRow["저가"] = drTick["L_PRICE"];
                    tickRow["등락율"] = drTick["RATE"];
                    tickRow["체결강도"] = drTick["POWER_RATE"];
                    tickRow["매수거래량"] = Math.Abs(Cls.ValInt(drTick["BUY_VOLUME"]));
                    tickRow["매도거래량"] = Math.Abs(Cls.ValInt(drTick["SELL_VOLUME"])) * -1;
                    tickRow["매수거래비용"] = Math.Abs(Cls.ValInt(drTick["BUY_TRADING_P"]));
                    tickRow["매도거래비용"] = Math.Abs(Cls.ValInt(drTick["SELL_TRADING_P"])) * -1;
                    tickRow["시작시간"] = drTick["S_TIME"];
                    tickRow["종료시간"] = drTick["E_TIME"];
                    tickRow["LINE5"] = drTick["LINE5"];
                    tickRow["LINE10"] = drTick["LINE10"];
                    tickRow["LINE20"] = drTick["LINE20"];
                    tickRow["LINE40"] = drTick["LINE40"];
                    tickRow["LINE60"] = drTick["LINE60"];
                    tickRow["BBUP"] = drTick["BBUP"];
                    tickRow["BBDOWN"] = drTick["BBDOWN"];
                    tickRow["매수유무"] = drTick["IS_BUY"];
                    tickRow["매도유무"] = drTick["IS_SELL"];
                    tickRow["매수신호"] = drTick["IS_BUYSIGNAL"];
                    tickRow["매도신호"] = drTick["IS_SELLSIGNAL"];
                    tickRow["COUNT"] = 30;
                    _dsTick60.Tables[drTick["STOCK_CODE"].ToString().Trim()].Rows.InsertAt(tickRow, 0);
                }

            }

        }

        
        private void GetFavList(string InterId)
        {
            dg관종.CellValueChanged -= dg관종_CellValueChanged;
            dg관종.CellValueChanged -= dg관종_CellValueChanged;
            if(cboTheme.Items.Count > 0) cboTheme.SelectedIndex = 0;

            string stockCodes = "";
            dg관종.RowCount = 0;
            
            DataGridViewRow dgv;
            DataSet ds;

            if (InterId == "A")
            {
                ds = _DataAcc.p_Psi02Query("4", _stockId, 0, "", "", "", null, null);
            }
            else
            {
                ds = _DataAcc.p_Psi02Query("1", _stockId, Cls.ValInt(InterId), "", "", "", null, null);
            }


            Decimal ret;
            if (ds.Tables[0].Rows.Count < 1) return;

            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (ucMainStockVer2._allStockDataset == null) return;
                    
                    dg관종.RowCount += 1;
                    dgv = dg관종.Rows[dg관종.RowCount - 1];
                    dgv.Cells["F_삭제"].Value = "D";
                    dgv.Cells[F_종목코드.Index].Value = dr["STOCK_CODE"].ToString().Trim();
                    dgv.Cells["F_종목명"].Value = ucMainStockVer2.GetStockInfo(dr["STOCK_CODE"].ToString().Trim());

                    DataTable dt = SetLine(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgv.Cells[H_3일선.Index].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                        dgv.Cells[H_5일선.Index].Value = dt.Rows[0]["line5"].ToString(); //H_5일선
                        dgv.Cells[H_10일선.Index].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                        dgv.Cells[H_15일선.Index].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                        dgv.Cells[H_20일선.Index].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                        dgv.Cells[H_40일선.Index].Value = dt.Rows[0]["line40"].ToString();//H_40일선
                        dgv.Cells[H_60일선.Index].Value = dt.Rows[0]["line60"].ToString();//H_60일선
                        dgv.Cells[H_120일선.Index].Value = dt.Rows[0]["line120"].ToString();//H_120일선
                        dgv.Cells[H_220일선.Index].Value = dt.Rows[0]["line220"].ToString();//H_220일선
                        dgv.Cells[F_세력선_H.Index].Value = (Decimal.TryParse(dt.Rows[0]["MA_H"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["MA_H"].ToString()).ToString("#,##0") : "");//MA_H
                        dgv.Cells[F_세력선_C.Index].Value = (Decimal.TryParse(dt.Rows[0]["MA_C"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["MA_C"].ToString()).ToString("#,##0") : "");//MA_C
                        dgv.Cells[F_세력선_L.Index].Value = (Decimal.TryParse(dt.Rows[0]["MA_L"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["MA_L"].ToString()).ToString("#,##0") : "");//MA_L
                        dgv.Cells[F_B상한.Index].Value = (Decimal.TryParse(dt.Rows[0]["B_UP"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["B_UP"].ToString()).ToString("#,##0.00") : "");//B_UP
                        dgv.Cells[F_B하한.Index].Value = (Decimal.TryParse(dt.Rows[0]["B_DOWN"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["B_DOWN"].ToString()).ToString("#,##0.00") : "");//B_DOWN
                        if (Decimal.TryParse(dgv.Cells[F_B상한.Index].Value.ToString(), out ret) != false && Decimal.TryParse(dgv.Cells[F_B하한.Index].Value.ToString(), out ret) != false && Convert.ToDouble(dgv.Cells[F_B하한.Index].Value.ToString()) != 0)
                        {
                            dgv.Cells[F_BB이격.Index].Value = ((Cls.Val(dgv.Cells[F_B상한.Index].Value.ToString()) - Cls.Val(dgv.Cells[F_B하한.Index].Value.ToString())) / Cls.Val(dgv.Cells[F_B하한.Index].Value.ToString()) * 100).ToString("#,##0.0");
                        }
                        if (Cls.IsDb(_ds전체일봉) == true)
                        {
                            dgv.Cells[F_시가.Index].Value = Cls.ValInt(dt.Rows[0]["s_price"]).ToString("#,##0");
                            dgv.Cells[F_저가.Index].Value = Cls.ValInt(dt.Rows[0]["l_price"]).ToString("#,##0");
                            dgv.Cells[F_고가.Index].Value = Cls.ValInt(dt.Rows[0]["h_price"]).ToString("#,##0");
                            dgv.Cells[F_거래량.Index].Value = Cls.ValInt(dt.Rows[0]["volume"]).ToString("#,##0");
                            dgv.Cells[F_거래대금.Index].Value = Cls.ValInt64(dt.Rows[0]["trading_value"]).ToString("#,##0");
                            dgv.Cells[F_등락율.Index].Value = Cls.Val(dt.Rows[0]["rate"]).ToString("0.00");
                            dgv.Cells[F_현재가.Index].Value = Cls.ValInt(dt.Rows[0]["end_price"]).ToString("#,##0");
                            dgv.Cells[F_대비.Index].Value = Cls.ValInt(dt.Rows[0]["daebi"]).ToString("#,##0");
                        }
                    }

                    dt = SetFinance(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgv.Cells[F_업종.Index].Value = dt.Rows[0]["CLASS_GB"].ToString();
                        dgv.Cells[F_신용비율.Index].Value = dt.Rows[0]["CREDIT_RATE"].ToString();
                        dgv.Cells[F_시가총액.Index].Value = dt.Rows[0]["STOCK_TOTAL_P"].ToString();
                        dgv.Cells[F_PER.Index].Value = dt.Rows[0]["PER"].ToString();
                        dgv.Cells[F_ROE.Index].Value = dt.Rows[0]["ROE"].ToString();
                        dgv.Cells[F_PBR.Index].Value = dt.Rows[0]["PBR"].ToString();
                        dgv.Cells[F_EV.Index].Value = dt.Rows[0]["EV"].ToString();
                        dgv.Cells[F_EPS.Index].Value = dt.Rows[0]["EPS"].ToString();
                        dgv.Cells[F_BPS.Index].Value = dt.Rows[0]["BPS"].ToString();
                        dgv.Cells[F_영업이익.Index].Value = dt.Rows[0]["O_PROFIT"].ToString();
                        dgv.Cells[F_당기순이익.Index].Value = dt.Rows[0]["P_PROFIT"].ToString();
                        dgv.Cells[F_250최고가.Index].Value = dt.Rows[0]["HIGH_250"].ToString();
                        dgv.Cells[F_250최저가.Index].Value = dt.Rows[0]["LOW_250"].ToString();
                        dgv.Cells[F_테마.Index].Value = dt.Rows[0]["THEME_NAME"].ToString();

                        try
                        {
                            Cls.ChangeColor(dgv, "D", 7, F_신용비율.Index);
                            Cls.ChangeColor(dgv, "D", 3, F_PBR.Index);
                            Cls.ChangeColor(dgv, "A", 0, F_영업이익.Index, F_당기순이익.Index);

                            //if (Cls.Val(dgv.Cells[F_신용비율.Index].Value.ToString()) > 5) dgv.Cells[F_신용비율.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_신용비율.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            //if (Cls.Val(dgv.Cells[F_PBR.Index].Value.ToString()) > 3) dgv.Cells[F_PBR.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_PBR.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            //if (Cls.Val(dgv.Cells[F_영업이익.Index].Value.ToString()) < 0) dgv.Cells[F_영업이익.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_영업이익.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            //if (Cls.Val(dgv.Cells[F_당기순이익.Index].Value.ToString()) < 0) dgv.Cells[F_당기순이익.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_당기순이익.Index].Style.ForeColor = System.Drawing.Color.Empty;
                        }
                        catch { }
                    }

                    dt = SetBuySellState(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgv.Cells[F_외국인.Index].Value = dt.Rows[0]["F_TU"].ToString();
                        dgv.Cells[F_기관.Index].Value = dt.Rows[0]["K_TU"].ToString();
                        dgv.Cells[F_외국인10.Index].Value = dt.Rows[1]["F_TU"].ToString();
                        dgv.Cells[F_기관10.Index].Value = dt.Rows[1]["K_TU"].ToString();

                        Cls.ChangeColor(dgv, "A", 0, F_외국인.Index, F_기관.Index, F_외국인10.Index, F_기관10.Index);
                    }

                    if (dr["MONITOR_YN"].ToString().Trim() != "")
                    {
                        dg관종.CellValueChanged -= dg관종_CellValueChanged;
                        dg관종.CellValueChanged -= dg관종_CellValueChanged;
                        dgv.Cells["F_모니터링"].Value = dr["MONITOR_YN"].ToString().Trim();
                    }

                    stockCodes += dr["STOCK_CODE"].ToString().Trim() + ";";
                }

                if (Cls.IsDb(_ds전체일봉) == true) { }
                else
                {
                    SetDsScreenNo("A", "4", "1", stockCodes, "", true);
                }
            }
            finally
            {
                dg관종.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dg관종_CellValueChanged);
            }
        }

        private void SetNewsOrder(DataSet ds)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            string stockCode = dr["종목코드"].ToString().Trim().Substring(1);
            
            var obj = (from DataGridViewRow dgr in dg뉴스체결.Rows
                       where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(stockCode)
                       select dgr).FirstOrDefault();

            if (obj != null)
            {
                DataGridViewRow dgr = (DataGridViewRow)obj;

                dgr.Cells[N주문번호.Index].Value = dr["주문번호"].ToString().Trim();

                if (!Cls.ValInt(dr["주문가격"]).Equals(0))
                {
                    dgr.Cells[N주문가.Index].Value = Math.Abs(Cls.ValInt(dr["주문가격"]));
                }

                if (!Cls.ValInt(dr["주문수량"]).Equals(0))
                {
                    dgr.Cells[N매수수량.Index].Value = Math.Abs(Cls.ValInt(dr["주문수량"]));
                }

                if (!Cls.ValInt(dr["체결가"]).Equals(0))
                {
                    dgr.Cells[N주문가.Index].Value = Math.Abs(Cls.ValInt(dr["체결가"]));
                }

                //if (dr["주문상태"].Equals("접수"))
                //{
                //    dgr.Cells[N주문가.Index].Value = Math.Abs(Cls.ValInt(dr["주문가격"].ToString()));
                //    dgr.Cells[N주문번호.Index].Value = dr["주문번호"].ToString().Trim();
                //    dgr.Cells[N매수수량.Index].Value = dr["주문수량"].ToString().Trim();
                //}
                //else if (dr["주문상태"].Equals("체결"))
                //{
                //    dgr.Cells[N주문가.Index].Value = Math.Abs(Cls.ValInt(dr["체결가"]));

                //}
            }
        }

        private void UcMainStockVer2_OnReceiveChejanData(DataSet ds)
        {
            Cls.WriteDataSet(ds);
            var tJan = SettingJango(ds);
            tJan.ContinueWith((t) => CalcAccountMi(Cls.Right(ds.Tables[0].Rows[0]["종목코드"].ToString(), 6))); //미체결내역 계산
            tJan.Wait();
        }

        private void CalcAccountMi(string stockCode)
        {
            int sumMi = 0;
            var objRow = (from DataGridViewRow dgr in dg미체결.Rows
                          where dgr.Cells[M종목코드.Index].Value != null && dgr.Cells[M종목코드.Index].Value.Equals(stockCode) && dgr.Cells[M주문구분.Index].Value.ToString().IndexOf("매도") > -1
                          select dgr);
            var objJangoRow = (from DataGridViewRow dgr in dg뉴스잔고.Rows
                               where dgr.Cells[J종목코드.Index].Value != null && dgr.Cells[J종목코드.Index].Value.Equals(stockCode)
                               select dgr).FirstOrDefault();

            foreach (DataGridViewRow dgr in objRow)
            {
                sumMi += Cls.ValInt(dgr.Cells[M미체결수량.Index].Value.ToString());
            }

            if (objJangoRow != null)
            {
                objJangoRow.Cells[J매매가능수량.Index].Value = Cls.ValInt(objJangoRow.Cells[J보유수량.Index].Value.ToString()) - sumMi;
            }
        }

        private Task SettingJango(DataSet ds)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            DataRow dr;
            string stockCode = "";
            dr = ds.Tables[0].Rows[0];
            stockCode = Cls.Right(dr["종목코드"].ToString(), 6);
            if (dr["주문번호"].Equals(""))
            {
                goto skip;
            }
            var objRow = (from DataGridViewRow dgr in dg미체결.Rows
                       where dgr.Cells[M주문번호.Index].Value != null && dgr.Cells[M주문번호.Index].Value.Equals(dr["주문번호"].ToString().Trim())
                        select dgr).FirstOrDefault();

            var objJangoRow = (from DataGridViewRow dgr in dg뉴스잔고.Rows
                               where dgr.Cells[J종목코드.Index].Value != null && dgr.Cells[J종목코드.Index].Value.Equals(stockCode)
                          select dgr).FirstOrDefault();

            var objNewsOrder = (from DataGridViewRow dgr in dg뉴스체결.Rows
                                where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(stockCode) && (dgr.Cells[N모니터링구분.Index].Value.Equals("공") || dgr.Cells[N모니터링구분.Index].Value.Equals("수") || dgr.Cells[N모니터링구분.Index].Value.Equals("조"))
                               select dgr).FirstOrDefault();

            if (objRow == null) 
            {
                if (Cls.Val(dr["미체결수량"].ToString()) > 0 && !dr["주문구분"].Equals("매수취소") && !dr["주문구분"].Equals("매도취소"))
                {
                    dg미체결.Rows.Insert(0, 1);
                    objRow = dg미체결.Rows[0];
                }
            }


            objRow.Cells[M주문가격.Index].Value = dr["주문가격"];
            objRow.Cells[M주문수량.Index].Value = dr["주문수량"];
            objRow.Cells[M미체결수량.Index].Value = dr["미체결수량"];
  
            if (!dr["주문상태"].Equals("체결") && !dr["주문번호"].Equals(""))
            {  //체결외
                objRow.Cells[M주문구분.Index].Value = dr["주문구분"]; //매수 , 매도 , 매수정정 , 매도정정 , 매수취소 , 매도취소
                objRow.Cells[M원주문번호.Index].Value = Cls.Val(dr["원주문번호"].ToString()) == 0 ? "" : dr["원주문번호"].ToString();
                objRow.Cells[M주문상태.Index].Value = dr["주문상태"]; //접수 , 확인 , 체결
                objRow.Cells[M주문번호.Index].Value = dr["주문번호"];
                objRow.Cells[M종목코드.Index].Value = stockCode;
                objRow.Cells[M종목명.Index].Value = dr["종목명"].ToString().Trim();
                objRow.Cells[M매매구분.Index].Value = dr["매매구분"];//보통 , 시간외 , 시장가 기타등등
                objRow.Cells[M시간.Index].Value = dr["주문/체결시간"];

                dg미체결.Sort(M주문번호, System.ComponentModel.ListSortDirection.Descending);
            }
            else //체결
            {
                if (objJangoRow == null && dr["주문구분"].ToString().IndexOf("매수") > -1) //잔고에 없을시에 새로 만들어준다
                {
                    dg뉴스잔고.RowCount++;
                    objJangoRow = dg뉴스잔고.Rows[dg뉴스잔고.Rows.Count - 1];
                    objJangoRow.Cells[J종목코드.Index].Value = stockCode;
                    objJangoRow.Cells[J종목명.Index].Value = dr["종목명"].ToString().Trim();
                    objJangoRow.Cells[J수익률.Index].Value = 0;
                    objJangoRow.Cells[J매매가능수량.Index].Value = dr["단위체결량"].ToString();
                    objJangoRow.Cells[J매입가.Index].Value = dr["체결가"].ToString().Trim();
                    objJangoRow.Cells[J보유수량.Index].Value = dr["단위체결량"].ToString(); //매입금액 변경 Raise CellValueChange
                    objJangoRow.Cells[J현재가.Index].Value = Math.Abs(Cls.ValInt(dr["현재가"].ToString()));//평가금액 변경 Raise CellValueChange
                    dg뉴스잔고.CellValueChanged -= dg뉴스잔고_CellValueChanged;
                    objJangoRow.Cells[J이익실현1.Index].Value = "0";
                    objJangoRow.Cells[J이익실현2.Index].Value = "0";
                    objJangoRow.Cells[J이익실현3.Index].Value = "0";
                    dg뉴스잔고.CellValueChanged += dg뉴스잔고_CellValueChanged;

                    if (objNewsOrder != null)
                    {
                        objJangoRow.Cells[J구분.Index].Value = "공";
                    }
                    else
                    {
                        objJangoRow.Cells[J구분.Index].Value = "";
                    }

                    SetDsScreenNo("A", "2", "1", stockCode, "", false);
                }
                else if (dr["주문구분"].ToString().IndexOf("매도") > -1) //매도시에 보유수량 계산
                {
                    objJangoRow.Cells[J보유수량.Index].Value = Cls.Val(objJangoRow.Cells[J보유수량.Index].Value.ToString()) - Cls.Val(dr["단위체결량"].ToString()); //매입금액 변경 Raise CellValueChange
                }
                else if (dr["주문구분"].ToString().IndexOf("매수") > -1) //매수시에 보유수량 계산
                {
                    int pre매입금액 = Cls.ValInt(objJangoRow.Cells[J매입금액.Index].Value.ToString());
                    int total매입금액 = pre매입금액 + (Cls.ValInt(dr["단위체결량"].ToString()) * Cls.ValInt(dr["단위체결가"].ToString()));
                    Double 매입가 = Cls.Val(total매입금액.ToString()) / (Cls.Val(objJangoRow.Cells[J보유수량.Index].Value.ToString()) + Cls.Val(dr["단위체결량"].ToString()));

                    objJangoRow.Cells[J매입가.Index].Value = Math.Round(매입가 , 0); 
                    objJangoRow.Cells[J보유수량.Index].Value = Cls.Val(objJangoRow.Cells[J보유수량.Index].Value.ToString()) + Cls.Val(dr["단위체결량"].ToString()); //매입금액 변경 Raise CellValueChange
                }
            }

            if (dr["주문구분"].ToString().IndexOf("취소") == -1 && dr["주문구분"].ToString().IndexOf("매도") == -1)
            {
                SetNewsOrder(ds);
            }

            if (objRow != null && Cls.Val(objRow.Cells[M미체결수량.Index].Value.ToString()) <= 0)
            {
                dg미체결.Rows.Remove(objRow);
            }
        skip:
            tcs.SetResult(true);
            return tcs.Task;
        }

        private void UcMainStockVer2_OnReceiveConditionVer(DataSet ds)
        {
            SetConditionListVer(ds);
        }

        private void UcMainStockVer2_OnReceiveInvestRealData(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveMsg(string msg)
        {
            Logger("일반", msg);
        }

        private void UcMainStockVer2_OnReceiveRealCondition(DataSet ds, string type, string index, string conName)
        {
            DataGridView dg = null;
            string ind = Cls.ValInt(index).ToString("000");
            if (cbo조건1.SelectedIndex != -1 && cbo조건1.Items[cbo조건1.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목1;
            }
            else if (cbo조건2.SelectedIndex != -1 && cbo조건2.Items[cbo조건2.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목2;
            }
            else if (cbo조건3.SelectedIndex != -1 && cbo조건3.Items[cbo조건3.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목3;
            }
            else if (cbo조건4.SelectedIndex != -1 && cbo조건4.Items[cbo조건4.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목4;
            }
            else if (cbo조건5.SelectedIndex != -1 && cbo조건5.Items[cbo조건5.SelectedIndex].ToString().Substring(0, 3) == ind)
            {
                dg = dg조건종목5;
            }
            if (dg == null) return;
            string 종목코드 = "";
            string 종목명 = "";
            string 구분 = "";
            Decimal ret;
            if (ds == null) { return; }
            if (ds.Tables["CondiStockReal"].Rows.Count < 1) { return; }
            DataRow dr = ds.Tables[0].Rows[0];
            종목코드 = dr["STOCK_CODE"].ToString().Trim();
            종목명 = dr["STOCK_NAME"].ToString().Trim();
            구분 = type;
            //var obj = (from DataGridViewRow dgr in dgN관종.Rows
            //           where dgr.Cells[P종목코드.Index].Value != null && dgr.Cells[P종목코드.Index].Value.Equals(종목코드) && dgr.Cells[P모니터링구분.Index].Value.Equals("조")
            //           select dgr).FirstOrDefault();

            var obj조건 = (from DataGridViewRow dgr in dg.Rows
                         where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();


            if (obj조건 != null)
            {
                if (구분.Equals("D"))
                {
                    obj조건.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                    //if (obj != null)
                    //{
                    //    dgN관종.Rows.Remove(obj);
                    //}
                }
                else
                {
                    obj조건.DefaultCellStyle.BackColor = System.Drawing.Color.Empty;
                }
            }
            else 
            {
                if (구분 == "I")
                {
                    dg.Rows.Insert(0, 1);
                    DataGridViewRow dgr = dg.Rows[0];
                    dgr.Cells[C종목코드.Index].Value = 종목코드;
                    dgr.Cells[C종목명.Index].Value = dr["STOCK_NAME"].ToString();
                    try
                    {
                        DataTable dt = SetLine(종목코드);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dgr.Cells[CH3일선.Index].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                            dgr.Cells[CH5일선.Index].Value = dt.Rows[0]["line5"].ToString();//H_5일선
                            dgr.Cells[CH10일선.Index].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                            dgr.Cells[CH15일선.Index].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                            dgr.Cells[CH20일선.Index].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                            dgr.Cells[CH40일선.Index].Value = dt.Rows[0]["line40"].ToString();//H_40일선
                            dgr.Cells[CH60일선.Index].Value = dt.Rows[0]["line60"].ToString();//H_60일선
                            dgr.Cells[C세력선_H.Index].Value = (Decimal.TryParse(dt.Rows[0]["MA_H"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["MA_H"].ToString()).ToString("#,##0") : "");//MA_H
                            dgr.Cells[C세력선_C.Index].Value = (Decimal.TryParse(dt.Rows[0]["MA_C"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["MA_C"].ToString()).ToString("#,##0") : "");//MA_C
                            dgr.Cells[C세력선_L.Index].Value = (Decimal.TryParse(dt.Rows[0]["MA_L"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["MA_L"].ToString()).ToString("#,##0") : "");//MA_L
                            dgr.Cells[CB상한.Index].Value = (Decimal.TryParse(dt.Rows[0]["B_UP"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["B_UP"].ToString()).ToString("#,##0.00") : "");//B_UP
                            dgr.Cells[CB하한.Index].Value = (Decimal.TryParse(dt.Rows[0]["B_DOWN"].ToString(), out ret) ? Cls.Val(dt.Rows[0]["B_DOWN"].ToString()).ToString("#,##0.00") : "");//B_DOWN


                            SetConditionPrice123(0, dg, conName , 0);
                            //SetConditionPrice(0, dg, conName);
                            if (Cls.IsDb(_ds전체일봉) == true)
                            {
                                dgr.Cells[C시가.Index].Value = Cls.ValInt(dt.Rows[0]["s_price"]).ToString("#,##0");
                                dgr.Cells[C저가.Index].Value = Cls.ValInt(dt.Rows[0]["l_price"]).ToString("#,##0");
                                dgr.Cells[C고가.Index].Value = Cls.ValInt(dt.Rows[0]["h_price"]).ToString("#,##0");
                                dgr.Cells[C거래량.Index].Value = Cls.ValInt(dt.Rows[0]["volume"]).ToString("#,##0");
                                //dg.Rows[0].Cells[C거래대금.Index].Value = Cls.ValInt64(dt.Rows[0]["trading_value"]).ToString("#,##0");
                                dgr.Cells[C등락률.Index].Value = Cls.Val(dt.Rows[0]["rate"]).ToString("0.00");
                                dgr.Cells[C현재가.Index].Value = Cls.ValInt(dt.Rows[0]["end_price"]).ToString("#,##0");
                                dgr.Cells[C대비.Index].Value = Cls.ValInt(dt.Rows[0]["daebi"]).ToString("#,##0");
                            }

                            dt = SetFinance(종목코드);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                dgr.Cells["C업종"].Value = dt.Rows[0]["CLASS_GB"].ToString();
                                dgr.Cells["C신용비율"].Value = dt.Rows[0]["CREDIT_RATE"].ToString();
                                dgr.Cells["C시가총액"].Value = dt.Rows[0]["STOCK_TOTAL_P"].ToString();
                                dgr.Cells["CPER"].Value = dt.Rows[0]["PER"].ToString();
                                dgr.Cells["CROE"].Value = dt.Rows[0]["ROE"].ToString();
                                dgr.Cells["CPBR"].Value = dt.Rows[0]["PBR"].ToString();
                                dgr.Cells["CEV"].Value = dt.Rows[0]["EV"].ToString();
                                dgr.Cells["CEPS"].Value = dt.Rows[0]["EPS"].ToString();
                                dgr.Cells["CBPS"].Value = dt.Rows[0]["BPS"].ToString();
                                dgr.Cells["C영업이익"].Value = dt.Rows[0]["O_PROFIT"].ToString();
                                dgr.Cells["C당기순이익"].Value = dt.Rows[0]["P_PROFIT"].ToString();
                                dgr.Cells["C250최고가"].Value = dt.Rows[0]["HIGH_250"].ToString();
                                dgr.Cells["C250최저가"].Value = dt.Rows[0]["LOW_250"].ToString();
                                dgr.Cells["C테마"].Value = dt.Rows[0]["THEME_NAME"].ToString();

                                try
                                {
                                    Cls.ChangeColor(dgr, "D", 7, C신용비율.Index);
                                    Cls.ChangeColor(dgr, "D", 3, CPBR.Index);
                                    Cls.ChangeColor(dgr, "A", 0, C영업이익.Index, C당기순이익.Index);

                                    //if (Cls.Val(dgr.Cells["C신용비율"].Value.ToString()) > 5) dgr.Cells["C신용비율"].Style.ForeColor = System.Drawing.Color.Red;
                                    //else dgr.Cells["C신용비율"].Style.ForeColor = System.Drawing.Color.Empty;
                                    //if (Cls.Val(dgr.Cells["CPBR"].Value.ToString()) > 3) dgr.Cells["CPBR"].Style.ForeColor = System.Drawing.Color.Red;
                                    //else dgr.Cells["CPBR"].Style.ForeColor = System.Drawing.Color.Empty;
                                    //if (Cls.Val(dgr.Cells["C영업이익"].Value.ToString()) < 0) dgr.Cells["C영업이익"].Style.ForeColor = System.Drawing.Color.Red;
                                    //else dgr.Cells["C영업이익"].Style.ForeColor = System.Drawing.Color.Empty;
                                    //if (Cls.Val(dgr.Cells["C당기순이익"].Value.ToString()) < 0) dgr.Cells["C당기순이익"].Style.ForeColor = System.Drawing.Color.Red;
                                    //else dgr.Cells["C당기순이익"].Style.ForeColor = System.Drawing.Color.Empty;
                                }
                                catch { }
                            }
                            dt = SetBuySellState(종목코드);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                dgr.Cells[C외국인.Index].Value = dt.Rows[0]["F_TU"].ToString();
                                dgr.Cells[C기관.Index].Value = dt.Rows[0]["K_TU"].ToString();
                                dgr.Cells[C외국인10.Index].Value = dt.Rows[1]["F_TU"].ToString();
                                dgr.Cells[C기관10.Index].Value = dt.Rows[1]["K_TU"].ToString();

                                Cls.ChangeColor(dgr, "A", 0, C외국인.Index, C기관.Index, C외국인10.Index, C기관10.Index);

                                //if (Cls.Val(dg.Rows[0].Cells[C외국인.Index].Value.ToString()) > 0) dg.Rows[0].Cells[C외국인.Index].Style.ForeColor = System.Drawing.Color.Red;
                                //else dg.Rows[0].Cells[C외국인.Index].Style.ForeColor = System.Drawing.Color.Blue;
                                //if (Cls.Val(dg.Rows[0].Cells[C기관.Index].Value.ToString()) > 0) dg.Rows[0].Cells[C기관.Index].Style.ForeColor = System.Drawing.Color.Red;
                                //else dg.Rows[0].Cells[C기관.Index].Style.ForeColor = System.Drawing.Color.Blue;
                            }
                        }
                        
                        if (Cls.IsDb(_ds전체일봉) == true){}
                        else
                        {
                            SetDsScreenNo("A", "3", "1", 종목코드, dr["STOCK_NAME"].ToString().Trim(), false);
                        }

                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    finally
                    {
                    }
                }
            }

        }

        private void UcMainStockVer2_OnReceiveRealData_HogaJanQty(DataSet ds)
        {
            if (ds == null) return;
            if (ds.Tables.Count == 0) return;

            if (ucHogaWindowNew1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
            {
                ucHogaWindowNew1.Property_GetStockHogaJanQty = ds;
            }
        }

        private void UcMainStockVer2_OnReceiveRealData_PriorityHoga(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveRealData_Sise(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveRealData_StockInfo(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveRealData_TimeOutHoga(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveRealData_TodayTradePort(DataSet ds)
        {
            if (ds.Tables.Count == 0) return;

            if (ucHogaWindowNew1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
            {
                ucHogaWindowNew1.Property_ToDayStockTradeAt = ds;
            }
        }

        private void UcMainStockVer2_OnReceiveRealData_TradePort(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveRealData_Volume(DataSet ds)
        {
            if (ds.Tables.Count == 0) return;
            SetRealDataVolume(ds);
        }

        private void UcMainStockVer2_OnReceiveTrCondition(DataSet ds, string index, string scrNo, string conName)
        {
            SetConditionList(ds, index, scrNo, conName);
        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10001(DataSet ds)
        {
            if (ds.Tables.Count == 0) return;
            if (ds.Tables[0].Rows.Count == 0) return;
            if (ds.Tables[0].Rows[0]["종목코드"].ToString().Trim() == "") return;
            if (ucFinance1.StockCode == ds.Tables[0].Rows[0]["종목코드"].ToString().Trim())
            {
                ucFinance1.Prop_StockBaseInfo = ds;
                tbStockList.SelectedIndex = 4;
            }
        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10002(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10003(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10004(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10005(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10006(DataSet ds)
        {
            //SettingOPT10006(ds);
        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10007(DataSet ds)
        {
            SettingOPT10007(ds);
        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10008(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10009(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10010(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10011(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10012(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10013(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10014(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10015(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10016(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10017(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10018(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10019(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10020(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10021(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10022(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10023(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10024(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10025(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10026(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10027(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10028(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10029(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10030(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10031(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10032(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10033(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_opt10085(DataSet ds)
        {

        }


        private void UcMainStockVer2_OnReceiveTrData_Opw00018(DataSet ds)
        {
            string strStockCodes = "";
            //DataRow dr;
            //string colName;
            //_dt보유잔고.Rows.Clear;

            DataGridViewRow dgr;
            dg뉴스잔고.RowCount = 0;
            ds = ConvertDsNumber(ds, J매입가.Index);
            //private string[] _보유잔고 = { "종목번호", "종목명", "매입가", "수익률(%)", "현재가", "대비", "등락율", "매입금액", "평가금액", "보유수량", "매매가능수량" , "이익실현1" , "이익실현2" , "이익실현3"};
            for (int row = 0; row < ds.Tables[0].Rows.Count ; row++)
            {
                dg뉴스잔고.RowCount += 1;
                dgr = dg뉴스잔고.Rows[dg뉴스잔고.RowCount - 1];
                dgr.Cells[J종목코드.Index].Value = Cls.Right(ds.Tables[0].Rows[row]["종목번호"].ToString() , 6);
                dgr.Cells[J종목명.Index].Value = ds.Tables[0].Rows[row]["종목명"].ToString();
                dgr.Cells[J매입가.Index].Value = ds.Tables[0].Rows[row]["매입가"].ToString();
                dgr.Cells[J수익률.Index].Value = ds.Tables[0].Rows[row]["수익률(%)"].ToString();
                dgr.Cells[J현재가.Index].Value = ds.Tables[0].Rows[row]["현재가"].ToString();
                dgr.Cells[J매입금액.Index].Value = ds.Tables[0].Rows[row]["매입금액"].ToString();
                dgr.Cells[J평가금액.Index].Value = ds.Tables[0].Rows[row]["평가금액"].ToString();
                dgr.Cells[J보유수량.Index].Value = ds.Tables[0].Rows[row]["보유수량"].ToString();
                dgr.Cells[J매매가능수량.Index].Value = ds.Tables[0].Rows[row]["매매가능수량"].ToString();

                Cls.ChangeColor(dgr, "A", 0, J수익률.Index);

                //이익실현 부분 셋팅 DB 의 내용을 불러옴 - S
                dg뉴스잔고.CellValueChanged -= dg뉴스잔고_CellValueChanged;

                if (_ds익절.Tables[0].Select(String.Format("STOCK_CODE = '{0}'", dgr.Cells[J종목코드.Index].Value)).Length == 0)
                {
                    dgr.Cells[J이익실현1.Index].Value = "0";
                    dgr.Cells[J이익실현2.Index].Value = "0";
                    dgr.Cells[J이익실현3.Index].Value = "0";
                }
                else
                {
                    foreach (DataRow dr in _ds익절.Tables[0].Select(String.Format("STOCK_CODE = '{0}'", dgr.Cells[J종목코드.Index].Value)))
                    {
                        if (Cls.Val(dr["SELL1_PRICE"].ToString()) > 0) dgr.Cells[J이익실현1.Index].Value = "1";
                        else dgr.Cells[J이익실현1.Index].Value = "0";
                        if (Cls.Val(dr["SELL2_PRICE"].ToString()) > 0) dgr.Cells[J이익실현2.Index].Value = "1";
                        else dgr.Cells[J이익실현2.Index].Value = "0";
                        if (Cls.Val(dr["SELL3_PRICE"].ToString()) > 0) dgr.Cells[J이익실현3.Index].Value = "1";
                        else dgr.Cells[J이익실현3.Index].Value = "0";
                        //if (Cls.Val(dr["SELL4_PRICE"].ToString()) > 0) dgr.Cells[J이익실현4.Index].Value = "1";
                        //else dgr.Cells[J이익실현4.Index].Value = "0";
                        //if (Cls.Val(dr["SELL5_PRICE"].ToString()) > 0) dgr.Cells[J이익실현5.Index].Value = "1";
                        //else dgr.Cells[J이익실현5.Index].Value = "0";
                        dgr.Cells[J구분.Index].Value = dr["GB"].ToString();
                        break;
                    }
                }
                dg뉴스잔고.CellValueChanged += dg뉴스잔고_CellValueChanged;
                //이익실현 부분 셋팅 DB 의 내용을 불러옴 - E

                strStockCodes += dgr.Cells[J종목코드.Index].Value.ToString() + ";";
            }

            SetDsScreenNo("A", "2", "1", strStockCodes, "", false);
            SystemSleep();
        }
        private void SystemSleep()
        {
            System.Threading.Thread.Sleep(_SLEEP_TIME);
        }

        private void dg관종_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (dg관종.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
            if (dg관종.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "") return;

            if (dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value == null || dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value.ToString() == "" || dg관종.Rows[e.RowIndex].Cells["F_종목명"].Value == null) return;

            string stockCode = dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value.ToString();
            string stockName = dg관종.Rows[e.RowIndex].Cells["F_종목명"].Value.ToString();

            if (e.ColumnIndex == F_모니터링.Index)
            {
                int interId = 0;
                if (rb1.Checked == true) interId = 1;
                else if (rb2.Checked == true) interId = 2;
                else if (rb3.Checked == true) interId = 3;
                else if (rb4.Checked == true) interId = 4;
                else if (rb5.Checked == true) interId = 5;

                _DataAcc.p_Psi02Add("U", _stockId, interId, dg관종.Rows[e.RowIndex].Cells[F_종목코드.Index].Value.ToString(), "00", "", dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value.ToString());
                return;
            }

            if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == false) return;

            if (dg관종.Rows[e.RowIndex].Cells[F_현재가.Index].Value == null) return;
            if (dg관종.Rows[e.RowIndex].Cells[F_시가.Index].Value == null) return;
            if (dg관종.Rows[e.RowIndex].Cells[F_저가.Index].Value == null) return;
            if (dg관종.Rows[e.RowIndex].Cells[F_고가.Index].Value == null) return;
            if (dg관종.Rows[e.RowIndex].Cells[F_거래량.Index].Value == null) return;
            if (dg관종.Rows[e.RowIndex].Cells[F_등락율.Index].Value == null) return;

            if (e.ColumnIndex == F_거래량.Index)
            {
                Double 등락률 = Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_등락율.Index].Value.ToString());
                if (dg관종.Rows[e.RowIndex].Cells[F_모니터링.Index].Value.ToString().Trim() == "하한가" && 등락률 < -29.7)
                {
                    if (_dsTick60.Tables[stockCode] != null)
                    {
                        int tickRow = _dsTick60.Tables[stockCode].Rows.Count - 1;
                        int 저가 = Cls.ValInt(_dsTick60.Tables[stockCode].Rows[tickRow]["저가"].ToString());
                        int 수량 = Cls.ValInt(nm관종매수금액.Value.ToString()) / 저가;
                        if (_dsTick60.Tables[stockCode].Rows[tickRow]["저가"].ToString() != _dsTick60.Tables[stockCode].Rows[tickRow]["고가"].ToString())
                        {
                            if (_dsTick60.Tables[stockCode].Select("일자 = '" + DateTime.Now.ToString("yyyyMMdd") + "' AND 매수유무 = '1'").Length < 1)
                            {
                                SendBuySellMsg(stockCode, "03", 1, 0, 저가, 수량, "2");
                            }
                        }
                    }
                }
            }

            if (e.ColumnIndex == F_대비.Index) //대비
            {
                if (Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_대비"].Value.ToString()) < 0)
                {
                    dg관종.Rows[e.RowIndex].Cells["F_현재가"].Style.ForeColor = System.Drawing.Color.Blue;
                    dg관종.Rows[e.RowIndex].Cells["F_대비"].Style.ForeColor = System.Drawing.Color.Blue;
                    dg관종.Rows[e.RowIndex].Cells["F_등락율"].Style.ForeColor = System.Drawing.Color.Blue;
                }
                else if (Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_대비"].Value.ToString()) > 0)
                {
                    dg관종.Rows[e.RowIndex].Cells["F_현재가"].Style.ForeColor = System.Drawing.Color.Red;
                    dg관종.Rows[e.RowIndex].Cells["F_대비"].Style.ForeColor = System.Drawing.Color.Red;
                    dg관종.Rows[e.RowIndex].Cells["F_등락율"].Style.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    dg관종.Rows[e.RowIndex].Cells["F_현재가"].Style.ForeColor = System.Drawing.Color.Empty;
                    dg관종.Rows[e.RowIndex].Cells["F_대비"].Style.ForeColor = System.Drawing.Color.Empty;
                    dg관종.Rows[e.RowIndex].Cells["F_등락율"].Style.ForeColor = System.Drawing.Color.Empty;
                }
            }


            if (e.ColumnIndex == F_현재가.Index)
            {
                try
                {

                    if (dg관종.Rows[e.RowIndex].Cells[H_3일선.Index].Value == null || dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value == null) return;

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[H_3일선.Index].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_3일선"].Value =
                            ((Cls.Val(dg관종.Rows[e.RowIndex].Cells[H_3일선.Index].Value.ToString()) + Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 3).ToString("#,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_3일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[H_5일선.Index].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_5일선"].Value =
                            ((Cls.Val(dg관종.Rows[e.RowIndex].Cells[H_5일선.Index].Value.ToString()) + Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 5).ToString("#,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_5일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[H_10일선.Index].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_10일선"].Value =
                            ((Cls.Val(dg관종.Rows[e.RowIndex].Cells[H_10일선.Index].Value.ToString()) + Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 10).ToString("#,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_10일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[H_15일선.Index].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_15일선"].Value =
                            ((Cls.Val(dg관종.Rows[e.RowIndex].Cells[H_15일선.Index].Value.ToString()) + Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 15).ToString("#,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_15일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[H_20일선.Index].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value =
                            ((Cls.Val(dg관종.Rows[e.RowIndex].Cells[H_20일선.Index].Value.ToString()) + Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 20).ToString("#,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[H_40일선.Index].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_40일선"].Value =
                            ((Cls.Val(dg관종.Rows[e.RowIndex].Cells[H_40일선.Index].Value.ToString()) + Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 40).ToString("#,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_40일선"].Value = "";
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[H_60일선.Index].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_60일선"].Value =
                            ((Cls.Val(dg관종.Rows[e.RowIndex].Cells[H_60일선.Index].Value.ToString()) + Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 60).ToString("#,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_60일선"].Value = "";
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[H_120일선.Index].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_120일선.Index].Value =
                            ((Cls.Val(dg관종.Rows[e.RowIndex].Cells[H_120일선.Index].Value.ToString()) + Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_현재가.Index].Value.ToString())) / 120).ToString("#,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_120일선.Index].Value = "";
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[H_220일선.Index].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_220일선.Index].Value =
                            ((Cls.Val(dg관종.Rows[e.RowIndex].Cells[H_220일선.Index].Value.ToString()) + Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_현재가.Index].Value.ToString())) / 220).ToString("#,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_220일선.Index].Value = "";
                    }



                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Value =
                            (
                            Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()) * (1 + (Cls.Val(envRate.Value.ToString()) / 100))
                            ).ToString("#,##0.00");

                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Value = "";
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Value =
                            (
                            Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()) * (1 - (Cls.Val(envRate.Value.ToString()) / 100))
                            ).ToString("#,##0.00");
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Value = "";
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_3일선"].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_3일선"].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_3일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_3일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_5일선"].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_5일선"].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_5일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_5일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_10일선"].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_10일선"].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_10일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_10일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_15일선"].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_15일선"].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_15일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_15일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_20일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_20일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_40일선"].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_40일선"].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_40일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_40일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_60일선"].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_60일선"].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_60일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_60일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[F_120일선.Index].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_120일선.Index].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_120일선.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_120일선.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[F_220일선.Index].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_220일선.Index].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_220일선.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_220일선.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[F_세력선_L.Index].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_세력선_L.Index].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_세력선_L.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_세력선_L.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[F_세력선_C.Index].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_세력선_C.Index].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_세력선_C.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_세력선_C.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[F_세력선_H.Index].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_세력선_H.Index].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_세력선_H.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_세력선_H.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[F_B상한.Index].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_B상한.Index].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_B상한.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_B상한.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells[F_B하한.Index].Value.ToString()) && Cls.Val(dg관종.Rows[e.RowIndex].Cells[F_B하한.Index].Value.ToString()) <= Cls.Val(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_B하한.Index].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells[F_B하한.Index].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value.ToString().Trim() != "X" && dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value.ToString().Trim() != "하한가")
                    {
                        DataRow[] drFinance = _ds전체재정.Tables[0].Select(String.Format("STOCK_CODE = '{0}'", stockCode));
                        if (drFinance.Length < 1) return;
                        if (dg관종.Rows[e.RowIndex].Cells["F_시가"].Value == null) return;
                        //시가 갭 하락이 일어 날 경우 개미털기일 가능성이 높기 때문에 상승 확률이 높다
                        DataRow dr = drFinance[0];
                        Double gabRate;
                        int preEndPrice;
                        int 시가 = Cls.ValInt(dg관종.Rows[e.RowIndex].Cells[F_시가.Index].Value);
                        int 고가 = Cls.ValInt(dg관종.Rows[e.RowIndex].Cells[F_고가.Index].Value);
                        int 저가 = Cls.ValInt(dg관종.Rows[e.RowIndex].Cells[F_저가.Index].Value);
                        int 현재가 = Cls.ValInt(dg관종.Rows[e.RowIndex].Cells[F_현재가.Index].Value);
                        preEndPrice = Math.Abs(Cls.ValInt(dr["END_PRICE"].ToString()));
                        gabRate = (double)(시가 - preEndPrice) / preEndPrice * 100;
                        //if (gabRate > -1.0) return;

                        string colName = "F_" + dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value;
                        Double cPrice = Cls.Val(현재가);
                        Double comparePrice = Cls.Val(dg관종.Rows[e.RowIndex].Cells[colName].Value.ToString());
                        Double percent = ((cPrice - comparePrice) / comparePrice * 100);

                        if (percent < Cls.Val(nmFMonitor.Value.ToString()))
                        {
                            if (chk관.Checked == true && chk자동매매.Checked == true)
                            {
                                NewsFavAdd(stockCode, "Y" , comparePrice.ToString(), colName , 시가 , 고가 , 저가 , 현재가);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger("에러 (dg관종_CellValueChanged)", ex.Message);
                }
                finally
                {
                }
            }
        }

        private void dg관종_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dg관종.CellValueChanged -= dg관종_CellValueChanged;
            dg관종.CellValueChanged -= dg관종_CellValueChanged;

            dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value = "X";
            lblTotal.Text = (dg관종.RowCount - 1).ToString();

            dg관종.CellValueChanged += dg관종_CellValueChanged;
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            var t1 = RealDataFavDisconnect();
            if (rb1.Checked == true) GetFavList(rb1.Text.Trim());
        }

        private Task RealDataFavDisconnect()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            DataRow[] drArr = _dt화면관리.Select("화면구분='4' AND 실시간구분 = '1'");
            for(int row = drArr.Length - 1 ; row >= 0 ; row--)
            {
                DataRow[] drArrReal = ucMainStockVer2._DtScreenNoManage.Select("TR_NAME='OptKWFid_OnReceiveRealData' AND STOCK_CODE = '" + drArr[row]["종목코드"].ToString().Trim() + "'");
                if (drArrReal.Length < 1) continue;
                ucMainStockVer2.DisconnectRealData(drArrReal[0]["SCREEN_NO"].ToString().Trim());
                _dt화면관리.Rows.Remove(drArr[row]);
            }
            if (tcs == null || tcs.Task.IsCompleted) return null;
            return tcs.Task;
        }


        private void 시장가매수ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string stockCode = "";
            int 매수수량 = 0;
            int 현재가 = 0;
            var dgObj = (DataGridView)cmsMenu.SourceControl;
            var dgr = dgObj.CurrentRow;

            if (dgr == null) return;
            if (dgObj.Name == dg뉴스잔고.Name) {stockCode = dgr.Cells[J종목코드.Index].Value.ToString().Trim();현재가 = Cls.ValInt(dgr.Cells[J현재가.Index].Value);}
            else if (dgObj.Name == dg관종.Name){stockCode = dgr.Cells[F_종목코드.Index].Value.ToString().Trim();현재가 = Cls.ValInt(dgr.Cells[F_현재가.Index].Value);}
            else if (dgObj.Name == dg조건종목1.Name) {stockCode = dgr.Cells[C종목코드.Index].Value.ToString();현재가 = Cls.ValInt(dgr.Cells[C현재가.Index].Value);}
            else if (dgObj.Name == dg조건종목2.Name) {stockCode = dgr.Cells[C종목코드.Index].Value.ToString();현재가 = Cls.ValInt(dgr.Cells[C현재가.Index].Value);}
            else if (dgObj.Name == dg조건종목3.Name) {stockCode = dgr.Cells[C종목코드.Index].Value.ToString();현재가 = Cls.ValInt(dgr.Cells[C현재가.Index].Value);}
            else if (dgObj.Name == dg조건종목4.Name) {stockCode = dgr.Cells[C종목코드.Index].Value.ToString();현재가 = Cls.ValInt(dgr.Cells[C현재가.Index].Value);}
            else if (dgObj.Name == dg조건종목5.Name) {stockCode = dgr.Cells[C종목코드.Index].Value.ToString(); 현재가 = Cls.ValInt(dgr.Cells[C현재가.Index].Value); }
            else if (dgObj.Name == dgN관종.Name){stockCode = dgr.Cells[P종목코드.Index].Value.ToString().Trim();현재가 = Cls.ValInt(dgr.Cells[P현재가.Index].Value);}

            if(Cls.Val(현재가) == 0) return;
            if (stockCode.Equals("")) return;

            매수수량 = Cls.ValInt(nm관종매수금액.Value) / Cls.ValInt(현재가);
            SendBuySellMsg(stockCode, "03", 1, 0, 0, 매수수량, "2");
        }

        private void 시장가매도ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string stockCode = "";
            int 수량 = 0;
            var dgObj = (DataGridView)cmsMenu.SourceControl;
            var dgr = dgObj.CurrentRow;
            if (dgObj.Name == dg뉴스잔고.Name){stockCode = dgr.Cells[J종목코드.Index].Value.ToString();수량 = Cls.ValInt(dgr.Cells[J매매가능수량.Index].Value);}

            if (stockCode == "") return;
            if (Cls.ValInt(수량) == 0) return;
            SendBuySellMsg(stockCode, "03", 2, 0, 0, 수량, "2");
        }

        private void 매수ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string 종목코드 = "";
            int row = -1;
            int col = -1;
            int 수량 = 0;
            string 현재가 = "";
            string 주문가 = "";
            PaikRichStock.UcForm.ReciveOrder rOrder = new PaikRichStock.UcForm.ReciveOrder();

            if (cmsMenu.SourceControl.Name == dg뉴스잔고.Name)
            {
                if (dg뉴스잔고.CurrentRow == null) return;
                row = dg뉴스잔고.CurrentRow.Index;
                if (dg뉴스잔고.Rows[row].Cells[J종목코드.Index].Value == null) return;
                종목코드 = dg뉴스잔고.Rows[row].Cells[J종목코드.Index].Value.ToString();
                현재가 = dg뉴스잔고.Rows[row].Cells[J현재가.Index].Value.ToString();
                주문가 = 현재가;
                rOrder.Auto = true;
            }
            else if (cmsMenu.SourceControl.Name == dg관종.Name)
            {
                if (dg관종.CurrentRow == null) return;
                row = dg관종.CurrentRow.Index;
                col = dg관종.CurrentCell.ColumnIndex;

                if (col < 8) return;
                if (dg관종.Rows[row].Cells[F_종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg관종.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg관종.Rows[row].Cells[F_종목코드.Index].Value.ToString();
                현재가 = dg관종.Rows[row].Cells[F_현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg관종.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;
            }
            else if (cmsMenu.SourceControl.Name == dg조건종목1.Name)
            {
                if (dg조건종목1.CurrentRow == null) return;
                row = dg조건종목1.CurrentRow.Index;
                col = dg조건종목1.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목1.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목1.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목1.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목1.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목1.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }

            else if (cmsMenu.SourceControl.Name == dg조건종목2.Name)
            {
                if (dg조건종목2.CurrentRow == null) return;
                row = dg조건종목2.CurrentRow.Index;
                col = dg조건종목2.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목2.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목2.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목2.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목2.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목2.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }


            else if (cmsMenu.SourceControl.Name == dg조건종목3.Name)
            {
                if (dg조건종목3.CurrentRow == null) return;
                row = dg조건종목3.CurrentRow.Index;
                col = dg조건종목3.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목3.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목3.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목3.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목3.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목3.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }


            else if (cmsMenu.SourceControl.Name == dg조건종목4.Name)
            {
                if (dg조건종목4.CurrentRow == null) return;
                row = dg조건종목4.CurrentRow.Index;
                col = dg조건종목4.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목4.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목4.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목4.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목4.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목4.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }

            else if (cmsMenu.SourceControl.Name == dg조건종목5.Name)
            {
                if (dg조건종목5.CurrentRow == null) return;
                row = dg조건종목5.CurrentRow.Index;
                col = dg조건종목5.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목5.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목5.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목5.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목5.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목5.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }
            if (주문가 == "") return;
            수량 = Cls.ValInt((Cls.Val(nm관종매수금액.Value.ToString()) / Cls.Val(주문가)).ToString("#,##0"));
            rOrder.Price = 주문가;
            rOrder.CPrice = 현재가;
            rOrder.Qty = 수량.ToString();
            rOrder.Gubun = "1";
            ChangeHogaTab(종목코드, rOrder);
        }

        private void 매도ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string 종목코드 = "";
            int row = -1;
            int col = -1;
            int 수량 = 0;
            string 현재가 = "";
            string 주문가 = "";
            PaikRichStock.UcForm.ReciveOrder rOrder = new PaikRichStock.UcForm.ReciveOrder();
            if (cmsMenu.SourceControl.Name == dg뉴스잔고.Name)
            {
                if (dg뉴스잔고.CurrentRow == null) return;
                row = dg뉴스잔고.CurrentRow.Index;
                if (dg뉴스잔고.Rows[row].Cells[J종목코드.Index].Value == null) return;
                종목코드 = dg뉴스잔고.Rows[row].Cells[J종목코드.Index].Value.ToString();
                수량 = (int)Cls.Val(dg뉴스잔고.Rows[row].Cells[J매매가능수량.Index].Value.ToString());
                현재가 = dg뉴스잔고.Rows[row].Cells[J현재가.Index].Value.ToString();
                주문가 = 현재가;
                rOrder.Auto = true;
            }
            else if (cmsMenu.SourceControl.Name == dg관종.Name)
            {
                if (dg관종.CurrentRow == null) return;

                row = dg관종.CurrentRow.Index;
                col = dg관종.CurrentCell.ColumnIndex;

                if (col < 8) return;
                if (dg관종.Rows[row].Cells[F_종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg관종.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg관종.Rows[row].Cells[F_종목코드.Index].Value.ToString();
                현재가 = dg관종.Rows[row].Cells[F_현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg관종.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;
            }
            else if (cmsMenu.SourceControl.Name == dg조건종목1.Name)
            {
                if (dg조건종목1.CurrentRow == null) return;
                row = dg조건종목1.CurrentRow.Index;
                col = dg조건종목1.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목1.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목1.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목1.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목1.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목1.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }

            else if (cmsMenu.SourceControl.Name == dg조건종목2.Name)
            {
                if (dg조건종목2.CurrentRow == null) return;
                row = dg조건종목2.CurrentRow.Index;
                col = dg조건종목2.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목2.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목2.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목2.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목2.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목2.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }

            else if (cmsMenu.SourceControl.Name == dg조건종목3.Name)
            {
                if (dg조건종목3.CurrentRow == null) return;
                row = dg조건종목3.CurrentRow.Index;
                col = dg조건종목3.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목3.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목3.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목3.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목3.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목3.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }
            else if (cmsMenu.SourceControl.Name == dg조건종목4.Name)
            {
                if (dg조건종목4.CurrentRow == null) return;
                row = dg조건종목4.CurrentRow.Index;
                col = dg조건종목4.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목4.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목4.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목4.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목4.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목4.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }
            else if (cmsMenu.SourceControl.Name == dg조건종목5.Name)
            {
                if (dg조건종목5.CurrentRow == null) return;
                row = dg조건종목5.CurrentRow.Index;
                col = dg조건종목5.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목5.Rows[row].Cells[C종목코드.Index].Value == null) return;
                if (Cls.IsNumeric(dg조건종목5.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목5.Rows[row].Cells[C종목코드.Index].Value.ToString();
                현재가 = dg조건종목5.Rows[row].Cells[C현재가.Index].Value.ToString();
                주문가 = MakeOrderPrice((int)Cls.Val(dg조건종목5.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
            }
            if (주문가 == "") return;
            rOrder.Price = 주문가;
            rOrder.CPrice = 현재가;
            rOrder.Qty = 수량.ToString();
            rOrder.Gubun = "2";

            ChangeHogaTab(종목코드, rOrder);
        }


        private void ChangeHogaTab(string 종목코드, PaikRichStock.UcForm.ReciveOrder rOrder)
        {
            ucHogaWindowNew1.StockCode = 종목코드;
            ucHogaWindowNew1.ROrder = rOrder;
            ucHogaWindowNew1.SetManualOrder();
            tbStockList.SelectedIndex = 3;
        }


        private void envRate_ValueChanged(object sender, EventArgs e)
        {
            for (int row = 0; row < dg관종.Rows.Count - 1; row++)
            {
                dg관종.Rows[row].Cells["F_ENV상한"].Value =
                            (
                                Cls.Val(dg관종.Rows[row].Cells["F_20일선"].Value.ToString()) * (1 + (Cls.Val(envRate.Value.ToString()) / 100))
                            ).ToString("#,##0.00");
                dg관종.Rows[row].Cells["F_ENV하한"].Value =
                            (
                                Cls.Val(dg관종.Rows[row].Cells["F_20일선"].Value.ToString()) * (1 - (Cls.Val(envRate.Value.ToString()) / 100))
                            ).ToString("#,##0.00");
            }
        }

        private void 기업정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (_isReadyFor == false) return;

            int row;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString();
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목1.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목2.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목3.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목4.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목5.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();

            if (stockCode == "") return;
            ucFinance1.StockCode = stockCode;
            ucMainStockVer2.Opt10001_OnReceiveTrData(stockCode, "");
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2.Checked == true) GetFavList(rb2.Text.Trim());
        }

        private void chkPerRate_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목1.Columns[C1차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목1.Columns[C2차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목1.Columns[C3차매수가.Index].Visible = chkPerRate.Checked;

            dg조건종목2.Columns[C1차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목2.Columns[C2차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목2.Columns[C3차매수가.Index].Visible = chkPerRate.Checked;

            dg조건종목3.Columns[C1차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목3.Columns[C2차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목3.Columns[C3차매수가.Index].Visible = chkPerRate.Checked;

            dg조건종목4.Columns[C1차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목4.Columns[C2차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목4.Columns[C3차매수가.Index].Visible = chkPerRate.Checked;

            dg조건종목5.Columns[C1차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목5.Columns[C2차매수가.Index].Visible = chkPerRate.Checked;
            dg조건종목5.Columns[C3차매수가.Index].Visible = chkPerRate.Checked;
        }

        private void chkAvgLine_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목1.Columns[C3일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목1.Columns[C5일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목1.Columns[C10일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목1.Columns[C15일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목1.Columns[C20일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목1.Columns[C40일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목1.Columns[C60일선.Index].Visible = chkAvgLine.Checked;


            dg조건종목2.Columns[C3일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목2.Columns[C5일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목2.Columns[C10일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목2.Columns[C15일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목2.Columns[C20일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목2.Columns[C40일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목2.Columns[C60일선.Index].Visible = chkAvgLine.Checked;


            dg조건종목3.Columns[C3일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목3.Columns[C5일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목3.Columns[C10일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목3.Columns[C15일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목3.Columns[C20일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목3.Columns[C40일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목3.Columns[C60일선.Index].Visible = chkAvgLine.Checked;


            dg조건종목4.Columns[C3일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목4.Columns[C5일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목4.Columns[C10일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목4.Columns[C15일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목4.Columns[C20일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목4.Columns[C40일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목4.Columns[C60일선.Index].Visible = chkAvgLine.Checked;

            dg조건종목5.Columns[C3일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목5.Columns[C5일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목5.Columns[C10일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목5.Columns[C15일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목5.Columns[C20일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목5.Columns[C40일선.Index].Visible = chkAvgLine.Checked;
            dg조건종목5.Columns[C60일선.Index].Visible = chkAvgLine.Checked;
        }

        private void chkEnvelope_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목1.Columns[CENV상한.Index].Visible = chkEnvelope.Checked;
            dg조건종목1.Columns[CENV하한.Index].Visible = chkEnvelope.Checked;

            dg조건종목2.Columns[CENV상한.Index].Visible = chkEnvelope.Checked;
            dg조건종목2.Columns[CENV하한.Index].Visible = chkEnvelope.Checked;

            dg조건종목3.Columns[CENV상한.Index].Visible = chkEnvelope.Checked;
            dg조건종목3.Columns[CENV하한.Index].Visible = chkEnvelope.Checked;

            dg조건종목4.Columns[CENV상한.Index].Visible = chkEnvelope.Checked;
            dg조건종목4.Columns[CENV하한.Index].Visible = chkEnvelope.Checked;

            dg조건종목5.Columns[CENV상한.Index].Visible = chkEnvelope.Checked;
            dg조건종목5.Columns[CENV하한.Index].Visible = chkEnvelope.Checked;
        }

        private void chkBolBand_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목1.Columns[CB상한.Index].Visible = chkBolBand.Checked;
            dg조건종목1.Columns[CB하한.Index].Visible = chkBolBand.Checked;

            dg조건종목2.Columns[CB상한.Index].Visible = chkBolBand.Checked;
            dg조건종목2.Columns[CB하한.Index].Visible = chkBolBand.Checked;

            dg조건종목3.Columns[CB상한.Index].Visible = chkBolBand.Checked;
            dg조건종목3.Columns[CB하한.Index].Visible = chkBolBand.Checked;

            dg조건종목4.Columns[CB상한.Index].Visible = chkBolBand.Checked;
            dg조건종목4.Columns[CB하한.Index].Visible = chkBolBand.Checked;

            dg조건종목5.Columns[CB상한.Index].Visible = chkBolBand.Checked;
            dg조건종목5.Columns[CB하한.Index].Visible = chkBolBand.Checked;
        }

        private void chkLine20세력_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목1.Columns[C세력선_H.Index].Visible = chkLine20세력.Checked;
            dg조건종목1.Columns[C세력선_C.Index].Visible = chkLine20세력.Checked;
            dg조건종목1.Columns[C세력선_L.Index].Visible = chkLine20세력.Checked;

            dg조건종목2.Columns[C세력선_H.Index].Visible = chkLine20세력.Checked;
            dg조건종목2.Columns[C세력선_C.Index].Visible = chkLine20세력.Checked;
            dg조건종목2.Columns[C세력선_L.Index].Visible = chkLine20세력.Checked;


            dg조건종목3.Columns[C세력선_H.Index].Visible = chkLine20세력.Checked;
            dg조건종목3.Columns[C세력선_C.Index].Visible = chkLine20세력.Checked;
            dg조건종목3.Columns[C세력선_L.Index].Visible = chkLine20세력.Checked;


            dg조건종목4.Columns[C세력선_H.Index].Visible = chkLine20세력.Checked;
            dg조건종목4.Columns[C세력선_C.Index].Visible = chkLine20세력.Checked;
            dg조건종목4.Columns[C세력선_L.Index].Visible = chkLine20세력.Checked;

            dg조건종목5.Columns[C세력선_H.Index].Visible = chkLine20세력.Checked;
            dg조건종목5.Columns[C세력선_C.Index].Visible = chkLine20세력.Checked;
            dg조건종목5.Columns[C세력선_L.Index].Visible = chkLine20세력.Checked;
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3.Checked == true) GetFavList(rb3.Text.Trim());
        }

        private void chkFAvgLine_CheckedChanged(object sender, EventArgs e)
        {
            dg관종.Columns[F_3일선.Index].Visible = chkFAvgLine.Checked;
            dg관종.Columns[F_5일선.Index].Visible = chkFAvgLine.Checked;
            dg관종.Columns[F_10일선.Index].Visible = chkFAvgLine.Checked;
            dg관종.Columns[F_15일선.Index].Visible = chkFAvgLine.Checked;
            dg관종.Columns[F_20일선.Index].Visible = chkFAvgLine.Checked;
            dg관종.Columns[F_40일선.Index].Visible = chkFAvgLine.Checked;
            dg관종.Columns[F_60일선.Index].Visible = chkFAvgLine.Checked;
            dg관종.Columns[F_120일선.Index].Visible = chkFAvgLine.Checked;
            dg관종.Columns[F_220일선.Index].Visible = chkFAvgLine.Checked;
            VisibleSHLPrice(dg관종);
        }

        private void chkF20LineMa_CheckedChanged(object sender, EventArgs e)
        {
            dg관종.Columns[F_세력선_H.Index].Visible = chkF20LineMa.Checked;
            dg관종.Columns[F_세력선_C.Index].Visible = chkF20LineMa.Checked;
            dg관종.Columns[F_세력선_L.Index].Visible = chkF20LineMa.Checked;
            VisibleSHLPrice(dg관종);
        }

        private void chkFENV_CheckedChanged(object sender, EventArgs e)
        {
            dg관종.Columns[F_ENV상한.Index].Visible = chkFENV.Checked;
            dg관종.Columns[F_ENV하한.Index].Visible = chkFENV.Checked;
            VisibleSHLPrice(dg관종);
        }

        private void chkBol_CheckedChanged(object sender, EventArgs e)
        {
            dg관종.Columns[F_B상한.Index].Visible = chkBol.Checked;
            dg관종.Columns[F_B하한.Index].Visible = chkBol.Checked;
            dg관종.Columns[F_BB이격.Index].Visible = chkBol.Checked;
            VisibleSHLPrice(dg관종);
        }

        private void VisibleSHLPrice(DataGridView dg)
        {
            if (chkFAvgLine.Checked || chkF20LineMa.Checked || chkFENV.Checked || chkBol.Checked)
            {
                dg관종.Columns[F_시가.Index].Visible = false;
                dg관종.Columns[F_고가.Index].Visible= false;
                dg관종.Columns[F_저가.Index].Visible = false;
            }
            else
            {
                dg관종.Columns[F_시가.Index].Visible = true;
                dg관종.Columns[F_고가.Index].Visible = true;
                dg관종.Columns[F_저가.Index].Visible = true;
            }
        }

        private void cboTickDataMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgTick.DataMember = Cls.Left(cboTickDataMember.Text.Trim(), 6);
            lblTickTotal.Text = "일자:" + DateTime.Now.ToString("yyyyMMdd");
            lblTickTotal.Text += ",매수:" + _dsTick60.Tables[Cls.Left(cboTickDataMember.Text.Trim(), 6)].Compute("SUM(매수거래량)", String.Format("일자 = '{0}'", DateTime.Now.ToString("yyyyMMdd"))).ToString();
            lblTickTotal.Text += ",매도:" + _dsTick60.Tables[Cls.Left(cboTickDataMember.Text.Trim(), 6)].Compute("SUM(매도거래량)", String.Format("일자 = '{0}'", DateTime.Now.ToString("yyyyMMdd"))).ToString();
        }


        private void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAccount.Text == "3228538611" || cboAccount.Text == "8087774611" || cboAccount.Text == "5116998410")
            {
                _stockId = "000003";
            }
            else if (cboAccount.Text == "5095390910" || cboAccount.Text == "8086172111")
            {
                _stockId = "000005";
            }
            else if (cboAccount.Text == "8087645611")
            {
                _stockId = "000002";
            }
        }

        private void dgDartNew_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                System.Diagnostics.Process.Start(dgDartNew.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());
            }
        }

        private void dg미체결_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dg미체결.Rows[e.RowIndex].Cells[M종목코드.Index].Value == null || dg미체결.Rows[e.RowIndex].Cells[M종목코드.Index].Value.ToString() == "")
            {
                dg미체결.Rows.RemoveAt(e.RowIndex);
                return;
            }

            string 종목코드 = dg미체결.Rows[e.RowIndex].Cells[M종목코드.Index].Value.ToString().Trim();
            int 주문수량 = Cls.ValInt(dg미체결.Rows[e.RowIndex].Cells[M미체결수량.Index].Value.ToString().Trim());
            string 주문번호 = dg미체결.Rows[e.RowIndex].Cells[M주문번호.Index].Value.ToString().Trim();
            int 구분 = -1;

            if (dg미체결.Rows[e.RowIndex].Cells[M주문구분.Index].Value.ToString().IndexOf("매수") > -1) 구분 = (int)ucMainStock.OrderType.매수취소;
            else if (dg미체결.Rows[e.RowIndex].Cells[M주문구분.Index].Value.ToString().IndexOf("매도") > -1) 구분 = (int)ucMainStock.OrderType.매도취소;

            if (e.ColumnIndex == M취소.Index) //취소버튼 클릭시
            {
                SendBuySellMsg(종목코드, "00", 구분, 0, 0, 주문수량, "2", 주문번호);
            }
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {  // 로그
                if (ucMainStockVer2._OrderResult.Tables.Count > 0)
                {
                    dgOrderList.DataSource = ucMainStockVer2._OrderResult.Tables[0];
                    dgOrderList.AutoGenerateColumns = true;
                }
            }
        }

        private void btnTickSave_Click(object sender, EventArgs e)
        {
            TickSaveNew(_dsTick60.Tables[cboTickDataMember.Text.Substring(0, 6)]);
        }

        //private void TickSave(DataTable dt)
        //{
        //    DataSet ds;
        //    DataSet dsCondition;
        //    ds = _DataAcc.p_Psi02Query("3", _stockId, 0, "", "", "", null, null);
        //    try
        //    {
        //        dsCondition = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dg조건종목);
        //        if (ds.Tables[0].Select(String.Format("STOCK_CODE='{0}'", dt.TableName)).Length < 1
        //            && _dt보유잔고.Select(String.Format("종목번호 LIKE '%{0}%'", dt.TableName)).Length < 1
        //            && dsCondition.Tables[0].Select(String.Format("STOCK_CODE='{0}'", dt.TableName)).Length < 1

        //        ) return; //관심종목이 아니면 Tick 을 저장하지 않는다. 용량밑 실시간 데이터 요청시 200개 한도가 있음

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            if (DateTime.Now.ToString("yyyyMMdd") != dr["일자"].ToString().Trim()) { continue; }
        //            _DataAcc.p_stock_tick_Add("A", _stockId, dt.TableName, DateTime.Now.ToString("yyyyMMdd"),
        //            dr["시작시간"].ToString(),
        //            dr["종료시간"].ToString(),
        //            dr["현재가"].ToString(),
        //            dr["시가"].ToString(),
        //            dr["고가"].ToString(),
        //            dr["저가"].ToString(),
        //            dr["등락율"].ToString(),
        //            dr["체결강도"].ToString(),
        //            dr["매수거래량"].ToString(),
        //            dr["매도거래량"].ToString(),
        //            dr["매수거래비용"].ToString(),
        //            dr["매도거래비용"].ToString(),
        //            dr["LINE5"].ToString(),
        //            dr["LINE10"].ToString(),
        //            dr["LINE20"].ToString(),
        //            dr["LINE40"].ToString(),
        //            dr["LINE60"].ToString(),
        //            true, null, null
        //            );
        //        }
        //    }
        //    finally
        //    {
        //        _DataAcc.DisConnect();
        //    }
        //}

        private void TickSaveNew(DataTable dt)
        {
            DataSet ds;
            //DataSet dsCondition;
            ArrayParam arr = new ArrayParam();
            ArrayParams arrs = new ArrayParams();
            string filter = "";
            ds = _DataAcc.p_Psi02Query("3", _stockId, 0, "", "", "", null, null);
            try
            {
                //dsCondition = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dg조건종목1);
                if (ds.Tables[0].Select(String.Format("STOCK_CODE='{0}'", dt.TableName)).Length < 1
                    //&& _dt보유잔고.Select(String.Format("종목번호 LIKE '%{0}%'", dt.TableName)).Length < 1
                    //&& dsCondition.Tables[0].Select(String.Format("STOCK_CODE='{0}'", dt.TableName)).Length < 1

                ) return; //관심종목이 아니면 Tick 을 저장하지 않는다. 용량밑 실시간 데이터 요청시 200개 한도가 있음

                arrs.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    filter = String.Format("STOCK_ID = '{0}' AND STOCK_CODE = '{1}' AND S_TIME = '{2}'", _stockId, dt.TableName, dr["시작시간"].ToString());
                    if (_dsTickInit.Tables[0].Select(filter).Length > 0) continue;
                    //if (DateTime.Now.ToString("yyyyMMdd") != dr["일자"].ToString().Trim()) { continue; }
                    arr.Clear();
                    arr.Add("STOCK_ID", _stockId);
                    arr.Add("STOCK_CODE", dt.TableName);
                    arr.Add("STOCK_DATE", DateTime.Now.ToString("yyyyMMdd"));
                    arr.Add("S_TIME", dr["시작시간"].ToString());
                    arr.Add("E_TIME", dr["종료시간"].ToString());
                    arr.Add("C_PRICE", dr["현재가"].ToString());
                    arr.Add("S_PRICE", dr["시가"].ToString());
                    arr.Add("H_PRICE", dr["고가"].ToString());
                    arr.Add("L_PRICE", dr["저가"].ToString());
                    arr.Add("RATE", dr["등락율"].ToString());
                    arr.Add("POWER_RATE", dr["체결강도"].ToString());
                    arr.Add("BUY_VOLUME", dr["매수거래량"].ToString());
                    arr.Add("SELL_VOLUME", dr["매도거래량"].ToString());
                    arr.Add("BUY_TRADING_P", dr["매수거래비용"].ToString());
                    arr.Add("SELL_TRADING_P", dr["매도거래비용"].ToString());
                    arr.Add("LINE5", dr["LINE5"].ToString());
                    arr.Add("LINE10", dr["LINE10"].ToString());
                    arr.Add("LINE20", dr["LINE20"].ToString());
                    arr.Add("LINE40", dr["LINE40"].ToString());
                    arr.Add("LINE60", dr["LINE60"].ToString());
                    arr.Add("BBUP", dr["BBUP"].ToString());
                    arr.Add("BBDOWN", dr["BBDOWN"].ToString());
                    arr.Add("IS_BUY", dr["매수유무"].ToString());
                    arr.Add("IS_SELL", dr["매도유무"].ToString());
                    arr.Add("IS_BUYSIGNAL", dr["매수신호"].ToString());
                    arr.Add("IS_SELLSIGNAL", dr["매도신호"].ToString());
                    arrs.Add(arr);
                }
                if (arrs.Count > 0)
                {
                    mySqlDbConn conn = new mySqlDbConn();
                    conn.ExecuteNonMultiInsert("stock_tick", arrs);
                }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
        }

        private void dg뉴스체결_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            DataGridView dg = (DataGridView)sender;
            DataGridViewRow dgr = dg.Rows[e.RowIndex];

            Cls.ChangeColor(dgr, "A", 0 , N수익률.Index , N등락률.Index);
            if (Cls.Val(dgr.Cells[N등락률.Index].Value) < 0)
            {
                dgr.Cells[N현재가.Index].Style.ForeColor = System.Drawing.Color.Blue;
            }
            else if (Cls.Val(dgr.Cells[N등락률.Index].Value) > 0)
            {
                dgr.Cells[N현재가.Index].Style.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                dgr.Cells[N현재가.Index].Style.ForeColor = System.Drawing.Color.Empty;
            }

            if (Cls.Val(dgr.Cells[N현재가.Index].Value) > Cls.Val(dgr.Cells[N모니터링가격.Index].Value))
            {
                dgr.Cells[N모니터링가격.Index].Style.BackColor = System.Drawing.Color.LightBlue;
            }
        }

        private void txt관종_Enter(object sender, EventArgs e)
        {
            txt관종.SelectAll();
        }

        private void txt관종_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ucMainStockVer2._allStockDataset == null) return;
                DataView dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
                dv.RowFilter = "STOCK_NAME = '" + txt관종.Text.Trim() + "'";

                if (dv.Count < 1) return;
                string stockCode = dv[0]["STOCK_CODE"].ToString().Trim();
                if (stockCode.Equals("")) return;
                NewsFavAdd(stockCode , "Y" , "" , "수" , 0 , 0 , 0 , 0);
                txt관종.Text = "";
            }
        }

        private void NewsFavAdd(string stockCode , string isForce , string monPrice , string monGb , int 시가 , int 고가 , int 저가 , int 현재가 , bool chkCnt = true)
        {
            
            string stockName = ucMainStockVer2.GetStockInfo(stockCode);
            if (!monGb.Equals("공") && !monGb.Equals("수")) {
                if (chkCnt && dgN관종.Rows.Count >= 40) return;
                //공시외 3시 이후 체크 - S
                if (Cls.ValInt(DateTime.Now.ToString("HHmmss")) > 150000 || Cls.ValInt(DateTime.Now.ToString("HHmmss")) < 090000)
                {
                    return;
                }

                //전일 거래량이 50000 이하이거나 Tick 정보가 없으면 매수 하지 않는다 - S
                if (_ds전체재정.Tables[0].Select(String.Format("STOCK_CODE = '{0}' AND VOLUME > 50000", stockCode)).Length < 1)
                {
                    return;
                }
                //전일 거래량이 50000 이하이거나 Tick 정보가 없으면 매수 하지 않는다 - E
                //공시외 3시 이후 체크 - E
            }

            if (_ds전체일봉.Tables.Count == 0) return;
            if (_ds전체재정.Tables.Count == 0) return;
            if (!현재가.Equals(0))
            {
                //캔들 분석 - S
                double 매수신호가폭 = (고가 - 저가) / 저가 * 100;
                double 매수신호기준비율 = (1.00 - (매수신호가폭 * 0.1));
                if (매수신호가폭 > 5.0) { 매수신호기준비율 = 0.50; }

                if (Cls.Val(저가) + ((Cls.Val(고가) - Cls.Val(저가)) * 매수신호기준비율) <= Cls.Val(현재가))
                {
                    return;
                }
                //캔들 분석 - E
            }

            var obj = (from DataGridViewRow dgr in dgN관종.Rows
                       where dgr.Cells[P종목코드.Index].Value != null && dgr.Cells[P종목코드.Index].Value.Equals(stockCode)
                       select dgr).FirstOrDefault();

            var objOrder = (from DataGridViewRow dgr in dg뉴스체결.Rows
                       where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(stockCode)
                       select dgr).FirstOrDefault();

            if (obj == null && objOrder == null)
            {
                //DataTable tickDt;
                if (chk자동매매.Checked)
                {
                    dgN관종.Rows.Insert(0, 1);
                    DataGridViewRow dgr = dgN관종.Rows[0];
                    dgr.Cells[P종목코드.Index].Value = stockCode;
                    dgr.Cells[P종목명.Index].Value = stockName;
                    dgr.Cells[P최초거래시간.Index].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    dgr.Cells[P강제추가여부.Index].Value = isForce;
                    dgr.Cells[P모니터링가격.Index].Value = monPrice;
                    dgr.Cells[P모니터링구분.Index].Value = monGb;

                    SetDsScreenNo("A", "1", "1", stockCode, stockName, false);
                }
                //뉴스 관종에 등록 - E
            }
            lblNCnt.Text = dgN관종.Rows.Count.ToString();
        }

        private void dgN관종_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgN관종.RowCount == 0) return;

            DataGridViewRow dgr = dgN관종.CurrentRow;
            if (dgr == null) return;

            string stockCode = dgr.Cells[P종목코드.Index].Value.ToString();
            string stockName = dgr.Cells[P종목명.Index].Value.ToString();
            if (e.Control == true && e.KeyCode == Keys.D)
            {
                
                if (dgr.Cells[P모니터링구분.Index].Value.Equals("공"))
                {
                    SetDsScreenNo("D", "1", "1", stockCode, stockName, false);
                }
                dgN관종.Rows.Remove(dgr);
            }
        }

        private void dg뉴스체결_KeyDown(object sender, KeyEventArgs e)
        {
            if (dg뉴스체결.CurrentRow.Index == -1) return;
            if (dg뉴스체결.Rows[dg뉴스체결.CurrentRow.Index].Cells[N종목코드.Index].Value == null) return;
            string stockCode = dg뉴스체결.Rows[dg뉴스체결.CurrentRow.Index].Cells[N종목코드.Index].Value.ToString();
            if (e.Control == true && e.KeyCode == Keys.D)
            {
                CancelAll(stockCode);
                dg뉴스체결.Rows.RemoveAt(dg뉴스체결.CurrentRow.Index);
            }
        }


        private void CancelAll(string stockCode)
        {
            var obj = (from DataGridViewRow dgr in dg미체결.Rows
                       where dgr.Cells[M종목코드.Index].Value != null && dgr.Cells[M종목코드.Index].Value.Equals(stockCode)
                       select dgr);

            if (obj != null)
            {
                foreach (DataGridViewRow dgr in obj)
                {

                    SendBuySellMsg(dgr.Cells[M종목코드.Index].Value.ToString(), "00", (int)ucMainStock.OrderType.매수취소, 0, 0, Cls.ValInt(dgr.Cells[M미체결수량.Index].Value), "2", dgr.Cells[M주문번호.Index].Value.ToString());
                }
            }
        }

        private void dg뉴스잔고_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (dg뉴스잔고.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
            DataGridViewRow dgr = dg뉴스잔고.Rows[e.RowIndex];
            if (dgr.Cells[J종목코드.Index].Value == null) return;

            string stockCode = dgr.Cells[J종목코드.Index].Value.ToString();
            if (e.ColumnIndex == J대비.Index)
            {
                if (Cls.ValInt(dgr.Cells[J대비.Index].Value.ToString()) > 0)
                {
                    dgr.Cells[J현재가.Index].Style.ForeColor = System.Drawing.Color.Red;
                    dgr.Cells[J대비.Index].Style.ForeColor = System.Drawing.Color.Red;
                    dgr.Cells[J등락율.Index].Style.ForeColor = System.Drawing.Color.Red;
                }
                else if (Cls.ValInt(dgr.Cells[J대비.Index ].Value.ToString()) < 0)
                {
                    dgr.Cells[J현재가.Index].Style.ForeColor = System.Drawing.Color.Blue;
                    dgr.Cells[J대비.Index].Style.ForeColor = System.Drawing.Color.Blue;
                    dgr.Cells[J등락율.Index].Style.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    dgr.Cells[J현재가.Index].Style.ForeColor = System.Drawing.Color.Empty;
                    dgr.Cells[J대비.Index].Style.ForeColor = System.Drawing.Color.Empty;
                    dgr.Cells[J등락율.Index].Style.ForeColor = System.Drawing.Color.Empty;
                }

                if (Cls.Val(dgr.Cells[J수익률.Index].Value.ToString()) > 0.00)
                {
                    dgr.Cells[J수익률.Index].Style.ForeColor = System.Drawing.Color.Red;
                }
                else if (Cls.Val(dg뉴스잔고.Rows[e.RowIndex].Cells[J수익률.Index].Value.ToString()) < 0.00)
                {
                    dgr.Cells[J수익률.Index].Style.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    dgr.Cells[J수익률.Index].Style.ForeColor = System.Drawing.Color.Empty;
                }
            }
            else if (e.ColumnIndex == J현재가.Index)
            {
                if (dgr.Cells[J보유수량.Index].Value == null) return;
                dgr.Cells[J평가금액.Index].Value = (Cls.Val(dgr.Cells[J보유수량.Index].Value.ToString()) * Cls.Val(dgr.Cells[J현재가.Index].Value.ToString())).ToString("#,##0");
            }
            else if (e.ColumnIndex == J보유수량.Index)
            {
                dgr.Cells[J매입금액.Index].Value = (Cls.Val(dgr.Cells[J보유수량.Index].Value.ToString()) * Cls.Val(dgr.Cells[J매입가.Index].Value.ToString())).ToString("#,##0");

                if (Cls.Val(dgr.Cells[J보유수량.Index].Value.ToString()) <= 0) {
                    _DataAcc.p_stock_profit_Add("D", cboAccount.Text, dgr.Cells[J종목코드.Index].Value.ToString(), "", "", "", "", "", "");
                    UpdateDsProfit("D", dgr.Cells[J종목코드.Index].Value.ToString(), null, "-1", "-1", "1", "-1", "-1");

                    SetDsScreenNo("D", "2", "1", dgr.Cells[J종목코드.Index].Value.ToString(), dgr.Cells[J종목명.Index].Value.ToString(), false);
                    dg뉴스잔고.Rows.Remove(dg뉴스잔고.Rows[e.RowIndex]);
                }
            }
            else if (e.ColumnIndex == J이익실현1.Index)
            {
                _DataAcc.p_stock_profit_Add("U", cboAccount.Text, stockCode, dgr.Cells[J이익실현1.Index].Value.ToString(), "-1", "-1", "-1", "-1", "");
                UpdateDsProfit("U" , stockCode, null, dgr.Cells[J이익실현1.Index].Value.ToString(), "-1", "-1", "-1", "-1");
            }
            else if (e.ColumnIndex == J이익실현2.Index)
            {
                _DataAcc.p_stock_profit_Add("U", cboAccount.Text, stockCode, "-1", dgr.Cells[J이익실현2.Index].Value.ToString(), "-1", "-1", "-1", "");
                UpdateDsProfit("U" , stockCode, null, "-1", dgr.Cells[J이익실현2.Index].Value.ToString(), "-1", "-1", "-1");
            }
            else if (e.ColumnIndex == J이익실현3.Index)
            {
                _DataAcc.p_stock_profit_Add("U", cboAccount.Text, stockCode, "-1", "-1", dgr.Cells[J이익실현3.Index].Value.ToString(), "-1", "-1", "");
                UpdateDsProfit("U" , stockCode, null, "-1", "-1", dgr.Cells[J이익실현3.Index].Value.ToString(), "-1", "-1");
            }
            else if (e.ColumnIndex == J구분.Index)
            {
                _DataAcc.p_stock_profit_Add("U", cboAccount.Text, stockCode, "-1", "-1", "-1", "-1", "-1", dgr.Cells[J구분.Index].Value.ToString());
                UpdateDsProfit("U" , stockCode, dgr.Cells[J구분.Index].Value.ToString(), "-1", "-1", "-1", "-1", "-1");
            }
        }

        private void UpdateDsProfit(string act , string stockCode , string gb , params string[] profits) {
            DataRow[] drs;
            DataRow drObj;
            drs = _ds익절.Tables[0].Select(String.Format("ACCOUNT_NO = '{0}' AND STOCK_CODE = '{1}'", cboAccount.Text, stockCode));
            if (!act.Equals("D") && drs.Length == 0)
            {
                drObj = _ds익절.Tables[0].NewRow();
                drObj["ACCOUNT_NO"] = cboAccount.Text;
                drObj["STOCK_CODE"] = stockCode;
                drObj["SELL1_PRICE"] = 0;
                drObj["SELL2_PRICE"] = 0;
                drObj["SELL3_PRICE"] = 0;
                drObj["SELL4_PRICE"] = 0;
                drObj["SELL5_PRICE"] = 0;
                if (gb != null) drObj["GB"] = gb;
                _ds익절.Tables[0].Rows.Add(drObj);
            }
            else
            {
                foreach (DataRow dr in drs)
                {
                    if (act.Equals("D")) _ds익절.Tables[0].Rows.Remove(dr);
                    else { 
                        if (Cls.Val(profits[0]) > -1) dr["SELL1_PRICE"] = profits[0];
                        if (Cls.Val(profits[1]) > -1) dr["SELL2_PRICE"] = profits[1];
                        if (Cls.Val(profits[2]) > -1) dr["SELL3_PRICE"] = profits[2];
                        if (Cls.Val(profits[3]) > -1) dr["SELL4_PRICE"] = profits[3];
                        if (Cls.Val(profits[4]) > -1) dr["SELL5_PRICE"] = profits[4];
                        if (gb != null) dr["GB"] = gb;
                    }
                }
            }
        }


        object _tmpObj;
        private void cmsMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cmsMenu.SourceControl == null) return;
            if (cmsMenu.SourceControl.Name != "") _tmpObj = cmsMenu.SourceControl;

            //if (
            //    cmsMenu.SourceControl.Name == dgN관종.Name ||
            //    cmsMenu.SourceControl.Name == dg뉴스체결.Name
            //    )
            //{
            //    시장가매수ToolStripMenuItem.Enabled = false;
            //    시장가매도ToolStripMenuItem.Enabled = false;
            //}
            //else
            //{
            //    시장가매수ToolStripMenuItem.Enabled = true;
            //    시장가매도ToolStripMenuItem.Enabled = true;
            //}

            if (cmsMenu.SourceControl.Name == dg관종.Name)
            {
                관종추가ToolStripMenuItem.Visible = false;
                관종이동ToolStripMenuItem.Visible = true;
            }
            else
            {
                관종추가ToolStripMenuItem.Visible = true;
                관종이동ToolStripMenuItem.Visible = false;
            }
        }

        private void btnAllTickSave_Click(object sender, EventArgs e)
        {
            tbStockList.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = _dsTick60.Tables.Count;
            progressBar1.Value = 0;
            try { 
                foreach (DataTable dt in _dsTick60.Tables)
                {
                    TickSaveNew(dt);
                    progressBar1.Value += 1;
                    Application.DoEvents();
                }
            }
            finally { 
                progressBar1.Visible = false;
                tbStockList.Enabled = true;
            }
        }

        private void tICKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = -1;
            int ix = 0;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목1.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목2.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목3.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목4.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목5.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg미체결.Name) stockCode = dgObj.Rows[row].Cells["종목코드"].Value.ToString().Replace("A", "");

            if (stockCode == "") return;

            foreach (string str in cboTickDataMember.Items)
            {
                if (str.Substring(0, 6) == stockCode)
                {
                    cboTickDataMember.SelectedIndex = ix;
                    tbStockList.SelectedIndex = 7;
                    break;
                }
                ix++;
            }
        }

        private void 차트ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (_isReadyFor == false) return;
            int row;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목1.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목2.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목3.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목4.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목5.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();

            if (stockCode == "") return;
            ucFinance1.StockCode = stockCode;
            ucMainStockVer2.Opt10001_OnReceiveTrData(stockCode, "");
        }

        private void rbA_CheckedChanged(object sender, EventArgs e)
        {
            if (rbA.Checked == true) GetFavList(rbA.Text.Trim());
        }

        private void chkFinance_CheckedChanged(object sender, EventArgs e)
        {
            //dg관종.Columns[F_신용비율.Index].Visible = chkFinance.Checked;
            //dg관종.Columns[F_시가총액.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_PER.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_ROE.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_PBR.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_EV.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_EPS.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_BPS.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_영업이익.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_당기순이익.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_250최고가.Index].Visible = chkFinance.Checked;
            dg관종.Columns[F_250최저가.Index].Visible = chkFinance.Checked;
        }

        private void 매동ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = -1;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목1.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목2.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목3.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목4.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목5.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg미체결.Name) stockCode = dgObj.Rows[row].Cells["종목코드"].Value.ToString().Replace("A", "");

            if (stockCode == "") return;

            PaikRichStock.UcForm.ucVolumeAnalysis.StructureGetStockInfo stockInfo = new PaikRichStock.UcForm.ucVolumeAnalysis.StructureGetStockInfo();

            stockInfo.stockCode = stockCode;
            stockInfo.stockName = ucMainStockVer2.GetStockInfo(stockCode);

            tbStockList.SelectedIndex = 1;
        }

        private void rb4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb4.Checked == true) GetFavList(rb4.Text.Trim());
        }

        private void 관종추가3번ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dg관종_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView obj = (DataGridView)sender;
            if (obj.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                obj.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;
            int row = -1;
            int interIdNew = Cls.ValInt(obj.Text);
            int interId = 0;
            if (rb1.Checked == true) interId = 1;
            if (rb2.Checked == true) interId = 2;
            if (rb3.Checked == true) interId = 3;
            if (rb4.Checked == true) interId = 4;
            if (rb5.Checked == true) interId = 5;
            if (rbA.Checked == true) interId = 1;
            
            string stockCode = "";
            DataGridView dgObj = (DataGridView)_tmpObj;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목1.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목2.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목3.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목4.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목5.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg미체결.Name) stockCode = dgObj.Rows[row].Cells["종목코드"].Value.ToString().Replace("A", "");

            if (stockCode == "") return;
            _DataAcc.p_Psi02Add("M", _stockId, interId, stockCode, "00", interIdNew.ToString(), "", null, null);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;
            int row = -1;
            int interId = Cls.ValInt(obj.Text);

            string stockCode = "";
            DataGridView dgObj = (DataGridView)_tmpObj;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목1.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목2.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목3.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목4.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목5.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg미체결.Name) stockCode = dgObj.Rows[row].Cells["종목코드"].Value.ToString().Replace("A", "");

            if (stockCode == "") return;
            _DataAcc.p_Psi02Add("A", _stockId, interId, stockCode, "00", "", "", null, null);
        }

        private void rb5_CheckedChanged(object sender, EventArgs e)
        {
            if (rb5.Checked == true) GetFavList(rb5.Text.Trim());
        }

        //private Form _frmChart;
        //private Chart.frmChart _uc;

        private void 차트ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //if (_isReadyFor == false) return;

            int row;

            string stockCode = "";
            int stockNameIdx = -1;

            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name)
            {
                stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString();
                stockNameIdx = 1;
            }
            else if (dgObj.Name == dgN관종.Name)
            {
                stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
                stockNameIdx = P종목명.Index;
            }
            else if (dgObj.Name == dg뉴스체결.Name)
            {
                stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
                stockNameIdx = N종목명.Index;
            }
            else if (dgObj.Name == dg조건종목1.Name)
            {
                stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
                stockNameIdx = C종목명.Index;
            }
            else if (dgObj.Name == dg조건종목2.Name)
            {
                stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
                stockNameIdx = C종목명.Index;
            }
            else if (dgObj.Name == dg조건종목3.Name)
            {
                stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
                stockNameIdx = C종목명.Index;
            }
            else if (dgObj.Name == dg조건종목4.Name)
            {
                stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
                stockNameIdx = C종목명.Index;
            }
            else if (dgObj.Name == dg조건종목5.Name)
            {
                stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
                stockNameIdx = C종목명.Index;
            }
            else if (dgObj.Name == dg관종.Name)
            {
                stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();
                stockNameIdx = F_종목명.Index;
            }

            if (stockCode == "") return;

            Chart.frmChart frmChart = new Chart.frmChart();
            frmChart.MainStock = ucMainStockVer2;
            frmChart.GetChartData(stockCode);
            frmChart.Text = dgObj.Rows[row].Cells[stockNameIdx].Value.ToString() + "(" + stockCode + ")";
            frmChart.Show();

        }

        void _frmChart_SizeChanged(object sender, EventArgs e)
        {
            ////_uc.Width = _frmChart.Width;
            ////_uc.Height = _frmChart.Height;

        }

        private void frmChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_uc = null;

            ////((Form)sender).Hide();
            ////e.Cancel = true;
        }


        private void cbo조건_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox obj = (ComboBox)sender;
            string no;
            string conName;
            if (obj.SelectedIndex == -1) return;
            no = obj.Items[obj.SelectedIndex].ToString().Split('^')[0];
            conName = obj.Items[obj.SelectedIndex].ToString().Split('^')[1];
            DataGridView dg = (DataGridView)tabPage3.Controls.Find("dg조건종목" + Cls.Right(obj.Name , 1) , true)[0];
            dg.RowCount = 0;
            ucMainStockVer2.SendCondition_OnReceiveConditionVer("9" + no, conName, no, 1);
            obj.Enabled = false;
        }


        private void cboTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            string themeName = "";
            rb1.Checked = false;
            rb2.Checked = false;
            rb3.Checked = false;
            rb4.Checked = false;
            rb5.Checked = false;
            rbA.Checked = false;

            dg관종.RowCount = 0;
            if (cboTheme.SelectedIndex < 1) return;

            var t1 = RealDataFavDisconnect();
            themeName = cboTheme.Items[cboTheme.SelectedIndex].ToString().Trim();
            GetThemeList(themeName);
        }

        private void GetThemeList(string themeName)
        {
            dg관종.CellValueChanged -= dg관종_CellValueChanged;
            dg관종.CellValueChanged -= dg관종_CellValueChanged;

            DataSet ds;
            DataGridViewRow dgr;
            string stockCodes = "";
            ds = _DataAcc.p_stock_theme_query("3", "", themeName, false);


            Decimal ret;
            if (ds.Tables[0].Rows.Count < 1) return;

            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (ucMainStockVer2._allStockDataset == null) return;

                    dg관종.RowCount += 1;
                    dgr = dg관종.Rows[dg관종.RowCount - 1];
                    dgr.Cells[F_삭제.Index].Value = "D";
                    dgr.Cells[F_종목코드.Index].Value = dr["STOCK_CODE"].ToString().Trim();
                    dgr.Cells[F_종목명.Index].Value = dr["STOCK_NAME"].ToString().Trim();

                    DataTable dt = SetLine(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgr.Cells[H_3일선.Index].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                        dgr.Cells[H_5일선.Index].Value = dt.Rows[0]["line5"].ToString(); //H_5일선
                        dgr.Cells[H_10일선.Index].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                        dgr.Cells[H_15일선.Index].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                        dgr.Cells[H_20일선.Index].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                        dgr.Cells[H_40일선.Index].Value = dt.Rows[0]["line40"].ToString();//H_40일선
                        dgr.Cells[H_60일선.Index].Value = dt.Rows[0]["line60"].ToString();//H_60일선
                        dgr.Cells[H_120일선.Index].Value = dt.Rows[0]["line120"].ToString();//H_120일선
                        dgr.Cells[H_220일선.Index].Value = dt.Rows[0]["line220"].ToString();//H_220일선
                        dgr.Cells[F_세력선_H.Index].Value = Cls.Val(dt.Rows[0]["MA_H"].ToString()).ToString("#,##0");//MA_H
                        dgr.Cells[F_세력선_C.Index].Value = Cls.Val(dt.Rows[0]["MA_C"].ToString()).ToString("#,##0");//MA_C
                        dgr.Cells[F_세력선_L.Index].Value = Cls.Val(dt.Rows[0]["MA_L"].ToString()).ToString("#,##0");//MA_L
                        dgr.Cells[F_B상한.Index].Value = Cls.Val(dt.Rows[0]["B_UP"].ToString()).ToString("#,##0.00");//B_UP
                        dgr.Cells[F_B하한.Index].Value = Cls.Val(dt.Rows[0]["B_DOWN"].ToString()).ToString("#,##0.00");//B_DOWN
                        if (Decimal.TryParse(dgr.Cells[F_B상한.Index].Value.ToString(), out ret) != false && Decimal.TryParse(dgr.Cells[F_B하한.Index].Value.ToString(), out ret) != false)
                        {
                            if (Cls.Val(dgr.Cells[F_B하한.Index].Value.ToString()) != 0) { 
                                dgr.Cells[F_BB이격.Index].Value = ((Cls.Val(dgr.Cells[F_B상한.Index].Value.ToString()) - Cls.Val(dgr.Cells[F_B하한.Index].Value.ToString())) / Cls.Val(dgr.Cells[F_B하한.Index].Value.ToString()) * 100).ToString("#,##0.0");
                            }
                        }
                        if (Cls.IsDb(_ds전체일봉) == true)
                        {
                            dgr.Cells[F_시가.Index].Value = Cls.ValInt(dt.Rows[0]["s_price"]).ToString("#,##0");
                            dgr.Cells[F_저가.Index].Value = Cls.ValInt(dt.Rows[0]["l_price"]).ToString("#,##0");
                            dgr.Cells[F_고가.Index].Value = Cls.ValInt(dt.Rows[0]["h_price"]).ToString("#,##0");
                            dgr.Cells[F_거래량.Index].Value = Cls.ValInt(dt.Rows[0]["volume"]).ToString("#,##0");
                            dgr.Cells[F_거래대금.Index].Value = Cls.ValInt64(dt.Rows[0]["trading_value"]).ToString("#,##0");
                            dgr.Cells[F_등락율.Index].Value = Cls.Val(dt.Rows[0]["rate"]).ToString("0.00");
                            dgr.Cells[F_현재가.Index].Value = Cls.ValInt(dt.Rows[0]["end_price"]).ToString("#,##0");
                            dgr.Cells[F_대비.Index].Value = Cls.ValInt(dt.Rows[0]["daebi"]).ToString("#,##0");
                        }
                    }

                    dt = SetFinance(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgr.Cells[F_업종.Index].Value = dt.Rows[0]["CLASS_GB"].ToString();
                        dgr.Cells[F_신용비율.Index].Value = dt.Rows[0]["CREDIT_RATE"].ToString();
                        dgr.Cells[F_시가총액.Index].Value = dt.Rows[0]["STOCK_TOTAL_P"].ToString();
                        dgr.Cells[F_PER.Index].Value = dt.Rows[0]["PER"].ToString();
                        dgr.Cells[F_ROE.Index].Value = dt.Rows[0]["ROE"].ToString();
                        dgr.Cells[F_PBR.Index].Value = dt.Rows[0]["PBR"].ToString();
                        dgr.Cells[F_EV.Index].Value = dt.Rows[0]["EV"].ToString();
                        dgr.Cells[F_EPS.Index].Value = dt.Rows[0]["EPS"].ToString();
                        dgr.Cells[F_BPS.Index].Value = dt.Rows[0]["BPS"].ToString();
                        dgr.Cells[F_영업이익.Index].Value = dt.Rows[0]["O_PROFIT"].ToString();
                        dgr.Cells[F_당기순이익.Index].Value = dt.Rows[0]["P_PROFIT"].ToString();
                        dgr.Cells[F_250최고가.Index].Value = dt.Rows[0]["HIGH_250"].ToString();
                        dgr.Cells[F_250최저가.Index].Value = dt.Rows[0]["LOW_250"].ToString();
                        dgr.Cells[F_테마.Index].Value = dt.Rows[0]["THEME_NAME"].ToString();

                        try
                        {
                            Cls.ChangeColor(dgr, "D", 7, F_신용비율.Index);
                            Cls.ChangeColor(dgr, "D", 3, F_PBR.Index);
                            Cls.ChangeColor(dgr, "A", 0, F_영업이익.Index, F_당기순이익.Index);

                            //if (Cls.Val(dgv.Cells[F_신용비율.Index].Value.ToString()) > 5) dgv.Cells[F_신용비율.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_신용비율.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            //if (Cls.Val(dgv.Cells[F_PBR.Index].Value.ToString()) > 3) dgv.Cells[F_PBR.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_PBR.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            //if (Cls.Val(dgv.Cells[F_영업이익.Index].Value.ToString()) < 0) dgv.Cells[F_영업이익.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_영업이익.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            //if (Cls.Val(dgv.Cells[F_당기순이익.Index].Value.ToString()) < 0) dgv.Cells[F_당기순이익.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_당기순이익.Index].Style.ForeColor = System.Drawing.Color.Empty;
                        }
                        catch { }
                    }

                    dt = SetBuySellState(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgr.Cells[F_외국인.Index].Value = dt.Rows[0]["F_TU"].ToString();
                        dgr.Cells[F_기관.Index].Value = dt.Rows[0]["K_TU"].ToString();
                        dgr.Cells[F_외국인10.Index].Value = dt.Rows[1]["F_TU"].ToString();
                        dgr.Cells[F_기관10.Index].Value = dt.Rows[1]["K_TU"].ToString();

                        Cls.ChangeColor(dgr, "A", 0, F_외국인.Index, F_기관.Index, F_외국인10.Index, F_기관10.Index);

                        //if (Cls.Val(dgv.Cells[F_외국인.Index].Value.ToString()) > 0) dgv.Cells[F_외국인.Index].Style.ForeColor = System.Drawing.Color.Red;
                        //else dgv.Cells[F_외국인.Index].Style.ForeColor = System.Drawing.Color.Blue;
                        //if (Cls.Val(dgv.Cells[F_기관.Index].Value.ToString()) > 0) dgv.Cells[F_기관.Index].Style.ForeColor = System.Drawing.Color.Red;
                        //else dgv.Cells[F_기관.Index].Style.ForeColor = System.Drawing.Color.Blue;
                    }

                    if (ds.Tables[0].Columns.Contains("MONITOR_YN") == true && dr["MONITOR_YN"].ToString().Trim() != "")
                    {
                        dgr.Cells["F_모니터링"].Value = dr["MONITOR_YN"].ToString().Trim();
                    }

                    stockCodes += dr["STOCK_CODE"].ToString().Trim() + ";";
                }

                //if (
                //    (Cls.GetWeekOfDay(DateTime.Now) == "토" || Cls.GetWeekOfDay(DateTime.Now) == "일") ||
                //    DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:00:00")) || DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00"))
                //    )
                if (Cls.IsDb(_ds전체일봉) == true) { }
                else{SetDsScreenNo("A", "4", "1", stockCodes, "", true);}
                //else if (Cls.IsDb(_ds전체일봉) == false && DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:00:00")) && DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00")))
                //{
                //    SetDsScreenNo("A", "4", "1", stockCodes, "", true);
                //}
                //else
                //{
                //    SetDsScreenNo("A", "4", "1", stockCodes, "", true);
                //}
            }
            finally
            {
                dg관종.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dg관종_CellValueChanged);
            }
        }

        private void GetJangoList()
        {
            dg관종.CellValueChanged -= dg관종_CellValueChanged;
            dg관종.CellValueChanged -= dg관종_CellValueChanged;
            DataGridViewRow dgr;
            string stockCodes = "";
            string stockCode = "";
            string stockName = "";
            Decimal ret;
            DataGridViewRow dr;
            if (dg뉴스잔고.RowCount == 0) return;
            try
            {
                for (int row = 0; row < dg뉴스잔고.Rows.Count; row++ )
                {
                    dr = dg뉴스잔고.Rows[row];

                    if (ucMainStockVer2._allStockDataset == null) return;
                    if (dr.Cells[J종목코드.Index].Value == null) continue;
                    stockCode = dr.Cells[J종목코드.Index].Value.ToString();
                    stockName = dr.Cells[J종목명.Index].Value.ToString();

                    dg관종.RowCount += 1;
                    dgr = dg관종.Rows[dg관종.RowCount - 1];
                    dgr.Cells[F_삭제.Index].Value = "D";
                    dgr.Cells[F_종목코드.Index].Value = stockCode;
                    dgr.Cells[F_종목명.Index].Value = stockName;

                    DataTable dt = SetLine(stockCode);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgr.Cells[H_3일선.Index].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                        dgr.Cells[H_5일선.Index].Value = dt.Rows[0]["line5"].ToString(); //H_5일선
                        dgr.Cells[H_10일선.Index].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                        dgr.Cells[H_15일선.Index].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                        dgr.Cells[H_20일선.Index].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                        dgr.Cells[H_40일선.Index].Value = dt.Rows[0]["line40"].ToString();//H_40일선
                        dgr.Cells[H_60일선.Index].Value = dt.Rows[0]["line60"].ToString();//H_60일선
                        dgr.Cells[F_세력선_H.Index].Value = Cls.Val(dt.Rows[0]["MA_H"].ToString());//MA_H
                        dgr.Cells[F_세력선_C.Index].Value = Cls.Val(dt.Rows[0]["MA_C"].ToString());//MA_C
                        dgr.Cells[F_세력선_L.Index].Value = Cls.Val(dt.Rows[0]["MA_L"].ToString());//MA_L
                        dgr.Cells[F_B상한.Index].Value = Cls.Val(dt.Rows[0]["B_UP"].ToString());//B_UP
                        dgr.Cells[F_B하한.Index].Value = Cls.Val(dt.Rows[0]["B_DOWN"].ToString());//B_DOWN
                        if (Decimal.TryParse(dgr.Cells[F_B상한.Index].Value.ToString(), out ret) != false && Decimal.TryParse(dgr.Cells[F_B하한.Index].Value.ToString(), out ret) != false)
                        {
                            dgr.Cells[F_BB이격.Index].Value = ((Cls.Val(dgr.Cells[F_B상한.Index].Value.ToString()) - Cls.Val(dgr.Cells[F_B하한.Index].Value.ToString())) / Cls.Val(dgr.Cells[F_B하한.Index].Value.ToString()) * 100).ToString("#,##0.0");
                        }
                        if (Cls.IsDb(_ds전체일봉) == true)
                        {
                            dgr.Cells[F_시가.Index].Value = Cls.ValInt(dt.Rows[0]["s_price"]).ToString("#,##0");
                            dgr.Cells[F_저가.Index].Value = Cls.ValInt(dt.Rows[0]["l_price"]).ToString("#,##0");
                            dgr.Cells[F_고가.Index].Value = Cls.ValInt(dt.Rows[0]["h_price"]).ToString("#,##0");
                            dgr.Cells[F_거래량.Index].Value = Cls.ValInt(dt.Rows[0]["volume"]).ToString("#,##0");
                            dgr.Cells[F_거래대금.Index].Value = Cls.ValInt64(dt.Rows[0]["trading_value"]).ToString("#,##0");
                            dgr.Cells[F_등락율.Index].Value = Cls.Val(dt.Rows[0]["rate"]).ToString("0.00");
                            dgr.Cells[F_현재가.Index].Value = Cls.ValInt(dt.Rows[0]["end_price"]).ToString("#,##0");
                            dgr.Cells[F_대비.Index].Value = Cls.ValInt(dt.Rows[0]["daebi"]).ToString("#,##0");
                        }
                    }

                    dt = SetFinance(stockCode);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgr.Cells[F_업종.Index].Value = dt.Rows[0]["CLASS_GB"].ToString();
                        dgr.Cells[F_신용비율.Index].Value = dt.Rows[0]["CREDIT_RATE"].ToString();
                        dgr.Cells[F_시가총액.Index].Value = dt.Rows[0]["STOCK_TOTAL_P"].ToString();
                        dgr.Cells[F_PER.Index].Value = dt.Rows[0]["PER"].ToString();
                        dgr.Cells[F_ROE.Index].Value = dt.Rows[0]["ROE"].ToString();
                        dgr.Cells[F_PBR.Index].Value = dt.Rows[0]["PBR"].ToString();
                        dgr.Cells[F_EV.Index].Value = dt.Rows[0]["EV"].ToString();
                        dgr.Cells[F_EPS.Index].Value = dt.Rows[0]["EPS"].ToString();
                        dgr.Cells[F_BPS.Index].Value = dt.Rows[0]["BPS"].ToString();
                        dgr.Cells[F_영업이익.Index].Value = dt.Rows[0]["O_PROFIT"].ToString();
                        dgr.Cells[F_당기순이익.Index].Value = dt.Rows[0]["P_PROFIT"].ToString();
                        dgr.Cells[F_250최고가.Index].Value = dt.Rows[0]["HIGH_250"].ToString();
                        dgr.Cells[F_250최저가.Index].Value = dt.Rows[0]["LOW_250"].ToString();
                        dgr.Cells[F_테마.Index].Value = dt.Rows[0]["THEME_NAME"].ToString();

                        try
                        {
                            Cls.ChangeColor(dgr, "D", 7, F_신용비율.Index);
                            Cls.ChangeColor(dgr, "D", 3, F_PBR.Index);
                            Cls.ChangeColor(dgr, "A", 0, F_영업이익.Index, F_당기순이익.Index);

                            //if (Cls.Val(dgv.Cells[F_신용비율.Index].Value.ToString()) > 5) dgv.Cells[F_신용비율.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_신용비율.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            //if (Cls.Val(dgv.Cells[F_PBR.Index].Value.ToString()) > 3) dgv.Cells[F_PBR.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_PBR.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            //if (Cls.Val(dgv.Cells[F_영업이익.Index].Value.ToString()) < 0) dgv.Cells[F_영업이익.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_영업이익.Index].Style.ForeColor = System.Drawing.Color.Empty;
                            //if (Cls.Val(dgv.Cells[F_당기순이익.Index].Value.ToString()) < 0) dgv.Cells[F_당기순이익.Index].Style.ForeColor = System.Drawing.Color.Red;
                            //else dgv.Cells[F_당기순이익.Index].Style.ForeColor = System.Drawing.Color.Empty;
                        }
                        catch { }
                    }

                    dt = SetBuySellState(stockCode);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgr.Cells[F_외국인.Index].Value = dt.Rows[0]["F_TU"].ToString();
                        dgr.Cells[F_기관.Index].Value = dt.Rows[0]["K_TU"].ToString();
                        dgr.Cells[F_외국인10.Index].Value = dt.Rows[1]["F_TU"].ToString();
                        dgr.Cells[F_기관10.Index].Value = dt.Rows[1]["K_TU"].ToString();

                        Cls.ChangeColor(dgr, "A", 0, F_외국인.Index, F_기관.Index, F_외국인10.Index, F_기관10.Index);

                        //if (Cls.Val(dgv.Cells[F_외국인.Index].Value.ToString()) > 0) dgv.Cells[F_외국인.Index].Style.ForeColor = System.Drawing.Color.Red;
                        //else dgv.Cells[F_외국인.Index].Style.ForeColor = System.Drawing.Color.Blue;
                        //if (Cls.Val(dgv.Cells[F_기관.Index].Value.ToString()) > 0) dgv.Cells[F_기관.Index].Style.ForeColor = System.Drawing.Color.Red;
                        //else dgv.Cells[F_기관.Index].Style.ForeColor = System.Drawing.Color.Blue;
                    }

                    //if (ds.Tables[0].Columns.Contains("MONITOR_YN") == true && dr["MONITOR_YN"].ToString().Trim() != "")
                    //{
                    //    dgv.Cells["F_모니터링"].Value = dr["MONITOR_YN"].ToString().Trim();
                    //}

                    stockCodes += stockCode + ";";
                }

                //if (
                //    (Cls.GetWeekOfDay(DateTime.Now) == "토" || Cls.GetWeekOfDay(DateTime.Now) == "일") ||
                //    DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:00:00")) || DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00"))
                //    )
                if (Cls.IsDb(_ds전체일봉) != true)
                {
                    SetDsScreenNo("A", "4", "1", stockCodes, "", true);
                }
            }
            finally
            {
                dg관종.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dg관종_CellValueChanged);
            }
        }

        private void btnThemeR_Click(object sender, EventArgs e)
        {
            dg관종.CellValueChanged -= dg관종_CellValueChanged;
            dg관종.CellValueChanged -= dg관종_CellValueChanged;
            dg관종.RowCount = 0;
            SetTheme();
            _ds전체재정 = _DataAcc.p_stock_finance_query("2", _stockId, false, null, null);
            dg관종.CellValueChanged += dg관종_CellValueChanged;

        }

        private void btn조건1_Click(object sender, EventArgs e)
        {
            Button btnobj = (Button)sender;
            
            Control[] tempObj = tabPage3.Controls.Find("cbo조건" + Cls.Right(btnobj.Name , 1) , true);
            if (tempObj.Length < 1) return;
            ComboBox obj = (ComboBox)tempObj[0];

            if (obj.Enabled == true) return;

            string no;
            string conName;
            if (obj.SelectedIndex == -1) return;
            no = obj.Items[obj.SelectedIndex].ToString().Split('^')[0];
            conName = obj.Items[obj.SelectedIndex].ToString().Split('^')[1];
            ucMainStockVer2.SendConditionStop("9" + no, conName, no);
            obj.Enabled = true;
        }

        private void dgDart_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1 ) return;
            if(dgDart.Rows[e.RowIndex].Cells[e.ColumnIndex] == null ) return;
            if (e.ColumnIndex == 종목.Index) { 
                txt관종.Text = dgDart.Rows[e.RowIndex].Cells[종목.Index].Value.ToString();
                txt관종_KeyDown(txt관종, new KeyEventArgs(Keys.Enter));
            }
        }

        private void chkCFinance_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 1; i <= 5; i++)
            {
                DataGridView dg = (DataGridView)tabPage3.Controls.Find("dg조건종목" + i.ToString(), true)[0];
                dg.Columns[CPER.Index].Visible = chkCFinance.Checked;
                dg.Columns[CROE.Index].Visible = chkCFinance.Checked;
                dg.Columns[CPBR.Index].Visible = chkCFinance.Checked;
                dg.Columns[CEV.Index].Visible = chkCFinance.Checked;
                dg.Columns[CEPS.Index].Visible = chkCFinance.Checked;
                dg.Columns[CBPS.Index].Visible = chkCFinance.Checked;
                dg.Columns[C영업이익.Index].Visible = chkCFinance.Checked;
                dg.Columns[C당기순이익.Index].Visible = chkCFinance.Checked;
                dg.Columns[C250최고가.Index].Visible = chkCFinance.Checked;
                dg.Columns[C250최저가.Index].Visible = chkCFinance.Checked;

            }
        }

        private void btnFav보유_Click(object sender, EventArgs e)
        {
            rb1.Checked = false;
            rb2.Checked = false;
            rb3.Checked = false;
            rb4.Checked = false;
            rb5.Checked = false;
            rbA.Checked = false;

            dg관종.RowCount = 0;

            GetJangoList();
        }

        private void btnTickReset_Click(object sender, EventArgs e)
        {
            _dsTick60.Tables[cboTickDataMember.Text.Substring(0, 6)].Rows.Clear();
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            splitContainer3.Panel2Collapsed = !splitContainer3.Panel2Collapsed;
        }

        private void rb1_Click(object sender, EventArgs e)
        {
            //RadioButton rb = (RadioButton)sender;
            //if (rb.Name.Equals(rb1.Name)) rb1_CheckedChanged(rb1, new EventArgs());
            //if (rb.Name.Equals(rb2.Name)) rb2_CheckedChanged(rb2, new EventArgs());
            //if (rb.Name.Equals(rb3.Name)) rb3_CheckedChanged(rb3, new EventArgs());
            //if (rb.Name.Equals(rb4.Name)) rb4_CheckedChanged(rb4, new EventArgs());
            //if (rb.Name.Equals(rb5.Name)) rb5_CheckedChanged(rb5, new EventArgs());
            //if (rb.Name.Equals(rbA.Name)) rbA_CheckedChanged(rbA, new EventArgs());
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (rb1.Checked) rb1_CheckedChanged(rb1, new EventArgs());
            else if (rb2.Checked) rb2_CheckedChanged(rb2, new EventArgs());
            else if (rb3.Checked) rb3_CheckedChanged(rb3, new EventArgs());
            else if (rb4.Checked) rb4_CheckedChanged(rb4, new EventArgs());
            else if (rb5.Checked) rb5_CheckedChanged(rb5, new EventArgs());
            else if (rbA.Checked) rbA_CheckedChanged(rbA, new EventArgs());
        }

        private void 유무ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString();
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목1.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목2.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목3.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목4.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목5.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();

            if (stockCode == "") return;

            foreach (DataRow dr in _dsTick60.Tables[stockCode].Select("매수유무 = 1 OR 매도유무 = 1"))
            {
                dr["매수유무"] = 0;
                dr["매도유무"] = 0;
            }
        }

        private void 신호ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString();
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목1.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목2.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목3.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목4.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목5.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();

            if (stockCode == "") return;

            ResetSignal(stockCode , 0);
        }
        private void ResetSignal(string stockCode , int resetCnt)
        {
            int cnt= 0;
            foreach (DataRow dr in _dsTick60.Tables[stockCode].Select("매수신호 = 1"))
            {
                dr["매수신호"] = 0;
                cnt++;
                if (!resetCnt.Equals(0) && cnt.Equals(resetCnt)) { break; }
            }

            foreach (DataRow dr in _dsTick60.Tables[stockCode].Select("매수유무 = 1"))
            {
                dr["매수유무"] = 0;
            }
        }
        private void tICKToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            int row;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells[J종목코드.Index].Value.ToString();
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells[P종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells[N종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목1.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목2.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목3.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목4.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg조건종목5.Name) stockCode = dgObj.Rows[row].Cells[C종목코드.Index].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells[F_종목코드.Index].Value.ToString();

            if (stockCode == "") return;
            _dsTick60.Tables[stockCode].Rows.Clear();
        }

        private void dg뉴스잔고_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView obj = (DataGridView)sender;
            if (obj.IsCurrentCellDirty)
            {
                obj.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Equals(""))
            {
                foreach (DataGridViewRow dgr in dg관종.Rows)
                {
                    if (dgr.Cells[F_종목명.Index].Value == null) break;
                    dgr.Visible = true;
                }
            }   
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                foreach (DataGridViewRow dgr in dg관종.Rows)
                {
                    if (dgr.Cells[F_종목명.Index].Value == null) break;
                    if (txtSearch.Text.Equals(dgr.Cells[F_종목명.Index].Value)) dgr.Visible = true;
                    else dgr.Visible = false;
                }
            }
        }

        private void dgN관종_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1) return ;

            DataGridView dg = (DataGridView)sender;
            DataGridViewRow dgr = dg.Rows[e.RowIndex];

            Cls.ChangeColor(dgr, "A", 0 , P등락률.Index);
            if (Cls.Val(dgr.Cells[P등락률.Index].Value) < 0)
            {
                dgr.Cells[P현재가.Index].Style.ForeColor = System.Drawing.Color.Blue;
            }
            else if (Cls.Val(dgr.Cells[P등락률.Index].Value) > 0)
            {
                dgr.Cells[P현재가.Index].Style.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                dgr.Cells[P현재가.Index].Style.ForeColor = System.Drawing.Color.Empty;
            }

            if (Cls.Val(dgr.Cells[P현재가.Index].Value) >= Cls.Val(dgr.Cells[P모니터링가격.Index].Value))
            {
                dgr.Cells[P모니터링가격.Index].Style.BackColor = System.Drawing.Color.LightBlue;
            }
            else
            {
                dgr.Cells[P모니터링가격.Index].Style.BackColor = System.Drawing.Color.Empty;
            }
        }

        private void cboBuySellDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBuySellDay.Text.Equals("5"))
            {
                dg조건종목1.Columns[C외국인.Index].Visible = true;
                dg조건종목1.Columns[C기관.Index].Visible = true;
                dg조건종목2.Columns[C외국인.Index].Visible = true;
                dg조건종목2.Columns[C기관.Index].Visible = true;
                dg조건종목3.Columns[C외국인.Index].Visible = true;
                dg조건종목3.Columns[C기관.Index].Visible = true;
                dg조건종목4.Columns[C외국인.Index].Visible = true;
                dg조건종목4.Columns[C기관.Index].Visible = true;
                dg조건종목5.Columns[C외국인.Index].Visible = true;
                dg조건종목5.Columns[C기관.Index].Visible = true;

                dg조건종목1.Columns[C외국인10.Index].Visible = false;
                dg조건종목1.Columns[C기관10.Index].Visible = false;
                dg조건종목2.Columns[C외국인10.Index].Visible = false;
                dg조건종목2.Columns[C기관10.Index].Visible = false;
                dg조건종목3.Columns[C외국인10.Index].Visible = false;
                dg조건종목3.Columns[C기관10.Index].Visible = false;
                dg조건종목4.Columns[C외국인10.Index].Visible = false;
                dg조건종목4.Columns[C기관10.Index].Visible = false;
                dg조건종목5.Columns[C외국인10.Index].Visible = false;
                dg조건종목5.Columns[C기관10.Index].Visible = false;

                dg관종.Columns[F_외국인.Index].Visible = true;
                dg관종.Columns[F_기관.Index].Visible = true;
                dg관종.Columns[F_외국인10.Index].Visible = false;
                dg관종.Columns[F_기관10.Index].Visible = false;
            }
            else if (cboBuySellDay.Text.Equals("10"))
            {
                dg조건종목1.Columns[C외국인.Index].Visible = false;
                dg조건종목1.Columns[C기관.Index].Visible = false;
                dg조건종목2.Columns[C외국인.Index].Visible = false;
                dg조건종목2.Columns[C기관.Index].Visible = false;
                dg조건종목3.Columns[C외국인.Index].Visible = false;
                dg조건종목3.Columns[C기관.Index].Visible = false;
                dg조건종목4.Columns[C외국인.Index].Visible = false;
                dg조건종목4.Columns[C기관.Index].Visible = false;
                dg조건종목5.Columns[C외국인.Index].Visible = false;
                dg조건종목5.Columns[C기관.Index].Visible = false;

                dg조건종목1.Columns[C외국인10.Index].Visible = true;
                dg조건종목1.Columns[C기관10.Index].Visible = true;
                dg조건종목2.Columns[C외국인10.Index].Visible = true;
                dg조건종목2.Columns[C기관10.Index].Visible = true;
                dg조건종목3.Columns[C외국인10.Index].Visible = true;
                dg조건종목3.Columns[C기관10.Index].Visible = true;
                dg조건종목4.Columns[C외국인10.Index].Visible = true;
                dg조건종목4.Columns[C기관10.Index].Visible = true;
                dg조건종목5.Columns[C외국인10.Index].Visible = true;
                dg조건종목5.Columns[C기관10.Index].Visible = true;

                dg관종.Columns[F_외국인.Index].Visible = false;
                dg관종.Columns[F_기관.Index].Visible = false;
                dg관종.Columns[F_외국인10.Index].Visible = true;
                dg관종.Columns[F_기관10.Index].Visible = true;
            }
            else
            {
                dg조건종목1.Columns[C외국인.Index].Visible = true;
                dg조건종목1.Columns[C기관.Index].Visible = true;
                dg조건종목2.Columns[C외국인.Index].Visible = true;
                dg조건종목2.Columns[C기관.Index].Visible = true;
                dg조건종목3.Columns[C외국인.Index].Visible = true;
                dg조건종목3.Columns[C기관.Index].Visible = true;
                dg조건종목4.Columns[C외국인.Index].Visible = true;
                dg조건종목4.Columns[C기관.Index].Visible = true;
                dg조건종목5.Columns[C외국인.Index].Visible = true;
                dg조건종목5.Columns[C기관.Index].Visible = true;

                dg조건종목1.Columns[C외국인10.Index].Visible = true;
                dg조건종목1.Columns[C기관10.Index].Visible = true;
                dg조건종목2.Columns[C외국인10.Index].Visible = true;
                dg조건종목2.Columns[C기관10.Index].Visible = true;
                dg조건종목3.Columns[C외국인10.Index].Visible = true;
                dg조건종목3.Columns[C기관10.Index].Visible = true;
                dg조건종목4.Columns[C외국인10.Index].Visible = true;
                dg조건종목4.Columns[C기관10.Index].Visible = true;
                dg조건종목5.Columns[C외국인10.Index].Visible = true;
                dg조건종목5.Columns[C기관10.Index].Visible = true;

                dg관종.Columns[F_외국인.Index].Visible = true;
                dg관종.Columns[F_기관.Index].Visible = true;
                dg관종.Columns[F_외국인10.Index].Visible = true;
                dg관종.Columns[F_기관10.Index].Visible = true;
            }
            
        }

        private void txtOrderStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ucMainStockVer2._allStockDataset == null) return;
                DataView dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
                dv.RowFilter = "STOCK_NAME = '" + txtOrderStock.Text.Trim() + "'";

                if (dv.Count < 1) return;
                string stockCode = dv[0]["STOCK_CODE"].ToString().Trim();
                if (stockCode.Equals("")) return;
                NewsOrderAdd(stockCode, "Y", "", "수", 0, 0, 0, 0);
                txtOrderStock.Text = "";
            }
        }

        private void NewsOrderAdd(string stockCode, string isForce, string monPrice, string monGb, int 시가, int 고가, int 저가, int 현재가)
        {
            string stockName = ucMainStockVer2.GetStockInfo(stockCode);
            var objOrder = (from DataGridViewRow dgr in dg뉴스체결.Rows
                            where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(stockCode)
                            select dgr).FirstOrDefault();

            if (objOrder == null)
            {
                dg뉴스체결.Rows.Insert(0, 1);
                DataGridViewRow dgr = dg뉴스체결.Rows[0];
                dgr.Cells[N종목코드.Index].Value = stockCode;
                dgr.Cells[N종목명.Index].Value = stockName;
                dgr.Cells[N최초거래시간.Index].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dgr.Cells[N강제추가여부.Index].Value = isForce;
                dgr.Cells[N모니터링가격.Index].Value = monPrice;
                dgr.Cells[N모니터링구분.Index].Value = monGb;
                //뉴스 체결에 등록 - E
            }
        }

        private void btnDartOnOff_Click(object sender, EventArgs e)
        {
            if (btnDartOnOff.Text.Equals("▶"))
            {
                tmrNews.Enabled = true;
                tmrDart.Enabled = true;
                tmrDartKrx.Enabled = true;
                btnDartOnOff.Text = "||";
            }
            else
            {
                tmrNews.Enabled = false;
                tmrDart.Enabled = false;
                tmrDartKrx.Enabled = false;
                btnDartOnOff.Text = "▶";
            }
        }

        private void dgN관종_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblNCnt.Text = dgN관종.RowCount.ToString();
        }

        private void dgTotalNews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == T주소.Index)
            {
                System.Diagnostics.Process.Start(dgTotalNews.Rows[e.RowIndex].Cells[T주소.Index].Value.ToString().Trim());
            }
        }

        private void btnTotalNewsR_Click(object sender, EventArgs e)
        {
            dgTotalNews.RowCount = 0;
        }
    }
}
