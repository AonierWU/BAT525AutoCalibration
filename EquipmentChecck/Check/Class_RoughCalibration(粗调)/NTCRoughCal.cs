using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool NTCRoughCal(string Box, double NtcAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point, out string strADCData1, out string strADCData2, out string strADCData3)
        {

            int[] CaliValue = { 0, 100, 500, 1000, 1500, 2000 };//静态电流20000nA校正点
            int[] Vol_ADC_Add_L = new int[16];//存放ADC读取值的低地址   
            int[] Vol_ADC_Add_L1 = new int[16];//
            int[] Vol_ADC_Add_L2 = new int[16];//
            byte NtcRange;
            double NtcCaliTesterValue = 0;
            Point = "未设初始值";
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            //double Resolution = 0.019;//电压零点以外的点的分辨率
            //double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            for (int j = 0; j < 6; j++)
            {
                Vol_ADC_Add_L[j] = 0x5E + j * 2;// addr

                Vol_ADC_Add_L1[j] = 0xA9 + j * 4;// addr
                Vol_ADC_Add_L2[j] = 0xA9 + j * 4 + 2;// addr

            }
           //byte VoltDL, VoltDH, VoltDL2, VoltDH2, VoltDL3, VoltDH3;
            double Err1 = 0;
           //double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            //double CellCurCheckDMMValue = 0;
            //double NTCCheckTesterValue = 0;
            byte adcDL, adcDH;
            byte adcDL2, adcDH2;
            byte adcDL3, adcDH3;
            strADCData1 = "";
            strADCData2 = "";
            strADCData3 = "";
            try
            {
                if (ReadEEPROM)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixNTCCalibration())
                            return false;
                        if (!com.MixNTCRange(0x00))
                            return false;
                        strADCData1 = "NTC2KADC值1;";
                        strADCData2 = "NTC2KADC值2;";
                        strADCData3 = "NTC2KADC值3;";
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            Point = CaliValue[s].ToString() + "Ω";
                            if (!com.MixReadOcvADC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL, out adcDH))
                                return false;
                            if (!com.MixReadOcvADC(Convert.ToByte(Vol_ADC_Add_L1[s]), out adcDL2, out adcDH2))
                                return false;
                            if (!com.MixReadOcvADC(Convert.ToByte(Vol_ADC_Add_L2[s]), out adcDL3, out adcDH3))
                                return false;
                            strADCData1 = strADCData1 + CaliValue[s] + ":" + adcDL + "_" + adcDH + ";";
                            strADCData2 = strADCData2 + CaliValue[s] + ":" + adcDL2 + "_" + adcDH2 + ";";
                            strADCData3 = strADCData3 + CaliValue[s] + ":" + adcDL3 + "_" + adcDH3 + ";";
                            int ValueADC = adcDH * 256 + adcDL;
                            int ValueADC2 = adcDH2 * 256 + adcDL2;
                            int ValueADC3 = adcDH3 * 256 + adcDL3;
                            if (CaliValue[s] == 0)
                                Result = "√";
                            else
                            {
                                if (ValueADC != 0 && ValueADC2 != 0 && ValueADC3 != 0)
                                    Result = "√";
                                else Result = "×";
                            }
                            UpdateUidelegate(DeviceType, Box, "读NTC2K_EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
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
                        NtcRange = 0x00;
                        if (!com.MixNTCCalibration())
                            return false;
                        if (!com.IDRResCal_RY(1000, true))
                            return false;
                        if (!com.MixNTCRange(NtcRange))//00--2000R，01--20K，02--200K, 03--3M
                            return false;
                        if (!com.MixReadNTCValue(0x00, NtcRange, out NtcCaliTesterValue))
                            return false;
                        Err1 = NtcCaliTesterValue - 1000;
                        if (Math.Abs(Err1) < 200)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, "NTC1粗调点检", "1000", "", NtcCaliTesterValue.ToString("f2"), Err1.ToString(), "", "200", Result);
                        if (Result == "×")
                        {
                            Point = "误差比对超出200Ω";
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
                    com.MixNTCEnable();
                }
            }
        }
    }
}
