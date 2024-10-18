using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool Dsg30ACurCali(string Box, double CurCaliAcc, double CurCheckAcc, string CurAccResRegulation, string ResType, string CheckType, string DeviceType, bool CaliType,bool ExtRes, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<int> Vol_DAC_Set_H = new List<int> { };
            List<int> Vol_DAC_Set_L = new List<int> { };
            int[] Vol_DAC_Add_L = new int[] { };//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址
            int[] Vol_ADC_Add_L2 = new int[] { };//存放ADC读取值的低地址

            //int[] CaliValue = { 3000, 5000, 10000, 15000, 20000, 25000, 30000 };//电流校正点
            //int[] Vol_DAC_Set_H = new int[7] { 0x1A, 0x2A, 0x53, 0x5C, 0x7B, 0x9A, 0xB8 };//DAC设置的高地址
            //int[] Vol_DAC_Set_L = new int[7] { 0x3D, 0xAF, 0x56, 0x09, 0xA4, 0x15, 0x52 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            //double DsgCurCaliTesterValue = 0.0;
            double DsgCurCaliDMMValue = 0;
            Point = "未设初始值";
            int step = 0;
            int step_H = 0;
            int step_L = 0;
            double Resolution = 0.65;//电压零点以外的点的分辨率
           // double Deviation = 0.2;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03

            byte VoltDL, VoltDM, VoltDH, VoltDL2, VoltDH2;
            byte refDACDH = 0;
            byte refADCDH = 0;
            // byte DM = 0;
            byte DACDL, DACDH, ADCDL, ADCDM, ADCDH;
            double Err1 = 0;
            double Err2 = 0;
            string Result = "√";
            int rate = 0;
            double DsgCurCheckDMMValue = 0;
            double DsgCurCaliTesterValue1 = 0;
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 3000, 5000, 10000, 15000, 20000, 25000, 30000 };
                    CaliValue.AddRange(s);
                    int[] H = new int[] { 0x1A, 0x2A, 0x53, 0x5C, 0x7B, 0x9A, 0xB8 };
                    Vol_DAC_Set_H.AddRange(H);
                    int[] L = new int[] { 0x3D, 0xAF, 0x56, 0x09, 0xA4, 0x15, 0x52 };
                    Vol_DAC_Set_L.AddRange(L);

                    refDACDH = 184;
                    refADCDH = 183;

                    Vol_DAC_Add_L = new int[7];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[7];//存放ADC读取值的低地址
                    for (int j = 0; j < 7; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X3B + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X85 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 3000, 5000, 10000, 15000, 20000, 25000, 30000 };
                    CaliValue.AddRange(h);
                    int[] H3 = new int[] { 0x1A, 0x2A, 0x53, 0x5C, 0x7B, 0x9A, 0xB8 };
                    Vol_DAC_Set_H.AddRange(H3);
                    int[] L3 = new int[] { 0x3D, 0xAF, 0x56, 0x09, 0xA4, 0x15, 0x52 };
                    Vol_DAC_Set_L.AddRange(L3);

                    refDACDH = 184;
                    refADCDH = 183;

                    Vol_DAC_Add_L = new int[7];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[7];//存放ADC读取值的低地址
                    for (int j = 0; j < 7; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X3B + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X85 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525C":
                    int[] c = new int[] { 3500, 5000, 10000, 15000, 20000, 25000, 30000 };
                    CaliValue.AddRange(c);
                    int[] H1 = new int[] { 0x15, 0x23, 0x46, 0x69, 0x8C, 0xAE, 0xD1 };
                    Vol_DAC_Set_H.AddRange(H1);
                    int[] L1 = new int[] { 0xA0, 0x32, 0x3C, 0x6F, 0x00, 0xD5, 0xC6 };
                    Vol_DAC_Set_L.AddRange(L1);

                    Vol_DAC_Add_L = new int[7];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[7];//存放ADC读取值的低地址
                    Vol_ADC_Add_L2 = new int[7];//存放ADC读取值的低地址
                    for (int j = 0; j < 7; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X7C + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0XA4 + j * 3;//循环设定ADC的读取值的地址
                        Vol_ADC_Add_L2[j] = 0XCC + j * 2;
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 3500, 5000, 10000, 15000, 20000, 25000, 30000 };
                    CaliValue.AddRange(d);
                    int[] H2 = new int[] { 0x15, 0x23, 0x46, 0x69, 0x8C, 0xAE, 0xD1 };
                    Vol_DAC_Set_H.AddRange(H2);
                    int[] L2 = new int[] { 0xA0, 0x32, 0x3C, 0x6F, 0x00, 0xD5, 0xC6 };
                    Vol_DAC_Set_L.AddRange(L2);

                    Vol_DAC_Add_L = new int[7];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[7];//存放ADC读取值的低地址
                    Vol_ADC_Add_L2 = new int[7];//存放ADC读取值的低地址
                    for (int j = 0; j < 7; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X7C + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0XA4 + j * 3;//循环设定ADC的读取值的地址
                        Vol_ADC_Add_L2[j] = 0XCC + j * 2;
                    }
                    break;
            }
            try
            {
                if (ExtRes)
                {
                    if (ResType == "100mΩ")
                        rate = 10000;
                    else if (ResType == "10mΩ")
                        rate = 100000;
                    else if (ResType == "1mΩ")
                        rate = 1000000;
                    else if (ResType == "2mΩ")
                        rate = 500000;
                }
                else
                {
                    rate = 1000000;
                }
                if (!com.MixDsgCurCalibration())
                    return false;
                if (!com.MixSetCurRange(0x01))
                    return false;
                if (!com.MixSendChgVolt(0xC8, 0xAF))
                    return false;
                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                    return false;
                if (ExtRes)
                {
                    if (!com.DsgCur30ACalExtRes_RY(true))
                        return false;
                }
                else
                {
                    if (!com.DsgCur30ACal_RY(true))
                        return false;
                }
                System.Threading.Thread.Sleep(100);
                if (!StopStatus)
                {

                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {
                        Point = CaliValue[s].ToString() + "mA校准NG";
                        int DL = Vol_DAC_Set_L[s];
                        int DH = Vol_DAC_Set_H[s];
                        for (int i = 0; i < 30; i++)
                        {
                            //设定预设置
                            if (!com.MixSendDsgCur(Convert.ToByte(DL), Convert.ToByte(DH)))
                                return false;
                            if (!com.MixEnable(0x01))
                                return false;
                            System.Threading.Thread.Sleep(100);
                            if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out DsgCurCaliDMMValue))
                                return false;
                            DsgCurCaliDMMValue = Math.Abs(DsgCurCaliDMMValue * rate);
                            //判断测试
                            if (!com.MixEnable(0x00))
                                return false;
                            if ((DsgCurCaliDMMValue > CaliValue[s] - CurCaliAcc) && (DsgCurCaliDMMValue < CaliValue[s] + CurCaliAcc))
                            {
                                if (!com.MixEnable(0x01))
                                    return false;
                                System.Threading.Thread.Sleep(100);
                                if (!com.MixWriteVoltDAC_ADCCaliValue(Convert.ToByte(Vol_DAC_Add_L[s]), Convert.ToByte(DL), Convert.ToByte(DH)))
                                    break;
                                System.Threading.Thread.Sleep(50);
                                if (!com.MixReadCurADCCaliValue(out VoltDL, out VoltDM, out VoltDH, out VoltDL2, out VoltDH2))
                                    break;
                                if (!com.MixReadCurADCCaliValue(out VoltDL, out VoltDM, out VoltDH, out VoltDL2, out VoltDH2))
                                    break;
                                if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                                {
                                    if (!com.MixWriteVoltADCCaliValue(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDM, VoltDH))
                                        break;
                                    if (!com.MixWriteVoltDAC_ADCCaliValue(Convert.ToByte(Vol_ADC_Add_L2[s]), VoltDL2, VoltDH2))
                                        break;
                                }
                                else
                                {
                                    if (!com.MixWriteVoltDAC_ADCCaliValue(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                        break;
                                }
                                //System.Threading.Thread.Sleep(50);

                                break;//满足结束循环
                            }
                            else if (DsgCurCaliDMMValue > (CaliValue[s]))//如果万用表的值过大，则要减小偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((DsgCurCaliDMMValue - CaliValue[s]) / Resolution);
                                step_H = step / 256;
                                step_L = step % 256;

                                DL = DL - step_L;

                                if (DL < 0)
                                {
                                    DL = DL + 256;
                                    DH = DH - step_H - 1;
                                }
                                else
                                {
                                    DH = DH - step_H;
                                }
                                continue;//结束当前的循环，进入接下来的循环，判断循环的条件是否满足
                            }
                            else if (DsgCurCaliDMMValue < (CaliValue[s]))//如果万用表的值过小，则要增大偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((CaliValue[s] - DsgCurCaliDMMValue) / Resolution);
                                step_H = step / 256;
                                step_L = step % 256;

                                DL = DL + step_L;

                                if (DL > 255)
                                {
                                    DL = DL - 256;
                                    DH = DH + step_H + 1;
                                }
                                else
                                {
                                    DH = DH + step_H;
                                }
                                if (DH > 255)
                                    DH = 255;
                                continue;
                            }


                        }
                        Err1 = DsgCurCaliDMMValue - Convert.ToDouble(CaliValue[s]);
                        //Err2 = DsgCurCaliTesterValue - Convert.ToDouble(CaliValue[s]);
                        if (Math.Abs(Err1) < CurCaliAcc)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), DsgCurCaliDMMValue.ToString("F2"), "校准以DMM值为准", Err1.ToString("f2"), "", CurCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == 0)
                        {
                            if (!com.MixUpdate())
                                break;
                            if (DeviceType != "BAT525C" && DeviceType != "BAT525D")
                            {
                                com.MixReadChgVolDAC((Convert.ToByte(Vol_DAC_Add_L[CaliValue.Count - 1])), out DACDL, out DACDH);
                                com.MixReadChgVolADC((Convert.ToByte(Vol_ADC_Add_L[CaliValue.Count - 1])), out ADCDL, out ADCDM, out ADCDH);
                                if (!((-5 <= (DACDH - refDACDH) && (DACDH - refDACDH) <= 5) && (-5 <= (ADCDH - refADCDH) && (ADCDH - refADCDH) <= 5)))
                                {
                                    Point = "设定DAC高值:" + DACDH.ToString("X2") + ";" + "读取ADC高值:" + ADCDH.ToString("X2") + ";存在异常，请检查!";
                                    return false;
                                }
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
                    System.Threading.Thread.Sleep(1000);
                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {
                        Point = CaliValue[s].ToString() + "mA点检NG";
                        if (!com.MixSetCurValue(0x01, CaliValue[s]))
                            return false;
                        if (!com.MixEnable(0x01))
                            return false;
                        System.Threading.Thread.Sleep(200);
                        for (int i = 0; i < 5; i++)
                        {
                            if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out DsgCurCheckDMMValue))
                                return false;
                            if (!com.MixReadCurValue(out DsgCurCaliTesterValue1))
                                return false;
                            DsgCurCheckDMMValue = Math.Abs(DsgCurCheckDMMValue * rate);
                            if (Math.Abs(DsgCurCheckDMMValue - CaliValue[s]) >= CurCheckAcc)
                            {
                                System.Threading.Thread.Sleep(300);
                                continue;
                            }
                            else break;
                        }
                        if (!com.MixEnable(0x00))
                            return false;
                        switch (CurAccResRegulation)
                        {
                            case "设置值、万用表、设备读值":
                                Err1 = DsgCurCheckDMMValue - CaliValue[s];
                                Err2 = DsgCurCaliTesterValue1 - DsgCurCheckDMMValue;

                                if (Math.Abs(Err1) < CurCheckAcc && Math.Abs(Err2) < CurCheckAcc)
                                { Result = "√"; ChkTestNG[s] = false; }
                                else
                                { Result = "×"; ChkTestNG[s] = true; }
                                break;
                            case "设置值、万用表":
                                Err1 = DsgCurCheckDMMValue - CaliValue[s];
                                if (Math.Abs(Err1) < CurCheckAcc)
                                { Result = "√"; ChkTestNG[s] = false; }
                                else
                                { Result = "×"; ChkTestNG[s] = true; }
                                break;
                            case "设置值、设备读值":
                                Err1 = DsgCurCaliTesterValue1 - CaliValue[s];
                                if (Math.Abs(Err1) < CurCheckAcc)
                                { Result = "√"; ChkTestNG[s] = false; }
                                else
                                { Result = "×"; ChkTestNG[s] = true; }
                                break;
                            case "万用表、设备读值":
                                Err1 = DsgCurCheckDMMValue - DsgCurCaliTesterValue1;
                                if (Math.Abs(Err1) < CurCheckAcc)
                                { Result = "√"; ChkTestNG[s] = false; }
                                else
                                { Result = "×"; ChkTestNG[s] = true; }
                                break;
                            default:
                                break;
                        }
                        UpdateUidelegate(DeviceType, Box, "放电电流30A点检", CaliValue[s].ToString(), DsgCurCheckDMMValue.ToString("f2"), DsgCurCaliTesterValue1.ToString(), Err1.ToString("f2"), Err2.ToString("f2"), CurCheckAcc.ToString(), Result);
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
                com.MixEnable(0x00);
                com.MixSendChgVolt(0x00, 0x00);
                com.MixSendDsgCur(0x00, 0x00);
                //com.DsgCur30ACal_RY(false);
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
