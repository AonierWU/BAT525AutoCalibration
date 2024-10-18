using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool LoadPartVoltCali(string Box, double VoltCaliAcc, double VoltCheckAcc, string VoltAccResRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<int> Vol_DAC_Set_H = new List<int> { };
            List<int> Vol_DAC_Set_L = new List<int> { };
            int[] Vol_DAC_Add_L = new int[] { };//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址
            int[] Vol_ADC_Add_L2 = new int[] { };//存放ADC读取值的低地址

            //int[] CaliValue = { 0, 100, 500, 1000, 2000, 3000, 4000, 5000 };//电压校正点
            //int[] Vol_DAC_Set_H = new int[8] { 0x00, 0x03, 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94 };//DAC设置的高地址
            //int[] Vol_DAC_Set_L = new int[8] { 0x00, 0x1F, 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            //int[] Vol_DAC_Add_L = new int[8];//存放DAC设置值的低地址
            //int[] Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址
            //for (int j = 0; j < 8; j++)
            //{
            //    Vol_DAC_Add_L[j] = 0X05 + j * 2;//循环设定DAC的设定值的地址
            //    Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
            //}
            //double LoadPartVolCaliTesterValue = 0;
            double LoadPartVolCaliDMMValue = 0;
            double LoadPartVolCheckDMMValue = 0;
            double LoadPartVolCheckTesterValue = 0;
            int step = 0;
            int step_H = 0;
            int step_L = 0;
            Point = "未设初始值";
            double Resolution = 0.076;//电压零点以外的点的分辨率
            //double Deviation = 0.05;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03

            byte VoltDL, VoltDM, VoltDH;
            byte refDACDH = 0;
            byte refADCDH = 0;
            byte DACDL, DACDH, ADCDL, ADCDH;
            double Err1 = 0;
            double Err2 = 0;
            string Result = "√";
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, 100, 500, 1000, 2000, 3000, 4000, 5000 };
                    CaliValue.AddRange(s);
                    int[] H = new int[] { 0x04, 0x03, 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94 };
                    Vol_DAC_Set_H.AddRange(H);
                    int[] L = new int[] { 0x00, 0x1F, 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79 };
                    Vol_DAC_Set_L.AddRange(L);

                    refDACDH = 248;
                    refADCDH = 245;

                    Vol_DAC_Add_L = new int[8];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址
                    for (int j = 0; j < 8; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X05 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 0, 100, 500, 1000, 2000, 3000, 4000, 5000 };
                    CaliValue.AddRange(h);
                    int[] H3 = new int[] { 0x04, 0x03, 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94 };
                    Vol_DAC_Set_H.AddRange(H3);
                    int[] L3 = new int[] { 0x00, 0x1F, 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79 };
                    Vol_DAC_Set_L.AddRange(L3);

                    refDACDH = 248;
                    refADCDH = 245;

                    Vol_DAC_Add_L = new int[8];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址
                    for (int j = 0; j < 8; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X05 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525C":
                    int[] c = new int[] { 0, 20, 100, 1000, 2000, 2500, 3000, 3600, 3800, 4000, 4200, 4500, 5000 };
                    CaliValue.AddRange(c);
                    int[] H1 = new int[] { 0x00, 0x00, 0x04, 0x31, 0x62, 0x7B, 0x93, 0xB1, 0xBB, 0xC4, 0xCE, 0xDD, 0xF6 };
                    Vol_DAC_Set_H.AddRange(H1);
                    int[] L1 = new int[] { 0x93, 0xF9, 0xE7, 0x3B, 0x89, 0x3D, 0xDF, 0x49, 0x1A, 0xFB, 0xC6, 0x95, 0x51 };
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
                    int[] H2 = new int[] { 0x00, 0x00, 0x04, 0x31, 0x62, 0x7B, 0x93, 0xB1, 0xBB, 0xC4, 0xCE, 0xDD, 0xF6 };
                    Vol_DAC_Set_H.AddRange(H2);
                    int[] L2 = new int[] { 0x93, 0xF9, 0xE7, 0x3B, 0x89, 0x3D, 0xDF, 0x49, 0x1A, 0xFB, 0xC6, 0x95, 0x51 };
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
                if (!com.MixLoadVoltCalibration())
                    return false;
                System.Threading.Thread.Sleep(100);
                if (!com.OCVorLVVolCal_RY(true))
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
                                if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                                {
                                    for (int a = 0; a < 127; a++)
                                    {
                                        if (!com.MixSendZeroLoadPartVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                            return false;
                                        System.Threading.Thread.Sleep(500);
                                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartVolCaliDMMValue))
                                            return false;
                                        LoadPartVolCaliDMMValue = LoadPartVolCaliDMMValue * 1000;
                                        if (-0.1 <= LoadPartVolCaliDMMValue && LoadPartVolCaliDMMValue <= 0.1)
                                        {
                                            break;
                                        }
                                        else if (LoadPartVolCaliDMMValue > 0)
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
                                        if (!com.MixSendLoadPartVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                            return false;
                                        System.Threading.Thread.Sleep(2000);
                                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartVolCaliDMMValue))
                                            return false;
                                        LoadPartVolCaliDMMValue = LoadPartVolCaliDMMValue * 1000;
                                        if ((-0.05 <= LoadPartVolCaliDMMValue) && (LoadPartVolCaliDMMValue <= 0.05))
                                        {
                                            break;
                                        }
                                        else if (LoadPartVolCaliDMMValue - CaliValue[s] > 0)
                                        {
                                            step = Convert.ToInt16((LoadPartVolCaliDMMValue - CaliValue[s]) / Resolution);
                                            dblZero = dblZero - step;
                                            DH = dblZero / 256;
                                            DL = dblZero % 256;
                                        }
                                        else
                                        {
                                            step = Convert.ToInt16((Math.Abs(LoadPartVolCaliDMMValue) - CaliValue[s]) / Resolution);
                                            dblZero = dblZero + step;
                                            DH = dblZero / 256;
                                            DL = dblZero % 256;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!com.MixSendLoadPartVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                    return false;
                                System.Threading.Thread.Sleep(200);
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartVolCaliDMMValue))
                                    return false;
                                LoadPartVolCaliDMMValue = LoadPartVolCaliDMMValue * 1000;
                            }
                            //判断测试
                            if ((LoadPartVolCaliDMMValue > CaliValue[s] - VoltCaliAcc) && (LoadPartVolCaliDMMValue < CaliValue[s] + VoltCaliAcc))
                            {
                                if (!com.MixWriteLoadPart_DAC(Convert.ToByte(Vol_DAC_Add_L[s]), Convert.ToByte(DL), Convert.ToByte(DH)))
                                    break;
                                System.Threading.Thread.Sleep(100);
                                if (!com.MixReadLoadPartVolt_ADCValue(out VoltDL, out VoltDM, out VoltDH))
                                    break;
                                System.Threading.Thread.Sleep(100);
                                if (!com.MixWriteLoadPartVol_ADC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDM, VoltDH))
                                    break;
                                if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                                {
                                    if (!com.MixWriteLoadPartVol_ADC(Convert.ToByte(Vol_ADC_Add_L2[s]), VoltDL, VoltDM, VoltDH))
                                        break;
                                }
                                System.Threading.Thread.Sleep(100);
                                //System.Threading.Thread.Sleep(100);
                                //if (!com.MixReadLoadPatVoltValue(out LoadPartVolCaliTesterValue))
                                //    return false;

                                break;//满足结束循环
                            }
                            #region
                            else if (LoadPartVolCaliDMMValue > (CaliValue[s]))//如果万用表的值过大，则要减小偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((LoadPartVolCaliDMMValue - CaliValue[s]) / Resolution);
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
                            else if (LoadPartVolCaliDMMValue < (CaliValue[s]))//如果万用表的值过小，则要增大偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((CaliValue[s] - LoadPartVolCaliDMMValue) / Resolution);
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
                        Err1 = LoadPartVolCaliDMMValue - Convert.ToDouble(CaliValue[s]);
                        // Err2 = LoadPartVolCaliTesterValue - Convert.ToDouble(CaliValue[s]);
                        if (Math.Abs(Err1) < VoltCaliAcc)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), LoadPartVolCaliDMMValue.ToString("f2"), "校准以DMM值为准", Err1.ToString("f2"), "", VoltCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == CaliValue.Count - 1)
                        {
                            if (!com.MixUpdateLoadPart())
                                break;
                            if (DeviceType != "BAT525C" && DeviceType != "BAT525D")
                            {
                                com.MixReadOcvDAC((Convert.ToByte(Vol_DAC_Add_L[s])), out DACDL, out DACDH);
                                com.MixReadOcvADC((Convert.ToByte(Vol_ADC_Add_L[s])), out ADCDL, out ADCDH);
                                if (!((-10 <= (DACDH - refDACDH) && (DACDH - refDACDH) <= 10) && (-10 <= (ADCDH - refADCDH) && (ADCDH - refADCDH) <= 10)))
                                {
                                    Point = "设定DAC高值:" + DACDH.ToString("X2") + ";" + "读取ADC高值:" + ADCDH.ToString("X2") + ";存在异常，请检查!";
                                    return false;

                                }
                            }

                        }
                        else continue;
                    }

                }
                else
                    return false;

                #region 点检所有的点
                if (!StopStatus)
                {
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {
                        Point = CaliValue[s].ToString() + "mV点检NG";
                        if (!com.MixSetLoadPartVoltValue(CaliValue[s]))
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartVolCheckDMMValue))
                            return false;
                        if (Math.Abs((LoadPartVolCheckDMMValue * 1000) - CaliValue[s]) >= VoltCheckAcc || CaliValue[s] == 0)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartVolCheckDMMValue))
                                    return false;
                                if ((Math.Abs(LoadPartVolCheckDMMValue * 1000) - CaliValue[s]) < VoltCheckAcc)
                                    break;
                                else
                                {
                                    System.Threading.Thread.Sleep(300);
                                    continue;
                                }
                            }
                        }
                        LoadPartVolCheckDMMValue = LoadPartVolCheckDMMValue * 1000;
                        if (!com.MixReadLoadPatVoltValue(out LoadPartVolCheckTesterValue))
                            return false;
                        switch (VoltAccResRegulation)
                        {
                            case "设置值、万用表、设备读值":
                                Err1 = LoadPartVolCheckDMMValue - CaliValue[s];
                                Err2 = LoadPartVolCheckTesterValue - LoadPartVolCheckDMMValue;
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 1 && Math.Abs(Err2) < 1)
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
                                Err1 = LoadPartVolCheckDMMValue - CaliValue[s];
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 1)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }else
                                {
                                    if (Math.Abs(Err1) < VoltCheckAcc)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                break;
                            case "设置值、设备读值":
                                Err1 = LoadPartVolCheckTesterValue - CaliValue[s];
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 1)
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
                                Err1 = LoadPartVolCheckDMMValue - LoadPartVolCheckTesterValue;
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 1)
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
                        UpdateUidelegate(DeviceType, Box, "LV电压点检", CaliValue[s].ToString(), LoadPartVolCheckDMMValue.ToString("f2"), LoadPartVolCheckTesterValue.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), VoltCheckAcc.ToString(), Result);
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
                //ks.ConfDevice("VOLT", "AUTO");
                com.MixLoadPartEnable(0x00);
                com.MixSendLoadPartVolt(0x00, 0x00);
                com.MixSendLoadPartCur(0x00, 0x00);
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
