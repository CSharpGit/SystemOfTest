using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemOfTest
{
   public class DownLoadProcess
    {

       public delegate void DownloadStatusChangeHandle(object sender);
       public event DownloadStatusChangeHandle OnDownloadStatusChanged;

       public event DownloadStatusChangeHandle OnUpLoadComplete;//上传完成

       FTPHelper FTP;
       fileInfo file;
       string Path;
       Thread downloadThread;
       Thread uploadThread;
       string userfilename;
       #region Properties
       public string DownLoadStatus
       { get; set; }

       public fileInfo downloadFile
       {
           get { return file; }
           set { file = value; }
       }

       #endregion
       
       public DownLoadProcess(FTPHelper f, fileInfo fi,string path,params string[] filename)
       {
           if(filename!=null&&filename.Length>0)
             userfilename = filename[0];
           FTP = f;
           file = fi;
           Path = path;
           FTP.OnDownLoadProgressChanged += new FTPHelper.OnDonwnLoadProcessHandle(FTP_OnDownLoadProgressChanged);//下载
           FTP.OnUpLoadProgressChanged += new FTPHelper.OnUpLoadProcessHandle(FTP_OnUpLoadProgressChanged);//上传
       }

       //上传进度
       void FTP_OnUpLoadProgressChanged(object sender)
       {
           FTPHelper ftp = sender as FTPHelper;
           file.complete = ftp.UpLoadComplete;
       }

       //下载进度
       void FTP_OnDownLoadProgressChanged(object sender)
       {
           FTPHelper ftp = sender as FTPHelper;
           file.complete = ftp.DownComplete;
       }

       //用于线程调用开始下载
       private void ThreadProc()
       {
           FTP.Download(Path, file.fileName);
           if (this.OnDownloadStatusChanged != null&&FTP.DownLoadCancelStatus!=true)
           {
               this.DownLoadStatus = "Finished";
               OnDownloadStatusChanged(this);
           }
       }

       //用于线程调用开始上传
       private void ThreadUpLoad()
       {
           this.file.DownLoadStatus = "上传中...";
           FTP.Upload(file.LocalPath, userfilename);
           if (FTP.UpLoadCancel != true)
           { this.file.DownLoadStatus = "完成"; }
           else
           { this.file.DownLoadStatus = "已取消"; }
           Thread.Sleep(300);
           if (this.OnUpLoadComplete != null)
           {
               OnUpLoadComplete(this);
           }
       }

       //开始下载
       public void StartDownLoad()
       {
           downloadThread = new Thread(new ThreadStart(this.ThreadProc));
           downloadThread.Start();
           this.file.DownLoadStatus = "取消";
       }

       //开始上传
       public void StartUpLoad()
       {
           uploadThread = new Thread(new ThreadStart(this.ThreadUpLoad));
           uploadThread.Start();
       }
       //取消上传
       public void StopUpLoad()
       {
           if (FTP != null)
           {
               FTP.CancelUpLoad();
           }
       }

       //取消下载
       public void StopDownload()
       {
           if (FTP != null)
           {
               FTP.CancelDownLoad();
               
               if (OnDownloadStatusChanged != null)
               {
                   this.DownLoadStatus = "Canceled";
                   OnDownloadStatusChanged(this);
               }
           }
       }
    }
}
