using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace Woom.Dart.Class
{
    class ClsDartApi
    {
        protected string callWebClientZipSave(string targetURL, string outputPath)
        {
            string result = string.Empty;
            string saveFolder = "";
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
        public static bool UnZipFiles(string zipFilePath, string unZipTargetFolderPath,
                                        string password, bool isDeleteZipFile)
        {

            // ZIP 파일이 있는 경우만 수행. 
            //  if (File.Exists(zipFilePath))
            //{
            //    // ZIP 스트림 생성. 
            //    ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath));


            //    // 패스워드가 있는 경우 패스워드 지정. 
            //    if (password != null && password != string.Empty)
            //    zipInputStream.Password = password;

            //    try
            //    {
            //        ZipEntry theEntry;
            //        long Count = 0;
            //        // 반복하며 파일을 가져옴. 
            //        while ((theEntry = zipInputStream.GetNextEntry()) != null)
            //        {
            //            // 폴더 
            //            string directoryName = Path.GetDirectoryName(theEntry.Name);
            //            string fileName = Path.GetFileName(theEntry.Name); // 파일 

            //            // 폴더 생성 
            //            Directory.CreateDirectory(unZipTargetFolderPath + directoryName);

            //            // 파일 이름이 있는 경우 
            //            if (fileName != string.Empty)
            //            {
            //                // 파일 스트림 생성.(파일생성) 
            //                FileStream streamWriter =
            //                      File.Create((unZipTargetFolderPath + theEntry.Name));

            //                int size = 2048;
            //                byte[] data = new byte[2048];

            //                // 파일 복사 
            //                while (true)
            //                {
            //                    size = zipInputStream.Read(data, 0, data.Length);

            //                    if (size > 0)
            //                    streamWriter.Write(data, 0, size);
            //                else
            //                        break;
            //                }

            //                // 파일스트림 종료 
            //                streamWriter.Close();
            //            }
            //            ++Count;
            //        }
            //    }
            //    catch
            //    {
            //        retVal = false;
            //    }
            //    finally
            //    {
            //        // ZIP 파일 스트림 종료 
            //        zipInputStream.Close();
            //    }

            //    // ZIP파일 삭제를 원할 경우 파일 삭제. 
            //    if (isDeleteZipFile)
            //        try
            //        {
            //            File.Delete(zipFilePath);
            //        }
            //        catch { }
            //}

            //return retVal;
            return true;
        }


    }
}
