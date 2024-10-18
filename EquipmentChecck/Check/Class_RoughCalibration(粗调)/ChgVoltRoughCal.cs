using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool ChgVoltRoughCal(string Box, double VoltAcc, string VoltAccJudgeRegulation, string CheckType, string DeviceType, bool ReadEEPROM,bool RoughCheck, out string Point,out string strDACData,out string strADCData)
        {
            int[] CaliValue=new int[] { };//电压校正点
            int[] Vol_DAC_Set_H = new int[] { };//DAC设置的高地址
            int[] Vol_DAC_Set_L = new int[] { };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] Vol_DAC_Add_L = new int[12];//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[12];//存放ADC读取值的低地址
            if (DeviceType == "BAT525G")
            {
                CaliValue = new int[12] { 0, 20, 100, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000 };//电压校正点
                Vol_DAC_Set_H = new int[12] { 0x01, 0x03, 0x0E, 0x1A, 0x3E, 0x63, 0x7C, 0x94, 0xAC, 0xC5, 0xDD, 0xF6 };//DAC设置的高地址
                Vol_DAC_Set_L = new int[12] { 0xAF, 0x1F, 0x14, 0x4C, 0xF3, 0x9A, 0x0A, 0x79, 0xE8, 0x58, 0xC7, 0x37 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
                for (int j = 0; j < 12; j++)
                {
                    Vol_DAC_Add_L[j] = 0X07 + j * 2;//循环设定DAC的设定值的地址
                    Vol_ADC_Add_L[j] = 0X51 + j * 2;//循环设定ADC的读取值的地址
                }
            }
           // double ChgVolCaliTesterValue = 0;
            double ChgVolCaliDMMValue = 0;
            double ChgVolCheckDMMValue = 0;
            double ChgVolCheckTesterValue = 0;
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            Point = "未设初始值";
            //double Resolution = 0.076;//电压零点以外的点的分辨率
            //double Deviation = 0.05;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03

            //byte VoltDL, VoltDH;
            byte dacDL, dacDH;
            byte adcDL, adcDM, adcDH;
            double Err1 = 0;
           // double Err2 = 0;
            string Result = "√";
            strDACData = "";
            strADCData = "";
            try
            {
                if(ReadEEPROM)//读取eeprom
                {
                    if (!StopStatus)
                    {
                        strDACData = "充电电压DAC值;";
                        strADCData = "充电电压ADC值;";
                        if (!com.MixChgVolCalibration())
                            return false;
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            Point = CaliValue[s].ToString() + "mV";
                            if (!com.MixReadChgVolDAC(Convert.ToByte(Vol_DAC_Add_L[s]), out dacDL, out dacDH))//设置充电电压
                                return false;
                            if (!com.MixReadChgVolADC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL, out adcDM, out adcDH))//读取充电电压
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
                            UpdateUidelegate(DeviceType, Box, "读充电电压EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
                            //if (Result == "×")
                            //    return false;
                        }
                    }
                    else return false;
                }
                if(RoughCheck)//true为点检
                {
                    if (!StopStatus)
                    {
                        Point = "充电电压3800mV，点检失败!";
                        if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                            return false;
                        if (!com.MixChgVolCalibration())
                            return false;
                        if (!com.ChgVolCal_RY(true))
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.MixSetVoltValue(3800))
                            return false;
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out ChgVolCaliDMMValue))
                            return false;
                        ChgVolCheckDMMValue = ChgVolCheckDMMValue * 1000;
                        if (!com.MixReadVoltValue(out ChgVolCheckTesterValue))
                            return false;
                        Err1 = ChgVolCheckDMMValue - ChgVolCheckTesterValue;
                        if (Math.Abs(Err1) < 200)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, "充电电压粗调点检", "3800", ChgVolCheckDMMValue.ToString("f2"), ChgVolCheckTesterValue.ToString("f2"), Err1.ToString("f2"), "", "200", Result);
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
            catch
            {
                return false;
            }
            finally
            {
                if(RoughCheck)
                {
                    com.MixSetVoltValue(0);
                    com.MixEnable(0x00);
                    com.MixCHGEnable();
                    com.ChgVolCal_RY(false);
                }
            }
        }
    }
}
