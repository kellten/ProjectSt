using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Woom.DataAccess.OptCaller.InterFace;
using Woom.DataDefine.OptData;
using Woom.DataAccess.PlugIn;

namespace Woom.DataAccess.OptCaller.Class
{
    public class ClsOpt10060_New : IOptCaller
    {
                private const string ConScreenNoFooter = "60";
        public string ScreenNoFooter { get { return ConScreenNoFooter; } }
        private string _screenNo = "";

        #region Event

        public delegate void OnReceivedEventHandler(string stockCode, DataTable dt, int sPreNext);
        public event OnReceivedEventHandler Opt10060_OnReceived;

        #endregion Event

        #region Enum

        private enum Column10060Index
        {
            일자, 현재가, 전일대비, 누적거래대금, 개인투자자, 외국인투자자, 기관계, 금융투자, 보험, 투신, 기타금융, 은행,
            연기금등, 사모펀드, 국가, 기타법인, 내외국인
        }

        #endregion Enum

        #region Const

        private const string RqName = "종목별투자자기관별차트요청_1";
        private const string OptName = "Opt10060";

        #endregion Const

        #region 전역변수

        private DataTable _dt = new DataTable();

        private string _startDate = "";
        private string _stockCode = "";
        private string _stockName = "";
        private string _amountQtyGb = "";
        private string _maeMaeGb = "";
        private string _unitGb = "";

        //private object lockObject = new object();

        #endregion 전역변수

        /// <summary>
        /// 화면번호 세팅 
        /// </summary>
        /// <param name="FormId"></param>
        public void SetInit(string FormId)
        {
            _screenNo = FormId + ConScreenNoFooter;
        }

        
        public void MakeDataTable()
        {
            if (_dt != null)
            {
                _dt = null;
                _dt = new DataTable();
            }

            using (ClsColumnSets oBasicDataType = new ClsColumnSets())
            {
                foreach (int i in Enum.GetValues(typeof(Column10060Index)))
                {
                    int j = 0;
                    j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(Column10060Index), i));

                    if (_dt.Columns.Contains(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j).ToString()) != true)
                    {
                        _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
                    }
                }
            };
        }

        public void Opt10060(bool nextCall = false)
        {
            ArrayList SetInputValue = new ArrayList();

            SetInputValue.Add(_startDate);
            SetInputValue.Add(_stockCode);
            SetInputValue.Add(_amountQtyGb);
            SetInputValue.Add(_maeMaeGb);
            SetInputValue.Add(_unitGb);

            string type = "";

            if (_amountQtyGb == "1")
            {
                if (_maeMaeGb == "1")
                { type = "1"; }
                else
                { type = "2"; }
            }
            else
            {
                if (_maeMaeGb == "1")
                { type = "3"; }
                else
                { type = "4"; }

            }

            if (nextCall == false)
            {
                JustRequest(SetInputValue, type + "," + _stockCode);
            }
            else
            {
                ReJustRequest(SetInputValue, type + "," + _stockCode);
            }
        }

        private void JustRequest(ArrayList arrayL, string type)
        {
            //TaskCompletionSource<bool> tcs = null;
            //tcs = new TaskCompletionSource<bool>();

            //AxKH.OnReceiveTrData += (object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) =>
            //{
            //    if (tcs == null || tcs.Task.IsCompleted)
            //    { return; }
            //    AxKH_OnReceiveTrData(sender, e);
            //    tcs.SetResult(true);
            //};

            //// AxKH.CommRqData(RqName, OptName, 0, _screenNo);

            ClsAxKH.AxKH.OptCommRqData(OptType.Opt10060, arrayL, RqName + "," + type, OptName, 0, _screenNo);
            //await tcs.Task;
            //AxKH.OnReceiveTrData -= AxKH_OnReceiveTrData;

        }
        private void ReJustRequest(ArrayList arrayL, string type)
        {

            ClsAxKH.AxKH.OptCommRqData(OptType.Opt10060, arrayL, RqName + "_" + type, OptName, 2, _screenNo);
        }

        private void AxKH_OnReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            if (e.sScrNo != _screenNo || e.sRQName != RqName)
            {
                return;
            }

            //Task<string> t = Task.Run(() =>
            //{
            //    MakeDataTable();

            //    var handler = Opt10060_OnReceived;

            //    int nCnt = AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            //    if (nCnt == 0)
            //    {
            //        if (handler != null)
            //        {
            //            // _OptStatus.InitOptCallingStatus();
            //            Opt10060_OnReceived(_stockCode, null, 0);
            //        }
            //    }

            //    for (int i = 0; i < nCnt; i++)
            //    {
            //        DataRow dr = _dt.NewRow();
            //        for (int intColumName = 0; intColumName < _dt.Columns.Count; intColumName++)
            //        {
            //            var type = _dt.Columns[intColumName].DataType;
            //            dr[_dt.Columns[intColumName].ColumnName.ToString()] = Convert.ChangeType(AxKH.GetCommData(e.sTrCode, e.sRQName, i, _dt.Columns[intColumName].ColumnName.ToString()).ToString().Trim(), type);
            //        }

            //        _dt.Rows.Add(dr);
            //    }

            //    if (handler != null)
            //    {
            //        if (Convert.ToInt32(e.sPrevNext) != 2)
            //        {
            //            //  _OptStatus.InitOptCallingStatus();
            //        }
            //        Opt10060_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
            //    }

            //    return e.sPrevNext;
            //});

            //Task cwt = t.ContinueWith(task =>
            //{
            //    if (t.Result != "2")
            //    {
            //        //_OptStatus.InitOptCallingStatus();
            //    }

            //    Opt10060_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
            //});

            MakeDataTable();

            var handler = Opt10060_OnReceived;

            int nCnt = ClsAxKH.AxKH.GetRepeatCnt(e.sTrCode, e.sRQName);

            if (nCnt == 0)
            {
                if (handler != null)
                {
                    //_OptStatus.InitOptCallingStatus();
                    Opt10060_OnReceived(_stockCode, null, 0);
                }
            }

            for (int i = 0; i < nCnt; i++)
            {
                DataRow dr = _dt.NewRow();
                for (int intColumName = 0; intColumName < _dt.Columns.Count; intColumName++)
                {
                    var type = _dt.Columns[intColumName].DataType;
                    dr[_dt.Columns[intColumName].ColumnName.ToString()] = Convert.ChangeType(AxKH.GetCommData(e.sTrCode, e.sRQName, i, _dt.Columns[intColumName].ColumnName.ToString()).ToString().Trim(), type);
                }

                _dt.Rows.Add(dr);
            }

            if (handler != null)
            {
                if (Convert.ToInt32(e.sPrevNext) != 2)
                {
                    //_OptStatus.InitOptCallingStatus();
                }
                Opt10060_OnReceived(_stockCode, _dt, Convert.ToInt32(e.sPrevNext));
            }
        }

    }
}
