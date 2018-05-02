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
    /// StudentScore.xaml 的交互逻辑
    /// </summary>
    public partial class StudentScore : Window
    {
        public StudentScore()
        {
            InitializeComponent();
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            string cmdText = "select T_StuName, T_StuClass, T_StuNum, T_ChoicesScore, T_FillScore, T_OperationScore, T_TotalScore from T_Students order by T_TotalScore";          
            DataTable table = SqlHelper.ExecuteDataTable(cmdText);
            this.dataGrid1.ItemsSource = table.DefaultView;
        }
    }
}
