using System;
using System.Collections.Generic;
using System.IO;
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

namespace SystemOfTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void  button_Click(object sender, RoutedEventArgs e)
        {
                if (System.Windows.MessageBox.Show("确定退出?",
                                                 "退出登录",
                                                  MessageBoxButton.YesNo,
                                                  MessageBoxImage.Question,
                                                  MessageBoxResult.No) == MessageBoxResult.Yes)
                {

                    System.Windows.Application.Current.Shutdown();
                }
                return;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (rbStu.IsChecked == true)
            {
                this.Hide();
                StudentsLogin SL = new SystemOfTest.StudentsLogin();
                SL.Show();
            }
            if (rbTeach.IsChecked == true)
            {
                this.Hide();
                TeachersLoging TL = new SystemOfTest.TeachersLoging();
                TL.Show();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {         
            if (!SqlHelper.OpenConnection())
            {
                MessageBox.Show("未连接到服务器，请重试！");
                System.Windows.Application.Current.Shutdown();
            }
            if (!Directory.Exists(StaticInform.path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(StaticInform.path);
                directoryInfo.Create();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button_Copy_Click(sender, e);
            }
        }
    }
}
