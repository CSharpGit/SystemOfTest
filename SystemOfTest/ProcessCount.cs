using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOfTest
{
    public class ProcessCount// 实现倒计时功能的类
    {
        private Int32 _TotalSecond;
        public Int32 TotalSecond
        {
            get { return _TotalSecond; }
            set { _TotalSecond = value; }
        }        
        public ProcessCount(Int32 totalSecond)// 构造函数
        {
            this._TotalSecond = totalSecond;
        }
        public bool ProcessStartTest()
        {
            if (_TotalSecond == 0)
                return false;
            else
            {
                _TotalSecond--;
                return true;
            }
        } // 减秒
        public string GetHour()// 获取小时显示值 
        {
            return String.Format("{0:D2}", (_TotalSecond / 3600));
        }
        public string GetMinute()// 获取分钟显示值
        {
            return String.Format("{0:D2}", (_TotalSecond % 3600) / 60);
        }
        public string GetSecond()// 获取秒显示值
        {
            return String.Format("{0:D2}", _TotalSecond % 60);
        }
    }
}
