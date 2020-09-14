using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnSt.Define.Attribute
{
    public class ClsStockAttribute : INotifyPropertyChanged

    {
        public ClsStockAttribute()
        {
            _stockCode = "";
            _stockName = "";
        }
        public event PropertyChangedHandler PropertyChanged;
        public delegate void PropertyChangedHandler(object sender, PropertyChangedEventArgs e);
        private string _stockCode = "";
        private string _stockName = "";

        public string StockCode { get { return _stockCode; } set { _stockCode = value; OnPropertyChanged<string>("StockCode"); } }
        public string StockName { get { return _stockName; } set { _stockName = value; } }

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
