using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool IDRmoduleCheck(double IdrAcc, out bool IsNG, out string Points)
        {
            int[] idrValue1 = new int[] { 0, 50, 100, 150, 200, 250, 300, 400, 500, 600, 800 };
            int[] idrValue2 = new int[] { 1000, 1500, 2000, 2500, 3000, 4000, 5000, 6000, 8000 };
            int[] idrValue3 = new int[] { 10000, 15000, 20000, 25000, 30000, 40000, 50000, 60000, 80000 };
            int[] idrValue4 = new int[] { 100000, 150000, 200000, 250000, 300000, 400000, 500000, 600000, 700000, 800000 };
            int[] idrValue5 = new int[] { 1000000, 1500000, 2000000, 2500000, 3000000 };
            double NTCCheckTesterValue = 0;
            double dblIdrAcc = 0.0f;
            double Err = 0.0f;
            string Result = "√";
            IsNG = false;
            Points = "未设初始值";
            string msg;
            try
            {
                if (!com.ConfMeter(frmVerifyDevice.strMultimeterConType, "RES", ""))
                    return false;
                if (!com.IdrSelfCheck(true))
                    return false;

                //if (!com.IDRResCal_RY(250000, true))
                //    return false;
                //System.Threading.Thread.Sleep(1000);
                //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                //    return false;

                for (int i = 0; i < idrValue1.Length; i++)
                {
                    msg = idrValue1[i].ToString() + "Ω自检_NG";
                    dblIdrAcc = idrValue1[i] * IdrAcc;
                    if (!com.IDRResCal_RY(idrValue1[i], true))
                        return false;
                    System.Threading.Thread.Sleep(1000);
                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                        return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    Err = NTCCheckTesterValue - idrValue1[i];
                    if (idrValue1[i] == 0)
                    {
                        dblIdrAcc = 1;
                        if (Math.Abs(Err) < dblIdrAcc)
                        {
                            Result = "√";
                            msg = idrValue1[i].ToString() + "Ω自检_OK";
                        }
                        else
                        { Result = "×"; IsNG = true; }
                    }
                    else
                    {
                        if (Math.Abs(Err) < dblIdrAcc)
                        {
                            Result = "√";
                            msg = idrValue1[i].ToString() + "Ω自检_OK";
                        }
                        else { Result = "×"; IsNG = true; }
                    }
                    UpdateListBoxdelegate(msg);
                    UpdateUidelegate(frmVerifyDevice.strCalEQMType, "", "IDR自检", idrValue1[i].ToString(), NTCCheckTesterValue.ToString("f2"), "", Err.ToString("f2"), "", dblIdrAcc.ToString(), Result);
                    //if (Result == "×")
                    //{
                    //    Points += msg + "\r\n";
                    //}
                    if (!com.IDRResCal_RY(idrValue1[i], false))
                        return false;
                }
                for (int i = 0; i < idrValue2.Length; i++)
                {
                    msg = idrValue2[i].ToString() + "Ω自检_NG";
                    dblIdrAcc = idrValue2[i] * IdrAcc;
                    if (!com.IDRResCal_RY(idrValue2[i], true))
                        return false;
                    System.Threading.Thread.Sleep(1000);
                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                        return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    Err = NTCCheckTesterValue - idrValue2[i];
                    if (Math.Abs(Err) < dblIdrAcc)
                    {
                        Result = "√";
                        msg = idrValue2[i].ToString() + "Ω自检_OK";
                    }
                    else { Result = "×"; IsNG = true; }
                    UpdateListBoxdelegate(msg);
                    UpdateUidelegate(frmVerifyDevice.strCalEQMType, "", "IDR自检", idrValue2[i].ToString(), NTCCheckTesterValue.ToString("f2"), "", Err.ToString("f2"), "", dblIdrAcc.ToString(), Result);
                    //if (Result == "×")
                    //{
                    //    Points += msg + "\r\n";
                    //}
                    if (!com.IDRResCal_RY(idrValue2[i], false))
                        return false;
                }
                for (int i = 0; i < idrValue3.Length; i++)
                {
                    msg = idrValue3[i].ToString() + "Ω自检_NG";
                    dblIdrAcc = idrValue3[i] * IdrAcc;
                    if (!com.IDRResCal_RY(idrValue3[i], true))
                        return false;
                    System.Threading.Thread.Sleep(1000);
                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                        return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    Err = NTCCheckTesterValue - idrValue3[i];

                    if (Math.Abs(Err) < dblIdrAcc)
                    {
                        Result = "√";
                        msg = idrValue3[i].ToString() + "Ω自检_OK";
                    }
                    else { Result = "×"; IsNG = true; }
                    UpdateListBoxdelegate(msg);
                    UpdateUidelegate(frmVerifyDevice.strCalEQMType, "", "IDR自检", idrValue3[i].ToString(), NTCCheckTesterValue.ToString("f2"), "", Err.ToString("f2"), "", dblIdrAcc.ToString(), Result);
                    //if (Result == "×")
                    //{
                    //    Points += msg + "\r\n";
                    //}
                    if (!com.IDRResCal_RY(idrValue3[i], false))
                        return false;
                }
                for (int i = 0; i < idrValue4.Length; i++)
                {
                    msg = idrValue4[i].ToString() + "Ω自检_NG";
                    dblIdrAcc = idrValue4[i] * IdrAcc;
                    if (!com.IDRResCal_RY(idrValue4[i], true))
                        return false;
                    System.Threading.Thread.Sleep(1000);
                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                        return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    Err = NTCCheckTesterValue - idrValue4[i];

                    if (Math.Abs(Err) < dblIdrAcc)
                    {
                        Result = "√";
                        msg = idrValue4[i].ToString() + "Ω自检_OK";
                    }
                    else { Result = "×"; IsNG = true; }
                    UpdateListBoxdelegate(msg);
                    UpdateUidelegate(frmVerifyDevice.strCalEQMType, "", "IDR自检", idrValue4[i].ToString(), NTCCheckTesterValue.ToString("f2"), "", Err.ToString("f2"), "", dblIdrAcc.ToString(), Result);
                    //if (Result == "×")
                    //{
                    //    Points += msg + "\r\n";
                    //}
                    if (!com.IDRResCal_RY(idrValue4[i], false))
                        return false;
                }
                for (int i = 0; i < idrValue5.Length; i++)
                {
                    msg = idrValue5[i].ToString() + "Ω自检_NG";
                    dblIdrAcc = idrValue5[i] * IdrAcc;
                    if (!com.IDRResCal_RY(idrValue5[i], true))
                        return false;
                    System.Threading.Thread.Sleep(1000);
                    if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                        return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    //System.Threading.Thread.Sleep(200);
                    //if (!com.ReadMeterValue(frmVerifyDevice.strMultimeterConType, out NTCCheckTesterValue))
                    //    return false;
                    Err = NTCCheckTesterValue - idrValue5[i];

                    if (Math.Abs(Err) < dblIdrAcc)
                    {
                        Result = "√";
                        msg = idrValue5[i].ToString() + "Ω自检_OK";
                    }
                    else { Result = "×"; IsNG = true; }
                    UpdateListBoxdelegate(msg);
                    UpdateUidelegate(frmVerifyDevice.strCalEQMType, "", "IDR自检", idrValue5[i].ToString(), NTCCheckTesterValue.ToString("f2"), "", Err.ToString("f2"), "", dblIdrAcc.ToString(), Result);
                    //if (Result == "×")
                    //{
                    //    Points += msg + "\r\n";
                    //}
                    if (!com.IDRResCal_RY(idrValue5[i], false))
                        return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
