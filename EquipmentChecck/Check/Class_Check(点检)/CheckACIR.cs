using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static TestSystem_Pack.MyEqmCmd;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {

        public void ReadACIR(string DeviceType, string TesterID, string CheckType, double intSetRes, double ResAcc, double EqmCurValue, bool CutRange, out bool isNG)
        {
            double Res = 0;
            double ResDMM = 0;
            //string strErrorCode = "";
            string Result = "";
            double Err1 = 0;
            //double Err2 = 0;
            double ReadingCurr = 0.0f;
            byte Rang = 0x00;
            isNG = false;
            try
            {
                //double RangeValue = intSetRes;
                if (!com.DCIRCal_RY((int)intSetRes, true))
                    return;
                if (!com.RY3Enable_RY(true))
                    return;
                System.Threading.Thread.Sleep(100);
                if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingCurr))
                    return;
                if (!com.RY3Enable_RY(false))
                    return;
                System.Threading.Thread.Sleep(100);
                if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                {
                    if (intSetRes <= 100)
                    {
                        Rang = 0X04;
                    }
                    else
                    {
                        Rang = 0X05;
                    }
                }
                if (CutRange)
                {
                    Rang = 0X05;
                }
                if (!com.MixReadDCIRValue(Rang, out Res))
                    return;
                if (!com.DCIRCal_RY((int)intSetRes, false))
                    return;
                ReadingCurr = Math.Abs(ReadingCurr) * 1000;
                ResDMM = (ReadingCurr / Math.Abs(EqmCurValue)) * 1000;
                Err1 = ResDMM - Res;
                if (Math.Abs(Err1) < ResAcc)
                { Result = "¡Ì"; }
                else
                { Result = "¡Á"; isNG = true; }

                UpdateUidelegate(DeviceType, TesterID, CheckType, intSetRes.ToString(), ReadingCurr.ToString("f2"), Res.ToString("f2"), Err1.ToString("f2"), "", ResAcc.ToString(), Result);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                com.DCIRCal_RY((int)intSetRes, false);
            }


        }

    }
}
