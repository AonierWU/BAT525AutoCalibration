using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public void ReadSNCode(string DeviceType,  string CheckType, out bool isNG,out string strSNCode,out string msg)
        {
            isNG = false;
            string Result = "√";
            string strEqmType;
            string strDateTime;
            string strSNnum;
            strSNCode = "";
            msg = "";
            try
            {
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    if (!com.MixReadSNCode(out strEqmType, out strDateTime, out strSNnum))
                    { 
                        if(strEqmType=="-1")
                        {
                            msg = "回读条码长度异常!";
                        }
                        return; 
                    }
                    if(strEqmType!=DeviceType)
                    {
                        Result = "×";
                        isNG = true;
                        msg = "校准设备类型与连接设备不一致!";
                    }
                    strSNCode = strEqmType + strDateTime + strSNnum;
                    UpdateUidelegate(DeviceType, strSNnum, CheckType, "", "", strSNCode, "", "", "", Result);
                }
                else
                    return;
            }
            catch
            {
                return;
            }
        }
    }
}
