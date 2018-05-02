using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SystemOfTest.FTPFilesUpDownLoad
{
    /// <summary>
    /// remotePath表示服务器目录中的子文件夹，UserFiles表示考生上传文件夹，Files表示考题文件夹
    /// 批卷者下载考试答卷时要使用   private const string remotePath = "UserFiles//";
    /// 考生下载答卷时要使用    private const string remotePath = "Files//";
    /// </summary>
    public partial class FTPFilesDownLoadController : System.Windows.Controls.UserControl
    {
        private const string serverIP = "59.72.194.42";
        private const string userID = "FileReader";
        private const string passWord = "rjyfzx";
        private string remotePath;

        //private const string remotePath = "Files//";

        string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"StudentFolder");//默认下载后本地保存路径

         FTPHelper FTP;

        ObservableCollection<fileInfo> files = new ObservableCollection<fileInfo>();
        List<DownLoadProcess> OnDownLoadList = new List<DownLoadProcess>();
        public FTPFilesDownLoadController(bool manager)
        {
            InitializeComponent();
            this.fileList.ItemsSource = files;
            if(manager)
                remotePath = "UserFiles//";
            else
                remotePath = "Files//";

            FTP = new FTPHelper(serverIP, remotePath, userID, passWord);
        }
        private void Connect_click(object sender, System.Windows.RoutedEventArgs e)
        {
            // 在此处添加事件处理程序实现。
           
            GetFiles(FTP);

        }

        //获取文件列表
        private void GetFiles(FTPHelper FTP)
        {
            if (FTP != null)
            {
                List<string> strFiles = FTP.GetFileList("*");
                if (strFiles == null)
                {
                    ErroLable.Content = "很抱歉，文件夹为空！";
                    return;
                }
                ErroLable.Content = "";
                files.Clear();
                for (int i = 0; i < strFiles.Count; i++)
                {
                    fileInfo f = new fileInfo();
                    f.fileName = strFiles[i];
                    f.DownLoadStatus = "下载";
                    files.Add(f);
                }
            }
        }


        private void btn_Click(object sender, RoutedEventArgs e)
        {

            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            fileInfo file = btn.Tag as fileInfo;

            if (file.DownLoadStatus == "下载")
            {
                //这里可以设置是否打开文件存储路径，取消注释就会出现路径选择窗口
                //FolderBrowserDialog dia = new FolderBrowserDialog();
                //DialogResult result = dia.ShowDialog();
                //if (result == System.Windows.Forms.DialogResult.Cancel)
                //{ return; }
                //path = dia.SelectedPath;


                DownLoadProcess dp = new DownLoadProcess(FTP, file, path);
                this.OnDownLoadList.Add(dp);
                dp.StartDownLoad();//开始下载

                dp.OnDownloadStatusChanged += new DownLoadProcess.DownloadStatusChangeHandle(dp_OnDownloadStatusChanged);

            }
            //取消下载
            else if (file.DownLoadStatus == "打开")
            {
                try
                {
                    System.Diagnostics.Process.Start(path + "//" + file.fileName);
                }
                catch
                {
                    System.Windows.MessageBox.Show("文件已丢失，请重新下载！");
                }
                }
            else
            {
                foreach (DownLoadProcess d in OnDownLoadList)
                {
                    if (d.downloadFile == file)
                    {
                        d.StopDownload();
                        OnDownLoadList.Remove(d);
                        System.Windows.MessageBox.Show("已取消");
                        break;
                    }
                }
            }


        }

        //下载状态发生变化
        void dp_OnDownloadStatusChanged(object sender)
        {
            DownLoadProcess dp = sender as DownLoadProcess;

            if (dp.DownLoadStatus == "Canceled")
            {
                dp.downloadFile.complete = 0;
                dp.downloadFile.DownLoadStatus = "下载";
            }
            else if (dp.DownLoadStatus == "Finished")
            {
                dp.downloadFile.DownLoadStatus = "打开";
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetFiles(FTP);
        }

        
    }
}
