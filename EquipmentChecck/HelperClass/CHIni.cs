using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace TestSystem_Pack
{
    class CHIni
    {
        private string strReadDefault_;
        private string strFilePath_;
        private readonly int M_L_READ_MAX_LEN = 512;
        //ini api
        /// <summary>  
        /// 获取某个指定节点(Section)中所有KEY和Value  
        /// </summary>  
        /// <param name="lpAppName">节点名称</param>  
        /// <param name="lpReturnedString">返回值的内存地址,每个之间用\0分隔</param>  
        /// <param name="nSize">内存大小(characters)</param>  
        /// <param name="lpFileName">Ini文件</param>  
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string in_sDefault, StringBuilder retVal, int size, string filePath);

        public CHIni()
        {
            ReadDefault_ = "INI_EMPTY";
        }

        //ini read default
        public string ReadDefault_
        {
            get { return strReadDefault_; }
            set { strReadDefault_ = value; }
        }
        //ini file path
        public string FilePath_
        {
            get { return strFilePath_; }
            set { strFilePath_ = value; }
        }

        public bool CHWriteIni(string in_sSection, string in_sKey, string in_sVal)
        {
            if (0 == WritePrivateProfileString(in_sSection, in_sKey, in_sVal, strFilePath_))
                return false;
            return true;
        }

        public bool CHReadIni(string in_sSection, string in_sKey, out string in_sVal)
        {
            in_sVal = "";
            StringBuilder strReadBuilder = new StringBuilder(M_L_READ_MAX_LEN);
            if (null == strReadBuilder)
                return false;

            GetPrivateProfileString(in_sSection, in_sKey, strReadDefault_, strReadBuilder, M_L_READ_MAX_LEN, strFilePath_);

            in_sVal = strReadBuilder.ToString();
            if (in_sVal == strReadDefault_)
                return false;

            return true;
        }

        public bool CHWriteIniInt(string in_sSection, string in_sKey, int in_nVal)
        {
            return CHWriteIni(in_sSection, in_sKey, in_nVal.ToString());
        }

        public bool CHReadIniInt(string in_sSection, string in_sKey, out int in_nVal)
        {
            in_nVal = 0;
            string strRead = "";
            if (!CHReadIni(in_sSection, in_sKey, out strRead))
                return false;
            if (!int.TryParse(strRead, out in_nVal))
                return false;
            return true;
        }
        public string[] INIGetAllItems(string section)
        {
            //返回值形式为 key=value,例如 Color=Red  
            uint MAX_BUFFER = 32767;    //默认为32767  
            string[] items = new string[0];      //返回值  

            //分配内存  
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));
            uint bytesReturned = GetPrivateProfileSection(section, pReturnedString, MAX_BUFFER, strFilePath_);
            if (!(bytesReturned == MAX_BUFFER - 2) || (bytesReturned == 0))
            {
                string returnedString = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned);
                items = returnedString.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            Marshal.FreeCoTaskMem(pReturnedString);     //释放内存  
            return items;
        }
    }
}
