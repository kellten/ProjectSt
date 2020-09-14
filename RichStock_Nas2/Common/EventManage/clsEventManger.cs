using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PaikRichStock.Common;
using System.Windows.Forms;

namespace CSharp.Common.EventManage
{
    public class clsEventManger
    {

        private PaikRichStock.Common.ucMainStockVer2 _MainStockVer2 = new PaikRichStock.Common.ucMainStockVer2();
        private DataSet _ds10059 = new DataSet();
        private DataSet _ds10059Price = new DataSet();
        private DataSet _ds10081 = new DataSet();
        private DataGridView _dgv10059;
        private DataGridView _dgv10059Price;
        private DataGridView _dgv10081New;
        private clsCSharpFunc clsCsfunc = new clsCSharpFunc();

        private bool _JobCom10059 = false;
        private bool _JobCom10059Price = false;
        private bool _JobCom10081New = false;
        
        public DataGridView dgv10059 { set { _dgv10059 = value; } }
        public DataGridView dgv10059Price { set { _dgv10059Price = value; } }
        public DataGridView dgv10081New { set { _dgv10081New = value; } }
        
        public PaikRichStock.Common.ucMainStockVer2 MainStockVer2 {set { _MainStockVer2 = value; }}

        public void MainCombine(string stockCode, string stockName, string stdDate)
        {
            _ds10059 = null;
            _ds10059 = new DataSet();
            _ds10059Price = null;
            _ds10059Price = new DataSet();
            _ds10081 = null;
            _ds10081 = new DataSet();


            DoOpt10059( stockCode,  stockName,  stdDate);
            DoOpt10059Price(stockCode, stockName, stdDate);
            DoOpt10081(stockCode, stockName, stdDate);
        }

        public async void DoOpt10059(string stockCode, string stockName, string stdDate)
        {
            await DoOpt10059Async(stockCode, stockName, stdDate);
        }

        private async Task DoOpt10059Async(string stockCode, string stockName, string stdDate)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            _MainStockVer2.OnReceiveTrData_Opt10059 += (d) =>
                {
                    if (tcs.Task.IsCompleted)
                        return;
                    GetOpt10059Data(stockCode, d);
                    tcs.SetResult(true);
                    System.Threading.Thread.Sleep(300);
                };

            // _MainStockVer2.Opt10059_OnReceiveTrData(DateTime.Now.ToString("yyyyMMdd"), stockCode, stockName, "1", "0", "1");
            _MainStockVer2.Opt10059_OnReceiveTrData(stdDate, stockCode, stockName, "2", "0", "1");
            await tcs.Task;

        }

        public void GetOpt10059Data(String stockCode, DataSet ds)
        {
            if (ds.Tables[0].Rows.Count < 1 )
            {
                _JobCom10059 = true;
                Combine();
            }
            else
            {
                if (_ds10059.Tables.Count < 1)
                {
                    _ds10059 = ds.Copy();
                    // clsCsfunc.DataSetColumnCloneToDataSet(_ds10059, ds);
                }
                else
                { 
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        _ds10059.Tables[0].ImportRow(dr);
                    }
                }
                
                DoOpt10059(stockCode, _MainStockVer2.GetStockInfo(stockCode), clsCsfunc.DayDateAdd( ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["일자"].ToString().Trim(), -1));
            }
        }

        public async void DoOpt10059Price(string stockCode, string stockName, string stdDate)
        {
            await DoOpt10059PriceAsync(stockCode, stockName, stdDate);
        }

        private async Task DoOpt10059PriceAsync(string stockCode, string stockName, string stdDate)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            _MainStockVer2.OnReceiveTrData_Opt10059Price += (d) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                    return;
                GetOpt10059PriceData(stockCode, d);
                tcs.SetResult(true);
                System.Threading.Thread.Sleep(300);
            };

            // _MainStockVer2.Opt10059Price_OnReceiveTrData(DateTime.Now.ToString("yyyyMMdd"), stockCode, stockName, "1", "0", "1");
            _MainStockVer2.Opt10059_OnReceiveTrDataPrice(stdDate, stockCode, stockName, "1", "0", "1");
            await tcs.Task;
            tcs.Task.Dispose();
            tcs = null;
        }

        public void GetOpt10059PriceData(String stockCode, DataSet ds)
        {
            if (ds.Tables[0].Rows.Count < 1)
            {
                _JobCom10059Price = true;
                Combine();
            }
            else
            {
                if (_ds10059Price.Tables.Count < 1)
                {
                    _ds10059Price = ds.Copy();
                    // clsCsfunc.DataSetColumnCloneToDataSet(_ds10059Price, ds);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        _ds10059Price.Tables[0].ImportRow(dr);
                    }
                }


                DoOpt10059Price(stockCode, _MainStockVer2.GetStockInfo(stockCode), clsCsfunc.DayDateAdd(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["일자"].ToString().Trim(), -1));
            }
        }

        public async void DoOpt10081(string stockCode, string stockName, string stdDate)
        {
            await DoOpt10081Async(stockCode, stockName, stdDate);
        }

        private async Task DoOpt10081Async(string stockCode, string stockName, string stdDate)
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            _MainStockVer2.OnReceiveTrData_opt10081New += (d) =>
            {
                if (tcs == null || tcs.Task.IsCompleted)
                    return;
                GetOpt10081Data(stockCode, d);
                tcs.SetResult(true);
                System.Threading.Thread.Sleep(300);
            };

            // _MainStockVer2.Opt10081_OnReceiveTrData(DateTime.Now.ToString("yyyyMMdd"), stockCode, stockName, "1", "0", "1");
            _MainStockVer2.Opt10081New_OnReceiveTrData(stockCode, stockName, stdDate);
            await tcs.Task;
            tcs.Task.Dispose();
            tcs = null;
        }

        public void GetOpt10081Data(String stockCode, DataSet ds)
        {
            if (ds.Tables[0].Rows.Count < 1)
            {
                _JobCom10081New = true;
                Combine();
            }
            else
            {
                if (_ds10081.Tables.Count < 1)
                {
                    _ds10081 = ds.Copy();
                    // clsCsfunc.DataSetColumnCloneToDataSet(_ds10081, ds);
                }
                else
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        _ds10081.Tables[0].ImportRow(dr);
                    }
                }


                DoOpt10081(stockCode, _MainStockVer2.GetStockInfo(stockCode), clsCsfunc.DayDateAdd(ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["일자"].ToString().Trim(), -1));
            }
        }

        public void Combine()
        {
            if (_JobCom10059 == true || _JobCom10059Price == true || _JobCom10081New == true)
            {

                _dgv10059.DataSource = _ds10059.Tables[0];
                _dgv10059Price.DataSource = _ds10059Price.Tables[0];
                _dgv10081New.DataSource = _ds10081.Tables[0];

                _JobCom10059 = false;
                _JobCom10059Price = false;
                _JobCom10081New = false;

            }
            
        }

    }
}
