using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace SystemOfTest
{
    /// <summary>
    /// StudentsLogin.xaml 的交互逻辑
    /// </summary>
    public partial class StudentsLogin : Window
    {
        public StudentsLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show("确定退出?",
                                                 "退出登录",
                                                  MessageBoxButton.YesNo,
                                                  MessageBoxImage.Question,
                                                  MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                this.Close();
                MainWindow MW = new SystemOfTest.MainWindow();
                MW.Show();
                //System.Windows.Application.Current.Shutdown();
            }
            return;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textNum.Text.Trim()==string.Empty)
            {
                MessageBox.Show("请输入学号");
            }
            else
            {
                Information InFor = new SystemOfTest.Information();               
                string cmdText = "select T_StuClass,T_StuName,T_StuNum from T_Students where T_StuNum=@Num";
                SqlParameter parameters = new SqlParameter("@Num", textNum.Text);
                DataSet dataset = new DataSet();
                dataset = SqlHelper.ExcuteDataSet(cmdText, parameters);
                DataTable table = dataset.Tables[0];
                DataRowCollection rows = table.Rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i];
                    InFor.Class = (string)row["T_StuClass"];
                    InFor.Name = (string)row["T_StuName"];
                    InFor.Num = (string)row["T_StuNum"];
                }
                LoGing_Grid.DataContext = InFor;
                //textName.DataContext = InFor;              
                if (Convert.ToString(textNum.Text) != InFor.Num)
                {
                    MessageBox.Show("没有该考生信息，请重新输入！");                 
                }
                else 
                {
                    if (System.Windows.MessageBox.Show("信息核对完毕?",
                                                     "信息核对",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Question,
                                                      MessageBoxResult.Yes) == MessageBoxResult.Yes)
                    {
                        StaticInform.Class = InFor.Class;
                        StaticInform.Name = InFor.Name;
                        StaticInform.Num = InFor.Num;
                        btCheck.Visibility = Visibility.Collapsed;
                        btLoging.Visibility = Visibility.Visible;
                        wp1.Visibility = Visibility.Visible;
                        wp2.Visibility = Visibility.Visible;
                        btLoging.Focus();
                    }
                }
            }
            return;
        }

        private void btLoging_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            StartTest ST = new SystemOfTest.StartTest();
            ST.Show();
            Info info = new Info();
            info.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btLoging.Visibility = Visibility.Collapsed;//将控件隐藏
            textNum.Focus();
        }

        private void textNum_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                button_Click(sender,e);
            }
        }
    }
}
