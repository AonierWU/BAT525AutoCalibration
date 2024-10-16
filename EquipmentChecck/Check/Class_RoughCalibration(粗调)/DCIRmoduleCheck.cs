using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool DCIRmoduleCheck(double DCIRAcc, out bool IsNG, out string Points)
        {
            int[] DCIRValue1 = new int[] { 0, 10, 30, 50, 100, 150, 200, 300};
            int[] DCIRValue2 = new int[] { 400, 500, 600, 700, 800, 900, 1000};
            double DCIRCheckTesterValue = 0;
            double dblDCIRAcc = 0.0f;
            double Err = 0.0f;
            double CurCheckDMMValue;
            string Result = "√";
            IsNG = false;
            Points = "未设初始值";
            string msg;
            try
            {
                //先判断设备电流
                if (frmVerifyDevice.strMultimeterType == "34401A")
                {
                    if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "3"))
                        return false;
                    if (!com.MultimeterCur10A(false))
                        return false;
                }
                else
                {
                    if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "CURR", "10"))
                        return false;
                    if (!com.MultimeterCur10A(true))
                        return false;
                }
                if (!com.DsgCur3ACal_RY(true))
                    return false;
                if (!com.MixDCIRCalibration())
                    return false;
                if (frmVerifyDevice.strMultimeterType == "34401A")
                {
                    if (!com.ReadCurValue(frmVerifyDevice.strMultimeterConType, out CurCheckDMMValue))
                        return false;
                }
                else
                {
                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out CurCheckDMMValue))
                        return false;
                }
                CurCheckDMMValue = Math.Abs(CurCheckDMMValue * 1000);
                if (Math.Abs(CurCheckDMMValue - 1000) <= 0.5)
                {
                    if (!com.MixEnable(0X00))//关闭电流使能
                        return false;
                    if (!com.DsgCur3ACal_RY(false))
                        return false;
                    if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "VOLT", "AUTO"))
                        return false;
                    if (!com.RY3Enable_RY(true))
                        return false;
                    for (int i = 0; i < DCIRValue1.Length; i++)
                    {
                        msg = DCIRValue1[i].ToString() + "mΩ自检_NG";
                        dblDCIRAcc = DCIRValue1[i] * DCIRAcc;
                        if (!com.DCIRCal_RY(DCIRValue1[i], true))
                            return false;
                        if (!com.MixEnable(0x01))//打开电流使能
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out DCIRCheckTesterValue))
                            return false;
                        DCIRCheckTesterValue = (DCIRCheckTesterValue * 1000) / CurCheckDMMValue;
                        Err = DCIRCheckTesterValue - DCIRValue1[i];
                        if (DCIRValue1[i] == 0)
                        {
                            dblDCIRAcc = 0.5;
                            if (Math.Abs(Err) < dblDCIRAcc)
                            {
                                Result = "√";
                                msg = DCIRValue1[i].ToString() + "mΩ自检_OK";
                            }
                            else
                            { Result = "×"; IsNG = true; }
                        }
                        else
                        {
                            if (Math.Abs(Err) < dblDCIRAcc)
                            {
                                Result = "√";
                                msg = DCIRValue1[i].ToString() + "mΩ自检_OK";
                            }
                            else { Result = "×"; IsNG = true; }
                        }
                        UpdateListBoxdelegate(msg);
                        UpdateUidelegate(frmVerifyDevice.strCalEQMType, "", "DCIR自检", DCIRValue1[i].ToString(), DCIRCheckTesterValue.ToString("f2"), "", Err.ToString("f2"), "", dblDCIRAcc.ToString(), Result);
                        //if (Result == "×")
                        //{
                        //    Points += msg + "\r\n";
                        //}
                        if (!com.MixEnable(0x00))//关闭电流使能
                            return false;
                        if (!com.DCIRCal_RY(DCIRValue1[i], false))
                            return false;
                    }
                    for (int i = 0; i < DCIRValue2.Length; i++)
                    {
                        msg = DCIRValue2[i].ToString() + "mΩ自检_NG";;
                        dblDCIRAcc = DCIRValue2[i] * DCIRAcc;
                        if (!com.DCIRCal_RY(DCIRValue2[i], true))
                            return false;
                        if (!com.MixEnable(0x01))//打开电流使能
                            return false;
                        System.Threading.Thread.Sleep(100);
                        if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out DCIRCheckTesterValue))
                            return false;
                        DCIRCheckTesterValue = (DCIRCheckTesterValue * 1000) / CurCheckDMMValue;
                        Err = DCIRCheckTesterValue - DCIRValue2[i];
                        if (Math.Abs(Err) < dblDCIRAcc)
                        {
                            Result = "√";
                            msg = DCIRValue2[i].ToString() + "mΩ自检_OK";
                        }
                        else { Result = "×"; IsNG = true; }
                        UpdateListBoxdelegate(msg);
                        UpdateUidelegate(frmVerifyDevice.strCalEQMType, "", "DCIR自检", DCIRValue2[i].ToString(), DCIRCheckTesterValue.ToString("f2"), "", Err.ToString("f2"), "", dblDCIRAcc.ToString(), Result);
                        //if (Result == "×")
                        //{
                        //    Points += msg + "\r\n";
                        //}
                        if (!com.MixEnable(0x00))//关闭电流使能
                            return false;
                        if (!com.DCIRCal_RY(DCIRValue2[i], false))
                            return false;
                    }
                    return true;
                }
                else 
                {
                    Points = "1A电流误差大于0.5mA!";
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
