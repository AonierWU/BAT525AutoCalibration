using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Collections;
using NPOI.SS.Formula.Functions;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Runtime.Versioning;

namespace TestSystem_Pack
{

    public partial class frmVerifyDevice : Form
    {
        public frmVerifyDevice()
        {
            InitializeComponent();
        }
        private CHIni config = new CHIni();
        private CHIni Accconfig = new CHIni();
        private static bool blStop = false;
        private string[] TestItems;
        private List<string> CheckItems = new List<string> { };
        int num = 0;
        int numRough = 0;
        public static string Line;
        public static string TestID;
        public static string RelayCh;
        public static string strCHComNum;
        public static string strRelayComNum;
        public static bool blUseRelay;
        public static string strDeviceType;
        public static string strMeterAddr;
        public static string strConnType;
        public static string TesterType;
        public static string strCalEQMType;
        public static string strDMMcom;
        public static string strMultimeterType;
        public static string strMultimeterAddr;
        public static string strMultimeterConType;
        public static string strDCsourceType;
        public static string strDCsourceAddr;
        public static string strDCsourceConType;
        public static string strDCch;
        public static string strSelectType;
        public static int intCount;
        public static int intRoughCount;
        public static string strConfigPath;
        public static string[] strAllLines;
        public static bool connMultimeter;
        public static bool connDCsource;
        public static bool AloneSave;
        public static bool SaveLog;
        public static bool stcCurReadVol;
        public static string InitialValue;
        public static string EndValue;
        public Thread AutoCaliOrCheck = null;
        private string strLoadParamPath = "";
        public static string CheckReportPath = "";
        public static XSSFWorkbook xssfWorkbook;
        public static XSSFSheet xssfSheet;
        public static XSSFRow xssfRow;
        public static XSSFCell xssfCell;
        public static XSSFRow xssfRow2;
        public static XSSFCell xssfCell2;
        public static XSSFRow xssfRow3;
        public static XSSFCell xssfCell3;
        public IFont font;
        CheckItem checkTest = null;
        TestInfo testinfo = new TestInfo();
        Dictionary<string, double> CaliACC = new Dictionary<string, double> { };
        Dictionary<string, double> CheckACC = new Dictionary<string, double> { };
        // public static string strLogPath = Application.StartupPath + @"\LogFile";
        public static Logger InfoLog = new Logger("APP_log");
        private string strParamPath = Application.StartupPath + @"\Param.csv";
        const string SoftWareVer = "BAT525系列设备自动校准  Ver1.0 [20241008]";
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // 发送消息
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public bool AutoClose = false;
        // 关闭消息
        private const uint WM_CLOSE = 0x0010;
        /*v1.2 20220719
         * 1.增加精度范围比对;
         * 
         * v1.1 20210926 
         * 1.增加采样电阻测试电流方法Curr(10mΩ)，Curr(100mΩ);
         * 2.增加BAT548;
         * 3.设定点可以集合在一个单元格，使用‘|’分隔；
         * 
        */
        private void frmVerifyDevice_Load(object sender, EventArgs e)
        {
            config.FilePath_ = Application.StartupPath + "\\Config.ini";
            //获取电脑硬件信息
            LoadConfig();
            if (chkSaveLog.Checked)
            {
                HardInfoClass HD = new HardInfoClass();
                InfoLog.Log("*Software Version:" + SoftWareVer, LogLevel.Info);
                InfoLog.Log("*Host Name:" + HD.GetHostName(), LogLevel.Info);
                InfoLog.Log("*OS Name:" + HD.GetOSName(), LogLevel.Info);
                InfoLog.Log("*Is64BitOS:" + HD.GetOSIs64bit(), LogLevel.Info);
                InfoLog.Log("*Cpu Name:" + HD.GetCpuName(), LogLevel.Info);
                InfoLog.Log("*Memory:" + HD.GetMemory(), LogLevel.Info);
                InfoLog.Log("*Drives:" + HD.GetDirver(), LogLevel.Info);
                InfoLog.Log("************************************", LogLevel.Info);
            }
            this.Text = SoftWareVer;
            InitPorts();
            //LoadTestItem();
            HideTabPage();
            checkTest = new CheckItem(UpdateUi, UpdateListBox, WriteDataToReport);
            intCount = 0;
            intRoughCount = 0;
            tabControl1.TabPages.RemoveAt(2);
            btnImportParam.Enabled = false;
            picTestTatus.SizeMode = PictureBoxSizeMode.StretchImage;
            picTestTatus.Image = Image.FromFile(Application.StartupPath + "\\PictureFile\\待测试.png");
        }
        private void InitPorts()
        {
            try
            {
                string[] strPorts = System.IO.Ports.SerialPort.GetPortNames();
                foreach (var name in strPorts)
                {
                    cmbCHPort.Items.Add(name);
                    //cboRelayPort.Items.Add(name);
                    cboEqmCalSerialPort.Items.Add(name);
                }
            }
            catch (Exception)
            {

            }

        }
        private void HideTabPage()
        {
            try
            {
                chkCalibration.Enabled = false;
                chkAll.Enabled = false;
                chkCheck.Enabled = false;
                chkIDRcheck.Enabled = false;
                chkDCIRcheck.Enabled = false;
                int tabpageCount = tabCalibrationSelect.TabPages.Count;
                for (int i = tabpageCount; i == tabCalibrationSelect.TabPages.Count; i--)
                {
                    if (i > 1)
                        tabCalibrationSelect.TabPages.RemoveAt(i - 1);
                    else
                    {
                        tabCalibrationSelect.SelectedIndex = 0;
                        break;
                    }

                }
            }
            catch (Exception) { }
        }
        private void SaveConfig()
        {

            //config.CHWriteIni("config", "Line", txtLine.Text);
            //config.CHWriteIni("config", "TestID", txtTestID.Text);
            //config.CHWriteIni("config", "Channel", cboChannel.Text);
            //config.CHWriteIni("config", "TesterType", cboTesterType.Text);
            //config.CHWriteIni("config", "DeviceType", cboDeviceType.Text);
            //config.CHWriteIni("config", "ConnectType", cboConnectType.Text);

            //config.CHWriteIni("config", "CHport", cmbCHPort.Text);
            //config.CHWriteIni("config", "RelayPort", cboRelayPort.Text);
            //config.CHWriteIni("config", "UseRelay", chkUseRelay.Checked.ToString());

            //config.CHWriteIni("config", "MeterAddr", txtMeterAddr.Text);

            //config.CHWriteIni("config", "BoardID", numericUpDownBoardID.Text);
            //config.CHWriteIni("config", "PrimaryAddr", numericUpDownPrimaryAddr.Text);
            //config.CHWriteIni("config", "secondaryAddress", cmbsecondaryAddress.Text);
            //基本信息
            //config.CHWriteIni("config", "Line", txtLines.Text);
            //config.CHWriteIni("config", "TesterType", cboCalEQMType.Text);
            //config.CHWriteIni("config", "CHport", cboEqmCalSerialPort.Text);
            //config.CHWriteIni("config", "RelayPort", cboToolingSerialPort.Text);
            //config.CHWriteIni("config", "ResSpecification", cboResSpecification.Text);
            config.CHWriteIni("config", "SaveErrrRule", cboSaveErrrRule.Text);
            //万用表信息 
            config.CHWriteIni("config", "MultimeterType", cboMultimeterType.Text);
            config.CHWriteIni("config", "MultimeterConType", cboMultimeterConType.Text);
            config.CHWriteIni("config", "MultimeterAddress", txtMultimeterAddress.Text);
            //DC电源
            //config.CHWriteIni("config", "DCsourceType", cboDCsourceType.Text);
            //config.CHWriteIni("config", "DCsourceConType", cboDCsourceConType.Text);
            //config.CHWriteIni("config", "DCCH", cboDCch.Text);
            //config.CHWriteIni("config", "DCsourceAddress", txtDCsourceAddress.Text);
            config.CHWriteIni("config", "chkSaveLog", Convert.ToString(chkSaveLog.Checked));
            //config.CHWriteIni("config", "chkReadStcCurVol", Convert.ToString(chkstcCur_ReadVol.Checked));


        }
        private void LoadConfig()
        {
            string info = "";
            //config.CHReadIni("config", "Line", out info);
            //txtLines.Text = info;

            //config.CHReadIni("config", "ResSpecification", out info);
            //cboResSpecification.Text = info;

            config.CHReadIni("config", "SaveErrrRule", out info);
            cboSaveErrrRule.Text = info;

            config.CHReadIni("config", "MultimeterType", out info);
            cboMultimeterType.Text = info;

            config.CHReadIni("config", "MultimeterConType", out info);
            cboMultimeterConType.Text = info;

            config.CHReadIni("config", "MultimeterAddress", out info);
            txtMultimeterAddress.Text = info;

            //config.CHReadIni("config", "DCsourceType", out info);
            //cboDCsourceType.Text = info;
            //config.CHReadIni("config", "DCsourceConType", out info);
            //cboDCsourceConType.Text = info;

            //config.CHReadIni("config", "DCCH", out info);
            //cboDCch.Text = info;

            //config.CHReadIni("config", "DCsourceAddress", out info);
            //txtDCsourceAddress.Text = info;

            config.CHReadIni("config", "chkSaveLog", out info);
            chkSaveLog.Checked = Convert.ToBoolean(info);

            //config.CHReadIni("config", "chkReadStcCurVol", out info);
            //chkstcCur_ReadVol.Checked = Convert.ToBoolean(info);
        }
        private void frmVerifyDevice_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveTestItem();
            SaveConfig();
            InfoLog.CloseLog();
        }
        private void SaveTestItem()
        {

            if (strLoadParamPath != "")
            {
                if (dgvChkParameter.Rows.Count > 1)
                {
                    if (File.Exists(strLoadParamPath))
                    {
                        File.Delete(strLoadParamPath);
                    }
                    string info = "";
                    for (int Row = 0; Row < dgvChkParameter.Rows.Count - 1; Row++)
                    {
                        info = "";
                        for (int Col = 0; Col < dgvChkParameter.ColumnCount; Col++)
                        {

                            if (dgvChkParameter.Rows[Row].Cells[Col].Value == null)
                            {
                                if (Col == dgvChkParameter.ColumnCount - 1)
                                    info += "False";
                                else
                                    info += "" + ",";
                            }
                            else
                                info += dgvChkParameter.Rows[Row].Cells[Col].Value.ToString() + ",";

                        }
                        if (File.Exists(strLoadParamPath))
                            ExportDataToCSVData(strLoadParamPath, info.TrimEnd(','));
                        else
                        {
                            ExportDataToCSVHead(strLoadParamPath, "No,Items,Point,Num,Sel");
                            ExportDataToCSVData(strLoadParamPath, info.TrimEnd(','));
                        }
                    }
                }
                else return;
            }
            else
            {
                if (cboCalEQMType.Text != "")
                {
                    if (dgvChkParameter.Rows.Count > 1)
                    {
                        strLoadParamPath = Application.StartupPath + "\\点检参数配置\\" + cboCalEQMType.Text + ".txt";
                        if (File.Exists(strLoadParamPath))
                        {
                            File.Delete(strLoadParamPath);
                        }
                        string info = "";
                        for (int Row = 0; Row < dgvChkParameter.Rows.Count - 1; Row++)
                        {
                            info = "";
                            for (int Col = 0; Col < dgvChkParameter.ColumnCount; Col++)
                            {

                                if (dgvChkParameter.Rows[Row].Cells[Col].Value == null)
                                {
                                    if (Col == dgvChkParameter.ColumnCount - 1)
                                        info += "False";
                                    else
                                        info += "" + ",";
                                }
                                else
                                    info += dgvChkParameter.Rows[Row].Cells[Col].Value.ToString() + ",";

                            }
                            if (File.Exists(strLoadParamPath))
                                ExportDataToCSVData(strLoadParamPath, info.TrimEnd(','));
                            else
                            {
                                ExportDataToCSVHead(strLoadParamPath, "No,Items,Point,Num,Sel");
                                ExportDataToCSVData(strLoadParamPath, info.TrimEnd(','));
                            }
                        }
                    }
                    else
                        return;
                }
                else return;
            }

        }
        private bool LoadTestItem()
        {
            string[] AllLines;

            if (!File.Exists(strLoadParamPath))
                return false;
            else
            {
                dgvChkParameter.Rows.Clear();
                // Type.Items.Clear();
                AllLines = File.ReadAllLines(strLoadParamPath);
                for (int i = 1; i < AllLines.Length; i++)
                {
                    if (AllLines[i] == null || AllLines[i] == "")
                    {
                        continue;
                    }
                    else
                    {
                        string[] Items = AllLines[i].Split(',');
                        if (Items.Length < 5)
                        {
                            continue;
                        }
                        else
                        {
                            dgvChkParameter.Rows.Add(Items);
                        }
                    }
                }
                rowNumber = AllLines.Count();
                return true;
            }
        }
        public void ReadTestItems()
        {
            CheckItems.Clear();
            TestItems = new string[dgvChkParameter.Rows.Count - 1];
            bool[] judgeConn = new bool[TestItems.Length];
            for (int Row = 0; Row < dgvChkParameter.Rows.Count - 1; Row++)
            {
                TestItems[Row] = "";
                for (int Col = 0; Col < dgvChkParameter.ColumnCount; Col++)
                {
                    if (dgvChkParameter.Rows[Row].Cells[4].Value == null)
                        dgvChkParameter.Rows[Row].Cells[4].Value = false;
                    if (dgvChkParameter.Rows[Row].Cells[3].Value == null || dgvChkParameter.Rows[Row].Cells[3].Value.ToString() == "")
                        dgvChkParameter.Rows[Row].Cells[3].Value = 1;
                    TestItems[Row] += dgvChkParameter.Rows[Row].Cells[Col].Value.ToString() + ",";
                }
                TestItems[Row] = TestItems[Row].TrimEnd(',');
            }
            int process = 0;
            for (int n = 0; n < TestItems.Length; n++)
            {
                string[] Itemssplit = TestItems[n].Split(',');
                if (Itemssplit[4].ToUpper() == "TRUE")
                {
                    CheckItems.Add(TestItems[n]);
                    switch (cboCalEQMType.Text)
                    {
                        case "BAT525G":
                            if (Itemssplit[1] == "充电电压_mV" || Itemssplit[1] == "开路电压_mV" || Itemssplit[1] == "编程电压_mV" || Itemssplit[1] == "Cell电压_mV" ||
                            Itemssplit[1] == "Cell电流_mA" || Itemssplit[1] == "放电电流3A_mA" || Itemssplit[1] == "放电电流30A_mA" || Itemssplit[1] == "直流内阻_mΩ" ||
                            Itemssplit[1] == "LV电压_mV" || Itemssplit[1] == "LV放电电流_mA")
                                judgeConn[n] = true;
                            else judgeConn[n] = false;
                            break;
                        case "BAT525H":
                            if (Itemssplit[1] == "充电电压_mV" || Itemssplit[1] == "开路电压_mV" || Itemssplit[1] == "编程电压_mV" || Itemssplit[1] == "Cell电压_mV" ||
                            Itemssplit[1] == "Cell电流_mA" || Itemssplit[1] == "放电电流3A_mA" || Itemssplit[1] == "放电电流30A_mA" || Itemssplit[1] == "直流内阻_mΩ" ||
                            Itemssplit[1] == "LV电压_mV" || Itemssplit[1] == "LV放电电流_mA")
                                judgeConn[n] = true;
                            else judgeConn[n] = false;
                            break;
                        case "BAT525C":
                            if (Itemssplit[1] == "充电电压_mV" || Itemssplit[1] == "开路电压_mV" || Itemssplit[1] == "编程电压_mV" || Itemssplit[1] == "Cell电压_mV" || Itemssplit[1] == "放电电流30A_mA" ||
                       Itemssplit[1] == "Cell电流_mA" || Itemssplit[1] == "放电电流3A_mA" || Itemssplit[1] == "直流内阻_mΩ" || Itemssplit[1] == "LV电压_mV" || Itemssplit[1] == "LV充电电流_mA"
                                || Itemssplit[1] == "端口电压_mV")
                                judgeConn[n] = true;
                            else judgeConn[n] = false;
                            break;
                        case "BAT525D":
                            if (Itemssplit[1] == "充电电压_mV" || Itemssplit[1] == "开路电压_mV" || Itemssplit[1] == "编程电压_mV" || Itemssplit[1] == "Cell电压_mV" || Itemssplit[1] == "放电电流30A_mA" ||
                       Itemssplit[1] == "Cell电流_mA" || Itemssplit[1] == "放电电流3A_mA" || Itemssplit[1] == "直流内阻_mΩ" || Itemssplit[1] == "LV电压_mV" || Itemssplit[1] == "LV充电电流_mA"
                                || Itemssplit[1] == "端口电压_mV")
                                judgeConn[n] = true;
                            else judgeConn[n] = false;
                            break;
                    }
                    if (Itemssplit[2].Contains('|'))
                    {
                        string[] temp = Itemssplit[2].Split('|');
                        process += temp.Length * Convert.ToInt32(Itemssplit[3]);
                    }
                    else
                        process += Convert.ToInt32(Itemssplit[3]);
                }
                else
                    continue;
            }
            if (Array.Exists(judgeConn, element => element))
            {
                connMultimeter = true;
            }
            else
                connMultimeter = false;
            progressBar1.Maximum = process;
        }
        public static bool ExportDataToCSVHead(string OutFileName, string strHead)
        {
            string strFile = OutFileName;
            System.IO.FileStream fs = new System.IO.FileStream(strFile, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //  StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
            //   StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
            sw.Write(strHead);
            sw.Write("\r");
            sw.Flush();
            sw.Close();
            return true;

        }

        public static bool ExportDataToCSVData(string OutFileName, string strData)
        {
            string strFile = OutFileName;
            System.IO.FileStream fs = new System.IO.FileStream(strFile, System.IO.FileMode.Append, System.IO.FileAccess.Write);
            //  StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("GB2312"));
            //StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
            sw.Write(strData);
            sw.Write("\r");
            sw.Flush();
            sw.Close();
            return true;

        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            blStop = true;
            CheckItem.StopStatus = true;
            //dgvChkParameter.ReadOnly = false;
        }
        Thread testCheckThread;
        private void btnRoughCali_Click(object sender, EventArgs e)
        {
            //dgvChkParameter.ReadOnly = true;
            //ReadTestItems();
            // btnCali.Enabled = false;
            if (!JudgeTestItem())
                return;
            AloneSave = chkAloneSave.Checked;
            if (!String.IsNullOrEmpty(txtRoughNum.Text))
            {

                //Line = txtLine.Text;
                //TestID = txtTestID.Text;
                TesterType = cboTesterType.Text;
                strCHComNum = cmbCHPort.Text;//通道串口号
                testinfo.BoxNum = txtRoughType.Text + txtRoughNum.Text;
                testinfo.DeviceType = cboTesterType.Text;
                testinfo.DeviceCom = cmbCHPort.Text;
                strCalEQMType = cboTesterType.Text;
                connMultimeter = false;
                connDCsource = false;
                if (strCHComNum == "")
                {
                    MessageBox.Show("请选择串口号!");
                }
                else
                {
                    testinfo.DeviceCom = strCHComNum;
                    if (chkReadEE.Checked || chkReadAllEE.Checked)
                    {
                        testinfo.ReadEEPROM = true;
                        testinfo.Calibration = false;
                        if (txtConfigFile.Text == "")
                        {
                            strConfigPath = Application.StartupPath + "\\粗调配置文件";
                        }
                    }
                    else { testinfo.ReadEEPROM = false; testinfo.Calibration = false; }
                    if (chkRoughAllWrite.Checked || chkRoughWrite.Checked)
                    {
                        strAllLines = new string[] { };
                        strConfigPath = txtConfigFile.Text;
                        strAllLines = File.ReadAllLines(strConfigPath);
                        testinfo.Calibration = false;
                        testinfo.RoughCali = true;
                        testinfo.IDRcheck = false;
                        //if (chkWriteCheck.Checked)
                        //    testinfo.RoughCalicheck = true;
                        //else testinfo.RoughCalicheck = false;
                    }
                    else
                    {
                        testinfo.Calibration = false;
                        testinfo.RoughCali = false;
                        testinfo.IDRcheck = false;
                        //testinfo.RoughCalicheck = false;
                    }
                    if (TesterType == "BAT525G")
                    {
                        testinfo.TestMode.ChgVoltRoughCali = SBT825G_chkChgVoltRoughCal.Checked;
                        testinfo.TestMode.OCVRoughCali = SBT825G_chkOCVRoughCal.Checked;
                        testinfo.TestMode.ProVoltRoughCali = SBT825G_chkProVoltRoughCal.Checked;
                        testinfo.TestMode.DsgCurRoughCali = SBT825G_chkDsgCurRoughCal.Checked;
                        testinfo.TestMode.DsgCurRoughCali30A = SBT825G_chkDsgCurRoughCal30A.Checked;
                        testinfo.TestMode.LoadVoltRoughCali = SBT825G_chkLoadPartVoltRoughCal.Checked;
                        testinfo.TestMode.LoadCurRoughCali = SBT825G_chkLoadPartCurRoughCal.Checked;
                        testinfo.TestMode.CellVoltRoughCali = SBT825G_chkCellVoltRoughCal.Checked;
                        testinfo.TestMode.CellCurRoughCali = SBT825G_chkCellCurRoughCal.Checked;
                        testinfo.TestMode.StCurRoughCali = SBT825G_chkStCurRoughCal.Checked;
                        testinfo.TestMode.StCur2RoughCali = SBT825G_chkStCur2RoughCal.Checked;
                        testinfo.TestMode.StCur3RoughCali = SBT825G_chkStCur3RoughCal.Checked;
                        testinfo.TestMode.StCur4RoughCali = SBT825G_chkStCur4RoughCal.Checked;
                        testinfo.TestMode.NTCRoughCali = SBT825G_chkNTCRoughCal.Checked;
                        testinfo.TestMode.DCIRRoughCali = SBT825G_chkDCIRRoughCal.Checked;
                        testinfo.TestMode.CNTStPRoughCali = SBT825G_chkCNTStPositiveRoughCal.Checked;
                        testinfo.TestMode.CNTStNRoughCali = SBT825G_chkCNTStNegativeRoughCal.Checked;
                        testinfo.TestMode.WriteCheck = chkWriteCheck.Checked;

                        //testinfo.Calibration=chkCalibration.Checked;

                    }
                    if ((testinfo.TestMode.WriteCheck) && (testinfo.TestMode.CellCurRoughCali || testinfo.TestMode.CellVoltRoughCali || testinfo.TestMode.ChgVoltRoughCali || testinfo.TestMode.DsgCurRoughCali || testinfo.TestMode.DsgCurRoughCali30A ||
                        testinfo.TestMode.LoadVoltRoughCali || testinfo.TestMode.OCVRoughCali || testinfo.TestMode.ProVoltRoughCali))
                    {
                        connMultimeter = true;
                        strMultimeterType = cboMultimeterType.Text;
                        strMultimeterConType = cboMultimeterConType.Text;
                        strDMMcom = cboDMMcom.Text;
                        if (strMultimeterType == "34401A" && String.IsNullOrEmpty(strDMMcom))
                        {
                            MessageBox.Show("34401A万用表串口不能为空!");
                            return;
                        }
                        if (strMultimeterConType == "Keysight库")
                        {
                            strMultimeterAddr = txtMultimeterAddress.Text;
                        }
                    }
                    //if ((testinfo.TestMode.WriteCheck) && (testinfo.TestMode.LoadCurRoughCali || testinfo.TestMode.CNTStNRoughCali))
                    //{
                    //    connDCsource = true;
                    //    strDCsourceType = cboDCsourceType.Text;
                    //    strDCsourceConType = cboDCsourceConType.Text;
                    //    strDCch = cboDCch.Text;

                    //    if (strDCsourceConType == "Keysight库")
                    //    {
                    //        strDCsourceAddr = txtDCsourceAddress.Text;
                    //    }
                    //}
                    dgvRoughCalResult.Rows.Clear();
                    numRough = 0;
                    lstRoughCalResult.Items.Clear();
                    if (testinfo.ReadEEPROM || testinfo.RoughCali)
                    {
                        blStop = false;
                        CheckItem.StopStatus = false;
                        btnRoughCali.Enabled = false;
                        AutoCaliOrCheck = new Thread(CheckOrCalibration);  //启动的线程
                        AutoCaliOrCheck.IsBackground = true;
                        AutoCaliOrCheck.Start();
                    }
                    else
                        return;
                }
            }
            else if (chkIDRcheck.Checked)//IDR自检
            {
                try
                {
                    if (!string.IsNullOrEmpty(cmbCHPort.Text))
                    {
                        testinfo.DeviceCom = cmbCHPort.Text;
                    }
                    else
                    {
                        MessageBox.Show("串口号不能为空!");
                        return;
                    }
                    testinfo.Calibration = false;
                    testinfo.RoughCali = false;
                    testinfo.check = false;
                    testinfo.DCIRCheck = false;
                    dgvRoughCalResult.Rows.Clear();
                    lstRoughCalResult.Items.Clear();
                    numRough = 0;
                    strCalEQMType = cboTesterType.Text;
                    testinfo.IdrAcc = Convert.ToDouble(txtIDRAcc.Text);
                    testinfo.IDRcheck = true;
                    connMultimeter = true;
                    strMultimeterType = cboMultimeterType.Text;
                    strMultimeterConType = cboMultimeterConType.Text;
                    strDMMcom = cboDMMcom.Text;
                    if (strMultimeterType == "34401A" && String.IsNullOrEmpty(strDMMcom))
                    {
                        MessageBox.Show("34401A万用表串口不能为空!");
                        return;
                    }
                    if (strMultimeterConType == "Keysight库")
                    {
                        strMultimeterAddr = txtMultimeterAddress.Text;
                    }
                    blStop = false;
                    CheckItem.StopStatus = false;
                    btnRoughCali.Enabled = false;
                    AutoCaliOrCheck = new Thread(CheckOrCalibration);  //启动的线程
                    AutoCaliOrCheck.IsBackground = true;
                    AutoCaliOrCheck.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (chkDCIRcheck.Checked)
            {
                try
                {
                    if (!string.IsNullOrEmpty(cmbCHPort.Text))
                    {
                        testinfo.DeviceCom = cmbCHPort.Text;
                    }
                    else
                    {
                        MessageBox.Show("串口号不能为空!");
                        return;
                    }
                    testinfo.Calibration = false;
                    testinfo.RoughCali = false;
                    testinfo.check = false;
                    testinfo.IDRcheck = false;
                    dgvRoughCalResult.Rows.Clear();
                    lstRoughCalResult.Items.Clear();
                    numRough = 0;
                    strCalEQMType = cboTesterType.Text;
                    testinfo.DCIRAcc = Convert.ToDouble(txtDCIRacc.Text);
                    testinfo.DCIRCheck = true;
                    connMultimeter = true;
                    strMultimeterType = cboMultimeterType.Text;
                    strMultimeterConType = cboMultimeterConType.Text;
                    strDMMcom = cboDMMcom.Text;
                    if (strMultimeterType == "34401A" && String.IsNullOrEmpty(strDMMcom))
                    {
                        MessageBox.Show("34401A万用表串口不能为空!");
                        return;
                    }
                    if (strMultimeterConType == "Keysight库")
                    {
                        strMultimeterAddr = txtMultimeterAddress.Text;
                    }
                    blStop = false;
                    CheckItem.StopStatus = false;
                    btnRoughCali.Enabled = false;
                    AutoCaliOrCheck = new Thread(CheckOrCalibration);  //启动的线程
                    AutoCaliOrCheck.IsBackground = true;
                    AutoCaliOrCheck.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("TestID不能为空!");
                return;
            }

        }
        public void CheckThread()
        {

            string strCHNum = Thread.CurrentThread.Name;
            //checkTest = new CheckItem();
            try
            {
                StartTest(strCHNum);
            }
            catch (Exception)
            {

            }

        }

        private void ShowLog(string msg)
        {
            if (SaveLog)
            {
                Action<string> actionInit = new Action<string>((string str) =>
              {
                  //txtLogShow.AppendText(DateTime.Now.ToString("HH:mm:ss.fff ") + msg + "\r\n");
                  InfoLog.Log(msg, LogLevel.Info);
              });
                this.Invoke(actionInit, msg);
            }
            else return;
        }

        private delegate void ShowPBDelegate(int value);
        private delegate void StopTestDelegate(bool TestResult, List<string> listResult);

        private void ShowProgress(int intValue)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ShowPBDelegate(ShowProgress), intValue);

            }
            else
            {
                progressBar1.Value = intValue;//显示结果
            }

        }
        private void StopTestProgress(bool TestResult, List<string> listResult)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new StopTestDelegate(StopTestProgress), TestResult, listResult);
            }
            else
            {
                btnStop.PerformClick();//显示结果
                btnStopCalibration.PerformClick();
                TestOverShow(TestResult, listResult);
            }

        }
        private void btnQ_Click(object sender, EventArgs e)
        {
            ShowData();
        }
        private void ShowData()
        {
            DataSet TblTestData = new DataSet();
            DataSet TblTestData2 = new DataSet();

            CheckTestData db = new CheckTestData();

            string sql1 = "";

            sql1 = "SELECT *" +
                // " ID ,Line,Tester,Channel,Item,Tester_measurement as Tester measurement, DMM_measurement as DMM measurement,Error,CheckTime" +
                " FROM t_TestData WHERE ";

            if (txtDtLine.Text != null && txtDtLine.Text != "")
                sql1 += "Line = '" + txtDtLine.Text.TrimEnd(' ') + "' and ";

            if (txtDtID.Text != null && txtDtID.Text != "")
                sql1 += "Tester = '" + txtDtID.Text.TrimEnd(' ') + "' and ";

            if (dtpStartTime.Value != null && dtpEndTime.Value != null)
                sql1 += "CheckTime >= '" + dtpStartTime.Value.ToString("yyyy/MM/dd HH:mm:ss") + "' and ";

            if (dtpStartTime.Value != null && dtpEndTime.Value != null)
                sql1 += "CheckTime <= '" + dtpEndTime.Value.ToString("yyyy/MM/dd HH:mm:ss") + "' ORDER BY Num ";

            TblTestData = db.GetDataSet(sql1, "t_TestData");

            dgvVoltage.DataSource = TblTestData.Tables[0].DefaultView;
            dgvVoltage.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;//设置间隔行颜色

        }
        private void ClearTestSelect()
        {
            try
            {
                testinfo.TestMode.ChgVoltCalibration = false;
                testinfo.TestMode.OCVCalibration = false;
                testinfo.TestMode.ProVoltCalibration = false;
                testinfo.TestMode.DsgCurCalibration = false;
                testinfo.TestMode.DsgCurCalibration30A = false;
                testinfo.TestMode.LoadVoltCalibration = false;
                testinfo.TestMode.LoadCurCalibration = false;
                testinfo.TestMode.CellVoltCalibration = false;
                testinfo.TestMode.CellCurCalibration = false;
                testinfo.TestMode.StCurCalibration = false;
                testinfo.TestMode.StCur2Calibration = false;
                testinfo.TestMode.StCur3Calibration = false;
                testinfo.TestMode.StCur4Calibration = false;
                testinfo.TestMode.NTCCalibration = false;
                testinfo.TestMode.DCIRCalibration = false;
                testinfo.TestMode.CNTStPCalibration = false;
                testinfo.TestMode.CNTStNCalibration = false;
                testinfo.TestMode.StCuruACalibration = false;
                testinfo.TestMode.StCurnACalibration = false;
                testinfo.TestMode.IDRCalibration = false;
            }
            catch
            { return; }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.dgvVoltage.Rows.Count <= 0)
                {
                    MessageBox.Show("无数据可以导出！");
                    return;
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Excel files(*.xls)|*.xls";
                saveFile.FilterIndex = 0;
                saveFile.RestoreDirectory = true;
                // saveFile.CreatePrompt = true;
                saveFile.Title = "导出Excel文件到";

                DateTime now = DateTime.Now;
                saveFile.FileName = "点检数据" + now.ToString("yyyyMMddHHmmss");
                if (saveFile.ShowDialog() != DialogResult.OK)
                    return;
                string path = saveFile.FileName;

                DataTable tbVol = GetDgvToTable(dgvVoltage);
                // DataTable tbCur = GetDgvToTable(dgvCurr);


                //新建workbook工作簿
                HSSFWorkbook workbook = new HSSFWorkbook();

                //新建sheet工作表
                HSSFSheet sheetVolt = (HSSFSheet)workbook.CreateSheet("Tester");
                //   HSSFSheet sheetCurr = (HSSFSheet)workbook.CreateSheet("Cur");

                //设置默认列宽
                sheetVolt.DefaultColumnWidth = 15;
                //  sheetCurr.DefaultColumnWidth = 15;

                //填充Vol表头
                HSSFRow dataRow = (HSSFRow)sheetVolt.CreateRow(0);
                foreach (DataColumn column in tbVol.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                }
                //填充Cur工作表的内容
                for (int i = 0; i < tbVol.Rows.Count; i++)
                {
                    dataRow = (HSSFRow)sheetVolt.CreateRow(i + 1);
                    for (int j = 0; j < tbVol.Columns.Count; j++)
                    {
                        dataRow.CreateCell(j).SetCellValue(tbVol.Rows[i][j].ToString());
                    }
                }

                //保存文件
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fs);
                }

                MessageBox.Show("导出完成!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "导出失败");
                // throw;
            }

        }
        private void ExportData(DataGridView dgvData, string FilePath)
        {
            try
            {
                string fileType = FilePath.Substring(FilePath.LastIndexOf('.') + 1).ToLower();
                long num = dgvData.RowCount;
                string strSplit = "";
                if (fileType == "csv")
                {
                    strSplit = ",";
                }
                else
                {
                    strSplit = "\t";
                }

                // StreamWriter sw = new StreamWriter(FilePath, false, Encoding.GetEncoding("gb2312"));
                StreamWriter sw = new StreamWriter(FilePath, false, Encoding.Default);
                StringBuilder sb = new StringBuilder();

                foreach (DataGridViewColumn name in dgvData.Columns)
                    sb.Append(name.HeaderText + strSplit);
                sb.Remove((sb.Length - 1), 1);
                sb.Append(Environment.NewLine);

                for (int i = 0; i < dgvData.RowCount; i++)
                {
                    for (int j = 0; j < dgvData.ColumnCount; j++)
                    {
                        sb.Append(dgvData.Rows[i].Cells[j].Value + strSplit);
                    }
                    sb.Remove((sb.Length - 1), 1);
                    sb.Append(Environment.NewLine);
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                MessageBox.Show("数据导出完成");

            }
            catch (Exception ex)
            {
                MessageBox.Show("数据导出error---" + ex.ToString());
            }

        }
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        //private void txtLogShow_ContentsResized(object sender, ContentsResizedEventArgs e)
        //{
        //    txtLogShow.SelectionStart = txtLogShow.Text.Length;
        //    txtLogShow.ScrollToCaret();
        //}

        private void chkUseRelay_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmVerifyDevice_SizeChanged(object sender, EventArgs e)
        {

        }

        private void MainIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCali_Click(object sender, EventArgs e)
        {

        }

        private void btnDelect_Click(object sender, EventArgs e)
        {

            string strValue = dgvVoltage.Rows[dgvVoltage.CurrentCell.RowIndex].Cells["CheckTime"].Value.ToString();
            string sql1 = "DELETE FROM t_TestData WHERE CheckTime='" + strValue + "'";
            CheckTestData db = new CheckTestData();
            MessageBox.Show("删除数据" + db.ExeCuteSQL(sql1, "t_TestData"));

        }

        private void btnTestConnect_Click(object sender, EventArgs e)
        {
            this.ConnectMeter(txtMultimeterTest, cboMultimeterType.Text, cboMultimeterConType.Text, txtMultimeterAddress.Text, cboDMMcom.Text);
        }
        private void ConnectMeter(TextBox textBox, string strMeterType, string strConnectType, string strMeterAddr, string strMeterCom)
        {
            string info = "Connect testing";
            textBox.Text = info;
            textBox.BackColor = Color.Yellow;
            textBox.Refresh();
            keysight ks = new keysight();
            try
            {
                if (strMeterType == "34401A")
                {
                    if (String.IsNullOrEmpty(cboDMMcom.Text))
                    {
                        MessageBox.Show("34401A万用表串口不能为空!");
                        textBox.Text = " Connect Fail";
                        textBox.BackColor = Color.Red;
                        return;
                    }
                    else
                    {
                        ks.ComPort = strMeterCom;
                        ks.ConnectAdr = strMeterAddr;
                        ks.DeviceType = strMeterType;
                        ks.InitConnect();
                        ks.TestConnect(out info);
                        if (info != "Connect Fail")
                            textBox.BackColor = Color.Green;
                        else
                            textBox.BackColor = Color.Red;
                        textBox.Text = info;
                    }
                }
                else
                {
                    if (strConnectType == "Keysight库")
                    {
                        ks.ConnectAdr = strMeterAddr;
                        ks.DeviceType = strMeterType;
                        ks.InitConnect();
                        ks.TestConnect(out info);
                        if (info != "Connect Fail")
                            textBox.BackColor = Color.Green;
                        else
                            textBox.BackColor = Color.Red;
                        textBox.Text = info;

                    }
                }
                if (info != "Connect Fail")
                {
                    ks.CloseConnect();
                }


            }
            catch (Exception)
            {
                textBox.BackColor = Color.Red;
                textBox.Text = info;
            }
        }

        //private void btnDCsourceConn_Click(object sender, EventArgs e)
        //{
        //    this.ConnectMeter(txtDCsourceTest, cboDCsourceConType.Text, txtDCsourceAddress.Text);
        //}

        TabPage tabPage = new TabPage();
        TabPage tabPageRough = new TabPage();
        private void cboCalEQMType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (intCount == 0)//说明是第一次选择设备型号
            {
                dgvChkParameter.Rows.Clear();
                Type.Items.Clear();
                strSelectType = cboCalEQMType.Text;
                switch (cboCalEQMType.Text)
                {
                    case "BAT525G":
                        tabPage = BAT525G;
                        tabPage.Text = "BAT525G";
                        Type.Items.AddRange("充电电压_mV", "端口电压_mV", "开路电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                            "静态电流_200uA", "静态电流_2000uA", "静态电流_2000nA", "静态电流_20000nA", "CNT静态(正)_nA", "CNT静态(负)_nA", "直流内阻_mΩ",
                                            "IDR电阻_Ω", "NTC电阻_Ω", "LV电压_mV", "LV放电电流_mA");
                        chkZXSource.Visible = false;
                        chkZXSource.Checked = false;
                        break;
                    case "BAT525H":
                        tabPage = BAT525G;
                        tabPage.Text = "BAT525H";
                        Type.Items.AddRange("充电电压_mV", "端口电压_mV", "开路电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                            "静态电流_200uA", "静态电流_2000uA", "静态电流_2000nA", "静态电流_20000nA", "CNT静态(正)_nA", "CNT静态(负)_nA", "直流内阻_mΩ",
                                            "IDR电阻_Ω", "NTC电阻_Ω", "LV电压_mV", "LV放电电流_mA");
                        chkZXSource.Visible = false;
                        chkZXSource.Checked = false;
                        break;
                    case "BAT525C":
                        tabPage = BAT525C;
                        Type.Items.AddRange("充电电压_mV", "开路电压_mV", "端口电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                            "静态电流_1000uA", "静态电流_1000nA", "直流内阻_mΩ", "NTC电阻_Ω", "IDR电阻_Ω", "LV电压_mV", "LV充电电流_mA");
                        chkZXSource.Visible = true;
                        break;
                    case "BAT525D":
                        tabPage = BAT525D;
                        Type.Items.AddRange("充电电压_mV", "开路电压_mV", "端口电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                            "静态电流D_2000uA", "静态电流_1000nA", "直流内阻_mΩ", "NTC电阻_Ω", "IDR电阻_Ω", "LV电压_mV", "LV充电电流_mA");
                        chkZXSource.Visible = true;
                        break;
                    default:
                        break;
                }
                tabCalibrationSelect.TabPages.RemoveAt(0);
                tabCalibrationSelect.TabPages.Add(tabPage);
                chkCalibration.Enabled = true;
                chkAll.Enabled = true;
                chkCheck.Enabled = true;
                txtSNdeviceType.Text = cboCalEQMType.Text;
                intCount++;
                cboAccSetEQM.Text = cboCalEQMType.Text;
                btnImportParam.Enabled = true;
                txtChkParameterAddr.Text = "";

            }
            else//更改设备类型
            {
                if (strSelectType == cboCalEQMType.Text)//说明设备类型没有改变
                {
                    //无需做出改变
                    intCount++;
                }
                else
                {
                    chkCalibration.Checked = false;
                    chkAll.Checked = false;//先清空checkBox
                    chkCheck.Checked = false;
                    CheckBox box = new CheckBox();
                    foreach (Control control in tabPage.Controls)
                    {
                        if (control is CheckBox)
                        {
                            box = (CheckBox)control;
                            box.Checked = false;
                        }
                    }
                    ClearTestSelect();
                    strSelectType = cboCalEQMType.Text;
                    dgvChkParameter.Rows.Clear();
                    Type.Items.Clear();
                    switch (cboCalEQMType.Text)
                    {
                        case "BAT525G":
                            tabPage = BAT525G;
                            tabPage.Text = "BAT525G";
                            Type.Items.AddRange("充电电压_mV", "端口电压_mV", "开路电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                            "静态电流_200uA", "静态电流_2000uA", "静态电流_2000nA", "静态电流_20000nA", "CNT静态(正)_nA", "CNT静态(负)_nA", "直流内阻_mΩ",
                                            "IDR电阻_Ω", "NTC电阻_Ω", "LV电压_mV", "LV放电电流_mA");
                            chkZXSource.Visible = false;
                            chkZXSource.Checked = false;
                            break;
                        case "BAT525H":
                            tabPage = BAT525G;
                            tabPage.Text = "BAT525H";
                            Type.Items.AddRange("充电电压_mV", "端口电压_mV", "开路电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                                "静态电流_200uA", "静态电流_2000uA", "静态电流_2000nA", "静态电流_20000nA", "CNT静态(正)_nA", "CNT静态(负)_nA", "直流内阻_mΩ",
                                                "IDR电阻_Ω", "NTC电阻_Ω", "LV电压_mV", "LV放电电流_mA");
                            chkZXSource.Visible = false;
                            chkZXSource.Checked = false;
                            break;
                        case "BAT525C":
                            tabPage = BAT525C;
                            Type.Items.AddRange("充电电压_mV", "端口电压_mV", "开路电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                             "静态电流_1000uA", "静态电流_1000nA", "直流内阻_mΩ", "NTC电阻_Ω", "IDR电阻_Ω", "LV电压_mV", "LV充电电流_mA");
                            chkZXSource.Visible = true;
                            break;
                        case "BAT525D":
                            tabPage = BAT525D;
                            Type.Items.AddRange("充电电压_mV", "端口电压_mV", "开路电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                                "静态电流D_2000uA", "静态电流_1000nA", "直流内阻_mΩ", "NTC电阻_Ω", "IDR电阻_Ω", "LV电压_mV", "LV充电电流_mA");
                            chkZXSource.Visible = true;
                            break;
                        default:
                            break;
                    }
                    tabCalibrationSelect.TabPages.RemoveAt(0);
                    tabCalibrationSelect.TabPages.Add(tabPage);
                    txtSNdeviceType.Text = cboCalEQMType.Text;
                    cboAccSetEQM.Text = cboCalEQMType.Text;
                    intCount++;
                    btnImportParam.Enabled = true;
                    txtChkParameterAddr.Text = "";
                }
            }
        }
        public struct TestInfo
        {
            public string RYCom;
            public string DeviceCom;
            public string AgilentCom;
            public string BoxNum;
            public string SNCode;
            public List<byte> ChNum;
            public string Line;
            public Mode TestMode;
            public EqmAcc TestAcc;
            public string multimeterAdrr;
            public string DCsourceAdrr;
            public bool check;
            public bool Calibration;//精调
            public bool RoughCali;//粗调
            public bool RoughCalicheck;//粗调并点检
            public bool ReadEEPROM;//读eeprom
            public bool ExtRes;
            public DataTable CheckData;
            public double VoltAcc;
            public double CurAcc;
            public double IdrAcc;
            public double DCIRAcc;
            public string ErrMethod;
            public string JudgeRegulation;
            public string ResType;
            public string DeviceType;
            public string Operator;
            public string MultimeterType;
            public string MultimeterConType;
            public string DCsourceType;
            public string DCsourceConType;
            public string CalorCheckDepart;
            public string CalorCheckUser;
            public bool IDRcheck;
            public bool DCIRCheck;
        }
        public struct Mode
        {
            //精调
            public bool WriteSNCode;
            public bool ReadSNCode;

            public bool ChgVoltCalibration;
            public bool OCVCalibration;
            public bool ProVoltCalibration;
            public bool DsgCurCalibration;
            public bool DsgCurCalibration30A;

            public bool CellVoltCalibration;
            public bool CellCurCalibration;
            public bool LoadVoltCalibration;
            public bool LoadCurCalibration;

            public bool StCurCalibration;
            public bool StCur2Calibration;
            public bool StCur3Calibration;
            public bool StCur4Calibration;

            public bool StCuruACalibration;//BAT525C
            public bool StCurnACalibration;//BAT525C

            public bool IDRCalibration;//BAT525C
            public bool NTCCalibration;



            public bool DCIRCalibration;
            //public bool RoughCalibration;

            public bool CNTStPCalibration;
            public bool CNTStNCalibration;

            //public bool Calibration;
            //public bool OnlyCheck;

            //粗调
            public bool ChgVoltRoughCali;
            public bool OCVRoughCali;
            public bool ProVoltRoughCali;
            public bool DsgCurRoughCali;
            public bool DsgCurRoughCali30A;

            public bool CellVoltRoughCali;
            public bool CellCurRoughCali;
            public bool LoadVoltRoughCali;
            public bool LoadCurRoughCali;
            public bool StCurRoughCali;
            public bool StCur2RoughCali;
            public bool StCur3RoughCali;
            public bool StCur4RoughCali;

            public bool StCuruARoughCali;//BAT525C
            public bool StCurnARoughCali;//BAT525C

            public bool IDRCRoughCali;//BAT525C
            public bool NTCRoughCali;
            public bool NTCRoughCali2;
            public bool NTCRoughCali3;
            public bool NTCRoughCali4;
            public bool DCIRRoughCali;

            public bool WriteCheck;
            // public bool RoughCalibration;

            public bool CNTStPRoughCali;
            public bool CNTStNRoughCali;
        }
        public struct EqmAcc
        {
            public double ChgVolCalAcc;
            public double ChgVolChkAcc;

            public double OcvVolCalAcc;
            public double OcvVolChkAcc;

            public double PrgVolCalAcc;
            public double PrgVolChkAcc;

            public double CellVolCalAcc;
            public double CellVolChkAcc;

            public double CellCurCalAcc;
            public double CellCurChkAcc;

            public double DsgCur3ACalAcc;
            public double DsgCur3AChkAcc;

            public double DsgCur30ACalAcc;
            public double DsgCur30AChkAcc;

            public double StCur_1000uACalAcc;
            public double StCur_1000uAChkAcc;

            public double StCur_200uACalAcc;
            public double StCur_200uAChkAcc;

            public double StCur_2000uACalAcc;
            public double StCur_2000uAChkAcc;

            public double StCur_1000nACalAcc;
            public double StCur_1000nAChkAcc;

            public double StCur_2000nACalAcc;
            public double StCur_2000nAChkAcc;

            public double StCur_20000nACalAcc;
            public double StCur_20000nAChkAcc;

            public double CNTCurPCalAcc;
            public double CNTCurPChkAcc;

            public double CNTCurNCalAcc;
            public double CNTCurNChkAcc;

            public double DCIRCalAcc;
            public double DCIRChkAcc;

            public double NTC_2KCalAcc;
            public double NTC_2KChkAcc;

            public double NTC_20KCalAcc;
            public double NTC_20KChkAcc;

            public double NTC_200KCalAcc;
            public double NTC_200KChkAcc;

            public double NTC_3000KCalAcc;
            public double NTC_3000KChkAcc;

            public double LoadPartVolCalAcc;
            public double LoadPartVolChkAcc;

            public double LoadPartCurCalAcc;
            public double LoadPartCurChkAcc;

            public double IDR_2KCalAcc;
            public double IDR_2KChkAcc;

            public double IDR_20KCalAcc;
            public double IDR_20KChkAcc;

            public double IDR_200KCalAcc;
            public double IDR_200KChkAcc;

            public double IDR_3000KCalAcc;
            public double IDR_3000KChkAcc;

            public double PortVolt_CalAcc;
            public double PortVolt_ChkAcc;

            //public double 
        }
        public void ReadEqmACC(bool Calibration, string strEQMtype)
        {
            try
            {
                if (Calibration)//校准
                {
                    switch (strEQMtype)
                    {
                        case "BAT525G":
                            testinfo.TestAcc.ChgVolCalAcc = CaliACC["充电电压_mV"];
                            testinfo.TestAcc.ChgVolChkAcc = CheckACC["充电电压_mV"];

                            testinfo.TestAcc.OcvVolCalAcc = CaliACC["开路电压_mV"];
                            testinfo.TestAcc.OcvVolChkAcc = CheckACC["开路电压_mV"];

                            testinfo.TestAcc.PrgVolCalAcc = CaliACC["编程电压_mV"];
                            testinfo.TestAcc.PrgVolChkAcc = CheckACC["编程电压_mV"];

                            testinfo.TestAcc.CellVolCalAcc = CaliACC["Cell电压_mV"];
                            testinfo.TestAcc.CellVolChkAcc = CheckACC["Cell电压_mV"];

                            testinfo.TestAcc.CellCurCalAcc = CaliACC["Cell电流_mA"];
                            testinfo.TestAcc.CellCurChkAcc = CheckACC["Cell电流_mA"];

                            testinfo.TestAcc.DsgCur3ACalAcc = CaliACC["放电电流3A_mA"];
                            testinfo.TestAcc.DsgCur3AChkAcc = CheckACC["放电电流3A_mA"];

                            testinfo.TestAcc.DsgCur30ACalAcc = CaliACC["放电电流30A_mA"];
                            testinfo.TestAcc.DsgCur30AChkAcc = CheckACC["放电电流30A_mA"];

                            testinfo.TestAcc.StCur_200uACalAcc = CaliACC["静态电流_200uA"];
                            testinfo.TestAcc.StCur_200uAChkAcc = CheckACC["静态电流_200uA"];

                            testinfo.TestAcc.StCur_2000uACalAcc = CaliACC["静态电流_2000uA"];
                            testinfo.TestAcc.StCur_2000uAChkAcc = CheckACC["静态电流_2000uA"];

                            testinfo.TestAcc.StCur_2000nACalAcc = CaliACC["静态电流_2000nA"];
                            testinfo.TestAcc.StCur_2000nAChkAcc = CheckACC["静态电流_2000nA"];

                            testinfo.TestAcc.StCur_20000nACalAcc = CaliACC["静态电流_20000nA"];
                            testinfo.TestAcc.StCur_20000nAChkAcc = CheckACC["静态电流_20000nA"];

                            testinfo.TestAcc.CNTCurPCalAcc = CaliACC["CNT静态(正)_nA"];
                            testinfo.TestAcc.CNTCurPChkAcc = CheckACC["CNT静态(正)_nA"];

                            testinfo.TestAcc.CNTCurNCalAcc = CaliACC["CNT静态(负)_nA"];
                            testinfo.TestAcc.CNTCurNChkAcc = CheckACC["CNT静态(负)_nA"];

                            testinfo.TestAcc.DCIRCalAcc = CaliACC["直流内阻_mΩ"];
                            testinfo.TestAcc.DCIRChkAcc = CheckACC["直流内阻_mΩ"];

                            testinfo.TestAcc.NTC_2KCalAcc = CaliACC["NTC_2KΩ"];
                            testinfo.TestAcc.NTC_2KChkAcc = CheckACC["NTC_2KΩ"];
                            testinfo.TestAcc.IDR_2KChkAcc = CheckACC["NTC_2KΩ"];

                            testinfo.TestAcc.NTC_20KCalAcc = CaliACC["NTC_20KΩ"];
                            testinfo.TestAcc.NTC_20KChkAcc = CheckACC["NTC_20KΩ"];
                            testinfo.TestAcc.IDR_20KChkAcc = CheckACC["NTC_20KΩ"];

                            testinfo.TestAcc.NTC_200KCalAcc = CaliACC["NTC_200KΩ"];
                            testinfo.TestAcc.NTC_200KChkAcc = CheckACC["NTC_200KΩ"];
                            testinfo.TestAcc.IDR_200KChkAcc = CheckACC["NTC_200KΩ"];

                            testinfo.TestAcc.NTC_3000KCalAcc = CaliACC["NTC_3000KΩ"];
                            testinfo.TestAcc.NTC_3000KChkAcc = CheckACC["NTC_3000KΩ"];
                            testinfo.TestAcc.IDR_3000KChkAcc = CheckACC["NTC_3000KΩ"];

                            testinfo.TestAcc.LoadPartVolCalAcc = CaliACC["LV电压_mV"];
                            testinfo.TestAcc.LoadPartVolChkAcc = CheckACC["LV电压_mV"];

                            testinfo.TestAcc.LoadPartCurCalAcc = CaliACC["LV放电电流_mA"];
                            testinfo.TestAcc.LoadPartCurChkAcc = CheckACC["LV放电电流_mA"];
                            break;
                        case "BAT525H":
                            testinfo.TestAcc.ChgVolCalAcc = CaliACC["充电电压_mV"];
                            testinfo.TestAcc.ChgVolChkAcc = CheckACC["充电电压_mV"];

                            testinfo.TestAcc.OcvVolCalAcc = CaliACC["开路电压_mV"];
                            testinfo.TestAcc.OcvVolChkAcc = CheckACC["开路电压_mV"];

                            testinfo.TestAcc.PrgVolCalAcc = CaliACC["编程电压_mV"];
                            testinfo.TestAcc.PrgVolChkAcc = CheckACC["编程电压_mV"];

                            testinfo.TestAcc.CellVolCalAcc = CaliACC["Cell电压_mV"];
                            testinfo.TestAcc.CellVolChkAcc = CheckACC["Cell电压_mV"];

                            testinfo.TestAcc.CellCurCalAcc = CaliACC["Cell电流_mA"];
                            testinfo.TestAcc.CellCurChkAcc = CheckACC["Cell电流_mA"];

                            testinfo.TestAcc.DsgCur3ACalAcc = CaliACC["放电电流3A_mA"];
                            testinfo.TestAcc.DsgCur3AChkAcc = CheckACC["放电电流3A_mA"];

                            testinfo.TestAcc.DsgCur30ACalAcc = CaliACC["放电电流30A_mA"];
                            testinfo.TestAcc.DsgCur30AChkAcc = CheckACC["放电电流30A_mA"];

                            testinfo.TestAcc.StCur_200uACalAcc = CaliACC["静态电流_200uA"];
                            testinfo.TestAcc.StCur_200uAChkAcc = CheckACC["静态电流_200uA"];

                            testinfo.TestAcc.StCur_2000uACalAcc = CaliACC["静态电流_2000uA"];
                            testinfo.TestAcc.StCur_2000uAChkAcc = CheckACC["静态电流_2000uA"];

                            testinfo.TestAcc.StCur_2000nACalAcc = CaliACC["静态电流_2000nA"];
                            testinfo.TestAcc.StCur_2000nAChkAcc = CheckACC["静态电流_2000nA"];

                            testinfo.TestAcc.StCur_20000nACalAcc = CaliACC["静态电流_20000nA"];
                            testinfo.TestAcc.StCur_20000nAChkAcc = CheckACC["静态电流_20000nA"];

                            testinfo.TestAcc.CNTCurPCalAcc = CaliACC["CNT静态(正)_nA"];
                            testinfo.TestAcc.CNTCurPChkAcc = CheckACC["CNT静态(正)_nA"];

                            testinfo.TestAcc.CNTCurNCalAcc = CaliACC["CNT静态(负)_nA"];
                            testinfo.TestAcc.CNTCurNChkAcc = CheckACC["CNT静态(负)_nA"];

                            testinfo.TestAcc.DCIRCalAcc = CaliACC["直流内阻_mΩ"];
                            testinfo.TestAcc.DCIRChkAcc = CheckACC["直流内阻_mΩ"];

                            testinfo.TestAcc.NTC_2KCalAcc = CaliACC["NTC_2KΩ"];
                            testinfo.TestAcc.NTC_2KChkAcc = CheckACC["NTC_2KΩ"];
                            testinfo.TestAcc.IDR_2KChkAcc = CheckACC["NTC_2KΩ"];

                            testinfo.TestAcc.NTC_20KCalAcc = CaliACC["NTC_20KΩ"];
                            testinfo.TestAcc.NTC_20KChkAcc = CheckACC["NTC_20KΩ"];
                            testinfo.TestAcc.IDR_20KChkAcc = CheckACC["NTC_20KΩ"];

                            testinfo.TestAcc.NTC_200KCalAcc = CaliACC["NTC_200KΩ"];
                            testinfo.TestAcc.NTC_200KChkAcc = CheckACC["NTC_200KΩ"];
                            testinfo.TestAcc.IDR_200KChkAcc = CheckACC["NTC_200KΩ"];

                            testinfo.TestAcc.NTC_3000KCalAcc = CaliACC["NTC_3000KΩ"];
                            testinfo.TestAcc.NTC_3000KChkAcc = CheckACC["NTC_3000KΩ"];
                            testinfo.TestAcc.IDR_3000KChkAcc = CheckACC["NTC_3000KΩ"];

                            testinfo.TestAcc.LoadPartVolCalAcc = CaliACC["LV电压_mV"];
                            testinfo.TestAcc.LoadPartVolChkAcc = CheckACC["LV电压_mV"];

                            testinfo.TestAcc.LoadPartCurCalAcc = CaliACC["LV放电电流_mA"];
                            testinfo.TestAcc.LoadPartCurChkAcc = CheckACC["LV放电电流_mA"];
                            break;
                        case "BAT525C":
                            testinfo.TestAcc.ChgVolCalAcc = CaliACC["充电电压_mV"];
                            testinfo.TestAcc.ChgVolChkAcc = CheckACC["充电电压_mV"];

                            testinfo.TestAcc.OcvVolCalAcc = CaliACC["开路电压_mV"];
                            testinfo.TestAcc.OcvVolChkAcc = CheckACC["开路电压_mV"];

                            testinfo.TestAcc.PrgVolCalAcc = CaliACC["编程电压_mV"];
                            testinfo.TestAcc.PrgVolChkAcc = CheckACC["编程电压_mV"];

                            testinfo.TestAcc.CellVolCalAcc = CaliACC["Cell电压_mV"];
                            testinfo.TestAcc.CellVolChkAcc = CheckACC["Cell电压_mV"];

                            testinfo.TestAcc.CellCurCalAcc = CaliACC["Cell电流_mA"];
                            testinfo.TestAcc.CellCurChkAcc = CheckACC["Cell电流_mA"];

                            testinfo.TestAcc.DsgCur3ACalAcc = CaliACC["放电电流3A_mA"];
                            testinfo.TestAcc.DsgCur3AChkAcc = CheckACC["放电电流3A_mA"];

                            testinfo.TestAcc.DsgCur30ACalAcc = CaliACC["放电电流30A_mA"];
                            testinfo.TestAcc.DsgCur30AChkAcc = CheckACC["放电电流30A_mA"];

                            testinfo.TestAcc.StCur_1000uACalAcc = CaliACC["静态电流_1000uA"];
                            testinfo.TestAcc.StCur_1000uAChkAcc = CheckACC["静态电流_1000uA"];

                            testinfo.TestAcc.StCur_1000nACalAcc = CaliACC["静态电流_1000nA"];
                            testinfo.TestAcc.StCur_1000nAChkAcc = CheckACC["静态电流_1000nA"];

                            testinfo.TestAcc.DCIRCalAcc = CaliACC["直流内阻_mΩ"];
                            testinfo.TestAcc.DCIRChkAcc = CheckACC["直流内阻_mΩ"];

                            testinfo.TestAcc.NTC_2KCalAcc = CaliACC["NTC_1KΩ"];
                            testinfo.TestAcc.NTC_2KChkAcc = CheckACC["NTC_1KΩ"];

                            testinfo.TestAcc.NTC_20KCalAcc = CaliACC["NTC_10KΩ"];
                            testinfo.TestAcc.NTC_20KChkAcc = CheckACC["NTC_10KΩ"];

                            testinfo.TestAcc.NTC_200KCalAcc = CaliACC["NTC_100KΩ"];
                            testinfo.TestAcc.NTC_200KChkAcc = CheckACC["NTC_100KΩ"];

                            testinfo.TestAcc.NTC_3000KCalAcc = CaliACC["NTC_1000KΩ"];
                            testinfo.TestAcc.NTC_3000KChkAcc = CheckACC["NTC_1000KΩ"];

                            testinfo.TestAcc.IDR_2KCalAcc = CaliACC["IDR_1KΩ"];
                            testinfo.TestAcc.IDR_2KChkAcc = CheckACC["IDR_1KΩ"];

                            testinfo.TestAcc.IDR_20KCalAcc = CaliACC["IDR_10KΩ"];
                            testinfo.TestAcc.IDR_20KChkAcc = CheckACC["IDR_10KΩ"];

                            testinfo.TestAcc.IDR_200KCalAcc = CaliACC["IDR_100KΩ"];
                            testinfo.TestAcc.IDR_200KChkAcc = CheckACC["IDR_100KΩ"];

                            testinfo.TestAcc.IDR_3000KCalAcc = CaliACC["IDR_1000KΩ"];
                            testinfo.TestAcc.IDR_3000KChkAcc = CheckACC["IDR_1000KΩ"];

                            testinfo.TestAcc.LoadPartVolCalAcc = CaliACC["LV电压_mV"];
                            testinfo.TestAcc.LoadPartVolChkAcc = CheckACC["LV电压_mV"];

                            testinfo.TestAcc.LoadPartCurCalAcc = CaliACC["LV充电电流_mA"];
                            testinfo.TestAcc.LoadPartCurChkAcc = CheckACC["LV充电电流_mA"];
                            break;
                        case "BAT525D":
                            testinfo.TestAcc.ChgVolCalAcc = CaliACC["充电电压_mV"];
                            testinfo.TestAcc.ChgVolChkAcc = CheckACC["充电电压_mV"];

                            testinfo.TestAcc.OcvVolCalAcc = CaliACC["开路电压_mV"];
                            testinfo.TestAcc.OcvVolChkAcc = CheckACC["开路电压_mV"];

                            testinfo.TestAcc.PrgVolCalAcc = CaliACC["编程电压_mV"];
                            testinfo.TestAcc.PrgVolChkAcc = CheckACC["编程电压_mV"];

                            testinfo.TestAcc.CellVolCalAcc = CaliACC["Cell电压_mV"];
                            testinfo.TestAcc.CellVolChkAcc = CheckACC["Cell电压_mV"];

                            testinfo.TestAcc.CellCurCalAcc = CaliACC["Cell电流_mA"];
                            testinfo.TestAcc.CellCurChkAcc = CheckACC["Cell电流_mA"];

                            testinfo.TestAcc.DsgCur3ACalAcc = CaliACC["放电电流3A_mA"];
                            testinfo.TestAcc.DsgCur3AChkAcc = CheckACC["放电电流3A_mA"];

                            testinfo.TestAcc.DsgCur30ACalAcc = CaliACC["放电电流30A_mA"];
                            testinfo.TestAcc.DsgCur30AChkAcc = CheckACC["放电电流30A_mA"];

                            testinfo.TestAcc.StCur_2000uACalAcc = CaliACC["静态电流D_2000uA"];
                            testinfo.TestAcc.StCur_2000uAChkAcc = CheckACC["静态电流D_2000uA"];

                            testinfo.TestAcc.StCur_1000nACalAcc = CaliACC["静态电流_1000nA"];
                            testinfo.TestAcc.StCur_1000nAChkAcc = CheckACC["静态电流_1000nA"];

                            testinfo.TestAcc.DCIRCalAcc = CaliACC["直流内阻_mΩ"];
                            testinfo.TestAcc.DCIRChkAcc = CheckACC["直流内阻_mΩ"];

                            testinfo.TestAcc.NTC_2KCalAcc = CaliACC["NTC_1KΩ"];
                            testinfo.TestAcc.NTC_2KChkAcc = CheckACC["NTC_1KΩ"];

                            testinfo.TestAcc.NTC_20KCalAcc = CaliACC["NTC_10KΩ"];
                            testinfo.TestAcc.NTC_20KChkAcc = CheckACC["NTC_10KΩ"];

                            testinfo.TestAcc.NTC_200KCalAcc = CaliACC["NTC_100KΩ"];
                            testinfo.TestAcc.NTC_200KChkAcc = CheckACC["NTC_100KΩ"];

                            testinfo.TestAcc.NTC_3000KCalAcc = CaliACC["NTC_1000KΩ"];
                            testinfo.TestAcc.NTC_3000KChkAcc = CheckACC["NTC_1000KΩ"];

                            testinfo.TestAcc.IDR_2KCalAcc = CaliACC["IDR_1KΩ"];
                            testinfo.TestAcc.IDR_2KChkAcc = CheckACC["IDR_1KΩ"];

                            testinfo.TestAcc.IDR_20KCalAcc = CaliACC["IDR_10KΩ"];
                            testinfo.TestAcc.IDR_20KChkAcc = CheckACC["IDR_10KΩ"];

                            testinfo.TestAcc.IDR_200KCalAcc = CaliACC["IDR_100KΩ"];
                            testinfo.TestAcc.IDR_200KChkAcc = CheckACC["IDR_100KΩ"];

                            testinfo.TestAcc.IDR_3000KCalAcc = CaliACC["IDR_1000KΩ"];
                            testinfo.TestAcc.IDR_3000KChkAcc = CheckACC["IDR_1000KΩ"];

                            testinfo.TestAcc.LoadPartVolCalAcc = CaliACC["LV电压_mV"];
                            testinfo.TestAcc.LoadPartVolChkAcc = CheckACC["LV电压_mV"];

                            testinfo.TestAcc.LoadPartCurCalAcc = CaliACC["LV充电电流_mA"];
                            testinfo.TestAcc.LoadPartCurChkAcc = CheckACC["LV充电电流_mA"];
                            break;
                    }
                }
                else//点检
                {
                    switch (strEQMtype)
                    {
                        case "BAT525G":
                            testinfo.TestAcc.ChgVolChkAcc = CheckACC["充电电压_mV"];

                            testinfo.TestAcc.OcvVolChkAcc = CheckACC["开路电压_mV"];

                            testinfo.TestAcc.PrgVolChkAcc = CheckACC["编程电压_mV"];

                            testinfo.TestAcc.CellVolChkAcc = CheckACC["Cell电压_mV"];

                            testinfo.TestAcc.CellCurChkAcc = CheckACC["Cell电流_mA"];

                            testinfo.TestAcc.DsgCur3AChkAcc = CheckACC["放电电流3A_mA"];

                            testinfo.TestAcc.DsgCur30AChkAcc = CheckACC["放电电流30A_mA"];

                            testinfo.TestAcc.StCur_200uAChkAcc = CheckACC["静态电流_200uA"];

                            testinfo.TestAcc.StCur_2000uAChkAcc = CheckACC["静态电流_2000uA"];

                            testinfo.TestAcc.StCur_2000nAChkAcc = CheckACC["静态电流_2000nA"];

                            testinfo.TestAcc.StCur_20000nAChkAcc = CheckACC["静态电流_20000nA"];

                            testinfo.TestAcc.CNTCurPChkAcc = CheckACC["CNT静态(正)_nA"];

                            testinfo.TestAcc.CNTCurNChkAcc = CheckACC["CNT静态(负)_nA"];

                            testinfo.TestAcc.DCIRChkAcc = CheckACC["直流内阻_mΩ"];

                            testinfo.TestAcc.NTC_2KChkAcc = CheckACC["NTC_2KΩ"];
                            testinfo.TestAcc.IDR_2KChkAcc = CheckACC["NTC_2KΩ"];

                            testinfo.TestAcc.NTC_20KChkAcc = CheckACC["NTC_20KΩ"];
                            testinfo.TestAcc.IDR_20KChkAcc = CheckACC["NTC_20KΩ"];

                            testinfo.TestAcc.NTC_200KChkAcc = CheckACC["NTC_200KΩ"];
                            testinfo.TestAcc.IDR_200KChkAcc = CheckACC["NTC_200KΩ"];

                            testinfo.TestAcc.NTC_3000KChkAcc = CheckACC["NTC_3000KΩ"];
                            testinfo.TestAcc.IDR_3000KChkAcc = CheckACC["NTC_3000KΩ"];

                            testinfo.TestAcc.LoadPartVolChkAcc = CheckACC["LV电压_mV"];

                            testinfo.TestAcc.LoadPartCurChkAcc = CheckACC["LV放电电流_mA"];
                            break;
                        case "BAT525H":
                            testinfo.TestAcc.ChgVolChkAcc = CheckACC["充电电压_mV"];

                            testinfo.TestAcc.OcvVolChkAcc = CheckACC["开路电压_mV"];

                            testinfo.TestAcc.PrgVolChkAcc = CheckACC["编程电压_mV"];

                            testinfo.TestAcc.CellVolChkAcc = CheckACC["Cell电压_mV"];

                            testinfo.TestAcc.CellCurChkAcc = CheckACC["Cell电流_mA"];

                            testinfo.TestAcc.DsgCur3AChkAcc = CheckACC["放电电流3A_mA"];

                            testinfo.TestAcc.DsgCur30AChkAcc = CheckACC["放电电流30A_mA"];

                            testinfo.TestAcc.StCur_200uAChkAcc = CheckACC["静态电流_200uA"];

                            testinfo.TestAcc.StCur_2000uAChkAcc = CheckACC["静态电流_2000uA"];

                            testinfo.TestAcc.StCur_2000nAChkAcc = CheckACC["静态电流_2000nA"];

                            testinfo.TestAcc.StCur_20000nAChkAcc = CheckACC["静态电流_20000nA"];

                            testinfo.TestAcc.CNTCurPChkAcc = CheckACC["CNT静态(正)_nA"];

                            testinfo.TestAcc.CNTCurNChkAcc = CheckACC["CNT静态(负)_nA"];

                            testinfo.TestAcc.DCIRChkAcc = CheckACC["直流内阻_mΩ"];

                            testinfo.TestAcc.NTC_2KChkAcc = CheckACC["NTC_2KΩ"];
                            testinfo.TestAcc.IDR_2KChkAcc = CheckACC["NTC_2KΩ"];

                            testinfo.TestAcc.NTC_20KChkAcc = CheckACC["NTC_20KΩ"];
                            testinfo.TestAcc.IDR_20KChkAcc = CheckACC["NTC_20KΩ"];

                            testinfo.TestAcc.NTC_200KChkAcc = CheckACC["NTC_200KΩ"];
                            testinfo.TestAcc.IDR_200KChkAcc = CheckACC["NTC_200KΩ"];

                            testinfo.TestAcc.NTC_3000KChkAcc = CheckACC["NTC_3000KΩ"];
                            testinfo.TestAcc.IDR_3000KChkAcc = CheckACC["NTC_3000KΩ"];

                            testinfo.TestAcc.LoadPartVolChkAcc = CheckACC["LV电压_mV"];

                            testinfo.TestAcc.LoadPartCurChkAcc = CheckACC["LV放电电流_mA"];
                            break;
                        case "BAT525C":
                            testinfo.TestAcc.ChgVolChkAcc = CheckACC["充电电压_mV"];

                            testinfo.TestAcc.OcvVolChkAcc = CheckACC["开路电压_mV"];

                            testinfo.TestAcc.PrgVolChkAcc = CheckACC["编程电压_mV"];

                            testinfo.TestAcc.CellVolChkAcc = CheckACC["Cell电压_mV"];

                            testinfo.TestAcc.CellCurChkAcc = CheckACC["Cell电流_mA"];

                            testinfo.TestAcc.DsgCur3AChkAcc = CheckACC["放电电流3A_mA"];

                            testinfo.TestAcc.DsgCur30AChkAcc = CheckACC["放电电流30A_mA"];

                            testinfo.TestAcc.StCur_1000uAChkAcc = CheckACC["静态电流_1000uA"];

                            testinfo.TestAcc.StCur_1000nAChkAcc = CheckACC["静态电流_1000nA"];

                            testinfo.TestAcc.DCIRChkAcc = CheckACC["直流内阻_mΩ"];

                            testinfo.TestAcc.NTC_2KChkAcc = CheckACC["NTC_1KΩ"];

                            testinfo.TestAcc.NTC_20KChkAcc = CheckACC["NTC_10KΩ"];

                            testinfo.TestAcc.NTC_200KChkAcc = CheckACC["NTC_100KΩ"];

                            testinfo.TestAcc.NTC_3000KChkAcc = CheckACC["NTC_1000KΩ"];

                            testinfo.TestAcc.IDR_2KChkAcc = CheckACC["IDR_1KΩ"];

                            testinfo.TestAcc.IDR_20KChkAcc = CheckACC["IDR_10KΩ"];

                            testinfo.TestAcc.IDR_200KChkAcc = CheckACC["IDR_100KΩ"];

                            testinfo.TestAcc.IDR_3000KChkAcc = CheckACC["IDR_1000KΩ"];

                            testinfo.TestAcc.LoadPartVolChkAcc = CheckACC["LV电压_mV"];

                            testinfo.TestAcc.LoadPartCurChkAcc = CheckACC["LV充电电流_mA"];
                            break;
                        case "BAT525D":
                            testinfo.TestAcc.ChgVolChkAcc = CheckACC["充电电压_mV"];

                            testinfo.TestAcc.OcvVolChkAcc = CheckACC["开路电压_mV"];

                            testinfo.TestAcc.PrgVolChkAcc = CheckACC["编程电压_mV"];

                            testinfo.TestAcc.CellVolChkAcc = CheckACC["Cell电压_mV"];

                            testinfo.TestAcc.CellCurChkAcc = CheckACC["Cell电流_mA"];

                            testinfo.TestAcc.DsgCur3AChkAcc = CheckACC["放电电流3A_mA"];

                            testinfo.TestAcc.DsgCur30AChkAcc = CheckACC["放电电流30A_mA"];

                            testinfo.TestAcc.StCur_2000uAChkAcc = CheckACC["静态电流D_2000uA"];

                            testinfo.TestAcc.StCur_1000nAChkAcc = CheckACC["静态电流_1000nA"];

                            testinfo.TestAcc.DCIRChkAcc = CheckACC["直流内阻_mΩ"];

                            testinfo.TestAcc.NTC_2KChkAcc = CheckACC["NTC_1KΩ"];

                            testinfo.TestAcc.NTC_20KChkAcc = CheckACC["NTC_10KΩ"];

                            testinfo.TestAcc.NTC_200KChkAcc = CheckACC["NTC_100KΩ"];

                            testinfo.TestAcc.NTC_3000KChkAcc = CheckACC["NTC_1000KΩ"];

                            testinfo.TestAcc.IDR_2KChkAcc = CheckACC["IDR_1KΩ"];

                            testinfo.TestAcc.IDR_20KChkAcc = CheckACC["IDR_10KΩ"];

                            testinfo.TestAcc.IDR_200KChkAcc = CheckACC["IDR_100KΩ"];

                            testinfo.TestAcc.IDR_3000KChkAcc = CheckACC["IDR_1000KΩ"];

                            testinfo.TestAcc.LoadPartVolChkAcc = CheckACC["LV电压_mV"];

                            testinfo.TestAcc.LoadPartCurChkAcc = CheckACC["LV充电电流_mA"];
                            break;
                    }
                }

            }
            catch
            {
                return;
            }
        }
        private void btnCalibration_Click(object sender, EventArgs e)
        {
            try
            {
                num = 0;
                SaveLog = chkSaveLog.Checked;
                dgvTestResult.Rows.Clear();
                lstShowResult.Items.Clear();
                AloneSave = chkAloneSave.Checked;
                stcCurReadVol = chkstcCur_ReadVol.Checked;
                if (chkExtRes.Checked)
                {
                    if (cboResSpecification.Text == "" || cboResSpecification.Text == null)
                    {
                        MessageBox.Show("使用外接电阻，请选择对应的分流器规格!");
                        return;
                    }
                }
                if (chkCalibration.Checked)
                {
                    if (!String.IsNullOrEmpty(txtSNnum.Text) || (chkReadSNCodeStart.Checked && (cboCalEQMType.Text == "BAT525H" || cboCalEQMType.Text == "BAT525G")))
                    {
                        blStop = false;
                        //基本信息保存
                        testinfo.RoughCali = false;
                        testinfo.IDRcheck = false;
                        testinfo.ChNum = new List<byte>();
                        //testinfo.Line = txtLine.Text;
                        if (chkWriteSNCode.Checked)
                        {
                            if (txtSNnum.Text.Length != 4)
                            {
                                MessageBox.Show("设备编码长度不等于4位，请重新确认!");
                                return;
                            }
                            else if (txtDateTime.Text.Length != 8)
                            {
                                MessageBox.Show("时间长度不等于8位，请重新确认!");
                                return;
                            }
                            else
                            {
                                testinfo.SNCode = txtSNdeviceType.Text + "-" + txtDateTime.Text + "-" + txtSNnum.Text + "-" + "0";
                                testinfo.BoxNum = txtSNnum.Text;
                            }
                        }
                        else
                        {
                            if (chkReadSNCodeStart.Checked)
                            {
                                testinfo.BoxNum = "";
                            }
                            else
                                testinfo.BoxNum = txtSNnum.Text;
                        }
                        testinfo.ResType = cboResSpecification.Text;
                        testinfo.DeviceType = cboCalEQMType.Text;
                        testinfo.DeviceCom = cboEqmCalSerialPort.Text;
                        testinfo.JudgeRegulation = cboSaveErrrRule.Text;
                        testinfo.CalorCheckDepart = txtCalorCheckDepart.Text;
                        testinfo.CalorCheckUser = txtCalorCheckUser.Text;
                        testinfo.ExtRes = chkExtRes.Checked;
                        strCalEQMType = cboCalEQMType.Text;
                        //testinfo.AgilentCom = cmbComConnect.Text;
                        //testinfo.check = chkCheck.Checked;
                        //testinfo.RoughCalicheck = chkRoughCalibration.Checked;
                        //RoughCalicheck
                        //仪表信息保存
                        testinfo.MultimeterType = cboMultimeterType.Text;
                        testinfo.MultimeterConType = cboMultimeterConType.Text;
                        testinfo.multimeterAdrr = txtMultimeterAddress.Text;
                        //testinfo.DCsourceAdrr = txtDCsourceAddress.Text;
                        //testinfo.DCsourceType = cboDCsourceType.Text;
                        //testinfo.DCsourceConType = cboDCsourceConType.Text;
                        ReadEqmACC(true, testinfo.DeviceType);
                        //校准项目保存

                        switch (testinfo.DeviceType)
                        {
                            case "BAT525G":
                                testinfo.TestMode.WriteSNCode = chkWriteSNCode.Checked;
                                testinfo.TestMode.ReadSNCode = chkReadSNCodeStart.Checked;
                                testinfo.TestMode.ChgVoltCalibration = SBT825G_chkChgVoltCalibration.Checked;
                                testinfo.TestMode.OCVCalibration = SBT825G_chkOCVCalibration.Checked;
                                testinfo.TestMode.ProVoltCalibration = SBT825G_chkProVoltCalibration.Checked;
                                testinfo.TestMode.DsgCurCalibration = SBT825G_chkDsgCurCalibration.Checked;
                                testinfo.TestMode.DsgCurCalibration30A = SBT825G_chkDsgCurCalibration30A.Checked;
                                testinfo.TestMode.LoadVoltCalibration = SBT825G_chkLoadPartVolt.Checked;
                                testinfo.TestMode.LoadCurCalibration = SBT825G_chkLoadPartCur.Checked;
                                testinfo.TestMode.CellVoltCalibration = SBT825G_chkCellVolt.Checked;
                                testinfo.TestMode.CellCurCalibration = SBT825G_chkCellCur.Checked;
                                testinfo.TestMode.StCurCalibration = SBT825G_chkStCurCalibration.Checked;
                                testinfo.TestMode.StCur2Calibration = SBT825G_chkStCur2Calibration.Checked;
                                testinfo.TestMode.StCur3Calibration = SBT825G_chkStCur3Calibration.Checked;
                                testinfo.TestMode.StCur4Calibration = SBT825G_chkStCur4Calibration.Checked;
                                testinfo.TestMode.NTCCalibration = SBT825G_chkNTCCalibration.Checked;
                                testinfo.TestMode.DCIRCalibration = SBT825G_chkDCIRCalibration.Checked;
                                testinfo.TestMode.CNTStPCalibration = SBT825G_chkCNTStPositive.Checked;
                                testinfo.TestMode.CNTStNCalibration = SBT825G_chkCNTStNegative.Checked;
                                if (testinfo.TestMode.CellCurCalibration || testinfo.TestMode.CellVoltCalibration || testinfo.TestMode.ChgVoltCalibration || testinfo.TestMode.DsgCurCalibration || testinfo.TestMode.DsgCurCalibration30A || testinfo.TestMode.LoadCurCalibration || testinfo.TestMode.LoadVoltCalibration || testinfo.TestMode.OCVCalibration || testinfo.TestMode.ProVoltCalibration || testinfo.TestMode.DCIRCalibration)
                                {
                                    connMultimeter = true;
                                }
                                else
                                    connMultimeter = false;
                                break;
                            case "BAT525H":
                                testinfo.TestMode.WriteSNCode = chkWriteSNCode.Checked;
                                testinfo.TestMode.ReadSNCode = chkReadSNCodeStart.Checked;
                                testinfo.TestMode.ChgVoltCalibration = SBT825G_chkChgVoltCalibration.Checked;
                                testinfo.TestMode.OCVCalibration = SBT825G_chkOCVCalibration.Checked;
                                testinfo.TestMode.ProVoltCalibration = SBT825G_chkProVoltCalibration.Checked;
                                testinfo.TestMode.DsgCurCalibration = SBT825G_chkDsgCurCalibration.Checked;
                                testinfo.TestMode.DsgCurCalibration30A = SBT825G_chkDsgCurCalibration30A.Checked;
                                testinfo.TestMode.LoadVoltCalibration = SBT825G_chkLoadPartVolt.Checked;
                                testinfo.TestMode.LoadCurCalibration = SBT825G_chkLoadPartCur.Checked;
                                testinfo.TestMode.CellVoltCalibration = SBT825G_chkCellVolt.Checked;
                                testinfo.TestMode.CellCurCalibration = SBT825G_chkCellCur.Checked;
                                testinfo.TestMode.StCurCalibration = SBT825G_chkStCurCalibration.Checked;
                                testinfo.TestMode.StCur2Calibration = SBT825G_chkStCur2Calibration.Checked;
                                testinfo.TestMode.StCur3Calibration = SBT825G_chkStCur3Calibration.Checked;
                                testinfo.TestMode.StCur4Calibration = SBT825G_chkStCur4Calibration.Checked;
                                testinfo.TestMode.NTCCalibration = SBT825G_chkNTCCalibration.Checked;
                                testinfo.TestMode.DCIRCalibration = SBT825G_chkDCIRCalibration.Checked;
                                testinfo.TestMode.CNTStPCalibration = SBT825G_chkCNTStPositive.Checked;
                                testinfo.TestMode.CNTStNCalibration = SBT825G_chkCNTStNegative.Checked;
                                if (testinfo.TestMode.CellCurCalibration || testinfo.TestMode.CellVoltCalibration || testinfo.TestMode.ChgVoltCalibration || testinfo.TestMode.DsgCurCalibration || testinfo.TestMode.DsgCurCalibration30A || testinfo.TestMode.LoadCurCalibration || testinfo.TestMode.LoadVoltCalibration || testinfo.TestMode.OCVCalibration || testinfo.TestMode.ProVoltCalibration || testinfo.TestMode.DCIRCalibration)
                                {
                                    connMultimeter = true;
                                }
                                else
                                    connMultimeter = false;
                                break;
                            case "BAT525C":
                                testinfo.TestMode.WriteSNCode = false;
                                testinfo.TestMode.ReadSNCode = false;
                                testinfo.TestMode.ChgVoltCalibration = BAT525C_chkChgVoltCalibration.Checked;
                                testinfo.TestMode.OCVCalibration = BAT525C_chkOCVCalibration.Checked;
                                //testinfo.TestMode.ProVoltCalibration = BAT525C_chkProVoltCalibration.Checked;
                                testinfo.TestMode.DsgCurCalibration = BAT525C_chkDsgCurCalibration.Checked;
                                testinfo.TestMode.DsgCurCalibration30A = BAT525C_chkDsgCurCalibration30A.Checked;
                                testinfo.TestMode.LoadVoltCalibration = BAT525C_chkLoadPartVolt.Checked;
                                testinfo.TestMode.LoadCurCalibration = BAT525C_chkLoadPartCur.Checked;
                                testinfo.TestMode.CellVoltCalibration = BAT525C_chkCellVolt.Checked;
                                testinfo.TestMode.CellCurCalibration = BAT525C_chkCellCur.Checked;
                                testinfo.TestMode.StCuruACalibration = BAT525C__chkStCuruACalibration.Checked;
                                testinfo.TestMode.StCurnACalibration = BAT525C_chkStCurnACalibration.Checked;
                                testinfo.TestMode.NTCCalibration = BAT525C_chkNTCCalibration.Checked;
                                testinfo.TestMode.IDRCalibration = BAT525C_chkIDRCalibration.Checked;
                                testinfo.TestMode.DCIRCalibration = BAT525C_chkDCIRCalibration.Checked;
                                if (testinfo.TestMode.CellCurCalibration || testinfo.TestMode.CellVoltCalibration || testinfo.TestMode.ChgVoltCalibration || testinfo.TestMode.DsgCurCalibration || testinfo.TestMode.DsgCurCalibration30A || testinfo.TestMode.LoadCurCalibration || testinfo.TestMode.LoadVoltCalibration || testinfo.TestMode.OCVCalibration || testinfo.TestMode.ProVoltCalibration || testinfo.TestMode.DCIRCalibration)
                                {
                                    connMultimeter = true;
                                }
                                else
                                    connMultimeter = false;
                                break;
                            case "BAT525D":
                                testinfo.TestMode.WriteSNCode = false;
                                testinfo.TestMode.ReadSNCode = false;
                                testinfo.TestMode.ChgVoltCalibration = BAT525D_chkChgVoltCalibration.Checked;
                                testinfo.TestMode.OCVCalibration = BAT525D_chkOCVCalibration.Checked;
                                //testinfo.TestMode.ProVoltCalibration = BAT525D_chkProVoltCalibration.Checked;
                                testinfo.TestMode.DsgCurCalibration = BAT525D_chkDsgCurCalibration.Checked;
                                testinfo.TestMode.DsgCurCalibration30A = BAT525D_chkDsgCurCalibration30A.Checked;
                                testinfo.TestMode.LoadVoltCalibration = BAT525D_chkLoadPartVolt.Checked;
                                testinfo.TestMode.LoadCurCalibration = BAT525D_chkLoadPartCur.Checked;
                                testinfo.TestMode.CellVoltCalibration = BAT525D_chkCellVolt.Checked;
                                testinfo.TestMode.CellCurCalibration = BAT525D_chkCellCur.Checked;
                                testinfo.TestMode.StCuruACalibration = BAT525D__chkStCuruACalibration.Checked;
                                testinfo.TestMode.StCurnACalibration = BAT525D_chkStCurnACalibration.Checked;
                                testinfo.TestMode.NTCCalibration = BAT525D_chkNTCCalibration.Checked;
                                testinfo.TestMode.IDRCalibration = BAT525D_chkIDRCalibration.Checked;
                                testinfo.TestMode.DCIRCalibration = BAT525D_chkDCIRCalibration.Checked;
                                if (testinfo.TestMode.CellCurCalibration || testinfo.TestMode.CellVoltCalibration || testinfo.TestMode.ChgVoltCalibration || testinfo.TestMode.DsgCurCalibration || testinfo.TestMode.DsgCurCalibration30A || testinfo.TestMode.LoadCurCalibration || testinfo.TestMode.LoadVoltCalibration || testinfo.TestMode.OCVCalibration || testinfo.TestMode.ProVoltCalibration || testinfo.TestMode.DCIRCalibration)
                                {
                                    connMultimeter = true;
                                }
                                else
                                    connMultimeter = false;
                                break;
                        }
                        if (connMultimeter)
                        {
                            strMultimeterType = cboMultimeterType.Text;
                            strMultimeterConType = cboMultimeterConType.Text;
                            strDMMcom = cboDMMcom.Text;
                            if (strMultimeterType == "34401A" && String.IsNullOrEmpty(strDMMcom))
                            {
                                MessageBox.Show("34401A万用表串口不能为空!");
                                return;
                            }
                            if (strMultimeterConType == "Keysight库")
                            {
                                strMultimeterAddr = txtMultimeterAddress.Text;
                            }
                        }
                        if (testinfo.DeviceCom == "")
                        {
                            MessageBox.Show("请选择设备串口!");
                            return;
                        }
                        else
                        {
                            testinfo.check = false;
                            testinfo.Calibration = true;
                            testinfo.RoughCali = false;
                            testinfo.RoughCalicheck = false;
                            testinfo.ReadEEPROM = false;
                            testinfo.IDRcheck = false;
                            btnCalibration.Enabled = false;
                            CheckItem.StopStatus = false;
                            blStop = false;
                            AutoCaliOrCheck = new Thread(CheckOrCalibration);  //启动的线程
                            AutoCaliOrCheck.IsBackground = true;
                            AutoCaliOrCheck.Start();
                        }
                    }
                    else
                        MessageBox.Show("请输入待校准设备编码!");
                }
                else if (chkCheck.Checked)//点检时输出点检报告
                {
                    if (!String.IsNullOrEmpty(txtSNnum.Text) || (chkReadSNCodeStart.Checked && (cboCalEQMType.Text == "BAT525H" || cboCalEQMType.Text == "BAT525G")))
                    {
                        if (cboEqmCalSerialPort.Text != "")
                        {
                            AloneSave = chkAloneSave.Checked;
                            testinfo.Line = txtSNnum.Text;
                            testinfo.BoxNum = txtSNdeviceType.Text + txtSNnum.Text;
                            testinfo.ResType = cboResSpecification.Text;
                            testinfo.DeviceType = cboCalEQMType.Text;
                            testinfo.DeviceCom = cboEqmCalSerialPort.Text;
                            testinfo.JudgeRegulation = cboSaveErrrRule.Text;
                            testinfo.CalorCheckDepart = txtCalorCheckDepart.Text;
                            testinfo.CalorCheckUser = txtCalorCheckUser.Text;
                            testinfo.ExtRes = chkExtRes.Checked;
                            testinfo.check = true;
                            testinfo.Calibration = false;
                            testinfo.RoughCali = false;
                            testinfo.RoughCalicheck = false;
                            testinfo.ReadEEPROM = false;
                            testinfo.IDRcheck = false;
                            strCalEQMType = cboCalEQMType.Text;
                            dgvTestResult.ReadOnly = true;
                            ReadTestItems();
                            ReadEqmACC(false, testinfo.DeviceType);
                            //Line = txtLines.Text;
                            TestID = txtSNnum.Text;
                            TesterType = cboCalEQMType.Text;
                            strCHComNum = cboEqmCalSerialPort.Text;//通道串口号
                            if (chkReadSNCodeStart.Checked)
                            {
                                switch (testinfo.DeviceType)
                                {
                                    case "BAT525G":
                                        testinfo.TestMode.WriteSNCode = false;
                                        testinfo.TestMode.ReadSNCode = true;
                                        break;
                                    case "BAT525H":
                                        testinfo.TestMode.WriteSNCode = false;
                                        testinfo.TestMode.ReadSNCode = true;
                                        break;
                                    case "BAT525C":
                                        testinfo.TestMode.WriteSNCode = false;
                                        testinfo.TestMode.ReadSNCode = false;
                                        break;
                                    case "BAT525D":
                                        testinfo.TestMode.WriteSNCode = false;
                                        testinfo.TestMode.ReadSNCode = false;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                                testinfo.TestMode.WriteSNCode = false;
                                testinfo.TestMode.ReadSNCode = false;
                            }
                            if (connMultimeter)
                            {
                                strMultimeterType = cboMultimeterType.Text;//仪器名称
                                strConnType = cboMultimeterConType.Text; //连接方式
                                strDMMcom = cboDMMcom.Text;
                                if (strMultimeterType == "34401A" && String.IsNullOrEmpty(strDMMcom))
                                {
                                    MessageBox.Show("34401A万用表串口不能为空!");
                                    return;
                                }
                                if (strConnType == "Keysight库")
                                {
                                    if (txtMultimeterAddress.Text != "")
                                        strMultimeterAddr = txtMultimeterAddress.Text;
                                    else
                                    {
                                        MessageBox.Show("万用表地址为空!");
                                        return;
                                    }
                                }

                            }
                            btnCalibration.Enabled = false;
                            blStop = false;
                            testCheckThread = new Thread(CheckThread);
                            testCheckThread.IsBackground = true;
                            testCheckThread.Name = strCHComNum;
                            testCheckThread.Start();
                        }
                        else
                        {
                            MessageBox.Show("请选择设备串口!");
                            return;
                        }
                    }
                    else
                        MessageBox.Show("请输入待点检设备编码!");
                }
                else
                {
                    MessageBox.Show("请选择设备测试类型(校准或点检)!");
                    return;
                }
            }
            catch
            {

            }
        }
        private void CheckOrCalibration()
        {
            try
            {
                StartCalibration(testinfo);
            }
            catch (Exception)
            {

            }
        }
        public void UpdateUi(string DeviceType, string BoxNum, string CheckType, string CheckValue, string DMMValue, string TestValue, string Err1, string Err2, string Acc, string result)
        {
            try
            {
                if (testinfo.Calibration || testinfo.check)
                {
                    if (this.InvokeRequired)//因为这个返回的是False
                    {
                        this.Invoke(new Action(() =>
                        {
                            num++;
                            dgvTestResult.Rows.Add();
                            dgvTestResult.Rows[num - 1].Cells[0].Value = num.ToString();
                            dgvTestResult.Rows[num - 1].Cells[1].Value = DeviceType;
                            dgvTestResult.Rows[num - 1].Cells[2].Value = BoxNum;
                            dgvTestResult.Rows[num - 1].Cells[3].Value = CheckType;
                            dgvTestResult.Rows[num - 1].Cells[4].Value = CheckValue;
                            dgvTestResult.Rows[num - 1].Cells[5].Value = DMMValue;
                            dgvTestResult.Rows[num - 1].Cells[6].Value = TestValue;
                            dgvTestResult.Rows[num - 1].Cells[7].Value = Err1;
                            dgvTestResult.Rows[num - 1].Cells[8].Value = Err2;
                            dgvTestResult.Rows[num - 1].Cells[9].Value = Acc;
                            dgvTestResult.Rows[num - 1].Cells[10].Value = result;
                            dgvTestResult.FirstDisplayedScrollingRowIndex = dgvTestResult.Rows.Count - 1;
                            dgvTestResult.Refresh();
                        }));
                    }
                }
                else if (testinfo.RoughCali || testinfo.RoughCalicheck || testinfo.ReadEEPROM || testinfo.IDRcheck)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            numRough++;
                            dgvRoughCalResult.Rows.Add();
                            dgvRoughCalResult.Rows[numRough - 1].Cells[0].Value = numRough.ToString();
                            dgvRoughCalResult.Rows[numRough - 1].Cells[1].Value = DeviceType;
                            dgvRoughCalResult.Rows[numRough - 1].Cells[2].Value = BoxNum;
                            dgvRoughCalResult.Rows[numRough - 1].Cells[3].Value = CheckType;
                            dgvRoughCalResult.Rows[numRough - 1].Cells[4].Value = CheckValue;
                            dgvRoughCalResult.Rows[numRough - 1].Cells[5].Value = DMMValue;
                            dgvRoughCalResult.Rows[numRough - 1].Cells[6].Value = TestValue;
                            dgvRoughCalResult.Rows[numRough - 1].Cells[7].Value = Err1;
                            dgvRoughCalResult.Rows[numRough - 1].Cells[8].Value = Err2;
                            dgvRoughCalResult.Rows[numRough - 1].Cells[9].Value = Acc;
                            dgvRoughCalResult.Rows[numRough - 1].Cells[10].Value = result;
                            dgvRoughCalResult.FirstDisplayedScrollingRowIndex = dgvRoughCalResult.Rows.Count - 1;
                            dgvTestResult.Refresh();
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public bool SaveTestData(DataGridView dgv)
        {
            try
            {
                string[] BoxHead = new string[11] { "序号", "设备类型", "设备SN", "测试类型", "设置值", "万用表读值", "设备采样值", "误差1", "误差2", "允许误差", "是否合格" };
                if (AloneSave)
                {
                    string[,] strInfo = new string[5, 2];
                    strInfo[0, 0] = "设备类型:";
                    strInfo[0, 1] = testinfo.DeviceType;
                    strInfo[1, 0] = "设备SN:";
                    strInfo[1, 1] = testinfo.BoxNum;
                    strInfo[2, 0] = "校准/点检日期:";
                    strInfo[2, 1] = DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss");
                    strInfo[3, 0] = "校准/点检部门:";
                    strInfo[3, 1] = testinfo.CalorCheckDepart;
                    strInfo[4, 0] = "校准/点检人:";
                    strInfo[4, 1] = testinfo.CalorCheckUser;
                    SaveFileDialog saveFile = new SaveFileDialog();
                    //saveFile.Filter = "Excel files(*.xls)|*.xls";
                    saveFile.FilterIndex = 0;
                    saveFile.RestoreDirectory = true;
                    if (testinfo.Calibration)
                    {
                        saveFile.FileName = testinfo.DeviceType + "校准数据" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    }
                    else if (testinfo.check)
                    {
                        saveFile.FileName = testinfo.DeviceType + "点检数据" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    }
                    else if (testinfo.RoughCali || testinfo.TestMode.WriteCheck)
                    {
                        saveFile.FileName = testinfo.DeviceType + "粗调数据" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    }
                    else if (testinfo.IDRcheck)
                    {
                        saveFile.FileName = "IDR自检数据" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    }
                    else if (testinfo.DCIRCheck)
                    {
                        saveFile.FileName = "DCIR自检数据" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    }
                    string path = Application.StartupPath + "\\TestData" + "\\" + saveFile.FileName;
                    DataTable tbVol = GetDgvToTable(dgv);
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    HSSFSheet sheetVolt = (HSSFSheet)workbook.CreateSheet("Tester");
                    sheetVolt.DefaultColumnWidth = 15;
                    HSSFRow dataRows = null;
                    for (int i = 0; i < 5; i++)
                    {
                        dataRows = (HSSFRow)sheetVolt.CreateRow(i);
                        for (int j = 0; j < 2; j++)
                        {
                            HSSFCell cell1 = (HSSFCell)dataRows.CreateCell(j);
                            cell1.SetCellValue(strInfo[i, j]);
                        }
                    }
                    dataRows = (HSSFRow)sheetVolt.CreateRow(5);
                    //foreach (DataColumn column in tbVol.Columns)
                    //{
                    //    dataRows.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    //}
                    int x = 0;
                    foreach (var head in BoxHead)
                    {
                        dataRows.CreateCell(x).SetCellValue(head);
                        x++;
                    }
                    //填充Cur工作表的内容
                    for (int i = 0; i < tbVol.Rows.Count - 1; i++)
                    {
                        dataRows = (HSSFRow)sheetVolt.CreateRow(i + 6);
                        for (int j = 0; j < tbVol.Columns.Count; j++)
                        {
                            dataRows.CreateCell(j).SetCellValue(tbVol.Rows[i][j].ToString());
                        }
                    }
                    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fs);
                        workbook.Close();
                    }
                    return true;

                }
                else
                {
                    int AllRows = 0;
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.FilterIndex = 0;
                    saveFile.RestoreDirectory = true;
                    DataTable tbVol = GetDgvToTable(dgv);
                    if (tbVol.Rows.Count > 1)
                    {
                        saveFile.FileName = Application.StartupPath + "\\TestData" + "\\" + DateTime.Now.ToString("yyyyMMdd");
                        if (!Directory.Exists(saveFile.FileName))
                        {
                            Directory.CreateDirectory(saveFile.FileName);
                        }
                        string path = "";
                        if (testinfo.Calibration)
                            path = saveFile.FileName + "\\TestData_" + "校准数据" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
                        else if (testinfo.check)
                            path = saveFile.FileName + "\\TestData_" + "点检数据" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
                        else
                            path = saveFile.FileName + "\\TestData_" + "其他数据" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
                        if (File.Exists(path))
                        {
                            HSSFWorkbook book = new HSSFWorkbook();
                            HSSFSheet sheet = null;
                            HSSFRow AddRows = null;
                            int Num = 0;
                            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                book = new HSSFWorkbook(fs);
                                sheet = (HSSFSheet)book.GetSheet("Tester");
                                AllRows = sheet.LastRowNum + 2;
                                AddRows = (HSSFRow)sheet.CreateRow(AllRows);
                            }
                            for (int i = 0; i < tbVol.Rows.Count - 1; i++)
                            {
                                AddRows = (HSSFRow)sheet.CreateRow(i + AllRows);
                                for (int j = 0; j < tbVol.Columns.Count; j++)
                                {

                                    if (j == 0)
                                    {
                                        Num = AllRows + i;
                                        AddRows.CreateCell(j).SetCellValue(Num);
                                    }
                                    else
                                        AddRows.CreateCell(j).SetCellValue(tbVol.Rows[i][j].ToString());
                                }
                            }
                            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write))
                            {
                                book.Write(fs);
                                book.Close();
                            }
                        }
                        else
                        {
                            HSSFWorkbook workbook = new HSSFWorkbook();
                            HSSFSheet sheetVolt = (HSSFSheet)workbook.CreateSheet("Tester");
                            sheetVolt.DefaultColumnWidth = 15;
                            HSSFRow dataRows = null;
                            dataRows = (HSSFRow)sheetVolt.CreateRow(0);
                            int x = 0;
                            foreach (var head in BoxHead)
                            {
                                dataRows.CreateCell(x).SetCellValue(head);
                                x++;
                            }
                            for (int i = 0; i < tbVol.Rows.Count - 1; i++)
                            {
                                dataRows = (HSSFRow)sheetVolt.CreateRow(i + 1);
                                for (int j = 0; j < tbVol.Columns.Count; j++)
                                {
                                    dataRows.CreateCell(j).SetCellValue(tbVol.Rows[i][j].ToString());
                                }
                            }

                            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                            {
                                workbook.Write(fs);
                                workbook.Close();
                            }
                        }
                    }
                    return true;
                }
            }

            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); return false; }
        }

        private void StopCalibration_Click(object sender, EventArgs e)
        {
            try
            {
                blStop = true;
                CheckItem.StopStatus = true;
            }
            catch (Exception) { }
        }
        public void TestOverShow(bool TestResult, List<string> listResult)
        {
            try
            {
                groupBox9.Enabled = true;
                btnCalibration.Enabled = true;
                btnRoughCali.Enabled = true;
                txtSNnum.Text = "";
                txtRoughNum.Text = "";
                grpDCSourceSet.Location = new Point(8, 448);
                grpDCSourceSet.Size = new Size(489, 202);

                picTestTatus.Visible = true;
                lstShowResult.Items.Clear();
                if (TestResult)
                {
                    picTestTatus.Location = new Point(2, 17);
                    picTestTatus.Size = new Size(484, 176);
                    picTestTatus.Image = Image.FromFile(Application.StartupPath + "\\PictureFile\\Pass.png");
                }
                else
                {
                    picTestTatus.Location = new Point(2, 17);
                    picTestTatus.Size = new Size(484, 81);
                    lstShowResult.Location = new Point(3, 104);
                    lstShowResult.Size = new Size(483, 89);
                    picTestTatus.Image = Image.FromFile(Application.StartupPath + "\\PictureFile\\Fail.png");
                    foreach (string strInfo in listResult)
                    {
                        if (testinfo.Calibration || testinfo.check)
                            lstShowResult.Items.Add(strInfo);
                        else if (testinfo.RoughCali || testinfo.RoughCalicheck)
                            lstRoughCalResult.Items.Add(strInfo);
                    }
                }
                testinfo.Calibration = false;
                testinfo.RoughCali = false;
                testinfo.RoughCalicheck = false;
                testinfo.check = false;
                testinfo.ReadEEPROM = false;
                testinfo.IDRcheck = false;
            }
            catch
            {
                return;
            }
        }
        public void ShowTestResult()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.ShowTestResult));
            }
            else
            {
                if (testinfo.Calibration || testinfo.check)
                {
                    groupBox9.Enabled = false;
                    picTestTatus.Image = null;
                    picTestTatus.Visible = false;
                    grpDCSourceSet.Location = new Point(9, 180);
                    grpDCSourceSet.Size = new Size(489, 455);
                    lstShowResult.Location = new Point(3, 19);
                    lstShowResult.Size = new Size(483, 433);
                }
            }

        }
        private int rowNumber = 1;
        private void dgvChkParameter_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            int resetRowNum = 1;
            int rows = dgvChkParameter.Rows.Count;
            for (int i = 0; i < rows - 1; i++)
            {
                if (resetRowNum.ToString() != dgvChkParameter[0, i].Value.ToString())
                {
                    dgvChkParameter[0, i].Value = resetRowNum;
                }
                resetRowNum++;
                rowNumber = resetRowNum;
            }
        }
        private void dgvChkParameter_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                if (dgvChkParameter[0, row].Value == null)
                {
                    rowNumber = row + 1;
                    if (dgvChkParameter[1, row].Value != null || dgvChkParameter[2, row].Value != null || dgvChkParameter[3, row].Value != null || dgvChkParameter[4, row].Value != null)
                    {
                        dgvChkParameter[0, row].Value = rowNumber;
                        rowNumber++;
                    }
                    else return;
                }
                else return;
            }
            catch (Exception)
            { return; }
        }

        private void dgvChkParameter_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int column = e.ColumnIndex;
                if (cboCalEQMType.Text == "BAT525G")
                {
                    if (column == 2)
                    {

                        if (dgvChkParameter[1, row].Value.ToString() != "" || dgvChkParameter[1, row].Value != null)
                        {

                            string TestName = dgvChkParameter[1, row].Value.ToString();
                            switch (TestName)
                            {
                                case "充电电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Scl电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Sda电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "开路电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "编程电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:200-10000mV)" + "  示例:200|1000";
                                    break;
                                case "Cell电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Cell电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1250mA)" + "  示例:0|100";
                                    break;
                                case "放电电流3A_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-3000mA)" + "  示例:0|100";
                                    break;
                                case "放电电流30A_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:3000-30000mA)" + "  示例:3000|10000";
                                    break;
                                case "静态电流_200uA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-200uA)" + "  示例:0|10";
                                    break;
                                case "静态电流_2000uA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-2000uA)" + "  示例:0|100";
                                    break;
                                case "静态电流_2000nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-2000nA)" + "  示例:0|10";
                                    break;
                                case "静态电流_20000nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-20000nA)" + "  示例:0|500";
                                    break;
                                case "CNT静态(正)nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000nA)" + "  示例:0|1000";
                                    break;
                                case "CNT静态(负)_nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:-5000nA-0)" + "  示例:-1000|0";
                                    break;
                                case "直流内阻_mΩ":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-50mΩ)" + "  示例:0|10";
                                    break;
                                case "IDR电阻_Ω":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-3000000Ω)" + "  示例:0|100";
                                    break;
                                case "NTC电阻_Ω":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-3000000Ω)" + "  示例:0|100";
                                    break;
                                case "LV电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "LV充电电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1250mA)" + "  示例:0|100";
                                    break;
                                case "LV放电电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1250mA)" + "  示例:0|100";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
                else if (cboCalEQMType.Text == "BAT525H")
                {
                    if (column == 2)
                    {

                        if (dgvChkParameter[1, row].Value.ToString() != "" || dgvChkParameter[1, row].Value != null)
                        {

                            string TestName = dgvChkParameter[1, row].Value.ToString();
                            switch (TestName)
                            {
                                case "充电电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Scl电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Sda电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "开路电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "编程电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:200-10000mV)" + "  示例:200|1000";
                                    break;
                                case "Cell电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Cell电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1250mA)" + "  示例:0|100";
                                    break;
                                case "放电电流3A_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-3000mA)" + "  示例:0|100";
                                    break;
                                case "放电电流30A_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:3000-40000mA)" + "  示例:3000|10000";
                                    break;
                                case "静态电流_200uA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-200uA)" + "  示例:0|10";
                                    break;
                                case "静态电流_2000uA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-2000uA)" + "  示例:0|100";
                                    break;
                                case "静态电流_2000nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-2000nA)" + "  示例:0|10";
                                    break;
                                case "静态电流_20000nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-20000nA)" + "  示例:0|500";
                                    break;
                                case "CNT静态(正)nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000nA)" + "  示例:0|1000";
                                    break;
                                case "CNT静态(负)_nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:-5000nA-0)" + "  示例:-1000|0";
                                    break;
                                case "直流内阻_mΩ":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-50mΩ)" + "  示例:0|10";
                                    break;
                                case "IDR电阻_Ω":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-3000000Ω)" + "  示例:0|100";
                                    break;
                                case "NTC电阻_Ω":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-3000000Ω)" + "  示例:0|100";
                                    break;
                                case "LV电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "LV充电电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1250mA)" + "  示例:0|100";
                                    break;
                                case "LV放电电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1250mA)" + "  示例:0|100";
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
                else if (cboCalEQMType.Text == "BAT525C")
                {
                    if (column == 2)
                    {
                        if (dgvChkParameter[1, row].Value.ToString() != "" || dgvChkParameter[1, row].Value != null)
                        {

                            string TestName = dgvChkParameter[1, row].Value.ToString();
                            switch (TestName)
                            {
                                case "充电电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Sda电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "开路电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "编程电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:500-24000mV)" + "  示例:500|1000";
                                    break;
                                case "Cell电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Cell电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1200mA)" + "  示例:0|100";
                                    break;
                                case "放电电流3A_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-3000mA)" + "  示例:0|100";
                                    break;
                                case "放电电流30A_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:3000-30000mA)" + "  示例:3000|10000";
                                    break;
                                case "静态电流_1000uA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1000uA)" + "  示例:0|10";
                                    break;
                                case "静态电流_1000nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1000nA)" + "  示例:0|10";
                                    break;
                                case "直流内阻_mΩ":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-300mΩ)" + "  示例:0|10";
                                    break;
                                case "IDR电阻_Ω":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1000000Ω)" + "  示例:0|100";
                                    break;
                                case "NTC电阻_Ω":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1000000Ω)" + "  示例:0|100";
                                    break;
                                case "LV电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "LV充电电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1200mA)" + "  示例:0|100";
                                    break;
                                default:
                                    break;
                            }

                        }
                        else return;
                    }
                    else
                        return;
                }
                else if (cboCalEQMType.Text == "BAT525D")
                {
                    if (column == 2)
                    {
                        if (dgvChkParameter[1, row].Value == null)
                        {
                            return;
                        }
                        else
                        {
                            string TestName = dgvChkParameter[1, row].Value.ToString();
                            switch (TestName)
                            {
                                case "充电电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Sda电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "开路电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "编程电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:500-24000mV)" + "  示例:500|1000";
                                    break;
                                case "Cell电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "Cell电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-2500mA)" + "  示例:0|100";
                                    break;
                                case "放电电流3A_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-3000mA)" + "  示例:0|100";
                                    break;
                                case "放电电流30A_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:3000-30000mA)" + "  示例:3000|10000";
                                    break;
                                case "静态电流_2000uA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-2000uA)" + "  示例:0|10";
                                    break;
                                case "静态电流_1000nA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1000nA)" + "  示例:0|10";
                                    break;
                                case "直流内阻_mΩ":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-300mΩ)" + "  示例:0|10";
                                    break;
                                case "IDR电阻_Ω":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1000000Ω)" + "  示例:0|100";
                                    break;
                                case "NTC电阻_Ω":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1000000Ω)" + "  示例:0|100";
                                    break;
                                case "LV电压_mV":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-5000mV)" + "  示例:0|100";
                                    break;
                                case "LV充电电流_mA":
                                    dgvChkParameter.Columns[column].HeaderText = "点(测试范围:0-1200mA)" + "  示例:0|100";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                        return;
                }
            }
            catch { return; }
        }
        int checkCount = 0;
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = new CheckBox();
            if (checkCount == 0)
            {
                if (chkCalibration.Checked)
                {
                    if (chkAll.Text == "校准项目\r\n全选")
                    {
                        foreach (Control control in tabPage.Controls)
                        {
                            if (tabPage == BAT525C || tabPage == BAT525D)
                            {
                                if (control is CheckBox)
                                {
                                    box = (CheckBox)control;
                                    if (box.Name == "BAT525C_chkDsgCurCalibration30A" || box.Name == "BAT525C_chkProVoltCalibration" ||
                                        box.Name == "BAT525D_chkDsgCurCalibration30A" || box.Name == "BAT525D_chkProVoltCalibration")
                                        box.Checked = false;
                                    else box.Checked = true;
                                }
                            }
                            else
                            {
                                if (control is CheckBox)
                                {
                                    box = (CheckBox)control;
                                    box.Checked = true;
                                }
                            }
                        }
                        chkAll.Text = "取消全选";
                        checkCount = 0;
                    }
                    else if (chkAll.Text == "取消全选")
                    {
                        foreach (Control control in tabPage.Controls)
                        {
                            if (control is CheckBox)
                            {
                                box = (CheckBox)control;
                                box.Checked = false;
                            }
                        }
                        chkAll.Text = "校准项目\r\n全选";
                        checkCount = 0;
                    }
                }

                else if (chkCheck.Checked)
                {
                    if (chkAll.Text == "点检项目\r\n全选")
                    {
                        if (dgvChkParameter.Rows.Count > 1)
                        {
                            int checkBoxColumnIndex = dgvChkParameter.Columns["select"].Index;
                            int NumColumnIndex = dgvChkParameter.Columns["No"].Index;
                            int TestName = dgvChkParameter.Columns["Type"].Index;
                            foreach (DataGridViewRow row in dgvChkParameter.Rows)
                            {
                                if (cboCalEQMType.Text == "BAT525C" || cboCalEQMType.Text == "BAT525D")
                                {
                                    if (row.Cells[NumColumnIndex].Value == null)
                                        continue;
                                    else if (row.Cells[TestName].Value.ToString() == "放电电流30A_mA")
                                        row.Cells[checkBoxColumnIndex].Value = false;
                                    else row.Cells[checkBoxColumnIndex].Value = true;
                                }
                                else
                                {
                                    if (row.Cells[NumColumnIndex].Value == null)
                                        continue;
                                    else row.Cells[checkBoxColumnIndex].Value = true;
                                }

                            }
                            chkAll.Text = "取消全选";
                            checkCount = 0;
                        }
                        else
                        {
                            MessageBox.Show("点检参数为空,请先添加!");
                            checkCount++;
                            chkAll.Checked = false;
                        }
                    }
                    else if (chkAll.Text == "取消全选")
                    {
                        if (dgvChkParameter.Rows.Count > 1)
                        {
                            int checkBoxColumnIndex = dgvChkParameter.Columns["select"].Index;
                            int NumColumnIndex = dgvChkParameter.Columns["No"].Index;
                            foreach (DataGridViewRow row in dgvChkParameter.Rows)
                            {
                                if (row.Cells[NumColumnIndex].Value == null)
                                {
                                    continue;

                                }
                                else row.Cells[checkBoxColumnIndex].Value = false;

                            }
                            chkAll.Text = "点检项目\r\n全选";
                            checkCount = 0;
                        }
                        else return;
                    }
                }
                else if (chkCalibration.Checked == false)
                {
                    if (chkAll.Text == "取消全选")
                    {
                        foreach (Control control in tabPage.Controls)
                        {
                            if (control is CheckBox)
                            {
                                box = (CheckBox)control;
                                box.Checked = false;
                            }
                        }
                        chkAll.Text = "校准项目\r\n全选";
                        checkCount = 0;
                    }
                    else
                    {
                        checkCount++;
                        chkAll.Checked = false;
                        return;
                    }
                }
                else
                {
                    checkCount++;
                    chkAll.Checked = false;

                }

            }
            else
            {
                checkCount = 0;
                return;
            }
        }
        private void chkCalibration_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCalibration.Checked)
            {
                chkCheck.Checked = false;
                chkCheck.Enabled = false;
                chkAll.Text = "校准项目\r\n全选";
                chkAll.Enabled = true;
            }
            else
            {
                chkCheck.Enabled = true;
                chkAll.Enabled = true;
                chkAll.Checked = false;
            }
        }

        private void chkCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheck.Checked)
            {
                chkCalibration.Checked = false;
                chkCalibration.Enabled = false;
                //chkAll.Text= "点检项目\r\n全选";
                chkAll.Checked = false;
                chkAll.Enabled = true;
                CheckBox box = new CheckBox();
                if (chkAll.Text == "取消全选")
                {
                    foreach (Control control in tabPage.Controls)
                    {
                        if (control is CheckBox)
                        {
                            box = (CheckBox)control;
                            box.Checked = false;
                        }
                    }
                    chkAll.Text = "点检项目\r\n全选";
                }
                else chkAll.Text = "点检项目\r\n全选";
            }
            else
            {
                chkCalibration.Enabled = true;
                chkAll.Checked = false;
                //chkAll.Enabled = true;
            }
        }

        int Counts = 0;

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (cboTesterType.Text != "")
                {
                    switch (cboTesterType.Text)
                    {
                        case "BAT525G":
                            tabPageRough = RoughBAT525G;
                            tabPageRough.Text = "BAT525G";
                            break;
                        case "BAT525H":
                            tabPageRough = RoughBAT525G;
                            tabPageRough.Text = "BAT525H";
                            break;
                    }
                }
                else
                {
                    chkReadEE.Enabled = false;
                    chkReadAllEE.Enabled = false;
                    chkRoughAllWrite.Enabled = false;
                    chkRoughWrite.Enabled = false;
                    chkWriteCheck.Enabled = false;
                    int tabpageCount = tabSelectDevice.TabPages.Count;
                    for (int i = tabpageCount; i == tabSelectDevice.TabPages.Count; i--)
                    {
                        if (i > 1)
                            tabSelectDevice.TabPages.RemoveAt(i - 1);
                        else
                        {
                            tabSelectDevice.SelectedIndex = 0;
                            break;
                        }

                    }
                }
            }
            else return;
        }

        private void cboTesterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (intRoughCount == 0)//说明是第一次选择设备型号
            {
                dgvRoughCalResult.Rows.Clear();
                strSelectType = cboTesterType.Text;
                switch (strSelectType)
                {
                    case "BAT525G":
                        tabPageRough = RoughBAT525G;
                        tabPageRough.Text = "BAT525G";
                        break;
                    case "BAT525H":
                        tabPageRough = RoughBAT525G;
                        tabPageRough.Text = "BAT525H";
                        break;
                    default:
                        break;
                }
                tabSelectDevice.TabPages.RemoveAt(0);
                tabSelectDevice.TabPages.Add(tabPageRough);
                chkReadEE.Enabled = true;
                chkReadAllEE.Enabled = true;
                chkRoughAllWrite.Enabled = true;
                chkRoughWrite.Enabled = true;
                chkWriteCheck.Enabled = true;
                chkIDRcheck.Enabled = true;
                chkDCIRcheck.Enabled = true;
                intRoughCount++;
                txtRoughType.Text = cboTesterType.Text;
                cboAccSetEQM.Text = cboTesterType.Text;
            }
            else//更改设备类型
            {
                if (strSelectType == cboTesterType.Text)//说明设备类型没有改变
                {
                    //无需做出改变
                    intRoughCount++;
                }
                else
                {
                    chkReadEE.Enabled = true;
                    chkReadAllEE.Enabled = true;
                    chkRoughAllWrite.Enabled = true;
                    chkRoughWrite.Enabled = true;
                    chkWriteCheck.Enabled = true;
                    strSelectType = cboTesterType.Text;
                    dgvRoughCalResult.Rows.Clear();
                    switch (strSelectType)
                    {
                        case "BAT525G":
                            tabPageRough = RoughBAT525G;
                            tabPageRough.Text = "BAT525G";
                            break;
                        case "BAT525H":
                            tabPageRough = RoughBAT525G;
                            tabPageRough.Text = "BAT525H";
                            break;
                        default:
                            break;
                    }
                    tabSelectDevice.TabPages.RemoveAt(0);
                    tabSelectDevice.TabPages.Add(tabPageRough);
                    intRoughCount++;
                    txtRoughType.Text = cboTesterType.Text;
                    cboAccSetEQM.Text = cboTesterType.Text;
                }
            }
        }

        private void chkReadEE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReadEE.Checked)
            {
                chkReadAllEE.Checked = false;
                chkReadAllEE.Enabled = false;
                chkRoughAllWrite.Checked = false;
                chkRoughAllWrite.Enabled = false;
                chkRoughWrite.Checked = false;
                chkRoughWrite.Enabled = false;
                chkWriteCheck.Checked = false;
                chkWriteCheck.Enabled = false;
            }
            else
            {
                chkReadAllEE.Enabled = true;
                chkRoughAllWrite.Enabled = true;
                chkRoughWrite.Enabled = true;
                chkWriteCheck.Enabled = true;
            }
        }

        private void chkReadAllEE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReadAllEE.Checked)
            {
                chkReadEE.Checked = false;
                chkReadEE.Enabled = false;
                chkRoughAllWrite.Checked = false;
                chkRoughAllWrite.Enabled = false;
                chkRoughWrite.Checked = false;
                chkRoughWrite.Enabled = false;
                chkWriteCheck.Enabled = false;
                CheckBox box = new CheckBox();
                foreach (Control control in tabPageRough.Controls)
                {
                    if (control is CheckBox)
                    {
                        box = (CheckBox)control;
                        box.Checked = true;
                    }
                }
            }
            else
            {
                chkReadEE.Enabled = true;
                chkRoughAllWrite.Enabled = true;
                chkRoughWrite.Enabled = true;
                chkWriteCheck.Enabled = true;
                CheckBox box = new CheckBox();
                foreach (Control control in tabPageRough.Controls)
                {
                    if (control is CheckBox)
                    {
                        box = (CheckBox)control;
                        box.Checked = false;
                    }
                }
            }
        }

        private void chkRoughAllWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRoughAllWrite.Checked)
            {
                if (txtConfigFile.Text == "")
                {
                    MessageBox.Show("校准文件路径配置不能为空!");
                    chkRoughAllWrite.Checked = false;
                }
                else
                {
                    chkRoughWrite.Checked = false;
                    chkRoughWrite.Enabled = false;
                    chkReadEE.Checked = false;
                    chkReadEE.Enabled = false;
                    chkReadAllEE.Checked = false;
                    chkReadAllEE.Enabled = false;

                    CheckBox box = new CheckBox();
                    foreach (Control control in tabPageRough.Controls)
                    {
                        if (control is CheckBox)
                        {
                            box = (CheckBox)control;
                            box.Checked = true;
                        }
                    }
                }
            }
            else
            {
                chkRoughWrite.Enabled = true;
                chkReadEE.Enabled = true;
                chkReadAllEE.Enabled = true;

                CheckBox box = new CheckBox();
                foreach (Control control in tabPageRough.Controls)
                {
                    if (control is CheckBox)
                    {
                        box = (CheckBox)control;
                        box.Checked = false;
                    }
                }
            }
        }

        private void chkRoughWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRoughWrite.Checked)
            {
                if (txtConfigFile.Text == "")
                {
                    MessageBox.Show("校准文件路径配置不能为空!");
                    chkRoughWrite.Checked = false;
                }
                else
                {
                    chkRoughAllWrite.Checked = false;
                    chkRoughAllWrite.Enabled = false;
                    chkReadEE.Checked = false;
                    chkReadEE.Enabled = false;
                    chkReadAllEE.Checked = false;
                    chkReadAllEE.Enabled = false;
                }
            }
            else
            {
                chkRoughAllWrite.Enabled = true;
                chkReadEE.Enabled = true;
                chkReadAllEE.Enabled = true;
            }
        }
        public bool JudgeTestItem()
        {
            if (chkReadEE.Checked || chkRoughWrite.Checked)//这两项测试先判断有没有勾选
            {
                bool judge = false;//false=不勾选
                CheckBox box = new CheckBox();
                foreach (Control control in tabPageRough.Controls)
                {
                    if (control is CheckBox)
                    {
                        box = (CheckBox)control;
                        if (box.Checked == true)
                            judge = true;
                        else
                            continue;
                    }
                }
                if (!judge)
                {
                    MessageBox.Show("请勾选测试项!");
                    return false;
                }
            }
            return true;
        }

        private void btnFileAddr_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.InitialDirectory = Application.StartupPath + "\\粗调配置文件";
                openFile.Title = "选择校准配置文件";
                openFile.Filter = "文本文件 (*.txt)| *.txt";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    txtConfigFile.Text = openFile.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void UpdateSNNum(string time, string num)
        {
            try
            {
                if (txtSNnum.InvokeRequired)
                {
                    txtDateTime.Invoke(new Action<string>(y =>
                    {
                        txtDateTime.Text = y;
                    }), time);
                    txtSNnum.Invoke(new Action<string>(x =>
                    {
                        txtSNnum.Text = x;
                    }), num);
                }
                else
                {
                    txtDateTime.Text = time;
                    txtSNnum.Text = num;
                }
            }
            catch
            {
                return;
            }
        }
        private void UpdateListBox(string item)
        {
            if (testinfo.RoughCali || testinfo.RoughCalicheck)
            {
                if (lstRoughCalResult.InvokeRequired)
                {
                    lstRoughCalResult.Invoke(new Action<string>(x =>
                    {
                        lstRoughCalResult.DrawMode = DrawMode.OwnerDrawFixed;
                        lstRoughCalResult.Items.Add(x);
                        lstRoughCalResult.TopIndex = lstRoughCalResult.Items.Count - (int)(lstRoughCalResult.Height / lstRoughCalResult.ItemHeight);
                        lstRoughCalResult.DrawItem += new DrawItemEventHandler(LstRoughCalResult_DrawItem);
                        lstRoughCalResult.SelectedIndexChanged += new EventHandler(this.lstRoughSelectedIndexChanged);
                    }), item);
                }
                else
                {
                    lstRoughCalResult.DrawMode = DrawMode.OwnerDrawFixed;
                    lstRoughCalResult.Items.Add(item);
                    lstRoughCalResult.TopIndex = lstRoughCalResult.Items.Count - (int)(lstRoughCalResult.Height / lstRoughCalResult.ItemHeight);
                    lstRoughCalResult.DrawItem += new DrawItemEventHandler(LstRoughCalResult_DrawItem);
                    lstRoughCalResult.SelectedIndexChanged += new EventHandler(this.lstRoughSelectedIndexChanged);
                }
            }
            else if (testinfo.Calibration || testinfo.check)
            {
                if (lstShowResult.InvokeRequired)
                {
                    lstShowResult.Invoke(new Action<string>(x =>
                    {
                        lstShowResult.DrawMode = DrawMode.OwnerDrawFixed;
                        lstShowResult.Items.Add(x);
                        lstShowResult.TopIndex = lstShowResult.Items.Count - (int)(lstShowResult.Height / lstShowResult.ItemHeight);
                        lstShowResult.DrawItem += new DrawItemEventHandler(LstShowResult_DrawItem);
                        lstShowResult.SelectedIndexChanged += new EventHandler(this.LstShowSelectedIndexChanged);

                    }), item);
                }
                else
                {
                    lstShowResult.DrawMode = DrawMode.OwnerDrawFixed;
                    lstShowResult.Items.Add(item);
                    lstShowResult.TopIndex = lstShowResult.Items.Count - (int)(lstShowResult.Height / lstShowResult.ItemHeight);
                    lstShowResult.DrawItem += new DrawItemEventHandler(LstShowResult_DrawItem);
                    lstShowResult.SelectedIndexChanged += new EventHandler(this.LstShowSelectedIndexChanged);
                }
            }
        }

        private void LstShowResult_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            Color[] colors = { Color.Red, Color.Green, Color.Black }; // 对应不同的行
            Brush brush = new SolidBrush(colors[2]);
            if (e.Index < 0 || e.Index >= listBox.Items.Count)
                return;

            e.DrawBackground(); // 绘制背景
            string str = listBox.Items[e.Index].ToString();
            // 设置字体颜色
            if (str.Contains("NG") || str.Contains("失败"))
            {
                brush = new SolidBrush(colors[0]);
            }
            else if (str.Contains("OK"))
            {
                brush = new SolidBrush(colors[1]);
            }
            // 绘制文本
            e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
            e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, brush, new PointF(e.Bounds.X, e.Bounds.Y));
            brush.Dispose();
            // 默认情况下，我们不绘制文本
            if (e.Index == listBox.SelectedIndex)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds);
                e.Graphics.DrawRectangle(new Pen(Color.Black), e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));
            }
            e.DrawFocusRectangle();
        }

        private void LstRoughCalResult_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            listBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            Color[] colors = { Color.Red, Color.Green, Color.Black }; // 对应不同的行
            Brush brush = new SolidBrush(colors[2]);
            if (e.Index < 0 || e.Index >= listBox.Items.Count)
                return;
            e.DrawBackground(); // 绘制背景
            string str = listBox.Items[e.Index].ToString();
            // 设置字体颜色
            if (str.Contains("NG") || str.Contains("失败"))
            {
                brush = new SolidBrush(colors[0]);
            }
            else if (str.Contains("OK"))
            {
                brush = new SolidBrush(colors[1]);
            }
            // 绘制文本
            e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
            e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, brush, new PointF(e.Bounds.X, e.Bounds.Y));
            // 重置刷子
            brush.Dispose();
            if (e.Index == listBox.SelectedIndex)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds);
                e.Graphics.DrawRectangle(new Pen(Color.Black), e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));
            }
            // 默认情况下，我们不绘制文本
            e.DrawFocusRectangle();
        }
        private void lstRoughSelectedIndexChanged(object sender, EventArgs e)
        {
            // 强制ListBox重绘
            this.Invalidate();
        }
        private void LstShowSelectedIndexChanged(object sender, EventArgs e)
        {
            // 强制ListBox重绘
            this.Invalidate();
        }
        private void dgvChkParameter_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvChkParameter.IsCurrentCellDirty) { dgvChkParameter.CommitEdit(DataGridViewDataErrorContexts.Commit); }
        }
        int counts = 0;
        private void dgvChkParameter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int column = e.ColumnIndex;
                if (cboCalEQMType.Text == "BAT525C" || cboCalEQMType.Text == "BAT525D")
                {
                    if (column == 4)
                    {
                        if ((bool)dgvChkParameter[column, row].EditedFormattedValue == true)
                        {

                            if (dgvChkParameter[1, row].Value != null)
                            {
                                string TestName = dgvChkParameter[1, row].Value.ToString();
                                if (TestName == "放电电流30A_mA")
                                {
                                    if (chkZXSource.Checked == false)
                                    {
                                        if (counts == 0)
                                        {
                                            counts++;
                                            dgvChkParameter.Rows[row].Cells[4].Value = false;
                                            MessageBox.Show("请确认连接兆信电源已连接!");
                                            return;
                                        }
                                        else
                                        {
                                            counts = 0;
                                            return;
                                        }
                                    }

                                }
                            }
                        }

                    }
                }
            }
            catch { return; }
        }

        private void chkIDRcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (txtMultimeterAddress.Text == "" || txtMultimeterAddress.Text == null)
            {
                if (chkIDRcheck.Checked)
                {
                    chkIDRcheck.Checked = false;
                    if (Counts == 0)
                    {
                        DialogResult result = MessageBox.Show("请连接万用表!", "提示", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                            Counts++;
                    }
                    else Counts = 0;
                }
                else Counts = 0;
            }
        }

        private void btnUnLock_Click(object sender, EventArgs e)
        {
            string seal = "sbgc201510";
            try
            {
                if (!String.IsNullOrEmpty(txtSeal.Text))
                {
                    if (txtSeal.Text == seal)
                    {
                        dgvACCset.Enabled = true;
                        btnUnLock.Text = "解锁成功";
                        btnUnLock.BackColor = Color.Green;
                    }
                }
                else
                {
                    MessageBox.Show("请输入解锁密码!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public bool SaveACCdata(string strPath, DataGridView dgv)
        {
            string data = "";
            string headName = "测试项目,校准精度设置,点检精度设置";
            try
            {
                if (File.Exists(strPath))
                {
                    File.SetAttributes(strPath, File.GetAttributes(strPath) & ~FileAttributes.ReadOnly);
                    File.Delete(strPath);
                    FileStream fs = new FileStream(strPath, FileMode.Create);
                    fs.Close();
                    fs.Dispose();
                }
                else
                {
                    FileStream fs = new FileStream(strPath, FileMode.Create);
                    fs.Close();
                    fs.Dispose();
                }
                using (FileStream fileStream = new FileStream(strPath, FileMode.Open, FileAccess.Write))
                {
                    Encoding encode = UTF8Encoding.UTF8;
                    StreamWriter sw = new StreamWriter(fileStream, encode);
                    sw.WriteLine(headName);
                    sw.Flush();
                    for (int i = 0; i < dgv.Rows.Count - 1; i++)
                    {
                        data = dgv[0, i].Value.ToString() + "," + dgv[1, i].Value.ToString() + "," + dgv[2, i].Value.ToString();
                        sw.WriteLine(data);
                        sw.Flush();
                    }
                    File.SetAttributes(strPath, File.GetAttributes(strPath) | FileAttributes.ReadOnly);
                    sw.Close();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        private void LoadAccParameter(string Path)
        {
            CaliACC.Clear();
            CheckACC.Clear();
            dgvACCset.Columns[2].HeaderText = "点检精度设置";
            if (dgvACCset.Rows.Count >= 2)
            {
                dgvACCset.Rows.Clear();
            }
            try
            {
                string[] strItems = File.ReadAllLines(Path);
                for (int i = 1; i < strItems.Length; i++)
                {
                    dgvACCset.Rows.Add();
                    string[] strData = strItems[i].Split(',');
                    for (int j = 0; j < strData.Length; j++)
                    {
                        dgvACCset[j, i - 1].Value = strData[j];
                    }
                    CaliACC.Add(strData[0], Convert.ToDouble(strData[1]));
                    CheckACC.Add(strData[0], Convert.ToDouble(strData[2].TrimEnd('%')));
                }

            }
            catch (Exception)
            {
                return;
            }
        }
        private void btnSaveAcc_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + @"\精度数据配置" + "\\" + cboAccSetEQM.Text + ".txt";
                if (SaveACCdata(path, dgvACCset))
                {
                    LoadAccParameter(path);//更新参数
                    MessageBox.Show("保存成功!");
                    dgvACCset.Enabled = false;
                    txtSeal.Text = "";
                    btnUnLock.Text = "解锁";
                    btnUnLock.BackColor = Color.Red;
                }
                else
                {
                    MessageBox.Show("保存失败!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cboAccSetEQM_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string Path = Application.StartupPath + @"\精度数据配置" + "\\" + cboAccSetEQM.Text + ".txt";
                dgvACCset.Rows.Clear();
                Column1.Items.Clear();
                switch (cboAccSetEQM.Text)
                {
                    case "BAT525G":
                        Column1.Items.AddRange("充电电压_mV", "开路电压_mV", "端口电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                            "静态电流_200uA", "静态电流_2000uA", "静态电流_2000nA", "静态电流_20000nA", "CNT静态(正)_nA", "CNT静态(负)_nA", "直流内阻_mΩ",
                                            "NTC_2KΩ", "NTC_20KΩ", "NTC_200KΩ", "NTC_3000KΩ", "LV电压_mV", "LV放电电流_mA");

                        break;
                    case "BAT525H":
                        Column1.Items.AddRange("充电电压_mV", "开路电压_mV", "端口电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                            "静态电流_200uA", "静态电流_2000uA", "静态电流_2000nA", "静态电流_20000nA", "CNT静态(正)_nA", "CNT静态(负)_nA", "直流内阻_mΩ",
                                            "NTC_2KΩ", "NTC_20KΩ", "NTC_200KΩ", "NTC_3000KΩ", "LV电压_mV", "LV放电电流_mA");

                        break;
                    case "BAT525C":
                        Column1.Items.AddRange("充电电压_mV", "开路电压_mV", "端口电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                            "静态电流_1000uA", "静态电流_1000nA", "直流内阻_mΩ", "NTC_1KΩ", "NTC_10KΩ", "NTC_100KΩ", "NTC_1000KΩ", "IDR_1KΩ", "IDR_10KΩ", "IDR_100KΩ", "IDR_1000KΩ",
                                            "LV电压_mV", "LV充电电流_mA");
                        break;
                    case "BAT525D":
                        Column1.Items.AddRange("充电电压_mV", "开路电压_mV", "端口电压_mV", "编程电压_mV", "Cell电压_mV", "Cell电流_mA", "放电电流3A_mA", "放电电流30A_mA",
                                            "静态电流D_2000uA", "静态电流_1000nA", "直流内阻_mΩ", "NTC_1KΩ", "NTC_10KΩ", "NTC_100KΩ", "NTC_1000KΩ", "IDR_1KΩ", "IDR_10KΩ", "IDR_100KΩ", "IDR_1000KΩ",
                                            "LV电压_mV", "LV充电电流_mA");
                        break;
                }
                if (File.Exists(Path))
                {
                    LoadAccParameter(Path);
                    dgvACCset.Enabled = false;
                }
                else
                {
                    MessageBox.Show("无匹配的精度文件,请先添加!");
                    return;

                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void btnImportParam_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Title = "选择导入测试参数文件";
                //file.Filter = "CSV Files (*.csc)| *.csv|*.txt";
                file.Filter = "文本文件(*.txt)|*.txt";
                file.InitialDirectory = Application.StartupPath + @"\点检参数配置";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    string FileName = Path.GetFileNameWithoutExtension(file.FileName);
                    if (FileName.ToUpper() == cboCalEQMType.Text)
                    {
                        txtChkParameterAddr.Text = file.FileName;
                        strLoadParamPath = txtChkParameterAddr.Text;
                        LoadTestItem();
                    }
                    else if (cboCalEQMType.Text == "BAT525H")
                    {
                        if (FileName.ToUpper() == "BAT525G")
                        {
                            txtChkParameterAddr.Text = file.FileName;
                            strLoadParamPath = txtChkParameterAddr.Text;
                            LoadTestItem();

                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择对应的设备型号点检参数!" + cboCalEQMType.Text);
                    }
                }
            }
            catch (Exception ex) { ex.Message.ToString(); }
        }

        private void cboMultimeterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboDMMcom.Items.Clear();
            try
            {
                if (cboMultimeterType.Text == "34401A")
                {
                    label3.Visible = true;
                    cboDMMcom.Visible = true;
                    string[] strPorts = System.IO.Ports.SerialPort.GetPortNames();
                    foreach (var name in strPorts)
                    {
                        cboDMMcom.Items.Add(name);
                    }
                }
                else
                {
                    label3.Visible = false;
                    cboDMMcom.Visible = false;
                }
            }
            catch
            {
                return;
            }
        }
        //Thread thread;
        private int ShowMessage(string sMsg, int nSecondCount)
        {
            // 创建一个线程来执行倒计时操作
            AutoClose = false;
            Thread thread = new Thread(() =>
            {
                // 倒计时3秒
                Thread.Sleep(nSecondCount * 1000);

                // 关闭MessageBox
                if (InvokeRequired)
                {
                    Invoke(new Action(() => { CloseMessageBox(); }));
                }
                else
                {
                    CloseMessageBox();
                }
            });
            // 启动线程
            thread.Start();
            // 弹出MessageBox提示框，注意：这里的标题必须与下方查找关闭MessageBox里的标题一致。
            DialogResult dr = MessageBox.Show(sMsg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dr != DialogResult.OK || AutoClose == true)
                return 0;
            else
                return 1;
        }
        private void CloseMessageBox()
        {
            // 查找并关闭MessageBox窗口
            IntPtr hwnd = FindWindow(null, "提示");//一致
            if (hwnd != IntPtr.Zero)
            {
                SendMessage(hwnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            AutoClose = true;
        }

        private void chkZXSource_CheckedChanged(object sender, EventArgs e)
        {
            if (chkZXSource.Checked)
            {
                if (cboCalEQMType.Text == "BAT525C")
                    BAT525C_chkDsgCurCalibration30A.Enabled = true;
                else if (cboCalEQMType.Text == "BAT525D")
                    BAT525D_chkDsgCurCalibration30A.Enabled = true;
            }
            else
            {
                if (cboCalEQMType.Text == "BAT525C")
                {
                    BAT525C_chkDsgCurCalibration30A.Checked = false;
                    BAT525C_chkDsgCurCalibration30A.Enabled = false;
                }
                else if (cboCalEQMType.Text == "BAT525D")
                {
                    BAT525D_chkDsgCurCalibration30A.Checked = false;
                    BAT525D_chkDsgCurCalibration30A.Enabled = false;

                }
            }
        }

        private bool HasValueGreaterThanThreshold(string[] array, int threshold)
        {
            int intValue = 0;
            try
            {
                foreach (string value in array)
                {
                    if (int.TryParse(value, out intValue) && Math.Abs(intValue) > Math.Abs(threshold))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        Dictionary<string, int> nameToValueMap_BAT525G = new Dictionary<string, int>
        {
            {"充电电压_mV" ,5000},
            {"放电电流3A_mA",3000 },
            {"放电电流30A_mA",30000 },
            {"开路电压_mV",5000 },
            {"编程电压_mV",10000 },
            {"Cell电压_mV",5000 },
            {"Cell电流_mA",1250 },
            {"静态电流_200uA",200 },
            {"静态电流_2000uA",2000 },
            {"静态电流_2000nA" ,2000},
            {"静态电流_20000nA",20000 },
            {"CNT静态(正)_nA",5000 },
            {"CNT静态(负)_nA",-5000 },
            {"直流内阻_mΩ",50 },
            {"IDR电阻_Ω",3000000 },
            {"NTC电阻_Ω",3000000 },
            {"LV电压_mV",5000 },
            { "LV放电电流_mA",1250 },
        };
        Dictionary<string, int> nameToValueMap_BAT525H = new Dictionary<string, int>
        {
            {"充电电压_mV" ,5000},
            {"放电电流3A_mA",3000 },
            {"放电电流30A_mA",40000 },
            {"开路电压_mV",5000 },
            {"编程电压_mV",10000 },
            {"Cell电压_mV",5000 },
            {"Cell电流_mA",1250 },
            {"静态电流_200uA",200 },
            {"静态电流_2000uA",2000 },
            {"静态电流_2000nA" ,2000},
            {"静态电流_20000nA",20000 },
            {"CNT静态(正)_nA",5000 },
            {"CNT静态(负)_nA",-5000 },
            {"直流内阻_mΩ",50 },
            {"IDR电阻_Ω",3000000 },
            {"NTC电阻_Ω",3000000 },
            {"LV电压_mV",5000 },
            { "LV放电电流_mA",1250 },
        };
        Dictionary<string, int> nameToValueMap_BAT525C = new Dictionary<string, int>
        {
            {"充电电压_mV" ,5000},
            {"放电电流3A_mA",3000 },
            {"放电电流30A_mA",30000 },
            {"开路电压_mV",5000 },
            {"端口电压_mV",5000 },
            {"编程电压_mV",24000 },
            {"Cell电压_mV",5000 },
            {"Cell电流_mA",1200 },
            {"静态电流_1000uA",1000 },
            {"静态电流_1000nA" ,1000},
            {"直流内阻_mΩ",300 },
            {"IDR电阻_Ω",1000000 },
            {"NTC电阻_Ω",1000000 },
            {"LV电压_mV",5000 },
            { "LV充电电流_mA",1200 },
        };
        Dictionary<string, int> nameToValueMap_BAT525D = new Dictionary<string, int>
        {
            {"充电电压_mV" ,5000},
            {"放电电流3A_mA",3000 },
            {"放电电流30A_mA",30000 },
            {"开路电压_mV",5000 },
            {"端口电压_mV",5000 },
            {"编程电压_mV",24000 },
            {"Cell电压_mV",5000 },
            {"Cell电流_mA",2500 },
            {"静态电流D_2000uA",2000 },
            {"静态电流_1000nA" ,1000},
            {"直流内阻_mΩ",300 },
            {"IDR电阻_Ω",1000000 },
            {"NTC电阻_Ω",1000000 },
            {"LV电压_mV",5000 },
            { "LV充电电流_mA",1200 },
        };

        private void chkDCIRcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (txtMultimeterAddress.Text == "" || txtMultimeterAddress.Text == null)
            {
                if (chkDCIRcheck.Checked)
                {
                    chkDCIRcheck.Checked = false;
                    if (Counts == 0)
                    {
                        DialogResult result = MessageBox.Show("请连接万用表!", "提示", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                            Counts++;
                    }
                    else Counts = 0;
                }
                else Counts = 0;
            }
        }

        private void chkWriteSNCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWriteSNCode.Checked)
            {
                chkReadSNCodeStart.Checked = false;
                txtSNnum.MaxLength = 4;
            }
            else
            {
                txtSNnum.MaxLength = 20;
            }
        }

        private void chkReadSNCodeStart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReadSNCodeStart.Checked)
            {
                chkWriteSNCode.Checked = false;
                txtSNnum.MaxLength = 20;
            }
        }

        private void dgvACCset_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int column = e.ColumnIndex;
                if (cboAccSetEQM.Text == "BAT525G")
                {
                    if (column == 2)
                    {

                        if (dgvACCset[1, row].Value.ToString() != "" && dgvACCset[1, row].Value != null)
                        {

                            string TestName = dgvACCset[0, row].Value.ToString();
                            switch (TestName)
                            {
                                case "NTC_2KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_20KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_200KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_3000KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                default:
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置";
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
                else if (cboAccSetEQM.Text == "BAT525H")
                {
                    if (column == 2)
                    {

                        if (dgvACCset[1, row].Value.ToString() != "" && dgvACCset[1, row].Value != null)
                        {

                            string TestName = dgvACCset[0, row].Value.ToString();
                            switch (TestName)
                            {
                                case "NTC_2KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_20KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_200KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_3000KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                default:
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置";
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
                else if (cboAccSetEQM.Text == "BAT525D")
                {
                    if (column == 2)
                    {

                        if (dgvACCset[1, row].Value.ToString() != "" && dgvACCset[1, row].Value != null)
                        {

                            string TestName = dgvACCset[0, row].Value.ToString();
                            switch (TestName)
                            {
                                case "NTC_1KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_10KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_100KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_1000KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "IDR_1KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "IDR_10KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "IDR_100KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "IDR_1000KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                default:
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置";
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
                else if (cboAccSetEQM.Text == "BAT525C")
                {
                    if (column == 2)
                    {

                        if (dgvACCset[1, row].Value.ToString() != "" && dgvACCset[1, row].Value != null)
                        {

                            string TestName = dgvACCset[0, row].Value.ToString();
                            switch (TestName)
                            {
                                case "NTC_1KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_10KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_100KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "NTC_1000KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "IDR_1KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "IDR_10KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "IDR_100KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                case "IDR_1000KΩ":
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置(电阻按比例值计算)" + "  示例:0.1";
                                    InitialValue = dgvACCset[column, row].Value.ToString();
                                    break;
                                default:
                                    dgvACCset.Columns[column].HeaderText = "点检精度设置";
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
            }
            catch { return; }
        }

        private void dgvACCset_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                int column = e.ColumnIndex;
                if (cboAccSetEQM.Text == "BAT525G")
                {
                    if (column == 2)
                    {
                        if (dgvACCset[1, row].Value.ToString() != "" && dgvACCset[1, row].Value != null)
                        {
                            string TestName = dgvACCset[0, row].Value.ToString();
                            switch (TestName)
                            {
                                case "NTC_2KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_20KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_200KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_3000KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
                else if (cboAccSetEQM.Text == "BAT525H")
                {
                    if (column == 2)
                    {
                        if (dgvACCset[1, row].Value.ToString() != "" && dgvACCset[1, row].Value != null)
                        {
                            string TestName = dgvACCset[0, row].Value.ToString();
                            switch (TestName)
                            {
                                case "NTC_2KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_20KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_200KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_3000KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
                else if (cboAccSetEQM.Text == "BAT525D")
                {
                    if (column == 2)
                    {

                        if (dgvACCset[1, row].Value.ToString() != "" && dgvACCset[1, row].Value != null)
                        {

                            string TestName = dgvACCset[0, row].Value.ToString();
                            switch (TestName)
                            {
                                case "NTC_1KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_10KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_100KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_1000KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "IDR_1KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "IDR_10KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "IDR_100KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "IDR_1000KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
                else if (cboAccSetEQM.Text == "BAT525C")
                {
                    if (column == 2)
                    {

                        if (dgvACCset[1, row].Value.ToString() != "" && dgvACCset[1, row].Value != null)
                        {

                            string TestName = dgvACCset[0, row].Value.ToString();
                            switch (TestName)
                            {
                                case "NTC_1KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_10KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_100KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "NTC_1000KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "IDR_1KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "IDR_10KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "IDR_100KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                case "IDR_1000KΩ":
                                    EndValue = dgvACCset[column, row].Value.ToString();
                                    if (InitialValue != EndValue)
                                        dgvACCset[column, row].Value = EndValue.TrimEnd('%') + "%";
                                    else
                                        dgvACCset[column, row].Value = InitialValue;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                            return;
                    }
                    else
                        return;
                }
            }
            catch { return; }
        }
        private void txtSeal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUnLock.PerformClick();
            }
        }

        private void btnGetNowTime_Click(object sender, EventArgs e)
        {
            try
            {
                string nowTime = DateTime.Now.ToString("yyyyMMdd");
                txtDateTime.Text = nowTime;
            }
            catch
            {
                return;
            }
        }

        //private void initializeCheckReport()//初始化读取点检报告
        //{
        //    try
        //    {
        //        CheckReportPath = Application.StartupPath + "\\BAT525H点检报告.xlsx";
        //        using (FileStream fileStream = new FileStream(CheckReportPath, FileMode.Open, FileAccess.Read))
        //        {
        //            xssfWorkbook = (XSSFWorkbook)WorkbookFactory.Create(fileStream);
        //        }
        //        xssfSheet = (XSSFSheet)xssfWorkbook.GetSheetAt(1);//确定需更改的表
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}
        //private delegate void SaveBarCodeDelegate(string BarCode);
        private void SaveBarCode(string BarCode, string Inspector, string CheckTool)
        {
            try
            {
                Action<string,string,string> actionInit = new Action<string,string,string>((string str,string str1,string str2) =>
                {
                    CheckReportPath = Application.StartupPath + "\\BAT525H点检报告模板.xlsx";
                    using (FileStream fileStream = new FileStream(CheckReportPath, FileMode.Open, FileAccess.Read))
                    {
                        xssfWorkbook = (XSSFWorkbook)WorkbookFactory.Create(fileStream);
                    }
                    //font = xssfWorkbook.CreateFont();
                    //font.FontHeightInPoints = 11;
                    //font.IsBold = true;
                    xssfSheet = (XSSFSheet)xssfWorkbook.GetSheetAt(1);
                    xssfRow = (XSSFRow)xssfSheet.GetRow(3);
                    xssfCell = (XSSFCell)xssfRow.GetCell(1);
                    xssfCell.SetCellValue(str.Substring(0, 7));
                    xssfRow2 = (XSSFRow)xssfSheet.GetRow(3);
                    xssfCell2 = (XSSFCell)xssfRow2.GetCell(4);
                    xssfCell2.SetCellValue(Convert.ToDouble(str.Substring(15, 4)));
                    xssfRow3 = (XSSFRow)xssfSheet.GetRow(3);
                    xssfCell3 = (XSSFCell)xssfRow3.GetCell(7);
                    xssfCell3.SetCellValue(Convert.ToDouble(str.Substring(7, 8)));
                    XSSFRow xssfRow4 = (XSSFRow)xssfSheet.GetRow(195);
                    XSSFCell xSSFCell4 = (XSSFCell)xssfRow4.GetCell(0);
                    xSSFCell4.SetCellValue("校验仪器：" + str2);
                    XSSFRow xssfRow5 = (XSSFRow)xssfSheet.GetRow(197);
                    XSSFCell xSSFCell5 = (XSSFCell)xssfRow5.GetCell(0);
                    xSSFCell5.SetCellValue("校验员：" + str1);
                    string NewFileName = str + "_出货检测报告.xlsx";
                    CheckReportPath = Application.StartupPath + @"\TestData\1.点检出货报告" + @"\" + NewFileName;
                    using (FileStream file = new FileStream(CheckReportPath, FileMode.Create, FileAccess.Write))
                    {
                        xssfWorkbook.Write(file);
                    }
                });
                this.Invoke(actionInit, BarCode, Inspector, CheckTool);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void WriteDataToReport(int[,] Cell, string DMMValue, string EQMValue)//增加委托;1.int[,] 0位是Row,1位是Cell
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (DMMValue != "")
                        {
                            xssfRow = (XSSFRow)xssfSheet.GetRow(Cell[0, 0]);
                            xssfCell = (XSSFCell)xssfRow.GetCell(Cell[0, 1]);
                            xssfCell.SetCellValue(Convert.ToDouble(DMMValue));
                        }
                        if (EQMValue != "")
                        {
                            xssfRow2 = (XSSFRow)xssfSheet.GetRow(Cell[1, 0]);
                            xssfCell2 = (XSSFCell)xssfRow2.GetCell(Cell[1, 1]);
                            xssfCell2.SetCellValue(Convert.ToDouble(EQMValue));
                        }
                        using (FileStream fileStream = new FileStream(CheckReportPath, FileMode.Open, FileAccess.Write))
                        {
                            xssfWorkbook.Write(fileStream);
                        }

                    }));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private delegate void SaveCheckReportDelegate(string BarCode, bool TestStatus);
        private void SaveCheckReport(string BarCode, bool TestStatus)
        {
            try
            {
                string newFileName = "";
                string ReportPath = "";
                if (!this.InvokeRequired)
                {
                    this.Invoke(new SaveCheckReportDelegate(SaveCheckReport), BarCode, TestStatus);
                }
                else
                {
                    if (TestStatus)
                    {
                        newFileName = BarCode + "_出货检测报告_合格.xlsx";
                    }
                    else
                    {
                        newFileName = BarCode + "_出货检测报告_不良.xlsx";
                    }
                    ReportPath = Application.StartupPath + @"\TestData\1.点检出货报告" + @"\" + newFileName;
                    if (File.Exists(ReportPath))
                    {
                        File.Delete(ReportPath);
                    }
                    File.Move(CheckReportPath, ReportPath);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
        }
    }

}
