using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Woom.Dart.Class
{
    class ClsDartApi
    {
        protected string callWebClientZipSave(string targetURL, string outputPath)
        {
            string result = string.Empty; Byte[] bytes = null; try
            {
                WebClient client = new WebClient(); //특정 요청 헤더값을 추가해준다. 
                                                    //client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)"); 
                client.Headers.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.3; WOW64; Trident/7.0)");
                using (Stream data = client.OpenRead(targetURL))
                { byte[] buffer = new byte[16 * 1024];
                    using (MemoryStream ms = new MemoryStream())
                    { int read; while ((read = data.Read(buffer, 0, buffer.Length)) > 0)
                        { ms.Write(buffer, 0, read); } bytes = ms.ToArray(); } data.Close(); }
                string zipFileName = "corpCode_" + DateTime.Now.ToShortDateString() + ".zip";
                using (MemoryStream ms = new MemoryStream(bytes))
                { //write to file FileStream file = new FileStream(txtSaveFolder.Text + "\\" + zipFileName, FileMode.Create, FileAccess.Write); 
                    ms.WriteTo(file); file.Close(); ms.Close(); }
                result = txtSaveFolder.Text + "\\" + zipFileName; } catch (Exception e) { //통신 실패시 처리로직 Console.WriteLine(e.ToString()); } return result; }

               // 출처: https://checkblock.tistory.com/72?category=536063 [체크개발자's Blog]
    }
}
