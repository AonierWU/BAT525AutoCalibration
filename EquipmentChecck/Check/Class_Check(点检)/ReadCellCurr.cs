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

        public void ReadCellCurrent(string DeviceType, string TesterID, int intSetCurrent,  string judge, double CurAcc,out bool isNG, bool WriteExcel)
        {
            double dblCurr = 0.0f;
            double ReadingCurr = 0.0f;
            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            isNG = false;
            bool WriteStatus = false;
            int CellRowDmm = 58;//-1000起始点
            int CellColDmm = 4;
            int CellColEqm = 5;
            try
            {
                int[,] WriteCell = new int[2, 2];
                if (WriteExcel)
                {
                    switch ((int)intSetCurrent)
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
                        case 1250:
                            WriteStatus = true;
                            CellRowDmm += 4;
                            break;

                    }
                    WriteCell[0, 0] = CellRowDmm;
                    WriteCell[0, 1] = CellColDmm;
                    WriteCell[1, 0] = CellRowDmm;
                    WriteCell[1, 1] = CellColEqm;
                }
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
                        Err1 = ReadingCurr - intSetCurrent;
                        Err2 = dblCurr - ReadingCurr;
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
                if (WriteExcel && WriteStatus)
                    WriteExcelData(WriteCell, ReadingCurr.ToString("f2"), dblCurr.ToString("f2"));
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
