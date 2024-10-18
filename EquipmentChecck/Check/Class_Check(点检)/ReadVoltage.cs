using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using static TestSystem_Pack.MyEqmCmd;
using System.Xml;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {

        public void ReadVoltage(string DeviceType, string TesterID, int intSetVolt, string judge, double VoltAcc, out bool isNG, bool WriteExcel)
        {
            double dblVoltage = 0.0f;
            double ReadingVolt = 0.0f;
            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            isNG = false;
            bool WriteStatus = false;
            int CellRowDmm = 5;//-1000起始点
            int CellColDmm = 4;
            int CellColEqm = 5;
            try
            {
                //VoltAcc = intSetVolt * 0.0002 + 5000 * 0.0002;//万二RD+千一FS
                int[,] WriteCell = new int[2, 2];
                if (WriteExcel)
                {
                    switch (intSetVolt)
                    {
                        case 0:
                            WriteStatus = true;
                            break;
                        case 20:
                            WriteStatus = true;
                            CellRowDmm += 1;
                            break;
                        case 100:
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
                        case 2500:
                            WriteStatus = true;
                            CellRowDmm += 6;
                            break;
                        case 3000:
                            WriteStatus = true;
                            CellRowDmm += 7;
                            break;
                        case 3500:
                            WriteStatus = true;
                            CellRowDmm += 8;
                            break;
                        case 4000:
                            WriteStatus = true;
                            CellRowDmm += 9;
                            break;
                        case 4500:
                            WriteStatus = true;
                            CellRowDmm += 10;
                            break;
                        case 5000:
                            WriteStatus = true;
                            CellRowDmm += 11;
                            break;
                        default:
                            break;
                    }
                    WriteCell[0, 0] = CellRowDmm;
                    WriteCell[0, 1] = CellColDmm;
                    WriteCell[1, 0] = CellRowDmm;
                    WriteCell[1, 1] = CellColEqm;
                }
                if (!com.MixSetVoltValue(intSetVolt))
                    return;
                System.Threading.Thread.Sleep(100);
                if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingVolt))
                    return;
                if (!com.MixReadVoltValue(out dblVoltage))
                    return;
                ReadingVolt = ReadingVolt * 1000.0;//单位换算为：mV
                switch (judge)
                {
                    case "设置值、万用表、设备读值":
                        Err1 = ReadingVolt - intSetVolt;
                        Err2 = dblVoltage - ReadingVolt;
                        if (Math.Abs(Err1) < VoltAcc && Math.Abs(Err2) < VoltAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "设置值、万用表":
                        Err1 = ReadingVolt - intSetVolt;
                        if (Math.Abs(Err1) < VoltAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "设置值、设备读值":
                        Err1 = dblVoltage - intSetVolt;
                        if (Math.Abs(Err1) < VoltAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "万用表、设备读值":
                        Err1 = ReadingVolt - dblVoltage;
                        if (Math.Abs(Err1) < VoltAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                }
                if (WriteExcel&&WriteStatus)
                    WriteExcelData(WriteCell, ReadingVolt.ToString("f2"), dblVoltage.ToString("f2"));
                UpdateUidelegate(DeviceType, TesterID, "充电电压点检", intSetVolt.ToString(), ReadingVolt.ToString("f2"), dblVoltage.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), VoltAcc.ToString(), Result);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //com.MixChgDsgTest(SelectChgDsgMode.CHG, 0, 0, 10, true, ReadFlag.VOLT, out dblVoltage, out dblRCurr, out strErrorCode);
            }
        }

    }
}
