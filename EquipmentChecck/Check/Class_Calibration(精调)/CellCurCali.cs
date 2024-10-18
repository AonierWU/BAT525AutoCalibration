using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool CellCurCali(string Box, double CurCaliAcc, double CurCheckAcc, string CurAccJudgeRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            //电流校正点
            List<int> CaliValue = new List<int> { };
            List<int> Vol_DAC_Set_H = new List<int> { };
            List<int> Vol_DAC_Set_L = new List<int> { };
            //int[] CaliValue = new int[] { };
            //int[] Vol_DAC_Set_H = new int[] { };//DAC设置的高地址
            //int[] Vol_DAC_Set_L = new int[] { };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] Vol_DAC_Add_L = new int[] { };//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址   
            double CellCurCaliDMMValue = 0;
            Point = "未设初始值";
            int step = 0;
            int step_H = 0;
            int step_L = 0;
            double Resolution = 0.019;//电压零点以外的点的分辨率
            double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            byte VoltDL, VoltDM, VoltDH;
            byte refDACDH = 0;
            byte refADCDH = 0;
            byte DACDL, DACDH, ADCDL, ADCDM, ADCDH;
            double Err1 = 0;
            double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            double CellCurCheckDMMValue = 0;
            double CellCurCaliTesterValue1 = 0;
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, 100, 500, 1000, 1250 };
                    CaliValue.AddRange(s);
                    int[] H = new int[] { 0x00, 0x15, 0x78, 0xBF, 0xEE };
                    Vol_DAC_Set_H.AddRange(H);
                    int[] L = new int[] { 0x00, 0xAF, 0x2B, 0x32, 0x31 };
                    Vol_DAC_Set_L.AddRange(L);
                    Resolution = 0.020;
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
                    int[] H1 = new int[] { 0x00, 0x15, 0x78, 0xBF, 0xEE };
                    Vol_DAC_Set_H.AddRange(H1);
                    int[] L1 = new int[] { 0x00, 0xAF, 0x2B, 0x32, 0x31 };
                    Vol_DAC_Set_L.AddRange(L1);
                    Resolution = 0.020;
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
                    int[] H2 = new int[] { 0x00, 0x00, 0x03, 0x09, 0x13, 0x27, 0x4F, 0x76, 0x9E, 0xC5, 0XED };
                    Vol_DAC_Set_H.AddRange(H2);
                    int[] L2 = new int[] { 0x77, 0xFA, 0xF3, 0xE5, 0xCB, 0x19, 0x3C, 0xB5, 0x50, 0xF1, 0X8D };
                    Vol_DAC_Set_L.AddRange(L2);


                    Vol_DAC_Add_L = new int[11];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[11];//存放ADC读取值的低地址
                    for (int j = 0; j < 11; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X45 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X5B + j * 3;//循环设定ADC的读取值的地址
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 0, 5, 20, 50, 100, 200, 400, 600, 800, 1000, 1500, 2000, 2500 };
                    CaliValue.AddRange(d);
                    int[] H3 = new int[] { 0x00, 0x00, 0x03, 0x09, 0x13, 0x27, 0x4F, 0x76, 0x9E, 0xC5, 0XED, 0XED, 0XED };
                    Vol_DAC_Set_H.AddRange(H3);
                    int[] L3 = new int[] { 0x77, 0xFA, 0xF3, 0xE5, 0xCB, 0x19, 0x3C, 0xB5, 0x50, 0xF1, 0X8D, 0X8D, 0X8D };
                    Vol_DAC_Set_L.AddRange(L3);
                    Resolution = 0.04;

                    Vol_DAC_Add_L = new int[13];//存放DAC设置值的低地址
                    Vol_ADC_Add_L = new int[13];//存放ADC读取值的低地址
                    for (int j = 0; j < 13; j++)
                    {
                        Vol_DAC_Add_L[j] = 0X45 + j * 2;//循环设定DAC的设定值的地址
                        Vol_ADC_Add_L[j] = 0X5F + j * 3;//循环设定ADC的读取值的地址
                    }
                    break;
            }
            try
            {
                if (!com.MixCellCurCalibration())
                    return false;
                if (!com.CellCurCal_RY(true))
                    return false;
                System.Threading.Thread.Sleep(100);
                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "3"))
                    return false;
                if (!StopStatus)
                {
                    System.Threading.Thread.Sleep(200);
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        if (CaliValue[s] > 0)
                        {
                            Deviation = 0.05;//0以上的误差
                        }
                        Point = CaliValue[s].ToString() + "mA校准NG";
                        int DL = Vol_DAC_Set_L[s];
                        int DH = Vol_DAC_Set_H[s];
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
                        for (int i = 0; i < 30; i++)
                        {
                            if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                            {
                                if (CaliValue[s] == 0)
                                {
                                    //if (!com.MixEnable(0x01))
                                    //    return false;
                                    for (int j = 0; j < 127; j++)
                                    {
                                        if (!com.MixSendZeroCellCur(Convert.ToByte(DL), Convert.ToByte(DH)))
                                            return false;

                                        System.Threading.Thread.Sleep(200);
                                        if (frmVerifyDevice.strMultimeterType == "34401A")
                                        {
                                            if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CellCurCaliDMMValue))
                                                break;
                                            if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CellCurCaliDMMValue))
                                                break;
                                        }
                                        else
                                        {
                                            if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellCurCaliDMMValue))
                                                break;
                                        }
                                        CellCurCaliDMMValue = Math.Round((CellCurCaliDMMValue * 1000), 3);
                                        if (CellCurCaliDMMValue <= 0.1)
                                        {
                                            break;
                                        }
                                        else if (CellCurCaliDMMValue > 0)
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
                                    if (!com.MixSendCellCur(Convert.ToByte(DL), Convert.ToByte(DH)))
                                        return false;
                                    //DMM读取值
                                    System.Threading.Thread.Sleep(200);
                                    if (frmVerifyDevice.strMultimeterType == "34401A")
                                    {
                                        if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CellCurCaliDMMValue))
                                            break;
                                        if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CellCurCaliDMMValue))
                                            break;
                                    }
                                    else
                                    {
                                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellCurCaliDMMValue))
                                            break;
                                    }
                                    CellCurCaliDMMValue = CellCurCaliDMMValue * 1000;
                                }
                            }
                            else
                            {
                                //设定预设置
                                if (!com.MixSendCellCur(Convert.ToByte(DL), Convert.ToByte(DH)))
                                    return false;
                                //DMM读取值
                                System.Threading.Thread.Sleep(200);
                                if (frmVerifyDevice.strMultimeterType == "34401A")
                                {
                                    if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CellCurCaliDMMValue))
                                        break;
                                    if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CellCurCaliDMMValue))
                                        break;
                                }
                                else
                                {
                                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellCurCaliDMMValue))
                                        break;
                                }
                                CellCurCaliDMMValue = CellCurCaliDMMValue * 1000;
                            }
                            //判断测试
                            if ((CellCurCaliDMMValue > CaliValue[s] - Deviation) && (CellCurCaliDMMValue < CaliValue[s] + Deviation))
                            {
                                if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_DAC_Add_L[s]), Convert.ToByte(DL), Convert.ToByte(DH)))
                                    break;
                                System.Threading.Thread.Sleep(50);
                                if (!com.MixReadCellCur_ADCValue(out VoltDL, out VoltDM, out VoltDH))
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
                                //System.Threading.Thread.Sleep(50);

                                //System.Threading.Thread.Sleep(50);
                                //if (!com.MixReadCellCurValue(out CellCurCaliTesterValue))
                                //    return false;
                                break;//满足结束循环
                            }
                            else if (CellCurCaliDMMValue > (CaliValue[s]))//如果万用表的值过大，则要减小偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((CellCurCaliDMMValue - CaliValue[s]) / Resolution);
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
                            else if (CellCurCaliDMMValue < (CaliValue[s]))//如果万用表的值过小，则要增大偏移
                            {
                                //如果不在规定的范围内，则先计算校正的时候偏移的次数
                                step = Convert.ToInt16((CaliValue[s] - CellCurCaliDMMValue) / Resolution);
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
                        Err1 = CellCurCaliDMMValue - Convert.ToDouble(CaliValue[s]);
                        if (Math.Abs(Err1) < CurCaliAcc)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), CellCurCaliDMMValue.ToString("f2"), "校准以DMM值为准", Err1.ToString("f2"), "", CurCaliAcc.ToString(), Result);
                        if (Result == "×")
                            return false;
                        if (s == CaliValue.Count - 1)
                        {
                            if (!com.MixUpdateCellVolt())
                                break;
                            if (DeviceType != "BAT525C" && DeviceType != "BAT525D")
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

                #region 点检所有的点
                if (!StopStatus)
                {
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    for (int s = CaliValue.Count - 1; s >= 0; s--)
                    {
                        //CurAcc = CaliValue[s] * 0.0005 + 0.12;//点检允许的精度误差，万2RD+千1FS
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
                        if (!com.MixSetCellCurValue(CaliValue[s]))
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (frmVerifyDevice.strMultimeterType == "34401A")
                        {
                            if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CellCurCheckDMMValue))
                                break;
                            if (Math.Abs((CellCurCheckDMMValue * 1000) - CaliValue[s]) >  CurCheckAcc|| CaliValue[s] == 0)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CellCurCheckDMMValue))
                                        return false;
                                    if ((Math.Abs(CellCurCheckDMMValue * 1000) - CaliValue[s]) < CurCheckAcc)
                                        break;
                                    else continue;
                                }
                            }
                        }
                        else
                        {
                            if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellCurCheckDMMValue))
                                break;
                            if (Math.Abs((CellCurCheckDMMValue * 1000) - CaliValue[s]) > CurCheckAcc || CaliValue[s] == 0)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellCurCheckDMMValue))
                                        break;
                                    if ((Math.Abs(CellCurCheckDMMValue * 1000) - CaliValue[s]) < CurCheckAcc)
                                        break;
                                    else continue;
                                }
                            }
                        }
                        CellCurCheckDMMValue = CellCurCheckDMMValue * 1000;
                        if (!com.MixReadCellCurValue(out CellCurCaliTesterValue1))
                            return false;
                        if (!com.MixReadCellCurValue(out CellCurCaliTesterValue1))
                            return false;
                        switch (CurAccJudgeRegulation)
                        {
                            case "设置值、万用表、设备读值":
                                Err1 = CellCurCheckDMMValue - CaliValue[s];
                                Err2 = CellCurCaliTesterValue1 - CellCurCheckDMMValue;
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2 && Math.Abs(Err2) < 2)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else
                                {
                                    if (Math.Abs(Err1) < CurCheckAcc && Math.Abs(Err2) < CurCheckAcc)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                break;
                            case "设置值、万用表":
                                Err1 = CellCurCheckDMMValue - CaliValue[s];
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else
                                {
                                    if (Math.Abs(Err1) < CurCheckAcc)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                break;
                            case "设置值、设备读值":
                                Err1 = CellCurCaliTesterValue1 - CaliValue[s];
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else
                                {
                                    if (Math.Abs(Err1) < CurCheckAcc)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                break;
                            case "万用表、设备读值":
                                Err1 = CellCurCheckDMMValue - CellCurCaliTesterValue1;
                                if (CaliValue[s] == 0)
                                {
                                    if (Math.Abs(Err1) < 2)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                else
                                {
                                    if (Math.Abs(Err1) < CurCheckAcc)
                                    { Result = "√"; ChkTestNG[s] = false; }
                                    else
                                    { Result = "×"; ChkTestNG[s] = true; }
                                }
                                break;
                            default:
                                break;
                        }
                        UpdateUidelegate(DeviceType, Box, "串电流点检_mA", CaliValue[s].ToString(), CellCurCheckDMMValue.ToString("f2"), CellCurCaliTesterValue1.ToString(), Err1.ToString("f2"), Err2.ToString("f2"), CurCheckAcc.ToString(), Result);
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
                com.MixSendCellCur(0x00, 0x00);
                com.MixSendCellVolt(0x00, 0x00);
                //com.MixStCurEnable();
                com.Reset_RY();
                com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO");
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
