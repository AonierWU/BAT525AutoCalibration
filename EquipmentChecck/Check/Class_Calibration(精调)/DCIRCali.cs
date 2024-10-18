using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool DCIRCali(string Box, double VoltCaliAcc, double VoltCheckAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool CaliType, out string Point)
        {
            List<int> CaliValue = new List<int> { };
            //int[] CaliValue = { 0, 10, 30, 50 };//静态电流20000nA校正点
            int[] Vol_ADC_Add_L = new int[] { };//存放ADC读取值的低地址   
            //double CellCurCaliTesterValue = 0;
            Point = "未设初始值";
            byte refADCDH = 0;
            byte ADCDL, ADCDM, ADCDH;
            byte VoltDL, VoltDH;
            double Err1 = 0;
            string Result = "√";
            double CellCurCheckDMMValue = 0;
            double CellCurCaliTesterValue1 = 0;
            double CurValue;
            // byte Rang = 0x00;
            byte ReadADCRang = 0x00;
            byte ReadValueRang = 0x00;
            switch (DeviceType)
            {
                case "BAT525G":
                    int[] s = new int[] { 0, 10, 30, 50 };
                    CaliValue.AddRange(s);
                    refADCDH = 124;
                    Vol_ADC_Add_L = new int[4];//存放ADC读取值的低地址
                    for (int j = 0; j < 4; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x9D + j * 2;// addr
                    }
                    break;
                case "BAT525H":
                    int[] h = new int[] { 1, 10, 30, 50 };
                    CaliValue.AddRange(h);
                    refADCDH = 124;
                    Vol_ADC_Add_L = new int[4];//存放ADC读取值的低地址
                    for (int j = 0; j < 4; j++)
                    {
                        Vol_ADC_Add_L[j] = 0x9D + j * 2;// addr
                    }
                    break;
                case "BAT525C":
                    int[] c = new int[] { 0, 10, 30, 50, 100, 100, 150, 200, 300 };
                    CaliValue.AddRange(c);
                    Vol_ADC_Add_L = new int[9];//存放ADC读取值的低地址
                    for (int j = 0; j < 9; j++)
                    {
                        Vol_ADC_Add_L[j] = 0xDC + j * 2;// addr
                    }
                    break;
                case "BAT525D":
                    int[] d = new int[] { 0, 10, 30, 50, 100, 100, 150, 200, 300 };
                    CaliValue.AddRange(d);
                    Vol_ADC_Add_L = new int[9];//存放ADC读取值的低地址
                    for (int j = 0; j < 9; j++)
                    {
                        Vol_ADC_Add_L[j] = 0xDC + j * 2;// addr
                    }
                    break;
            }
            try
            {
                //先判断设备电流
                if (frmVerifyDevice.strMultimeterType == "34401A")
                {
                    if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "3"))
                        return false;
                    if (!com.MultimeterCur10A(false))
                        return false;
                }
                else
                {
                    if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "10"))
                        return false;
                    if (!com.MultimeterCur10A(true))
                        return false;
                }
                if (!com.DsgCur3ACal_RY(true))
                    return false;
                if (!com.MixDCIRCalibration())
                    return false;
                if (frmVerifyDevice.strMultimeterType == "34401A")
                {
                    if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CellCurCheckDMMValue))
                        return false;
                }
                else
                {
                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellCurCheckDMMValue))
                        return false;

                }
                CellCurCaliTesterValue1 = Math.Abs(CellCurCheckDMMValue * 1000);
                if (Math.Abs(CellCurCaliTesterValue1 - 1000) <= 0.5)
                {
                    if (!com.MixEnable(0X00))//关闭电流使能
                        return false;
                    if (!com.DsgCur3ACal_RY(false))
                        return false;
                    if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                        return false;
                    if (!StopStatus)
                    {
                        System.Threading.Thread.Sleep(200);
                        if (DeviceType == "BAT525H")
                        {
                            //判断是0mΩ还是1mΩ
                            if (!com.ChecckDcirRange())
                            {
                                CaliValue.Clear();
                                int[] hm = new int[] { 0, 10, 30, 50 };
                                CaliValue.AddRange(hm);
                            }

                        }
                        for (int s = 0; s < CaliValue.Count; s++)
                        {
                            if (DeviceType == "BAT525G")
                            {
                                //if (s == 0)
                                //    s = 1;
                            }
                            //else if (DeviceType == "BAT525H")
                            //{
                            //    //判断是0mΩ还是1mΩ
                            //    if (!com.ChecckDcirRange())
                            //    {
                            //        //CaliValue[s] = 0;
                            //        int[] hm = new int[] { 1, 10, 30, 50 };
                            //        CaliValue.AddRange(hm);
                            //    }

                            //}
                            CurValue = 1000;

                            Point = CaliValue[s].ToString() + "mR校准NG";
                            if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                            {
                                if (s <= 4)
                                {
                                    ReadADCRang = 0X0C;
                                }
                                else
                                {
                                    ReadADCRang = 0X0D;
                                }
                            }
                            //设定预设置
                            if (!com.DCIRCal_RY(CaliValue[s], true))
                                return false;
                            if (!com.RY3Enable_RY(true))
                                return false;
                            if (!com.MixEnable(0x01))//打开电流使能
                                return false;
                            for (int i = 0; i < 30; i++)
                            {
                                if (!com.MixSetCurValue(0x00, CurValue))
                                    return false;
                                System.Threading.Thread.Sleep(100);
                                if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellCurCheckDMMValue))
                                    return false;
                                CellCurCheckDMMValue = Math.Abs(CellCurCheckDMMValue * 1000);
                                if (CaliValue[s] == 0 || CaliValue[s] == 1)
                                {
                                    CellCurCheckDMMValue = Math.Round(CellCurCheckDMMValue, 2);
                                    if (CaliValue[s] - 0.1 <= CellCurCheckDMMValue && CellCurCheckDMMValue <= CaliValue[s] + 0.1)
                                    {
                                        if (!com.RY3Enable_RY(false))//电压OK后需要断开万用表
                                            return false;
                                        System.Threading.Thread.Sleep(100);
                                        if (!com.MixReadDCIR_ADCValue(ReadADCRang, out VoltDL, out VoltDH))
                                            break;
                                        if (!com.MixWriteDCIR_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                            break;
                                        //System.Threading.Thread.Sleep(50);
                                        //if (!com.MixUpdateDCIR())
                                        //    break;
                                        break;
                                    }
                                }
                                else
                                {
                                    if ((CellCurCheckDMMValue > CaliValue[s] - 0.01) && (CellCurCheckDMMValue < CaliValue[s] + 0.01))
                                    {
                                        //if (CaliValue[s] == 0)
                                        //{
                                        //    System.Threading.Thread.Sleep(8000);//0点校准要9秒ADC值才稳定
                                        //}
                                        //System.Threading.Thread.Sleep(100);
                                        //判断测试
                                        if (!com.RY3Enable_RY(false))//电压OK后需要断开万用表
                                            return false;
                                        System.Threading.Thread.Sleep(100);
                                        if (!com.MixReadDCIR_ADCValue(ReadADCRang, out VoltDL, out VoltDH))
                                            break;
                                        if (!com.MixWriteDCIR_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                                            break;
                                        //System.Threading.Thread.Sleep(50);

                                        //System.Threading.Thread.Sleep(50);
                                        //if (!com.MixReadDCIRValue(out CellCurCaliTesterValue))
                                        //    return false;
                                        break;//退出当前循环
                                    }
                                }
                                if (CellCurCheckDMMValue < CaliValue[s])//实际值偏小，增加电流
                                {
                                    double CurOffSet;
                                    double VolOffset = CaliValue[s] - CellCurCheckDMMValue;//计算电压偏移量
                                    if (CaliValue[s] == 0)
                                    {
                                        CurOffSet = (VolOffset / VolOffset) * 1000;
                                    }
                                    else
                                        CurOffSet = (VolOffset / CaliValue[s]) * 1000;
                                    CurValue = CurValue + CurOffSet;
                                }
                                else if (CellCurCheckDMMValue > CaliValue[s])//实际值偏大，减小电流
                                {
                                    double CurOffSet;
                                    double VolOffset = CellCurCheckDMMValue - CaliValue[s];//计算电压偏移量
                                    if (CaliValue[s] == 0)
                                    {
                                        CurOffSet = VolOffset * 1000;
                                    }
                                    else
                                        CurOffSet = (VolOffset / CaliValue[s]) * 1000;
                                    CurValue = CurValue - CurOffSet;
                                    if (CurValue <= 0)
                                    {
                                        Point = "电流已调节至零，无法校准。判断范围<=0.1mΩ";
                                        return false;
                                    }
                                }
                            }
                            Err1 = CellCurCheckDMMValue - Convert.ToDouble(CaliValue[s]);
                            if (CaliValue[s] == 0 || CaliValue[s] == 1)
                            {
                                if (Math.Abs(Err1) < 0.2)
                                    Result = "√";
                                else
                                    Result = "×";
                            }
                            else
                            {
                                if (Math.Abs(Err1) < VoltCaliAcc)
                                    Result = "√";
                                else
                                    Result = "×";
                            }
                            UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), CellCurCheckDMMValue.ToString("f2"), "校准以DMM值为准", Err1.ToString("f2"), "", VoltCaliAcc.ToString(), Result);
                            if (!com.DCIRCal_RY(CaliValue[s], false))
                                return false;
                            if (Result == "×")
                                return false;
                            if (s == CaliValue.Count - 1)
                            {
                                if (!com.MixUpdateDCIR())
                                    break;
                                if (DeviceType != "BAT525C" && DeviceType != "BAT525D")
                                {
                                    com.MixReadChgVolADC((Convert.ToByte(Vol_ADC_Add_L[s])), out ADCDL, out ADCDM, out ADCDH);
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
                    else
                        return false;
                }
                else
                {
                    Point = "1A电流误差大于0.5mA!";
                    return false;
                }
                #region 点检所有的点
                if (!StopStatus)
                {
                    bool[] ChkTestNG = new bool[CaliValue.Count];
                    if (!com.MixDCIRCalibration())
                        return false;
                    for (int s = 0; s < CaliValue.Count; s++)
                    {
                        //CurRange = 0x00;
                        if (s == 0)
                        {
                            if (DeviceType == "BAT525G")
                            {
                                //s = 1;
                            }
                        }
                        Point = CaliValue[s].ToString() + "mR点检NG";
                        if (DeviceType == "BAT525C" || DeviceType == "BAT525D")
                        {
                            if (s <= 4)
                            {
                                ReadValueRang = 0X04;
                            }
                            else
                            {
                                ReadValueRang = 0X05;
                            }
                        }
                        if (!com.DCIRCal_RY(CaliValue[s], true))
                            return false;
                        System.Threading.Thread.Sleep(100);
                        for (int i = 0; i < 5; i++)
                        {
                            if (!com.RY3Enable_RY(true))
                                return false;
                            System.Threading.Thread.Sleep(100);
                            if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellCurCheckDMMValue))
                                return false;
                            if (!com.RY3Enable_RY(false))
                                return false;
                            System.Threading.Thread.Sleep(300);
                            if (!com.MixReadDCIRValue(ReadValueRang, out CellCurCaliTesterValue1))
                                return false;
                            CellCurCheckDMMValue = Math.Abs(CellCurCheckDMMValue * 1000);
                            Err1 = CellCurCheckDMMValue - CellCurCaliTesterValue1;
                            if (Math.Abs(Err1) >= VoltCheckAcc)
                            {
                                System.Threading.Thread.Sleep(200);
                                continue;
                            }
                            else
                                break;
                        }
                        if (Math.Abs(Err1) < VoltCheckAcc)
                        { Result = "√"; ChkTestNG[s] = false; }
                        else
                        { Result = "×"; ChkTestNG[s] = true; }
                        UpdateUidelegate(DeviceType, Box, "DCIR点检", CaliValue[s].ToString(), CellCurCheckDMMValue.ToString("f2"), CellCurCaliTesterValue1.ToString(), Err1.ToString("f2"), "", VoltCheckAcc.ToString(), Result);
                        if (!com.DCIRCal_RY(CaliValue[s], false))
                            return false;
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
                com.MixDCIREnable();
                //com.MixChgVolCalibration();
                //com.MixDCIREnable();
                //com.MixLoadCurCalibration();
                //com.DsgCur3ACal_RY(false);
                //com.RY3Enable_RY(false);
                //com.MixEQMInit(out Result);
                com.Reset_RY();
                if (DeviceType == "BAT525G" || DeviceType == "BAT525H")
                {
                    com.MixEQMInit(out Result);
                }
            }
        }
    }
}
