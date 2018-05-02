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
using System.Windows.Shapes;
using SystemOfTest.FTPFilesUpDownLoad;

namespace SystemOfTest
{
    /// <summary>
    /// DownLoad.xaml 的交互逻辑
    /// </summary>
    public partial class DownLoad : Window
    {
        public DownLoad()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            FTPFilesDownLoadController download = new FTPFilesDownLoadController(false);
            //学生下载答卷，赋值false，批卷人查看试卷，则赋值true
            stackpanel2.Children.Add(download);
        }
    }
}
