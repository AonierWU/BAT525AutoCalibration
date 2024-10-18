/********************************************************
**作    者: 陈 伟
**修改时间: 2020/10/01 08:00:00
**描    述: 记录日志在文件中
**名    称: LogHelper
********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace TestSystem_Pack
{
	

        /// <summary>
        /// 日志类型枚举:Error,Warning,SQL,Info,Debug
        /// </summary>
        public enum LogLevel
        {
            /// <summary>
            /// 错误
            /// </summary>
            Error,

            /// <summary>
            /// 警告
            /// </summary>
            Warning,

            /// <summary>
            /// SQL语句
            /// </summary>
            SQL,

            /// <summary>
            /// 一般输出
            /// </summary>
            Info,

            /// <summary>
            /// 调试输出
            /// </summary>
            Debug
        }

        /// <summary>
        /// 日志记录工具
        /// </summary>
        public class Logger
        {

            private LogLevel loglevel = LogLevel.Info;
            private bool stopLog;//停止记录，关闭线程；
            /// <summary>
            /// 日志等级设定，等级由低到高：Error，Warning , SQL , Info , Debug
            /// </summary>
            public LogLevel logLevel
            {
                get { return loglevel; }
                set { loglevel = value; }
            }

            private readonly Thread LogThread;
            private readonly ConcurrentQueue<string> LogQueue; //自定义线程安全的Queue
            private readonly object SyncRoot;
            private readonly string FilePath;
            private readonly string FileName;

            /// <summary>
            /// 因为线程是死循环读取队列,在没有日志数据的时候可能会消耗不必要的资源,所有当队列没有数据的时候用该类控制线程的(继续和暂停)
            /// </summary>
            private readonly AutoResetEvent AutoReset = null;

            public Logger(string fileName)
            {
                AutoReset = new AutoResetEvent(false);
                SyncRoot = new object();
                FilePath = Application.StartupPath+  @"\\LogFile\\" + DateTime.Now.ToString("yyyyMMdd") + @"\\";
                FileName = fileName;
                stopLog = true;
                LogThread = new Thread(WriteLog);
                LogThread.IsBackground = true;
                LogQueue = new ConcurrentQueue<string>();
                LogThread.Start();
            }

            ///// <summary>
            ///// 记录日志,不受日志等级限制
            ///// </summary>
            ///// <param name="msg">日志内容</param>
            //public  void Log(string msg)
            //{
            //    string _msg = string.Format("{0} : {1}", DateTime.Now.ToString("HH:mm:ss.fff"), msg);
            //    LogQueue.Enqueue(msg);
            //    AutoReset.Set();
            //}


            /// <summary>
            /// 记录日志，受日志等级限制
            /// </summary>
            /// <param name="msg">日志内容</param>
            /// <param name="type">日志类型</param>
            public void Log(string msg, LogLevel type)
            {
                if (loglevel >= type)
                {
                    string _msg = string.Format("{0} {1}: {2}", DateTime.Now.ToString("HH:mm:ss.fff"), type, msg);
                    LogQueue.Enqueue(_msg);
                    AutoReset.Set();
                }
            }


            /// <summary>
            /// 记录异常日志，不受日志等级限制
            /// </summary>
            /// <param name="ex">异常</param>
            public void Log(Exception ex)
            {
                if (ex != null)
                {
                    // string _newLine = string.Empty; //Environment.NewLine;
                    string _newLine = Environment.NewLine;
                    StringBuilder _builder = new StringBuilder();
                    _builder.AppendFormat("{0}: {1}{2}", DateTime.Now.ToString("HH:mm:ss.fff"), ex.Message, _newLine);
                    _builder.AppendFormat("{0}{1}", ex.GetType(), _newLine);
                    _builder.AppendFormat("{0}{1}", ex.Source, _newLine);
                    _builder.AppendFormat("{0}{1}", ex.TargetSite, _newLine);
                    _builder.AppendFormat("{0}{1}", ex.StackTrace, _newLine);
                    LogQueue.Enqueue(_builder.ToString());
                    AutoReset.Set();
                }
            }

            public void CloseLog()
            {
                Thread.Sleep(1000);
                stopLog = false;
            }

            /// <summary>
            /// 写入日志
            /// </summary>
            private void WriteLog()
            {
                StringBuilder strBuilder = new StringBuilder();
                while (stopLog)
                {
                    if (LogQueue.Count() > 0)
                    {
                        string _msg;
                        LogQueue.TryDequeue(out _msg);
                        if (!string.IsNullOrWhiteSpace(_msg))
                        {
                            //字符串拼接
                            strBuilder.Append(_msg).AppendLine();
                        }

                        if (strBuilder.Length >= 1024)//日志数据长度达到500先记录下;防止异常时过多的丢失数据
                        {
                            //if (!string.IsNullOrWhiteSpace(strBuilder.ToString()))
                            //{
                            Monitor.Enter(SyncRoot);
                            if (!CreateDirectory()) continue;
                            string _path = string.Format("{0}_{1}.log", FilePath + FileName, DateTime.Now.ToString("yyyyMMdd"));
                            Monitor.Exit(SyncRoot);
                            lock (SyncRoot)
                            {
                                if (CreateFile(_path))
                                    ProcessWriteLog(_path, strBuilder.ToString()); //写入日志到文本
                            }
                            strBuilder.Clear();
                            //}
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(strBuilder.ToString()))
                        {
                            Monitor.Enter(SyncRoot);
                            if (!CreateDirectory()) continue;
                            string _path = string.Format("{0}_{1}.log", FilePath + FileName, DateTime.Now.ToString("yyyyMMdd"));
                            Monitor.Exit(SyncRoot);
                            lock (SyncRoot)
                            {
                                if (CreateFile(_path))
                                    ProcessWriteLog(_path, strBuilder.ToString()); //写入日志到文本
                            }
                            strBuilder.Clear();
                        }
                        //在这里,线程会被暂停,直到收到信号;
                        AutoReset.WaitOne();
                    }
                }

                //当log文件需要关闭时，将现有数据全部写入log后再关闭
                while (LogQueue.Count() > 0)
                {
                    string _msg;
                    LogQueue.TryDequeue(out _msg);
                    if (!string.IsNullOrWhiteSpace(_msg))
                    {
                        //字符串拼接
                        strBuilder.Append(_msg).AppendLine();
                    }

                    if (strBuilder.Length >= 1024)//日志数据长度达到500先记录下;防止异常时过多的丢失数据
                    {
                        Monitor.Enter(SyncRoot);
                        if (!CreateDirectory()) continue;
                        string _path = string.Format("{0}_{1}.log", FilePath + FileName, DateTime.Now.ToString("yyyyMMdd"));
                        Monitor.Exit(SyncRoot);
                        lock (SyncRoot)
                        {
                            if (CreateFile(_path))
                                ProcessWriteLog(_path, strBuilder.ToString()); //写入日志到文本
                        }
                        strBuilder.Clear();
                    }
                }
                if (strBuilder.Length > 0)
                {
                    Monitor.Enter(SyncRoot);
                    CreateDirectory();
                    string _path = string.Format("{0}_{1}.log", FilePath + FileName, DateTime.Now.ToString("yyyyMMdd"));
                    Monitor.Exit(SyncRoot);
                    lock (SyncRoot)
                    {
                        if (CreateFile(_path))
                            ProcessWriteLog(_path, strBuilder.ToString()); //写入日志到文本
                    }
                    strBuilder.Clear();
                    LogThread.Abort();
                }
            }


            /// <summary>
            /// 写入文件
            /// </summary>
            /// <param name="path">文件路径返回文件名</param>
            /// <param name="msg">写入内容</param>
            private void ProcessWriteLog(string path, string msg)
            {
                try
                {
                    StreamWriter _sw = File.AppendText(path);
                    //_sw.BaseStream.Seek(1, SeekOrigin.Current);
                    _sw.Write(msg);
                    _sw.Flush();
                    _sw.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("写入日志失败，原因:{0}", ex.Message));
                }
            }


            /// <summary>
            /// 创建文件
            /// </summary>
            /// <param name="path"></param>
            /// <returns></returns>
            private bool CreateFile(string path)
            {
                bool _result = true;
                try
                {
                    if (!File.Exists(path))
                    {
                        FileStream _files = File.Create(path);
                        _files.Close();
                    }
                }
                catch (Exception)
                {
                    _result = false;
                }
                return _result;
            }


            /// <summary>
            /// 创建文件夹
            /// </summary>
            /// <returns></returns>
            private bool CreateDirectory()
            {
                bool _result = true;
                try
                {
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                }
                catch (Exception)
                {
                    _result = false;
                }
                return _result;
            }
        }

    
}
