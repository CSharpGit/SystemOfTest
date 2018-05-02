using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using SystemOfTest.FTPFilesUpDownLoad;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace SystemOfTest
{
    /// <summary>
    /// StartTest.xaml 的交互逻辑
    /// </summary>
    public partial class StartTest : Window
    {
        
        public StartTest()
        {
            InitializeComponent();           
        }      
      
        private void button_Click(object sender, RoutedEventArgs e)
        {
            string debug = System.AppDomain.CurrentDomain.BaseDirectory;
                string path1 = System.IO.Path.Combine(debug, @"StudentFolder"); ;
            System.Diagnostics.Process.Start("explorer",path1);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DownLoad dl = new SystemOfTest.DownLoad();
            dl.Show();
        }

        private void bt_Sub_Click(object sender, RoutedEventArgs e)
        {
            FTPFilesDownLoadController download = new FTPFilesDownLoadController(false);
            //学生下载答卷，赋值false，批卷人查看试卷，则赋值true
            //stackpanel2.Children.Add(download);
        }

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    FTPFilesUpLoadController upload = new FTPFilesUpLoadController(false);
        //    //学生传答卷，赋值false，出题人传试卷则，赋值true
        //    stackpanel1.Children.Add(upload);
        //}

        private void Operation_C_Click(object sender, RoutedEventArgs e)
        {
            string debug = System.AppDomain.CurrentDomain.BaseDirectory;
            string path1 = System.IO.Path.Combine(debug, @"StudentFolder");
            System.Diagnostics.Process.Start("explorer", path1);
        }

        private void Operation_A_Click(object sender, RoutedEventArgs e)
        {
            string debug = System.AppDomain.CurrentDomain.BaseDirectory;
            string path1 = System.IO.Path.Combine(debug, @"StudentFolder");
            System.Diagnostics.Process.Start("explorer", path1);
        }

        private void Operation_Web_Click(object sender, RoutedEventArgs e)
        {
            string debug = System.AppDomain.CurrentDomain.BaseDirectory;
            string path1 = System.IO.Path.Combine(debug, @"StudentFolder");
            System.Diagnostics.Process.Start("explorer", path1);
        }
      
        private void Read_Click(object sender, RoutedEventArgs e)
        {
            string debug = System.AppDomain.CurrentDomain.BaseDirectory;
            string path1 = System.IO.Path.Combine(debug, @"StudentFolder");
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = path1;
            dialog.Filter = "XPS 文档(*.xps)|*.xps";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                XpsDocument doc = new XpsDocument(dialog.FileName, System.IO.FileAccess.Read);
                docViewer.Document = doc.GetFixedDocumentSequence();
            }
        }

        public void CreateTextbox( int x)
        {
            for (int i = 0; i <x; i++)
            {
                StackPanel s = new StackPanel();
                for (int j = 0; j < 1; j++)
                {
                    System.Windows.Controls.TextBox tb = new System.Windows.Controls.TextBox();
                    tb.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    tb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    tb.Margin = new Thickness { Bottom = 10, Left = 20, Right = 5, Top = 40 };
                    tb.Text = Convert.ToString(i + 1);
                    tb.Height = 60;
                    tb.Width = 100;
                    System.Windows.Data.Binding fill = new System.Windows.Data.Binding("Fill");
                    s.Children.Add(tb);               
                }
                sp.Children.Add(s);
            }

        }


        public void CreateControl()
        {            
            for (int i = 0; i < StaticInform.ChoicesNum; i++)
            {
                StackPanel s = new StackPanel();
                System.Windows.Controls.Label lb = new System.Windows.Controls.Label();
                lb.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                lb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                lb.Content = i + 1;
                s.Children.Add(lb);
                for (int j = 0; j < 4; j++)
                {
                    System.Windows.Controls.RadioButton rb = new System.Windows.Controls.RadioButton();
                    rb.Width = 30;
                    rb.Height = 20;
                    rb.Margin = new Thickness { Bottom = 5, Left = 5, Right = 5, Top = 5 };
                    rb.CommandParameter = i + "_" + j;
                    rb.Click += new RoutedEventHandler(OK);
                    switch (j)
                    {
                        case 0:
                            rb.Content = "A";
                            break;
                        case 1:
                            rb.Content = "B";
                            break;
                        case 2:
                            rb.Content = "C";
                            break;
                        case 3:
                            rb.Content = "D";
                            break;
                    }
                    s.Children.Add(rb);
                }
                sp.Children.Add(s);
            }
        }
        private void Choice_C_Click(object sender, RoutedEventArgs e)
        {
            string cmdText = "select S_C_ChoiceNum from T_Score";
            StaticInform.ChoicesNum = Convert.ToInt32(SqlHelper.ExcuteScalar(cmdText));
            sp.Children.Clear();
            CreateControl();          
            C_Choice.Visibility = Visibility.Visible;
            C_Fill.Visibility = Visibility.Hidden;
            Web_Choice.Visibility = Visibility.Hidden;
            Web_Fill.Visibility = Visibility.Hidden;
            A_Choice.Visibility = Visibility.Hidden;
            A_Fill.Visibility = Visibility.Hidden;
        }

        int[] Number = new int[1000];
        char[] Answer = new char[1000];

        public IEnumerable<System.Windows.Controls.Control> Controls { get; private set; }

        public void OK(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < StaticInform.ChoicesNum; i++)
            {
                Number[i] = i;
            }
            string par = ((System.Windows.Controls.RadioButton)sender).CommandParameter.ToString();
            string[] b = par.Split('_');
            for (int i = 0; i < StaticInform.ChoicesNum; i++)
            {
                if (Convert.ToInt32(b[0]) == Number[i])
                {

                    switch (Convert.ToInt32(b[1]))
                    {
                        case 0:
                            Answer[i] = 'A';
                            break;
                        case 1:
                            Answer[i] = 'B';
                            break;
                        case 2:
                            Answer[i] = 'C';
                            break;
                        case 3:
                            Answer[i] = 'D';
                            break;
                    }
                }
            }
        }

        private void Choice_A_Click(object sender, RoutedEventArgs e)
        {
            string cmdText = "select S_A_ChoiceNum from T_Score";
            StaticInform.ChoicesNum = Convert.ToInt32(SqlHelper.ExcuteScalar(cmdText));
            sp.Children.Clear();
            CreateControl();
            C_Choice.Visibility = Visibility.Hidden;
            C_Fill.Visibility = Visibility.Hidden;
            Web_Choice.Visibility = Visibility.Hidden;
            Web_Fill.Visibility = Visibility.Hidden;
            A_Choice.Visibility = Visibility.Visible;
            A_Fill.Visibility = Visibility.Hidden;
        }

        private void Choice_Web_Click(object sender, RoutedEventArgs e)
        {
            string cmdText = "select S_Web_ChoiceNum from T_Score";
            StaticInform.ChoicesNum = Convert.ToInt32(SqlHelper.ExcuteScalar(cmdText));
            sp.Children.Clear();
            CreateControl();
            C_Choice.Visibility = Visibility.Hidden;
            C_Fill.Visibility = Visibility.Hidden;
            Web_Choice.Visibility = Visibility.Visible;
            Web_Fill.Visibility = Visibility.Hidden;
            A_Choice.Visibility = Visibility.Hidden;
            A_Fill.Visibility = Visibility.Hidden;
        }

        private void Fill_C_Click(object sender, RoutedEventArgs e)
        {
            sp.Children.Clear();
            CreateTextbox(20);
            //SqlHelper.CreateControlView(canvas1, 49, 2);
            C_Choice.Visibility = Visibility.Hidden;
            C_Fill.Visibility = Visibility.Visible;
            Web_Choice.Visibility = Visibility.Hidden;
            Web_Fill.Visibility = Visibility.Hidden;
            A_Choice.Visibility = Visibility.Hidden;
            A_Fill.Visibility = Visibility.Hidden;
        }

        private void Fill_A_Click(object sender, RoutedEventArgs e)
        {
            sp.Children.Clear();
            CreateTextbox(10);
            //SqlHelper.CreateControlView(canvas1, 51, 2);
            C_Choice.Visibility = Visibility.Hidden;
            C_Fill.Visibility = Visibility.Hidden;
            Web_Choice.Visibility = Visibility.Hidden;
            Web_Fill.Visibility = Visibility.Hidden;
            A_Choice.Visibility = Visibility.Hidden;
            A_Fill.Visibility = Visibility.Visible;
        }

        private void Fill_Web_Click(object sender, RoutedEventArgs e)
        {
            sp.Children.Clear();
            CreateTextbox(20);
            //SqlHelper.CreateControlView(sp, 60, 2);
            C_Choice.Visibility = Visibility.Hidden;
            C_Fill.Visibility = Visibility.Hidden;
            Web_Choice.Visibility = Visibility.Hidden;
            Web_Fill.Visibility = Visibility.Visible;
            A_Choice.Visibility = Visibility.Hidden;
            A_Fill.Visibility = Visibility.Hidden;
        }

        public int Compare(string s1, string s2)
        {
            int count = 0;/*相同字符个数*/
            int n = s1.Length > s2.Length ? s2.Length : s1.Length;/*获得较短的字符串的长度*/
            for (int i = 0; i < n; i++)
            {
                if (s1.Substring(i, 1) == s2.Substring(i, 1))/*同位置字符是否相同*/
                {
                    count++;
                }
            }
            return count;
        }

        private void C_Choice_Click(object sender, RoutedEventArgs e)
        {
            string result = new string(Answer);
            string Resultvalue = result.Trim("\0".ToCharArray());
            string cmdText = "select A_Choices_C from T_Answer";
            string cmdText2 = "select S_C_Choice from T_Score";
            int OneChoiceScore= Convert.ToInt32(SqlHelper.ExcuteScalar(cmdText2));
            string answer = Convert.ToString(SqlHelper.ExcuteScalar(cmdText));
            int TrueNum = Compare(Resultvalue, answer);
            int ChoiceScore = OneChoiceScore * TrueNum;
            string cmdText1 = "update T_Students set S_C_Choice=@ChoiceScore where T_StuNum=@Num";
            SqlParameter[] parameters1 = new SqlParameter[]
            {
                new SqlParameter("@ChoiceScore",ChoiceScore),
                new SqlParameter("@Num",StaticInform.Num)
            };
            if (SqlHelper.ExecuteNonQuery(cmdText1, parameters1) > 0)
                System.Windows.MessageBox.Show("答案上传成功！");
            Array.Clear(Answer, 0, Answer.Length);
        }

        private void A_Choice_Click(object sender, RoutedEventArgs e)
        {
            string result = new string(Answer);
            string Resultvalue = result.Trim("\0".ToCharArray());
            string cmdText = "select A_Choices_A from T_Answer";
            string cmdText2 = "select S_A_Choice from T_Score";
            int OneChoiceScore = Convert.ToInt32(SqlHelper.ExcuteScalar(cmdText2));
            string answer = Convert.ToString(SqlHelper.ExcuteScalar(cmdText));
            int TrueNum = Compare(Resultvalue, answer);
            int ChoiceScore = OneChoiceScore * TrueNum;
            string cmdText1 = "update T_Students set S_A_Choice=@ChoiceScore where T_StuNum=@Num";
            SqlParameter[] parameters1 = new SqlParameter[]
            {
                new SqlParameter("@ChoiceScore",ChoiceScore),
                new SqlParameter("@Num",StaticInform.Num)
            };
            if (SqlHelper.ExecuteNonQuery(cmdText1, parameters1) > 0)
                System.Windows.MessageBox.Show("答案上传成功！");
            Array.Clear(Answer, 0, Answer.Length);
        }

        private void Web_Choice_Click(object sender, RoutedEventArgs e)
        {
            string result = new string(Answer);
            string Resultvalue = result.Trim("\0".ToCharArray());
            string cmdText = "select A_Choices_Web from T_Answer";
            string cmdText2 = "select S_Web_Choice from T_Score";
            int OneChoiceScore = Convert.ToInt32(SqlHelper.ExcuteScalar(cmdText2));
            string answer = Convert.ToString(SqlHelper.ExcuteScalar(cmdText));
            int TrueNum = Compare(Resultvalue, answer);
            int ChoiceScore = OneChoiceScore * TrueNum;
            string cmdText1 = "update T_Students set S_Web_Choice=@ChoiceScore where T_StuNum=@Num";
            SqlParameter[] parameters1 = new SqlParameter[]
            {
                new SqlParameter("@ChoiceScore",ChoiceScore),
                new SqlParameter("@Num",StaticInform.Num)
            };
            if (SqlHelper.ExecuteNonQuery(cmdText1, parameters1) > 0)
                System.Windows.MessageBox.Show("答案上传成功！");
            Array.Clear(Answer, 0, Answer.Length);
        }

        private void C_Fill_Click(object sender, RoutedEventArgs e)
        {          
            System.Windows.MessageBox.Show("答案上传成功！");
        }
    }
}
