using PaikRichStock.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

public static class Cls
{
    public static Int64 ValInt64(object value)
    {
        //bool isKiho = false;
        //String result = String.Empty;
        //String valueTemp = value.ToString();
        //foreach (char c in valueTemp)
        //{
        //    if (c.Equals('+') || c.Equals('-')) { isKiho = true; continue; }
        //    if (c.Equals(',')) { continue; }
        //    if (c.Equals('.')) { break ; }
        //    if (Char.IsNumber(c))
        //        result += c;
        //    else if (!c.Equals(' '))
        //        return String.IsNullOrEmpty(result) ? 0 : Convert.ToInt32(result);
        //}
        //if (isKiho) { result = valueTemp.Substring(0, 1) + result; }
        //return String.IsNullOrEmpty(result) ? 0 : Convert.ToInt32(result);
        return Convert.ToInt64(Conversion.Val(value.ToString().Replace(",", "")).ToString("#0"));
    }

    public static Int32 ValInt(object value)
    {
        //bool isKiho = false;
        //String result = String.Empty;
        //String valueTemp = value.ToString();
        //foreach (char c in valueTemp)
        //{
        //    if (c.Equals('+') || c.Equals('-')) { isKiho = true; continue; }
        //    if (c.Equals(',')) { continue; }
        //    if (c.Equals('.')) { break ; }
        //    if (Char.IsNumber(c))
        //        result += c;
        //    else if (!c.Equals(' '))
        //        return String.IsNullOrEmpty(result) ? 0 : Convert.ToInt32(result);
        //}
        //if (isKiho) { result = valueTemp.Substring(0, 1) + result; }
        //return String.IsNullOrEmpty(result) ? 0 : Convert.ToInt32(result);
        if (value == null) return 0;
        return Convert.ToInt32(Conversion.Val(value.ToString().Replace(",", "")).ToString("#0"));
    }

    public static Double Val(object value)
    {
        //bool isKiho = false;
        //String result = String.Empty;
        //String valueTemp = value.ToString();

        //foreach (char c in valueTemp)
        //{
        //    if (c.Equals('+') || c.Equals('-')) {isKiho = true; continue;}
        //    if (c.Equals(',')) { continue; }
        //    if (Char.IsNumber(c) || (c.Equals('.') && result.Count(x => x.Equals('.')) == 0))
        //        result += c;
        //    else if (!c.Equals(' '))
        //        return String.IsNullOrEmpty(result) ? 0 : Convert.ToDouble(result);
        //}
        //if (isKiho) { result = valueTemp.Substring(0, 1) + result; }
        //return String.IsNullOrEmpty(result) ? 0 : Convert.ToDouble(result);
        if (value == null) return 0;
        return Conversion.Val(value.ToString().Replace("," , ""));
    }

    public static void WriteDataSet(DataSet ds) {
        string str = "";
        DataRow dr = ds.Tables[0].Rows[0];
        for (int col = 0; col < ds.Tables[0].Columns.Count - 1; col++)
        {
            str += ds.Tables[0].Columns[col].ColumnName + " : " + dr[col].ToString().Trim() + " | ";
        }
        Console.WriteLine(str);
    }
    public static DataTable ConvertRelateHtml(string html)
    {
        DataTable dt = new DataTable("RELATE");
        dt.Columns.Add("RELATE_STR");

        string[] arr;
        arr = Regex.Split(html, "\"*q\">");
        //string step1 = html.Substring(start, count);

        for (int ix = 1; ix < arr.Length; ix++)
        {
            if (arr[ix].Trim().Substring(0, 1) == "<") continue;

            DataRow dr = dt.Rows.Add();
            dr["RELATE_STR"] = arr[ix].Substring(0, arr[ix].IndexOf("</a>"));
        }
        return dt;
    }

    public static void ChangeColor(DataGridViewRow dgr, string sort, Double condiNum = 0, params int[] cols)
    {
        for (int col = 0; col < cols.Length; col++)
        {
            if (dgr.Cells[cols[col]].Value == null) continue;
            Double x;
            x = Cls.Val(dgr.Cells[cols[col]].Value);

            dgr.Cells[cols[col]].Style.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dgr.Cells[cols[col]].Style.ForeColor = System.Drawing.Color.Empty;

            if (sort == "D")
            {
                if (x < condiNum) dgr.Cells[cols[col]].Style.ForeColor = System.Drawing.Color.Red;
                else if (x > condiNum) dgr.Cells[cols[col]].Style.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                if (x < condiNum) dgr.Cells[cols[col]].Style.ForeColor = System.Drawing.Color.Blue;
                else if (x > condiNum) dgr.Cells[cols[col]].Style.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    public static Object ToNumber(string str, Type type, string format)
    {
        Object obj = null;
        Int32 rInt32;
        Int64 rInt64;
        Double rDouble;
        Decimal rDecimal;
        bool ret = false;

        str = str.Replace(",", "");
        if (type == null && str.IndexOf(".") > -1) type = Type.GetType("System.Double");
        if (type == null) type = Type.GetType("System.Int32");
        

        if (type == Type.GetType("System.Int32"))
        {
            str = str.IndexOf(".") > -1 ? str.Substring(0, str.IndexOf(".")) : str;
            ret = Int32.TryParse(str, out rInt32);
            if (ret)
            {
                if (format != "") obj = String.Format(format, rInt32);
                else obj = rInt32;
            }
            else obj = str;
        }
        if (type == Type.GetType("System.Int64"))
        {
            str = str.IndexOf(".") > -1 ? str.Substring(0, str.IndexOf(".")) : str;
            ret = Int64.TryParse(str, out rInt64);
            if (ret)
            {
                if (format != "") obj = String.Format(format, rInt64);
                else obj = rInt64;
            }
            else obj = str;
        }
        if (type == Type.GetType("System.Double"))
        {
            ret = Double.TryParse(str, out rDouble);
            if (ret)
            {
                if (format != "") obj = String.Format(format, rDouble);
                else obj = rDouble;
            }
            else obj = str;
        }
        if (type == Type.GetType("System.Decimal"))
        {
            ret = Decimal.TryParse(str, out rDecimal);
            if (ret)
            {
                if (format != "") obj = String.Format(format, rDecimal);
                else obj = rDecimal;
            }
            else obj = str;
        }

        
        return obj;
    }

    public static string GetWeekOfDay(DateTime dt)
    {
        string strDay = "";
        switch(dt.DayOfWeek)
        {
            case DayOfWeek.Monday:
                strDay = "월";
                break;
            case DayOfWeek.Tuesday:
                strDay = "화";
                break;
            case DayOfWeek.Wednesday:
                strDay = "수";
                break;
            case DayOfWeek.Thursday:
                strDay = "목";
                break;
            case DayOfWeek.Friday:
                strDay = "금";
                break;
            case DayOfWeek.Saturday:
                strDay = "토";
                break;
            case DayOfWeek.Sunday:
                strDay = "일";
                break;
        }
        return strDay;
    }

    public static int isExistsRow(DataGridView dg , int col , string oStr)
    {
        if (dg.RowCount < 2) return -1;
        for (int row = 0; row < dg.RowCount - 1; row++)
        {
            if (dg.Rows[row].Cells[col].Value == null) { continue; }

            if (dg.Rows[row].Cells[col].Value.ToString().Trim() == oStr)
            {
                return row;
            }
        }
        return -1;
    }

    public static string HtmlToPlainText(string html)
    {
        const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
        const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
        const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
        var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
        var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
        var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

        var text = html;
        //Decode html specific characters
        text = System.Net.WebUtility.HtmlDecode(text);
        //Remove tag whitespace/line breaks
        text = tagWhiteSpaceRegex.Replace(text, "><");
        //Replace <br /> with line breaks
        text = lineBreakRegex.Replace(text, Environment.NewLine);
        //Strip formatting
        text = stripFormattingRegex.Replace(text, string.Empty);

        return text;
    }

    public static DataSet NaverNews(string SearchStr , int count)
    {
        string str = System.Web.HttpUtility.UrlEncode(SearchStr, System.Text.Encoding.UTF8);
        DataSet ds = new DataSet();
        String apiURL = "https://openapi.naver.com/v1/search/news.xml?query=" + str + "&display=" + count.ToString()+ "&sort=date";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);

        int seed = DateTime.Now.Millisecond;
        Random r = new Random(seed);

        int ranNum = r.Next();

        request.Method = "GET";

        if (ranNum % 4 == 0) { 
            request.Headers.Add("X-Naver-Client-Id", "MbZd5RTwx737EUZkTS4R"); //시영
            request.Headers.Add("X-Naver-Client-Secret", "E7u5wgKVW2"); //시영
        }
        else if (ranNum % 4 == 1)
        {
            request.Headers.Add("X-Naver-Client-Id", "lLk8wyiPEClhz4iSE5uS"); //동권
            request.Headers.Add("X-Naver-Client-Secret", "AO5aMsIpja");  //동권
        }
        else if (ranNum % 4 == 2)
        {
            request.Headers.Add("X-Naver-Client-Id", "QHyiah6SUKyP9THlnFZm"); //병수
            request.Headers.Add("X-Naver-Client-Secret", "QbAoDXqYoG");  //병수
        }
        else
        {
            request.Headers.Add("X-Naver-Client-Id", "dIcPMP6xI1dX3QKlm2Qw "); //종근
            request.Headers.Add("X-Naver-Client-Secret", "Cc7SdrsvrR"); //종근
        }


        try { 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader1 = new StreamReader(response.GetResponseStream());

            string page = reader1.ReadToEnd();
            StringReader sReader = new StringReader(page);

            System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

            ds.ReadXml(reader);
            return ds;
        }
        catch { return null; }
    }

    //Daum 은 뉴스 검색을 제공하고 있지 않습니다. '해당답변 뉴스는 계약 관계 때문에 문제가 많이 발생해서 막은거라 제공하기 어려울 것 같습니다. 
    //public static DataSet DaumNews(string SearchStr)
    //{
    //    String apiURL = "https://apis.daum.net/search/web?apikey=129f8f599040b8b48d7eb1c7f967d231&q=" + SearchStr + "&output=XML";

    //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);

    //    request.Method = "GET";
    //    request.Headers.Add("X-Naver-Client-Id", "MbZd5RTwx737EUZkTS4R"); //시영
    //    request.Headers.Add("X-Naver-Client-Secret", "E7u5wgKVW2"); // 시영
    //    //request.Headers.Add("X-Naver-Client-Id", "lLk8wyiPEClhz4iSE5uS");
    //    //request.Headers.Add("X-Naver-Client-Secret", "AO5aMsIpja");

    //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

    //    StreamReader reader1 = new StreamReader(response.GetResponseStream());

    //    string page = reader1.ReadToEnd();
    //    System.Data.DataSet ds = new System.Data.DataSet();

    //    StringReader sReader = new StringReader(page);

    //    System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

    //    ds.ReadXml(reader);
    //    return ds;
    //}

    public static DataSet NaverBlog(string SearchStr, int count)
    {
        String text = System.Web.HttpUtility.UrlEncode(SearchStr, System.Text.Encoding.UTF8);
        DataSet ds = new DataSet();
        String apiURL = "https://openapi.naver.com/v1/search/blog.xml?query=" + text + "&display=" + count.ToString() + "&sort=date";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);

        int seed = DateTime.Now.Millisecond;
        Random r = new Random(seed);

        int ranNum = r.Next();
        if (ranNum % 4 == 0)
        {
            request.Headers.Add("X-Naver-Client-Id", "MbZd5RTwx737EUZkTS4R"); //시영
            request.Headers.Add("X-Naver-Client-Secret", "E7u5wgKVW2"); //시영
        }
        else if (ranNum % 4 == 1)
        {
            request.Headers.Add("X-Naver-Client-Id", "lLk8wyiPEClhz4iSE5uS"); //동권
            request.Headers.Add("X-Naver-Client-Secret", "AO5aMsIpja");  //동권
        }
        else if (ranNum % 4 == 2)
        {
            request.Headers.Add("X-Naver-Client-Id", "QHyiah6SUKyP9THlnFZm"); //병수
            request.Headers.Add("X-Naver-Client-Secret", "QbAoDXqYoG");  //병수
        }
        else
        {
            request.Headers.Add("X-Naver-Client-Id", "dIcPMP6xI1dX3QKlm2Qw "); //종근
            request.Headers.Add("X-Naver-Client-Secret", "Cc7SdrsvrR"); //종근
        }
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        StreamReader reader1 = new StreamReader(response.GetResponseStream());

        string page = reader1.ReadToEnd();
        StringReader sReader = new StringReader(page);

        System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

        ds.ReadXml(reader);
        return ds;
    }

    public static bool IsNumeric(string str)
    {
        Decimal result;
        if (Decimal.TryParse(str, out result)) { return true; }
        return false;
    }

    public static DataSet Dart()
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

        String apiURL = "http://dart.fss.or.kr/api/search.xml?auth=" + authKey + "&crp_cd=&start_dt=&end_dt=&page_set=100";
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

    public static DataSet Dart(string stockcode , string startDate , string endDate)
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

        String apiURL = "http://dart.fss.or.kr/api/search.xml?auth=" + authKey + "&crp_cd=" + stockcode + "&start_dt=" + startDate + "&end_dt=" + endDate + "&page_set=100&sort=date&series=asc";
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
    //Mid
    /// <summary>
    /// 문자열 원본의 지정한 위치에서 부터 추출할 갯수 만큼 문자열을 가져옵니다.
    /// </summary>
    /// <param name="sString">문자열 원본</param>
    /// <param name="nStart">추출을 시작할 위치</param>
    /// <param name="nLength">추출할 갯수</param>
    /// <returns>추출된 문자열</returns>
    public static string Mid(string sString, int nStart, int nLength)
    {
        string sReturn;

        //VB에서 문자열의 시작은 0이 아니므로 같은 처리를 하려면 
        //스타트 위치를 인덱스로 바꿔야 하므로 -1을 하여
        //1부터 시작하면 0부터 시작하도록 변경하여 준다.
        --nStart;

        //시작위치가 데이터의 범위를 안넘겼는지?
        if (nStart <= sString.Length)
        {
            //안넘겼다.

            //필요한 부분이 데이터를 넘겼는지?
            if ((nStart + nLength) <= sString.Length)
            {
                //안넘겼다.
                sReturn = sString.Substring(nStart, nLength);
            }
            else
            {
                //넘겼다.

                //데이터 끝까지 출력
                sReturn = sString.Substring(nStart);
            }

        }
        else
        {
            //넘겼다.

            //그렇다는 것은 데이터가 없음을 의미한다.
            sReturn = string.Empty;
        }

        return sReturn;
    }

    //Left
    /// <summary>
    /// 문자열 원본에서 왼쪽에서 부터 추출한 갯수만큼 문자열을 가져옵니다.
    /// </summary>
    /// <param name="sString">문자열 원본</param>
    /// <param name="nLength">추출할 갯수</param>
    /// <returns>추출된 문자열</returns>
    public static string Left(string sString, int nLength)
    {
        string sReturn;

        //추출할 갯수가 문자열 길이보다 긴지?
        if (nLength > sString.Length)
        {
            //길다!

            //길다면 원본의 길이만큼 리턴해 준다.
            nLength = sString.Length;
        }

        //문자열 추출
        sReturn = sString.Substring(0, nLength);

        return sReturn;
    }

    //Right
    /// <summary>
    /// 문자열 원본에서 오른쪽에서 부터 추출한 갯수만큼 문자열을 가져옵니다.
    /// </summary>
    /// <param name="sString">문자열 원본</param>
    /// <param name="nLength">추출할 갯수</param>
    /// <returns>추출된 문자열</returns>
    public static string Right(string sString, int nLength)
    {
        string sReturn;

        //추출할 갯수가 문자열 길이보다 긴지?
        if (nLength > sString.Length)
        {
            //길다!

            //길다면 원본의 길이만큼 리턴해 준다.
            nLength = sString.Length;
        }

        //문자열 추출
        sReturn = sString.Substring(sString.Length - nLength, nLength);

        return sReturn;

    }

    public static DataTable GetDayMaDt(DataTable dt)
    {
        DataTable dtMa = new DataTable();
        double sum = 0;
        dtMa = dt.Clone();
        DataColumn[] columns = new DataColumn[] { dtMa.Columns["일자"] };
        dtMa.PrimaryKey = columns;
        dtMa.Columns.Add("Line3", Type.GetType("System.Double"));
        dtMa.Columns.Add("Line5", Type.GetType("System.Double"));
        dtMa.Columns.Add("Line10", Type.GetType("System.Double"));
        dtMa.Columns.Add("Line20", Type.GetType("System.Double"));
        dtMa.Columns.Add("Line40", Type.GetType("System.Double"));
        dtMa.Columns.Add("Line60", Type.GetType("System.Double"));
        dtMa.Columns.Add("Line120", Type.GetType("System.Double"));
        dtMa.Columns.Add("Line240", Type.GetType("System.Double"));
        dtMa.Columns.Add("BBH20", Type.GetType("System.Double"));
        dtMa.Columns.Add("BBL20", Type.GetType("System.Double"));
        dtMa.Columns.Add("BBH40", Type.GetType("System.Double"));
        dtMa.Columns.Add("BBL40", Type.GetType("System.Double"));
        int row = 0;
        int line = 0;
        foreach (DataRow dr in dt.Rows)
        {
            sum = 0;
            dtMa.Rows.Add(dr.ItemArray);


            for (int col = dt.Columns.Count - 1; col < dtMa.Columns.Count; col ++ )
            {
                if(dtMa.Columns[col].ColumnName.IndexOf ("Line") < 0) continue;

                line = Convert.ToInt32(dtMa.Columns[col].ColumnName.Replace("Line", ""));
                if (dt.Rows.Count >= row + line)
                {
                    sum = Convert.ToDouble(dt.Compute("SUM(현재가)", String.Format("일자 <= '{0}' AND 일자 >= '{1}'", dt.Rows[row]["일자"].ToString(), dt.Rows[row + line - 1]["일자"].ToString())).ToString());
                    dtMa.Rows[dtMa.Rows.Count - 1]["Line" + line] = Math.Round(sum / line, 2);
                }
            }
            row++;
        }

        //drTemp = dtMa.NewRow();
        //drTemp["일자"] = "29990101";
        //row = 0;
        //for (int col = dt.Columns.Count - 1; col < dtMa.Columns.Count; col++)
        //{
        //    if (dtMa.Columns[col].ColumnName.IndexOf("Line") < 0) continue;

        //    line = Convert.ToInt32(dtMa.Columns[col].ColumnName.Replace("Line", ""));
        //    if (dt.Rows.Count >= row + line)
        //    {
        //        sum = Convert.ToDouble(dt.Compute("SUM(현재가)", String.Format("일자 <= '{0}' AND 일자 >= '{1}'", dt.Rows[row]["일자"].ToString(), dt.Rows[line - 1]["일자"].ToString())).ToString());
        //        drTemp["Line" + line] = sum - Convert.ToDouble(dt.Rows[row]["현재가"].ToString());
        //    }
        //}
        //dtMa.Rows.Add(drTemp);
        return dtMa;
    }

    public static bool IsDb(DataSet ds)
    {
        if (ds == null) return false;
        if (ds.Tables.Count == 0) return false;
        if (Cls.GetWeekOfDay(DateTime.Today) == "토" || Cls.GetWeekOfDay(DateTime.Today) == "일" ||
            ds.Tables[0].Rows[0]["STOCK_DATE"].ToString() == DateTime.Today.ToString("yyyyMMdd") ||
            (Convert.ToInt32(DateTime.Now.ToString("HH")) >= 0 && Convert.ToInt32(DateTime.Now.ToString("HH")) <= 6)
        ) { return true; }

        return false;
    }

    public static bool IsJangTime()
    {
        if (Cls.GetWeekOfDay(DateTime.Today) == "토" || Cls.GetWeekOfDay(DateTime.Today) == "일" ||
            (Convert.ToInt32(DateTime.Now.ToString("HHmm")) < 900 || Convert.ToInt32(DateTime.Now.ToString("HHmm")) > 1530)
        ) { return false; }

        return true;
    }

    public static void DoubleBuffered(this DataGridView dgv, bool setting)
    {
        Type dgvType = dgv.GetType();
        PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
            BindingFlags.Instance | BindingFlags.NonPublic);
        pi.SetValue(dgv, setting, null);
    }
}