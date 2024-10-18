using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool LoadPartCurCali(string Box, double CurCaliAcc, double CurCheckAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            List<int> Vol_DAC_Set_H = new List<int> { };
            List<int> Vol_DAC_Set_L = new List<int> { };
            int[] Vol_DAC_Add_L = new int[] { };//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址

            //int[] CaliValue = { 0, 100, 500, 1000, 1250 };//电流校正点
            //int[] Vol_DAC_Set_H = new int[5] { 0x00, 0x16, 0x73, 0xBF, 0xEE };//DAC设置的高地址
            //int[] Vol_DAC_Set_L = new int[5] { 0x00, 0xAF, 0x2B, 0xBF, 0x31 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            //int[] Vol_DAC_Add_L = new int[5];//存放DAC设置值的低地址
            //int[] Vol_ADC_Add_L = new int[5];//存放ADC读取值的低地址   

            //double LoadPartCurCaliTesterValue = 0;
            double LoadPartCurCaliDMMValue = 0;
            Point = "未设初始值";
            int step = 0;
            int step_H = 0;
            int step_L = 0;
            double Resolution = 0.019;//电压零点以外的点的分辨率
            //double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            byte VoltDL, VoltDH;
            byte refDACDH = 0;
            byte refADCDH = 0;
            byte DACDL, DACDH, ADCDL, ADCDH;
            double Err1 = 0;
            double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            double LoadPartCurCheckDMMValue = 0;
            double LoadPartCurCaliTesterValue1 = 0;
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, 100, 500, 1000, 1250 };
                    CaliValue.AddRange(s);
                    int[] H = new int[] { 0x00, 0x16, 0x73, 0xBF, 0xEE };
                    Vol_DAC_Set_H.AddRange(H);
                    int[] L = new int[] { 0x00, 0xAF, 0x2B, 0xBF, 0x31 };
                    Vol_DAC_Set_L.AddRange(L);

                    refDACDH = 241;
                    refADCDH = 238;

                    Vol_DAC_Add_L = new int[5];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[5];//存放ADC读取值的低地址
                    for (int j = 0; j < 5; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X1F + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X3E + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 0, 100, 500, 1000, 1250 };
                    CaliValue.AddRange(h);
                    int[] H3 = new int[] { 0x00, 0x16, 0x73, 0xBF, 0xEE };
                    Vol_DAC_Set_H.AddRange(H3);
                    int[] L3 = new int[] { 0x00, 0xAF, 0x2B, 0xBF, 0x31 };
                    Vol_DAC_Set_L.AddRange(L3);

                    refDACDH = 241;
                    refADCDH = 238;

                    Vol_DAC_Add_L = new int[5];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[5];//存放ADC读取值的低地址
                    for (int j = 0; j < 5; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X1F + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X3E + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525C":
                    int[] c = new int[] { 0, 5, 20, 50, 100, 200, 400, 600, 800, 1000, 1200 };
                    CaliValue.AddRange(c);
                    int[] H1 = new int[] { 0x00, 0x00, 0x03, 0x09, 0x13, 0x27, 0x4E, 0x76, 0x9D, 0xC4, 0xEC };
                    Vol_DAC_Set_H.AddRange(H1);
                    int[] L1 = new int[] { 0x8B, 0xFE, 0xF2, 0xD8, 0xB9, 0x4E, 0xAF, 0x18, 0x7C, 0xBF, 0x13 };
                    Vol_DAC_Set_L.AddRange(L1);

                    Vol_DAC_Add_L = new int[11];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[11];//存放ADC读取值的低地址
                    for (int j = 0; j < 11; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X6C + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X82 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 0, 5, 20, 50, 100, 200, 400, 600, 800, 1000, 1200 };
                    CaliValue.AddRange(d);
                    int[] H2 = new int[] { 0x00, 0x00, 0x03, 0x09, 0x13, 0x27, 0x4E, 0x76, 0x9D, 0xC4, 0xEC };
                    Vol_DAC_Set_H.AddRange(H2);
                    int[] L2 = new int[] { 0x8B, 0xFE, 0xF2, 0xD8, 0xB9, 0x4E, 0xAF, 0x18, 0x7C, 0xBF, 0x13 };
                    Vol_DAC_Set_L.AddRange(L2);

                    Vol_DAC_Add_L = new int[11];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[11];//存放ADC读取值的低地址
                    for (int j = 0; j < 11; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X6C + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X82 + j * 2;//循环设定ADC的读取值的地址
                    }
                    break;
            }
            try
            {
                System.Threading.Thread.Sleep(100);
                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "3"))
                    return false;
                if (!com.MultimeterCur10A(false))
                    return false;
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    if (!com.MixChgVolCalibration())
                        return false;
                    if (!com.MixLoadCurCalibration())
                        return false;
                    if (!com.LVDsgCurCal_RY(true))
                        return false;
                    if (!com.MixSendChgVolt(0x1C, 0x96))
                        return false;
                    if (!com.MixEnable(0x00))
                        return false;
                    if (!com.MixSendLoadPartVolt(0x00, 0xBF))
                        return false;
                    if (!com.MixLoadPartEnable(0x00))
                        return false;
                }
                else
                {
                    if (!com.MixLoadCurCalibration())
                        return false;
                    if (!com.LVChgCurCal_RY(true))
                        return false;
                }
                if (!StopStatus)
                {
                    System.Threading.Thread.Sleep(200);

                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        Point = CaliValue[s].ToString() + "mA校准NG";
                        int DL = Vol_DAC_Set_L[s];
                        int DH = Vol_DAC_Set_H[s];
                        //if (DeviceType != "BAT525G"&& DeviceType != "BAT525H")
                        //{
                        if (CaliValue[s] >= 200)
                        {
                            if (frmVerifyDevice.strMultimeterType != "34401A")
                            {
                                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "10"))
                                    return false;
                                if (!com.MultimeterCur10A(true))
                                    return false;
                            }
                        }
                        //}
                        for (int i = 0; i < 30; i++)
                        {
                            if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                            {
                                if (CaliValue[s] == 0)
                                {
                                    for (int j = 0; j < 127; j++)
                                    {
                                        if (!com.MixSendZeroLoadPartCur(Convert.ToByte(DL), Convert.ToByte(DH)))
                                            return false;
                                        System.Threading.Thread.Sleep(200);
                                        if (frmVerifyDevice.strMultimeterType == "34401A")
                                        {
                                            if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                                break;
                                        }
                                        else
                                        {
                                            if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                                break;
                                        }
                                        LoadPartCurCaliDMMValue = Math.Round((LoadPartCurCaliDMMValue * 1000), 2);
                                        if (-0.1 <= LoadPartCurCaliDMMValue && LoadPartCurCaliDMMValue <= 0.1)
                                        {
                                            break;
                                        }
                                        else if (LoadPartCurCaliDMMValue > 0)
                                        {
                                            DL++;
                                        }
                                        else
                                        {
                                            DL--;
                                        }
                                        if (j == 126)
                                        {
                                            return false;
                                        }
                                    }
                                }
                                else
                                {
                                    //设定预设置
                                    if (!com.MixSendLoadPartCur(Convert.ToByte(DL), Convert.ToByte(DH)))
                                        return false;
                                    //DMM读取值
                                    System.Threading.Thread.Sleep(200);
                                    if (frmVerifyDevice.strMultimeterType == "34401A")
                                    {
                                        if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                            break;
                                        if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                            break;
                                    }
                                    else
                                    {
                                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                            break;
                                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                            break;
                                    }
                                    LoadPartCurCaliDMMValue = LoadPartCurCaliDMMValue * 1000;
                                }
                            }
                            else
                            {
                                //设定预设置
                                if (!com.MixSendLoadPartCur(Convert.ToByte(DL), Convert.ToByte(DH)))
                                    return false;
                                //DMM读取值
                                if (!com.MixEnable(0x01))
                                    return false;
                                if (!com.MixLoadPartEnable(0x01))
                                    return false;
                                System.Threading.Thread.Sleep(200);
                                if (frmVerifyDevice.strMultimeterType == "34401A")
                                {
                                    if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                        break;
                                    if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                        break;
                                }
                                else
                                {
                                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                        break;
                                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                                        break;
                                }
                                LoadPartCurCaliDMMValue = LoadPartCurCaliDMMValue * 1000;
                            }
                            //判断测试
                            if ((LoadPartCurCaliDMMValue > CaliValue[s] - CurCaliAcc) && (LoadPartCurCaliDMMValue < CaliValue[s] + CurCaliAcc))
                            {

                                if (!com.MixWriteLoadPart_DAC(Convert.ToByte(Vol_DAC_Add_L[s]), Convert.ToByte(DL), Convert.ToByte(DH)))
                                    break;
                                System.Threading.Thread.Sleep(100);
                                if (!com.MixReadLoadPartCur_ADCValue(out VoltDL, out VoltDH))
                                    break;
                                System.Threading.Thread.Sleep(100);
                                if (!com.MixWriteLoadPart_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                    break;
                                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                                {
                                    System.Threading.Thread.Sleep(100);
                                    if (!com.MixEnable(0x00))
                                        return false;
                                    if (!com.MixLoadPartEnable(0x00))
                                        return false;
                                }
                                //System.Threading.Thread.Sleep(100);
                                //if (!com.MixReadLoadPartCurValue(out LoadPartCurCaliTesterValue))
                                //    return false;
                                break;//满足结束循环
                            }

                            else if (LoadPartCurCaliDMMValue > (CaliValue[s]))//如果万用表的值过大，则要减小偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((LoadPartCurCaliDMMValue - CaliValue[s]) / Resolution);
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
                            else if (LoadPartCurCaliDMMValue < (CaliValue[s]))//如果万用表的值过小，则要增大偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((CaliValue[s] - LoadPartCurCaliDMMValue) / Resolution);
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
                        Err1 = LoadPartCurCaliDMMValue - Convert.ToDouble(CaliValue[s]);
                        //Err2 = LoadPartCurCaliTesterValue - Convert.ToDouble(CaliValue[s]);
                        if (Math.Abs(Err1) < CurCaliAcc)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), LoadPartCurCaliDMMValue.ToString("f2"), "校准以DMM值为准", Err1.ToString("f2"), "", CurCaliAcc.ToString(), Result);
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
                else { return false; }
                #region 点检所有的点

                if (!StopStatus)
                {
                    //if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "3"))
                    //    return false;
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                    {
                        if (!com.MixEnable(0X01))
                            return false;
                        if (!com.MixLoadPartEnable(0X01))
                            return false;
                    }
                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {
                        Point = CaliValue[s].ToString() + "mA点检NG";
                        if (CaliValue[s] < 200)
                        {
                            if (frmVerifyDevice.strMultimeterType != "34401A")
                            {
                                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "3"))
                                    return false;
                                if (!com.MultimeterCur10A(false))
                                    return false;
                            }
                        }
                        if (!com.MixSetLoadPartCurValue(CaliValue[s]))
                            return false;
                        System.Threading.Thread.Sleep(500);
                        if (frmVerifyDevice.strMultimeterType == "34401A")
                        {
                            if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCheckDMMValue))
                                break;
                            if (Math.Abs((LoadPartCurCheckDMMValue * 1000) - CaliValue[s]) >= CurCheckAcc || CaliValue[s] == 0)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCheckDMMValue))
                                        break;
                                    if ((Math.Abs(LoadPartCurCheckDMMValue * 1000) - CaliValue[s]) < CurCheckAcc)
                                        break;
                                    else continue;
                                }
                            }
                        }
                        else
                        {
                            if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCheckDMMValue))
                                break;
                            if (Math.Abs((LoadPartCurCheckDMMValue * 1000) - CaliValue[s]) >= CurCheckAcc || CaliValue[s] == 0)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCheckDMMValue))
                                        break;
                                    if ((Math.Abs(LoadPartCurCheckDMMValue * 1000) - CaliValue[s]) <CurCheckAcc)
                                        break;
                                    else continue;
                                }
                            }
                        }
                        LoadPartCurCheckDMMValue = LoadPartCurCheckDMMValue * 1000;
                        if (!com.MixReadLoadPartCurValue(out LoadPartCurCaliTesterValue1))
                            return false;
                        switch (CurAccResRegulation)
                        {
                            case "设置值、万用表、设备读值":
                                Err1 = LoadPartCurCheckDMMValue - CaliValue[s];
                                Err2 = LoadPartCurCaliTesterValue1 - LoadPartCurCheckDMMValue;
                                if (Math.Abs(Err1) < CurCheckAcc && Math.Abs(Err2) < CurCheckAcc)
                                { Result = "√"; ChkTestNG[s] = false; }
                                else
                                { Result = "×"; ChkTestNG[s] = true; }
                                break;
                            case "设置值、万用表":
                                Err1 = LoadPartCurCheckDMMValue - CaliValue[s];
                                if (Math.Abs(Err1) < CurCheckAcc)
                                { Result = "√"; ChkTestNG[s] = false; }
                                else
                                { Result = "×"; ChkTestNG[s] = true; }
                                break;
                            case "设置值、设备读值":
                                Err1 = LoadPartCurCaliTesterValue1 - CaliValue[s];
                                if (Math.Abs(Err1) < CurCheckAcc)
                                { Result = "√"; ChkTestNG[s] = false; }
                                else
                                { Result = "×"; ChkTestNG[s] = true; }
                                break;
                            case "万用表、设备读值":
                                Err1 = LoadPartCurCheckDMMValue - LoadPartCurCaliTesterValue1;
                                if (Math.Abs(Err1) < CurCheckAcc)
                                { Result = "√"; ChkTestNG[s] = false; }
                                else
                                { Result = "×"; ChkTestNG[s] = true; }
                                break;
                            default:
                                break;
                        }
                        UpdateUidelegate(DeviceType, Box, "LV电流点检", CaliValue[s].ToString(), LoadPartCurCheckDMMValue.ToString("f2"), LoadPartCurCaliTesterValue1.ToString(), Err1.ToString("f2"), Err2.ToString("f2"), CurCheckAcc.ToString(), Result);
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
                com.MixLoadPartEnable(0x00);
                com.MixSendLoadPartVolt(0x00, 0x00);
                com.MixSendLoadPartCur(0x00, 0x00);
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                    com.MixSendChgVolt(0x00, 0x00);
                    com.MixEnable(0x00);
                }
                //com.MultimeterCur10A(false);
                com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO");
                //if (frmVerifyDevice.strCalEQMType == "BAT525G")
                //{
                //    //com.LVDsgCurCal_RY(false);
                //    DCsource.SetVolandOutPut(frmVerifyDevice.strDCsourceConType, 0, 0, frmVerifyDevice.strDCch, false);
                //}
                com.Reset_RY();
            }
        }
    }
}
