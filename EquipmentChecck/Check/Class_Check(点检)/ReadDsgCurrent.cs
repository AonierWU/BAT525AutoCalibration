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
        public void ReadDsgCurrent(string DeviceType, string TesterID, string CheckType, int intSetCurr, string ResType, bool blUseRes, bool ExtRes, string judge, double CurAcc, out bool isNG, bool WriteExcel)
        {
            // DiffResult = false;
            double dblCurrent = 0.0f;
            double ReadingCurr = 0.0f;
            byte Rang = 0x00;
            int SampleRes = 0;
            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            isNG = false;
            bool WriteStatus = false;
            int CellRowDmm = 17;//-1000起始点
            int CellColDmm = 4;
            int CellColEqm = 5;
            try
            {
                int[,] WriteCell = new int[2, 2];
                if (CheckType == "放电电流3A点检")
                {
                    Rang = 0x00;
                    if (WriteExcel)
                    {
                        switch (intSetCurr)
                        {
                            case 0:
                                WriteStatus = true;
                                break;
                            case 10:
                                WriteStatus = true;
                                CellRowDmm += 1;
                                break;
                            case 100:
                                WriteStatus = true;
                                CellRowDmm += 2;
                                break;
                            case 600:
                                WriteStatus = true;
                                CellRowDmm += 3;
                                break;
                            case 1000:
                                WriteStatus = true;
                                CellRowDmm += 4;
                                break;
                            case 2000:
                                WriteStatus = true;
                                CellRowDmm += 5;
                                break;
                            case 2500:
                                WriteStatus = true;
                                CellRowDmm += 6;
                                break;
                            case 3000:
                                WriteStatus = true;
                                CellRowDmm += 7;
                                break;
                            default:
                                break;
                        }
                        WriteCell[0, 0] = CellRowDmm;
                        WriteCell[0, 1] = CellColDmm;
                        WriteCell[1, 0] = CellRowDmm;
                        WriteCell[1, 1] = CellColEqm;
                    }
                }
                else
                {
                    Rang = 0x01;
                    if (WriteExcel)
                    {
                        CellRowDmm = 25;
                        switch (intSetCurr)
                        {
                            case 3000:
                                WriteStatus = true;
                                break;
                            case 5000:
                                WriteStatus = true;
                                CellRowDmm += 1;
                                break;
                            case 10000:
                                WriteStatus = true;
                                CellRowDmm += 2;
                                break;
                            case 15000:
                                WriteStatus = true;
                                CellRowDmm += 3;
                                break;
                            case 20000:
                                WriteStatus = true;
                                CellRowDmm += 4;
                                break;
                            case 25000:
                                WriteStatus = true;
                                CellRowDmm += 5;
                                break;
                            case 30000:
                                WriteStatus = true;
                                CellRowDmm += 6;
                                break;
                            case 40000:
                                WriteStatus = true;
                                CellRowDmm += 7;
                                break;
                            default:
                                break;
                        }
                        WriteCell[0, 0] = CellRowDmm;
                        WriteCell[0, 1] = CellColDmm;
                        WriteCell[1, 0] = CellRowDmm;
                        WriteCell[1, 1] = CellColEqm;
                    }
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
                        if (Math.Abs(intSetCurr - (Math.Abs(ReadingCurr) * 1000)) > CurAcc)
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
                            if (Math.Abs(intSetCurr - ((Math.Abs(ReadingCurr * 1000) / SampleRes) * 1000)) > CurAcc)
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
                    ReadingCurr = Math.Abs(ReadingCurr / SampleRes) * 1000;//转换为A
                }
                ReadingCurr = (Convert.ToDouble(ReadingCurr) * 1000.0);//单位换算为：mA
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
                        {
                            Result = "√"; //DiffResult = true;
                        }
                        else
                        { Result = "×"; isNG = true; }
                        break;
                }
                if (WriteExcel && WriteStatus)
                    WriteExcelData(WriteCell, ReadingCurr.ToString("f2"), dblCurrent.ToString("f2"));
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
