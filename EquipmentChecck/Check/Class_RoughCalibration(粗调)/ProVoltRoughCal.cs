using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool ProVoltRoughCal(string Box, double VoltAcc, string VoltAccResRegulation, string CheckType, string DeviceType,bool ReadEEPROM, bool RoughCheck, out string Point, out string strDACData,out string strADCData)
        {
            int[] CaliValue = { 200, 500, 1000, 3000, 5000, 7000, 9000, 10000 };//电压校正点
            int[] Vol_DAC_Set_H = new int[8] { 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94, 0xAC, 0xC5 };//DAC设置的高地址
            int[] Vol_DAC_Set_L = new int[8] { 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79, 0xE8, 0x58 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] Vol_DAC_Add_L = new int[8];//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址
            for (int j = 0; j < 8; j++)
            {
                Vol_DAC_Add_L[j] = 0X05 + j * 2;//循环设定DAC的设定值的地址
                Vol_ADC_Add_L[j] = 0X2C + j * 2;//循环设定ADC的读取值的地址
            }
            //double ProVolCaliTesterValue = 0;
            //double ProVolCaliDMMValue = 0;
            double ProVolCheckDMMValue = 0;
            double ProVolCheckTesterValue = 0;
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            Point = "未设初始值";
            //double Resolution = 0.153;//电压零点以外的点的分辨率
            //double Deviation = 0.5;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03

            //byte VoltDL, VoltDH;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            byte adcDL, adcDH;
            byte dacDL, dacDH;
            strADCData = "";strDACData = "";
            try
            {
                if(ReadEEPROM)
                {
                    if(!StopStatus)
                    {
                        if (!com.MixProVoltCalibration())
                            return false;
                        strDACData = "编程电压DAC值;";
                        strADCData = "编程电压ADC值;";
                        for (int s=0;s<CaliValue.Length;s++)
                        {
                            Point = CaliValue[s].ToString() + "mV";
                            if (!com.MixReadPrgDAC(Convert.ToByte(Vol_DAC_Add_L[s]), out dacDL, out dacDH))
                                return false;
                            if (!com.MixReadPrgADC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL, out adcDH))
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
                            UpdateUidelegate(DeviceType, Box, "读编程电压EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
                            //if (Result == "×")
                            //    return false;
                        }
                    }
                }
                if(RoughCheck)
                {
                    if (!StopStatus)
                    {
                        if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                            return false;
                        if (!com.MixProVoltCalibration())
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.PrgVolCal_RY(true))
                            return false;
                        if (!com.MixSetProVoltValue(3800))
                            return false;
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ProVolCheckDMMValue))
                            return false;
                        ProVolCheckDMMValue = ProVolCheckDMMValue * 1000;
                        if (!com.MixReadProVoltValue(out ProVolCheckTesterValue))
                            return false;
                        Err1 = ProVolCheckDMMValue - ProVolCheckTesterValue;
                        if (Math.Abs(Err1) < 200)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType,Box, "编程电压粗调点检", "3800", ProVolCheckDMMValue.ToString("f2"), ProVolCheckTesterValue.ToString("f2"), Err1.ToString("f2"),"", "200", Result);
                        if (Result == "×")
                        {
                            Point = "误差比对超出200mV";
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
                    com.MixProVoltEnable(0x00);
                    com.PrgVolCal_RY(false);
                }
                
            }
        }
    }
}
