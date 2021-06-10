using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;
using System.IO;

namespace Woom.CallForm.Uc
{
    public partial class UcNaverSearch : UserControl
    {

        const string _apiUrl = "https://openapi.naver.com/v1/search/news.json";
        const string _clientId = "dIcPMP6xI1dX3QKlm2Qw"; //Application Client ID 입력
        const string _clientSecret = "CyyzooLmi3"; //Application Client Secret 입력

        public string PropStockCode { get {return _stockCode; } set { _stockCode = value; SearchNews(); } }

        string _stockCode;

        public UcNaverSearch()
        {
            InitializeComponent();
        }

        private void SearchNews()
        {
            try
            {
                textBoxKeyword.Text = _stockCode;

                string results = getResults();
                results = results.Replace("<b>", "");
                results = results.Replace("</b>", "");
                results = results.Replace("&lt;", "<");
                results = results.Replace("&gt;", ">");

                var parseJson = JObject.Parse(results);
                var countsOfDisplay = Convert.ToInt32(parseJson["display"]);
                var countsOfResults = Convert.ToInt32(parseJson["total"]);

                listViewResults.Items.Clear();
                for (int i = 0; i < countsOfDisplay; i++)
                {
                    ListViewItem item = new ListViewItem((i + 1).ToString());

                    var title = parseJson["items"][i]["title"].ToString();
                    title = title.Replace("&quot;", "\"");

                    var description = parseJson["items"][i]["description"].ToString();
                    description = description.Replace("&quot;", "\"");

                    var link = parseJson["items"][i]["link"].ToString();

                    item.SubItems.Add(title);
                    item.SubItems.Add(description);
                    item.SubItems.Add(link);

                    listViewResults.Items.Add(item);
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }

        }

        private string getResults()
        {
            string keyword = _stockCode;
            string display = trackBarDisplayCounts.Value.ToString();
            string sort = "date";
               
            string query = string.Format("?query={0}&display={1}sort={2}", keyword, display, sort);

            WebRequest request = WebRequest.Create(_apiUrl + query);
            request.Headers.Add("X-Naver-Client-Id", _clientId);
            request.Headers.Add("X-Naver-Client-Secret", _clientSecret);

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

        private void listViewResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewResults.SelectedItems.Count > 0)
            {
                int selectItemIndex = listViewResults.SelectedItems[0].Index;
                System.Diagnostics.Process.Start(listViewResults.Items[selectItemIndex].SubItems[3].Text.ToString());
            }
            
        }
    }
}
