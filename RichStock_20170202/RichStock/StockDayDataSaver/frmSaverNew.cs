﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VB6 = Microsoft.VisualBasic;
using System.Globalization;
using PaikRichStock.Common;
using System.Text.RegularExpressions;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.Internals;
using System.Net;
using System.IO;
using PaikRichStock.UcForm;
namespace StockDayDataSaver
{
    public partial class frmSaverNew : Form
    {
        private bool _isOneDay;
        private DataTable _dt = new DataTable("DAYINFO");
        private string _FILTERSTR = "STOCK_CODE <> '' AND STOCK_NAME NOT LIKE '%ETN%' AND STOCK_NAME NOT LIKE '%선물%'  AND STOCK_NAME NOT LIKE '트러스%'  AND STOCK_NAME NOT LIKE 'KBSTAR%' AND STOCK_NAME NOT LIKE '하이골드%' AND STOCK_NAME NOT LIKE 'KINDEX%'  AND STOCK_NAME NOT LIKE 'KOSEF%'   AND STOCK_NAME NOT LIKE '하나니켈%'   AND STOCK_NAME NOT LIKE 'TREX%' AND STOCK_NAME NOT LIKE '코리아0%' AND STOCK_NAME NOT LIKE '동북아%'  AND STOCK_NAME NOT LIKE '아시아 %' AND STOCK_NAME NOT LIKE '%우B' AND STOCK_NAME NOT LIKE '%1호' AND STOCK_NAME NOT LIKE '%2호' AND STOCK_NAME NOT LIKE '%K' AND STOCK_NAME NOT LIKE '%채권%' AND STOCK_NAME NOT LIKE '%신탁%' AND STOCK_NAME NOT LIKE '%TIGER%' AND STOCK_NAME NOT LIKE '%KODEX%' AND STOCK_NAME NOT LIKE '%KINDEX%' AND STOCK_NAME NOT LIKE '%ARIRANG%' AND STOCK_NAME NOT LIKE '%(합성%'";
        private DataAccess _DataAcc;
        private string _mode = "";
        int _workerCnt = 0;
        public frmSaverNew()
        {
            InitializeComponent();
            _DataAcc = new DataAccess();
        }

        public frmSaverNew(string mode)
        {
            InitializeComponent();
            _DataAcc = new DataAccess();
            _mode = mode;
        }

        public void ExcuteSendDataMibi()
        {
            _rowCnt = 0;
            _isOneDay = true;

            DataSet ds;
            DataView dv;
            ds = _DataAcc.p_stock_batch_mibi(null, null);
            dv = new DataView(ds.Tables[0]);
            dv.Sort = "STOCK_CODE ASC";
            dataGridView1.DataSource = dv;
            if (dv.Count < 1)
            {
                mySqlDbConn.Close();
                ucMainStockVer2.DisConnection();
                _browser.Dispose();
                Application.Exit();
                
                return;
            }
            ucStockAvgMagipPrice1.ProStdDate = DateTime.Now.ToString("yyyyMMdd");
            ucStockAvgMagipPrice1.PropWriteStockList10081 = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dataGridView1);
        }

        public void ExcuteStockMaster()
        {
            DataView dv;
            try
            {
                if (ucMainStockVer2._KospiStockDataset == null) { return; }
                dv = new DataView(ucMainStockVer2._KospiStockDataset.Tables[0]);
                dv.RowFilter = _FILTERSTR;
                foreach (DataRowView dr in dv)
                {
                    if (dr["STOCK_CODE"].ToString().Trim() == "") continue;
                    _DataAcc.p_Stock_Master_Add("A", dr["STOCK_CODE"].ToString().Trim(), dr["STOCK_NAME"].ToString().Trim(), "U", true, null, null);
                }


                if (ucMainStockVer2._KosDakStockDataset == null) { return; }
                dv = new DataView(ucMainStockVer2._KosDakStockDataset.Tables[0]);
                dv.RowFilter = _FILTERSTR;
                foreach (DataRowView dr in dv)
                {
                    if (dr["STOCK_CODE"].ToString().Trim() == "") continue;
                    _DataAcc.p_Stock_Master_Add("A", dr["STOCK_CODE"].ToString().Trim(), dr["STOCK_NAME"].ToString().Trim(), "K", true, null, null);
                }
            }
            finally
            {
                _DataAcc.DisConnect();
            }
        }

        public void ExcuteSendData()
        {
            _rowCnt = 0;
            _isOneDay = true;
            DataView dv;
            if (ucMainStockVer2._allStockDataset == null) { return; }

            dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
            dv.RowFilter = _FILTERSTR;
            dv.Sort = "STOCK_CODE ASC";

            dataGridView1.DataSource = dv;
            ucStockAvgMagipPrice1.ProStdDate = DateTime.Now.ToString("yyyyMMdd");
            ucStockAvgMagipPrice1.PropWriteStockList10081 = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dataGridView1);
            
        }

        public void ExcuteSendDataWeek()
        {
            _rowCnt = 0;
            _isOneDay = true;
            DataView dv;
            if (ucMainStockVer2._allStockDataset == null) { return; }

            dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
            dv.RowFilter = _FILTERSTR;
            dv.Sort = "STOCK_CODE ASC";

            dataGridView1.DataSource = dv;
            ucStockAvgMagipPrice1.ProStdDate = DateTime.Now.ToString("yyyyMMdd");
            ucStockAvgMagipPrice1.PropWriteStockList10082 = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dataGridView1);

        }

        public void ExcuteSendDataFirst()
        {
            _rowCnt = 0;
            _isOneDay = false;
            DataView dv;
            if (ucMainStockVer2._allStockDataset == null) { return; }

            dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
            dv.RowFilter = _FILTERSTR;
            dv.Sort = "STOCK_CODE ASC";

            dataGridView1.DataSource = dv;
            ucStockAvgMagipPrice1.ProStdDate = DateTime.Now.ToString("yyyyMMdd");
            ucStockAvgMagipPrice1.PropWriteStockList10081 = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dataGridView1);
        }

        public void ExcuteSendDataWeekFirst()
        {
            _rowCnt = 0;
            _isOneDay = false;
            DataView dv;
            if (ucMainStockVer2._allStockDataset == null) { return; }

            dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
            dv.RowFilter = _FILTERSTR;
            dv.Sort = "STOCK_CODE ASC";

            dataGridView1.DataSource = dv;
            ucStockAvgMagipPrice1.ProStdDate = DateTime.Now.ToString("yyyyMMdd");
            ucStockAvgMagipPrice1.PropWriteStockList10082 = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dataGridView1);
        }

        public void ExcuteCompanyCEO()
        {
            DataView dv;
            DataSet ds = new DataSet();
            DataTable dt;


            if (ucMainStockVer2._allStockDataset == null) { return; }

            try
            {
                dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
                dv.RowFilter = _FILTERSTR;
                dv.Sort = "STOCK_CODE ASC";
                foreach (DataRowView dr in dv)
                {
                    tslStatus.Text = dr["STOCK_CODE"].ToString().Trim() + "-" + _workerCnt;
                    try
                    {
                        dt = GetCEOAndETC(dr["STOCK_CODE"].ToString().Trim());
                    }
                    catch { continue; }
                    if (dt == null) continue;
                    if (dt.Rows.Count < 1) continue;

                    foreach (DataRow drv in dt.Rows)
                    {
                        _DataAcc.p_company_ceo_Add(
                            "A",
                            dr["STOCK_CODE"].ToString().Trim(),
                            drv["PUB_DATE"].ToString().Trim(),
                            drv["성명"].ToString().Trim(),
                            drv["성별"].ToString().Trim(),
                            drv["출생년월"].ToString().Trim(),
                            drv["직위"].ToString().Trim(),
                            drv["등기임원여부"].ToString().Trim(),
                            drv["상근여부"].ToString().Trim(),
                            drv["담당업무"].ToString().Trim(),
                            drv["주요경력"].ToString().Trim(),
                            drv["의결권있는주식"].ToString().Trim(),
                            drv["의결권없는주식"].ToString().Trim(),
                            drv["재직기간"].ToString().Trim(),
                            drv["임기만료일"].ToString().Trim(),
                            true,
                            null,
                            null
                            );
                    }
                    _workerCnt++;
                    Application.DoEvents();
                }
            }
            finally
            {
                _DataAcc.DisConnect();
            }
        }

        public void ExcuteCompanyData()
        {
            DataSet ds = new DataSet();
            DataRow drCompany;
            DataView dv, dvCompany;
            DataTable dt;


            ds = _DataAcc.p_company_info_query("2", "", false);
            dvCompany = new DataView(ds.Tables[0]);

            if (ucMainStockVer2._allStockDataset == null) { return; }

            try
            {
                dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
                dv.RowFilter = _FILTERSTR;
                dv.Sort = "STOCK_CODE ASC";
                foreach (DataRowView dr in dv)
                {
                    dt = GetCompanyInfo(dr["STOCK_CODE"].ToString().Trim());
                    if (dt.Rows.Count < 1) continue;
                    drCompany = dt.Rows[0];
                    _DataAcc.p_company_info_Add("A",
                        drCompany["STOCK_CODE"].ToString().Trim(),
                        drCompany["JUJU"].ToString().Trim(),
                        drCompany["CLASS_GB"].ToString().Trim(),
                        drCompany["CEO"].ToString().Trim(),
                        drCompany["PRE_CNAME"].ToString().Trim(),
                        drCompany["CREATE_DATE"].ToString().Trim(),
                        drCompany["SANGJANG_DATE"].ToString().Trim(),
                        drCompany["WORKER_CNT"].ToString().Trim(),
                        drCompany["GROUP_NAME"].ToString().Trim(),
                        drCompany["HOMEPAGE"].ToString().Trim(),
                        drCompany["ADDRESS"].ToString().Trim(),
                        drCompany["COMPANY_TEL"].ToString().Trim(),
                        drCompany["JUDAM_TEL"].ToString().Trim(),
                        drCompany["GAMSA_TEXT"].ToString().Trim(),
                        drCompany["MAIN_BANK"].ToString().Trim(),
                        drCompany["MAIN_PRODUCT"].ToString().Trim(),
                        drCompany["ETC"].ToString().Trim(),
                        drCompany["REMARK"].ToString().Trim(),
                        true,
                        null,
                        null
                        );
                }
            }
            finally
            {
                _DataAcc.DisConnect();
            }
        }

        private void btn일봉데이터전송_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteSendData();

            tslStatus.Text = "작업이 완료되었습니다!!";
        }

        private void frmSaver_Load(object sender, EventArgs e)
        {
            InitBrowser();
            tslStatus.Text = "";
            ucMainStockVer2.Connection();
        }

        private int _rowCnt = 0;
        private void StoredStockDayDataOneDay(DataSet ds)
        {
            ArrayParam arr = new ArrayParam();
            ArrayParams arrs = new ArrayParams();
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1) {
                if (dataGridView1.Rows.Count - 2 == _rowCnt)
                {
                    mySqlDbConn.Close();
                    ucMainStockVer2.DisConnection();
                    _browser.Dispose();
                    Application.Exit();
                }
                
                return; 
            }

            string 종목코드;
            int 현재가;
            int 거래량;
            int 거래대금;
            string 일자;
            int 시가;
            int 고가;
            int 저가;
            string 수정주가구분;
            string 수정비율;
            string 대업종구분;
            string 소업종구분;
            string 종목정보;
            string 수정주가이벤트;
            string 전일종가;
            string 저가MA;
            string 기간최저가;
            string 기간종가최저가;
            string 최저가MA;
            string 최저가종가MA;
            string 최고저가종가MA;

            if (ds.Tables[0].Rows[0]["수정주가구분"].ToString().Trim() != "")
            {
                종목코드 = ds.Tables[0].Rows[0]["종목코드"].ToString().Trim();

                mySqlDbConn.Open();
                mySqlDbConn.ExecuteNonQuery("delete from stock_day_data where stock_code = '" + 종목코드 + "'", null, CommandType.Text);

                arrs.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                    거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
                    거래대금 = Convert.ToInt32(dr["거래대금"].ToString().Trim());
                    일자 = dr["일자"].ToString().Trim();
                    시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                    고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                    저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                    수정주가구분 = dr["수정주가구분"].ToString().Trim();
                    수정비율 = dr["수정비율"].ToString().Trim();
                    대업종구분 = dr["대업종구분"].ToString().Trim();
                    소업종구분 = dr["소업종구분"].ToString().Trim();
                    종목정보 = dr["종목정보"].ToString().Trim();
                    수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                    전일종가 = dr["전일종가"].ToString().Trim();


                    저가MA = dr["저가MA"].ToString().Trim();
                    기간최저가 = dr["기간최저가"].ToString().Trim();
                    기간종가최저가 = dr["기간종가최저가"].ToString().Trim();
                    최저가MA = dr["최저가MA"].ToString().Trim();
                    최저가종가MA = dr["최저가종가MA"].ToString().Trim();
                    최고저가종가MA = dr["최고저가종가MA"].ToString().Trim();

                    arr.Clear();
                    arr.Add("STOCK_CODE", 종목코드);
                    arr.Add("STOCK_DATE", 일자);
                    arr.Add("END_PRICE", 현재가.ToString().Trim());
                    arr.Add("VOLUME", 거래량.ToString().Trim());
                    arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                    arr.Add("S_PRICE", 시가.ToString().Trim());
                    arr.Add("H_PRICE", 고가.ToString().Trim());
                    arr.Add("L_PRICE", 저가.ToString().Trim());
                    arr.Add("MOD_GUBUN", 수정주가구분);
                    arr.Add("MOD_RATE", 수정비율.ToString().Trim());
                    arr.Add("CATE_GUBUN1", 대업종구분);
                    arr.Add("CATE_GUBUN2", 소업종구분);
                    arr.Add("STOCK_INFO", 종목정보);
                    arr.Add("MOD_EVENT", 수정주가이벤트);
                    arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                    arr.Add("LOW_MA", 저가MA.ToString().Trim());
                    arr.Add("LOWEST_PERIOD", 기간최저가.ToString().Trim());
                    arr.Add("ENDLOW_PERIOD", 기간종가최저가.ToString().Trim());
                    arr.Add("LOWEST_MA", 최저가MA.ToString().Trim());
                    arr.Add("LOWEND_MA", 최저가종가MA.ToString().Trim());
                    arr.Add("H_LOWEND_MA", 최고저가종가MA.ToString().Trim());
                    arrs.Add(arr);
                }
                if (arrs.Count > 0) { 
                    mySqlDbConn.ExecuteNonMultiInsert("stock_day_data", arrs);
                }
                mySqlDbConn.Close();
                _rowCnt++;
                return;
            }

            if (_dt.Columns.Count < 1) _dt = ds.Tables[0].Clone();

            _dt.Rows.Add(ds.Tables[0].Rows[0].ItemArray);
            tslStatus.Text = ds.Tables[0].Rows[0]["종목코드"].ToString().Trim();
            
            if (dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["STOCK_CODE"].Value.ToString() == ds.Tables[0].Rows[0]["종목코드"].ToString().Trim())
            {
                try
                {
                    mySqlDbConn.Open();
                    arrs.Clear();
                    foreach (DataRow dr in _dt.Rows )
                    {
                        종목코드 = dr["종목코드"].ToString().Trim();
                        현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                        거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
                        거래대금 = Convert.ToInt32(dr["거래대금"].ToString().Trim());
                        일자 = dr["일자"].ToString().Trim();
                        시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                        고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                        저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                        수정주가구분 = dr["수정주가구분"].ToString().Trim();
                        수정비율 = dr["수정비율"].ToString().Trim();
                        대업종구분 = dr["대업종구분"].ToString().Trim();
                        소업종구분 = dr["소업종구분"].ToString().Trim();
                        종목정보 = dr["종목정보"].ToString().Trim();
                        수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                        전일종가 = dr["전일종가"].ToString().Trim();


                        저가MA = dr["저가MA"].ToString().Trim();
                        기간최저가 = dr["기간최저가"].ToString().Trim();
                        기간종가최저가 = dr["기간종가최저가"].ToString().Trim();
                        최저가MA = dr["최저가MA"].ToString().Trim();
                        최저가종가MA = dr["최저가종가MA"].ToString().Trim();
                        최고저가종가MA = dr["최고저가종가MA"].ToString().Trim();

                        arr.Clear();
                        arr.Add("STOCK_CODE", 종목코드);
                        arr.Add("STOCK_DATE", 일자);
                        arr.Add("END_PRICE", 현재가.ToString().Trim());
                        arr.Add("VOLUME", 거래량.ToString().Trim());
                        arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                        arr.Add("S_PRICE", 시가.ToString().Trim());
                        arr.Add("H_PRICE", 고가.ToString().Trim());
                        arr.Add("L_PRICE", 저가.ToString().Trim());
                        arr.Add("MOD_GUBUN", 수정주가구분);
                        arr.Add("MOD_RATE", 수정비율.ToString().Trim());
                        arr.Add("CATE_GUBUN1", 대업종구분);
                        arr.Add("CATE_GUBUN2", 소업종구분);
                        arr.Add("STOCK_INFO", 종목정보);
                        arr.Add("MOD_EVENT", 수정주가이벤트);
                        arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                        arr.Add("LOW_MA", 저가MA.ToString().Trim());
                        arr.Add("LOWEST_PERIOD", 기간최저가.ToString().Trim());
                        arr.Add("ENDLOW_PERIOD", 기간종가최저가.ToString().Trim());
                        arr.Add("LOWEST_MA", 최저가MA.ToString().Trim());
                        arr.Add("LOWEND_MA", 최저가종가MA.ToString().Trim());
                        arr.Add("H_LOWEND_MA", 최고저가종가MA.ToString().Trim());
                        arrs.Add(arr);
                    }
                    mySqlDbConn.ExecuteNonMultiInsert("stock_day_data", arrs);
                }
                finally
                {
                    mySqlDbConn.Close();
                    ucMainStockVer2.DisConnection();
                    _browser.Dispose();
                    Application.Exit();
                }
            }
            else
            {
                _rowCnt++;
            }
        }

        private void StoredStockDayData(DataSet ds)
        {
            ArrayParam arr = new ArrayParam();
            ArrayParams arrs = new ArrayParams();
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1) return;

            string 종목코드 = ds.Tables[0].Rows[0]["종목코드"].ToString().Trim();
            int 현재가;
            int 거래량;
            int 거래대금;
            string 일자;
            int 시가;
            int 고가;
            int 저가;
            string 수정주가구분;
            string 수정비율;
            string 대업종구분;
            string 소업종구분;
            string 종목정보;
            string 수정주가이벤트;
            string 전일종가;
            string 저가MA;
            string 기간최저가;
            string 기간종가최저가;
            string 최저가MA;
            string 최저가종가MA;
            string 최고저가종가MA;

            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "일자 >= " + DateTime.Now.AddDays(-360).ToString("yyyyMMdd") + "";

            tslStatus.Text = 종목코드 + "-" + _workerCnt;
            try
            {
                mySqlDbConn.Open();
                arrs.Clear();
                foreach (DataRowView dr in dv)
                {
                    현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                    거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
                    거래대금 =Convert.ToInt32(dr["거래대금"].ToString().Trim());
                    일자 = dr["일자"].ToString().Trim();
                    시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                    고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                    저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                    수정주가구분 = dr["수정주가구분"].ToString().Trim();
                    수정비율 = dr["수정비율"].ToString().Trim();
                    대업종구분 = dr["대업종구분"].ToString().Trim();
                    소업종구분 = dr["소업종구분"].ToString().Trim();
                    종목정보 = dr["종목정보"].ToString().Trim();
                    수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                    전일종가 = dr["전일종가"].ToString().Trim();


                    저가MA = dr["저가MA"].ToString().Trim();
                    기간최저가 = dr["기간최저가"].ToString().Trim();
                    기간종가최저가 = dr["기간종가최저가"].ToString().Trim();
                    최저가MA = dr["최저가MA"].ToString().Trim();
                    최저가종가MA = dr["최저가종가MA"].ToString().Trim();
                    최고저가종가MA = dr["최고저가종가MA"].ToString().Trim();

                    arr.Clear();
                    arr.Add("STOCK_CODE", 종목코드);
                    arr.Add("STOCK_DATE", 일자);
                    arr.Add("END_PRICE", 현재가.ToString().Trim());
                    arr.Add("VOLUME", 거래량.ToString().Trim());
                    arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                    arr.Add("S_PRICE", 시가.ToString().Trim());
                    arr.Add("H_PRICE", 고가.ToString().Trim());
                    arr.Add("L_PRICE", 저가.ToString().Trim());
                    arr.Add("MOD_GUBUN", 수정주가구분);
                    arr.Add("MOD_RATE", 수정비율.ToString().Trim());
                    arr.Add("CATE_GUBUN1", 대업종구분);
                    arr.Add("CATE_GUBUN2", 소업종구분);
                    arr.Add("STOCK_INFO", 종목정보);
                    arr.Add("MOD_EVENT", 수정주가이벤트);
                    arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                    arr.Add("LOW_MA", 저가MA.ToString().Trim());
                    arr.Add("LOWEST_PERIOD", 기간최저가.ToString().Trim());
                    arr.Add("ENDLOW_PERIOD", 기간종가최저가.ToString().Trim());
                    arr.Add("LOWEST_MA", 최저가MA.ToString().Trim());
                    arr.Add("LOWEND_MA", 최저가종가MA.ToString().Trim());
                    arr.Add("H_LOWEND_MA", 최고저가종가MA.ToString().Trim());
                    arrs.Add(arr);
                }
                mySqlDbConn.ExecuteNonMultiInsert("stock_day_data" , arrs);
            }
            finally
            {
                mySqlDbConn.Close();
            }
        }

        private void StoredStockWeekDataOneDay(DataSet ds)
        {
            ArrayParam arr = new ArrayParam();
            ArrayParam arrDelete = new ArrayParam();
            ArrayParams arrs = new ArrayParams();
            ArrayParams arrsDelete = new ArrayParams();
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1)
            {
                if (dataGridView1.Rows.Count - 2 == _rowCnt)
                {
                    mySqlDbConn.Close();
                    ucMainStockVer2.DisConnection();
                    _browser.Dispose();
                    Application.Exit();
                }

                return;
            }

            string 종목코드;
            int 현재가;
            int 거래량;
            int 거래대금;
            string 일자;
            int 시가;
            int 고가;
            int 저가;
            string 수정주가구분;
            string 수정비율;
            string 대업종구분;
            string 소업종구분;
            string 종목정보;
            string 수정주가이벤트;
            string 전일종가;

            //_DataAcc.p_stock_week_data_Add("D", ds.Tables[0].Rows[0]["종목코드"].ToString().Trim(), "0", "0", "0", ds.Tables[0].Rows[0]["일자"].ToString().Trim(), "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", true, null, null);

            if (ds.Tables[0].Rows[0]["수정주가구분"].ToString().Trim() != "")
            {
                종목코드 = ds.Tables[0].Rows[0]["종목코드"].ToString().Trim();

                mySqlDbConn.Open();
                mySqlDbConn.ExecuteNonQuery("delete from stock_day_data where stock_code = '" + 종목코드 + "'", null, CommandType.Text);

                arrs.Clear();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                    거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
                    거래대금 = Convert.ToInt32(dr["거래대금"].ToString().Trim());
                    일자 = dr["일자"].ToString().Trim();
                    시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                    고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                    저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                    수정주가구분 = dr["수정주가구분"].ToString().Trim();
                    수정비율 = dr["수정비율"].ToString().Trim();
                    대업종구분 = dr["대업종구분"].ToString().Trim();
                    소업종구분 = dr["소업종구분"].ToString().Trim();
                    종목정보 = dr["종목정보"].ToString().Trim();
                    수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                    전일종가 = dr["전일종가"].ToString().Trim();


                    arr.Clear();
                    arr.Add("STOCK_CODE", 종목코드);
                    arr.Add("STOCK_DATE", 일자);
                    arr.Add("END_PRICE", 현재가.ToString().Trim());
                    arr.Add("VOLUME", 거래량.ToString().Trim());
                    arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                    arr.Add("S_PRICE", 시가.ToString().Trim());
                    arr.Add("H_PRICE", 고가.ToString().Trim());
                    arr.Add("L_PRICE", 저가.ToString().Trim());
                    arr.Add("MOD_GUBUN", 수정주가구분);
                    arr.Add("MOD_RATE", 수정비율.ToString().Trim());
                    arr.Add("CATE_GUBUN1", 대업종구분);
                    arr.Add("CATE_GUBUN2", 소업종구분);
                    arr.Add("STOCK_INFO", 종목정보);
                    arr.Add("MOD_EVENT", 수정주가이벤트);
                    arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                    arrs.Add(arr);
                }
                if (arrs.Count > 0)
                {
                    mySqlDbConn.ExecuteNonMultiInsert("stock_week_data", arrs);
                }
                mySqlDbConn.Close();
                _rowCnt++;
                return;
            }

            if (_dt.Columns.Count < 1) _dt = ds.Tables[0].Clone();

            _dt.Rows.Add(ds.Tables[0].Rows[0].ItemArray);
            tslStatus.Text = ds.Tables[0].Rows[0]["종목코드"].ToString().Trim();

            

            if (dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["STOCK_CODE"].Value.ToString() == ds.Tables[0].Rows[0]["종목코드"].ToString().Trim())
            {
                try
                {
                    mySqlDbConn.Open();
                    arrs.Clear();
                    arrsDelete.Clear();
                    foreach (DataRow dr in _dt.Rows)
                    {
                        종목코드 = dr["종목코드"].ToString().Trim();
                        현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                        거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
                        거래대금 = (int)(Convert.ToInt64(dr["거래대금"].ToString().Trim()) / 1000000);
                        일자 = dr["일자"].ToString().Trim();
                        시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                        고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                        저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                        수정주가구분 = dr["수정주가구분"].ToString().Trim();
                        수정비율 = dr["수정비율"].ToString().Trim();
                        대업종구분 = dr["대업종구분"].ToString().Trim();
                        소업종구분 = dr["소업종구분"].ToString().Trim();
                        종목정보 = dr["종목정보"].ToString().Trim();
                        수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                        전일종가 = dr["전일종가"].ToString().Trim();

                        arrDelete.Clear();
                        arrDelete.Add("STOCK_CODE", 종목코드);
                        arrDelete.Add("STOCK_DATE", 일자);
                        arrsDelete.Add(arrDelete);

                        arr.Clear();
                        arr.Add("STOCK_CODE", 종목코드);
                        arr.Add("STOCK_DATE", 일자);
                        arr.Add("END_PRICE", 현재가.ToString().Trim());
                        arr.Add("VOLUME", 거래량.ToString().Trim());
                        arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                        arr.Add("S_PRICE", 시가.ToString().Trim());
                        arr.Add("H_PRICE", 고가.ToString().Trim());
                        arr.Add("L_PRICE", 저가.ToString().Trim());
                        arr.Add("MOD_GUBUN", 수정주가구분);
                        arr.Add("MOD_RATE", 수정비율.ToString().Trim());
                        arr.Add("CATE_GUBUN1", 대업종구분);
                        arr.Add("CATE_GUBUN2", 소업종구분);
                        arr.Add("STOCK_INFO", 종목정보);
                        arr.Add("MOD_EVENT", 수정주가이벤트);
                        arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                        arrs.Add(arr);
                    }
                    mySqlDbConn.ExecuteNonMultiDelete("stock_week_data", arrsDelete);
                    mySqlDbConn.ExecuteNonMultiInsert("stock_week_data", arrs);
                }
                finally
                {
                    mySqlDbConn.Close();
                    ucMainStockVer2.DisConnection();
                    _browser.Dispose();
                    Application.Exit();
                }
            }
            else
            {
                _rowCnt++;
            }
        }

        private void StoredStockWeekData(DataSet ds)
        {
            ArrayParam arr = new ArrayParam();
            ArrayParams arrs = new ArrayParams();
            if (ds.Tables.Count < 1) { _rowCnt++; return; }
            if (ds.Tables[0].Rows.Count < 1) { _rowCnt++; return; }

            string 종목코드 = ds.Tables[0].Rows[0]["종목코드"].ToString().Trim();
            int 현재가;
            int 거래량;
            int 거래대금;
            string 일자;
            int 시가;
            int 고가;
            int 저가;
            string 수정주가구분;
            string 수정비율;
            string 대업종구분;
            string 소업종구분;
            string 종목정보;
            string 수정주가이벤트;
            string 전일종가;

            DataView dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "일자 >= '20100101'";
            tslStatus.Text = 종목코드 + "-" + _workerCnt;
            try
            {
                mySqlDbConn.Open();
                arrs.Clear();
                foreach (DataRowView dr in dv)
                {
                    현재가 = Convert.ToInt32(dr["현재가"].ToString().Trim());
                    거래량 = Convert.ToInt32(dr["거래량"].ToString().Trim());
                    거래대금 = (int)(Convert.ToInt64(dr["거래대금"].ToString().Trim()) / 1000000);
                    일자 = dr["일자"].ToString().Trim();
                    시가 = Convert.ToInt32(dr["시가"].ToString().Trim());
                    고가 = Convert.ToInt32(dr["고가"].ToString().Trim());
                    저가 = Convert.ToInt32(dr["저가"].ToString().Trim());
                    수정주가구분 = dr["수정주가구분"].ToString().Trim();
                    수정비율 = dr["수정비율"].ToString().Trim();
                    대업종구분 = dr["대업종구분"].ToString().Trim();
                    소업종구분 = dr["소업종구분"].ToString().Trim();
                    종목정보 = dr["종목정보"].ToString().Trim();
                    수정주가이벤트 = dr["수정주가이벤트"].ToString().Trim();
                    전일종가 = dr["전일종가"].ToString().Trim();

                    arr.Clear();
                    arr.Add("STOCK_CODE", 종목코드);
                    arr.Add("STOCK_DATE", 일자);
                    arr.Add("END_PRICE", 현재가.ToString().Trim());
                    arr.Add("VOLUME", 거래량.ToString().Trim());
                    arr.Add("TRADING_VALUE", 거래대금.ToString().Trim());
                    arr.Add("S_PRICE", 시가.ToString().Trim());
                    arr.Add("H_PRICE", 고가.ToString().Trim());
                    arr.Add("L_PRICE", 저가.ToString().Trim());
                    arr.Add("MOD_GUBUN", 수정주가구분);
                    arr.Add("MOD_RATE", 수정비율.ToString().Trim());
                    arr.Add("CATE_GUBUN1", 대업종구분);
                    arr.Add("CATE_GUBUN2", 소업종구분);
                    arr.Add("STOCK_INFO", 종목정보);
                    arr.Add("MOD_EVENT", 수정주가이벤트);
                    arr.Add("PRE_E_PRICE", 전일종가.ToString().Trim());
                    arrs.Add(arr);
                }
                mySqlDbConn.ExecuteNonMultiInsert("stock_week_data", arrs);
            }
            finally
            {
                mySqlDbConn.Close();
            }
        }

        private bool _IsReady = false;
        public DataTable GetCompanyInfo(string stockCode)
        {
            DataRow dr;
            DataTable dt = new DataTable();
            dt.Columns.Add("STOCK_CODE");
            dt.Columns.Add("JUJU");
            dt.Columns.Add("CLASS_GB");
            dt.Columns.Add("CEO");
            dt.Columns.Add("PRE_CNAME");
            dt.Columns.Add("CREATE_DATE");
            dt.Columns.Add("SANGJANG_DATE");
            dt.Columns.Add("WORKER_CNT");
            dt.Columns.Add("GROUP_NAME");
            dt.Columns.Add("HOMEPAGE");
            dt.Columns.Add("ADDRESS");
            dt.Columns.Add("COMPANY_TEL");
            dt.Columns.Add("JUDAM_TEL");
            dt.Columns.Add("GAMSA_TEXT");
            dt.Columns.Add("MAIN_BANK");
            dt.Columns.Add("MAIN_PRODUCT");
            dt.Columns.Add("ETC");
            dt.Columns.Add("REMARK");
            dr = dt.NewRow();

            dr["STOCK_CODE"] = stockCode;

            //WebBrowser browser = new WebBrowser();
            //browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            string url = String.Format("http://media.kisline.com/highlight/mainHighlight.nice?paper_stock={0}&nav=1&header=N&comp=daishin", stockCode);
            //browser.ScriptErrorsSuppressed = true;
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                _browser.Load(url);
            }
            //while (browser.IsBusy || browser.DocumentText.Length < 5) { Application.DoEvents(); }
            _IsReady = false;
            do
            {
                try
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); //스택 오버플로워 오류 가능성있음
                }


            } while (!_IsReady);
            //browser.DocumentCompleted -= new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);

            string source = _htmlSource;
            source = System.Net.WebUtility.HtmlDecode(source);
            string text = "";

            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<';
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            const string tabformatSpace = @"(\r|\n|\t)";

            var tabformatSpaceRegex = new Regex(tabformatSpace, RegexOptions.Multiline);
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            ////Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            ////Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);

            //if (source.Length < 3000) { return dt; }
            if (source.IndexOf("<p><img src=\"http://cdn-image.kisline.com/media/common/images/search/txt_sea1.gif\" alt=\"기업분석의 최강자! 국내 최대/최고의 기업정보 보유 기관! 여러분의 성공투자를 NICE신용평가정보와 함께 하세요.\" /></p>") > -1) return dt;


            if (source.IndexOf("series: [{") < 0)
            {
                dr["JUJU"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("series: [{"), source.IndexOf("}]") - source.IndexOf("series: [{"));
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = text.Substring(text.IndexOf("[[") + 1, text.IndexOf("],{") - text.IndexOf("[["));
                dr["JUJU"] = text.Replace("'", string.Empty);//주주현황
            }


            if (source.IndexOf("<p class=\"cot_tx\" title=\"회사정보\">") < 0)
            {
                dr["CLASS_GB"] = "";//업종
            }
            else
            {
                text = source.Substring(source.IndexOf("<p class=\"cot_tx\" title=\"회사정보\">"));
                text = text.Substring(0, text.IndexOf("</p>") + 4);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["CLASS_GB"] = text;//업종
            }

            if (source.IndexOf("<th scope=\"row\">대표이사</th>") < 0)
            {
                dr["CEO"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">대표이사</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["CEO"] = text;//대표이사
            }

            if (source.IndexOf("<th scope=\"row\">구상호</th>") < 0)
            {
                dr["PRE_CNAME"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">구상호</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["PRE_CNAME"] = text;//(구)상호
            }

            if (source.IndexOf("<th scope=\"row\">설립일</th>") < 0) return dt;
            text = source.Substring(source.IndexOf("<th scope=\"row\">설립일</th>"));
            text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
            text = tabformatSpaceRegex.Replace(text, string.Empty);
            text = tagWhiteSpaceRegex.Replace(text, "><");
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            text = stripFormattingRegex.Replace(text, string.Empty);
            dr["CREATE_DATE"] = text;//설립일


            if (source.IndexOf("<th scope=\"row\">상장일</th>") < 0)
            {
                dr["SANGJANG_DATE"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">상장일</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["SANGJANG_DATE"] = text;//상장일
            }


            if (source.IndexOf("<th scope=\"row\">종업원수</th>") < 0)
            {
                dr["WORKER_CNT"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">종업원수</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["WORKER_CNT"] = text;//종업원수
            }

            if (source.IndexOf("<th scope=\"row\">그룹명</th>") < 0)
            {
                dr["GROUP_NAME"] = "";//그룹명
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">그룹명</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["GROUP_NAME"] = text;//그룹명
            }


            if (source.IndexOf("<th scope=\"row\">홈페이지</th>") < 0)
            {
                dr["HOMEPAGE"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">홈페이지</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["HOMEPAGE"] = text;//홈페이지
            }

            if (source.IndexOf("<th scope=\"row\">주소</th>") < 0)
            {
                dr["ADDRESS"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">주소</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["ADDRESS"] = text;//주소
            }


            if (source.IndexOf("<th scope=\"row\">대표전화</th>") < 0)
            {
                dr["COMPANY_TEL"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">대표전화</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["COMPANY_TEL"] = text;//대표전화
            }


            if (source.IndexOf("<th scope=\"row\">주식담당자</th>") < 0)
            {
                dr["JUDAM_TEL"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">주식담당자</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["JUDAM_TEL"] = text;//주식담당자
            }



            if (source.IndexOf("<th scope=\"row\">감사의견</th>") < 0)
            {
                dr["GAMSA_TEXT"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">감사의견</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["GAMSA_TEXT"] = text;//감사의견
            }

            if (source.IndexOf("<th scope=\"row\">주거래은행</th>") < 0)
            {
                dr["MAIN_BANK"] = "";//주거래은행
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">주거래은행</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["MAIN_BANK"] = text;//주거래은행
            }


            if (source.IndexOf("<th scope=\"row\">주요품목</th>") < 0)
            {
                dr["MAIN_PRODUCT"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">주요품목</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["MAIN_PRODUCT"] = text;//주요품목
            }

            if (source.IndexOf("<th scope=\"row\">특기사항</th>") < 0)
            {
                dr["ETC"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<th scope=\"row\">특기사항</th>"));
                text = text.Substring(text.IndexOf("<td>"), text.IndexOf("</td>") - text.IndexOf("<td>") + 5);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["ETC"] = text;//특기사항
            }

            if (source.IndexOf("<!-- 현황 및 전망 -->") < 0)
            {
                dr["REMARK"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("<!-- 현황 및 전망 -->"));
                text = text.Substring(text.IndexOf("<tbody>"), text.IndexOf("</tbody>") - text.IndexOf("<tbody>") + 8);
                text = tabformatSpaceRegex.Replace(text, string.Empty);
                text = tagWhiteSpaceRegex.Replace(text, "><");
                text = lineBreakRegex.Replace(text, Environment.NewLine);
                text = stripFormattingRegex.Replace(text, string.Empty);
                dr["REMARK"] = text;//현황과 전망
            }
            dt.Rows.Add(dr);

            GC.Collect();
            return dt;
        }

        private ChromiumWebBrowser _browser;
        private string _htmlSource = "";
        private void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            _browser = new ChromiumWebBrowser("");
            _browser.FrameLoadEnd += new EventHandler<CefSharp.FrameLoadEndEventArgs>(browser_FrameLoadEnd);
            this.Controls.Add(_browser);
            _browser.Dock = DockStyle.Fill;
        }

        private void DisposeBrowser()
        {
            _browser.Dispose();
            Cef.Shutdown();
        }

        private void browser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                _browser.GetSourceAsync().ContinueWith(taskHtml =>
                {
                    _IsReady = true;
                    _htmlSource = taskHtml.Result;
                });
            }
        }

        private void StoredStockBaseData(DataSet ds)
        {
            ArrayParam arr = new ArrayParam();
            ArrayParams arrs = new ArrayParams();
            ArrayParam arrDelete = new ArrayParam();
            ArrayParams arrsDelete = new ArrayParams();

            if (ds.Tables.Count < 1) { _rowCnt++; return; }
            if (ds.Tables[0].Rows.Count < 1)
            {
                if (dataGridView1.Rows.Count - 2 == _rowCnt)
                {
                    mySqlDbConn.Close();
                    ucMainStockVer2.DisConnection();
                    _browser.Dispose();
                    Application.Exit();
                }

                _rowCnt++; return;
            }

            if (ds.Tables[0].Rows[0]["종목코드"] == null) { _rowCnt++; return; }
            if (ds.Tables[0].Rows[0]["종목코드"].ToString() == "") { _rowCnt++; return; }


            if (_dt.Columns.Count < 1) _dt = ds.Tables[0].Clone();

            _dt.Rows.Add(ds.Tables[0].Rows[0].ItemArray);
            tslStatus.Text = ds.Tables[0].Rows[0]["종목코드"].ToString().Trim();

            if (dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["STOCK_CODE"].Value.ToString() == ds.Tables[0].Rows[0]["종목코드"].ToString().Trim())
            {
                try
                {
                    mySqlDbConn.Open();
                    arrs.Clear();
                    arrsDelete.Clear();
                    foreach (DataRow dr in _dt.Rows )
                    {
                        string 종목코드 = dr["종목코드"].ToString().Trim();
                        string 종목명 = dr["종목명"].ToString().Trim();
                        string 결산월 = dr["결산월"].ToString().Trim();
                        string 액면가 = dr["액면가"].ToString().Trim();
                        string 자본금 = dr["자본금"].ToString().Trim();
                        string 상장주식 = dr["상장주식"].ToString().Trim();
                        string 신용비율 = dr["신용비율"].ToString().Trim();
                        string 연중최고 = dr["연중최고"].ToString().Trim();
                        string 연중최저 = dr["연중최저"].ToString().Trim();
                        string 시가총액 = dr["시가총액"].ToString().Trim();
                        string 시가총액비중 = dr["시가총액비중"].ToString().Trim();
                        string 외인소진률 = dr["외인소진률"].ToString().Trim();
                        string 대용가 = dr["대용가"].ToString().Trim();
                        string PER = dr["PER"].ToString().Trim();
                        string EPS = dr["EPS"].ToString().Trim();
                        string ROE = dr["ROE"].ToString().Trim();
                        string PBR = dr["PBR"].ToString().Trim();
                        string EV = dr["EV"].ToString().Trim();
                        string BPS = dr["BPS"].ToString().Trim();
                        string 매출액 = dr["매출액"].ToString().Trim();
                        string 영업이익 = dr["영업이익"].ToString().Trim();
                        string 당기순이익 = dr["당기순이익"].ToString().Trim();
                        string 최고250 = dr["250최고"].ToString().Trim();
                        string 최저250 = dr["250최저"].ToString().Trim();
                        string 시가 = dr["시가"].ToString().Trim();
                        string 고가 = dr["고가"].ToString().Trim();
                        string 저가 = dr["저가"].ToString().Trim();
                        string 상한가 = dr["상한가"].ToString().Trim();
                        string 하한가 = dr["하한가"].ToString().Trim();
                        string 기준가 = dr["기준가"].ToString().Trim();
                        string 최고가일250 = dr["250최고가일"].ToString().Trim();
                        string 최고가대비율250 = dr["250최고가대비율"].ToString().Trim();
                        string 최저가일250 = dr["250최저가일"].ToString().Trim();
                        string 최저가대비율250 = dr["250최저가대비율"].ToString().Trim();
                        string 현재가 = dr["현재가"].ToString().Trim();
                        string 대비기호 = dr["대비기호"].ToString().Trim();
                        string 전일대비 = dr["전일대비"].ToString().Trim();
                        string 등락율 = dr["등락율"].ToString().Trim();
                        string 거래량 = dr["거래량"].ToString().Trim();
                        string 거래대비 = dr["거래대비"].ToString().Trim();
                        string 액면가단위 = dr["액면가단위"].ToString().Trim();


                        arrDelete.Clear();
                        arrDelete.Add("STOCK_CODE", 종목코드);
                        arrsDelete.Add(arrDelete);

                        arr.Clear();
                        arr.Add("STOCK_CODE", 종목코드)                                ;
                        arr.Add("STOCK_NAME", 종목명)                                  ;
                        arr.Add("REULT_MONTH", 결산월)                                 ;
                        arr.Add("BASE_P", 액면가)                                      ;
                        arr.Add("CAPITAL_P", 자본금)                                   ;
                        arr.Add("STOCK_QTY", 상장주식)                                 ;
                        arr.Add("CREDIT_RATE", 신용비율)                               ;
                        arr.Add("YEAR_HIGH_P", 연중최고)                               ;
                        arr.Add("YEAR_LOW_P", 연중최저)                                ;
                        arr.Add("STOCK_TOTAL_P", 시가총액)                             ;
                        arr.Add("STOCK_TOTAL_P_RATE", 시가총액비중)                    ;
                        arr.Add("F_REDUCE_RATE", 외인소진률)                           ;
                        arr.Add("RENTAL_P", 대용가)                                    ;
                        arr.Add("PER", PER)                                            ;
                        arr.Add("EPS", EPS)                                            ;
                        arr.Add("ROE", ROE)                                            ;
                        arr.Add("PBR", PBR)                                            ;
                        arr.Add("EV", EV)                                              ;
                        arr.Add("BPS", BPS)                                            ;
                        arr.Add("SALES_P", 매출액)                                     ;
                        arr.Add("O_PROFIT", 영업이익)                                  ;
                        arr.Add("P_PROFIT", 당기순이익)                                ;
                        arr.Add("HIGH_250", 최고250)                                   ;
                        arr.Add("LOW_250", 최저250)                                    ;
                        arr.Add("S_PRICE", 시가)                                       ;
                        arr.Add("H_PRICE", 고가)                                       ;
                        arr.Add("L_PROCE", 저가)                                       ;
                        arr.Add("UP_PRICE", 상한가)                                    ;
                        arr.Add("DOWN_PRICE", 하한가)                                  ;
                        arr.Add("STD_PRICE", 기준가)                                   ;
                        arr.Add("HIGH_250_DAY", 최고가일250)                           ;
                        arr.Add("HIGH_250_RATE", 최고가대비율250)                      ;
                        arr.Add("LOW_250_DAY", 최저가일250)                            ;
                        arr.Add("LOW_250_RATE", 최저가대비율250)                       ;
                        arr.Add("END_PRICE", 현재가)                                   ;
                        arr.Add("SYMBOL", 대비기호)                                    ;
                        arr.Add("PRE_CONPARE", 전일대비)                               ;
                        arr.Add("END_PRICE_RATE", 등락율)                              ;
                        arr.Add("VOLUME", 거래량)                                      ;
                        arr.Add("VOLUME_RATE", 거래대비)                              ;
                        arr.Add("PRICE_UNIT", 액면가단위)                              ;
                        arr.Add("DEUNG_DATE", DateTime.Now.ToString("yyyyMMdd"));
                        arrs.Add(arr);
                    }
                    mySqlDbConn.ExecuteNonMultiDelete("stock_finance", arrsDelete);
                    mySqlDbConn.ExecuteNonMultiInsert("stock_finance", arrs);
                }
                finally
                {
                    mySqlDbConn.Close();
                    ucMainStockVer2.DisConnection();
                    _browser.Dispose();
                    Application.Exit();
                }
            }
            else
            {
                _rowCnt++;
            }

            //_DataAcc.p_stock_finance_Add(
            //    "A",
            //    종목코드,
            //    종목명,
            //    결산월,
            //    액면가,
            //    자본금,
            //    상장주식,
            //    신용비율,
            //    연중최고,
            //    연중최저,
            //    시가총액,
            //    시가총액비중,
            //    외인소진률,
            //    대용가,
            //    PER,
            //    EPS,
            //    ROE,
            //    PBR,
            //    EV,
            //    BPS,
            //    매출액,
            //    영업이익,
            //    당기순이익,
            //    최고250,
            //    최저250,
            //    시가,
            //    고가,
            //    저가,
            //    상한가,
            //    하한가,
            //    기준가,
            //    최고가일250,
            //    최고가대비율250,
            //    최저가일250,
            //    최저가대비율250,
            //    현재가,
            //    대비기호,
            //    전일대비,
            //    등락율,
            //    거래량,
            //    거래대비,
            //    액면가단위,
            //    isConnect,
            //    null,
            //    null);
                    
            //if (dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["STOCK_CODE"].Value.ToString().Trim() == 종목코드)
            //{
            //    _DataAcc.DisConnect();
            //    mySqlDbConn.Close();
            //    ucMainStockVer2.DisConnection();
            //    _browser.Dispose();
            //    Application.Exit();
            //}
            //_rowCnt++;
        }

        public void ExcuteStockFinance()
        {
            DataView dv;
            if (ucMainStockVer2._allStockDataset == null) { return; }
            //mySqlDbConn.GetDataTableCommndText("TRUNCATE TABLE STOCK_FINACE");
            dv = new DataView(ucMainStockVer2._allStockDataset.Tables[0]);
            dv.RowFilter = _FILTERSTR;
            dv.Sort = "STOCK_CODE ASC";

            dataGridView1.DataSource = dv;
            ucStockAvgMagipPrice1.PropWriteStockList10001 = PaikRichStock.Common.clsFunc.FavAddDataGridViewToDataSet(dataGridView1);
        }

        private DataTable GetCEOAndETC(string stockCode)
        {

            DataSet ds = Dart(stockCode);
            DataTable dt = new DataTable("STOCK_CEO");
            DataRow dr;

            if (ds.Tables["list"] == null) return dt;
            string pubDate = ds.Tables["list"].Rows[0]["rcp_dt"].ToString().Trim();
            string url = "http://dart.fss.or.kr/dsaf001/main.do?rcpNo=" + ds.Tables["list"].Rows[0]["rcp_no"].ToString().Trim();
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                _browser.Load(url);
            }
            //while (browser.IsBusy || browser.DocumentText.Length < 5) { Application.DoEvents(); }
            _IsReady = false;
            do
            {
                try
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); //스택 오버플로워 오류 가능성있음
                }


            } while (!_IsReady);

            //임원 및 직원의 현황
            string source = _htmlSource;
            source = System.Net.WebUtility.HtmlDecode(source);
            string text = "";

            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<';
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            const string tabformatSpace = @"(\r|\n|\t)";
            const string trformatSpace = @"</(tr|TR)>";
            const string tdformatSpace = @"</(td|TD)>";

            var tabformatSpaceRegex = new Regex(tabformatSpace, RegexOptions.Multiline);
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);
            var trformatSpaceRegex = new Regex(trformatSpace, RegexOptions.Multiline);
            var tdformatSpaceRegex = new Regex(tdformatSpace, RegexOptions.Multiline);


            if (source.IndexOf("임원 및 직원의 현황") < 0)
            {
                //dr["JUJU"] = "";
            }
            else
            {
                text = source.Substring(source.IndexOf("임원 및 직원의 현황"));
                text = text.Substring(text.IndexOf("viewDoc("), text.IndexOf("'dart3.xsd');}") - text.IndexOf("viewDoc("));
                text = text.Replace("viewDoc(", "").Replace("'", "");
                string[] str = text.Split(',');
                url = String.Format("http://dart.fss.or.kr/report/viewer.do?rcpNo={0}&dcmNo={1}&eleId={2}&offset={3}&length={4}&dtd=dart3.xsd", str[0].Trim(), str[1].Trim(), str[2].Trim(), str[3].Trim(), str[4].Trim());
            }


            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                _browser.Load(url);
            }
            //while (browser.IsBusy || browser.DocumentText.Length < 5) { Application.DoEvents(); }
            _IsReady = false;
            do
            {
                try
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); //스택 오버플로워 오류 가능성있음
                }


            } while (!_IsReady);

            //임원 및 직원의 현황

            source = _htmlSource;
            source = System.Net.WebUtility.HtmlDecode(source);

            text = tabformatSpaceRegex.Replace(source, string.Empty);
            text = tagWhiteSpaceRegex.Replace(text, "><");

            text = text.Substring(text.IndexOf("</thead><tbody><tr>") + 15);
            text = text.Substring(0, text.IndexOf("</tbody>"));
            text = text.Replace("</p>", ":").Replace("<br>", ":");
            text = trformatSpaceRegex.Replace(text, "^");
            text = tdformatSpaceRegex.Replace(text, "|");
            text = stripFormattingRegex.Replace(text, "");

            string[] strTr = text.Split('^');
            string[] strTd;

            dt.Columns.Add("PUB_DATE");
            dt.Columns.Add("성명");
            dt.Columns.Add("성별");
            dt.Columns.Add("출생년월");
            dt.Columns.Add("직위");
            dt.Columns.Add("등기임원여부");
            dt.Columns.Add("상근여부");
            dt.Columns.Add("담당업무");
            dt.Columns.Add("주요경력");
            dt.Columns.Add("의결권있는주식");
            dt.Columns.Add("의결권없는주식");
            dt.Columns.Add("재직기간");
            dt.Columns.Add("임기만료일");
            int ix = 0;
            foreach (string str in strTr)
            {
                ix = 0;
                if (str == "") continue;
                dr = dt.NewRow();
                strTd = str.Split('|');
                dr["PUB_DATE"] = pubDate;
                dr["성명"] = strTd[ix++].Trim().Replace(":", "");
                if (strTd.Length == 13) { 
                    dr["성별"] = strTd[ix++].Trim().Replace(":", "");
                }
                dr["출생년월"] = strTd[ix++].Trim().Replace(":", "");
                dr["직위"] = strTd[ix++].Trim().Replace(":", "");
                dr["등기임원여부"] = strTd[ix++].Trim().Replace(":", "");
                dr["상근여부"] = strTd[ix++].Trim().Replace(":", "");
                dr["담당업무"] = strTd[ix++].Trim().Replace(":", "");
                dr["주요경력"] = strTd[ix++].Trim().Replace(":", Environment.NewLine);
                dr["의결권있는주식"] = strTd[ix++].Trim().Replace(":", "");
                dr["의결권없는주식"] = strTd[ix++].Trim().Replace(":", "");
                dr["재직기간"] = strTd[ix++].Trim().Replace(":", "");
                dr["임기만료일"] = strTd[ix++].Trim().Replace(":", "");
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataSet Dart(string stockCode)
        {
            string authKey = "";
            int seed = DateTime.Now.Millisecond;
            Random r = new Random(seed);

            int ranNum = r.Next();


            /*
            sundown99@hanafos.com
            인증키 : 2871266923d9676db1ebde770fddbc27af172b99
            발급일 : 2016-11-23

            sundown99@nate.com
            인증키 : 65c65a332282daf3dcf8b593111001ea05edb8d3
            발급일 : 2016-12-09

            kojedevil@gmail.com
            인증키 : 111688347f9a0c9363962e559b49db99ebf4f7e0
            발급일 : 2016-12-09

            kojedevil@gmail.com
            인증키 : 111688347f9a0c9363962e559b49db99ebf4f7e0
            발급일 : 2016-12-09


            ydk3119@hanmail.net
            인증키 : 751d353e9651c526800b4a5f25232d8ce826a486
            발급일 : 2016-12-09
             * 

            kojeyangari@naver.com
            인증키 : 1738de9acf96edf110619de6573ded998c98a17b
            발급일 : 2016-12-09
             * 
              //인증키 : da676408b4af06f3bec0d0200edf6f2d731cd61b   //sundown99@daum.net
            //발급일 : 2016-12-12
             */
            if (ranNum % 9 == 0)
            {
                authKey = "2871266923d9676db1ebde770fddbc27af172b99";
            }
            else if (ranNum % 9 == 1)
            {
                authKey = "65c65a332282daf3dcf8b593111001ea05edb8d3";
            }
            else if (ranNum % 9 == 2)
            {
                authKey = "111688347f9a0c9363962e559b49db99ebf4f7e0";
            }
            else if (ranNum % 9 == 3)
            {
                authKey = "111688347f9a0c9363962e559b49db99ebf4f7e0";
            }
            else if (ranNum % 9 == 4)
            {
                authKey = "751d353e9651c526800b4a5f25232d8ce826a486";
            }
            else if (ranNum % 9 == 5)
            {
                authKey = "dabf505d5d3326b779cb6d7b523c8a4766bfeb8d";
            }
            else if (ranNum % 9 == 6)
            {
                authKey = "a241a5f9b253c93b17f7a9d51768ff58131fb5dd";
            }
            else if (ranNum % 9 == 7)
            {
                authKey = "1738de9acf96edf110619de6573ded998c98a17b";
            }
            else if (ranNum % 9 == 8)
            {
                authKey = "da676408b4af06f3bec0d0200edf6f2d731cd61b";
            }
            else
            {
                authKey = "db3e22ead89d0449f95fbbf966969cc41e6cf943"; // 종근
            }

            authKey = "da676408b4af06f3bec0d0200edf6f2d731cd61b";

            String apiURL = "http://dart.fss.or.kr/api/search.xml?auth=" + authKey + "&crp_cd=" + stockCode + "&start_dt=19990101&bsn_tp=A003&page_set=1";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader1 = new StreamReader(response.GetResponseStream());

            string page = reader1.ReadToEnd();
            System.Data.DataSet ds = new System.Data.DataSet();

            StringReader sReader = new StringReader(page);

            System.Xml.XmlReader reader = System.Xml.XmlReader.Create((TextReader)sReader);

            ds.ReadXml(reader);
            return ds;
        }

        private int _SLEEP_TIME = 100000;
        private void SystemSleep()
        {
            int i = 0;
            while (i < _SLEEP_TIME)
            {
                Application.DoEvents();
                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteCompanyCEO();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteStockMaster();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteCompanyData();
        }

        private void ucMainStockVer2_OnEventConnect(string status)
        {
            ucStockAvgMagipPrice1.MainStock = ucMainStockVer2;
            dataGridView1.DataSource = ucMainStockVer2._allStockDataset.Tables[0];

            if (_mode == "auto") { ExcuteSendData();  }
            else if (_mode == "automibi") ExcuteSendDataMibi();
            else if (_mode == "finance") ExcuteStockFinance();
            else if (_mode == "autoweek") ExcuteSendDataWeek();
        }

        private void ucMainStockVer2_OnReceiveTrData_Opt10001(DataSet ds)
        {
            StoredStockBaseData(ds); 
        }

        private void ucMainStockVer2_OnReceiveTrData_opt10081(DataSet ds)
        {
            //Cls.GetDayMaDt(ds.Tables[0]);
            if (_isOneDay)
            {
                StoredStockDayDataOneDay(ds);
            }
            else {  //초기자료 생성시
                StoredStockDayData(ds);
            }
        }

        private void ucMainStockVer2_OnReceiveTrData_opt10082(DataSet ds)
        {
            if (_isOneDay)
            {
                StoredStockWeekDataOneDay(ds);
            }
            else
            {  //초기자료 생성시
                StoredStockWeekData(ds);
            }
        }
        private void frmSaverNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            DisposeBrowser();
        }

        private void btnOpt10001_Click(object sender, EventArgs e)
        {
            _rowCnt = 0;
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteStockFinance();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteSendDataMibi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteSendDataFirst();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteSendDataWeekFirst();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ExcuteSendDataWeek();

            tslStatus.Text = "작업이 완료되었습니다!!";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (ucMainStockVer2._allStockDataset == null) { return; }
            ucMainStockVer2.Opt10081_OnReceiveTrData("002140", "고려산업", "20170126");
        }

    }
}
