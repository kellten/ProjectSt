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
using Common;
using System.Collections;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.Internals;
using System.Threading.Tasks;

namespace NewsFinder
{
    public partial class frmMainVer2 : Form
    {
        private DataSet _ds뉴스 = new DataSet();
        private DataSet _ds조건체결 = new DataSet();
        private DataTable _dt보유잔고 = new DataTable();
        private DataTable _dt미체결 = new DataTable();
        private DataSet _dsTick60 = new DataSet();
        private DataSet _ds관종일봉 = new DataSet();
        private DataSet _ds관종재정 = new DataSet();

        private string[] _뉴스 = { "현재가", "등락율", "체결강도", "매수거래량", "매도거래량", "매수거래비용", "매도거래비용", "거래시간" };
        private string[] _체결 = { "체결시간", "현재가", "전일대비", "등락율", "거래량", "누적거래량", "누적거래대금", "시가", "고가", "저가", "전일거래량대비", "거래비용", "체결강도", "상한가발생시간", "최고거래량", "최저거래량", "최고체결강도", "최저체결강도" };
        private string[] _보유잔고 = { "종목번호", "종목명", "매입가", "수익률(%)", "현재가", "매입금액", "평가금액", "보유수량", "매매가능수량" };
        private string[,] _미체결 = { { "주문구분", "System.String" }, { "주문번호", "System.String" }, { "종목코드", "System.String" }, { "종목명", "System.String" }, { "주문가격", "System.Int32" }, { "주문수량", "System.Int32" }, { "미체결수량", "System.Int32" }, { "주문금액", "System.Int32" } };
        private string[,] _Tick60 = { { "일자", "System.String" }, { "현재가", "System.Int32" }, { "시가", "System.Int32" }, { "고가", "System.Int32" }, { "저가", "System.Int32" }, { "등락율", "System.Decimal" }, { "체결강도", "System.Decimal" }, { "매수거래량", "System.Int32" }, { "매도거래량", "System.Int32" }, { "매수거래비용", "System.Int32" }, { "매도거래비용", "System.Int32" }, { "시작시간", "System.String" }, { "종료시간", "System.String" }, { "LINE5", "System.Decimal" }, { "LINE10", "System.Decimal" }, { "LINE20", "System.Decimal" }, { "LINE40", "System.Decimal" }, { "LINE60", "System.Decimal" }, { "BBUP", "System.Double" }, { "BBDOWN", "System.Double" }, { "COUNT", "System.Int32" }, { "매수유무", "System.Int32" }, { "매도유무", "System.Int32" } };
        private DataAccess _DataAcc;
        private DataTable _dt화면관리 = new DataTable();
        private DataTable _dtDart = new DataTable();

        private clsKiwoomBaseInfo _clsKiwoomBaseInfo = new clsKiwoomBaseInfo();
        private int _SLEEP_TIME = 200;
        //private string[] dart = { 
        //                            "무상증자" ,
        //                            "소유상황" ,
        //                            "대량보유" ,
        //                            "자기주식" ,
        //                            "최대주주" ,
        //                            "유상증자" ,
        //                            "단일판매",
        //                            "주식변동",
        //                            "회생계획"          
        //                        };
        public frmMainVer2()
        {

            InitializeComponent();

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

            foreach (string str in _보유잔고)
            {
                _dt보유잔고.Columns.Add(str);
            }

            for (int i = 0; i < _미체결.Length / 2; i++)
            {
                _dt미체결.Columns.Add(_미체결[i, 0], Type.GetType(_미체결[i, 1]));
            }

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
                //request.Headers.Add("X-Naver-Client-Id", "lLk8wyiPEClhz4iSE5uS");
                //request.Headers.Add("X-Naver-Client-Secret", "AO5aMsIpja");

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

                //dt = ds.Tables["ITEM"].Clone();
                foreach (System.Data.DataRow dr in ds.Tables["ITEM"].Rows)
                {
                    dt.Rows.Add(dr.ItemArray);
                }
            }



            //dv = new DataView(dt);
            //dv.RowFilter = "title like '%" + keyword.Replace("종목분석", "").Trim() + "%'" ;
            ////dv.Sort = "link desc";
            //foreach (System.Data.DataRowView dr in dv)
            //{
            //    if (dataGridView3.Rows.Count > 1)
            //    {
            //        Boolean blnTrue = false;

            //        for (int i = 0; i < dataGridView3.Rows.Count; i++)
            //        {
            //            if (i + 1 == dataGridView3.Rows.Count)
            //            {
            //                break;
            //            }

            //            if (dataGridView3.Rows[i].Cells[3].Value.ToString().Trim() == dr["LINK"].ToString().Trim())
            //            {
            //                blnTrue = true;
            //                break;
            //            }
            //        }
            //        if (blnTrue == false)
            //        {
            //            dataGridView3.Rows.Insert(0, 1);

            //            dataGridView3.Rows[0].Cells[0].Value = DateTime.Now;
            //            dataGridView3.Rows[0].Cells[1].Value = dr["TITLE"].ToString().Trim();
            //            dataGridView3.Rows[0].Cells[2].Value = dr["DESCRIPTION"].ToString().Trim();
            //            dataGridView3.Rows[0].Cells[3].Value = dr["LINK"].ToString().Trim();
            //        }
            //    }
            //    else
            //    {
            //        dataGridView3.Rows.Insert(0, 1);

            //        dataGridView3.Rows[0].Cells[0].Value = DateTime.Now;
            //        dataGridView3.Rows[0].Cells[1].Value = dr["TITLE"].ToString().Trim();
            //        dataGridView3.Rows[0].Cells[2].Value = dr["DESCRIPTION"].ToString().Trim();
            //        dataGridView3.Rows[0].Cells[3].Value = dr["LINK"].ToString().Trim();
            //    }

            //}
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
                this.Text = "RichStock - " + itemText;
                _idx1++;
                if (_idx1 > 1000) _idx1 = 0;
            }

            ds = Cls.NaverNews(itemText);
            DataView dv = new DataView(ds.Tables["item"]);
            dv.RowFilter = "ORIGINALLINK NOT LIKE '%star.mt.co.kr%'";
            bool blnTrue = false;
            foreach (System.Data.DataRowView dr in dv)
            {
                blnTrue = false;
                for (int row = 0; row < dgNews.Rows.Count - 1; row++)
                {
                    if (dgNews.Rows[row].Cells["N주소"].Value.ToString().Trim() == dr["ORIGINALLINK"].ToString().Trim() ||
                        dgNews.Rows[row].Cells["N제목"].Value.ToString().Trim() == Cls.HtmlToPlainText(dr["TITLE"].ToString().Trim()))
                    {
                        blnTrue = true;
                        break;
                    }
                }

                if (blnTrue == true) continue;
                dgNews.Rows.Insert(0, 1);

                dgNews.Rows[0].Cells["N시간"].Value = DateTime.Now;
                dgNews.Rows[0].Cells["N키워드"].Value = lst검색어.Items[idx].ToString().Trim();
                dgNews.Rows[0].Cells["N제목"].Value = Cls.HtmlToPlainText(dr["TITLE"].ToString().Trim());
                dgNews.Rows[0].Cells["N주소"].Value = dr["ORIGINALLINK"].ToString().Trim();
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
                    ////            double tmpCnt = double.Parse(html.Substring(index3 + 1, index4 - index3 - 1));
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
            try
            {
                GetNaNews();
            }
            catch (Exception ex) { Logger("에러 (tmrNaver)", ex.Message); }
            finally
            {
                if (tmrNaver.Enabled == false)
                    tmrNaver.Enabled = true;
            }
            return;
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
            //    dgLog.Rows.RemoveAt(dgLog.Rows.Count - 2);
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
            //    매출액대비Per = Decimal.Parse(title.Replace("-자동매수", "").Substring(title.IndexOf("대비:") + 3));
            //}

            if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:20:00")))
            {
                if (title.IndexOf("3자배정") < 0) return;
            }

            if (UcMainStockVer2._allStockDataset == null) { return; }
            DataView dv = new DataView(UcMainStockVer2._allStockDataset.Tables[0]);
            dv.RowFilter = "STOCK_NAME = '" + StockName + "'";

            if (dv.Count == 0) { return; }

            DataRow drDart = _dtDart.NewRow();

            foreach (DataRowView dr in dv)
            {
                if (chk자동매매.Checked == true)
                {
                    drDart["시간"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    drDart["종목코드"] = dr["STOCK_CODE"].ToString().Trim();
                    drDart["종목명"] = dr["STOCK_NAME"].ToString().Trim();
                    drDart["제목"] = title;
                    _dtDart.Rows.Add(drDart);

                    if (Cls.isExistsRow(dgN관종, 0, dr["STOCK_CODE"].ToString().Trim()) == -1 && Cls.isExistsRow(dg뉴스체결, 0, dr["STOCK_CODE"].ToString().Trim()) == -1 && chk공.Checked == true)
                    {
                        dgN관종.Rows.Insert(0, 1);
                        dgN관종.Rows[0].Cells["P종목코드"].Value = dr["STOCK_CODE"].ToString().Trim();
                        dgN관종.Rows[0].Cells["P종목명"].Value = dr["STOCK_NAME"].ToString().Trim();
                        dgN관종.Rows[0].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dgN관종.Rows[0].Cells["P강제추가여부"].Value = "";
                        dgN관종.Rows[0].Cells["P모니터링가격"].Value = "";
                        dgN관종.Rows[0].Cells["P모니터링구분"].Value = "공";

                        if (_ds뉴스.Tables[dr["STOCK_CODE"].ToString().Trim()] == null)
                        {
                            DataTable dt = _ds뉴스.Tables.Add(dr["STOCK_CODE"].ToString().Trim());
                            foreach (string str in _뉴스)
                            {
                                dt.Columns.Add(str);
                            }

                        }

                        SetDsScreenNo("A", "1", "1", dr["STOCK_CODE"].ToString().Trim(), dr["STOCK_NAME"].ToString().Trim(), true);
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
            if (매매Type == ucMainStock.OrderType.신규매수)
            {
                UcMainStockVer2.SendOrder_OnReceiveChejanData("매수", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.신규매수,
                   "", 종목코드, 수량, 거래구분 == "03" || 거래구분 == "61" || 거래구분 == "81" ? 0 : 주문금액, 거래구분, "");
            }
            else if (매매Type == ucMainStock.OrderType.신규매도)
            {
                UcMainStockVer2.SendOrder_OnReceiveChejanData("매도", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.신규매도,
                   "", 종목코드, 수량, 거래구분 == "03" || 거래구분 == "61" || 거래구분 == "81" ? 0 : 주문금액, 거래구분, "");
            }
            else if (매매Type == ucMainStock.OrderType.매수취소)
            {
                if (_dt미체결.Select(String.Format("종목코드 LIKE '%{0}'", 종목코드)).Length > 0)
                {
                    UcMainStockVer2.SendOrder_OnReceiveChejanData("매수취소", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.매수취소,
                   "", 종목코드, 수량, 0, 거래구분, 주문번호);
                }
            }
            else if (매매Type == ucMainStock.OrderType.매도취소)
            {
                if (_dt미체결.Select(String.Format("종목코드 LIKE '%{0}'", 종목코드)).Length > 0)
                {
                    UcMainStockVer2.SendOrder_OnReceiveChejanData("매도취소", cboAccount.Text.Trim(), ucMainStockVer2.OrderType.매도취소,
                       "", 종목코드, 수량, 0, 거래구분, 주문번호);
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
            if (Cls.GetWeekOfDay(DateTime.Now) == "토" || Cls.GetWeekOfDay(DateTime.Now) == "일")
            {
                tmrDart.Enabled = false;
                return;
            }
            tmrDart.Enabled = false;
            try
            {
                if (Cls.GetWeekOfDay(DateTime.Now) == "토" ||
                    Cls.GetWeekOfDay(DateTime.Now) == "일"
                    )
                {
                    tmrDart.Enabled = false;
                }


                GetGong();

                if (dgDart.RowCount > 50) { dgDart.Rows.RemoveAt(dgDart.Rows.Count - 2); }
            }
            catch (Exception ex) { Logger("에러 (tmrDart)", ex.Message); }
            finally
            {
                if (tmrDart.Enabled == false)
                    tmrDart.Enabled = true;
            }
        }

        private void tmrDartKrx_Tick(object sender, EventArgs e)
        {
            if (Cls.GetWeekOfDay(DateTime.Now) == "토" || Cls.GetWeekOfDay(DateTime.Now) == "일")
            {
                tmrDartKrx.Enabled = false;
                return;
            }
            tmrDartKrx.Enabled = false;
            try
            {
                if (Cls.GetWeekOfDay(DateTime.Now) == "토" ||
                    Cls.GetWeekOfDay(DateTime.Now) == "일"
                    )
                {
                    tmrDartKrx.Enabled = false;
                }


                GetGongDataKrx();

                if (dgDartNew.RowCount > 50) { dgDartNew.Rows.RemoveAt(dgDartNew.Rows.Count - 2); }
            }
            catch (Exception ex) { Logger("에러 (tmrDart)", ex.Message); }
            finally
            {
                if (tmrDartKrx.Enabled == false)
                    tmrDartKrx.Enabled = true;
            }
        }

        public async void GetGongData()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = System.Text.UTF8Encoding.UTF8;
                    //return await wc.DownloadStringTaskAsync("http://dart.fss.or.kr/api/todayRSS.xml");        
                    //wc.DownloadStringCompleted += (sender, e) => e.Result;
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
                    //return await wc.DownloadStringTaskAsync("http://dart.fss.or.kr/api/todayRSS.xml");        
                    //wc.DownloadStringCompleted += (sender, e) => e.Result;
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
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WcDownLoadCompApi);
                    await wc.DownloadStringTaskAsync(apiURL);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger("에러 (GetGongDataApi)", ex.ToString());
            }

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
                            //            double tmpCnt = double.Parse(html.Substring(index3 + 1, index4 - index3 - 1));
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

        void WcDownLoadCompApi(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                bool 팝업필터 = false;
                DataView dv;
                DataSet ds = new DataSet();

                StringReader sReader = new StringReader(e.Result);
                System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

                ds.ReadXml(reader);

                //DataSet ds = Cls.Dart();
                dv = new DataView(ds.Tables["list"]);
                dv.RowFilter = "crp_cls in ('Y' , 'K')";
                foreach (System.Data.DataRowView dr in dv)
                {
                    Boolean blnTrue = false;
                    for (int i = 0; i < dgDartApi.Rows.Count; i++)
                    {

                        if (dgDartApi.Rows[i].Cells[2].Value == null) break;
                        string tmpStr = dgDartApi.Rows[i].Cells[2].Value.ToString();
                        if (tmpStr == "") break;
                        ////if (tmpStr.IndexOf("매출액 대비") > -1)
                        ////{
                        ////    tmpStr = dgDartApi.Rows[i].Cells[2].Value.ToString().Replace("-자동매수", "").Substring(0, dgDartApi.Rows[i].Cells[2].Value.ToString().Replace("-자동매수", "").Length - 7);
                        ////}
                        ////else
                        ////{
                        ////    tmpStr = dgDartApi.Rows[i].Cells[2].Value.ToString().Replace("-자동매수", "");
                        ////}

                        if (dgDartApi.Rows[i].Cells[1].Value.ToString() + dgDartApi.Rows[i].Cells[2].Value.ToString() == dr["crp_nm"].ToString().Trim() + dr["rpt_nm"].ToString().Trim())
                        {
                            blnTrue = true;
                            break;
                        }
                    }

                    if (blnTrue == false)
                    {
                        dgDartApi.Rows.Insert(0, 1);

                        dgDartApi.Rows[0].Cells[0].Value = dr["rcp_dt"].ToString().Trim() + " " + DateTime.Now.ToString("HH:mm:ss.") + DateTime.Now.Millisecond;
                        dgDartApi.Rows[0].Cells[1].Value = dr["crp_nm"].ToString().Trim();
                        dgDartApi.Rows[0].Cells[2].Value = dr["rpt_nm"].ToString().Trim();
                        dgDartApi.Rows[0].Cells[3].Value = "http://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + dr["rcp_no"].ToString().Trim();

                        팝업필터 = false;
                        foreach (string str2 in lst팝업필터.Items)
                        {
                            if (dgDartApi.Rows[0].Cells[2].Value.ToString().IndexOf(str2) > -1)
                            {
                                팝업필터 = true;
                                break;
                            }
                        }

                        if (팝업필터 == true) { break; }

                        ////bool is자동매수 = false;
                        ////foreach (string str1 in lsb자동매수단어.Items)
                        ////{
                        ////    if (dgDartApi.Rows[0].Cells[2].Value.ToString().IndexOf(str1) > -1)
                        ////    {

                        ////        AutoBuy(dgDartApi.Rows[0].Cells[1].Value.ToString().Trim(), dgDartApi.Rows[0].Cells[2].Value.ToString().Trim());
                        ////        is자동매수 = true;
                        ////        break;
                        ////    }
                        ////}

                        ////if (is자동매수 == true)
                        ////{
                        ////    string oStr = "";
                        ////    oStr = dgDartApi.Rows[0].Cells[1].Value.ToString() + "-" + dgDartApi.Rows[0].Cells[2].Value.ToString() + " " + dgDartApi.Rows[0].Cells[3].Value.ToString();
                        ////    _frm.UpdateNoti(oStr);
                        ////    _frm.Visible = true;
                        ////    _frm.TopMost = true;
                        ////    //System.Windows.Forms.Clipboard.SetText(dgDartApi.Rows[0].Cells["종목"].Value.ToString().Trim());
                        ////    break;
                        ////}
                    }
                }
            }
            catch (Exception ex)
            {

                Logger("에러 (WcDownLoadCompApi)", ex.ToString());
            }
        }

        private void datagridview2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                System.Diagnostics.Process.Start(dgDart.Rows[e.RowIndex].Cells[3].Value.ToString().Trim());
            }
        }

        Form1 _frm;
        private void SimpleBrowserForm_Load(object sender, EventArgs e)
        {
            //InitBrowser();
            _frm = new Form1();
            dgTick.DataSource = _dsTick60;
            dg미체결.DataSource = _dt미체결;
            cboTickStd.SelectedIndex = 0;
            UcMainStockVer2.Connection();
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
            tmrNaver.Stop();
            tmrDart.Stop();
            tmrDartKrx.Stop();
            tmrDart1.Stop();

            Task.WaitAll();
            if (MessageBox.Show("TICK 테이블 저장하시겠습니까?", "알림!!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                tbStockList.SelectedIndex = 7;
                foreach (DataTable dt in _dsTick60.Tables)
                {
                    TickSaveNew(dt);
                }
            }

            _frm.Dispose();
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
            //tmrDaum.Start();
            //tmrNaver.Start();

            //chk자동매매.Checked = false;
            dgDart.RowCount = 1;
            dgDartApi.RowCount = 1;
            dgDartNew.RowCount = 1;

            tmrDart.Start();
            tmrDartKrx.Start();
            //tmrDart1.Start();


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
                chk이익실현.Enabled = false;
                nm이익실현.Enabled = false;
                chk손절.Enabled = false;
                nm손절.Enabled = false;
            }
            else
            {
                btnStopLoss.Text = "▶";
                chk이익실현.Enabled = true;
                nm이익실현.Enabled = true;
                chk손절.Enabled = true;
                nm손절.Enabled = true;
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
                if (UcMainStockVer2._allStockDataset == null) return;
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
                    DataView dv = new DataView(UcMainStockVer2._allStockDataset.Tables[0]);
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
                if (dg조건리스트.Rows.Count < 2)
                {
                    UcMainStockVer2.GetConditionLoad_OnReceiveTrCondition();
                }
            }
            else if (tbStockList.SelectedIndex == 3)
            {

            }
        }


        private void btn로그클리어_Click(object sender, EventArgs e)
        {
            dgLog.RowCount = 1;
        }


        private void dg조건리스트_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value.ToString().Trim() == "||") return;
            //dg조건종목.RowCount = 1;
            //UcMainStockVer2.SendCondition_OnReceiveConditionVer("9" + dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim()
            //    , dg조건리스트.Rows[e.RowIndex].Cells["조건명"].Value.ToString().Trim()
            //    , dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim(), 0);

        }


        private void btn조건식_Click(object sender, EventArgs e)
        {

        }

        private void btn일괄매수_Click(object sender, EventArgs e)
        {
            //Decimal 매수금액 = 0;
            //Decimal 수량 = 0;
            //Decimal 주문가 = 0;
            //Decimal 현재가 = 0;
            //string 종목코드 = "";
            //for (int row = 0; row < dg조건종목.RowCount - 1; row++)
            //{
            //    if (dg조건종목.Rows[row].Cells["C종목코드"].Value == null) { continue; }
            //    if (Convert.ToBoolean(dg조건종목.Rows[row].Cells["C체크"].Value) == true)
            //    {
            //        종목코드 = dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString();
            //        현재가 = Decimal.Parse(dg조건종목.Rows[row].Cells["C현재가"].Value.ToString());
            //        try
            //        {
            //            if (txt1차매수금액.Text.Trim() == "" || dg조건종목.Rows[row].Cells["C1차매수가"].Value.ToString().Trim() == "") { continue; }
            //            주문가 = MakeOrderPrice(Convert.ToInt32(Convert.ToDecimal(dg조건종목.Rows[row].Cells["C1차매수가"].Value.ToString()).ToString("########")));
            //            if (현재가 > 주문가)
            //            {
            //                매수금액 = Int32.Parse(txt1차매수금액.Text.Trim());
            //                수량 = Convert.ToInt32(매수금액 / 주문가);
            //                if (매수금액 < 주문가) { continue; }
            //                SendBuySellMsg(종목코드, "00", 1, 0, (int)주문가, (int)수량, "3");
            //                SystemSleep();
            //            }

            //            if (txt2차매수금액.Text.Trim() == "" || dg조건종목.Rows[row].Cells["C2차매수가"].Value.ToString().Trim() == "") { continue; }
            //            주문가 = MakeOrderPrice(Convert.ToInt32(Convert.ToDecimal(dg조건종목.Rows[row].Cells["C2차매수가"].Value.ToString()).ToString("########")));
            //            if (현재가 > 주문가)
            //            {
            //                매수금액 = Int32.Parse(txt2차매수금액.Text.Trim());
            //                수량 = Convert.ToInt32(매수금액 / 주문가);
            //                if (매수금액 < 주문가) { continue; }
            //                SendBuySellMsg(종목코드, "00", 1, 0, (int)주문가, (int)수량, "3");
            //                SystemSleep();
            //            }

            //            if (txt3차매수금액.Text.Trim() == "" || dg조건종목.Rows[row].Cells["C3차매수가"].Value.ToString().Trim() == "") { continue; }
            //            주문가 = MakeOrderPrice(Convert.ToInt32(Convert.ToDecimal(dg조건종목.Rows[row].Cells["C3차매수가"].Value.ToString()).ToString("########")));
            //            if (현재가 > 주문가)
            //            {
            //                매수금액 = Int32.Parse(txt3차매수금액.Text.Trim());
            //                수량 = Convert.ToInt32(매수금액 / 주문가);
            //                if (매수금액 < 주문가) { continue; }
            //                SendBuySellMsg(종목코드, "00", 1, 0, (int)주문가, (int)수량, "3");
            //                SystemSleep();
            //            }
            //        }
            //        finally
            //        {
            //            //dg조건종목.Rows[row].Cells["C체크"].Value = false;
            //        }
            //    }
            //}
        }

        public int MakeOrderPrice(int 금액)
        {
            int 주문가 = 0;
            if (금액 < 1000) 주문가 = 금액;
            else if (금액 >= 1000 && 금액 < 10000) 주문가 = Convert.ToInt32(Cls.Left(금액.ToString(), 3) + "0");
            else if (금액 >= 10000 && 금액 < 100000) 주문가 = Convert.ToInt32(Cls.Left(금액.ToString(), 3) + "00");
            else if (금액 >= 100000 && 금액 < 1000000) 주문가 = Convert.ToInt32(Cls.Left(금액.ToString(), 3) + "000");
            else if (금액 >= 1000000 && 금액 < 10000000) 주문가 = Convert.ToInt32(Cls.Left(금액.ToString(), 3) + "0000");

            return 주문가;
        }

        private bool _실시간실행여부;
        private void dg조건리스트_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value.ToString().Trim() == "▶")
                {
                    //for (int row = 0; row < dg조건리스트.RowCount - 1; row++)
                    //{
                    //    if (dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value.ToString().Trim() == "||")
                    //    {
                    //        _실시간실행여부 = true;
                    //        break;
                    //    }
                    //}

                    if (_실시간실행여부 == true)
                    {
                        MessageBox.Show("실행중인 조건검색이 있습니다. 종료하고 실행해 주십시요", "경고!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dg조건종목.RowCount = 1;
                    UcMainStockVer2.SendCondition_OnReceiveConditionVer("9" + dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim()
                        , dg조건리스트.Rows[e.RowIndex].Cells["조건명"].Value.ToString().Trim()
                        , dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim(), 1);

                    dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value = "||";
                    _실시간실행여부 = true;
                }
                else
                {
                    dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value = "▶";
                    UcMainStockVer2.SendConditionStop("9" + dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim()
                        , dg조건리스트.Rows[e.RowIndex].Cells["조건명"].Value.ToString().Trim(),
                        dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim());
                    _실시간실행여부 = false;
                }
            }
        }


        private void SettingAccountData(DataSet ds)
        {
            Decimal 매입가 = 0;
            Decimal 현재가 = 0;
            string 종목코드 = "";
            string 종목명 = "";
            //Decimal 매매가능수량 = 0;
            if (ds == null) return;
            DataRow drReal = ds.Tables[0].Rows[0];
            Decimal temp = 0;

            foreach (DataRow dr보유잔고 in _dt보유잔고.Rows)
            {
                종목코드 = Cls.Right(dr보유잔고["종목번호"].ToString().Trim(), 6);
                종목명 = dr보유잔고["종목명"].ToString().Trim();
                if (drReal["STOCK_CODE"].ToString().Trim() == 종목코드)
                {
                    dr보유잔고["현재가"] = Math.Abs(Decimal.Parse(drReal["현재가"].ToString().Trim())).ToString("###,###,###,##0");
                    매입가 = Decimal.Parse(dr보유잔고["매입가"].ToString().Trim());
                    현재가 = Math.Abs(Decimal.Parse(dr보유잔고["현재가"].ToString().Trim()));
                    dr보유잔고["수익률(%)"] = ((현재가 - 매입가) / 매입가 * 100).ToString("###,###,###,##0.00");
                    dr보유잔고["평가금액"] = (현재가 * Decimal.Parse(dr보유잔고["보유수량"].ToString().Trim())).ToString("###,###,###,##0");
                    //최고체결강도를 UPDATE 해준다. - S

                    Decimal 갭체결강도 = nmStopPwRate.Value;

                    if (_ht최고체결강도[종목코드] != null)
                    {
                        temp = Decimal.Parse(_ht최고체결강도[종목코드].ToString().Trim());
                        if (temp < Decimal.Parse(drReal["체결강도"].ToString().Trim()))
                        {
                            _ht최고체결강도[종목코드] = Decimal.Parse(drReal["체결강도"].ToString().Trim());
                        }
                        else
                        {
                            if (Decimal.Parse(drReal["체결강도"].ToString().Trim()) > 100) 갭체결강도 = 갭체결강도 * 2;
                            if (
                                Decimal.Parse(_ht최고체결강도[종목코드].ToString().Trim()) - Decimal.Parse(drReal["체결강도"].ToString().Trim()) > 갭체결강도
                            )
                            {
                                bool isSell = true;
                                if (Decimal.Parse(dr보유잔고["매매가능수량"].ToString().Trim()) == 0) { isSell = false; }
                                //else if (Decimal.Parse(dr보유잔고["수익률(%)"].ToString().Trim()) >= nm이익실현.Value * (decimal)1.80) { isSell = true; }
                                else
                                {
                                    if (_dsTick60.Tables[종목코드] != null) //잔고 실시간 송신할때 _dsTick60 에 테이블을 만듬 (SetDsScreenNo)
                                    {
                                        DataView dv = new DataView(_dsTick60.Tables[종목코드]);
                                        dv.Sort = "일자 DESC , 시작시간 DESC";
                                        if (dv.Count > 0)
                                        {
                                            if (Convert.ToDouble(dv[0]["COUNT"].ToString()) < 10 || //짧게 잡아야 매도가 빠르다 확인한다.
                                                Convert.ToDouble(dv[0]["매수거래량"].ToString()) > Convert.ToDouble(Math.Abs(Convert.ToInt32(dv[0]["매도거래량"].ToString())) * 0.95)
                                                ) { isSell = false; } //매수량이 더 많은데 구지 매도할 필요없음 매도량의 10% 까지는 본다.
                                        }
                                    }
                                }
                                if (isSell == true)
                                {
                                    ProcessStopLoss(
                                        Decimal.Parse(dr보유잔고["수익률(%)"].ToString().Trim()),
                                        Decimal.Parse(dr보유잔고["현재가"].ToString().Trim()),
                                        종목코드,
                                        종목명,
                                        Decimal.Parse(dr보유잔고["매매가능수량"].ToString().Trim())
                                        );
                                }
                            }
                        }
                    }
                    else
                    {
                        _ht최고체결강도.Add(종목코드, Decimal.Parse(drReal["체결강도"].ToString().Trim()));
                    }
                }
                //최고체결강도를 UPDATE 해준다. - E
            }

        }

        private void ProcessStopLoss(Decimal 수익률, Decimal 현재가, string 종목코드, string 종목명, Decimal 매매가능수량)
        {
            if (종목코드 == "" || 종목명 == "" || 매매가능수량 == 0) return;
            if (btnStopLoss.Text.Trim() == "▶") return;
            if (lsb제외항목.Items.Contains(종목명 + "|" + 종목코드) == true) return;

            Decimal stoplossPer = 0;
            stoplossPer = 수익률;
            Decimal fitPer = nm이익실현.Value;
            Decimal lossPer = nm손절.Value;

            if (chk이익실현.Checked == true)
            {
                if (stoplossPer >= fitPer)
                {
                    SendBuySellMsg(종목코드, "03", (int)ucMainStock.OrderType.신규매도, -1, Convert.ToInt32(현재가), Convert.ToInt32(매매가능수량), "2");
                }
            }
            if (chk손절.Checked == true)
            {
                if (stoplossPer <= lossPer)
                {
                    SendBuySellMsg(종목코드, "00", (int)ucMainStock.OrderType.신규매도, 0, Convert.ToInt32(현재가), Convert.ToInt32(매매가능수량), "2");
                }
            }
        }

        private void SettingConditionStockListDetailData(DataSet ds)
        {
            DataRow dr;
            int row = -1;
            if (ds == null) return;
            dr = ds.Tables[0].Rows[0];
            string 종목코드 = dr["STOCK_CODE"].ToString().Trim();

            var obj = (from DataGridViewRow dgr in dg조건종목.Rows
                    where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                    select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;

                dg조건종목.Rows[row].Cells["C현재가"].Value = Math.Abs(Decimal.Parse(dr["현재가"].ToString().Trim())).ToString("###,###,###,##0");
                dg조건종목.Rows[row].Cells["C등락률"].Value = dr["등락율"].ToString().Trim();
                dg조건종목.Rows[row].Cells["C대비"].Value = dr["전일대비"].ToString().Trim();
                dg조건종목.Rows[row].Cells["C거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString().Trim()).ToString("###,###,###,##0");
                dg조건종목.Rows[row].Cells["C시가"].Value = Math.Abs(Decimal.Parse(dr["시가"].ToString().Trim())).ToString("###,###,###,##0");
                dg조건종목.Rows[row].Cells["C고가"].Value = Math.Abs(Decimal.Parse(dr["고가"].ToString().Trim())).ToString("###,###,###,##0");
                dg조건종목.Rows[row].Cells["C저가"].Value = Math.Abs(Decimal.Parse(dr["저가"].ToString().Trim())).ToString("###,###,###,##0");
            }
        }

        private void SetConditionListVer(DataSet ds)
        {
            if (ds == null) { return; }

            try
            {
                dg조건리스트.RowCount = 1;

                int row = 0;

                foreach (DataRow dr in ds.Tables["CondiList"].Select("CONDI_NAME LIKE 'API%'"))
                {
                    if (dg조건리스트.RowCount - 1 <= row) dg조건리스트.RowCount = dg조건리스트.RowCount + 1;
                    dg조건리스트.Rows[row].Cells["조건명"].Value = dr["CONDI_NAME"].ToString().Trim();
                    dg조건리스트.Rows[row].Cells["번호"].Value = dr["CONDI_SEQ"].ToString().Trim();
                    dg조건리스트.Rows[row].Cells["실행"].Value = "▶";

                    row = row + 1;
                }

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
                            dr[col] = Decimal.Parse(dr[col].ToString().Trim()).ToString("###,###,###,###,###,##0.00");
                        }
                        else
                        {
                            if (col == 3) //수익률이 값이 이상하게 넘어온다.
                            {
                                dr[col] = (Decimal.Parse(dr[col].ToString().Trim()) / 100).ToString("###,###,###,###,###,##0.00");
                            }
                            else
                            {
                                dr[col] = Int32.Parse(dr[col].ToString().Trim()).ToString("###,###,###,###,###,##0");
                            }
                        }
                    }
                }
            }
            return ds;
        }

        //private void UcMainStockVer2_OnDsReceiveChejanData(DataSet ds)
        //{
        //    //Public ChejanFidList(,) As String = {{"9201", "계좌번호"}, {"9203", "주문번호"}, {"9001", "종목코드"}, {"913", "주문상태"}, {"302", "종목명"}, {"900", "주문수량"}, _
        //    //                        {"901", "주문가격"}, {"902", "미체결수량"}, {"903", "체결누계금액"}, {"904", "원주문번호"}, {"905", "주문구분"}, {"906", "매매구분"}, _
        //    //                        {"907", "매도수구분"}, {"908", "주문/체결시간"}, {"909", "체결번호"}, {"910", "체결가"}, {"911", "체결량"}, {"10", "현재가"}, _
        //    //                        {"27", "(최우선)매도호가"}, {"28", "(최우선)매수호가"}, {"914", "단위체결가"}, {"915", "단위체결량"}, {"919", "거부사유"}, _
        //    //                        {"920", "화면번호"}, {"917", "신용구분"}, {"916", "대출일"}, {"930", "보유수량"}, {"931", "매입단가"}, {"932", "총매입가"}, _
        //    //                        {"933", "주문가능수량"}, {"945", "당일순매수수량"}, {"946", "매도/매수구분"}, {"950", "당일총매도손일"}, {"951", "예수금"}, _
        //    //                        {"307", "기준가"}, {"8019", "손익율"}, {"957", "신용금액"}, {"958", "신용이자"}, {"918", "만기일"}, {"990", "당일실현손익(유가)"}, _
        //    //                        {"991", "당일실현손익률(유가)"}, {"992", "당일실현손익(신용)"}, {"993", "당일실현손익률(신용)"}, {"397", "파생상품거래단위"}, _
        //    //                        {"305", "상한가"}, {"306", "하한가"}}
        //    //private string[,] _미체결 = 
        //    //{ {"주문번호" , "System.String"}, {"종목코드" , "System.String"} , {"종목명","System.String"} , {"주문단가" , "System.Int32"}, {"주문수량" , "System.Int32"}, {"미체결수량" , "System.Int32"}, {"주문금액" , "System.Int32"}, {"취소" , "System.Int32"}};

        //    string str = "";
        //    if (ds == null) { return; }
        //    if (ds.Tables.Count < 1) { return; }
        //    if (ds.Tables[0].Rows.Count < 1) { return; }

        //    DataRow dr;
        //    dr = ds.Tables[0].Rows[0];

        //    for (int col = 0; col < ds.Tables[0].Columns.Count - 1; col++)
        //    {
        //        str += ds.Tables[0].Columns[col].ColumnName + " : " + dr[col].ToString().Trim() + " | ";
        //    }

        //    Logger("체결정보" + DateTime.Now.ToString("HH:mm:ss") + " (OnDsReceiveChejanData) ", str);

        //    if (ds.Tables[0].Select("미체결수량 = '0' AND 체결가 <> ''").Length > 0)
        //    {
        //        dr = ds.Tables[0].Select("미체결수량 = '0' AND 체결가 <> ''")[0];
        //    }
        //    else
        //    {
        //        return;
        //    }


        //    string 종목코드 = dr["종목코드"].ToString().Substring(1).Trim();
        //    DataView dv;
        //    dv = new DataView(_dt화면관리);
        //    dv.RowFilter = "종목코드 = '" + 종목코드 + "' AND 실시간구분 = '1' AND 화면구분 = '1'";

        //    if (dv.Count > 0) SettingNewsOrderPrice(종목코드, dr["체결가"].ToString().Trim());

        //    //_frm.UpdateNoti(str);
        //}

        private void SettingNewsOrderPrice(string 종목코드, string 체결가)
        {
            int row = -1;
            var obj = (from DataGridViewRow dgr in dg뉴스체결.Rows
                       where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(종목코드)
                       select dgr).FirstOrDefault();

            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;
                dg뉴스체결.Rows[row].Cells["N주문가"].Value = Decimal.Parse(체결가).ToString("###,###,###,##0");
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
                try
                {
                    dv.RowFilter = "종목코드 LIKE '%" + 종목코드 + "%' AND 실시간구분 = '1' AND 화면구분 = '1'";
                    if (dv.Count > 0) SettingNewFav(ds);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    //TAB 2 조건검색 실시간 종목 처리 - S
                    dv.RowFilter = "종목코드 LIKE '%" + 종목코드 + "%' AND 실시간구분 = '1' AND 화면구분 = '2'";
                    if (dv.Count > 0) SettingAccountData(ds);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    //TAB 3 조건검색 실시간 종목 처리 - S
                    dv.RowFilter = "종목코드 LIKE '%" + 종목코드 + "%' AND 실시간구분 = '1' AND 화면구분 = '3'";
                    if (dv.Count > 0)
                    {
                        //    if (_ds조건체결.Tables[종목코드] != null)
                        //    {
                        //        if (_ds조건체결.Tables[종목코드].Rows.Count > 0)
                        //        {
                        //            dr = _ds조건체결.Tables[종목코드].Rows[0];
                        //        }
                        //        else
                        //        {
                        //            dr = _ds조건체결.Tables[종목코드].NewRow();
                        //        }
                        //        for (int col = 0; col < ds.Tables[0].Columns.Count - 1; col++)
                        //        {
                        //            if (_ds조건체결.Tables[종목코드].Columns.Contains(ds.Tables[0].Columns[col].ColumnName) == false) { continue; }
                        //            dr[ds.Tables[0].Columns[col].ColumnName] = ds.Tables[0].Rows[0][ds.Tables[0].Columns[col].ColumnName];
                        //        }

                        //        string 금일최고거래량;
                        //        string 금일최저거래량;
                        //        string 금일최고체결강도;
                        //        string 금일최저체결강도;
                        //        if (_ds조건체결.Tables[종목코드].Rows.Count > 0)
                        //        {
                        //            //금일최고거래량 = Convert.ToString(_ds조건체결.Tables[종목코드].Compute("MAX(거래량)", String.Empty));
                        //            //금일최저거래량 = Convert.ToString(_ds조건체결.Tables[종목코드].Compute("MIN(거래량)", String.Empty));
                        //            //금일최고체결강도 = Convert.ToString(_ds조건체결.Tables[종목코드].Compute("MAX(체결강도)", String.Empty));
                        //            //금일최저체결강도 = Convert.ToString(_ds조건체결.Tables[종목코드].Compute("MIN(체결강도)", String.Empty));

                        //            금일최고거래량 = dr["최고거래량"].ToString(); ;
                        //            금일최저거래량 = dr["최저거래량"].ToString(); ;
                        //            금일최고체결강도 = dr["최고체결강도"].ToString(); ;
                        //            금일최저체결강도 = dr["최저체결강도"].ToString(); ;
                        //            if (Convert.ToInt32(금일최고거래량) < Convert.ToInt32(dr["거래량"].ToString()))
                        //            {
                        //                금일최고거래량 = dr["거래량"].ToString();
                        //            }
                        //            if (Convert.ToInt32(금일최저거래량) > Convert.ToInt32(dr["거래량"].ToString()))
                        //            {
                        //                금일최저거래량 = dr["거래량"].ToString();
                        //            }
                        //            if (Convert.ToDecimal(금일최고체결강도) < Convert.ToDecimal(dr["체결강도"].ToString()))
                        //            {
                        //                금일최고체결강도 = dr["체결강도"].ToString();
                        //            }
                        //            if (Convert.ToDecimal(금일최저체결강도) > Convert.ToDecimal(dr["체결강도"].ToString()))
                        //            {
                        //                금일최저체결강도 = dr["체결강도"].ToString();
                        //            }
                        //            dr["최고거래량"] = 금일최고거래량;
                        //            dr["최저거래량"] = 금일최저거래량;
                        //            dr["최고체결강도"] = 금일최고체결강도;
                        //            dr["최저체결강도"] = 금일최저체결강도;
                        //        }
                        //        else
                        //        {
                        //            dr["최고거래량"] = dr["거래량"].ToString();
                        //            dr["최저거래량"] = dr["거래량"].ToString();
                        //            dr["최고체결강도"] = dr["체결강도"].ToString();
                        //            dr["최저체결강도"] = dr["체결강도"].ToString();
                        //            _ds조건체결.Tables[종목코드].Rows.Add(dr);
                        //        }
                        //    }
                        //    if (Math.Abs(Convert.ToInt32(_ds조건체결.Tables[종목코드].Rows[0]["최고거래량"].ToString().Trim())) > Math.Abs(Convert.ToInt32(_ds조건체결.Tables[종목코드].Rows[0]["최저거래량"].ToString().Trim())))
                        //    {

                        //    }
                        SettingConditionStockListDetailData(ds);
                    }

                    //TAB 3 조건검색 실시간 종목 처리 - E
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    dv.RowFilter = "종목코드 LIKE '%" + 종목코드 + "%' AND 실시간구분 = '1' AND 화면구분 = '4'";
                    if (dv.Count > 0)
                    {
                        SettingFavData(ds);
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    if (_dsTick60.Tables[종목코드] != null)
                    {
                        SettingTick(ds, _dsTick60.Tables[종목코드]);
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (UcHogaWindowNew1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
                {
                    UcHogaWindowNew1.Property_GetStockTrade = ds;
                }
            }
            else if (ds.Tables[0].Rows[0]["sRealType"].ToString().Trim() == "주식호가잔량")
            {
                if (UcHogaWindowNew1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
                {
                    UcHogaWindowNew1.Property_GetStockHogaJanQty = ds;
                }
            }
            else if (ds.Tables[0].Rows[0]["sRealType"].ToString().Trim() == "주식당일거래원")
            {
                if (UcHogaWindowNew1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
                {
                    UcHogaWindowNew1.Property_ToDayStockTradeAt = ds;
                }
            }
        }

        private void SettingTick(DataSet ds, DataTable dt)
        {
            if (ds == null) return;
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1) return;
            DataRow dr;
            DataRow drReal = ds.Tables[0].Rows[0];

            if (Math.Abs(Int32.Parse(drReal["거래량"].ToString().Trim())) <= 10) return;

            if (dt.Rows.Count == 0)
            {
                dr = dt.Rows.Add();
                dr["일자"] = DateTime.Now.ToString("yyyyMMdd");
                dr["현재가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                dr["시가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                dr["고가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                dr["저가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                dr["등락율"] = drReal["등락율"];
                dr["체결강도"] = drReal["체결강도"];
                if (Int32.Parse(drReal["거래량"].ToString().Trim()) < 0)
                {
                    dr["매수거래량"] = 0;
                    dr["매도거래량"] = drReal["거래량"];
                    dr["매수거래비용"] = 0;
                    dr["매도거래비용"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim())) * Int32.Parse(drReal["거래량"].ToString().Trim());
                }
                else
                {
                    dr["매수거래량"] = drReal["거래량"];
                    dr["매도거래량"] = 0;
                    dr["매수거래비용"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim())) * Int32.Parse(drReal["거래량"].ToString().Trim());
                    dr["매도거래비용"] = 0;
                }
                dr["시작시간"] = drReal["체결시간"];
                dr["종료시간"] = drReal["체결시간"];
                dr["COUNT"] = 1;
                dr["매수유무"] = 0;
                dr["매도유무"] = 0;

            }
            else
            {
                DataRow[] drArr = dt.Select("Convert(COUNT , 'System.Int32') < 30");
                if (drArr.Length < 1)
                {
                    dr = dt.Rows.Add();
                    dr["일자"] = DateTime.Now.ToString("yyyyMMdd");
                    dr["현재가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                    dr["시가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                    dr["고가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                    dr["저가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                    dr["등락율"] = drReal["등락율"];
                    dr["체결강도"] = drReal["체결강도"];
                    if (Int32.Parse(drReal["거래량"].ToString().Trim()) < 0)
                    {
                        dr["매수거래량"] = 0;
                        dr["매도거래량"] = drReal["거래량"];
                        dr["매수거래비용"] = 0;
                        dr["매도거래비용"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim())) * Int32.Parse(drReal["거래량"].ToString().Trim());
                    }
                    else
                    {
                        dr["매수거래량"] = drReal["거래량"];
                        dr["매도거래량"] = 0;
                        dr["매수거래비용"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim())) * Int32.Parse(drReal["거래량"].ToString().Trim());
                        dr["매도거래비용"] = 0;
                    }
                    dr["시작시간"] = drReal["체결시간"];
                    dr["종료시간"] = drReal["체결시간"];
                    dr["COUNT"] = 1;
                    dr["매수유무"] = 0;
                    dr["매도유무"] = 0;
                }
                else
                {
                    dr = drArr[0];
                    dr["현재가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                    if (Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim())) > Math.Abs(Int32.Parse(dr["고가"].ToString().Trim())))
                    {
                        dr["고가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                    }
                    if (Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim())) < Math.Abs(Int32.Parse(dr["저가"].ToString().Trim())))
                    {
                        dr["저가"] = Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim()));
                    }

                    dr["등락율"] = drReal["등락율"];
                    dr["체결강도"] = drReal["체결강도"];
                    if (Int32.Parse(drReal["거래량"].ToString().Trim()) < 0)
                    {
                        dr["매도거래량"] = Int32.Parse(dr["매도거래량"].ToString().Trim()) + Int32.Parse(drReal["거래량"].ToString().Trim());
                        dr["매도거래비용"] = Int32.Parse(dr["매도거래비용"].ToString().Trim()) + Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim())) * Int32.Parse(drReal["거래량"].ToString().Trim());
                    }
                    else
                    {
                        dr["매수거래량"] = Int32.Parse(dr["매수거래량"].ToString().Trim()) + Int32.Parse(drReal["거래량"].ToString().Trim());
                        dr["매수거래비용"] = Int32.Parse(dr["매수거래비용"].ToString().Trim()) + Math.Abs(Int32.Parse(drReal["현재가"].ToString().Trim())) * Int32.Parse(drReal["거래량"].ToString().Trim());
                    }
                    dr["종료시간"] = drReal["체결시간"];
                    dr["COUNT"] = Int32.Parse(dr["COUNT"].ToString().Trim()) + 1;
                }
                SettingTickLine(dr, dt);
            }
        }

        private void SettingTickLine(DataRow dr, DataTable dt)
        {
            Decimal[] sum = { 0, 0, 0, 0, 0 }; // 5 , 10  , 20 , 40 , 60
            Decimal[] avg = { 0, 0, 0, 0, 0 };// 5 , 10  , 20 , 40 , 60

            DataView dv = new DataView(dt);
            dv.Sort = "일자 DESC , 시작시간 DESC";
            Double pwSum = 0;
            if (dv.Count >= 5)
            {
                for (int row = 0; row < 5; row++)
                {

                    sum[0] += Decimal.Parse(dv[row]["현재가"].ToString());
                }
                dr["LINE5"] = (sum[0] / 5).ToString("#########0.00");
            }
            if (dv.Count >= 10)
            {
                for (int row = 0; row < 10; row++)
                {

                    sum[1] += Decimal.Parse(dv[row]["현재가"].ToString());
                }
                dr["LINE10"] = (sum[1] / 10).ToString("#########0.00");
            }
            if (dv.Count >= 20)
            {
                for (int row = 0; row < 20; row++)
                {

                    sum[2] += Decimal.Parse(dv[row]["현재가"].ToString());
                }
                dr["LINE20"] =(sum[2] / 20).ToString("#########0.00");
            }

            if (dv.Count >= 40)
            {
                for (int row = 0; row < 40; row++)
                {
                    sum[3] += Decimal.Parse(dv[row]["현재가"].ToString());
                }
                dr["LINE40"] = (sum[3] / 40).ToString("#########0.00");

                pwSum = 0;
                for (int row = 0; row < 40; row++)
                {
                    pwSum += Math.Pow(Double.Parse((sum[3] / 40).ToString("#########0.00")) - Double.Parse(dv[row]["현재가"].ToString()), 2);
                }
                dr["BBUP"] = (Double.Parse((sum[3] / 40).ToString("#########0.00")) + 2 * Math.Sqrt(pwSum / 40.0)).ToString("#########0.00");
                dr["BBDOWN"] = (Double.Parse((sum[3] / 40).ToString("#########0.00")) - 2 * Math.Sqrt(pwSum / 40.0)).ToString("#########0.00");
            }

            if (dv.Count >= 60)
            {
                for (int row = 0; row < 60; row++)
                {

                    sum[4] += Decimal.Parse(dv[row]["현재가"].ToString());
                }
                dr["LINE60"] = (sum[4] / 60).ToString("#########0.00");
            }


        }

        private void SettingFavData(DataSet ds)
        {
            string 현재가;
            string 시가;
            string 저가;
            string 고가;
            string 종목코드;
            int row = -1;
            if (ds == null) return;
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1) return;
            DataRow dr = ds.Tables[0].Rows[0];
            종목코드 = dr["STOCK_CODE"].ToString().Trim();
            
            var obj = (from DataGridViewRow dgr in dg관종.Rows
                    where dgr.Cells[F_종목코드.Index].Value != null && dgr.Cells[F_종목코드.Index].Value.Equals(종목코드)
                    select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;
                현재가 = Math.Abs(Decimal.Parse(dr["현재가"].ToString())).ToString("###,###,##0").Trim();
                시가 = Math.Abs(Decimal.Parse(dr["시가"].ToString())).ToString("###,###,##0").Trim();
                저가 = Math.Abs(Decimal.Parse(dr["저가"].ToString())).ToString("###,###,##0").Trim();
                고가 = Math.Abs(Decimal.Parse(dr["고가"].ToString())).ToString("###,###,##0").Trim();

                dg관종.Rows[row].Cells["F_현재가"].Value = 현재가;
                dg관종.Rows[row].Cells["F_등락율"].Value = Decimal.Parse(dr["등락율"].ToString()).ToString("###,###,##0.00").Trim();
                dg관종.Rows[row].Cells["F_대비"].Value = Decimal.Parse(dr["전일대비"].ToString()).ToString("###,###,##0").Trim();
                dg관종.Rows[row].Cells["F_거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString()).ToString("###,###,###,###,##0").Trim();
                dg관종.Rows[row].Cells["F_체결강도"].Value = Decimal.Parse(dr["체결강도"].ToString()).ToString("###,###,##0.00").Trim();
                dg관종.Rows[row].Cells["F_시가"].Value = 시가;
                dg관종.Rows[row].Cells["F_저가"].Value = 저가;
                dg관종.Rows[row].Cells["F_고가"].Value = 고가;
                dg관종.Rows[row].Cells["F_시간"].Value = dr["체결시간"].ToString();
                
            }
        }

        private void SettingOPT10006(DataSet ds)
        {
            DataView dv = new DataView(ds.Tables[0]);
            Decimal 조건체결강도 = Decimal.Parse(nm조건체결강도.Value.ToString().Trim());
            //string 조건시간  = DateTime.Now.AddMilliseconds(-2000).ToString("HHmmss");

            string 현재가 = "";
            if (dv.Count < 1) return;
            string 종목코드 = dv[0]["종목코드"].ToString().Trim();
            string 시가 = Math.Abs(Convert.ToInt32(dv[0]["시가"].ToString().Trim())).ToString();
            string 고가 = Math.Abs(Convert.ToInt32(dv[0]["고가"].ToString().Trim())).ToString();
            string 저가 = Math.Abs(Convert.ToInt32(dv[0]["저가"].ToString().Trim())).ToString();
            string 대비 = Convert.ToInt32(dv[0]["대비"].ToString().Trim()).ToString();

            현재가 = dv[0]["종가"].ToString().Trim();
            string 등락률 = dv[0]["등락률"].ToString().Trim();
            string 현재거래량 = dv[0]["거래량"].ToString().Trim();
            string 현재거래대금 = dv[0]["거래대금"].ToString().Trim();
            string 현재체결강도 = dv[0]["체결강도"].ToString().Trim();
            //int 주문가 = (int)Decimal.Parse(현재가);
            //int 주문가 = 999999999;
            //int 매수금액 = (int)Decimal.Parse(txt매수금액.Text);
            //int 매수수량 = Math.Abs(매수금액 / 주문가);

            //string 거래구분 = "";
            //if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:20:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:40:00")))
            //{
            //    거래구분 = "61";
            //}
            //else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:00:00")))
            //{
            //    거래구분 = "81";
            //}
            //else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:00:00")) && DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 18:00:00")))
            //{
            //    거래구분 = "62";
            //}

            //if (거래구분 != "")
            //{
            //    //시장가 매수 체결된 정보의 주문가랑 조금은 틀릴수 있음 -- 정보요청을 덜하기 위한 수단임
            //    DataView dvDart = new DataView(_dtDart);
            //    dvDart.RowFilter = String.Format("종목코드 = '{0}'", 종목코드);
            //    dvDart.Sort = "시간 DESC";
            //    if (dvDart.Count > 0)
            //    {
            //        if (dvDart[0]["제목"].ToString().IndexOf("3자배정") > -1 && dvDart[0]["제목"].ToString().IndexOf("자율") < 0)
            //        {
            //            SendBuySellMsg(종목코드, 거래구분, 1, 0, 주문가, 매수수량, "1");
            //        }
            //    }
            //}


            //TAB 0 처리 - S
            //for (int row = 0; row < dgN관종.RowCount - 1; row++)
            //{
            //    if (dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim() == 종목코드)
            //    {
            //        //최초등록되자말자 10 이상 차이? 바로매수 할까? - E
            //        //if (Decimal.Parse(dv[0]["체결강도"].ToString().Trim()) - Decimal.Parse(dv[dv.Count - 1]["체결강도"].ToString().Trim()) > 10) 
            //        //{
            //        //    //시장가 매수 체결된 정보의 주문가랑 조금은 틀릴수 있음 -- 정보요청을 덜하기 위한 수단임
            //        //    SendBuySellMsg(종목코드, "03", 1, 0, 주문가, 매수수량 , "1");
            //        //    if (Cls.isExistsRow(dg뉴스체결, 0, 종목코드) == false)
            //        //    {
            //        //        DataView dv1 = new DataView(UcMainStockVer2._allStockDataset.Tables[0]);
            //        //        dv1.RowFilter = "STOCK_CODE = '" + 종목코드 + "'";


            //        //        dg뉴스체결.Rows.Insert(0, 1);
            //        //        dg뉴스체결.Rows[0].Cells["N종목코드"].Value = 종목코드;

            //        //        if (dv1.Count > 0) { dg뉴스체결.Rows[0].Cells["N종목명"].Value = dv1[0]["STOCK_NAME"].ToString().Trim(); }
            //        //        else { dg뉴스체결.Rows[0].Cells["N종목명"].Value = ""; }

            //        //        dg뉴스체결.Rows[0].Cells["N주문가"].Value = 주문가.ToString("###,###,###,##0").Trim();
            //        //        dg뉴스체결.Rows[0].Cells["N현재가"].Value = 주문가.ToString("###,###,###,##0").Trim();
            //        //        dg뉴스체결.Rows[0].Cells["N수익률"].Value = "0.00";

            //        //        dg뉴스체결.Rows[0].Cells["N거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
            //        //        dg뉴스체결.Rows[0].Cells["N거래대금"].Value = (Math.Floor(Decimal.Parse(현재거래대금) / 1000000)).ToString("###,###,###,##0");
            //        //        dg뉴스체결.Rows[0].Cells["N체결강도"].Value = 현재체결강도.ToString().Trim();
            //        //        dg뉴스체결.Rows[0].Cells["N매수수량"].Value = 매수수량.ToString("###,###,###,##0");

            //        //        SetDsScreenNo("D", "1", "1", 종목코드, "", false);
            //        //        dgN관종.Rows.RemoveAt(row);
            //        //        Application.DoEvents();

            //        //        continue;
            //        //    }
            //        //}
            //        //최초등록되자말자 10 이상 차이? 바로매수 할까? - E

            //        //dgN관종.Rows[row].Cells["P현재가"].Value = Math.Abs(Decimal.Parse(현재가)).ToString("###,###,###,##0");
            //        dgN관종.Rows[row].Cells["P등락률"].Value = 등락률;
            //        dgN관종.Rows[row].Cells["P체결강도"].Value = 현재체결강도;
            //        dgN관종.Rows[row].Cells["P거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
            //        //dgN관종.Rows[row].Cells["P거래대금"].Value = (Math.Floor(Decimal.Parse(현재거래대금) / 1000000)).ToString("###,###,###,##0");
            //        dgN관종.Rows[row].Cells["P거래대금"].Value = Int32.Parse(현재거래대금).ToString("###,###,###,##0");
            //        if (Decimal.Parse(현재체결강도) > 0)
            //        {
            //            dgN관종.Rows[row].Cells["P최초등락률"].Value = 등락률;
            //            dgN관종.Rows[row].Cells["P최초체결강도"].Value = 현재체결강도;
            //            dgN관종.Rows[row].Cells["P최초거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
            //            //dgN관종.Rows[row].Cells["P최초거래대금"].Value = (Math.Floor(Decimal.Parse(현재거래대금) / 1000000)).ToString("###,###,###,##0");
            //            dgN관종.Rows[row].Cells["P최초거래대금"].Value = Int32.Parse(현재거래대금).ToString("###,###,###,##0");
            //            dgN관종.Rows[row].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //        }

            //        //체결 30번전 체결강도랑 비교를 할까말까?
            //        //if (Decimal.Parse(dv[0]["체결강도"].ToString().Trim()) < Decimal.Parse(dv[dv.Count - 1]["체결강도"].ToString().Trim())) {
            //        //    //나가리???
            //        //    dgN관종.Rows.RemoveAt(row);
            //        //    Application.DoEvents();
            //        //}
            //    }
            //}
            //TAB 0 처리 - E

            //TAB 3 처리 - S
            if (tbStockList.SelectedIndex == 2)
            {

                for (int row = 0; row < dg조건종목.RowCount - 1; row++)
                {
                    if (dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString() == 종목코드)
                    {
                        //dg조건종목.Rows[row].Cells["C현재가"].Value = Math.Abs(Decimal.Parse(현재가)).ToString("###,###,###,##0");


                        //KIWOOM API 에서 종가를 안 던져 준다 이런 미친 그래서 DB 에서 읽어와서 현재가를 계산함(장중에는 필요없음 장전 장후 조회시 필요) - S
                        if (dg조건종목.Rows[row].Cells["C현재가"].Value.ToString().IndexOf("|") > -1)
                        {
                            string 최근종가 = dg조건종목.Rows[row].Cells["C현재가"].Value.ToString().Split('|')[0];
                            string 최근일 = dg조건종목.Rows[row].Cells["C현재가"].Value.ToString().Split('|')[1];

                            if (Cls.GetWeekOfDay(DateTime.Now) == "토" || Cls.GetWeekOfDay(DateTime.Now) == "일")
                            {
                                dg조건종목.Rows[row].Cells["C현재가"].Value = 최근종가;//제일 최근의 전일종가를 가져온다.
                            }
                            else
                            {
                                if (DateTime.Now.ToString("yyyyMMdd") == 최근일)
                                {
                                    dg조건종목.Rows[row].Cells["C현재가"].Value = Convert.ToInt32(최근종가).ToString("###,###,###,##0");
                                }
                                else
                                {
                                    dg조건종목.Rows[row].Cells["C현재가"].Value = (Convert.ToInt32(최근종가) + Convert.ToInt32(대비)).ToString("###,###,###,##0");
                                }
                            }
                        }
                        else
                        {
                            //dg조건종목.Rows[row].Cells["C현재가"].Value = "";
                        }
                        //KIWOOM API 에서 종가를 안 던져 준다 이런 미친 그래서 DB 에서 읽어와서 현재가를 계산함(장중에는 필요없음 장전 장후 조회시 필요) - E

                        dg조건종목.Rows[row].Cells["C대비"].Value = 대비;
                        dg조건종목.Rows[row].Cells["C등락률"].Value = 등락률;
                        dg조건종목.Rows[row].Cells["C거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C시가"].Value = Decimal.Parse(시가).ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C고가"].Value = Decimal.Parse(고가).ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C저가"].Value = Decimal.Parse(저가).ToString("###,###,###,##0");
                    }
                }
            }
            //TAB 3 처리 - E

            //TAB 4 처리 - S
            //if (tbStockList.SelectedIndex == 3)
            //{
            //    for (int row = 0; row < dg관종.RowCount - 1; row++)
            //    {
            //        if (dg관종.Rows[row].Cells["F_종목코드"].Value.ToString() == 종목코드)
            //        {
            //            //dg관종.Rows[row].Cells["F_현재가"].Value = Math.Abs(Decimal.Parse(현재가)).ToString("###,###,###,##0");
            //            dg관종.Rows[row].Cells["F_등락율"].Value = 등락률;
            //            dg관종.Rows[row].Cells["F_거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
            //            dg관종.Rows[row].Cells["F_시가"].Value = Decimal.Parse(시가).ToString("###,###,###,##0");
            //            dg관종.Rows[row].Cells["F_고가"].Value = Decimal.Parse(고가).ToString("###,###,###,##0");
            //            dg관종.Rows[row].Cells["F_저가"].Value = Decimal.Parse(저가).ToString("###,###,###,##0");
            //            dg관종.Rows[row].Cells["F_체결강도"].Value = Decimal.Parse(현재체결강도).ToString("###,###,###,##0.00");
            //            dg관종.Rows[row].Cells["F_대비"].Value = Decimal.Parse(대비).ToString("###,###,###,##0");
            //            dg관종.Rows[row].Cells["F_시간"].Value = DateTime.Now.ToString("HHmmss");
            //            Application.DoEvents();

            //        }
            //    }
            //}
            //TAB 4 처리 - E
        }

        private void SettingOPT10001(DataSet ds)
        {
            DataView dv = new DataView(ds.Tables[0]);
            if (dv.Count < 1) return;
            DataRowView dr = dv[0];

            string 종목코드 = dr["종목코드"].ToString().Trim();
            Decimal 등락률 = Convert.ToDecimal(dr["등락율"].ToString().Trim());
            int 거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
            //int 거래대금 = Convert.ToInt32(dr["거래대금"].ToString().Trim());  //단위 백만원
            int 현재가 = Math.Abs(Convert.ToInt32(dr["현재가"].ToString().Trim()));
            int 고가 = Math.Abs(Convert.ToInt32(dr["고가"].ToString().Trim()));
            int 시가 = Math.Abs(Convert.ToInt32(dr["시가"].ToString().Trim()));
            int 저가 = Math.Abs(Convert.ToInt32(dr["저가"].ToString().Trim()));
            int 대비 = Convert.ToInt32(dr["전일대비"].ToString().Trim());
            int 주문가 = 현재가;
            int 매수금액 = Int32.Parse(nm공시매수금액.Value.ToString());
            int 매수수량 = 매수금액 / 주문가;

            //장전후 거래 - S            
            string 거래구분 = "";
            if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:20:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:40:00")))
            {
                거래구분 = "61";
            }
            else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:00:00")))
            {
                거래구분 = "81";
            }
            else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:00:00")) && DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 18:00:00")))
            {
                거래구분 = "62";
            }
            else
            {
                거래구분 = "03";
            }

            //시장가 매수 체결된 정보의 주문가랑 조금은 틀릴수 있음 -- 정보요청을 덜하기 위한 수단임
            DataView dvDart = new DataView(_dtDart);
            dvDart.RowFilter = String.Format("종목코드 = '{0}' AND 제목 LIKE '%3자배정%'", 종목코드);
            dvDart.Sort = "시간 DESC";
            if (거래구분 != "")
            {
                if (dvDart.Count > 0)
                {
                    if (dvDart[0]["제목"].ToString().IndexOf("자율") < 0 && dvDart[0]["제목"].ToString().IndexOf("정정") < 0 && dvDart[0]["제목"].ToString().IndexOf("추가상장") < 0)
                    {
                        SendBuySellMsg(종목코드, 거래구분, 1, 0, 주문가, 매수수량, "1");
                    }
                }
            }
            //장전후 거래 - E

            //TAB 0 처리 - S
            for (int row = 0; row < dgN관종.RowCount - 1; row++)
            {
                if (dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim() == 종목코드)
                {
                    dgN관종.Rows[row].Cells["P현재가"].Value = 현재가.ToString("###,###,###,##0");
                    dgN관종.Rows[row].Cells["P등락률"].Value = 등락률;
                    dgN관종.Rows[row].Cells["P체결강도"].Value = ""; //주식시세표요청에서는 체결강도를 넘겨주지 않는다.
                    dgN관종.Rows[row].Cells["P거래량"].Value = 거래량.ToString("###,###,###,##0");
                    //dgN관종.Rows[row].Cells["P거래대금"].Value = 거래대금.ToString("###,###,###,##0");
                    dgN관종.Rows[row].Cells["P최초등락률"].Value = 등락률;
                    dgN관종.Rows[row].Cells["P최초체결강도"].Value = "";//주식시세표요청에서는 체결강도를 넘겨주지 않는다.
                    dgN관종.Rows[row].Cells["P최초거래량"].Value = 거래량.ToString("###,###,###,##0");
                    //dgN관종.Rows[row].Cells["P최초거래대금"].Value = 거래대금.ToString("###,###,###,##0");
                    //dgN관종.Rows[row].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            //TAB 0 처리 - E

            //TAB 2 처리 - S
            if (tbStockList.SelectedIndex == 2)
            {
                for (int row = 0; row < dg조건종목.RowCount - 1; row++)
                {
                    if (dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString().Trim() == 종목코드)
                    {
                        dg조건종목.Rows[row].Cells["C현재가"].Value = 현재가.ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C대비"].Value = 대비;
                        dg조건종목.Rows[row].Cells["C등락률"].Value = 등락률;
                        dg조건종목.Rows[row].Cells["C거래량"].Value = 거래량.ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C시가"].Value = 시가.ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C고가"].Value = 고가.ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C저가"].Value = 저가.ToString("###,###,###,##0");
                    }
                }
            }
            //TAB 2 처리 - E

            //TAB 3 관심종목 처리 -S
            if (tbStockList.SelectedIndex == 3)
            {
                for (int row = 0; row < dg관종.RowCount - 1; row++)
                {
                    if (dg관종.Rows[row].Cells["F_종목코드"].Value.ToString() == 종목코드)
                    {
                        dg관종.Rows[row].Cells["F_현재가"].Value = 현재가.ToString("###,###,###,##0");
                        dg관종.Rows[row].Cells["F_등락율"].Value = 등락률;
                        dg관종.Rows[row].Cells["F_거래량"].Value = 거래량.ToString("###,###,###,##0");
                        dg관종.Rows[row].Cells["F_시가"].Value = 시가.ToString("###,###,###,##0");
                        dg관종.Rows[row].Cells["F_고가"].Value = 고가.ToString("###,###,###,##0");
                        dg관종.Rows[row].Cells["F_저가"].Value = 저가.ToString("###,###,###,##0");
                        dg관종.Rows[row].Cells["F_체결강도"].Value = "0";
                        dg관종.Rows[row].Cells["F_대비"].Value = 대비.ToString("###,###,###,##0");
                        dg관종.Rows[row].Cells["F_시간"].Value = DateTime.Now.ToString("HH:mm:ss");
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
            Decimal 등락률 = Convert.ToDecimal(dr["등락률"].ToString().Trim());
            int 거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
            int 거래대금 = Convert.ToInt32(dr["거래대금"].ToString().Trim());  //단위 백만원
            int 현재가 = Math.Abs(Convert.ToInt32(dr["현재가"].ToString().Trim()));
            int 고가 = Math.Abs(Convert.ToInt32(dr["고가"].ToString().Trim()));
            int 시가 = Math.Abs(Convert.ToInt32(dr["시가"].ToString().Trim()));
            int 저가 = Math.Abs(Convert.ToInt32(dr["저가"].ToString().Trim()));
            int 대비 = 현재가 - Convert.ToInt32(dr["전일종가"].ToString().Trim());
            int 주문가 = 현재가;
            int 매수금액 = Int32.Parse(nm공시매수금액.Value.ToString());
            int 매수수량 = 매수금액 / 주문가;

            //장전후 거래 - S            
            string 거래구분 = "";
            if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:20:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:40:00")))
            {
                거래구분 = "61";
            }
            else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:00:00")))
            {
                거래구분 = "81";
            }
            else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:00:00")) && DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 18:00:00")))
            {
                거래구분 = "62";
            }
            else
            {
                거래구분 = "03";
            }

            //시장가 매수 체결된 정보의 주문가랑 조금은 틀릴수 있음 -- 정보요청을 덜하기 위한 수단임
            DataView dvDart = new DataView(_dtDart);
            dvDart.RowFilter = String.Format("종목코드 = '{0}' AND 제목 LIKE '%3자배정%'", 종목코드);
            dvDart.Sort = "시간 DESC";
            if (거래구분 != "")
            {
                if (dvDart.Count > 0)
                {
                    if (dvDart[0]["제목"].ToString().IndexOf("자율") < 0 && dvDart[0]["제목"].ToString().IndexOf("정정") < 0 && dvDart[0]["제목"].ToString().IndexOf("추가상장") < 0)
                    {
                        SendBuySellMsg(종목코드, 거래구분, 1, 0, 주문가, 매수수량, "1");
                    }
                }
            }
            //장전후 거래 - E

            //TAB 0 처리 - S
            var obj = (from DataGridViewRow dgr in dgN관종.Rows
                         where dgr.Cells[P종목코드.Index].Value != null && dgr.Cells[P종목코드.Index].Value.Equals(종목코드)
                         select dgr).FirstOrDefault();

            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;
                dgN관종.Rows[row].Cells["P현재가"].Value = 현재가.ToString("###,###,###,##0");
                dgN관종.Rows[row].Cells["P등락률"].Value = 등락률;
                dgN관종.Rows[row].Cells["P체결강도"].Value = ""; //주식시세표요청에서는 체결강도를 넘겨주지 않는다.
                dgN관종.Rows[row].Cells["P거래량"].Value = 거래량.ToString("###,###,###,##0");
                dgN관종.Rows[row].Cells["P거래대금"].Value = 거래대금.ToString("###,###,###,##0");
                dgN관종.Rows[row].Cells["P최초등락률"].Value = 등락률;
                dgN관종.Rows[row].Cells["P최초체결강도"].Value = "";//주식시세표요청에서는 체결강도를 넘겨주지 않는다.
                dgN관종.Rows[row].Cells["P최초거래량"].Value = 거래량.ToString("###,###,###,##0");
                dgN관종.Rows[row].Cells["P최초거래대금"].Value = 거래대금.ToString("###,###,###,##0");
            }
            //dgN관종.Rows[row].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //for (int row = 0; row < dgN관종.RowCount - 1; row++)
            //{
            //    if (dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim() == 종목코드)
            //    {
            //        dgN관종.Rows[row].Cells["P현재가"].Value = 현재가.ToString("###,###,###,##0");
            //        dgN관종.Rows[row].Cells["P등락률"].Value = 등락률;
            //        dgN관종.Rows[row].Cells["P체결강도"].Value = ""; //주식시세표요청에서는 체결강도를 넘겨주지 않는다.
            //        dgN관종.Rows[row].Cells["P거래량"].Value = 거래량.ToString("###,###,###,##0");
            //        dgN관종.Rows[row].Cells["P거래대금"].Value = 거래대금.ToString("###,###,###,##0");
            //        dgN관종.Rows[row].Cells["P최초등락률"].Value = 등락률;
            //        dgN관종.Rows[row].Cells["P최초체결강도"].Value = "";//주식시세표요청에서는 체결강도를 넘겨주지 않는다.
            //        dgN관종.Rows[row].Cells["P최초거래량"].Value = 거래량.ToString("###,###,###,##0");
            //        dgN관종.Rows[row].Cells["P최초거래대금"].Value = 거래대금.ToString("###,###,###,##0");
            //        //dgN관종.Rows[row].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //        break;
            //    }
            //}
            //TAB 0 처리 - E

            //TAB 2 처리 - S
            if (tbStockList.SelectedIndex == 2)
            {
                obj = (from DataGridViewRow dgr in dg조건종목.Rows
                      where dgr.Cells[C종목코드.Index].Value != null && dgr.Cells[C종목코드.Index].Value.Equals(종목코드)
                      select dgr).FirstOrDefault();
                if (obj != null)
                {
                    row = ((DataGridViewRow)obj).Index;
                    dg조건종목.Rows[row].Cells["C현재가"].Value = 현재가.ToString("###,###,###,##0");
                    dg조건종목.Rows[row].Cells["C대비"].Value = 대비;
                    dg조건종목.Rows[row].Cells["C등락률"].Value = 등락률;
                    dg조건종목.Rows[row].Cells["C거래량"].Value = 거래량.ToString("###,###,###,##0");
                    dg조건종목.Rows[row].Cells["C시가"].Value = 시가.ToString("###,###,###,##0");
                    dg조건종목.Rows[row].Cells["C고가"].Value = 고가.ToString("###,###,###,##0");
                    dg조건종목.Rows[row].Cells["C저가"].Value = 저가.ToString("###,###,###,##0");
                }
                //for (int row = 0; row < dg조건종목.RowCount - 1; row++)
                //{
                //    if (dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString().Trim() == 종목코드)
                //    {
                //        dg조건종목.Rows[row].Cells["C현재가"].Value = 현재가.ToString("###,###,###,##0");
                //        dg조건종목.Rows[row].Cells["C대비"].Value = 대비;
                //        dg조건종목.Rows[row].Cells["C등락률"].Value = 등락률;
                //        dg조건종목.Rows[row].Cells["C거래량"].Value = 거래량.ToString("###,###,###,##0");
                //        dg조건종목.Rows[row].Cells["C시가"].Value = 시가.ToString("###,###,###,##0");
                //        dg조건종목.Rows[row].Cells["C고가"].Value = 고가.ToString("###,###,###,##0");
                //        dg조건종목.Rows[row].Cells["C저가"].Value = 저가.ToString("###,###,###,##0");
                //        break;
                //    }
                //}
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

                    dg관종.Rows[row].Cells["F_현재가"].Value = 현재가.ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_등락율"].Value = 등락률;
                    dg관종.Rows[row].Cells["F_거래량"].Value = 거래량.ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_시가"].Value = 시가.ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_고가"].Value = 고가.ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_저가"].Value = 저가.ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_체결강도"].Value = "0";
                    dg관종.Rows[row].Cells["F_대비"].Value = 대비.ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_시간"].Value = DateTime.Now.ToString("HH:mm:ss");
                }
                //for (int row = 0; row < dg관종.RowCount - 1; row++)
                //{
                //    if (dg관종.Rows[row].Cells["F_종목코드"].Value.ToString() == 종목코드)
                //    {
                //        dg관종.Rows[row].Cells["F_현재가"].Value = 현재가.ToString("###,###,###,##0");
                //        dg관종.Rows[row].Cells["F_등락율"].Value = 등락률;
                //        dg관종.Rows[row].Cells["F_거래량"].Value = 거래량.ToString("###,###,###,##0");
                //        dg관종.Rows[row].Cells["F_시가"].Value = 시가.ToString("###,###,###,##0");
                //        dg관종.Rows[row].Cells["F_고가"].Value = 고가.ToString("###,###,###,##0");
                //        dg관종.Rows[row].Cells["F_저가"].Value = 저가.ToString("###,###,###,##0");
                //        dg관종.Rows[row].Cells["F_체결강도"].Value = "0";
                //        dg관종.Rows[row].Cells["F_대비"].Value = 대비.ToString("###,###,###,##0");
                //        dg관종.Rows[row].Cells["F_시간"].Value = DateTime.Now.ToString("HH:mm:ss");
                //        break;
                //    }
                //}
            }
            //TAB 3 관심종목 처리 -E
        }

        private DataTable SetLine(string 종목코드)
        {
            DataSet ds;
            DataTable dt = new DataTable();
            DataRow dr;
            DataRow[] drArr;
            drArr = _ds관종일봉.Tables[0].Select(String.Format("STOCK_CODE = '{0}'", 종목코드));
            if (drArr.Length < 1)
            {
                ds = _DataAcc.p_stock_day_data_query_Line2("1", 종목코드, true, null, null);
                ds.Tables[0].Columns.Add("B_UP");
                ds.Tables[0].Columns.Add("B_DOWN");

                if (ds.Tables.Count > 1)
                {
                    if (ds.Tables[0].Rows.Count > 0) { 
                        ds.Tables[0].Rows[0]["B_UP"] = ds.Tables[1].Rows[0]["B_LINEUP"].ToString().Trim();
                        ds.Tables[0].Rows[0]["B_DOWN"] = ds.Tables[1].Rows[0]["B_LINEDOWN"].ToString().Trim();
                    }
                }
            }
            else
            {
                ds = new DataSet();

                dt.Columns.Add("line3", Type.GetType("System.String"));
                dt.Columns.Add("line5", Type.GetType("System.String"));
                dt.Columns.Add("line10", Type.GetType("System.String"));
                dt.Columns.Add("line15", Type.GetType("System.String"));
                dt.Columns.Add("line20", Type.GetType("System.String"));
                dt.Columns.Add("line40", Type.GetType("System.String"));
                dt.Columns.Add("line60", Type.GetType("System.String"));
                dt.Columns.Add("MA_H", Type.GetType("System.String"));
                dt.Columns.Add("MA_C", Type.GetType("System.String"));
                dt.Columns.Add("MA_L", Type.GetType("System.String"));
                dt.Columns.Add("B_UP", Type.GetType("System.String"));
                dt.Columns.Add("B_DOWN", Type.GetType("System.String"));

                dr = dt.Rows.Add();

                if (drArr.Length > 2)
                    dr["line3"] = _ds관종일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[1]["STOCK_DATE"].ToString()));
                else
                    dr["line3"] = "";
                if (drArr.Length > 4)
                    dr["line5"] = _ds관종일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[3]["STOCK_DATE"].ToString()));
                else
                    dr["line5"] = "";

                if (drArr.Length > 9)
                    dr["line10"] = _ds관종일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[8]["STOCK_DATE"].ToString()));
                else
                    dr["line10"] = "";

                if (drArr.Length > 14)
                    dr["line15"] = _ds관종일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[13]["STOCK_DATE"].ToString()));
                else
                    dr["line15"] = "";

                if (drArr.Length > 19)
                    dr["line20"] = _ds관종일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[18]["STOCK_DATE"].ToString()));
                else
                    dr["line20"] = "";

                if (drArr.Length > 39)
                    dr["line40"] = _ds관종일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[38]["STOCK_DATE"].ToString()));
                else
                    dr["line40"] = "";

                if (drArr.Length > 59)
                    dr["line60"] = _ds관종일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[58]["STOCK_DATE"].ToString()));
                else
                    dr["line60"] = "";

                dr["MA_H"] = _ds관종일봉.Tables[0].Compute("SUM(H_LOWEND_MA)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[0]["STOCK_DATE"].ToString()));
                dr["MA_L"] = _ds관종일봉.Tables[0].Compute("SUM(LOWEND_MA)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[0]["STOCK_DATE"].ToString()));
                if (Cls.IsNumeric(dr["MA_H"].ToString()) == true)
                {
                    dr["MA_C"] = (Double.Parse(dr["MA_H"].ToString()) + Double.Parse(dr["MA_L"].ToString())) / 2.0;
                }
                else
                {
                    dr["MA_C"] = "";
                }


                if (drArr.Length >= 41)
                {
                    Double tempBB = 0;
                    Double ma = Convert.ToDouble(_ds관종일봉.Tables[0].Compute("SUM(END_PRICE)", String.Format("STOCK_CODE = '{0}' AND STOCK_DATE >= '{1}'", 종목코드, drArr[40]["STOCK_DATE"].ToString())).ToString()) / 41;
                    DataRow drBB;
                    for (int row = 0; row < 41; row++)
                    {
                        drBB = drArr[row];
                        tempBB += Math.Pow(ma - Double.Parse(drBB["END_PRICE"].ToString()), 2);
                    }
                    dr["B_UP"] = ma + 2 * Math.Sqrt(tempBB / 41);
                    dr["B_DOWN"] = ma - 2 * Math.Sqrt(tempBB / 41);
                }
                else
                {
                    dr["B_UP"] = "";
                    dr["B_DOWN"] = "";
                }

                ds.Tables.Add(dt);
            }
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

                dr = dt.Rows.Add();

                DataRow[] drArr = _ds관종재정.Tables[0].Select(String.Format("STOCK_CODE = '{0}'", 종목코드));
                if (drArr.Length < 1) return null;
                drFinance = drArr[0];

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

                ds.Tables.Add(dt);
            //}
            return ds.Tables[0];
        }

        //DataTable 사용시 int minLavel = Convert.ToInt32(dt.Compute("min(AccountLevel)", string.Empty));
        Hashtable _ht최고체결강도 = new Hashtable();

        //TAB 0 실시간데이터 처리 부분 - S
        private void SetNewsDs(DataRow RealDr)
        {
            DataTable dt = _ds뉴스.Tables[RealDr["STOCK_CODE"].ToString().Trim()];
            DataRow dr;
            if (dt == null) return;
            //private string[] _뉴스 = { "현재가", "등락율", "체결강도", "매수거래량", "매도거래량", "매수거래비용", "매도거래비용" , "거래시간"};

            if (dt.Rows.Count < 1)
            {
                dr = dt.Rows.Add();
                if (Convert.ToInt32(RealDr["거래량"].ToString().Trim()) > 0)
                {
                    dr["매수거래량"] = RealDr["거래량"];
                    dr["매수거래비용"] = Math.Abs(Convert.ToInt32(RealDr["현재가"].ToString().Trim())) * Convert.ToInt32(RealDr["거래량"].ToString().Trim());
                    dr["매도거래량"] = 0;
                    dr["매도거래비용"] = 0;
                }
                else
                {
                    dr["매수거래량"] = 0;
                    dr["매수거래비용"] = 0;
                    dr["매도거래량"] = RealDr["거래량"];
                    dr["매도거래비용"] = Math.Abs(Convert.ToInt32(RealDr["현재가"].ToString().Trim())) * Convert.ToInt32(RealDr["거래량"].ToString().Trim());
                }
            }
            else
            {
                dr = dt.Rows[0];
                if (Convert.ToInt32(RealDr["거래량"].ToString().Trim()) > 0)
                {
                    dr["매수거래량"] = Convert.ToInt32(dr["매수거래량"].ToString().Trim()) + Convert.ToInt32(RealDr["거래량"].ToString().Trim());
                    dr["매수거래비용"] = Convert.ToInt32(dr["매수거래비용"].ToString().Trim()) + (
                        Math.Abs(Convert.ToInt32(RealDr["현재가"].ToString().Trim())) * Convert.ToInt32(RealDr["거래량"].ToString().Trim())
                        );
                }
                else
                {
                    dr["매도거래량"] = Convert.ToInt32(dr["매도거래량"].ToString().Trim()) + Convert.ToInt32(RealDr["거래량"].ToString().Trim());
                    dr["매도거래비용"] = Convert.ToInt32(dr["매도거래비용"].ToString().Trim()) + (
                         Math.Abs(Convert.ToInt32(RealDr["현재가"].ToString().Trim())) * Convert.ToInt32(RealDr["거래량"].ToString().Trim())
                        );
                }
            }

            dr["현재가"] = RealDr["현재가"];
            dr["등락율"] = RealDr["등락율"];
            dr["체결강도"] = RealDr["체결강도"];
            dr["거래시간"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        }

        private void SettingNewFav(DataSet ds)
        {
            
            DataRow drNews;
            Boolean isBuy;
            DataView dvTick;
            Decimal avgPwRate;
            DataRow dr = ds.Tables[0].Rows[0];

            string 종목코드 = dr["STOCK_CODE"].ToString().Trim();
            int 현재가 = Math.Abs(Int32.Parse(dr["현재가"].ToString().Trim()));
            int 주문가;
            Decimal 최초체결강도;
            Decimal 현재체결강도;
            Decimal 최초거래대금;
            Decimal 현재거래대금;
            Decimal 최초거래량;
            Decimal 현재거래량;
            Decimal 조건체결강도;
            Decimal 조건거래대금;


            int 매수매도거래비용;
            string 모니터링구분 = "";
            DateTime 최초거래시간;
            TimeSpan ts;
            int diffSecond;
            Decimal 조건시간;
            int 매수수량;
            int 매수금액;
            int row = -1;

            현재체결강도 = Decimal.Parse(dr["체결강도"].ToString().Trim());
            현재거래대금 = Decimal.Parse(dr["누적거래대금"].ToString().Trim());
            현재거래량 = Decimal.Parse(dr["누적거래량"].ToString().Trim());

            var obj = (from DataGridViewRow dgr in dgN관종.Rows
                    where dgr.Cells[P종목코드.Index].Value != null && dgr.Cells[P종목코드.Index].Value.Equals(종목코드)
                    select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;

                SetNewsDs(dr);

                dgN관종.Rows[row].Cells["P현재가"].Value = Math.Abs(현재가).ToString("###,###,###,##0");
                dgN관종.Rows[row].Cells["P등락률"].Value = dr["등락율"].ToString().Trim();
                dgN관종.Rows[row].Cells["P체결강도"].Value = Decimal.Parse(dr["체결강도"].ToString().Trim()).ToString("###,###,###,##0.00");
                dgN관종.Rows[row].Cells["P거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString().Trim()).ToString("###,###,###,##0");
                dgN관종.Rows[row].Cells["P거래대금"].Value = Decimal.Parse(dr["누적거래대금"].ToString().Trim()).ToString("###,###,###,##0");

                if (dgN관종.Rows[row].Cells["P최초체결강도"].Value == null || dgN관종.Rows[row].Cells["P최초체결강도"].Value.ToString().Trim() == "")
                {
                    dgN관종.Rows[row].Cells["P최초등락률"].Value = dr["등락율"].ToString().Trim();
                    dgN관종.Rows[row].Cells["P최초체결강도"].Value = Decimal.Parse(dr["체결강도"].ToString().Trim()).ToString("###,###,###,##0.00");
                    dgN관종.Rows[row].Cells["P최초거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString().Trim()).ToString("###,###,###,##0");
                    dgN관종.Rows[row].Cells["P최초거래대금"].Value = Decimal.Parse(dr["누적거래대금"].ToString().Trim()).ToString("###,###,###,##0");
                    //dgN관종.Rows[row].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    if (chk자동매매.Checked == false) return;
                    최초거래시간 = DateTime.Parse(dgN관종.Rows[row].Cells["P최초거래시간"].Value.ToString().Trim());
                    ts = DateTime.Now - 최초거래시간;
                    diffSecond = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
                    조건시간 = Decimal.Parse(nm조건시간.Value.ToString().Trim());


                    최초체결강도 = Decimal.Parse(dgN관종.Rows[row].Cells["P최초체결강도"].Value.ToString().Trim());
                    최초거래대금 = Decimal.Parse(dgN관종.Rows[row].Cells["P최초거래대금"].Value.ToString().Trim());
                    최초거래량 = Decimal.Parse(dgN관종.Rows[row].Cells["P최초거래량"].Value.ToString().Trim());

                    if (
                        dgN관종.Rows[row].Cells["P강제추가여부"].Value.ToString() == "Y" ||
                        diffSecond <= 조건시간
                        ) //강제 추가 거나 시간비교 넘어갔을때 관종에서 삭제됨
                    {
                        isBuy = false;

                        조건체결강도 = Decimal.Parse(nm조건체결강도.Value.ToString().Trim());
                        조건거래대금 = Decimal.Parse(nm조건거래대금.Value.ToString().Trim());
                        매수매도거래비용 = 0;

                        if (현재체결강도 > 100)
                        {
                            조건체결강도 = 조건체결강도 * 2;
                        }

                        if (dgN관종.Rows[row].Cells["P강제추가여부"].Value.ToString() == "Y")
                        {
                            if (_dsTick60.Tables[종목코드] != null && _dsTick60.Tables[종목코드].Rows.Count > 0)
                            {

                                dvTick = new DataView(_dsTick60.Tables[종목코드]);
                                dvTick.Sort = "일자 DESC  , 시작시간 DESC";
                                if (dvTick.Count > 4)
                                {
                                    try
                                    {
                                        avgPwRate = Decimal.Parse(_dsTick60.Tables[종목코드].Compute("MIN(체결강도)", "일자 = '" + DateTime.Now.ToString("yyyyMMdd") + "'").ToString());

                                        if (
                                            //Decimal.Parse(dvTick[0]["체결강도"].ToString()) - avgPwRate > 조건체결강도 &&
                                            Decimal.Parse(dvTick[0]["COUNT"].ToString()) >= Convert.ToInt32(nmTickCount.Value.ToString()) &&
                                            현재체결강도 - avgPwRate > 조건체결강도 &&
                                            Decimal.Parse(dvTick[0]["매수거래비용"].ToString()) + Decimal.Parse(dvTick[0]["매도거래비용"].ToString()) > (조건거래대금 * 1000000)
                                            )
                                        {
                                            isBuy = true;
                                        }
                                    }
                                    catch { }
                                }
                            }

                            if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 10:00:00")) && 현재거래량 <= 20000) isBuy = false;
                            else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 11:00:00")) && 현재거래량 <= 30000) isBuy = false;
                            else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 12:00:00")) && 현재거래량 <= 40000) isBuy = false;
                            else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 13:00:00")) && 현재거래량 <= 50000) isBuy = false;
                            else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 14:00:00")) && 현재거래량 <= 60000) isBuy = false;
                            else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:00:00")) && 현재거래량 <= 70000) isBuy = false;
                            else if (DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:00:00")) && 현재거래량 <= 75000) isBuy = false;
                        }
                        else
                        {
                            if (_ds뉴스.Tables[종목코드] == null) return;
                            if (_ds뉴스.Tables[종목코드].Rows.Count < 1) return;
                            drNews = _ds뉴스.Tables[종목코드].Rows[0];
                            매수매도거래비용 = Convert.ToInt32(drNews["매수거래비용"].ToString().Trim()) + Convert.ToInt32(drNews["매도거래비용"].ToString().Trim()); //매도거래비용이 음수이기 때문에 + 해준다.
                            if (
                                현재체결강도 - 최초체결강도 > 조건체결강도 &&
                                매수매도거래비용 > (조건거래대금 * 1000000) &&
                                최초체결강도 >= nm최소체결강도.Value
                                )
                            {
                                isBuy = true;
                            }
                        }

                        if (isBuy == true)
                        {
                            매수금액 = 0;
                            int 강제호가 = Int32.Parse(nmYHoga.Value.ToString());
                            int 공시호가 = Convert.ToInt32(nmDHoga.Value.ToString());

                            if (dgN관종.Rows[row].Cells["P모니터링구분"].Value != null)
                            {
                                if (dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString() == "조")
                                {
                                    매수금액 = Convert.ToInt32(nm조건매수금액.Value);
                                }
                                else if (dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString() == "공")
                                {
                                    매수금액 = Convert.ToInt32(nm공시매수금액.Value);
                                }
                                else
                                {
                                    매수금액 = Convert.ToInt32(nm관종매수금액.Value);
                                }
                            }
                            else
                            {
                                매수금액 = Convert.ToInt32(nm관종매수금액.Value);
                            }

                            매수수량 = Math.Abs(매수금액 / 현재가);
                            if (매수수량 > 0)
                            {
                                주문가 = 현재가;
                                if (dgN관종.Rows[row].Cells["P강제추가여부"].Value.ToString() == "Y")
                                {
                                    try
                                    {
                                        if (cboTickStd.Text.Trim() != "실시간")
                                        {
                                            주문가 = Int32.Parse(_dsTick60.Tables[종목코드].Compute("MIN(" + cboTickStd.Text.Trim() + ")", String.Empty).ToString());
                                        }

                                        if (_dsTick60.Tables[종목코드].Rows.Count < 10)
                                        { //TickCount 가 10개 이상 안되면 신빙성이 없어 매수가를 최대한 낮게 잡는다
                                            강제호가 = Convert.ToInt32(nmYHoga.Value) + 6;
                                        }

                                        //조건일때
                                        if (
                                            dgN관종.Rows[row].Cells["P모니터링구분"].Value != null &&
                                            dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString() == "조"
                                            )
                                        {
                                            if (주문가 > Decimal.Parse(dgN관종.Rows[row].Cells["P모니터링가격"].Value.ToString()))
                                            {
                                                주문가 = MakeOrderPrice(Convert.ToInt32(Decimal.Parse(dgN관종.Rows[row].Cells["P모니터링가격"].Value.ToString())));
                                            }
                                            else
                                            {
                                                if (_dsTick60.Tables[종목코드].Rows.Count < 30)
                                                {
                                                    강제호가 = Convert.ToInt32(nmYHoga.Value) + 6;
                                                }
                                                else
                                                {
                                                    강제호가 = Convert.ToInt32(nmYHoga.Value) - 2;
                                                }
                                            }
                                        }
                                        //관종일때
                                        else if (
                                            dgN관종.Rows[row].Cells["P모니터링구분"].Value != null &&
                                            dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString() == "" &&
                                            (dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("3일선") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("5일선") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("10일선") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("15일선") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("20일선") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("40일선") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("60일선") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("세력선") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("ENV") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("B상한") > -1
                                            || dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString().IndexOf("B하한") > -1
                                            )
                                        )
                                        {
                                            if (주문가 > Decimal.Parse(dgN관종.Rows[row].Cells["P모니터링가격"].Value.ToString()))
                                            {
                                                Decimal tmpP = Decimal.Parse(dgN관종.Rows[row].Cells["P모니터링가격"].Value.ToString().Replace(",", ""));
                                                주문가 = MakeOrderPrice(Int32.Parse(tmpP.ToString("#########0")));
                                            }
                                            else
                                            {
                                                강제호가 = Convert.ToInt32(nmYHoga.Value) - 2;
                                            }
                                        }
                                        else if (
                                            dgN관종.Rows[row].Cells["P모니터링구분"].Value != null &&
                                            dgN관종.Rows[row].Cells["P모니터링구분"].Value.ToString() == "수"
                                            )
                                        {
                                            주문가 = Int32.Parse(_dsTick60.Tables[종목코드].Compute("MIN(" + cboTickStd.Text.Trim() + ")", "일자 = '" + DateTime.Now.ToString("yyyyMMdd") + "'").ToString()); //강제 수동 추가일경우에는 그날 급등할껄 예상하고 등록하기 때문에 그날 저점에 주문금액을 등록한다.
                                            강제호가 = Convert.ToInt32(nmYHoga.Value) - 1;
                                        }
                                        SendBuySellMsg(종목코드, "00", 1, 강제호가, 주문가, 매수수량, "1");
                                    }
                                    catch { }
                                }
                                else
                                {
                                    string 거래구분 = "00";

                                    if (Cls.isExistsRow(dg뉴스체결, 0, dgN관종.Rows[row].Cells[0].Value.ToString().Trim()) == -1)
                                    {
                                        if (현재체결강도 - 최초체결강도 > nm조건시장가강도.Value &&
                                            최초체결강도 > nm최소체결강도.Value)
                                        {
                                            거래구분 = "03";
                                            dgN관종.Rows[row].Cells["P최초거래시간"].Value = "2010-01-01 00:00:00";
                                        }
                                        SendBuySellMsg(종목코드, 거래구분, 1, 공시호가, 현재가, 매수수량, "1");
                                    }
                                }

                                if (Cls.isExistsRow(dg뉴스체결, 0, dgN관종.Rows[row].Cells[0].Value.ToString().Trim()) == -1)
                                {
                                    dg뉴스체결.Rows.Insert(0, 1);
                                    dg뉴스체결.Rows[0].Cells["N종목코드"].Value = dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim();
                                    dg뉴스체결.Rows[0].Cells["N종목명"].Value = dgN관종.Rows[row].Cells["P종목명"].Value.ToString().Trim();
                                    dg뉴스체결.Rows[0].Cells["N주문가"].Value = Math.Abs(주문가).ToString("###,###,###,##0").Trim();
                                    dg뉴스체결.Rows[0].Cells["N현재가"].Value = Decimal.Parse(dgN관종.Rows[row].Cells["P현재가"].Value.ToString()).ToString("###,###,###,##0");

                                    if (dg뉴스체결.Rows[0].Cells["N주문가"].Value.ToString() != "" && dg뉴스체결.Rows[0].Cells["N현재가"].Value.ToString() != "")
                                    {
                                        Decimal temp1 = Decimal.Parse(dg뉴스체결.Rows[0].Cells["N주문가"].Value.ToString());
                                        Decimal temp2 = Decimal.Parse(dg뉴스체결.Rows[0].Cells["N현재가"].Value.ToString());
                                        dg뉴스체결.Rows[0].Cells["N수익률"].Value = ((temp2 - temp1) / temp1 * 100).ToString("###,###,###,##0.00");
                                    }

                                    dg뉴스체결.Rows[0].Cells["N거래량"].Value = dgN관종.Rows[row].Cells["P거래량"].Value.ToString().Trim();
                                    dg뉴스체결.Rows[0].Cells["N거래대금"].Value = dgN관종.Rows[row].Cells["P거래대금"].Value.ToString().Trim();
                                    dg뉴스체결.Rows[0].Cells["N체결강도"].Value = dgN관종.Rows[row].Cells["P체결강도"].Value.ToString().Trim();
                                    dg뉴스체결.Rows[0].Cells["N강제추가여부"].Value = dgN관종.Rows[row].Cells["P강제추가여부"].Value.ToString().Trim();
                                    dg뉴스체결.Rows[0].Cells["N매수수량"].Value = 매수수량.ToString().Trim();
                                    dg뉴스체결.Rows[0].Cells["N최초체결강도"].Value = dgN관종.Rows[row].Cells["P최초체결강도"].Value;
                                    dg뉴스체결.Rows[0].Cells["N최초거래시간"].Value = dgN관종.Rows[row].Cells["P최초거래시간"].Value;
                                    dg뉴스체결.Rows[0].Cells["N모니터링가격"].Value = dgN관종.Rows[row].Cells["P모니터링가격"].Value;
                                    dg뉴스체결.Rows[0].Cells["N모니터링구분"].Value = dgN관종.Rows[row].Cells["P모니터링구분"].Value;
                                    if (_ds뉴스.Tables[종목코드] != null) _ds뉴스.Tables.Remove(_ds뉴스.Tables[종목코드]);
                                    dgN관종.Rows.RemoveAt(row);
                                }
                            } //매수수량 0 -- END
                        }
                    }
                    else
                    {
                        //조건시간 나가리
                        if (dgN관종.Rows[row].Cells["P강제추가여부"].Value.ToString() != "Y")
                        {
                            _DataAcc.p_Psi02Add("A", _stockId, 3, 종목코드, "00", "", "", null, null);

                            SetDsScreenNo("A", "4", "1", dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim(), dgN관종.Rows[row].Cells["P종목명"].Value.ToString().Trim(), false);
                            SystemSleep();
                            if (_ds뉴스.Tables[종목코드] != null) _ds뉴스.Tables.Remove(_ds뉴스.Tables[종목코드]);

                            SetDsScreenNo("D", "1", "1", dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim(), dgN관종.Rows[row].Cells["P종목명"].Value.ToString().Trim(), false);
                            dgN관종.Rows.RemoveAt(row);
                        }
                    }
                }
            }


            //체결쪽으로 넘어간 데이터를 검수하고 RealData를 업데이트 해준다. - S
            obj = (from DataGridViewRow dgr in dg뉴스체결.Rows
                   where dgr.Cells[N종목코드.Index].Value != null && dgr.Cells[N종목코드.Index].Value.Equals(종목코드)
                    select dgr).FirstOrDefault();
            if (obj != null)
            {
                row = ((DataGridViewRow)obj).Index;
                dg뉴스체결.Rows[row].Cells["N현재가"].Value = Math.Abs(Decimal.Parse(dr["현재가"].ToString().Trim())).ToString("###,###,###,##0");
                dg뉴스체결.Rows[row].Cells["N등락률"].Value = dr["등락율"].ToString().Trim();
                dg뉴스체결.Rows[row].Cells["N체결강도"].Value = Decimal.Parse(dr["체결강도"].ToString().Trim()).ToString("###,###,###,##0.00");
                dg뉴스체결.Rows[row].Cells["N거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString().Trim()).ToString("###,###,###,##0");
                dg뉴스체결.Rows[row].Cells["N거래대금"].Value = Decimal.Parse(dr["누적거래대금"].ToString().Trim()).ToString("###,###,###,##0");

                if (dg뉴스체결.Rows[row].Cells["N주문가"].Value != null && dg뉴스체결.Rows[row].Cells["N현재가"].Value != null &&
                    dg뉴스체결.Rows[row].Cells["N주문가"].Value.ToString() != "" && dg뉴스체결.Rows[row].Cells["N현재가"].Value.ToString() != ""
                    )
                {
                    Decimal temp1 = Decimal.Parse(dg뉴스체결.Rows[row].Cells["N주문가"].Value.ToString());
                    Decimal temp2 = Decimal.Parse(dg뉴스체결.Rows[row].Cells["N현재가"].Value.ToString());

                    if (temp1 != 0)
                    {
                        dg뉴스체결.Rows[row].Cells["N수익률"].Value = ((temp2 - temp1) / temp1 * 100).ToString("###,###,###,##0.00");
                    }
                }


                if (dg뉴스체결.Rows[row].Cells["N주문가"].Value != null && dg뉴스체결.Rows[row].Cells["N현재가"].Value != null && dg뉴스체결.Rows[row].Cells["N주문번호"].Value != null && dg뉴스체결.Rows[row].Cells["N매수수량"].Value != null)
                {

                    if (dg뉴스체결.Rows[row].Cells["N모니터링구분"].Value == null) { 모니터링구분 = ""; }
                    else { 모니터링구분 = dg뉴스체결.Rows[row].Cells["N모니터링구분"].Value.ToString(); }

                    if (dg뉴스체결.Rows[row].Cells["N최초체결강도"].Value == null) { 최초체결강도 = 999; }
                    else { 최초체결강도 = Decimal.Parse(dg뉴스체결.Rows[row].Cells["N최초체결강도"].Value.ToString()); }

                    if (dg뉴스체결.Rows[row].Cells["N최초거래시간"].Value == null) { 최초거래시간 = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")); }
                    else { 최초거래시간 = DateTime.Parse(dg뉴스체결.Rows[row].Cells["N최초거래시간"].Value.ToString().Trim()); }

                    ts = DateTime.Now - 최초거래시간;
                    diffSecond = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
                    조건시간 = Decimal.Parse(nm조건시간.Value.ToString().Trim());
                    string 주문번호 = dg뉴스체결.Rows[row].Cells["N주문번호"].Value.ToString();



                    if (모니터링구분 == "공" &&
                        diffSecond <= 조건시간 &&
                        현재체결강도 - 최초체결강도 > nm조건시장가강도.Value &&
                        최초체결강도 > nm최소체결강도.Value
                        )
                    {
                        try
                        {
                            if (dg뉴스체결.Rows[row].Cells["N최초거래시간"].Value.ToString().Trim() != "2010-01-01 00:00:00")
                            {
                                매수금액 = Convert.ToInt32(nm공시매수금액.Value.ToString());
                                매수수량 = 매수금액 / 현재가;
                                SendBuySellMsg(종목코드, "00", (int)ucMainStock.OrderType.매수취소, 0, 0, Convert.ToInt32(dg뉴스체결.Rows[row].Cells["N매수수량"].Value.ToString()), "2", 주문번호);
                                SendBuySellMsg(종목코드, "03", (int)ucMainStock.OrderType.신규매수, 0, 0, 매수수량, "2", "");
                                dg뉴스체결.Rows[row].Cells["N최초거래시간"].Value = "2010-01-01 00:00:00";
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        if (dg뉴스체결.Rows[row].Cells["N최초거래시간"].Value.ToString().Trim() != "2010-01-01 00:00:00")
                        {
                            if (dg뉴스체결.Rows[row].Cells["N주문가"].Value.ToString() != "" && dg뉴스체결.Rows[row].Cells["N현재가"].Value.ToString() != "")
                            {
                                Decimal temp1 = Decimal.Parse(dg뉴스체결.Rows[row].Cells["N주문가"].Value.ToString());
                                Decimal temp2 = Decimal.Parse(dg뉴스체결.Rows[row].Cells["N현재가"].Value.ToString());
                                if (
                                    (모니터링구분 != "공" && 모니터링구분 != "수" && temp2 > temp1 + (temp1 * nmNYRate.Value / 100)) ||
                                    (모니터링구분 == "공" && ((diffSecond > 조건시간) || temp2 > temp1 + (temp1 * nmNDRate.Value / 100)))
                                )
                                {
                                    매수수량 = Convert.ToInt32(dg뉴스체결.Rows[row].Cells["N매수수량"].Value.ToString());
                                    SendBuySellMsg(종목코드, "00", (int)ucMainStock.OrderType.매수취소, 0, 0, 매수수량, "2", 주문번호);
                                    dg뉴스체결.Rows.RemoveAt(row);
                                }
                            }
                        }
                    }
                }
            } //if obj null
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
                        UcMainStockVer2.OptKWFid_OnReceiveRealData(tmpStr, cnt);
                        tmpStr = "";
                        cnt = 0;
                    }
                    cnt += 1;
                }
                UcMainStockVer2.OptKWFid_OnReceiveRealData(tmpStr, cnt);
            }
            else
            {
                UcMainStockVer2.OptKWFid_OnReceiveRealData(종목코드, cnt);
            }

            //dgUcReal.DataSource = UcMainStockVer2._DtScreenNoManage;
            //return UcMainStockVer2.ScreenNoManage(ucMainStockVer2.Enum_ScreenNo.TR_OPTK, "OptKWFid_OnReceiveRealData", 종목코드);
        }
        //뉴스 Tab 에 RealData를 요청 하겠다는 함수 OPT10003 은 최초 시간을 빠르게 가져 오기 위해 삽입 계속 바꾸어 가며 테스트중 - E


        private void SetDsScreenNo(string actGb, string 화면구분, string 실시간구분, string 종목코드, string 종목명, bool isOPT10007)
        {
            DataRow drAdd;
            DataRow[] drArr;

            if (actGb == "A")
            {
                if (isOPT10007 == true && 종목코드.Length == 6)
                {
                    if (
                        Cls.GetWeekOfDay(DateTime.Now) == "토" || Cls.GetWeekOfDay(DateTime.Now) == "일" ||
                        DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 09:00:00")) || DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:20:00")))
                    {
                        //if (화면구분 == "3")
                        //{
                        //    Task t = Task.Run(() => UcMainStockVer2.Opt10006_OnReceiveTrData_NewsFinder(종목코드, 종목명));
                        //    t.ContinueWith(
                        //        taks => Console.WriteLine("Opt10006_NewsFinder 호출 완료"), TaskContinuationOptions.OnlyOnRanToCompletion);
                        //    t.ContinueWith(
                        //        taks => Console.WriteLine("Opt10006_NewsFinder 호출 에러"), TaskContinuationOptions.OnlyOnFaulted);
                        //    Task.WaitAll(t);
                        //    SystemSleep();
                        //}
                        //else
                        //{
                        UcMainStockVer2.Opt10007_OnReceiveTrData(종목코드, 종목명);
                        //UcMainStockVer2.Opt10007_OnReceiveTrData(종목코드, 종목명);
                        //SystemSleep();
                        //t.ContinueWith(
                        //    taks => Console.WriteLine("Opt10007 호출 완료"), TaskContinuationOptions.OnlyOnRanToCompletion);
                        //t.ContinueWith(
                        //    taks => Console.WriteLine("Opt10007 호출 에러"), TaskContinuationOptions.OnlyOnFaulted);
                        //Task.WaitAll(t);
                        SystemSleep();
                        //}
                        if (DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:00:00")) || DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:20:00")))
                        {
                            //return;
                        }

                        //Task t = new Task(() => UcMainStockVer2.Opt10007_OnReceiveTrData(종목코드, 종목명));
                        //t.Start();
                        //t.Wait();
                        //return;
                    }
                }

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
                                if (UcMainStockVer2._allStockDataset.Tables["STOCKLIST"].Select("STOCK_CODE = '" + str + "'").Length > 0)
                                {
                                    cboTickDataMember.Items.Add(str + "-" + UcMainStockVer2._allStockDataset.Tables["STOCKLIST"].Select("STOCK_CODE = '" + str + "'")[0]["STOCK_NAME"].ToString());
                                    tickDt = _dsTick60.Tables.Add(str);
                                    for (int i = 0; i < _Tick60.Length / 2; i++)
                                    {
                                        tickDt.Columns.Add(_Tick60[i, 0], Type.GetType(_Tick60[i, 1]));
                                    }
                                }

                            }
                        }
                    }

                    if (UcMainStockVer2._DtScreenNoManage != null)
                    {
                        if (UcMainStockVer2._DtScreenNoManage.Rows.Count < 1)
                        {
                            RequestRealData(종목코드);
                        }
                        else
                        {
                            if (UcMainStockVer2._DtScreenNoManage.Select("STOCK_CODE LIKE '%" + 종목코드 + "%'").Length < 1)
                            {
                                RequestRealData(종목코드);
                            }
                        }
                    }
                    //else
                    //{
                    //    drArr = _dt화면관리.Select(String.Format("실시간구분 = '{0}' AND 종목코드 = '{1}' AND 화면구분 = '{2}'", 실시간구분, 종목코드, 화면구분));
                    //    if (drArr.Length > 1)
                    //    {
                    //        화면번호 = drArr[0]["화면번호"].ToString().Trim();
                    //    }
                    //    else
                    //    {
                    //        //화면번호 = UcMainStockVer2._dtOptKWFidScreenNo.Select(String.Format("STOCK_CODE = '{0}'", 종목코드))[0]["SCREEN_NO"].ToString();
                    //        화면번호 = GetScrNum();
                    //    }
                    //}
                }


                drArr = _dt화면관리.Select(String.Format("화면구분 = '{0}' AND 실시간구분 = '{1}' AND 종목코드 LIKE '%{2}%'", 화면구분, 실시간구분, 종목코드));
                if (drArr.Length > 0) return;

                drAdd = _dt화면관리.NewRow();
                drAdd["화면구분"] = 화면구분;
                drAdd["실시간구분"] = 실시간구분;
                drAdd["종목코드"] = 종목코드;
                //drAdd["화면번호"] = 화면번호;
                _dt화면관리.Rows.Add(drAdd);
            }
            else if (actGb == "D")
            {
                DataRow[] drArrSub;

                drArr = _dt화면관리.Select(String.Format("화면구분 = '{0}' AND 실시간구분 = '{1}' AND 종목코드 = '{2}'", 화면구분, 실시간구분, 종목코드));
                foreach (DataRow dr in drArr)
                {
                    drArrSub = _dt화면관리.Select(String.Format("화면구분 <> '{0}' AND 실시간구분 = '{1}' AND 종목코드 LIKE '%{2}%'", 화면구분, 실시간구분, 종목코드));
                    if (drArrSub.Length < 1)
                    {  // 다른 화면에서 사용할 가능성이 있기 때문에 끊지 않음
                        UcMainStockVer2.DisconnectRealDataStockCode(종목코드); //실시간 데이터가 필요 없을 경우 실시간 데이터 요청을 끊어준다. - S
                    }

                    _dt화면관리.Rows.Remove(dr);
                    SystemSleep();
                }
            }
        }


        //조건검색 받는쪽 - S
        private void SetConditionList(DataSet ds)
        {
            string strCodes = "";
            if (ds == null) return;
            try
            {
                int row = 0;
                int interId = 0;
                Decimal ret = 0;
                string monitor = "";
                dg조건종목.RowCount = 1;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["STOCK_CODE"].ToString().Trim() == "") continue;

                    dg조건종목.RowCount++;
                    dg조건종목.Rows[row].Cells["C종목코드"].Value = dr["STOCK_CODE"].ToString().Trim();
                    dg조건종목.Rows[row].Cells["C종목명"].Value = dr["STOCK_NAME"].ToString().Trim();

                    DataTable dt = SetLine(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dg조건종목.Rows[row].Cells["CH3일선"].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                        dg조건종목.Rows[row].Cells["CH5일선"].Value = dt.Rows[0]["line5"].ToString();//H_5일선
                        dg조건종목.Rows[row].Cells["CH10일선"].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                        dg조건종목.Rows[row].Cells["CH15일선"].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                        dg조건종목.Rows[row].Cells["CH20일선"].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                        dg조건종목.Rows[row].Cells["CH40일선"].Value = dt.Rows[0]["line40"].ToString();//H_40일선
                        dg조건종목.Rows[row].Cells["CH60일선"].Value = dt.Rows[0]["line60"].ToString();//H_60일선
                        dg조건종목.Rows[row].Cells["C세력선_H"].Value = (Decimal.TryParse(dt.Rows[0]["MA_H"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_H"].ToString()).ToString("###,###,##0") : "");//MA_H
                        dg조건종목.Rows[row].Cells["C세력선_C"].Value = (Decimal.TryParse(dt.Rows[0]["MA_C"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_C"].ToString()).ToString("###,###,##0") : "");//MA_C
                        dg조건종목.Rows[row].Cells["C세력선_L"].Value = (Decimal.TryParse(dt.Rows[0]["MA_L"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_L"].ToString()).ToString("###,###,##0") : "");//MA_L
                        dg조건종목.Rows[row].Cells["CB상한"].Value = (Decimal.TryParse(dt.Rows[0]["B_UP"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["B_UP"].ToString()).ToString("###,###,##0.00") : "");//B_UP
                        dg조건종목.Rows[row].Cells["CB하한"].Value = (Decimal.TryParse(dt.Rows[0]["B_DOWN"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["B_DOWN"].ToString()).ToString("###,###,##0.00") : "");//B_DOWN
                    }

                    strCodes += dr["STOCK_CODE"].ToString().Trim() + ";";
                    row++;
                    
                    for (int ix = 0; ix < dg조건리스트.Rows.Count - 1; ix++ )
                    {
                        if (dg조건리스트.Rows[ix].Cells["실행"].Value.ToString() == "||")
                        {
                            if (dg조건리스트.Rows[ix].Cells["조건명"].Value.ToString().IndexOf("B41하단") > -1) { interId = 5; monitor = "B하한"; }
                            else if (dg조건리스트.Rows[ix].Cells["조건명"].Value.ToString().IndexOf("B41중심") > -1) { interId = 5; monitor = "세력선_L"; }
                            else { interId = 4; monitor = "B하한"; }
                            break;
                        }
                    }
                    _DataAcc.p_Psi02Add("A", _stockId, interId, dr["STOCK_CODE"].ToString().Trim(), "00", "", monitor, null, null);
                }

                if (
                    (Cls.GetWeekOfDay(DateTime.Now) == "토" || Cls.GetWeekOfDay(DateTime.Now) == "일") ||
                    DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:00:00")) || DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:20:00"))
                   )
                {
                    ucStockAvgMagipPrice1.PropWriteStockList10007 = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dg조건종목);
                }
                else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:00:00")) || DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:20:00")))
                {
                    SetDsScreenNo("A", "3", "1", strCodes, "", false);
                    SystemSleep();
                }
                SetConditionPrice(-1);
            }
            catch (Exception ex)
            {
                Logger("에러 (OnDsSetConditionList)", ex.ToString());
            }
            finally
            {
                _DataAcc.DisConnect();
            }
        }
        //조건검색 받는쪽 - E

        private void UcMainStockVer2_OnDsStockByTradePortNumer(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnDsTradePortInfo(DataRow dr)
        {

        }

        //조건 검색 API 25% , 50% , 75% , 0% 계산법 - S
        private void SetConditionPrice(int rowIndex)
        {
            DataSet ds;
            DataView dv;
            string 종목코드, 종목명;
            Decimal temp;
            Decimal 지지선1;
            Decimal 지지선2;
            Decimal 지지선3;
            Decimal 시가;
            Decimal 저가;
            Decimal 등락률;
            Decimal 종가;
            Decimal 고가;
            Decimal 전일종가;
            Decimal 저종MA;
            Decimal 최고저종MA;
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
                eRow = dg조건종목.Rows.Count - 2;
            }

            for (int row = sRow; row <= eRow; row++)
            {
                dg조건종목.Rows[row].Cells["C현재가"].Value = "";
                dg조건종목.Rows[row].Cells["C1차매수가"].Value = "";
                dg조건종목.Rows[row].Cells["C2차매수가"].Value = "";
                dg조건종목.Rows[row].Cells["C3차매수가"].Value = "";

                종목코드 = dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString().Trim();
                종목명 = dg조건종목.Rows[row].Cells["C종목명"].Value.ToString().Trim();
                ds = _DataAcc.p_stock_day_data_query("4", 종목코드, DateTime.Now.AddDays(-10).ToString("yyyyMMdd"), true, null, null);
                dv = new DataView(ds.Tables[0]);

                if (dg조건리스트.CurrentRow.Cells["조건명"].Value.ToString().IndexOf("시종") > -1)
                {
                    dv.RowFilter = "CANDLE_RATE >= 6.00 ";
                }
                else
                {
                    dv.RowFilter = "DAY_RATE >= 6.00 ";
                }

                dv.Sort = "L_PRICE ASC";
                if (dv.Count < 1) { continue; }

                종가 = Decimal.Parse(dv[0]["END_PRICE"].ToString().Trim());
                전일종가 = Decimal.Parse(dv[0]["PRE_E_PRICE"].ToString().Trim());
                시가 = Decimal.Parse(dv[0]["S_PRICE"].ToString().Trim());
                저가 = Decimal.Parse(dv[0]["L_PRICE"].ToString().Trim());
                고가 = Decimal.Parse(dv[0]["H_PRICE"].ToString().Trim());

                저종MA = Decimal.Parse(dv[0]["LOWEND_MA"].ToString().Trim());
                최고저종MA = Decimal.Parse(dv[0]["H_LOWEND_MA"].ToString().Trim());
                등락률 = Decimal.Parse(dv[0]["DAY_RATE"].ToString().Trim());

                //if (dg조건종목.Rows[row].Cells["C현재가"].Value.ToString() == "")
                //{
                //    dg조건종목.Rows[row].Cells["C현재가"].Value =  dv[0]["LAST_END_PRICE_DATE"].ToString().Trim();//제일 최근의 전일종가를 가져온다.
                //}

                if (등락률 < 15)
                {
                    지지선1 = (종가 + 저가) / 2;
                    dg조건종목.Rows[row].Cells["C1차매수가"].Value = 지지선1.ToString("###,###,###,##0").Trim();
                    dg조건종목.Rows[row].Cells["C2차매수가"].Value = 저가.ToString("###,###,###,##0").Trim();

                    if (저가 > 최고저종MA) dg조건종목.Rows[row].Cells["C3차매수가"].Value = 최고저종MA.ToString("###,###,###,##0").Trim();
                    if (저가 > (최고저종MA + 저종MA) / 2) dg조건종목.Rows[row].Cells["C3차매수가"].Value = (최고저종MA + 저종MA).ToString("###,###,###,##0").Trim();
                    if (저가 > 저종MA) dg조건종목.Rows[row].Cells["C3차매수가"].Value = 저종MA.ToString("###,###,###,##0").Trim();

                    //temp = (지지선1 - 시가) / 시가 * 100;
                    //if (temp < 1)
                    //{
                    //    dg조건종목.Rows[row].Cells["C1차매수가"].Value = 시가.ToString("###,###,###,##0").Trim(); ;
                    //}
                    //else
                    //{
                    //    dg조건종목.Rows[row].Cells["C1차매수가"].Value = 지지선1.ToString("###,###,###,##0").Trim(); ;
                    //}

                    temp = (시가 - 저가) / 저가 * 100;

                    //if (temp < 1)
                    //{
                    //    dg조건종목.Rows[row].Cells["C2차매수가"].Value = 저가.ToString("###,###,###,##0").Trim(); ;
                    //    dg조건종목.Rows[row].Cells["C3차매수가"].Value = 저종MA.ToString("###,###,###,##0").Trim();
                    //}
                    //else
                    //{
                    //    dg조건종목.Rows[row].Cells["C2차매수가"].Value = 시가.ToString("###,###,###,##0").Trim(); ;
                    //    dg조건종목.Rows[row].Cells["C3차매수가"].Value = 저가.ToString("###,###,###,##0").Trim(); ;
                    //}

                }
                else
                {
                    지지선1 = 종가 - ((종가 - 저가) / 3);
                    지지선2 = (종가 + 저가) / 2;
                    지지선3 = 저가;

                    dg조건종목.Rows[row].Cells["C1차매수가"].Value = 지지선1.ToString("###,###,###,##0").Trim(); ;
                    dg조건종목.Rows[row].Cells["C2차매수가"].Value = 지지선2.ToString("###,###,###,##0").Trim(); ;
                    dg조건종목.Rows[row].Cells["C3차매수가"].Value = 지지선3.ToString("###,###,###,##0").Trim(); ;
                }

                dv.RowFilter = "";
            }
            _DataAcc.DisConnect();
        }
        //조건 검색 API 25% , 50% , 75% , 0% 계산법 - E

        //관종 실시간 리스트 리셋 - S
        private void btnReset_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dgN관종.RowCount - 1; row++)
            {
                SetDsScreenNo("D", "1", "1", dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim(), dgN관종.Rows[row].Cells["P종목명"].Value.ToString().Trim(), false);
            }
            _ds뉴스.Tables.Clear();
            dgN관종.RowCount = 1;
        }
        //관종 실시간 리스트 리셋 - E

        //체결된 실시간 리스트 리셋 - S
        private void btn뉴스체결_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dg뉴스체결.RowCount - 1; row++)
            {
                SetDsScreenNo("D", "1", "1", dg뉴스체결.Rows[row].Cells["N종목코드"].Value.ToString().Trim(), dg뉴스체결.Rows[row].Cells["N종목명"].Value.ToString().Trim(), false);
            }
            dg뉴스체결.RowCount = 1;
        }
        //체결된 실시간 리스트 리셋 - E

        private void txtTmDaum_Leave(object sender, EventArgs e)
        {
            //tmrDaum.Start();
            //tmrDaum.Interval = Int32.Parse(txtTmDaum1.Text.Trim());
        }

        private void txtTmNaver_Leave(object sender, EventArgs e)
        {
            tmrNaver.Start();
            tmrNaver.Interval = Int32.Parse(txtTmNaver2.Text.Trim());
        }

        private void txtTmDart_Leave(object sender, EventArgs e)
        {
            tmrDart.Start();
            tmrDartKrx.Start();
            tmrDart.Interval = Int32.Parse(txtTmDart3.Text.Trim());
            tmrDartKrx.Interval = Int32.Parse(txtTmDart3.Text.Trim());
            //tmrDart1.Start();
            //tmrDart1.Interval = Int32.Parse(txtTmDart3.Text.Trim());
        }


        //확장버튼 - S
        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (btnExpand.Text == "◀")
            {
                groupBox3.Width = 952;
                groupBox4.Width = 952;
                groupBox3.Left -= 400;
                groupBox4.Left -= 400;
                txt관종.Left -= 400;
                btnExpand.Left -= 400;
                btnExpand.Text = "▶";
            }
            else
            {
                groupBox3.Width = 552;
                groupBox4.Width = 552;
                groupBox3.Left += 400;
                groupBox4.Left += 400;
                txt관종.Left += 400;
                btnExpand.Left += 400;
                btnExpand.Text = "◀";
            }
        }
        //확장버튼 - E



        private void dgN관종_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 1)
            {
                SetDsScreenNo("A", "1", "1", dgN관종.Rows[e.RowIndex].Cells["P종목코드"].ToString().Trim(), dgN관종.Rows[e.RowIndex].Cells["P종목명"].ToString().Trim(), true);
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
                tmrNaver.Start();
                btnNewsStop.Text = "뉴스타이머중지(■)";
            }
            else
            {
                //tmrDaum.Stop();
                tmrNaver.Stop();
                btnNewsStop.Text = "뉴스타이머실행(▶)";
            }
        }

        private void btn로그실행_Click(object sender, EventArgs e)
        {

        }

        private void btn잔고_Click(object sender, EventArgs e)
        {
            UcMainStockVer2.Opw00018_OnReceiveTrData(cboAccount.Items[cboAccount.SelectedIndex].ToString());
            _dt미체결.Rows.Clear();
            UcMainStockVer2.Opt10075_OnReceiveChejanData(cboAccount.Items[cboAccount.SelectedIndex].ToString());
        }

        private void dg관종_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtF종목코드_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridViewRow dgv;
            if (e.KeyCode == Keys.Enter)
            {
                if (UcMainStockVer2._allStockDataset == null) return;
                DataView dv = new DataView(UcMainStockVer2._allStockDataset.Tables[0]);
                dv.RowFilter = "STOCK_NAME = '" + txtF종목코드.Text.Trim() + "'";

                bool blnTrue = false;
                Decimal ret;
                if (dv.Count < 1) return;

                blnTrue = false;
                for (int row = 0; row < dg관종.Rows.Count - 1; row++)
                {
                    if (dg관종.Rows[row].Cells["F_종목코드"].Value.ToString().Trim() == dv[0]["STOCK_CODE"].ToString())
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
                        dgv = dg관종.Rows[dg관종.RowCount - 2];
                        dgv.Cells["F_삭제"].Value = "D";
                        dgv.Cells["F_종목코드"].Value = dv[0]["STOCK_CODE"].ToString().Trim();
                        dgv.Cells["F_종목명"].Value = dv[0]["STOCK_NAME"].ToString().Trim();

                        DataTable dt = SetLine(dv[0]["STOCK_CODE"].ToString().Trim());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dgv.Cells["H_3일선"].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                            dgv.Cells["H_5일선"].Value = dt.Rows[0]["line5"].ToString(); //H_5일선
                            dgv.Cells["H_10일선"].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                            dgv.Cells["H_15일선"].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                            dgv.Cells["H_20일선"].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                            dgv.Cells["H_40일선"].Value = dt.Rows[0]["line40"].ToString();//H_40일선
                            dgv.Cells["H_60일선"].Value = dt.Rows[0]["line60"].ToString();//H_60일선
                            dgv.Cells["F_세력선_H"].Value = (Decimal.TryParse(dt.Rows[0]["MA_H"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_H"].ToString()).ToString("###,###,##0") : "");//MA_H
                            dgv.Cells["F_세력선_C"].Value = (Decimal.TryParse(dt.Rows[0]["MA_C"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_C"].ToString()).ToString("###,###,##0") : "");//MA_C
                            dgv.Cells["F_세력선_L"].Value = (Decimal.TryParse(dt.Rows[0]["MA_L"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_L"].ToString()).ToString("###,###,##0") : "");//MA_L
                            dgv.Cells["F_B상한"].Value = (Decimal.TryParse(dt.Rows[0]["B_UP"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["B_UP"].ToString()).ToString("###,###,##0.00") : "");//B_UP
                            dgv.Cells["F_B하한"].Value = (Decimal.TryParse(dt.Rows[0]["B_DOWN"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["B_DOWN"].ToString()).ToString("###,###,##0.00") : "");//B_DOWN

                            if(Decimal.TryParse(dgv.Cells["F_B상한"].Value.ToString(), out ret) != false && Decimal.TryParse(dgv.Cells["F_B하한"].Value.ToString(), out ret) != false) {
                                dgv.Cells["F_BB이격"].Value = ((Decimal.Parse(dgv.Cells["F_B상한"].Value.ToString()) - Decimal.Parse(dgv.Cells["F_B하한"].Value.ToString())) / Decimal.Parse(dgv.Cells["F_B하한"].Value.ToString()) * 100).ToString("#0.0");
                            }
                        }

                        dt = SetFinance(dv[0]["STOCK_CODE"].ToString().Trim());
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dgv.Cells["F_신용비율"].Value = dt.Rows[0]["CREDIT_RATE"].ToString();
                            dgv.Cells["F_시가총액"].Value = dt.Rows[0]["STOCK_TOTAL_P"].ToString();
                            dgv.Cells["F_PER"].Value = dt.Rows[0]["PER"].ToString();
                            dgv.Cells["F_ROE"].Value = dt.Rows[0]["ROE"].ToString();
                            dgv.Cells["F_PBR"].Value = dt.Rows[0]["PBR"].ToString();
                            dgv.Cells["F_EV"].Value = dt.Rows[0]["EV"].ToString();
                            dgv.Cells["F_EPS"].Value = dt.Rows[0]["EPS"].ToString();
                            dgv.Cells["F_BPS"].Value = dt.Rows[0]["BPS"].ToString();
                            dgv.Cells["F_영업이익"].Value = dt.Rows[0]["O_PROFIT"].ToString();
                            dgv.Cells["F_당기순이익"].Value = dt.Rows[0]["P_PROFIT"].ToString();
                            dgv.Cells["F_250최고가"].Value = dt.Rows[0]["HIGH_250"].ToString();
                            dgv.Cells["F_250최저가"].Value = dt.Rows[0]["LOW_250"].ToString();

                            try { 
                                if (Decimal.Parse(dgv.Cells["F_신용비율"].Value.ToString()) > 5) dgv.Cells["F_신용비율"].Style.ForeColor = System.Drawing.Color.Red;
                                else dgv.Cells["F_신용비율"].Style.ForeColor = System.Drawing.Color.Empty;
                                if (Decimal.Parse(dgv.Cells["F_PBR"].Value.ToString()) > 3) dgv.Cells["F_PBR"].Style.ForeColor = System.Drawing.Color.Red;
                                else dgv.Cells["F_PBR"].Style.ForeColor = System.Drawing.Color.Empty;
                                if (Decimal.Parse(dgv.Cells["F_영업이익"].Value.ToString()) < 0) dgv.Cells["F_영업이익"].Style.ForeColor = System.Drawing.Color.Red;
                                else dgv.Cells["F_영업이익"].Style.ForeColor = System.Drawing.Color.Empty;
                                if (Decimal.Parse(dgv.Cells["F_당기순이익"].Value.ToString()) < 0) dgv.Cells["F_당기순이익"].Style.ForeColor = System.Drawing.Color.Red;
                                else dgv.Cells["F_당기순이익"].Style.ForeColor = System.Drawing.Color.Empty;
                            }
                            catch { }
                        }

                        //dg관종.Rows.Add(dgv);

                        int interId = 0;
                        if (rb1.Checked == true) interId = 1;
                        else if (rb2.Checked == true) interId = 2;
                        else if (rb3.Checked == true) interId = 3;
                        else if (rb4.Checked == true) interId = 4;
                        else if (rb5.Checked == true) interId = 5;

                        _DataAcc.p_Psi02Add("A", _stockId, interId, dv[0]["STOCK_CODE"].ToString().Trim(), "00", "", "", null, null);
                        SetDsScreenNo("A", "4", "1", dv[0]["STOCK_CODE"].ToString().Trim(), dv[0]["STOCK_NAME"].ToString().Trim(), true);
                    }
                    finally
                    {
                        _DataAcc.DisConnect();
                    }
                }

                txtF종목코드.Text = "";
            }

        }

        private void dg관종_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) //삭제버튼클릭시
            {
                int interId = 0;
                if (rb1.Checked == true) interId = 1;
                else if (rb2.Checked == true) interId = 2;
                else if (rb3.Checked == true) interId = 3;
                else if (rb4.Checked == true) interId = 4;
                else if (rb5.Checked == true) interId = 5;

                _DataAcc.p_Psi02Add("D", _stockId, interId, dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value.ToString().Trim(), "00", "", "", null, null);

                SetDsScreenNo("D", "4", "1", dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value.ToString().Trim(), dg관종.Rows[e.RowIndex].Cells["F_종목명"].Value.ToString().Trim(), false);
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
            if (dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value == null) return;
            UcHogaWindowNew1.StockCode = dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value.ToString().Trim();
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
            System.Drawing.Color preColor;
            preColor = System.Drawing.Color.Empty;

            if (e.RowIndex == -1) return;
            if (e.ColumnIndex == C거래량.Index){//거래량일때만
                preColor = dg조건종목.Rows[e.RowIndex].DefaultCellStyle.BackColor;
                if (preColor == System.Drawing.Color.LightYellow) preColor = System.Drawing.Color.Empty;
                dg조건종목.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                if (Int32.Parse(DateTime.Now.ToString("HH")) <= 16 && Int32.Parse(DateTime.Now.ToString("HH")) >= 9) Application.DoEvents();
            }
            if (e.ColumnIndex == C대비.Index) //대비
            {
                if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C대비"].Value.ToString()) < 0)
                {
                    dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Style.ForeColor = System.Drawing.Color.Blue;
                    dg조건종목.Rows[e.RowIndex].Cells["C대비"].Style.ForeColor = System.Drawing.Color.Blue;
                    dg조건종목.Rows[e.RowIndex].Cells["C등락률"].Style.ForeColor = System.Drawing.Color.Blue;
                }
                else if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C대비"].Value.ToString()) > 0)
                {
                    dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Style.ForeColor = System.Drawing.Color.Red;
                    dg조건종목.Rows[e.RowIndex].Cells["C대비"].Style.ForeColor = System.Drawing.Color.Red;
                    dg조건종목.Rows[e.RowIndex].Cells["C등락률"].Style.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Style.ForeColor = System.Drawing.Color.Empty;
                    dg조건종목.Rows[e.RowIndex].Cells["C대비"].Style.ForeColor = System.Drawing.Color.Empty;
                    dg조건종목.Rows[e.RowIndex].Cells["C등락률"].Style.ForeColor = System.Drawing.Color.Empty;
                }
            }

            if (e.ColumnIndex == 3)
            {
                try
                {
                    if (dg조건종목.Rows[e.RowIndex].Cells["CH3일선"].Value == null) return;
                    if (dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value == null || dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString().Trim() == "") return;

                    //가격변경 - S
                    if (Cls.IsNumeric(dg조건종목.Rows[e.RowIndex].Cells["CH3일선"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C3일선"].Value =
                            ((Convert.ToDecimal(dg조건종목.Rows[e.RowIndex].Cells["CH3일선"].Value.ToString()) + Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString())) / 3).ToString("###,###,###,##0.00")
                            ;
                    }
                    if (Cls.IsNumeric(dg조건종목.Rows[e.RowIndex].Cells["CH5일선"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C5일선"].Value =
                            ((Convert.ToDecimal(dg조건종목.Rows[e.RowIndex].Cells["CH5일선"].Value.ToString()) + Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString())) / 5).ToString("###,###,###,##0.00")
                            ;
                    }
                    if (Cls.IsNumeric(dg조건종목.Rows[e.RowIndex].Cells["CH10일선"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C10일선"].Value =
                            ((Convert.ToDecimal(dg조건종목.Rows[e.RowIndex].Cells["CH10일선"].Value.ToString()) + Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString())) / 10).ToString("###,###,###,##0.00")
                            ;
                    }
                    if (Cls.IsNumeric(dg조건종목.Rows[e.RowIndex].Cells["CH15일선"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C15일선"].Value =
                            ((Convert.ToDecimal(dg조건종목.Rows[e.RowIndex].Cells["CH15일선"].Value.ToString()) + Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString())) / 15).ToString("###,###,###,##0.00")
                            ;
                    }
                    if (Cls.IsNumeric(dg조건종목.Rows[e.RowIndex].Cells["CH20일선"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value =
                            ((Convert.ToDecimal(dg조건종목.Rows[e.RowIndex].Cells["CH20일선"].Value.ToString()) + Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString())) / 20).ToString("###,###,###,##0.00")
                            ;
                    }

                    if (Cls.IsNumeric(dg조건종목.Rows[e.RowIndex].Cells["CH40일선"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C40일선"].Value =
                            ((Convert.ToDecimal(dg조건종목.Rows[e.RowIndex].Cells["CH40일선"].Value.ToString()) + Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString())) / 40).ToString("###,###,###,##0.00")
                            ;
                    }

                    if (Cls.IsNumeric(dg조건종목.Rows[e.RowIndex].Cells["CH60일선"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C60일선"].Value =
                            ((Convert.ToDecimal(dg조건종목.Rows[e.RowIndex].Cells["CH60일선"].Value.ToString()) + Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString())) / 60).ToString("###,###,###,##0.00")
                            ;
                    }

                    if (dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value != null && Cls.IsNumeric(dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CENV상한"].Value =
                            (
                            Convert.ToDecimal(dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value.ToString()) * (1 + (envRate.Value / 100))
                            ).ToString("###,###,##0.00");

                    }

                    if (dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value != null && Cls.IsNumeric(dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CENV하한"].Value =
                            (
                            Convert.ToDecimal(dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value.ToString()) * (1 - (envRate.Value / 100))
                            ).ToString("###,###,##0.00");
                    }
                    //가격변경 - E


                    //색깔변경 - S
                    if (dg조건종목.Rows[e.RowIndex].Cells["C1차매수가"].Value != null && dg조건종목.Rows[e.RowIndex].Cells["C1차매수가"].Value.ToString().Trim() != "")
                    {
                        if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C1차매수가"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                        {
                            dg조건종목.Rows[e.RowIndex].Cells["C1차매수가"].Style.BackColor = System.Drawing.Color.LightBlue;
                        }
                        else
                        {
                            dg조건종목.Rows[e.RowIndex].Cells["C1차매수가"].Style.BackColor = System.Drawing.Color.Empty;
                        }
                    }

                    if (dg조건종목.Rows[e.RowIndex].Cells["C2차매수가"].Value != null && dg조건종목.Rows[e.RowIndex].Cells["C2차매수가"].Value.ToString().Trim() != "")
                    {
                        if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C2차매수가"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                        {
                            dg조건종목.Rows[e.RowIndex].Cells["C2차매수가"].Style.BackColor = System.Drawing.Color.LightBlue;
                        }
                        else
                        {
                            dg조건종목.Rows[e.RowIndex].Cells["C2차매수가"].Style.BackColor = System.Drawing.Color.Empty;
                        }
                    }

                    if (dg조건종목.Rows[e.RowIndex].Cells["C3차매수가"].Value != null && dg조건종목.Rows[e.RowIndex].Cells["C3차매수가"].Value.ToString().Trim() != "")
                    {
                        if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C3차매수가"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                        {
                            dg조건종목.Rows[e.RowIndex].Cells["C3차매수가"].Style.BackColor = System.Drawing.Color.LightBlue;
                        }
                        else
                        {
                            dg조건종목.Rows[e.RowIndex].Cells["C3차매수가"].Style.BackColor = System.Drawing.Color.Empty;
                        }
                    }

                    if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C3일선"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C3일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C3일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C5일선"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C5일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C5일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C10일선"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C10일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C10일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C15일선"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C15일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C15일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value != null && Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (dg조건종목.Rows[e.RowIndex].Cells["C40일선"].Value != null && Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C40일선"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C40일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C40일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (dg조건종목.Rows[e.RowIndex].Cells["C60일선"].Value != null && Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C60일선"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C60일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C60일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C세력선_L"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C세력선_L"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C세력선_L"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C세력선_C"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C세력선_C"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C세력선_C"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C세력선_H"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C세력선_H"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["C세력선_H"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value != null && Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["CENV상한"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CENV상한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CENV상한"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (dg조건종목.Rows[e.RowIndex].Cells["C20일선"].Value != null && Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["CENV하한"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CENV하한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CENV하한"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (dg조건종목.Rows[e.RowIndex].Cells["CB상한"].Value.ToString() != "" && Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["CB상한"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CB상한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CB상한"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (dg조건종목.Rows[e.RowIndex].Cells["CB하한"].Value.ToString() != "" && Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["CB하한"].Value.ToString()) <= Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString()))
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CB하한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg조건종목.Rows[e.RowIndex].Cells["CB하한"].Style.BackColor = System.Drawing.Color.Empty;
                    }
                    //색깔변경 - S


                    if (dg조건종목.Rows[e.RowIndex].Cells["C시가"].Value != null
                        && dg조건종목.Rows[e.RowIndex].Cells["C1차매수가"].Value != null
                        && dg조건종목.Rows[e.RowIndex].Cells["C1차매수가"].Value.ToString().Trim() != ""
                        && dg조건종목.Rows[e.RowIndex].Cells["C1차매수가"].Value.ToString().Trim() != "0"
                        && dg조건종목.Rows[e.RowIndex].Cells["C2차매수가"].Value != null
                        && dg조건종목.Rows[e.RowIndex].Cells["C2차매수가"].Value.ToString().Trim() != ""
                        && dg조건종목.Rows[e.RowIndex].Cells["C2차매수가"].Value.ToString().Trim() != "0"
                        )
                    {
                        string 종목코드 = dg조건종목.Rows[e.RowIndex].Cells["C종목코드"].Value.ToString().Trim();
                        string 종목명 = dg조건종목.Rows[e.RowIndex].Cells["C종목명"].Value.ToString().Trim();
                        Double 시가 = Double.Parse(dg조건종목.Rows[e.RowIndex].Cells["C시가"].Value.ToString().Trim());
                        Double 현재가 = Double.Parse(dg조건종목.Rows[e.RowIndex].Cells["C현재가"].Value.ToString().Trim());
                        Double 매수가1차 = Double.Parse(dg조건종목.Rows[e.RowIndex].Cells["C1차매수가"].Value.ToString().Trim());
                        Double 매수가2차 = Double.Parse(dg조건종목.Rows[e.RowIndex].Cells["C2차매수가"].Value.ToString().Trim());
                        Double percent = 100;
                        Double mPrice = 0;
                        DataRow[] drFinance;
                        string lowPDate = "";
                        drFinance = _ds관종재정.Tables[0].Select("STOCK_CODE = '" + 종목코드 + "'");
                        if (drFinance.Length < 1) return;

                        lowPDate = String.Format("{0}-{1}-{2}", drFinance[0]["LOW_250_DAY"].ToString().Substring(0, 4), drFinance[0]["LOW_250_DAY"].ToString().Substring(4, 2), drFinance[0]["LOW_250_DAY"].ToString().Substring(6, 2));
                        
                        //if (DateTime.Now.AddMonths(-1) <= Convert.ToDateTime(lowPDate + " 00:00:00") && Convert.ToDecimal(drFinance[0]["LOW_250_RATE"].ToString()) > 50) return;
                        //else if (DateTime.Now.AddMonths(-2) <= Convert.ToDateTime(lowPDate + " 00:00:00") && Convert.ToDecimal(drFinance[0]["LOW_250_RATE"].ToString()) > 100) return;
                        //else if (DateTime.Now.AddMonths(-3) <= Convert.ToDateTime(lowPDate + " 00:00:00") && Convert.ToDecimal(drFinance[0]["LOW_250_RATE"].ToString()) > 150) return;
                        //else if (DateTime.Now.AddMonths(-4) <= Convert.ToDateTime(lowPDate + " 00:00:00") && Convert.ToDecimal(drFinance[0]["LOW_250_RATE"].ToString()) > 200) return;
                        //else if (Convert.ToDecimal(drFinance[0]["LOW_250_RATE"].ToString()) > 300) return;
                        
                        if (DateTime.Now.AddMonths(-1) <= Convert.ToDateTime(lowPDate + " 00:00:00") && Convert.ToDecimal(drFinance[0]["LOW_250_RATE"].ToString()) > 50) return;
                        else if (Convert.ToDecimal(drFinance[0]["LOW_250_RATE"].ToString()) > 100) return;
                        if (Convert.ToDecimal(drFinance[0]["CREDIT_RATE"].ToString()) > 7) return;

                        if (시가 > 매수가1차 && 현재가 > 매수가1차) { percent = (현재가 - 매수가1차) / 매수가1차 * 100; mPrice = 매수가1차; }
                        else if (시가 > 매수가2차 && 현재가 > 매수가2차) { percent = (현재가 - 매수가2차) / 매수가2차 * 100; mPrice = 매수가2차; }
                        else
                        {
                            
                            if (
                                dg조건종목.Rows[e.RowIndex].Cells["C3차매수가"].Value != null
                                && dg조건종목.Rows[e.RowIndex].Cells["C3차매수가"].Value.ToString().Trim() != ""
                                && dg조건종목.Rows[e.RowIndex].Cells["C3차매수가"].Value.ToString().Trim() != "0")
                            {
                                Double 매수가3차 = Double.Parse(dg조건종목.Rows[e.RowIndex].Cells["C3차매수가"].Value.ToString().Trim());
                                percent = (현재가 - 매수가3차) / 매수가3차 * 100;
                                mPrice = 매수가3차;
                            }

                        }

                        if (dg조건종목.Rows[e.RowIndex].Cells["C거래량"].Value == null || dg조건종목.Rows[e.RowIndex].Cells["C거래량"].Value.ToString() == "") return;
                        Decimal 거래량 = Decimal.Parse(dg조건종목.Rows[e.RowIndex].Cells["C거래량"].Value.ToString());
                        if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 10:00:00")) && 거래량 <= 20000) return;
                        else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 11:00:00")) && 거래량 <= 30000) return;
                        else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 12:00:00")) && 거래량 <= 40000) return;
                        else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 13:00:00")) && 거래량 <= 50000) return;
                        else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 14:00:00")) && 거래량 <= 60000) return;
                        else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:00:00")) && 거래량 <= 70000) return;
                        else if (DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:00:00")) && 거래량 <= 75000) return; 

                        if (mPrice > Double.Parse(dg조건종목.Rows[e.RowIndex].Cells["C3일선"].Value.ToString().Trim())) mPrice = Double.Parse(dg조건종목.Rows[e.RowIndex].Cells["C3일선"].Value.ToString().Trim());
                        if (percent < Double.Parse(nmCMonitor.Value.ToString()))
                        {
                            if (Cls.isExistsRow(dgN관종, 0, 종목코드) == -1 && Cls.isExistsRow(dgN관종, 0, 종목코드) == -1 && chk조.Checked == true && chk자동매매.Checked == true)
                            {
                                txt관종.Text = 종목명;
                                txt관종_KeyDown(txt관종, new KeyEventArgs(Keys.Enter));

                                if (dgN관종.Rows[0].Cells["P종목코드"].Value != null && dgN관종.Rows[0].Cells["P종목코드"].Value.ToString() == 종목코드)
                                {
                                    dgN관종.Rows[0].Cells["P모니터링가격"].Value = mPrice;
                                    dgN관종.Rows[0].Cells["P모니터링구분"].Value = "조";
                                }
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
            dg조건종목.Rows[e.RowIndex].DefaultCellStyle.BackColor = preColor;
        }

        private void btnDiscon_Click(object sender, EventArgs e)
        {

        }


        private void UcMainStockVer2_OnEventConnect(string status)
        {
            UcMainStockVer2.GetAccount();

            foreach (DataRow dr in UcMainStockVer2._AccNo.Tables["ACCNO"].Rows)
            {
                cboAccount.Items.Add(dr["ACCNO"].ToString().Trim());
            }
            cboAccount.SelectedIndex = 0;
            //tmrDaum.Stop();
            tmrNaver.Stop();
            tmrDart.Start();
            tmrDartKrx.Start();
            //tmrDart1.Start();

            UcMainStockVer2.Opw00018_OnReceiveTrData(cboAccount.Text.Trim());
            UcMainStockVer2.Opt10075_OnReceiveChejanData(cboAccount.Text.Trim());

            //UcMainStockVer2.Opt10085_OnReceiveChejanData(cboAccount.Text.Trim(), "0998");


            if (cboAccount.Text == "3228538611" || cboAccount.Text == "8087774611")
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

            if (_stockId == "000003" || _stockId == "000005") //실계좌일때는 정상 셋팅한다.
            {
                nm조건시간.Value = 4;
                nm조건체결강도.Value = (decimal)1.7;
                nm조건거래대금.Value = 21;
                tmrDart.Interval = 400;
                cboTickStd.SelectedIndex = 1;
                nmDHoga.Value = 5;
                nmYHoga.Value = -2;
                nmTickCount.Value = 15;
            }

            //txtTmDaum1.Text = tmrDaum.Interval.ToString();
            txtTmNaver2.Text = tmrNaver.Interval.ToString();
            txtTmDart3.Text = tmrDart.Interval.ToString();
            dgUcReal.DataSource = UcMainStockVer2._DtScreenNoManage;
            dgLog.DataSource = UcMainStockVer2._sLogger;
            dg화면관리.DataSource = _dt화면관리;

            dgUcReal.AutoGenerateColumns = true;
            dgLog.AutoGenerateColumns = true;
            dg화면관리.AutoGenerateColumns = true;

            
            UcHogaWindowNew1.MainStock = UcMainStockVer2;
            ucFinance1.UcStockMain = UcMainStockVer2;
            ucStockVolumeAnalysis1.MainStock = UcMainStockVer2;
            ucStockAvgMagipPrice1.MainStock = UcMainStockVer2;
            UcMainStockVer2.btnLoggerStart.PerformClick();

            _ds관종일봉 = _DataAcc.p_stock_day_data_query_Line2("2", _stockId, false, null, null);
            _ds관종재정 = _DataAcc.p_stock_finance_query("2", _stockId, false, null, null);

            SetTickInit();
            btnStopLoss.PerformClick();
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

                foreach (DataRow drTick in _dsTickInit.Tables[0].Rows)
                {
                    if (drTick["STOCK_CODE"].ToString().Trim() == "") continue;
                    tickRow = _dsTick60.Tables[drTick["STOCK_CODE"].ToString().Trim()].Rows.Add();
                    tickRow["일자"] = drTick["STOCK_DATE"];
                    tickRow["현재가"] = drTick["C_PRICE"];
                    tickRow["시가"] = drTick["S_PRICE"];
                    tickRow["고가"] = drTick["H_PRICE"];
                    tickRow["저가"] = drTick["L_PRICE"];
                    tickRow["등락율"] = drTick["RATE"];
                    tickRow["체결강도"] = drTick["POWER_RATE"];
                    tickRow["매수거래량"] = drTick["BUY_VOLUME"];
                    tickRow["매도거래량"] = drTick["SELL_VOLUME"];
                    tickRow["매수거래비용"] = drTick["BUY_TRADING_P"];
                    tickRow["매도거래비용"] = drTick["SELL_TRADING_P"];
                    tickRow["시작시간"] = drTick["S_TIME"];
                    tickRow["종료시간"] = drTick["E_TIME"];
                    tickRow["LINE5"] = drTick["LINE5"];
                    tickRow["LINE10"] = drTick["LINE10"];
                    tickRow["LINE20"] = drTick["LINE20"];
                    tickRow["LINE40"] = drTick["LINE40"];
                    tickRow["LINE60"] = drTick["LINE60"];
                    tickRow["COUNT"] = 30;
                }
            }

        }
        private void GetFavList(string InterId)
        {
            dg관종.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(dg관종_CellValueChanged);

            string stockCodes = "";
            dg관종.RowCount = 1;

            DataGridViewRow dgv;
            DataSet ds;

            if (InterId == "A")
            {
                ds = _DataAcc.p_Psi02Query("4", _stockId, 0, "", "", "", null, null);
            }
            else
            {
                ds = _DataAcc.p_Psi02Query("1", _stockId, Convert.ToInt32(InterId), "", "", "", null, null);
            }


            Decimal ret;
            if (ds.Tables[0].Rows.Count < 1) return;

            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (UcMainStockVer2._allStockDataset == null) return;
                    if (dg관종.Rows[0] == null) return;

                    //dgv = (DataGridViewRow)dg관종.Rows[0].Clone();
                    dg관종.RowCount += 1;
                    dgv = dg관종.Rows[dg관종.RowCount - 2];
                    dgv.Cells["F_삭제"].Value = "D";
                    dgv.Cells["F_종목코드"].Value = dr["STOCK_CODE"].ToString().Trim();
                    dgv.Cells["F_종목명"].Value = UcMainStockVer2._allStockDataset.Tables["STOCKLIST"].Select("STOCK_CODE = '" + dr["STOCK_CODE"].ToString().Trim() + "'")[0]["STOCK_NAME"].ToString().Trim();

                    DataTable dt = SetLine(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgv.Cells["H_3일선"].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                        dgv.Cells["H_5일선"].Value = dt.Rows[0]["line5"].ToString(); //H_5일선
                        dgv.Cells["H_10일선"].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                        dgv.Cells["H_15일선"].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                        dgv.Cells["H_20일선"].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                        dgv.Cells["H_40일선"].Value = dt.Rows[0]["line40"].ToString();//H_40일선
                        dgv.Cells["H_60일선"].Value = dt.Rows[0]["line60"].ToString();//H_60일선
                        dgv.Cells["F_세력선_H"].Value = (Decimal.TryParse(dt.Rows[0]["MA_H"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_H"].ToString()).ToString("###,###,##0") : "");//MA_H
                        dgv.Cells["F_세력선_C"].Value = (Decimal.TryParse(dt.Rows[0]["MA_C"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_C"].ToString()).ToString("###,###,##0") : "");//MA_C
                        dgv.Cells["F_세력선_L"].Value = (Decimal.TryParse(dt.Rows[0]["MA_L"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_L"].ToString()).ToString("###,###,##0") : "");//MA_L
                        dgv.Cells["F_B상한"].Value = (Decimal.TryParse(dt.Rows[0]["B_UP"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["B_UP"].ToString()).ToString("###,###,##0.00") : "");//B_UP
                        dgv.Cells["F_B하한"].Value = (Decimal.TryParse(dt.Rows[0]["B_DOWN"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["B_DOWN"].ToString()).ToString("###,###,##0.00") : "");//B_DOWN
                        if (Decimal.TryParse(dgv.Cells["F_B상한"].Value.ToString(), out ret) != false && Decimal.TryParse(dgv.Cells["F_B하한"].Value.ToString(), out ret) != false)
                        {
                            dgv.Cells["F_BB이격"].Value = ((Decimal.Parse(dgv.Cells["F_B상한"].Value.ToString()) - Decimal.Parse(dgv.Cells["F_B하한"].Value.ToString())) / Decimal.Parse(dgv.Cells["F_B하한"].Value.ToString()) * 100).ToString("#0.0");
                        }
                    }

                    dt = SetFinance(dr["STOCK_CODE"].ToString().Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgv.Cells["F_신용비율"].Value = dt.Rows[0]["CREDIT_RATE"].ToString();
                        dgv.Cells["F_시가총액"].Value = dt.Rows[0]["STOCK_TOTAL_P"].ToString();
                        dgv.Cells["F_PER"].Value = dt.Rows[0]["PER"].ToString();
                        dgv.Cells["F_ROE"].Value = dt.Rows[0]["ROE"].ToString();
                        dgv.Cells["F_PBR"].Value = dt.Rows[0]["PBR"].ToString();
                        dgv.Cells["F_EV"].Value = dt.Rows[0]["EV"].ToString();
                        dgv.Cells["F_EPS"].Value = dt.Rows[0]["EPS"].ToString();
                        dgv.Cells["F_BPS"].Value = dt.Rows[0]["BPS"].ToString();
                        dgv.Cells["F_영업이익"].Value = dt.Rows[0]["O_PROFIT"].ToString();
                        dgv.Cells["F_당기순이익"].Value = dt.Rows[0]["P_PROFIT"].ToString();
                        dgv.Cells["F_250최고가"].Value = dt.Rows[0]["HIGH_250"].ToString();
                        dgv.Cells["F_250최저가"].Value = dt.Rows[0]["LOW_250"].ToString();

                        try
                        {
                            if (Decimal.Parse(dgv.Cells["F_신용비율"].Value.ToString()) > 5) dgv.Cells["F_신용비율"].Style.ForeColor = System.Drawing.Color.Red;
                            else dgv.Cells["F_신용비율"].Style.ForeColor = System.Drawing.Color.Empty;
                            if (Decimal.Parse(dgv.Cells["F_PBR"].Value.ToString()) > 3) dgv.Cells["F_PBR"].Style.ForeColor = System.Drawing.Color.Red;
                            else dgv.Cells["F_PBR"].Style.ForeColor = System.Drawing.Color.Empty;
                            if (Decimal.Parse(dgv.Cells["F_영업이익"].Value.ToString()) < 0) dgv.Cells["F_영업이익"].Style.ForeColor = System.Drawing.Color.Red;
                            else dgv.Cells["F_영업이익"].Style.ForeColor = System.Drawing.Color.Empty;
                            if (Decimal.Parse(dgv.Cells["F_당기순이익"].Value.ToString()) < 0) dgv.Cells["F_당기순이익"].Style.ForeColor = System.Drawing.Color.Red;
                            else dgv.Cells["F_당기순이익"].Style.ForeColor = System.Drawing.Color.Empty;
                        }
                        catch { }
                    }

                    if (dr["MONITOR_YN"].ToString().Trim() != "")
                    {
                        dgv.Cells["F_모니터링"].Value = dr["MONITOR_YN"].ToString().Trim();
                    }
                    //dg관종.Rows.Add(dgv);

                    //if (DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:00:00")) || DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00"))) {
                    //    SetDsScreenNo("A", "4", "1", dr["STOCK_CODE"].ToString().Trim(), dr["STOCK_NAME"].ToString().Trim(), true);
                    //    SystemSleep(); 
                    //}
                    stockCodes += dr["STOCK_CODE"].ToString().Trim() + ";";
                }

                if (
                    (Cls.GetWeekOfDay(DateTime.Now) == "토" || Cls.GetWeekOfDay(DateTime.Now) == "일") ||
                    DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:00:00")) || DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00"))
                    )
                {
                    ucStockAvgMagipPrice1.PropWriteStockList10007 = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dg관종);
                }
                else if (DateTime.Now >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:00:00")) && DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00")))
                {
                    SetDsScreenNo("A", "4", "1", stockCodes, "", true);
                }
            }
            finally
            {
                _DataAcc.DisConnect();
                dg관종.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dg관종_CellValueChanged);
            }
        }

        private void SetNewsOrder(DataSet ds)
        {
            for (int row = 0; row < dg뉴스체결.RowCount - 1; row++)
            {
                if (dg뉴스체결.Rows[row].Cells["N종목코드"].Value.ToString().Trim() == ds.Tables[0].Rows[0]["종목코드"].ToString().Trim().Substring(1))
                {
                    if (ds.Tables[0].Rows[0]["체결가"].ToString().Trim() == "")
                    {
                        dg뉴스체결.Rows[row].Cells["N주문가"].Value = Math.Abs(Double.Parse(ds.Tables[0].Rows[0]["주문가격"].ToString().Trim()));
                        dg뉴스체결.Rows[row].Cells["N주문번호"].Value = ds.Tables[0].Rows[0]["주문번호"].ToString().Trim();
                        dg뉴스체결.Rows[row].Cells["N매수수량"].Value = ds.Tables[0].Rows[0]["주문수량"].ToString().Trim();
                    }
                    else
                    {
                        dg뉴스체결.Rows[row].Cells["N주문가"].Value = Math.Abs(Double.Parse(ds.Tables[0].Rows[0]["체결가"].ToString().Trim()));
                        dg뉴스체결.Rows[row].Cells["N주문번호"].Value = ds.Tables[0].Rows[0]["주문번호"].ToString().Trim();
                        dg뉴스체결.Rows[row].Cells["N매수수량"].Value = ds.Tables[0].Rows[0]["주문수량"].ToString().Trim();
                    }
                    return;
                }
            }
        }

        private void UcMainStockVer2_OnReceiveChejanData(DataSet ds)
        {
            //    //Public ChejanFidList(,) As String = {{"9201", "계좌번호"}, {"9203", "주문번호"}, {"9001", "종목코드"}, {"913", "주문상태"}, {"302", "종목명"}, {"900", "주문수량"}, _
            //    //                        {"901", "주문가격"}, {"902", "미체결수량"}, {"903", "체결누계금액"}, {"904", "원주문번호"}, {"905", "주문구분"}, {"906", "매매구분"}, _
            //    //                        {"907", "매도수구분"}, {"908", "주문/체결시간"}, {"909", "체결번호"}, {"910", "체결가"}, {"911", "체결량"}, {"10", "현재가"}, _
            //    //                        {"27", "(최우선)매도호가"}, {"28", "(최우선)매수호가"}, {"914", "단위체결가"}, {"915", "단위체결량"}, {"919", "거부사유"}, _
            //    //                        {"920", "화면번호"}, {"917", "신용구분"}, {"916", "대출일"}, {"930", "보유수량"}, {"931", "매입단가"}, {"932", "총매입가"}, _
            //    //                        {"933", "주문가능수량"}, {"945", "당일순매수수량"}, {"946", "매도/매수구분"}, {"950", "당일총매도손일"}, {"951", "예수금"}, _
            //    //                        {"307", "기준가"}, {"8019", "손익율"}, {"957", "신용금액"}, {"958", "신용이자"}, {"918", "만기일"}, {"990", "당일실현손익(유가)"}, _
            //    //                        {"991", "당일실현손익률(유가)"}, {"992", "당일실현손익(신용)"}, {"993", "당일실현손익률(신용)"}, {"397", "파생상품거래단위"}, _
            //    //                        {"305", "상한가"}, {"306", "하한가"}}
            //    //private string[,] _미체결 = 
            //    //{ {"주문구분" , "System.String"} , {"주문번호" , "System.String"}, {"종목코드" , "System.String"} , {"종목명","System.String"} , {"주문가격" , "System.Int32"}, {"주문수량" , "System.Int32"}, {"미체결수량" , "System.Int32"}, {"주문금액" , "System.Int32"}};
            string str = "";
            DataRow dr;
            DataRow drMi;
            DataRow[] drMiArr;
            DataRow[] drArray;
            try
            {
                dr = ds.Tables[0].Rows[0];

                if (dr["주문번호"].ToString().Trim() == "") return;

                for (int col = 0; col < ds.Tables[0].Columns.Count - 1; col++)
                {
                    str += ds.Tables[0].Columns[col].ColumnName + " : " + dr[col].ToString().Trim() + " | ";
                }
                Console.WriteLine(str);
                drArray = _dt보유잔고.Select(String.Format("종목번호 LIKE '%{0}%'", dr["종목코드"].ToString().Trim()));

                if (dr["주문상태"].ToString().Trim() == "접수" && dr["주문구분"].ToString().Trim() != "매수취소" && dr["주문구분"].ToString().Trim() != "매도취소")
                {
                    if (dr["미체결수량"].ToString().Trim() != "0")
                    {
                        drMi = _dt미체결.Rows.Add();
                        drMi["주문구분"] = dr["주문구분"];
                        drMi["주문번호"] = dr["주문번호"];
                        drMi["종목코드"] = dr["종목코드"];
                        drMi["종목명"] = dr["종목명"];
                        drMi["주문가격"] = dr["주문가격"];
                        drMi["주문수량"] = dr["주문수량"];
                        drMi["미체결수량"] = dr["미체결수량"];
                        drMi["주문금액"] = Int32.Parse(dr["주문가격"].ToString().Trim()) * Int32.Parse(dr["주문수량"].ToString().Trim());

                        if (dr["주문구분"].ToString().Trim() == "-매도") //매도시에 매매가능수량 계산
                        {
                            if (drArray.Length < 1) btn잔고.PerformClick();
                            drArray[0]["매매가능수량"] = Decimal.Parse(drArray[0]["매매가능수량"].ToString()) - Decimal.Parse(dr["주문수량"].ToString());
                        }
                        else
                        {
                            SetNewsOrder(ds);
                            //btn잔고.PerformClick();
                        }
                    }
                }
                else if (dr["주문상태"].ToString().Trim() == "확인")
                {
                    if (dr["주문구분"].ToString().Trim() == "매수취소" || dr["주문구분"].ToString().Trim() == "매도취소")
                    {
                        drMiArr = _dt미체결.Select(String.Format("주문번호 = '{0}'", dr["원주문번호"]));
                        if (drMiArr.Length > 0)
                        {
                            drMi = drMiArr[0];
                            if (Int32.Parse(dr["미체결수량"].ToString().Trim()) > 0) drMi["미체결수량"] = dr["미체결수량"];
                            else
                            {
                                if (dr["주문구분"].ToString().Trim() == "매도취소")
                                {
                                    if (drArray.Length > 0)
                                    {
                                        drArray[0]["매매가능수량"] = Decimal.Parse(drArray[0]["매매가능수량"].ToString().Trim()) + Decimal.Parse(drMi["미체결수량"].ToString().Trim());
                                    }
                                }
                                _dt미체결.Rows.Remove(drMi);
                            }
                        }

                    }
                }
                else if (dr["주문상태"].ToString().Trim() == "체결")
                {

                    drMiArr = _dt미체결.Select(String.Format("주문번호 = '{0}'", dr["주문번호"]));
                    if (drMiArr.Length > 0)
                    {
                        drMi = drMiArr[0];
                        if (Int32.Parse(dr["미체결수량"].ToString().Trim()) > 0) drMi["미체결수량"] = dr["미체결수량"];
                        else _dt미체결.Rows.Remove(drMi);
                    }

                    if (dr["주문구분"].ToString().Trim() == "-매도") //매도시에 보유수량 계산
                    {
                        if (drArray.Length < 1) btn잔고.PerformClick();
                        drArray[0]["보유수량"] = (
                            Decimal.Parse(drArray[0]["보유수량"].ToString()) - Decimal.Parse(dr["단위체결량"].ToString())
                            ).ToString("###,###,###,##0");
                        drArray[0]["매입금액"] = (Decimal.Parse(drArray[0]["매입가"].ToString()) * Decimal.Parse(drArray[0]["보유수량"].ToString())).ToString("###,###,###,##0");
                        if (drArray[0]["현재가"].ToString().Trim() != "")
                        {
                            drArray[0]["평가금액"] = (Decimal.Parse(drArray[0]["현재가"].ToString()) * Decimal.Parse(drArray[0]["보유수량"].ToString())).ToString("###,###,###,##0");
                        }
                    }
                    else
                    {
                        if (drArray.Length < 1)
                        {
                            DataRow dr신규잔고 = _dt보유잔고.Rows.Add();
                            dr신규잔고["종목번호"] = dr["종목코드"].ToString().Trim();
                            dr신규잔고["종목명"] = dr["종목명"].ToString().Trim();
                            dr신규잔고["매입가"] = Decimal.Parse(dr["체결가"].ToString().Trim()).ToString("###,###,###,##0"); ;
                            dr신규잔고["매입금액"] = (Decimal.Parse(dr["체결가"].ToString().Trim()) * Decimal.Parse(dr["단위체결량"].ToString().Trim())).ToString("###,###,###,##0");
                            dr신규잔고["보유수량"] = Decimal.Parse(dr["단위체결량"].ToString().Trim()).ToString("###,###,###,##0");
                            dr신규잔고["매매가능수량"] = Decimal.Parse(dr["단위체결량"].ToString().Trim()).ToString("###,###,###,##0");
                            SetDsScreenNo("A", "2", "1", dr["종목코드"].ToString().Trim().Substring(1), dr["종목명"].ToString().Trim(), false);
                            SystemSleep();
                        }
                        else
                        {
                            drArray[0]["매매가능수량"] = (
                                Decimal.Parse(drArray[0]["매매가능수량"].ToString()) + Decimal.Parse(dr["단위체결량"].ToString())
                                ).ToString("###,###,###,##0");
                            drArray[0]["보유수량"] = (
                                Decimal.Parse(drArray[0]["보유수량"].ToString()) + Decimal.Parse(dr["단위체결량"].ToString())
                                ).ToString("###,###,###,##0");
                            drArray[0]["매입금액"] = (Decimal.Parse(drArray[0]["매입금액"].ToString()) + Decimal.Parse(dr["체결가"].ToString().Trim()) * Decimal.Parse(dr["단위체결량"].ToString().Trim())).ToString("###,###,###,##0");
                            drArray[0]["매입가"] = Math.Floor(Decimal.Parse(drArray[0]["매입금액"].ToString()) / Decimal.Parse(drArray[0]["보유수량"].ToString()));
                            if (drArray[0]["현재가"].ToString().Trim() != "")
                            {
                                drArray[0]["평가금액"] = (Decimal.Parse(drArray[0]["현재가"].ToString()) * Decimal.Parse(drArray[0]["보유수량"].ToString())).ToString("###,###,###,##0");
                            }
                            SetNewsOrder(ds);
                        }
                    }

                }
                if (drArray.Length > 0)
                {
                    if (Convert.ToInt32(drArray[0]["보유수량"].ToString().Replace(",", "")) == 0)
                    {
                        _dt보유잔고.Rows.Remove(drArray[0]);
                        SetDsScreenNo("D", "2", "1", dr["종목코드"].ToString().Trim().Substring(1), dr["종목명"].ToString().Trim(), false);
                    }
                }
                Logger(dr["주문상태"].ToString().Trim(), str);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

        private void UcMainStockVer2_OnReceiveRealCondition(DataSet ds)
        {
            string 종목코드 = "";
            string 종목명 = "";
            string 구분 = "";
            Decimal ret;
            bool 존재여부 = false;
            if (ds == null) { return; }
            if (ds.Tables["CondiStockReal"].Rows.Count < 1) { return; }
            DataRow dr = ds.Tables[0].Rows[0];
            종목코드 = dr["STOCK_CODE"].ToString().Trim();
            종목명 = dr["STOCK_NAME"].ToString().Trim();
            구분 = dr["STR_TYPE"].ToString().Trim();
            int NRow = -1;
            for (int row = dg조건종목.RowCount - 2; row >= 0; row--)
            {
                //strType : 편입(“I”), 이탈(“D”) 
                if (dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString().Trim() == 종목코드)
                {
                    if (구분 == "D")
                    {
                        dg조건종목.Rows[row].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        NRow = Cls.isExistsRow(dgN관종, 0, dr["STOCK_CODE"].ToString().Trim());
                        if (NRow > -1)
                        {
                            dgN관종.Rows.RemoveAt(NRow);
                        }
                    }
                    else
                    {
                        dg조건종목.Rows[row].DefaultCellStyle.BackColor = System.Drawing.Color.Empty;
                    }
                    존재여부 = true;
                }
            }
            if (존재여부 == false)
            {
                if (구분 == "I")
                {
                    dg조건종목.Rows.Insert(0, 1);
                    dg조건종목.Rows[0].Cells["C종목코드"].Value = 종목코드;
                    dg조건종목.Rows[0].Cells["C종목명"].Value = dr["STOCK_NAME"].ToString();
                    try
                    {
                        DataTable dt = SetLine(종목코드);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            dg조건종목.Rows[0].Cells["CH3일선"].Value = dt.Rows[0]["line3"].ToString(); //H_3일선
                            dg조건종목.Rows[0].Cells["CH5일선"].Value = dt.Rows[0]["line5"].ToString();//H_5일선
                            dg조건종목.Rows[0].Cells["CH10일선"].Value = dt.Rows[0]["line10"].ToString();//H_10일선
                            dg조건종목.Rows[0].Cells["CH15일선"].Value = dt.Rows[0]["line15"].ToString();//H_15일선
                            dg조건종목.Rows[0].Cells["CH20일선"].Value = dt.Rows[0]["line20"].ToString();//H_20일선
                            dg조건종목.Rows[0].Cells["CH40일선"].Value = dt.Rows[0]["line40"].ToString();//H_20일선
                            dg조건종목.Rows[0].Cells["CH60일선"].Value = dt.Rows[0]["line60"].ToString();//H_20일선
                            dg조건종목.Rows[0].Cells["C세력선_H"].Value = (Decimal.TryParse(dt.Rows[0]["MA_H"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_H"].ToString()).ToString("###,###,##0") : "");//MA_H
                            dg조건종목.Rows[0].Cells["C세력선_C"].Value = (Decimal.TryParse(dt.Rows[0]["MA_C"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_C"].ToString()).ToString("###,###,##0") : "");//MA_C
                            dg조건종목.Rows[0].Cells["C세력선_L"].Value = (Decimal.TryParse(dt.Rows[0]["MA_L"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["MA_L"].ToString()).ToString("###,###,##0") : "");//MA_L
                            dg조건종목.Rows[0].Cells["CB상한"].Value = (Decimal.TryParse(dt.Rows[0]["B_UP"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["B_UP"].ToString()).ToString("###,###,##0.00") : "");//B_UP
                            dg조건종목.Rows[0].Cells["CB하한"].Value = (Decimal.TryParse(dt.Rows[0]["B_DOWN"].ToString(), out ret) ? Decimal.Parse(dt.Rows[0]["B_DOWN"].ToString()).ToString("###,###,##0.00") : "");//B_DOWN
                        }
                        SetConditionPrice(0);
                        SetDsScreenNo("A", "3", "1", 종목코드, dr["STOCK_NAME"].ToString().Trim(), false);
                        _DataAcc.p_Psi02Add("A", _stockId, 4, 종목코드, "00", "", "B하한", null, null);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                    finally
                    {
                        _DataAcc.DisConnect();
                    }
                }
            }

        }

        private void UcMainStockVer2_OnReceiveRealData_ExpectVolume(DataSet ds)
        {

        }

        private void UcMainStockVer2_OnReceiveRealData_HogaJanQty(DataSet ds)
        {
            if (ds == null) return;
            if (ds.Tables.Count == 0) return;

            if (UcHogaWindowNew1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
            {
                UcHogaWindowNew1.Property_GetStockHogaJanQty = ds;
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

            if (UcHogaWindowNew1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
            {
                UcHogaWindowNew1.Property_ToDayStockTradeAt = ds;
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

        private void UcMainStockVer2_OnReceiveTrCondition(DataSet ds)
        {
            SetConditionList(ds);
        }

        private void UcMainStockVer2_OnReceiveTrData_Opt10001(DataSet ds)
        {
            SettingOPT10001(ds);
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
            SettingOPT10006(ds);
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
            DataRow dr;
            string colName;
            _dt보유잔고.Rows.Clear();

            ds = ConvertDsNumber(ds, 2);
            for (int row = 0; row <= ds.Tables[0].Rows.Count - 1; row++)
            {
                dr = _dt보유잔고.Rows.Add();
                for (int col = 0; col <= ds.Tables[0].Columns.Count - 1; col++)
                {
                    colName = ds.Tables[0].Columns[col].ColumnName;
                    if (_dt보유잔고.Columns[colName] == null) continue;
                    dr[colName] = ds.Tables[0].Rows[row][col].ToString().Trim();
                }

                //SetDsScreenNo("A", "2", "1", dr["종목번호"].ToString().Trim().Substring(1), dr["종목명"].ToString().Trim(), false);

                strStockCodes += dr["종목번호"].ToString().Trim().Substring(1) + ";";
            }

            SetDsScreenNo("A", "2", "1", strStockCodes, "", false);
            SystemSleep();

            dg뉴스잔고.DataSource = _dt보유잔고;
            // Automatically generate the DataGridView columns.
            dg뉴스잔고.AutoGenerateColumns = true;
        }
        private void SystemSleep()
        {
            System.Threading.Thread.Sleep(_SLEEP_TIME);
            //int i = 0;
            //while (i < _SLEEP_TIME)
            //{
            //    Application.DoEvents();
            //    System.Threading.Thread.Sleep(1);
            //    i++;
            //}
        }



        private void dg관종_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            
            if (dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value == null || dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value.ToString() == "") return;
            if (e.ColumnIndex == F_거래량.Index)
            {
                dg관종.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                if (Int32.Parse(DateTime.Now.ToString("HH")) <= 16 && Int32.Parse(DateTime.Now.ToString("HH")) >= 9) Application.DoEvents();
            }

            if (e.ColumnIndex == F_대비.Index ) //대비
            {
                if (Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_대비"].Value.ToString()) < 0)
                {
                    dg관종.Rows[e.RowIndex].Cells["F_현재가"].Style.ForeColor = System.Drawing.Color.Blue;
                    dg관종.Rows[e.RowIndex].Cells["F_대비"].Style.ForeColor = System.Drawing.Color.Blue;
                    dg관종.Rows[e.RowIndex].Cells["F_등락율"].Style.ForeColor = System.Drawing.Color.Blue;
                }
                else if (Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_대비"].Value.ToString()) > 0)
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

            if (e.ColumnIndex == F_모니터링.Index)
            {
                int interId = 0;
                if (rb1.Checked == true) interId = 1;
                else if (rb2.Checked == true) interId = 2;
                else if (rb3.Checked == true) interId = 3;
                else if (rb4.Checked == true) interId = 4;
                else if (rb5.Checked == true) interId = 5;

                _DataAcc.p_Psi02Add("U", _stockId, interId, dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value.ToString(), "00", "", dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value.ToString());
            }

            if (e.ColumnIndex == F_현재가.Index)
            {
                try
                {
                    if (dg관종.Rows[e.RowIndex].Cells["H_3일선"].Value == null) return;

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["H_3일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_3일선"].Value =
                            ((Convert.ToDecimal(dg관종.Rows[e.RowIndex].Cells["H_3일선"].Value.ToString()) + Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 3).ToString("###,###,###,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_3일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["H_5일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_5일선"].Value =
                            ((Convert.ToDecimal(dg관종.Rows[e.RowIndex].Cells["H_5일선"].Value.ToString()) + Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 5).ToString("###,###,###,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_5일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["H_10일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_10일선"].Value =
                            ((Convert.ToDecimal(dg관종.Rows[e.RowIndex].Cells["H_10일선"].Value.ToString()) + Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 10).ToString("###,###,###,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_10일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["H_15일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_15일선"].Value =
                            ((Convert.ToDecimal(dg관종.Rows[e.RowIndex].Cells["H_15일선"].Value.ToString()) + Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 15).ToString("###,###,###,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_15일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["H_20일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value =
                            ((Convert.ToDecimal(dg관종.Rows[e.RowIndex].Cells["H_20일선"].Value.ToString()) + Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 20).ToString("###,###,###,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value = "";
                    }
                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["H_40일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_40일선"].Value =
                            ((Convert.ToDecimal(dg관종.Rows[e.RowIndex].Cells["H_40일선"].Value.ToString()) + Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 40).ToString("###,###,###,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_40일선"].Value = "";
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["H_60일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_60일선"].Value =
                            ((Convert.ToDecimal(dg관종.Rows[e.RowIndex].Cells["H_60일선"].Value.ToString()) + Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString())) / 60).ToString("###,###,###,##0.00")
                            ;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_60일선"].Value = "";
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Value =
                            (
                            Convert.ToDecimal(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()) * (1 + (envRate.Value / 100))
                            ).ToString("###,###,##0.00");

                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Value = "";
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Value =
                            (
                            Convert.ToDecimal(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()) * (1 - (envRate.Value / 100))
                            ).ToString("###,###,##0.00");
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Value = "";
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_3일선"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_3일선"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_3일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_3일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_5일선"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_5일선"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_5일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_5일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_10일선"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_10일선"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_10일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_10일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_15일선"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_15일선"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_15일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_15일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_20일선"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_20일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_20일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_40일선"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_40일선"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_40일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_40일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_60일선"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_60일선"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_60일선"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_60일선"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_세력선_L"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_세력선_L"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_세력선_L"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_세력선_L"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_세력선_C"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_세력선_C"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_세력선_C"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_세력선_C"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_세력선_H"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_세력선_H"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_세력선_H"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_세력선_H"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV상한"].Style.BackColor = System.Drawing.Color.Empty;
                    }


                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_ENV하한"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_B상한"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_B상한"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_B상한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_B상한"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (Cls.IsNumeric(dg관종.Rows[e.RowIndex].Cells["F_B하한"].Value.ToString()) && Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_B하한"].Value.ToString()) <= Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString()))
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_B하한"].Style.BackColor = System.Drawing.Color.LightBlue;
                    }
                    else
                    {
                        dg관종.Rows[e.RowIndex].Cells["F_B하한"].Style.BackColor = System.Drawing.Color.Empty;
                    }

                    if (dg관종.Rows[e.RowIndex].Cells["F_거래량"].Value == null || dg관종.Rows[e.RowIndex].Cells["F_거래량"].Value.ToString() == "") return;
                    Decimal 거래량 = Decimal.Parse(dg관종.Rows[e.RowIndex].Cells["F_거래량"].Value.ToString());
                    if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 10:00:00")) && 거래량 <= 20000) return;
                    else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 11:00:00")) && 거래량 <= 30000) return;
                    else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 12:00:00")) && 거래량 <= 40000) return;
                    else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 13:00:00")) && 거래량 <= 50000) return;
                    else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 14:00:00")) && 거래량 <= 60000) return;
                    else if (DateTime.Now <= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:00:00")) && 거래량 <= 70000) return;
                    else if (DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:00:00")) && 거래량 <= 75000) return; 

                    if (dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value.ToString().Trim() != "X")
                    {
                        string stockCode = dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value.ToString();
                        string stockName = dg관종.Rows[e.RowIndex].Cells["F_종목명"].Value.ToString();
                        string colName = "F_" + dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value;
                        Double cPrice = Double.Parse(dg관종.Rows[e.RowIndex].Cells["F_현재가"].Value.ToString());
                        Double comparePrice = Double.Parse(dg관종.Rows[e.RowIndex].Cells[colName].Value.ToString());
                        Double percent = ((cPrice - comparePrice) / comparePrice * 100);

                        //if (dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value.ToString().Trim() != "B하한" &&
                        //    dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value.ToString().Trim() != "세력선_L" &&
                        //    (dg관종.Rows[e.RowIndex].Cells["F_시가"].Value == null || 
                        //        Double.Parse(dg관종.Rows[e.RowIndex].Cells["F_시가"].Value.ToString()) < comparePrice)
                        //   ) return;
                        if (percent < Double.Parse(nmFMonitor.Value.ToString()))
                        {
                            if (Cls.isExistsRow(dgN관종, 0, stockCode) == -1 && Cls.isExistsRow(dgN관종, 0, stockCode) == -1 && chk관.Checked == true && chk자동매매.Checked == true)
                            {
                                txt관종.Text = stockName;
                                txt관종_KeyDown(txt관종, new KeyEventArgs(Keys.Enter));

                                if (dgN관종.Rows[0].Cells["P종목코드"].Value != null)
                                {
                                    if (dgN관종.Rows[0].Cells["P종목코드"].Value.ToString() == stockCode)
                                    {
                                        dgN관종.Rows[0].Cells["P모니터링가격"].Value = comparePrice;
                                        dgN관종.Rows[0].Cells["P모니터링구분"].Value = colName;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger("에러 (dg관종_CellValueChanged)", ex.Message);
                }
            }
            dg관종.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.Empty;
        }

        private void dg관종_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //string 종목코드 = dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value.ToString().Trim();
            dg관종.Rows[e.RowIndex].Cells["F_모니터링"].Value = "X";
            lblTotal.Text = (dg관종.RowCount - 1).ToString();
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb1.Checked == true) GetFavList(rb1.Text.Trim());
        }

        private void 시장가매수ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string 종목코드 = "";
            int row = -1;
            int 매수수량 = 0;
            string 현재가 = "";
            if (cmsMenu.SourceControl.Name == dg뉴스잔고.Name)
            {
                if (dg뉴스잔고.CurrentRow == null) return;
                row = dg뉴스잔고.CurrentRow.Index;
                종목코드 = dg뉴스잔고.Rows[row].Cells["종목번호"].Value.ToString().Substring(1);
                현재가 = dg뉴스잔고.Rows[row].Cells["현재가"].Value.ToString().Replace(",", "");
            }
            else if (cmsMenu.SourceControl.Name == dg관종.Name)
            {
                if (dg관종.CurrentRow == null) return;
                row = dg관종.CurrentRow.Index;
                종목코드 = dg관종.Rows[row].Cells["F_종목코드"].Value.ToString();
                현재가 = dg관종.Rows[row].Cells["F_현재가"].Value.ToString().Replace("," , "");
            }
            if (종목코드 == "" || 현재가=="") return;
            매수수량 = Int32.Parse(nm관종매수금액.Value.ToString()) / Convert.ToInt32(현재가);
            SendBuySellMsg(종목코드, "03", 1, 0, 0, 매수수량, "2");
        }

        private void 시장가매도ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string 종목코드 = "";
            int row = -1;
            int 수량 = 0;
            if (cmsMenu.SourceControl.Name == dg뉴스잔고.Name)
            {
                if (dg뉴스잔고.CurrentRow == null) return;
                row = dg뉴스잔고.CurrentRow.Index;
                종목코드 = dg뉴스잔고.Rows[row].Cells["종목번호"].Value.ToString().Substring(1);
                수량 = (int)Decimal.Parse(dg뉴스잔고.Rows[row].Cells["매매가능수량"].Value.ToString());
            }

            if (종목코드 == "") return;
            SendBuySellMsg(종목코드, "03", 2, 0, 0, 수량, "2");
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
                if (dg뉴스잔고.Rows[row].Cells["종목번호"].Value == null) return;
                종목코드 = dg뉴스잔고.Rows[row].Cells["종목번호"].Value.ToString().Substring(1);
                현재가 = dg뉴스잔고.Rows[row].Cells["현재가"].Value.ToString();
                주문가 = 현재가;
                rOrder.Auto = true;
            }
            else if (cmsMenu.SourceControl.Name == dg관종.Name)
            {
                if (dg관종.CurrentRow == null) return;
                row = dg관종.CurrentRow.Index;
                col = dg관종.CurrentCell.ColumnIndex;

                if (col < 8) return;
                if (dg관종.Rows[row].Cells["F_종목코드"].Value == null) return;
                if (Cls.IsNumeric(dg관종.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg관종.Rows[row].Cells["F_종목코드"].Value.ToString();
                현재가 = dg관종.Rows[row].Cells["F_현재가"].Value.ToString();
                주문가 = MakeOrderPrice((int)Decimal.Parse(dg관종.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                if (btn관종Expand.Text == "◀") btn관종Expand.PerformClick();
            }
            else if (cmsMenu.SourceControl.Name == dg조건종목.Name)
            {
                if (dg조건종목.CurrentRow == null) return;
                row = dg조건종목.CurrentRow.Index;
                col = dg조건종목.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목.Rows[row].Cells["C종목코드"].Value == null) return;
                if (Cls.IsNumeric(dg조건종목.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString();
                현재가 = dg조건종목.Rows[row].Cells["C현재가"].Value.ToString();
                주문가 = MakeOrderPrice((int)Decimal.Parse(dg조건종목.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
                if (btn관종Expand.Text == "◀") btn관종Expand.PerformClick();
            }
            if (주문가 == "") return;
            수량 = (int)(nm관종매수금액.Value / Decimal.Parse(주문가));
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
                if (dg뉴스잔고.Rows[row].Cells["종목번호"].Value == null) return;
                종목코드 = dg뉴스잔고.Rows[row].Cells["종목번호"].Value.ToString().Substring(1);
                수량 = (int)Decimal.Parse(dg뉴스잔고.Rows[row].Cells["매매가능수량"].Value.ToString());
                현재가 = dg뉴스잔고.Rows[row].Cells["현재가"].Value.ToString();
                주문가 = 현재가;
                rOrder.Auto = true;
            }
            else if (cmsMenu.SourceControl.Name == dg관종.Name)
            {
                if (dg관종.CurrentRow == null) return;

                row = dg관종.CurrentRow.Index;
                col = dg관종.CurrentCell.ColumnIndex;

                if (col < 8) return;
                if (dg관종.Rows[row].Cells["F_종목코드"].Value == null) return;
                if (Cls.IsNumeric(dg관종.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg관종.Rows[row].Cells["F_종목코드"].Value.ToString();
                현재가 = dg관종.Rows[row].Cells["F_현재가"].Value.ToString();
                주문가 = MakeOrderPrice((int)Decimal.Parse(dg관종.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;
                if (btn관종Expand.Text == "◀") btn관종Expand.PerformClick();
            }
            else if (cmsMenu.SourceControl.Name == dg조건종목.Name)
            {
                if (dg조건종목.CurrentRow == null) return;
                row = dg조건종목.CurrentRow.Index;
                col = dg조건종목.CurrentCell.ColumnIndex;

                if (col < 10) return;
                if (dg조건종목.Rows[row].Cells["C종목코드"].Value == null) return;
                if (Cls.IsNumeric(dg조건종목.Rows[row].Cells[col].Value.ToString()) == false) return;

                종목코드 = dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString();
                현재가 = dg조건종목.Rows[row].Cells["C현재가"].Value.ToString();
                주문가 = MakeOrderPrice((int)Decimal.Parse(dg조건종목.Rows[row].Cells[col].Value.ToString())).ToString();
                rOrder.Auto = false;

                tbStockList.SelectedIndex = 3;
                if (btn관종Expand.Text == "◀") btn관종Expand.PerformClick();
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
            UcHogaWindowNew1.StockCode = 종목코드;
            UcHogaWindowNew1.ROrder = rOrder;
            UcHogaWindowNew1.SetManualOrder();
            tbStockList.SelectedIndex = 3;
        }

        private void tmrDart1_Tick_1(object sender, EventArgs e)
        {
            if (Cls.GetWeekOfDay(DateTime.Now) == "토" || Cls.GetWeekOfDay(DateTime.Now) == "일")
            {
                tmrDart1.Enabled = false;
                return;
            }
            tmrDart1.Enabled = false;
            try
            {
                if (Cls.GetWeekOfDay(DateTime.Now) == "토" ||
                    Cls.GetWeekOfDay(DateTime.Now) == "일"
                    )
                {
                    tmrDart1.Enabled = false;
                }

                GetDartDataApi();

                if (dgDartApi.RowCount > 50) { dgDartApi.Rows.RemoveAt(dgDartApi.Rows.Count - 2); }
            }
            catch (Exception ex) { Logger("에러 (tmrDart)", ex.Message); }
            finally
            {
                if (tmrDart1.Enabled == false)
                    tmrDart1.Enabled = true;
            }
        }

        private void btn관종Expand_Click(object sender, EventArgs e)
        {
            if (btn관종Expand.Text == "◀")
            {
                btn관종Expand.Text = "▶";
                UcHogaWindowNew1.Left -= UcHogaWindowNew1.Width;
                btn관종Expand.Left -= UcHogaWindowNew1.Width;
                dg관종.Width -= UcHogaWindowNew1.Width;
            }
            else
            {
                btn관종Expand.Text = "◀";
                UcHogaWindowNew1.Left += UcHogaWindowNew1.Width;
                btn관종Expand.Left += UcHogaWindowNew1.Width;
                dg관종.Width += UcHogaWindowNew1.Width;
            }
        }

        private void envRate_ValueChanged(object sender, EventArgs e)
        {
            for (int row = 0; row < dg관종.Rows.Count - 1; row++)
            {
                dg관종.Rows[row].Cells["F_ENV상한"].Value =
                            (
                                Convert.ToDecimal(dg관종.Rows[row].Cells["F_20일선"].Value.ToString()) * (1 + (envRate.Value / 100))
                            ).ToString("###,###,##0.00");
                dg관종.Rows[row].Cells["F_ENV하한"].Value =
                            (
                                Convert.ToDecimal(dg관종.Rows[row].Cells["F_20일선"].Value.ToString()) * (1 - (envRate.Value / 100))
                            ).ToString("###,###,##0.00");
            }
        }

        private void 기업정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells["종목번호"].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells["P종목코드"].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells["N종목코드"].Value.ToString();
            else if (dgObj.Name == dg조건종목.Name) stockCode = dgObj.Rows[row].Cells["C종목코드"].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells["F_종목코드"].Value.ToString();

            if (stockCode == "") return;
            ucFinance1.StockCode = stockCode;
            UcMainStockVer2.Opt10001_OnReceiveTrData(stockCode, "");
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2.Checked == true) GetFavList(rb2.Text.Trim());
        }

        private void chkPerRate_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목.Columns["C1차매수가"].Visible = chkPerRate.Checked;
            dg조건종목.Columns["C2차매수가"].Visible = chkPerRate.Checked;
            dg조건종목.Columns["C3차매수가"].Visible = chkPerRate.Checked;
        }

        private void chkAvgLine_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목.Columns["C3일선"].Visible = chkAvgLine.Checked;
            dg조건종목.Columns["C5일선"].Visible = chkAvgLine.Checked;
            dg조건종목.Columns["C10일선"].Visible = chkAvgLine.Checked;
            dg조건종목.Columns["C15일선"].Visible = chkAvgLine.Checked;
            dg조건종목.Columns["C20일선"].Visible = chkAvgLine.Checked;
            dg조건종목.Columns["C40일선"].Visible = chkAvgLine.Checked;
            dg조건종목.Columns["C60일선"].Visible = chkAvgLine.Checked;
        }

        private void chkEnvelope_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목.Columns["CENV상한"].Visible = chkEnvelope.Checked;
            dg조건종목.Columns["CENV하한"].Visible = chkEnvelope.Checked;
        }

        private void chkBolBand_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목.Columns["CB상한"].Visible = chkBolBand.Checked;
            dg조건종목.Columns["CB하한"].Visible = chkBolBand.Checked;
        }

        private void chkLine20세력_CheckedChanged(object sender, EventArgs e)
        {
            dg조건종목.Columns["C세력선_H"].Visible = chkLine20세력.Checked;
            dg조건종목.Columns["C세력선_C"].Visible = chkLine20세력.Checked;
            dg조건종목.Columns["C세력선_L"].Visible = chkLine20세력.Checked;
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3.Checked == true) GetFavList(rb3.Text.Trim());
        }

        private void chkFAvgLine_CheckedChanged(object sender, EventArgs e)
        {
            dg관종.Columns["F_3일선"].Visible = chkFAvgLine.Checked;
            dg관종.Columns["F_5일선"].Visible = chkFAvgLine.Checked;
            dg관종.Columns["F_10일선"].Visible = chkFAvgLine.Checked;
            dg관종.Columns["F_15일선"].Visible = chkFAvgLine.Checked;
            dg관종.Columns["F_20일선"].Visible = chkFAvgLine.Checked;
            dg관종.Columns["F_40일선"].Visible = chkFAvgLine.Checked;
            dg관종.Columns["F_60일선"].Visible = chkFAvgLine.Checked;
        }

        private void chkF20LineMa_CheckedChanged(object sender, EventArgs e)
        {
            dg관종.Columns["F_세력선_H"].Visible = chkF20LineMa.Checked;
            dg관종.Columns["F_세력선_C"].Visible = chkF20LineMa.Checked;
            dg관종.Columns["F_세력선_L"].Visible = chkF20LineMa.Checked;
        }

        private void chkFENV_CheckedChanged(object sender, EventArgs e)
        {
            dg관종.Columns["F_ENV상한"].Visible = chkFENV.Checked;
            dg관종.Columns["F_ENV하한"].Visible = chkFENV.Checked;
        }

        private void chkBol_CheckedChanged(object sender, EventArgs e)
        {
            dg관종.Columns["F_B상한"].Visible = chkBol.Checked;
            dg관종.Columns["F_B하한"].Visible = chkBol.Checked;
            dg관종.Columns["F_BB이격"].Visible = chkBol.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            if (cboAccount.Text == "3228538611" || cboAccount.Text == "8085884911")
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
            string 종목코드 = "";
            if (dg미체결.Rows[e.RowIndex].Cells["종목코드"].Value.ToString().Trim().Length == 6)
            {
                종목코드 = dg미체결.Rows[e.RowIndex].Cells["종목코드"].Value.ToString().Trim();
            }
            else
            {
                종목코드 = dg미체결.Rows[e.RowIndex].Cells["종목코드"].Value.ToString().Trim().Substring(1);
            }
            int 주문수량 = Int32.Parse(dg미체결.Rows[e.RowIndex].Cells["미체결수량"].Value.ToString().Trim());
            string 주문번호 = dg미체결.Rows[e.RowIndex].Cells["주문번호"].Value.ToString().Trim();
            int 구분 = -1;

            if (dg미체결.Rows[e.RowIndex].Cells["주문구분"].Value.ToString().Trim() == "+매수") 구분 = (int)ucMainStock.OrderType.매수취소;
            else if (dg미체결.Rows[e.RowIndex].Cells["주문구분"].Value.ToString().Trim() == "-매도") 구분 = (int)ucMainStock.OrderType.매도취소;

            if (e.ColumnIndex == 0) //취소버튼 클릭시
            {
                SendBuySellMsg(종목코드, "00", 구분, 0, 0, 주문수량, "2", 주문번호);
            }
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {  // 로그
                if (UcMainStockVer2._OrderResult.Tables.Count > 0)
                {
                    dgOrderList.DataSource = UcMainStockVer2._OrderResult.Tables[0];
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
            DataSet dsCondition;
            ArrayParam arr = new ArrayParam();
            ArrayParams arrs = new ArrayParams();
            string filter = "";
            ds = _DataAcc.p_Psi02Query("3", _stockId, 0, "", "", "", null, null);
            try
            {
                dsCondition = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dg조건종목);
                if (ds.Tables[0].Select(String.Format("STOCK_CODE='{0}'", dt.TableName)).Length < 1
                    && _dt보유잔고.Select(String.Format("종목번호 LIKE '%{0}%'", dt.TableName)).Length < 1
                    && dsCondition.Tables[0].Select(String.Format("STOCK_CODE='{0}'", dt.TableName)).Length < 1

                ) return; //관심종목이 아니면 Tick 을 저장하지 않는다. 용량밑 실시간 데이터 요청시 200개 한도가 있음

                arrs.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    filter = String.Format("STOCK_ID = '{0}' AND STOCK_CODE = '{1}' AND S_TIME = '{2}'", _stockId, dt.TableName, dr["시작시간"].ToString());
                    if (_dsTickInit.Tables[0].Select(filter).Length > 0) continue;
                    //if (DateTime.Now.ToString("yyyyMMdd") != dr["일자"].ToString().Trim()) { continue; }
                    arr.Clear();
                    arr.Add("STOCK_ID",_stockId);
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
                    arrs.Add(arr);
                }
                if (arrs.Count > 0) { 
                    mySqlDbConn.ExecuteNonMultiInsert("stock_tick", arrs);
                }
            }
            finally
            {
                _DataAcc.DisConnect();
            }
        }

        private void dg뉴스체결_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt관종_Enter(object sender, EventArgs e)
        {
            txt관종.SelectAll();
        }

        private void txt관종_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (UcMainStockVer2._allStockDataset == null) return;
                DataView dv = new DataView(UcMainStockVer2._allStockDataset.Tables[0]);
                dv.RowFilter = "STOCK_NAME = '" + txt관종.Text.Trim() + "'";

                bool blnTrue = false;
                if (dv.Count < 1) return;

                blnTrue = false;
                for (int row = 0; row < dgN관종.Rows.Count - 1; row++)
                {
                    if (dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim() == dv[0]["STOCK_CODE"].ToString())
                    {
                        blnTrue = true;
                        break;
                    }
                }

                if (blnTrue == false)
                {
                    //DataTable tickDt;
                    foreach (DataRowView dr in dv)
                    {
                        if (chk자동매매.Checked == true)
                        {
                            if (Cls.isExistsRow(dgN관종, 0, dr["STOCK_CODE"].ToString().Trim()) == -1 && Cls.isExistsRow(dg뉴스체결, 0, dr["STOCK_CODE"].ToString().Trim()) == -1)
                            {
                                dgN관종.Rows.Insert(0, 1);
                                dgN관종.Rows[0].Cells["P종목코드"].Value = dr["STOCK_CODE"].ToString().Trim();
                                dgN관종.Rows[0].Cells["P종목명"].Value = dr["STOCK_NAME"].ToString().Trim();
                                dgN관종.Rows[0].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                dgN관종.Rows[0].Cells["P강제추가여부"].Value = "Y";
                                dgN관종.Rows[0].Cells["P모니터링가격"].Value = "";
                                dgN관종.Rows[0].Cells["P모니터링구분"].Value = "수";
                                //if (_dsTick60.Tables[dr["STOCK_CODE"].ToString().Trim()] == null)
                                //{
                                //    cboTickDataMember.Items.Add(dr["STOCK_CODE"].ToString().Trim() + "-" + dr["STOCK_NAME"].ToString().Trim());
                                //    tickDt = _dsTick60.Tables.Add(dr["STOCK_CODE"].ToString().Trim());
                                //    for (int i = 0; i < _Tick60.Length / 2; i++)
                                //    {
                                //        tickDt.Columns.Add(_Tick60[i, 0], Type.GetType(_Tick60[i, 1]));
                                //    }
                                //}
                                //else
                                //{
                                //    _dsTick60.Tables[dr["STOCK_CODE"].ToString().Trim()].Rows.Clear();
                                //}

                                SetDsScreenNo("A", "1", "1", dr["STOCK_CODE"].ToString().Trim(), dr["STOCK_NAME"].ToString().Trim(), false);
                            }
                        }
                        //뉴스 관종에 등록 - E
                    }
                }
                txt관종.Text = "";
            }
        }

        private void dgN관종_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.D)
            {
                dgN관종.Rows.RemoveAt(dgN관종.CurrentRow.Index);
            }
        }

        private void dg뉴스체결_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.D)
            {
                if (dg뉴스체결.Rows[dg뉴스체결.CurrentRow.Index].Cells["N주문번호"].Value != null)
                {
                    SendBuySellMsg(dg뉴스체결.Rows[dg뉴스체결.CurrentRow.Index].Cells["N종목코드"].Value.ToString(), "00", (int)ucMainStock.OrderType.매수취소, 0, 0, Convert.ToInt32(dg뉴스체결.Rows[dg뉴스체결.CurrentRow.Index].Cells["N매수수량"].Value.ToString()), "2", dg뉴스체결.Rows[dg뉴스체결.CurrentRow.Index].Cells["N주문번호"].Value.ToString());
                }
                dg뉴스체결.Rows.RemoveAt(dg뉴스체결.CurrentRow.Index);
            }
        }

        private void dg뉴스잔고_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UcMainStockVer2_OnReceiveTrData_opt10075(DataSet ds)
        {
            //private string[,] _미체결 = { { "주문구분", "System.String" }, { "주문번호", "System.String" }, { "종목코드", "System.String" }, { "종목명", "System.String" }, { "주문가격", "System.Int32" }, { "주문수량", "System.Int32" }, { "미체결수량", "System.Int32" }, { "주문금액", "System.Int32" }};
            //DataTable dt접수 = ds.Tables[0].Clone();
            //DataTable dt확인 = ds.Tables[0].Clone();
            //DataTable dt체결 = ds.Tables[0].Clone();

            //DataRow[] dr접수 = ds.Tables[0].Select("주문상태 = '접수'");
            //DataRow[] dr확인 = ds.Tables[0].Select("주문상태 = '확인'");
            //DataRow[] dr체결 = ds.Tables[0].Select("주문상태 = '체결'");
            //DataRow[] tempRow;

            //foreach(DataRow dr in dr접수) {
            //    dt접수.Rows.Add(dr.ItemArray);
            //}

            //foreach (DataRow dr in dr확인)
            //{
            //    tempRow = dt접수.Select(String.Format("주문번호 = '{0}'", dr["원주문번호"].ToString().Trim()));
            //    if (tempRow.Length < 1 ) continue;
            //    tempRow[0]["주문수량"] = Int32.Parse(tempRow[0]["주문수량"].ToString()) - Int32.Parse(dr["주문수량"].ToString());
            //    if (Int32.Parse(tempRow[0]["주문수량"].ToString()) == 0)
            //    {
            //        dt접수.Rows.Remove(tempRow[0]);
            //    }
            //}

            DataRow tRow;
            foreach (DataRow dr in ds.Tables["미체결"].Rows)
            {
                tRow = _dt미체결.Rows.Add();
                tRow["주문구분"] = dr["주문구분"];
                tRow["주문번호"] = dr["주문번호"];
                tRow["종목코드"] = dr["종목코드"];
                tRow["종목명"] = dr["종목명"];
                tRow["주문가격"] = dr["주문가격"];
                tRow["주문수량"] = dr["주문수량"];
                tRow["미체결수량"] = dr["주문수량"];
                tRow["주문금액"] = Int32.Parse(dr["주문가격"].ToString()) * Int32.Parse(dr["주문수량"].ToString());
            }
            dg미체결.AutoGenerateColumns = true;

            UcHogaWindowNew1.Opt10075Dt = _dt미체결;
        }

        object _tmpObj;
        private void cmsMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (cmsMenu.SourceControl.Name != "") _tmpObj = cmsMenu.SourceControl;

            if (
                cmsMenu.SourceControl.Name == dgN관종.Name ||
                cmsMenu.SourceControl.Name == dg뉴스체결.Name
                )
            {
                시장가매수ToolStripMenuItem.Enabled = false;
                시장가매도ToolStripMenuItem.Enabled = false;
            }
            else
            {
                시장가매수ToolStripMenuItem.Enabled = true;
                시장가매도ToolStripMenuItem.Enabled = true;
            }

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

        private void btnCLeft_Click(object sender, EventArgs e)
        {
            if (btnCLeft.Text == "◀")
            {
                pnl조건.Left -= pnl조건.Width;
                pnl조건종목.Left -= pnl조건.Width;
                pnl조건종목.Width += pnl조건.Width;
                btnCLeft.Left -= pnl조건.Width;
                btnCLeft.Text = "▶";
            }
            else
            {
                pnl조건.Left += pnl조건.Width;
                pnl조건종목.Left += pnl조건.Width;
                pnl조건종목.Width -= pnl조건.Width;
                btnCLeft.Left += pnl조건.Width;
                btnCLeft.Text = "◀";
            }
        }

        private void btnAllTickSave_Click(object sender, EventArgs e)
        {
            tbStockList.Enabled = false;
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = _dsTick60.Tables.Count;
            progressBar1.Value = 0;
            foreach (DataTable dt in _dsTick60.Tables)
            {
                TickSaveNew(dt);
                progressBar1.Value += 1;
                Application.DoEvents();
            }
            progressBar1.Visible = false;
            tbStockList.Enabled = true;
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

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells["종목번호"].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells["P종목코드"].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells["N종목코드"].Value.ToString();
            else if (dgObj.Name == dg조건종목.Name) stockCode = dgObj.Rows[row].Cells["C종목코드"].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells["F_종목코드"].Value.ToString();
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
            int row;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells["종목번호"].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells["P종목코드"].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells["N종목코드"].Value.ToString();
            else if (dgObj.Name == dg조건종목.Name) stockCode = dgObj.Rows[row].Cells["C종목코드"].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells["F_종목코드"].Value.ToString();

            if (stockCode == "") return;
            ucFinance1.StockCode = stockCode;
            UcMainStockVer2.Opt10001_OnReceiveTrData(stockCode, "");
        }

        private void rbA_CheckedChanged(object sender, EventArgs e)
        {
            if (rbA.Checked == true) GetFavList(rbA.Text.Trim());
        }

        private void chkFinance_CheckedChanged(object sender, EventArgs e)
        {
            dg관종.Columns["F_신용비율"].Visible = chkFinance.Checked;
            dg관종.Columns["F_시가총액"].Visible = chkFinance.Checked;
            dg관종.Columns["F_PER"].Visible = chkFinance.Checked;
            dg관종.Columns["F_ROE"].Visible = chkFinance.Checked;
            dg관종.Columns["F_PBR"].Visible = chkFinance.Checked;
            dg관종.Columns["F_EV"].Visible = chkFinance.Checked;
            dg관종.Columns["F_EPS"].Visible = chkFinance.Checked;
            dg관종.Columns["F_BPS"].Visible = chkFinance.Checked;
            dg관종.Columns["F_영업이익"].Visible = chkFinance.Checked;
            dg관종.Columns["F_당기순이익"].Visible = chkFinance.Checked;
            dg관종.Columns["F_250최고가"].Visible = chkFinance.Checked;
            dg관종.Columns["F_250최저가"].Visible = chkFinance.Checked;
        }

        private void 매동ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int row = -1;

            string stockCode = "";
            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells["종목번호"].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells["P종목코드"].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells["N종목코드"].Value.ToString();
            else if (dgObj.Name == dg조건종목.Name) stockCode = dgObj.Rows[row].Cells["C종목코드"].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells["F_종목코드"].Value.ToString();
            else if (dgObj.Name == dg미체결.Name) stockCode = dgObj.Rows[row].Cells["종목코드"].Value.ToString().Replace("A", "");

            if (stockCode == "") return;

            ucStockVolumeAnalysis1.StockCode = stockCode;
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
            if (this.dg관종.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                dg관종.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;
            int row = -1;
            int interIdNew = Convert.ToInt32(obj.Text);
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

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells["종목번호"].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells["P종목코드"].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells["N종목코드"].Value.ToString();
            else if (dgObj.Name == dg조건종목.Name) stockCode = dgObj.Rows[row].Cells["C종목코드"].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells["F_종목코드"].Value.ToString();
            else if (dgObj.Name == dg미체결.Name) stockCode = dgObj.Rows[row].Cells["종목코드"].Value.ToString().Replace("A", "");

            if (stockCode == "") return;
            _DataAcc.p_Psi02Add("M", _stockId, interId, stockCode, "00", interIdNew.ToString(), "", null, null);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem obj = (ToolStripMenuItem)sender;
            int row = -1;
            int interId = Convert.ToInt32(obj.Text);

            string stockCode = "";
            DataGridView dgObj = (DataGridView)_tmpObj;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name) stockCode = dgObj.Rows[row].Cells["종목번호"].Value.ToString().Replace("A", "");
            else if (dgObj.Name == dgN관종.Name) stockCode = dgObj.Rows[row].Cells["P종목코드"].Value.ToString();
            else if (dgObj.Name == dg뉴스체결.Name) stockCode = dgObj.Rows[row].Cells["N종목코드"].Value.ToString();
            else if (dgObj.Name == dg조건종목.Name) stockCode = dgObj.Rows[row].Cells["C종목코드"].Value.ToString();
            else if (dgObj.Name == dg관종.Name) stockCode = dgObj.Rows[row].Cells["F_종목코드"].Value.ToString();
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
            int row;

            string stockCode = "";
            string stockName = "";

            DataGridView dgObj = (DataGridView)cmsMenu.SourceControl;
            if (dgObj.CurrentRow == null) return;
            row = dgObj.CurrentRow.Index;
            if (row == -1) return;

            if (dgObj.Name == dg뉴스잔고.Name)
            {
                stockCode = dgObj.Rows[row].Cells["종목번호"].Value.ToString().Replace("A", "");
                stockName = "종목명";
            }
            else if (dgObj.Name == dgN관종.Name)
            {
                stockCode = dgObj.Rows[row].Cells["P종목코드"].Value.ToString();
                stockName = "P종목명";
            }
            else if (dgObj.Name == dg뉴스체결.Name)
            {
                stockCode = dgObj.Rows[row].Cells["N종목코드"].Value.ToString();
                stockName = "N종목명";
            }
            else if (dgObj.Name == dg조건종목.Name)
            {
                stockCode = dgObj.Rows[row].Cells["C종목코드"].Value.ToString();
                stockName = "C종목명";
            }

            else if (dgObj.Name == dg관종.Name)
            {
                stockCode = dgObj.Rows[row].Cells["F_종목코드"].Value.ToString();
                stockName = "F_종목명";
            }        

            if (stockCode == "") return;

            Chart.frmChart frmChart = new Chart.frmChart();
            //_uc = new Chart.ucChart();
            frmChart.MainStock = UcMainStockVer2;
            frmChart.GetChartData(stockCode);

            ////Panel pnlChart = new Panel();
            ////pnlChart.Width = _uc.Width + 5;
            ////pnlChart.Height = _uc.Height + 5;
            ////frmChart.Dock = DockStyle.Fill;
            ////pnlChart.Controls.Add(_uc);
            ////pnlChart.Dock = DockStyle.Fill;
            
            ////frmChart.Width = pnlChart.Width + 5;
            ////frmChart.Height = pnlChart.Height + 5;
            ////frmChart.Controls.Add(pnlChart);

            frmChart.Text = dgObj.Rows[row].Cells[stockName].Value.ToString() + "(" + stockCode + ")";
            frmChart.Show();
            
            ////if(_frmChart == null)
            ////{
            ////     _frmChart = new Form();

            ////     _uc = new Chart.ucChart();
            ////     _uc.MainStock = UcMainStockVer2;
            ////     _uc.GetChartData(stockCode);

            ////     Panel pnlChart = new Panel(); 
            ////     pnlChart.Width = _uc.Width + 5;
            ////     pnlChart.Height = _uc.Height + 5;
            ////     pnlChart.Controls.Add(_uc);

            ////     _frmChart.Width = pnlChart.Width + 5;
            ////     _frmChart.Height = pnlChart.Height + 5;
            ////     _frmChart.Controls.Add(pnlChart);

            ////     _frmChart.Show();

            ////     _frmChart.FormClosing += frmChart_FormClosing;
            ////}
            ////else
            ////{
            ////    _uc.GetChartData(stockCode);
            ////    _frmChart.Show();
            ////}

            
            //ucFinance1.StockCode = stockCode;
            //UcMainStockVer2.Opt10001_OnReceiveTrData(stockCode, "");
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

        private void btnDartOff_Click(object sender, EventArgs e)
        {
            tmrDart.Enabled = !tmrDart.Enabled;
            tmrDart1.Enabled = !tmrDart1.Enabled;
            tmrDartKrx.Enabled = !tmrDartKrx.Enabled;
            
        }
        
    }
}