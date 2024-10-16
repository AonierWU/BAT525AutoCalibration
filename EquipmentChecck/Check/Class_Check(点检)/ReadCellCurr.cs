using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using static TestSystem_Pack.MyEqmCmd;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {

        public void ReadCellCurrent(string DeviceType, string TesterID, int intSetCurrent,  string judge, double CurAcc,out bool isNG)
        {
            double dblCurr = 0.0f;
            double ReadingCurr = 0.0f;

            //int nSetCellVolt = 3000;
            //int nWaitTime = 300;
         
            //double dblVoltage = 0.0f;

            //string strErrorCode="";
            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            isNG = false;
            try
            {
                if (!com.MultimeterCur10A(false))
                    return;
                if (!com.ConfMeter(frmVerifyDevice.strConnType, "CURR", "3"))
                    return;
                if (intSetCurrent >= 200)
                {
                    if (frmVerifyDevice.strMultimeterType != "34401A")
                    {
                        if (!com.ConfMeter(frmVerifyDevice.strConnType, "CURR", "10"))
                            return;
                        if (!com.MultimeterCur10A(true))
                            return;
                    }
                }
                if (!com.MixSetCellCurValue(intSetCurrent))
                    return;
                System.Threading.Thread.Sleep(100);
                if (frmVerifyDevice.strMultimeterType == "34401A")
                {
                    if (!com.ReadCurValue(frmVerifyDevice.strConnType, out ReadingCurr))
                        return;
                }
                else
                {
                    if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingCurr))
                        return;
                }
                if (!com.MixReadCellCurValue(out dblCurr))
                    return;
                ReadingCurr = ReadingCurr * 1000.0;//单位换算为：mA
                switch (judge)
                {
                    case "设置值、万用表、设备读值":
                        Err1 = dblCurr - intSetCurrent;
                        Err2 = ReadingCurr - intSetCurrent;
                        if (Math.Abs(Err1) < CurAcc && Math.Abs(Err2) < CurAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "设置值、万用表":
                        Err1 = ReadingCurr - intSetCurrent;
                        if (Math.Abs(Err1) < CurAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "设置值、设备读值":
                        Err1 = dblCurr - intSetCurrent;
                        if (Math.Abs(Err1) < CurAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "万用表、设备读值":
                        Err1 = ReadingCurr - dblCurr;
                        if (Math.Abs(Err1) < CurAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                }
                UpdateUidelegate(DeviceType, TesterID, "Cell电流点检", intSetCurrent.ToString(), ReadingCurr.ToString("f2"), dblCurr.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), CurAcc.ToString(), Result);

            }
            catch (Exception)
            {
                throw;
            }
            finally 
            {
            }
        }

    }
}
