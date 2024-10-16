using NPOI.SS.Formula.Functions;
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
        public void ReadDsgCurrent(string DeviceType, string TesterID, string CheckType, int intSetCurr, string ResType, bool blUseRes,bool ExtRes, string judge, double CurAcc, out bool isNG)
        {
            // DiffResult = false;
            double dblCurrent = 0.0f;
            double ReadingCurr = 0.0f;
            byte Rang=0x00;

            int SampleRes = 0;
            //int nWaitTime = 500;
            //int intSetVolt = 8000;
            //string strErrorCode = "";
            //double dblVol1 = 0.0f;
            //double dblVol2 = 0.0f;
            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            isNG = false;
            try
            {
                if (CheckType == "放电电流3A点检")
                { Rang = 0x00; }
                else
                {
                    Rang = 0x01;
                }
                if (!blUseRes)
                {
                    if (!com.MultimeterCur10A(false))
                        return;
                    if (!com.ConfMeter(frmVerifyDevice.strConnType, "CURR", "3"))
                        return;
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
                    if (!com.MixSetCurRange(Rang))
                        return;
                }
                else
                {
                    if (ExtRes)
                    {
                        if (ResType == "100mΩ")
                            SampleRes = 100;
                        else if (ResType == "10mΩ")
                            SampleRes = 10;
                        else if (ResType == "1mΩ")
                            SampleRes = 1;
                        else if (ResType == "2mΩ")
                            SampleRes = 2;
                    }
                    else
                    {
                        SampleRes = 1;
                    }
                    if (!com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", "AUTO"))
                        return;
                    if (!com.MixSetCurRange(Rang))
                        return;
                    if (!com.MixSendChgVolt(0xC8, 0xAF))
                        return;
                }
                if (!com.MixSetCurValue(Rang, intSetCurr))
                    return;
                if (!com.MixEnable(0x01))
                    return;
                System.Threading.Thread.Sleep(200);
                if (!blUseRes)
                {
                    if (frmVerifyDevice.strMultimeterType == "34401A")
                    {
                        if (!com.ReadCurValue(frmVerifyDevice.strConnType, out ReadingCurr))
                            return;
                        if (Math.Abs( intSetCurr - (Math.Abs(ReadingCurr) * 1000)) > CurAcc)
                        {
                            for (int i = 0; i <= 5; i++)
                            {
                                if (!com.ReadCurValue(frmVerifyDevice.strConnType, out ReadingCurr))
                                    return;
                                if (Math.Abs(intSetCurr - (Math.Abs(ReadingCurr) * 1000)) > CurAcc)
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
                        if (Math.Abs(intSetCurr - (Math.Abs(ReadingCurr) * 1000)) > CurAcc)
                        {
                            for (int i = 0; i <= 5; i++)
                            {
                                if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingCurr))
                                    return;
                                if (Math.Abs(intSetCurr - (Math.Abs(ReadingCurr) * 1000)) > CurAcc)
                                {
                                    continue;
                                }
                                else
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingCurr))
                        return;
                    if (Math.Abs(intSetCurr - ((Math.Abs(ReadingCurr * 1000) / SampleRes) * 1000)) > CurAcc)
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingCurr))
                                return;
                            if (Math.Abs(intSetCurr - ((Math.Abs(ReadingCurr*1000) / SampleRes) * 1000)) > CurAcc)
                            {
                                continue;
                            }
                            else
                                break;
                        }
                    }
                }
                if (!com.MixReadCurValue(out dblCurrent))
                    return;

                if (!com.MixEnable(0x00))
                    return;
                if (blUseRes)//读取的值为电压值
                {
                    ReadingCurr = Math.Abs(ReadingCurr / SampleRes)*1000;//转换为A
                }
                ReadingCurr = (Convert.ToDouble(ReadingCurr) * 1000.0);//单位换算为：mA
                switch (judge)
                {
                    case "设置值、万用表、设备读值":
                        Err1 = dblCurrent - intSetCurr;
                        Err2 = ReadingCurr - intSetCurr;
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
                        {
                            Result = "√"; //DiffResult = true;
                        }
                        else
                        { Result = "×"; isNG = true; }
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
                //com.MixChgDsgTest(SelectChgDsgMode.DSG, 0, 0, nWaitTime, true, ReadFlag.CURR, out dblVol1, out dblCurrent, out strErrorCode);
            }
        }

    }
}
