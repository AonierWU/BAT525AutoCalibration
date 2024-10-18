using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool Dsg3ACurRoughCal(string Box, double CurAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point, out string strDACData, out string strADCData)
        {
            int[] CaliValue = { 0, 10, 100, 600, 1000, 2000, 2500, 3000 };//电流校正点
            int[] Vol_DAC_Set_H = new int[8] { 0x00, 0x02, 0x0A, 0x32, 0x53, 0x93, 0xA4, 0xB9 };//DAC设置的高地址
            //int[] Vol_DAC_Set_H = new int[8] { 0x00, 0x02, 0x0A, 0x32, 0x53, 0xA4, 0xCC, 0xF5 };//DAC设置的高地址
            int[] Vol_DAC_Set_L = new int[8] { 0x00, 0xAF, 0x2B, 0xBF, 0x31, 0x53, 0xE4, 0x79 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] Vol_DAC_Add_L = new int[8];//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[8];//存放ADC读取值的低地址   

            //double DsgCurCaliTesterValue = 0;
            //double DsgCurCaliDMMValue = 0;
            Point = "未设初始值";
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            //double Resolution = 0.06;//电压零点以外的点的分辨率
            //double Deviation = 0.2;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            for (int j = 0; j < 8; j++)
            {
                Vol_DAC_Add_L[j] = 0X25 + j * 2;//循环设定DAC的设定值的地址
                Vol_ADC_Add_L[j] = 0X6F + j * 2;//循环设定ADC的读取值的地址
            }
           // byte VoltDL, VoltDH;
            byte dacDL, dacDH;
            byte adcDL, adcDM, adcDH;
            double Err1 = 0;
           // double Err2 = 0;
            string Result = "√";
           // int rate = 0;
            double DsgCurCheckDMMValue = 0;
            double DsgCurCaliTesterValue1 = 0;
            strDACData = ""; strADCData = "";
            try
            {
                if (ReadEEPROM)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixDsgCurCalibration())
                            return false;
                        if (!com.MixSetCurRange(0x00))
                            return false;
                        strDACData = "3A放电电流DAC值;";
                        strADCData = "3A放电电流ADC值;";
                        System.Threading.Thread.Sleep(100);
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            Point = CaliValue[s].ToString() + "mA";
                            if (!com.MixReadChgVolDAC(Convert.ToByte(Vol_DAC_Add_L[s]), out dacDL, out dacDH))
                                return false;
                            if (!com.MixReadChgVolADC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL,out adcDM, out adcDH))
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
                            UpdateUidelegate(DeviceType, Box, "读3A放电电流EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
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
                        if (!com.DsgCur3ACal_RY(true))
                            return false;
                        if (!com.MixSetCurRange(0x00))
                            return false;
                        if (!com.MixSetCurValue(0x00,1000))
                            return false;
                        //DMM读取值
                        if (!com.MixEnable(0x01))
                            return false;
                        System.Threading.Thread.Sleep(200);
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out DsgCurCheckDMMValue))
                            return false;
                            DsgCurCheckDMMValue = Math.Abs(DsgCurCheckDMMValue) * 1000;
                        if (!com.MixReadCurValue(out DsgCurCaliTesterValue1))
                            return false;
                        if (!com.MixEnable(0x00))
                            return false;
                        Err1 = DsgCurCheckDMMValue - DsgCurCaliTesterValue1;
                        if (Math.Abs(Err1) < 200)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType,Box, "小电流粗调点检", "1000", DsgCurCheckDMMValue.ToString("f2"), DsgCurCaliTesterValue1.ToString("f2"), Err1.ToString("f2"),"", "200", Result);
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
                    com.DsgCur3ACal_RY(false);
                    com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO");
                }
            }
        }
    }
}
