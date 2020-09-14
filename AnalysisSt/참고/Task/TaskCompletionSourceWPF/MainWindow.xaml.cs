using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskCompletionSourceWPF
{
    /// <summary>
    /// 시작 이후 종료 할 때까지의 대기 시간을 TextBlock에 표시 해준다.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 대리자 객체  선언
        /// </summary>
        private TaskCompletionSource<DateTime> stopTimeTCS = null;

        /// <summary>
        /// 생성자
        /// </summary>
        public MainWindow()
        {
            // 기본 초기화 함수
            InitializeComponent();
        }

        /// <summary>
        /// 대기 시작을 수행하여 초기화를 하고 완료될 때까지 표시를 대기한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnWaiting_Click(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;
            obj.IsEnabled = false;

            txtMessage.Text = "대기 중...";

            var waitingTime = DateTime.Now;
            /// 대리자 객체 초기화 - DateTime를 처리 하도록 선언
            stopTimeTCS = new TaskCompletionSource<DateTime>();
            var stopTime = await stopTimeTCS.Task;

            txtMessage.Text = string.Format("대기 시작 시간 : {0}\r\n종료 시간 : {1}\r\n대기 : {2}",
                waitingTime.ToString(),
                stopTime.ToString(), 
                (stopTime - waitingTime).TotalMilliseconds.ToString());

            obj.IsEnabled = true;
        }

        /// <summary>
        /// 대기 종료를 수행하여 대기중인 객체가 반환되도록 값을 할당한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            // Business logic

            // 대기 객체에 값을 할당하여 대기를 종료 하도록 한다.
            stopTimeTCS.SetResult(DateTime.Now);
        }
    }
}
