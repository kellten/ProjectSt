using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalysisSt.DataBaseFunc;
using AnalysisSt.Common;

namespace AnalysisSt.Analysis.VolumeAnalysis.Uc
{
    public partial class ucVolumeAnalysis0 : UserControl
    {
        public ucVolumeAnalysis0()
        {
            InitializeComponent();
        }
        private void ucVolumeAnalysis0_Load(object sender, EventArgs e)
        {
            InitDgvVolume0();
        }

        private string _StockCode;
        private string _FromDate;
        private string _ToDate;
        private double[] _arrPrice;
    
        private DataTable _dt10060Price = new DataTable();
        private DataTable _dt10059Scare = new DataTable();
        private DataTable _dtOpt10081 = new DataTable();
        private DataTable _corrDt = new DataTable();

        private TechnicalFunc.TradeAnalysis.clsTechStats oTechStats = new TechnicalFunc.TradeAnalysis.clsTechStats();
        public string StockCode { get { return _StockCode; } set { _StockCode = value; DisplayVolume(); } }
        public string FromDate { get { return _FromDate; } set { _FromDate = value; } }
        public string ToDate { get { return _ToDate; } set { _ToDate = value; } }
        
        private void InitDgvVolume0()
        { 
            dgvVolume0.ColumnCount = 10;
            dgvVolume0.Columns[0].Name = "일자";
            dgvVolume0.Columns[1].Name = "퍼센";
            dgvVolume0.Columns[2].Name = "주체";
            dgvVolume0.Columns[3].Name = "전체";
            dgvVolume0.Columns[4].Name = "매수";
            dgvVolume0.Columns[5].Name = "매도";
            dgvVolume0.Columns[6].Name = "상관계수";
            dgvVolume0.Columns[7].Name = "기간누적매수";
            dgvVolume0.Columns[8].Name = "기간누적매도";
            dgvVolume0.Columns[9].Name = "현재퍼센";

            dgvVolume1.RowCount = 7;  
           
        }

        private void InitCorrDt()
        { 
            if (_corrDt != null) {_corrDt = null; _corrDt = new DataTable();}

            _corrDt.Columns.Add("주체");
            _corrDt.Columns.Add("상관계수", typeof(double));

        }

        private void DisplayVolume()
        {
            if (_StockCode == "" || _StockCode == null) { return; }

            InitCorrDt();
            GetDgvVolume0();
            GetDgvVolume1();
            Get10060PriceData();
            Get10059ScareData();
            Get10081Data();
            GetCorrData();
        }

        private void GetDgvVolume0()
        {
            if (_StockCode == "" || _StockCode == null) { return; }
            if (_FromDate == "" || _FromDate == null) { return; }
            if (_ToDate == "" || _ToDate == null) { return; }
            
            dgvVolume0.Rows.Clear();

            lblMinTradeDaeGum.Text = "";

          
           // int i = 0;
          

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    dgvVolume0.Rows.Add();
            //    dgvVolume0.Rows[i].Cells["퍼센"].Value = dr["SGROUP_NAME"].ToString();
            //    dgvVolume0.Rows[i].Cells["주체"].Value = dr["SGROUP_INFO"].ToString();
            //    dgvVolume0.Rows[i].Cells["전체"].Value = dr["SGROUP_CODE"].ToString();
            //    dgvVolume0.Rows[i].Cells["퍼센"].Value = dr["SGROUP_NAME"].ToString();
            //    dgvVolume0.Rows[i].Cells["매수"].Value = dr["SGROUP_INFO"].ToString();
            //    dgvVolume0.Rows[i].Cells["매도"].Value = dr["SGROUP_CODE"].ToString();
            //    dgvVolume0.Rows[i].Cells["상관계수"].Value = dr["SGROUP_NAME"].ToString();
            //    dgvVolume0.Rows[i].Cells["기간누적매수"].Value = dr["SGROUP_INFO"].ToString();
            //    dgvVolume0.Rows[i].Cells["기간누적매도"].Value = dr["SGROUP_CODE"].ToString();
            //    dgvVolume0.Rows[i].Cells["현재퍼센"].Value = dr["SGROUP_INFO"].ToString();

            //    i = i + 1;
            //}

            
            
        }

        private void GetDgvVolume1()
        {
            if (_StockCode == "" || _StockCode == null) { return; }
            DataSet ds;
            KiwoomQuery oKi = new KiwoomQuery();
            int i = 0;
            
            ds = oKi.p_Smm01UnPivotQuery("2", _StockCode, "", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1) { return; }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                 i = Convert.ToInt32(dr["SEQ"]);
                 i = i - 1;
                if (i < 7)
                {
                
                    switch (dr["MAX_MAEDO_WHO"].ToString().Trim())
	                {

                        case Common.Class.clsDicDefine.GAIN_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.GAIN; break;
                        case Common.Class.clsDicDefine.FORE_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.FORE; break;
                        case Common.Class.clsDicDefine.GIGAN_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.GIGAN; break;
                        case Common.Class.clsDicDefine.GUMY_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.GUMY; break;
                        case Common.Class.clsDicDefine.BOHUM_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.BOHUM; break;
                        case Common.Class.clsDicDefine.TOSIN_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.TOSIN; break;
                        case Common.Class.clsDicDefine.GITA_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.GITA; break;
                        case Common.Class.clsDicDefine.BANK_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.BANK; break;
                        case Common.Class.clsDicDefine.YEONGI_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.YEONGI; break;
                        case Common.Class.clsDicDefine.SAMO_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.SAMO; break;
                        case Common.Class.clsDicDefine.NATION_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.NATION; break;
                        case Common.Class.clsDicDefine.BUBIN_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.BUBIN; break;
                        case Common.Class.clsDicDefine.IOFORE_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.IOFORE; break;
                        case Common.Class.clsDicDefine.GIGAN_SUM_PRICE: dgvVolume1.Rows[i].Cells["주체1"].Value = Common.Class.clsDicDefine.GIGAN_SUM; break;
	                 }

                    dgvVolume1.Rows[i].Cells["매도1"].Value = dr["MAX_MAEDO_PRICE"];
                    dgvVolume1.Rows[i].Cells["매수1"].Value = dr["MAX_MAESU_PRICE"];
                    dgvVolume1.Rows[i].Cells["범위1"].Value = dr["SCARE_PRICE"];
                    
                }
                else 
                {
                    switch (dr["MAX_MAEDO_WHO"].ToString().Trim())
                    {

                        case Common.Class.clsDicDefine.GAIN_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.GAIN; break;
                        case Common.Class.clsDicDefine.FORE_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.FORE; break;
                        case Common.Class.clsDicDefine.GIGAN_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.GIGAN; break;
                        case Common.Class.clsDicDefine.GUMY_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.GUMY; break;
                        case Common.Class.clsDicDefine.BOHUM_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.BOHUM; break;
                        case Common.Class.clsDicDefine.TOSIN_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.TOSIN; break;
                        case Common.Class.clsDicDefine.GITA_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.GITA; break;
                        case Common.Class.clsDicDefine.BANK_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.BANK; break;
                        case Common.Class.clsDicDefine.YEONGI_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.YEONGI; break;
                        case Common.Class.clsDicDefine.SAMO_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.SAMO; break;
                        case Common.Class.clsDicDefine.NATION_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.NATION; break;
                        case Common.Class.clsDicDefine.BUBIN_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.BUBIN; break;
                        case Common.Class.clsDicDefine.IOFORE_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.IOFORE; break;
                        case Common.Class.clsDicDefine.GIGAN_SUM_PRICE: dgvVolume1.Rows[i - 7].Cells["주체2"].Value = Common.Class.clsDicDefine.GIGAN_SUM; break;
                    }

                    dgvVolume1.Rows[i - 7].Cells["매도2"].Value = dr["MAX_MAEDO_PRICE"];
                    dgvVolume1.Rows[i - 7].Cells["매수2"].Value = dr["MAX_MAESU_PRICE"];
                    dgvVolume1.Rows[i - 7].Cells["범위2"].Value = dr["SCARE_PRICE"];
                }
            }

            ds.Reset();
        }

        private void Get10060PriceData()
        {
            DataSet ds;
            KiwoomQuery oKi = new KiwoomQuery();

            ds = oKi.p_OPT10060PriceQuery("1", _StockCode, _FromDate, _ToDate, "", false);

            if (ds == null || ds.Tables[0].Rows.Count < 1) { ds.Reset(); return; }

            if (_dt10060Price != null) { _dt10060Price = null; _dt10060Price = new DataTable(); }
            _dt10060Price = ds.Tables[0].Copy();

            ds.Reset();

        }

        private void Get10059ScareData()
        {
            DataSet ds;
            KiwoomQuery oKi = new KiwoomQuery();

            ds = oKi.p_Opt10059ScarePriceNujukQuery("1", _StockCode, _FromDate, _ToDate, false);

            if (ds == null || ds.Tables[0].Rows.Count < 1) { ds.Reset(); return; }
            if (_dt10059Scare != null) { _dt10059Scare = null; _dt10059Scare = new DataTable(); }

            _dt10059Scare = ds.Tables[0].Copy();

            ds.Reset();
        }

        private void Get10081Data()
        {
            DataSet ds;
            KiwoomQuery oKi = new KiwoomQuery();

            ds = oKi.p_Opt10081Query("2", _StockCode, _FromDate, _ToDate, false);

            if (ds == null || ds.Tables[0].Rows.Count < 1) { ds.Reset(); return; }
            if (_dtOpt10081 != null) { _dtOpt10081 = null; _dtOpt10081 = new DataTable(); }

            _dtOpt10081 = ds.Tables[0].Copy();

            if (_arrPrice != null)
            {
                _arrPrice = null;
            }

            _arrPrice = _dtOpt10081.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("NOW_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();

            ds.Reset();
        }
        private void GetPaCorrData()
        {
            InitCorrDt();

            Parallel.For((int)AnalysisSt.Common.Class.clsDicDefine.DicParamIndex.Gain, (int)AnalysisSt.Common.Class.clsDicDefine.DicParamIndex.IoFore, (int i) =>
                {
                    double[] _array = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>
                                                (AnalysisSt.Common.Class.clsDicDefine.GetJuchePriceName((AnalysisSt.Common.Class.clsDicDefine.DicParamIndex)i)), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
                    DataRow newDr = _corrDt.NewRow();

                    newDr["주체"] = AnalysisSt.Common.Class.clsDicDefine.GetJucheName((AnalysisSt.Common.Class.clsDicDefine.DicParamIndex)i);
                    newDr["상관계수"] = oTechStats.Tech_Correlation(_arrPrice, _array);
                    _corrDt.Rows.Add(newDr);
                });

        }
        private void GetCorrData()
        {
            
            //double[] arrGainPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("GAIN_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrForePrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("FORE_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrGiganPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("GIGAN_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrGumyPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("GUMY_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrBohumPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("BOHUM_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrTosinPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("TOSIN_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrGitaPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("GITA_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrBankPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("BANK_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrYeongiPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("YEONGI_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrSamoPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("SAMO_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrNationPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("NATION_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrBubinPrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("BUBIN_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            //double[] arrIoForePrice = _dt10059Scare.AsEnumerable().Select(row => Convert.ToDouble(row.Field<Decimal>("IOFORE_PRICE"), System.Globalization.CultureInfo.InvariantCulture)).ToArray();

            //DataRow newDr = _corrDt.NewRow();

            //newDr["주체"] = Common.Class.clsDicDefine.GAIN;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrGainPrice);

            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.FORE;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrForePrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.GIGAN;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrGiganPrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.GUMY;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrGumyPrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.BOHUM;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrBohumPrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.TOSIN;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrTosinPrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.GITA;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrGitaPrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.BANK;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrBankPrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.YEONGI;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrYeongiPrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.SAMO;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrSamoPrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.NATION;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrNationPrice);
            //_corrDt.Rows.Add(newDr);
            //newDr = null;
            //newDr = _corrDt.NewRow();
            //newDr["주체"] = Common.Class.clsDicDefine.BUBIN;
            //newDr["상관계수"] = oTechStats.Tech_Correlation(arrPrice, arrBubinPrice);
            //_corrDt.Rows.Add(newDr);
        }

    }
}
