using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace hangulClock
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 타이머를 사용하여 실시간으로 시간을 업데이트합니다.
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();

            // 창이 화면 중앙에 위치하도록 설정합니다.
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // 창 이동을 처리하지 않도록 합니다.
            e.Handled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("ko-KR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            DateTime currentTime = DateTime.Now;
            string koreanTime = currentTime.ToString("h시 mm분 tt", culture);

            int hour = currentTime.Hour - 12;
            int minute = currentTime.Minute;

            // 로그 창에 한글 시간을 출력합니다.
            Debug.WriteLine(hour);
            Debug.WriteLine(minute);
            Debug.WriteLine(koreanTime);

            // 시간에 따른 텍스트 색상 변경
            ApplyColorToTextBlocks(hour, Colors.White);

            // 분에 따른 로직 추가
            ApplyMinuteLogic(minute);
        }

        private void ApplyColorToTextBlocks(int hour, Color color)
        {
            int[] targetRows = { (hour <= 5) ? 1 : ((hour <= 8) ? 2 : 3), (hour == 5 || hour == 7 || hour == 8 || hour == 10 || hour == 11 || hour == 12 || hour == 24) ? 1 : ((hour == 6 || hour == 9) ? 2 : 3), 3 };
            int[] targetColumns = { 1, 2, 3, 4, 5, 6 };

            foreach (int targetRow in targetRows)
            {
                foreach (int targetColumn in targetColumns)
                {
                    TextBlock targetTextBlock = grid.FindName($"textBlock_{targetRow}_{targetColumn}") as TextBlock;
                    if (targetTextBlock != null)
                    {
                        targetTextBlock.Foreground = new SolidColorBrush(color);
                    }
                }
            }
        }

        private void ApplyMinuteLogic(int minute)
        {
            // 분에 따른 로직 추가
            // 예시: 10의 배수 분에 따른 로직
            if (minute % 10 == 0)
            {
                // 추가 로직
            }
        }
    }
}
