using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

//using System.Diagnostics;

namespace Common
{
    /// <summary>
    /// 简单地利用 IOStream 写入字符串来实现保存日志功能
    /// </summary>
    public class MyLog
    {
        /// <summary>
        /// 将字符串写入日志
        /// </summary>
        /// <param name="msg">字符串</param>
        public static void WriteLog(string msg)
        {
            try
            {
                string logPath = Application.StartupPath + @"\MyLog.log";
                System.IO.StreamWriter sw = System.IO.File.AppendText(logPath);
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss : ") + msg);
                sw.Close();
            }
            catch
            { }
        }
    
        /// <summary>
        /// 将异常信息写入日志
        /// </summary>
        /// <param name="Ex">异常信息</param>
        public static void WriteLog(Exception  Ex)
        {
            try
            {
                string logPath = Application.StartupPath + @"\MyLog.log";
                System.IO.StreamWriter sw = System.IO.File.AppendText(logPath);
                ////通过如下代码来记录异常详细的信息
                //var trace = new StackTrace(Ex, true).GetFrame(0);
                //string  msg =Ex.ToString ()+ string.Format("文件名:{0},行号:{1},列号:{2}", trace.GetFileName(), trace.GetFileLineNumber(), trace.GetFileColumnNumber());
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss : ") + Ex.ToString());
                sw.Close();
            }
            catch
            { }
        }
    }
}
