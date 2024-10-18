using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static TestSystem_Pack.MyEqmCmd;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {

        public void ReadACIR(string DeviceType, string TesterID, string CheckType, double intSetRes, double ResAcc, double EqmCurValue, bool CutRange, out bool isNG, bool WriteExcel)
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
            bool WriteStatus = false;
            int CellRowDmm = 76;//-1000ÆðÊ¼µã
            int CellColDmm = 4;
            int CellColEqm = 5;
            try
            {
                int[,] WriteCell = new int[2, 2];
                if (WriteExcel)
                {
                    switch ((int)intSetRes)
                    {
                        case 0:
                            WriteStatus = true;
                            break;
                        case 1:
                            WriteStatus = true;
                            break;
                        case 10:
                            WriteStatus = true;
                            CellRowDmm += 1;
                            break;
                        case 30:
                            WriteStatus = true;
                            CellRowDmm += 2;
                            break;
                        case 50:
                            WriteStatus = true;
                            CellRowDmm += 3;
                            break;

                    }
                    WriteCell[0, 0] = CellRowDmm;
                    WriteCell[0, 1] = CellColDmm;
                    WriteCell[1, 0] = CellRowDmm;
                    WriteCell[1, 1] = CellColEqm;
                }
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
                if (WriteExcel && WriteStatus)
                    WriteExcelData(WriteCell, ReadingCurr.ToString("f2"), Res.ToString("f2"));
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
