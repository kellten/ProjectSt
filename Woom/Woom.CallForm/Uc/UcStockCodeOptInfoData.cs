using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Collections.Concurrent;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woom.DataAccess;
using Woom.DataDefine;
using Woom.DataAccess.OptCaller.Class;
using Woom.DataAccess.PlugIn;
using Woom.DataDefine.OptData;
using SDataAccess;
using Woom.DataDefine.Util;

namespace Woom.CallForm.Uc
{
    public partial class UcStockCodeOptInfoData : UserControl
    {
        public string StockCode { get { return _stockCode; } set { _stockCode = value; GetOptStockInfo(_stockCode); } }

        private string _stockCode = "";
        public UcStockCodeOptInfoData()
        {
            InitializeComponent();
            SetDgv();
        }

        private void SetDgv()
        {
            DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
            dataGridViewButtonColumn.Name = "WEB_VIEW";
            dataGridViewButtonColumn.HeaderText = "View";
            dataGridViewButtonColumn.ReadOnly = false;
            dgvStockInfo.Columns.Add(dataGridViewButtonColumn);
            
            dgvStockInfo.Columns.Add(columnName: "STOCK_CODE", headerText: "종목코드");
            dgvStockInfo.Columns.Add(columnName: "종목명", headerText: "종목명");
            dgvStockInfo.Columns.Add(columnName: "현재가",         headerText: "현재가");
            dgvStockInfo.Columns.Add(columnName: "거래량",                            headerText: "거래량");
            dgvStockInfo.Columns.Add(columnName: "거래대금",                          headerText: "거래대금");
            dgvStockInfo.Columns.Add(columnName: "시가",                              headerText: "시가");
            dgvStockInfo.Columns.Add(columnName: "고가",                              headerText: "고가");
            dgvStockInfo.Columns.Add(columnName: "저가",                              headerText: "저가");
            dgvStockInfo.Columns.Add(columnName: "기준일",                            headerText: "기준일");
            dgvStockInfo.Columns.Add(columnName: "결산월",                            headerText:"결산월");
            dgvStockInfo.Columns.Add(columnName: "액면가",                     headerText: "액면가");
            dgvStockInfo.Columns.Add(columnName: "자본금",                            headerText: "자본금");
            dgvStockInfo.Columns.Add(columnName: "상장주식",                          headerText: "상장주식");
            dgvStockInfo.Columns.Add(columnName: "신용비율",                          headerText: "신용비율");
            dgvStockInfo.Columns.Add(columnName: "연중최고",                          headerText: "연중최고");
            dgvStockInfo.Columns.Add(columnName: "연중최저",                          headerText: "연중최저");
            dgvStockInfo.Columns.Add(columnName: "시가총액",                          headerText: "시가총액");
            dgvStockInfo.Columns.Add(columnName: "시가총액비중",                      headerText: "시가총액비중");
            dgvStockInfo.Columns.Add(columnName: "외인소진률",                        headerText: "외인소진률");
            dgvStockInfo.Columns.Add(columnName: "대용가",                            headerText: "대용가");
            dgvStockInfo.Columns.Add(columnName: "PER",                               headerText: "PER");
            dgvStockInfo.Columns.Add(columnName: "EPS",                               headerText: "EPS");
            dgvStockInfo.Columns.Add(columnName: "ROE",                               headerText: "ROE");
            dgvStockInfo.Columns.Add(columnName: "PBR",                               headerText: "PBR");
            dgvStockInfo.Columns.Add(columnName: "EV",                                headerText: "EV");
            dgvStockInfo.Columns.Add(columnName: "BPS",                               headerText: "BPS");
            dgvStockInfo.Columns.Add(columnName: "매출액",                            headerText: "매출액");
            dgvStockInfo.Columns.Add(columnName: "영업이익",                          headerText: "영업이익");
            dgvStockInfo.Columns.Add(columnName: "당기순이익",                        headerText: "당기순이익");
            dgvStockInfo.Columns.Add(columnName: "최고250",                           headerText: "최고250");
            dgvStockInfo.Columns.Add(columnName: "최저250",                           headerText: "최저250");
            dgvStockInfo.Columns.Add(columnName: "최고가일250",                       headerText: "최고가일250");
            dgvStockInfo.Columns.Add(columnName: "최고가대비율250",                   headerText: "최고가대비율250");
            dgvStockInfo.Columns.Add(columnName: "최저가일250",                       headerText: "최저가일250");
            dgvStockInfo.Columns.Add(columnName: "최저가대비율250",                   headerText: "최저가대비율250");
            dgvStockInfo.Columns.Add(columnName: "유통주식",                          headerText: "유통주식");
            dgvStockInfo.Columns.Add(columnName: "유통비율",                          headerText: "유통비율");
            dgvStockInfo.Columns.Add(columnName: "종가",                              headerText: "종가");
            dgvStockInfo.Columns.Add(columnName: "전일대비기호",                      headerText: "전일대비기호");
            dgvStockInfo.Columns.Add(columnName: "전일대비",                          headerText: "전일대비");
            dgvStockInfo.Columns.Add(columnName: "등락율",                            headerText: "등락율");
            dgvStockInfo.Columns.Add(columnName: "거래량",                            headerText: "거래량");
            dgvStockInfo.Columns.Add(columnName: "거래대금", headerText: " 거래대금");

            ClsDataGridViewUtil clsDataGridViewUtil = new ClsDataGridViewUtil();
            clsDataGridViewUtil.RemoveGridViewRow(dgvStockInfo);
            
        }

        private void GetOptStockInfo(string stockCode)
        {
            if (stockCode == null)
            { return; }

            if (stockCode == "")
            { return; }
            KiwoomQuery kiwoomQuery = new KiwoomQuery();
            DataTable dt = new DataTable();

            ClsCollectOptDataFunc clsCollectOptDataFunc = new ClsCollectOptDataFunc();
            string stdDate = clsCollectOptDataFunc.AvailableTradingDate();

            dt = kiwoomQuery.p_StockCodeOptInfoQuery(query: "1", stockCode: stockCode, stockDate: stdDate, bln3tier: false).Tables[0].Copy();

            if (dt.Rows.Count > 0)
            {
                int row = 0;
                if (dgvStockInfo.Rows.Count == 1)
                { row = 0; }
                else
                { dgvStockInfo.Rows.Add();
                    row = dgvStockInfo.Rows.Count - 1;
                }
                                         

                DataRow dr = dt.Rows[0];

                dgvStockInfo.Rows[row].Cells["STOCK_CODE"].Value = dr["STOCK_CODE"].ToString().Trim();
                dgvStockInfo.Rows[row].Cells["종목명"].Value = ClsAxKH.GetMasterCodeName(dr["STOCK_CODE"].ToString().Trim());
dgvStockInfo.Rows[row].Cells["현재가"].Value =                          dr["현재가"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["거래량"].Value =                          dr["거래량"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["거래대금"].Value =                        dr["거래대금"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["시가"].Value =                            dr["시가"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["고가"].Value =                            dr["고가"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["저가"].Value =                            dr["저가"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["기준일"].Value =                          dr["CALL_DATE"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["결산월"].Value =                          dr["결산월"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["액면가"].Value =                          dr["액면가"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["자본금"].Value =                          dr["자본금"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["상장주식"].Value =                        dr["상장주식"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["신용비율"].Value =                        dr["신용비율"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["연중최고"].Value =                        dr["연중최고"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["연중최저"].Value =                        dr["연중최저"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["시가총액"].Value =                        dr["시가총액"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["시가총액비중"].Value =                    dr["시가총액비중"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["외인소진률"].Value =                      dr["외인소진률"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["대용가"].Value =                          dr["대용가"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["PER"].Value =                             dr["PER"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["EPS"].Value =                             dr["EPS"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["ROE"].Value =                             dr["ROE"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["PBR"].Value =                             dr["PBR"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["EV"].Value =                              dr["EV"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["BPS"].Value =                             dr["BPS"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["매출액"].Value =                          dr["매출액"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["영업이익"].Value =                        dr["영업이익"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["당기순이익"].Value =                      dr["당기순이익"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["최고250"].Value =                         dr["최고250"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["최저250"].Value =                         dr["최저250"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["최고가일250"].Value =                     dr["최고가일250"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["최고가대비율250"].Value =                 dr["최고가대비율250"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["최저가일250"].Value =                     dr["최저가일250"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["최저가대비율250"].Value =                 dr["최저가대비율250"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["유통주식"].Value =                        dr["유통주식"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["유통비율"].Value =                        dr["유통비율"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["종가"].Value =                            dr["종가"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["전일대비기호"].Value =                    dr["전일대비기호"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["전일대비"].Value =                        dr["전일대비"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["등락율"].Value =                          dr["등락율"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["거래량"].Value =                          dr["거래량"].ToString().Trim();
dgvStockInfo.Rows[row].Cells["거래대금"].Value =                        dr["거래대금"].ToString().Trim();
                dgvStockInfo.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }

            dt = null;
        }

        private void dgvStockInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                System.Diagnostics.Process.Start("https://finance.naver.com/item/coinfo.nhn?code=" + dgvStockInfo.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());
                System.Diagnostics.Process.Start("https://finance.naver.com/item/fchart.nhn?code=" + dgvStockInfo.Rows[e.RowIndex].Cells["STOCK_CODE"].Value.ToString().Trim());
            }
        }
    }
}
