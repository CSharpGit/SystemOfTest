using System;
using System.Collections.Generic;
using System.IO;
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
    /// 当培训部长上传考题时要使用   private const string remotePath = "Files//";
    /// 当考生上传题目答卷时要使用   private const string userID = "UserFiles";
    /// </summary>
    public partial class FTPFilesUpLoadController : System.Windows.Controls.UserControl
    {
        private const string serverIP = "59.72.194.42";
        private const string userID = "FileReader";
        private const string passWord = "rjyfzx";
        //private string remotePath = "UserFiles//";
  
        private  string remotePath = "Files//";
        string userfilename = null;     //上传到服务器后文件的名称
        FTPHelper FTP;
        bool ismanager = false;
        public FTPFilesUpLoadController(bool manager)
        {
            ismanager = manager;
            InitializeComponent();
            if (manager)//如果是出题人上传试题，则规定文件夹为Files
                remotePath = "Files//";
            else
                remotePath = "UserFiles//";

            FTP = new FTPHelper(serverIP, remotePath, userID, passWord);
        }
        DownLoadProcess upPro;
        fileInfo file = new fileInfo();
        private void btn_UpLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            // 在此处添加事件处理程序实现。
                gdUpLoad.DataContext = null;
                this.filePanel.Visibility = System.Windows.Visibility.Hidden;
                btn_uplaod.Content = "上传";
                string filename;
                System.Windows.Forms.OpenFileDialog opd = new System.Windows.Forms.OpenFileDialog();
                DialogResult result = opd.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    filename = opd.FileName;
                    FileInfo fileInf = new FileInfo(filename);
                    file.LocalPath = filename;
                    file.complete = 0;
                    file.fileName = fileInf.Name;

                    gdUpLoad.DataContext = file;

                    this.btn_uplaod.Visibility = Visibility.Visible;
                    filePanel.Visibility = System.Windows.Visibility.Visible;
                }
            
           
        }

        //上传完成
        void upPro_OnUpLoadComplete(object sender)
        {
            UploadCompleteHandle del = new UploadCompleteHandle(UpLoadComplete);
            this.Dispatcher.BeginInvoke(del); //使用分发器
        }

        private delegate void UploadCompleteHandle();
        private void UpLoadComplete()
        {
            this.btnUpload.Content = "选择文件";
        }
      
        private void btn_uplaod_Click(object sender, RoutedEventArgs e)
        {
            
            if (ismanager)
            {
                userfilename = file.fileName;
            }
            else
            {
                //注意：
                //如果是学生上传答卷
                //请在这里定义上传文件的新名称，规则是添加时间戳+学号前缀,比如：20170227_2014309010124_             
                userfilename = DateTime.Now.ToString("yyyyMMdd") + "_"+StaticInform.Num + "_"+file.fileName;
            }

            if(btn_uplaod.Content.ToString()=="上传"){
                btn_uplaod.Content = "撤销";
                upPro = new DownLoadProcess(FTP, file, "", userfilename);
                upPro.OnUpLoadComplete += new DownLoadProcess.DownloadStatusChangeHandle(upPro_OnUpLoadComplete);
                upPro.StartUpLoad();
               
            }
           else
            {
                btn_uplaod.Content= "上传";

                if (userfilename != null || userfilename!="")
                {
                    file.complete = 0;
                    FTP.Delete(userfilename);
                }
                    
            }
        }

    }
}
