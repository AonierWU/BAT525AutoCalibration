using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool Dsg30ACurRoughCal(string Box, double CurAcc, string CurAccResRegulation, string ResType, string CheckType, string DeviceType,bool ReadEEPROM, bool RoughCheck, out string Point, out string strDACData, out string strADCData)
        {
            int[] CaliValue = { 3000, 5000, 10000, 15000, 20000, 25000, 30000 };//电流校正点
            int[] Vol_DAC_Set_H = new int[7] { 0x1A, 0x2A, 0x53, 0x5C, 0x7B, 0x99, 0xB7 };//DAC设置的高地址
            int[] Vol_DAC_Set_L = new int[7] { 0x3D, 0xAF, 0x56, 0x09, 0xA4, 0x52, 0x69 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] Vol_DAC_Add_L = new int[7];//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[7];//存放ADC读取值的低地址   

            double DsgCurCaliTesterValue = 0.0;
            double DsgCurCaliDMMValue = 0;
            Point = "未设初始值";
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            //double Resolution = 0.65;//电压零点以外的点的分辨率
            //double Deviation = 0.2;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            for (int j = 0; j < 7; j++)
            {
                Vol_DAC_Add_L[j] = 0X3B + j * 2;//循环设定DAC的设定值的地址
                Vol_ADC_Add_L[j] = 0X85 + j * 2;//循环设定ADC的读取值的地址
            }
           // byte VoltDL, VoltDH;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            int rate = 0;
            //double DsgCurCheckDMMValue = 0;
            //double DsgCurCaliTesterValue1 = 0;
            byte dacDL, dacDH;
            byte adcDL, adcDM, adcDH;
            strDACData = ""; strADCData = "";
            try
            {
                if (ReadEEPROM)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixDsgCurCalibration())
                            return false;
                        if (!com.MixSetCurRange(0x01))
                            return false;
                        System.Threading.Thread.Sleep(100);
                        strDACData = "30A放电电流DAC值;";
                        strADCData = "30A放电电流ADC值;";
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            Point = CaliValue[s].ToString() + "mA";
                            if (!com.MixReadChgVolDAC(Convert.ToByte(Vol_DAC_Add_L[s]), out dacDL, out dacDH))
                                return false;
                            if (!com.MixReadChgVolADC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL, out adcDM, out adcDH))
                                return false;
                            strDACData = strDACData + CaliValue[s] + ":" + dacDL + "_" + dacDH + ";";
                            strADCData = strADCData + CaliValue[s] + ":" + adcDL + "_" + adcDH + ";";
                            int ValueDAC = dacDH * 256 + dacDL;
                            int ValueADC = adcDH * 256 + adcDL;
                            if (CaliValue[s] == 0)
                                Result = "√";
                            else
                            {
                                if (ValueDAC != 0 && ValueADC != 0)
                                    Result = "√";
                                else Result = "×";
                            }
                            UpdateUidelegate(DeviceType, Box, "读30A放电电流EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
                            //if (Result == "×")
                            //    return false;
                        }
                    }
                    else return false;
                }
                if(RoughCheck)
                {
                    if (!StopStatus)
                    {
                        if (ResType == "1A  100mV")
                            rate = 10000;
                        else if (ResType == "1A  10mV")
                            rate = 100000;
                        else if (ResType == "1A  1mV")
                            rate = 1000000;
                        if (!com.MixDsgCurCalibration())
                            return false;
                        if (!com.MixSetCurRange(0x01))
                            return false;
                        if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                            return false;
                        if (!com.DsgCur30ACal_RY(true))
                            return false;
                        if (!com.MixEnable(0x00))
                            return false;
                        if (!com.MixSetCurValue(0x01,10000))
                            return false;
                        if (!com.MixEnable(0x01))
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out DsgCurCaliDMMValue))
                            return false;
                        if (!com.MixReadCurValue(out DsgCurCaliTesterValue))
                            return false;
                        if (!com.MixEnable(0x00))
                            return false;
                        DsgCurCaliDMMValue = Math.Abs(DsgCurCaliDMMValue * rate);
                        Err1 = DsgCurCaliDMMValue - DsgCurCaliTesterValue;
                        if(Math.Abs(Err1)<500)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, "放电电流30A粗调点检", "10000", DsgCurCaliDMMValue.ToString("f2"), DsgCurCaliTesterValue.ToString("f2"), Err1.ToString("f2"), "", "500", Result);
                        if (Result == "×")
                        {
                            Point = "误差比对超出500mA";
                            return false;
                        }
                    }
                    else return false;
                }
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
                    com.DsgCur30ACal_RY(false);
                }
            }
        }
    }
}
