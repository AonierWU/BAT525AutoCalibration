using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool NTCCali(string Box, double NtcCaliAcc, double NtcCheckAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };

            //int[] CaliValue = { 0, 100, 500, 1000, 1500, 2000 };//静态电流20000nA校正点
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址   
            int[] Vol_ADC_Add_L1 = new int[] { };//
            int[] Vol_ADC_Add_L2 = new int[] { };//
            List<byte> CaliDHValue = new List<byte> { };
            List<byte> CaliDLValue = new List<byte> { };
            List<int> CheckCaliValue = new List<int> { };
            byte NtcRange;
            //double NtcCaliTesterValue = 0;
            Point = "未设初始值";
            double dblResAcc = 0;
            byte VoltDL, VoltDH, VoltDL2, VoltDH2, VoltDL3, VoltDH3;
            byte Volt2DL, Volt2DH, Volt2DL2, Volt2DH2, Volt2DL3, Volt2DH3;
            byte refADCDH = 0;
            byte ADCDL, ADCDH;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            // double CellCurCheckDMMValue = 0;
            double NTCCheckTesterValue = 0;
            //double FSValue = 0.0f;
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, 100, 500, 1000, 1500, 2000 };
                    CaliValue.AddRange(s);

                    refADCDH = 243;
                    Vol_ADC_Add_L = new int[6];//存放DAC设置值的低地址
                    Vol_ADC_Add_L1 = new int[6];//存放ADC读取值的低地址
                    Vol_ADC_Add_L2 = new int[6];
                    for (int j = 0; j < 6; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x5E + j * 2;// addr
                        Vol_ADC_Add_L1[j] = 0xA9 + j * 4;// addr
                        Vol_ADC_Add_L2[j] = 0xA9 + j * 4 + 2;// addr
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 0, 100, 500, 1000, 1500, 2000 };
                    CaliValue.AddRange(h);

                    refADCDH = 243;
                    Vol_ADC_Add_L = new int[6];//存放DAC设置值的低地址
                    Vol_ADC_Add_L1 = new int[6];//存放ADC读取值的低地址
                    Vol_ADC_Add_L2 = new int[6];
                    for (int j = 0; j < 6; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x5E + j * 2;// addr
                        Vol_ADC_Add_L1[j] = 0xA9 + j * 4;// addr
                        Vol_ADC_Add_L2[j] = 0xA9 + j * 4 + 2;// addr
                    }
                    break;
                case "BAT525C":
                    int[] c = new int[] { 0, 100, 200, 400, 600, 800, 1000 };
                    CaliValue.AddRange(c);
                    Vol_ADC_Add_L = new int[7];//存放DAC设置值的低地址
                    for (int j = 0; j < 7; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x58 + j * 3;// addr
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 0, 100, 200, 400, 600, 800, 1000 };
                    CaliValue.AddRange(d);
                    Vol_ADC_Add_L = new int[7];//存放DAC设置值的低地址
                    for (int j = 0; j < 7; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x58 + j * 3;// addr
                    }
                    break;
            }
            try
            {
                if (!com.MixNTCCalibration())
                    return false;
                if (!StopStatus)
                {
                    System.Threading.Thread.Sleep(100);
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        NtcRange = 0x00;
                        Point = CaliValue[s].ToString() + "R校准NG";
                        dblResAcc = (CaliValue[s] * NtcCaliAcc) / 100;
                        if (!com.MixNTCRange(NtcRange))//00--2000R，01--20K，02--200K, 03--3M
                            return false;
                        System.Threading.Thread.Sleep(1000);
                        if (DeviceType == "BAT525C"|| DeviceType == "BAT525D")
                        {
                            if (CaliValue[s] == 0)
                            {
                                if (!com.MixReadNtcADCValue(Convert.ToByte(Vol_ADC_Add_L[s]), out VoltDL, out VoltDH, out VoltDH2))
                                    break;
                                //if (VoltDL < 0XFF && VoltDH == 0X00 && VoltDH2 == 0X00)
                                //{
                                //    //CaliDHValue.Add(VoltDH2);
                                //    //CaliDLValue.Add(VoltDH);
                                //    //CheckCaliValue.Add(Convert.ToInt16(VoltDH2) * 65536 + Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                //    CheckCaliValue.Add(Convert.ToInt16(VoltDH2) * 65536 + Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                //    if (!com.MixWriteNTC_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH, 0X00, 0X00, VoltDH2, 0X00, 0X00, 0X00))
                                //        break;
                                //}
                                //else
                                //{
                                //    Point = "NTC_1K量程0Ω读取异常,请手动写入零点!";
                                //    return false;
                                //}
                                CheckCaliValue.Add(Convert.ToInt16(VoltDH2) * 65536 + Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                if (!com.MixWriteNTC_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH, 0X00, 0X00, VoltDH2, 0X00, 0X00, 0X00))
                                    break;
                            }
                            else
                            {
                                NtcRange = 0X06;
                                if (!com.IDRResCal_RY(CaliValue[s], true))
                                    return false;
                                if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out Volt2DL, out Volt2DH, out Volt2DL2, out Volt2DH2, out Volt2DL3, out Volt2DH3))
                                    break;
                                System.Threading.Thread.Sleep(200);
                                if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out VoltDL, out VoltDH, out VoltDL2, out VoltDH2, out VoltDL3, out VoltDH3))
                                    break;
                                if (Math.Abs((Volt2DH2 * 65536 + Volt2DH * 256 + Volt2DL) - (VoltDH2 * 65536 + VoltDH * 256 + VoltDL)) <= 125)
                                {
                                    //CaliDHValue.Add(VoltDH2);
                                    //CaliDLValue.Add(VoltDH);
                                    CheckCaliValue.Add(Convert.ToInt16(VoltDH2) * 65536 + Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                    if (!com.MixWriteNTC_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH, 0X00, 0X00, VoltDH2, 0X00, 0X00, 0X00))
                                        break;
                                }
                                else
                                {
                                    for (int i = 0; i <= 4; i++)
                                    {
                                        if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out Volt2DL, out Volt2DH, out Volt2DL2, out Volt2DH2, out Volt2DL3, out Volt2DH3))
                                            break;
                                        System.Threading.Thread.Sleep(200);
                                        if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out VoltDL, out VoltDH, out VoltDL2, out VoltDH2, out VoltDL3, out VoltDH3))
                                            break;
                                        if (Math.Abs((Volt2DH2 * 65536 + Volt2DH * 256 + Volt2DL) - (VoltDH2 * 65536 + VoltDH * 256 + VoltDL)) <= 125)
                                        {
                                            CheckCaliValue.Add(Convert.ToInt16(VoltDH2) * 65536 + Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                            if (!com.MixWriteNTC_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH, 0X00, 0X00, VoltDH2, 0X00, 0X00, 0X00))
                                                break;
                                            break;
                                        }
                                        if (i == 4)
                                        {
                                            Point = "ADC高位读值循环读取五次后，均不一致";
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!com.IDRResCal_RY(CaliValue[s], true))
                                return false;
                            System.Threading.Thread.Sleep(100);
                            if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out Volt2DL, out Volt2DH, out Volt2DL2, out Volt2DH2, out Volt2DL3, out Volt2DH3))
                                break;
                            System.Threading.Thread.Sleep(200);
                            if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out VoltDL, out VoltDH, out VoltDL2, out VoltDH2, out VoltDL3, out VoltDH3))
                                break;
                            if (Math.Abs((Volt2DH * 256 + Volt2DL) - (VoltDH * 256 + VoltDL)) <= 125)
                            {
                                //System.Threading.Thread.Sleep(100);
                                //CaliDHValue.Add(VoltDH);
                                CheckCaliValue.Add(Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                if (!com.MixWriteNTC_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH, Convert.ToByte(Vol_ADC_Add_L1[s]), VoltDL2, VoltDH2, Convert.ToByte(Vol_ADC_Add_L2[s]), VoltDL3, VoltDH3))
                                    break;
                            }
                            else
                            {
                                for (int i = 0; i <= 4; i++)
                                {
                                    if (!com.MixReadNTC_ADCValue(Convert.ToByte(Vol_ADC_Add_L[s]), NtcRange, out Volt2DL, out Volt2DH, out Volt2DL2, out Volt2DH2, out Volt2DL3, out Volt2DH3))
                                        break;
                                    System.Threading.Thread.Sleep(200);
                                    if (!com.MixReadNTC_ADCValue(Convert.ToByte(Vol_ADC_Add_L[s]), NtcRange, out VoltDL, out VoltDH, out VoltDL2, out VoltDH2, out VoltDL3, out VoltDH3))
                                        break;
                                    if (Math.Abs((Volt2DH * 256 + Volt2DL) - (VoltDH * 256 + VoltDL)) <= 125)
                                    {
                                        CheckCaliValue.Add(Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                        if (!com.MixWriteNTC_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH, Convert.ToByte(Vol_ADC_Add_L1[s]), VoltDL2, VoltDH2, Convert.ToByte(Vol_ADC_Add_L2[s]), VoltDL3, VoltDH3))
                                            break;
                                        break;
                                    }
                                    if (i == 4)
                                    {
                                        Point = "ADC高位读值循环读取五次后，均不一致";
                                        return false;
                                    }
                                }

                            }
                        }
                        if (s == 0)
                        {
                            Result = "√";
                        }
                        else
                        {
                            if (CheckCaliValue[s] > CheckCaliValue[s - 1])
                                Result = "√";
                            else
                            {
                                Result = "×";
                            }
                        }
                        UpdateUidelegate(DeviceType, Box, CheckType+"校准", CaliValue[s].ToString(), "", "校准判断地址位", "", "", dblResAcc.ToString(), Result);
                        if (!com.IDRResCal_RY(CaliValue[s], false))
                            return false;
                        if (Result == "×")
                            return false;
                        if (s == CaliValue.Count - 1)
                        {
                            if (!com.MixUpdateNTC())
                                return false;
                            if (DeviceType != "BAT525C" && DeviceType != "BAT525D")
                            {
                                com.MixReadOcvADC((Convert.ToByte(Vol_ADC_Add_L[s])), out ADCDL, out ADCDH);

                                if (!(-10 <= (ADCDH - refADCDH) && (ADCDH - refADCDH) <= 10))
                                {
                                    Point = "读取ADC高值:" + ADCDH.ToString("X2") + ";存在异常，请检查!";
                                    return false;

                                }
                            }

                        }
                        else continue;


                    }
                }
                #region 点检所有的点

                if (!StopStatus)
                {
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        NtcRange = 0x00;

                        //       
                        Point = CaliValue[s].ToString() + "R点检NG";
                        dblResAcc = (CaliValue[s] * NtcCheckAcc) / 100;
                        if (!com.IDRResCal_RY(CaliValue[s], true))
                            return false;
                        if (!com.MixNTCRange(NtcRange))//00--2000R，01--20K，02--200K, 03--3M
                            return false;
                        System.Threading.Thread.Sleep(1000);
                       // if (!com.MixReadNTCValue(0x00, NtcRange, out NTCCheckTesterValue))
                            //return false;
                       // if (!com.MixReadNTCValue(0x00, NtcRange, out NTCCheckTesterValue))
                            //return false;
                        //Err1 = Convert.ToDouble(CaliValue[s]) - NTCCheckTesterValue;

                        for (int i = 0; i < 5; i++)
                        {
                            if (!com.MixReadNTCValue(0x00, NtcRange, out NTCCheckTesterValue))
                                return false;
                            Err1 = Convert.ToDouble(CaliValue[s]) - NTCCheckTesterValue;
                            if (Math.Abs(Err1) >= dblResAcc && CaliValue[s] != 0)
                            {
                                System.Threading.Thread.Sleep(200);
                                continue;
                            }
                            else break;
                        }
                        if (CaliValue[s] == 0)
                        {
                            if (Math.Abs(Err1) < 2)
                            { Result = "√"; ChkTestNG[s] = false; }
                            else
                            { Result = "×"; ChkTestNG[s] = true; }
                        }
                        else
                        {
                            if (Math.Abs(Err1) <= dblResAcc)
                            { Result = "√"; ChkTestNG[s] = false; }
                            else
                            { Result = "×"; ChkTestNG[s] = true; }
                        }
                        UpdateUidelegate(DeviceType, Box, CheckType+"点检", CaliValue[s].ToString(), "", NTCCheckTesterValue.ToString(), Err1.ToString("f2"), "", dblResAcc.ToString(), Result);
                        if (!com.IDRResCal_RY(CaliValue[s], false))
                            return false;
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
                com.MixNTCEnable();
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
