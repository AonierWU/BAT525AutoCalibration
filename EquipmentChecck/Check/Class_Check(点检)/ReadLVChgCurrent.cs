using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static TestSystem_Pack.MyEqmCmd;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public void ReadLVChgCurrent(string DeviceType, string TesterID, int intSetCurr, string judge, double CurAcc,out bool isNG)
        {
            double dblCurrent = 0.0f;
            double ReadingCurr = 0.0f;
            //int nWaitTime = 500;
            //int intSetVolt = 3000;//
            //string strErrorCode = "";
            //double dblVol1 = 0.0f;

            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            isNG = false;
            try
            {
                if (intSetCurr >= 200)
                {
                    if (frmVerifyDevice.strMultimeterType != "34401A")
                    {
                        if (!com.ConfMeter(frmVerifyDevice.strConnType, "CURR", "10"))
                            return;
                        if (!com.MultimeterCur10A(true))
                            return;
                    }
                }
                else
                {
                    if (!com.MultimeterCur10A(false))
                        return;
                    if (!com.ConfMeter(frmVerifyDevice.strConnType, "CURR", "3"))
                        return;
                }
                if (!com.MixSetLoadPartCurValue(intSetCurr))
                    return;
                if (!com.MixLoadPartEnable(0x01))
                    return;
                System.Threading.Thread.Sleep(300);
                if (frmVerifyDevice.strMultimeterType == "34401A")
                {
                    if (!com.ReadCurValue(frmVerifyDevice.strConnType, out ReadingCurr))
                        return;
                    if (intSetCurr - (ReadingCurr * 1000) > CurAcc)
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            if (!com.ReadCurValue(frmVerifyDevice.strConnType, out ReadingCurr))
                                return;
                            if (intSetCurr - (ReadingCurr * 1000) > CurAcc)
                            {
                                continue;
                            }
                            else
                                break;
                        }
                    }
                }
                else
                {
                    if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingCurr))
                        return;
                    if (intSetCurr - (ReadingCurr * 1000) > CurAcc)
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingCurr))
                                return;
                            if (intSetCurr - (ReadingCurr * 1000) > CurAcc)
                            {
                                continue;
                            }
                            else
                                break;
                        }
                    }
                }
                if (!com.MixReadLoadPartCurValue(out dblCurrent)) 
                    return;
                if (!com.MixLoadPartEnable(0x00))
                    return;
                ReadingCurr = ReadingCurr * 1000.0;//单位换算为：mA
                switch (judge)
                {
                    case "设置值、万用表、设备读值":
                        Err1 = ReadingCurr - intSetCurr;
                        Err2 = dblCurrent - ReadingCurr;
                        if (Math.Abs(Err1) < CurAcc && Math.Abs(Err2) < CurAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "设置值、万用表":
                        Err1 = ReadingCurr - intSetCurr;
                        if (Math.Abs(Err1) < CurAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "设置值、设备读值":
                        Err1 = dblCurrent - intSetCurr;
                        if (Math.Abs(Err1) < CurAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "万用表、设备读值":
                        Err1 = ReadingCurr - dblCurrent;
                        if (Math.Abs(Err1) < CurAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                }
                UpdateUidelegate(DeviceType, TesterID, "LV充电电流点检", intSetCurr.ToString(), ReadingCurr.ToString("f2"), dblCurrent.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), CurAcc.ToString(), Result);




            }
            catch (Exception) 
            {
                
                throw;
            }

            finally
            {
                //com.MixLoadPartChgDsg(SelectChgDsgMode.CHG, 0, 0, nWaitTime, true, ReadFlag.CURR, out dblVol1, out dblCurrent, out strErrorCode);
                //com.MixLoadPartEnable(0x00);

            }
        }

}
}
