using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SystemOfTest
{
   public class fileInfo:INotifyPropertyChanged
    {
       public fileInfo()
       {
           complete = 0;
       }

       //文件名
       public string fileName { get; set; }
      //完成进度
       int _complete;
       public int complete
       {
           get { return _complete; }
           set
           {
               _complete = value;
               OnPropertyChanged("complete");
           }
       }

       //下载状态
       string downloadStatus; 
       public string DownLoadStatus
       {
           get { return downloadStatus; }
           set
           {
               downloadStatus = value;
               OnPropertyChanged("DownLoadStatus");
           }
       }

       //本地路径
       public string LocalPath
       { get; set; }
       
       public event PropertyChangedEventHandler PropertyChanged;

       private void OnPropertyChanged(string propertyName)
       {
           if (PropertyChanged != null)
           {
               PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
           }
       }
    }
}
