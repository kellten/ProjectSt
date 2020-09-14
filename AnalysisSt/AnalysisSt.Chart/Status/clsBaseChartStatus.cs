using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisSt.Chart.Status
{
    public class clsBaseChartStatus
    {
        private string _StockCode;
        private string _StockName;

        private string _FromDate;
        private string _ToDate;           

        private bool _AreaA;
        private bool _Price;
        private bool _Volume;
                         
        private bool _AreaB;
        private bool _Gain; 
        private bool _Fore; 
        private bool _Gigan;
        private bool _Gumy; 
        private bool _Bohum;
        private bool _Tosin;
        private bool _Gita; 
        private bool _Bank;
        private bool _Yeongi;
        private bool _Samo; 
        private bool _Nation;
        private bool _Bubin; 
        private bool _Iofore;
        
        #region Prop
        public string StockCode { get { return _StockCode; } set { _StockCode = value; } }
        public string StockName { get { return _StockName; } set { _StockName = value; } }

        public string FromDate { get { return _FromDate; } set { _FromDate = value; } }
        public string ToDate { get { return _ToDate; } set { _ToDate = value; } }

        public bool AreaA { get { return _AreaA; } set { _AreaA = value; } }
        public bool Price { get { return _Price; } set { _Price = value; } }
        public bool Volume { get { return _Volume; } set { _Volume = value; } }
        
        public bool AreaB { get { return _AreaB; } set { _AreaB = value; } }
        public bool Gain { get { return _Gain; } set { _Gain = value; } }
        public bool Fore { get { return _Fore; } set { _Fore = value; } }
        public bool Gigan { get { return _Gigan; } set { _Gigan = value; } }
        public bool Gumy { get { return _Gumy; } set { _Gumy = value; } }
        public bool Bohum { get { return _Bohum; } set { _Bohum = value; } }
        public bool Tosin { get { return _Tosin; } set { _Tosin = value; } }
        public bool Gita { get { return _Gita; } set { _Gita = value; } }
        public bool Bank { get { return _Bank; } set { _Bank = value; } }
        public bool Yeongi { get { return _Yeongi; } set { _Yeongi = value; } }
        public bool Samo { get { return _Samo; } set { _Samo = value; } }
        public bool Nation { get { return _Nation; } set { _Nation = value; } }
        public bool Bubin { get { return _Bubin; } set { _Bubin = value; } }
        public bool Iofore { get { return _Iofore; } set { _Iofore = value; } }
        #endregion

        #region ChangeStatus
        /// <summary>
        /// 상태값이 바뀌면 Chart에 나타나는 내역을 변경한다.
        /// </summary>
        private void ChangedStatus()
        { 

        }       
        #endregion

    }
}
