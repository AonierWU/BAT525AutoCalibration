using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool ProVoltCali(string Box, double VoltCaliAcc, double VoltCheckAcc, string VoltAccRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<int> Vol_DAC_Set_H = new List<int> { };
            List<int> Vol_DAC_Set_L = new List<int> { };
            //int[] CaliValue = { 200, 500, 1000, 3000, 5000, 7000, 9000, 10000 };//电压校正点
            //int[] Vol_DAC_Set_H = new int[8] { 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94, 0xAC, 0xC5 };//DAC设置的高地址
            //int[] Vol_DAC_Set_L = new int[8] { 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79, 0xE8, 0x58 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] Vol_DAC_Add_L = new int[] { };//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址
            //for (int j = 0; j < 8; j++)
            //{
            //    Vol_DAC_Add_L[j] = 0X05 + j * 2;//循环设定DAC的设定值的地址
            //    Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
            //}
            //double ProVolCaliTesterValue = 0;
            double ProVolCaliDMMValue = 0;
            double ProVolCheckDMMValue = 0;
            double ProVolCheckTesterValue = 0;
            int step = 0;
            int step_H = 0;
            int step_L = 0;

            Point = "未设初始值";
            double Resolution = 0.153;//电压零点以外的点的分辨率
           // double Deviation = 0.5;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03

            byte VoltDL, VoltDH, VoltDM;
            byte refDACDH = 0;
            byte refADCDH = 0;
            byte DACDL, DACDH, ADCDL, ADCDH;
            double Err1 = 0;
            double Err2 = 0;
            string Result = "√";
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 200, 500, 1000, 3000, 5000, 7000, 9000, 10000 };
                    CaliValue.AddRange(s);
                    int[] H = new int[] { 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94, 0xAC, 0xC5 };
                    Vol_DAC_Set_H.AddRange(H);
                    int[] L = new int[] { 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79, 0xE8, 0x58 };
                    Vol_DAC_Set_L.AddRange(L);

                    refDACDH = 249;
                    refADCDH = 15;

                    Vol_DAC_Add_L = new int[8];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址
                    for (int j = 0; j < 8; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X05 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 200, 500, 1000, 3000, 5000, 7000, 9000, 10000 };
                    CaliValue.AddRange(h);
                    int[] H3 = new int[] { 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94, 0xAC, 0xC5 };
                    Vol_DAC_Set_H.AddRange(H3);
                    int[] L3 = new int[] { 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79, 0xE8, 0x58 };
                    Vol_DAC_Set_L.AddRange(L3);

                    refDACDH = 249;
                    refADCDH = 15;

                    Vol_DAC_Add_L = new int[8];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址
                    for (int j = 0; j < 8; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X05 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525C":
                    int[] c = new int[] { 500, 1000, 2000, 3000, 5000, 7000, 9000, 12000, 12000, 14000, 16000, 18000, 20000, 22000, 24000 };
                    CaliValue.AddRange(c);
                    int[] H1 = new int[] { 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x02, 0x03, 0x01, 0x02, 0x02, 0x02, 0x03, 0x03, 0x03 };
                    Vol_DAC_Set_H.AddRange(H1);
                    int[] L1 = new int[] { 0x26, 0x4A, 0x94, 0xDE, 0x74, 0x07, 0x9D, 0x7C, 0xE9, 0x3C, 0x8B, 0xE3, 0x2D, 0x80, 0xDC };
                    Vol_DAC_Set_L.AddRange(L1);
                    Vol_DAC_Add_L = new int[15];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[15];//存放ADC读取值的低地址
                    for (int j = 0; j < 15; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X04 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 500, 1000, 2000, 3000, 5000, 7000, 9000, 12000, 12000, 14000, 16000, 18000, 20000, 22000, 24000 };
                    CaliValue.AddRange(d);
                    int[] H2 = new int[] { 0x00, 0x00, 0x00, 0x00, 0x01, 0x02, 0x02, 0x03, 0x01, 0x02, 0x02, 0x02, 0x03, 0x03, 0x03 };
                    Vol_DAC_Set_H.AddRange(H2);
                    int[] L2 = new int[] { 0x26, 0x4A, 0x94, 0xDE, 0x74, 0x07, 0x9D, 0x7C, 0xE9, 0x3C, 0x8B, 0xE3, 0x2D, 0x80, 0xDC };
                    Vol_DAC_Set_L.AddRange(L2);
                    Vol_DAC_Add_L = new int[15];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[15];//存放ADC读取值的低地址
                    for (int j = 0; j < 15; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X04 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;

            }
            try
            {
                if (!com.MixProVoltCalibration())
                    return false;
                System.Threading.Thread.Sleep(100);
                if (!com.PrgVolCal_RY(true))
                    return false;
                System.Threading.Thread.Sleep(100);

                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                    return false;
                if (true)
                {
                    if (DeviceType == "BAT525C"|| DeviceType == "BAT525D")
                    {
                        Resolution = 13.26;
                        if (!com.MixProVoltSetRang(0x00))
                            return false;
                      
                    }
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        if (!StopStatus)
                        {
                            Point = CaliValue[s].ToString() + "mV校准NG";
                            int DL = Vol_DAC_Set_L[s];
                            int DH = Vol_DAC_Set_H[s];
                            if (s==8)
                            {
                                Resolution = 24.21;
                               
                                if (DeviceType=="BAT525C"|| DeviceType == "BAT525D")
                                {
                                    if (!com.MixSendProVolt(0x00, 0x00))
                                        return false;
                                    if (!com.MixProVoltSetRang(0x01))
                                        return false;
                                }
                            }
                            for (int i = 0; i < 30; i++)
                            {
                                if (!com.MixSendProVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                                    return false;
                                System.Threading.Thread.Sleep(200);
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ProVolCaliDMMValue))
                                    return false;
                                ProVolCaliDMMValue = ProVolCaliDMMValue * 1000;

                                //判断测试
                                if ((ProVolCaliDMMValue > CaliValue[s] - VoltCaliAcc) && (ProVolCaliDMMValue < CaliValue[s] + VoltCaliAcc))
                                {
                                    if (!com.MixWriteProVolt_DAC(Convert.ToByte(Vol_DAC_Add_L[s]), Convert.ToByte(DL), Convert.ToByte(DH)))
                                        break;
                                    System.Threading.Thread.Sleep(100);
                                    if (!com.MixReadProADCCaliValue(out VoltDL,out VoltDM, out VoltDH))
                                        break;
                                    System.Threading.Thread.Sleep(100);
                                    if (DeviceType == "BAT525C"|| DeviceType == "BAT525D")
                                    {
                                        if (!com.MixWritePro_ADCValue(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDM))
                                            break;
                                    }
                                    else
                                    {
                                        if (!com.MixWritePro_ADCValue(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                            break;
                                    }
                                    //System.Threading.Thread.Sleep(100);

                                    //System.Threading.Thread.Sleep(200);
                                    //if (!com.MixReadProVoltValue(out ProVolCaliTesterValue))
                                    //    return false;
                                    //if (!com.MixReadProVoltValue(out ProVolCaliTesterValue))
                                    //    return false;
                                    break;//满足结束循环
                                }
                                #region
                                else if (ProVolCaliDMMValue > (CaliValue[s]))//如果万用表的值过大，则要减小偏移
                                {
                                    //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                    step = Convert.ToInt16((ProVolCaliDMMValue - CaliValue[s]) / Resolution);
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
                                else if (ProVolCaliDMMValue < (CaliValue[s]))//如果万用表的值过小，则要增大偏移
                                {
                                    //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                    step = Convert.ToInt16((CaliValue[s] - ProVolCaliDMMValue) / Resolution);
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
                            Err1 = ProVolCaliDMMValue - Convert.ToDouble(CaliValue[s]);
                            //Err2 = ProVolCaliTesterValue - Convert.ToDouble(CaliValue[s]);
                            
                            if (Math.Abs(Err1) < VoltCaliAcc)
                                Result = "√";
                            else
                                Result = "×";
                            UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), ProVolCaliDMMValue.ToString("f2"), "校准以DMM值为准", Err1.ToString("f2"), "", VoltCaliAcc.ToString(), Result);
                            if (Result == "×")
                                return false;
                            if (s == CaliValue.Count - 1)
                            {
                                if (!com.MixUpdateProVolt())
                                    return false;
                                if (DeviceType != "BAT525C"&& DeviceType != "BAT525D")
                                {
                                    com.MixReadPrgDAC((Convert.ToByte(Vol_DAC_Add_L[s])), out DACDL, out DACDH);
                                    com.MixReadPrgADC((Convert.ToByte(Vol_ADC_Add_L[s])), out ADCDL, out ADCDH);
                                    if (!((-10 <= (DACDH - refDACDH) && (DACDH - refDACDH) <= 10) && (-10 <= (ADCDH - refADCDH) && (ADCDH - refADCDH) <= 10)))
                                    {
                                        Point = "设定DAC高值:" + DACDH.ToString("X2") + ";" + "读取ADC高值:" + ADCDH.ToString("X2") + ";存在异常，请检查!";
                                        return false;
                                    }
                                }

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
                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {

                        Point = CaliValue[s].ToString() + "mV点检NG";
                        if(s==7)
                        {
                            if (DeviceType == "BAT525C"|| DeviceType == "BAT525D")
                                com.MixProVoltSetRang(0X00);
                        }
                        if (!com.MixSetProVoltValue(CaliValue[s]))
                            return false;
                        //System.Threading.Thread.Sleep(3000);
                        System.Threading.Thread.Sleep(100);
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ProVolCheckDMMValue))
                            return false;
                        if (Math.Abs((ProVolCheckDMMValue * 1000) - CaliValue[s]) >= VoltCheckAcc)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ProVolCheckDMMValue))
                                    return false;
                                if ((Math.Abs(ProVolCheckDMMValue * 1000) - CaliValue[s]) < VoltCheckAcc)
                                    break;
                                else { System.Threading.Thread.Sleep(300); 
                                    continue; }
                            }
                        }
                        ProVolCheckDMMValue = ProVolCheckDMMValue * 1000;
                        if (!com.MixReadProVoltValue(out ProVolCheckTesterValue))
                            return false;
                        System.Threading.Thread.Sleep(200);
                        if (!com.MixReadProVoltValue(out ProVolCheckTesterValue))
                            return false;
                        switch (VoltAccRegulation)
                        {
                            case "设置值、万用表、设备读值":
                                Err1 = ProVolCheckDMMValue - CaliValue[s];
                                Err2 = ProVolCheckTesterValue - ProVolCheckDMMValue;
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
                                Err1 = ProVolCheckDMMValue - CaliValue[s];
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
                                Err1 = ProVolCheckTesterValue - CaliValue[s];
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
                                Err1 = ProVolCheckDMMValue - ProVolCheckTesterValue;
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
                        UpdateUidelegate(DeviceType, Box, "PV电压点检", CaliValue[s].ToString(), ProVolCheckDMMValue.ToString("f2"), ProVolCheckTesterValue.ToString("f2"), Err1.ToString("f2"), Err2.ToString("f2"), VoltCheckAcc.ToString(), Result);
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
                com.MixSendProVolt(0x00, 0x00);
                com.MixProVoltEnable(0x00);
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
