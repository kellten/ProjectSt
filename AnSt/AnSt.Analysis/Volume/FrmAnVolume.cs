using AnSt.Util.Func;
using SDataAccess;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AnSt.Analysis.Volume
{
    public partial class FrmAnVolume : Form
    {

        private AnSt.Util.Func.ClsUtilFunc _clsUtilFunc = new ClsUtilFunc();
        private DataTable _dt = new DataTable();
        public FrmAnVolume()
        {
            InitializeComponent();
            ucDasinFav1.onChoosGroupCode += new AnSt.BasicSetting.Favorite.UcDasinFav.ChooseGroupCodeEventHandler(ucDasinFav_onChoosGroupCode);
        }
        private void FrmAnVolume_Load(object sender, EventArgs e)
        {
            DvVolumeGiganList.AllowUserToAddRows = false;
            DvVolumeList.AllowUserToAddRows = false;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (_dt != null)
            {
                DisplayVolumeListGigan(_dt);
                DisplayVolumeList(_dt);
            }
        }

        private void ucDasinFav_onChoosGroupCode(DataTable dt, string groupCode, string groupName)
        {
            _dt = dt.Copy();
            txtGroupName.Text = groupName;
            txtGroupCode.Text = groupCode;
        }

        private void DisplayVolumeList(DataTable RecDt)
        {
            SDataAccess.KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();
            int row = 0;

            DvVolumeList.Rows.Clear();

            foreach (DataRow dr in RecDt.Rows)
            {
                dt = new DataTable();

                dt = oKiwoomQuery.p_OPT10060PriceQuery(query: "2", stockCode: dr["STOCK_CODE"].ToString().Trim(), fromdate: _clsUtilFunc.DateToString(DtpDate.Value.ToString()),
                    toDate: _clsUtilFunc.DateToString(DtpDate.Value.ToString()), maemeGb: "", bln3tier: false).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr2th in dt.Rows)
                    {

                        DvVolumeList.Rows.Add();

                        DvVolumeList.Rows[row].Cells["StockName"].Value = dr2th["STOCK_NAME"].ToString();
                        DvVolumeList.Rows[row].Cells["GainPr"].Value = dr2th["GAIN_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["ForePr"].Value = dr2th["FORE_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["GiganPr"].Value = dr2th["GIGAN_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["GumyPr"].Value = dr2th["GUMY_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["BohumPr"].Value = dr2th["BOHUM_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["TosinPr"].Value = dr2th["TOSIN_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["GitaPr"].Value = dr2th["GITA_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["BankPr"].Value = dr2th["BANK_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["YeongiPr"].Value = dr2th["YEONGI_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["SamoPr"].Value = dr2th["SAMO_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["NationPr"].Value = dr2th["NATION_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["BubinPr"].Value = dr2th["BUBIN_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["IoforePr"].Value = dr2th["IOFORE_PR"].ToString();
                        DvVolumeList.Rows[row].Cells["SimpleStockCode"].Value = dr["STOCK_CODE"].ToString().Trim();

                        row = row + 1;
                    }
                }

                dt.Clear();

                CellColorChange(DvVolumeList, 1);
            }
            DvVolumeList.AutoResizeColumns();
        }

        private void DisplayVolumeListDetail(string stockCode)
        {
            SDataAccess.KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();
            int row = 0;
            int gainPr, forePr, giganPr, gumyPr, bohumPr, tosinPr, gitaPr, bankPr, yeongiPr, samoPr, nationPr, bubinPr, ioForePR = 0;

            DgvDetail.Rows.Clear();
            DgvNuDetail.Rows.Clear();

            dt = oKiwoomQuery.p_OPT10060PriceQuery(query: "5", stockCode: stockCode, fromdate: _clsUtilFunc.DateToString(DtpDate.Value.ToString()),
                    toDate: _clsUtilFunc.DateToString(DtpDate.Value.ToString()), maemeGb: "", bln3tier: false).Tables[0];

            if (dt.Rows.Count > 0)
            {

                gainPr = Convert.ToInt32(dt.Compute("SUM(GAIN_PR)", string.Empty));
                forePr = Convert.ToInt32(dt.Compute("SUM(FORE_PR)", string.Empty));
                giganPr = Convert.ToInt32(dt.Compute("SUM(GIGAN_PR)", string.Empty));
                gumyPr = Convert.ToInt32(dt.Compute("SUM(GUMY_PR)", string.Empty));
                bohumPr = Convert.ToInt32(dt.Compute("SUM(BOHUM_PR)", string.Empty));
                tosinPr = Convert.ToInt32(dt.Compute("SUM(TOSIN_PR)", string.Empty));
                gitaPr = Convert.ToInt32(dt.Compute("SUM(GITA_PR)", string.Empty));
                bankPr = Convert.ToInt32(dt.Compute("SUM(BANK_PR)", string.Empty));
                yeongiPr = Convert.ToInt32(dt.Compute("SUM(YEONGI_PR)", string.Empty));
                samoPr = Convert.ToInt32(dt.Compute("SUM(SAMO_PR)", string.Empty));
                nationPr = Convert.ToInt32(dt.Compute("SUM(NATION_PR)", string.Empty));
                bubinPr = Convert.ToInt32(dt.Compute("SUM(BUBIN_PR)", string.Empty));
                ioForePR = Convert.ToInt32(dt.Compute("SUM(IOFORE_PR)", string.Empty));

                foreach (DataRow dr2th in dt.Rows)
                {

                    DgvDetail.Rows.Add();
                    DgvNuDetail.Rows.Add();

                    DgvDetail.Rows[row].Cells["dateDe"].Value = dr2th["STOCK_DATE"].ToString();
                    DgvDetail.Rows[row].Cells["DeNowPr"].Value = dr2th["NOW_PRICE"].ToString();
                    DgvDetail.Rows[row].Cells["DeHighPr"].Value = dr2th["HIGH_PRICE"].ToString();
                    DgvDetail.Rows[row].Cells["DeLowPr"].Value = dr2th["LOW_PRICE"].ToString();
                    DgvDetail.Rows[row].Cells["DeStartPr"].Value = dr2th["START_PRICE"].ToString();
                    DgvDetail.Rows[row].Cells["GainPrDe"].Value = dr2th["GAIN_PR"].ToString();
                    DgvDetail.Rows[row].Cells["ForePrDe"].Value = dr2th["FORE_PR"].ToString();
                    DgvDetail.Rows[row].Cells["GiganPrDe"].Value = dr2th["GIGAN_PR"].ToString();
                    DgvDetail.Rows[row].Cells["GumyPrDe"].Value = dr2th["GUMY_PR"].ToString();
                    DgvDetail.Rows[row].Cells["BohumPrDe"].Value = dr2th["BOHUM_PR"].ToString();
                    DgvDetail.Rows[row].Cells["TosinPrDe"].Value = dr2th["TOSIN_PR"].ToString();
                    DgvDetail.Rows[row].Cells["GitaPrDe"].Value = dr2th["GITA_PR"].ToString();
                    DgvDetail.Rows[row].Cells["BankPrDe"].Value = dr2th["BANK_PR"].ToString();
                    DgvDetail.Rows[row].Cells["YeongiPrDe"].Value = dr2th["YEONGI_PR"].ToString();
                    DgvDetail.Rows[row].Cells["SamoPrDe"].Value = dr2th["SAMO_PR"].ToString();
                    DgvDetail.Rows[row].Cells["NationPrDe"].Value = dr2th["NATION_PR"].ToString();
                    DgvDetail.Rows[row].Cells["BubinPrDe"].Value = dr2th["BUBIN_PR"].ToString();
                    DgvDetail.Rows[row].Cells["IoforePrDe"].Value = dr2th["IOFORE_PR"].ToString();

                    if (row == 0)
                    {
                    }
                    else
                    {
                        gainPr = gainPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["GainPrDe"].Value.ToString());
                        forePr = forePr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["ForePrDe"].Value.ToString());
                        giganPr = giganPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["GiganPrDe"].Value.ToString());
                        gumyPr = gumyPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["GumyPrDe"].Value.ToString());
                        bohumPr = bohumPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["BohumPrDe"].Value.ToString());
                        tosinPr = tosinPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["TosinPrDe"].Value.ToString());
                        gitaPr = gitaPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["GitaPrDe"].Value.ToString());
                        bankPr = bankPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["BankPrDe"].Value.ToString());
                        yeongiPr = yeongiPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["YeongiPrDe"].Value.ToString());
                        samoPr = samoPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["SamoPrDe"].Value.ToString());
                        nationPr = nationPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["NationPrDe"].Value.ToString());
                        bubinPr = bubinPr - Int32.Parse(DgvDetail.Rows[row - 1].Cells["BubinPrDe"].Value.ToString());
                        ioForePR = ioForePR - Int32.Parse(DgvDetail.Rows[row - 1].Cells["IoforePrDe"].Value.ToString());
                    }

                    DgvNuDetail.Rows[row].Cells["dateNuDe"].Value = dr2th["STOCK_DATE"].ToString();
                    DgvNuDetail.Rows[row].Cells["NuDeNowPr"].Value = dr2th["NOW_PRICE"].ToString();
                    DgvNuDetail.Rows[row].Cells["NuDeHighPr"].Value = dr2th["HIGH_PRICE"].ToString();
                    DgvNuDetail.Rows[row].Cells["NuDeLowPr"].Value = dr2th["LOW_PRICE"].ToString();
                    DgvNuDetail.Rows[row].Cells["NuDeStartPr"].Value = dr2th["START_PRICE"].ToString();
                    DgvNuDetail.Rows[row].Cells["GainPrNuDe"].Value = gainPr.ToString();
                    DgvNuDetail.Rows[row].Cells["ForePrNuDe"].Value = forePr.ToString();
                    DgvNuDetail.Rows[row].Cells["GiganPrNuDe"].Value = giganPr.ToString();
                    DgvNuDetail.Rows[row].Cells["GumyPrNuDe"].Value = gumyPr.ToString();
                    DgvNuDetail.Rows[row].Cells["BohumPrNuDe"].Value = bohumPr.ToString();
                    DgvNuDetail.Rows[row].Cells["TosinPrNuDe"].Value = tosinPr.ToString();
                    DgvNuDetail.Rows[row].Cells["GitaPrNuDe"].Value = gitaPr.ToString();
                    DgvNuDetail.Rows[row].Cells["BankPrNuDe"].Value = bankPr.ToString();
                    DgvNuDetail.Rows[row].Cells["YeongiPrNuDe"].Value = yeongiPr.ToString();
                    DgvNuDetail.Rows[row].Cells["SamoPrNuDe"].Value = samoPr.ToString();
                    DgvNuDetail.Rows[row].Cells["NationPrNuDe"].Value = nationPr.ToString();
                    DgvNuDetail.Rows[row].Cells["BubinPrNuDe"].Value = bubinPr.ToString();
                    DgvNuDetail.Rows[row].Cells["IoforePrNuDe"].Value = ioForePR.ToString();
                    DgvNuDetail.Rows[row].Cells["VolumeNuDe"].Value = dr2th["TRADE_DAEGUM"].ToString();

                    row = row + 1;

                }
            }

            dt.Clear();

            CellColorChange(DgvDetail, 5);
            CellColorChange(DgvNuDetail, 5);

            DgvDetail.AutoResizeColumns();
            DgvNuDetail.AutoResizeColumns();

            // DataTable dtaa = DgvDetail.DataSource as DataTable;
            DataTable dtNu = new DataTable();

            dtNu = AnSt.Util.Func.ClsPStaticUtilFunc.GetDataGridViewAsDataTableColumName(DgvNuDetail);
            ChartInit();
            DisplayChart(dtNu);
        }

        private DataTable DataTableFromDataGridView(ref DataGridView dgv)
        {
            DataTable dt = new DataTable();

            return dt;
        }

        private void VisibleSuVolumeListGigan(string value, bool bValue)
        {
            for (int i = 0; i < DvVolumeGiganList.Rows.Count - 1; i++)
            {
                if (DvVolumeGiganList.Rows[i].Cells["Gsu"].Value == null)
                {
                    return;
                }
                if (DvVolumeGiganList.Rows[i].Cells["Gsu"].Value.ToString() == value)
                {
                    DvVolumeGiganList.Rows[i].Visible = bValue;
                }
            }

        }

        private void DisplayVolumeListGigan(DataTable RecDt)
        {
            SDataAccess.KiwoomQuery oKiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();
            int row = 0;
            string fromDate = _clsUtilFunc.DateAddYear(_clsUtilFunc.DateToString(DtpDate.Value.ToString()), -3);
            string toDate = _clsUtilFunc.DateToString(DtpDate.Value.ToString());

            DvVolumeGiganList.Rows.Clear();

            dt = new DataTable();

            foreach (DataRow dr in RecDt.Rows)
            {
                dt = new DataTable();

                dt = oKiwoomQuery.p_OPT10060PriceQuery(query: "3", stockCode: dr["STOCK_CODE"].ToString().Trim(), fromdate: fromDate,
                    toDate: toDate, maemeGb: "", bln3tier: false).Tables[0];

                if (dt.Rows.Count > 0)
                {

                    DvVolumeGiganList.Rows.Add(14);
                    SetSuTitle(row);
                    for (int i = row; i < row + 13; i++)
                    {
                        DvVolumeGiganList.Rows[i].Cells["GStockName"].Value = dt.Rows[0]["STOCK_NAME"].ToString();
                    }
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.DateToString(DtpDate.Text).ToString(), _clsUtilFunc.DateToString(DtpDate.Text).ToString(), "Today");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.WeekMonday(toDate).ToString(), _clsUtilFunc.WeekFriday(toDate).ToString(), "Week1");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.WeekMonday(_clsUtilFunc.DateAddDay(toDate, -7)).ToString(), _clsUtilFunc.WeekFriday(toDate).ToString(), "Week2");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.WeekMonday(_clsUtilFunc.DateAddDay(toDate, -14)).ToString(), _clsUtilFunc.WeekFriday(toDate).ToString(), "Week3");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(toDate).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month1");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -1)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month2");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -2)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month3");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -3)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month4");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -4)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month5");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -5)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month6");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -6)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month7");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -7)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month8");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -8)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month9");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -9)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month10");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -10)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month11");
                    //ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, _clsUtilFunc.MonthFirstDay(_clsUtilFunc.DateAddMonth(toDate, -11)).ToString(), _clsUtilFunc.MonthEndDay(toDate).ToString(), "Month12");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, ClsPStaticUtilFunc.Mid(toDate, 1, 4).ToString() + "0101", ClsPStaticUtilFunc.Mid(toDate, 1, 4).ToString() + "1201", "Year1");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, ClsPStaticUtilFunc.Mid(_clsUtilFunc.DateAddYear(toDate, -1).ToString(), 1, 4).ToString() + "0101", ClsPStaticUtilFunc.Mid(_clsUtilFunc.DateAddYear(toDate, -1).ToString(), 1, 4).ToString() + "1201", "Year2");
                    ViewGiganBySu(row, dr["STOCK_CODE"].ToString(), dt, ClsPStaticUtilFunc.Mid(_clsUtilFunc.DateAddYear(toDate, -2).ToString(), 1, 4).ToString() + "0101", ClsPStaticUtilFunc.Mid(_clsUtilFunc.DateAddYear(toDate, -2).ToString(), 1, 4).ToString() + "1201", "Year3");

                    row = row + 13;
                }

                dt.Clear();
            }

            CellColorChange(DvVolumeGiganList, 2);

            DvVolumeGiganList.AutoResizeColumns();
        }

        private void SetSuTitle(int row)
        {

            DvVolumeGiganList.Rows[row].Cells["GSu"].Value = "개";
            DvVolumeGiganList.Rows[row + 1].Cells["GSu"].Value = "외";
            DvVolumeGiganList.Rows[row + 2].Cells["GSu"].Value = "기";
            DvVolumeGiganList.Rows[row + 3].Cells["GSu"].Value = "금";
            DvVolumeGiganList.Rows[row + 4].Cells["GSu"].Value = "보";
            DvVolumeGiganList.Rows[row + 5].Cells["GSu"].Value = "투";
            DvVolumeGiganList.Rows[row + 6].Cells["GSu"].Value = "기";
            DvVolumeGiganList.Rows[row + 7].Cells["GSu"].Value = "은";
            DvVolumeGiganList.Rows[row + 8].Cells["GSu"].Value = "연";
            DvVolumeGiganList.Rows[row + 9].Cells["GSu"].Value = "사";
            DvVolumeGiganList.Rows[row + 10].Cells["GSu"].Value = "국";
            DvVolumeGiganList.Rows[row + 11].Cells["GSu"].Value = "법";
            DvVolumeGiganList.Rows[row + 12].Cells["GSu"].Value = "내";

        }

        private void ViewGiganBySu(int row, string stockCode, DataTable dt, string fromDate, string toDate, string columnName)
        {
            // 1주
            int gainPr = 0;
            int forePr = 0;
            int giganPr = 0;
            int gumyPr = 0;
            int bohumPr = 0;
            int tosinPr = 0;
            int gitaPr = 0;
            int bankPr = 0;
            int yeongiPr = 0;
            int samoPr = 0;
            int nationPr = 0;
            int bubinPr = 0;
            int ioForePr = 0;

            var results = dt.AsEnumerable().Where(a => a["STOCK_CODE"].ToString() == stockCode && (
                                                       Int32.Parse(a["STOCK_DATE"].ToString()) >= Int32.Parse(fromDate) &&
                                                       Int32.Parse(a["STOCK_DATE"].ToString()) <= Int32.Parse(toDate)));

            foreach (DataRow dr in results)
            {
                gainPr = gainPr + Int32.Parse(dr["GAIN_PRICE"].ToString());
                forePr = forePr + Int32.Parse(dr["FORE_PRICE"].ToString());
                giganPr = giganPr + Int32.Parse(dr["GIGAN_PRICE"].ToString());
                gumyPr = gumyPr + Int32.Parse(dr["GUMY_PRICE"].ToString());
                bohumPr = bohumPr + Int32.Parse(dr["BOHUM_PRICE"].ToString());
                tosinPr = tosinPr + Int32.Parse(dr["TOSIN_PRICE"].ToString());
                gitaPr = gitaPr + Int32.Parse(dr["GITA_PRICE"].ToString());
                bankPr = bankPr + Int32.Parse(dr["BANK_PRICE"].ToString());
                yeongiPr = yeongiPr + Int32.Parse(dr["YEONGI_PRICE"].ToString());
                samoPr = samoPr + Int32.Parse(dr["SAMO_PRICE"].ToString());
                nationPr = nationPr + Int32.Parse(dr["NATION_PRICE"].ToString());
                bubinPr = bubinPr + Int32.Parse(dr["BUBIN_PRICE"].ToString());
                ioForePr = ioForePr + Int32.Parse(dr["IOFORE_PRICE"].ToString());
            }

            DvVolumeGiganList.Rows[row].Cells[columnName].Value = gainPr;
            DvVolumeGiganList.Rows[row + 1].Cells[columnName].Value = forePr;
            DvVolumeGiganList.Rows[row + 2].Cells[columnName].Value = giganPr;
            DvVolumeGiganList.Rows[row + 3].Cells[columnName].Value = gumyPr;
            DvVolumeGiganList.Rows[row + 4].Cells[columnName].Value = bohumPr;
            DvVolumeGiganList.Rows[row + 5].Cells[columnName].Value = tosinPr;
            DvVolumeGiganList.Rows[row + 6].Cells[columnName].Value = gitaPr;
            DvVolumeGiganList.Rows[row + 7].Cells[columnName].Value = bankPr;
            DvVolumeGiganList.Rows[row + 8].Cells[columnName].Value = yeongiPr;
            DvVolumeGiganList.Rows[row + 9].Cells[columnName].Value = samoPr;
            DvVolumeGiganList.Rows[row + 10].Cells[columnName].Value = nationPr;
            DvVolumeGiganList.Rows[row + 11].Cells[columnName].Value = bubinPr;
            DvVolumeGiganList.Rows[row + 12].Cells[columnName].Value = ioForePr;

        }

        private void ViewGiganBySu(int row, string stockCode, DataTable dt, string searchDate)
        {
            // 1주
            int gainPr = 0;
            int forePr = 0;
            int giganPr = 0;
            int gumyPr = 0;
            int bohumPr = 0;
            int tosinPr = 0;
            int gitaPr = 0;
            int bankPr = 0;
            int yeongiPr = 0;
            int samoPr = 0;
            int nationPr = 0;
            int bubinPr = 0;
            int ioForePr = 0;

            var results = dt.AsEnumerable().Where(a => a["STOCK_CODE"].ToString() == stockCode && (
                                                       Int32.Parse(a["STOCK_DATE"].ToString()) >= Int32.Parse(_clsUtilFunc.WeekMonday(searchDate).ToString()) &&
                                                       Int32.Parse(a["STOCK_DATE"].ToString()) <= Int32.Parse(_clsUtilFunc.WeekFriday(searchDate))));

            foreach (DataRow dr in results)
            {
                gainPr = gainPr + Int32.Parse(dr["GAIN_PRICE"].ToString());
                forePr = forePr + Int32.Parse(dr["FORE_PRICE"].ToString());
                giganPr = giganPr + Int32.Parse(dr["GIGAN_PRICE"].ToString());
                gumyPr = gumyPr + Int32.Parse(dr["GUMY_PRICE"].ToString());
                bohumPr = bohumPr + Int32.Parse(dr["BOHUM_PRICE"].ToString());
                tosinPr = tosinPr + Int32.Parse(dr["TOSIN_PRICE"].ToString());
                gitaPr = gitaPr + Int32.Parse(dr["GITA_PRICE"].ToString());
                bankPr = bankPr + Int32.Parse(dr["BANK_PRICE"].ToString());
                yeongiPr = yeongiPr + Int32.Parse(dr["YEONGI_PRICE"].ToString());
                samoPr = samoPr + Int32.Parse(dr["SAMO_PRICE"].ToString());
                nationPr = nationPr + Int32.Parse(dr["NATION_PRICE"].ToString());
                bubinPr = bubinPr + Int32.Parse(dr["BUBIN_PRICE"].ToString());
                ioForePr = ioForePr + Int32.Parse(dr["IOFORE_PRICE"].ToString());
            }

            DvVolumeGiganList.Rows[row].Cells["Week1"].Value = gainPr;
            DvVolumeGiganList.Rows[row + 1].Cells["Week1"].Value = forePr;
            DvVolumeGiganList.Rows[row + 2].Cells["Week1"].Value = giganPr;
            DvVolumeGiganList.Rows[row + 3].Cells["Week1"].Value = gumyPr;
            DvVolumeGiganList.Rows[row + 4].Cells["Week1"].Value = bohumPr;
            DvVolumeGiganList.Rows[row + 5].Cells["Week1"].Value = tosinPr;
            DvVolumeGiganList.Rows[row + 6].Cells["Week1"].Value = gitaPr;
            DvVolumeGiganList.Rows[row + 7].Cells["Week1"].Value = bankPr;
            DvVolumeGiganList.Rows[row + 8].Cells["Week1"].Value = yeongiPr;
            DvVolumeGiganList.Rows[row + 9].Cells["Week1"].Value = samoPr;
            DvVolumeGiganList.Rows[row + 10].Cells["Week1"].Value = nationPr;
            DvVolumeGiganList.Rows[row + 11].Cells["Week1"].Value = bubinPr;
            DvVolumeGiganList.Rows[row + 12].Cells["Week1"].Value = ioForePr;


            results = null;

            gainPr = 0;
            forePr = 0;
            giganPr = 0;
            gumyPr = 0;
            bohumPr = 0;
            tosinPr = 0;
            gitaPr = 0;
            bankPr = 0;
            yeongiPr = 0;
            samoPr = 0;
            nationPr = 0;
            bubinPr = 0;
            ioForePr = 0;

            results = dt.AsEnumerable().Where(a => a["STOCK_CODE"].ToString() == stockCode && (
                                                      Int32.Parse(a["STOCK_DATE"].ToString()) >= Int32.Parse(_clsUtilFunc.WeekMonday(_clsUtilFunc.DateAddDay(searchDate, -7)).ToString()) &&
                                                      Int32.Parse(a["STOCK_DATE"].ToString()) <= Int32.Parse(_clsUtilFunc.WeekFriday(searchDate))));

            foreach (DataRow dr in results)
            {
                gainPr = gainPr + Int32.Parse(dr["GAIN_PRICE"].ToString());
                forePr = forePr + Int32.Parse(dr["FORE_PRICE"].ToString());
                giganPr = giganPr + Int32.Parse(dr["GIGAN_PRICE"].ToString());
                gumyPr = gumyPr + Int32.Parse(dr["GUMY_PRICE"].ToString());
                bohumPr = bohumPr + Int32.Parse(dr["BOHUM_PRICE"].ToString());
                tosinPr = tosinPr + Int32.Parse(dr["TOSIN_PRICE"].ToString());
                gitaPr = gitaPr + Int32.Parse(dr["GITA_PRICE"].ToString());
                bankPr = bankPr + Int32.Parse(dr["BANK_PRICE"].ToString());
                yeongiPr = yeongiPr + Int32.Parse(dr["YEONGI_PRICE"].ToString());
                samoPr = samoPr + Int32.Parse(dr["SAMO_PRICE"].ToString());
                nationPr = nationPr + Int32.Parse(dr["NATION_PRICE"].ToString());
                bubinPr = bubinPr + Int32.Parse(dr["BUBIN_PRICE"].ToString());
                ioForePr = ioForePr + Int32.Parse(dr["IOFORE_PRICE"].ToString());
            }

            DvVolumeGiganList.Rows[row].Cells["Week2"].Value = gainPr;
            DvVolumeGiganList.Rows[row + 1].Cells["Week2"].Value = forePr;
            DvVolumeGiganList.Rows[row + 2].Cells["Week2"].Value = giganPr;
            DvVolumeGiganList.Rows[row + 3].Cells["Week2"].Value = gumyPr;
            DvVolumeGiganList.Rows[row + 4].Cells["Week2"].Value = bohumPr;
            DvVolumeGiganList.Rows[row + 5].Cells["Week2"].Value = tosinPr;
            DvVolumeGiganList.Rows[row + 6].Cells["Week2"].Value = gitaPr;
            DvVolumeGiganList.Rows[row + 7].Cells["Week2"].Value = bankPr;
            DvVolumeGiganList.Rows[row + 8].Cells["Week2"].Value = yeongiPr;
            DvVolumeGiganList.Rows[row + 9].Cells["Week2"].Value = samoPr;
            DvVolumeGiganList.Rows[row + 10].Cells["Week2"].Value = nationPr;
            DvVolumeGiganList.Rows[row + 11].Cells["Week2"].Value = bubinPr;
            DvVolumeGiganList.Rows[row + 12].Cells["Week2"].Value = ioForePr;

        }

        private void CellColorChange(DataGridView dg, int startColum)
        {

            foreach (DataGridViewRow row in dg.Rows)
            {
                for (int i = startColum; i < dg.Columns.Count - startColum; i++)
                {
                    if (Convert.ToInt32(row.Cells[i].Value) == 0)
                    {
                        row.Cells[i].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToInt32(row.Cells[i].Value) < 0)
                    {
                        row.Cells[i].Style.ForeColor = Color.Blue;
                    }
                    else
                    {
                        row.Cells[i].Style.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void ChartInit()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();

            chartArea1.Name = "ChartArea1";
            chartArea2.Name = "Volume";
            chartArea3.Name = "Price";

            chartArea2.AxisX.Enabled = AxisEnabled.False;
            chartArea3.AxisX.Enabled = AxisEnabled.False;

            chartTradeAnaly.ChartAreas.Clear();
            chartTradeAnaly.Series.Clear();
            chartTradeAnaly.ChartAreas.Add(chartArea1);
            chartTradeAnaly.ChartAreas.Add(chartArea2);
            chartTradeAnaly.ChartAreas.Add(chartArea3);

            series1.ChartArea = "Price";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Name = "Price";
            series1.YValuesPerPoint = 4;

            series3.ChartArea = "Volume";
            series3.Name = "거래량";

            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = Color.Purple;
            series4.Name = "개인";

            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = Color.YellowGreen;
            series5.Name = "외국인";

            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = Color.Green;
            series6.Name = "기관";

            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = Color.Magenta;
            series2.Name = "금융";

            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series13.Color = Color.Orange;
            series13.Name = "보험";

            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = Color.Pink;
            series7.Name = "투신";

            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Color = Color.Blue;
            series8.Name = "기타";

            //series9.YValuesPerPoint = 4;
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            series9.Color = Color.Transparent;
            series9.BorderColor = Color.Black;
            series9.Name = "은행";

            //series10.YValuesPerPoint = 4;
            series10.ChartArea = "ChartArea1";
            series10.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Range;
            series10.Color = Color.Transparent;
            series10.BorderColor = Color.DarkGray;
            series10.Name = "연기금";

            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series11.Color = Color.DeepSkyBlue;
            series11.Name = "사모";

            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series12.Color = Color.Red;
            series12.Name = "국가";

            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series14.Color = Color.Black;
            series14.Name = "기법";

            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series15.Color = Color.Gold;
            series15.Name = "내외";


            chartTradeAnaly.ChartAreas["Volume"].AxisX.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["Volume"].AxisX.MajorTickMark.Enabled = false;
            chartTradeAnaly.ChartAreas["Volume"].AxisX.LineWidth = 0;
            chartTradeAnaly.ChartAreas["Volume"].AxisY.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["Volume"].AxisY.MajorTickMark.Enabled = false;
            chartTradeAnaly.ChartAreas["Volume"].AxisY.LineWidth = 0;

            chartTradeAnaly.ChartAreas["Price"].AxisX.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["Price"].AxisX.MajorTickMark.Enabled = false;
            chartTradeAnaly.ChartAreas["Price"].AxisX.LineWidth = 0;
            chartTradeAnaly.ChartAreas["Price"].AxisY.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["Price"].AxisY.MajorTickMark.Enabled = false;
            chartTradeAnaly.ChartAreas["Price"].AxisY.LineWidth = 0;


            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.ScaleView.Position = chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.Maximum;
            chartTradeAnaly.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chartTradeAnaly.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            //_baseChart.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Days;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisX.Interval = 10;
            chartTradeAnaly.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;

            chartTradeAnaly.ChartAreas["Volume"].CursorX.IsUserEnabled = true;
            chartTradeAnaly.ChartAreas["Volume"].CursorX.IsUserSelectionEnabled = true;
            chartTradeAnaly.ChartAreas["Volume"].AxisX.ScaleView.Zoomable = true;

            chartTradeAnaly.ChartAreas["Price"].CursorX.IsUserEnabled = true;
            chartTradeAnaly.ChartAreas["Price"].CursorX.IsUserSelectionEnabled = true;
            chartTradeAnaly.ChartAreas["Price"].AxisX.ScaleView.Zoomable = true;

            chartTradeAnaly.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            chartTradeAnaly.ChartAreas["Volume"].InnerPlotPosition.Auto = true;
            chartTradeAnaly.ChartAreas["Price"].InnerPlotPosition.Auto = true;

            chartTradeAnaly.ChartAreas["Volume"].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
            chartTradeAnaly.ChartAreas["Volume"].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;
            chartTradeAnaly.ChartAreas["Volume"].AlignWithChartArea = "ChartArea1";

            chartTradeAnaly.ChartAreas["Price"].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
            chartTradeAnaly.ChartAreas["Price"].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;
            chartTradeAnaly.ChartAreas["Price"].AlignWithChartArea = "ChartArea1";

            chartTradeAnaly.ChartAreas["ChartArea1"].Position.Auto = false;
            chartTradeAnaly.ChartAreas["ChartArea1"].Position.Height = 75;
            chartTradeAnaly.ChartAreas["ChartArea1"].Position.Width = 100;
            chartTradeAnaly.ChartAreas["ChartArea1"].Position.X = 0;
            chartTradeAnaly.ChartAreas["ChartArea1"].Position.Y = 0;

            chartTradeAnaly.ChartAreas["Price"].Position.Auto = false;
            chartTradeAnaly.ChartAreas["Price"].Position.Height = 15;
            chartTradeAnaly.ChartAreas["Price"].Position.Width = 100;
            chartTradeAnaly.ChartAreas["Price"].Position.X = chartTradeAnaly.ChartAreas["ChartArea1"].Position.X;
            chartTradeAnaly.ChartAreas["Price"].Position.Y = chartTradeAnaly.ChartAreas["ChartArea1"].Position.Y + chartTradeAnaly.ChartAreas["ChartArea1"].Position.Height;

            chartTradeAnaly.ChartAreas["Volume"].Position.Auto = false;
            chartTradeAnaly.ChartAreas["Volume"].Position.Height = 15;
            chartTradeAnaly.ChartAreas["Volume"].Position.Width = 100;
            chartTradeAnaly.ChartAreas["Volume"].Position.X = chartTradeAnaly.ChartAreas["ChartArea1"].Position.X;
            chartTradeAnaly.ChartAreas["Volume"].Position.Y = chartTradeAnaly.ChartAreas["ChartArea1"].Position.Y + chartTradeAnaly.ChartAreas["ChartArea1"].Position.Height;

            chartTradeAnaly.Series.Add(series1);
            chartTradeAnaly.Series.Add(series2);
            chartTradeAnaly.Series.Add(series3);
            chartTradeAnaly.Series.Add(series4);
            chartTradeAnaly.Series.Add(series5);
            chartTradeAnaly.Series.Add(series6);
            chartTradeAnaly.Series.Add(series7);
            chartTradeAnaly.Series.Add(series8);
            chartTradeAnaly.Series.Add(series9);
            chartTradeAnaly.Series.Add(series10);
            chartTradeAnaly.Series.Add(series11);
            chartTradeAnaly.Series.Add(series12);
            chartTradeAnaly.Series.Add(series13);
            chartTradeAnaly.Series.Add(series14);
            chartTradeAnaly.Series.Add(series15);

            chartTradeAnaly.Series["Price"].ToolTip = "#AXISLABEL";
            chartTradeAnaly.Series["Price"].IsXValueIndexed = false;
            chartTradeAnaly.Series["Price"].XValueType = ChartValueType.DateTime;

            chartTradeAnaly.Width = this.Width - 20;
            chartTradeAnaly.Height = this.Height - 5;

        }

        private void DisplayChart(DataTable dt)
        {
            try
            {


                Double[] arrP = new Double[1];
                int pt = 0;
                DataView dv = new DataView(dt);
                int high = 0;
                int low = 0;

                dv.Sort = "일자 asc";

                try
                {

                    for (int ix = chartTradeAnaly.Series.Count - 1; ix >= 0; ix--)
                    {
                        if (chartTradeAnaly.Series[ix].Name.IndexOf(DateTime.Now.ToString("yyyyMMdd")) == -1)
                            chartTradeAnaly.Series[ix].Points.Clear();
                        else
                        {
                            if (chartTradeAnaly.Series[ix].Points.Count > 0)
                            {
                                arrP[arrP.Length - 1] = chartTradeAnaly.Series[ix].Points[0].YValues[0];
                                Array.Resize(ref arrP, arrP.Length + 1);
                            }
                            chartTradeAnaly.Series.RemoveAt(ix);
                        }
                    }
                }
                catch (System.NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                foreach (DataRowView dr in dv)
                {
                    if (dr["일자"].ToString().Trim() == "")
                    {
                        continue;
                    }
                    //if (low == 0)
                    //{
                    //    high = int.Parse(dr["HIGH_PRICE"].ToString());
                    //}
                    //else
                    //{
                    //    if (high < int.Parse(dr["HIGH_PRICE"].ToString()))
                    //    {
                    //        high = int.Parse(dr["HIGH_PRICE"].ToString());
                    //    }
                    //}


                    //if (low == 0)
                    //{
                    //    low = int.Parse(dr["LOW_PRICE"].ToString());
                    //}
                    //else
                    //{
                    //    if (low > int.Parse(dr["LOW_PRICE"].ToString()))
                    //    {
                    //        low = int.Parse(dr["LOW_PRICE"].ToString());
                    //    }
                    //}

                    if (high == 0)
                    {
                        high = int.Parse(dr["고가"].ToString());
                    }
                    else
                    {
                        if (high < int.Parse(dr["고가"].ToString()))
                        {
                            high = int.Parse(dr["고가"].ToString());
                        }
                    }

                    if (low == 0)
                    {
                        low = int.Parse(dr["저가"].ToString());
                    }
                    else
                    {
                        if (low > int.Parse(dr["저가"].ToString()))
                        {
                            low = int.Parse(dr["저가"].ToString());
                        }
                    }

                    chartTradeAnaly.Series["Price"].Points.AddXY((object)dr["일자"], int.Parse(dr["고가"].ToString()));
                    chartTradeAnaly.Series["Price"].Points[pt].YValues[1] = int.Parse(dr["저가"].ToString());
                    chartTradeAnaly.Series["Price"].Points[pt].YValues[2] = int.Parse(dr["시작가"].ToString());
                    chartTradeAnaly.Series["Price"].Points[pt].YValues[3] = int.Parse(dr["현재가"].ToString());

                    chartTradeAnaly.Series["거래량"].Points.AddXY(dr["일자"], double.Parse(dr["거래량"].ToString()));
                    int curCnt = chartTradeAnaly.Series["거래량"].Points.Count - 1;
                    chartTradeAnaly.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Red;

                    if (curCnt > 0)
                    {
                        Double preVolume = chartTradeAnaly.Series["거래량"].Points[curCnt - 1].YValues[0];
                        Double CurVolume = chartTradeAnaly.Series["거래량"].Points[curCnt].YValues[0];

                        if (preVolume < CurVolume)
                        {
                            chartTradeAnaly.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Red;
                        }
                        else
                        {
                            chartTradeAnaly.Series["거래량"].Points[curCnt].Color = System.Drawing.Color.Blue;
                        }
                    }

                    if (int.Parse(dr["시작가"].ToString()) > int.Parse(dr["현재가"].ToString()))
                    {
                        chartTradeAnaly.Series["Price"].Points[pt].Color = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        chartTradeAnaly.Series["Price"].Points[pt].Color = System.Drawing.Color.Red;
                    }

                    chartTradeAnaly.Series["개인"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.GAIN].ToString()));
                    chartTradeAnaly.Series["외국인"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.FORE].ToString()));
                    chartTradeAnaly.Series["기관"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.GIGAN].ToString()));
                    chartTradeAnaly.Series["금융"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.GUMY].ToString()));
                    chartTradeAnaly.Series["보험"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.BOHUM].ToString()));
                    chartTradeAnaly.Series["투신"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.TOSIN].ToString()));
                    chartTradeAnaly.Series["기타"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.GITA].ToString()));
                    chartTradeAnaly.Series["은행"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.BANK].ToString()));
                    chartTradeAnaly.Series["연기금"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.YEONGI].ToString()));
                    chartTradeAnaly.Series["사모"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.SAMO].ToString()));
                    chartTradeAnaly.Series["기법"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.BUBIN].ToString()));
                    chartTradeAnaly.Series["내외"].Points.AddXY(dr["일자"], double.Parse(dr[AnSt.Define.clsDicDefine.IOFORE].ToString()));

                    pt++;
                }
                //_lastVolume = Int64.Parse(chartTradeAnaly.Series["거래량"].Points[chartTradeAnaly.Series["거래량"].Points.Count - 1].YValues[0].ToString());

                chartTradeAnaly.ChartAreas["Price"].AxisY.Minimum = low - (low * 0.1);
                chartTradeAnaly.ChartAreas["Price"].AxisY.Maximum = high + (high * 0.1);
                chartTradeAnaly.ChartAreas["Volume"].AxisX.IsLabelAutoFit = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return;
            }
        }

        private void DvVolumeList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DvVolumeList.Rows[e.RowIndex].Cells["SimpleStockCode"].Value.ToString() != "")
            {
                DisplayVolumeListDetail(DvVolumeList.Rows[e.RowIndex].Cells["SimpleStockCode"].Value.ToString().Trim());
            }
        }

        private void chkSu_CheckedChanged(object sender, EventArgs e)
        {
            VisibleSuVolumeListGigan(((CheckBox)sender).Text, ((CheckBox)sender).Checked);
        }

    }
}
