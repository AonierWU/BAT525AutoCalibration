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
        public void ReadChgCurrent(string DeviceType,string TesterID,string CheckType, int intSetCurr,int SampleRes,bool blUseRes,string judge,double CurAcc)
        {
            double dblCurrent = 0.0f;
            double ReadingCurr = 0.0f;
            //int nWaitTime = 500;
            //int intSetVolt = 8000;//
            //string strErrorCode = "";
            //double dblVol1 = 0.0f;

            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            try
            {
                if (!blUseRes)
                {
                    if (!com.ConfMeter(frmVerifyDevice.strConnType, "CURR", intSetCurr.ToString()))
                        return;
                }
                else
                {
                    if (!com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", (intSetCurr * (SampleRes / 1000.0)).ToString()))
                        return;
                }

                if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingCurr))
                    return;
                if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingCurr))
                    return;

                if (blUseRes)//读取的值为电压值
                {
                    ReadingCurr = ReadingCurr / (SampleRes / 1000.0);
                }

                ReadingCurr = ReadingCurr * 1000.0;//单位换算为：mA
                switch (judge)
                {
                    case "设置值、万用表、设备读值":
                        Err1 = dblCurrent - intSetCurr;
                        Err2 = ReadingCurr - intSetCurr;
                        if (Math.Abs(Err1) < CurAcc && Math.Abs(Err2) < CurAcc)
                            Result = "√";
                        else
                            Result = "×";
                        break;
                    case "设置值、万用表":
                        Err1 = ReadingCurr - intSetCurr;
                        if (Math.Abs(Err1) < CurAcc)
                            Result = "√";
                        else
                            Result = "×";
                        break;
                    case "设置值、设备读值":
                        Err1 = dblCurrent - intSetCurr;
                        if (Math.Abs(Err1) < CurAcc)
                            Result = "√";
                        else
                            Result = "×";
                        break;
                    case "万用表、设备读值":
                        Err1 = ReadingCurr - dblCurrent;
                        if (Math.Abs(Err1) < CurAcc)
                            Result = "√";
                        else
                            Result = "×";
                        break;
                }
                UpdateUidelegate(DeviceType, TesterID, CheckType, intSetCurr.ToString(), ReadingCurr.ToString("f2"), dblCurrent.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), CurAcc.ToString(), Result);
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
