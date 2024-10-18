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

        public void ReadCNT(string DeviceType, string TesterID, int intSetCurrent, string TestType, double CurAcc, double CellVol, out bool isNG, bool WriteExcel)
        {
            double dblCurr = 0.0f;
            double nLoadVol = 0;
            string Result = "";
            double Err1 = 0;
            double SampleRes = 10000;
            byte status = 0x00;
            isNG = false;
            bool WriteStatus = false;
            int CellRowDmm = 79;//-1000起始点
            int CellColDmm = 4;
            int CellColEqm = 5;
            try
            {
                int[,] WriteCell = new int[2, 2];
                if (TestType == "CNT静态(正)_nA")
                {
                    SampleRes = 1010000;
                    nLoadVol = (intSetCurrent) * (SampleRes / 1000000);
                    if (WriteExcel)
                    {
                        switch (intSetCurrent)
                        {
                            case 0:
                                WriteStatus = true;
                                CellRowDmm += 4;
                                break;
                            case 1000:
                                WriteStatus = true;
                                CellRowDmm += 5;
                                break;
                            case 3000:
                                WriteStatus = true;
                                CellRowDmm += 6;
                                break;
                            case 5000:
                                WriteStatus = true;
                                CellRowDmm += 7;
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
                    status = 0x01;
                    nLoadVol = 1000 - (Math.Abs(intSetCurrent) * (SampleRes / 1000000));
                    if (WriteExcel)
                    {
                        switch (intSetCurrent)
                        {

                            case 0:
                                WriteStatus = true;
                                break;
                            case -1000:
                                WriteStatus = true;
                                CellRowDmm += 1;
                                break;
                            case -3000:
                                WriteStatus = true;
                                CellRowDmm += 2;
                                break;
                            case -5000:
                                WriteStatus = true;
                                CellRowDmm += 3;
                                break;

                        }
                        WriteCell[0, 0] = CellRowDmm;
                        WriteCell[0, 1] = CellColDmm;
                        WriteCell[1, 0] = CellRowDmm;
                        WriteCell[1, 1] = CellColEqm;
                    }
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
                if (WriteExcel && WriteStatus)
                    WriteExcelData(WriteCell, "", dblCurr.ToString("f2"));
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
