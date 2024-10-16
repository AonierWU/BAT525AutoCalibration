using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Timers;
using System.Collections;
using System.Threading;

namespace TestSystem_Pack
{
    public partial class Com
    {
        public string ErrCode = "";
        const string TimeOverErr = "串口通信超时！";   //代码 001
        const string ComError = "串口通信异常，无返回数据，请检查电源及通信线连接是否正常！";//代码 002
        const string DataLenthErr = "串口返回数据长度不对！";//代码 003
        const string DataContentErr = "串口返回数据出错，请检查！";//代码 004
        const string DataFormatErr = "串口未打开或发送数据格式处理异常！";//代码 005
        const string OK = "OK";
        private int CheckCount = 0;
        const int DelayTime = 10;
        const int LoopCount = 150;
        public SerialPort com=null;
        private SerialPort Relaycom;

        private byte[] ReceiveData = new byte[1] { 0 };
        //keysight ks;
        //NI device;
        public Com(SerialPort DmmCom)
        {
            //com = new SerialPort();
           this.com = DmmCom;
        }
        public bool InitCom(int BaudRate, int ByteSize, string comNumber)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Close();
                }
                com.PortName = comNumber;
                com.BaudRate = BaudRate;
                com.DataBits = ByteSize;
                com.Parity = Parity.None;
                com.StopBits = StopBits.One;
                com.ReadTimeout = 5000;
                com.DtrEnable = true;
                return true;
            }

            catch (System.Exception)
            {
                return false;
            }
        }
        public bool OpenCom()
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Close();
                }
                com.Open();

                return true;

            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public bool CloseCom()
        {
            try
            {
                if (com.IsOpen)
                    com.Close();
                return true;
            }
            catch (System.Exception)
            {
                return false;

            }
        }

        public bool InitRelayCom(int BaudRate, int ByteSize, string comNumber)
        {
            try
            {

                if (Relaycom.IsOpen)
                {
                    Relaycom.Close();
                }
                Relaycom.PortName = comNumber;
                Relaycom.BaudRate = BaudRate;
                Relaycom.DataBits = ByteSize;
                Relaycom.Parity = Parity.None;
                Relaycom.StopBits = StopBits.One;
                Relaycom.ReceivedBytesThreshold = 7;

                return true;
            }

            catch (System.Exception)
            {
                return false;
            }
        }
        public bool OpenRelayCom()
        {
            try
            {
                if (Relaycom.IsOpen)
                {
                    Relaycom.Close();
                }
                Relaycom.Open();

                return true;

            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public bool CloseRelayCom()
        {
            try
            {
                if (Relaycom.IsOpen)
                    Relaycom.Close();
                return true;
            }
            catch (System.Exception)
            {
                return false;

            }
        }

        public bool SelectRelayCH(int CH)
        {
            try
            {
                string Cmd = "";
                    Cmd = "CF-CC-70-"+CH.ToString("x2")+"-01-00";
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                if (SendandCheckRelay(Cmd))
                    return true;
                else
                    return false;
            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
        }
        public bool SelectRelayOCVEnable()
        {
            try
            {
                string Cmd = "";
                Cmd = "CF-CC-70-16-01-00";
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                if (SendandCheckRelay(Cmd))
                    return true;
                else
                    return false;
            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
        }
        public bool SelectRelayOCVDisable()
        {
            try
            {
                string Cmd = "";
                Cmd = "CF-CC-70-16-01-00";
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                if (SendandCheckRelay(Cmd))
                    return true;
                else
                    return false;
            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
        }

        public bool SelectRelayChgCurEnable()
        {
            try
            {
                string Cmd = "";
                Cmd = "CF-CC-70-15-01-00";
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                if (SendandCheckRelay(Cmd))
                    return true;
                else
                    return false;
            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
        }
        public bool SelectRelayChgCurDisable()
        {
            try
            {
                string Cmd = "";
                Cmd = "CF-CC-70-15-00-00";
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                if (SendandCheckRelay(Cmd))
                    return true;
                else
                    return false;
            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
        }
        public bool SelectRelayDsgCurEnable()
        {
            try
            {
                string Cmd = "";
                Cmd = "CF-CC-70-17-01-00";
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                if (SendandCheckRelay(Cmd))
                    return true;
                else
                    return false;
            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
        }
        public bool SelectRelayDsgCurDisable()
        {
            try
            {
                string Cmd = "";
                Cmd = "CF-CC-70-17-00-00";
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                if (SendandCheckRelay(Cmd))
                    return true;
                else
                    return false;
            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
        }


        private bool SendandCheckRelay(string strData)
        {
            //====================New Com====================
            try
            {
                Relaycom.ReceivedBytesThreshold = 7;
                Relaycom.DiscardInBuffer();
                byte[] data = new byte[((strData.Length + 1) / 3)];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Convert.ToByte(Convert.ToInt16(strData.Substring(i * 3, 2), 16));
                Relaycom.Write(data, 0, data.Length);
                for (CheckCount = 0; CheckCount < LoopCount; CheckCount++)
                {
                    if (Relaycom.BytesToRead >= Relaycom.ReceivedBytesThreshold)
                    {
                        System.Threading.Thread.Sleep(20);
                        ReceiveData = new byte[Relaycom.BytesToRead];
                        Relaycom.Read(ReceiveData, 0, Relaycom.BytesToRead);
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);

                }
                if (CheckCount >= LoopCount)
                {
                    CheckCount = 0;
                    ErrCode = "001";
                    return false;
                }
                if (ReceiveData.Length != data.Length)
                {
                    ErrCode = "003";
                    return false;
                }
                string strch = "";
                strch = CheckReturnData(ReceiveData);
                if (strch != strData)
                {
                    ErrCode = "004";
                    return false;
                }
                else
                    return true;

            }
            catch (System.Exception)
            {
                ErrCode = "005";
                return false;
            }

        }

        public bool WriteToCom(string strData, out string Result, int WaitTime)
        {
          
            //====================New Com==========================
            Result = "01-01-01";
            try
            {
                strData = strData + "-" + CalcuCheckSum(strData);
                byte[] data = new byte[((strData.Length + 1) / 3)];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Convert.ToByte(Convert.ToInt16(strData.Substring(i * 3, 2), 16));
                com.Write(data, 0, data.Length);

                for (CheckCount = 0; CheckCount < LoopCount; CheckCount++)
                {
                    if (com.BytesToRead >= com.ReceivedBytesThreshold)
                    {
                        ReceiveData = new byte[com.BytesToRead];
                        com.Read(ReceiveData, 0, com.BytesToRead);
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);

                }
                if (CheckCount >= LoopCount)
                {
                    CheckCount = 0;
                    ErrCode = "001";
                    return false;
                }
                if (ReceiveData.Length != 0)
                {
                    Result = "";
                    for (int i = 0; i < ReceiveData.Length; i++)
                    {
                        if (i != (ReceiveData.Length - 1))
                            Result = Result + ReceiveData[i].ToString("X2") + "-";
                        else
                            Result = Result + ReceiveData[i].ToString("X2");

                    }

                    return true;
                }
                else
                {
                    ErrCode = "002";
                    return false;
                }
            }
            catch (System.Exception)
            {
                ErrCode = "005";
                return false;
            }
        }

        public bool WriteCommand(string strData)
        {
            try
            {
                strData = strData + "-" + CalcuCheckSum(strData);
                if (SendandCheck(strData))
                    return true;
                else
                    return false;
            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
        }

        private bool SendandCheck(string strData)
        {
            //====================New Com====================
            try
            {
                com.ReceivedBytesThreshold = 7;
                com.DiscardInBuffer();
                byte[] data = new byte[((strData.Length + 1) / 3)];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Convert.ToByte(Convert.ToInt16(strData.Substring(i * 3, 2), 16));
                com.Write(data, 0, data.Length);
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
                {
                    CheckCount = 0;
                    ErrCode = "001";
                    return false;
                }
                if (ReceiveData.Length != data.Length)
                {
                    ErrCode = "003";
                    return false;
                }
                string strch = "";
                strch = CheckReturnData(ReceiveData);
                if (strch != strData)
                {
                    ErrCode = "004";
                    return false;
                }
                else
                    return true;

            }
            catch (System.Exception)
            {
                ErrCode = "005";
                return false;
            }

        }

        private bool CheckReadIOStatus(string strData, out bool blOpen)
        {
            blOpen = false;
            try
            {
                com.ReceivedBytesThreshold = 7;
                com.DiscardInBuffer();
                byte[] data = new byte[((strData.Length + 1) / 3)];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Convert.ToByte(Convert.ToInt16(strData.Substring(i * 3, 2), 16));
                com.Write(data, 0, data.Length);
                for (CheckCount = 0; CheckCount < LoopCount; CheckCount++)
                {
                    if (com.BytesToRead >= com.ReceivedBytesThreshold)
                    {
                        ReceiveData = new byte[com.BytesToRead];
                        com.Read(ReceiveData, 0, com.BytesToRead);
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);

                }
                if (CheckCount >= LoopCount)
                {
                    CheckCount = 0;
                    ErrCode = "001";
                    return false;
                }
                if (ReceiveData.Length != data.Length)
                {
                    ErrCode = "003";
                    return false;
                }
                //更改状态
                string strRecvOpenCmd = "01";
                string strRecvCloseCmd = "00";

                string strRecvOpen = strData.Substring(0, 12) + strRecvOpenCmd + strData.Substring(14);
                string strRecvClose = strData.Substring(0, 12) + strRecvCloseCmd + strData.Substring(14);
                string strch = "";
                strch = CheckReturnData(ReceiveData);
                if (strch.Substring(0, 17) == strRecvOpen.Substring(0, 17))
                    blOpen = false;
                else if (strch.Substring(0, 17) == strRecvClose.Substring(0, 17))
                    blOpen = true;
                else
                {
                    return false;
                }
                    return true;

            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
        }
        private string CheckReturnData(byte[] ReturnData)
        {
            try
            {
                string strData = "";
                for (int i = 0; i < ReturnData.Length; i++)
                {
                    if (i != (ReturnData.Length - 1))
                        strData = strData + ReturnData[i].ToString("X2") + "-";
                    else
                        strData = strData + ReturnData[i].ToString("X2");

                }
                return strData;

            }
            catch (System.Exception)
            {
                return "Error";
            }
        }

        public byte[] ReadData()
        {
            return ReceiveData;
        }
        public bool ReadOCPCurrent()
        {
            //=====================Old Com============
        //    com.ReceivedBytesThreshold = 14;
        //    TimeOver = false;
        //    time.Interval = 5000;
        //    time.Enabled = true;
        //    time.AutoReset = false;
        //    time.Elapsed += new System.Timers.ElapsedEventHandler(time_Elapsed);
        //    while (TimeOver == false && ReceiveComleted == false)
        //        System.Threading.Thread.Sleep(5);
        //    time.Enabled = false;
        //    com.ReceivedBytesThreshold = 7;
        //    if (TimeOver)
        //    {
        //        TimeOver = false;
        //        ReceiveComleted = false;
        //        ErrCode = TimeOverErr;
        //        return false;
        //    }
        //    if (ReceiveData.Length != 14)
        //    {
        //        ReceiveComleted = false;
        //        ErrCode = DataLenthErr;
        //        return false;
        //    }
        //    else
        //    {
        //        ReceiveComleted = false;
        //        return true;
        //    }
            //=========================New Com============
            try
            {

                com.ReceivedBytesThreshold = 14;
                com.DiscardInBuffer();
                for (CheckCount = 0; CheckCount < LoopCount; CheckCount++)
                {
                    if (com.BytesToRead >= com.ReceivedBytesThreshold)
                    {
                        ReceiveData = new byte[com.BytesToRead];
                        com.Read(ReceiveData, 0, com.BytesToRead);
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);
                }
                com.ReceivedBytesThreshold = 7;
                if (CheckCount >= LoopCount)
                {
                    CheckCount = 0;
                    ErrCode = "001";
                    return false;
                }
                if (ReceiveData.Length != 14)
                {
                    ErrCode = "003";
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool ReadOCPCurrent545()
        {
            try
            {
                com.ReceivedBytesThreshold = 16;
                //com.DiscardInBuffer();
                for (CheckCount = 0; CheckCount < LoopCount; CheckCount++)
                {
                    if (com.BytesToRead >= com.ReceivedBytesThreshold)
                    {
                        ReceiveData = new byte[com.BytesToRead];
                        com.Read(ReceiveData, 0, com.BytesToRead);
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);
                }
                com.ReceivedBytesThreshold = 7;
                if (CheckCount >= LoopCount)
                {
                    CheckCount = 0;
                    ErrCode = "001";
                    return false;
                }
                if (ReceiveData.Length != 16)
                {
                    ErrCode = "003";
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private string CalcuCheckSum(string strToCalcuCheckSum)
        {
            int intCheckSumTemp = 0;
            byte bytCheckSum = 0;
            byte[] data = new byte[(strToCalcuCheckSum.Length + 1) / 3];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Convert.ToByte(Convert.ToInt16(strToCalcuCheckSum.Substring(i * 3, 2), 16));
                intCheckSumTemp = intCheckSumTemp + data[i];
                intCheckSumTemp = intCheckSumTemp % 256;
            }
            bytCheckSum = Convert.ToByte(intCheckSumTemp);
            bytCheckSum = Convert.ToByte(255 - bytCheckSum);
            return bytCheckSum.ToString("X2");

        }

        private bool Read8Bytes(string Cmd, out int Value)
        {
            Value = 0;
            try
            {
                com.ReceivedBytesThreshold = 8;
                com.DiscardInBuffer();
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                byte[] data = new byte[((Cmd.Length + 1) / 3)];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Convert.ToByte(Convert.ToInt16(Cmd.Substring(i * 3, 2), 16));
                com.Write(data, 0, data.Length);
                for (CheckCount = 0; CheckCount < LoopCount; CheckCount++)
                {
                    if (com.BytesToRead >= com.ReceivedBytesThreshold)
                    {
                        ReceiveData = new byte[com.BytesToRead];
                        com.Read(ReceiveData, 0, com.BytesToRead);
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);

                }
                com.ReceivedBytesThreshold = 7;
                if (CheckCount >= LoopCount)
                {
                    CheckCount = 0;
                    ErrCode = "001";
                    return false;
                }
                if (ReceiveData.Length != 8)
                {
                    ErrCode = "003";
                    return false;
                }
                else
                {
                    Value = ReceiveData[6] * 256 * 256 + ReceiveData[5] * 256 + ReceiveData[4];
                    return true;
                }

            }
            catch (System.Exception)
            {
                ErrCode = "005";
                return false;
            }

        }

        private bool Read7Bytes(string Cmd, out int Value)
        {
            /*
            Value = 0;
            try
            {
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                byte[] data = new byte[((Cmd.Length + 1) / 3)];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Convert.ToByte(Convert.ToInt16(Cmd.Substring(i * 3, 2), 16));
                com.Write(data, 0, data.Length);
                ReceiveComleted = false;
                TimeOver = false;
                time.Interval = 5000;
                time.Enabled = true;
                time.AutoReset = false;
                time.Elapsed += new System.Timers.ElapsedEventHandler(time_Elapsed);
                while (TimeOver == false && ReceiveComleted == false)
                    System.Threading.Thread.Sleep(5);
                time.Enabled = false;
                if (TimeOver)
                {
                    TimeOver = false;
                    ReceiveComleted = false;
                    ErrCode = TimeOverErr;
                    return false;
                }
                if (ReceiveData.Length != 7)
                {
                    ReceiveComleted = false;
                    ErrCode = DataLenthErr;
                    return false;
                }
                else
                {
                    ReceiveComleted = false;
                    Value = ReceiveData[5] * 256 + ReceiveData[4];
                    return true;
                }

            }
            catch (System.Exception)
            {
                ErrCode = DataFormatErr;
                return false;
            }
            */
            Value = 0;
            try
            {
                com.ReceivedBytesThreshold = 7;
                com.DiscardInBuffer();
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                byte[] data = new byte[((Cmd.Length + 1) / 3)];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Convert.ToByte(Convert.ToInt16(Cmd.Substring(i * 3, 2), 16));
                com.Write(data, 0, data.Length);
                for (CheckCount = 0; CheckCount < LoopCount; CheckCount++)
                {
                    if (com.BytesToRead >= com.ReceivedBytesThreshold)
                    {
                        ReceiveData = new byte[com.BytesToRead];
                        com.Read(ReceiveData, 0, com.BytesToRead);
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);

                }
                if (CheckCount >= LoopCount)
                {
                    CheckCount = 0;
                    ErrCode = "001";
                    return false;
                }
                if (ReceiveData.Length != 7)
                {
                    ErrCode = "003";
                    return false;
                }
                else
                {
                    Value = ReceiveData[5] * 256 + ReceiveData[4];
                    return true;
                }

            }
            catch (System.Exception)
            {
                ErrCode = "005";
                return false;
            }

        }

        private bool Read7BytesTemp(string Cmd, out int Value)
        {
            Value = 0;
            try
            {
                com.ReceivedBytesThreshold = 7;
                com.DiscardInBuffer();
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                byte[] data = new byte[((Cmd.Length + 1) / 3)];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Convert.ToByte(Convert.ToInt16(Cmd.Substring(i * 3, 2), 16));
                com.Write(data, 0, data.Length);
                for (CheckCount = 0; CheckCount < 200; CheckCount++)
                {
                    if (com.BytesToRead >= com.ReceivedBytesThreshold)
                    {
                        ReceiveData = new byte[com.BytesToRead];
                        com.Read(ReceiveData, 0, com.BytesToRead);
                        break;
                    }
                    else
                        System.Threading.Thread.Sleep(DelayTime);

                }
                if (CheckCount >= LoopCount)
                {
                    CheckCount = 0;
                    ErrCode = "001";
                    return false;
                }
                if (ReceiveData.Length != 7)
                {
                    ErrCode = "003";
                    return false;
                }
                else
                {
                    Value = ReceiveData[5] * 256 + ReceiveData[4];
                    return true;
                }

            }
            catch (System.Exception)
            {
                ErrCode = "005";
                return false;
            }

        }


        //public bool InitMeter(string DeviceType,string ConnType,string Addr)
        //{
        //    try
        //    {
        //        if (ConnType == "Keysight库")
        //        {

        //            ks = new keysight();
        //            ks.DeviceType = DeviceType;
        //            ks.ConnectAdr = Addr;
        //            ks.InitConnect();
        //        }
        //        else
        //        {
        //           string []Addrs =Addr.Split('|');
        //            device = new NI();
        //            device.DeviceType = DeviceType;
        //            device.ConnectAdr = Addr;
        //            device.InitConnect();
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return false;

        //    }

        //}

        //public bool RSTMeter(string ConnType)
        //{

        //    try
        //    {
        //        if (ConnType == "Keysight库")
        //        {

        //             ks.RSTDevice();
        //        }
        //        else
        //        {
        //            ks.RSTDevice();
        //        }
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return false;

        //    }

        //}

        //public bool ConfMeter(string ConnType,string MeasType,int Range)
        //{
        //    try
        //    {
        //        if (ConnType == "Keysight库")
        //        {
        //            if (ks.ConfDevice(MeasType,Math.Abs (Range)))
        //                return true;
        //        }
        //        else
        //        {
        //            if (device.ConfDevice(MeasType, Math.Abs(Range)))
        //                return true;
        //        }
        //        return false;

        //    }
        //    catch (Exception)
        //    {

        //        return false;

        //    }
        //}

        //public bool ReadMeterValue(string ConnType, out double Vol)
        //{
        //    Vol = 0.0f;
        //    try
        //    {
        //        if (ConnType == "Keysight库")
        //        {
        //            if (ks.ReadValue( out Vol))
        //                return true;
        //        }
        //        else
        //        {
        //            if (device.ReadValue(out Vol))
        //                return true;
        //        }
        //        return false;

        //    }
        //    catch (Exception)
        //    {

        //        return false;

        //    }
        //}

        public int WriteRead(string a, out int b)
        {
            b = -1;
            try
            {
                com.WriteLine(a);
                b = 1;
                return b;
            }
            catch (Exception)
            {
                return b;
            }

        }

    }
}
