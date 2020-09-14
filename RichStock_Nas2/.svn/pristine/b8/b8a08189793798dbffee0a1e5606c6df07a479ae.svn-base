using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaikRichStock.Common;
using System.Windows.Forms.DataVisualization.Charting;

namespace Common
{
    public partial class UcFinance : UserControl
    {
        private DataAccess _DataAcc = new DataAccess();
        private PaikRichStock.Common.ucMainStockVer2 _ucMainStockVer2;
        private DataSet _stockBaseInfo;
        private string _stockCode;

        public UcFinance()
        {
            InitializeComponent();
        }

        public PaikRichStock.Common.ucMainStockVer2 UcStockMain {
            get { return _ucMainStockVer2; }
            set { 
                _ucMainStockVer2 = value;
            }
        }

        public string StockCode
        {
            get { return _stockCode; }
            set { _stockCode = value; InitWindow(); }
        }
        public DataSet Prop_StockBaseInfo
        {
            get { return _stockBaseInfo; }
            set
            {
                if (_stockCode == "") return;
                _stockBaseInfo = value;
                SetCompanyInfo();
            }
        }

        private void InitWindow()
        {
            txtCompanyName.Text = "";
            txtAddress.Text = "";
            txtCeo.Text = "";
            txtSector.Text = "";
            txtHomepage.Text = "";
            txtSangJangDate.Text = "";
            txtMainProduct.Text = "";
            txtETC.Text = "";
            txtRemark.Text = "";
            txt주주.Text = "";
            dgImWon.RowCount = 1;
            txt결산월.Text = "";
            txt액면가.Text = "";
            txt자본금.Text = "";
            txt상장주식.Text = "";
            txt신용비율.Text = "";
            txt시가총액.Text = "";
            txt외인소진률.Text = "";
            txtPER.Text = "";
            txtEPS.Text = "";
            txtROE.Text = "";
            txtPBR.Text = "";
            txtEV.Text = "";
            txtBPS.Text = "";
            txt매출액.Text = "";
            txt영업이익.Text = "";
            txt당기순이익.Text = "";
            txt250최고.Text = "";
            txt250최고가대비율.Text = "";
            txt250최고가일.Text = "";
            txt연중최고.Text = "";
            txt250최저.Text = "";
            txt250최저가대비율.Text = "";
            txt250최저가일.Text = "";
            txt연중최저.Text = "";
            txt현재가.Text = "";
            txt시가.Text = "";
            txt고가.Text = "";
            txt저가.Text = "";
            
        }

        private void SetCompanyInfo()
        {
            if (_ucMainStockVer2 == null) return;
            if (_stockBaseInfo == null) return;
            DataRow dr = _stockBaseInfo.Tables[0].Rows[0];

            txt결산월.Text = dr["결산월"].ToString().Trim();
            txt액면가.Text = dr["액면가"].ToString().Trim();
            txt자본금.Text = dr["자본금"].ToString().Trim();
            txt상장주식.Text = dr["상장주식"].ToString().Trim();
            txt신용비율.Text = dr["신용비율"].ToString().Trim();
            txt시가총액.Text = dr["시가총액"].ToString().Trim();
            txt외인소진률.Text = dr["외인소진률"].ToString().Trim();
            txtPER.Text = dr["PER"].ToString().Trim();
            txtEPS.Text = dr["EPS"].ToString().Trim();
            txtROE.Text = dr["ROE"].ToString().Trim();
            txtPBR.Text = dr["PBR"].ToString().Trim();
            txtEV.Text = dr["EV"].ToString().Trim();
            txtBPS.Text = dr["BPS"].ToString().Trim();
            txt매출액.Text = dr["매출액"].ToString().Trim();
            txt영업이익.Text = dr["영업이익"].ToString().Trim();
            txt당기순이익.Text = dr["당기순이익"].ToString().Trim();
            txt250최고.Text = dr["250최고"].ToString().Trim();
            txt250최고가대비율.Text = dr["250최고가대비율"].ToString().Trim();
            txt250최고가일.Text = dr["250최고가일"].ToString().Trim();
            txt연중최고.Text = dr["연중최고"].ToString().Trim();
            txt250최저.Text = dr["250최저"].ToString().Trim();
            txt250최저가대비율.Text = dr["250최저가대비율"].ToString().Trim();
            txt250최저가일.Text = dr["250최저가일"].ToString().Trim();
            txt연중최저.Text = dr["연중최저"].ToString().Trim();
            txt현재가.Text = dr["현재가"].ToString().Trim();
            txt시가.Text = dr["시가"].ToString().Trim();
            txt고가.Text = dr["고가"].ToString().Trim();
            txt저가.Text = dr["저가"].ToString().Trim();


            DataSet dsCompany = _DataAcc.p_company_info_query("1", _stockCode , false , null  , null);
            if (dsCompany == null) return;
            if (dsCompany.Tables[0].Rows.Count < 1) return;
            dr = dsCompany.Tables[0].Rows[0];
            txtCompanyName.Text = _ucMainStockVer2._allStockDataset.Tables["STOCKLIST"].Select("STOCK_CODE = '" + _stockCode + "'")[0]["STOCK_NAME"].ToString().Trim();
            txtAddress.Text = dr["ADDRESS"].ToString().Trim();
            txtCeo.Text = dr["CEO"].ToString().Trim();
            txtSector.Text = dr["CLASS_GB"].ToString().Trim();
            txtHomepage.Text = dr["HOMEPAGE"].ToString().Trim();
            txtSangJangDate.Text = dr["SANGJANG_DATE"].ToString().Trim();
            txtMainProduct.Text = dr["MAIN_PRODUCT"].ToString().Trim();
            txtETC.Text = dr["ETC"].ToString().Trim();
            txtRemark.Text = dr["REMARK"].ToString().Trim().Replace("-", "\r\n\r\n-").Substring(3);
            txt주주.Text = dr["JUJU"].ToString().Trim();
            int row = 0;
            foreach (DataRow drCEO in dsCompany.Tables[0].Rows)
            {
                if (row >= dgImWon.RowCount - 1) dgImWon.RowCount += 1;
                dgImWon.Rows[row].Cells["이름"].Value = drCEO["NAME"].ToString().Trim();
                dgImWon.Rows[row].Cells["직급"].Value = drCEO["GRADE"].ToString().Trim();
                dgImWon.Rows[row].Cells["경력"].Value = drCEO["CAREER"].ToString().Trim();
                dgImWon.Rows[row].Cells["의결주식수"].Value = drCEO["POWER_STOCK"].ToString().Trim() == "" ? 0 : Decimal.Parse(drCEO["POWER_STOCK"].ToString().Trim());
                row++;
            }
            dgImWon.Sort(dgImWon.Columns["의결주식수"], ListSortDirection.Descending);
            webBrowser1.Navigate("http://finance.naver.com/item/coinfo.nhn?code=" + _stockCode);
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/coinfo.nhn?code=") > -1) { 
                webBrowser1.Document.Window.ScrollTo(0, webBrowser1.Document.Body.ScrollRectangle.Height / 2);
            }
            else if (
                webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/fchart.nhn?code=") > -1 ||
                webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/sise.nhn?code=") > -1 ||
                webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/frgn.nhn?code=") > -1 ||
                webBrowser1.Url.AbsoluteUri.IndexOf("http://finance.naver.com/item/board.nhn?code=") > -1 
                )
            {
                webBrowser1.Document.Window.ScrollTo(0, webBrowser1.Document.Body.ScrollRectangle.Height / 6);
            }
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://finance.naver.com/item/fchart.nhn?code=" + _stockCode);
        }

        private void btnFinance_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://finance.naver.com/item/coinfo.nhn?code=" + _stockCode);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            _DataAcc.p_company_info_Add("UR", _stockCode, txt주주.Text.Trim() , txtSector.Text.Trim(), txtCeo.Text.Trim(), "", "", txtSangJangDate.Text.Trim(), "", "", txtHomepage.Text.Trim(), txtAddress.Text.Trim(), "", "", "","", txtMainProduct.Text.Trim(),txtETC.Text.Trim(), txtRemark.Text.Trim().Replace("\r\n" , ""), false, null, null);
        }

        private void txt현재가_TextChanged(object sender, EventArgs e)
        {
            TextBox txtObj = (TextBox)sender;
            if (txtObj.Text.Trim() == "") return;

            if (txtObj.Text.IndexOf("+") < 0 && txtObj.Text.IndexOf("-") < 0) { txtObj.ForeColor = System.Drawing.Color.Empty; return; }

            Decimal num = Decimal.Parse(txtObj.Text.Trim());

            if(num < 0) txtObj.ForeColor = System.Drawing.Color.Blue;
            else if(num > 0) txtObj.ForeColor = System.Drawing.Color.Red;
            else txtObj.ForeColor = System.Drawing.Color.Empty;
        }

        private void btnDart_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://dart.fss.or.kr/html/search/SearchCompany_M2.html?textCrpNM=" + _stockCode);
        }

        private void btn시세_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://finance.naver.com/item/sise.nhn?code=" + _stockCode);
        }

        private void btn매동_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://finance.naver.com/item/frgn.nhn?code=" + _stockCode);
        }

        private void btn토론_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://finance.naver.com/item/board.nhn?code=" + _stockCode);
        }
    }
}
