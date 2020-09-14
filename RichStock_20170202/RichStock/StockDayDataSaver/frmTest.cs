using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockDayDataSaver
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }
        private bool _IsReady = false;
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
                    _IsReady = true;
                    _htmlSource = taskHtml.Result;
                });
            }
        }

        private DataTable GetCEOAndETC(string stockCode)
        {
            DataSet ds = Dart(stockCode);
            string url = "http://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + ds.Tables["list"].Rows[0]["rcp_no"].ToString().Trim();
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                _browser.Load(url);
            }
            //while (browser.IsBusy || browser.DocumentText.Length < 5) { Application.DoEvents(); }
            _IsReady = false;
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


            } while (!_IsReady);

            //임원 및 직원의 현황

            source = _htmlSource;
            source = System.Net.WebUtility.HtmlDecode(source);
            
            text = tabformatSpaceRegex.Replace(source, string.Empty);
            text = tagWhiteSpaceRegex.Replace(text, "><");

            text = text.Substring(text.IndexOf("</thead><tbody><tr>") + 15);
            text = text.Substring(0 , text.IndexOf("</tbody>"));
            text = text.Replace("</p>", ":").Replace("<br>", ":");
            text = trformatSpaceRegex.Replace(text, "^");
            text = tdformatSpaceRegex.Replace(text, ",");
            text = stripFormattingRegex.Replace(text, "");

            string[] strTr = text.Split('^');
            string[] strTd;
            DataTable dt = new DataTable();
            DataRow dr;
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
            foreach (string str in strTr)
            {
                if (str == "") continue;
                dr = dt.NewRow();
                strTd = str.Split(',');
                dr["성명"] = strTd[0].Trim().Replace(":","");
                dr["성별"] = strTd[1].Trim().Replace(":", "");
                dr["출생년월"] = strTd[2].Trim().Replace(":", "");
                dr["직위"] = strTd[3].Trim().Replace(":", "");
                dr["등기임원여부"] = strTd[4].Trim().Replace(":", "");
                dr["상근여부"] = strTd[5].Trim().Replace(":", "");
                dr["담당업무"] = strTd[6].Trim().Replace(":", "");
                dr["주요경력"] = strTd[7].Trim().Replace(":", Environment.NewLine);
                dr["의결권있는주식"] = strTd[8].Trim().Replace(":", "");
                dr["의결권없는주식"] = strTd[9].Trim().Replace(":", "");
                dr["재직기간"] = strTd[10].Trim().Replace(":", "");
                dr["임기만료일"] = strTd[11].Trim().Replace(":", "");
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

            String apiURL = "http://dart.fss.or.kr/api/search.xml?auth=" + authKey + "&crp_cd="+stockCode+"&start_dt=19990101&bsn_tp=A003&page_set=1";
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

        private void frmTest_Load(object sender, EventArgs e)
        {
            InitBrowser();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1 = new DataGridView();
            
        }
    }
}
