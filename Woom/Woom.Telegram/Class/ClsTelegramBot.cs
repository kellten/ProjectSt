using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Cache;
using System.IO;

namespace Woom.Telegram.Class
{
    public class ClsTelegramBot
    {

        private static readonly string _baseUrl = "https://api.telegram.org/bot";
        private static readonly string _token = "1831992690:AAF-n31jzEBkxn6C0nxZFD846I75EAzlWns";
        public static string _chatid = "848121202";
        /// <summary>
        /// 텔레그램봇에 메시지를 보냅니다.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool SendMessage(string text, out string errorMessage)
        {

            return SendMessage(_chatid, text, out errorMessage);
        }

        public static  bool SendMessage(string chatId, string text, out string errorMessage)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            errorMessage = "";
            string url = string.Format("{0}{1}/sendMessage", _baseUrl, _token);

            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            req.Timeout = 30 * 1000;
            req.Method = "POST";
            req.ContentType = "application/json";

            string json = String.Format("{{\"chat_id\":\"{0}\", \"text\":\"{1}\"}}", chatId, EncodeJsonChars(text));
            byte[] data = UTF8Encoding.UTF8.GetBytes(json);
            req.ContentLength = data.Length;
            using (Stream stream = req.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }

            HttpWebResponse httpWebResponse = null;

            try
            {
                httpWebResponse = req.GetResponse() as HttpWebResponse;

                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    string responseData = null;
                    using (Stream responseStream = httpWebResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream, UTF8Encoding.UTF8))
                        {
                            responseData = reader.ReadToEnd();
                        }
                    }

                    if (0 < responseData.IndexOf("\"ok\":true"))
                    {
                        errorMessage = String.Empty;
                        return true;
                    }
                    else
                    {
                        errorMessage = String.Format("결과 json 파싱 오류 ({0})", responseData);
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
                throw;
            }
            finally
            {
                if (httpWebResponse != null)
                    httpWebResponse.Close();
            }
            return true;

        }

        private static string EncodeJsonChars(string text)
        {
            return text.Replace("\b", "\\\b")
                       .Replace("\f", "\\\f")
                       .Replace("\n", "\\\n")
                       .Replace("\r", "\\\r")
                       .Replace("\t", "\\\t")
                       .Replace("\"", "\\\"")
                       .Replace("\\", "\\\\");
        }
    }
}
