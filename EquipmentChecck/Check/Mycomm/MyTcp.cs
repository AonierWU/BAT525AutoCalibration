using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace TestSystem_Pack
{
    public class TcpIp : MyComm
    {

        private static Logger logger = null;
        //IPEndPoint IPEPoint = new IPEndPoint(IPAddress.Parse(textBox1.Text), 80);
        private IPEndPoint endPoint = null;
        protected string TcpIPInfo = null;

        public TcpIp(string TcpIPInfo, Logger log)
        {
            this.TcpIPInfo = TcpIPInfo;
            logger = log;
        }
        private TcpClient client = new TcpClient();
        private NetworkStream networkStream = null;
        public byte[] TCPRecvData = new byte[1] { 0 };

        private int SendData(byte[] buffer, int count = -1)
        {
            try
            {
                if (buffer.Length > 0)
                {
                    count = buffer.Length;
                    networkStream.WriteTimeout = WRITETIMEOUT;
                    networkStream.Write(buffer, 0, buffer.Length);

                    if (logger != null)
                    {
                        string strSendInfo = "";
                        for (int i = 0; i < buffer.Length; i++)
                            strSendInfo += buffer[i].ToString("X2") + " ";
                        logger.Log("**W=>: " + strSendInfo, LogLevel.Info);
                    }
                }
                return count;
            }
            catch (Exception ex)
            {

                logger.Log(ex);
                return (int)CommErrors.COMM_WRITE_ERROR;
            }
        }
        private int ReadData(Int32 waitT, out byte[] buffer)
        {
            buffer = null;
            int nCount = -1;
            // int CheckCount = 0;
            List<byte> receiveData = new List<byte>();
            byte[] receiveBuffer = new byte[2048];
            bool blOT = false;
            DateTime dt1 = DateTime.Now;

            try
            {

                while (!blOT)
                {

                    DateTime dt2 = DateTime.Now;
                    double SpanTime = dt2.Subtract(dt1).TotalMilliseconds;
                    if (SpanTime > waitT)
                    {
                        blOT = true;
                    }

                    if (networkStream.DataAvailable)
                    {
                        nCount = networkStream.Read(receiveBuffer, 0, receiveBuffer.Length);
                        for (int c = 0; c < nCount; c++)
                            receiveData.Add(receiveBuffer[c]);
                        if (receiveData.Count > 3)
                        {
                            if (receiveData.Count == receiveData[2])
                            {
                                break;
                            }
                        }
                    }
                }

                if (logger != null)
                {
                    string strSendInfo = "";
                    for (int i = 0; i < receiveData.Count; i++)
                        strSendInfo += receiveData[i].ToString("X2") + " ";
                    logger.Log("**R<=: " + strSendInfo, LogLevel.Info);
                }

                if (blOT)
                {
                    return (int)CommErrors.COMM_READ_TIMEOUT_ERROR;
                }

                if (receiveData.Count == 0)
                {
                    return (int)CommErrors.CMD_READ_DATA_LEN_ERROR;
                }
                else
                {
                    buffer = receiveData.ToArray();
                    return receiveData.Count();
                }
            }
            catch (System.Exception ex)
            {
                logger.Log(ex);
                return (int)CommErrors.CMD_READ_DATA_ERROR;
            }

        }
        private int ReadDataNew(Int32 waitT, out byte[] buffer)
        {
            buffer = null;
            int nCount = -1;
            int haveReceiveLen = 0;
            int CheckCount = 0;
            List<byte> receiveData = new List<byte>();
            byte[] receiveBuffer = new byte[2048];
            try
            {
                for (CheckCount = 0; CheckCount < waitT / INTERVALTIMEOUT; CheckCount++)
                {
                    if (networkStream.DataAvailable)
                    {
                        nCount = networkStream.Read(receiveBuffer, 0, receiveBuffer.Length);
                        haveReceiveLen = haveReceiveLen + nCount;
                        for (int c = 0; c < nCount; c++)
                            receiveData.Add(receiveBuffer[c]);
                    }
                    else
                    {
                        if (nCount == -1)//网口还没返回值
                        {
                            Thread.Sleep(INTERVALTIMEOUT);
                            continue;
                        }
                        break;
                    }

                    Thread.Sleep(INTERVALTIMEOUT);
                }

                if (logger != null)
                {
                    string strSendInfo = "";
                    for (int i = 0; i < receiveData.Count; i++)
                        strSendInfo += receiveData[i].ToString("X2") + " ";
                    logger.Log("**R<=: " + strSendInfo, LogLevel.Info);
                }

                if (CheckCount >= waitT / INTERVALTIMEOUT)
                {
                    return (int)CommErrors.COMM_READ_TIMEOUT_ERROR;
                }

                if (receiveData.Count == 0)
                {
                    return (int)CommErrors.CMD_READ_DATA_LEN_ERROR;
                }
                else
                {
                    buffer = receiveData.ToArray();
                    return receiveData.Count();
                }
            }
            catch (System.Exception ex)
            {
                logger.Log(ex);
                return (int)CommErrors.CMD_READ_DATA_ERROR;
            }

        }
        public override bool Connect()
        {
            try
            {
                if (!TcpIPInfo.Contains('-'))
                {
                    return false;
                }
                string[] Info = TcpIPInfo.Split('-');
                endPoint = new IPEndPoint(IPAddress.Parse(Info[0]), Convert.ToInt32(Info[1]));
                //endPoint.Address = IPAddress.Parse(Info[0]);
                //endPoint.Port = Convert.ToInt32(Info[1]);

                IAsyncResult result = client.BeginConnect(endPoint.Address, endPoint.Port, null, null);
                bool success = result.AsyncWaitHandle.WaitOne(CONNECTTIMEOUT);
                if (success)
                {
                    networkStream = client.GetStream();
                }

                logger.Log("连接:" + Info[0] + "-" + Info[1], LogLevel.Info);
                return success;
            }
            catch (System.Exception ex)
            {
                logger.Log(ex);

                return false;
            }
        }

        public override bool Disconnect()
        {
            try
            {
                if (client != null)
                {
                    client.Close();
                    if (logger != null)
                    {
                        logger.Log("断开连接", LogLevel.Info);
                        //logger.CloseLog();
                    }
                    client = new TcpClient();
                }
                if (networkStream != null)
                {
                    networkStream.Close();
                    networkStream.Dispose();

                }
                return true;
            }
            catch (System.Exception ex)
            {
                logger.Log(ex);
                return false;
            }
            finally
            {
            }
        }

        public override int WriteRead(byte[] writeBuffer, out byte[] readBuffer, Int32 WaitT = 5000)
        {
            readBuffer = null;
            int writeCount = SendData(writeBuffer, writeBuffer.Length);
            if (writeCount < 0)
                return writeCount;
            int readCount = ReadData(WaitT, out readBuffer);
            if (readCount < 0)
                return readCount;
            return readCount;
        }
        public override int WriteReadNew(byte[] writeBuffer, out byte[] readBuffer, Int32 WaitT = 5000)
        {
            readBuffer = null;
            int writeCount = SendData(writeBuffer, writeBuffer.Length);
            if (writeCount < 0)
                return writeCount;
            int readCount = ReadDataNew(WaitT, out readBuffer);
            if (readCount < 0)
                return readCount;
            return readCount;
        }
        public override int WriteCommand(byte[] cmd)
        {
            throw new NotImplementedException();
        }
        public override int WriteReadCommand(byte[] write, out byte[] read)
        {
            throw new NotImplementedException();
        }
        public override bool DiscardInBuffer()
        {
            throw new NotImplementedException();
        }
        public override int WriteSNCode(byte[] writeBuffer, int WaitT = 5000)
        {
            throw new NotImplementedException();
        }
    }
}
