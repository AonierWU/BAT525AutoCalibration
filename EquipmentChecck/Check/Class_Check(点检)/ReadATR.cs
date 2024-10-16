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
        public void ReadATR(string DeviceType, string TesterID, string IDType, int intSetRes, double ResAcc, bool CutRange, out bool isNG)
        {
            double Res = 0;
            //Checkdata.ItemName = IDType;
            //Checkdata.SetValue = intSetRes.ToString();
            //Checkdata.TesterValue = (Res).ToString("f2");
            //Checkdata.DmmValue = (intSetRes).ToString("f2");
            //Checkdata.ErrValue = (Res - intSetRes).ToString("f2");
            double chkResACC = 0;
            string CheckType = "";
            //string strErrorCode = "";
            byte NtcRange = 0x03;
            byte R = 0x00;
            string Result = "";
            double Err1 = 0;
            isNG = false;

            try
            {
                chkResACC = (intSetRes * ResAcc) / 100;
                if (!com.IDRResCal_RY(intSetRes, true))
                    return;
                double RangeValue = intSetRes / 1000.0;
                switch (IDType)
                {
                    case "NTC电阻_Ω":
                        CheckType = "NTC电阻点检";
                        break;
                    case "IDR电阻_Ω":
                        CheckType = "IDR电阻点检";
                        break;
                    default:
                        return;
                }
                switch (DeviceType)
                {
                    case "BAT525G":
                        if (RangeValue <= 2)
                        {
                            NtcRange = 0x00;
                            R = 0x00;
                        }
                        else if (RangeValue > 2 && RangeValue <= 20)
                        {
                            NtcRange = 0x01;
                            R = 0x00;
                        }
                        else if (RangeValue > 20 && RangeValue <= 200)
                        {
                            NtcRange = 0x02;
                            R = 0x00;
                        }
                        else if (RangeValue > 200 && RangeValue <= 3000)
                        {
                            NtcRange = 0x03;
                            R = 0x00;
                        }
                        else
                        {
                            return;
                        }
                        if (CutRange)
                        {
                            if (NtcRange < 3)
                            {
                                NtcRange += 1;
                            }
                        }
                        if (!com.MixNTCRange(NtcRange))//00--2000R，01--20K，02--200K, 03--3M
                            return;
                        System.Threading.Thread.Sleep(1000);
                        if (!com.MixReadNTCValue(R, NtcRange, out Res))
                            return;
                        if (!com.MixReadNTCValue(R, NtcRange, out Res))
                            return;
                        break;
                    case "BAT525H":
                        if (RangeValue <= 2)
                        {
                            NtcRange = 0x00;
                            R = 0x00;
                        }
                        else if (RangeValue > 2 && RangeValue <= 20)
                        {
                            NtcRange = 0x01;
                            R = 0x00;
                        }
                        else if (RangeValue > 20 && RangeValue <= 200)
                        {
                            NtcRange = 0x02;
                            R = 0x00;
                        }
                        else if (RangeValue > 200 && RangeValue <= 3000)
                        {
                            NtcRange = 0x03;
                            R = 0x00;
                        }
                        else
                        {
                            return;
                        }
                        if (CutRange)
                        {
                            if (NtcRange < 3)
                            {
                                NtcRange += 1;
                            }
                        }
                        if (!com.MixNTCRange(NtcRange))//00--2000R，01--20K，02--200K, 03--3M
                            return;
                        System.Threading.Thread.Sleep(1000);
                        if (!com.MixReadNTCValue(R, NtcRange, out Res))
                            return;
                        if (!com.MixReadNTCValue(R, NtcRange, out Res))
                            return;
                        break;
                    case "BAT525C":
                        if (RangeValue <= 1)
                        {
                            NtcRange = 0x00;
                            R = 0x00;
                        }
                        else if (RangeValue > 1 && RangeValue <= 10)
                        {
                            NtcRange = 0x01;
                            R = 0x00;
                        }
                        else if (RangeValue > 10 && RangeValue <= 100)
                        {
                            NtcRange = 0x02;
                            R = 0x00;
                        }
                        else if (RangeValue > 100 && RangeValue <= 1000)
                        {
                            NtcRange = 0x03;
                            R = 0x00;
                        }
                        else
                        {
                            return;
                        }
                        if (CutRange)
                        {
                            if (NtcRange < 3)
                            {
                                NtcRange += 1;
                            }
                        }
                        switch (CheckType)
                        {
                            case "NTC电阻点检":
                                if (!com.MixNTCRange(NtcRange))
                                    return;
                                System.Threading.Thread.Sleep(1000);
                                if (!com.MixReadNTCValue(R, NtcRange, out Res))
                                    return;
                                if (!com.MixReadNTCValue(R, NtcRange, out Res))
                                    return;
                                break;
                            case "IDR电阻点检":
                                if (!com.MixIDRRange(NtcRange))
                                    return;
                                System.Threading.Thread.Sleep(1000);
                                if (!com.MixReadIDRValue(out Res))
                                    return;
                                if (!com.MixReadIDRValue(out Res))
                                    return;
                                break;
                        }
                        break;
                    case "BAT525D":
                        if (RangeValue <= 1)
                        {
                            NtcRange = 0x00;
                            R = 0x00;
                        }
                        else if (RangeValue > 1 && RangeValue <= 10)
                        {
                            NtcRange = 0x01;
                            R = 0x00;
                        }
                        else if (RangeValue > 10 && RangeValue <= 100)
                        {
                            NtcRange = 0x02;
                            R = 0x00;
                        }
                        else if (RangeValue > 100 && RangeValue <= 1000)
                        {
                            NtcRange = 0x03;
                            R = 0x00;
                        }
                        else
                        {
                            return;
                        }
                        if (CutRange)
                        {
                            if (NtcRange < 3)
                            {
                                NtcRange += 1;
                            }
                        }
                        switch (CheckType)
                        {
                            case "NTC电阻点检":
                                if (!com.MixNTCRange(NtcRange))
                                    return;
                                System.Threading.Thread.Sleep(1000);
                                if (!com.MixReadNTCValue(R, NtcRange, out Res))
                                    return;
                                if (!com.MixReadNTCValue(R, NtcRange, out Res))
                                    return;
                                break;
                            case "IDR电阻点检":
                                if (!com.MixIDRRange(NtcRange))
                                    return;
                                System.Threading.Thread.Sleep(1000);
                                if (!com.MixReadIDRValue(out Res))
                                    return;
                                if (!com.MixReadIDRValue(out Res))
                                    return;
                                break;
                        }
                        break;
                }
                Err1 = Res - intSetRes;
                if (intSetRes == 0)
                {
                    if (Math.Abs(Err1) < 2)
                        Result = "√";
                    else
                    { Result = "×"; isNG = true; }
                }
                else
                {
                    if (Math.Abs(Err1) < chkResACC)
                        Result = "√";
                    else
                    { Result = "×"; isNG = true; }
                }
                UpdateUidelegate(DeviceType, TesterID, CheckType, intSetRes.ToString(), "", Res.ToString("f2"), Err1.ToString("f2"), "", chkResACC.ToString(), Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            finally
            {
                com.IDRResCal_RY(intSetRes, false);
            }


        }

    }
}
