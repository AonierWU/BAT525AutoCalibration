using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool StCurRoughCal(string Box, double CurAcc, string CurAccResRegulation, string CheckType, string DeviceType, bool ReadEEPROM, bool RoughCheck, out string Point,  out string strADCData)
        {
            int[] CaliValue = { 0, 3, 6, 10, 10, 20, 40, 60, 60, 80, 100, 120, 120, 150, 180, 200 };//静态电流200u校正点

            double[] CaliValueset = { 0, 7.5, 15, 25, 25, 50, 100, 150, 150, 200, 250, 300, 300, 375, 450, 500 };//静态电流200u校正点设定电压值


            //int[] Vol_DAC_Set_H = new int[5] { 0x00, 0x15, 0x78, 0xBF, 0xEE };//DAC设置的高地址
            //int[] Vol_DAC_Set_L = new int[5] { 0x00, 0xAF, 0x2B, 0x32, 0x31 };//DAC设置的低地址//这个值没有规律，不能通过循环设定
            //int[] Vol_DAC_Add_L = new int[5];//存放DAC设置值的低地址
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
                Vol_ADC_Add_L[j] = 0x8F + j * 2;//uA addr
            }
           // byte VoltDL, VoltDH;
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
                        strADCData = "200uA静态电流ADC值;";
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
                            Point = CaliValue[s].ToString() + "uA";
                            if (!com.MixStCurRange(0x02, CurRange))
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
                            UpdateUidelegate(DeviceType, Box, "读200uA静态电流EEPROM", CaliValue[s].ToString(), "", "", "", "", "", Result);
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
                        if (!com.StCurCal_RY("200uA", true))
                            return false;
                        if (!com.MixStCurRange(0x02, 0x02))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                            return false;
                        if (!com.MixSetCellVoltValue((200)))//设定电压
                            return false;
                        if (!com.MixReadStCurValue(0x01, 0x02, out CellCurCaliTesterValue))
                            return false;
                        Err1 = CellCurCaliTesterValue - 100000;
                        if (CellCurCaliTesterValue > 60)
                            Result = "√";
                        else
                            Result = "×";
                        UpdateUidelegate(DeviceType, Box, "静态电流200uA粗调点检", "100", CellCurCaliDMMValue.ToString("f2"), CellCurCaliTesterValue.ToString("f2"), Err1.ToString("f2"), "", "2000", Result);
                        if (Result == "×")
                        {
                            Point = "误差比对超出20000nA";
                            return false;
                        }
                    }
                    else return false;
                }
                //if (!com.MixStCurCalibration())
                //    return false;
                //System.Threading.Thread.Sleep(100);
                //if (!com.StCurCal_RY("200uA", true))
                //    return false;
                //if (true)
                //{
                //    System.Threading.Thread.Sleep(200);
                //    for (int s = 0; s < CaliValue.Length; s++)
                //    {
                //        CurRange = 0x00;
                //        if (s < 4)
                //        {
                //            CurRange = 0x00;
                //        }
                //        else if (s < 8 && s >= 4)
                //        {
                //            CurRange = 0x01;
                //        }
                //        else if (s < 12 && s >= 8)
                //        {
                //            CurRange = 0x02;
                //        }
                //        else if (s < 16 && s >= 12)
                //        {
                //            CurRange = 0x03;
                //        }
                //        if (CaliValue[s] > 0)
                //        {
                //            Deviation = 0.05;//0以上的误差
                //        }
                //        CurAcc = CaliValue[s] * 0.001 + 0.2;//点检允许的精度误差，万2RD+千1FS
                //        if (!StopStatus)
                //        {
                //            Point = CaliValue[s].ToString() + "mA校准NG";
                //            //int DL = Vol_DAC_Set_L[s];
                //            //int DH = Vol_DAC_Set_H[s];
                //            //for (int i = 0; i < 30; i++)
                //            //{
                //            //设定预设置
                //            //if (!DeviceCom.SendCellVolt(Convert.ToByte(DL), Convert.ToByte(DH)))
                //            //    return false; 
                //            if (!com.MixStCurRange(0x02, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                //                return false;
                //            if (!com.MixSetCellVoltValue((CaliValueset[s])))//设定电压
                //                return false;
                //            //DMM读取值
                //            // System.Threading.Thread.Sleep(200);
                //            if (Convert.ToInt32(CaliValue[s]) == 3 || Convert.ToInt32(CaliValue[s]) == 6)
                //            {
                //                System.Threading.Thread.Sleep(1500);
                //            }
                //            else
                //            {
                //                System.Threading.Thread.Sleep(300);
                //            }
                //            //if (!ks.ReadValueCur(out CellCurCaliDMMValue))
                //            //    return false;
                //            //CellCurCaliDMMValue = CellCurCaliDMMValue * 1000;
                //            //判断测试


                //            if (!com.MixReadStCur_ADCValue(out VoltDL, out VoltDH))
                //                break;
                //            if (!com.MixWriteCellVolt_DAC(Convert.ToByte(Vol_ADC_Add_L[s]), VoltDL, VoltDH))
                //                break;
                //            System.Threading.Thread.Sleep(50);
                //            if (!com.MixUpdateCellVolt())
                //                break;
                //            System.Threading.Thread.Sleep(50);
                //            if (!com.MixReadStCurValue(0x01, CurRange, out CellCurCaliTesterValue))
                //                return false;
                //            Err1 = CellCurCaliTesterValue - Convert.ToDouble(CaliValue[s]);
                //            if (Math.Abs(Err1) < CurAcc)
                //                Result = "√";
                //            else
                //                Result = "×";
                //            UpdateUidelegate(DeviceType,Box, CheckType, CaliValue[s].ToString(), CellCurCaliDMMValue.ToString("f2"), CellCurCaliTesterValue.ToString("f2"), Err1.ToString("f2"),"", CurAcc.ToString(), Result);
                //            if (Result == "×")
                //                return false;
                //            continue;
                //        }

                //        break;
                //    }
                //}
                //#region 点检所有的点

                //if (!StopStatus)
                //{
                //    for (int s = 0; s < CaliValue.Length; s++)
                //    {
                //        CurRange = 0x00;
                //        if (s < 4)
                //        {
                //            CurRange = 0x00;
                //        }
                //        else if (s < 8 && s >= 4)
                //        {
                //            CurRange = 0x01;
                //        }
                //        else if (s < 12 && s >= 8)
                //        {
                //            CurRange = 0x02;
                //        }
                //        else if (s < 16 && s >= 12)
                //        {
                //            CurRange = 0x03;
                //        }
                //        CurAcc = CaliValue[s] * 0.001 + 200 * 0.001;//点检允许的精度误差，万2RD+千1FS
                //                                                    //       
                //        Point = CaliValue[s].ToString() + "uA点检NG";
                //        //if (!DeviceCom.SetCellCurValue(CaliValue[s]))
                //        //    return false;
                //        if (!com.MixStCurRange(0x02, CurRange))//StCurRange（R,range）静态的电阻R=01 50R ，02 500R ，03 5000R ，04 50K,range=0-3
                //            return false;
                //        if (!com.MixSetCellVoltValue((CaliValueset[s])))//设定电压
                //            return false;
                //        System.Threading.Thread.Sleep(300);

                //        //        CellCurCheckDMMValue = CellCurCheckDMMValue * 1000;
                //        //if (!DeviceCom.ReadCellCurValue(out CellCurCaliTesterValue1))
                //        //    return false;
                //        if (!com.MixReadStCurValue(0x01, CurRange, out CellCurCaliTesterValue1))
                //            return false;
                //        // ReadStCurValue(byte R, byte Range, out double dblCellCur)//R是不同电阻，R=1》200uA R=2》2000uA range是4个量程

                //        //
                //        Err1 = Convert.ToDouble(CaliValue[s]) - CellCurCaliTesterValue1;
                //        if (Math.Abs(Err1) < CurAcc)
                //            Result = "√";
                //        else
                //            Result = "×";
                //        UpdateUidelegate(DeviceType,Box, "静态电流点检", CaliValue[s].ToString(), CellCurCheckDMMValue.ToString("f2"), CellCurCaliTesterValue1.ToString(), Err1.ToString("f2"),"", CurAcc.ToString(), Result);
                //        if (Result == "×")
                //            return false;
                //    }
                //}

                //#endregion

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
                    com.StCurCal_RY("200uA", false);
                }
            }
        }
    }
}
