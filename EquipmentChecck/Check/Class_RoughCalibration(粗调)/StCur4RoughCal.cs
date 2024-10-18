using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool StCur4RoughCal(string Box, double CurAcc, string CurAccResRegulation,  string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point, out string strADCData)
        {
            int[] CaliValue = { 0, 300, 600, 1000, 1000, 2000, 4000, 6000, 6000, 8000, 10000, 12000, 12000, 15000, 18000, 20000 };//静态电流20000nA校正点
            double[] CaliValueset = { 0, 61.5, 123, 205, 205, 410, 820, 1230, 1230, 1640, 2050, 2460, 2460, 3075, 3690, 4100 };//静态电流20000nA校正点设定电压值
            int[] Vol_ADC_Add_L = new int[16];//存放ADC读取值的低地址   
            byte CurRange;
            double CellCurCaliTesterValue = 0;
            double CellCurCaliDMMValue = 0;
            Point = "未设初始值";
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            //double Resolution = 0.019;//电压零点以外的点的分辨率
            //double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            for (int j = 0; j < 16; j++)
            {
                Vol_ADC_Add_L[j] = 0x6C + j * 2;//nA addr
            }
           // byte VoltDL, VoltDH;
            double Err1 = 0;
           // double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            //double CellCurCheckDMMValue = 0;
            //double CellCurCaliTesterValue1 = 0;
            byte adcDL, ADCDM, adcDH;
            strADCData = "";
            try
            {
                if (ReadEEPROM)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixStCurCalibration())
                            return false;
                        strADCData = "20000nA静态电流ADC值;";
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            CurRange = 0x00;
                            if (s < 4)
                            {
                                CurRange = 0x00;
                            }
                            else if (s < 8 && s >= 4)
                            {
                                CurRange = 0x01;
                            }
                            else if (s < 12 && s >= 8)
                            {
                                CurRange = 0x02;
                            }
                            else if (s < 16 && s >= 12)
                            {
                                CurRange = 0x03;
                            }
                            Point = CaliValue[s].ToString() + "nA";
                            if (!com.MixStCurRange(0x03, CurRange))
                                return false;
                            if (!com.MixReadCellADC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL, out ADCDM, out adcDH))
                                return false;
                            strADCData = strADCData + CaliValue[s] + ":" + adcDL + "_" + adcDH + ";";
                            int ValueADC = adcDH * 256 + adcDL;
                            if (CaliValue[s] == 0)
                                Result = "√";
                            else
                            {
                                if (ValueADC != 0)
                                    Result = "√";
                                else Result = "×";
                            }
                            UpdateUidelegate(DeviceType, Box, "读20000nA静态电流EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
                            //if (Result == "×")
                            //    return false;
                        }
                    }
                }
                if (RoughCheck)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixStCurCalibration())
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.StCurCal_RY("20000nA", true))
                            return false;
                        if (!com.MixStCurRange(0x03, 0x02))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                            return false;
                        if (!com.MixSetCellVoltValue((2000)))//设定电压
                            return false;
                        if (!com.MixReadStCurValue(0x02, 0x02, out CellCurCaliTesterValue))
                            return false;
                        Err1 = CellCurCaliTesterValue - 10000;
                        if (CellCurCaliTesterValue > 6000)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, "静态电流20000nA粗调点检", "1000", CellCurCaliDMMValue.ToString("f2"), CellCurCaliTesterValue.ToString("f2"), Err1.ToString("f2"), "", "2000", Result);
                        if (Result == "×")
                        {
                            Point = "误差比对超出2000nA";
                            return false;
                        }
                    }
                    else return false;
                }
                //if (!com.MixStCurCalibration())
                //    return false;
                //    System.Threading.Thread.Sleep(100);
                //if (!com.StCurCal_RY("20000nA", true))
                //    return false;
                //if (true)
                //{
                //    System.Threading.Thread.Sleep(200);
                //    for (int s = 0; s < CaliValue.Length; s++)
                //    {
                //        CurRange = 0x00;
                //        if (s < 4)
                //        {
                //            CurRange = 0x00;
                //        }
                //        else if (s < 8 && s >= 4)
                //        {
                //            CurRange = 0x01;
                //        }
                //        else if (s < 12 && s >= 8)
                //        {
                //            CurRange = 0x02;
                //        }
                //        else if (s < 16 && s >= 12)
                //        {
                //            CurRange = 0x03;
                //        }

                //        if (CaliValue[s] > 0)
                //        {
                //            Deviation = 0.05;//0以上的误差
                //        }
                //        CurAcc = 1;//校准时允许的精度误差
                //        CurAcc = CaliValue[s] * 0.001 + 2;//点检允许的精度误差，万2RD+千1FS
                //        if (!StopStatus)
                //        {
                //            Point = CaliValue[s].ToString() + "nA校准NG";
                //            if (!com.MixStCurRange(0x03, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                //                return false;
                //            if (!com.MixSetCellVoltValue((CaliValueset[s])))//设定电压
                //                return false;
                //            System.Threading.Thread.Sleep(800);
                //            if (!com.MixReadStCur_ADCValue(out VoltDL, out VoltDH))
                //                break;
                //            if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                //                break;
                //            System.Threading.Thread.Sleep(50);
                //            if (!com.MixUpdateCellVolt())
                //                break;
                //            System.Threading.Thread.Sleep(50);
                //            if (!com.MixReadStCurValue(0x02, CurRange, out CellCurCaliTesterValue))
                //                return false;
                //            Err1 = CellCurCaliTesterValue - Convert.ToDouble(CaliValue[s]);
                //            if (Math.Abs(Err1) < CurAcc)
                //                Result = "√";
                //            else
                //                Result = "×";
                //            UpdateUidelegate(DeviceType,Box, CheckType, CaliValue[s].ToString(), CellCurCaliDMMValue.ToString("f2"), CellCurCaliTesterValue.ToString("f2"), Err1.ToString("f2"),"", CurAcc.ToString(), Result);
                //            if (Result == "×")
                //                return false;
                //            continue;
                //        }
                //        else
                //            break;
                //    }
                //}
                //#region 点检所有的点
                //if (!StopStatus)
                //{
                //    for (int s = CaliValue.Length - 1; s >= 0; s--)
                //    {
                //        CurRange = 0x00;
                //        if (s < 4)
                //        {
                //            CurRange = 0x00;
                //        }
                //        else if (s < 8 && s >= 4)
                //        {
                //            CurRange = 0x01;
                //        }
                //        else if (s < 12 && s >= 8)
                //        {
                //            CurRange = 0x02;
                //        }
                //        else if (s < 16 && s >= 12)
                //        {
                //            CurRange = 0x03;
                //        }
                //        CurAcc = CaliValue[s] * 0.001 + 20000 * 0.001;//点检允许的精度误差，万2RD+千1FS                                            //       
                //        Point = CaliValue[s].ToString() + "nA点检NG";
                //        if (!com.MixStCurRange(0x03, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                //            return false;
                //        if (!com.MixSetCellVoltValue((CaliValueset[s])))//设定电压
                //            return false;
                //        if (s == CaliValue.Length - 1)
                //        {
                //            System.Threading.Thread.Sleep(1000);
                //        }
                //        if (s == 0)
                //        {
                //            System.Threading.Thread.Sleep(1000);
                //        }
                //        System.Threading.Thread.Sleep(1000);
                //        if (!com.MixReadStCurValue(0x02, CurRange, out CellCurCaliTesterValue1))
                //            return false;
                //        Err1 = Convert.ToDouble(CaliValue[s]) - CellCurCaliTesterValue1;
                //        if (Math.Abs(Err1) < CurAcc)
                //            Result = "√";
                //        else
                //            Result = "×";
                //        UpdateUidelegate(DeviceType,Box, "静态电流点检", CaliValue[s].ToString(), CellCurCheckDMMValue.ToString("f2"), CellCurCaliTesterValue1.ToString(), Err1.ToString("f2"),"", CurAcc.ToString(), Result);
                //        if (Result == "×")
                //            return false;
                //    }
                //}
                //#endregion
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (RoughCheck)
                {
                    com.MixCellEnable(0x00);
                    com.MixStCurEnable();
                    com.StCurCal_RY("20000nA", false);
                }
            }
        }
    }
}
