using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SystemOfTest
{
   public static class SqlHelper
    {
        public static readonly string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;
        public static bool OpenConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    return true;
                }

            }
            catch
            {
                return false;
            }
        }
        public static int ExecuteNonQuery(string cmdText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExcuteScalar(string cmdText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        public static DataSet ExcuteDataSet(string cmdText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset;
                }
            }
        }
        public static DataTable ExecuteDataTable(string cmdText,params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }                             
            }
        }
        public static object ExcuteReader(string cmdText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                return ExcuteReader(cmdText, parameters);
            }
        }

        public static void CreateControlView(StackPanel sp, int x, int y)
        {
            sp.Children.Clear();
            double width = (sp.ActualWidth - (x + 1) * 5) / x;
            double height = (sp.ActualHeight - (y + 1) * 5) / y;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i % 2 == 0)
                    {
                        int label = i / 2 + 1;
                        if (j == 1)
                        {
                            if (x % 2 == 0)
                                label = x / 2 + i / 2 + 1;
                            else
                                label = x / 2 + i / 2 + 2;
                        }
                        System.Windows.Controls.Label lb = new System.Windows.Controls.Label();
                        lb.Width = width;
                        lb.Height = height;
                        lb.Content = Convert.ToString(label);
                        lb.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        lb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                        //lb.Name = Convert.ToString(label);
                        lb.FontSize = 22;                                           
                        Canvas.SetTop(lb, j * height + 5);
                        Canvas.SetLeft(lb, i * width + 5);
                        sp.Children.Add(lb);
                    }
                    else
                    {
                        System.Windows.Controls.TextBox tb = new System.Windows.Controls.TextBox()
                        {
                            Width = width,
                            Height = height,
                        };
                        Canvas.SetTop(tb, j * height + 5);
                        Canvas.SetLeft(tb, i * width + 5);
                        sp.Children.Add(tb);
                    }
                }
            }
        }
    }
}
