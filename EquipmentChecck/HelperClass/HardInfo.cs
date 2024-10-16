//类HardInfoClass代码如下  

using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Management; //需要在解决方案中引用System.Management.DLL文件  
using System.IO;

namespace TestSystem_Pack
{
    /// <summary>  
    /// HardInfoClass 的摘要说明。  
    /// </summary>  
    public class HardInfoClass
    { 
        [DllImport("kernel32.dll")]
         private static extern int GetVolumeInformation(
         string lpRootPathName,
         string lpVolumeNameBuffer,
         int nVolumeNameSize,
         ref int lpVolumeSerialNumber,
         int lpMaximumComponentLength,
         int lpFileSystemFlags,
         string lpFileSystemNameBuffer,
         int nFileSystemNameSize
         );

        public HardInfoClass()
        {
            //  
            // TODO: 在此处添加构造函数逻辑  
            //  
        }

        /// <summary>
        /// 取机器名 
        /// </summary>
        /// <returns></returns>
        public string GetHostName()
        {
            return System.Net.Dns.GetHostName();
        }

        //取CPU编号   
        public string GetCpuID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                string strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            }
            catch
            {
                return "";
            }

        }
        
        public string GetCpuName()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                string strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo["Name"].ToString();
                    break;
                }
                return strCpuID;
            }
            catch
            {
                return "";
            }

        }
        
        //取第一块硬盘编号   
        public string GetHardDiskID()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                string strHardDiskID = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    strHardDiskID = mo["SerialNumber"].ToString().Trim();
                    break;
                }
                return strHardDiskID;
            }
            catch
            {
                return "";
            }
        }
       
        //获取网卡MAC地址  
        public string GetNetCardMAC()
        {
            try
            {
                string stringMAC = "";
                ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection MOC = MC.GetInstances();

                foreach (ManagementObject MO in MOC)
                {
                    if ((bool)MO["IPEnabled"] == true)
                    {
                        stringMAC += MO["MACAddress"].ToString();

                    }
                }
                return stringMAC;
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// 获取硬盘信息的代码  
        /// </summary>
        /// <param name="drvID"></param>
        /// <returns></returns>
        public string GetVolOf(string drvID)
        {
            try
            {
                const int MAX_FILENAME_LEN = 256;
                int retVal = 0;
                int a = 0;
                int b = 0;
                string str1 = null;
                string str2 = null;


                int i = GetVolumeInformation(
                 drvID + @":/",
                 str1,
                 MAX_FILENAME_LEN,
                 ref retVal,
                 a,
                 b,
                 str2,
                 MAX_FILENAME_LEN
                 );

                return retVal.ToString("x");
            }
            catch
            {
                return "";
            }
        }


        //获取当前网卡IP地址  
        public string GetNetCardIP()
        {
            try
            {
                string stringIP = "";
                ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection MOC = MC.GetInstances();

                foreach (ManagementObject MO in MOC)
                {
                    if ((bool)MO["IPEnabled"] == true)
                    {
                        string[] IPAddresses = (string[])MO["IPAddress"];
                        if (IPAddresses.Length > 0)
                            stringIP = IPAddresses[0].ToString();

                    }
                }
                return stringIP;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 判断是否为64操作系统
        /// </summary>
        /// <returns></returns>
        public string GetOSIs64bit()
        {
            string is64Bit = String.Empty;

            try
            {
                is64Bit = Environment.Is64BitOperatingSystem.ToString();
                return is64Bit;
            }
            catch (Exception)
            {
                return "";
            }
        
         }

        /// <summary>
        /// 获取内存容量
        /// </summary>
        /// <returns></returns>
        public string GetMemory()
        {
            
            string stringMemory = "";

            ManagementClass mc = new
            ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            try
            {
                foreach (ManagementObject mo in moc)
                {
                    stringMemory = (((UInt64) mo["TotalPhysicalMemory"]) / Math.Pow(2, 30)).ToString("f2");
                }
                return stringMemory+"GB";
            }
            catch (Exception)
            {
                return "";
            }
         }

        /// <summary>
        /// 获取操作系统名字
        /// </summary>
        /// <returns></returns>
        public string GetOSName()
        {
            string OSName = "";
            try
            {
                ManagementClass manag = new ManagementClass("Win32_OperatingSystem");
                ManagementObjectCollection managCollection = manag.GetInstances();

                foreach (ManagementObject m in managCollection)
                {
                    OSName = m["Name"].ToString();
                    break;
                }

                if (OSName.Contains("|"))
                    OSName = OSName.Substring(0, OSName.IndexOf('|'));
                return OSName;
            }
            catch (Exception)
            {

                return "";
            }
            

        }

        /// <summary>
        /// 屏幕分辨率
        /// </summary>
        /// <returns></returns>
        public string DesktopMonitor()
        {
            try
            {
                string DesktopMonitor = "";
                ManagementClass manage = new ManagementClass("Win32_DesktopMonitor");
                ManagementObjectCollection manageCollection = manage.GetInstances();
                foreach (ManagementObject m in manageCollection)
                {
                    DesktopMonitor = m["ScreenWidth"].ToString() + "*" + m["ScreenHeight"].ToString();
                    break;
                }
                return DesktopMonitor;
            }
            catch (Exception)
            {

                return "";
            }
           

        }

        /// <summary>
        /// 显卡芯片和显存
        /// </summary>
        /// <returns></returns>
        public string VideoAndAdapterRAM()
        {
            try
            {
                string strResult = "";
                ManagementClass manage = new ManagementClass("Win32_VideoController");
                ManagementObjectCollection manageCollection = manage.GetInstances();
                foreach (ManagementObject m in manageCollection)
                {
                    strResult = m["VideoProcessor"].ToString().Replace("Family", "")+ "/" + ((Convert.ToInt64(m["AdapterRAM"]))/Math.Pow(2,30)).ToString("f2")  +"GB";
                    break;
                }
                return strResult;
            }
            catch (Exception)
            {

                return "";
            }
           

        }
     
        /// <summary>
        /// 获取硬盘分区
        /// </summary>
        /// <returns></returns>
        public string GetDirver()
        {
            try
            {
                string[] s1 = Directory.GetLogicalDrives();
                string strValues = "";
                foreach (string s in s1)
                    strValues += s + ",";
                return strValues.TrimEnd(',');
            }
            catch (Exception)
            {
                return "";
              
            }
           
        }
    }
}
