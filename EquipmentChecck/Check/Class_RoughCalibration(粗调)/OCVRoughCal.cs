using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool OCVRoughCal(string Box, double VoltAcc, string VoltAccResRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point,out string strADCData)
        {
            int[] Cali5VOCVValue = { 0, 100, 500, 1000, 2000, 3000, 4000, 5000 };//电压校正点
            int[] OCV5Vol_DAC_Add_L = new int[8];//存放DAC设置值的低地址
            int[] OCV5Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址
            for (int j = 0; j < 8; j++)
            {
                OCV5Vol_DAC_Add_L[j] = 0X4B + j * 2;//循环设定DAC的设定值的地址
                OCV5Vol_ADC_Add_L[j] = 0X4B + j * 2;//循环设定ADC的读取值的地址
            }
            double OCVVolCaliDMMValue = 0;
            //double OCVVolCheckDMMValue = 0;
            double ReadOCV = 0;
            Point = "未设初始值";
           // double Resolution = 0.076;//电压零点以外的点的分辨率
           // double Deviation = 1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03

            double Err1 = 0;
           // double Err2 = 0;
            string Result = "√";
            byte adcDL, adcDH;
            strADCData = "";
            try
            {
                if(ReadEEPROM)
                {
                    if(!StopStatus)
                    {
                        if (!com.MixOCVCalibration())
                            return false;
                        strADCData = "开路电压电压ADC值;";
                        for (int s=0;s<Cali5VOCVValue.Length;s++)
                        {
                            Point = Cali5VOCVValue[s].ToString() + "mV";
                            if (!com.MixReadOcvADC(Convert.ToByte(OCV5Vol_ADC_Add_L[s]), out adcDL, out adcDH))
                                return false;
                            strADCData = strADCData + Cali5VOCVValue[s] + ":" + adcDL + "_" + adcDH + ";";
                            int ValueADC = adcDH * 256 + adcDL;
                            if (Cali5VOCVValue[s] == 0)
                                Result = "√";
                            else
                            {
                                if (ValueADC != 0)
                                    Result = "√";
                                else Result = "×";
                            }
                            UpdateUidelegate(DeviceType, Box, "读开路电压EEPROM", Cali5VOCVValue[s].ToString(), "", "", "", "", "", Result);
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
                        if (!com.MixChgVolCalibration())
                            return false;
                        if (!com.MixOCVCalibration())
                            return false;
                        if (!com.ChgVolCal_RY(true))
                            return false;
                        if (com.OCVorLVVolCal_RY(true))
                            return false;
                        if (!com.MixSetVoltValue(3800))//设置充电电压
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out OCVVolCaliDMMValue))
                            return false;
                        OCVVolCaliDMMValue = OCVVolCaliDMMValue * 1000;
                        if (!com.MixReadOCVValue(out ReadOCV))
                            return false;
                        Err1= OCVVolCaliDMMValue - OCVVolCaliDMMValue;
                        if (Math.Abs(Err1) < 200)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType,Box, "OCV粗调点检", "3800", OCVVolCaliDMMValue.ToString("f2"), ReadOCV.ToString("f2"), Err1.ToString("f2"),"", "200", Result);
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
                    com.MixEnable(0x00);
                    com.MixOCVEnable(0x00);
                    com.OCVorLVVolCal_RY(false);
                    com.ChgVolCal_RY(false);
                }
            }
        }
    }
}
