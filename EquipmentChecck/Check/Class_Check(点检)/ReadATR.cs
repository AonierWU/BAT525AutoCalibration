using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using static TestSystem_Pack.MyEqmCmd;
using ICSharpCode.SharpZipLib.Zip;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public void ReadATR(string DeviceType, string TesterID, string IDType, int intSetRes, double ResAcc, bool CutRange, out bool isNG, bool WriteExcel)
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
            bool WriteStatus = false;
            int CellRowDmm = 88;
            int CellColDmm = 4;
            int CellColEqm = 5;

            try
            {
                int[,] WriteCell = new int[2, 2];
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
                        CellRowDmm = 109;
                        break;
                    default:
                        return;
                }
                if (WriteExcel)
                {
                    switch (intSetRes)
                    {
                        case 0:
                            WriteStatus = true;
                            break;
                        case 100:
                            WriteStatus = true;
                            CellRowDmm += 1;
                            break;
                        case 500:
                            WriteStatus = true;
                            CellRowDmm += 2;
                            break;
                        case 1000:
                            WriteStatus = true;
                            CellRowDmm += 3;
                            break;
                        case 1500:
                            WriteStatus = true;
                            CellRowDmm += 4;
                            break;
                        case 2000:
                            WriteStatus = true;
                            CellRowDmm += 5;
                            break;
                        case 5000:
                            WriteStatus = true;
                            CellRowDmm += 7;
                            break;
                        case 10000:
                            WriteStatus = true;
                            CellRowDmm += 8;
                            break;
                        case 15000:
                            WriteStatus = true;
                            CellRowDmm += 9;
                            break;
                        case 20000:
                            WriteStatus = true;
                            CellRowDmm += 10;
                            break;
                        case 50000:
                            WriteStatus = true;
                            CellRowDmm += 12;
                            break;
                        case 100000:
                            WriteStatus = true;
                            CellRowDmm += 13;
                            break;
                        case 150000:
                            WriteStatus = true;
                            CellRowDmm += 14;
                            break;
                        case 200000:
                            WriteStatus = true;
                            CellRowDmm += 15;
                            break;
                        case 500000:
                            WriteStatus = true;
                            CellRowDmm += 17;
                            break;
                        case 1000000:
                            WriteStatus = true;
                            CellRowDmm += 18;
                            break;
                        case 2000000:
                            WriteStatus = true;
                            CellRowDmm += 19;
                            break;
                        case 3000000:
                            WriteStatus = true;
                            CellRowDmm += 20;
                            break;
                    }
                    if ((intSetRes == 2000 || intSetRes == 20000 || intSetRes == 200000) && (CutRange))
                    {
                        CellRowDmm += 1;
                    }
                    WriteCell[0, 0] = CellRowDmm;
                    WriteCell[0, 1] = CellColDmm;
                    WriteCell[1, 0] = CellRowDmm;
                    WriteCell[1, 1] = CellColEqm;
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
                if (WriteExcel && WriteStatus)
                    WriteExcelData(WriteCell, "", Res.ToString("f2"));
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
