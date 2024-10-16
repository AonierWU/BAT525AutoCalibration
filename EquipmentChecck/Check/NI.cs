using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.NI4882;
using System.IO.Ports;
using System.Net.Sockets;
using System.Threading;
using System.Net;


namespace TestSystem_Pack
{
    public class NI
    {
        private string connectAdr;
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

        private string deviceType;
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
      
        
        //NI
        private Device device;

        public  bool RSTDevice()
        {
            try
            {
                
                device.Write("*RST"); 
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public  bool InitConnect()
        {
            try
            {
                string[] addr = connectAdr.Split('|');
                device = new Device(Convert.ToInt32 (addr[0]),Convert.ToByte (addr[1]),Convert.ToByte(addr[2]),TimeoutValue.T10s);
               
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public  bool CloseConnect()
        {
            try
            {
               device.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
                if (deviceType == "Fluke8508A")
                {
                    if (ReadType == "VOLT")
                    {
                        CMD = "DCV " + RangeValue  + ",RESL8,FAST_ON,TWO_WR";

                    }
                    else if (ReadType == "CURR")
                    {
                        CMD = "DCI " + RangeValue + ",RESL7,FAST_ON";
                    }
                    else
                        return false;
                }
                else
                { 
                    CMD = "CONF:" + ReadType + ":DC " + RangeValue + ", MIN";
                }

                device.Write(CMD);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool ReadValue(out double dblValue)
        {
            dblValue = 0.0f;
            string Cmd = "";
            try
            {
                if (deviceType == "Fluke8508A")
                {
                    Cmd = "RDG?";
                }
                else
                {
                    Cmd = "READ?";
                }
                device.Write(Cmd);
                string strVolt = device.ReadString();
                dblValue = Convert.ToDouble(strVolt);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

       
        public  bool TestConnect(out string strInfo)
        {
            strInfo = "Connect Fail";
            try
            {
                device.Write("*IDN?");
                strInfo = device.ReadString();
                device.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        
    }
}
