using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool ChgVoltCali(string Box, double VoltCaliAcc, double VoltCheckAcc, string VoltAccJudgeRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<int> Vol_DAC_Set_H = new List<int> { };
            List<int> Vol_DAC_Set_L = new List<int> { };
            //int[] CaliValue = { 0, 20, 100, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000 };//电压校正点
            //int[] Vol_DAC_Set_H = new int[12] { 0x01, 0x03, 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94, 0xAC, 0xC5, 0xDD, 0xF6 };//DAC设置的高地址
            //int[] Vol_DAC_Set_L = new int[12] { 0xAF, 0x1F, 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79, 0xE8, 0x58, 0xC7, 0x37 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] Vol_DAC_Add_L = new int[] { };//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址
            int[] Vol_ADC_Add_L2 = new int[] { };

            //double ChgVolCaliTesterValue = 0;
            double ChgVolCaliDMMValue = 0;
            double ChgVolCheckDMMValue = 0;
            double ChgVolCheckTesterValue = 0;
            int step = 0;
            int step_H = 0;
            int step_L = 0;
            Point = "未设初始值";
            double Resolution = 0.076;//电压零点以外的点的分辨率
            double Deviation = 0.05;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03

            byte VoltDL, VoltDM, VoltDH;
            byte refDACDH = 0;
            byte refADCDH = 0;
            //int DM = 0;
            byte DACDL, DACDH, ADCDL, ADCDM, ADCDH;
            double Err1 = 0;
            double Err2 = 0;
            string Result = "√";
            switch (DeviceType)
            {
                case "BAT525G":
                       

                    refDACDH = 248;
                    refADCDH = 248;

                    Vol_DAC_Add_L = new int[12];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[12];//存放ADC读取值的低地址
                    for (int j = 0; j < 12; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X07 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X51 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 0, 20, 100, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000 };
                    CaliValue.AddRange(h);
                    int[] H3 = new int[] { 0x03, 0x03, 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94, 0xAC, 0xC5, 0xDD, 0xF6 };
                    Vol_DAC_Set_H.AddRange(H3);
                    int[] L3 = new int[] { 0x00, 0x1F, 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79, 0xE8, 0x58, 0xC7, 0x37 };
                    Vol_DAC_Set_L.AddRange(L3);

                    refDACDH = 248;
                    refADCDH = 248;

                    Vol_DAC_Add_L = new int[12];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[12];//存放ADC读取值的低地址
                    for (int j = 0; j < 12; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X07 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X51 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525C":
                    int[] c = new int[] { 0, 20, 100, 1000, 2000, 2500, 3000, 3600, 3800, 4000, 4200, 4500, 5000 };
                    CaliValue.AddRange(c);
                    int[] H1 = new int[] { 0X00, 0X00, 0X04, 0X31, 0X62, 0X7B, 0X94, 0XB1, 0XBB, 0XC5, 0XCF, 0XDE, 0XF6 };
                    int[] L1 = new int[] { 0X8B, 0XFE, 0XF2, 0X55, 0XC5, 0X5D, 0X1D, 0XCA, 0XAE, 0X97, 0X74, 0X31, 0XDC };
                    Vol_DAC_Set_H.AddRange(H1);
                    Vol_DAC_Set_L.AddRange(L1);

                    Vol_DAC_Add_L = new int[13];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[13];//存放ADC读取值的低地址
                    Vol_ADC_Add_L2 = new int[13];
                    for (int j = 0; j < 13; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X04 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X1E + j * 3;//循环设定ADC的读取值的地址
                        Vol_ADC_Add_L2[j] = 0X45 + j * 3;
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 0, 20, 100, 1000, 2000, 2500, 3000, 3600, 3800, 4000, 4200, 4500, 5000 };
                    CaliValue.AddRange(d);
                    int[] H2 = new int[] { 0X00, 0X00, 0X04, 0X31, 0X62, 0X7B, 0X94, 0XB1, 0XBB, 0XC5, 0XCF, 0XDE, 0XF6 };
                    int[] L2 = new int[] { 0X8B, 0XFE, 0XF2, 0X55, 0XC5, 0X5D, 0X1D, 0XCA, 0XAE, 0X97, 0X74, 0X31, 0XDC };
                    Vol_DAC_Set_H.AddRange(H2);
                    Vol_DAC_Set_L.AddRange(L2);

                    Vol_DAC_Add_L = new int[13];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[13];//存放ADC读取值的低地址
                    Vol_ADC_Add_L2 = new int[13];
                    for (int j = 0; j < 13; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X04 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X1E + j * 3;//循环设定ADC的读取值的地址
                        Vol_ADC_Add_L2[j] = 0X45 + j * 3;
                    }
                    break;
            }
            try
            {
                if (!com.MixChgVolCalibration())
                    return false;
                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                    return false;
                if (!com.ChgVolCal_RY(true))
                    return false;
                Thread.Sleep(50);
                if (!StopStatus)
                {
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        Point = CaliValue[s].ToString() + "mV校准NG";
                        int DL = Vol_DAC_Set_L[s];
                        int DH = Vol_DAC_Set_H[s];

                        for (int i = 0; i < 30; i++)
                        {
                            if (CaliValue[s] == 0)
                            {
                                if (DeviceType == "BAT525C"|| DeviceType == "BAT525D")
                                {
                                    for (int a = 0; a < 127; a++)
                                    {
                                        if (!com.MixSendZeroChgVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                            return false;
                                        System.Threading.Thread.Sleep(500);
                                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ChgVolCaliDMMValue))
                                            return false;
                                        ChgVolCaliDMMValue = ChgVolCaliDMMValue * 1000;
                                        if (-0.1 < ChgVolCaliDMMValue && ChgVolCaliDMMValue < 0.1)
                                        {
                                            break;
                                        }
                                        else if (ChgVolCaliDMMValue > 0)
                                        {
                                            DL++;
                                        }
                                        //else
                                        //{
                                        //    int j =Convert.ToInt16(ChgVolCaliDMMValue / 0.5);
                                        //    int z = (DH * 256 + DL) - j;
                                        //    DH = z / 256;
                                        //    DL = z % 256;
                                        //}
                                        //break;

                                    }
                                }
                                else
                                {
                                    int dblZero = DH * 256 + DL;
                                    for (int a = 0; a < 127; a++)
                                    {
                                        if (!com.MixSendChgVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                            return false;
                                        System.Threading.Thread.Sleep(2000);
                                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ChgVolCaliDMMValue))
                                            return false;
                                        ChgVolCaliDMMValue = ChgVolCaliDMMValue * 1000;
                                        if ((-0.05 <= ChgVolCaliDMMValue) && (ChgVolCaliDMMValue <= 0.05))
                                        {
                                            break;
                                        }
                                        else if (ChgVolCaliDMMValue - CaliValue[s] > 0)
                                        {
                                            step = Convert.ToInt16((ChgVolCaliDMMValue - CaliValue[s]) / Resolution);
                                            dblZero = dblZero - step;
                                            DH = dblZero / 256;
                                            DL = dblZero % 256;
                                        }
                                        else
                                        {
                                            step = Convert.ToInt16((Math.Abs(ChgVolCaliDMMValue) - CaliValue[s]) / Resolution);
                                            dblZero = dblZero + step;
                                            DH = dblZero / 256;
                                            DL = dblZero % 256;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!com.MixSendChgVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                    return false;
                                System.Threading.Thread.Sleep(100);
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ChgVolCaliDMMValue))
                                    return false;
                                ChgVolCaliDMMValue = ChgVolCaliDMMValue * 1000;
                            }
                            //判断测试
                            if ((ChgVolCaliDMMValue > CaliValue[s] - Deviation) && (ChgVolCaliDMMValue < CaliValue[s] + Deviation))
                            {
                                if (!com.MixWriteVoltDAC_ADCCaliValue(Convert.ToByte(Vol_DAC_Add_L[s]), Convert.ToByte(DL), Convert.ToByte(DH)))
                                    return false;
                                System.Threading.Thread.Sleep(10);
                                if (!com.MixReadVoltADCCaliValue(out VoltDL, out VoltDM, out VoltDH))
                                    return false;
                                System.Threading.Thread.Sleep(50);
                                if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                                {
                                    if (!com.MixWriteVoltADCCaliValue(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDM, VoltDH))
                                        return false;
                                    if (!com.MixWriteVoltADCCaliValue(Convert.ToByte(Vol_ADC_Add_L2[s]), VoltDL, VoltDM, VoltDH))
                                        return false;
                                }
                                else
                                {
                                    if (!com.MixWriteVoltDAC_ADCCaliValue(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                        return false;
                                }
                                System.Threading.Thread.Sleep(50);
                                //System.Threading.Thread.Sleep(50);
                                //if (!com.MixReadVoltValue(out ChgVolCaliTesterValue))
                                //    return false;
                                break;//满足结束循环
                            }
                            #region
                            else if (ChgVolCaliDMMValue > (CaliValue[s]))//如果万用表的值过大，则要减小偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((ChgVolCaliDMMValue - CaliValue[s]) / Resolution);
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
                            else if (ChgVolCaliDMMValue < (CaliValue[s]))//如果万用表的值过小，则要增大偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((CaliValue[s] - ChgVolCaliDMMValue) / Resolution);
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

                            #endregion

                        }
                        Err1 = ChgVolCaliDMMValue - Convert.ToDouble(CaliValue[s]);
                        if (Math.Abs(Err1) < VoltCaliAcc)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), ChgVolCaliDMMValue.ToString("f2"), "校准以DMM值为准", Err1.ToString("f2"), "", VoltCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == CaliValue.Count - 1)
                        {
                            if (!com.MixUpdate())
                                return false;
                            if (DeviceType != "BAT525C" && DeviceType != "BAT525D")
                            {
                                com.MixReadChgVolDAC((Convert.ToByte(Vol_DAC_Add_L[s])), out DACDL, out DACDH);
                                com.MixReadChgVolADC((Convert.ToByte(Vol_ADC_Add_L[s])), out ADCDL, out ADCDM, out ADCDH);
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
                if (!StopStatus)
                {
                    if (!com.MixChgVolCalibration())
                        return false;
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {
                        //VoltAcc = CaliValue[s] * 0.0002 + 5000 * 0.0002;//万二RD+千一FS
                        Point = CaliValue[s].ToString() + "mV点检NG";
                        if (!com.MixSetVoltValue(CaliValue[s]))
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ChgVolCheckDMMValue))
                            return false;
                        if(Math.Abs((ChgVolCheckDMMValue*1000)-CaliValue[s])> VoltCheckAcc||CaliValue[s]==0)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ChgVolCheckDMMValue))
                                    return false;
                                if ((Math.Abs(ChgVolCheckDMMValue * 1000) - CaliValue[s]) < VoltCheckAcc)
                                    break;
                                else continue;
                            }
                        }
                        ChgVolCheckDMMValue = ChgVolCheckDMMValue * 1000;
                        if (!com.MixReadVoltValue(out ChgVolCheckTesterValue))
                            return false;
                        switch (VoltAccJudgeRegulation)
                        {
                            case "设置值、万用表、设备读值":
                                Err1 = ChgVolCheckDMMValue - CaliValue[s];
                                Err2 = ChgVolCheckTesterValue - ChgVolCheckDMMValue;
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2 && Math.Abs(Err2) < 2)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                               else if (CaliValue[s] == 20)
                                {
                                    if (Math.Abs(Err1) < 0.3 && Math.Abs(Err2) < 0.3)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else
                                {
                                    if (Math.Abs(Err1) < VoltCheckAcc && Math.Abs(Err2) < VoltCheckAcc)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                break;
                            case "设置值、万用表":
                                Err1 = ChgVolCheckDMMValue - CaliValue[s];
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else if (CaliValue[s] == 20)
                                {
                                    if (Math.Abs(Err1) < 0.3 && Math.Abs(Err2) < 0.3)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else
                                {
                                    if (Math.Abs(Err1) < VoltCheckAcc)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                break;
                            case "设置值、设备读值":
                                Err1 = ChgVolCheckTesterValue - CaliValue[s];
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else if (CaliValue[s] == 20)
                                {
                                    if (Math.Abs(Err1) < 0.3 && Math.Abs(Err2) < 0.3)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else
                                {
                                    if (Math.Abs(Err1) < VoltCheckAcc)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                break;
                            case "万用表、设备读值":
                                Err1 = ChgVolCheckDMMValue - ChgVolCheckTesterValue;
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else if (CaliValue[s] == 20)
                                {
                                    if (Math.Abs(Err1) < 0.3 && Math.Abs(Err2) < 0.3)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else
                                {
                                    if (Math.Abs(Err1) < VoltCheckAcc)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                break;
                            default:
                                break;
                        }
                        UpdateUidelegate(DeviceType, Box, "充电电压点检", CaliValue[s].ToString(), ChgVolCheckDMMValue.ToString("f2"), ChgVolCheckTesterValue.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), VoltCheckAcc.ToString(), Result);
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
            catch
            {
                return false;
            }
            finally
            {
                com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO");
                com.MixSendChgVolt(0x00, 0x00);
                com.MixEnable(0X00);
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
