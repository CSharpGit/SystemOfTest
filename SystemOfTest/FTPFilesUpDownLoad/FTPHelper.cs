
//FTP操作

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace SystemOfTest
{
   public class FTPHelper
    {

        //下载进度变化
        public delegate void OnDonwnLoadProcessHandle(object sender);
       //上传进度变化
       public delegate void OnUpLoadProcessHandle(object sender);

       public event OnDonwnLoadProcessHandle OnDownLoadProgressChanged;
       public event OnUpLoadProcessHandle OnUpLoadProgressChanged;

       string ftpServerIP;
       string ftpRemotePath;
       string ftpUserID;
       string ftpPassword;
       string ftpURI;
       int dowLoadComplete; //为图方便，上传和下载都用这个数据
       bool DownLoadCancel;
       bool _UPLoadCancel;
       int upLoadComplete;

       //下载进度
       public int DownComplete
       {
           get { return dowLoadComplete; }
       }
       //上传进度
       public int UpLoadComplete
       {
           get { return upLoadComplete; }
       }

      //取消状态

       public bool  DownLoadCancelStatus
       {
           get { return DownLoadCancel; }
       }
       //取消上传状态
       public bool UpLoadCancel
       {
           get { return _UPLoadCancel; }
       }
       /// <summary>
       /// 初始化
       /// </summary>
       /// <param name="server"></param>
       /// <param name="remotePath"></param>
       /// <param name="userID"></param>
       /// <param name="password"></param>
       public FTPHelper(string server, string remotePath, string userID, string password)
       {
           ftpServerIP = server;
           ftpRemotePath = remotePath;
           ftpUserID = userID;
           ftpPassword = password;
           ftpURI = "ftp://" + ftpServerIP + "/" + remotePath ;
           dowLoadComplete = 0;
           DownLoadCancel = false;//下载操作是否取消
           _UPLoadCancel = false;//上传是否取消
       }

       //上传文件
       //public string UploadFile(string[] filePaths)
       //{
       //    StringBuilder sb = new StringBuilder();
       //    if (filePaths != null && filePaths.Length > 0)
       //    {
       //        foreach (var file in filePaths)
       //        {
       //            sb.Append(Upload(file));

       //        }
       //    }
       //    return sb.ToString();
       //}
 
       /// <summary>
       /// 上传文件
       /// </summary>
       /// <param name="filename"></param>
      public string Upload(string filename,string updatefilename)
       {
          
           FileInfo fileInf = new FileInfo(filename);
           
           if (!fileInf.Exists)
           {
               return filename + " 不存在!\n";
           }

           if (updatefilename == null || updatefilename=="")
          {
              updatefilename = fileInf.Name;
          }
           long fileSize = fileInf.Length;//获取本地文件的大小


           string uri = ftpURI + updatefilename;
           FtpWebRequest reqFTP;
           reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

           reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
           reqFTP.KeepAlive = false;
           reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
           reqFTP.UseBinary = true;
           reqFTP.UsePassive = false;  //选择主动还是被动模式
           //Entering Passive Mode
           reqFTP.ContentLength = fileInf.Length;
           int buffLength = 2048;
           byte[] buff = new byte[buffLength];
           int contentLen;
           FileStream fs = fileInf.OpenRead();
           try
           {
               Stream strm = reqFTP.GetRequestStream();
               contentLen = fs.Read(buff, 0, buffLength);
               long hasUpLoad = contentLen;
               while (contentLen != 0)
               {
                   strm.Write(buff, 0, contentLen);
                   contentLen = fs.Read(buff, 0, buffLength);
                   hasUpLoad += contentLen;
                   upLoadComplete= (int)((Single)hasUpLoad / (Single)fileSize * 100.0);
                   if (this.OnUpLoadProgressChanged != null)
                   {
                       OnUpLoadProgressChanged(this);
                   }
                   if (this.UpLoadCancel == true) //是否已经取消上传
                   {
                       strm.Close();
                       fs.Close();
                       if (FileExist(fileInf.Name))//删除服务器中已经存在的文件
                       { Delete(fileInf.Name); }
                       return "";
                   }
               }
               strm.Close();
               fs.Close();
           }
           catch 
           {
               return "同步 " + filename + "时连接不上服务器!\n";
           }
           return "";
       }

       /// <summary>
       /// 下载
       /// </summary>
       /// <param name="filePath"></param>
       /// <param name="fileName"></param>
       public void Download(string filePath, string fileName)
       {
           FtpWebRequest reqFTP;
           try
           {
                Directory.CreateDirectory(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"StudentFolder"));
               FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

               reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + fileName));
               reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
               reqFTP.UseBinary = true;
               reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
               FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
               Stream ftpStream = response.GetResponseStream();
               long cl = response.ContentLength;
               int bufferSize = 2048;
               int readCount;
               byte[] buffer = new byte[bufferSize];

               readCount = ftpStream.Read(buffer, 0, bufferSize);
               //获取文件大小
               long fileLength = GetFileSize(fileName);
               long hasDonwnload = (long)readCount;

               while (readCount > 0)
               {
                   outputStream.Write(buffer, 0, readCount);
                   readCount = ftpStream.Read(buffer, 0, bufferSize);
                   hasDonwnload += (long)readCount;
                   //获取下载进度
                   this.dowLoadComplete = (int)((Single)hasDonwnload / (Single)fileLength * 100.0);
                   if (OnDownLoadProgressChanged != null)
                   {
                       OnDownLoadProgressChanged(this);//触发事件，用于客户端获取下载进度
                   }
                   if (DownLoadCancel == true)
                   {
                       ftpStream.Close();
                       outputStream.Close();
                       response.Close();
                       //删除文件
                       if (File.Exists(filePath + "\\" + fileName))
                       {
                           File.Delete(filePath + "\\" + fileName);
                       }
                       return;//退出程序
                   }
               }

               ftpStream.Close();
               outputStream.Close();
               response.Close();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       //取消下载
       public void CancelDownLoad()
       {
           this.DownLoadCancel = true;
       }

       //取消上传
       public void CancelUpLoad()
       {
           this._UPLoadCancel = true;
       }
       /// <summary>
       /// 删除文件
       /// </summary>
       /// <param name="fileName"></param>
       public void Delete(string fileName)
       {
           try
           {
               string uri = ftpURI + fileName;
               FtpWebRequest reqFTP;
               reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

               reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
               reqFTP.KeepAlive = false;
               reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

               string result = String.Empty;
               FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
               long size = response.ContentLength;
               Stream datastream = response.GetResponseStream();
               StreamReader sr = new StreamReader(datastream);
               result = sr.ReadToEnd();
               sr.Close();
               datastream.Close();
               response.Close();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       /// <summary>
       /// 获取当前目录下明细(包含文件和文件夹)
       /// </summary>
       /// <returns></returns>
       public string[] GetFilesDetailList()
       {
           try
           {
               StringBuilder result = new StringBuilder();
               FtpWebRequest ftp;
               ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
               ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
               ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
               WebResponse response = ftp.GetResponse();
               StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("GB2312"));
               string line = reader.ReadLine();
               //line = reader.ReadLine();
               //line = reader.ReadLine();
               while (line != null)
               {
                   result.Append(line);
                   result.Append("\n");
                   line = reader.ReadLine();
               }
               //result.Remove(result.ToString().LastIndexOf("\n"), 1);
               reader.Close();
               response.Close();
               return result.ToString().Split('\n');
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       /// <summary>
       /// 获取当前目录下文件列表(仅文件)
       /// </summary>
       /// <returns></returns>
       public List<string> GetFileList(string mask)
       {
           StringBuilder result = new StringBuilder();
           FtpWebRequest reqFTP;
           try
           {
               reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI));
               reqFTP.UseBinary = true;
               reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
               reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
               WebResponse response = reqFTP.GetResponse();
               StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("GB2312"));
               
               string line = reader.ReadLine();
               while (line != null)
               {
                   if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
                   {
                       string mask_ = mask.Substring(0, mask.IndexOf("*"));
                       if (line.Substring(0, mask_.Length) == mask_)
                       {
                               result.Append(line);
                               result.Append("\n");
                       }
                   }
                   else
                   {
                           result.Append(line);
                           result.Append("\n");
                   }
                 
                   line = reader.ReadLine();
               }
               if (result.Length <= 0)
               {
                   return null;
               }
               result.Remove(result.ToString().LastIndexOf('\n'), 1);
               reader.Close();
               response.Close();
               
              string[] files= result.ToString().Split('\n');
              string[] Directors = GetDirectoryList();
              List<string> tempList = new List<string>();
              for (int i = 0; i < files.Length; i++)
              {
                  bool isFile = true;
                  for (int j = 0; j < Directors.Length; j++)
                  {
                      if (files[i].Trim() == Directors[j].Trim())
                      { isFile = false; }
                  }
                  if (isFile == true)
                  {
                      tempList.Add(files[i]);
                  }
              }
              return tempList;
           }
           catch (Exception ex)
           {
               if (ex.Message.Trim() != "远程服务器返回错误: (550) 文件不可用(例如，未找到文件，无法访问文件)。")
               {
                   throw new Exception(ex.Message);
               }
               throw new Exception("获取文件列表出错,错误:" + ex.Message);
           }
       }

       /// <summary>
       /// 获取当前目录下所有的文件夹列表(仅文件夹)
       /// </summary>
       /// <returns></returns>
       public string[] GetDirectoryList()
       {
           string[] drectory = GetFilesDetailList();
           string m = string.Empty;
           foreach (string str in drectory)
           {
               if (str.Contains("<DIR>"))
               {
                   m += str.Substring(39).Trim() + "\n";
               }
           }
           if(m.Length>0)
           m = m.Substring(0, m.Length - 1);
           return m.Split('\n');
       }
     
       /// <summary>
       /// 判断当前目录下指定的子目录是否存在
       /// </summary>
       /// <param name="RemoteDirectoryName">指定的目录名</param>
       public bool DirectoryExist(string RemoteDirectoryName)
       {
           string[] dirList = GetDirectoryList();
           foreach (string str in dirList)
           {
               if (str.Trim() == RemoteDirectoryName.Trim())
               {
                   return true;
               }
           }
           return false;
       }

       /// <summary>
       /// 判断当前目录下指定的文件是否存在
       /// </summary>
       /// <param name="RemoteFileName">远程文件名</param>
       public bool FileExist(string RemoteFileName)
       {
           List<string> fileList = GetFileList("*.*");
           foreach (string str in fileList)
           {
               if (str.Trim() == RemoteFileName.Trim())
               {
                   return true;
               }
           }
           return false;
       }

       /// <summary>
       /// 创建文件夹
       /// </summary>
       /// <param name="dirName"></param>
       public void MakeDir(string dirName)
       {
           FtpWebRequest reqFTP;
           try
           {
               // dirName = name of the directory to create.
               reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + dirName));
               reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
               reqFTP.UseBinary = true;
               reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
               FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
               Stream ftpStream = response.GetResponseStream();

               ftpStream.Close();
               response.Close();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       /// <summary>
       /// 获取指定文件大小
       /// </summary>
       /// <param name="filename"></param>
       /// <returns></returns>
       public long GetFileSize(string filename)
       {
           FtpWebRequest reqFTP;
           long fileSize = 0;
           try
           {
               reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + filename));
               reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
               reqFTP.UseBinary = true;
               reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
               FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
               Stream ftpStream = response.GetResponseStream();
               fileSize = response.ContentLength;

               ftpStream.Close();
               response.Close();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return fileSize;
       }

       /// <summary>
       /// 改名
       /// </summary>
       /// <param name="currentFilename"></param>
       /// <param name="newFilename"></param>
       public void ReName(string currentFilename, string newFilename)
       {
           FtpWebRequest reqFTP;
           try
           {
               reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURI + currentFilename));
               reqFTP.Method = WebRequestMethods.Ftp.Rename;
               reqFTP.RenameTo = newFilename;
               reqFTP.UseBinary = true;
               reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
               FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
               Stream ftpStream = response.GetResponseStream();

               ftpStream.Close();
               response.Close();
           }
           catch (Exception ex)
           {
               throw new Exception( ex.Message);
           }
       }

       /// <summary>
       /// 移动文件
       /// </summary>
       /// <param name="currentFilename"></param>
       /// <param name="newFilename"></param>
       public void MovieFile(string currentFilename, string newDirectory)
       {
           ReName(currentFilename, newDirectory);
       }
    }
}
