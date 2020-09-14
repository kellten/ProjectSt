using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnSt.Singleton.ChaPro
{
    public class ClsPassingStockCode : INotifyPropertyChanged
    {
        private static ClsPassingStockCode _instance = null;
        private static readonly object padlock = new object();

        public event PropertyChangedHandler PropertyChanged;
        public delegate void PropertyChangedHandler(object sender, PropertyChangedEventArgs e);

        private string _stockCode = "";
        private string _stockName = "";

        public string StockCode { get { return _stockCode; } set { _stockCode = value; OnPropertyChanged<string>(StockCode); } }
        public string StockName { get { return _stockName; } set { _stockName = value; } }

        public static ClsPassingStockCode Instance()
        {
            if (_instance == null)
            {
                lock (padlock)
                {
                    _instance = new ClsPassingStockCode();
                }
            }

            return _instance;

        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        protected void OnPropertyChanged<T>([CallerMemberName] string caller = null)
        {
            // make sure only to call this if the value actually changes

            var handler = PropertyChanged;
            if (handler != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
