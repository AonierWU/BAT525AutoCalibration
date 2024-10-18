using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool LoadPartCurRoughCal(string Box, double CurAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point, out string strDACData, out string strADCData)
        {
            int[] CaliValue = { 0, 100, 500, 1000, 1250 };//电流校正点
            int[] Vol_DAC_Set_H = new int[5] { 0x00, 0x16, 0x73, 0xBF, 0xEE };//DAC设置的高地址
            int[] Vol_DAC_Set_L = new int[5] { 0x00, 0xAF, 0x2B, 0xBF, 0x31 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] Vol_DAC_Add_L = new int[5];//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[5];//存放ADC读取值的低地址   

            double LoadPartCurCaliTesterValue = 0;
            double LoadPartCurCaliDMMValue = 0;
            Point = "未设初始值";
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            //double Resolution = 0.019;//电压零点以外的点的分辨率
            //double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            for (int j = 0; j < 5; j++)
            {
                Vol_DAC_Add_L[j] = 0X1F + j * 2;//循环设定DAC的设定值的地址
                Vol_ADC_Add_L[j] = 0X3E + j * 2;//循环设定ADC的读取值的地址
            }
            //byte VoltDL, VoltDH;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            //double LoadPartCurCheckDMMValue = 0;
            //double LoadPartCurCaliTesterValue1 = 0;
            byte adcDL, adcDH;
            byte dacDL, dacDH;
            strADCData = ""; strDACData = "";
            try
            {
                if (ReadEEPROM)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixLoadCurCalibration())
                            return false;
                        strDACData = "LoadPart电流DAC值;";
                        strADCData = "LoadPart电流ADC值;";
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            Point = CaliValue[s].ToString() + "mA";
                            if (!com.MixReadOcvDAC(Convert.ToByte(Vol_DAC_Add_L[s]), out dacDL, out dacDH))
                                return false;
                            if (!com.MixReadOcvADC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL, out adcDH))
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
                            UpdateUidelegate(DeviceType, Box, CheckType, CaliValue[s].ToString(), "", "", "", "", "", Result);
                            //if (Result == "×")
                            //    return false;
                        }
                    }
                }
                if (RoughCheck)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixLoadCurCalibration())
                            return false;
                        if (frmVerifyDevice.strMultimeterType != "34401A")
                        {
                            if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "10"))
                                return false;
                        }
                        else
                        {
                            if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "3"))
                                return false;
                        }
                        if (frmVerifyDevice.strDeviceType == "BAT525G")
                        {
                            if (com.LVDsgCurCal_RY(true))
                                return false;
                            if (com.SetVolandOutPut(frmVerifyDevice.strDCsourceConType, 3000, 1500, frmVerifyDevice.strDCch, true))
                                return false;
                            if (!com.MixSendLoadPartVolt(0x00, 0xBF))
                                return false;
                        }
                        if (!com.MixSetLoadPartCurValue(1000))
                            return false;
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out LoadPartCurCaliDMMValue))
                            return false;
                        LoadPartCurCaliDMMValue = LoadPartCurCaliDMMValue * 1000;
                        if (!com.MixReadLoadPartCurValue(out LoadPartCurCaliTesterValue))
                            return false;
                        Err1 = LoadPartCurCaliDMMValue - LoadPartCurCaliTesterValue;
                        if (Math.Abs(Err1) < 200)
                            Result = "√";
                        else
                            Result = "×";
                        com.MixLoadPartEnable(0x00);
                        UpdateUidelegate(DeviceType,Box, "LV电流粗调点检", "1000", LoadPartCurCaliDMMValue.ToString("f2"), LoadPartCurCaliTesterValue.ToString("f2"), Err1.ToString("f2"),"", "200", Result);
                        if (Result == "×")
                        {
                            Point = "误差比对超出200mA";
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
                    com.MixLoadPartEnable(0x00);
                    com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO");
                    if (frmVerifyDevice.strCalEQMType == "BAT525G")
                    {
                        com.LVDsgCurCal_RY(false);
                        com.SetVolandOutPut(frmVerifyDevice.strDCsourceConType, 0, 0, frmVerifyDevice.strDCch, false);
                    }
                }
            }
        }
    }
}
