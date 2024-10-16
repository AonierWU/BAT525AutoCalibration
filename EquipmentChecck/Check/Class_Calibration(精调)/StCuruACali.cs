using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool StCuruACali(string Box, double CurCaliAcc, double CurCheckAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<double> CaliValueset = new List<double> { };
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址   
           // List<byte> CaliDHValue = new List<byte> { };
            List<int> CheckCaliValue = new List<int> { };

            Point = "未设初始值";
            byte CurRange;
            byte VoltDL, VoltDH;
            byte VoltDL1, VoltDH1;
            double Err1 = 0;
            string Result = "√";
            double CellCurCaliTesterValue1 = 0;
            switch (DeviceType)
            {
                case "BAT525C":
                    int[] s = new int[] { 0, 20, 60, 100, 100, 160, 200, 200, 400, 600, 600, 800, 1000 };
                    CaliValue.AddRange(s);

                    double[] v1 = new double[] { 0, 40.7, 122, 203.3, 203.3, 325.3, 406.6, 406.6, 813.3, 1220, 1220, 1626.6, 2033.3 };
                    CaliValueset.AddRange(v1);

                    //refADCDH = 239;

                    Vol_ADC_Add_L = new int[13];//存放ADC读取值的低地址
                    for (int j = 0; j < 13; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x98 + j * 2;//uA addr
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 0, 40, 120, 200, 200, 320, 400, 400, 800, 1200, 1200, 1600, 2000 };
                    CaliValue.AddRange(d);

                    double[] v2 = new double[] { 0, 80, 244, 408, 408, 652, 815, 815, 1630, 2451, 2451, 3268, 4083 };
                    CaliValueset.AddRange(v2);
                    Vol_ADC_Add_L = new int[13];//存放ADC读取值的低地址
                    for (int j = 0; j < 13; j++)
                    {
                        Vol_ADC_Add_L[j] = 0xA8 + j * 2;//uA addr
                    }
                    break;
            }
            try
            {
                if (!com.MixCellVoltCalibration())
                    return false;
                if (!com.MixStCurCalibration())
                    return false;
                if (!com.StCurCal_RY("1000uA", true))
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
                        else if (s < 7 && s >= 4)
                        {
                            CurRange = 0x01;
                        }
                        else if (s < 10 && s >= 7)
                        {
                            CurRange = 0x02;
                        }
                        else if (s < 13 && s >= 10)
                        {
                            CurRange = 0x03;
                        }
                        Point = CaliValue[s].ToString() + "uA校准NG";
                        if (CaliValue[s] == 0)
                        {
                            if (!com.MixReadCellDAC(Convert.ToByte(Vol_ADC_Add_L[s]), out VoltDL, out VoltDH))
                                break;
                            //if(VoltDH!=0x00)
                            //{
                            //    Point = "静态电流0uA校准值异常，请手动校准0uA点";
                            //    return false;
                            //}
                            //else
                            //{
                            //    CaliDHValue.Add(VoltDH);
                            //}
                            CheckCaliValue.Add(Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                            if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                return false;
                        }
                        else
                        {
                            if (!com.MixStCurRange(0x01, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                                return false;
                            if (!com.MixSetCellVoltValue(CaliValueset[s]))//设定电压
                                return false;
                            System.Threading.Thread.Sleep(300);
                            if (!com.MixReadStCur_ADCValue(0x0B, out VoltDL1, out VoltDH1))
                                return false;
                            System.Threading.Thread.Sleep(300);
                            if (!com.MixReadStCur_ADCValue(0x0B, out VoltDL, out VoltDH))
                                return false;
                            if (Math.Abs((VoltDH1 * 256 + VoltDL1) - (VoltDH * 256 + VoltDL)) <= 125)
                            {
                                CheckCaliValue.Add(Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                    return false;
                            }
                            else
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    if (!com.MixReadStCur_ADCValue(0x0B, out VoltDL1, out VoltDH1))
                                        return false;
                                    System.Threading.Thread.Sleep(300);
                                    if (!com.MixReadStCur_ADCValue(0x0B, out VoltDL, out VoltDH))
                                        return false;
                                    if (Math.Abs((VoltDH1 * 256 + VoltDL1) - (VoltDH * 256 + VoltDL)) <= 125)
                                    {
                                        //CaliDHValue.Add(VoltDH);
                                        CheckCaliValue.Add(Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                        if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                            return false;
                                        break;
                                    }
                                    if (i == 3)
                                    {
                                        Point = "ADC高位读值循环读取五次后，均不一致！";
                                        return false;
                                    }
                                }
                            }
                        }
                        if (s == 0 || s == 4 || s == 7 || s == 10)
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
                        UpdateUidelegate(DeviceType, Box, CheckType+"校准", CaliValue[s].ToString(), "", "校准判断地址位", "", "", CurCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == CaliValue.Count - 1)
                        {
                            if (!com.MixUpdateCellVolt())
                                return false;
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
                        else if (s < 4 && s >= 4)
                        {
                            CurRange = 0x01;
                        }
                        else if (s < 10 && s >= 4)
                        {
                            CurRange = 0x02;
                        }
                        else if (s < 13 && s >= 10)
                        {
                            CurRange = 0x03;
                        }    
                        Point = CaliValue[s].ToString() + "uA点检NG";
                        if (!com.MixStCurRange(0x01, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                            return false;
                        if (!com.MixSetCellVoltValue((CaliValueset[s])))//设定电压
                            return false;
                        System.Threading.Thread.Sleep(200);
                        //if (CaliValue[s] == 0)
                        //{
                        //    if (!com.MixStCurCalibration())
                        //        return false;
                        //    if (!com.Stc_0uA(false))
                        //        return false;
                        //    for (int i = 0; i < 30; i++)
                        //    {
                        //        System.Threading.Thread.Sleep(100);
                        //        if (!com.MixReadStCurValue(0x00, CurRange, out CellCurCaliTesterValue1))
                        //            return false;
                        //        if (CellCurCaliTesterValue1 == 0 || i == 29)
                        //        {
                        //            break;
                        //        }
                        //        else
                        //            continue;
                        //    }

                        //}
                        if (!com.MixReadStCurValue(0x00, CurRange, out CellCurCaliTesterValue1))
                            return false;
                        if (!com.MixReadStCurValue(0x00, CurRange, out CellCurCaliTesterValue1))
                            return false;
                        if(DeviceType=="BAT525D")
                        {
                            CellCurCaliTesterValue1 = CellCurCaliTesterValue1 * 2;
                        }
                        Err1 = CellCurCaliTesterValue1 - Convert.ToDouble(CaliValue[s]);
                        if (Math.Abs(Err1) < CurCheckAcc)
                        { Result = "√"; ChkTestNG[s] = false; }
                        else
                        { Result = "×"; ChkTestNG[s] = true; }
                        UpdateUidelegate(DeviceType, Box, CheckType+"点检", CaliValue[s].ToString(), "", CellCurCaliTesterValue1.ToString(), Err1.ToString("f2"), "", CurCheckAcc.ToString(), Result);
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
