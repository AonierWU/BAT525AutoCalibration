using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;
namespace TestSystem_Pack
{
    public delegate string UpDataTxt(string x);
    public partial class frmVerifyDevice : Form
    {
        public void StartCalibration(TestInfo testinfo)
        {
            switch (strCalEQMType)
            {
                case "BAT525G":
                    checkTest.com = new SBT825G();
                    break;
                case "BAT525H":
                    checkTest.com = new SBT825G();
                    break;
                case "BAT525C":
                    checkTest.com = new BAT525C();
                    break;
                case "BAT525D":
                    checkTest.com = new BAT525C();
                    break;
                default:
                    break;
            }
            // checkTest.relayCom = new Com();
            string VerifyStatus = "";
            string Point = "";
            string Point1 = "";
            string Point2 = "";
            string Point3 = "";
            string Msg = "";
            string strSNCode;
            bool NGcontinue = true;
            bool isNG = false;
            List<string> listResult = new List<string>();
            bool TestResult = false;
            //string ChgVoltPoint = "";
            //string DsgCurPoint = "";
            //string DsgVoltPoint = "";
            try
            {
                if (!checkTest.com.Connect(CommType.RS232, testinfo.DeviceCom, InfoLog))//打开设备串口连接
                {
                    MessageBox.Show(testinfo.DeviceCom + "初始化通道失败!");
                    return;
                }

                //设备连接测试

                //万用表连接测试
                if (connMultimeter)
                {
                    if (!checkTest.com.InitMeter(strMultimeterType, strMultimeterConType, strMultimeterAddr, strDMMcom))
                    {
                        MessageBox.Show("万用表连接失败，请检查!");
                        return;
                    }
                    System.Threading.Thread.Sleep(100);
                }
                if (!checkTest.com.MixEQMInit(out Msg))
                {
                    MessageBox.Show("设备连接失败:" + Msg);
                    return;
                }
                if (!checkTest.com.Reset_RY())
                {
                    MessageBox.Show("继电器板复位失败，请检查连接!");
                    return;
                }
                ShowTestResult();
                if (!blStop)
                {
                    #region 设备精调
                    if (testinfo.Calibration)//精调
                    {
                        if (testinfo.TestMode.WriteSNCode || testinfo.TestMode.ReadSNCode)
                        {
                            if (testinfo.TestMode.WriteSNCode)
                            {
                                VerifyStatus = "设备编码待写入";
                                if (!blStop)
                                {

                                    ShowLog("--------设备编码开始写入----------");
                                    if (!checkTest.WriteSNCodeCali(testinfo.BoxNum, testinfo.SNCode, "设备编码写入", testinfo.DeviceType, true, out Point, out strSNCode))
                                    {
                                        VerifyStatus = "设备编码写入失败_" + Point;
                                        listResult.Add(VerifyStatus);
                                        isNG = true;
                                    }
                                    else
                                        VerifyStatus = "设备编码写入_OK(" + strSNCode + ")";


                                    ShowLog("设备编码写入结果：" + VerifyStatus);
                                    ShowLog("--------设备编码写入结束----------");

                                    UpdateListBox(VerifyStatus);
                                    if (isNG)
                                    {
                                        if (!NGcontinue)
                                            return;
                                    }

                                }
                            }
                            else
                            {
                                VerifyStatus = "设备编码待读取";
                                if (!blStop)
                                {

                                    ShowLog("--------设备编码开始读取----------");
                                    if (!checkTest.WriteSNCodeCali("", "", "设备编码读取", testinfo.DeviceType, false, out Point, out strSNCode))
                                    {
                                        VerifyStatus = "设备编码读取失败_" + Point;
                                        listResult.Add(VerifyStatus);
                                        isNG = true;
                                    }
                                    else
                                        VerifyStatus = "设备编码读取_OK(" + strSNCode + ")";
                                    ShowLog("设备编码读取结果：" + VerifyStatus);
                                    ShowLog("--------设备编码读取结束----------");
                                    //BAT525H202409090001
                                    UpdateSNNum(strSNCode.Substring(7, 8), strSNCode.Substring(15, 4));
                                    testinfo.BoxNum = txtSNnum.Text;
                                    UpdateListBox(VerifyStatus);
                                    if (isNG)
                                    {
                                        if (!NGcontinue)
                                            return;
                                    }

                                }
                            }
                        }
                        if (testinfo.TestMode.ChgVoltCalibration)
                        {
                            VerifyStatus = "充电电压待校准";
                            if (!blStop)
                            {

                                ShowLog("--------充电电压开始校准----------");
                                if (!checkTest.ChgVoltCali(testinfo.BoxNum, testinfo.TestAcc.ChgVolCalAcc, testinfo.TestAcc.ChgVolChkAcc, testinfo.JudgeRegulation, "充电电压校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "充电电压校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                    isNG = true;
                                }
                                else
                                    VerifyStatus = "充电电压校准_OK";


                                ShowLog("充电电压校准结果：" + VerifyStatus);
                                ShowLog("--------充电电压结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }

                            }
                        }
                        if (testinfo.TestMode.DsgCurCalibration)
                        {
                            VerifyStatus = "放电电流待校准";
                            if (!blStop)
                            {

                                ShowLog("--------放电电流3A开始校准----------");
                                if (!checkTest.Dsg3ACurCali(testinfo.BoxNum, testinfo.TestAcc.DsgCur3ACalAcc, testinfo.TestAcc.DsgCur3AChkAcc, testinfo.JudgeRegulation, "放电电流3A校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "放电电流3A校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "放电电流3A校准_OK";

                                ShowLog("放电电流3A结果：" + VerifyStatus);
                                ShowLog("--------放电电流3A结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.DsgCurCalibration30A)
                        {
                            //if (testinfo.DeviceType == "BAT525C")
                            //{
                            //    int Result = ShowMessage("确认兆信电源已连接!", 30);
                            //    if (Result != 1)
                            //    {
                            //        VerifyStatus = "放电电流30A未校准，兆信电源未连接或确认超时！";
                            //        UpdateListBox(VerifyStatus);
                            //        return;
                            //    }
                            //}
                            VerifyStatus = "放电电流30A待校准";
                            if (!blStop)
                            {

                                ShowLog("--------放电电流30A开始校准----------");
                                if (!checkTest.Dsg30ACurCali(testinfo.BoxNum, testinfo.TestAcc.DsgCur30ACalAcc, testinfo.TestAcc.DsgCur30AChkAcc, testinfo.JudgeRegulation, testinfo.ResType, "放电电流30A", testinfo.DeviceType, false, testinfo.ExtRes, out Point))
                                {
                                    VerifyStatus = "放电电流30A校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "放电电流30A校准_OK";

                                ShowLog("放电电流30A结果：" + VerifyStatus);
                                ShowLog("--------放电电流30A结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }

                            }
                        }
                        if (testinfo.TestMode.CellVoltCalibration)
                        {
                            VerifyStatus = "串电压待校准";
                            if (!blStop)
                            {

                                ShowLog("--------Cell电压开始校准----------");
                                if (!checkTest.CellVoltCali(testinfo.BoxNum, testinfo.TestAcc.CellVolCalAcc, testinfo.TestAcc.CellVolChkAcc, testinfo.JudgeRegulation, "串电压校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "Cell电压校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "Cell电压校准_OK";

                                ShowLog("Cell电压校准结果：" + VerifyStatus);
                                ShowLog("--------Cell电压结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.CellCurCalibration)
                        {
                            VerifyStatus = "串电流待校准";
                            if (!blStop)
                            {

                                ShowLog("--------Cell电流开始校准----------");
                                if (!checkTest.CellCurCali(testinfo.BoxNum, testinfo.TestAcc.CellCurCalAcc, testinfo.TestAcc.CellCurChkAcc, testinfo.JudgeRegulation, "串电流校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "Cell电流校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "Cell电流校准_OK";

                                ShowLog("Cell电流校准结果：" + VerifyStatus);
                                ShowLog("--------Cell电流结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.OCVCalibration)
                        {
                            VerifyStatus = "开路电压待校准";
                            if (!blStop)
                            {

                                ShowLog("--------开路电压开始校准----------");
                                if (!checkTest.OCVCali(testinfo.BoxNum, testinfo.TestAcc.OcvVolCalAcc, testinfo.TestAcc.OcvVolChkAcc, testinfo.JudgeRegulation, "开路电压校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "开路电压校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "开路电压校准_OK";
                                ShowLog("开路电压校准结果：" + VerifyStatus);
                                ShowLog("--------开路电压结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.ProVoltCalibration)
                        {
                            VerifyStatus = "编程电压待校准";
                            if (!blStop)
                            {

                                ShowLog("--------编程电压开始校准----------");
                                if (!checkTest.ProVoltCali(testinfo.BoxNum, testinfo.TestAcc.PrgVolCalAcc, testinfo.TestAcc.PrgVolChkAcc, testinfo.JudgeRegulation, "编程电压校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "编程电压校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "编程电压校准_OK";

                                ShowLog("编程电压校准结果：" + VerifyStatus);
                                ShowLog("--------编程电压结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.LoadVoltCalibration)
                        {
                            VerifyStatus = "LoadPart电压待校准";
                            if (!blStop)
                            {

                                ShowLog("--------LoadPart电压开始校准----------");
                                if (!checkTest.LoadPartVoltCali(testinfo.BoxNum, testinfo.TestAcc.LoadPartVolCalAcc, testinfo.TestAcc.LoadPartVolChkAcc, testinfo.JudgeRegulation, "LoadPart电压校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "LoadPart电压校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "LoadPart电压校准_OK";

                                ShowLog("LoadPart电压校准结果：" + VerifyStatus);
                                ShowLog("--------LoadPart电压结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.LoadCurCalibration)
                        {
                            VerifyStatus = "LoadPart电流待校准";
                            if (!blStop)
                            {

                                ShowLog("--------LoadPart电流开始校准----------");
                                if (!checkTest.LoadPartCurCali(testinfo.BoxNum, testinfo.TestAcc.LoadPartCurCalAcc, testinfo.TestAcc.LoadPartCurChkAcc, testinfo.JudgeRegulation, "LoadPart电流校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "LoadPart电流校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "LoadPart电流校准_OK";

                                ShowLog("LoadPart电流校准结果：" + VerifyStatus);
                                ShowLog("--------LoadPart电流结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.NTCCalibration)
                        {
                            string TestType1 = "";
                            string TestType2 = "";
                            string TestType3 = "";
                            string TestType4 = "";
                            bool calibrationStatus = true;
                            switch (strCalEQMType)
                            {
                                case "BAT525G":
                                    TestType1 = "NTC电阻_2K";
                                    TestType2 = "NTC电阻_20K";
                                    TestType3 = "NTC电阻_200K";
                                    TestType4 = "NTC电阻_3000K";
                                    break;
                                case "BAT525H":
                                    TestType1 = "NTC电阻_2K";
                                    TestType2 = "NTC电阻_20K";
                                    TestType3 = "NTC电阻_200K";
                                    TestType4 = "NTC电阻_3000K";
                                    break;
                                case "BAT525C":
                                    TestType1 = "NTC电阻_1K";
                                    TestType2 = "NTC电阻_10K";
                                    TestType3 = "NTC电阻_100K";
                                    TestType4 = "NTC电阻_1000K";
                                    break;
                                case "BAT525D":
                                    TestType1 = "NTC电阻_1K";
                                    TestType2 = "NTC电阻_10K";
                                    TestType3 = "NTC电阻_100K";
                                    TestType4 = "NTC电阻_1000K";
                                    break;
                            }
                            if ((!blStop) && calibrationStatus)
                            {
                                VerifyStatus = TestType1 + "待校准";
                                ShowLog("--------" + TestType1 + "开始校准----------");
                                if (!checkTest.NTCCali(testinfo.BoxNum, testinfo.TestAcc.NTC_2KCalAcc, testinfo.TestAcc.NTC_2KChkAcc, testinfo.JudgeRegulation, TestType1, testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = TestType1 + "校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                    calibrationStatus = false;
                                }
                                else
                                    VerifyStatus = TestType1 + "校准_OK";

                                ShowLog(TestType1 + "校准结果：" + VerifyStatus);
                                ShowLog("--------" + TestType1 + "结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                            if ((!blStop) && calibrationStatus)
                            {
                                VerifyStatus = TestType2 + "待校准";

                                ShowLog("--------" + TestType2 + "开始校准----------");
                                if (!checkTest.NTCCali2(testinfo.BoxNum, testinfo.TestAcc.NTC_20KCalAcc, testinfo.TestAcc.NTC_20KChkAcc, testinfo.JudgeRegulation, TestType2, testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = TestType2 + "校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                    calibrationStatus = false;
                                }
                                else
                                    VerifyStatus = TestType2 + "校准_OK";

                                ShowLog(TestType2 + "校准结果：" + VerifyStatus);
                                ShowLog("--------" + TestType2 + "结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                            if ((!blStop) && calibrationStatus)
                            {
                                VerifyStatus = TestType3 + "待校准";

                                ShowLog("--------" + TestType3 + "开始校准----------");
                                if (!checkTest.NTCCali3(testinfo.BoxNum, testinfo.TestAcc.NTC_200KCalAcc, testinfo.TestAcc.NTC_200KChkAcc, testinfo.JudgeRegulation, TestType3, testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = TestType3 + "校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                    calibrationStatus = false;
                                }
                                else
                                    VerifyStatus = TestType3 + "校准_OK";
                                ShowLog(TestType3 + "校准结果：" + VerifyStatus);
                                ShowLog("--------" + TestType3 + "结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                            if ((!blStop) && calibrationStatus)
                            {
                                VerifyStatus = TestType4 + "待校准";

                                ShowLog("--------" + TestType4 + "开始校准----------");
                                if (!checkTest.NTCCali4(testinfo.BoxNum, testinfo.TestAcc.NTC_3000KCalAcc, testinfo.TestAcc.NTC_3000KChkAcc, testinfo.JudgeRegulation, TestType4, testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = TestType4 + "校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                    calibrationStatus = false;
                                }
                                else
                                    VerifyStatus = TestType4 + "校准_OK";

                                ShowLog(TestType4 + "校准结果：" + VerifyStatus);
                                ShowLog("--------" + TestType4 + "结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.IDRCalibration)
                        {
                            VerifyStatus = "IDR电阻_1K待校准";
                            bool calibrationStatus = true;
                            if ((!blStop) && calibrationStatus)
                            {

                                ShowLog("--------IDR电阻_1K开始校准----------");
                                if (!checkTest.IDRCali(testinfo.BoxNum, testinfo.TestAcc.IDR_2KCalAcc, testinfo.TestAcc.IDR_2KChkAcc, testinfo.JudgeRegulation, "IDR电阻_1K校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "IDR电阻_1K校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                    calibrationStatus = false;
                                }
                                else
                                    VerifyStatus = "IDR电阻_1K校准_OK";

                                ShowLog("IDR电阻_1K校准结果：" + VerifyStatus);
                                ShowLog("--------IDR电阻_1K结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                            if ((!blStop) && calibrationStatus)
                            {
                                VerifyStatus = "IDR电阻_10K待校准";

                                ShowLog("--------IDR电阻_10K开始校准----------");
                                if (!checkTest.IDRCali2(testinfo.BoxNum, testinfo.TestAcc.IDR_20KCalAcc, testinfo.TestAcc.IDR_20KChkAcc, testinfo.JudgeRegulation, "IDR电阻_10K校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "IDR电阻_10K校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                    calibrationStatus = false;
                                }
                                else
                                    VerifyStatus = "IDR电阻_10K校准_OK";

                                ShowLog("IDR电阻_10K校准结果：" + VerifyStatus);
                                ShowLog("--------IDR电阻_10K结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                            if ((!blStop) && calibrationStatus)
                            {
                                VerifyStatus = "IDR电阻_100K待校准";

                                ShowLog("--------IDR电阻_100K开始校准----------");
                                if (!checkTest.IDRCali3(testinfo.BoxNum, testinfo.TestAcc.IDR_200KCalAcc, testinfo.TestAcc.IDR_200KChkAcc, testinfo.JudgeRegulation, "IDR电阻_100K校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "IDR电阻_100K校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                    calibrationStatus = false;
                                }
                                else
                                    VerifyStatus = "IDR电阻_100K校准_OK";
                                ShowLog("IDR电阻_100K校准结果：" + VerifyStatus);
                                ShowLog("--------IDR电阻_100K结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                            if ((!blStop) && calibrationStatus)
                            {
                                VerifyStatus = "IDR电阻_1000K待校准";

                                ShowLog("--------IDR电阻_1000K开始校准----------");
                                if (!checkTest.IDRCali4(testinfo.BoxNum, testinfo.TestAcc.IDR_3000KCalAcc, testinfo.TestAcc.IDR_3000KChkAcc, testinfo.JudgeRegulation, "IDR电阻_1000K校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "IDR电阻_1000K校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                    calibrationStatus = false;
                                }
                                else
                                    VerifyStatus = "IDR电阻_1000K校准_OK";

                                ShowLog("IDR电阻_1000K校准结果：" + VerifyStatus);
                                ShowLog("--------IDR电阻_1000K结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.StCurCalibration)
                        {
                            VerifyStatus = "静态电流_200uA待校准";
                            if (!blStop)
                            {

                                ShowLog("--------静态电流_200uA开始校准----------");
                                if (!checkTest.StCurCali(testinfo.BoxNum, testinfo.TestAcc.StCur_200uACalAcc, testinfo.TestAcc.StCur_200uAChkAcc, testinfo.JudgeRegulation, "静态电流_200uA校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "静态电流_200uA校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "静态电流_200uA校准_OK";

                                ShowLog("静态电流_200uA校准结果：" + VerifyStatus);
                                ShowLog("--------静态电流_200uA结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.StCur2Calibration)
                        {
                            VerifyStatus = "静态电流_2000uA待校准";
                            if (!blStop)
                            {

                                ShowLog("--------静态电流_2000uA开始校准----------");
                                if (!checkTest.StCur2Cali(testinfo.BoxNum, testinfo.TestAcc.StCur_2000uACalAcc, testinfo.TestAcc.StCur_2000uAChkAcc, testinfo.JudgeRegulation, "静态电流_2000uA校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "静态电流_2000uA校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "静态电流_2000uA校准_OK";

                                ShowLog("静态电流_2000uA校准结果：" + VerifyStatus);
                                ShowLog("--------静态电流_2000uA结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.StCur3Calibration)
                        {
                            VerifyStatus = "静态电流_2000nA待校准";
                            if (!blStop)
                            {

                                ShowLog("--------静态电流_2000nA开始校准----------");
                                if (!checkTest.StCur3Cali(testinfo.BoxNum, testinfo.TestAcc.StCur_2000nACalAcc, testinfo.TestAcc.StCur_2000nAChkAcc, testinfo.JudgeRegulation, "静态电流_2000nA校准", testinfo.DeviceType, false, out Point))
                                {
                                    VerifyStatus = "静态电流_2000nA校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "静态电流_2000nA校准_OK";

                                ShowLog("静态电流_2000nA校准结果：" + VerifyStatus);
                                ShowLog("--------静态电流_2000nA结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.StCur4Calibration)
                        {
                            VerifyStatus = "静态电流_20000nA待校准";
                            if (!blStop)
                            {

                                ShowLog("--------静态电流_20000nA开始校准----------");
                                if (!checkTest.StCur4Cali(testinfo.BoxNum, testinfo.TestAcc.StCur_20000nACalAcc, testinfo.TestAcc.StCur_20000nAChkAcc, testinfo.JudgeRegulation, "静态电流_20000nA校准", testinfo.DeviceType, false, out Point))
                                { VerifyStatus = "静态电流_20000nA校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "静态电流_20000nA校准_OK";

                                ShowLog("静态电流_20000nA校准结果：" + VerifyStatus);
                                ShowLog("--------静态电流_20000nA结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.StCuruACalibration)
                        {
                            string TestType1 = "";
                            switch (strCalEQMType)
                            {
                                case "BAT525C":
                                    TestType1 = "静态电流_1000uA";
                                    break;
                                case "BAT525D":
                                    TestType1 = "静态电流_2000uA";
                                    break;
                            }
                            VerifyStatus = TestType1 + "待校准";
                            if (!blStop)
                            {
                                ShowLog("--------" + TestType1 + "开始校准----------");
                                //嵌入测试代码
                                if (!checkTest.StCuruACali(testinfo.BoxNum, testinfo.TestAcc.StCur_1000uACalAcc, testinfo.TestAcc.StCur_1000uAChkAcc, testinfo.JudgeRegulation, TestType1, testinfo.DeviceType, false, out Point))
                                { VerifyStatus = TestType1 + "校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = TestType1 + "校准_OK";

                                ShowLog(TestType1 + "校准结果：" + VerifyStatus);
                                ShowLog("--------" + TestType1 + "结束校准----------");
                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.StCurnACalibration)
                        {
                            VerifyStatus = "静态电流_1000nA待校准";
                            if (!blStop)
                            {
                                ShowLog("--------静态电流_1000nA开始校准----------");
                                //嵌入测试代码
                                if (!checkTest.StCurnACali(testinfo.BoxNum, testinfo.TestAcc.StCur_1000nACalAcc, testinfo.TestAcc.StCur_1000nAChkAcc, testinfo.JudgeRegulation, "静态电流_1000nA校准", testinfo.DeviceType, false, out Point))
                                { VerifyStatus = "静态电流_1000nA校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "静态电流_1000nA校准_OK";

                                ShowLog("静态电流_1000nA校准结果：" + VerifyStatus);
                                ShowLog("--------静态电流_1000nA结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.DCIRCalibration)
                        {
                            VerifyStatus = "直流内阻待校准";
                            if (!blStop)
                            {

                                ShowLog("--------直流内阻开始校准----------");
                                if (!checkTest.DCIRCali(testinfo.BoxNum, testinfo.TestAcc.DCIRCalAcc, testinfo.TestAcc.DCIRChkAcc, testinfo.JudgeRegulation, "直流内阻校准", testinfo.DeviceType, false, out Point))
                                { VerifyStatus = "直流内阻校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "直流内阻校准_OK";

                                ShowLog("直流内阻校准结果：" + VerifyStatus);
                                ShowLog("--------直流内阻结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.CNTStPCalibration)
                        {
                            VerifyStatus = "CNT静态(正)电流待校准";
                            if (!blStop)
                            {

                                ShowLog("--------CNT静态(正)电流开始校准----------");
                                if (!checkTest.CNTstCurP(testinfo.BoxNum, testinfo.TestAcc.CNTCurPCalAcc, testinfo.TestAcc.CNTCurPChkAcc, testinfo.JudgeRegulation, "CNT静态(正)校准", testinfo.DeviceType, false, out Point))
                                { VerifyStatus = "CNT静态(正)电流校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "CNT静态(正)电流校准_OK";

                                ShowLog("CNT静态(正)电流校准结果：" + VerifyStatus);
                                ShowLog("--------CNT静态(正)电流结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }
                        if (testinfo.TestMode.CNTStNCalibration)
                        {
                            VerifyStatus = "CNT静态(负)电流待校准";
                            if (!blStop)
                            {

                                ShowLog("--------CNT静态(负)电流开始校准----------");
                                if (!checkTest.CNTstCurN(testinfo.BoxNum, testinfo.TestAcc.CNTCurNCalAcc, testinfo.TestAcc.CNTCurNChkAcc, testinfo.JudgeRegulation, "CNT静态(负)校准", testinfo.DeviceType, false, out Point))
                                { VerifyStatus = "CNT静态(负)电流校准失败_" + Point;
                                    listResult.Add(VerifyStatus);
                                }
                                else
                                    VerifyStatus = "CNT静态(负)电流校准_OK";

                                ShowLog("CNT静态(负)电流校准结果：" + VerifyStatus);
                                ShowLog("--------CNT静态(负)电流结束校准----------");

                                UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }
                            }
                        }

                    }
                    #endregion
                    #region IDR自检
                    else if (testinfo.IDRcheck)
                    {
                        try
                        {
                            if (!blStop)
                            {
                                VerifyStatus = "IDR模块自检待读取";

                                ShowLog("--------IDR模块自检开始读取----------");
                                if (!checkTest.IDRmoduleCheck(testinfo.IdrAcc, out isNG, out Point))
                                    return;
                                if (isNG)
                                {
                                    VerifyStatus = "IDR模块自检阻值不在范围_失败!";
                                    //MessageBox.Show(Point);
                                }
                                else
                                    VerifyStatus = "IDR模块自检阻值_OK!";

                                ShowLog("--------IDR模块自检读取结束----------");
                                //UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }

                            }
                        }
                        catch
                        {
                            return;
                        }

                    }
                    #endregion
                    #region DCIR自检
                    else if (testinfo.DCIRCheck)
                    {
                        try
                        {
                            if (!blStop)
                            {
                                VerifyStatus = "DCIR模块自检待读取";

                                ShowLog("--------DCIR模块自检开始读取----------");
                                if (!checkTest.DCIRmoduleCheck(testinfo.IdrAcc, out isNG, out Point))
                                    return;
                                if (isNG)
                                {
                                    VerifyStatus = "DCIR模块自检阻值不在范围_失败!";
                                    //MessageBox.Show(Point);
                                }
                                else
                                    VerifyStatus = "DCIR模块自检阻值_OK!";

                                ShowLog("--------DCIR模块自检读取结束----------");
                                //UpdateListBox(VerifyStatus);
                                if (isNG)
                                {
                                    if (!NGcontinue)
                                        return;
                                }

                            }
                        }
                        catch
                        {
                            return;
                        }
                    }
                    #endregion
                    #region 读取EEPROM 和 粗调
                    else//粗调或者粗调并点检
                    {
                        #region 读取EEPROM
                        if (testinfo.ReadEEPROM)
                        {
                            try
                            {
                                Encoding encode = UTF8Encoding.UTF8;
                                strConfigPath = strConfigPath + "\\" + TesterType + "粗调文件" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                                if (!File.Exists(strConfigPath))
                                {
                                    FileStream fs = new FileStream(strConfigPath, FileMode.Create);
                                    fs.Close();
                                    fs.Dispose();
                                }
                                FileStream fileStream = new FileStream(strConfigPath, FileMode.Open, FileAccess.Write);
                                StreamWriter sw = new StreamWriter(fileStream, encode);
                                string strADCData, strDACData;
                                string strADCData2, strADCData3;
                                if (testinfo.TestMode.ChgVoltRoughCali)
                                {
                                    strADCData = ""; strDACData = "";
                                    VerifyStatus = "充电电压EEPROM待读取";
                                    if (!blStop)
                                    {

                                        ShowLog("--------充电电压EEPROM开始读取----------");
                                        if (!checkTest.ChgVoltRoughCal(testinfo.BoxNum, 0, "", "读充电电压EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strDACData, out strADCData))
                                        { VerifyStatus = "充电电压EEPROM读取失败" + Point; isNG = true; }
                                        else
                                            VerifyStatus = "充电电压EEPROM读取_OK";

                                        ShowLog("充电电压EEPROM读取结果：" + VerifyStatus);
                                        ShowLog("--------充电电压EEPROM读取结束----------");

                                        UpdateListBox(VerifyStatus);
                                        sw.WriteLine(strDACData);
                                        sw.WriteLine(strADCData);
                                        sw.Flush();
                                        if (isNG)
                                        {
                                            if (!NGcontinue)
                                            {
                                                sw.Close();
                                                fileStream.Close();
                                                return;
                                            }
                                        }
                                    }
                                }
                                if (testinfo.TestMode.DsgCurRoughCali)
                                {
                                    VerifyStatus = "3A放电电流EEPROM待读取";
                                    strADCData = ""; strDACData = "";
                                    if (!blStop)
                                    {

                                        ShowLog("--------3A放电电流EEPROM开始读取----------");
                                        if (!checkTest.Dsg3ACurRoughCal(testinfo.BoxNum, 0, "", "读3A放电电流EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strDACData, out strADCData))
                                        { VerifyStatus = "3A放电电流EEPROM读取失败" + Point; isNG = true; }
                                        else
                                            VerifyStatus = "3A放电电流EEPROM读取_OK";


                                        ShowLog("3A放电电流EEPROM读取结果：" + VerifyStatus);
                                        ShowLog("--------3A放电电流EEPROM读取结束----------");

                                        UpdateListBox(VerifyStatus);
                                        sw.WriteLine(strDACData);
                                        sw.WriteLine(strADCData);
                                        sw.Flush();
                                        if (isNG)
                                        {
                                            if (!NGcontinue)
                                            {
                                                sw.Close();
                                                fileStream.Close();
                                                return;
                                            }
                                        }
                                    }
                                }
                                if (testinfo.TestMode.DsgCurRoughCali30A)
                                {
                                    VerifyStatus = "30A放电电流EEPROM待读取";
                                    strADCData = ""; strDACData = "";
                                    if (!blStop)
                                    {

                                        ShowLog("--------30A放电电流EEPROM开始读取----------");
                                        if (!checkTest.Dsg30ACurRoughCal(testinfo.BoxNum, 0, "", "", "读30A放电电流EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strDACData, out strADCData))
                                        { VerifyStatus = "30A放电电流EEPROM读取失败" + Point; isNG = true; }
                                        else
                                            VerifyStatus = "30A放电电流EEPROM读取_OK";
                                        ShowLog("30A放电电流EEPROM读取结果：" + VerifyStatus);
                                        ShowLog("--------30A放电电流EEPROM读取结束----------");

                                        UpdateListBox(VerifyStatus);
                                        sw.WriteLine(strDACData);
                                        sw.WriteLine(strADCData);
                                        sw.Flush();
                                        if (isNG)
                                        {
                                            if (!NGcontinue)
                                            {
                                                sw.Close();
                                                fileStream.Close();
                                                return;
                                            }
                                        }
                                    }
                                }
                                if (testinfo.TestMode.OCVRoughCali)
                                {
                                    VerifyStatus = "开路电压EEPROM待读取";
                                    strADCData = "";
                                    if (!blStop)
                                    {

                                        ShowLog("--------开路电压EEPROM开始读取----------");
                                        if (!checkTest.OCVRoughCal(testinfo.BoxNum, 0, "", "读开路电压EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData))
                                        { VerifyStatus = "开路电压EEPROM读取失败" + Point; isNG = true; }
                                        else
                                            VerifyStatus = "开路电压EEPROM读取_OK";

                                        ShowLog("开路电压EEPROM读取结果：" + VerifyStatus);
                                        ShowLog("--------开路电压EEPROM读取结束----------");

                                        UpdateListBox(VerifyStatus);
                                        sw.WriteLine(strADCData);
                                        sw.Flush();
                                        if (isNG)
                                        {
                                            if (!NGcontinue)
                                            {
                                                sw.Close();
                                                fileStream.Close();
                                                return;
                                            }
                                        }
                                    }
                                }
                                if (testinfo.TestMode.ProVoltRoughCali)
                                {
                                    VerifyStatus = "编程电压EEPROM待读取";
                                    strDACData = ""; strADCData = "";
                                    if (!blStop)
                                    {

                                        ShowLog("--------编程电压EEPROM开始读取----------");
                                        if (!checkTest.ProVoltRoughCal(testinfo.BoxNum, 0, "", "读编程电压EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strDACData, out strADCData))
                                        { VerifyStatus = "编程电压EEPROM读取失败" + Point; isNG = true; }
                                        else
                                            VerifyStatus = "编程电压EEPROM读取_OK";

                                        ShowLog("编程电压EEPROM读取结果：" + VerifyStatus);
                                        ShowLog("--------编程电压EEPROM读取结束----------");

                                        UpdateListBox(VerifyStatus);
                                        sw.WriteLine(strDACData);
                                        sw.WriteLine(strADCData);
                                        sw.Flush();
                                        if (isNG)
                                        {
                                            if (!NGcontinue)
                                            {
                                                sw.Close();
                                                fileStream.Close();
                                                return;
                                            }
                                        }
                                    }
                                }
                                if (testinfo.TestMode.CellVoltRoughCali)
                                {
                                    VerifyStatus = "Cell电压EEPROM待读取";
                                    strADCData = ""; strDACData = "";
                                    if (!blStop)
                                    {

                                        ShowLog("--------Cell电压EEPROM开始读取----------");
                                        if (!checkTest.CellVoltRoughCal(testinfo.BoxNum, 0, "", "读Cell电压EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strDACData, out strADCData))
                                        { VerifyStatus = "Cell电压EEPROM读取失败" + Point; isNG = true; }
                                        else
                                            VerifyStatus = "Cell电压EEPROM读取_OK";

                                        ShowLog("Cell电压EEPROM读取结果：" + VerifyStatus);
                                        ShowLog("--------Cell电压EEPROM读取结束----------");

                                        UpdateListBox(VerifyStatus);
                                        sw.WriteLine(strDACData);
                                        sw.WriteLine(strADCData);
                                        sw.Flush();
                                        if (isNG)
                                        {
                                            if (!NGcontinue)
                                            {
                                                sw.Close();
                                                fileStream.Close();
                                                return;
                                            }
                                        }
                                    }
                                }
                                if (testinfo.TestMode.CellCurRoughCali)
                                {
                                    VerifyStatus = "Cell电流EEPROM待读取";
                                    strADCData = ""; strDACData = "";
                                    if (!blStop)
                                    {

                                        ShowLog("--------Cell电流EEPROM开始读取----------");
                                        if (!checkTest.CellCurRoughCal(testinfo.BoxNum, 0, "", "读Cell电流EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strDACData, out strADCData))
                                        { VerifyStatus = "Cell电流EEPROM读取失败" + Point; isNG = true; }
                                        else
                                            VerifyStatus = "Cell电流EEPROM读取_OK";

                                        ShowLog("Cell电流EEPROM读取结果：" + VerifyStatus);
                                        ShowLog("--------Cell电流EEPROM读取结束----------");

                                        UpdateListBox(VerifyStatus);
                                        sw.WriteLine(strDACData);
                                        sw.WriteLine(strADCData);
                                        sw.Flush();
                                        if (isNG)
                                        {
                                            if (!NGcontinue)
                                            {
                                                sw.Close();
                                                fileStream.Close();
                                                return;
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.LoadVoltRoughCali)
                                    {
                                        VerifyStatus = "LV电压EEPROM待读取";
                                        strADCData = ""; strDACData = "";
                                        if (!blStop)
                                        {

                                            ShowLog("--------LV电压EEPROM开始读取----------");
                                            if (!checkTest.LoadPartVoltRoughCal(testinfo.BoxNum, 0, "", "读LV电压EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strDACData, out strADCData))
                                            { VerifyStatus = "LV电压EEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "LV电压EEPROM读取_OK";

                                            ShowLog("LV电压EEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------LV电压EEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strDACData);
                                            sw.WriteLine(strADCData);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.LoadCurRoughCali)
                                    {
                                        VerifyStatus = "LV电流EEPROM待读取";
                                        strADCData = ""; strDACData = "";
                                        if (!blStop)
                                        {

                                            ShowLog("--------LV电流EEPROM开始读取----------");
                                            if (!checkTest.LoadPartCurRoughCal(testinfo.BoxNum, 0, "", "读LV电流EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strDACData, out strADCData))
                                            { VerifyStatus = "LV电流EEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "LV电流EEPROM读取_OK";

                                            ShowLog("LV电流EEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------LV电流EEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strDACData);
                                            sw.WriteLine(strADCData);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.NTCRoughCali)
                                    {
                                        VerifyStatus = "NTC电阻EEPROM待读取";
                                        if (!blStop)
                                        {
                                            strADCData = "";
                                            strADCData2 = "";
                                            strADCData3 = "";

                                            ShowLog("--------NTC1电阻EEPROM开始读取----------");
                                            if (!checkTest.NTCRoughCal(testinfo.BoxNum, 0, "", "读NTC1电阻EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData, out strADCData2, out strADCData3))
                                            { VerifyStatus = "NTC1电阻EEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "NTC1电阻EEPROM读取_OK";

                                            ShowLog("NTC1电阻EEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------NTC1电阻EEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.WriteLine(strADCData2);
                                            sw.WriteLine(strADCData3);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }

                                            strADCData = "";
                                            strADCData2 = "";
                                            strADCData3 = "";

                                            ShowLog("--------NTC2电阻EEPROM开始读取----------");
                                            if (!checkTest.NTCRoughCal2(testinfo.BoxNum, 0, "", "读NTC2电阻EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData, out strADCData2, out strADCData3))
                                            { VerifyStatus = "NTC2电阻EEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "NTC2电阻EEPROM读取_OK";

                                            ShowLog("NTC2电阻EEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------NTC2电阻EEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.WriteLine(strADCData2);
                                            sw.WriteLine(strADCData3);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }

                                            strADCData = "";
                                            strADCData2 = "";
                                            strADCData3 = "";

                                            ShowLog("--------NTC3电阻EEPROM开始读取----------");
                                            if (!checkTest.NTCRoughCal3(testinfo.BoxNum, 0, "", "读NTC3电阻EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData, out strADCData2, out strADCData3))
                                            { VerifyStatus = "NTC3电阻EEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "NTC3电阻EEPROM读取_OK";

                                            ShowLog("NTC3电阻EEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------NTC3电阻EEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.WriteLine(strADCData2);
                                            sw.WriteLine(strADCData3);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }

                                            strADCData = "";
                                            strADCData2 = "";
                                            strADCData3 = "";

                                            ShowLog("--------NTC4电阻EEPROM开始读取----------");
                                            if (!checkTest.NTCRoughCal4(testinfo.BoxNum, 0, "", "读NTC4电阻EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData, out strADCData2, out strADCData3))
                                            { VerifyStatus = "NTC4电阻EEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "NTC4电阻EEPROM读取_OK";

                                            ShowLog("NTC4电阻EEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------NTC4电阻EEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.WriteLine(strADCData2);
                                            sw.WriteLine(strADCData3);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.StCurRoughCali)
                                    {
                                        VerifyStatus = "静态电流200uAEEPROM待读取";
                                        strADCData = "";
                                        if (!blStop)
                                        {

                                            ShowLog("--------静态电流200uAEEPROM开始读取----------");
                                            if (!checkTest.StCurRoughCal(testinfo.BoxNum, 0, "", "读静态电流200uAEEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData))
                                            { VerifyStatus = "静态电流200uAEEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "静态电流200uAEEPROM读取_OK";

                                            ShowLog("静态电流200uAEEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------静态电流200uAEEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.StCur2RoughCali)
                                    {
                                        VerifyStatus = "静态电流2000uAEEPROM待读取";
                                        strADCData = "";
                                        if (!blStop)
                                        {

                                            ShowLog("--------静态电流2000uAEEPROM开始读取----------");
                                            if (!checkTest.StCur2RoughCal(testinfo.BoxNum, 0, "", "读静态电流2000uAEEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData))
                                            { VerifyStatus = "静态电流2000uAEEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "静态电流2000uAEEPROM读取_OK";

                                            ShowLog("静态电流2000uAEEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------静态电流2000uAEEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.StCur3RoughCali)
                                    {
                                        VerifyStatus = "静态电流2000nAEEPROM待读取";
                                        strADCData = "";
                                        if (!blStop)
                                        {

                                            ShowLog("--------静态电流2000nAEEPROM开始读取----------");
                                            if (!checkTest.StCur3RoughCal(testinfo.BoxNum, 0, "", "读静态电流2000nAEEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData))
                                            { VerifyStatus = "静态电流2000nAEEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "静态电流2000nAEEPROM读取_OK";

                                            ShowLog("静态电流2000nAEEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------静态电流2000nAEEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.StCur4RoughCali)
                                    {
                                        VerifyStatus = "静态电流20000nAEEPROM待读取";
                                        strADCData = "";
                                        if (!blStop)
                                        {

                                            ShowLog("--------静态电流20000nAEEPROM开始读取----------");
                                            if (!checkTest.StCur4RoughCal(testinfo.BoxNum, 0, "", "读静态电流20000nAEEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData))
                                            { VerifyStatus = "静态电流20000nAEEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "静态电流20000nAEEPROM读取_OK";

                                            ShowLog("静态电流20000nAEEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------静态电流20000nAEEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.DCIRRoughCali)
                                    {
                                        VerifyStatus = "直流内阻EEPROM待读取";
                                        strADCData = "";
                                        if (!blStop)
                                        {

                                            ShowLog("--------直流内阻EEPROM开始读取----------");
                                            if (!checkTest.DCIRRoughCal(testinfo.BoxNum, 0, "", "读直流内阻EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData))
                                            { VerifyStatus = "直流内阻EEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "直流内阻EEPROM读取_OK";

                                            ShowLog("直流内阻EEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------直流内阻EEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.CNTStPRoughCali)
                                    {
                                        VerifyStatus = "CNT静态(正)EEPROM待读取";
                                        strADCData = "";
                                        if (!blStop)
                                        {

                                            ShowLog("--------CNT静态(正)EEPROM开始读取----------");
                                            if (!checkTest.CNTstCurPRoughCal(testinfo.BoxNum, 0, "", "读CNT静态(正)EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData))
                                            { VerifyStatus = "CNT静态(正)EEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "CNT静态(正)EEPROM读取_OK";

                                            ShowLog("CNT静态(正)EEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------CNT静态(正)EEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    if (testinfo.TestMode.CNTStNRoughCali)
                                    {
                                        VerifyStatus = "CNT静态(负)EEPROM待读取";
                                        strADCData = "";
                                        if (!blStop)
                                        {

                                            ShowLog("--------CNT静态(负)EEPROM开始读取----------");
                                            if (!checkTest.CNTstCurNRoughCal(testinfo.BoxNum, 0, "", "读CNT静态(负)EEPROM", testinfo.DeviceType, testinfo.ReadEEPROM, false, out Point, out strADCData))
                                            { VerifyStatus = "CNT静态(负)EEPROM读取失败" + Point; isNG = true; }
                                            else
                                                VerifyStatus = "CNT静态(负)EEPROM读取_OK";

                                            ShowLog("CNT静态(负)EEPROM读取结果：" + VerifyStatus);
                                            ShowLog("--------CNT静态(负)EEPROM读取结束----------");

                                            UpdateListBox(VerifyStatus);
                                            sw.WriteLine(strADCData);
                                            sw.Flush();
                                            if (isNG)
                                            {
                                                if (!NGcontinue)
                                                {
                                                    sw.Close();
                                                    fileStream.Close();
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                    sw.Close();
                                    fileStream.Close();
                                }
                            }
                            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
                        }
                        #endregion
                        #region 粗调写入
                        else if (testinfo.RoughCali)
                        {
                            List<int> ReadCaliValue = new List<int> { };
                            List<byte> DAC_L = new List<byte> { };
                            List<byte> DAC_H = new List<byte> { };
                            List<byte> ADC_L = new List<byte> { };
                            List<byte> ADC_H = new List<byte> { };
                            List<byte> ADC_L2 = new List<byte> { };
                            List<byte> ADC_H2 = new List<byte> { };
                            List<byte> ADC_L3 = new List<byte> { };
                            List<byte> ADC_H3 = new List<byte> { };
                            for (int i = 0; i < strAllLines.Length; i++)
                            {
                                string[] strSplit = strAllLines[i].TrimEnd(';').Split(';');
                                #region 充电电压
                                if (strSplit[0] == "充电电压DAC值")
                                {
                                    if (testinfo.TestMode.ChgVoltRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "充电电压ADC值")
                                        {
                                            for (int J = 1; J < strSplit.Length; J++)
                                            {
                                                string X = strSplit[J];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                DAC_L.Add(Convert.ToByte(n[0]));
                                                DAC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            for (int K = 1; K < strSplit2.Length; K++)
                                            {
                                                string X = strSplit2[K];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ADC_L.Add(Convert.ToByte(n[0]));
                                                ADC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            i = i + 1;
                                        }
                                        //嵌入测试代码

                                        if (!blStop)
                                        {

                                            ShowLog("--------充电电压粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "充电电压", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "充电电压粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------充电电压粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------充电电压粗调开始点检----------");
                                                    if (!checkTest.ChgVoltRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2))
                                                    { VerifyStatus = "充电电压粗调点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "充电电压粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------充电电压粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "充电电压粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------充电电压粗调写入失败----------");
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                else if (strSplit[0] == "3A放电电流DAC值")
                                {
                                    if (testinfo.TestMode.DsgCurRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "3A放电电流ADC值")
                                        {
                                            for (int J = 1; J < strSplit.Length; J++)
                                            {
                                                string X = strSplit[J];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                DAC_L.Add(Convert.ToByte(n[0]));
                                                DAC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            for (int K = 1; K < strSplit2.Length; K++)
                                            {
                                                string X = strSplit2[K];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ADC_L.Add(Convert.ToByte(n[0]));
                                                ADC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            i = i + 1;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------3A放电电流粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "3A放电电流", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "3A放电电流粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------3A放电电流粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------3A放电电流粗调开始点检----------");
                                                    if (!checkTest.Dsg3ACurRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2))
                                                    { VerifyStatus = "3A放电电流点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "3A放电电流粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------3A放电电流粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "3A放电电流粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "30A放电电流DAC值")
                                {
                                    if (testinfo.TestMode.DsgCurRoughCali30A)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "30A放电电流ADC值")
                                        {
                                            for (int J = 1; J < strSplit.Length; J++)
                                            {
                                                string X = strSplit[J];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                DAC_L.Add(Convert.ToByte(n[0]));
                                                DAC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            for (int K = 1; K < strSplit2.Length; K++)
                                            {
                                                string X = strSplit2[K];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ADC_L.Add(Convert.ToByte(n[0]));
                                                ADC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            i = i + 1;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------30A放电电流粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "放电电流30A", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "30A放电电流粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------30A放电电流粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------30A放电电流粗调开始点检----------");
                                                    if (!checkTest.Dsg30ACurRoughCal(testinfo.BoxNum, 200, "", "", testinfo.ResType, testinfo.DeviceType, false, true, out Point, out Point1, out Point2))
                                                    { VerifyStatus = "30A放电电流点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "30A放电电流粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------30A放电电流粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "30A放电电流粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "开路电压电压ADC值")
                                {
                                    if (testinfo.TestMode.OCVRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        for (int K = 1; K < strSplit.Length; K++)
                                        {
                                            string X = strSplit[K];
                                            string[] V = X.Split(':');
                                            string B = V[1];
                                            string[] n = B.Split('_');
                                            ReadCaliValue.Add(Convert.ToInt32(V[0]));
                                            ADC_L.Add(Convert.ToByte(n[0]));
                                            ADC_H.Add(Convert.ToByte(n[1]));
                                        }
                                        //测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------开路电压粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "开路电压", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "开路电压粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------开路电压粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------开路电压粗调开始点检----------");
                                                    if (!checkTest.OCVRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1))
                                                    { VerifyStatus = "开路电压点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "开路电压粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------开路电压粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "开路电压粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "编程电压DAC值")
                                {
                                    if (testinfo.TestMode.ProVoltRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "编程电压ADC值")
                                        {
                                            for (int J = 1; J < strSplit.Length; J++)
                                            {
                                                string X = strSplit[J];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                DAC_L.Add(Convert.ToByte(n[0]));
                                                DAC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            for (int K = 1; K < strSplit2.Length; K++)
                                            {
                                                string X = strSplit2[K];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ADC_L.Add(Convert.ToByte(n[0]));
                                                ADC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            i = i + 1;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------编程电压粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "编程电压", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "编程电压粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------编程电压粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------编程电压粗调开始点检----------");
                                                    if (!checkTest.ProVoltRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2))
                                                    { VerifyStatus = "编程电压点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "编程电压粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------编程电压粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "编程电压粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "串电压DAC值")
                                {
                                    if (testinfo.TestMode.CellVoltRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "串电压ADC值")
                                        {
                                            for (int J = 1; J < strSplit.Length; J++)
                                            {
                                                string X = strSplit[J];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                DAC_L.Add(Convert.ToByte(n[0]));
                                                DAC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            for (int K = 1; K < strSplit2.Length; K++)
                                            {
                                                string X = strSplit2[K];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ADC_L.Add(Convert.ToByte(n[0]));
                                                ADC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            i = i + 1;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------串电压粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "Cell电压", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "串电压粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------串电压粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------串电压粗调开始点检----------");
                                                    if (!checkTest.CellVoltRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2))
                                                    { VerifyStatus = "串电压点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "串电压粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------串电压粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "串电压粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "串电流DAC值")
                                {
                                    if (testinfo.TestMode.CellCurRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "串电流ADC值")
                                        {
                                            for (int J = 1; J < strSplit.Length; J++)
                                            {
                                                string X = strSplit[J];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                DAC_L.Add(Convert.ToByte(n[0]));
                                                DAC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            for (int K = 1; K < strSplit2.Length; K++)
                                            {
                                                string X = strSplit2[K];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ADC_L.Add(Convert.ToByte(n[0]));
                                                ADC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            i = i + 1;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------串电流粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "Cell电流", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "串电流粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------串电流粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------串电流粗调开始点检----------");
                                                    if (!checkTest.CellCurRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2))
                                                    { VerifyStatus = "串电流点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "串电流粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------串电流粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "串电流粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "LoadPart电压DAC值")
                                {
                                    if (testinfo.TestMode.LoadVoltRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "LoadPart电压ADC值")
                                        {
                                            for (int J = 1; J < strSplit.Length; J++)
                                            {
                                                string X = strSplit[J];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                DAC_L.Add(Convert.ToByte(n[0]));
                                                DAC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            for (int K = 1; K < strSplit2.Length; K++)
                                            {
                                                string X = strSplit2[K];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ADC_L.Add(Convert.ToByte(n[0]));
                                                ADC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            i = i + 1;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------LoadPart电压粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "LV电压", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "LoadPart电压粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------LoadPart电压粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------LoadPart电压粗调开始点检----------");
                                                    if (!checkTest.LoadPartVoltRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2))
                                                    { VerifyStatus = "LoadPart电压点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "LoadPart电压粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------LoadPart电压粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "LoadPart电压粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "LoadPart电流DAC值")
                                {
                                    if (testinfo.TestMode.LoadCurRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "LoadPart电流ADC值")
                                        {
                                            for (int J = 1; J < strSplit.Length; J++)
                                            {
                                                string X = strSplit[J];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                DAC_L.Add(Convert.ToByte(n[0]));
                                                DAC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            for (int K = 1; K < strSplit2.Length; K++)
                                            {
                                                string X = strSplit2[K];
                                                string[] V = X.Split(':');
                                                string B = V[1];
                                                string[] n = B.Split('_');
                                                ADC_L.Add(Convert.ToByte(n[0]));
                                                ADC_H.Add(Convert.ToByte(n[1]));
                                            }
                                            i = i + 1;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------LoadPart电流粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "LV电流", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "LoadPart电流粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------LoadPart电流粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------LoadPart电流粗调开始点检----------");
                                                    if (!checkTest.LoadPartCurRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2))
                                                    { VerifyStatus = "LoadPart电流点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "LoadPart电流粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------LoadPart电流粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "LoadPart电流粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "NTC2KADC值1")
                                {
                                    if (testinfo.TestMode.NTCRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        ADC_L2.Clear();
                                        ADC_H2.Clear();
                                        ADC_L3.Clear();
                                        ADC_H3.Clear();
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "NTC2KADC值2")
                                        {
                                            string[] strSplit3 = strAllLines[i + 2].TrimEnd(';').Split(';');
                                            if (strSplit3[0] == "NTC2KADC值3")
                                            {
                                                for (int J = 1; J < strSplit.Length; J++)
                                                {
                                                    string X = strSplit[J];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                    ADC_L.Add(Convert.ToByte(n[0]));
                                                    ADC_H.Add(Convert.ToByte(n[1]));
                                                }
                                                for (int K = 1; K < strSplit2.Length; K++)
                                                {
                                                    string X = strSplit2[K];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ADC_L2.Add(Convert.ToByte(n[0]));
                                                    ADC_H2.Add(Convert.ToByte(n[1]));
                                                }
                                                for (int m = 1; m < strSplit3.Length; m++)
                                                {
                                                    string X = strSplit2[m];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ADC_L3.Add(Convert.ToByte(n[0]));
                                                    ADC_H3.Add(Convert.ToByte(n[1]));
                                                }
                                            }
                                            i = i + 2;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------NTC2K粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "NTC1", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "NTC2K粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------NTC2K粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------NTC2K粗调开始点检----------");
                                                    if (!checkTest.NTCRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2, out Point3))
                                                    { VerifyStatus = "NTC2K点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "NTC2K粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------NTC2K粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "NTC2K粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "NTC20KADC值1")
                                {
                                    if (testinfo.TestMode.NTCRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        ADC_L2.Clear();
                                        ADC_H2.Clear();
                                        ADC_L3.Clear();
                                        ADC_H3.Clear();
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "NTC20KADC值2")
                                        {
                                            string[] strSplit3 = strAllLines[i + 2].TrimEnd(';').Split(';');
                                            if (strSplit3[0] == "NTC20KADC值3")
                                            {
                                                for (int J = 1; J < strSplit.Length; J++)
                                                {
                                                    string X = strSplit[J];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                                    ADC_L.Add(Convert.ToByte(n[0]));
                                                    ADC_H.Add(Convert.ToByte(n[1]));
                                                }
                                                for (int K = 1; K < strSplit2.Length; K++)
                                                {
                                                    string X = strSplit2[K];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ADC_L2.Add(Convert.ToByte(n[0]));
                                                    ADC_H2.Add(Convert.ToByte(n[1]));
                                                }
                                                for (int m = 1; m < strSplit3.Length; m++)
                                                {
                                                    string X = strSplit2[m];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ADC_L3.Add(Convert.ToByte(n[0]));
                                                    ADC_H3.Add(Convert.ToByte(n[1]));
                                                }
                                            }
                                            i = i + 2;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------NTC20K粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "NTC2", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "NTC20K粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------NTC20K粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------NTC20K粗调开始点检----------");
                                                    if (!checkTest.NTCRoughCal2(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2, out Point3))
                                                    { VerifyStatus = "NTC20K点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "NTC20K粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------NTC20K粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "NTC20K粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "NTC200KADC值1")
                                {
                                    if (testinfo.TestMode.NTCRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        ADC_L2.Clear();
                                        ADC_H2.Clear();
                                        ADC_L3.Clear();
                                        ADC_H3.Clear();
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "NTC200KADC值2")
                                        {
                                            string[] strSplit3 = strAllLines[i + 2].TrimEnd(';').Split(';');
                                            if (strSplit3[0] == "NTC200KADC值3")
                                            {
                                                for (int J = 1; J < strSplit.Length; J++)
                                                {
                                                    string X = strSplit[J];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ReadCaliValue.Add(Convert.ToInt32(V[0]));
                                                    ADC_L.Add(Convert.ToByte(n[0]));
                                                    ADC_H.Add(Convert.ToByte(n[1]));
                                                }
                                                for (int K = 1; K < strSplit2.Length; K++)
                                                {
                                                    string X = strSplit2[K];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ADC_L2.Add(Convert.ToByte(n[0]));
                                                    ADC_H2.Add(Convert.ToByte(n[1]));
                                                }
                                                for (int m = 1; m < strSplit3.Length; m++)
                                                {
                                                    string X = strSplit2[m];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ADC_L3.Add(Convert.ToByte(n[0]));
                                                    ADC_H3.Add(Convert.ToByte(n[1]));
                                                }
                                            }
                                            i = i + 2;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------NTC200K粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "NTC3", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "NTC200K粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------NTC200K粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------NTC200K粗调开始点检----------");
                                                    if (!checkTest.NTCRoughCal3(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2, out Point3))
                                                    { VerifyStatus = "NTC200K点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "NTC200K粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------NTC200K粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "NTC200K粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "NTC3000KADC值1")
                                {
                                    if (testinfo.TestMode.NTCRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        DAC_L.Clear();
                                        DAC_H.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        ADC_L2.Clear();
                                        ADC_H2.Clear();
                                        ADC_L3.Clear();
                                        ADC_H3.Clear();
                                        string[] strSplit2 = strAllLines[i + 1].TrimEnd(';').Split(';');
                                        if (strSplit2[0] == "NTC3000KADC值2")
                                        {
                                            string[] strSplit3 = strAllLines[i + 2].TrimEnd(';').Split(';');
                                            if (strSplit3[0] == "NTC3000KADC值3")
                                            {
                                                for (int J = 1; J < strSplit.Length; J++)
                                                {
                                                    string X = strSplit[J];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ReadCaliValue.Add(Convert.ToInt32(V[0]));
                                                    ADC_L.Add(Convert.ToByte(n[0]));
                                                    ADC_H.Add(Convert.ToByte(n[1]));
                                                }
                                                for (int K = 1; K < strSplit2.Length; K++)
                                                {
                                                    string X = strSplit2[K];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ADC_L2.Add(Convert.ToByte(n[0]));
                                                    ADC_H2.Add(Convert.ToByte(n[1]));
                                                }
                                                for (int m = 1; m < strSplit3.Length; m++)
                                                {
                                                    string X = strSplit2[m];
                                                    string[] V = X.Split(':');
                                                    string B = V[1];
                                                    string[] n = B.Split('_');
                                                    ADC_L3.Add(Convert.ToByte(n[0]));
                                                    ADC_H3.Add(Convert.ToByte(n[1]));
                                                }
                                            }
                                            i = i + 2;
                                        }
                                        //嵌入测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------NTC3000K粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "NTC4", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "NTC3000K粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------NTC3000K粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------NTC3000K粗调开始点检----------");
                                                    if (!checkTest.NTCRoughCal4(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1, out Point2, out Point3))
                                                    { VerifyStatus = "NTC3000K点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "NTC3000K粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------NTC3000K粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "NTC3000K粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "200uA静态电流ADC值")
                                {
                                    if (testinfo.TestMode.StCurRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        for (int K = 1; K < strSplit.Length; K++)
                                        {
                                            string X = strSplit[K];
                                            string[] V = X.Split(':');
                                            string B = V[1];
                                            string[] n = B.Split('_');
                                            ReadCaliValue.Add(Convert.ToInt32(V[0]));
                                            ADC_L.Add(Convert.ToByte(n[0]));
                                            ADC_H.Add(Convert.ToByte(n[1]));
                                        }
                                        //测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------200uA静态电流粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "静态电流1", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "200uA静态电流粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------200uA静态电流粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------200uA静态电流粗调开始点检----------");
                                                    if (!checkTest.StCurRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1))
                                                    { VerifyStatus = "200uA静态电流点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "200uA静态电流粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------200uA静态电流粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "200uA静态电流粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "2000uA静态电流ADC值")
                                {
                                    if (testinfo.TestMode.StCur2RoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        for (int K = 1; K < strSplit.Length; K++)
                                        {
                                            string X = strSplit[K];
                                            string[] V = X.Split(':');
                                            string B = V[1];
                                            string[] n = B.Split('_');
                                            ReadCaliValue.Add(Convert.ToInt32(V[0]));
                                            ADC_L.Add(Convert.ToByte(n[0]));
                                            ADC_H.Add(Convert.ToByte(n[1]));
                                        }
                                        //测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------2000uA静态电流粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "静态电流2", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "2000uA静态电流粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------2000uA静态电流粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------2000uA静态电流粗调开始点检----------");
                                                    if (!checkTest.StCur2RoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1))
                                                    { VerifyStatus = "2000uA静态电流点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "2000uA静态电流粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------2000uA静态电流粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "2000uA静态电流粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "2000nA静态电流ADC值")
                                {
                                    if (testinfo.TestMode.StCur3RoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        for (int K = 1; K < strSplit.Length; K++)
                                        {
                                            string X = strSplit[K];
                                            string[] V = X.Split(':');
                                            string B = V[1];
                                            string[] n = B.Split('_');
                                            ReadCaliValue.Add(Convert.ToInt32(V[0]));
                                            ADC_L.Add(Convert.ToByte(n[0]));
                                            ADC_H.Add(Convert.ToByte(n[1]));
                                        }
                                        //测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------2000nA静态电流粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "静态电流3", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "2000nA静态电流粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------2000nA静态电流粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------2000nA静态电流粗调开始点检----------");
                                                    if (!checkTest.StCur3RoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1))
                                                    { VerifyStatus = "2000nA静态电流点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "2000nA静态电流粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------2000nA静态电流粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "2000nA静态电流粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "20000nA静态电流ADC值")
                                {
                                    if (testinfo.TestMode.StCur4RoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        for (int K = 1; K < strSplit.Length; K++)
                                        {
                                            string X = strSplit[K];
                                            string[] V = X.Split(':');
                                            string B = V[1];
                                            string[] n = B.Split('_');
                                            ReadCaliValue.Add(Convert.ToInt32(V[0]));
                                            ADC_L.Add(Convert.ToByte(n[0]));
                                            ADC_H.Add(Convert.ToByte(n[1]));
                                        }
                                        //测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------20000nA静态电流粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "静态电流4", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "20000nA静态电流粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------20000nA静态电流粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------20000nA静态电流粗调开始点检----------");
                                                    if (!checkTest.StCur4RoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1))
                                                    { VerifyStatus = "20000nA静态电流点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "20000nA静态电流粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------20000nA静态电流粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "20000nA静态电流粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "直流内阻ADC值")
                                {
                                    if (testinfo.TestMode.DCIRRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        for (int K = 1; K < strSplit.Length; K++)
                                        {
                                            string X = strSplit[K];
                                            string[] V = X.Split(':');
                                            string B = V[1];
                                            string[] n = B.Split('_');
                                            ReadCaliValue.Add(Convert.ToInt32(V[0]));
                                            ADC_L.Add(Convert.ToByte(n[0]));
                                            ADC_H.Add(Convert.ToByte(n[1]));
                                        }
                                        //测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------直流内阻粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "直流内阻", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "直流内阻粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------直流内阻粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------直流内阻粗调开始点检----------");
                                                    if (!checkTest.DCIRRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1))
                                                    { VerifyStatus = "直流内阻点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "直流内阻粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------直流内阻粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "直流内阻粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "CNT静态(正)ADC值")
                                {
                                    if (testinfo.TestMode.CNTStPRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        for (int K = 1; K < strSplit.Length; K++)
                                        {
                                            string X = strSplit[K];
                                            string[] V = X.Split(':');
                                            string B = V[1];
                                            string[] n = B.Split('_');
                                            ReadCaliValue.Add(Convert.ToInt32(V[0]));
                                            ADC_L.Add(Convert.ToByte(n[0]));
                                            ADC_H.Add(Convert.ToByte(n[1]));
                                        }
                                        //测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------CNT静态(正)粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "CNT静态(正)", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "CNT静态(正)粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------CNT静态(正)粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------CNT静态(正)粗调开始点检----------");
                                                    if (!checkTest.CNTstCurPRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1))
                                                    { VerifyStatus = "CNT静态(正)点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "CNT静态(正)粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------CNT静态(正)粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "CNT静态(正)粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (strSplit[0] == "CNT静态(负)ADC值")
                                {
                                    if (testinfo.TestMode.CNTStNRoughCali)
                                    {
                                        ReadCaliValue.Clear();
                                        ADC_L.Clear();
                                        ADC_H.Clear();//进来先清空
                                        for (int K = 1; K < strSplit.Length; K++)
                                        {
                                            string X = strSplit[K];
                                            string[] V = X.Split(':');
                                            string B = V[1];
                                            string[] n = B.Split('_');
                                            ReadCaliValue.Add(Convert.ToInt16(V[0]));
                                            ADC_L.Add(Convert.ToByte(n[0]));
                                            ADC_H.Add(Convert.ToByte(n[1]));
                                        }
                                        //测试代码
                                        if (!blStop)
                                        {

                                            ShowLog("--------CNT静态(负)粗调开始写入----------");
                                            if (checkTest.WriteEEPROM(testinfo.BoxNum, testinfo.DeviceType, "CNT静态(负)", ReadCaliValue.ToArray(), DAC_L.ToArray(), DAC_H.ToArray(), ADC_L.ToArray(), ADC_H.ToArray(), ADC_L2.ToArray(), ADC_H2.ToArray(), ADC_L3.ToArray(), ADC_H3.ToArray(), out Point))
                                            {
                                                VerifyStatus = "CNT静态(负)粗调写入_OK";
                                                UpdateListBox(VerifyStatus);

                                                ShowLog("--------CNT静态(负)粗调写入成功----------");
                                                if (testinfo.TestMode.WriteCheck)
                                                {

                                                    ShowLog("--------CNT静态(负)粗调开始点检----------");
                                                    if (!checkTest.CNTstCurNRoughCal(testinfo.BoxNum, 200, "", "", testinfo.DeviceType, false, true, out Point, out Point1))
                                                    { VerifyStatus = "CNT静态(负)点检失败!" + Point; isNG = true; }
                                                    else VerifyStatus = "CNT静态(负)粗调点检_OK";
                                                    UpdateListBox(VerifyStatus);

                                                    ShowLog("--------CNT静态(负)粗调结束点检----------");
                                                    if (isNG)
                                                    {
                                                        if (!NGcontinue)
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                VerifyStatus = "CNT静态(负)粗调写入失败!" + Point;
                                                UpdateListBox(VerifyStatus);
                                                if (!NGcontinue)
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else
                                    continue;
                            }
                        }
                        #endregion
                        else
                            return;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally//保存测试数据，导出Excel
            {
                checkTest.com.DisConnect();
                checkTest.com.CloseDMMConn();
                if (listResult.Count == 0) 
                {
                    TestResult = true;
                }
                if (testinfo.Calibration || testinfo.check)
                {
                    if (!SaveTestData(dgvTestResult))
                        MessageBox.Show("数据保存失败!");
                }
                else if (testinfo.RoughCali || testinfo.TestMode.WriteCheck || testinfo.IDRcheck || testinfo.DCIRCheck)
                {
                    if (!SaveTestData(dgvRoughCalResult))
                        MessageBox.Show("数据保存失败!");
                }
                MessageBox.Show("测试结束");
                StopTestProgress(TestResult,listResult);

            }
        }
    }
}
