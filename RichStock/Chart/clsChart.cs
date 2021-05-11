﻿using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PaikRichStock.Common;

namespace Chart
{
    public class clsChart
    {
        public class ResultDtEventArgs : EventArgs
        {
            public System.Data.DataSet Data { get; set; }
            public ResultDtEventArgs(System.Data.DataSet data)
            {
                Data = data;
            }          
        }

        //public event EventHandler<ResultDtEventArgs> OnEventReturn10081ResultDt;
        //public event EventHandler<ResultDtEventArgs> OnEventReturnRealTime;

        public event OnEvent10081ResultDtEventHandler OnEventReturn10081ResultDt;
        public event OnEventReturnRealTimeEventHandler OnEventReturnRealTime;
                
        public delegate void OnEvent10081ResultDtEventHandler(DataSet ds);
        public delegate void OnEventReturnRealTimeEventHandler(DataSet ds);

        private PaikRichStock.Common.ucMainStockVer2 _MainStock;

        private System.Data.DataTable _dtOpt10081 = new System.Data.DataTable();
        private string _stockCode_Opt10081 = "";

        private string _lastDate = "";

        public  PaikRichStock.Common.ucMainStockVer2 MainStock
        {
            set
            {
                _MainStock = value;
                
                _MainStock.OnReceiveTrData_opt10081New += new PaikRichStock.Common.ucMainStockVer2.OnReceiveTrData_opt10081NewEventHandler(OnReceiveTrData_opt10081);
                _MainStock.OnReceiveRealData_Volume +=  new PaikRichStock.Common.ucMainStockVer2.OnReceiveRealData_VolumeEventHandler(OnReceiveRealData_Volume);
            }
        }
        
        public string LastDate
        {
            set
            {
                _lastDate = value;
            }
        }
        
        
        #region CallOpt10081Day(주식일봉차트조회요청)
        
        #endregion

        public void GetOpt10081(string stockCode)
        {
            if (stockCode == "")
            {
                return;
            }

            string stdDate = "";

            //_MainStock.OnReceiveTrData_opt10081 += new PaikRichStock.Common.ucMainStockVer2.OnReceiveTrData_opt10081EventHandler(OnReceiveTrData_opt10081);

            stdDate = PaikRichStock.Common.CDateTime.FormatDate(DateTime.Now.Date.ToString("yyyyMMdd"), "");

            _stockCode_Opt10081 = stockCode;

            CallOpt10081(_stockCode_Opt10081, stdDate);

            //_MainStock.OptKWFid_OnReceiveRealData(stockCode, 1);


        }

        private void CallOpt10081(string stockCode, string stdDate)
        {
            _MainStock.Opt10081New_OnReceiveTrData(stockCode, _MainStock.GetStockInfo(stockCode), stdDate);
        }

        void OnReceiveRealData_Volume(DataSet ds)
        {            
            if (ds == null) return;
            if (ds.Tables.Count < 1) return;
            if (ds.Tables[0].Rows.Count < 1) return;

            if(_stockCode_Opt10081 == ds.Tables[0].Rows[0]["STOCK_CODE"].ToString())
            {
                var handler = OnEventReturnRealTime;
                if (handler != null)
                {
                    handler(ds);                    
                }
            }
            

        }

        private void OnReceiveTrData_opt10081(System.Data.DataSet ds)
        {
            if(ds==null)
            {
                //var handler = OnEventReturn10081ResultDt;
                //if (handler != null)
                //{
                //    handler(ds);                    
                //}
            }
            
            if(ds.Tables[0].Rows.Count < 1)
            {
                //var handler = OnEventReturn10081ResultDt;
                //if (handler != null)
                //{
                //    handler(ds);                    
                //}
            }

            var handler1 = OnEventReturn10081ResultDt;
            if (handler1 != null)
            {
                handler1(ds);                   
            }
            
        }

    }
}