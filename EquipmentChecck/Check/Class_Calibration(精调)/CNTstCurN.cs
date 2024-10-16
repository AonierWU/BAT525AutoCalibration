using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool CNTstCurN(string Box, double CurCaliAcc, double CurCheckAcc, string VoltAccJudgeRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<int> ChgVolValue = new List<int> { };
            List<int> CellVolValue = new List<int> { };
            int[] CNT_ADC_AddL = new int[] { };//存放ADC设置值的低地址
            List<byte> CaliDHValue = new List<byte> { };
            double CNTCurCaliTesterValue = 0;
            Point = "未设初始值";
            double Err1 = 0;
            string Result = "√";
            byte refADCDH = 0;
            byte ADCDL, ADCDH;
            byte DL, DH;
            byte DL2, DH2;
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, -1, -3, -5 };
                    CaliValue.AddRange(s);
                    int[] v1 = new int[] { 1000, 990, 970, 950 };
                    ChgVolValue.AddRange(v1);
                    CellVolValue.Add(1000);
                    refADCDH = 8;
                    CNT_ADC_AddL = new int[4];//存放ADC读取值的低地址
                    for (int j = 0; j < 4; j++)
                    {
                        CNT_ADC_AddL[j] = 0XA0 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 0, -1, -3, -5 };
                    CaliValue.AddRange(h);
                    int[] v2 = new int[] { 1000, 990, 970, 950 };
                    ChgVolValue.AddRange(v2);
                    CellVolValue.Add(1000);
                    refADCDH = 8;
                    CNT_ADC_AddL = new int[4];//存放ADC读取值的低地址
                    for (int j = 0; j < 4; j++)
                    {
                        CNT_ADC_AddL[j] = 0XA0 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
            }
            try
            {
                //if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                //    return false;
                if (!com.CNTstCurCal_RY("负", true))
                    return false;
                if (!com.MixCellVoltCalibration())
                    return false;
                if (!com.MixChgVolCalibration())
                    return false;
                if (!com.MixCNTCalibration())
                    return false;
                System.Threading.Thread.Sleep(100);
                if (!StopStatus)
                {
                    if (!com.MixSetCellVoltValue(CellVolValue[0]))
                        return false;
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        Point = CaliValue[s].ToString() + "uACNT静态(负)校准NG";
                        if (!com.MixSetVoltValue(ChgVolValue[s]))
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
                            if (CaliDHValue[s] < CaliDHValue[s - 1])
                            {
                                Result = "√";
                            }
                            else
                            {
                                Result = "×";
                            }
                        }
                        UpdateUidelegate(DeviceType, Box, CheckType + "_nA", (CaliValue[s] * 1000).ToString(), "", "校准判断地址位", "", "", CurCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == CaliValue.Count - 1)
                        {
                            if (!com.MixUpdateOCV())
                                return false;
                            if (!com.MixSetVoltValue(0))
                                return false;
                            if (!com.MixSetCellVoltValue(0))
                                return false;
                            //if (!com.MixCellEnable(0x00))
                            //    return false;
                            //if (!com.MixEnable(0x00))
                            //    return false;
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
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    //if (!com.MixCellVoltCalibration())
                    //    return false;
                    //if (!com.MixChgVolCalibration())
                    //    return false;
                    //if (!com.MixCNTCalibration())
                    //    return false;
                    if (!com.MixSetCellVoltValue(CellVolValue[0]))
                        return false;
                    System.Threading.Thread.Sleep(100);
                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {
                        Point = CaliValue[s].ToString() + "uACNT静态(负)点检NG";
                        if (!com.MixSetVoltValue(ChgVolValue[s]))
                            return false;
                        System.Threading.Thread.Sleep(100);
                        for (int i = 0; i < 5; i++)
                        {
                            if (!com.MixReadCNTValue(0x01, out CNTCurCaliTesterValue))
                                return false;
                            Err1 = Math.Abs(CaliValue[s] * 1000) - CNTCurCaliTesterValue;
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
                        UpdateUidelegate(DeviceType, Box, "CNT(负)点检_nA", (CaliValue[s] * 1000).ToString(), "", CNTCurCaliTesterValue.ToString(), Err1.ToString("f2"), "", CurCheckAcc.ToString(), Result);
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
                return true;

            }
            catch (Exception) { return false; }
            finally
            {
                com.MixCNTEnable();
                com.MixCHGEnable();
                com.MixEnable(0x00);
                com.MixSetVoltValue(0x00);
                com.MixCellEnable(0x00);
                com.MixSetCellVoltValue(0x00);
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
                // com.CNTstCurCal_RY("负", false);
            }
        }
    }
}
