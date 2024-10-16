using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool WriteSNCodeCali(string Box, string SNCodeNum, string CheckType, string DeviceType, bool CaliType, out string Point, out string SNCode)
        {
            Point = "未设初始值";
            SNCode = "";
            string Result = "√";
            string strEqmType;
            string strDateTime;
            string strSNnum;
            string[] strSplit=new string[] { };
            try
            {
                if (CaliType)
                    strSplit = SNCodeNum.Split('-');
                if (CaliType)
                {
                    if (!StopStatus)
                    {
                        if (com.MixWriteSNCode(strSplit[0], strSplit[1], strSplit[2], strSplit[3]))
                        {
                            Result = "√";
                            UpdateUidelegate(DeviceType, strSplit[2], CheckType, strSplit[0] + strSplit[1] + strSplit[2], "", "", "", "", "", Result); ;
                        }
                        else
                        {
                            Point = "设备编码写入_NG!";
                            return false;
                        }
                    }
                }
                if (!StopStatus)
                {
                    if (com.MixReadSNCode(out strEqmType, out strDateTime, out strSNnum))
                    {
                        if (CaliType)
                        {
                            if (strEqmType == strSplit[0] && strDateTime == strSplit[1] && strSNnum == strSplit[2])
                                Result = "√";
                            else
                                Result = "×";
                        }
                        else
                        {
                            if (strEqmType == DeviceType)
                                Result = "√";
                            else
                            {
                                Point = "校准设备与连接设备不符!";
                                Result = "×";
                            }
                        }
                        SNCode = strEqmType + strDateTime + strSNnum;
                        UpdateUidelegate(DeviceType, strSNnum, "设备编码读取", "", "", SNCode, "", "", "", Result);
                        if (Result == "×")
                            return false;
                    }
                    else
                    {
                        Point = "设备编码读取失败!";
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
