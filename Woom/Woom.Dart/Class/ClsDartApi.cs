using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Data;
using ICSharpCode.SharpZipLib.Zip;
using IronPython.Hosting;
using SDataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Woom.DataDefine.Util;

namespace Woom.Dart.Class
{
    public class ClsDartApi
    {
        private void PythonConnect()
        {
            var peEngine = Python.CreateEngine();
            var vScope = peEngine.CreateScope();

            try
            {
                var vSource = peEngine.CreateScriptSourceFromFile(@"E:\ProjectSt\OpenDartReader-master");
                

            }
            catch (Exception)
            {

                throw;
            }
        }
        const string authKey = "fc9f7996b19984e91edab1bed1dd0a6249836aa8"; // 종근
        public string callWebClientZipSave(string targetURL, string outputPath)
        {
            string result = string.Empty;
            string saveFolder = @"C:\TEMP";
            Byte[] bytes = null; 
            try
            {
                WebClient client = new WebClient(); //특정 요청 헤더값을 추가해준다. //client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                client.Headers.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.3; WOW64; Trident/7.0)"); 
                using (Stream data = client.OpenRead(targetURL)) 
                { byte[] buffer = new byte[16 * 1024]; 
                    using (MemoryStream ms = new MemoryStream()) 
                    { int read; 
                        while ((read = data.Read(buffer, 0, buffer.Length)) > 0) 
                        { ms.Write(buffer, 0, read); } 
                        bytes = ms.ToArray(); 
                    } data.Close(); 
                } 
                string zipFileName = "corpCode_" + DateTime.Now.ToShortDateString() + ".zip"; 
                using (MemoryStream ms = new MemoryStream(bytes)) 
                { //write to file
                  FileStream file = new FileStream(saveFolder + "\\" + zipFileName, FileMode.Create, FileAccess.Write); 
                    ms.WriteTo(file); file.Close(); ms.Close(); } result = saveFolder + "\\" + zipFileName; } 
            catch 
            (Exception e) 
            { //통신 실패시 처리로직
              Console.WriteLine(e.ToString()); 
            } 
            return result;
        }

        /// 압축 파일 풀기 
        /// ZIP파일 경로 
        /// 압축 풀 폴더 경로 
        /// 해지 암호 
        /// zip파일 삭제 여부 
        /// 압축 풀기 성공 여부  
        /// password -null
        /// isDeleteZipFile true 지운다, false 안지운다
        public bool UnZipFiles(string zipFilePath, string unZipTargetFolderPath,
                                        string password, bool isDeleteZipFile, out string outFileName )
        {
            bool retVal = true;
            outFileName = "";
            //ZIP 파일이 있는 경우만 수행.
              if (File.Exists(zipFilePath))
            {
                // ZIP 스트림 생성. 
                ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath));

                // 패스워드가 있는 경우 패스워드 지정. 
                if (password != null && password != string.Empty)
                    zipInputStream.Password = password;

                try
                {
                    ZipEntry theEntry;
                    long Count = 0;
                    // 반복하며 파일을 가져옴. 
                    while ((theEntry = zipInputStream.GetNextEntry()) != null)
                    {
                        // 폴더 
                        string directoryName = Path.GetDirectoryName(theEntry.Name);
                        string fileName = Path.GetFileName(theEntry.Name); // 파일 

                        outFileName = fileName;

                        // 폴더 생성 
                        Directory.CreateDirectory(unZipTargetFolderPath + directoryName);

                        // 파일 이름이 있는 경우 
                        if (fileName != string.Empty)
                        {
                            // 파일 스트림 생성.(파일생성) 
                            FileStream streamWriter =
                                  File.Create((unZipTargetFolderPath + theEntry.Name));

                            int size = 2048;
                            byte[] data = new byte[2048];

                            // 파일 복사 
                            while (true)
                            {
                                size = zipInputStream.Read(data, 0, data.Length);

                                if (size > 0)
                                    streamWriter.Write(data, 0, size);
                                else
                                    break;
                            }

                            // 파일스트림 종료 
                            streamWriter.Close();
                        }
                        ++Count;
                    }
                }
                catch
                {
                    retVal = false;
                }
                finally
                {
                    // ZIP 파일 스트림 종료 
                    zipInputStream.Close();
                }

                // ZIP파일 삭제를 원할 경우 파일 삭제. 
                if (isDeleteZipFile)
                    try
                    {
                        File.Delete(zipFilePath);
                    }
                    catch { }
            }
              
            return retVal;
        }

        public DataTable Dart()
        {
            string authKey = "";
            int seed = DateTime.Now.Millisecond;
            Random r = new Random(seed);

            int ranNum = r.Next();
            //authKey = "fc9f7996b19984e91edab1bed1dd0a6249836aa8"; // 종근
     
            String apiURL = "http://dart.fss.or.kr/api/search.xml?auth=" + authKey + "&crp_cd=&start_dt=&end_dt=&page_set=100";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader1 = new StreamReader(response.GetResponseStream());

            string page = reader1.ReadToEnd();
            System.Data.DataTable dt = new System.Data.DataTable();

            StringReader sReader = new StringReader(page);

            System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

            dt.ReadXml(reader);
            return dt;
        }

        public DataTable Dart(string stockcode, string startDate, string endDate)
        {
            string authKey = "";
            int seed = DateTime.Now.Millisecond;
            Random r = new Random(seed);

            int ranNum = r.Next();

            String apiURL = "http://dart.fss.or.kr/api/search.xml?auth=" + authKey + "&crp_cd=" + stockcode + "&start_dt=" + startDate + "&end_dt=" + endDate + "&page_set=100&sort=date&series=asc";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader1 = new StreamReader(response.GetResponseStream());

            string page = reader1.ReadToEnd();
            System.Data.DataTable dt = new System.Data.DataTable();

            StringReader sReader = new StringReader(page);

            System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

            dt.ReadXml(reader);
            return dt;
        }
        /// <summary>
        /// 공시검색 개발가이드 	https://opendart.fss.or.kr/api/list.json - JSON https://opendart.fss.or.kr/api/list.xml - XML
        /// https://opendart.fss.or.kr/guide/detail.do?apiGrpCd=DS001&apiId=2019001
        /// </summary>
        /// <param name="crtfc_key">API 인증키</param>
        /// <param name="corp_code">고유번호</param>
        /// <param name="bgn_de">시작일</param>
        /// <param name="end_de">종료일</param>
        /// <param name="last_report_at">최종보고서 검색여부</param>
        /// <param name="bpIntf_ty">공시유형</param>
        /// <param name="pblntf_detail_ty">공시상세유형</param>
        /// <param name="corp_cls">법인구분</param>
        /// <param name="sort">정렬</param>
        /// <param name="sort_mth">정렬방법</param>
        /// <param name="page_no">페이지 번호</param>
        /// <param name="page_count">페이지 별 건수</param>
        /// <returns></returns>
        public DataSet GetDartSearchByDate(string stockCode, string crtfc_key, string corp_code, string bgn_de, string end_de, string last_report_at,
                                             string pbIntf_ty, string pblntf_detail_ty, string corp_cls, string sort, string sort_mth, string page_no, string page_count)
        {
            RichQuery richQuery = new RichQuery();
            DataTable dt = new DataTable();

            dt = richQuery.p_DARTQuery(query: "1", corp_code: "", stock_code: stockCode, stock_name: "", DELETE_DATE: "", bln3tier: false).Tables[0].Copy();

            if (dt.Rows.Count < 1)
            {
                return null;
            }

            string corpCode = dt.Rows[0]["corp_code"].ToString().Trim();

            

            //string apiUrl = "https://opendart.fss.or.kr/api/list.xml?" + "&crtfc_key=" + authKey + "&corp_code=" + corpCode + "&bgn_de=" + bgn_de + "&end_de=" + end_de +
            //                "&last_report_at=" + last_report_at + "&pbIntf_ty=" + pbIntf_ty + "&pblntf_detail_ty=" + pblntf_detail_ty + "&corp_cls=" + corp_cls + "&sort=" + sort +
            //                "&sort_mth=" + sort_mth + "&page_no=" + page_no + "&page_count=" + page_count;

            //string apiUrl = "https://opendart.fss.or.kr/api/list.json?" + "&crtfc_key=" + authKey + "&corp_code=" + corpCode + "&bgn_de=" + bgn_de + "&end_de=" + end_de +
            //                "&pbIntf_ty=" + pbIntf_ty + "&sort=" + sort +
            //                "&sort_mth=" + sort_mth + "&page_no=" + page_no + "&page_count=" + page_count;

            string apiUrl = "https://opendart.fss.or.kr/api/list.xml?" + "&crtfc_key=" + authKey + "&corp_code=" + corpCode + "&bgn_de=" + bgn_de + "&end_de=" + end_de +
                            "&pbIntf_ty=" + pbIntf_ty + "&sort=" + sort +
                            "&sort_mth=" + sort_mth + "&page_no=" + page_no + "&page_count=" + page_count;

            //string apiUrl = "https://opendart.fss.or.kr/api/list.json?" + "&crtfc_key=" + authKey + "&corp_code=" + corpCode + "&bgn_de=" + bgn_de + "&end_de=" + end_de +
            //                "&page_no=" + page_no + "&page_count=" + page_count;

            //string apiUrl = "https://opendart.fss.or.kr/api/list.json?crtfc_key=fc9f7996b19984e91edab1bed1dd0a6249836aa8&corp_code=00126380&bgn_de=20210117&end_de=20210717&pblntf_ty=A&pblntf_detail_ty=A001&corp_cls=Y&page_no=5&page_count=10";


            string webClientResult = callWebClient(apiUrl);

            int i = webClientResult.IndexOf("<status>");
            int j = webClientResult.IndexOf("<list>");

            ClsUtil clsUtil = new ClsUtil();

            string cutString = clsUtil.Mid(webClientResult, i + 1, j - i);

            //string makexml = webClientResult.Replace(cutString, "").Replace("<?xml version="1.0" encoding="UTF - 8" standalone="true"?>", "");
            StringReader sr = new StringReader(webClientResult.Replace(cutString, ""));

            DataSet ds = new DataSet();
            ds.ReadXml(sr);


            if (ds.Tables[0].Rows.Count < 0)
            {
                return null;
            }
            else
            {
                if (ds.Tables[0].Rows[0]["stock_code"].ToString().Trim() != stockCode)
                {
                    ArrayParam arrParam = new ArrayParam();
                    Sql oSql = new Sql(SDataAccess.ClsServerInfo.VADISSEVER, "RICHDB");

                    arrParam.Clear();
                    arrParam.Add("@ACTION_GB", "C");
                    arrParam.Add("@corp_code", corpCode);
                    arrParam.Add("@STOCK_CODE", stockCode);
                    arrParam.Add("@STOCK_NAME", "");
                    arrParam.Add("@DELETE_DATE", DateTime.Now.ToString("yyyyMMdd"));
                    arrParam.Add("@R_ERRORCD", -1, SqlDbType.Int, ParameterDirection.InputOutput);

                    oSql.ExecuteNonQuery("p_DartAdd", CommandType.StoredProcedure, arrParam);

                    GetDartSearchByDate(stockCode, crtfc_key, corp_code, bgn_de, end_de, last_report_at, pbIntf_ty, pblntf_detail_ty, corp_cls, sort, sort_mth, page_no, page_count);
                }
            }

            // < corp_code > 00119195 </ corp_code >
//< corp_name > 동화약품 </ corp_name >
// < stock_code > 000020 </ stock_code >
// < corp_cls > Y </ corp_cls >
// < report_nm > 공정거래자율준수프로그램운영현황(안내공시) </ report_nm >
// < rcept_no > 20210426800538 </ rcept_no >
 // < flr_nm > 동화약품 </ flr_nm >
// < rcept_dt > 20210426 </ rcept_dt >
// < rm > 유 </ rm >


            return ds;

        }

        public void GetDartDocuments(string rcept_no)
        {
            try
            {
                string path = @"C:\temp\";
                string filePath = "";
                string outStringFileName = "";
                DataSet ds = new DataSet();

                ClsDartApi clsDartApi = new ClsDartApi();

                filePath = clsDartApi.callWebClientZipSave("https://opendart.fss.or.kr/api/document.xml?crtfc_key=fc9f7996b19984e91edab1bed1dd0a6249836aa8&rcept_no=" + rcept_no, path);

                clsDartApi.UnZipFiles(filePath, path, "", true, out outStringFileName);

                FileStream fileStream = new FileStream(path + outStringFileName, FileMode.Open);

                ds.ReadXml(fileStream);

                fileStream.Close();

                //dgvList.DataSource = _ds.Tables[0];

                //GetDartCodeInStockName();

            }
            catch (Exception e)
            {
                throw;
            }
        }

        private string getResults(string apiUrl)
        {
            
            WebRequest request = WebRequest.Create(apiUrl);
            request.Method = "GET";
            string requestResult = "";
            using (var response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        requestResult = reader.ReadToEnd();
                    }
                }
            }


            return requestResult;
        }
        public static string callWebClient(string apiUrl)
        {
            string result = string.Empty;
            try
            {
                WebClient client = new WebClient();

                //특정 요청 헤더값을 추가해준다. 
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                using (Stream data = client.OpenRead(apiUrl))
                {
                    using (StreamReader reader = new StreamReader(data))
                    {
                        string s = reader.ReadToEnd();
                        result = s;

                        reader.Close();
                        data.Close();
                    }
                }

            }
            catch (Exception e)
            {
                //통신 실패시 처리로직
                Console.WriteLine(e.ToString());
            }
            return result;
        }


        public static string callWebRequest(string apiUrl)
        {
            string responseFromServer = string.Empty;

            try
            {

                WebRequest request = WebRequest.Create(apiUrl);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers["user-agent"] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
                using (WebResponse response = request.GetResponse())
                using (Stream dataStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    responseFromServer = reader.ReadToEnd();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return responseFromServer;
        }

    }
}
