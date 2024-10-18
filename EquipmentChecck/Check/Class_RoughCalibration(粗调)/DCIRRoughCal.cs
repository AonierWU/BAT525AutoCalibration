using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool DCIRRoughCal(string Box, double CurAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point, out string strADCData)
        {
            int[] CaliValue = { 0, 10, 30, 50 };//静态电流20000nA校正点
            double[] CaliValueset = { 0, 61.5, 123, 205, 205, 410, 820, 1230, 1230, 1640, 2050, 2460, 2460, 3075, 3690, 4100 };//静态电流20000nA校正点设定电压值
            int[] Vol_ADC_Add_L = new int[16];//存放ADC读取值的低地址   
            int[] Vol_ADC_Add_L1 = new int[16];//
            int[] Vol_ADC_Add_L2 = new int[16];//
            //byte CurRange;
            double CellCurCaliTesterValue = 0;
            //double CellCurCaliDMMValue = 0;
            Point = "未设初始值";
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            //double Resolution = 0.019;//电压零点以外的点的分辨率
            //double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            for (int j = 0; j < 4; j++)
            {
                Vol_ADC_Add_L[j] = 0x9D + j * 2;// addr
            }
            //byte VoltDL, VoltDH, VoltDL2, VoltDH2, VoltDL3, VoltDH3;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            double CellCurCheckDMMValue = 0;
            double CellCurCaliTesterValue1 = 0;
            byte adcDL, adcDM, adcDH;
            byte Rang = 0x00;
            strADCData = "";
            try
            {
                if (ReadEEPROM)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixDCIRCalibration())
                            return false;

                        strADCData = "直流内阻ADC值;";
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            Point = CaliValue[s].ToString() + "uA";
                            if (!com.MixReadChgVolADC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL,out adcDM, out adcDH))
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
                            UpdateUidelegate(DeviceType, Box, "读直流内阻EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
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
                        Point = "直流内阻，点检失败!";
                        if (!com.MixDCIRCalibration())
                            return false;
                        if (!com.DCIRCal_RY(30, true))
                            return false;
                        if (!com.MixReadDCIRValue(Rang,out CellCurCaliTesterValue))
                            return false;
                        CellCurCheckDMMValue = 30;
                        Err1 = CellCurCheckDMMValue - CellCurCaliTesterValue1;
                        if (Math.Abs(Err1) < 10)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType,Box, "DCIR粗调点检", "30", CellCurCheckDMMValue.ToString("f2"), CellCurCaliTesterValue1.ToString("f2"), Err1.ToString("f2"),"", "10", Result);
                        if (Result == "×")
                        {
                            Point = "误差比对超出10mΩ";
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
                    com.MixDCIREnable();
                    com.DCIRCal_RY(30, false);
                }
            }
        }
    }
}
