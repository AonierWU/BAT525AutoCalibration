using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using static TestSystem_Pack.MyEqmCmd;

namespace TestSystem_Pack
{
    public partial class frmVerifyDevice : Form
    {
        //public static CheckItem checkTest = new CheckItem();
        private string[] SplitItems;
        Dictionary<string, int> nameToValueMap;
        public void StartTest(string ComNum)
        {

            //string[] coms = ComNum.Split('|');
            string strCom = ComNum; //通道串口号
            string Msg = "";
            string strEQMType = "";
            string strSNCode = "";
            List<string> listResult = new List<string>();
            bool TestResult = false;
            bool Report = false;
            string CheckTool = "";
            try
            {
                switch (strCalEQMType)
                {
                    case "BAT525G":
                        checkTest.com = new SBT825G();
                        nameToValueMap = nameToValueMap_BAT525G;
                        strEQMType = "BAT525G";
                        break;
                    case "BAT525H":
                        checkTest.com = new SBT825G();
                        nameToValueMap = nameToValueMap_BAT525H;
                        strEQMType = "BAT525H";
                        break;
                    case "BAT525C":
                        checkTest.com = new BAT525C();
                        nameToValueMap = nameToValueMap_BAT525C;
                        strEQMType = "BAT525C";
                        break;
                    case "BAT525D":
                        checkTest.com = new BAT525C();
                        nameToValueMap = nameToValueMap_BAT525D;
                        strEQMType = "BAT525D";
                        break;
                    default: break;
                }

                if (!checkTest.com.Connect(CommType.RS232, strCom, InfoLog))
                {
                    MessageBox.Show(strCom + "初始化通道失败");
                    return;
                }
                if (connMultimeter)
                {
                    if (!checkTest.com.InitMeter(strMultimeterType, strConnType, strMultimeterAddr, strDMMcom))
                    {
                        MessageBox.Show("万用表连接失败，请检查!");
                        return;
                    }
                }
                if (!checkTest.com.MixEQMInit(out Msg))//检查设备连接
                {
                    MessageBox.Show("设备复位失败");
                    ShowLog("设备复位失败" + Msg);
                    return;
                }
                if (!checkTest.com.Reset_RY())
                {
                    MessageBox.Show("继电器板复位失败，请检查连接!");
                    ShowLog("继电器板复位失败");
                    return;
                }
                //if (connDCsource)
                //{
                //    if (!checkTest.DCsource.InitMeter(strDCsourceType, strDCsourceConType, strDCsourceAddr))
                //    {
                //        MessageBox.Show("DC电源连接失败，请检查!");
                //        return;
                //    }
                //}
                System.Threading.Thread.Sleep(100);
                int pb = 0;
                ShowProgress(pb);
                ShowTestResult();
                //bool DiffResult = false;
                string VerifyStatus = "";
                if (AloneSave)
                {
                    if (testinfo.ExtRes)
                    {
                        CheckTool = strMultimeterType + "+外接" + testinfo.ResType + "检流电阻";
                    }
                    else
                    {
                        if (strMultimeterType == "" || strMultimeterType == null)
                            CheckTool = "";
                        else
                            CheckTool = strMultimeterType + "+校准板自带1mΩ检流电阻";
                    }
                }
                if (testinfo.TestMode.ReadSNCode)
                {
                    bool TestStatus = false;
                    checkTest.ReadSNCode(testinfo.DeviceType, "读设备编码", out TestStatus, out strSNCode, out Msg);
                    if (TestStatus)
                    {
                        UpdateListBox("读设备编码_NG:" + Msg);
                        listResult.Add("读设备编码_NG:" + Msg);
                        return;
                    }
                    else
                    {
                        if (testinfo.Line != "")
                        {
                            if (testinfo.Line == strSNCode.Substring(15, 4))
                            {
                                UpdateSNNum(strSNCode.Substring(7, 8), strSNCode.Substring(15, 4));
                                testinfo.BoxNum = txtSNnum.Text;
                                UpdateListBox("读设备编码_OK(" + strSNCode + ")");
                            }
                            else
                            {
                                UpdateListBox("读设备编码_NG(" + strSNCode + ")序列号比对NG");
                                listResult.Add("读设备编码_NG(" + strSNCode + ")序列号比对NG");
                                return;
                            }
                        }
                        else
                        {
                            UpdateSNNum(strSNCode.Substring(7, 8), strSNCode.Substring(15, 4));
                            testinfo.BoxNum = txtSNnum.Text;
                            UpdateListBox("读设备编码_OK(" + strSNCode + ")");
                        }
                        if (AloneSave && strCalEQMType == "BAT525H")
                        {
                            Report = true;
                            SaveBarCode(strSNCode, testinfo.CalorCheckUser, CheckTool);
                        }
                    }
                }

                for (int n = 0; n < CheckItems.Count; n++)
                {
                    SplitItems = CheckItems[n].Split(','); //1,Volt,3800|1000|500,20,True

                    if (SplitItems[4].ToUpper() == "TRUE")
                    {
                        #region 充电电压_mV
                        if (SplitItems[1] == "充电电压_mV")
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] VoltItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                VoltItems = SplitItems[2].Split('|');
                            }
                            else
                                VoltItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[VoltItems.Count()];
                            if (!HasValueGreaterThanThreshold(VoltItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.ChgVolCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                if (!checkTest.com.MixChgVolCalibration())
                                    return;
                                ShowLog("仪器复位...");
                                if (!checkTest.com.RSTMeter(strConnType))
                                    return;
                                ShowLog("复位完成");
                                if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", "AUTO"))
                                    return;
                                for (int num = 0; num < VoltItems.Length; num++)
                                {
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.Reset_RY();
                                            checkTest.com.MixSetVoltValue(0);
                                            checkTest.com.MixSetCurValue(0x00, 0.0);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadVoltage(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(VoltItems[num]), testinfo.JudgeRegulation, testinfo.TestAcc.ChgVolChkAcc, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = VoltItems[num] + "mV点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == VoltItems.Length - 1)//判定测试完成
                                    {
                                        checkTest.com.Reset_RY();
                                        checkTest.com.MixEnable(0X00);
                                        checkTest.com.MixSetVoltValue(0);
                                        checkTest.com.MixSetCurValue(0x00, 0.0);
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 放电电流3A_mA
                        else if (SplitItems[1] == "放电电流3A_mA")
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] CurItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                CurItems = SplitItems[2].Split('|');
                            }
                            else
                                CurItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[CurItems.Count()];
                            if (!HasValueGreaterThanThreshold(CurItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.DsgCur3ACal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                if (!checkTest.com.MixDsgCurCalibration())
                                    return;
                                if (!checkTest.com.MixSendChgVolt(0x54, 0xC7))
                                    return;
                                ShowLog("仪器复位...");
                                if (!checkTest.com.RSTMeter(strConnType))
                                    return;
                                ShowLog("仪器复位完成");
                                for (int num = 0; num < CurItems.Length; num++)
                                {
                                    ShowLog("step：放电电流3A测试********" + CurItems[num] + "mA********");
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.Reset_RY();
                                            checkTest.com.MixSetVoltValue(0);
                                            checkTest.com.MixSetCurValue(0x00, 0);
                                            checkTest.com.MixEnable(0x00);
                                            checkTest.com.RSTMeter(strConnType);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadDsgCurrent(testinfo.DeviceType, testinfo.BoxNum, "放电电流3A点检", Convert.ToInt32(CurItems[num]), testinfo.ResType, false, false, testinfo.JudgeRegulation, testinfo.TestAcc.DsgCur3AChkAcc, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = CurItems[num] + "mA点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}

                                    }
                                    if (num == CurItems.Length - 1)
                                    {
                                        checkTest.com.Reset_RY();
                                        //checkTest.com.MultimeterCur10A(false);
                                        checkTest.com.MixSetVoltValue(0);
                                        checkTest.com.MixSetCurValue(0x00, 0);
                                        checkTest.com.MixEnable(0x00);
                                        checkTest.com.RSTMeter(strConnType);
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region IDR电阻_Ω
                        else if (SplitItems[1] == "IDR电阻_Ω" || SplitItems[1] == "NTC电阻_Ω")
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] IDRItems;
                            bool CutRange = false;
                            double ResAcc = 0;
                            if (SplitItems[2].Contains('|'))
                            {
                                IDRItems = SplitItems[2].Split('|');
                            }
                            else
                                IDRItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[IDRItems.Count()];
                            if (!HasValueGreaterThanThreshold(IDRItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (SplitItems[1] == "IDR电阻_Ω")
                                {
                                    if (!checkTest.com.MixIDRCalibration())
                                        return;
                                    ResAcc = testinfo.TestAcc.IDR_2KChkAcc;
                                }
                                else
                                {

                                    if (!checkTest.com.MixNTCCalibration())
                                        return;
                                    ResAcc = testinfo.TestAcc.NTC_2KChkAcc;
                                }
                                for (int num = 0; num < IDRItems.Length; num++)
                                {
                                    ShowLog("step：电阻测试******** " + IDRItems[num] + "Ω ********");
                                    if (num > 0)
                                    {
                                        if (IDRItems[num] == IDRItems[num - 1])
                                            CutRange = true;
                                        else
                                            CutRange = false;
                                        # region 
                                        if (SplitItems[1] == "IDR电阻_Ω")
                                        {
                                            switch (strEQMType)
                                            {
                                                case "BAT525G":
                                                    if (Convert.ToDouble(IDRItems[num]) <= 2000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_20KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_2KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 2000 && Convert.ToDouble(IDRItems[num]) <= 20000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_200KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_20KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 20000 && Convert.ToDouble(IDRItems[num]) <= 200000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_3000KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_200KChkAcc;
                                                    }
                                                    else
                                                    {
                                                        ResAcc = testinfo.TestAcc.IDR_3000KChkAcc;
                                                    }
                                                    break;
                                                case "BAT525H":
                                                    if (Convert.ToDouble(IDRItems[num]) <= 2000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_20KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_2KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 2000 && Convert.ToDouble(IDRItems[num]) <= 20000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_200KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_20KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 20000 && Convert.ToDouble(IDRItems[num]) <= 200000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_3000KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_200KChkAcc;
                                                    }
                                                    else
                                                    {
                                                        ResAcc = testinfo.TestAcc.IDR_3000KChkAcc;
                                                    }
                                                    break;
                                                case "BAT525C":
                                                    if (Convert.ToDouble(IDRItems[num]) <= 1000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_20KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_2KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 1000 && Convert.ToDouble(IDRItems[num]) <= 10000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_200KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_20KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 10000 && Convert.ToDouble(IDRItems[num]) <= 100000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_3000KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_200KChkAcc;
                                                    }
                                                    else
                                                    {
                                                        ResAcc = testinfo.TestAcc.IDR_3000KChkAcc;
                                                    }
                                                    break;
                                                case "BAT525D":
                                                    if (Convert.ToDouble(IDRItems[num]) <= 1000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_20KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_2KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 1000 && Convert.ToDouble(IDRItems[num]) <= 10000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_200KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_20KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 10000 && Convert.ToDouble(IDRItems[num]) <= 100000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.IDR_3000KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.IDR_200KChkAcc;
                                                    }
                                                    else
                                                    {
                                                        ResAcc = testinfo.TestAcc.IDR_3000KChkAcc;
                                                    }
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (strEQMType)
                                            {
                                                case "BAT525G":
                                                    if (Convert.ToDouble(IDRItems[num]) <= 2000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_20KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_2KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 2000 && Convert.ToDouble(IDRItems[num]) <= 20000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_200KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_20KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 20000 && Convert.ToDouble(IDRItems[num]) <= 200000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_3000KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_200KChkAcc;
                                                    }
                                                    else
                                                    {
                                                        ResAcc = testinfo.TestAcc.NTC_3000KChkAcc;
                                                    }
                                                    break;
                                                case "BAT525H":
                                                    if (Convert.ToDouble(IDRItems[num]) <= 2000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_20KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_2KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 2000 && Convert.ToDouble(IDRItems[num]) <= 20000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_200KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_20KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 20000 && Convert.ToDouble(IDRItems[num]) <= 200000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_3000KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_200KChkAcc;
                                                    }
                                                    else
                                                    {
                                                        ResAcc = testinfo.TestAcc.NTC_3000KChkAcc;
                                                    }
                                                    break;
                                                case "BAT525C":
                                                    if (Convert.ToDouble(IDRItems[num]) <= 1000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_20KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_2KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 1000 && Convert.ToDouble(IDRItems[num]) <= 10000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_200KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_20KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 10000 && Convert.ToDouble(IDRItems[num]) <= 100000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_3000KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_200KChkAcc;
                                                    }
                                                    else
                                                    {
                                                        ResAcc = testinfo.TestAcc.NTC_3000KChkAcc;
                                                    }
                                                    break;
                                                case "BAT525D":
                                                    if (Convert.ToDouble(IDRItems[num]) <= 1000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_20KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_2KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 1000 && Convert.ToDouble(IDRItems[num]) <= 10000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_200KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_20KChkAcc;
                                                    }
                                                    else if (Convert.ToDouble(IDRItems[num]) > 10000 && Convert.ToDouble(IDRItems[num]) <= 100000)
                                                    {
                                                        if (CutRange)
                                                            ResAcc = testinfo.TestAcc.NTC_3000KChkAcc;
                                                        else
                                                            ResAcc = testinfo.TestAcc.NTC_200KChkAcc;
                                                    }
                                                    else
                                                    {
                                                        ResAcc = testinfo.TestAcc.NTC_3000KChkAcc;
                                                    }
                                                    break;
                                            }
                                        }
                                        #endregion
                                    }
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.MixNTCEnable();
                                            checkTest.com.Reset_RY();
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadATR(testinfo.DeviceType, testinfo.BoxNum, SplitItems[1], Convert.ToInt32(IDRItems[num]), ResAcc, CutRange, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                    }
                                    if (num == IDRItems.Length - 1)
                                    {
                                        checkTest.com.MixNTCEnable();
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 直流内阻_mΩ
                        else if (SplitItems[1] == "直流内阻_mΩ")
                        {
                            //DiffResult = false;
                            Msg = "";
                            VerifyStatus = "直流内阻点检_NG!";
                            bool CutRange = false;
                            double EqmCurValue = 0.0f;
                            string[] ACIRItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                ACIRItems = SplitItems[2].Split('|');
                            }
                            else
                                ACIRItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[ACIRItems.Count()];
                            //ShowLog("仪器复位...");
                            //if (!checkTest.com.RSTMeter(strConnType))
                            //    return;
                            //ShowLog("复位完成");
                            if (!HasValueGreaterThanThreshold(ACIRItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (frmVerifyDevice.strMultimeterType == "34401A")
                                {
                                    if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "CURR", "3"))
                                        return;
                                }
                                else
                                {
                                    if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "CURR", "10"))
                                        return;
                                    if (!checkTest.com.MultimeterCur10A(true))
                                        return;
                                }
                                if (!checkTest.com.DsgCur3ACal_RY(true))
                                    return;
                                if (!checkTest.com.MixDCIRCalibration())
                                    return;
                                if (frmVerifyDevice.strMultimeterType == "34401A")
                                {
                                    if (!checkTest.com.ReadCurValue(frmVerifyDevice.strConnType, out EqmCurValue))
                                        return;
                                }
                                else
                                {
                                    //if (!checkTest.com.MultimeterCur10A(true))
                                    //    return;
                                    if (!checkTest.com.ReadMeterValue(frmVerifyDevice.strConnType, out EqmCurValue))
                                        return;
                                }
                                EqmCurValue = EqmCurValue * 1000;
                                if (!checkTest.com.DsgCur3ACal_RY(false))
                                    return;
                                if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", "AUTO"))
                                    return;
                                if (Math.Abs(EqmCurValue) < 10)
                                {
                                    VerifyStatus += "电流输出异常!" + EqmCurValue + "mA";
                                    UpdateListBox(VerifyStatus);
                                    return;
                                }
                                else
                                {
                                    for (int num = 0; num < ACIRItems.Length; num++)
                                    {
                                        ShowLog("step：Dcir测试******** " + ACIRItems[num] + "mΩ********");
                                        if (num > 0)
                                        {
                                            if (ACIRItems[num] == ACIRItems[num - 1])
                                            {
                                                CutRange = true;
                                            }
                                            else CutRange = false;
                                        }
                                        for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                        {
                                            pb++;
                                            if (blStop)
                                            {
                                                checkTest.com.MixSetVoltValue(0);
                                                checkTest.com.MixSetCurValue(0x00, 0);
                                                checkTest.com.MixEnable(0x00);
                                                checkTest.com.Reset_RY();
                                                return;
                                            }

                                            ShowLog("循环" + (loop + 1) + "设备测试中...");

                                            checkTest.ReadACIR(testinfo.DeviceType, testinfo.BoxNum, "直流内阻点检", Convert.ToInt32(ACIRItems[num]), testinfo.TestAcc.DCIRChkAcc, EqmCurValue, CutRange, out TestStatus[num], Report);

                                            ShowLog("循环" + (loop + 1) + "设备测试结束");
                                            //if (TestStatus[num])
                                            //{
                                            //    VerifyStatus = ACIRItems[num] + "mR点检_NG";
                                            //    UpdateListBox(VerifyStatus);
                                            //}
                                            ShowProgress(pb);
                                        }
                                        if (num == ACIRItems.Length - 1)
                                        {
                                            checkTest.com.RSTMeter(strConnType);
                                            checkTest.com.MixSetVoltValue(0);
                                            checkTest.com.MixSetCurValue(0x00, 0);
                                            checkTest.com.MixEnable(0x00);
                                            checkTest.com.Reset_RY();
                                            if (Array.Exists(TestStatus, element => element))
                                            {
                                                VerifyStatus = SplitItems[1] + "点检_NG";
                                                UpdateListBox(VerifyStatus);
                                                listResult.Add(VerifyStatus);
                                            }
                                            else
                                            {
                                                VerifyStatus = SplitItems[1] + "点检_OK";
                                                UpdateListBox(VerifyStatus);
                                            }
                                            if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                            {
                                                checkTest.com.MixEQMInit(out Msg);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 放电电流30A_mA
                        else if (SplitItems[1] == "放电电流30A_mA")
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] CurItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                CurItems = SplitItems[2].Split('|');
                            }
                            else
                                CurItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[CurItems.Count()];
                            if (!HasValueGreaterThanThreshold(CurItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (testinfo.ExtRes)
                                {
                                    if (!checkTest.com.DsgCur30ACalExtRes_RY(true))
                                    {
                                        ShowLog("继电器开启切换失败!");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (!checkTest.com.DsgCur30ACal_RY(true))
                                    {
                                        ShowLog("继电器开启切换失败!");
                                        return;
                                    }
                                }
                                if (!checkTest.com.MixDsgCurCalibration())
                                    return;
                                ShowLog("仪器复位...");

                                if (!checkTest.com.RSTMeter(strConnType))
                                    return;
                                ShowLog("仪器复位完成");

                                for (int num = 0; num < CurItems.Length; num++)
                                {
                                    ShowLog("step：放电电流测试********* " + CurItems[num] + "mA ********");
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.Reset_RY();
                                            checkTest.com.MixSetVoltValue(0);
                                            checkTest.com.MixSetCurValue(0x00, 0);
                                            checkTest.com.MixEnable(0x00);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadDsgCurrent(testinfo.DeviceType, testinfo.BoxNum, "放电电流30A点检", Convert.ToInt32(CurItems[num]), testinfo.ResType, true, testinfo.ExtRes, testinfo.JudgeRegulation, testinfo.TestAcc.DsgCur30AChkAcc, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = CurItems[num] + "mA点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == CurItems.Length - 1)
                                    {
                                        checkTest.com.Reset_RY();
                                        checkTest.com.MixSetVoltValue(0);
                                        checkTest.com.MixSetCurValue(0x00, 0);
                                        checkTest.com.MixEnable(0x00);
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 编程电压_mV
                        else if (SplitItems[1] == "编程电压_mV") //
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] SMCItems;
                            bool CutRange = false;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixProVoltCalibration())
                                    return;
                                if (!checkTest.com.PrgVolCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                ShowLog("仪器复位...");
                                if (!checkTest.com.RSTMeter(strConnType))
                                    return;
                                ShowLog("仪器复位完成");
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:编程电压测试-----------" + SMCItems[num] + "mV------------");
                                    if (testinfo.DeviceType == "BAT525C")
                                    {
                                        if (num > 0)
                                        {
                                            if (SMCItems[num] == SMCItems[num - 1])
                                                CutRange = true;
                                            else
                                                CutRange = false;
                                        }
                                    }
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.Reset_RY();
                                            checkTest.com.MixSetProVoltValue(0);
                                            checkTest.com.MixProVoltEnable(0x00);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadProVoltage(testinfo.DeviceType, testinfo.BoxNum, "编程电压点检", Convert.ToInt32(SMCItems[num]), testinfo.JudgeRegulation, testinfo.TestAcc.PrgVolChkAcc, CutRange, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "mV点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.Reset_RY();
                                        checkTest.com.MixSetProVoltValue(0);
                                        checkTest.com.MixProVoltEnable(0x00);
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 静态电流_200uA&静态电流_2000uA
                        else if (SplitItems[1] == "静态电流_200uA" || SplitItems[1] == "静态电流_2000uA") //
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] SMCItems;
                            bool CutRange = false;
                            double StcCurAcc = 0.0f;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (SplitItems[1] == "静态电流_200uA")
                                    StcCurAcc = testinfo.TestAcc.StCur_200uAChkAcc;
                                else
                                    StcCurAcc = testinfo.TestAcc.StCur_2000uAChkAcc;
                                if (!checkTest.com.MixStCurCalibration())
                                    return;
                                if (!checkTest.com.MixCellVoltCalibration())
                                    return;
                                if (!checkTest.com.StCurCal_RY("200uA", true))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:静态电流测试-----------" + SMCItems[num] + "uA------------");
                                    if (num > 0)
                                    {
                                        if (SMCItems[num] == SMCItems[num - 1])
                                        {
                                            CutRange = true;
                                        }
                                        else
                                            CutRange = false;
                                    }
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;

                                        if (blStop)
                                        {
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0x00);
                                            checkTest.com.MixStCurEnable();
                                            checkTest.com.Reset_RY();
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");

                                        checkTest.ReadStaticCurrent(testinfo.DeviceType, testinfo.BoxNum, SplitItems[1], Convert.ToInt32(SMCItems[num]), StcCurAcc, CutRange, out TestStatus[num], Report);

                                        ShowLog("循环" + (loop + 1) + "设备测试结束");

                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "uA点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0x00);
                                        checkTest.com.MixStCurEnable();
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 静态电流_20000nA
                        else if (SplitItems[1] == "静态电流_20000nA") //
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] SMCItems;
                            bool CutRange = false;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixStCurCalibration())
                                    return;
                                if (!checkTest.com.MixCellVoltCalibration())
                                    return;
                                if (!checkTest.com.StCurCal_RY("20000nA", true))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:静态电流测试-----------" + SMCItems[num] + "nA------------");
                                    if (num > 0)
                                    {
                                        if (SMCItems[num] == SMCItems[num - 1])
                                        {
                                            CutRange = true;
                                        }
                                        else
                                            CutRange = false;
                                    }
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;

                                        if (blStop)
                                        {
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0x00);
                                            checkTest.com.MixStCurEnable();
                                            checkTest.com.Reset_RY();
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");

                                        checkTest.ReadStaticCurrent(testinfo.DeviceType, testinfo.BoxNum, SplitItems[1], Convert.ToInt32(SMCItems[num]), testinfo.TestAcc.StCur_20000nAChkAcc, CutRange, out TestStatus[num], Report);

                                        ShowLog("循环" + (loop + 1) + "设备测试结束");

                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "nA点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0x00);
                                        checkTest.com.MixStCurEnable();
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 静态电流_2000nA
                        else if (SplitItems[1] == "静态电流_2000nA") //
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] SMCItems;
                            bool CutRange = false;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixStCurCalibration())
                                    return;
                                if (!checkTest.com.MixCellVoltCalibration())
                                    return;
                                if (!checkTest.com.StCurCal_RY("2000nA", true))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:静态电流测试-----------" + SMCItems[num] + "nA------------");
                                    if (num > 0)
                                    {
                                        if (SMCItems[num] == SMCItems[num - 1])
                                        {
                                            CutRange = true;
                                        }
                                        else
                                            CutRange = false;
                                    }

                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0x00);
                                            checkTest.com.MixStCurEnable();
                                            checkTest.com.Reset_RY();
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");

                                        checkTest.ReadStaticCurrent(testinfo.DeviceType, testinfo.BoxNum, SplitItems[1], Convert.ToInt32(SMCItems[num]), testinfo.TestAcc.StCur_2000nAChkAcc, CutRange, out TestStatus[num], Report);

                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "nA点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0x00);
                                        checkTest.com.MixStCurEnable();
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region CNT静态(正)_nA
                        else if (SplitItems[1] == "CNT静态(正)_nA") //
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] SMCItems;
                            double CellVolDMMValue = 0.0f;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixChgVolCalibration())
                                    return;
                                if (!checkTest.com.MixCNTCalibration())
                                    return;
                                if (!checkTest.com.CNTstCurCal_RY("正", true))
                                    return;
                                //ShowLog("仪器复位...");
                                //if (!checkTest.com.RSTMeter(strConnType))
                                //    return;
                                //ShowLog("仪器复位完成");
                                //if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", "AUTO"))
                                //    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:CNT(正)电流测试-----------" + SMCItems[num] + "nA------------");


                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        checkTest.Checkdata.ID = checkTest.Checkdata.MaxProName() + 1;

                                        if (blStop)
                                        {
                                            checkTest.com.MixCHGEnable();
                                            checkTest.com.MixCNTEnable();
                                            checkTest.com.Reset_RY();
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");

                                        checkTest.ReadCNT(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(SMCItems[num]), SplitItems[1], testinfo.TestAcc.CNTCurPChkAcc, CellVolDMMValue, out TestStatus[num], Report);

                                        ShowLog("循环" + (loop + 1) + "设备测试结束");

                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "nA点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }

                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.MixCHGEnable();
                                        checkTest.com.MixCNTEnable();
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region CNT静态(负)_nA
                        else if (SplitItems[1] == "CNT静态(负)_nA") //
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] SMCItems;
                            double CellVolDMMValue = 0.0f;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixCellVoltCalibration())
                                    return;
                                if (!checkTest.com.MixChgVolCalibration())
                                    return;
                                if (!checkTest.com.MixCNTCalibration())
                                    return;
                                if (!checkTest.com.MixSetCellVoltValue(1000))
                                    return;
                                if (!checkTest.com.CNTstCurCal_RY("负", true))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:CNT(负)电流-----------" + SMCItems[num] + "nA------------");


                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.MixCNTEnable();
                                            checkTest.com.MixCHGEnable();
                                            checkTest.com.MixEnable(0x00);
                                            checkTest.com.MixSetVoltValue(0);
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0);
                                            checkTest.com.Reset_RY();
                                            return;
                                        }

                                        ShowLog("循环" + (loop + 1) + "设备测试中...");

                                        checkTest.ReadCNT(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(SMCItems[num]), SplitItems[1], testinfo.TestAcc.CNTCurNChkAcc, CellVolDMMValue, out TestStatus[num], Report);

                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "nA点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.MixCNTEnable();
                                        checkTest.com.MixCHGEnable();
                                        checkTest.com.MixEnable(0x00);
                                        checkTest.com.MixSetVoltValue(0);
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0);
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }


                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 开路电压_mV
                        else if (SplitItems[1] == "开路电压_mV")
                        {
                            string[] SMCItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixLoadPartDisEn())
                                    return;
                                if (!checkTest.com.MixCellVoltCalibration())
                                    return;
                                if (testinfo.DeviceType != "BAT525C")
                                {
                                    if (!checkTest.com.MixOCVCalibration())
                                        return;
                                }
                                if (!checkTest.com.OCVorLVVolCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                if (!checkTest.com.CellVolCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", "AUTO"))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:电压测试-----------" + SMCItems[num] + "mV------------");
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.Reset_RY();
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0x00);
                                            checkTest.com.MixOCVEnable(0x00);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadPinVoltage(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(SMCItems[num]), "OCV", testinfo.JudgeRegulation, testinfo.TestAcc.OcvVolChkAcc, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "mV点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.Reset_RY();
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0x00);
                                        checkTest.com.MixOCVEnable(0x00);
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region Cell电压_mV
                        else if (SplitItems[1] == "Cell电压_mV")
                        {
                            string[] SMCItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixCellVoltCalibration())
                                    return;
                                if (!checkTest.com.CellVolCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", "AUTO"))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:电压测试-----------" + SMCItems[num] + "mV------------");
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.Reset_RY();
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0x00);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadCellVoltage(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(SMCItems[num]), testinfo.JudgeRegulation, testinfo.TestAcc.CellVolChkAcc, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "mV点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0x00);
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }


                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }

                        }
                        #endregion
                        #region Cell电流_mA
                        else if (SplitItems[1] == "Cell电流_mA")
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] CurItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                CurItems = SplitItems[2].Split('|');
                            }
                            else
                                CurItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[CurItems.Count()];
                            if (!HasValueGreaterThanThreshold(CurItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixCellCurCalibration())
                                    return;
                                if (!checkTest.com.CellCurCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                for (int num = 0; num < CurItems.Length; num++)
                                {
                                    ShowLog("step：串电流测试********" + CurItems[num] + "mA********");
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0);
                                            checkTest.com.MixSetCellCurValue(0);
                                            checkTest.com.Reset_RY();
                                            checkTest.com.RSTMeter(strConnType);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadCellCurrent(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(CurItems[num]), testinfo.JudgeRegulation, testinfo.TestAcc.CellCurChkAcc, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");

                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = CurItems[num] + "mV点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == CurItems.Length - 1)
                                    {
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0);
                                        checkTest.com.MixSetCellCurValue(0);
                                        checkTest.com.Reset_RY();
                                        checkTest.com.RSTMeter(strConnType);
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region LV电压_mV
                        else if (SplitItems[1] == "LV电压_mV")
                        {
                            string[] SMCItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixLoadVoltCalibration())
                                    return;
                                if (!checkTest.com.OCVorLVVolCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", "AUTO"))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:电压测试-----------" + SMCItems[num] + "mV------------");
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.MixLoadPartEnable(0x00);
                                            checkTest.com.MixLoadPartDisEn();
                                            checkTest.com.MixSetLoadPartCurValue(0);
                                            checkTest.com.Reset_RY();
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadLVVoltage(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(SMCItems[num]), testinfo.JudgeRegulation, testinfo.TestAcc.LoadPartVolChkAcc, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "mV点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.MixLoadPartEnable(0x00);
                                        checkTest.com.MixLoadPartDisEn();
                                        checkTest.com.MixSetLoadPartCurValue(0);
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region LV放电电流_mA
                        else if (SplitItems[1] == "LV放电电流_mA")
                        {
                            //DiffResult = false;
                            Msg = "";
                            string[] CurItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                CurItems = SplitItems[2].Split('|');
                            }
                            else
                                CurItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[CurItems.Count()];
                            if (!HasValueGreaterThanThreshold(CurItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "CURR", "3"))
                                    return;
                                if (!checkTest.com.MixChgVolCalibration())
                                    return;
                                if (!checkTest.com.MixSendChgVolt(0x1C, 0x96))
                                    return;
                                if (!checkTest.com.MixEnable(0x00))
                                    return;
                                if (!checkTest.com.MixLoadCurCalibration())
                                    return;
                                if (!checkTest.com.MixSendLoadPartVolt(0x00, 0xBF))
                                    return;
                                if (!checkTest.com.MixLoadPartEnable(0x00))
                                    return;
                                if (!checkTest.com.LVDsgCurCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                for (int num = 0; num < CurItems.Length; num++)
                                {
                                    ShowLog("step：LoadPart电流测试********" + CurItems[num] + "mA********");
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.Reset_RY();
                                            checkTest.com.MixLoadPartEnable(0x00);
                                            checkTest.com.MixSendLoadPartVolt(0x00, 0x00);
                                            checkTest.com.RSTMeter(strConnType);
                                            checkTest.com.MixSendChgVolt(0x00, 0x00);
                                            checkTest.com.MixEnable(0x00);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadLVCurrent(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(CurItems[num]), testinfo.JudgeRegulation, testinfo.TestAcc.LoadPartCurChkAcc, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");


                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = CurItems[num] + "mA点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}

                                    }
                                    if (num == CurItems.Length - 1)
                                    {
                                        checkTest.com.Reset_RY();
                                        checkTest.com.MixLoadPartEnable(0x00);
                                        checkTest.com.MixSendLoadPartVolt(0x00, 0x00);
                                        checkTest.com.RSTMeter(strConnType);
                                        checkTest.com.MixSendChgVolt(0x00, 0x00);
                                        checkTest.com.MixEnable(0x00);


                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region BAT525C/D静态电流1000uA
                        else if (SplitItems[1] == "静态电流_1000uA" || SplitItems[1] == "静态电流D_2000uA")
                        {
                            Msg = "";
                            string[] SMCItems;
                            bool CutRange = false;
                            double StcCurAcc = 0.0f;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (SplitItems[1] == "静态电流_1000uA")
                                    StcCurAcc = testinfo.TestAcc.StCur_1000uAChkAcc;
                                else
                                    StcCurAcc = testinfo.TestAcc.StCur_2000uAChkAcc;
                                if (!checkTest.com.MixCellVoltCalibration())
                                    return;
                                if (!checkTest.com.MixStCurCalibration())
                                    return;
                                if (!checkTest.com.StCurCal_RY("1000uA", true))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:静态电流测试-----------" + SMCItems[num] + "uA------------");
                                    if (num > 0)
                                    {
                                        if (SMCItems[num] == SMCItems[num - 1])
                                        {
                                            CutRange = true;
                                        }
                                        else
                                            CutRange = false;
                                    }
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;

                                        if (blStop)
                                        {
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0x00);
                                            checkTest.com.MixStCurEnable();
                                            checkTest.com.Reset_RY();
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");

                                        checkTest.ReadStaticCurrent(testinfo.DeviceType, testinfo.BoxNum, SplitItems[1], Convert.ToInt32(SMCItems[num]), testinfo.CurAcc, CutRange, out TestStatus[num], Report);

                                        ShowLog("循环" + (loop + 1) + "设备测试结束");

                                        ShowProgress(pb);
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0x00);
                                        checkTest.com.MixStCurEnable();
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 静态电流1000nA
                        else if (SplitItems[1] == "静态电流_1000nA")
                        {
                            Msg = "";
                            string[] SMCItems;
                            bool CutRange = false;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixStCurCalibration_nA())
                                    return;
                                if (!checkTest.com.MixCellVoltCalibration())
                                    return;
                                if (!checkTest.com.StCurCal_RY("1000nA", true))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:静态电流测试-----------" + SMCItems[num] + "nA------------");
                                    if (num > 0)
                                    {
                                        if (SMCItems[num] == SMCItems[num - 1])
                                        {
                                            CutRange = true;
                                        }
                                        else
                                            CutRange = false;
                                    }
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;

                                        if (blStop)
                                        {
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0x00);
                                            checkTest.com.MixStCurEnable();
                                            checkTest.com.Reset_RY();
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");

                                        checkTest.ReadStaticCurrent(testinfo.DeviceType, testinfo.BoxNum, SplitItems[1], Convert.ToInt32(SMCItems[num]), testinfo.TestAcc.StCur_1000nAChkAcc, CutRange, out TestStatus[num], Report);

                                        ShowLog("循环" + (loop + 1) + "设备测试结束");

                                        ShowProgress(pb);
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0x00);
                                        checkTest.com.MixStCurEnable();
                                        checkTest.com.Reset_RY();
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region 端口电压
                        else if (SplitItems[1] == "端口电压_mV")
                        {
                            string[] SMCItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                SMCItems = SplitItems[2].Split('|');
                            }
                            else
                                SMCItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[SMCItems.Count()];
                            if (!HasValueGreaterThanThreshold(SMCItems, nameToValueMap[SplitItems[1]]))
                            {
                                if (!checkTest.com.MixCellVoltCalibration())
                                    return;
                                if (!checkTest.com.PortVolCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                if (!checkTest.com.CellVolCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "VOLT", "AUTO"))
                                    return;
                                for (int num = 0; num < SMCItems.Length; num++)
                                {
                                    ShowLog("step:电压测试-----------" + SMCItems[num] + "mV------------");
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.Reset_RY();
                                            checkTest.com.MixCellEnable(0x00);
                                            checkTest.com.MixSetCellVoltValue(0x00);
                                            checkTest.com.MixOCVEnable(0x00);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadPinVoltage(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(SMCItems[num]), "SDA", testinfo.JudgeRegulation, testinfo.TestAcc.PortVolt_ChkAcc, out TestStatus[num], Report);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");
                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = SMCItems[num] + "mV点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}
                                    }
                                    if (num == SMCItems.Length - 1)
                                    {
                                        checkTest.com.Reset_RY();
                                        checkTest.com.MixCellEnable(0x00);
                                        checkTest.com.MixSetCellVoltValue(0x00);
                                        checkTest.com.MixOCVEnable(0x00);
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                        #region LV充电电流
                        else if (SplitItems[1] == "LV充电电流_mA")
                        {
                            Msg = "";
                            string[] CurItems;
                            if (SplitItems[2].Contains('|'))
                            {
                                CurItems = SplitItems[2].Split('|');
                            }
                            else
                                CurItems = new string[1] { SplitItems[2] };
                            bool[] TestStatus = new bool[CurItems.Count()];
                            if (!HasValueGreaterThanThreshold(CurItems, nameToValueMap[SplitItems[1]]))
                            {
                                //if (!checkTest.com.ConfMeter(frmVerifyDevice.strConnType, "CURR", "3"))
                                //    return;
                                //if (!checkTest.com.MixChgVolCalibration())
                                //    return;
                                //if (!checkTest.com.MixSendChgVolt(0x1C, 0x96))
                                //    return;
                                //if (!checkTest.com.MixEnable(0x00))
                                //    return;
                                if (!checkTest.com.MixLoadCurCalibration())
                                    return;
                                //if (!checkTest.com.MixSendLoadPartVolt(0x00, 0xBF))
                                //    return;
                                if (!checkTest.com.MixLoadPartEnable(0x00))
                                    return;
                                if (!checkTest.com.LVChgCurCal_RY(true))
                                {
                                    ShowLog("继电器开启切换失败!");
                                    return;
                                }
                                for (int num = 0; num < CurItems.Length; num++)
                                {
                                    ShowLog("step：LoadPart电流测试********" + CurItems[num] + "mA********");
                                    for (int loop = 0; loop < Convert.ToInt16(SplitItems[3]); loop++)
                                    {
                                        pb++;
                                        if (blStop)
                                        {
                                            checkTest.com.Reset_RY();
                                            checkTest.com.MixLoadPartEnable(0x00);
                                            checkTest.com.MixSendLoadPartVolt(0x00, 0x00);
                                            checkTest.com.RSTMeter(strConnType);
                                            //checkTest.com.MixSendChgVolt(0x00, 0x00);
                                            //checkTest.com.MixEnable(0x00);
                                            return;
                                        }
                                        ShowLog("循环" + (loop + 1) + "设备测试中...");
                                        checkTest.ReadLVChgCurrent(testinfo.DeviceType, testinfo.BoxNum, Convert.ToInt32(CurItems[num]), testinfo.JudgeRegulation, testinfo.TestAcc.LoadPartCurChkAcc, out TestStatus[num]);
                                        ShowLog("循环" + (loop + 1) + "设备测试结束");


                                        ShowProgress(pb);
                                        //if (TestStatus[num])
                                        //{
                                        //    VerifyStatus = CurItems[num] + "mA点检_NG";
                                        //    UpdateListBox(VerifyStatus);
                                        //}

                                    }
                                    if (num == CurItems.Length - 1)
                                    {
                                        checkTest.com.Reset_RY();
                                        checkTest.com.MixLoadPartEnable(0x00);
                                        checkTest.com.MixSendLoadPartVolt(0x00, 0x00);
                                        checkTest.com.RSTMeter(strConnType);
                                        if (Array.Exists(TestStatus, element => element))
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_NG";
                                            UpdateListBox(VerifyStatus);
                                            listResult.Add(VerifyStatus);
                                        }
                                        else
                                        {
                                            VerifyStatus = SplitItems[1] + "点检_OK";
                                            UpdateListBox(VerifyStatus);
                                        }
                                        if (strEQMType == "BAT525G" || strEQMType == "BAT525H")
                                        {
                                            checkTest.com.MixEQMInit(out Msg);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SplitItems[1] + "点检值填写超出限制范围:" + nameToValueMap[SplitItems[1]]);
                                return;
                            }
                        }
                        #endregion
                    }

                    // ShowLog("测试结束~~~~~~~~~~~~~~~~");


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
            finally
            {
                checkTest.com.Reset_RY();
                checkTest.com.DisConnect();
                checkTest.com.CloseDMMConn();
                if (listResult.Count == 0)
                {
                    TestResult = true;
                }
                if (testinfo.check && Report == false)
                {
                    if (!SaveTestData(dgvTestResult))
                        MessageBox.Show("数据保存失败!");
                }
                else
                {
                    SaveCheckReport(strSNCode, TestResult);
                }
                MessageBox.Show("测试结束!");
                StopTestProgress(TestResult, listResult);

            }


        }


    }
}
