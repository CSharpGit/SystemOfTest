using System;
using System.Windows;
using System.Windows.Threading;
namespace SystemOfTest
{
    /// <summary>
    /// Info.xaml 的交互逻辑
    /// </summary>
    public partial class Info : Window
    {
        private Information Infor = new Information();
        private DispatcherTimer timer;
        private ProcessCount processCount;
        public Info()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(StartTest_Loaded);
        }

        private event StartTestHandler startTest;

        public delegate bool StartTestHandler();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            Information INFor = new SystemOfTest.Information();
            INFor.Class = StaticInform.Class;
            INFor.Name = StaticInform.Name;
            INFor.Num = StaticInform.Num;
            textClass.DataContext = INFor;
            textName.DataContext = INFor;
            textNum.DataContext = INFor;
        }

        private void StartTest_Loaded(object sender, RoutedEventArgs e)
        {
            //设置定时器 
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(10000000); //时间间隔为一秒 
            timer.Tick += new EventHandler(timer_Tick);
            //转换成秒数 
            Int32 hour = Convert.ToInt32(HourArea.Text);
            Int32 minute = Convert.ToInt32(MinuteArea.Text);
            Int32 second = Convert.ToInt32(SecondArea.Text);
            //处理倒计时的类 
            processCount = new ProcessCount(hour * 3600 + minute * 60 + second);
            startTest += new StartTestHandler(processCount.ProcessStartTest);
            //开启定时器 
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (OnStartTest())
            {
                HourArea.Text = processCount.GetHour();
                MinuteArea.Text = processCount.GetMinute();
                SecondArea.Text = processCount.GetSecond();
            }
            else
                timer.Stop();
        }

        public bool OnStartTest()
        {
            if (startTest != null)
                return startTest();
            return false;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {          
            if (System.Windows.MessageBox.Show("确定交卷?",
                                                 "交卷",
                                                  MessageBoxButton.YesNo,
                                                  MessageBoxImage.Question,
                                                  MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                this.Close();
                SubmitOperation so = new SubmitOperation();
                so.Show();            
            }         
        }
    }
}
