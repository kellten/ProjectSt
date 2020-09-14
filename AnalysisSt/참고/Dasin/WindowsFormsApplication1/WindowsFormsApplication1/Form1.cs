using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Collections;


//사이보스 관련 모듈
using CPSYSDIBLib;
using DSCBO1Lib;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            OPEN = new List<float>();
            HIGH = new List<float>();
            LOW = new List<float>();
            CLOSE = new List<float>();
            TIME = new List<string>();

            Tick_Time = new List<string>();
            Tick_Price = new List<float>();

            //선물 히스토리 데이터 수신
            FutOpt = new FutOptChartClass();
            FutOpt.Received += new _ISysDibEvents_ReceivedEventHandler(FutOpt_OnReceived);
            //선물 실시간 데이터 수신
            FuCurOnly = new FutureCurOnlyClass();
            FuCurOnly.Received += new _IDibEvents_ReceivedEventHandler(FuCurOnly_OnReceived);

            //옵션 실시간 데이터 수신
            OptCurOnly = new OptionCurOnlyClass();
            OptCurOnly.Received +=new _ISysDibEvents_ReceivedEventHandler(OptCurOnly_Received);

        }

        // 틱데이터를 담는 배열
        List<string> Tick_Time;
        List<float> Tick_Price;


        int CurrentDrumTime;
        int LastDrumTime = 900;

        // 개장시간
        int MARKET_OPEN_HH = 9;
        int MARKET_OPEN_MM = 0;
        int MARKET_OPEN_SS = 0;
        // 선택된 데이터 타임프레임
        char TimeFrame;
        // 타임프레임의 카운트
        int TimeCount;

        // 데이터를 저장할 배열의 참조순번
        int ArrayIndex;

        //현재 참조중인 틱데이터배열의 순번(for 문)
        int DataIndex = 1;


        //시장 가격 데이터 저장 배열
        List<float> OPEN, HIGH, LOW, CLOSE;
        List<string> TIME;





        //선물옵션차트의 요청필드(항목)
        enum FutOptChart_Request_Field { 날짜 = 0, 시간 = 1, 시가 = 2, 고가 = 3, 저가 = 4, 종가 = 5, 전일대비 = 6, 거래량 = 8, 거래대금 = 9, 누적체결매도수량 = 10, 누적체결매수수량 = 11, 기관순매수 = 20, 미결제약정 = 27, 선물이론가 = 28, 베이시스 = 29, 옵션이론가 = 30, IV = 31, DELTA = 32, THETA = 33, VAGA = 34, RHO = 35 };
        //선물옵션차트의 수신필드(항목)
        enum FutOptChart_Received_Field { 종목코드 = 0, 필드개수 = 1, 필드명 = 2, 수신개수 = 3, 마지막봉틱수 = 4, 최근거래일 = 5, 전일종가 = 6, 현재가 = 7, 대비 = 8, 거래량 = 9, 매도호가 = 10, 매수호가 = 11, 시가 = 12, 고가 = 13, 저가 = 14, 거래대금 = 15, BASIS = 16, 미결제약정 = 17, KOSPI200 = 18, 전일거래량 = 19, 최근갱신시간 = 20 };

        // 데이터 저장 배열
        struct Current_Data
        {
            public ushort LastTickCount;        // 마지막 봉틱수
            public long LastUpdateTime;         // 최근갱신시간
            public List<string> Date;           // 데이터수신날짜
            public List<string> Time;           // 데이터 수신 시간
            public List<double> Open;           // 시가
            public List<double> High;           // 고가
            public List<double> Low;            // 저가
            public List<double> Close;          // 종가
            public List<int> Volume;            // 거래량
        }

        FutOptChartClass FutOpt;                // 선물.옵션차트용 히스토리 데이터 수신 클래스
        FutureCurOnlyClass FuCurOnly;           //선물의 실시간 시세 수신 클래스
        Current_Data NewData;                   //데이터 저장배열


        OptionCurOnlyClass OptCurOnly;          //옵션 실시간 시세 수신 클래스




        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader Rd = new StreamReader (Application.StartupPath +  "\\선물틱데이터.txt");

            string sLine = "";
            int Index = 1;

            while (sLine !=null)
            {
                sLine = Rd.ReadLine();
                if (sLine == null)
                { return; }
                object [] str = sLine.Split('\t');

                if (sLine != null && str.Length > 0)
                {
                    string str1 = str[0].ToString() + ":" + Index.ToString("0000");
                    Tick_Time.Add(str1);
                    Tick_Price.Add(Convert.ToSingle(str[1]));
                    //System.Console.WriteLine(str1);
                    Index++;
                }
                
                if (Index > 999)
                { Index = 1; }

            }

        }


        float Open, High, Low, Close;
        void CompressData(int TimeFrame)
        {
            Open = 0;
            High = -999999999;
            Low = 999999999;
            Close = 0;

            OPEN.Clear();
            HIGH.Clear();
            LOW.Clear();
            CLOSE.Clear();

            ActiveGridView.Rows.Clear();
            DataIndex = 1;
            //이전 배열 참조순번
            int BeforeIndex = -1;
            for (int i = 0; i < Tick_Time.Count; i++ )
            {
                int DataTime = ConvertToTimeFrame(Tick_Time[i], ':');
                // 데이터를 저장할 배열의 참조순번
                ArrayIndex = DataTime / TimeCount;
                if (BeforeIndex == -1) { BeforeIndex = ArrayIndex; }


                //체결가격 데이터
                float value = (float)Tick_Price[i];
                if (ArrayIndex > BeforeIndex)
                {
                    OPEN.Add(Open);
                    HIGH.Add(High);
                    LOW.Add(Low);
                    CLOSE.Add(Close);
                    TIME.Add(Tick_Time[i].ToString());

                    int k = ActiveGridView.Rows.Count;
                    ActiveGridView.Rows.Add();
                    ActiveGridView.Rows[k].SetValues(ConvertToTime(/*DataTime*/ ArrayIndex * TimeCount), Open, High, Low, Close);

                    //System.Console.WriteLine(string.Format("{4}분봉 시가:{0}, 고가{1}, 저가:{2}, 종가:{3}", Open, High, Low, Close, MMToTime(MM, MARKET_OPEN_TIME)));

                    // 시,종가 저장
                    Open = value;
                    High = value;
                    Low = value;
                    Close = value;
                }
                else
                {
                    if (Open == 0) { Open = value; }
                    // 고가와 저가 저장
                    if (value >= High) { High = value; }
                    if (value <= Low) { Low = value; }
                    Close = value;
                }
                //현재 배열참조 순번을 저장
                BeforeIndex = ArrayIndex;
                DataIndex++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //시장 타임프레임
            if (radioButton1.Checked == true)
            { TimeFrame = 'Y'; }
            if (radioButton2.Checked == true)
            { TimeFrame = 'M'; }
            if (radioButton3.Checked == true)
            { TimeFrame = 'W'; }
            if (radioButton4.Checked == true)
            { TimeFrame = 'D'; }
            if (radioButton5.Checked == true)
            { TimeFrame = 'm'; }
            if (radioButton6.Checked == true)
            { TimeFrame = 'S'; }
            if (radioButton7.Checked == true)
            { TimeFrame = 'T'; }

            try
            {
                TimeCount = Convert.ToInt32(textBox1.Text);
                CompressData(Convert.ToInt32(textBox1.Text));
            }
            catch (FormatException )
            {
                MessageBox.Show("입력오류입니다!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //시간을 받아 분을 반환한다.
        public int TimeToMinute(string TimeStr, char Seperator, int BaseHour)
        {
            string [] str = TimeStr.Split(Seperator);
            return (Convert.ToInt32(str[0]) - BaseHour) * 60 + Convert.ToInt32(str[1]);
        }

        //시간을 받아 초을 반환한다.
        public int TimeToSecond(string TimeStr, char Seperator, int BaseHour, int BaseMinute)
        {
            string[] str = TimeStr.Split(Seperator);
            return (Convert.ToInt32(str[0]) - BaseHour) * 60 + (Convert.ToInt32(str[1]) - BaseMinute) * 60;
        }

        //시간을 받아 현재 타임프레임으로을 변환한 후 반환한다.
        public int ConvertToTimeFrame(string TimeStr, char Seperator)
        {
            string[] str = TimeStr.Split(Seperator);
            int Value = 0;

            //년,월,주,일,분,초 구분
            if (TimeFrame == 'Y')
            { Value = 0; }
            else if (TimeFrame == 'M')
            { Value = 0; }
            else if (TimeFrame == 'D')
            { Value = 0; }
            else if (TimeFrame == 'm')
            { Value = (Convert.ToInt32(str[0]) - MARKET_OPEN_HH) * 60 + Convert.ToInt32(str[1]); }
            else if (TimeFrame == 'S')
            { Value = (Convert.ToInt32(str[0]) - MARKET_OPEN_HH) * 3600 + (Convert.ToInt32(str[1]) - MARKET_OPEN_MM) * 60 + Convert.ToInt32(str[2]); }
            else if (TimeFrame == 'T')
            { Value = DataIndex; }

            return Value;

        }

        //분을 시간으로 돌려준다.
        public string MMToTime(int Minute, int BaseTime)
        {
            return string.Format("{0}:{1}", (Minute / 60 + BaseTime).ToString("00"), (Minute % 60).ToString("00"));
        }

        //초을 시간으로 돌려준다.
        public string SSToTime(int Second, int BaseHour, int BaseMinute)
        {
            int HH = Second / 3600;
            int MM = (Second % 3600) / 60;
            int SS = (Second % 3600) % 60;

            return string.Format("{0}:{1}:{2}", HH.ToString("00"), MM.ToString("00"), SS.ToString("00"));
        }

        //현재 타임프레임을 시간으로 변환하여 반환한다.
        public string ConvertToTime(int MarketTimeFrame)
        {
            string Time = "";

            //년,월,주,일,분,초 구분
            if (TimeFrame == 'Y')
            { Time = ""; }
            else if (TimeFrame == 'M')
            { Time = ""; }
            else if (TimeFrame == 'D')
            { Time = ""; }
            else if (TimeFrame == 'm')
            { Time = string.Format("{0}:{1}", (MarketTimeFrame / 60 + MARKET_OPEN_HH).ToString("00"), (MarketTimeFrame % 60).ToString("00")); }
            else if (TimeFrame == 'S')
            {
                Time = string.Format("{0}:{1}:{2}", ((MarketTimeFrame / 3600) + MARKET_OPEN_HH).ToString("00"),
                    ((MarketTimeFrame % 3600) / 60).ToString("00"), ((MarketTimeFrame % 3600) % 60).ToString("00"));
            }
            else if (TimeFrame == 'T')
            {   
                Time = Tick_Time[DataIndex].ToString();
            }

            return Time;
        }













        //선물 실시간 데이터 요청
        public void GetRealTimeFuture(string SymbolCode)
        {
            FuCurOnly.Unsubscribe();
            FuCurOnly.SetInputValue(0, SymbolCode);
            FuCurOnly.SubscribeLatest();
        }


        //옵션 실시간 데이터 요청
        public void GetRealTimeOption(string SymbolCode)
        {
            OptCurOnly.Unsubscribe();
            OptCurOnly.SetInputValue(0, SymbolCode);
            OptCurOnly.SubscribeLatest();
        }




        public void FutOpt_OnReceived()
        {


        }

        public void FuCurOnly_OnReceived()
        {
            string Msg = string.Format("  Price {4}  Time: {0}:{1}:{2}:{3}", DateTime.Now.Hour.ToString("00"), DateTime.Now.Minute.ToString("00"),
                         DateTime.Now.Second.ToString("00"), DateTime.Now.Millisecond.ToString("0000"), FuCurOnly.GetHeaderValue(1).ToString());

            System.Console.WriteLine(Msg);
        }

        public void OptCurOnly_Received()
        {
            string Msg = string.Format("  Time: {0}:{1}:{2}:{3}", DateTime.Now.Hour.ToString("00"), DateTime.Now.Minute.ToString("00"),
                         DateTime.Now.Second.ToString("00"), DateTime.Now.Millisecond.ToString("0000"));

            System.Console.WriteLine(Msg);
        }






        private void button2_Click(object sender, EventArgs e)
        {
            //GetRealTimeFuture("101G3");
            GetRealTimeOption("*");
        }














    }
}
