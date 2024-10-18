using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool CellVoltCali(string Box, double VoltCaliAcc, double VoltCheckAcc, string VoltAccJudgeRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<int> Vol_DAC_Set_H = new List<int> { };
            List<int> Vol_DAC_Set_L = new List<int> { };
            int[] Vol_DAC_Add_L = new int[] { };//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址

            //double CellVolCaliTesterValue = 0;
            double CellVolCaliDMMValue = 0;
            double CellVolCheckDMMValue = 0;
            double CellVolCheckTesterValue = 0;
            int step = 0;
            int step_H = 0;
            int step_L = 0;
            Point = "未设初始值";
            double Resolution = 0.076;//电压零点以外的点的分辨率
            double Deviation = 0.05;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            byte VoltDL, VoltDM, VoltDH;
            byte refDACDH = 0;
            byte refADCDH = 0;
            byte DACDL, DACDH, ADCDL, ADCDM, ADCDH;
            double Err1 = 0;
            double Err2 = 0;
            string Result = "√";
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, 20, 100, 500, 1000, 2000, 3000, 4000, 5000 };
                    CaliValue.AddRange(s);
                    int[] H = new int[] { 0x03, 0x03, 0x07, 0x1A, 0x33, 0x64, 0x96, 0xC7, 0xF8 };
                    Vol_DAC_Set_H.AddRange(H);
                    int[] L = new int[] { 0x00, 0x1F, 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79, 0xE8 };
                    Vol_DAC_Set_L.AddRange(L);

                    refDACDH = 248;
                    refADCDH = 245;

                    Vol_DAC_Add_L = new int[9];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[9];//存放ADC读取值的低地址
                    for (int j = 0; j < 9; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X05 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 0, 20, 100, 500, 1000, 2000, 3000, 4000, 5000 };
                    CaliValue.AddRange(h);
                    int[] H3 = new int[] { 0x03, 0x03, 0x07, 0x1A, 0x33, 0x64, 0x96, 0xC7, 0xF8 };
                    Vol_DAC_Set_H.AddRange(H3);
                    int[] L3 = new int[] { 0x00, 0x1F, 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79, 0xE8 };
                    Vol_DAC_Set_L.AddRange(L3);

                    refDACDH = 248;
                    refADCDH = 245;

                    Vol_DAC_Add_L = new int[9];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[9];//存放ADC读取值的低地址
                    for (int j = 0; j < 9; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X05 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525C":
                    int[] c = new int[] { 0, 20, 100, 1000, 2000, 2500, 3000, 3600, 3800, 4000, 4200,4350, 4500 };
                    CaliValue.AddRange(c);
                    int[] H1 = new int[] { 0x00, 0x00, 0x04, 0x31, 0x62, 0x7B, 0x93, 0xB1, 0xBB, 0xC4, 0xCE,  0xDD, 0xF2 };
                    Vol_DAC_Set_H.AddRange(H1);
                    int[] L1 = new int[] { 0x9F, 0xFE, 0xF0, 0x43, 0x71, 0x13, 0xAC, 0x3B, 0x09, 0xDA, 0xB5,  0x8B, 0xD1 };
                    Vol_DAC_Set_L.AddRange(L1);


                    Vol_DAC_Add_L = new int[13];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[13];//存放ADC读取值的低地址
                    for (int j = 0; j < 13; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X04 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X1E + j * 3;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 0, 20, 100, 1000, 2000, 2500, 3000, 3600, 3800, 4000, 4200, 4350, 4500 };
                    CaliValue.AddRange(d);
                    int[] H2 = new int[] { 0x00, 0x00, 0x04, 0x31, 0x62, 0x7B, 0x93, 0xB1, 0xBB, 0xC4, 0xCE, 0xDD, 0xF2 };
                    Vol_DAC_Set_H.AddRange(H2);
                    int[] L2 = new int[] { 0x9F, 0xFE, 0xF0, 0x43, 0x71, 0x13, 0xAC, 0x3B, 0x09, 0xDA, 0xB5, 0x8B, 0xD1 };
                    Vol_DAC_Set_L.AddRange(L2);


                    Vol_DAC_Add_L = new int[13];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[13];//存放ADC读取值的低地址
                    for (int j = 0; j < 13; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X04 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X1E + j * 3;//循环设定ADC的读取值的地址
                    }
                    break;
            }
            try
            {
                if (!com.MixCellVoltCalibration())
                    return false;
                if (!com.CellVolCal_RY(true))
                    return false;
                System.Threading.Thread.Sleep(100);
                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                    return false;
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
                                        if (!com.MixSendZeroCellVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                            return false;
                                        System.Threading.Thread.Sleep(500);
                                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellVolCaliDMMValue))
                                            return false;
                                        CellVolCaliDMMValue = CellVolCaliDMMValue * 1000;
                                        if (-0.1 <= CellVolCaliDMMValue && CellVolCaliDMMValue <= 0.1)
                                        {
                                            break;
                                        }
                                        else if (CellVolCaliDMMValue > 0)
                                        {
                                            DL++;
                                        }
                                    }
                                }
                                else
                                {
                                    int dblZero = DH * 256 + DL;
                                    for (int a = 0; a < 127; a++)
                                    {
                                        if (!com.MixSendCellVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                            return false;
                                        System.Threading.Thread.Sleep(2000);
                                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellVolCaliDMMValue))
                                            return false;
                                        CellVolCaliDMMValue = CellVolCaliDMMValue * 1000;
                                        if ((-0.05 <= CellVolCaliDMMValue) && (CellVolCaliDMMValue <= 0.05))
                                        {
                                            break;
                                        }
                                        else if (CellVolCaliDMMValue - CaliValue[s] > 0)
                                        {
                                            step = Convert.ToInt16((CellVolCaliDMMValue - CaliValue[s]) / Resolution);
                                            dblZero = dblZero - step;
                                            DH = dblZero / 256;
                                            DL = dblZero % 256;
                                        }
                                        else
                                        {
                                            step = Convert.ToInt16((Math.Abs(CellVolCaliDMMValue) - CaliValue[s]) / Resolution);
                                            dblZero = dblZero + step;
                                            DH = dblZero / 256;
                                            DL = dblZero % 256;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!com.MixSendCellVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                    return false;
                                System.Threading.Thread.Sleep(200);
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellVolCaliDMMValue))
                                    return false;
                                CellVolCaliDMMValue = CellVolCaliDMMValue * 1000;
                            }
                            //判断测试
                            if ((CellVolCaliDMMValue > CaliValue[s] - Deviation) && (CellVolCaliDMMValue < CaliValue[s] + Deviation))
                            {
                                if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_DAC_Add_L[s]), Convert.ToByte(DL), Convert.ToByte(DH)))
                                    break;
                                System.Threading.Thread.Sleep(50);
                                if (!com.MixReadCellVolt_ADCValue(out VoltDL, out VoltDM, out VoltDH))
                                    break;
                                if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                                {
                                    if (!com.MixWriteCellVolt_ADC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDM, VoltDH))
                                        break;
                                }
                                else
                                {
                                    if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                        break;
                                }
                                System.Threading.Thread.Sleep(50);
                                break;//满足结束循环
                            }
                            #region
                            else if (CellVolCaliDMMValue > (CaliValue[s]))//如果万用表的值过大，则要减小偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((CellVolCaliDMMValue - CaliValue[s]) / Resolution);
                                step_H = step / 256;
                                step_L = step % 256;

                                DL = DL - step_L;

                                if (DL < 0)
                                {
                                    DL = DL + 256;
                                    DH = DH - step_H - 1;
                                    if (DH < 0)
                                        DH = 00;
                                }
                                else
                                {
                                    DH = DH - step_H;
                                }
                                continue;//结束当前的循环，进入接下来的循环，判断循环的条件是否满足
                            }
                            else if (CellVolCaliDMMValue < (CaliValue[s]))//如果万用表的值过小，则要增大偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((CaliValue[s] - CellVolCaliDMMValue) / Resolution);
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
                                //}

                                #endregion
                            }
                        }
                        Err1 = CellVolCaliDMMValue - Convert.ToDouble(CaliValue[s]);
                        // Err2= CellVolCaliTesterValue - Convert.ToDouble(CaliValue[s]);
                        if (Math.Abs(Err1) < VoltCaliAcc)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), CellVolCaliDMMValue.ToString("f2"), "校准以DMM值为准", Err1.ToString("f2"), "", VoltCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == CaliValue.Count - 1)
                        {
                            if (!com.MixUpdateCellVolt())
                                break;
                            if (DeviceType != "BAT525C"&& DeviceType != "BAT525D")
                            {
                                com.MixReadCellDAC((Convert.ToByte(Vol_DAC_Add_L[s])), out DACDL, out DACDH);
                                com.MixReadCellADC((Convert.ToByte(Vol_ADC_Add_L[s])), out ADCDL, out ADCDM, out ADCDH);
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

                if (!StopStatus)
                {
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    #region 点检所有的点
                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {
                        //VoltAcc = CaliValue[s] * 0.0002 + 0.1;//万二RD+千一FS
                        Point = CaliValue[s].ToString() + "mV点检NG";
                        if (!com.MixSetCellVoltValue(CaliValue[s]))
                            return false;
                        //if (CaliValue[s] == 0)
                        //    System.Threading.Thread.Sleep(3200);
                        System.Threading.Thread.Sleep(100);
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellVolCheckDMMValue))
                            return false;
                        if (Math.Abs((CellVolCheckDMMValue * 1000) - CaliValue[s]) > VoltCheckAcc || CaliValue[s] == 0)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellVolCheckDMMValue))
                                    return false;
                                if ((Math.Abs(CellVolCheckDMMValue * 1000) - CaliValue[s]) < VoltCheckAcc)
                                    break;
                                else continue;
                            }
                        }
                        CellVolCheckDMMValue = CellVolCheckDMMValue * 1000;
                        if (!com.MixReadCellVoltValue(out CellVolCheckTesterValue))
                            return false;
                        switch (VoltAccJudgeRegulation)
                        {
                            case "设置值、万用表、设备读值":
                                Err1 = CellVolCheckDMMValue - CaliValue[s];
                                Err2 = CellVolCheckTesterValue - CellVolCheckDMMValue;
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2 && Math.Abs(Err2) < 2)
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
                                Err1 = CellVolCheckDMMValue - CaliValue[s];
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2)
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
                                Err1 = CellVolCheckTesterValue - CaliValue[s];
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2)
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
                                Err1 = CellVolCheckDMMValue - CellVolCheckTesterValue;
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2)
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
                        UpdateUidelegate(DeviceType, Box, "Cell电压点检", CaliValue[s].ToString(), CellVolCheckDMMValue.ToString("f2"), CellVolCheckTesterValue.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), VoltCheckAcc.ToString(), Result);
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
                com.MixSendCellVolt(0x00, 0x00);
                com.MixSendCellCur(0x00, 0x00);
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
