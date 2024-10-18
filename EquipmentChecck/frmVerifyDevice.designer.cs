namespace TestSystem_Pack
{
    partial class frmVerifyDevice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVerifyDevice));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtMultimeterAddress = new System.Windows.Forms.TextBox();
            this.contextMenuStripFrom = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Open = new System.Windows.Forms.ToolStripMenuItem();
            this.Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvVoltage = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnDelect = new System.Windows.Forms.Button();
            this.txtDtID = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDtLine = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnQ = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.lstRoughCalResult = new System.Windows.Forms.ListBox();
            this.btnFileAddr = new System.Windows.Forms.Button();
            this.txtConfigFile = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkDCIRcheck = new System.Windows.Forms.CheckBox();
            this.chkIDRcheck = new System.Windows.Forms.CheckBox();
            this.chkWriteCheck = new System.Windows.Forms.CheckBox();
            this.chkReadEE = new System.Windows.Forms.CheckBox();
            this.tabSelectDevice = new System.Windows.Forms.TabControl();
            this.HomePage = new System.Windows.Forms.TabPage();
            this.RoughBAT525G = new System.Windows.Forms.TabPage();
            this.SBT825G_chkCNTStNegativeRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkCNTStPositiveRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkDCIRRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkNTCRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkStCur4RoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkStCur3RoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkStCur2RoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkStCurRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkLoadPartCurRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkLoadPartVoltRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkCellCurRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkCellVoltRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkProVoltRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkDsgCurRoughCal30A = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkDsgCurRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkOCVRoughCal = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkChgVoltRoughCal = new System.Windows.Forms.CheckBox();
            this.chkReadAllEE = new System.Windows.Forms.CheckBox();
            this.chkRoughAllWrite = new System.Windows.Forms.CheckBox();
            this.chkRoughWrite = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvRoughCalResult = new System.Windows.Forms.DataGridView();
            this.Nums = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestIDs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TestTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SetValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MulValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeviceValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Err1s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Err2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllowErr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Judge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRoughCali = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtDCIRacc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRoughType = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRoughNum = new System.Windows.Forms.TextBox();
            this.txtIDRAcc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboTesterType = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbCHPort = new System.Windows.Forms.ComboBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grpDCSourceSet = new System.Windows.Forms.GroupBox();
            this.picTestTatus = new System.Windows.Forms.PictureBox();
            this.lstShowResult = new System.Windows.Forms.ListBox();
            this.grpMultimeterSet = new System.Windows.Forms.GroupBox();
            this.cboDMMcom = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtMultimeterTest = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnMultimeterConnect = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.cboMultimeterConType = new System.Windows.Forms.ComboBox();
            this.cboMultimeterType = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.dgvTestResult = new System.Windows.Forms.DataGridView();
            this.序号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设备类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.测试ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.测试类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设置值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.万用表读值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设备采样值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.误差1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.误差2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.允许误差 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否合格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCalibration = new System.Windows.Forms.Button();
            this.btnStopCalibration = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnGetNowTime = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDateTime = new System.Windows.Forms.TextBox();
            this.chkReadSNCodeStart = new System.Windows.Forms.CheckBox();
            this.chkExtRes = new System.Windows.Forms.CheckBox();
            this.chkWriteSNCode = new System.Windows.Forms.CheckBox();
            this.chkZXSource = new System.Windows.Forms.CheckBox();
            this.chkAloneSave = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSNdeviceType = new System.Windows.Forms.TextBox();
            this.txtCalorCheckUser = new System.Windows.Forms.TextBox();
            this.txtCalorCheckDepart = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.cboSaveErrrRule = new System.Windows.Forms.ComboBox();
            this.label31 = new System.Windows.Forms.Label();
            this.cboResSpecification = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.cboCalEQMType = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cboEqmCalSerialPort = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtSNnum = new System.Windows.Forms.TextBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.chkCheck = new System.Windows.Forms.CheckBox();
            this.tabCalibrationSelect = new System.Windows.Forms.TabControl();
            this.Frist = new System.Windows.Forms.TabPage();
            this.BAT525G = new System.Windows.Forms.TabPage();
            this.SBT825G_chkCNTStNegative = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkCNTStPositive = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkDCIRCalibration = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkNTCCalibration = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkStCur4Calibration = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkStCur3Calibration = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkStCur2Calibration = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkStCurCalibration = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkLoadPartCur = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkLoadPartVolt = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkCellCur = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkCellVolt = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkProVoltCalibration = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkDsgCurCalibration30A = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkDsgCurCalibration = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkOCVCalibration = new System.Windows.Forms.CheckBox();
            this.SBT825G_chkChgVoltCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525C = new System.Windows.Forms.TabPage();
            this.BAT525C_chkIDRCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkDCIRCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkNTCCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkStCurnACalibration = new System.Windows.Forms.CheckBox();
            this.BAT525C__chkStCuruACalibration = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkLoadPartCur = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkLoadPartVolt = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkCellCur = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkCellVolt = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkProVoltCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkDsgCurCalibration30A = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkDsgCurCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkOCVCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525C_chkChgVoltCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525D = new System.Windows.Forms.TabPage();
            this.BAT525D_chkIDRCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkDCIRCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkNTCCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkStCurnACalibration = new System.Windows.Forms.CheckBox();
            this.BAT525D__chkStCuruACalibration = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkLoadPartCur = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkLoadPartVolt = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkCellCur = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkCellVolt = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkProVoltCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkDsgCurCalibration30A = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkDsgCurCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkOCVCalibration = new System.Windows.Forms.CheckBox();
            this.BAT525D_chkChgVoltCalibration = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkCalibration = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.dgvChkParameter = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cycles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnImportParam = new System.Windows.Forms.Button();
            this.txtChkParameterAddr = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.chkstcCur_ReadVol = new System.Windows.Forms.CheckBox();
            this.chkSaveLog = new System.Windows.Forms.CheckBox();
            this.btnSaveAcc = new System.Windows.Forms.Button();
            this.btnUnLock = new System.Windows.Forms.Button();
            this.txtSeal = new System.Windows.Forms.TextBox();
            this.cboAccSetEQM = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvACCset = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripFrom.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoltage)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabSelectDevice.SuspendLayout();
            this.RoughBAT525G.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoughCalResult)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.grpDCSourceSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTestTatus)).BeginInit();
            this.grpMultimeterSet.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResult)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tabCalibrationSelect.SuspendLayout();
            this.BAT525G.SuspendLayout();
            this.BAT525C.SuspendLayout();
            this.BAT525D.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChkParameter)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvACCset)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // txtMultimeterAddress
            // 
            this.txtMultimeterAddress.Location = new System.Drawing.Point(111, 23);
            this.txtMultimeterAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtMultimeterAddress.Multiline = true;
            this.txtMultimeterAddress.Name = "txtMultimeterAddress";
            this.txtMultimeterAddress.Size = new System.Drawing.Size(338, 33);
            this.txtMultimeterAddress.TabIndex = 46;
            // 
            // contextMenuStripFrom
            // 
            this.contextMenuStripFrom.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStripFrom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open,
            this.Exit});
            this.contextMenuStripFrom.Name = "contextMenuStripFrom";
            this.contextMenuStripFrom.Size = new System.Drawing.Size(109, 48);
            // 
            // Open
            // 
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(108, 22);
            this.Open.Text = "Open";
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // Exit
            // 
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(108, 22);
            this.Exit.Text = "Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Location = new System.Drawing.Point(0, 746);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1468, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1460, 716);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvVoltage);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(4, 75);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1452, 637);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // dgvVoltage
            // 
            this.dgvVoltage.AllowUserToAddRows = false;
            this.dgvVoltage.AllowUserToDeleteRows = false;
            this.dgvVoltage.AllowUserToResizeColumns = false;
            this.dgvVoltage.AllowUserToResizeRows = false;
            this.dgvVoltage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVoltage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVoltage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVoltage.Location = new System.Drawing.Point(4, 20);
            this.dgvVoltage.Margin = new System.Windows.Forms.Padding(4);
            this.dgvVoltage.Name = "dgvVoltage";
            this.dgvVoltage.ReadOnly = true;
            this.dgvVoltage.RowHeadersVisible = false;
            this.dgvVoltage.RowHeadersWidth = 72;
            this.dgvVoltage.RowTemplate.Height = 23;
            this.dgvVoltage.Size = new System.Drawing.Size(1444, 613);
            this.dgvVoltage.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnDelect);
            this.groupBox4.Controls.Add(this.txtDtID);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.txtDtLine);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.dtpStartTime);
            this.groupBox4.Controls.Add(this.btnExport);
            this.groupBox4.Controls.Add(this.btnQ);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.dtpEndTime);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(4, 4);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(1452, 71);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            // 
            // btnDelect
            // 
            this.btnDelect.Location = new System.Drawing.Point(1028, 21);
            this.btnDelect.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelect.Name = "btnDelect";
            this.btnDelect.Size = new System.Drawing.Size(88, 33);
            this.btnDelect.TabIndex = 11;
            this.btnDelect.Text = "删除";
            this.btnDelect.UseVisualStyleBackColor = true;
            this.btnDelect.Click += new System.EventHandler(this.btnDelect_Click);
            // 
            // txtDtID
            // 
            this.txtDtID.Location = new System.Drawing.Point(677, 26);
            this.txtDtID.Margin = new System.Windows.Forms.Padding(4);
            this.txtDtID.Name = "txtDtID";
            this.txtDtID.Size = new System.Drawing.Size(140, 23);
            this.txtDtID.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(606, 29);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 17);
            this.label12.TabIndex = 9;
            this.label12.Text = "TesterID:";
            // 
            // txtDtLine
            // 
            this.txtDtLine.Location = new System.Drawing.Point(472, 26);
            this.txtDtLine.Margin = new System.Windows.Forms.Padding(4);
            this.txtDtLine.Name = "txtDtLine";
            this.txtDtLine.Size = new System.Drawing.Size(126, 23);
            this.txtDtLine.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(435, 29);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 17);
            this.label11.TabIndex = 7;
            this.label11.Text = "Line:";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(49, 26);
            this.dtpStartTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(159, 23);
            this.dtpStartTime.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(932, 21);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 33);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnQ
            // 
            this.btnQ.Location = new System.Drawing.Point(838, 21);
            this.btnQ.Margin = new System.Windows.Forms.Padding(4);
            this.btnQ.Name = "btnQ";
            this.btnQ.Size = new System.Drawing.Size(88, 33);
            this.btnQ.TabIndex = 1;
            this.btnQ.Text = "查询";
            this.btnQ.UseVisualStyleBackColor = true;
            this.btnQ.Click += new System.EventHandler(this.btnQ_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(225, 29);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 17);
            this.label13.TabIndex = 5;
            this.label13.Text = "-";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(241, 26);
            this.dtpEndTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(164, 23);
            this.dtpEndTime.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 29);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 17);
            this.label15.TabIndex = 4;
            this.label15.Text = "日期:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnFileAddr);
            this.tabPage1.Controls.Add(this.txtConfigFile);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btnRoughCali);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1460, 716);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "(粗调)RoughCalibration";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Maroon;
            this.label1.Location = new System.Drawing.Point(1081, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 34);
            this.label1.TabIndex = 68;
            this.label1.Text = "读EEPROM时，文件保存默认路径；\r\n粗调写入时，此路径不能为空，需要选择配置文件";
            // 
            // lstRoughCalResult
            // 
            this.lstRoughCalResult.FormattingEnabled = true;
            this.lstRoughCalResult.ItemHeight = 17;
            this.lstRoughCalResult.Location = new System.Drawing.Point(6, 22);
            this.lstRoughCalResult.Name = "lstRoughCalResult";
            this.lstRoughCalResult.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstRoughCalResult.Size = new System.Drawing.Size(428, 395);
            this.lstRoughCalResult.TabIndex = 67;
            this.lstRoughCalResult.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstRoughCalResult_DrawItem);
            // 
            // btnFileAddr
            // 
            this.btnFileAddr.Location = new System.Drawing.Point(1036, 13);
            this.btnFileAddr.Name = "btnFileAddr";
            this.btnFileAddr.Size = new System.Drawing.Size(39, 23);
            this.btnFileAddr.TabIndex = 66;
            this.btnFileAddr.Text = "...";
            this.btnFileAddr.UseVisualStyleBackColor = true;
            this.btnFileAddr.Click += new System.EventHandler(this.btnFileAddr_Click);
            // 
            // txtConfigFile
            // 
            this.txtConfigFile.Location = new System.Drawing.Point(577, 13);
            this.txtConfigFile.Name = "txtConfigFile";
            this.txtConfigFile.Size = new System.Drawing.Size(453, 23);
            this.txtConfigFile.TabIndex = 65;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(464, 16);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(107, 17);
            this.label21.TabIndex = 64;
            this.label21.Text = "校准文件配置路径:";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.chkDCIRcheck);
            this.groupBox5.Controls.Add(this.chkIDRcheck);
            this.groupBox5.Controls.Add(this.chkWriteCheck);
            this.groupBox5.Controls.Add(this.chkReadEE);
            this.groupBox5.Controls.Add(this.tabSelectDevice);
            this.groupBox5.Controls.Add(this.chkReadAllEE);
            this.groupBox5.Controls.Add(this.chkRoughAllWrite);
            this.groupBox5.Controls.Add(this.chkRoughWrite);
            this.groupBox5.Location = new System.Drawing.Point(467, 42);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(973, 189);
            this.groupBox5.TabIndex = 63;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "粗调项目配置";
            // 
            // chkDCIRcheck
            // 
            this.chkDCIRcheck.AutoSize = true;
            this.chkDCIRcheck.Location = new System.Drawing.Point(432, 43);
            this.chkDCIRcheck.Name = "chkDCIRcheck";
            this.chkDCIRcheck.Size = new System.Drawing.Size(104, 21);
            this.chkDCIRcheck.TabIndex = 66;
            this.chkDCIRcheck.Text = "DCIR模块自检";
            this.chkDCIRcheck.UseVisualStyleBackColor = true;
            this.chkDCIRcheck.CheckedChanged += new System.EventHandler(this.chkDCIRcheck_CheckedChanged);
            // 
            // chkIDRcheck
            // 
            this.chkIDRcheck.AutoSize = true;
            this.chkIDRcheck.Location = new System.Drawing.Point(432, 17);
            this.chkIDRcheck.Name = "chkIDRcheck";
            this.chkIDRcheck.Size = new System.Drawing.Size(96, 21);
            this.chkIDRcheck.TabIndex = 65;
            this.chkIDRcheck.Text = "IDR模块自检";
            this.chkIDRcheck.UseVisualStyleBackColor = true;
            this.chkIDRcheck.CheckedChanged += new System.EventHandler(this.chkIDRcheck_CheckedChanged);
            // 
            // chkWriteCheck
            // 
            this.chkWriteCheck.AutoSize = true;
            this.chkWriteCheck.Location = new System.Drawing.Point(309, 17);
            this.chkWriteCheck.Name = "chkWriteCheck";
            this.chkWriteCheck.Size = new System.Drawing.Size(87, 21);
            this.chkWriteCheck.TabIndex = 64;
            this.chkWriteCheck.Text = "写入后点检";
            this.chkWriteCheck.UseVisualStyleBackColor = true;
            // 
            // chkReadEE
            // 
            this.chkReadEE.AutoSize = true;
            this.chkReadEE.Location = new System.Drawing.Point(10, 17);
            this.chkReadEE.Name = "chkReadEE";
            this.chkReadEE.Size = new System.Drawing.Size(90, 21);
            this.chkReadEE.TabIndex = 62;
            this.chkReadEE.Text = "读EEPROM";
            this.chkReadEE.UseVisualStyleBackColor = true;
            this.chkReadEE.CheckedChanged += new System.EventHandler(this.chkReadEE_CheckedChanged);
            // 
            // tabSelectDevice
            // 
            this.tabSelectDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSelectDevice.Controls.Add(this.HomePage);
            this.tabSelectDevice.Controls.Add(this.RoughBAT525G);
            this.tabSelectDevice.Location = new System.Drawing.Point(10, 70);
            this.tabSelectDevice.Name = "tabSelectDevice";
            this.tabSelectDevice.SelectedIndex = 0;
            this.tabSelectDevice.Size = new System.Drawing.Size(957, 103);
            this.tabSelectDevice.TabIndex = 61;
            // 
            // HomePage
            // 
            this.HomePage.Location = new System.Drawing.Point(4, 26);
            this.HomePage.Name = "HomePage";
            this.HomePage.Size = new System.Drawing.Size(949, 73);
            this.HomePage.TabIndex = 1;
            this.HomePage.UseVisualStyleBackColor = true;
            // 
            // RoughBAT525G
            // 
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkCNTStNegativeRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkCNTStPositiveRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkDCIRRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkNTCRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkStCur4RoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkStCur3RoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkStCur2RoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkStCurRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkLoadPartCurRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkLoadPartVoltRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkCellCurRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkCellVoltRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkProVoltRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkDsgCurRoughCal30A);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkDsgCurRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkOCVRoughCal);
            this.RoughBAT525G.Controls.Add(this.SBT825G_chkChgVoltRoughCal);
            this.RoughBAT525G.Location = new System.Drawing.Point(4, 26);
            this.RoughBAT525G.Name = "RoughBAT525G";
            this.RoughBAT525G.Padding = new System.Windows.Forms.Padding(3);
            this.RoughBAT525G.Size = new System.Drawing.Size(949, 73);
            this.RoughBAT525G.TabIndex = 0;
            this.RoughBAT525G.Text = "BAT525G";
            this.RoughBAT525G.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkCNTStNegativeRoughCal
            // 
            this.SBT825G_chkCNTStNegativeRoughCal.AutoSize = true;
            this.SBT825G_chkCNTStNegativeRoughCal.Location = new System.Drawing.Point(688, 40);
            this.SBT825G_chkCNTStNegativeRoughCal.Name = "SBT825G_chkCNTStNegativeRoughCal";
            this.SBT825G_chkCNTStNegativeRoughCal.Size = new System.Drawing.Size(96, 21);
            this.SBT825G_chkCNTStNegativeRoughCal.TabIndex = 16;
            this.SBT825G_chkCNTStNegativeRoughCal.Text = "CNT静态(负)";
            this.SBT825G_chkCNTStNegativeRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkCNTStPositiveRoughCal
            // 
            this.SBT825G_chkCNTStPositiveRoughCal.AutoSize = true;
            this.SBT825G_chkCNTStPositiveRoughCal.Location = new System.Drawing.Point(592, 40);
            this.SBT825G_chkCNTStPositiveRoughCal.Name = "SBT825G_chkCNTStPositiveRoughCal";
            this.SBT825G_chkCNTStPositiveRoughCal.Size = new System.Drawing.Size(96, 21);
            this.SBT825G_chkCNTStPositiveRoughCal.TabIndex = 15;
            this.SBT825G_chkCNTStPositiveRoughCal.Text = "CNT静态(正)";
            this.SBT825G_chkCNTStPositiveRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkDCIRRoughCal
            // 
            this.SBT825G_chkDCIRRoughCal.AutoSize = true;
            this.SBT825G_chkDCIRRoughCal.Location = new System.Drawing.Point(511, 40);
            this.SBT825G_chkDCIRRoughCal.Name = "SBT825G_chkDCIRRoughCal";
            this.SBT825G_chkDCIRRoughCal.Size = new System.Drawing.Size(75, 21);
            this.SBT825G_chkDCIRRoughCal.TabIndex = 14;
            this.SBT825G_chkDCIRRoughCal.Text = "直流内阻";
            this.SBT825G_chkDCIRRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkNTCRoughCal
            // 
            this.SBT825G_chkNTCRoughCal.AutoSize = true;
            this.SBT825G_chkNTCRoughCal.Location = new System.Drawing.Point(756, 13);
            this.SBT825G_chkNTCRoughCal.Name = "SBT825G_chkNTCRoughCal";
            this.SBT825G_chkNTCRoughCal.Size = new System.Drawing.Size(52, 21);
            this.SBT825G_chkNTCRoughCal.TabIndex = 13;
            this.SBT825G_chkNTCRoughCal.Text = "NTC";
            this.SBT825G_chkNTCRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkStCur4RoughCal
            // 
            this.SBT825G_chkStCur4RoughCal.AutoSize = true;
            this.SBT825G_chkStCur4RoughCal.Location = new System.Drawing.Point(380, 40);
            this.SBT825G_chkStCur4RoughCal.Name = "SBT825G_chkStCur4RoughCal";
            this.SBT825G_chkStCur4RoughCal.Size = new System.Drawing.Size(125, 21);
            this.SBT825G_chkStCur4RoughCal.TabIndex = 12;
            this.SBT825G_chkStCur4RoughCal.Text = "静态电流20000nA";
            this.SBT825G_chkStCur4RoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkStCur3RoughCal
            // 
            this.SBT825G_chkStCur3RoughCal.AutoSize = true;
            this.SBT825G_chkStCur3RoughCal.Location = new System.Drawing.Point(256, 40);
            this.SBT825G_chkStCur3RoughCal.Name = "SBT825G_chkStCur3RoughCal";
            this.SBT825G_chkStCur3RoughCal.Size = new System.Drawing.Size(118, 21);
            this.SBT825G_chkStCur3RoughCal.TabIndex = 11;
            this.SBT825G_chkStCur3RoughCal.Text = "静态电流2000nA";
            this.SBT825G_chkStCur3RoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkStCur2RoughCal
            // 
            this.SBT825G_chkStCur2RoughCal.AutoSize = true;
            this.SBT825G_chkStCur2RoughCal.Location = new System.Drawing.Point(136, 40);
            this.SBT825G_chkStCur2RoughCal.Name = "SBT825G_chkStCur2RoughCal";
            this.SBT825G_chkStCur2RoughCal.Size = new System.Drawing.Size(118, 21);
            this.SBT825G_chkStCur2RoughCal.TabIndex = 10;
            this.SBT825G_chkStCur2RoughCal.Text = "静态电流2000uA";
            this.SBT825G_chkStCur2RoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkStCurRoughCal
            // 
            this.SBT825G_chkStCurRoughCal.AutoSize = true;
            this.SBT825G_chkStCurRoughCal.Location = new System.Drawing.Point(19, 40);
            this.SBT825G_chkStCurRoughCal.Name = "SBT825G_chkStCurRoughCal";
            this.SBT825G_chkStCurRoughCal.Size = new System.Drawing.Size(111, 21);
            this.SBT825G_chkStCurRoughCal.TabIndex = 9;
            this.SBT825G_chkStCurRoughCal.Text = "静态电流200uA";
            this.SBT825G_chkStCurRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkLoadPartCurRoughCal
            // 
            this.SBT825G_chkLoadPartCurRoughCal.AutoSize = true;
            this.SBT825G_chkLoadPartCurRoughCal.Location = new System.Drawing.Point(688, 13);
            this.SBT825G_chkLoadPartCurRoughCal.Name = "SBT825G_chkLoadPartCurRoughCal";
            this.SBT825G_chkLoadPartCurRoughCal.Size = new System.Drawing.Size(65, 21);
            this.SBT825G_chkLoadPartCurRoughCal.TabIndex = 8;
            this.SBT825G_chkLoadPartCurRoughCal.Text = "LV电流";
            this.SBT825G_chkLoadPartCurRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkLoadPartVoltRoughCal
            // 
            this.SBT825G_chkLoadPartVoltRoughCal.AutoSize = true;
            this.SBT825G_chkLoadPartVoltRoughCal.Location = new System.Drawing.Point(617, 13);
            this.SBT825G_chkLoadPartVoltRoughCal.Name = "SBT825G_chkLoadPartVoltRoughCal";
            this.SBT825G_chkLoadPartVoltRoughCal.Size = new System.Drawing.Size(65, 21);
            this.SBT825G_chkLoadPartVoltRoughCal.TabIndex = 7;
            this.SBT825G_chkLoadPartVoltRoughCal.Text = "LV电压";
            this.SBT825G_chkLoadPartVoltRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkCellCurRoughCal
            // 
            this.SBT825G_chkCellCurRoughCal.AutoSize = true;
            this.SBT825G_chkCellCurRoughCal.Location = new System.Drawing.Point(539, 13);
            this.SBT825G_chkCellCurRoughCal.Name = "SBT825G_chkCellCurRoughCal";
            this.SBT825G_chkCellCurRoughCal.Size = new System.Drawing.Size(72, 21);
            this.SBT825G_chkCellCurRoughCal.TabIndex = 6;
            this.SBT825G_chkCellCurRoughCal.Text = "Cell电流";
            this.SBT825G_chkCellCurRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkCellVoltRoughCal
            // 
            this.SBT825G_chkCellVoltRoughCal.AutoSize = true;
            this.SBT825G_chkCellVoltRoughCal.Location = new System.Drawing.Point(461, 13);
            this.SBT825G_chkCellVoltRoughCal.Name = "SBT825G_chkCellVoltRoughCal";
            this.SBT825G_chkCellVoltRoughCal.Size = new System.Drawing.Size(72, 21);
            this.SBT825G_chkCellVoltRoughCal.TabIndex = 5;
            this.SBT825G_chkCellVoltRoughCal.Text = "Cell电压";
            this.SBT825G_chkCellVoltRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkProVoltRoughCal
            // 
            this.SBT825G_chkProVoltRoughCal.AutoSize = true;
            this.SBT825G_chkProVoltRoughCal.Location = new System.Drawing.Point(380, 13);
            this.SBT825G_chkProVoltRoughCal.Name = "SBT825G_chkProVoltRoughCal";
            this.SBT825G_chkProVoltRoughCal.Size = new System.Drawing.Size(75, 21);
            this.SBT825G_chkProVoltRoughCal.TabIndex = 4;
            this.SBT825G_chkProVoltRoughCal.Text = "编程电压";
            this.SBT825G_chkProVoltRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkDsgCurRoughCal30A
            // 
            this.SBT825G_chkDsgCurRoughCal30A.AutoSize = true;
            this.SBT825G_chkDsgCurRoughCal30A.Location = new System.Drawing.Point(277, 13);
            this.SBT825G_chkDsgCurRoughCal30A.Name = "SBT825G_chkDsgCurRoughCal30A";
            this.SBT825G_chkDsgCurRoughCal30A.Size = new System.Drawing.Size(97, 21);
            this.SBT825G_chkDsgCurRoughCal30A.TabIndex = 3;
            this.SBT825G_chkDsgCurRoughCal30A.Text = "放电电流30A";
            this.SBT825G_chkDsgCurRoughCal30A.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkDsgCurRoughCal
            // 
            this.SBT825G_chkDsgCurRoughCal.AutoSize = true;
            this.SBT825G_chkDsgCurRoughCal.Location = new System.Drawing.Point(181, 13);
            this.SBT825G_chkDsgCurRoughCal.Name = "SBT825G_chkDsgCurRoughCal";
            this.SBT825G_chkDsgCurRoughCal.Size = new System.Drawing.Size(90, 21);
            this.SBT825G_chkDsgCurRoughCal.TabIndex = 2;
            this.SBT825G_chkDsgCurRoughCal.Text = "放电电流3A";
            this.SBT825G_chkDsgCurRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkOCVRoughCal
            // 
            this.SBT825G_chkOCVRoughCal.AutoSize = true;
            this.SBT825G_chkOCVRoughCal.Location = new System.Drawing.Point(100, 13);
            this.SBT825G_chkOCVRoughCal.Name = "SBT825G_chkOCVRoughCal";
            this.SBT825G_chkOCVRoughCal.Size = new System.Drawing.Size(75, 21);
            this.SBT825G_chkOCVRoughCal.TabIndex = 1;
            this.SBT825G_chkOCVRoughCal.Text = "开路电压";
            this.SBT825G_chkOCVRoughCal.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkChgVoltRoughCal
            // 
            this.SBT825G_chkChgVoltRoughCal.AutoSize = true;
            this.SBT825G_chkChgVoltRoughCal.Location = new System.Drawing.Point(19, 13);
            this.SBT825G_chkChgVoltRoughCal.Name = "SBT825G_chkChgVoltRoughCal";
            this.SBT825G_chkChgVoltRoughCal.Size = new System.Drawing.Size(75, 21);
            this.SBT825G_chkChgVoltRoughCal.TabIndex = 0;
            this.SBT825G_chkChgVoltRoughCal.Text = "充电电压";
            this.SBT825G_chkChgVoltRoughCal.UseVisualStyleBackColor = true;
            // 
            // chkReadAllEE
            // 
            this.chkReadAllEE.AutoSize = true;
            this.chkReadAllEE.Location = new System.Drawing.Point(10, 43);
            this.chkReadAllEE.Name = "chkReadAllEE";
            this.chkReadAllEE.Size = new System.Drawing.Size(126, 21);
            this.chkReadAllEE.TabIndex = 63;
            this.chkReadAllEE.Text = "读全部项EEPROM";
            this.chkReadAllEE.UseVisualStyleBackColor = true;
            this.chkReadAllEE.CheckedChanged += new System.EventHandler(this.chkReadAllEE_CheckedChanged);
            // 
            // chkRoughAllWrite
            // 
            this.chkRoughAllWrite.AutoSize = true;
            this.chkRoughAllWrite.Location = new System.Drawing.Point(205, 43);
            this.chkRoughAllWrite.Name = "chkRoughAllWrite";
            this.chkRoughAllWrite.Size = new System.Drawing.Size(111, 21);
            this.chkRoughAllWrite.TabIndex = 60;
            this.chkRoughAllWrite.Text = "粗调全部项写入";
            this.chkRoughAllWrite.UseVisualStyleBackColor = true;
            this.chkRoughAllWrite.CheckedChanged += new System.EventHandler(this.chkRoughAllWrite_CheckedChanged);
            // 
            // chkRoughWrite
            // 
            this.chkRoughWrite.AutoSize = true;
            this.chkRoughWrite.Location = new System.Drawing.Point(205, 17);
            this.chkRoughWrite.Name = "chkRoughWrite";
            this.chkRoughWrite.Size = new System.Drawing.Size(75, 21);
            this.chkRoughWrite.TabIndex = 59;
            this.chkRoughWrite.Text = "粗调写入";
            this.chkRoughWrite.UseVisualStyleBackColor = true;
            this.chkRoughWrite.CheckedChanged += new System.EventHandler(this.chkRoughWrite_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvRoughCalResult);
            this.groupBox1.Location = new System.Drawing.Point(467, 238);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(973, 470);
            this.groupBox1.TabIndex = 62;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试结果显示";
            // 
            // dgvRoughCalResult
            // 
            this.dgvRoughCalResult.AllowUserToDeleteRows = false;
            this.dgvRoughCalResult.AllowUserToResizeColumns = false;
            this.dgvRoughCalResult.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvRoughCalResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRoughCalResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoughCalResult.ColumnHeadersHeight = 40;
            this.dgvRoughCalResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nums,
            this.DeviceType,
            this.TestIDs,
            this.TestTypes,
            this.SetValue,
            this.MulValue,
            this.DeviceValue,
            this.Err1s,
            this.Err2s,
            this.AllowErr,
            this.Judge});
            this.dgvRoughCalResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoughCalResult.Location = new System.Drawing.Point(4, 20);
            this.dgvRoughCalResult.Margin = new System.Windows.Forms.Padding(4);
            this.dgvRoughCalResult.Name = "dgvRoughCalResult";
            this.dgvRoughCalResult.ReadOnly = true;
            this.dgvRoughCalResult.RowHeadersVisible = false;
            this.dgvRoughCalResult.RowHeadersWidth = 72;
            this.dgvRoughCalResult.RowTemplate.Height = 23;
            this.dgvRoughCalResult.Size = new System.Drawing.Size(965, 446);
            this.dgvRoughCalResult.TabIndex = 0;
            // 
            // Nums
            // 
            this.Nums.FillWeight = 65F;
            this.Nums.HeaderText = "序号";
            this.Nums.MinimumWidth = 9;
            this.Nums.Name = "Nums";
            this.Nums.ReadOnly = true;
            this.Nums.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Nums.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DeviceType
            // 
            this.DeviceType.HeaderText = "设备型号";
            this.DeviceType.MinimumWidth = 9;
            this.DeviceType.Name = "DeviceType";
            this.DeviceType.ReadOnly = true;
            this.DeviceType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DeviceType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TestIDs
            // 
            this.TestIDs.FillWeight = 80F;
            this.TestIDs.HeaderText = "测试ID";
            this.TestIDs.MinimumWidth = 9;
            this.TestIDs.Name = "TestIDs";
            this.TestIDs.ReadOnly = true;
            this.TestIDs.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TestIDs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TestTypes
            // 
            this.TestTypes.HeaderText = "测试类型";
            this.TestTypes.MinimumWidth = 9;
            this.TestTypes.Name = "TestTypes";
            this.TestTypes.ReadOnly = true;
            this.TestTypes.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TestTypes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SetValue
            // 
            this.SetValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SetValue.HeaderText = "设置值";
            this.SetValue.MinimumWidth = 9;
            this.SetValue.Name = "SetValue";
            this.SetValue.ReadOnly = true;
            this.SetValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SetValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SetValue.Width = 69;
            // 
            // MulValue
            // 
            this.MulValue.FillWeight = 104F;
            this.MulValue.HeaderText = "万用表读值";
            this.MulValue.MinimumWidth = 9;
            this.MulValue.Name = "MulValue";
            this.MulValue.ReadOnly = true;
            this.MulValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.MulValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DeviceValue
            // 
            this.DeviceValue.FillWeight = 104F;
            this.DeviceValue.HeaderText = "设备采样值";
            this.DeviceValue.MinimumWidth = 9;
            this.DeviceValue.Name = "DeviceValue";
            this.DeviceValue.ReadOnly = true;
            this.DeviceValue.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DeviceValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Err1s
            // 
            this.Err1s.FillWeight = 71F;
            this.Err1s.HeaderText = "误差1";
            this.Err1s.MinimumWidth = 9;
            this.Err1s.Name = "Err1s";
            this.Err1s.ReadOnly = true;
            this.Err1s.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Err1s.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Err2s
            // 
            this.Err2s.FillWeight = 71F;
            this.Err2s.HeaderText = "误差2";
            this.Err2s.MinimumWidth = 9;
            this.Err2s.Name = "Err2s";
            this.Err2s.ReadOnly = true;
            this.Err2s.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Err2s.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AllowErr
            // 
            this.AllowErr.FillWeight = 90F;
            this.AllowErr.HeaderText = "允许误差";
            this.AllowErr.MinimumWidth = 9;
            this.AllowErr.Name = "AllowErr";
            this.AllowErr.ReadOnly = true;
            this.AllowErr.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AllowErr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Judge
            // 
            this.Judge.FillWeight = 90F;
            this.Judge.HeaderText = "是否合格";
            this.Judge.MinimumWidth = 9;
            this.Judge.Name = "Judge";
            this.Judge.ReadOnly = true;
            this.Judge.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Judge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnRoughCali
            // 
            this.btnRoughCali.BackColor = System.Drawing.Color.Transparent;
            this.btnRoughCali.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnRoughCali.FlatAppearance.BorderSize = 2;
            this.btnRoughCali.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnRoughCali.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnRoughCali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoughCali.Location = new System.Drawing.Point(31, 650);
            this.btnRoughCali.Margin = new System.Windows.Forms.Padding(4);
            this.btnRoughCali.Name = "btnRoughCali";
            this.btnRoughCali.Size = new System.Drawing.Size(187, 52);
            this.btnRoughCali.TabIndex = 52;
            this.btnRoughCali.Text = "启动粗调";
            this.btnRoughCali.UseVisualStyleBackColor = false;
            this.btnRoughCali.Click += new System.EventHandler(this.btnRoughCali_Click);
            // 
            // btnStop
            // 
            this.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnStop.FlatAppearance.BorderSize = 2;
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Location = new System.Drawing.Point(239, 649);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(187, 52);
            this.btnStop.TabIndex = 53;
            this.btnStop.Text = "停止测试(&S)";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtDCIRacc);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.txtRoughType);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.txtRoughNum);
            this.groupBox6.Controls.Add(this.txtIDRAcc);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.cboTesterType);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.cmbCHPort);
            this.groupBox6.Location = new System.Drawing.Point(9, 8);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(440, 193);
            this.groupBox6.TabIndex = 42;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "基本信息设置";
            // 
            // txtDCIRacc
            // 
            this.txtDCIRacc.Location = new System.Drawing.Point(140, 154);
            this.txtDCIRacc.Name = "txtDCIRacc";
            this.txtDCIRacc.Size = new System.Drawing.Size(173, 23);
            this.txtDCIRacc.TabIndex = 52;
            this.txtDCIRacc.Text = "0.01";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(222, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 17);
            this.label7.TabIndex = 69;
            this.label7.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 17);
            this.label4.TabIndex = 51;
            this.label4.Text = "DCIR模块精度:";
            // 
            // txtRoughType
            // 
            this.txtRoughType.Location = new System.Drawing.Point(139, 27);
            this.txtRoughType.Name = "txtRoughType";
            this.txtRoughType.Size = new System.Drawing.Size(81, 23);
            this.txtRoughType.TabIndex = 68;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(59, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 17);
            this.label8.TabIndex = 66;
            this.label8.Text = "设备测试SN:";
            // 
            // txtRoughNum
            // 
            this.txtRoughNum.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRoughNum.Location = new System.Drawing.Point(242, 27);
            this.txtRoughNum.Margin = new System.Windows.Forms.Padding(4);
            this.txtRoughNum.Name = "txtRoughNum";
            this.txtRoughNum.Size = new System.Drawing.Size(98, 23);
            this.txtRoughNum.TabIndex = 67;
            // 
            // txtIDRAcc
            // 
            this.txtIDRAcc.Location = new System.Drawing.Point(139, 123);
            this.txtIDRAcc.Name = "txtIDRAcc";
            this.txtIDRAcc.Size = new System.Drawing.Size(173, 23);
            this.txtIDRAcc.TabIndex = 50;
            this.txtIDRAcc.Text = "0.001";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 49;
            this.label2.Text = "IDR模块精度:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(63, 94);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 17);
            this.label9.TabIndex = 48;
            this.label9.Text = "通道串口号:";
            // 
            // cboTesterType
            // 
            this.cboTesterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTesterType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboTesterType.FormattingEnabled = true;
            this.cboTesterType.Items.AddRange(new object[] {
            "BAT525G",
            "BAT525H"});
            this.cboTesterType.Location = new System.Drawing.Point(139, 58);
            this.cboTesterType.Margin = new System.Windows.Forms.Padding(4);
            this.cboTesterType.Name = "cboTesterType";
            this.cboTesterType.Size = new System.Drawing.Size(173, 25);
            this.cboTesterType.TabIndex = 46;
            this.cboTesterType.SelectedIndexChanged += new System.EventHandler(this.cboTesterType_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(75, 64);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 17);
            this.label17.TabIndex = 45;
            this.label17.Text = "设备型号:";
            // 
            // cmbCHPort
            // 
            this.cmbCHPort.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCHPort.FormattingEnabled = true;
            this.cmbCHPort.Location = new System.Drawing.Point(139, 91);
            this.cmbCHPort.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCHPort.Name = "cmbCHPort";
            this.cmbCHPort.Size = new System.Drawing.Size(174, 25);
            this.cmbCHPort.TabIndex = 47;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lstRoughCalResult);
            this.groupBox7.Location = new System.Drawing.Point(9, 217);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(440, 426);
            this.groupBox7.TabIndex = 69;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "测试结果";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grpDCSourceSet);
            this.tabPage3.Controls.Add(this.grpMultimeterSet);
            this.tabPage3.Controls.Add(this.groupBox11);
            this.tabPage3.Controls.Add(this.progressBar1);
            this.tabPage3.Controls.Add(this.btnCalibration);
            this.tabPage3.Controls.Add(this.btnStopCalibration);
            this.tabPage3.Controls.Add(this.groupBox9);
            this.tabPage3.Controls.Add(this.groupBox14);
            this.tabPage3.Controls.Add(this.groupBox10);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1460, 716);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "(精调)Calibration";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // grpDCSourceSet
            // 
            this.grpDCSourceSet.Controls.Add(this.picTestTatus);
            this.grpDCSourceSet.Controls.Add(this.lstShowResult);
            this.grpDCSourceSet.Location = new System.Drawing.Point(8, 448);
            this.grpDCSourceSet.Name = "grpDCSourceSet";
            this.grpDCSourceSet.Size = new System.Drawing.Size(489, 202);
            this.grpDCSourceSet.TabIndex = 63;
            this.grpDCSourceSet.TabStop = false;
            this.grpDCSourceSet.Text = "测试结果";
            // 
            // picTestTatus
            // 
            this.picTestTatus.Location = new System.Drawing.Point(2, 17);
            this.picTestTatus.Name = "picTestTatus";
            this.picTestTatus.Size = new System.Drawing.Size(484, 176);
            this.picTestTatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTestTatus.TabIndex = 1;
            this.picTestTatus.TabStop = false;
            // 
            // lstShowResult
            // 
            this.lstShowResult.FormattingEnabled = true;
            this.lstShowResult.ItemHeight = 17;
            this.lstShowResult.Location = new System.Drawing.Point(3, 104);
            this.lstShowResult.Name = "lstShowResult";
            this.lstShowResult.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstShowResult.Size = new System.Drawing.Size(483, 89);
            this.lstShowResult.TabIndex = 0;
            this.lstShowResult.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstShowResult_DrawItem);
            // 
            // grpMultimeterSet
            // 
            this.grpMultimeterSet.Controls.Add(this.cboDMMcom);
            this.grpMultimeterSet.Controls.Add(this.label3);
            this.grpMultimeterSet.Controls.Add(this.groupBox2);
            this.grpMultimeterSet.Controls.Add(this.label28);
            this.grpMultimeterSet.Controls.Add(this.cboMultimeterConType);
            this.grpMultimeterSet.Controls.Add(this.cboMultimeterType);
            this.grpMultimeterSet.Controls.Add(this.label24);
            this.grpMultimeterSet.Location = new System.Drawing.Point(9, 264);
            this.grpMultimeterSet.Margin = new System.Windows.Forms.Padding(4);
            this.grpMultimeterSet.Name = "grpMultimeterSet";
            this.grpMultimeterSet.Padding = new System.Windows.Forms.Padding(4);
            this.grpMultimeterSet.Size = new System.Drawing.Size(489, 183);
            this.grpMultimeterSet.TabIndex = 48;
            this.grpMultimeterSet.TabStop = false;
            this.grpMultimeterSet.Text = "万用表连接设置";
            // 
            // cboDMMcom
            // 
            this.cboDMMcom.FormattingEnabled = true;
            this.cboDMMcom.Location = new System.Drawing.Point(409, 24);
            this.cboDMMcom.Name = "cboDMMcom";
            this.cboDMMcom.Size = new System.Drawing.Size(71, 25);
            this.cboDMMcom.TabIndex = 49;
            this.cboDMMcom.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 48;
            this.label3.Text = "表串口:";
            this.label3.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtMultimeterTest);
            this.groupBox2.Controls.Add(this.txtMultimeterAddress);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.btnMultimeterConnect);
            this.groupBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox2.Location = new System.Drawing.Point(14, 50);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(467, 123);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "仪器Connect";
            // 
            // txtMultimeterTest
            // 
            this.txtMultimeterTest.Location = new System.Drawing.Point(111, 58);
            this.txtMultimeterTest.Margin = new System.Windows.Forms.Padding(4);
            this.txtMultimeterTest.Multiline = true;
            this.txtMultimeterTest.Name = "txtMultimeterTest";
            this.txtMultimeterTest.Size = new System.Drawing.Size(338, 57);
            this.txtMultimeterTest.TabIndex = 47;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 26);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 17);
            this.label16.TabIndex = 42;
            this.label16.Text = "万用表仪器地址:";
            // 
            // btnMultimeterConnect
            // 
            this.btnMultimeterConnect.Location = new System.Drawing.Point(15, 58);
            this.btnMultimeterConnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnMultimeterConnect.Name = "btnMultimeterConnect";
            this.btnMultimeterConnect.Size = new System.Drawing.Size(88, 57);
            this.btnMultimeterConnect.TabIndex = 44;
            this.btnMultimeterConnect.Text = "测试连接\r\n(万用表)";
            this.btnMultimeterConnect.UseVisualStyleBackColor = false;
            this.btnMultimeterConnect.Click += new System.EventHandler(this.btnTestConnect_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.Location = new System.Drawing.Point(177, 27);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(83, 17);
            this.label28.TabIndex = 38;
            this.label28.Text = "仪器连接方式:";
            // 
            // cboMultimeterConType
            // 
            this.cboMultimeterConType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMultimeterConType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboMultimeterConType.FormattingEnabled = true;
            this.cboMultimeterConType.Items.AddRange(new object[] {
            "Keysight库",
            "NI库"});
            this.cboMultimeterConType.Location = new System.Drawing.Point(261, 24);
            this.cboMultimeterConType.Margin = new System.Windows.Forms.Padding(4);
            this.cboMultimeterConType.Name = "cboMultimeterConType";
            this.cboMultimeterConType.Size = new System.Drawing.Size(95, 25);
            this.cboMultimeterConType.TabIndex = 44;
            // 
            // cboMultimeterType
            // 
            this.cboMultimeterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMultimeterType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboMultimeterType.FormattingEnabled = true;
            this.cboMultimeterType.Items.AddRange(new object[] {
            "34470A",
            "34401A",
            "34461A"});
            this.cboMultimeterType.Location = new System.Drawing.Point(83, 24);
            this.cboMultimeterType.Margin = new System.Windows.Forms.Padding(4);
            this.cboMultimeterType.Name = "cboMultimeterType";
            this.cboMultimeterType.Size = new System.Drawing.Size(83, 25);
            this.cboMultimeterType.TabIndex = 43;
            this.cboMultimeterType.SelectedIndexChanged += new System.EventHandler(this.cboMultimeterType_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.Location = new System.Drawing.Point(8, 27);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(71, 17);
            this.label24.TabIndex = 42;
            this.label24.Text = "万用表型号:";
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.Controls.Add(this.dgvTestResult);
            this.groupBox11.Location = new System.Drawing.Point(508, 375);
            this.groupBox11.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox11.Size = new System.Drawing.Size(945, 321);
            this.groupBox11.TabIndex = 61;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "测试结果显示";
            // 
            // dgvTestResult
            // 
            this.dgvTestResult.AllowUserToDeleteRows = false;
            this.dgvTestResult.AllowUserToResizeColumns = false;
            this.dgvTestResult.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvTestResult.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTestResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTestResult.ColumnHeadersHeight = 40;
            this.dgvTestResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.序号,
            this.设备类型,
            this.测试ID,
            this.测试类型,
            this.设置值,
            this.万用表读值,
            this.设备采样值,
            this.误差1,
            this.误差2,
            this.允许误差,
            this.是否合格});
            this.dgvTestResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTestResult.Location = new System.Drawing.Point(4, 20);
            this.dgvTestResult.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTestResult.Name = "dgvTestResult";
            this.dgvTestResult.ReadOnly = true;
            this.dgvTestResult.RowHeadersVisible = false;
            this.dgvTestResult.RowHeadersWidth = 72;
            this.dgvTestResult.RowTemplate.Height = 23;
            this.dgvTestResult.Size = new System.Drawing.Size(937, 297);
            this.dgvTestResult.TabIndex = 0;
            // 
            // 序号
            // 
            this.序号.FillWeight = 58.94028F;
            this.序号.HeaderText = "序号";
            this.序号.MinimumWidth = 7;
            this.序号.Name = "序号";
            this.序号.ReadOnly = true;
            this.序号.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.序号.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 设备类型
            // 
            this.设备类型.FillWeight = 83.30003F;
            this.设备类型.HeaderText = "设备型号";
            this.设备类型.MinimumWidth = 10;
            this.设备类型.Name = "设备类型";
            this.设备类型.ReadOnly = true;
            this.设备类型.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.设备类型.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 测试ID
            // 
            this.测试ID.FillWeight = 127.716F;
            this.测试ID.HeaderText = "测试ID";
            this.测试ID.MinimumWidth = 12;
            this.测试ID.Name = "测试ID";
            this.测试ID.ReadOnly = true;
            this.测试ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.测试ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 测试类型
            // 
            this.测试类型.FillWeight = 130.1408F;
            this.测试类型.HeaderText = "测试类型";
            this.测试类型.MinimumWidth = 10;
            this.测试类型.Name = "测试类型";
            this.测试类型.ReadOnly = true;
            this.测试类型.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.测试类型.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 设置值
            // 
            this.设置值.FillWeight = 89.82227F;
            this.设置值.HeaderText = "设置值";
            this.设置值.MinimumWidth = 9;
            this.设置值.Name = "设置值";
            this.设置值.ReadOnly = true;
            this.设置值.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.设置值.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 万用表读值
            // 
            this.万用表读值.FillWeight = 86.07027F;
            this.万用表读值.HeaderText = "万用表读值";
            this.万用表读值.MinimumWidth = 9;
            this.万用表读值.Name = "万用表读值";
            this.万用表读值.ReadOnly = true;
            this.万用表读值.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.万用表读值.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 设备采样值
            // 
            this.设备采样值.FillWeight = 85.88921F;
            this.设备采样值.HeaderText = "设备采样值";
            this.设备采样值.MinimumWidth = 9;
            this.设备采样值.Name = "设备采样值";
            this.设备采样值.ReadOnly = true;
            this.设备采样值.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.设备采样值.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 误差1
            // 
            this.误差1.FillWeight = 81.15267F;
            this.误差1.HeaderText = "误差1";
            this.误差1.MinimumWidth = 9;
            this.误差1.Name = "误差1";
            this.误差1.ReadOnly = true;
            this.误差1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.误差1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 误差2
            // 
            this.误差2.FillWeight = 80.22568F;
            this.误差2.HeaderText = "误差2";
            this.误差2.MinimumWidth = 9;
            this.误差2.Name = "误差2";
            this.误差2.ReadOnly = true;
            this.误差2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.误差2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 允许误差
            // 
            this.允许误差.FillWeight = 77.50417F;
            this.允许误差.HeaderText = "允许误差";
            this.允许误差.MinimumWidth = 9;
            this.允许误差.Name = "允许误差";
            this.允许误差.ReadOnly = true;
            this.允许误差.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.允许误差.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 是否合格
            // 
            this.是否合格.FillWeight = 74.23859F;
            this.是否合格.HeaderText = "是否合格";
            this.是否合格.MinimumWidth = 9;
            this.是否合格.Name = "是否合格";
            this.是否合格.ReadOnly = true;
            this.是否合格.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.是否合格.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(508, 696);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Maximum = 260;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(946, 17);
            this.progressBar1.TabIndex = 50;
            // 
            // btnCalibration
            // 
            this.btnCalibration.BackColor = System.Drawing.Color.Transparent;
            this.btnCalibration.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCalibration.FlatAppearance.BorderSize = 2;
            this.btnCalibration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCalibration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.btnCalibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalibration.Location = new System.Drawing.Point(40, 657);
            this.btnCalibration.Margin = new System.Windows.Forms.Padding(4);
            this.btnCalibration.Name = "btnCalibration";
            this.btnCalibration.Size = new System.Drawing.Size(187, 52);
            this.btnCalibration.TabIndex = 55;
            this.btnCalibration.Text = "启动校准";
            this.btnCalibration.UseVisualStyleBackColor = false;
            this.btnCalibration.Click += new System.EventHandler(this.btnCalibration_Click);
            // 
            // btnStopCalibration
            // 
            this.btnStopCalibration.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.btnStopCalibration.FlatAppearance.BorderSize = 2;
            this.btnStopCalibration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStopCalibration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnStopCalibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopCalibration.Location = new System.Drawing.Point(281, 657);
            this.btnStopCalibration.Margin = new System.Windows.Forms.Padding(4);
            this.btnStopCalibration.Name = "btnStopCalibration";
            this.btnStopCalibration.Size = new System.Drawing.Size(187, 52);
            this.btnStopCalibration.TabIndex = 56;
            this.btnStopCalibration.Text = "停止校准(&S)";
            this.btnStopCalibration.UseVisualStyleBackColor = true;
            this.btnStopCalibration.Click += new System.EventHandler(this.StopCalibration_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnGetNowTime);
            this.groupBox9.Controls.Add(this.label10);
            this.groupBox9.Controls.Add(this.txtDateTime);
            this.groupBox9.Controls.Add(this.chkReadSNCodeStart);
            this.groupBox9.Controls.Add(this.chkExtRes);
            this.groupBox9.Controls.Add(this.chkWriteSNCode);
            this.groupBox9.Controls.Add(this.chkZXSource);
            this.groupBox9.Controls.Add(this.chkAloneSave);
            this.groupBox9.Controls.Add(this.label6);
            this.groupBox9.Controls.Add(this.txtSNdeviceType);
            this.groupBox9.Controls.Add(this.txtCalorCheckUser);
            this.groupBox9.Controls.Add(this.txtCalorCheckDepart);
            this.groupBox9.Controls.Add(this.label33);
            this.groupBox9.Controls.Add(this.label32);
            this.groupBox9.Controls.Add(this.cboSaveErrrRule);
            this.groupBox9.Controls.Add(this.label31);
            this.groupBox9.Controls.Add(this.cboResSpecification);
            this.groupBox9.Controls.Add(this.label20);
            this.groupBox9.Controls.Add(this.label22);
            this.groupBox9.Controls.Add(this.cboCalEQMType);
            this.groupBox9.Controls.Add(this.label23);
            this.groupBox9.Controls.Add(this.cboEqmCalSerialPort);
            this.groupBox9.Controls.Add(this.label27);
            this.groupBox9.Controls.Add(this.txtSNnum);
            this.groupBox9.Location = new System.Drawing.Point(9, 7);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox9.Size = new System.Drawing.Size(489, 174);
            this.groupBox9.TabIndex = 47;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "基本信息设置";
            // 
            // btnGetNowTime
            // 
            this.btnGetNowTime.Location = new System.Drawing.Point(238, 56);
            this.btnGetNowTime.Name = "btnGetNowTime";
            this.btnGetNowTime.Size = new System.Drawing.Size(48, 66);
            this.btnGetNowTime.TabIndex = 75;
            this.btnGetNowTime.Text = "获取\r\n当前\r\n时间";
            this.btnGetNowTime.UseVisualStyleBackColor = true;
            this.btnGetNowTime.Click += new System.EventHandler(this.btnGetNowTime_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 17);
            this.label10.TabIndex = 74;
            this.label10.Text = "-";
            // 
            // txtDateTime
            // 
            this.txtDateTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDateTime.Location = new System.Drawing.Point(157, 20);
            this.txtDateTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.Size = new System.Drawing.Size(63, 23);
            this.txtDateTime.TabIndex = 73;
            // 
            // chkReadSNCodeStart
            // 
            this.chkReadSNCodeStart.AutoSize = true;
            this.chkReadSNCodeStart.Location = new System.Drawing.Point(382, 22);
            this.chkReadSNCodeStart.Name = "chkReadSNCodeStart";
            this.chkReadSNCodeStart.Size = new System.Drawing.Size(111, 21);
            this.chkReadSNCodeStart.TabIndex = 72;
            this.chkReadSNCodeStart.Text = "读设备编码启动";
            this.chkReadSNCodeStart.UseVisualStyleBackColor = true;
            this.chkReadSNCodeStart.CheckedChanged += new System.EventHandler(this.chkReadSNCodeStart_CheckedChanged);
            // 
            // chkExtRes
            // 
            this.chkExtRes.AutoSize = true;
            this.chkExtRes.Location = new System.Drawing.Point(296, 49);
            this.chkExtRes.Name = "chkExtRes";
            this.chkExtRes.Size = new System.Drawing.Size(150, 21);
            this.chkExtRes.TabIndex = 71;
            this.chkExtRes.Text = "30A校准/点检外接电阻";
            this.chkExtRes.UseVisualStyleBackColor = true;
            // 
            // chkWriteSNCode
            // 
            this.chkWriteSNCode.AutoSize = true;
            this.chkWriteSNCode.Location = new System.Drawing.Point(296, 22);
            this.chkWriteSNCode.Name = "chkWriteSNCode";
            this.chkWriteSNCode.Size = new System.Drawing.Size(87, 21);
            this.chkWriteSNCode.TabIndex = 70;
            this.chkWriteSNCode.Text = "写设备编码";
            this.chkWriteSNCode.UseVisualStyleBackColor = true;
            this.chkWriteSNCode.CheckedChanged += new System.EventHandler(this.chkWriteSNCode_CheckedChanged);
            // 
            // chkZXSource
            // 
            this.chkZXSource.AutoSize = true;
            this.chkZXSource.Location = new System.Drawing.Point(275, 143);
            this.chkZXSource.Name = "chkZXSource";
            this.chkZXSource.Size = new System.Drawing.Size(99, 21);
            this.chkZXSource.TabIndex = 69;
            this.chkZXSource.Text = "使用兆信电源";
            this.chkZXSource.UseVisualStyleBackColor = true;
            this.chkZXSource.Visible = false;
            this.chkZXSource.CheckedChanged += new System.EventHandler(this.chkZXSource_CheckedChanged);
            // 
            // chkAloneSave
            // 
            this.chkAloneSave.AutoSize = true;
            this.chkAloneSave.Location = new System.Drawing.Point(381, 143);
            this.chkAloneSave.Name = "chkAloneSave";
            this.chkAloneSave.Size = new System.Drawing.Size(99, 21);
            this.chkAloneSave.TabIndex = 68;
            this.chkAloneSave.Text = "生成数据报告";
            this.chkAloneSave.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 17);
            this.label6.TabIndex = 65;
            this.label6.Text = "-";
            // 
            // txtSNdeviceType
            // 
            this.txtSNdeviceType.Location = new System.Drawing.Point(83, 20);
            this.txtSNdeviceType.Name = "txtSNdeviceType";
            this.txtSNdeviceType.Size = new System.Drawing.Size(58, 23);
            this.txtSNdeviceType.TabIndex = 64;
            // 
            // txtCalorCheckUser
            // 
            this.txtCalorCheckUser.Location = new System.Drawing.Point(378, 109);
            this.txtCalorCheckUser.Name = "txtCalorCheckUser";
            this.txtCalorCheckUser.Size = new System.Drawing.Size(100, 23);
            this.txtCalorCheckUser.TabIndex = 63;
            // 
            // txtCalorCheckDepart
            // 
            this.txtCalorCheckDepart.Location = new System.Drawing.Point(377, 78);
            this.txtCalorCheckDepart.Name = "txtCalorCheckDepart";
            this.txtCalorCheckDepart.Size = new System.Drawing.Size(101, 23);
            this.txtCalorCheckDepart.TabIndex = 62;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(297, 112);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(85, 17);
            this.label33.TabIndex = 61;
            this.label33.Text = "校准/点检人：";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(285, 81);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(97, 17);
            this.label32.TabIndex = 60;
            this.label32.Text = "校准/点检部门：";
            // 
            // cboSaveErrrRule
            // 
            this.cboSaveErrrRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSaveErrrRule.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboSaveErrrRule.FormattingEnabled = true;
            this.cboSaveErrrRule.Items.AddRange(new object[] {
            "设置值、万用表、设备读值",
            "设置值、万用表",
            "设置值、设备读值",
            "万用表、设备读值"});
            this.cboSaveErrrRule.Location = new System.Drawing.Point(83, 139);
            this.cboSaveErrrRule.Margin = new System.Windows.Forms.Padding(4);
            this.cboSaveErrrRule.Name = "cboSaveErrrRule";
            this.cboSaveErrrRule.Size = new System.Drawing.Size(177, 25);
            this.cboSaveErrrRule.TabIndex = 59;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.Location = new System.Drawing.Point(1, 142);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(83, 17);
            this.label31.TabIndex = 58;
            this.label31.Text = "误差比对规则:";
            // 
            // cboResSpecification
            // 
            this.cboResSpecification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboResSpecification.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboResSpecification.FormattingEnabled = true;
            this.cboResSpecification.Items.AddRange(new object[] {
            "100mΩ",
            "10mΩ",
            "1mΩ",
            "2mΩ"});
            this.cboResSpecification.Location = new System.Drawing.Point(83, 108);
            this.cboResSpecification.Margin = new System.Windows.Forms.Padding(4);
            this.cboResSpecification.Name = "cboResSpecification";
            this.cboResSpecification.Size = new System.Drawing.Size(150, 25);
            this.cboResSpecification.TabIndex = 57;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(3, 111);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(71, 17);
            this.label20.TabIndex = 56;
            this.label20.Text = "分流器规格:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(14, 80);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 17);
            this.label22.TabIndex = 48;
            this.label22.Text = "设备串口:";
            // 
            // cboCalEQMType
            // 
            this.cboCalEQMType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCalEQMType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboCalEQMType.FormattingEnabled = true;
            this.cboCalEQMType.Items.AddRange(new object[] {
            "BAT525G",
            "BAT525H",
            "BAT525C",
            "BAT525D"});
            this.cboCalEQMType.Location = new System.Drawing.Point(83, 47);
            this.cboCalEQMType.Margin = new System.Windows.Forms.Padding(4);
            this.cboCalEQMType.Name = "cboCalEQMType";
            this.cboCalEQMType.Size = new System.Drawing.Size(150, 25);
            this.cboCalEQMType.TabIndex = 46;
            this.cboCalEQMType.SelectedIndexChanged += new System.EventHandler(this.cboCalEQMType_SelectedIndexChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(14, 50);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(59, 17);
            this.label23.TabIndex = 45;
            this.label23.Text = "设备型号:";
            // 
            // cboEqmCalSerialPort
            // 
            this.cboEqmCalSerialPort.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboEqmCalSerialPort.FormattingEnabled = true;
            this.cboEqmCalSerialPort.Location = new System.Drawing.Point(83, 77);
            this.cboEqmCalSerialPort.Margin = new System.Windows.Forms.Padding(4);
            this.cboEqmCalSerialPort.Name = "cboEqmCalSerialPort";
            this.cboEqmCalSerialPort.Size = new System.Drawing.Size(150, 25);
            this.cboEqmCalSerialPort.TabIndex = 47;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.Location = new System.Drawing.Point(3, 23);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(76, 17);
            this.label27.TabIndex = 2;
            this.label27.Text = "设备测试SN:";
            // 
            // txtSNnum
            // 
            this.txtSNnum.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSNnum.Location = new System.Drawing.Point(236, 20);
            this.txtSNnum.Margin = new System.Windows.Forms.Padding(4);
            this.txtSNnum.Name = "txtSNnum";
            this.txtSNnum.Size = new System.Drawing.Size(52, 23);
            this.txtSNnum.TabIndex = 7;
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.Controls.Add(this.chkCheck);
            this.groupBox14.Controls.Add(this.tabCalibrationSelect);
            this.groupBox14.Controls.Add(this.chkAll);
            this.groupBox14.Controls.Add(this.chkCalibration);
            this.groupBox14.Controls.Add(this.groupBox8);
            this.groupBox14.Location = new System.Drawing.Point(505, 6);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(948, 365);
            this.groupBox14.TabIndex = 61;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "校准项目配置";
            // 
            // chkCheck
            // 
            this.chkCheck.AutoSize = true;
            this.chkCheck.Location = new System.Drawing.Point(7, 87);
            this.chkCheck.Name = "chkCheck";
            this.chkCheck.Size = new System.Drawing.Size(51, 21);
            this.chkCheck.TabIndex = 62;
            this.chkCheck.Text = "点检";
            this.chkCheck.UseVisualStyleBackColor = true;
            this.chkCheck.CheckedChanged += new System.EventHandler(this.chkCheck_CheckedChanged);
            // 
            // tabCalibrationSelect
            // 
            this.tabCalibrationSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCalibrationSelect.Controls.Add(this.Frist);
            this.tabCalibrationSelect.Controls.Add(this.BAT525G);
            this.tabCalibrationSelect.Controls.Add(this.BAT525C);
            this.tabCalibrationSelect.Controls.Add(this.BAT525D);
            this.tabCalibrationSelect.Location = new System.Drawing.Point(84, 13);
            this.tabCalibrationSelect.Name = "tabCalibrationSelect";
            this.tabCalibrationSelect.SelectedIndex = 0;
            this.tabCalibrationSelect.Size = new System.Drawing.Size(857, 103);
            this.tabCalibrationSelect.TabIndex = 61;
            // 
            // Frist
            // 
            this.Frist.Location = new System.Drawing.Point(4, 26);
            this.Frist.Name = "Frist";
            this.Frist.Size = new System.Drawing.Size(849, 73);
            this.Frist.TabIndex = 1;
            this.Frist.UseVisualStyleBackColor = true;
            // 
            // BAT525G
            // 
            this.BAT525G.Controls.Add(this.SBT825G_chkCNTStNegative);
            this.BAT525G.Controls.Add(this.SBT825G_chkCNTStPositive);
            this.BAT525G.Controls.Add(this.SBT825G_chkDCIRCalibration);
            this.BAT525G.Controls.Add(this.SBT825G_chkNTCCalibration);
            this.BAT525G.Controls.Add(this.SBT825G_chkStCur4Calibration);
            this.BAT525G.Controls.Add(this.SBT825G_chkStCur3Calibration);
            this.BAT525G.Controls.Add(this.SBT825G_chkStCur2Calibration);
            this.BAT525G.Controls.Add(this.SBT825G_chkStCurCalibration);
            this.BAT525G.Controls.Add(this.SBT825G_chkLoadPartCur);
            this.BAT525G.Controls.Add(this.SBT825G_chkLoadPartVolt);
            this.BAT525G.Controls.Add(this.SBT825G_chkCellCur);
            this.BAT525G.Controls.Add(this.SBT825G_chkCellVolt);
            this.BAT525G.Controls.Add(this.SBT825G_chkProVoltCalibration);
            this.BAT525G.Controls.Add(this.SBT825G_chkDsgCurCalibration30A);
            this.BAT525G.Controls.Add(this.SBT825G_chkDsgCurCalibration);
            this.BAT525G.Controls.Add(this.SBT825G_chkOCVCalibration);
            this.BAT525G.Controls.Add(this.SBT825G_chkChgVoltCalibration);
            this.BAT525G.Location = new System.Drawing.Point(4, 26);
            this.BAT525G.Name = "BAT525G";
            this.BAT525G.Padding = new System.Windows.Forms.Padding(3);
            this.BAT525G.Size = new System.Drawing.Size(849, 73);
            this.BAT525G.TabIndex = 0;
            this.BAT525G.Text = "BAT525G";
            this.BAT525G.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkCNTStNegative
            // 
            this.SBT825G_chkCNTStNegative.AutoSize = true;
            this.SBT825G_chkCNTStNegative.Location = new System.Drawing.Point(675, 40);
            this.SBT825G_chkCNTStNegative.Name = "SBT825G_chkCNTStNegative";
            this.SBT825G_chkCNTStNegative.Size = new System.Drawing.Size(96, 21);
            this.SBT825G_chkCNTStNegative.TabIndex = 16;
            this.SBT825G_chkCNTStNegative.Text = "CNT静态(负)";
            this.SBT825G_chkCNTStNegative.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkCNTStPositive
            // 
            this.SBT825G_chkCNTStPositive.AutoSize = true;
            this.SBT825G_chkCNTStPositive.Location = new System.Drawing.Point(579, 40);
            this.SBT825G_chkCNTStPositive.Name = "SBT825G_chkCNTStPositive";
            this.SBT825G_chkCNTStPositive.Size = new System.Drawing.Size(96, 21);
            this.SBT825G_chkCNTStPositive.TabIndex = 15;
            this.SBT825G_chkCNTStPositive.Text = "CNT静态(正)";
            this.SBT825G_chkCNTStPositive.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkDCIRCalibration
            // 
            this.SBT825G_chkDCIRCalibration.AutoSize = true;
            this.SBT825G_chkDCIRCalibration.Location = new System.Drawing.Point(498, 40);
            this.SBT825G_chkDCIRCalibration.Name = "SBT825G_chkDCIRCalibration";
            this.SBT825G_chkDCIRCalibration.Size = new System.Drawing.Size(75, 21);
            this.SBT825G_chkDCIRCalibration.TabIndex = 14;
            this.SBT825G_chkDCIRCalibration.Text = "直流内阻";
            this.SBT825G_chkDCIRCalibration.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkNTCCalibration
            // 
            this.SBT825G_chkNTCCalibration.AutoSize = true;
            this.SBT825G_chkNTCCalibration.Location = new System.Drawing.Point(743, 13);
            this.SBT825G_chkNTCCalibration.Name = "SBT825G_chkNTCCalibration";
            this.SBT825G_chkNTCCalibration.Size = new System.Drawing.Size(52, 21);
            this.SBT825G_chkNTCCalibration.TabIndex = 13;
            this.SBT825G_chkNTCCalibration.Text = "NTC";
            this.SBT825G_chkNTCCalibration.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkStCur4Calibration
            // 
            this.SBT825G_chkStCur4Calibration.AutoSize = true;
            this.SBT825G_chkStCur4Calibration.Location = new System.Drawing.Point(367, 40);
            this.SBT825G_chkStCur4Calibration.Name = "SBT825G_chkStCur4Calibration";
            this.SBT825G_chkStCur4Calibration.Size = new System.Drawing.Size(125, 21);
            this.SBT825G_chkStCur4Calibration.TabIndex = 12;
            this.SBT825G_chkStCur4Calibration.Text = "静态电流20000nA";
            this.SBT825G_chkStCur4Calibration.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkStCur3Calibration
            // 
            this.SBT825G_chkStCur3Calibration.AutoSize = true;
            this.SBT825G_chkStCur3Calibration.Location = new System.Drawing.Point(243, 40);
            this.SBT825G_chkStCur3Calibration.Name = "SBT825G_chkStCur3Calibration";
            this.SBT825G_chkStCur3Calibration.Size = new System.Drawing.Size(118, 21);
            this.SBT825G_chkStCur3Calibration.TabIndex = 11;
            this.SBT825G_chkStCur3Calibration.Text = "静态电流2000nA";
            this.SBT825G_chkStCur3Calibration.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkStCur2Calibration
            // 
            this.SBT825G_chkStCur2Calibration.AutoSize = true;
            this.SBT825G_chkStCur2Calibration.Location = new System.Drawing.Point(123, 40);
            this.SBT825G_chkStCur2Calibration.Name = "SBT825G_chkStCur2Calibration";
            this.SBT825G_chkStCur2Calibration.Size = new System.Drawing.Size(118, 21);
            this.SBT825G_chkStCur2Calibration.TabIndex = 10;
            this.SBT825G_chkStCur2Calibration.Text = "静态电流2000uA";
            this.SBT825G_chkStCur2Calibration.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkStCurCalibration
            // 
            this.SBT825G_chkStCurCalibration.AutoSize = true;
            this.SBT825G_chkStCurCalibration.Location = new System.Drawing.Point(6, 40);
            this.SBT825G_chkStCurCalibration.Name = "SBT825G_chkStCurCalibration";
            this.SBT825G_chkStCurCalibration.Size = new System.Drawing.Size(111, 21);
            this.SBT825G_chkStCurCalibration.TabIndex = 9;
            this.SBT825G_chkStCurCalibration.Text = "静态电流200uA";
            this.SBT825G_chkStCurCalibration.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkLoadPartCur
            // 
            this.SBT825G_chkLoadPartCur.AutoSize = true;
            this.SBT825G_chkLoadPartCur.Location = new System.Drawing.Point(675, 13);
            this.SBT825G_chkLoadPartCur.Name = "SBT825G_chkLoadPartCur";
            this.SBT825G_chkLoadPartCur.Size = new System.Drawing.Size(65, 21);
            this.SBT825G_chkLoadPartCur.TabIndex = 8;
            this.SBT825G_chkLoadPartCur.Text = "LV电流";
            this.SBT825G_chkLoadPartCur.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkLoadPartVolt
            // 
            this.SBT825G_chkLoadPartVolt.AutoSize = true;
            this.SBT825G_chkLoadPartVolt.Location = new System.Drawing.Point(604, 13);
            this.SBT825G_chkLoadPartVolt.Name = "SBT825G_chkLoadPartVolt";
            this.SBT825G_chkLoadPartVolt.Size = new System.Drawing.Size(65, 21);
            this.SBT825G_chkLoadPartVolt.TabIndex = 7;
            this.SBT825G_chkLoadPartVolt.Text = "LV电压";
            this.SBT825G_chkLoadPartVolt.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkCellCur
            // 
            this.SBT825G_chkCellCur.AutoSize = true;
            this.SBT825G_chkCellCur.Location = new System.Drawing.Point(526, 13);
            this.SBT825G_chkCellCur.Name = "SBT825G_chkCellCur";
            this.SBT825G_chkCellCur.Size = new System.Drawing.Size(72, 21);
            this.SBT825G_chkCellCur.TabIndex = 6;
            this.SBT825G_chkCellCur.Text = "Cell电流";
            this.SBT825G_chkCellCur.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkCellVolt
            // 
            this.SBT825G_chkCellVolt.AutoSize = true;
            this.SBT825G_chkCellVolt.Location = new System.Drawing.Point(448, 13);
            this.SBT825G_chkCellVolt.Name = "SBT825G_chkCellVolt";
            this.SBT825G_chkCellVolt.Size = new System.Drawing.Size(72, 21);
            this.SBT825G_chkCellVolt.TabIndex = 5;
            this.SBT825G_chkCellVolt.Text = "Cell电压";
            this.SBT825G_chkCellVolt.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkProVoltCalibration
            // 
            this.SBT825G_chkProVoltCalibration.AutoSize = true;
            this.SBT825G_chkProVoltCalibration.Location = new System.Drawing.Point(367, 13);
            this.SBT825G_chkProVoltCalibration.Name = "SBT825G_chkProVoltCalibration";
            this.SBT825G_chkProVoltCalibration.Size = new System.Drawing.Size(75, 21);
            this.SBT825G_chkProVoltCalibration.TabIndex = 4;
            this.SBT825G_chkProVoltCalibration.Text = "编程电压";
            this.SBT825G_chkProVoltCalibration.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkDsgCurCalibration30A
            // 
            this.SBT825G_chkDsgCurCalibration30A.AutoSize = true;
            this.SBT825G_chkDsgCurCalibration30A.Location = new System.Drawing.Point(264, 13);
            this.SBT825G_chkDsgCurCalibration30A.Name = "SBT825G_chkDsgCurCalibration30A";
            this.SBT825G_chkDsgCurCalibration30A.Size = new System.Drawing.Size(97, 21);
            this.SBT825G_chkDsgCurCalibration30A.TabIndex = 3;
            this.SBT825G_chkDsgCurCalibration30A.Text = "放电电流30A";
            this.SBT825G_chkDsgCurCalibration30A.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkDsgCurCalibration
            // 
            this.SBT825G_chkDsgCurCalibration.AutoSize = true;
            this.SBT825G_chkDsgCurCalibration.Location = new System.Drawing.Point(168, 13);
            this.SBT825G_chkDsgCurCalibration.Name = "SBT825G_chkDsgCurCalibration";
            this.SBT825G_chkDsgCurCalibration.Size = new System.Drawing.Size(90, 21);
            this.SBT825G_chkDsgCurCalibration.TabIndex = 2;
            this.SBT825G_chkDsgCurCalibration.Text = "放电电流3A";
            this.SBT825G_chkDsgCurCalibration.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkOCVCalibration
            // 
            this.SBT825G_chkOCVCalibration.AutoSize = true;
            this.SBT825G_chkOCVCalibration.Location = new System.Drawing.Point(87, 13);
            this.SBT825G_chkOCVCalibration.Name = "SBT825G_chkOCVCalibration";
            this.SBT825G_chkOCVCalibration.Size = new System.Drawing.Size(75, 21);
            this.SBT825G_chkOCVCalibration.TabIndex = 1;
            this.SBT825G_chkOCVCalibration.Text = "开路电压";
            this.SBT825G_chkOCVCalibration.UseVisualStyleBackColor = true;
            // 
            // SBT825G_chkChgVoltCalibration
            // 
            this.SBT825G_chkChgVoltCalibration.AutoSize = true;
            this.SBT825G_chkChgVoltCalibration.Location = new System.Drawing.Point(6, 13);
            this.SBT825G_chkChgVoltCalibration.Name = "SBT825G_chkChgVoltCalibration";
            this.SBT825G_chkChgVoltCalibration.Size = new System.Drawing.Size(75, 21);
            this.SBT825G_chkChgVoltCalibration.TabIndex = 0;
            this.SBT825G_chkChgVoltCalibration.Text = "充电电压";
            this.SBT825G_chkChgVoltCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525C
            // 
            this.BAT525C.Controls.Add(this.BAT525C_chkIDRCalibration);
            this.BAT525C.Controls.Add(this.BAT525C_chkDCIRCalibration);
            this.BAT525C.Controls.Add(this.BAT525C_chkNTCCalibration);
            this.BAT525C.Controls.Add(this.BAT525C_chkStCurnACalibration);
            this.BAT525C.Controls.Add(this.BAT525C__chkStCuruACalibration);
            this.BAT525C.Controls.Add(this.BAT525C_chkLoadPartCur);
            this.BAT525C.Controls.Add(this.BAT525C_chkLoadPartVolt);
            this.BAT525C.Controls.Add(this.BAT525C_chkCellCur);
            this.BAT525C.Controls.Add(this.BAT525C_chkCellVolt);
            this.BAT525C.Controls.Add(this.BAT525C_chkProVoltCalibration);
            this.BAT525C.Controls.Add(this.BAT525C_chkDsgCurCalibration30A);
            this.BAT525C.Controls.Add(this.BAT525C_chkDsgCurCalibration);
            this.BAT525C.Controls.Add(this.BAT525C_chkOCVCalibration);
            this.BAT525C.Controls.Add(this.BAT525C_chkChgVoltCalibration);
            this.BAT525C.Location = new System.Drawing.Point(4, 26);
            this.BAT525C.Name = "BAT525C";
            this.BAT525C.Size = new System.Drawing.Size(849, 73);
            this.BAT525C.TabIndex = 2;
            this.BAT525C.Text = "BAT525C";
            this.BAT525C.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkIDRCalibration
            // 
            this.BAT525C_chkIDRCalibration.AutoSize = true;
            this.BAT525C_chkIDRCalibration.Location = new System.Drawing.Point(12, 40);
            this.BAT525C_chkIDRCalibration.Name = "BAT525C_chkIDRCalibration";
            this.BAT525C_chkIDRCalibration.Size = new System.Drawing.Size(48, 21);
            this.BAT525C_chkIDRCalibration.TabIndex = 32;
            this.BAT525C_chkIDRCalibration.Text = "IDR";
            this.BAT525C_chkIDRCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkDCIRCalibration
            // 
            this.BAT525C_chkDCIRCalibration.AutoSize = true;
            this.BAT525C_chkDCIRCalibration.Location = new System.Drawing.Point(334, 40);
            this.BAT525C_chkDCIRCalibration.Name = "BAT525C_chkDCIRCalibration";
            this.BAT525C_chkDCIRCalibration.Size = new System.Drawing.Size(75, 21);
            this.BAT525C_chkDCIRCalibration.TabIndex = 31;
            this.BAT525C_chkDCIRCalibration.Text = "直流内阻";
            this.BAT525C_chkDCIRCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkNTCCalibration
            // 
            this.BAT525C_chkNTCCalibration.AutoSize = true;
            this.BAT525C_chkNTCCalibration.Location = new System.Drawing.Point(749, 13);
            this.BAT525C_chkNTCCalibration.Name = "BAT525C_chkNTCCalibration";
            this.BAT525C_chkNTCCalibration.Size = new System.Drawing.Size(52, 21);
            this.BAT525C_chkNTCCalibration.TabIndex = 30;
            this.BAT525C_chkNTCCalibration.Text = "NTC";
            this.BAT525C_chkNTCCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkStCurnACalibration
            // 
            this.BAT525C_chkStCurnACalibration.AutoSize = true;
            this.BAT525C_chkStCurnACalibration.Location = new System.Drawing.Point(210, 40);
            this.BAT525C_chkStCurnACalibration.Name = "BAT525C_chkStCurnACalibration";
            this.BAT525C_chkStCurnACalibration.Size = new System.Drawing.Size(118, 21);
            this.BAT525C_chkStCurnACalibration.TabIndex = 29;
            this.BAT525C_chkStCurnACalibration.Text = "静态电流1000nA";
            this.BAT525C_chkStCurnACalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525C__chkStCuruACalibration
            // 
            this.BAT525C__chkStCuruACalibration.AutoSize = true;
            this.BAT525C__chkStCuruACalibration.Location = new System.Drawing.Point(77, 40);
            this.BAT525C__chkStCuruACalibration.Name = "BAT525C__chkStCuruACalibration";
            this.BAT525C__chkStCuruACalibration.Size = new System.Drawing.Size(118, 21);
            this.BAT525C__chkStCuruACalibration.TabIndex = 26;
            this.BAT525C__chkStCuruACalibration.Text = "静态电流1000uA";
            this.BAT525C__chkStCuruACalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkLoadPartCur
            // 
            this.BAT525C_chkLoadPartCur.AutoSize = true;
            this.BAT525C_chkLoadPartCur.Location = new System.Drawing.Point(681, 13);
            this.BAT525C_chkLoadPartCur.Name = "BAT525C_chkLoadPartCur";
            this.BAT525C_chkLoadPartCur.Size = new System.Drawing.Size(65, 21);
            this.BAT525C_chkLoadPartCur.TabIndex = 25;
            this.BAT525C_chkLoadPartCur.Text = "LV电流";
            this.BAT525C_chkLoadPartCur.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkLoadPartVolt
            // 
            this.BAT525C_chkLoadPartVolt.AutoSize = true;
            this.BAT525C_chkLoadPartVolt.Location = new System.Drawing.Point(610, 13);
            this.BAT525C_chkLoadPartVolt.Name = "BAT525C_chkLoadPartVolt";
            this.BAT525C_chkLoadPartVolt.Size = new System.Drawing.Size(65, 21);
            this.BAT525C_chkLoadPartVolt.TabIndex = 24;
            this.BAT525C_chkLoadPartVolt.Text = "LV电压";
            this.BAT525C_chkLoadPartVolt.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkCellCur
            // 
            this.BAT525C_chkCellCur.AutoSize = true;
            this.BAT525C_chkCellCur.Location = new System.Drawing.Point(532, 13);
            this.BAT525C_chkCellCur.Name = "BAT525C_chkCellCur";
            this.BAT525C_chkCellCur.Size = new System.Drawing.Size(72, 21);
            this.BAT525C_chkCellCur.TabIndex = 23;
            this.BAT525C_chkCellCur.Text = "Cell电流";
            this.BAT525C_chkCellCur.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkCellVolt
            // 
            this.BAT525C_chkCellVolt.AutoSize = true;
            this.BAT525C_chkCellVolt.Location = new System.Drawing.Point(454, 13);
            this.BAT525C_chkCellVolt.Name = "BAT525C_chkCellVolt";
            this.BAT525C_chkCellVolt.Size = new System.Drawing.Size(72, 21);
            this.BAT525C_chkCellVolt.TabIndex = 22;
            this.BAT525C_chkCellVolt.Text = "Cell电压";
            this.BAT525C_chkCellVolt.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkProVoltCalibration
            // 
            this.BAT525C_chkProVoltCalibration.AutoSize = true;
            this.BAT525C_chkProVoltCalibration.Enabled = false;
            this.BAT525C_chkProVoltCalibration.Location = new System.Drawing.Point(373, 13);
            this.BAT525C_chkProVoltCalibration.Name = "BAT525C_chkProVoltCalibration";
            this.BAT525C_chkProVoltCalibration.Size = new System.Drawing.Size(75, 21);
            this.BAT525C_chkProVoltCalibration.TabIndex = 21;
            this.BAT525C_chkProVoltCalibration.Text = "编程电压";
            this.BAT525C_chkProVoltCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkDsgCurCalibration30A
            // 
            this.BAT525C_chkDsgCurCalibration30A.AutoSize = true;
            this.BAT525C_chkDsgCurCalibration30A.Enabled = false;
            this.BAT525C_chkDsgCurCalibration30A.Location = new System.Drawing.Point(270, 13);
            this.BAT525C_chkDsgCurCalibration30A.Name = "BAT525C_chkDsgCurCalibration30A";
            this.BAT525C_chkDsgCurCalibration30A.Size = new System.Drawing.Size(97, 21);
            this.BAT525C_chkDsgCurCalibration30A.TabIndex = 20;
            this.BAT525C_chkDsgCurCalibration30A.Text = "放电电流30A";
            this.BAT525C_chkDsgCurCalibration30A.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkDsgCurCalibration
            // 
            this.BAT525C_chkDsgCurCalibration.AutoSize = true;
            this.BAT525C_chkDsgCurCalibration.Location = new System.Drawing.Point(174, 13);
            this.BAT525C_chkDsgCurCalibration.Name = "BAT525C_chkDsgCurCalibration";
            this.BAT525C_chkDsgCurCalibration.Size = new System.Drawing.Size(90, 21);
            this.BAT525C_chkDsgCurCalibration.TabIndex = 19;
            this.BAT525C_chkDsgCurCalibration.Text = "放电电流3A";
            this.BAT525C_chkDsgCurCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkOCVCalibration
            // 
            this.BAT525C_chkOCVCalibration.AutoSize = true;
            this.BAT525C_chkOCVCalibration.Location = new System.Drawing.Point(93, 13);
            this.BAT525C_chkOCVCalibration.Name = "BAT525C_chkOCVCalibration";
            this.BAT525C_chkOCVCalibration.Size = new System.Drawing.Size(75, 21);
            this.BAT525C_chkOCVCalibration.TabIndex = 18;
            this.BAT525C_chkOCVCalibration.Text = "开路电压";
            this.BAT525C_chkOCVCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525C_chkChgVoltCalibration
            // 
            this.BAT525C_chkChgVoltCalibration.AutoSize = true;
            this.BAT525C_chkChgVoltCalibration.Location = new System.Drawing.Point(12, 13);
            this.BAT525C_chkChgVoltCalibration.Name = "BAT525C_chkChgVoltCalibration";
            this.BAT525C_chkChgVoltCalibration.Size = new System.Drawing.Size(75, 21);
            this.BAT525C_chkChgVoltCalibration.TabIndex = 17;
            this.BAT525C_chkChgVoltCalibration.Text = "充电电压";
            this.BAT525C_chkChgVoltCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525D
            // 
            this.BAT525D.Controls.Add(this.BAT525D_chkIDRCalibration);
            this.BAT525D.Controls.Add(this.BAT525D_chkDCIRCalibration);
            this.BAT525D.Controls.Add(this.BAT525D_chkNTCCalibration);
            this.BAT525D.Controls.Add(this.BAT525D_chkStCurnACalibration);
            this.BAT525D.Controls.Add(this.BAT525D__chkStCuruACalibration);
            this.BAT525D.Controls.Add(this.BAT525D_chkLoadPartCur);
            this.BAT525D.Controls.Add(this.BAT525D_chkLoadPartVolt);
            this.BAT525D.Controls.Add(this.BAT525D_chkCellCur);
            this.BAT525D.Controls.Add(this.BAT525D_chkCellVolt);
            this.BAT525D.Controls.Add(this.BAT525D_chkProVoltCalibration);
            this.BAT525D.Controls.Add(this.BAT525D_chkDsgCurCalibration30A);
            this.BAT525D.Controls.Add(this.BAT525D_chkDsgCurCalibration);
            this.BAT525D.Controls.Add(this.BAT525D_chkOCVCalibration);
            this.BAT525D.Controls.Add(this.BAT525D_chkChgVoltCalibration);
            this.BAT525D.Location = new System.Drawing.Point(4, 26);
            this.BAT525D.Name = "BAT525D";
            this.BAT525D.Size = new System.Drawing.Size(849, 73);
            this.BAT525D.TabIndex = 3;
            this.BAT525D.Text = "BAT525D";
            this.BAT525D.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkIDRCalibration
            // 
            this.BAT525D_chkIDRCalibration.AutoSize = true;
            this.BAT525D_chkIDRCalibration.Location = new System.Drawing.Point(13, 39);
            this.BAT525D_chkIDRCalibration.Name = "BAT525D_chkIDRCalibration";
            this.BAT525D_chkIDRCalibration.Size = new System.Drawing.Size(48, 21);
            this.BAT525D_chkIDRCalibration.TabIndex = 46;
            this.BAT525D_chkIDRCalibration.Text = "IDR";
            this.BAT525D_chkIDRCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkDCIRCalibration
            // 
            this.BAT525D_chkDCIRCalibration.AutoSize = true;
            this.BAT525D_chkDCIRCalibration.Location = new System.Drawing.Point(335, 39);
            this.BAT525D_chkDCIRCalibration.Name = "BAT525D_chkDCIRCalibration";
            this.BAT525D_chkDCIRCalibration.Size = new System.Drawing.Size(75, 21);
            this.BAT525D_chkDCIRCalibration.TabIndex = 45;
            this.BAT525D_chkDCIRCalibration.Text = "直流内阻";
            this.BAT525D_chkDCIRCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkNTCCalibration
            // 
            this.BAT525D_chkNTCCalibration.AutoSize = true;
            this.BAT525D_chkNTCCalibration.Location = new System.Drawing.Point(774, 12);
            this.BAT525D_chkNTCCalibration.Name = "BAT525D_chkNTCCalibration";
            this.BAT525D_chkNTCCalibration.Size = new System.Drawing.Size(52, 21);
            this.BAT525D_chkNTCCalibration.TabIndex = 44;
            this.BAT525D_chkNTCCalibration.Text = "NTC";
            this.BAT525D_chkNTCCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkStCurnACalibration
            // 
            this.BAT525D_chkStCurnACalibration.AutoSize = true;
            this.BAT525D_chkStCurnACalibration.Location = new System.Drawing.Point(211, 39);
            this.BAT525D_chkStCurnACalibration.Name = "BAT525D_chkStCurnACalibration";
            this.BAT525D_chkStCurnACalibration.Size = new System.Drawing.Size(118, 21);
            this.BAT525D_chkStCurnACalibration.TabIndex = 43;
            this.BAT525D_chkStCurnACalibration.Text = "静态电流1000nA";
            this.BAT525D_chkStCurnACalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525D__chkStCuruACalibration
            // 
            this.BAT525D__chkStCuruACalibration.AutoSize = true;
            this.BAT525D__chkStCuruACalibration.Location = new System.Drawing.Point(78, 39);
            this.BAT525D__chkStCuruACalibration.Name = "BAT525D__chkStCuruACalibration";
            this.BAT525D__chkStCuruACalibration.Size = new System.Drawing.Size(118, 21);
            this.BAT525D__chkStCuruACalibration.TabIndex = 42;
            this.BAT525D__chkStCuruACalibration.Text = "静态电流2000uA";
            this.BAT525D__chkStCuruACalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkLoadPartCur
            // 
            this.BAT525D_chkLoadPartCur.AutoSize = true;
            this.BAT525D_chkLoadPartCur.Location = new System.Drawing.Point(706, 12);
            this.BAT525D_chkLoadPartCur.Name = "BAT525D_chkLoadPartCur";
            this.BAT525D_chkLoadPartCur.Size = new System.Drawing.Size(65, 21);
            this.BAT525D_chkLoadPartCur.TabIndex = 41;
            this.BAT525D_chkLoadPartCur.Text = "LV电流";
            this.BAT525D_chkLoadPartCur.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkLoadPartVolt
            // 
            this.BAT525D_chkLoadPartVolt.AutoSize = true;
            this.BAT525D_chkLoadPartVolt.Location = new System.Drawing.Point(635, 12);
            this.BAT525D_chkLoadPartVolt.Name = "BAT525D_chkLoadPartVolt";
            this.BAT525D_chkLoadPartVolt.Size = new System.Drawing.Size(65, 21);
            this.BAT525D_chkLoadPartVolt.TabIndex = 40;
            this.BAT525D_chkLoadPartVolt.Text = "LV电压";
            this.BAT525D_chkLoadPartVolt.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkCellCur
            // 
            this.BAT525D_chkCellCur.AutoSize = true;
            this.BAT525D_chkCellCur.Location = new System.Drawing.Point(533, 12);
            this.BAT525D_chkCellCur.Name = "BAT525D_chkCellCur";
            this.BAT525D_chkCellCur.Size = new System.Drawing.Size(105, 21);
            this.BAT525D_chkCellCur.TabIndex = 39;
            this.BAT525D_chkCellCur.Text = "Cell电流(2.5A)";
            this.BAT525D_chkCellCur.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkCellVolt
            // 
            this.BAT525D_chkCellVolt.AutoSize = true;
            this.BAT525D_chkCellVolt.Location = new System.Drawing.Point(455, 12);
            this.BAT525D_chkCellVolt.Name = "BAT525D_chkCellVolt";
            this.BAT525D_chkCellVolt.Size = new System.Drawing.Size(72, 21);
            this.BAT525D_chkCellVolt.TabIndex = 38;
            this.BAT525D_chkCellVolt.Text = "Cell电压";
            this.BAT525D_chkCellVolt.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkProVoltCalibration
            // 
            this.BAT525D_chkProVoltCalibration.AutoSize = true;
            this.BAT525D_chkProVoltCalibration.Enabled = false;
            this.BAT525D_chkProVoltCalibration.Location = new System.Drawing.Point(374, 12);
            this.BAT525D_chkProVoltCalibration.Name = "BAT525D_chkProVoltCalibration";
            this.BAT525D_chkProVoltCalibration.Size = new System.Drawing.Size(75, 21);
            this.BAT525D_chkProVoltCalibration.TabIndex = 37;
            this.BAT525D_chkProVoltCalibration.Text = "编程电压";
            this.BAT525D_chkProVoltCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkDsgCurCalibration30A
            // 
            this.BAT525D_chkDsgCurCalibration30A.AutoSize = true;
            this.BAT525D_chkDsgCurCalibration30A.Enabled = false;
            this.BAT525D_chkDsgCurCalibration30A.Location = new System.Drawing.Point(271, 12);
            this.BAT525D_chkDsgCurCalibration30A.Name = "BAT525D_chkDsgCurCalibration30A";
            this.BAT525D_chkDsgCurCalibration30A.Size = new System.Drawing.Size(97, 21);
            this.BAT525D_chkDsgCurCalibration30A.TabIndex = 36;
            this.BAT525D_chkDsgCurCalibration30A.Text = "放电电流30A";
            this.BAT525D_chkDsgCurCalibration30A.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkDsgCurCalibration
            // 
            this.BAT525D_chkDsgCurCalibration.AutoSize = true;
            this.BAT525D_chkDsgCurCalibration.Location = new System.Drawing.Point(175, 12);
            this.BAT525D_chkDsgCurCalibration.Name = "BAT525D_chkDsgCurCalibration";
            this.BAT525D_chkDsgCurCalibration.Size = new System.Drawing.Size(90, 21);
            this.BAT525D_chkDsgCurCalibration.TabIndex = 35;
            this.BAT525D_chkDsgCurCalibration.Text = "放电电流3A";
            this.BAT525D_chkDsgCurCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkOCVCalibration
            // 
            this.BAT525D_chkOCVCalibration.AutoSize = true;
            this.BAT525D_chkOCVCalibration.Location = new System.Drawing.Point(94, 12);
            this.BAT525D_chkOCVCalibration.Name = "BAT525D_chkOCVCalibration";
            this.BAT525D_chkOCVCalibration.Size = new System.Drawing.Size(75, 21);
            this.BAT525D_chkOCVCalibration.TabIndex = 34;
            this.BAT525D_chkOCVCalibration.Text = "开路电压";
            this.BAT525D_chkOCVCalibration.UseVisualStyleBackColor = true;
            // 
            // BAT525D_chkChgVoltCalibration
            // 
            this.BAT525D_chkChgVoltCalibration.AutoSize = true;
            this.BAT525D_chkChgVoltCalibration.Location = new System.Drawing.Point(13, 12);
            this.BAT525D_chkChgVoltCalibration.Name = "BAT525D_chkChgVoltCalibration";
            this.BAT525D_chkChgVoltCalibration.Size = new System.Drawing.Size(75, 21);
            this.BAT525D_chkChgVoltCalibration.TabIndex = 33;
            this.BAT525D_chkChgVoltCalibration.Text = "充电电压";
            this.BAT525D_chkChgVoltCalibration.UseVisualStyleBackColor = true;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(7, 48);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(75, 38);
            this.chkAll.TabIndex = 60;
            this.chkAll.Text = "校准项目\r\n全选";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // chkCalibration
            // 
            this.chkCalibration.AutoSize = true;
            this.chkCalibration.Location = new System.Drawing.Point(7, 26);
            this.chkCalibration.Name = "chkCalibration";
            this.chkCalibration.Size = new System.Drawing.Size(51, 21);
            this.chkCalibration.TabIndex = 59;
            this.chkCalibration.Text = "校准";
            this.chkCalibration.UseVisualStyleBackColor = true;
            this.chkCalibration.CheckedChanged += new System.EventHandler(this.chkCalibration_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.dgvChkParameter);
            this.groupBox8.Location = new System.Drawing.Point(5, 116);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(945, 238);
            this.groupBox8.TabIndex = 61;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "点检参数配置";
            // 
            // dgvChkParameter
            // 
            this.dgvChkParameter.AllowUserToResizeColumns = false;
            this.dgvChkParameter.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvChkParameter.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvChkParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChkParameter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChkParameter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChkParameter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Type,
            this.Point,
            this.Cycles,
            this.select});
            this.dgvChkParameter.Location = new System.Drawing.Point(4, 18);
            this.dgvChkParameter.Margin = new System.Windows.Forms.Padding(4);
            this.dgvChkParameter.Name = "dgvChkParameter";
            this.dgvChkParameter.RowHeadersVisible = false;
            this.dgvChkParameter.RowHeadersWidth = 72;
            this.dgvChkParameter.RowTemplate.Height = 23;
            this.dgvChkParameter.Size = new System.Drawing.Size(935, 219);
            this.dgvChkParameter.TabIndex = 0;
            this.dgvChkParameter.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvChkParameter_CellBeginEdit);
            this.dgvChkParameter.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvChkParameter_CellFormatting);
            this.dgvChkParameter.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChkParameter_CellValueChanged);
            this.dgvChkParameter.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvChkParameter_CurrentCellDirtyStateChanged);
            this.dgvChkParameter.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvChkParameter_UserDeletedRow);
            // 
            // No
            // 
            this.No.FillWeight = 29F;
            this.No.HeaderText = "序号";
            this.No.Name = "No";
            // 
            // Type
            // 
            this.Type.FillWeight = 80.60863F;
            this.Type.HeaderText = "项目";
            this.Type.MinimumWidth = 9;
            this.Type.Name = "Type";
            // 
            // Point
            // 
            this.Point.FillWeight = 243.9711F;
            this.Point.HeaderText = "点";
            this.Point.MinimumWidth = 9;
            this.Point.Name = "Point";
            this.Point.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Point.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Cycles
            // 
            this.Cycles.FillWeight = 53.8108F;
            this.Cycles.HeaderText = "循环次数";
            this.Cycles.MinimumWidth = 9;
            this.Cycles.Name = "Cycles";
            // 
            // select
            // 
            this.select.FillWeight = 41.57597F;
            this.select.HeaderText = "选中";
            this.select.MinimumWidth = 9;
            this.select.Name = "select";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnImportParam);
            this.groupBox10.Controls.Add(this.txtChkParameterAddr);
            this.groupBox10.Location = new System.Drawing.Point(10, 180);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(487, 85);
            this.groupBox10.TabIndex = 64;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "点检参数导入";
            // 
            // btnImportParam
            // 
            this.btnImportParam.Location = new System.Drawing.Point(390, 49);
            this.btnImportParam.Name = "btnImportParam";
            this.btnImportParam.Size = new System.Drawing.Size(90, 30);
            this.btnImportParam.TabIndex = 1;
            this.btnImportParam.Text = "导入点检参数";
            this.btnImportParam.UseVisualStyleBackColor = true;
            this.btnImportParam.Click += new System.EventHandler(this.btnImportParam_Click);
            // 
            // txtChkParameterAddr
            // 
            this.txtChkParameterAddr.Location = new System.Drawing.Point(10, 22);
            this.txtChkParameterAddr.Name = "txtChkParameterAddr";
            this.txtChkParameterAddr.Size = new System.Drawing.Size(470, 23);
            this.txtChkParameterAddr.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1468, 746);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.chkstcCur_ReadVol);
            this.tabPage4.Controls.Add(this.chkSaveLog);
            this.tabPage4.Controls.Add(this.btnSaveAcc);
            this.tabPage4.Controls.Add(this.btnUnLock);
            this.tabPage4.Controls.Add(this.txtSeal);
            this.tabPage4.Controls.Add(this.cboAccSetEQM);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.dgvACCset);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1460, 716);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "校准精度设置";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // chkstcCur_ReadVol
            // 
            this.chkstcCur_ReadVol.AutoSize = true;
            this.chkstcCur_ReadVol.Location = new System.Drawing.Point(787, 54);
            this.chkstcCur_ReadVol.Name = "chkstcCur_ReadVol";
            this.chkstcCur_ReadVol.Size = new System.Drawing.Size(147, 21);
            this.chkstcCur_ReadVol.TabIndex = 64;
            this.chkstcCur_ReadVol.Text = "静态电流校准判断电压";
            this.chkstcCur_ReadVol.UseVisualStyleBackColor = true;
            this.chkstcCur_ReadVol.Visible = false;
            // 
            // chkSaveLog
            // 
            this.chkSaveLog.AutoSize = true;
            this.chkSaveLog.Location = new System.Drawing.Point(787, 27);
            this.chkSaveLog.Name = "chkSaveLog";
            this.chkSaveLog.Size = new System.Drawing.Size(75, 21);
            this.chkSaveLog.TabIndex = 63;
            this.chkSaveLog.Text = "保存日志";
            this.chkSaveLog.UseVisualStyleBackColor = true;
            // 
            // btnSaveAcc
            // 
            this.btnSaveAcc.Location = new System.Drawing.Point(653, 27);
            this.btnSaveAcc.Name = "btnSaveAcc";
            this.btnSaveAcc.Size = new System.Drawing.Size(89, 35);
            this.btnSaveAcc.TabIndex = 62;
            this.btnSaveAcc.Text = "保存";
            this.btnSaveAcc.UseVisualStyleBackColor = true;
            this.btnSaveAcc.Click += new System.EventHandler(this.btnSaveAcc_Click);
            // 
            // btnUnLock
            // 
            this.btnUnLock.BackColor = System.Drawing.Color.Red;
            this.btnUnLock.Location = new System.Drawing.Point(510, 27);
            this.btnUnLock.Name = "btnUnLock";
            this.btnUnLock.Size = new System.Drawing.Size(80, 35);
            this.btnUnLock.TabIndex = 61;
            this.btnUnLock.Text = "解锁";
            this.btnUnLock.UseVisualStyleBackColor = false;
            this.btnUnLock.Click += new System.EventHandler(this.btnUnLock_Click);
            // 
            // txtSeal
            // 
            this.txtSeal.Location = new System.Drawing.Point(337, 33);
            this.txtSeal.Name = "txtSeal";
            this.txtSeal.PasswordChar = '*';
            this.txtSeal.Size = new System.Drawing.Size(167, 23);
            this.txtSeal.TabIndex = 60;
            this.txtSeal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSeal_KeyDown);
            // 
            // cboAccSetEQM
            // 
            this.cboAccSetEQM.FormattingEnabled = true;
            this.cboAccSetEQM.Items.AddRange(new object[] {
            "BAT525G",
            "BAT525H",
            "BAT525C",
            "BAT525D"});
            this.cboAccSetEQM.Location = new System.Drawing.Point(176, 33);
            this.cboAccSetEQM.Name = "cboAccSetEQM";
            this.cboAccSetEQM.Size = new System.Drawing.Size(121, 25);
            this.cboAccSetEQM.TabIndex = 2;
            this.cboAccSetEQM.SelectedIndexChanged += new System.EventHandler(this.cboAccSetEQM_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(58, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "校准/点检设备类型:";
            // 
            // dgvACCset
            // 
            this.dgvACCset.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvACCset.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvACCset.Location = new System.Drawing.Point(61, 76);
            this.dgvACCset.Name = "dgvACCset";
            this.dgvACCset.RowTemplate.Height = 23;
            this.dgvACCset.Size = new System.Drawing.Size(716, 558);
            this.dgvACCset.TabIndex = 0;
            this.dgvACCset.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvACCset_CellBeginEdit);
            this.dgvACCset.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvACCset_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "测试项目";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "校准精度设置";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "点检精度设置";
            this.Column3.Name = "Column3";
            // 
            // frmVerifyDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 768);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmVerifyDevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备点检校准";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmVerifyDevice_FormClosing);
            this.Load += new System.EventHandler(this.frmVerifyDevice_Load);
            this.contextMenuStripFrom.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVoltage)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabSelectDevice.ResumeLayout(false);
            this.RoughBAT525G.ResumeLayout(false);
            this.RoughBAT525G.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoughCalResult)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.grpDCSourceSet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picTestTatus)).EndInit();
            this.grpMultimeterSet.ResumeLayout(false);
            this.grpMultimeterSet.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResult)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.tabCalibrationSelect.ResumeLayout(false);
            this.BAT525G.ResumeLayout(false);
            this.BAT525G.PerformLayout();
            this.BAT525C.ResumeLayout(false);
            this.BAT525C.PerformLayout();
            this.BAT525D.ResumeLayout(false);
            this.BAT525D.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChkParameter)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvACCset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFrom;
        private System.Windows.Forms.ToolStripMenuItem Open;
        private System.Windows.Forms.ToolStripMenuItem Exit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvVoltage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnDelect;
        private System.Windows.Forms.TextBox txtDtID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDtLine;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnQ;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnRoughCali;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboTesterType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbCHPort;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.DataGridView dgvTestResult;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCalibration;
        private System.Windows.Forms.Button btnStopCalibration;
        private System.Windows.Forms.GroupBox grpMultimeterSet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMultimeterTest;
        private System.Windows.Forms.TextBox txtMultimeterAddress;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnMultimeterConnect;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.ComboBox cboMultimeterConType;
        private System.Windows.Forms.ComboBox cboMultimeterType;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.ComboBox cboResSpecification;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cboCalEQMType;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cboEqmCalSerialPort;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtSNnum;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.TabControl tabCalibrationSelect;
        private System.Windows.Forms.TabPage BAT525G;
        private System.Windows.Forms.CheckBox SBT825G_chkCNTStNegative;
        private System.Windows.Forms.CheckBox SBT825G_chkCNTStPositive;
        private System.Windows.Forms.CheckBox SBT825G_chkDCIRCalibration;
        private System.Windows.Forms.CheckBox SBT825G_chkNTCCalibration;
        private System.Windows.Forms.CheckBox SBT825G_chkStCur4Calibration;
        private System.Windows.Forms.CheckBox SBT825G_chkStCur3Calibration;
        private System.Windows.Forms.CheckBox SBT825G_chkStCur2Calibration;
        private System.Windows.Forms.CheckBox SBT825G_chkStCurCalibration;
        private System.Windows.Forms.CheckBox SBT825G_chkLoadPartCur;
        private System.Windows.Forms.CheckBox SBT825G_chkLoadPartVolt;
        private System.Windows.Forms.CheckBox SBT825G_chkCellCur;
        private System.Windows.Forms.CheckBox SBT825G_chkCellVolt;
        private System.Windows.Forms.CheckBox SBT825G_chkProVoltCalibration;
        private System.Windows.Forms.CheckBox SBT825G_chkDsgCurCalibration30A;
        private System.Windows.Forms.CheckBox SBT825G_chkDsgCurCalibration;
        private System.Windows.Forms.CheckBox SBT825G_chkOCVCalibration;
        private System.Windows.Forms.CheckBox SBT825G_chkChgVoltCalibration;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkCalibration;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Frist;
        private System.Windows.Forms.TabPage BAT525C;
        private System.Windows.Forms.ComboBox cboSaveErrrRule;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtCalorCheckUser;
        private System.Windows.Forms.TextBox txtCalorCheckDepart;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.DataGridView dgvChkParameter;
        private System.Windows.Forms.GroupBox grpDCSourceSet;
        private System.Windows.Forms.ListBox lstShowResult;
        private System.Windows.Forms.CheckBox chkCheck;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkReadEE;
        private System.Windows.Forms.TabControl tabSelectDevice;
        private System.Windows.Forms.TabPage HomePage;
        private System.Windows.Forms.TabPage RoughBAT525G;
        private System.Windows.Forms.CheckBox SBT825G_chkCNTStNegativeRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkCNTStPositiveRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkDCIRRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkNTCRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkStCur4RoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkStCur3RoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkStCur2RoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkStCurRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkLoadPartCurRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkLoadPartVoltRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkCellCurRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkProVoltRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkDsgCurRoughCal30A;
        private System.Windows.Forms.CheckBox SBT825G_chkDsgCurRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkOCVRoughCal;
        private System.Windows.Forms.CheckBox SBT825G_chkChgVoltRoughCal;
        private System.Windows.Forms.CheckBox chkRoughAllWrite;
        private System.Windows.Forms.CheckBox chkRoughWrite;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvRoughCalResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nums;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceType;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestIDs;
        private System.Windows.Forms.DataGridViewTextBoxColumn TestTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn SetValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn MulValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeviceValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Err1s;
        private System.Windows.Forms.DataGridViewTextBoxColumn Err2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllowErr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Judge;
        private System.Windows.Forms.Button btnFileAddr;
        private System.Windows.Forms.TextBox txtConfigFile;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chkReadAllEE;
        private System.Windows.Forms.CheckBox SBT825G_chkCellVoltRoughCal;
        private System.Windows.Forms.CheckBox chkWriteCheck;
        private System.Windows.Forms.CheckBox chkIDRcheck;
        private System.Windows.Forms.TextBox txtIDRAcc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox cboAccSetEQM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvACCset;
        private System.Windows.Forms.TextBox txtSNdeviceType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkAloneSave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRoughType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRoughNum;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button btnUnLock;
        private System.Windows.Forms.TextBox txtSeal;
        private System.Windows.Forms.Button btnSaveAcc;
        private System.Windows.Forms.CheckBox chkSaveLog;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button btnImportParam;
        private System.Windows.Forms.TextBox txtChkParameterAddr;
        private System.Windows.Forms.CheckBox chkstcCur_ReadVol;
        private System.Windows.Forms.ComboBox cboDMMcom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox BAT525C_chkIDRCalibration;
        private System.Windows.Forms.CheckBox BAT525C_chkDCIRCalibration;
        private System.Windows.Forms.CheckBox BAT525C_chkNTCCalibration;
        private System.Windows.Forms.CheckBox BAT525C_chkStCurnACalibration;
        private System.Windows.Forms.CheckBox BAT525C__chkStCuruACalibration;
        private System.Windows.Forms.CheckBox BAT525C_chkLoadPartCur;
        private System.Windows.Forms.CheckBox BAT525C_chkLoadPartVolt;
        private System.Windows.Forms.CheckBox BAT525C_chkCellCur;
        private System.Windows.Forms.CheckBox BAT525C_chkCellVolt;
        private System.Windows.Forms.CheckBox BAT525C_chkProVoltCalibration;
        private System.Windows.Forms.CheckBox BAT525C_chkDsgCurCalibration30A;
        private System.Windows.Forms.CheckBox BAT525C_chkDsgCurCalibration;
        private System.Windows.Forms.CheckBox BAT525C_chkOCVCalibration;
        private System.Windows.Forms.CheckBox BAT525C_chkChgVoltCalibration;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Point;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cycles;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.CheckBox chkZXSource;
        private System.Windows.Forms.TabPage BAT525D;
        private System.Windows.Forms.CheckBox BAT525D_chkIDRCalibration;
        private System.Windows.Forms.CheckBox BAT525D_chkDCIRCalibration;
        private System.Windows.Forms.CheckBox BAT525D_chkNTCCalibration;
        private System.Windows.Forms.CheckBox BAT525D_chkStCurnACalibration;
        private System.Windows.Forms.CheckBox BAT525D__chkStCuruACalibration;
        private System.Windows.Forms.CheckBox BAT525D_chkLoadPartCur;
        private System.Windows.Forms.CheckBox BAT525D_chkLoadPartVolt;
        private System.Windows.Forms.CheckBox BAT525D_chkCellCur;
        private System.Windows.Forms.CheckBox BAT525D_chkCellVolt;
        private System.Windows.Forms.CheckBox BAT525D_chkProVoltCalibration;
        private System.Windows.Forms.CheckBox BAT525D_chkDsgCurCalibration30A;
        private System.Windows.Forms.CheckBox BAT525D_chkDsgCurCalibration;
        private System.Windows.Forms.CheckBox BAT525D_chkOCVCalibration;
        private System.Windows.Forms.CheckBox BAT525D_chkChgVoltCalibration;
        private System.Windows.Forms.CheckBox chkWriteSNCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn 序号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 测试ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 测试类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设置值;
        private System.Windows.Forms.DataGridViewTextBoxColumn 万用表读值;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备采样值;
        private System.Windows.Forms.DataGridViewTextBoxColumn 误差1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 误差2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 允许误差;
        private System.Windows.Forms.DataGridViewTextBoxColumn 是否合格;
        private System.Windows.Forms.ListBox lstRoughCalResult;
        private System.Windows.Forms.CheckBox chkExtRes;
        private System.Windows.Forms.CheckBox chkDCIRcheck;
        private System.Windows.Forms.TextBox txtDCIRacc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkReadSNCodeStart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDateTime;
        private System.Windows.Forms.Button btnGetNowTime;
        private System.Windows.Forms.PictureBox picTestTatus;
    }
}