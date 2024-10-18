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
        public void ReadStaticCurrent(string DeviceType, string TesterID, string CurrType, int intSetCurrent, double CurAcc, bool CutRange, out bool isNG, bool WriteExcel)
        {
            double dblCurr = 0.0f;
            int SampleRes = 50;//单位Ω
            double SetCellVolt = 0.0f;
            string Result = "";
            double Err1 = 0;
            byte SetRes = 0x00;
            byte ReadRes = 0x00;
            byte Range = 0x00;
            isNG = false;
            string Type = "200uA";
            Dictionary<int, double> BAT525DVol = new Dictionary<int, double>
            {
                {0,0 },
                {40,80 },
                {120,244 },
                {200,408 },
                {320,652 },
                {400,815 },
                {800,1630 },
                {1200,2451 },
                {1600,3268 },
                {2000,4083 }
            };
            double SetCurr_uA = 0;
            bool WriteStatus = false;
            int CellRowDmm = 130;
            int CellColDmm = 4;
            int CellColEqm = 5;
            if (CurrType == "静态电流_2000uA")
            {
                Type = "2000uA";
                SetRes = 0x01;
                ReadRes = 0x00;
                SetCurr_uA = Convert.ToDouble(intSetCurrent);
                SampleRes = 2050;
                if (0 <= intSetCurrent && intSetCurrent <= 100)
                    Range = 0x00;
                else if (100 < intSetCurrent && intSetCurrent <= 600)
                    Range = 0x01;
                else if (600 < intSetCurrent && intSetCurrent <= 1200)
                    Range = 0x02;
                else
                    Range = 0x03;
                if (WriteExcel)
                {
                    switch (intSetCurrent)
                    {
                        case 0:
                            WriteStatus = true;
                            CellRowDmm += 16;
                            break;
                        case 30:
                            WriteStatus = true;
                            CellRowDmm += 17;
                            break;
                        case 60:
                            WriteStatus = true;
                            CellRowDmm += 18;
                            break;
                        case 100:
                            WriteStatus = true;
                            CellRowDmm += 19;
                            break;
                        case 200:
                            WriteStatus = true;
                            CellRowDmm += 21;
                            break;
                        case 400:
                            WriteStatus = true;
                            CellRowDmm += 22;
                            break;
                        case 600:
                            WriteStatus = true;
                            CellRowDmm += 23;
                            break;
                        case 800:
                            WriteStatus = true;
                            CellRowDmm += 25;
                            break;
                        case 1000:
                            WriteStatus = true;
                            CellRowDmm += 26;
                            break;
                        case 1200:
                            WriteStatus = true;
                            CellRowDmm += 27;
                            break;
                        case 1500:
                            WriteStatus = true;
                            CellRowDmm += 29;
                            break;
                        case 1800:
                            WriteStatus = true;
                            CellRowDmm += 30;
                            break;
                        case 2000:
                            WriteStatus = true;
                            CellRowDmm += 31;
                            break;
                    }
                    if ((intSetCurrent == 100 || intSetCurrent == 600 || intSetCurrent == 1200) && CutRange)
                    {
                        CellRowDmm += 1;
                    }
                }
            }
            else if (CurrType == "静态电流_200uA")
            {
                Type = "200uA";
                SetRes = 0x02;
                ReadRes = 0x01;
                SetCurr_uA = Convert.ToDouble(intSetCurrent);
                SampleRes = 2500;
                if (0 <= intSetCurrent && intSetCurrent <= 10)
                    Range = 0x00;
                else if (10 < intSetCurrent && intSetCurrent <= 60)
                    Range = 0x01;
                else if (60 < intSetCurrent && intSetCurrent <= 120)
                    Range = 0x02;
                else
                    Range = 0x03;
                if (WriteExcel)
                {
                    switch (intSetCurrent)
                    {
                        case 0:
                            WriteStatus = true;
                            break;
                        case 3:
                            WriteStatus = true;
                            CellRowDmm += 1;
                            break;
                        case 6:
                            WriteStatus = true;
                            CellRowDmm += 2;
                            break;
                        case 10:
                            WriteStatus = true;
                            CellRowDmm += 3;
                            break;
                        case 20:
                            WriteStatus = true;
                            CellRowDmm += 5;
                            break;
                        case 40:
                            WriteStatus = true;
                            CellRowDmm += 6;
                            break;
                        case 60:
                            WriteStatus = true;
                            CellRowDmm += 7;
                            break;
                        case 80:
                            WriteStatus = true;
                            CellRowDmm += 9;
                            break;
                        case 100:
                            WriteStatus = true;
                            CellRowDmm += 10;
                            break;
                        case 120:
                            WriteStatus = true;
                            CellRowDmm += 11;
                            break;
                        case 150:
                            WriteStatus = true;
                            CellRowDmm += 13;
                            break;
                        case 180:
                            WriteStatus = true;
                            CellRowDmm += 14;
                            break;
                        case 200:
                            WriteStatus = true;
                            CellRowDmm += 15;
                            break;
                    }
                    if ((intSetCurrent == 10 || intSetCurrent == 60 || intSetCurrent == 120) && CutRange)
                    {
                        CellRowDmm += 1;
                    }
                }
            }
            else if (CurrType == "静态电流_20000nA")
            {
                Type = "20000nA";
                SetRes = 0x03;
                ReadRes = 0x02;
                SetCurr_uA = Convert.ToDouble(intSetCurrent) / 1000.0;
                SampleRes = 205000;
                if (0 <= intSetCurrent && intSetCurrent <= 1000)
                    Range = 0x00;
                else if (1000 < intSetCurrent && intSetCurrent <= 6000)
                    Range = 0x01;
                else if (6000 < intSetCurrent && intSetCurrent <= 12000)
                    Range = 0x02;
                else
                    Range = 0x03;
                if (WriteExcel)
                {
                    switch (intSetCurrent)
                    {
                        case 0:
                            WriteStatus = true;
                            CellRowDmm += 48;
                            break;
                        case 300:
                            WriteStatus = true;
                            CellRowDmm += 49;
                            break;
                        case 600:
                            WriteStatus = true;
                            CellRowDmm += 50;
                            break;
                        case 1000:
                            WriteStatus = true;
                            CellRowDmm += 51;
                            break;
                        case 2000:
                            WriteStatus = true;
                            CellRowDmm += 53;
                            break;
                        case 4000:
                            WriteStatus = true;
                            CellRowDmm += 54;
                            break;
                        case 6000:
                            WriteStatus = true;
                            CellRowDmm += 55;
                            break;
                        case 8000:
                            WriteStatus = true;
                            CellRowDmm += 57;
                            break;
                        case 10000:
                            WriteStatus = true;
                            CellRowDmm += 58;
                            break;
                        case 12000:
                            WriteStatus = true;
                            CellRowDmm += 59;
                            break;
                        case 15000:
                            WriteStatus = true;
                            CellRowDmm += 61;
                            break;
                        case 18000:
                            WriteStatus = true;
                            CellRowDmm += 62;
                            break;
                        case 20000:
                            WriteStatus = true;
                            CellRowDmm += 63;
                            break;
                    }
                    if ((intSetCurrent == 1000 || intSetCurrent == 6000 || intSetCurrent == 12000) && CutRange)
                    {
                        CellRowDmm += 1;
                    }
                }
            }
            else if (CurrType == "静态电流_2000nA")
            {
                Type = "2000nA";
                SetRes = 0x04;
                ReadRes = 0x03;
                SetCurr_uA = Convert.ToDouble(intSetCurrent) / 1000.0;
                SampleRes = 2050000;
                if (0 <= intSetCurrent && intSetCurrent <= 100)
                    Range = 0x00;
                else if (100 < intSetCurrent && intSetCurrent <= 600)
                    Range = 0x01;
                else if (600 < intSetCurrent && intSetCurrent <= 1200)
                    Range = 0x02;
                else
                    Range = 0x03;
                if (WriteExcel)
                {
                    switch (intSetCurrent)
                    {
                        case 0:
                            WriteStatus = true;
                            CellRowDmm += 32;
                            break;
                        case 30:
                            WriteStatus = true;
                            CellRowDmm += 33;
                            break;
                        case 60:
                            WriteStatus = true;
                            CellRowDmm += 34;
                            break;
                        case 100:
                            WriteStatus = true;
                            CellRowDmm += 35;
                            break;
                        case 200:
                            WriteStatus = true;
                            CellRowDmm += 37;
                            break;
                        case 400:
                            WriteStatus = true;
                            CellRowDmm += 38;
                            break;
                        case 600:
                            WriteStatus = true;
                            CellRowDmm += 39;
                            break;
                        case 800:
                            WriteStatus = true;
                            CellRowDmm += 41;
                            break;
                        case 1000:
                            WriteStatus = true;
                            CellRowDmm += 42;
                            break;
                        case 1200:
                            WriteStatus = true;
                            CellRowDmm += 43;
                            break;
                        case 1500:
                            WriteStatus = true;
                            CellRowDmm += 45;
                            break;
                        case 1800:
                            WriteStatus = true;
                            CellRowDmm += 46;
                            break;
                        case 2000:
                            WriteStatus = true;
                            CellRowDmm += 47;
                            break;
                    }
                    if ((intSetCurrent == 100 || intSetCurrent == 600 || intSetCurrent == 1200) && CutRange)
                    {
                        CellRowDmm += 1;
                    }
                }
            }
            else if (CurrType == "静态电流_1000uA")
            {
                Type = "1000uA";
                SetRes = 0x00;
                ReadRes = 0x00;
                SampleRes = 2033;
                SetCurr_uA = Convert.ToDouble(intSetCurrent);
                if (0 <= intSetCurrent && intSetCurrent <= 100)
                    Range = 0x00;
                else if (100 < intSetCurrent && intSetCurrent <= 200)
                    Range = 0x01;
                else if (200 < intSetCurrent && intSetCurrent <= 600)
                    Range = 0x02;
                else
                    Range = 0x03;
            }
            else if (CurrType == "静态电流D_2000uA")
            {
                Type = "2000uA";
                SetRes = 0x00;
                ReadRes = 0x00;
                SampleRes = 2000;
                SetCurr_uA = Convert.ToDouble(intSetCurrent);
                if (0 <= intSetCurrent && intSetCurrent <= 200)
                    Range = 0x00;
                else if (200 < intSetCurrent && intSetCurrent <= 400)
                    Range = 0x01;
                else if (400 < intSetCurrent && intSetCurrent <= 1200)
                    Range = 0x02;
                else
                    Range = 0x03;
            }
            else if (CurrType == "静态电流_1000nA")
            {
                Type = "1000nA";
                SetRes = 0x00;
                ReadRes = 0x00;
                SampleRes = 2000000;
                SetCurr_uA = Convert.ToDouble(intSetCurrent) / 1000;
                if (0 <= intSetCurrent && intSetCurrent <= 50)
                { Range = 0x00; com.MixSetnARes(0x00); }
                else if (50 < intSetCurrent && intSetCurrent <= 300)
                { Range = 0x01; com.MixSetnARes(0x00); }
                else if (300 < intSetCurrent && intSetCurrent <= 500)
                { Range = 0x02; com.MixSetnARes(0x01); }
                else
                { Range = 0x03; com.MixSetnARes(0x01); }
            }
            try
            {
                int[,] WriteCell = new int[2, 2];
                if (WriteExcel)
                {
                    WriteCell[0, 0] = CellRowDmm;
                    WriteCell[0, 1] = CellColDmm;
                    WriteCell[1, 0]= CellRowDmm;
                    WriteCell[1,1] = CellColEqm;
                }
                if (CutRange)
                {
                    if (Range < 3)
                    {
                        Range += 1;
                        if (Type == "1000nA")
                        {
                            if (Range >= 2)
                            {
                                com.MixSetnARes(0x01);
                            }
                        }
                    }
                }
                if (CurrType == "静态电流D_2000uA")
                {
                    SetCellVolt = BAT525DVol[Convert.ToInt32(SetCurr_uA)];
                }
                else
                    SetCellVolt = Math.Round((SetCurr_uA * SampleRes) / 1000, 1);//
                if (Type != "1000nA")
                {
                    if (!com.MixStCurRange(SetRes, Range))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                        return;
                }
                else
                {
                    if (!com.MixStCurRange_nA(SetRes, Range))
                        return;
                }
                if (!com.MixSetCellVoltValue(SetCellVolt))
                    return;
                System.Threading.Thread.Sleep(200);
                if (Type == "1000nA")
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (!com.MixReadStCurValue_nA(Range, out dblCurr))
                            return;
                        if (Math.Abs(dblCurr - Math.Abs(intSetCurrent)) >= CurAcc)
                        {
                            System.Threading.Thread.Sleep(200);
                            continue;
                        }
                        else break;
                    }
                    com.MixSetnARes(0X00);
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (!com.MixReadStCurValue(ReadRes, Range, out dblCurr))
                            return;
                        if (CurrType == "静态电流D_2000uA")
                        {
                            dblCurr = dblCurr * 2;
                        }
                        if (Math.Abs(dblCurr - Math.Abs(intSetCurrent)) >= CurAcc)
                        {
                            System.Threading.Thread.Sleep(200);
                            continue;
                        }
                        else break;
                    }
                }
                Err1 = dblCurr - Math.Abs(intSetCurrent);
                if (Math.Abs(Err1) < CurAcc)
                    Result = "√";
                else
                { Result = "×"; isNG = true; }
                if (WriteExcel && WriteStatus)
                    WriteExcelData(WriteCell, "", dblCurr.ToString("f2"));
                UpdateUidelegate(DeviceType, TesterID, CurrType + "点检", intSetCurrent.ToString(), "", dblCurr.ToString("f2"), Err1.ToString("f2"), "", CurAcc.ToString(), Result);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //com.MixSetCell(0, 0, nWaitTime, true, ReadFlag.CURR, false, out dblVoltage, out dblCurr, out strErrorCode);
            }
        }

    }
}
