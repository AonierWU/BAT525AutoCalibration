using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool StCur2Cali(string Box, double CurCaliAcc, double CurCheckAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<double> CaliValueset = new List<double> { };
            //int[] CaliValue = { 0, 30, 60, 100, 100, 200, 400, 600, 600, 800, 1000, 1200, 1200, 1500, 1800, 2000 };//静态电流2000u校正点
            //double[] CaliValueset = { 0, 61.5, 123, 205, 205, 410, 820, 1230, 1230, 1640, 2050, 2460, 2460, 3075, 3690, 4100 };//静态电流2000u校正点设定电压值
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址   
            List<byte> CaliDHValue = new List<byte> { };
            //double CellCurCaliTesterValue = 0;
            //double CellVoltDMMValue = 0;
            Point = "未设初始值";
            byte CurRange;
            //double Resolution = 0.019;//电压零点以外的点的分辨率
            //double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03

            byte VoltDL, VoltDH;
            byte VoltDL2, VoltDH2;
            byte refADCDH = 0;
            byte ADCDL, ADCDM, ADCDH;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            //double CellCurCheckDMMValue = 0;
            double CellCurCaliTesterValue1 = 0;
            //int R = 2050;
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, 30, 60, 100, 100, 200, 400, 600, 600, 800, 1000, 1200, 1200, 1500, 1800, 2000 };
                    CaliValue.AddRange(s);

                    double[] v1 = new double[] { 0, 61.5, 123, 205, 205, 410, 820, 1230, 1230, 1640, 2050, 2460, 2460, 3075, 3690, 4100 };
                    CaliValueset.AddRange(v1);

                    refADCDH = 239;

                    Vol_ADC_Add_L = new int[16];//存放ADC读取值的低地址
                    for (int j = 0; j < 16; j++)
                    {
                        Vol_ADC_Add_L[j] = 0xAF + j * 2;//uA addr
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 0, 30, 60, 100, 100, 200, 400, 600, 600, 800, 1000, 1200, 1200, 1500, 1800, 2000 };
                    CaliValue.AddRange(h);

                    double[] v2 = new double[] { 0, 61.5, 123, 205, 205, 410, 820, 1230, 1230, 1640, 2050, 2460, 2460, 3075, 3690, 4100 };
                    CaliValueset.AddRange(v2);

                    refADCDH = 239;

                    Vol_ADC_Add_L = new int[16];//存放ADC读取值的低地址
                    for (int j = 0; j < 16; j++)
                    {
                        Vol_ADC_Add_L[j] = 0xAF + j * 2;//uA addr
                    }
                    break;
            }
            try
            {
                //if (!com.MixCellVoltCalibration())
                //    return false;
                if (!com.MixStCurCalibration())
                    return false;
                if (!com.StCurCal_RY("2000uA", true))
                    return false;
                System.Threading.Thread.Sleep(100);
                if (!StopStatus)
                {
                    //System.Threading.Thread.Sleep(100);
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
                        else if (s < 12 && s >= 8)
                        {
                            CurRange = 0x02;
                        }
                        else if (s < 16 && s >= 12)
                        {
                            CurRange = 0x03;
                        }
                        Point = CaliValue[s].ToString() + "uA校准NG";
                        if (!com.MixStCurRange(0x01, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                            return false;
                        if (!com.MixSetCellVoltValue(CaliValueset[s]))//设定电压
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.MixReadStCur_ADCValue(CurRange, out VoltDL2, out VoltDH2))
                            break;
                        System.Threading.Thread.Sleep(300);
                        if (!com.MixReadStCur_ADCValue(CurRange, out VoltDL, out VoltDH))
                            break;
                        if (Math.Abs((VoltDH2 * 256 + VoltDL2) - (VoltDH * 256 + VoltDL)) <= 125)
                        {
                            CaliDHValue.Add(VoltDH);
                            if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                break;
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (!com.MixReadStCur_ADCValue(CurRange, out VoltDL2, out VoltDH2))
                                    break;
                                System.Threading.Thread.Sleep(300);
                                if (!com.MixReadStCur_ADCValue(CurRange, out VoltDL, out VoltDH))
                                    break;
                                if (Math.Abs((VoltDH2 * 256 + VoltDL2) - (VoltDH * 256 + VoltDL)) <= 125)
                                {
                                    CaliDHValue.Add(VoltDH);
                                    if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                        return false;
                                    break;
                                }
                                if (i == 3)
                                {
                                    Point = "ADC高位读值循环读取五次后，均不一致！1." + VoltDL2 + " " + VoltDH2 + ";2." + VoltDL + " " + VoltDL;
                                    return false;
                                }
                            }
                            //System.Threading.Thread.Sleep(50);
                        }
                        if (s == 0 || s == 4 || s == 8 || s == 12)
                        {
                            Result = "√";
                        }
                        else
                        {
                            if (CaliDHValue[s] > CaliDHValue[s - 1])
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
                            if (!com.MixSetCellVoltValue(0))//设定电压
                                return false;

                            com.MixReadCellADC((Convert.ToByte(Vol_ADC_Add_L[s])), out ADCDL, out ADCDM, out ADCDH);
                            if (!(-10 <= (ADCDH - refADCDH) && (ADCDH - refADCDH) <= 10))
                            {
                                Point = "读取ADC高值:" + ADCDH.ToString("X2") + ";存在异常，请检查!";
                                return false;
                            }

                        }
                        else continue;
                    }

                }
                else return false;
                #region 点检所有的点

                if (!StopStatus)
                {
                    bool[] ChkTestNG = new bool[CaliValue.Count];
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
                        else if (s < 12 && s >= 8)
                        {
                            CurRange = 0x02;
                        }
                        else if (s < 16 && s >= 12)
                        {
                            CurRange = 0x03;
                        }    
                        Point = CaliValue[s].ToString() + "uA点检NG";
                        if (!com.MixStCurRange(0x01, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                            return false;
                        if (!com.MixSetCellVoltValue((CaliValueset[s])))//设定电压
                            return false;
                        System.Threading.Thread.Sleep(200);
                        if (!com.MixReadStCurValue(0x00, CurRange, out CellCurCaliTesterValue1))
                            return false;
                        if (Math.Abs(CellCurCaliTesterValue1 - CaliValue[s]) >= CurCheckAcc || CaliValue[s] == 0)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (!com.MixReadStCurValue(0x00, CurRange, out CellCurCaliTesterValue1))
                                    return false;
                                if (Math.Abs(CellCurCaliTesterValue1 - CaliValue[s]) < CurCheckAcc)
                                    break;
                                else continue;
                            }
                        }
                        Err1 = CellCurCaliTesterValue1 - Convert.ToDouble(CaliValue[s]);
                        if (Math.Abs(Err1) < CurCheckAcc)
                        { Result = "√"; ChkTestNG[s] = false; }
                        else
                        { Result = "×"; ChkTestNG[s] = true; }
                        UpdateUidelegate(DeviceType, Box, "STC_2000uA点检", CaliValue[s].ToString(), "", CellCurCaliTesterValue1.ToString(), Err1.ToString("f2"), "", CurCheckAcc.ToString(), Result);
                        if (s == CaliValue.Count - 1)
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
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
