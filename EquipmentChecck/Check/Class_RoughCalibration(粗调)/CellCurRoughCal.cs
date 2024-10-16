using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool CellCurRoughCal(string Box, double CurAcc, string CurAccJudgeRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point, out string strDACData, out string strADCData)
        {
            //int[] CaliValue = { 1250, 1000, 500, 100, 0 };//电流校正点
            int[] CaliValue = { 0, 100, 500, 1000, 1250 };//电流校正点
            int[] Vol_DAC_Set_H = new int[5] { 0x00, 0x15, 0x78, 0xBF, 0xEE };//DAC设置的高地址
            int[] Vol_DAC_Set_L = new int[5] { 0x00, 0xAF, 0x2B, 0x32, 0x31 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] Vol_DAC_Add_L = new int[5];//存放DAC设置值的低地址
            int[] Vol_ADC_Add_L = new int[5];//存放ADC读取值的低地址   
            Point = "未设初始值";
            //double Resolution = 0.019;//电压零点以外的点的分辨率
            //double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            for (int j = 0; j < 5; j++)
            {
                Vol_DAC_Add_L[j] = 0X1F + j * 2;//循环设定DAC的设定值的地址
                Vol_ADC_Add_L[j] = 0X3E + j * 2;//循环设定ADC的读取值的地址
            }
            double Err1 = 0;
           //double Err2 = 0;
            string Result = "√";
            double CellCurCheckDMMValue = 0;
            double CellCurCaliTesterValue1 = 0;
            byte adcDL, adcDH;
            byte dacDL, ADCDM, dacDH;
            strADCData = ""; strDACData = "";
            try
            {
                if (ReadEEPROM)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixCellCurCalibration())
                            return false;
                        strDACData = "串电流DAC值;";
                        strADCData = "串电流ADC值;";
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            Point = CaliValue[s].ToString() + "mA";
                            if (!com.MixReadCellADC(Convert.ToByte(Vol_DAC_Add_L[s]), out dacDL, out ADCDM, out dacDH))
                                return false;
                            if (!com.MixReadCellDAC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL,  out adcDH))
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
                            UpdateUidelegate(DeviceType, Box, "读串电流EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
                            //if (Result == "×")
                            //    return false;
                        }
                    }
                    else return false;
                }
                if (RoughCheck)
                {
                    if (!StopStatus)
                    {
                        Point = "放电电流1000mA，点检失败!";
                        if (frmVerifyDevice.strMultimeterType != "34401A")
                        {
                            if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "10"))
                                return false;
                        }
                        else {
                            if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "3"))
                                return false;
                        }
                        if (!com.MixCellCurCalibration())
                            return false;
                        if (!com.CellCurCal_RY(true))
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.MixSetCellCurValue(1000))
                            return false;
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CellCurCheckDMMValue))
                            return false;
                        CellCurCheckDMMValue = CellCurCheckDMMValue * 1000;
                        if (!com.MixReadCellCurValue(out CellCurCaliTesterValue1))
                            return false;
                        Err1 = CellCurCheckDMMValue - CellCurCaliTesterValue1;
                        if (Math.Abs(Err1) < 200)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, "串电流粗调点检", "1000", CellCurCheckDMMValue.ToString("f2"), CellCurCaliTesterValue1.ToString("f2"), Err1.ToString("f2"), "", "200", Result);
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
                    com.MixCellEnable(0x00);
                    com.MixStCurEnable();
                    com.CellCurCal_RY(false);
                    com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO");
                }
            }
        }
    }
}
