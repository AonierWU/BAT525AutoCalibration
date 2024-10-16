using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool CNTstCurNRoughCal(string Box, double CurAcc, string VoltAccJudgeRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point, out string strADCData)
        {
            int[] CaliValue = { 0, -1, -3, -5 };//静态电流校正点
            int[] CNT_ADC_AddL = new int[4];//存放ADC设置值的低地址
            for (int j = 0; j < 4; j++)
            {
                CNT_ADC_AddL[j] = 0XA0 + j * 2;//循环设定ADC的读取值的地址
            }
            double CNTCurCaliTesterValue = 0;
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

                        strADCData = "CNT静态(负)ADC值;";
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            Point = CaliValue[s].ToString() + "mA";
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
                            UpdateUidelegate(DeviceType, Box, "读CNT静态(负)EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
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
                        Point = "CNT(负)静态，点检失败!";
                        if (!com.MixCNTCalibration())
                            return false;
                        if (!com.CNTstCurCal_RY("负", true))
                            return false;
                        if (!com.SetVolandOutPut(frmVerifyDevice.strDCsourceConType, 3000, 100, frmVerifyDevice.strDCch, true))
                            return false;
                        System.Threading.Thread.Sleep(200);//电源开启等待200ms
                        if (com.MixReadCNTValue(0x01, out CNTCurCaliTesterValue))
                            return false;
                        Err1 = 3000 - CNTCurCaliTesterValue;
                        if (Math.Abs(Err1) < 500)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, "CNT(负)粗调点检", "3000", "", CNTCurCaliTesterValue.ToString(), Err1.ToString("f2"), "", CurAcc.ToString(), Result);
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
            catch (Exception) { return false; }
            finally
            {
                if (RoughCheck)
                {
                    DCsource.SetVolandOutPut(frmVerifyDevice.strDCsourceConType, 0, 0, frmVerifyDevice.strDCch, false);
                    com.MixCNTEnable();
                    com.CNTstCurCal_RY("负", false);
                }
            }
        }
    }
}
