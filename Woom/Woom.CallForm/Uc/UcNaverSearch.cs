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
using Woom.DataAccess.PlugIn;
using Woom.DataDefine.Util;

namespace Woom.CallForm.Uc
{
    public partial class UcNaverSearch : UserControl
    {

        const string _apiUrl = "https://openapi.naver.com/v1/search/news.json";
        const string _clientId = "dIcPMP6xI1dX3QKlm2Qw"; //Application Client ID 입력
        const string _clientSecret = "CyyzooLmi3"; //Application Client Secret 입력

        public string PropStockCode { get  {return _stockCode; } 
                                       set { _stockCode = value; 
                                             SearchNews(); } }
        int intDrgWindowWidth = 0;
        int intDrgColumnWidth = 0;
        string _stockCode;

        public UcNaverSearch()
        {
            InitializeComponent();
            dgvNaverSearch.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            //dgvNaverSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNaverSearch.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            intDrgWindowWidth = dgvNaverSearch.Width;
            intDrgColumnWidth = dgvNaverSearch.Columns[2].Width;
        }

        int _row;

        private void SearchNews()
        {
            try
            {

                if (chkStockName.Checked == true)
                {
                    textBoxKeyword.Text = ClsAxKH.GetMasterCodeName(_stockCode);
                }
                else
                {
                    textBoxKeyword.Text = _stockCode; 
                }

                ClsDataGridViewUtil clsDataGridViewUtil = new ClsDataGridViewUtil();

                if (chkAddSearch.Checked == true)
                {
                    // clsDataGridViewUtil.RemoveGridViewRow(dgvNaverSearch);
                }
                else
                {
                    clsDataGridViewUtil.RemoveGridViewRow(dgvNaverSearch);
                    _row = 0;
                }
                

                string results = getResults();
                results = results.Replace("<b>", "");
                results = results.Replace("</b>", "");
                results = results.Replace("&lt;", "<");
                results = results.Replace("&gt;", ">");

                var parseJson = JObject.Parse(results);
                var countsOfDisplay = Convert.ToInt32(parseJson["display"]);
                var countsOfResults = Convert.ToInt32(parseJson["total"]);
                

                for (int i = 0; i < countsOfDisplay; i++)
                {
                    var title = parseJson["items"][i]["title"].ToString();
                    title = title.Replace("&quot;", "\"");

                    var description = parseJson["items"][i]["description"].ToString();
                    description = description.Replace("&quot;", "\"");

                    var link = parseJson["items"][i]["link"].ToString();

                    if (_row > 0 && dgvNaverSearch.Rows[0].Cells["No"].Value.ToString() != "" )
                    {
                        DataGridViewRow dataGridViewRow = (DataGridViewRow)dgvNaverSearch.Rows[0].Clone();
                        dataGridViewRow.Cells[0].Value = (_row = +1).ToString();
                        dataGridViewRow.Cells[1].Value = textBoxKeyword.Text;
                        dataGridViewRow.Cells[2].Value = title;
                        dataGridViewRow.Cells[3].Value = description;
                        dataGridViewRow.Cells[4].Value = link;

                        dgvNaverSearch.Rows.Insert(0, dataGridViewRow);
                    }
                    else
                    {
                        dgvNaverSearch.Rows.Add();
                        dgvNaverSearch.Rows[_row].Cells["No"].Value = (_row + 1).ToString();
                        dgvNaverSearch.Rows[_row].Cells["검색명"].Value = textBoxKeyword.Text;
                        dgvNaverSearch.Rows[_row].Cells["제목"].Value = title;
                        dgvNaverSearch.Rows[_row].Cells["본문"].Value = description;
                        dgvNaverSearch.Rows[_row].Cells["링크"].Value = link;
                    }                    

                    _row = _row + 1;
                }

                
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }

        }

        private string getResults()
        {
            string keyword;
            if (txtAddWord.Text.ToString().Trim() == "")
            {
                keyword = textBoxKeyword.Text.ToString().Trim();
            }
            else
            {
                keyword = textBoxKeyword.Text.ToString().Trim() + " " + txtAddWord.Text.ToString().Trim();
            }
            
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

        private void dgvNaverSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNaverSearch.Rows[e.RowIndex].Cells["No"].Value.ToString().Trim() != "")
            {
                System.Diagnostics.Process.Start(dgvNaverSearch.Rows[e.RowIndex].Cells["링크"].Value.ToString().Trim());
            }
        }
              
        
        private void dgvNaverSearch_Resize(object sender, EventArgs e)
        {
            if (dgvNaverSearch.Width > intDrgWindowWidth)
            {
                dgvNaverSearch.Columns[3].Width = dgvNaverSearch.Columns[3].Width + (dgvNaverSearch.Width - intDrgWindowWidth);
            }
            else
            {
                dgvNaverSearch.Columns[3].Width = intDrgColumnWidth;
            }
        }
    }
}
