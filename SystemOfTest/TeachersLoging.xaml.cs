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
    /// TeachersLoging.xaml 的交互逻辑
    /// </summary>
    public partial class TeachersLoging : Window
    {
        public TeachersLoging()
        {
            InitializeComponent();
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
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
        private void ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PS.Focus();
            }         
        }

        private void PS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btLoging_Click(sender, e);
            }     
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ID.Focus();
        }

        private void btLoging_Click(object sender, RoutedEventArgs e)
        {
            string id = ID.Text.Trim();
            string pas = PS.Password;

            Information InFor = new SystemOfTest.Information();
            string cmdText = "select * from T_TeachUser where Teach_UserName=@UserName and Teach_PassWord=@PassWord";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName",ID.Text),
                new SqlParameter("@PassWord",PS.Password)
            };
            DataSet dataset = new DataSet();
            dataset = SqlHelper.ExcuteDataSet(cmdText, parameters);
            DataTable table = dataset.Tables[0];
            DataRowCollection rows = table.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i];
                InFor.ID = (string)row["Teach_UserName"];
                InFor.PS = (string)row["Teach_PassWord"];
            }
            TL_Grid.DataContext = InFor;

            if (id == InFor.ID && pas == InFor.PS)
            {
                this.Close();
                SetQuestion SQ = new SystemOfTest.SetQuestion();
                SQ.Show();
            }
            if (id != string.Empty && pas != string.Empty)
            {
                if (ID.Text != InFor.ID || pas != InFor.PS)
                {
                    MessageBox.Show("用户名或密码错误！");
                    return;
                }
            }
            if (id == string.Empty || pas == string.Empty)
            {
                MessageBox.Show("请输入账号和密码！");
                return;
            }            
        }
    }
}
