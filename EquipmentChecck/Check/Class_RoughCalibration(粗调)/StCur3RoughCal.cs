using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool StCur3RoughCal( string Box, double CurAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point, out string strADCData)
        {
            int[] CaliValue = { 0, 30, 60, 100, 100, 200, 400, 600, 600, 800, 1000, 1200, 1200, 1500, 1800, 2000 };//静态电流2000nA校正点
            double[] CaliValueset = { 0, 61.5, 123, 205, 205, 410, 820, 1230, 1230, 1640, 2050, 2460, 2460, 3075, 3690, 4100 };//静态电流2000nA校正点设定电压值
            int[] Vol_ADC_Add_L = new int[16];//存放ADC读取值的低地址   
            byte CurRange;
            double CellCurCaliTesterValue = 0;
            double CellCurCaliDMMValue = 0;
            Point = "未设初始值";
            //int step = 0;
            //int step_H = 0;
            //int step_L = 0;
            //double Resolution = 0.019;//电压零点以外的点的分辨率
            //double Deviation = 0.1;//电压允许误差   1->0.2->0.07-0.01->0.05-0.03
            for (int j = 0; j < 16; j++)
            {

                Vol_ADC_Add_L[j] = 0x4C + j * 2;//nA addr
            }
            //byte VoltDL, VoltDH;
            double Err1 = 0;
            //double Err2 = 0;
            string Result = "√";
            //int rate = 0;
            //double CellCurCheckDMMValue = 0;
            //double CellCurCaliTesterValue1 = 0;
            byte adcDL, ADCDM, adcDH;
            strADCData = "";
            try
            {
                if (ReadEEPROM)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixStCurCalibration())
                            return false;
                        strADCData = "2000nA静态电流ADC值;";
                        for (int s = 0; s < CaliValue.Length; s++)
                        {
                            CurRange = 0x00;
                            if (s < 4)
                            {
                                CurRange = 0x00;
                            }
                            else if (s < 8 && s >= 4)
                            {
                                CurRange = 0x01;
                            }
                            else if (s < 12 && s >= 8)
                            {
                                CurRange = 0x02;
                            }
                            else if (s < 16 && s >= 12)
                            {
                                CurRange = 0x03;
                            }
                            Point = CaliValue[s].ToString() + "nA";
                            if (!com.MixStCurRange(0x04, CurRange))
                                return false;
                            if (!com.MixReadCellADC(Convert.ToByte(Vol_ADC_Add_L[s]), out adcDL, out ADCDM, out adcDH))
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
                            UpdateUidelegate(DeviceType, Box, "读2000nA静态电流EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
                            //if (Result == "×")
                            //    return false;
                        }
                    }
                }
                if (RoughCheck)
                {
                    if (!StopStatus)
                    {
                        if (!com.MixStCurCalibration())
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.StCurCal_RY("2000nA", true))
                            return false;
                        if (!com.MixStCurRange(0x04, 0x02))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                            return false;
                        if (!com.MixSetCellVoltValue((2000)))//设定电压
                            return false;
                        if (!com.MixReadStCurValue(0x03, 0x02, out CellCurCaliTesterValue))
                            return false;
                        Err1 = CellCurCaliTesterValue - 1000;
                        if (CellCurCaliTesterValue > 600)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, "静态电流2000nA粗调点检", "1000", CellCurCaliDMMValue.ToString("f2"), CellCurCaliTesterValue.ToString("f2"), Err1.ToString("f2"), "", "200", Result);
                        if (Result == "×")
                        {
                            Point = "误差比对超出200nA";
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
                    com.StCurCal_RY("2000nA", false);
                }
            }
        }
    }
}
