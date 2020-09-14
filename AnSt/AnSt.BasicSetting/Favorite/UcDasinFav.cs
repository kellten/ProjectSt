using AnSt.Define.Attribute;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AnSt.BasicSetting.Favorite
{
    public partial class UcDasinFav : UserControl
    {
        private AnSt.Util.Func.ClsUtilFunc _clsUtilFunc = new AnSt.Util.Func.ClsUtilFunc();

        public UcDasinFav()
        {
            InitializeComponent();
            txtPath.Text = @"C:\DAISHIN\STOCK-I\usr\" + "cmportdata.ini";
            GetFavNumListToDataGrid();
        }

        private void GetDasinFav()
        {

        }

        #region Event & Delegate
        public delegate void ChooseStockCode(string stockCode);
        public event ChooseStockCode onChooseStockCode;
        public delegate void ChooseGroupCodeEventHandler(DataTable dt, string groupCode, string groupName);
        public event ChooseGroupCodeEventHandler onChoosGroupCode;
        #endregion

        #region Enum
        public enum dgvIndex
        {
            StockName = 0, StockCode, DStockCode
        }
        #endregion

        public void DoChooseStockCode(string stockCode)
        {
            onChooseStockCode(stockCode);
        }

        public void DoChoosGroupCode(DataTable dt, string groupcode, string groupName)
        {
            onChoosGroupCode(dt, groupcode, groupName);
        }


        private void GetFavNumList(int i)
        {
            string groupName = "GROUP" + i.ToString() + "_NAME";
            string groupNum = "GROUP" + i.ToString();
            string path = @txtPath.Text;
            string[] textValue = System.IO.File.ReadAllLines(path, Encoding.Default);

            dgvStockList.Rows.Clear();

            if (textValue.Length > 0)
            {
                for (int j = 0; j < textValue.Length; j++)
                {
                    if (textValue[j].Contains(groupName) == true)
                    {
                        lblSGroupName.Text = textValue[j].Replace(groupName + "=", "").Trim();
                        if (j + 1 < textValue.Length)
                        {
                            string[] txtStockCode = textValue[j + 1].Replace(groupNum, "").Replace("=", "").Replace("A", "").Split(',');

                            foreach (string stockCode in txtStockCode)
                            {
                                Display(stockCode);
                            }
                        }

                    }
                }
            }
        }

        private void GetDvStockCodeInGroupCode(string groupCode, string groupKName)
        {
            DataTable dt = new DataTable("StockCodes");

            dt.Columns.Add("STOCK_CODE", typeof(string));

            string groupName = "GROUP" + groupCode + "_NAME";
            string groupNum = "GROUP" + groupCode;
            string path = @txtPath.Text;
            string[] textValue = System.IO.File.ReadAllLines(path, Encoding.Default);
            int row = 0;

            dgvStockList.Rows.Clear();

            if (textValue.Length > 0)
            {
                for (int j = 0; j < textValue.Length; j++)
                {
                    if (textValue[j].Contains(groupName) == true)
                    {
                        if (j + 1 < textValue.Length)
                        {
                            string[] txtStockCode = textValue[j + 1].Replace(groupNum, "").Replace("=", "").Replace("A", "").Split(',');

                            foreach (string stockCode in txtStockCode)
                            {
                                dt.Rows.Add();
                                dt.Rows[row]["STOCK_CODE"] = stockCode;
                                row = row + 1;
                            }
                        }

                    }
                }
            }

            if (dt != null)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    Display(dr["STOCK_CODE"].ToString().Trim());
                }

                onChoosGroupCode(dt.Copy(), groupCode, groupKName);
            }
        }

        private void GetFavNumListToDataGrid()
        {
            string path = @txtPath.Text;
            string[] textValue = System.IO.File.ReadAllLines(path, Encoding.Default);
            string splitGroupName = "_NAME=";
            string splitGroupCode = "GROUP";
            int row = 0;

            for (int i = 0; i < textValue.Length; i++)
            {
                if (textValue[i].Contains("GROUP") == true && textValue[i].Contains("_NAME") == true)
                {
                    int splitName = textValue[i].IndexOf(splitGroupName) + 1 + splitGroupName.Length;

                    DvGroupList.Rows.Add();
                    DvGroupList.Rows[row].Cells["GroupName"].Value = AnSt.Util.Func.ClsPStaticUtilFunc.Mid(textValue[i], splitName, textValue[i].Length - splitName + 1).ToString();
                    DvGroupList.Rows[row].Cells["Groupcode"].Value = AnSt.Util.Func.ClsPStaticUtilFunc.Mid(textValue[i], splitGroupCode.Length + 1, 3).ToString();
                    row = row + 1;

                }
            }

        }

        private string EncodingText(string text)
        {
            StreamReader sr = new StreamReader(text, Encoding.Default);
            return sr.ReadToEnd();
        }

        private void Display(string stockCode)
        {
            SDataAccess.RichQuery richQuery = new SDataAccess.RichQuery();
            DataTable dt = new DataTable();

            dt = richQuery.p_ScodeQuery(query: "3", stockCode: stockCode, ybYongCode: "", bln3tier: false).Tables[0].Copy();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string[] row0 = { dt.Rows[0]["STOCK_NAME"].ToString().Trim(), stockCode, "A" + stockCode };

                    dgvStockList.Rows.Add(row0);
                }
            }
        }

        private void btnFav0_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav1_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav2_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav3_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav4_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav5_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav6_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav7_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav8_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav9_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));

        }

        private void btnFav10_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnFav11_Click(object sender, EventArgs e)
        {
            GetFavNumList(Int32.Parse(((Button)sender).Text.Replace("btnFav", "")));
        }

        private void btnPrv_Click(object sender, EventArgs e)
        {
            if (btnFav0.Tag.ToString().Trim() == "700")
            {
                return;
            }
            else

            {
                for (int i = 0; i < 12; i++)
                {
                    int j = 0;
                    j = Int32.Parse(splitContainer1.Panel1.Controls["btnFav" + i.ToString()].Text);
                    j = j - 1;
                    splitContainer1.Panel1.Controls["btnFav" + i.ToString()].Text = j.ToString();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 12; i++)
            {
                int j = 0;
                j = Int32.Parse(splitContainer1.Panel1.Controls["btnFav" + i.ToString()].Text);
                j = j + 1;
                splitContainer1.Panel1.Controls["btnFav" + i.ToString()].Text = j.ToString();
            }

        }

        ClsStockAttribute stockAttribute = new ClsStockAttribute();
        private void dgvStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DoChooseStockCode(dgvStockList.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());
        }

        private void DvGroupList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DvGroupList.Rows[e.RowIndex].Cells["GroupCode"].Value.ToString().Trim() == "")
            {
                return;
            }
            GetDvStockCodeInGroupCode(DvGroupList.Rows[e.RowIndex].Cells["GroupCode"].Value.ToString(), DvGroupList.Rows[e.RowIndex].Cells["GroupName"].Value.ToString());
        }


    }
}
