using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool CNTstCurP(string Box, double CurCaliAcc, double CurCheckAcc, string VoltAccJudgeRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<int> ChgVolValue = new List<int> { };
            //int[] CaliValue = { 0, 1, 3, 5 };//静态电流校正点
            //int[] ChgVolValue = { 0, 1010, 3030, 5050 };//静态电流设置电压
            //int[] CNT_DAC_Set_H = new int[4];//DAC设置的高地址
            //int[] CNT_DAC_Set_L = new int[4];//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] CNT_ADC_AddL = new int[] { };//存放ADC设置值的低地址
            //for (int j = 0; j < 4; j++)
            //{
            //    CNT_ADC_AddL[j] = 0X98 + j * 2;//循环设定ADC的读取值的地址
            //}
            List<byte> CaliDHValue = new List<byte> { };
            double CNTCurCaliTesterValue = 0;
            //double CNTCurCheckDMMValue = 0;
            string CNTDMMValue = "";
            // double ChgVolDMMValue = 0;
            Point = "未设初始值";
            byte refADCDH = 0;
            byte ADCDL, ADCDH;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            //double LoadPartCurCheckDMMValue = 0;
            //double LoadPartCurCaliTesterValue1 = 0;
            double SetChgVolValue;
            byte DL2, DH2;
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, 1, 3, 5 };
                    CaliValue.AddRange(s);

                    int[] v1 = new int[] { 0, 1010, 3030, 5050 };
                    ChgVolValue.AddRange(v1);

                    refADCDH = 246;

                    CNT_ADC_AddL = new int[4];//存放ADC读取值的低地址
                    for (int j = 0; j < 4; j++)
                    {
                        CNT_ADC_AddL[j] = 0X98 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 0, 1, 3, 5 };
                    CaliValue.AddRange(h);

                    int[] v2 = new int[] { 0, 1010, 3030, 5050 };
                    ChgVolValue.AddRange(v2);

                    refADCDH = 246;

                    CNT_ADC_AddL = new int[4];//存放ADC读取值的低地址
                    for (int j = 0; j < 4; j++)
                    {
                        CNT_ADC_AddL[j] = 0X98 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
            }
            try
            {
                if (!com.MixChgVolCalibration())
                    return false;
                Thread.Sleep(100);
                if (!com.MixCNTCalibration())
                    return false;
                Thread.Sleep(100);
                if (!com.CNTstCurCal_RY("正", true))
                    return false;
                if (!StopStatus)
                {
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        byte DL = 00;
                        byte DH = 00;
                        SetChgVolValue = ChgVolValue[s];
                        Point = CaliValue[s].ToString() + "nACNT静态(正)校准NG";

                        if (!com.MixSetVoltValue(SetChgVolValue))
                            return false;
                        System.Threading.Thread.Sleep(500);
                        if (!com.MixReadCNT_ADC(out DL2, out DH2))
                            return false;
                        System.Threading.Thread.Sleep(200);
                        if (!com.MixReadCNT_ADC(out DL, out DH))
                            return false;

                        if (Math.Abs((DH2 * 256 + DL2) - (DH * 256 + DL)) <= 125)
                        {
                            CaliDHValue.Add(DH);
                            if (!com.MixWriteCNT_ADC(Convert.ToByte(CNT_ADC_AddL[s]), DL, DH))
                                return false;
                        }
                        else
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (!com.MixReadCNT_ADC(out DL2, out DH2))
                                    return false;
                                System.Threading.Thread.Sleep(300);
                                if (!com.MixReadCNT_ADC(out DL, out DH))
                                    return false;
                                if (Math.Abs((DH2 * 256 + DL2) - (DH * 256 + DL)) <= 125)
                                {
                                    CaliDHValue.Add(DH);
                                    if (!com.MixWriteCNT_ADC(Convert.ToByte(CNT_ADC_AddL[s]), DL, DH))
                                        return false;
                                    break;
                                }
                                if (i == 3)
                                {
                                    Point = "ADC高位读值循环读取五次后，均不一致";
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
                            if (CaliDHValue[s] > CaliDHValue[s - 1])
                            {
                                Result = "√";
                            }
                            else
                            {
                                Result = "×";
                            }
                        }
                        UpdateUidelegate(DeviceType, Box, CheckType, (CaliValue[s] * 1000).ToString(), "", "校准判断地址位", "", "", CurCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == CaliValue.Count - 1)
                        {
                            if (!com.MixUpdateOCV())
                                return false;
                            if (!com.MixEnable(0x00))
                                return false;
                            if (!com.MixSetVoltValue(0))
                                return false;
                            com.MixReadOcvADC((Convert.ToByte(CNT_ADC_AddL[s])), out ADCDL, out ADCDH);
                            if (!(-5 <= (ADCDH - refADCDH) && (ADCDH - refADCDH) <= 5))
                            {
                                Point = "读取ADC高值:" + ADCDH.ToString("X2") + ";存在异常，请检查!";
                                return false;
                            }

                        }
                        else continue;
                    }
                }
                else return false;
                if (!StopStatus)//点检
                {
                    if (!com.MixChgVolCalibration())
                        return false;
                    Thread.Sleep(100);
                    if (!com.MixCNTCalibration())
                        return false;
                    Thread.Sleep(100);
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        Point = CaliValue[s].ToString() + "nACNT静态(正)点检NG";

                        if (!com.MixSetVoltValue(ChgVolValue[s]))
                            return false;
                        System.Threading.Thread.Sleep(200);
                        for (int i = 0; i < 5; i++)
                        {
                            if (!com.MixReadCNTValue(0x00, out CNTCurCaliTesterValue))
                                return false;
                            Err1 = CNTCurCaliTesterValue - (CaliValue[s] * 1000);
                            if (Math.Abs(Err1) >= CurCheckAcc)
                            {
                                System.Threading.Thread.Sleep(200);
                                continue;
                            }
                            else
                                break;
                        }
                        if (Math.Abs(Err1) < CurCheckAcc)
                        { Result = "√"; ChkTestNG[s] = false; }
                        else
                        { Result = "×"; ChkTestNG[s] = true; }
                        UpdateUidelegate(DeviceType, Box, "CNT(正)点检_nA", (CaliValue[s] * 1000).ToString(), CNTDMMValue, CNTCurCaliTesterValue.ToString(), Err1.ToString("f2"), "", CurCheckAcc.ToString(), Result);
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
                return true;

            }
            catch (Exception) { return false; }
            finally
            {
                com.MixCHGEnable();
                com.MixCNTEnable();
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }

            }
        }
    }

}
