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

        public void ReadCNT(string DeviceType, string TesterID, int intSetCurrent, string TestType, double CurAcc, double CellVol, out bool isNG)
        {
            double dblCurr = 0.0f;
            double nLoadVol = 0;
            //int nLoadCurr = 40;
            //int nCellVol = 0;
            //int nWaitTime = 300;

            //double dblVoltage = 0.0f;
            //double dblCurrent = 0.0f;

            string Result = "";
            double Err1 = 0;
            //string strErrorCode = "";
            double SampleRes = 10000;
            byte status = 0x00;
            isNG = false;
            try
            {
                if (TestType == "CNT静态(正)_nA")
                {
                    SampleRes = 1010000;
                    nLoadVol = (intSetCurrent) * (SampleRes / 1000000);
                }
                else
                {
                    status = 0x01;
                    nLoadVol = 1000 - (Math.Abs(intSetCurrent) * (SampleRes / 1000000));
                }
                if (!com.MixSetVoltValue(nLoadVol))
                    return;
                System.Threading.Thread.Sleep(200);
                for (int i = 0; i < 5; i++)
                {
                    if (!com.MixReadCNTValue(status, out dblCurr))
                        return;
                    Err1 = dblCurr - Math.Abs(intSetCurrent);
                    if (Math.Abs(Err1) > CurAcc)
                    {
                        System.Threading.Thread.Sleep(200);
                        continue;
                    }
                    else break;
                }
                //Err1 = dblCurr - Math.Abs(intSetCurrent);
                if (Math.Abs(Err1) <= CurAcc)
                    Result = "√";
                else
                { Result = "×"; isNG = true; }
                UpdateUidelegate(DeviceType, TesterID, TestType + "点检", intSetCurrent.ToString(), "", dblCurr.ToString("f2"), Err1.ToString("f2"), "", CurAcc.ToString(), Result);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //com.MixEnable(0x00);

            }
        }

    }
}
