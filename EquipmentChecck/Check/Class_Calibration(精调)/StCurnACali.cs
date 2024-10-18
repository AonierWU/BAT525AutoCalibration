using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool StCurnACali(string Box, double CurCaliAcc, double CurCheckAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<double> CaliValueset = new List<double> { };
            //int[] CaliValue = { 0, 30, 60, 100, 100, 200, 400, 600, 600, 800, 1000, 1200, 1200, 1500, 1800, 2000 };//静态电流2000nA校正点
            //double[] CaliValueset = { 0, 61.5, 123, 205, 205, 410, 820, 1230, 1230, 1640, 2050, 2460, 2460, 3075, 3690, 4100 };//静态电流2000nA校正点设定电压值
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址   
           // List<byte> CaliDHValue = new List<byte> { };
            List<int> CheckCaliValue = new List<int> { };
            byte CurRange;
            //double CellCurCaliTesterValue = 0;
            //double CellVoltDMMValue = 0;
            Point = "未设初始值";
            // double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            //for (int j = 0; j < 16; j++)
            //{

            //    Vol_ADC_Add_L[j] = 0x4C + j * 2;//nA addr
            //}
            byte VoltDL,  VoltDH;
            byte VoltDL1, VoltDH1;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            //double CellCurCheckDMMValue = 0;
            double CellCurCaliTesterValue1 = 0;
            //double Judge = 0.5;
            //int R = 2050000;
            switch (DeviceType)
            {
                case "BAT525C":
                    int[] s = new int[] { 0, 10, 30, 50, 50, 100, 200, 300, 300, 400, 500, 500, 800, 1000 };
                    CaliValue.AddRange(s);

                    double[] v1 = new double[] { 0, 20, 60, 100, 100, 200, 400, 600, 600, 800, 1000, 1000, 1600, 2000 };
                    CaliValueset.AddRange(v1);

                    //refADCDH = 239;

                    Vol_ADC_Add_L = new int[14];//存放ADC读取值的低地址
                    for (int j = 0; j < 14; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x7C + j * 2;//nA addr
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 0, 10, 30, 50, 50, 100, 200, 300, 300, 400, 500, 500, 800, 1000 };
                    CaliValue.AddRange(d);

                    double[] v2 = new double[] { 0, 20, 60, 100, 100, 200, 400, 600, 600, 800, 1000, 1000, 1600, 2000 };
                    CaliValueset.AddRange(v2);
                    Vol_ADC_Add_L = new int[14];//存放ADC读取值的低地址
                    for (int j = 0; j < 14; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x8C + j * 2;//nA addr
                    }
                    break;
            }
            try
            {
                if (!com.MixCellVoltCalibration())
                    return false;
                if (!com.MixStCurCalibration_nA())
                    return false;
                if (!com.StCurCal_RY("1000nA", true))
                    return false;
                System.Threading.Thread.Sleep(100);
                if (!StopStatus)
                {
                    System.Threading.Thread.Sleep(20);
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        CurRange = 0x00;
                        if (s < 4)
                        {
                            CurRange = 0x00;
                        }
                        else if (s < 8 && s >= 4)
                        {
                            CurRange = 0x01;
                        }
                        else if (s < 11 && s >= 8)
                        {
                            CurRange = 0x02;
                            com.MixSetnARes(0x01);
                        }
                        else if (s < 14 && s >= 11)
                        {
                            CurRange = 0x03;
                        }
                        Point = CaliValue[s].ToString() + "nA校准NG";
                        if (!com.MixStCurRange_nA(0x04, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                            return false;
                        if (!com.MixSetCellVoltValue(CaliValueset[s]))//设定电压
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.MixReadStCur_ADCValue(0X0C, out VoltDL1, out VoltDH1))
                            break;
                        System.Threading.Thread.Sleep(300);
                        if (!com.MixReadStCur_ADCValue(0X0C, out VoltDL, out VoltDH))
                            break;
                        if (Math.Abs((VoltDH1 * 256 + VoltDL1) - (VoltDH * 256 + VoltDL)) <= 125)
                        {
                            //CaliDHValue.Add(VoltDH);
                            CheckCaliValue.Add(Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                            if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                break;
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (!com.MixReadStCur_ADCValue(0X0C, out VoltDL1, out VoltDH1))
                                    break;
                                System.Threading.Thread.Sleep(300);
                                if (!com.MixReadStCur_ADCValue(0X0C, out VoltDL, out VoltDH))
                                    break;
                                if (Math.Abs((VoltDH1 * 256 + VoltDL1) - (VoltDH * 256 + VoltDL)) <= 125)
                                {
                                    //CaliDHValue.Add(VoltDH);
                                    CheckCaliValue.Add(Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                    if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                        break;
                                    break;
                                }
                                if (i == 3)
                                {
                                    Point = "ADC高位读值循环读取五次后，均不一致！";
                                    return false;
                                }
                            }
                        }
                        //System.Threading.Thread.Sleep(50);

                        if (s == 0 || s == 4 || s == 8 || s == 11)
                        {
                            Result = "√";
                        }
                        else
                        {
                            if (CheckCaliValue[s] > CheckCaliValue[s - 1])
                            {
                                Result = "√";
                            }
                            else
                            {
                                Result = "×";
                            }
                        }
                        UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), "", "校准判断地址位", "", "", CurCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == CaliValue.Count - 1)
                        {
                            if (!com.MixUpdateCellVolt())
                                return false;
                            com.MixSetnARes(0x00);


                        }
                        else continue;

                    }

                }
                else return false;
                #region 点检所有的点

                if (!StopStatus)
                {
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {
                        CurRange = 0x00;
                        if (s < 4)
                        {
                            CurRange = 0x00;

                        }
                        else if (s < 8 && s >= 4)
                        {
                            com.MixSetnARes(0x00);
                            CurRange = 0x01;
                        }
                        else if (s < 12 && s >= 8)
                        {

                            CurRange = 0x02;
                        }
                        else if (s < 16 && s >= 12)
                        {
                            com.MixSetnARes(0x01);
                            CurRange = 0x03;
                        }
                        Point = CaliValue[s].ToString() + "nA点检NG";
                        if (!com.MixStCurRange_nA(0x04, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                            return false;
                        if (!com.MixSetCellVoltValue((CaliValueset[s])))//设定电压
                            return false;
                        System.Threading.Thread.Sleep(300);
                        if (!com.MixReadStCurValue_nA(CurRange, out CellCurCaliTesterValue1))
                            return false;
                        Err1 = CellCurCaliTesterValue1 - Convert.ToDouble(CaliValue[s]);
                        if (Math.Abs(Err1) < CurCheckAcc)
                        { Result = "√"; ChkTestNG[s] = false; }
                        else
                        { Result = "×"; ChkTestNG[s] = true; }
                        UpdateUidelegate(DeviceType, Box, "STC_1000nA点检", CaliValue[s].ToString(), "", CellCurCaliTesterValue1.ToString(), Err1.ToString("f2"), "", CurCheckAcc.ToString(), Result);
                        if (s == 0)
                        {
                            if (Array.Exists(ChkTestNG, element => element))
                            {
                                Point = "点检_NG";
                                return false;
                            }
                            else
                                continue;
                        }
                    }
                }

                #endregion

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                com.MixCellEnable(0x00);
                com.MixSetCellVoltValue(0x00);
                com.MixStCurEnable();
                com.Reset_RY();
            }
        }
    }
}
