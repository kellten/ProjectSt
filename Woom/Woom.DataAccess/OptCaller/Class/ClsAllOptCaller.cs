using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woom.DataAccess.OptCaller.InterFace;
using Woom.DataDefine.OptData;
using static Woom.DataAccess.PlugIn.ClsAxKH;

namespace Woom.DataAccess.OptCaller.Class
{
    class ClsAllOptCaller : IDisposable
    {

        private DataTable _dt = new DataTable();

     

        #region IDispose 구현
        private void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            //_OptStatus.InitOptCallingStatus();
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        private async Task JustRequest()
        {
            TaskCompletionSource<bool> tcs = null;
            tcs = new TaskCompletionSource<bool>();

            //AxKH.OnReceiveTrData += (object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) =>
            //{
            //    if (tcs == null || tcs.Task.IsCompleted)
            //    { return; }
            //    AxKH_OnReceiveTrData(sender, e);
            //    tcs.SetResult(true);
            //};

            //// AxKH.CommRqData(RqName, OptName, 0, _screenNo);
            //await OptCommRqData(RqName, OptName, 0, _screenNo);

            //await tcs.Task;
        }

        private async Task ReJustRequest()
        {
            // await Task.Delay(TimeSpan.FromSeconds(4));

            //TaskCompletionSource<bool> tcs = null;
            //tcs = new TaskCompletionSource<bool>();

            //AxKH.OnReceiveTrData += (object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e) =>
            //{
            //    if (tcs == null || tcs.Task.IsCompleted)
            //    { return; }
            //    AxKH_OnReceiveTrData(sender, e);
            //    tcs.SetResult(true);
            //};

            //// AxKH.CommRqData(RqName, OptName, 2, _screenNo);
            //await OptCommRqData(RqName, OptName, 2, _screenNo);
            //await tcs.Task;

            //OptCommRqData(RqName, OptName, 2, _screenNo);
        }


        public void MakeDataTable()
        {
            //if (_dt != null)
            //{
            //    _dt = null;
            //    _dt = new DataTable();
            //}

            //using (ClsColumnSets oBasicDataType = new ClsColumnSets())
            //{

            //    foreach (int i in Enum.GetValues(typeof(Column10081Index)))
            //    {
            //        int j = 0;
            //        j = (int)Enum.Parse(typeof(ClsColumnSets.ColumnNameIndex), Enum.GetName(typeof(Column10081Index), i));
            //        // _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)i));
            //        _dt.Columns.Add(oBasicDataType.GetDataColumn((ClsColumnSets.ColumnNameIndex)j));
            //    }
            //};
        }
        #endregion IDispose 구현
    }

}
