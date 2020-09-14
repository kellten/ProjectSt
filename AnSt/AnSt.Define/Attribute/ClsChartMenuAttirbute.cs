using AnSt.Define.ChartAttribute;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AnSt.Define.Attribute
{
    public class ClsChartMenuAttirbute : INotifyPropertyChanged
    {
        #region 이벤트
        public event ChartMenuAttributeChangedHandler ChartMenuAttributeChanged;
        public delegate void ChartMenuAttributeChangedHandler(object sender, PropertyChangedEventArgs e);
        #endregion

        #region Enum
        public enum SeriesIndex { Price, Ma3, Ma5, Ma10, Ma20, Ma42, Ma60, Ma90, Ma120, Ma200, Ma480, Ma1000 }

        #endregion

        #region 멤버변수
        private ClsChartDefineMember clsChartDefineMember = new ClsChartDefineMember();
        private bool _price;

        private bool _Ma3;
        private bool _Ma5;
        private bool _Ma10;
        private bool _Ma20;
        private bool _Ma42;
        private bool _Ma60;
        private bool _Ma90;
        private bool _Ma120;
        private bool _Ma200;
        private bool _Ma480;
        private bool _Ma1000;
        #endregion

        #region CategoryAttribute
        // 가격
        [CategoryAttribute("가격"),
        DefaultValueAttribute("")]
        public bool Price { get { return _price; } set { _price = value; OnChartMenuAttributeChanged<string>("Price"); } }
        // 이동평균
        [CategoryAttribute("이동평균"),
        DefaultValueAttribute("")]
        public bool Ma3 { get { return _Ma3; } set { _Ma3 = value; OnChartMenuAttributeChanged<string>("Ma3"); } }
        [CategoryAttribute("이동평균"),
        DefaultValueAttribute("")]
        public bool Ma5 { get { return _Ma5; } set { _Ma5 = value; OnChartMenuAttributeChanged<string>("Ma5"); } }
        [CategoryAttribute("이동평균"),
        DefaultValueAttribute("")]
        public bool Ma10 { get { return _Ma10; } set { _Ma10 = value; OnChartMenuAttributeChanged<string>("Ma10"); } }
        [CategoryAttribute("이동평균"),
        DefaultValueAttribute("")]
        public bool Ma20 { get { return _Ma20; } set { _Ma20 = value; OnChartMenuAttributeChanged<string>("Ma20"); } }
        [CategoryAttribute("이동평균"),
        DefaultValueAttribute("")]
        public bool Ma42 { get { return _Ma42; } set { _Ma42 = value; OnChartMenuAttributeChanged<string>("Ma42"); } }
        [CategoryAttribute("이동평균"),
        DefaultValueAttribute("")]
        public bool Ma60 { get { return _Ma60; } set { _Ma60 = value; OnChartMenuAttributeChanged<string>("Ma60"); } }
        [CategoryAttribute("이동평균"),
         DefaultValueAttribute("")]
        public bool Ma90 { get { return _Ma90; } set { _Ma90 = value; OnChartMenuAttributeChanged<string>("Ma90"); } }
        [CategoryAttribute("이동평균"),
         DefaultValueAttribute("")]
        public bool Ma120 { get { return _Ma120; } set { _Ma120 = value; OnChartMenuAttributeChanged<string>("Ma120"); } }
        [CategoryAttribute("이동평균"),
         DefaultValueAttribute("")]
        public bool Ma200 { get { return _Ma200; } set { _Ma200 = value; OnChartMenuAttributeChanged<string>("Ma200"); } }
        [CategoryAttribute("이동평균"),
         DefaultValueAttribute("")]
        public bool Ma480 { get { return _Ma480; } set { _Ma480 = value; OnChartMenuAttributeChanged<string>("Ma480"); } }
        [CategoryAttribute("이동평균"),
       DefaultValueAttribute("")]
        public bool Ma1000 { get { return _Ma1000; } set { _Ma1000 = value; OnChartMenuAttributeChanged<string>("Ma1000"); } }
        #endregion

        protected void OnChartMenuAttributeChanged<T>([CallerMemberName] string caller = null)
        {
            // make sure only to call this if the value actually changes

            var handler = ChartMenuAttributeChanged;
            if (handler != null)
            {
                this.ChartMenuAttributeChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        #region INotifyPropertyChanged구현
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