using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnSt.Singleton.ChaPro
{
    public class ClsPassingViewContAtt : INotifyPropertyChanged
    {
        private static ClsPassingViewContAtt _instance = null;
        private static readonly object padlock = new object();

        public event ViewContAttPropertyChangedHandler ViewContAttPropertyChanged;
        public delegate void ViewContAttPropertyChangedHandler(object sender, PropertyChangedEventArgs e);

        private object _oControl;
        private string _FormName;

        public string FormName { get { return _FormName; } set { _FormName = value; } }
        public object OControl { get { return _oControl; } set { _oControl = value; OnPropertyChanged<string>(_oControl.ToString()); } }

        protected ClsPassingViewContAtt() { }

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

        public static ClsPassingViewContAtt Instance()
        {
            if (_instance == null)
            {
                lock (padlock)
                {
                    _instance = new ClsPassingViewContAtt();
                }
            }

            return _instance;

        }

        protected void OnPropertyChanged<T>([CallerMemberName] string caller = null)
        {
            // make sure only to call this if the value actually changes

            var handler = ViewContAttPropertyChanged;
            if (handler != null)
            {
                this.ViewContAttPropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

    }
}