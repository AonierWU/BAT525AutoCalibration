using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool OCVCali(string Box, double VoltCaliAcc, double VoltCheckAcc, string VoltAccResRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> Cali5VOCVValue = new List<int> { };
            //int[] Cali5VOCVValue = { 0, 100, 500, 1000, 2000, 3000, 4000, 5000 };//电压校正点
            //int[] OCV5Vol_DAC_Add_L = new int[8];//存放DAC设置值的低地址
            int[] OCV5Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址
            int[] PortVol_ADC_Add_L = new int[] { };
            //for (int j = 0; j < 8; j++)
            //{
            //   // OCV5Vol_DAC_Add_L[j] = 0X4B + j * 2;//循环设定DAC的设定值的地址
            //    OCV5Vol_ADC_Add_L[j] = 0X4B + j * 2;//循环设定ADC的读取值的地址
            //}
            double OCVVolCaliDMMValue = 0;
            double OCVVolCheckDMMValue = 0;
            double ReadOCV = 0;
            int step = 0;
            int step_H = 0;
            int step_L = 0;
            Point = "未设初始值";
            //double Resolution = 0.076;//电压零点以外的点的分辨率
            double Deviation = 1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            double Err1 = 0;
            double Err2 = 0;
            string Result = "√";
            double CellVolValue = 0.0f;
            byte refADCDH = 0;
            byte ADCDL, ADCDH;
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, 100, 500, 1000, 2000, 3000, 4000, 5000 };
                    Cali5VOCVValue.AddRange(s);
                    refADCDH = 246;
                    OCV5Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址
                    for (int j = 0; j < 8; j++)
                    {
                        OCV5Vol_ADC_Add_L[j] = 0X4B + j * 2;// addr
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 0, 100, 500, 1000, 2000, 3000, 4000, 5000 };
                    Cali5VOCVValue.AddRange(h);
                    refADCDH = 246;
                    OCV5Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址
                    for (int j = 0; j < 8; j++)
                    {
                        OCV5Vol_ADC_Add_L[j] = 0X4B + j * 2;// addr
                    }
                    break;
                case "BAT525C":
                    int[] c = new int[] { 0, 20, 100, 1000, 2000, 2500, 3000, 3600, 3800, 4000, 4200, 4500, 5000 };
                    Cali5VOCVValue.AddRange(c);

                    OCV5Vol_ADC_Add_L = new int[13];//存放ADC读取值的低地址
                    PortVol_ADC_Add_L = new int[13];
                    for (int j = 0; j < 13; j++)
                    {
                        OCV5Vol_ADC_Add_L[j] = 0X04 + j * 3;// addr
                        PortVol_ADC_Add_L[j] = 0X2B + j * 3;
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 0, 20, 100, 1000, 2000, 2500, 3000, 3600, 3800, 4000, 4200, 4500, 5000 };
                    Cali5VOCVValue.AddRange(d);

                    OCV5Vol_ADC_Add_L = new int[13];//存放ADC读取值的低地址
                    PortVol_ADC_Add_L = new int[13];
                    for (int j = 0; j < 13; j++)
                    {
                        OCV5Vol_ADC_Add_L[j] = 0X04 + j * 3;// addr
                        PortVol_ADC_Add_L[j] = 0X2B + j * 3;
                    }
                    break;
            }
            try
            {
                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                    return false;
                if (!com.MixLoadPartDisEn())
                    return false;
                if (!com.MixCellVoltCalibration())
                    return false;
                System.Threading.Thread.Sleep(100);
                if (!com.MixOCVCalibration())
                    return false;
                if (!com.CellVolCal_RY(true))
                    return false;
                System.Threading.Thread.Sleep(100);
                if (!com.OCVorLVVolCal_RY(true))
                    return false;
                if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                {
                    if (!com.PortVolCal_RY(true))
                        return false;
                }
                System.Threading.Thread.Sleep(100);
                if (!StopStatus)
                {
                    int VolDL = 0; int VolDH = 247;
                    for (int s = 0; s < Cali5VOCVValue.Count; s++)
                    {
                        Point = Cali5VOCVValue[s].ToString() + "mV校准NG";
                        CellVolValue = Cali5VOCVValue[s];
                        for (int i = 0; i < 30; i++)
                        {
                            if (Cali5VOCVValue[s] < 5000 || DeviceType == "BAT525G"|| DeviceType == "BAT525H")
                            {
                                if (!com.MixSetCellVoltValue(CellVolValue))//设置充电电压
                                    return false;
                                System.Threading.Thread.Sleep(200);
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out OCVVolCaliDMMValue))
                                    return false;
                                OCVVolCaliDMMValue = OCVVolCaliDMMValue * 1000;
                                if ((OCVVolCaliDMMValue > Cali5VOCVValue[s] - VoltCaliAcc) && (OCVVolCaliDMMValue < Cali5VOCVValue[s] + VoltCaliAcc))
                                {
                                    byte DL = 0; byte DM = 0; byte DH = 0; byte DL2 = 0; byte DM2 = 0; byte DH2 = 0;
                                    System.Threading.Thread.Sleep(100);
                                    if (!com.MixWriteOCV_DAC(out DL, out DM, out DH, out DL2, out DM2, out DH2))
                                        return false;
                                    if (!com.MixWriteOCV_DACCaliValue(Convert.ToByte(OCV5Vol_ADC_Add_L[s]), DL, DM, DH))
                                        return false;
                                    if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                                    {
                                        if (!com.MixWriteOCV_DACCaliValue(Convert.ToByte(PortVol_ADC_Add_L[s]), DL2, DM2, DH2))
                                            return false;
                                    }
                                    break;
                                }
                                else if (OCVVolCaliDMMValue < Cali5VOCVValue[s])
                                {
                                    double VolOffset = Cali5VOCVValue[s] - OCVVolCaliDMMValue;
                                    CellVolValue = CellVolValue + VolOffset;
                                }
                                else if (OCVVolCaliDMMValue > Cali5VOCVValue[s])
                                {
                                    double VolOffset = OCVVolCaliDMMValue - Cali5VOCVValue[s];
                                    CellVolValue = CellVolValue - VolOffset;
                                }
                            }
                            else
                            {
                                if (!com.MixSendCellVolt(Convert.ToByte(VolDL), Convert.ToByte(VolDH)))
                                    return false;
                                System.Threading.Thread.Sleep(200);
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out OCVVolCaliDMMValue))
                                    return false;
                                OCVVolCaliDMMValue = OCVVolCaliDMMValue * 1000;
                                if ((OCVVolCaliDMMValue > Cali5VOCVValue[s] - Deviation) && (OCVVolCaliDMMValue < Cali5VOCVValue[s] + Deviation))
                                {
                                    byte DL = 0; byte DM = 0; byte DH = 0; byte DL2 = 0; byte DM2 = 0; byte DH2 = 0;
                                    System.Threading.Thread.Sleep(100);
                                    if (!com.MixWriteOCV_DAC(out DL, out DM, out DH, out DL2, out DM2, out DH2))
                                        return false;
                                    if (!com.MixWriteOCV_DACCaliValue(Convert.ToByte(OCV5Vol_ADC_Add_L[s]), DL, DM, DH))
                                        return false;
                                    if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                                    {
                                        if (!com.MixWriteOCV_DACCaliValue(Convert.ToByte(PortVol_ADC_Add_L[s]), DL2, DM2, DH2))
                                            return false;
                                    }
                                    break;
                                }
                                #region
                                else if (OCVVolCaliDMMValue > (Cali5VOCVValue[s]))//如果万用表的值过大，则要减小偏移
                                {
                                    //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                    step = Convert.ToInt16((OCVVolCaliDMMValue - Cali5VOCVValue[s]) / 0.076);
                                    step_H = step / 256;
                                    step_L = step % 256;

                                    VolDL = VolDL - step_L;

                                    if (VolDL < 0)
                                    {
                                        VolDL = VolDL + 256;
                                        VolDH = VolDH - step_H - 1;
                                        if (VolDH < 0)
                                            VolDH = 00;
                                    }
                                    else
                                    {
                                        VolDH = VolDH - step_H;
                                    }
                                    continue;//结束当前的循环，进入接下来的循环，判断循环的条件是否满足
                                }
                                else if (OCVVolCaliDMMValue < (Cali5VOCVValue[s]))//如果万用表的值过小，则要增大偏移
                                {
                                    //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                    step = Convert.ToInt16((Cali5VOCVValue[s] - OCVVolCaliDMMValue) / 0.076);
                                    step_H = step / 256;
                                    step_L = step % 256;

                                    VolDL = VolDL + step_L;

                                    if (VolDL > 255)
                                    {
                                        VolDL = VolDL - 256;
                                        VolDH = VolDH + step_H + 1;
                                    }
                                    else
                                    {
                                        VolDH = VolDH + step_H;
                                    }
                                    if (VolDH > 255)
                                        VolDH = 255;
                                    continue;
                                    //}

                                    #endregion
                                }
                            }
                        }
                        Err1 = OCVVolCaliDMMValue - Convert.ToDouble(Cali5VOCVValue[s]);
                        //Err2 = ReadOCV - Convert.ToDouble(Cali5VOCVValue[s]);
                        if (Math.Abs(Err1) < VoltCaliAcc)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, CheckType, Cali5VOCVValue[s].ToString(), OCVVolCaliDMMValue.ToString("f2"), "校准以DMM值为准", Err1.ToString("f2"), "", VoltCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == Cali5VOCVValue.Count - 1)
                        {
                            if (!com.MixUpdateOCV())
                                return false;
                            if (DeviceType != "BAT525C" && DeviceType != "BAT525D")
                            {
                                com.MixReadOcvADC((Convert.ToByte(OCV5Vol_ADC_Add_L[s])), out ADCDL, out ADCDH);
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
                    bool[] ChkTestNG = new bool[Cali5VOCVValue.Count];
                    if (!com.PortVolCal_RY(false))
                        return false;
                    for (int s = 0; s < Cali5VOCVValue.Count; s++)
                    {
                        if (!StopStatus)
                        {
                            Point = Cali5VOCVValue[s].ToString() + "mV开路电压点检NG";
                            //if (Cali5VOCVValue[s] == 5000)
                            //{
                            //    if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                            //        VoltAcc = 2;
                            //}
                            if (!com.MixSetCellVoltValue(Cali5VOCVValue[s]))
                                return false;
                            if (Cali5VOCVValue[s] == 0)
                                System.Threading.Thread.Sleep(3000);
                            System.Threading.Thread.Sleep(100);
                            if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out OCVVolCheckDMMValue))
                                return false;
                            if (Math.Abs((OCVVolCheckDMMValue * 1000) - Cali5VOCVValue[s]) >= VoltCaliAcc || Cali5VOCVValue[s] == 0)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out OCVVolCheckDMMValue))
                                        return false;
                                    if ((Math.Abs(OCVVolCheckDMMValue * 1000) - Cali5VOCVValue[s]) < VoltCaliAcc)
                                        break;
                                    else continue;
                                }
                            }
                            OCVVolCheckDMMValue = OCVVolCheckDMMValue * 1000;
                            System.Threading.Thread.Sleep(100);
                            if (!com.MixReadOCVValue(out ReadOCV))
                                return false;
                            switch (VoltAccResRegulation)
                            {
                                case "设置值、万用表、设备读值":
                                    Err1 = OCVVolCheckDMMValue - Cali5VOCVValue[s];
                                    Err2 = ReadOCV - OCVVolCheckDMMValue;
                                    if (Cali5VOCVValue[s] == 0|| Cali5VOCVValue[s] == 5000)
                                    {
                                        if (Math.Abs(Err1) < 2 && Math.Abs(Err2) < 2)
                                        { Result = "√"; ChkTestNG[s] = false; }
                                        else
                                        { Result = "×"; ChkTestNG[s] = true; }
                                    }
                                    else
                                    {
                                        if (Math.Abs(Err1) < VoltCaliAcc && Math.Abs(Err2) < VoltCaliAcc)
                                        { Result = "√"; ChkTestNG[s] = false; }
                                        else
                                        { Result = "×"; ChkTestNG[s] = true; }
                                    }
                                    break;
                                case "设置值、万用表":
                                    Err1 = OCVVolCheckDMMValue - Cali5VOCVValue[s];
                                    if (Cali5VOCVValue[s] == 0 || Cali5VOCVValue[s] == 5000)
                                    {
                                        if (Math.Abs(Err1) < 2)
                                        { Result = "√"; ChkTestNG[s] = false; }
                                        else
                                        { Result = "×"; ChkTestNG[s] = true; }
                                    }
                                    else
                                    {
                                        if (Math.Abs(Err1) < VoltCaliAcc)
                                        { Result = "√"; ChkTestNG[s] = false; }
                                        else
                                        { Result = "×"; ChkTestNG[s] = true; }
                                    }
                                    break;
                                case "设置值、设备读值":
                                    Err1 = ReadOCV - Cali5VOCVValue[s];
                                    if (Cali5VOCVValue[s] == 0 || Cali5VOCVValue[s] == 5000)
                                    {
                                        if (Math.Abs(Err1) < 2)
                                        { Result = "√"; ChkTestNG[s] = false; }
                                        else
                                        { Result = "×"; ChkTestNG[s] = true; }
                                    }
                                    else
                                    {
                                        if (Math.Abs(Err1) < VoltCaliAcc)
                                        { Result = "√"; ChkTestNG[s] = false; }
                                        else
                                        { Result = "×"; ChkTestNG[s] = true; }
                                    }
                                    break;
                                case "万用表、设备读值":
                                    Err1 = OCVVolCheckDMMValue - ReadOCV;
                                    if (Cali5VOCVValue[s] == 0 || Cali5VOCVValue[s] == 5000)
                                    {
                                        if (Math.Abs(Err1) < 2)
                                        { Result = "√"; ChkTestNG[s] = false; }
                                        else
                                        { Result = "×"; ChkTestNG[s] = true; }
                                    }
                                    else
                                    {
                                        if (Math.Abs(Err1) < VoltCaliAcc)
                                        { Result = "√"; ChkTestNG[s] = false; }
                                        else
                                        { Result = "×"; ChkTestNG[s] = true; }
                                    }
                                    break;
                                default:
                                    break;
                            }
                            UpdateUidelegate(DeviceType, Box, "OCV电压点检", Cali5VOCVValue[s].ToString(), OCVVolCheckDMMValue.ToString("f2"), ReadOCV.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), VoltCaliAcc.ToString(), Result);
                            if (s == Cali5VOCVValue.Count - 1)
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
                com.MixSendCellCur(0x00, 0x00);
                com.MixSendCellVolt(0x00, 0x00);
                com.MixCellEnable(0x00);
                com.MixOCVEnable(0x00);
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
