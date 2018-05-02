using System;
using System.Collections.Generic;
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
using SystemOfTest.FTPFilesUpDownLoad;

namespace SystemOfTest
{
    /// <summary>
    /// SetQuestion.xaml 的交互逻辑
    /// </summary>
    public partial class SetQuestion : Window
    {
        public SetQuestion()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
                 
            FTPFilesUpLoadController upload = new FTPFilesUpLoadController(true);
            //学生传答卷，赋值false，出题人传试卷则，赋值true
            stackpanel1.Children.Add(upload);          
        }
        public static bool IsNumeric(string s)
        {
            double v;
            if (double.TryParse(s, out v))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void sure1_Click(object sender, RoutedEventArgs e)
        {           
            if (rb_A.IsChecked == false && rb_C.IsChecked == false && rb_Web.IsChecked == false)
            {
                MessageBox.Show("请勾选对应方向！");
                return;
            }
            if(tx_ScoreChoice.Text.Trim()==string.Empty)
            {
                MessageBox.Show("请确定每题分值！");
                return;
            }
            sub1.Visibility = Visibility.Visible;
            grid_sv1.Visibility = Visibility.Visible;
            sp.Children.Clear();
            int ChoicesNum;
            if (IsNumeric(tx_NumChoice.Text.Trim()))
            {
                ChoicesNum = Convert.ToInt32(tx_NumChoice.Text.Trim());
                if (ChoicesNum <= 0)
                {
                    MessageBox.Show("输入值无效！");
                }
                else
                {
                    StaticInform.ChoicesNum = ChoicesNum;                   
                    for (int i = 0; i < ChoicesNum; i++)
                    {
                        StackPanel s = new StackPanel();
                        Label lb = new Label();
                        lb.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        lb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                        lb.Content = i+1;
                        s.Children.Add(lb);
                        for (int j = 0; j <  4; j++)
                        {
                            RadioButton rb = new RadioButton();
                            rb.Width = 30;
                            rb.Height = 20;
                            rb.Margin = new Thickness { Bottom = 5, Left = 5, Right = 5, Top = 5 };
                            rb.CommandParameter = i + "_"+j;
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
            }
            else
            {
                MessageBox.Show("输入无效或未输入信息!");
            }
            SqlParameter parameters = new SqlParameter("@Score_Choice", tx_ScoreChoice.Text);
            if (rb_C.IsChecked == true)
            {
                string cmdText = "update T_Score set S_C_Choice=@Score_Choice";
                SqlHelper.ExecuteNonQuery(cmdText, parameters);
            }
            if (rb_A.IsChecked == true)
            {
                string cmdText = "update T_Score set S_A_Choice=@Score_Choice";           
                SqlHelper.ExecuteNonQuery(cmdText, parameters);
            }
            if (rb_Web.IsChecked == true)
            {
                string cmdText = "update T_Score set S_Web_Choice=@Score_Choice";
                SqlHelper.ExecuteNonQuery(cmdText, parameters);
            }
        }


        int[] Number = new int[1000];
        char[] Answer = new char[1000];
        public void OK(object sender, RoutedEventArgs e)
        {          
            for (int i = 0; i<StaticInform.ChoicesNum; i++)
            {
                Number[i] = i;
            }          
            string par=((RadioButton)sender).CommandParameter.ToString();       
            string[] b = par.Split('_');
            for (int i = 0; i <StaticInform.ChoicesNum; i++)
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
  
        private void bt_down_Click(object sender, RoutedEventArgs e)
        {
            FTPFilesDownLoadController download = new FTPFilesDownLoadController(true);
            //学生下载答卷，赋值false，批卷人查看试卷，则赋值true
            stackpanel2.Children.Add(download);
        }

        private void bt_StudentFolder_Click(object sender, RoutedEventArgs e)
        {
            string debug = System.AppDomain.CurrentDomain.BaseDirectory;
            string path1 = System.IO.Path.Combine(debug, @"StudentFolder");
            System.Diagnostics.Process.Start("explorer", path1);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StudentScore SS = new StudentScore();
            SS.Show();
        }

        private void rb_C_Click(object sender, RoutedEventArgs e)
        {
            sure1.Visibility = Visibility.Visible;
            sub1.Visibility = Visibility.Hidden;
            sure2.Visibility = Visibility.Visible;
            sub2.Visibility = Visibility.Hidden;
            sp.Children.Clear();
            tx_NumChoice.Clear();
            tx_ScoreChoice.Clear();
            tx_NumFill.Clear();
            tx_ScoreFill.Clear();
        }

        private void rb_A_Click(object sender, RoutedEventArgs e)
        {
            sure1.Visibility = Visibility.Visible;
            sub1.Visibility = Visibility.Hidden;
            sure2.Visibility = Visibility.Visible;
            sub2.Visibility = Visibility.Hidden;
            sp.Children.Clear();
            tx_NumChoice.Clear();
            tx_ScoreChoice.Clear();
            tx_NumFill.Clear();
            tx_ScoreFill.Clear();
        }

        private void rb_Web_Click(object sender, RoutedEventArgs e)
        {
            sure1.Visibility = Visibility.Visible;
            sub1.Visibility = Visibility.Hidden;
            sure2.Visibility = Visibility.Visible;
            sub2.Visibility = Visibility.Hidden;
            sp.Children.Clear();
            tx_NumChoice.Clear();
            tx_ScoreChoice.Clear();
            tx_NumFill.Clear();
            tx_ScoreFill.Clear();
        }

        private void sub1_Click(object sender, RoutedEventArgs e)
        {
            sure1.Visibility = Visibility.Hidden;
            string result = new string(Answer);
            string Resultvalue = result.Trim("\0".ToCharArray());
            string cmdText1 = "update T_Answer set A_Choices_C =@Result";
            string cmdText2 = "update T_Answer set A_Choices_A=@Result";
            string cmdText3 = "update T_Answer set A_Choices_Web =@Result";
            string cmdText_C = "update T_Score set S_C_ChoiceNum=@ChoiceNum";
            string cmdText_A = "update T_Score set S_A_ChoiceNum=@ChoiceNum";
            string cmdText_Web = "update T_Score set S_Web_ChoiceNum=@ChoiceNum";
            SqlParameter parameters1 = new SqlParameter("@ChoiceNum", StaticInform.ChoicesNum);
            SqlParameter parameters = new SqlParameter("@Result", Resultvalue);
            if (rb_C.IsChecked == true)
            {
                SqlHelper.ExecuteNonQuery(cmdText1, parameters);
                SqlHelper.ExecuteNonQuery(cmdText_C, parameters1);
            }
            if (rb_A.IsChecked == true)
            {
                SqlHelper.ExecuteNonQuery(cmdText2, parameters);
                SqlHelper.ExecuteNonQuery(cmdText_A, parameters1);
            }
            if (rb_Web.IsChecked == true)
            {
                SqlHelper.ExecuteNonQuery(cmdText3, parameters);
                SqlHelper.ExecuteNonQuery(cmdText_Web, parameters1);
            }
            MessageBox.Show("答案上传成功！");
            Array.Clear(Answer, 0, Answer.Length);
            sp.Children.Clear();
        }

        private void tx_OperationScore_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sure_Click(sender, e);
            }
        }

        private void sure_Click(object sender, RoutedEventArgs e)
        {
            string cmdText1 = "update T_Students set T_OperationScore=@OperationScore where T_StuNum =@Num";
            string cmdText2 = "update T_Students set T_TotalScore = T_ChoicesScore + T_FillScore + T_OperationScore where T_StuNum =@Num1";
            SqlParameter parameters2 = new SqlParameter("@Num1", tx_Num.Text);
            SqlParameter[] parameters1 = new SqlParameter[]
            {
                new SqlParameter("@OperationScore",tx_OperationScore.Text),
                new SqlParameter("@Num",tx_Num.Text)
            };
            SqlHelper.ExecuteNonQuery(cmdText1, parameters1);
            if (SqlHelper.ExecuteNonQuery(cmdText2, parameters2) > 0)
                MessageBox.Show("成绩录入成功！");
            else MessageBox.Show("成绩录入失败，请重试！");
        }

        private void tx_Num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tx_OperationScore.Focus();
            }
        }

        private void tx_NumChoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tx_ScoreChoice.Focus();
            }
        }

        private void tx_ScoreChoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(sure1.Visibility==Visibility.Visible)
                sure1_Click(sender, e);
            }
        }

        private void sub2_Click(object sender, RoutedEventArgs e)
        {
            sure2.Visibility = Visibility.Hidden;
            sp1.Children.Clear();
            //foreach (Control control in this.Controls)
            //{
            //    if (control is TextBox)
            //       MessageBox.Show((TextBox)control).Text);
            //}
        }

        private void sure2_Click(object sender, RoutedEventArgs e)
        {
            if (rb_A.IsChecked == false && rb_C.IsChecked == false && rb_Web.IsChecked == false)
            {
                MessageBox.Show("请勾选对应方向！");
                return;
            }
            if (tx_NumFill.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入题目数量！");
                return;
            }
            if (tx_ScoreFill.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请确定每题分值！");
                return;
            }
            sub2.Visibility = Visibility.Visible;
            grid_sv2.Visibility = Visibility.Visible;
            sp1.Children.Clear();
            if (IsNumeric(tx_NumFill.Text.Trim()))
                CreateTextbox(Convert.ToInt32(tx_NumFill.Text.Trim()));           
        }

        private void tx_NumFill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                tx_ScoreFill.Focus();
            }
        }

        private void tx_ScoreFill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (sure2.Visibility == Visibility.Visible)
                    sure2_Click(sender, e);
            }
        }

        public void CreateTextbox(int x)
        {
            for (int i = 0; i < x; i++)
            {
                StackPanel s = new StackPanel();
                for (int j = 0; j < 1; j++)
                {
                    TextBox tb = new TextBox();
                    tb.VerticalContentAlignment = VerticalAlignment.Center;
                    tb.HorizontalContentAlignment = HorizontalAlignment.Center;
                    tb.Margin = new Thickness { Bottom = 10, Left = 20, Right = 5, Top = 40 };
                    tb.Text = Convert.ToString(i + 1);
                    tb.Height = 60;
                    tb.Width = 100;
                    s.Children.Add(tb);
                }
                sp1.Children.Add(s);
            }

        }
    }
}
