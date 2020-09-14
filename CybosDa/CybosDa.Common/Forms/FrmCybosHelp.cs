using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CybosDa.Common.Forms
{
    public partial class FrmCybosHelp : Form
    {
        public FrmCybosHelp()
        {
            InitializeComponent();
            InitDgvHelpList();
        }
        private void InitDgvHelpList()
        {
            dgvHelpList.ColumnCount = 1;
            dgvHelpList.Columns[0].Name = "도움말";
            for (int i = 0; i < 24; i++)
            {
                dgvHelpList.Rows.Add();
            }
            
            dgvHelpList.Rows[0].Cells["도움말"].Value = "주식현재가 (단일종목) 조회";
            dgvHelpList.Rows[1].Cells["도움말"].Value =    "관심종목 실시간";
            dgvHelpList.Rows[2].Cells["도움말"].Value =    "주식 종목정보 조회";
            dgvHelpList.Rows[3].Cells["도움말"].Value =    "주식현재가 (복수종목) 조회";
            dgvHelpList.Rows[4].Cells["도움말"].Value =    "주식현재가 (복수종목) 실시간";
            dgvHelpList.Rows[5].Cells["도움말"].Value =    "투자주체별 현황";
            dgvHelpList.Rows[6].Cells["도움말"].Value =    "주식 시간대별 체결";
            dgvHelpList.Rows[7].Cells["도움말"].Value =    "주식 일자별 주가";
            dgvHelpList.Rows[8].Cells["도움말"].Value =    "주식 호가 (조회)";
            dgvHelpList.Rows[9].Cells["도움말"].Value =    "주식 호가 (실시간)";
            dgvHelpList.Rows[10].Cells["도움말"].Value =   "주식 현금주문(매수/매도)";
            dgvHelpList.Rows[11].Cells["도움말"].Value =   "주식 현금주문(유형정정)";
            dgvHelpList.Rows[12].Cells["도움말"].Value =   "주식 현금주문(취소)";
            dgvHelpList.Rows[13].Cells["도움말"].Value = "주식 계좌별 매수 가능금액/수량";
            dgvHelpList.Rows[14].Cells["도움말"].Value =   "주식 계좌별 매도 가능수량";
            dgvHelpList.Rows[15].Cells["도움말"].Value =   "주식 예약주문";
            dgvHelpList.Rows[16].Cells["도움말"].Value =   "주식 예약취소주문";
            dgvHelpList.Rows[17].Cells["도움말"].Value =   "주식 예약주문현황";
            dgvHelpList.Rows[18].Cells["도움말"].Value =   "금일 계좌별 주문/체결내역";
            dgvHelpList.Rows[19].Cells["도움말"].Value =   "금일/전일 체결기준 내역";
            dgvHelpList.Rows[20].Cells["도움말"].Value =   "계좌별 미체결잔량";
            dgvHelpList.Rows[21].Cells["도움말"].Value =   "주식 체결 실시간";
            dgvHelpList.Rows[22].Cells["도움말"].Value =   "계좌별 잔고 평가현황";
            dgvHelpList.Rows[23].Cells["도움말"].Value =   "주식 결제예정예수금 가계산";

        }

        private void ShowHelp(string title)
        {
            switch (title)
            {
                case "주식현재가 (단일종목) 조회":
                    webBrowser0.Navigate("http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=288&seq=3&page=6&searchString=&p=&v=&m=");
                    break;
                case "관심종목 실시간":
                    webBrowser0.Navigate("http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=288&seq=16&page=6&searchString=&p=&v=&m=");
                    break;
                case "주식 종목정보 조회":
                    webBrowser0.Navigate("http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=287&seq=5&page=1&searchString=&p=&v=&m=");
                    break;
                case "주식현재가 (복수종목) 조회":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=285&seq=131&page=1&searchString=marketeye&p=&v=&m=");
                    break;
                case "주식현재가 (복수종목) 실시간":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=288&seq=16&page=6&searchString=&p=&v=&m=");
                    break;
                case "투자주체별 현황":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=294&seq=242&page=1&searchString=&p=&v=&m=");
                    break;
                case "주식 시간대별 체결":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=288&seq=22&page=5&searchString=&p=&v=&m=");
                    break;
                case "주식 일자별 주가":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=288&seq=23&page=5&searchString=&p=&v=&m=");
                    break;
                case "주식 호가 (조회)":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=288&seq=20&page=5&searchString=&p=&v=&m=");
                    break;
                case "주식 호가 (실시간)":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=288&seq=21&page=5&searchString=&p=&v=&m=");
                    break;
                case "주식 현금주문(매수/매도)":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=159&page=3&searchString=&p=&v=&m=");
                    break;
                case "주식 현금주문(유형정정)":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=166&page=2&searchString=&p=&v=&m=");
                    break;
                case "주식 현금주문(취소)":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=162&page=2&searchString=&p=&v=&m=");
                    break;
                case "주식 계좌별 매수 가능금액/수량":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=171&page=2&searchString=&p=&v=&m=");
                    break;
                case "주식 계좌별 매도 가능수량":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=172&page=2&searchString=&p=&v=&m=");
                    break;
                case "주식 예약주문":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=187&page=1&searchString=&p=&v=&m=");
                    break;
                case "주식 예약취소주문":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=188&page=1&searchString=&p=&v=&m=");
                    break;
                case "주식 예약주문현황":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=189&page=1&searchString=&p=&v=&m=");
                    break;
                case "금일 계좌별 주문/체결내역":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=174&page=2&searchString=&p=&v=&m=");
                    break;
                case "금일/전일 체결기준 내역":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=175&page=2&searchString=&p=&v=&m=");
                    break;
                case "계좌별 미체결잔량":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=173&page=2&searchString=&p=&v=&m=");
                    break;
                case "주식 체결 실시간":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=155&page=3&searchString=&p=&v=&m=");
                    break;
                case "계좌별 잔고 평가현황":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=176&page=2&searchString=&p=&v=&m=");
                    break;
                case "주식 결제예정예수금 가계산":
                    webBrowser0.Navigate(":http://money2.daishin.com/e5/mboard/ptype_basic/HTS_Plus_Helper/DW_Basic_Read.aspx?boardseq=291&seq=169&page=2&searchString=&p=&v=&m=");
                    break;

            }


        }

        private void dgvHelpList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowHelp(dgvHelpList.Rows[e.RowIndex].Cells["도움말"].Value.ToString().Trim());
        }
    }
}
