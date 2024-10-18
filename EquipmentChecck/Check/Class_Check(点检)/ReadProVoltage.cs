using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {

        public void ReadProVoltage(string DeviceType, string TesterID, string CheckType,int intSetVolt, string judge, double VolAcc,bool CutRange,out bool isNG, bool WriteExcel)
        {
            double dblVoltage = 0.0f;
            double ReadingVolt = 0.0f;
            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            isNG = false;
            byte Range = 0x00;
            bool WriteStatus = false;
            int CellRowDmm = 33;//-1000起始点
            int CellColDmm = 4;
            int CellColEqm = 5;
            try
            {
                int[,] WriteCell = new int[2, 2];
                if (WriteExcel)
                {
                    switch (intSetVolt)
                    {
                        case 200:
                            WriteStatus = true;
                            break;
                        case 500:
                            WriteStatus = true;
                            CellRowDmm += 1;
                            break;
                        case 1000:
                            WriteStatus = true;
                            CellRowDmm += 2;
                            break;
                        case 3000:
                            WriteStatus = true;
                            CellRowDmm += 3;
                            break;
                        case 5000:
                            WriteStatus = true;
                            CellRowDmm += 4;
                            break;
                        case 7000:
                            WriteStatus = true;
                            CellRowDmm += 5;
                            break;
                        case 9000:
                            WriteStatus = true;
                            CellRowDmm += 6;
                            break;
                        case 10000:
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
                if (!com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", "AUTO"))
                    return;
                if(DeviceType=="BAT525C"|| DeviceType == "BAT525D")
                {
                    if(intSetVolt<=12000)
                    {
                        if (!com.MixProVoltSetRang(Range))
                            return;
                    }
                    else
                    {
                        Range = 0x01;
                        if (!com.MixProVoltSetRang(Range))
                            return;
                    }
                    if(CutRange)
                    {
                        if(Range==0x00)
                        {
                            Range = 0x01;
                        }
                    }
                }
                if(!com.MixSetProVoltValue(intSetVolt))
                    return;
                System.Threading.Thread.Sleep(100);
                for (int i = 0; i < 5; i++)
                {
                    if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingVolt))
                        return;
                    if (Math.Abs((ReadingVolt * 1000) - intSetVolt) > 1)
                        continue;
                    else break;
                }
                if (!com.MixReadProVoltValue(out dblVoltage))
                    return;
                ReadingVolt = ReadingVolt * 1000.0;//单位换算为：mV
                switch (judge)
                {
                    case "设置值、万用表、设备读值":
                        Err1 = ReadingVolt - intSetVolt;
                        Err2 = dblVoltage - ReadingVolt;
                        if (Math.Abs(Err1) < VolAcc && Math.Abs(Err2) < VolAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                            break;
                    case "设置值、万用表":
                        Err1 = ReadingVolt - intSetVolt;
                        if (Math.Abs(Err1) < VolAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "设置值、设备读值":
                        Err1 = dblVoltage - intSetVolt;
                        if (Math.Abs(Err1) < VolAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                    case "万用表、设备读值":
                        Err1 = ReadingVolt - dblVoltage;
                        if (Math.Abs(Err1) < VolAcc)
                            Result = "√";
                        else
                        { Result = "×"; isNG = true; }
                        break;
                }
                if (WriteExcel && WriteStatus)
                    WriteExcelData(WriteCell, ReadingVolt.ToString("f2"), dblVoltage.ToString("f2"));
                UpdateUidelegate(DeviceType, TesterID, CheckType, intSetVolt.ToString(), ReadingVolt.ToString("f2"), dblVoltage.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), VolAcc.ToString(), Result);
            }
            catch (Exception)
            {

                throw;
            }
            finally 
            {
                //com.MixSetProgVolt(0, 10, true, out dblVoltage, out strErrorCode);
                //com.ProVolControlDisable();
                //com.SetProVolt(0);
                //com.ProVolDisable();
                //com.MixProVoltEnable(0X00);
            }
          

        }

    }
}
