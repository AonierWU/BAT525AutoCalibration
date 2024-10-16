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

        public void ReadCellVoltage(string DeviceType, string TesterID, int intSetVolt, string judge, double VoltAcc, out bool isNG)
        {
            double dblVoltage = 0.0f;
            double ReadingVolt = 0.0f;

            //int nSetCurrent = 600;
            //int nWaitTime = 300;

            //double dblCurrent = 0.0f;

            //string strErrorCode = "";
            string Result = "";
            double Err1 = 0;
            double Err2 = 0;
            isNG = false;
            try
            {
                if (!com.MixSetCellVoltValue(intSetVolt))
                    return;
                System.Threading.Thread.Sleep(100);
                for (int i = 0; i < 5; i++)
                {
                    if (!com.ReadMeterValue(frmVerifyDevice.strConnType, out ReadingVolt))
                        return;
                    if (Math.Abs((ReadingVolt * 1000) - intSetVolt) < VoltAcc)
                    {
                        break;
                    }
                    else continue;
                }
                if (!com.MixReadCellVoltValue(out dblVoltage))
                    return;
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
                UpdateUidelegate(DeviceType, TesterID, "Cell电压点检", intSetVolt.ToString(), ReadingVolt.ToString("f2"), dblVoltage.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), VoltAcc.ToString(), Result);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //com.MixSetCell(0, 0, 10, true, ReadFlag.VOLT, true, out dblVoltage, out dblCurrent, out strErrorCode);
                //com.MixCellEnable(0X00);
            }
        }

    }
}
