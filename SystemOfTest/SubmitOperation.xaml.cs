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
    /// SubmitOperation.xaml 的交互逻辑
    /// </summary>
    public partial class SubmitOperation : Window
    {
        public SubmitOperation()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            System.Windows.Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FTPFilesUpLoadController upload = new FTPFilesUpLoadController(false);
            stackpanel1.Children.Add(upload);
        }
    }
}
