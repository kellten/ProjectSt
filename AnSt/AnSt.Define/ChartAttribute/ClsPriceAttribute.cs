using AnSt.Define.Attribute;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnSt.Define.ChartAttribute
{
    public class ClsPriceAttribute : INotifyPropertyChanged
    {
        #region 멤버변수
        public ClsStockAttribute clsStockAttribute;
        private string _fromDate;
        public string FromDate { get { return _fromDate; } set { _fromDate = value; } }
        private string _toDate;
        public string ToDate { get { return _toDate; } set { _toDate = value; } }
        #endregion

        #region 이벤트
        public event PricePropertyChangedHandler PricePropertyChanged;
        public delegate void PricePropertyChangedHandler(object sender, PropertyChangedEventArgs e);
        #endregion

        public ClsPriceAttribute()
        {
            clsStockAttribute = new ClsStockAttribute();
            clsStockAttribute.PropertyChanged += PropertyChanged;
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged<string>("StockCode");
        }

        protected void OnPropertyChanged<T>([CallerMemberName] string caller = null)
        {
            // make sure only to call this if the value actually changes

            var handler = PricePropertyChanged;
            if (handler != null)
            {
                this.PricePropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        #region INotifyPropertyChanged 명시적 구현
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
        #endregion
    }
}
