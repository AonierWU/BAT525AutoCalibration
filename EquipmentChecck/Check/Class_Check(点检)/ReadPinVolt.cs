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

        public void ReadPinVoltage(string DeviceType, string TesterID, int intSetVolt, string VoltType, string judge, double VoltAcc, out bool isNG)
        {
            double dblVoltage = 0.0f;
            double ReadingVolt = 0.0f;
            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            isNG = false;

            try
            {
                if (!com.MixSetCellVoltValue(intSetVolt))
                    return;
                for (int i = 0; i < 5; i++)
                {
                    if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingVolt))
                        return;
                    if (Math.Abs((ReadingVolt * 1000) - intSetVolt) > 1)
                        continue;
                    else break;
                }
                if (VoltType == "OCV")
                {
                    if (!com.MixReadOCVValue(out dblVoltage))
                        return;
                }
                else if (VoltType == "SDA")
                {
                    if (!com.MixReadPortVolValue(out dblVoltage))
                        return;
                }
                ReadingVolt = ReadingVolt * 1000.0;//单位换算为：mV
                switch (judge)
                {
                    case "设置值、万用表、设备读值":
                        Err1 = dblVoltage - intSetVolt;
                        Err2 = ReadingVolt - intSetVolt;
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
                if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                {
                    if (intSetVolt == 5000)
                    {
                        Result = "√";
                        isNG = false;
                    }
                }
                UpdateUidelegate(DeviceType, TesterID, VoltType + "电压点检", intSetVolt.ToString(), ReadingVolt.ToString("f2"), dblVoltage.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), VoltAcc.ToString(), Result);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //com.MixChgDsgTest(SelectChgDsgMode.CHG, 0, 0, 10, true, ReadFlag.VOLT, out dblVol1, out dblCurrent, out strErrorCode);
                //com.MixReadVolt(selectVoltPin, 10, true, MyEqmCmd.ReadFlag.VOLT, out dblVoltage, out strErrorCode);
            }
        }

    }
}
