using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace TestSystem_Pack
{
    public class RS232 : MyComm
    {

        private static Logger logger = null;
        protected SerialPort com = new SerialPort();
        protected string SerialInfo = null;
        private int CheckCount = 0;
        const int LoopCount = 700;
        const int DelayTime = 10;
        public byte[] ReceiveData = new byte[1] { 0 };

        private const int READBUFFER_MAXLEN = 4096;

        public enum Errors
        {
            FORMAT_ERROR = -10000,
        }
        /// <summary>
        /// 设定串口信息格式
        /// </summary>
        /// <param name="SerialInfo">串口名-波特率-数据位长度 ：COM1-115200-8</param>
        public RS232(string SerialInfo, Logger log)
        {
            this.SerialInfo = SerialInfo;
            logger = log;
        }

        /// <summary>
        ///串口连接 
        /// </summary>
        /// <param name="SerialInfo">Serial信息：PortName-BaudRate-DataBits 例如：COM1-115200-8</param>
        /// <returns></returns>
        public override bool Connect()
        {
            try
            {
                if (com != null)
                {
                    if (com.IsOpen)
                    {
                        com.Close();
                    }
                }
                if (!SerialInfo.Contains('-'))
                {
                    return false;
                }
                string[] Info = SerialInfo.Split('-');
                com.PortName = Info[0];
                com.BaudRate = Convert.ToInt32(Info[1]);
                com.DataBits = Convert.ToInt32(Info[2]);
                com.Parity = Parity.None;
                com.StopBits = StopBits.One;
                com.ReceivedBytesThreshold = 7;
                com.WriteTimeout = WRITETIMEOUT;
                com.Open();
                com.DiscardInBuffer();
                if (frmVerifyDevice.SaveLog)
                    logger.Log("连接: " + Info[0] + "-" + Info[1] + "-" + Info[2] + "-Parity.None-StopsBits.One", LogLevel.Info);
                return true;
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
                if (com.IsOpen)
                    com.Close();
                if (logger != null)
                {
                    if (frmVerifyDevice.SaveLog)
                        logger.Log("断开连接*****************", LogLevel.Info);
                    // logger.CloseLog();
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
        private int SendData(byte[] buffer, int count = -1)
        {
            try
            {
                if (buffer.Length > 0)
                {
                    com.DiscardInBuffer();
                    com.Write(buffer, 0, buffer.Length);
                    count = buffer.Length;
                    if (logger != null)
                    {
                        string strSendInfo = "";
                        for (int i = 0; i < buffer.Length; i++)
                            strSendInfo += buffer[i].ToString("X2") + " ";
                        if (frmVerifyDevice.SaveLog)
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
        private int ReadDataNew(Int32 waitT, out byte[] buffer)
        {
            buffer = null;
            int haveReceiveLen = 0;
            int CheckCount = 0;
            //byte[] receiveBuffer = new byte[2048];
            byte[] receiveBuffer = new byte[1];
            DateTime dt1 = DateTime.Now;
            try
            {
                System.Threading.Thread.Sleep(50);
                for (CheckCount = 0; CheckCount < waitT / (INTERVALTIMEOUT); CheckCount++)
                {
                    haveReceiveLen = com.BytesToRead;
                    if (com.BytesToRead > haveReceiveLen)
                    {
                        continue;
                    }
                    else if (com.BytesToRead == haveReceiveLen && haveReceiveLen != 0)
                    {
                        System.Threading.Thread.Sleep(20);
                        receiveBuffer = new byte[com.BytesToRead];
                        com.Read(receiveBuffer, 0, com.BytesToRead);

                        double t = DateTime.Now.Subtract(dt1).TotalMilliseconds;

                        if (logger != null)
                        {
                            string strSendInfo = "";
                            for (int i = 0; i < receiveBuffer.Length; i++)
                                strSendInfo += receiveBuffer[i].ToString("X2") + " ";
                            if (frmVerifyDevice.SaveLog)
                                logger.Log("**R<=: " + strSendInfo, LogLevel.Info);
                        }
                        break;
                    }
                    System.Threading.Thread.Sleep(50);
                }

                if (CheckCount >= (waitT / INTERVALTIMEOUT))
                {
                    CheckCount = 0;
                    return (int)CommErrors.COMM_READ_TIMEOUT_ERROR;
                }
                if (receiveBuffer.Length == 0)
                {
                    return (int)CommErrors.CMD_READ_DATA_LEN_ERROR;
                }
                else
                {
                    buffer = receiveBuffer.ToArray();
                    return receiveBuffer.Count();
                }
            }
            catch (System.Exception ex)
            {
                logger.Log(ex);
                return (int)CommErrors.CMD_READ_DATA_ERROR;
            }

        }
        private int ReadData(Int32 waitT, out byte[] buffer)
        {
            buffer = null;
            //int CheckCount = 0;
            int nCount = -1;

            byte[] receiveBuffer = new byte[2048];
            List<byte> receiveData = new List<byte>();
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
                    if (com.BytesToRead > 0)
                    {
                        nCount = com.Read(receiveBuffer, 0, com.BytesToRead);
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
        public override int WriteRead(byte[] writeBuffer, out byte[] readBuffer, int WaitT = 5000)
        {
            readBuffer = null;
            int writeCount = SendData(writeBuffer, writeBuffer.Length);
            System.Threading.Thread.Sleep(3000);
            if (writeCount < 0)
                return writeCount;
            int readCount = ReadDataNew(WaitT, out readBuffer);
            if (readCount < 0)
                return readCount;
            return readCount;
        }
        public override int WriteReadNew(byte[] cmd, out byte[] readBuffer, int WaitT = 5000)
        {
            readBuffer = null;
            byte[] writeBuffer = CalcChecksum(cmd);
            int writeCount = SendData(writeBuffer, writeBuffer.Length);
            if (writeCount < 0)
                return writeCount;
            int readCount = ReadDataNew(WaitT, out readBuffer);
            if (readCount < 0)
                return readCount;
            return readCount;
        }


        //2024/06/17
        public override int WriteCommand(byte[] cmd)
        {
            
            byte[] writeBuffer = CalcChecksum(cmd);
            byte[] readBuffer = new byte[writeBuffer.Length];
            int len = WriteRead_Calibration(writeBuffer, out readBuffer);
            if (len < 0)
                return len;
            if ((len != readBuffer.Length) || (Compare(writeBuffer, readBuffer) != 0))
                return (int)CommErrors.CMD_WRITE_ERROR;
            return 0;
        }
        private int Compare(byte[] bytes1, byte[] bytes2)
        {
            try
            {
                if (bytes1.Length != bytes2.Length)
                    return bytes1.Length - bytes2.Length;
                for (int i = 0; i < bytes1.Length; i++)
                {
                    if (bytes1[i] != bytes2[i])
                    {
                        return bytes1[i] - bytes2[i];
                    }
                }
                return 0;
            }
            catch (System.Exception)
            {
                return (int)Errors.FORMAT_ERROR;
            }
        }
        public int WriteRead_Calibration(byte[] writeBuffer, out byte[] readBuffer)
        {
            readBuffer = null;
            try
            {
                int writeCount = SendData(writeBuffer, writeBuffer.Length);
                if (writeCount < 0)
                    return writeCount;
                //System.Threading.Thread.Sleep(10);
                int readCount = ReadData_Calibration(writeBuffer, out readBuffer);
                if (readCount < 0)
                    return readCount;
                return readCount;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int ReadData_Calibration(byte[] Sendbuffer, out byte[] buffer)
        {
            string strSendInfo = "";
            buffer = null;
            com.ReceivedBytesThreshold = Sendbuffer.Length;
            try
            {
                System.Threading.Thread.Sleep(50);
                for (CheckCount = 0; CheckCount < LoopCount; CheckCount++)
                {
                    if (com.BytesToRead >= com.ReceivedBytesThreshold)
                    {
                        System.Threading.Thread.Sleep(20);
                        ReceiveData = new byte[com.BytesToRead];
                        com.Read(ReceiveData, 0, com.BytesToRead);

                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);
                }
                if (CheckCount >= LoopCount)
                    return (int)CommErrors.COMM_READ_TIMEOUT_ERROR;
                for (int i = 0; i < ReceiveData.Length; i++)
                    strSendInfo += ReceiveData[i].ToString("X2") + " ";
                logger.Log("**R=>: " + strSendInfo, LogLevel.Info);
                buffer = ReceiveData;
                return ReceiveData.Length;
            }
            catch (System.Exception)
            {
                return (int)CommErrors.CMD_READ_DATA_ERROR;
            }
        }
        /// <summary>
        /// 计算校验和
        /// </summary>
        /// <param name="cmd">要计算的数据</param>
        /// <returns>增加了校验和的数据</returns>
        private byte[] CalcChecksum(byte[] cmd)
        {
            byte byChecksum = NegativeSum(cmd);
            byte[] byNewCmd = new byte[cmd.Length + 1];
            Array.Copy(cmd, byNewCmd, cmd.Length);
            byNewCmd[cmd.Length] = byChecksum;

            return byNewCmd;
        }
        /// <summary>
        /// 累加取反校验
        /// </summary>
        /// <param name="data">要校验的数据</param>
        /// <param name="count">要校验的数据长度。默认-1:data数组长度</param>
        /// <returns>校验</returns>
        private byte NegativeSum(byte[] data, int count = -1)
        {
            byte byCheckSum = 0x00;
            if (count == -1)
                count = data.Length;
            for (int i = 0; i < count; i++)
            {
                byCheckSum += data[i];
            }
            byCheckSum = (byte)(0xFF - byCheckSum);

            return byCheckSum;
        }

        public override int WriteReadCommand(byte[] write, out byte[] read)
        {
            read = null;
            byte[] writeBuffer = CalcChecksum(write);
            byte[] readBuffer = new byte[READBUFFER_MAXLEN];
            read = null;
            int len = WriteRead_Calibration(writeBuffer, out readBuffer);
            if (len < 0)
                return len;
            else if (len == 30)
                return len;
            if (readBuffer[len - 1] != NegativeSum(readBuffer, len - 1))
                return (int)CommErrors.CMD_CHECKSUM_ERROR;
            read = new byte[len];
            Array.Copy(readBuffer, read, read.Length);
            if (!CheckCmdTag(write, read))
                return (int)CommErrors.CMD_READ_DATA_ERROR;
            return len;
        }
        private bool CheckCmdTag(byte[] write, byte[] read)
        {
            return (write[0] == read[0] && write[1] == read[1]);
        }
        public override bool DiscardInBuffer()
        {
            com.DiscardInBuffer();
            return true;
        }

        public override int WriteSNCode(byte[] cmd, int WaitT = 5000)
        {
            byte[] writeBuffer = CalcChecksum(cmd);
            int writeCount = SendData(writeBuffer, writeBuffer.Length);
            if (writeCount < 0)
                return writeCount;
            //System.Threading.Thread.Sleep(10);
            int readCount = ReadCode(writeBuffer);
            if (readCount < 0)
                return readCount;
            return readCount;

        }
        public int ReadCode(byte[] Sendbuffer)
        {
            string strSendInfo = "";
            com.ReceivedBytesThreshold = 7;
            try
            {
                System.Threading.Thread.Sleep(50);
                for (CheckCount = 0; CheckCount < LoopCount; CheckCount++)
                {
                    if (com.BytesToRead >= com.ReceivedBytesThreshold)
                    {
                        System.Threading.Thread.Sleep(20);
                        ReceiveData = new byte[com.BytesToRead];
                        com.Read(ReceiveData, 0, com.BytesToRead);

                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);
                }
                if (CheckCount >= LoopCount)
                    return (int)CommErrors.COMM_READ_TIMEOUT_ERROR;
                for (int i = 0; i < ReceiveData.Length; i++)
                    strSendInfo += ReceiveData[i].ToString("X2") + " ";
                logger.Log("**R=>: " + strSendInfo, LogLevel.Info);
                return ReceiveData.Length;
            }
            catch (System.Exception)
            {
                return (int)CommErrors.CMD_READ_DATA_ERROR;
            }
        }
    }
}
