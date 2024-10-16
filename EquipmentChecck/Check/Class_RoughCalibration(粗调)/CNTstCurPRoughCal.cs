using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool CNTstCurPRoughCal(string Box, double CurAcc, string VoltAccJudgeRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point, out string strADCData)
        {
            int[] CaliValue = { 0, 1, 3, 5 };//静态电流校正点
            int[] ChgVolValue = { 0, 1010, 3035, 5060 };//静态电流设置电压
            //int[] CNT_DAC_Set_H = new int[4];//DAC设置的高地址
            //int[] CNT_DAC_Set_L = new int[4];//DAC设置的低地址//这个值没有规律，不能通过循环设定
            int[] CNT_ADC_AddL = new int[4];//存放ADC设置值的低地址
            for (int j = 0; j < 4; j++)
            {
                CNT_ADC_AddL[j] = 0X98 + j * 2;//循环设定ADC的读取值的地址
            }
            double CNTCurCaliTesterValue = 0;
            //double CNTCaliDMMValue = 0;
            Point = "未设初始值";
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            //double Resolution = 0.019;//电压零点以外的点的分辨率
            //double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            //string dataForCheck;
            //byte VoltDL, VoltDH;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            //double LoadPartCurCheckDMMValue = 0;
            //double LoadPartCurCaliTesterValue1 = 0;
            byte adcDL, adcDH;
            strADCData = "";
            try
            {
                if (ReadEEPROM)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixCNTCalibration())
                            return false;

                        strADCData = "CNT静态(正)ADC值;";
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            Point = CaliValue[s].ToString() + "uA";
                            if (!com.MixReadOcvADC(Convert.ToByte(CNT_ADC_AddL[s]), out adcDL, out adcDH))
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
                            UpdateUidelegate(DeviceType, Box, "读CNT静态(正)EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
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
                        Point = "CNT(正)静态，点检失败!";
                        if (!com.MixChgVolCalibration())
                            return false;
                        if (!com.MixCNTCalibration())
                            return false;
                        if (!com.MixSetVoltValue(3000))
                            return false;
                        if (!com.CNTstCurCal_RY("正", true))
                            return false;
                        System.Threading.Thread.Sleep(100);//电源开启等待200ms
                        if (com.MixReadCNTValue(0x00, out CNTCurCaliTesterValue))
                            return false;
                        Err1 = 3000 - CNTCurCaliTesterValue;
                        if (Math.Abs(Err1) < 500)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, "CNT(正)粗调点检", "3000", "", CNTCurCaliTesterValue.ToString(), Err1.ToString("f2"), "", CurAcc.ToString(), Result);
                        if (Result == "×")
                        {
                            Point = "误差比对超出500nA";
                            return false;
                        }
                    }
                    else return false;
                }
                return true;

            }
            catch(Exception) { return false; }
            finally
            {
                if (RoughCheck)
                {
                    com.MixCHGEnable();
                    com.MixCNTEnable();
                    com.CNTstCurCal_RY("正", false);
                }
            }
        }
    }

}
