using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivi.Visa.Interop;
using System.IO.Ports;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace TestSystem_Pack
{
    public class keysight
    {
        private string connectAdr;
        private string connectAdrDCsource;
        private bool ReadRes = false;
        public string ConnectAdr
        {
            get
            {
                return connectAdr;
            }
            set
            {
                connectAdr = value;
            }
        }
        public string ConnectAdrDCsource
        {
            get
            {
                return connectAdrDCsource;
            }
            set
            {
                connectAdrDCsource = value;
            }
        }
        private string deviceType;
        private string deviceTypeDCsource;
        public string DeviceType
        {
            get
            {
                return deviceType;
            }
            set
            {
                deviceType = value;
            }
        }
        public string DeviceTypeDCsource
        {
            get
            {
                return deviceTypeDCsource;
            }
            set
            {
                deviceTypeDCsource = value;
            }
        }
        static SerialPort _com = new SerialPort();  //安捷伦COM
        Com myCom = new Com(_com);
        private string comPort;
        public string ComPort
        {
            get
            {
                return comPort;
            }
            set
            {
                comPort = value;
            }
        }
        //(keysight)
        ResourceManager rm = new ResourceManager();
        ResourceManager rmDCsource = new ResourceManager();
        FormattedIO488 Instrument = new FormattedIO488();
        FormattedIO488 InstrumentDCsource = new FormattedIO488();


        public bool RSTDevice()
        {
            try
            {
                if (deviceType == "34401A")
                {
                    int receiveData = 0;
                    string b = "*RST";
                    myCom.WriteRead(b, out receiveData);
                    Thread.Sleep(200);
                    if (receiveData != 1)
                        return false;
                    else
                        return true;

                }
                else
                {
                    Instrument.WriteString("*RST");
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InitConnect()
        {
            try
            {
                if (deviceType == "34401A")
                {
                    keysightOpenCom(comPort);
                    int receiveData = 0;
                    string a = "SYSTem:REMote", b = "*CLS", c = "TRIG:SOUR IMM", d = "CONF:VOLT:DC";
                    string E = "MEAS:VOLT:DC? 0.6\r";//CONF:CURR:DC
                    myCom.WriteRead(b, out receiveData);
                    Thread.Sleep(200);
                    myCom.WriteRead(a, out receiveData);
                    Thread.Sleep(300);
                    myCom.WriteRead(c, out receiveData);
                    Thread.Sleep(30);
                    myCom.WriteRead(d, out receiveData);
                    Thread.Sleep(30);
                    myCom.com.WriteLine(E);
                    System.Threading.Thread.Sleep(300);
                    string fff = myCom.com.ReadLine();
                }
                else
                {
                    Instrument.IO = (IMessage)rm.Open(connectAdr);
                    Instrument.IO.Timeout = 10000;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool InitConnectDCsource()
        {
            try
            {
                InstrumentDCsource.IO = (IMessage)rmDCsource.Open(connectAdrDCsource);
                InstrumentDCsource.IO.Timeout = 10000;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool CloseConnect()
        {
            try
            {
                if (deviceType == "34401A")
                {
                    keysightCloseCom();
                }
                else
                {
                    Instrument.IO.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 配置设备测量类型、量程ReadType="VOLT"||"CURR"
        /// </summary>
        /// <param name="ReadType"></param>
        /// <param name="RangeValue"></param>
        /// <returns></returns>
        public bool ConfDevice(string ReadType, string RangeValue)
        {
            try
            {
                //电压量程：
                //34401A：100mV,1V,10V,100V,1000V|AUTO
                //34461A: 100 mV|1 V|10 V|100 V|1K V| AUTO(自动调整量程)。
                //8588A:100 mV、1 V、10 V、100 V 和 1 kV。
                //8508A:
                //电流量程
                //34401A:10mA，100mA，1A，10A|AUTO
                //34461A:100 µA|1 mA|10 mA|100 mA|1 A|3 A|10 A|AUTO
                //8588A:10 μA、100 μA、1 mA、10 mA、100 mA、1 A、10 A 和 30 A
                //8508A:
                string CMD = "";
                if (deviceType == "34401A")
                {
                    int receiveData = 0;
                    CMD = "CONF:" + ReadType + ":DC " + RangeValue;//默认采集
                    myCom.WriteRead(CMD, out receiveData);
                    Thread.Sleep(200);
                    if (receiveData != 1)
                        return false;
                    else return true;
                }
                else
                {
                    //CMD = "CONF:" + ReadType + ":DC " + (RangeValue/1000.0) + ", DEF";
                    CMD = "CONF:" + ReadType + ":DC " + RangeValue + ", DEF";
                    Instrument.WriteString(CMD);
                    return true;
                }
                
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ConfRes()
        {
            try
            {
                string CMD = "CONF:RES";
                if (deviceType == "34401A")
                {
                    ReadRes = true;
                    int receiveData = 0;
                    myCom.WriteRead(CMD, out receiveData);
                    Thread.Sleep(200);
                    if (receiveData != 1)
                        return false;
                    else return true;
                }
                else
                {

                    Instrument.WriteString(CMD, true);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool SetVolandOutPut(double Vol, double Cur, string Ch, bool en = false)
        {
            try
            {
                int i = 0;
                double setCur = 0.0f;
                if (en)
                {
                    Vol = Vol / 1000;
                    Cur = Cur / 1000;
                    string Data1 = "APPL" + " " + Ch + "," + Vol + "," + Cur;
                    InstrumentDCsource.WriteString(Data1, true);
                    switch (Ch)
                    {
                        case "CH1":
                            i = 1;
                            break;
                        case "CH2":
                            i = 2;
                            break;
                        case "CH3":
                            i = 3;
                            break;
                        default: break;
                    }
                    string Data2 = "OUTP ON" + "," + "(@" + i + ")";
                    InstrumentDCsource.WriteString(Data2, true);
                    return true;
                }
                else
                {
                    switch (Ch)
                    {
                        case "CH1":
                            i = 1;
                            setCur = 0.002;
                            break;
                        case "CH2":
                            i = 2;
                            setCur = 0.001;
                            break;
                        case "CH3":
                            i = 3;
                            setCur = 0.001;
                            break;
                        default: break;
                    }
                    string Data2 = "OUTP OFF" + "," + "(@" + i + ")";
                    InstrumentDCsource.WriteString(Data2, true);
                    string Data1 = "APPL" + " " + Ch + ",0," + setCur; ;
                    InstrumentDCsource.WriteString(Data1, true);
                    return true;
                }
            }
            catch (Exception) { return false; }
        }

        public bool ReadValue(out double dblValue)
        {
            dblValue = 0.0f;
            try
            {
                if (deviceType == "34401A")
                {
                    if (ReadRes)
                    {
                        int receiveData = 0;
                        string b = "MEAS:RES? AUTO,MIN\r";
                        myCom.WriteRead(b, out receiveData);
                        Thread.Sleep(100);
                        dblValue = Convert.ToDouble(myCom.com.ReadLine());
                        if (receiveData != 1)
                            return false;
                        else
                            return true;
                    }
                    else
                    {
                        int receiveData = 0;
                        //string b = "MEAS:VOLT:DC? 10\r";//CONF:CURR:DC "MEAS:VOLT:DC? 0.6\r"
                        //string b = "MEAS:VOLT:DC? "+ Range + "\r";
                        string b = "MEAS:VOLT:DC? AUTO\r";
                        myCom.WriteRead(b, out receiveData);
                        Thread.Sleep(100);
                        dblValue = Convert.ToDouble(myCom.com.ReadLine());
                        if (receiveData != 1)
                            return false;
                        else
                            return true;
                    }
                }
                else
                {
                    Instrument.WriteString("SAMP:COUN 1");
                    Instrument.WriteString("READ?");
                    string strVolt = Instrument.ReadString();
                    dblValue = Convert.ToDouble(strVolt);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TestConnect(out string strInfo)
        {
            strInfo = "Connect Fail";
            try
            {
                if (deviceType == "34401A")
                {
                    keysightOpenCom(comPort);
                    int receiveData = 0;
                    string E = "*IDN?";//CONF:CURR:DC
                    myCom.WriteRead(E, out receiveData);
                    Thread.Sleep(200);
                    strInfo = Convert.ToString(myCom.com.ReadLine());
                    if (receiveData != 1)
                        return false;
                    else
                        return true;
                }
                else
                {
                    Instrument.WriteString("*IDN?", true);
                    strInfo = Instrument.ReadString();

                    Instrument.IO.Close();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ReadValueCur(out double dblValue)
        {
            dblValue = 0.0f;
            try
            {
                if (deviceType == "34401A")
                {
                    int receiveData = 0;
                    myCom.WriteRead("SAMP:COUN 1", out receiveData);
                    string b = "MEAS:CURR:DC? AUTO\r";//CONF:CURR:DC
                    myCom.WriteRead(b, out receiveData);
                    //myCom.WriteRead("READ?", out receiveData);
                    Thread.Sleep(100);
                    dblValue = Convert.ToDouble(myCom.com.ReadLine());
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int keysightOpenCom(string ComNum)
        {
            try
            {
                if (!myCom.InitCom(9600, 8, ComNum))
                    return -1;
                if (!myCom.OpenCom())
                    return -1;
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public int keysightCloseCom()
        {
            try
            {
                if (!myCom.CloseCom())
                    return -1;
                return 1;
            }
            catch
            {
                return -1;
            }
        }
    }

}
