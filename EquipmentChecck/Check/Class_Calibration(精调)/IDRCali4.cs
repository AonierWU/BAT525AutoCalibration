using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool IDRCali4(string Box, double NtcCaliAcc, double NtcCheckAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            //int[] CaliValue = { 200000, 500000, 1000000,2000000,3000000 };//静态电流20000nA校正点
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址   
            int[] Vol_ADC_Add_L1 = new int[] { };//
            int[] Vol_ADC_Add_L2 = new int[] { };//
            //List<byte> CaliDHValue = new List<byte> { };
            //List<byte> CaliDLValue = new List<byte> { };
            List<int> CheckCaliValue = new List<int> { };
            byte NtcRange;
            //double NtcCaliTesterValue = 0;
            Point = "未设初始值";
            double dblResAcc = 0;
            byte VoltDL, VoltDH, VoltDL2, VoltDH2, VoltDL3, VoltDH3;
            byte Volt2DL, Volt2DH, Volt2DL2, Volt2DH2, Volt2DL3, Volt2DH3;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            // int rate = 0;
            // double CellCurCheckDMMValue = 0;
            double NTCCheckTesterValue = 0;
            switch (DeviceType)
            {
                case "BAT525C":
                    int[] c = new int[] { 100000, 200000, 300000, 400000, 500000, 600000, 800000, 1000000 };
                    CaliValue.AddRange(c);

                    Vol_ADC_Add_L = new int[8];//存放DAC设置值的低地址
                    for (int j = 0; j < 8; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x40 + j * 3;// addr
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 100000, 200000, 300000, 400000, 500000, 600000, 800000, 1000000 };
                    CaliValue.AddRange(d);

                    Vol_ADC_Add_L = new int[8];//存放DAC设置值的低地址
                    for (int j = 0; j < 8; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x40 + j * 3;// addr
                    }
                    break;
            }
            try
            {
                if (!com.MixIDRCalibration())
                    return false;
                if (!StopStatus)
                {
                    System.Threading.Thread.Sleep(200);
                    NtcRange = 0x03;
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        if (!StopStatus)
                        {
                            Point = CaliValue[s].ToString() + "R校准NG";
                            dblResAcc = (CaliValue[s] * NtcCaliAcc) / 100;
                            if (!com.IDRResCal_RY(CaliValue[s], true))
                                return false;
                            if (!com.MixIDRRange(NtcRange))//00--2000R，01--20K，02--200K, 03--3M
                                return false;
                            NtcRange = 0X05;
                            System.Threading.Thread.Sleep(100);
                            if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out Volt2DL, out Volt2DH, out Volt2DL2, out Volt2DH2, out Volt2DL3, out Volt2DH3))
                                break;
                            System.Threading.Thread.Sleep(300);
                            if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out VoltDL, out VoltDH, out VoltDL2, out VoltDH2, out VoltDL3, out VoltDH3))
                                break;
                            if (Math.Abs((Volt2DH2 * 65536 + Volt2DH * 256 + Volt2DL) - (VoltDH2 * 65536 + VoltDH * 256 + VoltDL)) <= 1024)
                            {
                                //System.Threading.Thread.Sleep(100);
                                //CaliDHValue.Add(VoltDH2);
                                //CaliDLValue.Add(VoltDH);
                                CheckCaliValue.Add(Convert.ToInt16(VoltDH2) * 65536 + Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                if (!com.MixWriteNTC_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH, 0X00, 0X00, VoltDH2, 0X00, 0X00, 0X00))
                                    break;
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out Volt2DL, out Volt2DH, out Volt2DL2, out Volt2DH2, out Volt2DL3, out Volt2DH3))
                                        break;
                                    System.Threading.Thread.Sleep(300);
                                    if (!com.MixReadNTC_ADCValue(Convert.ToByte(s), NtcRange, out VoltDL, out VoltDH, out VoltDL2, out VoltDH2, out VoltDL3, out VoltDH3))
                                        break;
                                    if (Math.Abs((Volt2DH2 * 65536 + Volt2DH * 256 + Volt2DL) - (VoltDH2 * 65536 + VoltDH * 256 + VoltDL)) <= 1024)
                                    {
                                        //CaliDHValue.Add(VoltDH2);
                                        //CaliDLValue.Add(VoltDH);
                                        CheckCaliValue.Add(Convert.ToInt16(VoltDH2) * 65536 + Convert.ToInt16(VoltDH) * 256 + Convert.ToInt16(VoltDL));
                                        if (!com.MixWriteNTC_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH, 0X00, 0X00, VoltDH2, 0X00, 0X00, 0X00))
                                            break;
                                        break;
                                    }
                                    if (i == 2)
                                    {
                                        Point = "ADC高位读值循环读取三次后，均不一致";
                                        return false;
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
                            UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), "", "校准判断地址位", "", "", dblResAcc.ToString(), Result);
                            if (!com.IDRResCal_RY(CaliValue[s], false))
                                return false;
                            if (Result == "×")
                                return false;
                            if (s == CaliValue.Count - 1)
                            {
                                if (!com.MixUpdateNTC())
                                    return false;

                            }
                            else continue;
                        }
                        else
                            break;
                    }
                }
                #region 点检所有的点

                if (!StopStatus)
                {
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        Point = CaliValue[s].ToString() + "R点检NG";
                        dblResAcc = (CaliValue[s] * NtcCheckAcc) / 100;
                        if (!com.IDRResCal_RY(CaliValue[s], true))
                            return false;
                        if (!com.MixIDRRange(0x03))//00--2000R，01--20K，02--200K, 03--3M
                            return false;
                        System.Threading.Thread.Sleep(100);
                        //if (!com.MixReadIDRValue(out NTCCheckTesterValue))
                        //    return false;
                        //Err1 = Convert.ToDouble(CaliValue[s]) - NTCCheckTesterValue;
                        for (int i = 0; i < 5; i++)
                        {
                            if (!com.MixReadIDRValue(out NTCCheckTesterValue))
                                return false;
                            Err1 = Convert.ToDouble(CaliValue[s]) - NTCCheckTesterValue;
                            if (Math.Abs(Err1) >= dblResAcc)
                            {
                                System.Threading.Thread.Sleep(200);
                                continue;
                            }
                            else
                                break;
                        }
                        if (Math.Abs(Err1) < dblResAcc)
                        { Result = "√"; ChkTestNG[s] = false; }
                        else
                        { Result = "×"; ChkTestNG[s] = true; }
                        UpdateUidelegate(DeviceType, Box, "IDR电阻_1000K点检", CaliValue[s].ToString(), "", NTCCheckTesterValue.ToString(), Err1.ToString("f2"), "", dblResAcc.ToString(), Result);
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
            }
        }
    }
}
