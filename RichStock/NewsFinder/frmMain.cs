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
using System.Text.RegularExpressions;
using VB6 = Microsoft.VisualBasic ;
using System.Globalization;
using PaikRichStock.Common;
using Common;
using System.Collections;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.Internals;

namespace NewsFinder
{
    public partial class frmMain : Form
    {
        private DataSet _ds조건체결 = new DataSet();
        private string[] _체결 = { "체결시간", "현재가", "전일대비", "등락율", "거래량", "누적거래량", "누적거래대금", "시가", "고가", "저가", "전일거래량대비", "거래비용", "체결강도", "상한가발생시간", "최고거래량", "최저거래량", "최고체결강도", "최저체결강도" };
        private DataAccess _DataAcc;
        private DataTable _dt화면관리 = new DataTable();
        private DataTable _dtDart = new DataTable();

        private clsKiwoomBaseInfo _clsKiwoomBaseInfo = new clsKiwoomBaseInfo();
        private int _SLEEP_TIME = 1000;
        private string[] dart = { 
                                    "무상증자" ,
                                    "소유상황" ,
                                    "대량보유" ,
                                    "자기주식" ,
                                    "최대주주" ,
                                    "유상증자" ,
                                    "단일판매",
                                    "주식변동",
                                    "회생계획"          
                                };
        public frmMain()
        {

            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            ResizeBegin += (s, e) => SuspendLayout();
            ResizeEnd += (s, e) => ResumeLayout(true);

            _dt화면관리.Columns.Add("화면구분"); //1.뉴스, 2.잔고 , 3.조건
            _dt화면관리.Columns.Add("실시간구분"); //1.실시간 2.정적
            _dt화면관리.Columns.Add("종목코드"); //종목코드
            _dt화면관리.Columns.Add("화면번호"); //화면번호

            _dtDart.Columns.Add("시간");
            _dtDart.Columns.Add("종목코드");
            _dtDart.Columns.Add("종목명");
            _dtDart.Columns.Add("제목");
            _dtDart.Columns.Add("주소");

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

                    if (dgDart.Rows[0].Cells["제목"].Value.ToString().Trim().IndexOf("유상증자") > -1 && dgDart.Rows[0].Cells["제목"].Value.ToString().Trim().IndexOf("자율공시") < 0)
                    {
                        string html = "";

                        html = GetReport(dgDart.Rows[0].Cells["주소"].Value.ToString().Trim());

                        try
                        {
                            if (html.IndexOf("3자배정") > -1 && html.IndexOf("자율") < 0)
                            {
                                dgDart.Rows[0].Cells["제목"].Value = dgDart.Rows[0].Cells["제목"].Value.ToString().Trim() + "(3자배정)" + "-자동매수";
                            }
                        }
                        catch
                        {
                        }
                    }
                    else if (dgDart.Rows[0].Cells["제목"].Value.ToString().Trim().IndexOf("단일판매ㆍ공급계약체결") > -1 && dgDart.Rows[0].Cells["제목"].Value.ToString().Trim().IndexOf("기재정정") < 0)
                    {
                        string html = GetReport(dgDart.Rows[0].Cells["주소"].Value.ToString().Trim());
                        try
                        {

                            if (html.IndexOf("매출액 대비") > -1 || html.IndexOf("매출액대비") > -1)
                            {
                                int index1 = 0;
                                int index2 = 0;
                                int index3 = 0;
                                int index4 = 0;
                                if (html.IndexOf("매출액대비") > -1)
                                {
                                    index1 = html.IndexOf("매출액대비");
                                }
                                else if (html.IndexOf("매출액 대비") > -1)
                                {
                                    index1 = html.IndexOf("매출액 대비");
                                }

                                index2 = html.IndexOf("xforms_input", index1);
                                index3 = html.IndexOf(">", index2);
                                index4 = html.IndexOf("<", index3);

                                int leng = 7 - html.Substring(index3 + 1, index4 - index3 - 1).Length;
                                string tmpStr1 = html.Substring(index3 + 1, index4 - index3 - 1);
                                double tmpCnt = double.Parse(html.Substring(index3 + 1, index4 - index3 - 1));
                                for (int i = 0; i < leng; i++)
                                {
                                    tmpStr1 = " " + tmpStr1;
                                }

                                if (tmpCnt > 15)
                                {
                                    tmpStr1 = tmpStr1 + "-자동매수";
                                }

                                dgDart.Rows[0].Cells["제목"].Value = dgDart.Rows[0].Cells["제목"].Value + "-매출액 대비:" + tmpStr1;

                            }
                        }
                        catch
                        {
                        }
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

                    foreach (string str1 in lsb자동매수단어.Items)
                    {
                        if (dgDart.Rows[0].Cells["제목"].Value.ToString().IndexOf(str1) > -1)
                        {

                            AutoBuy(dgDart.Rows[0].Cells["종목"].Value.ToString().Trim(), dgDart.Rows[0].Cells["제목"].Value.ToString().Trim());

                            break;
                        }
                    }

                    foreach (string str in dart)
                    {
                        if (dgDart.Rows[0].Cells["제목"].Value.ToString().IndexOf(str) > -1)
                        {
                            string oStr = "";
                            oStr = dgDart.Rows[0].Cells["종목"].Value.ToString() + "-" + dgDart.Rows[0].Cells["제목"].Value.ToString() + " " + dgDart.Rows[0].Cells["주소"].Value.ToString();
                            _frm.UpdateNoti(oStr);
                            _frm.Visible = true;
                            _frm.TopMost = true;
                            //System.Windows.Forms.Clipboard.SetText(dgDart.Rows[0].Cells["종목"].Value.ToString().Trim());
                            break;
                        }
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
            bool 팝업필터 = false;

            WebClient wc = new WebClient();
            wc.Encoding = System.Text.UTF8Encoding.UTF8;

            //String apiURL = "http://dart.fss.or.kr/api/todayRSS.xml";

            String buffer = wc.DownloadString("http://dart.fss.or.kr/api/todayRSS.xml");

            wc.Dispose();
            StringReader sr = new StringReader(buffer);
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
                }
                if (blnTrue == false)
                {
                    dgDart.Rows.Insert(0, 1);

                    dgDart.Rows[0].Cells["시간"].Value = DateTime.Parse(node["pubDate"].InnerText).ToString("yyyy-MM-dd HH:mm") + DateTime.Now.ToString(":ss");
                    dgDart.Rows[0].Cells["종목"].Value = node["dc:creator"].InnerText;
                    dgDart.Rows[0].Cells["제목"].Value = node["title"].InnerText;
                    dgDart.Rows[0].Cells["주소"].Value = node["link"].InnerText;

                    if (node["title"].InnerText.IndexOf("유상증자") > -1 && node["title"].InnerText.IndexOf("자율공시") < 0)
                    {
                        string html = "";

                        html = GetReport(node["link"].InnerText);

                        try
                        {
                            if (html.IndexOf("3자배정") > -1 && html.IndexOf("자율") < 0)
                            {
                                dgDart.Rows[0].Cells["제목"].Value = node["title"].InnerText.Trim() + "(3자배정)" + "-자동매수";
                            }
                        }
                        catch
                        {
                        }
                    }
                    else if (node["title"].InnerText.IndexOf("단일판매ㆍ공급계약체결") > -1 && node["title"].InnerText.IndexOf("기재정정") < 0)
                    {
                        string html = GetReport(node["link"].InnerText);
                        try
                        {

                            if (html.IndexOf("매출액 대비") > -1 || html.IndexOf("매출액대비") > -1)
                            {
                                int index1 = 0;
                                int index2 = 0;
                                int index3 = 0;
                                int index4 = 0;
                                if (html.IndexOf("매출액대비") > -1)
                                {
                                    index1 = html.IndexOf("매출액대비");
                                }
                                else if (html.IndexOf("매출액 대비") > -1)
                                {
                                    index1 = html.IndexOf("매출액 대비");
                                }

                                index2 = html.IndexOf("xforms_input", index1);
                                index3 = html.IndexOf(">", index2);
                                index4 = html.IndexOf("<", index3);

                                int leng = 7 - html.Substring(index3 + 1, index4 - index3 - 1).Length;
                                string tmpStr1 = html.Substring(index3 + 1, index4 - index3 - 1);
                                double tmpCnt = double.Parse(html.Substring(index3 + 1, index4 - index3 - 1));
                                for (int i = 0; i < leng; i++)
                                {
                                    tmpStr1 = " " + tmpStr1;
                                }

                                if (tmpCnt > 15)
                                {
                                    tmpStr1 = tmpStr1 + "-자동매수";
                                }

                                dgDart.Rows[0].Cells["제목"].Value = node["title"].InnerText + "-매출액 대비:" + tmpStr1;

                            }
                        }
                        catch
                        {
                        }
                    }

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
                        _frm.UpdateNoti(oStr);
                        _frm.Visible = true;
                        _frm.TopMost = true;
                        //System.Windows.Forms.Clipboard.SetText(dgDart.Rows[0].Cells["종목"].Value.ToString().Trim());
                        break;
                    }
                }
            }
            dgDart.Sort(dgDart.Columns[0], System.ComponentModel.ListSortDirection.Descending);
        }

        private void Logger(string gubun, string msg)
        {
            if (btn로그실행.Text == "▶") return;
            if (dgLog.Rows.Count > 50)
            {
                dgLog.Rows.RemoveAt(dgLog.Rows.Count - 2);
            }
            dgLog.Rows.Insert(0, 1);
            dgLog.Rows[0].Cells[0].Value = gubun;
            dgLog.Rows[0].Cells[1].Value = msg;
        }


        //자동단어에 등록된 공시가 났을경우 타는 함수 - S
        public void AutoBuy(string StockName, string title)
        {
            Decimal 매출액대비Per = 0;
            if (title.IndexOf("매출액 대비") > -1)
            {
                매출액대비Per = Decimal.Parse(title.Replace("-자동매수", "").Substring(title.IndexOf("대비:") + 3));
            }

            if (UcMainStock1._allStockDataset == null) { return; }
            DataView dv = new DataView(UcMainStock1._allStockDataset.Tables[0]);
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

                    if (Cls.isExistsRow(dgN관종, 0, dr["STOCK_CODE"].ToString().Trim()) == -1)
                    {
                        dgN관종.Rows.Insert(0, 1);
                        dgN관종.Rows[0].Cells["P종목코드"].Value = dr["STOCK_CODE"].ToString().Trim();
                        dgN관종.Rows[0].Cells["P종목명"].Value = dr["STOCK_NAME"].ToString().Trim();
                        SetDsScreenNo("A", "1", "1", dr["STOCK_CODE"].ToString().Trim(), "", true);
                    }
                }
                //뉴스 관종에 등록 - E
            }
        }
        //자동단어에 등록된 공시가 났을경우 타는 함수 - S


        //모든 주문이 이루어지는 함수 - S
        private void SendBuySellMsg(string 종목코드, string 거래구분, int 매매구분, int 호가단위, int 현재가, int 수량, string 화면구분)
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

            //if (DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 07:20:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 08:40:00")) )  {
            //    거래구분 = "61";
            //}else if (DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 15:30:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:00:00")))
            //{
            //    거래구분 = "81";
            //}else if (DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 16:30:00")) && DateTime.Now < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 18:00:00")))
            //{
            //    거래구분 = "62";
            //}

            int 호가단위금액 = 0;
            int 주문금액 = 현재가;

            if (현재가 != 0)
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
                UcMainStock1.SendOrder("매수", scrNum, cboAccount.Text.Trim(), 매매Type,
                    종목코드, 수량, 거래구분 == "03" || 거래구분 == "61" || 거래구분 == "81" ? 0 : 주문금액, 거래구분, "");
            }
            else if (매매Type == ucMainStock.OrderType.신규매도)
            {
                UcMainStock1.SendOrder("매도", scrNum, cboAccount.Text.Trim(), 매매Type,
                    종목코드, 수량, 거래구분 == "03" || 거래구분 == "61" || 거래구분 == "81" ? 0 : 주문금액, 거래구분, "");
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

        private int _dartCnt = 0;
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

                //if (cboAccount.Text.Trim() != "8085884911") { 
                //    GetGong(); 
                //}
                //else { 
                //GetDart();
                //}
                GetGong();
                if (_dartCnt == 1)
                {
                    chk자동매매.Checked = true;
                }
            }
            catch (Exception ex) { Logger("에러 (tmrDart)", ex.Message); }
            finally
            {
                if (tmrDart.Enabled == false)
                    tmrDart.Enabled = true;

                _dartCnt++;
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
            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            InitBrowser();
            _frm = new Form1();

            if (bitness == "x64") { _SLEEP_TIME = _SLEEP_TIME / 2; }
            UcMainStock1.OnEventConnect = ModStatus.EventOn;
            UcMainStock1.OnReceiveConditionVer = ModStatus.EventOn;
            UcMainStock1.OnReceiveTrCondition = ModStatus.EventOn;
            UcMainStock1.OnReceiveTrData = ModStatus.EventOn;

            UcMainStock1.Connection();
        }

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

            txt자동매수단어.Text = "";
            txt팝업필터.Text = "";
            txtKeyWord.Focus();
        }

        private void SimpleBrowserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrNaver.Stop();
            tmrDaum.Stop();
            tmrDart.Stop();

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
            tmrDart.Start();

            dgDart.RowCount = 1;
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

        private void SendCurrentPriceMsg(string 종목코드)
        {
            UcMainStock1.GetStockBaseInfo(종목코드, GetScrNum());
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
                txt이익실현.Enabled = false;
                chk손절.Enabled = false;
                txt손절.Enabled = false;
            }
            else
            {
                btnStopLoss.Text = "▶";
                chk이익실현.Enabled = true;
                txt이익실현.Enabled = true;
                chk손절.Enabled = true;
                txt손절.Enabled = true;
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
                if (UcMainStock1._allStockDataset == null) return;
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
                    DataView dv = new DataView(UcMainStock1._allStockDataset.Tables[0]);
                    dv.RowFilter = "STOCK_NAME LIKE '" + txt제외항목.Text + "%'";

                    if (dv.Count == 0) return;

                    foreach (DataRowView dr in dv)
                    {
                        lsb제외다수.Items.Add(dr["STOCK_NAME"].ToString().Trim() + "|" + dr["STOCK_CODE"].ToString().Trim());
                    }

                    if (lsb제외다수.Items.Count == 1)
                    {
                        lsb제외항목.Items.Add(lsb제외다수.Items[0].ToString());
                        lsb제외다수.Visible = false;
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
                string scrNum = GetScrNum();
                UcMainStock1.Getopw00018(cboAccount.Text.Trim(), "", "", 1, "9001");
            }
            else if (tbStockList.SelectedIndex == 2)
            {
                if (dg조건리스트.Rows.Count < 2)
                {
                    UcMainStock1.GetUserConditionLoad();
                }
            }
        }


        private void btn로그클리어_Click(object sender, EventArgs e)
        {
            dgLog.RowCount = 1;
        }


        private void dg조건리스트_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value.ToString().Trim() == "||") return;
            if (dg조건리스트.Rows[e.RowIndex].Cells["화면번호"].Value.ToString().Trim() == "") return;
            dg조건종목.RowCount = 1;
            UcMainStock1.GetUserConditionStockLoad(dg조건리스트.Rows[e.RowIndex].Cells["화면번호"].Value.ToString().Trim()
                , dg조건리스트.Rows[e.RowIndex].Cells["조건명"].Value.ToString().Trim()
                , dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim(), 0);

        }


        private void btn조건식_Click(object sender, EventArgs e)
        {
            UcMainStock1.GetUserConditionLoad();
        }

        private void btn일괄매수_Click(object sender, EventArgs e)
        {
            Decimal 매수금액 = 0;
            Decimal 수량 = 0;
            Decimal 주문가 = 0;
            Decimal 현재가 = 0;
            string 종목코드 = "";
            for (int row = 0; row < dg조건종목.RowCount - 1; row++)
            {
                if (dg조건종목.Rows[row].Cells["C종목코드"].Value == null) { continue; }
                if (Convert.ToBoolean(dg조건종목.Rows[row].Cells["C체크"].Value) == true)
                {
                    종목코드 = dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString();
                    현재가 = Decimal.Parse(dg조건종목.Rows[row].Cells["C현재가"].Value.ToString());
                    try
                    {
                        if (txt1차매수금액.Text.Trim() == "" || dg조건종목.Rows[row].Cells["C1차매수가"].Value.ToString().Trim() == "") { continue; }
                        주문가 = MakeOrderPrice(Convert.ToInt32(Convert.ToDecimal(dg조건종목.Rows[row].Cells["C1차매수가"].Value.ToString()).ToString("########")));
                        if (현재가 > 주문가)
                        {
                            매수금액 = Int32.Parse(txt1차매수금액.Text.Trim());
                            수량 = Convert.ToInt32(매수금액 / 주문가);
                            if (매수금액 < 주문가) { continue; }
                            SendBuySellMsg(종목코드, "00", 1, 0, (int)주문가, (int)수량, "3");
                            System.Threading.Thread.Sleep(_SLEEP_TIME / 2);
                        }

                        if (txt2차매수금액.Text.Trim() == "" || dg조건종목.Rows[row].Cells["C2차매수가"].Value.ToString().Trim() == "") { continue; }
                        주문가 = MakeOrderPrice(Convert.ToInt32(Convert.ToDecimal(dg조건종목.Rows[row].Cells["C2차매수가"].Value.ToString()).ToString("########")));
                        if (현재가 > 주문가)
                        {
                            매수금액 = Int32.Parse(txt2차매수금액.Text.Trim());
                            수량 = Convert.ToInt32(매수금액 / 주문가);
                            if (매수금액 < 주문가) { continue; }
                            SendBuySellMsg(종목코드, "00", 1, 0, (int)주문가, (int)수량, "3");
                            System.Threading.Thread.Sleep(_SLEEP_TIME / 2);
                        }

                        if (txt3차매수금액.Text.Trim() == "" || dg조건종목.Rows[row].Cells["C3차매수가"].Value.ToString().Trim() == "") { continue; }
                        주문가 = MakeOrderPrice(Convert.ToInt32(Convert.ToDecimal(dg조건종목.Rows[row].Cells["C3차매수가"].Value.ToString()).ToString("########")));
                        if (현재가 > 주문가)
                        {
                            매수금액 = Int32.Parse(txt3차매수금액.Text.Trim());
                            수량 = Convert.ToInt32(매수금액 / 주문가);
                            if (매수금액 < 주문가) { continue; }
                            SendBuySellMsg(종목코드, "00", 1, 0, (int)주문가, (int)수량, "3");
                            System.Threading.Thread.Sleep(_SLEEP_TIME / 2);
                        }
                    }
                    finally
                    {
                        //dg조건종목.Rows[row].Cells["C체크"].Value = false;
                    }
                }
            }
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
            if (e.ColumnIndex == 3)
            {
                if (dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value.ToString().Trim() == "▶")
                {
                    if (dg조건리스트.Rows[e.RowIndex].Cells["화면번호"].Value.ToString().Trim() == "") return;

                    _실시간실행여부 = false;
                    for (int row = 0; row < dg조건리스트.RowCount - 1; row++)
                    {
                        if (dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value.ToString().Trim() == "||")
                        {
                            _실시간실행여부 = true;
                            break;
                        }
                    }

                    if (_실시간실행여부 == true)
                    {
                        MessageBox.Show("실행중인 조건검색이 있습니다. 종료하고 실행해 주십시요", "경고!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    dg조건종목.RowCount = 1;
                    UcMainStock1.GetUserConditionStockLoad(dg조건리스트.Rows[e.RowIndex].Cells["화면번호"].Value.ToString().Trim()
                        , dg조건리스트.Rows[e.RowIndex].Cells["조건명"].Value.ToString().Trim()
                        , dg조건리스트.Rows[e.RowIndex].Cells["번호"].Value.ToString().Trim(), 1);

                    dg조건종목.Tag = "2001";
                    dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value = "||";
                    _실시간실행여부 = true;
                }
                else
                {
                    dg조건리스트.Rows[e.RowIndex].Cells["실행"].Value = "▶";
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
            Decimal 매매가능수량 = 0;
            if (ds == null) return;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int row = 0; row < dg잔고.RowCount - 1; row++)
                {
                    종목코드 = Cls.Right(dg잔고.Rows[row].Cells["종목번호"].Value.ToString().Trim(), 6);
                    종목명 = dg잔고.Rows[row].Cells["종목명"].Value.ToString().Trim();
                    매매가능수량 = Decimal.Parse(dg잔고.Rows[row].Cells["매매가능수량"].Value.ToString().Trim());
                    if (dr["STOCK_CODE"].ToString().Trim() == 종목코드)
                    {
                        dg잔고.Rows[row].Cells["현재가"].Value = Math.Abs(Decimal.Parse(dr["현재가"].ToString().Trim())).ToString("##,###,###,##0");
                        매입가 = Decimal.Parse(dg잔고.Rows[row].Cells["매입가"].Value.ToString().Trim());
                        현재가 = Math.Abs(Decimal.Parse(dr["현재가"].ToString().Trim()));
                        dg잔고.Rows[row].Cells["수익률(%)"].Value = ((현재가 - 매입가) / 매입가 * 100).ToString("###,###,###,##0.00");

                        if (btnStopLoss.Text.Trim() == "||")
                        {
                            ProcessStopLoss(매입가, 현재가, 종목코드, 종목명, 매매가능수량, row);
                            Application.DoEvents();
                        }

                        return;
                    }
                }
            }

            Application.DoEvents();
        }

        private void ProcessStopLoss(Decimal 매입가, Decimal 현재가, string 종목코드, string 종목명, Decimal 매매가능수량, int row)
        {
            Decimal stoplossPer = 0;
            stoplossPer = (현재가 - 매입가) / 매입가 * 100;
            Decimal fitPer = Decimal.Parse(txt이익실현.Text.Trim());
            Decimal lossPer = Decimal.Parse(txt손절.Text.Trim());
            if (종목코드 == "" || 종목명 == "" || 매매가능수량 == 0) return;
            if (btnStopLoss.Text.Trim() == "▶") return;

            if (lsb제외항목.Items.Contains(종목명 + "|" + 종목코드) == true) return;

            if (chk이익실현.Checked == true)
            {
                if (stoplossPer >= fitPer)
                {
                    SendBuySellMsg(종목코드, "00", (int)ucMainStock.OrderType.신규매도, 0, Convert.ToInt32(현재가), Convert.ToInt32(매매가능수량), "2");
                    dg잔고.Rows[row].Cells["매매가능수량"].Value = Decimal.Parse(dg잔고.Rows[row].Cells["매매가능수량"].Value.ToString().Trim()) - Convert.ToInt32(매매가능수량);
                }
            }
            if (chk손절.Checked == true)
            {
                if (stoplossPer <= lossPer)
                {
                    SendBuySellMsg(종목코드, "00", (int)ucMainStock.OrderType.신규매도, 0, Convert.ToInt32(현재가), Convert.ToInt32(매매가능수량), "2");
                    dg잔고.Rows[row].Cells["매매가능수량"].Value = Decimal.Parse(dg잔고.Rows[row].Cells["매매가능수량"].Value.ToString().Trim()) - Convert.ToInt32(매매가능수량);
                }
            }
        }

        private void SettingConditionStockListDetailData(DataSet ds)
        {
            if (ds == null) return;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int row = 0; row < dg조건종목.RowCount - 1; row++)
                {
                    if (dr["STOCK_CODE"].ToString().Trim() == dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString().Trim())
                    {
                        dg조건종목.Rows[row].Cells["C현재가"].Value = Math.Abs(Decimal.Parse(dr["현재가"].ToString().Trim())).ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C등락률"].Value = dr["등락율"].ToString().Trim();
                        dg조건종목.Rows[row].Cells["C거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString().Trim()).ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C시가"].Value = Math.Abs(Decimal.Parse(dr["시가"].ToString().Trim())).ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C고가"].Value = Math.Abs(Decimal.Parse(dr["고가"].ToString().Trim())).ToString("###,###,###,##0");
                        dg조건종목.Rows[row].Cells["C저가"].Value = Math.Abs(Decimal.Parse(dr["저가"].ToString().Trim())).ToString("###,###,###,##0");
                        Application.DoEvents();
                        continue;
                    }
                }
            }
        }

        private void ucMainStock1_OnConnection(string status)
        {
            UcMainStock1.GetAccount();

            foreach (DataRow dr in UcMainStock1._AccNo.Tables["ACCNO"].Rows)
            {
                cboAccount.Items.Add(dr["ACCNO"].ToString().Trim());
            }
            cboAccount.SelectedIndex = 0;
            tmrDaum.Stop();
            tmrNaver.Stop();
            tmrDart.Start();

            UcMainStock1.Getopw00018(cboAccount.Items[cboAccount.SelectedIndex].ToString(), "", "", 1, "9001");
            UcHogaWindow1.MainStock = UcMainStock1;

            if (cboAccount.Text == "3228538611") //실계좌일때는 정상 셋팅한다.
            {
                txt매수금액.Text = "30000";
                txt조건시간.Text = "5";
                txt조건체결강도.Text = "2";
                txt조건거래대금.Text = "20";
                tmrDart.Interval = 500;
            }
            //txtTmDaum1.Text = tmrDaum.Interval.ToString();
            txtTmNaver2.Text = tmrNaver.Interval.ToString();
            txtTmDart3.Text = tmrDart.Interval.ToString();
        }

        private void ucMainStock1_OnDayDsBaseInfo(DataSet ds)
        {
        }

        private void ucMainStock1_OnDsBaseInfo(DataSet ds)
        {

        }

        private void ucMainStock1_OnDsGetConditionList(DataSet ds)
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
                    dg조건리스트.Rows[row].Cells["화면번호"].Value = "9" + dr["CONDI_SEQ"].ToString().Trim();
                    dg조건리스트.Rows[row].Cells["실행"].Value = "▶";

                    row = row + 1;
                }

            }
            catch (Exception ex)
            {
                Logger("에러 (OnDsGetConditionList)", ex.ToString());
                if (UcMainStock1.EVENT_STATUS.STATUS_OnReceiveConditionVer == ModStatus.EventOff)
                {
                    UcMainStock1.OnReceiveConditionVer = ModStatus.EventOn;
                }
            }
        }

        private DataSet ConvertDsNumber(DataSet ds)
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int col = 0; col < ds.Tables[0].Columns.Count - 1; col++)
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

        private void ucMainStock1_OnDsOpw00018(DataSet ds)
        {

            ds = ConvertDsNumber(ds);
            dg잔고.DataSource = ds.Tables[0];
            dg뉴스잔고.DataSource = ds.Tables[0];

            // Automatically generate the DataGridView columns.
            dg잔고.AutoGenerateColumns = true;
            dg뉴스잔고.AutoGenerateColumns = true;

            //string 종목코드;
            //for (int row = 0; row < dg잔고.RowCount - 1; row++)
            //{
            //    종목코드 = dg잔고.Rows[row].Cells["종목번호"].Value.ToString().Substring(1).Trim();
            //    RequestRealData(종목코드, "2" , false);

            //    System.Threading.Thread.Sleep(_SLEEP_TIME);
            //    Application.DoEvents();
            //}
        }

        private void ucMainStock1_OnDsReceiveChejanData(DataSet ds)
        {
            //Public ChejanFidList(,) As String = {{"9201", "계좌번호"}, {"9203", "주문번호"}, {"9001", "종목코드"}, {"913", "주문상태"}, {"302", "종목명"}, {"900", "주문수량"}, _
            //                        {"901", "주문가격"}, {"902", "미체결수량"}, {"903", "체결누계금액"}, {"904", "원주문번호"}, {"905", "주문구분"}, {"906", "매매구분"}, _
            //                        {"907", "매도수구분"}, {"908", "주문/체결시간"}, {"909", "체결번호"}, {"910", "체결가"}, {"911", "체결량"}, {"10", "현재가"}, _
            //                        {"27", "(최우선)매도호가"}, {"28", "(최우선)매수호가"}, {"914", "단위체결가"}, {"915", "단위체결량"}, {"919", "거부사유"}, _
            //                        {"920", "화면번호"}, {"917", "신용구분"}, {"916", "대출일"}, {"930", "보유수량"}, {"931", "매입단가"}, {"932", "총매입가"}, _
            //                        {"933", "주문가능수량"}, {"945", "당일순매수수량"}, {"946", "매도/매수구분"}, {"950", "당일총매도손일"}, {"951", "예수금"}, _
            //                        {"307", "기준가"}, {"8019", "손익율"}, {"957", "신용금액"}, {"958", "신용이자"}, {"918", "만기일"}, {"990", "당일실현손익(유가)"}, _
            //                        {"991", "당일실현손익률(유가)"}, {"992", "당일실현손익(신용)"}, {"993", "당일실현손익률(신용)"}, {"397", "파생상품거래단위"}, _
            //                        {"305", "상한가"}, {"306", "하한가"}}
            string str = "";
            if (ds == null) { return; }
            if (ds.Tables.Count < 1) { return; }
            if (ds.Tables[0].Rows.Count < 1) { return; }

            DataRow dr;
            dr = ds.Tables[0].Rows[0];

            for (int col = 0; col < ds.Tables[0].Columns.Count - 1; col++)
            {
                str += ds.Tables[0].Columns[col].ColumnName + " : " + dr[col].ToString().Trim() + " | ";
            }

            Logger("체결정보" + DateTime.Now.ToString("HH:mm:ss") + " (OnDsReceiveChejanData) ", str);

            if (ds.Tables[0].Select("미체결수량 = '0' AND 체결가 <> ''").Length > 0)
            {
                dr = ds.Tables[0].Select("미체결수량 = '0' AND 체결가 <> ''")[0];
            }
            else
            {
                return;
            }


            string 종목코드 = dr["종목코드"].ToString().Substring(1).Trim();
            DataView dv;
            dv = new DataView(_dt화면관리);
            dv.RowFilter = "종목코드 = '" + 종목코드 + "' AND 실시간구분 = '1' AND 화면구분 = '1'";

            if (dv.Count > 0) SettingNewsOrderPrice(종목코드, dr["체결가"].ToString().Trim());

            //_frm.UpdateNoti(str);
        }

        private void SettingNewsOrderPrice(string 종목코드, string 체결가)
        {
            //뉴스체결로 인해 시장가로 주문이 들어감 체결된 가격으로 UPDATE한다.
            for (int row = 0; row < dg뉴스체결.RowCount - 1; row++)
            {
                if (dg뉴스체결.Rows[row].Cells["N종목코드"].Value.ToString().Trim() == 종목코드)
                {
                    dg뉴스체결.Rows[row].Cells["N주문가"].Value = Decimal.Parse(체결가).ToString("###,###,###,##0");
                    return;
                }
            }
        }

        private void ucMainStock1_OnDsReceiveRealData(DataSet ds)
        {
            string str = "";
            DataRow dr;

            if (ds == null) return;
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1) return;


            dr = ds.Tables[0].Rows[0];

            for (int col = 0; col < ds.Tables[0].Columns.Count - 1; col++)
            {
                str += ds.Tables[0].Columns[col].ColumnName + " : " + dr[col].ToString().Trim() + " | ";
            }

            Logger(DateTime.Now.ToString("HH:mm:ss"), str);

            if (ds.Tables[0].Rows[0]["sRealType"].ToString().Trim() == "주식체결")
            {
                DataView dv;
                string 종목코드 = ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim();

                dv = new DataView(_dt화면관리);
                dv.RowFilter = "종목코드 = '" + 종목코드 + "' AND 실시간구분 = '1' AND 화면구분 = '1'";
                if (dv.Count > 0) SettingNewFav(ds);

                dv.RowFilter = "종목코드 = '" + 종목코드 + "' AND 실시간구분 = '1' AND 화면구분 = '2'";
                if (dv.Count > 0) SettingAccountData(ds);



                //TAB 3 조건검색 실시간 종목 처리 - S
                dv.RowFilter = "종목코드 = '" + 종목코드 + "' AND 실시간구분 = '1' AND 화면구분 = '3'";
                if (dv.Count > 0)
                {
                    if (_ds조건체결.Tables[종목코드] != null)
                    {
                        if (_ds조건체결.Tables[종목코드].Rows.Count > 0)
                        {
                            dr = _ds조건체결.Tables[종목코드].Rows[0];
                        }
                        else
                        {
                            dr = _ds조건체결.Tables[종목코드].NewRow();
                        }
                        for (int col = 0; col < ds.Tables[0].Columns.Count - 1; col++)
                        {
                            if (_ds조건체결.Tables[종목코드].Columns.Contains(ds.Tables[0].Columns[col].ColumnName) == false) { continue; }
                            dr[ds.Tables[0].Columns[col].ColumnName] = ds.Tables[0].Rows[0][ds.Tables[0].Columns[col].ColumnName];
                        }

                        string  금일최고거래량;
                        string  금일최저거래량;
                        string  금일최고체결강도;
                        string  금일최저체결강도;
                        if (_ds조건체결.Tables[종목코드].Rows.Count  > 0)
                        {
                            //금일최고거래량 = Convert.ToString(_ds조건체결.Tables[종목코드].Compute("MAX(거래량)", String.Empty));
                            //금일최저거래량 = Convert.ToString(_ds조건체결.Tables[종목코드].Compute("MIN(거래량)", String.Empty));
                            //금일최고체결강도 = Convert.ToString(_ds조건체결.Tables[종목코드].Compute("MAX(체결강도)", String.Empty));
                            //금일최저체결강도 = Convert.ToString(_ds조건체결.Tables[종목코드].Compute("MIN(체결강도)", String.Empty));

                            금일최고거래량 = dr["최고거래량"].ToString(); ;
                            금일최저거래량 = dr["최저거래량"].ToString(); ;
                            금일최고체결강도 = dr["최고체결강도"].ToString(); ;
                            금일최저체결강도 = dr["최저체결강도"].ToString(); ;
                            if (Convert.ToInt32(금일최고거래량) < Convert.ToInt32(dr["거래량"].ToString()))
                            {
                                금일최고거래량 = dr["거래량"].ToString();
                            }
                            if (Convert.ToInt32(금일최저거래량) > Convert.ToInt32(dr["거래량"].ToString()))
                            {
                                금일최저거래량 = dr["거래량"].ToString();
                            }
                            if (Convert.ToDecimal(금일최고체결강도) < Convert.ToDecimal(dr["체결강도"].ToString()))
                            {
                                금일최고체결강도 = dr["체결강도"].ToString();
                            }
                            if (Convert.ToDecimal(금일최저체결강도) > Convert.ToDecimal(dr["체결강도"].ToString()))
                            {
                                금일최저체결강도 = dr["체결강도"].ToString();
                            }
                            dr["최고거래량"] = 금일최고거래량;
                            dr["최저거래량"] = 금일최저거래량;
                            dr["최고체결강도"] = 금일최고체결강도;
                            dr["최저체결강도"] = 금일최저체결강도;
                        }
                        else
                        {
                            dr["최고거래량"] = dr["거래량"].ToString();
                            dr["최저거래량"] = dr["거래량"].ToString();
                            dr["최고체결강도"] = dr["체결강도"].ToString();
                            dr["최저체결강도"] = dr["체결강도"].ToString();
                            _ds조건체결.Tables[종목코드].Rows.Add(dr);
                        }
                    }
                    if (Math.Abs(Convert.ToInt32(_ds조건체결.Tables[종목코드].Rows[0]["최고거래량"].ToString().Trim())) > Math.Abs(Convert.ToInt32(_ds조건체결.Tables[종목코드].Rows[0]["최저거래량"].ToString().Trim())))
                    {

                    }
                    SettingConditionStockListDetailData(ds);
                }
                //TAB 3 조건검색 실시간 종목 처리 - E



                dv.RowFilter = "종목코드 = '" + 종목코드 + "' AND 실시간구분 = '1' AND 화면구분 = '4'";
                if (dv.Count > 0) { 
                    SettingFavData(ds);

                    if (UcHogaWindow1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
                    {
                        UcHogaWindow1.Property_GetStockTrade = ds;
                    }
                }
            }
            else if (ds.Tables[0].Rows[0]["sRealType"].ToString().Trim() == "주식호가잔량")
            {
                if (UcHogaWindow1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
                {
                    UcHogaWindow1.Property_GetStockHogaJanQty = ds;
                }
            }
            else if (ds.Tables[0].Rows[0]["sRealType"].ToString().Trim() == "주식당일거래원")
            {
                if (UcHogaWindow1.StockCode == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim())
                {
                    UcHogaWindow1.Property_ToDayStockTradeAt = ds;
                }
            }
        }

        private void SettingFavData(DataSet ds)
        {
            if (ds == null) return;
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1) return;
            DataRow dr = ds.Tables[0].Rows[0];

            string 종목코드 = "";
            for (int row = 0; row < dg관종.Rows.Count - 1; row++)
            {
                종목코드 = dg관종.Rows[row].Cells["F_종목코드"].Value.ToString();

                if (종목코드 != dr["STOCK_CODE"].ToString().Trim()) { continue; }
                dg관종.Rows[row].Cells["F_현재가"].Value = Decimal.Parse(dr["현재가"].ToString()).ToString("###,###,##0").Trim();
                dg관종.Rows[row].Cells["F_등락율"].Value = Decimal.Parse(dr["등락율"].ToString()).ToString("###,###,##0.00").Trim();
                dg관종.Rows[row].Cells["F_대비"].Value = Decimal.Parse(dr["전일대비"].ToString()).ToString("###,###,##0").Trim();
                dg관종.Rows[row].Cells["F_거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString()).ToString("###,###,###,###,##0").Trim();
                dg관종.Rows[row].Cells["F_체결강도"].Value = Decimal.Parse(dr["체결강도"].ToString()).ToString("###,###,##0.00").Trim();
                dg관종.Rows[row].Cells["F_시가"].Value = Decimal.Parse(dr["시가"].ToString()).ToString("###,###,##0").Trim();
                dg관종.Rows[row].Cells["F_저가"].Value = Decimal.Parse(dr["저가"].ToString()).ToString("###,###,##0").Trim();
                dg관종.Rows[row].Cells["F_고가"].Value = Decimal.Parse(dr["고가"].ToString()).ToString("###,###,##0").Trim();
                dg관종.Rows[row].Cells["F_시간"].Value = dr["체결시간"].ToString();
            }
        }

        private void SettingOPT10006(DataSet ds)
        {
            DataSet dsPreDayData;
            DataRow dr;
            DataView dv = new DataView(ds.Tables[0]);
            Decimal 조건체결강도 = Decimal.Parse(txt조건체결강도.Text.ToString().Trim());
            //string 조건시간  = DateTime.Now.AddMilliseconds(-2000).ToString("HHmmss");

            string 현재가 = "";
            if (dv.Count < 1) return;
            string 종목코드 = dv[0]["종목코드"].ToString().Trim();
            string 시가 = Math.Abs(Convert.ToInt32(dv[0]["시가"].ToString().Trim())).ToString();
            string 고가 = Math.Abs(Convert.ToInt32(dv[0]["고가"].ToString().Trim())).ToString();
            string 저가 = Math.Abs(Convert.ToInt32(dv[0]["저가"].ToString().Trim())).ToString();
            string 대비 = Convert.ToInt32(dv[0]["대비"].ToString().Trim()).ToString();

            //if (dv[0]["종가"].ToString().Trim() == "")
            //{
            //    dsPreDayData = _DataAcc.p_stock_day_data_query("5", 종목코드, "");
            //    if (dsPreDayData.Tables[0].Rows.Count > 0)
            //    {
            //        dr = dsPreDayData.Tables[0].Rows[0];

            //        if (dr["STOCK_DATE"].ToString().Trim() == DateTime.Now.ToString("yyyyMMdd")
            //            || Cls.GetWeekOfDay(DateTime.Now) == "토"
            //            || Cls.GetWeekOfDay(DateTime.Now) == "일"
            //            )
            //        {
            //            현재가 = Decimal.Parse(dr["END_PRICE"].ToString()).ToString(); ;
            //        }
            //        else
            //        {
            //            현재가 = (Decimal.Parse(dr["END_PRICE"].ToString()) + Decimal.Parse(dv[0]["대비"].ToString())).ToString();
            //        }

            //    }
            //    else
            //    {
            //        현재가 = "0";
            //    }
            //}
            //else
            //{
                현재가 = dv[0]["종가"].ToString().Trim();
            //}
            string 등락률 = dv[0]["등락률"].ToString().Trim();
            string 현재거래량 = dv[0]["거래량"].ToString().Trim();
            string 현재거래대금 = dv[0]["거래대금"].ToString().Trim();
            string 현재체결강도 = dv[0]["체결강도"].ToString().Trim();
            //int 주문가 = (int)Decimal.Parse(현재가);
            int 주문가 = 999999999;
            int 매수금액 = (int)Decimal.Parse(txt매수금액.Text);
            int 매수수량 = Math.Abs(매수금액 / 주문가);

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

            if (거래구분 != "")
            {
                //시장가 매수 체결된 정보의 주문가랑 조금은 틀릴수 있음 -- 정보요청을 덜하기 위한 수단임
                DataView dvDart = new DataView(_dtDart);
                dvDart.RowFilter = String.Format("종목코드 = '{0}'", 종목코드);
                dvDart.Sort = "시간 DESC";
                if (dvDart.Count > 0)
                {
                    if (dvDart[0]["제목"].ToString().IndexOf("3자배정") > -1 && dvDart[0]["제목"].ToString().IndexOf("자율") < 0)
                    {
                        SendBuySellMsg(종목코드, 거래구분, 1, 0, 주문가, 매수수량, "1");
                    }
                }
            }
            //TAB 0 처리 - S

            for (int row = 0; row < dgN관종.RowCount - 1; row++)
            {
                if (dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim() == 종목코드)
                {
                    //최초등록되자말자 10 이상 차이? 바로매수 할까? - E
                    //if (Decimal.Parse(dv[0]["체결강도"].ToString().Trim()) - Decimal.Parse(dv[dv.Count - 1]["체결강도"].ToString().Trim()) > 10) 
                    //{
                    //    //시장가 매수 체결된 정보의 주문가랑 조금은 틀릴수 있음 -- 정보요청을 덜하기 위한 수단임
                    //    SendBuySellMsg(종목코드, "03", 1, 0, 주문가, 매수수량 , "1");
                    //    if (Cls.isExistsRow(dg뉴스체결, 0, 종목코드) == false)
                    //    {
                    //        DataView dv1 = new DataView(UcMainStock1._allStockDataset.Tables[0]);
                    //        dv1.RowFilter = "STOCK_CODE = '" + 종목코드 + "'";


                    //        dg뉴스체결.Rows.Insert(0, 1);
                    //        dg뉴스체결.Rows[0].Cells["N종목코드"].Value = 종목코드;

                    //        if (dv1.Count > 0) { dg뉴스체결.Rows[0].Cells["N종목명"].Value = dv1[0]["STOCK_NAME"].ToString().Trim(); }
                    //        else { dg뉴스체결.Rows[0].Cells["N종목명"].Value = ""; }

                    //        dg뉴스체결.Rows[0].Cells["N주문가"].Value = 주문가.ToString("###,###,###,##0").Trim();
                    //        dg뉴스체결.Rows[0].Cells["N현재가"].Value = 주문가.ToString("###,###,###,##0").Trim();
                    //        dg뉴스체결.Rows[0].Cells["N수익률"].Value = "0.00";

                    //        dg뉴스체결.Rows[0].Cells["N거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
                    //        dg뉴스체결.Rows[0].Cells["N거래대금"].Value = (Math.Floor(Decimal.Parse(현재거래대금) / 1000000)).ToString("###,###,###,##0");
                    //        dg뉴스체결.Rows[0].Cells["N체결강도"].Value = 현재체결강도.ToString().Trim();
                    //        dg뉴스체결.Rows[0].Cells["N매수수량"].Value = 매수수량.ToString("###,###,###,##0");

                    //        SetDsScreenNo("D", "1", "1", 종목코드, "", false);
                    //        dgN관종.Rows.RemoveAt(row);
                    //        Application.DoEvents();

                    //        continue;
                    //    }
                    //}
                    //최초등록되자말자 10 이상 차이? 바로매수 할까? - E

                    //dgN관종.Rows[row].Cells["P현재가"].Value = Math.Abs(Decimal.Parse(현재가)).ToString("###,###,###,##0");
                    dgN관종.Rows[row].Cells["P등락률"].Value = 등락률;
                    dgN관종.Rows[row].Cells["P체결강도"].Value = 현재체결강도;
                    dgN관종.Rows[row].Cells["P거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
                    //dgN관종.Rows[row].Cells["P거래대금"].Value = (Math.Floor(Decimal.Parse(현재거래대금) / 1000000)).ToString("###,###,###,##0");
                    dgN관종.Rows[row].Cells["P거래대금"].Value = Int32.Parse(현재거래대금).ToString("###,###,###,##0");
                    if (Decimal.Parse(현재체결강도) > 0)
                    {
                        dgN관종.Rows[row].Cells["P최초등락률"].Value = 등락률;
                        dgN관종.Rows[row].Cells["P최초체결강도"].Value = 현재체결강도;
                        dgN관종.Rows[row].Cells["P최초거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
                        //dgN관종.Rows[row].Cells["P최초거래대금"].Value = (Math.Floor(Decimal.Parse(현재거래대금) / 1000000)).ToString("###,###,###,##0");
                        dgN관종.Rows[row].Cells["P최초거래대금"].Value = Int32.Parse(현재거래대금).ToString("###,###,###,##0");
                        dgN관종.Rows[row].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    //체결 30번전 체결강도랑 비교를 할까말까?
                    //if (Decimal.Parse(dv[0]["체결강도"].ToString().Trim()) < Decimal.Parse(dv[dv.Count - 1]["체결강도"].ToString().Trim())) {
                    //    //나가리???
                    //    dgN관종.Rows.RemoveAt(row);
                    //    Application.DoEvents();
                    //}
                }
            }
            //TAB 0 처리 - E

            //TAB 3 처리 - S
            for(int row = 0 ; row < dg조건종목.RowCount - 1;row++) {
                if (dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString() == 종목코드)
                {
                    //dg조건종목.Rows[row].Cells["C현재가"].Value = Math.Abs(Decimal.Parse(현재가)).ToString("###,###,###,##0");
                    dg조건종목.Rows[row].Cells["C등락률"].Value = 등락률;
                    dg조건종목.Rows[row].Cells["C거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
                    dg조건종목.Rows[row].Cells["C시가"].Value = Decimal.Parse(시가).ToString("###,###,###,##0");
                    dg조건종목.Rows[row].Cells["C고가"].Value = Decimal.Parse(고가).ToString("###,###,###,##0");
                    dg조건종목.Rows[row].Cells["C저가"].Value = Decimal.Parse(저가).ToString("###,###,###,##0");
                    Application.DoEvents();
                }
            }
            //TAB 3 처리 - E

            //TAB 4 처리 - S
            for (int row = 0; row < dg관종.RowCount - 1; row++)
            {
                if (dg관종.Rows[row].Cells["F_종목코드"].Value.ToString() == 종목코드)
                {
                    //dg관종.Rows[row].Cells["F_현재가"].Value = Math.Abs(Decimal.Parse(현재가)).ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_등락율"].Value = 등락률;
                    dg관종.Rows[row].Cells["F_거래량"].Value = Decimal.Parse(현재거래량).ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_시가"].Value = Decimal.Parse(시가).ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_고가"].Value = Decimal.Parse(고가).ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_저가"].Value = Decimal.Parse(저가).ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_체결강도"].Value = Decimal.Parse(현재체결강도).ToString("###,###,###,##0.00");
                    dg관종.Rows[row].Cells["F_대비"].Value = Decimal.Parse(대비).ToString("###,###,###,##0");
                    dg관종.Rows[row].Cells["F_시간"].Value = DateTime.Now.ToString("HHmmss");
                    Application.DoEvents();
                }
            }
            //TAB 4 처리 - E
        }

        //DataTable 사용시 int minLavel = Convert.ToInt32(dt.Compute("min(AccountLevel)", string.Empty));
        Hashtable _ht최고체결강도 = new Hashtable();

        //TAB 0 실시간데이터 처리 부분 - S
        private void SettingNewFav(DataSet ds)
        {
            string 종목코드 = "";
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                for (int row = 0; row < dgN관종.RowCount - 1; row++)
                {
                    종목코드 = dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim();
                    if (ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim() == 종목코드)
                    {
                        dgN관종.Rows[row].Cells["P현재가"].Value = Math.Abs(Decimal.Parse(dr["현재가"].ToString().Trim())).ToString("###,###,###,##0");
                        dgN관종.Rows[row].Cells["P등락률"].Value = dr["등락율"].ToString().Trim();
                        dgN관종.Rows[row].Cells["P체결강도"].Value = Decimal.Parse(dr["체결강도"].ToString().Trim()).ToString("###,###,###,##0.00");
                        dgN관종.Rows[row].Cells["P거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString().Trim()).ToString("###,###,###,##0");
                        dgN관종.Rows[row].Cells["P거래대금"].Value = Decimal.Parse(dr["누적거래대금"].ToString().Trim()).ToString("###,###,###,##0");

                        if (dgN관종.Rows[row].Cells["P최초거래시간"].Value == null || dgN관종.Rows[row].Cells["P최초거래시간"].Value.ToString().Trim() == "")
                        {
                            dgN관종.Rows[row].Cells["P최초등락률"].Value = dr["등락율"].ToString().Trim();
                            dgN관종.Rows[row].Cells["P최초체결강도"].Value = Decimal.Parse(dr["체결강도"].ToString().Trim()).ToString("###,###,###,##0.00");
                            dgN관종.Rows[row].Cells["P최초거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString().Trim()).ToString("###,###,###,##0");
                            dgN관종.Rows[row].Cells["P최초거래대금"].Value = Decimal.Parse(dr["누적거래대금"].ToString().Trim()).ToString("###,###,###,##0");
                            dgN관종.Rows[row].Cells["P최초거래시간"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            if (chk자동매매.Checked == false) return;
                            DateTime 최초거래시간 = DateTime.Parse(dgN관종.Rows[row].Cells["P최초거래시간"].Value.ToString().Trim());
                            TimeSpan ts = DateTime.Now - 최초거래시간;
                            int diffSecond = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
                            Decimal 조건시간 = Decimal.Parse(txt조건시간.Text.ToString().Trim());

                            if (diffSecond <= 조건시간) //시간비교 넘어갔을때 관종에서 삭제됨
                            {
                                Decimal 최초체결강도 = Decimal.Parse(dgN관종.Rows[row].Cells["P최초체결강도"].Value.ToString().Trim());
                                Decimal 현재체결강도 = Decimal.Parse(dr["체결강도"].ToString().Trim());

                                Decimal 최초거래대금 = Decimal.Parse(dgN관종.Rows[row].Cells["P최초거래대금"].Value.ToString().Trim());
                                Decimal 현재거래대금 = Decimal.Parse(dr["누적거래대금"].ToString().Trim());

                                Decimal 최초거래량 = Decimal.Parse(dgN관종.Rows[row].Cells["P최초거래량"].Value.ToString().Trim());
                                Decimal 현재거래량 = Decimal.Parse(dr["누적거래량"].ToString().Trim());

                                Decimal 조건체결강도 = Decimal.Parse(txt조건체결강도.Text.ToString().Trim());
                                Decimal 조건거래대금 = Decimal.Parse(txt조건거래대금.Text.ToString().Trim());

                                if (현재체결강도 - 최초체결강도 > 조건체결강도 &&
                                    현재거래대금 - 최초거래대금 > 조건거래대금
                                    )
                                {
                                    int 현재가 = (int)Decimal.Parse(dgN관종.Rows[row].Cells["P현재가"].Value.ToString().Trim());
                                    int 매수금액 = (int)Decimal.Parse(txt매수금액.Text);
                                    int 매수수량 = Math.Abs(매수금액 / 현재가);
                                    SendBuySellMsg(종목코드, "03", 1, 0, 현재가, 매수수량, "1");

                                    if (Cls.isExistsRow(dg뉴스체결, 0, dgN관종.Rows[row].Cells[0].Value.ToString().Trim()) == -1)
                                    {
                                        dg뉴스체결.Rows.Insert(0, 1);
                                        dg뉴스체결.Rows[0].Cells["N종목코드"].Value = dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim();
                                        dg뉴스체결.Rows[0].Cells["N종목명"].Value = dgN관종.Rows[row].Cells["P종목명"].Value.ToString().Trim();
                                        dg뉴스체결.Rows[0].Cells["N주문가"].Value = 현재가.ToString("###,###,###,##0").Trim();
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

                                        dg뉴스체결.Rows[0].Cells["N매수수량"].Value = 매수수량.ToString().Trim();
                                        dgN관종.Rows.RemoveAt(row);

                                    }
                                }
                            }
                            else
                            {
                                //조건시간 나가리
                                SetDsScreenNo("D", "1", "1", dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim(), "", false);
                                dgN관종.Rows.RemoveAt(row);

                                //키움API 오류로 인해 최초시간이 박히지 않을때가 있음 -- S
                                //for (int row1 = dgN관종.RowCount - 1; row1 >= 0; row1--)
                                //{
                                //    if (dgN관종.Rows[row1].Cells["P최초거래시간"].Value == null || dgN관종.Rows[row1].Cells["P최초거래시간"].Value.ToString().Trim() == "")
                                //    {
                                //        SetDsScreenNo("D", "1", "1", dgN관종.Rows[row1].Cells["P종목코드"].Value.ToString().Trim(), "", false);
                                //        dgN관종.Rows.RemoveAt(row1);
                                //    }
                                //}
                                //키움API 오류로 인해 최초시간이 박히지 않을때가 있음 -- E
                            }
                        }
                        Application.DoEvents();
                        break;
                    }
                }


                //체결쪽으로 넘어간 데이터를 검수하고 RealData를 업데이트 해준다. - S
                for (int row = 0; row < dg뉴스체결.RowCount - 1; row++)
                {
                    종목코드 = dg뉴스체결.Rows[row].Cells[0].Value.ToString().Trim();
                    if (ds.Tables[0].Rows[0]["STOCK_CODE"].ToString().Trim() == 종목코드)
                    {
                        dg뉴스체결.Rows[row].Cells["N현재가"].Value = Math.Abs(Decimal.Parse(dr["현재가"].ToString().Trim())).ToString("###,###,###,##0");
                        dg뉴스체결.Rows[row].Cells["N등락률"].Value = dr["등락율"].ToString().Trim();
                        dg뉴스체결.Rows[row].Cells["N체결강도"].Value = Decimal.Parse(dr["체결강도"].ToString().Trim()).ToString("###,###,###,##0.00");
                        dg뉴스체결.Rows[row].Cells["N거래량"].Value = Decimal.Parse(dr["누적거래량"].ToString().Trim()).ToString("###,###,###,##0");
                        dg뉴스체결.Rows[row].Cells["N거래대금"].Value = Decimal.Parse(dr["누적거래대금"].ToString().Trim()).ToString("###,###,###,##0");

                        Decimal temp1 = Decimal.Parse(dg뉴스체결.Rows[0].Cells["N주문가"].Value.ToString());
                        Decimal temp2 = Decimal.Parse(dg뉴스체결.Rows[0].Cells["N현재가"].Value.ToString());
                        dg뉴스체결.Rows[0].Cells["N수익률"].Value = ((temp2 - temp1) / temp1 * 100).ToString("###,###,###,##0.00");


                        //최고체결강도를 UPDATE 해준다. - S
                        if (_ht최고체결강도[종목코드] != null)
                        {
                            Decimal temp = Decimal.Parse(_ht최고체결강도[종목코드].ToString());
                            if (temp < Decimal.Parse(dg뉴스체결.Rows[0].Cells["N체결강도"].Value.ToString().Trim()))
                            {
                                _ht최고체결강도[종목코드] = Decimal.Parse(dg뉴스체결.Rows[0].Cells["N체결강도"].Value.ToString().Trim());
                            }
                        }
                        else
                        {
                            _ht최고체결강도.Add(종목코드, Decimal.Parse(dg뉴스체결.Rows[0].Cells["N체결강도"].Value.ToString().Trim()));
                        }
                        //최고체결강도를 UPDATE 해준다. - E


                        //수익률 or 최고 체결강도 비교를 한다. - S
                        if (txt수익률.Text.Trim() != "")
                        {
                            Decimal 현재체결강도 = Decimal.Parse(dg뉴스체결.Rows[row].Cells["N체결강도"].Value.ToString());
                            Decimal 최고체결강도 = Decimal.Parse(_ht최고체결강도[종목코드].ToString().Trim());
                            //Console.WriteLine((최고체결강도 - 현재체결강도).ToString());
                            if (
                                최고체결강도 - 현재체결강도 > 15 ||
                                Decimal.Parse(dg뉴스체결.Rows[row].Cells["N수익률"].Value.ToString().Trim()) >= Decimal.Parse(txt수익률.Text.Trim())
                                )
                            {
                                SendBuySellMsg(종목코드, "03", 2, 0,
                                    (int)Decimal.Parse(dg뉴스체결.Rows[row].Cells["N현재가"].Value.ToString().Trim()),
                                    (int)Decimal.Parse(dg뉴스체결.Rows[row].Cells["N매수수량"].Value.ToString().Trim()),
                                    "1");
                                SetDsScreenNo("D", "1", "1", 종목코드, "", false);
                                dg뉴스체결.Rows.RemoveAt(row);
                            }
                        }
                        //수익률 or 최고 체결강도 비교를 한다. - E
                        break;
                    } //if 종목코드
                } //for 전체

                //체결쪽으로 넘어간 데이터를 검수하고 RealData를 업데이트 해준다. - E
            }
        }
        //TAB 0 실시간데이터 처리 부분 - E


        //뉴스 Tab 에 리얼데이터 를 요청 하겠다는 함수 - 참고.OPT10003 은 최초 시간을 빠르게 가져 오기 위해 삽입 계속 바꾸어 가며 테스트중 - S
        private string RequestRealData(string 종목코드)
        {
            //if (UcMainStock1.InOptKWFidScreenNo("", 종목코드) == ucMainStock.ReturnScreenNo.Success)
            //{
                //string screenNo = UcMainStock1.GetOptKWFidScreenNo(종목코드);
                string screenNo = GetScrNum();
                UcMainStock1.GetOptKWFid(종목코드, 1, screenNo);
                //Application.DoEvents();
                return screenNo;
            //}
            //else
            //{
            //    //실시간데이터를 전송받고 있음
            //    string screenNo = UcMainStock1._dtOptKWFidScreenNo.Select(String.Format("STOCK_CODE = '{0}'", 종목코드))[0]["SCREEN_NO"].ToString();
            //    return screenNo;
            //}
        }
        //뉴스 Tab 에 RealData를 요청 하겠다는 함수 OPT10003 은 최초 시간을 빠르게 가져 오기 위해 삽입 계속 바꾸어 가며 테스트중 - E


        private void SetDsScreenNo(string actGb, string 화면구분, string 실시간구분, string 종목코드, string 화면번호, bool isOPT10006)
        {
            DataRow drAdd;
            DataRow[] drArr;

            if (actGb == "A")
            {
                if (실시간구분 == "1")
                {
                    if (isOPT10006 == true)
                    {
                        UcMainStock1.GetOpt10006(종목코드, "9002");
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(_SLEEP_TIME / 2);
                    }

                    drArr = _dt화면관리.Select(String.Format("실시간구분 = '{0}' AND 종목코드 = '{1}'", 실시간구분, 종목코드));
                    if (drArr.Length < 1)
                    {
                        화면번호 = RequestRealData(종목코드);
                    }
                    else
                    {
                        drArr = _dt화면관리.Select(String.Format("실시간구분 = '{0}' AND 종목코드 = '{1}' AND 화면구분 = '{2}'", 실시간구분, 종목코드, 화면구분));
                        if (drArr.Length > 1)
                        {
                            화면번호 = drArr[0]["화면번호"].ToString().Trim();
                        }
                        else
                        {
                            //화면번호 = UcMainStock1._dtOptKWFidScreenNo.Select(String.Format("STOCK_CODE = '{0}'", 종목코드))[0]["SCREEN_NO"].ToString();
                            화면번호 = GetScrNum();
                        }
                    }
                }

                if (화면번호 == "") return;

                drArr = _dt화면관리.Select(String.Format("화면구분 = '{0}' AND 실시간구분 = '{1}' AND 종목코드 = '{2}'", 화면구분, 실시간구분, 종목코드));
                if (drArr.Length > 0) return;

                drAdd = _dt화면관리.NewRow();
                drAdd["화면구분"] = 화면구분;
                drAdd["실시간구분"] = 실시간구분;
                drAdd["종목코드"] = 종목코드;
                drAdd["화면번호"] = 화면번호;
                _dt화면관리.Rows.Add(drAdd);
            }
            else if (actGb == "D")
            {
                DataRow[] drArrSub;

                drArr = _dt화면관리.Select(String.Format("화면구분 = '{0}' AND 실시간구분 = '{1}' AND 종목코드 = '{2}'", 화면구분, 실시간구분, 종목코드));
                foreach (DataRow dr in drArr)
                {
                    drArrSub = _dt화면관리.Select(String.Format("화면구분 <> '{0}' AND 실시간구분 = '{1}' AND 종목코드 = '{2}'", 화면구분, 실시간구분, 종목코드));
                    if (drArrSub.Length < 1)
                    {  // 다른 화면에서 사용할 가능성이 있기 때문에 끊지 않음
                        UcMainStock1.DisconnectRealData(dr["화면번호"].ToString()); //실시간 데이터가 필요 없을 경우 실시간 데이터 요청을 끊어준다. - S
                    }

                    _dt화면관리.Rows.Remove(dr);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(_SLEEP_TIME);
                }
            }
        }


        //조건검색 받는쪽 - S
        private void ucMainStock1_OnDsSetConditionList(DataSet ds)
        {
            if (ds == null) return;
            try
            {
                int row = 0;
                dg조건종목.RowCount = 1;
                //ds1 = _DataAcc.p_stock_day_data_query_MALowEnd("1", "", DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), null, null);
                //dv = new DataView(ds1.Tables[0]);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["STOCK_CODE"].ToString().Trim() == "") continue;
                    //dv.RowFilter = "STOCK_CODE = '" + dr["STOCK_CODE"].ToString().Trim() + "'";
                    //if (dv.Count < 1) continue;

                    dg조건종목.RowCount++;
                    dg조건종목.Rows[row].Cells["C종목코드"].Value = dr["STOCK_CODE"].ToString().Trim();
                    dg조건종목.Rows[row].Cells["C종목명"].Value = dr["STOCK_NAME"].ToString();

                    UcMainStock1.GetOpt10006(dr["STOCK_CODE"].ToString().Trim() , "9002");
                    System.Threading.Thread.Sleep(_SLEEP_TIME/2);

                    //SetDsScreenNo("A", "3", "1", dr["STOCK_CODE"].ToString().Trim(), "", false);
                    Application.DoEvents();
                    row++;
                }

                btn계산.PerformClick();
            }
            catch (Exception ex)
            {
                Logger("에러 (OnDsSetConditionList)", ex.ToString());
                if (UcMainStock1.EVENT_STATUS.STATUS_OnReceiveTrCondition = ModStatus.EventOff)
                {
                    UcMainStock1.OnReceiveTrCondition = ModStatus.EventOn;
                }
            }
        }
        //조건검색 받는쪽 - E

        private void ucMainStock1_OnDsStockByTradePortNumer(DataSet ds)
        {

        }

        private void ucMainStock1_OnDsTradePortInfo(DataRow dr)
        {

        }

        private void btn계산_Click(object sender, EventArgs e)
        {
            SetConditionPrice();
        }

        //조건 검색 API 25% , 50% , 75% , 0% 계산법 - S
        private void SetConditionPrice()
        {
            DataSet ds;
            DataView dv;
            string 종목코드;
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

            for (int row = 0; row < dg조건종목.Rows.Count - 1; row++)
            {

                dg조건종목.Rows[row].Cells["C1차매수가"].Value = "";
                dg조건종목.Rows[row].Cells["C2차매수가"].Value = "";
                dg조건종목.Rows[row].Cells["C3차매수가"].Value = "";

                종목코드 = dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString().Trim();

                ds = _DataAcc.p_stock_day_data_query("4", 종목코드, DateTime.Now.AddDays(-10).ToString("yyyyMMdd"), true, null, null);
                dv = new DataView(ds.Tables[0]);

                if (dg조건리스트.CurrentRow.Cells["조건명"].Value.ToString().IndexOf("시종") > -1)
                {
                    dv.RowFilter = "CANDLE_RATE >= 5.00 ";
                }
                else
                {
                    dv.RowFilter = "DAY_RATE >= 5.00 ";
                }
                
                dv.Sort = "L_PRICE ASC";
                if (dv.Count < 1) { continue; }

                종가 = Decimal.Parse(dv[0]["END_PRICE"].ToString().Trim());
                전일종가 = Decimal.Parse(dv[0]["PRE_E_PRICE"].ToString().Trim());
                시가 = Decimal.Parse(dv[0]["S_PRICE"].ToString().Trim());
                저가 = Decimal.Parse(dv[0]["L_PRICE"].ToString().Trim());
                고가 = Decimal.Parse(dv[0]["H_PRICE"].ToString().Trim());

                저종MA = Decimal.Parse(dv[0]["LOWEND_MA"].ToString().Trim());
                등락률 = Decimal.Parse(dv[0]["DAY_RATE"].ToString().Trim());

                if (등락률 < 10)
                {
                    지지선1 = (종가 + 저가) / 2;
                    dg조건종목.Rows[row].Cells["C1차매수가"].Value = 지지선1.ToString("###,###,###,##0").Trim();

                    temp = (지지선1 - 시가) / 시가 * 100;
                    if (temp < 2)
                    {
                        dg조건종목.Rows[row].Cells["C1차매수가"].Value = 시가.ToString("###,###,###,##0").Trim(); ;
                    }
                    else
                    {
                        dg조건종목.Rows[row].Cells["C1차매수가"].Value = 지지선1.ToString("###,###,###,##0").Trim(); ;
                    }

                    temp = (시가 - 저가) / 저가 * 100;

                    if (temp < 2)
                    {
                        dg조건종목.Rows[row].Cells["C2차매수가"].Value = 저가.ToString("###,###,###,##0").Trim(); ;
                        dg조건종목.Rows[row].Cells["C3차매수가"].Value = 저종MA.ToString("###,###,###,##0").Trim(); ;
                    }
                    else
                    {
                        dg조건종목.Rows[row].Cells["C2차매수가"].Value = 시가.ToString("###,###,###,##0").Trim(); ;
                        dg조건종목.Rows[row].Cells["C3차매수가"].Value = 저가.ToString("###,###,###,##0").Trim(); ;
                    }

                }
                else
                {
                    지지선1 = 종가 - ((종가 - 저가) / 4);
                    지지선2 = (종가 + 저가) / 2;
                    지지선3 = 저가 + ((종가 - 저가) / 4);

                    dg조건종목.Rows[row].Cells["C1차매수가"].Value = 지지선1.ToString("###,###,###,##0").Trim(); ;
                    dg조건종목.Rows[row].Cells["C2차매수가"].Value = 지지선2.ToString("###,###,###,##0").Trim(); ;
                    dg조건종목.Rows[row].Cells["C3차매수가"].Value = 지지선3.ToString("###,###,###,##0").Trim(); ;
                }


                dv.RowFilter = "";
                Application.DoEvents();
            }
        }
        //조건 검색 API 25% , 50% , 75% , 0% 계산법 - E

        private void SettingCondtionStockList()
        {

        }

        //계좌잔고 조회 - S
        private void SettingAccountList()
        {
            string strStockCode = "";
            int nCount = 0;
            string screenNo = dg잔고.Tag.ToString();

            for (int row = 0; row < dg잔고.RowCount - 1; row++)
            {
                if (dg잔고.Rows[row].Cells["종목번호"].Value.ToString() == "") return;
                strStockCode += Cls.Right(dg잔고.Rows[row].Cells["종목번호"].Value.ToString().Trim(), 6) + ";";
                nCount++;
            }
        }
        //계좌잔고 조회 - E

        private void UcMainStock1_OnDsSetConditionReal(DataSet ds)
        {
            string 종목코드 = "";
            string 종목명 = "";
            string 구분 = "";
            bool 존재여부 = false;
            if (ds == null) { return; }
            if (ds.Tables["CondiStockReal"].Rows.Count < 1) { return; }
            DataRow dr = ds.Tables[0].Rows[0];
            종목코드 = dr["STOCK_CODE"].ToString().Trim();
            종목명 = dr["STOCK_NAME"].ToString().Trim();
            구분 = dr["STR_TYPE"].ToString().Trim();

            for (int row = dg조건종목.RowCount - 1; row >= 0; row--)
            {
                //strType : 편입(“I”), 이탈(“D”) 
                if (dg조건종목.Rows[row].Cells["C종목코드"].Value.ToString().Trim() == 종목코드)
                {
                    if (구분 == "D")
                    {

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
                }
            }
        }

        //관종 실시간 리스트 리셋 - S
        private void btnReset_Click(object sender, EventArgs e)
        {
            //SetDsScreenNo("D", "1", "1", "028260", "", false);
            //Console.WriteLine(UcMainStock1._dtScreenNo.Rows.Count);
            for (int row = 0; row < dgN관종.RowCount - 1; row++)
            {
                SetDsScreenNo("D", "1", "1", dgN관종.Rows[row].Cells["P종목코드"].Value.ToString().Trim(), "", false);
            }
            dgN관종.RowCount = 1;
        }
        //관종 실시간 리스트 리셋 - E

        //체결된 실시간 리스트 리셋 - S
        private void btn뉴스체결_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < dg뉴스체결.RowCount - 1; row++)
            {
                SetDsScreenNo("D", "1", "1", dg뉴스체결.Rows[row].Cells["N종목코드"].Value.ToString().Trim(), "", false);
            }
            dg뉴스체결.RowCount = 1;
        }
        //체결된 실시간 리스트 리셋 - E

        private void txtTmDaum_Leave(object sender, EventArgs e)
        {
            tmrDaum.Start();
            tmrDaum.Interval = Int32.Parse(txtTmDaum1.Text.Trim());
        }

        private void txtTmNaver_Leave(object sender, EventArgs e)
        {
            tmrNaver.Start();
            tmrNaver.Interval = Int32.Parse(txtTmNaver2.Text.Trim());
        }

        private void txtTmDart_Leave(object sender, EventArgs e)
        {
            tmrDart.Start();
            tmrDart.Interval = Int32.Parse(txtTmDart3.Text.Trim());
        }


        //확장버튼 - S
        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (btnExpand.Text == "◀")
            {
                groupBox3.Width = 901;
                groupBox4.Width = 901;
                grp로그.Width = 901;
                groupBox3.Left -= 400;
                groupBox4.Left -= 400;
                btnExpand.Left -= 400;
                grp로그.Left -= 400;
                btnExpand.Text = "▶";
            }
            else
            {
                groupBox3.Width = 501;
                groupBox4.Width = 501;
                dgLog.Width = 501;
                groupBox3.Left += 400;
                groupBox4.Left += 400;
                btnExpand.Left += 400;
                grp로그.Left += 400;
                btnExpand.Text = "◀";
            }
        }
        //확장버튼 - E


        //체결정보요청 리시브 - S
        private void UcMainStock1_OnDsopt10003(DataSet ds)
        {
            //SettingOPT10006(ds);
        }
        //체결정보요청 리시브 - E

        private void dgN관종_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 1)
            {
                SetDsScreenNo("A", "1", "1", dgN관종.Rows[e.RowIndex].Cells["P종목코드"].ToString().Trim(), "", true);
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
                tmrDaum.Stop();
                tmrNaver.Stop();
                btnNewsStop.Text = "뉴스타이머중지(■)";
            }
            else
            {
                tmrDaum.Start();
                tmrNaver.Start();
                btnNewsStop.Text = "뉴스타이머실행(▶)";
            }
        }

        private void btn로그실행_Click(object sender, EventArgs e)
        {
            if (btn로그실행.Text == "■") btn로그실행.Text = "▶";
            else btn로그실행.Text = "■";
        }

        private void btn잔고_Click(object sender, EventArgs e)
        {
            UcMainStock1.Getopw00018(cboAccount.Items[cboAccount.SelectedIndex].ToString(), "", "", 1, "9001");
        }

        private void dg관종_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtF종목코드_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataView dv = new DataView(UcMainStock1._allStockDataset.Tables[0]);
                dv.RowFilter = "STOCK_NAME = '" + txtF종목코드.Text.Trim() + "'";

                bool blnTrue = false;
                if (dv.Count < 1) return;

                if (dg관종.Rows.Count == 1)
                {
                    dg관종.Rows.Insert(0, 1);
                    dg관종.Rows[0].Cells["F_종목코드"].Value = dv[0]["STOCK_CODE"].ToString().Trim();
                    dg관종.Rows[0].Cells["F_종목명"].Value = dv[0]["STOCK_NAME"].ToString().Trim();
                    dg관종.Rows[0].Cells["F_삭제"].Value = "D";
                    SetDsScreenNo("A", "4", "1", dv[0]["STOCK_CODE"].ToString().Trim(), "", true);
                }
                else
                {

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
                        dg관종.Rows.Insert(0, 1);
                        dg관종.Rows[0].Cells["F_종목코드"].Value = dv[0]["STOCK_CODE"].ToString().Trim();
                        dg관종.Rows[0].Cells["F_종목명"].Value = dv[0]["STOCK_NAME"].ToString().Trim();
                        dg관종.Rows[0].Cells["F_삭제"].Value = "D";
                        SetDsScreenNo("A", "4", "1", dv[0]["STOCK_CODE"].ToString().Trim(), "", true);
                    }
                }
                txtF종목코드.Text = "";
            }

        }

        private void dg관종_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) //삭제버튼클릭시
            {
                SetDsScreenNo("D", "4", "1", dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value.ToString().Trim(), "", false);
                dg관종.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void txtF종목코드_Enter(object sender, EventArgs e)
        {
            txtF종목코드.SelectAll();
        }

        private void dg관종_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value == null) return;
            UcHogaWindow1.StockCode = dg관종.Rows[e.RowIndex].Cells["F_종목코드"].Value.ToString().Trim();
        }

        private void UcMainStock1_OnDsopt10006(DataSet ds)
        {
            SettingOPT10006(ds);
        }

        private DataSet DaumNews(string SearchStr)
        {
            DataSet ds = new DataSet();
            string url = "http://search.daum.net/search?w=news&cluster=n&q=" + SearchStr + "&sort=1";

            //LoadPageAsync(_browser, url);
            //_browser.Load(url);
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                _browser.Load(url);
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

        private ChromiumWebBrowser _browser;
        private string _htmlSource = "";
        private void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            _browser = new ChromiumWebBrowser("");
            _browser.FrameLoadEnd += new EventHandler<CefSharp.FrameLoadEndEventArgs>(browser_FrameLoadEnd);
            this.Controls.Add(_browser);
            _browser.Dock = DockStyle.Fill;
        }

        private void browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                _browser.GetSourceAsync().ContinueWith(taskHtml =>
                {
                    _htmlSource = taskHtml.Result;
                });
            }
        }

        private void dg조건종목_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dg조건종목_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dg조건종목_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                dg조건종목.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dg조건종목_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string 종목코드 = "";
            if (e.RowIndex < 0 )  return;
            if (e.ColumnIndex == 0)
            {
                종목코드 = dg조건종목.Rows[e.RowIndex].Cells["C종목코드"].Value.ToString();

                if ((bool)dg조건종목.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == true)
                {
                    if (_ds조건체결.Tables[종목코드] == null)
                    {
                        _ds조건체결.Tables.Add(new DataTable(종목코드));
                        foreach (string str in _체결)
                        {
                            _ds조건체결.Tables[종목코드].Columns.Add(str);
                        }
                    }
                    SetDsScreenNo("A", "3", "1", 종목코드, "", false);
                }
                else if (dg조건종목.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && (bool)dg조건종목.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == false)
                {
                    if (_ds조건체결.Tables[종목코드] != null) _ds조건체결.Tables.Remove(_ds조건체결.Tables[종목코드]);
                    SetDsScreenNo("D", "3", "1", 종목코드, "", false);
                }
            }
        }

        private void btnDiscon_Click(object sender, EventArgs e)
        {
            //DataRow[] dr = UcMainStock1._dtOptKWFidScreenNo.Select(String.Format("STOCK_CODE = {0}", txtDisCon.Text.Trim()));
            //if (dr.Length > 0)
            //{
            //    UcMainStock1.DisconnectRealData(dr[0]["SCREEN_NO"].ToString());
            //}
        }
    }
}
