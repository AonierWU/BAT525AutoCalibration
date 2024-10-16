using NationalInstruments.NI4882;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TestSystem_Pack
{
    public partial class SBT825G : MyEqmCmd
    {
        //public override bool MixChgDsgTest(SelectChgDsgMode chgDsgMode, int SetVolt, int SetCur, int Delay, bool isDisable, ReadFlag flag, out double dblVolt, out double dblCur, out string Errcode)
        //{
        //    return ChgDsgTest(chgDsgMode, SetVolt, SetCur, Delay, isDisable, flag, out dblVolt, out dblCur, out Errcode);

        //}
        //public override bool MixCNTTest(bool chk10mA, int Delay, bool isDisable, out double dblCur, out string Errcode)
        //{
        //    return CNTTest(chk10mA, Delay, isDisable, out dblCur, out Errcode);
        //}

        //public override bool MixCocpOrDocp(SelectChgDsgMode chgDsgMode, int SetLoadVolt, int SetLoadCur, double SetProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out string Errcode)
        //{
        //    return CocpOrDocp(chgDsgMode, SetLoadVolt, SetLoadCur, SetProDelay, TrigPercent, isDisable, out dblProDelay, out Errcode);
        //}

        //public override bool MixCocpOrDocpScan(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetCurMin, int SetCurMax, int stepC, int stepT, double SetProDelay, double RealProTime, int TrigPercent, bool isDisable, out double dblProCur, out string Errcode)
        //{
        //    return CocpOrDocpScan(LoadchgDsgMode, SetLoadVolt, SetCurMin, SetCurMax, stepC, stepT, SetProDelay, RealProTime, TrigPercent, isDisable, out dblProCur, out Errcode);
        //}

        //public override bool MixComminit(string mode, string upRes, string upVolt, byte aDDR, string speed, out string Errcode)
        //{
        //    return Comminit(mode, upRes, upVolt, aDDR, speed, out Errcode);
        //}

        //public override bool MixCommPinEnable(bool blSda, bool blScl, out string Errcode)
        //{
        //    return CommPinEnable(blSda, blScl, out Errcode);
        //}

        //public override bool MixCommRead(byte Addr, byte RegAddr, int Len, out byte[] Data, out string Errcode)
        //{
        //    return CommRead(Addr, RegAddr, Len, out Data, out Errcode);
        //}

        //public override bool MixCommWrite(byte Addr, byte RegAddr, byte[] Data, out string Errcode)
        //{
        //    return CommWrite(Addr, RegAddr, Data, out Errcode);
        //}

        //public override bool MixDCIR(SelectChgDsgMode chgDsgMode, int SetCur, int Delay, bool isDisable, out double dblDCIR, out string Errcode)
        //{
        //    return DCIR(chgDsgMode, SetCur, Delay, isDisable, out dblDCIR, out Errcode);
        //}

        //public override bool MixDisableCell(out string Errcode)
        //{
        //    return DisModels("07", out Errcode);

        //}

        //public override bool MixDisableChgDsgTest(out string Errcode)
        //{
        //    return DisModels("0C", out Errcode);

        //}

        //public override bool MixDisableCNTTest(out string Errcode)
        //{
        //    return DisModels("0A", out Errcode);

        //}

        //public override bool MixDisableIR(out string Errcode)
        //{
        //    return DisModels("03", out Errcode);

        //}

        //public override bool MixDisableLoadPartChgDsg(out string Errcode)
        //{
        //    return DisModels("05", out Errcode);

        //}

        //public override bool MixDisableProgVolt(out string Errcode)
        //{
        //    return DisModels("11", out Errcode);

        //}

        //public override bool MixDisableReadStaticCurr(out string Errcode)
        //{
        //    return DisModels("0E", out Errcode);

        //}

        //public override bool MixDisableSecOVP(out string Errcode)
        //{
        //    return DisModels("13", out Errcode);
        //}

        //public override bool MixDisReadVolt(out string Errcode)
        //{
        //    return DisModels("15", out Errcode);
        //}

        public override bool MixEQMInit(out string Errcode)
        {
            return EQMInit(out Errcode);
        }

        //public override bool MixLoadPartChgDsg(SelectChgDsgMode chgDsgMode, int setVolt, int setCur, int Delay, bool isDisable, ReadFlag flag, out double dblVolt, out double dblCur, out string Errcode)
        //{
        //    return LoadPartChgDsg(chgDsgMode, setVolt, setCur, Delay, isDisable, flag, out dblVolt, out dblCur, out Errcode);
        //}

        //public override bool MixOVPorUVP(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetLoadCur, int SetCellVolt, double SetProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out string Errcode)
        //{
        //    return OVPorUVP(LoadchgDsgMode, SetLoadVolt, SetLoadCur, SetCellVolt, SetProDelay, TrigPercent, isDisable, out dblProDelay, out Errcode);
        //}

        //public override bool MixOVPorUVPScan(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetLoadCur, int SetCellVoltMin, int SetCellVoltMax, int stepV, int stepT, double SetProDelay, double RealProDelay, int TrigPercent, bool isDisable, out double dblReleaseVolt, out string Errcode)
        //{
        //    return OVPorUVPScan(LoadchgDsgMode, SetLoadVolt, SetLoadCur, SetCellVoltMin, SetCellVoltMax, stepV, stepT, SetProDelay, RealProDelay, TrigPercent, isDisable, out dblReleaseVolt, out Errcode);
        //}

        //public override bool MixReadIR(SelectIRType iRPin, SelectIRRange iRRange, SelectIDRPort IdrPort, int Delay, bool isDisable, out double dblIR, out string Errcode)
        //{
        //    dblIR = 0.0f;
        //    Errcode = "";
        //    bool blFlag = false;


        //    if (!ReadIR(iRPin, iRRange, Delay, isDisable, out dblIR, out Errcode))
        //    {
        //        Errcode += "/IDR读取失败";
        //        blFlag = false;
        //    }
        //    else
        //        blFlag = true;


        //    //Thread.Sleep(100);
        //    return blFlag;
        //}

        //public override bool MixReadOneTimeStaticCurr(SelectSCurrRange sCurrRange, bool isDisable, out double dblSCur, out string Errcode)
        //{
        //    return ReadOneTimeStaticCurr(sCurrRange, isDisable, out dblSCur, out Errcode);
        //}

        //public override bool MixReadStaticCurr(SelectSCurrRange sCurrRange, int Delay, int iCycle, bool blSO, bool isDisable, out double dblSCur, out string Errcode)
        //{
        //    return ReadStaticCurr(sCurrRange, Delay, iCycle, blSO, isDisable, out dblSCur, out Errcode);
        //}

        //public override bool MixReadVolt(SelectVoltPin voltPin, int Delay, bool isDisable, ReadFlag flag, out double dblVolt, out string Errcode)
        //{
        //    return ReadPinVolt(voltPin, Delay, isDisable, flag, out dblVolt, out Errcode);
        //}

        //public override bool MixSecOVP(bool blSaveT, int SetVolt, double SetProTime, bool isDisable, out double dblProDelay, out string Errcode)
        //{
        //    return SecOVP(blSaveT, SetVolt, SetProTime, isDisable, out dblProDelay, out Errcode);
        //}

        //public override bool MixSecOVPScan(bool blSaveT, int SetVoltMin, int SetVoltMax, int SetProTime, int stepV, int stepT, double RealProTime, bool isDisable, out double dblProDelay, out double dblProVolt, out string Errcode)
        //{
        //    return SecOVPScan(blSaveT, SetVoltMin, SetVoltMax, SetProTime, stepV, stepT, RealProTime, isDisable, out dblProDelay, out dblProVolt, out Errcode);
        //}
        //public override bool MixOVPorUVPRelease(SelectChgDsgMode LoadchgDsgMode, int ReCellVoltMin, int ReCellVoltMax, double ReleaseTime, int StepVolt, SelectReleaseMode selectReleaseMode, int LoadVolt, int LoadCurr, out double dblReleaseVolt, out string Errcode)
        //{
        //    return OVPorUVPRelease(LoadchgDsgMode, ReCellVoltMin, ReCellVoltMax, ReleaseTime, StepVolt, selectReleaseMode, LoadVolt, LoadCurr, out dblReleaseVolt, out Errcode);
        //}
        //public override bool MixSetCell(int setVolt, int setCur, int Delay, bool isDisable, ReadFlag flag, bool blReadOutValue, out double dblVolt, out double dblCur, out string Errcode)
        //{
        //    return SetCell(setVolt, setCur, Delay, isDisable, flag, blReadOutValue, out dblVolt, out dblCur, out Errcode);
        //}

        //public override bool MixSetProgVolt(int SetVolt, int Delay, bool isDisable, out double dblVolt, out string Errcode)
        //{
        //    return SetProgVolt(SetVolt, Delay, isDisable, out dblVolt, out Errcode);
        //}

        //public override bool MixShortTest(SelectChgDsgMode chgDsgMode, int SetLoadVolt, int SetLoadCur, int SetProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out string Errcode)
        //{
        //    return ShortTest(chgDsgMode, SetLoadVolt, SetLoadCur, SetProDelay, TrigPercent, isDisable, out dblProDelay, out Errcode);
        //}

        //public override bool MixShortTestScan(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetCurMin, int SetCurMax, int stepC, int stepT, double SetProDelay, double RealProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out double dblProCur, out string Errcode)
        //{
        //    return ShortTestScan(LoadchgDsgMode, SetLoadVolt, SetCurMin, SetCurMax, stepC, stepT, SetProDelay, RealProDelay, TrigPercent, isDisable, out dblProDelay, out dblProCur, out Errcode);
        //}

        //public override bool MixReadErrMsg(out string Msg, out string Errcode)
        //{
        //    return ReadErrMsg(out Msg, out Errcode);
        //}

        //public override bool MixShortActiveEnable(int wait, bool isDisable, out string Errcode)
        //{
        //    return ShortActiveEnable(wait, isDisable, out Errcode);
        //}

        //public override bool MixShortActiveDisEnable(out string Errcode)
        //{
        //    return DisModels("31", out Errcode);
        //}
        //public override bool MixReadTemp(out double dblTemp, out string Errcode)
        //{
        //    dblTemp = 0.0f;
        //    if (!SetCommRelayConn(true, out Errcode))
        //        return false;

        //    return ReadTemp(out dblTemp, out Errcode);
        //}

        //public override bool MixRelayBoardCmd(string MixCmd, out string strErrorCode)
        //{

        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}


        //public override bool MixResetRelayBoard(out string strErrorCode)
        //{


        //    string MixCmd = "30-00";
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //    // return ResetRelayBoard();

        //}
        //public override bool MixAllCmdSend(out string strErrorCode)
        //{
        //    return AllCmdSend(out strErrorCode);
        //}

        //string strErrorCode;
        //public override bool MixMCUDelay(Int32 Delay)
        //{
        //    return MCUDelay(Delay, out strErrorCode);
        //}

        ////继电器板相关指令
        //public override bool MixPrimaryProtLoadRelay(bool Enable, out string strErrorCode)
        //{
        //    //if(!RY6EN_Enable(Enable))
        //    //    return false;
        //    //if (!TP1_TP2_Enable(Enable))
        //    //    return false;
        //    //if (!Ln_Pn_Enable(Enable))
        //    //    return false;
        //    //if (!LSn_PSn_Enable(Enable))
        //    //    return false;
        //    //if (!Lp_Bn_Enable(Enable))
        //    //    return false;
        //    //if (!LSp_BSn_Enable(Enable))
        //    //    return false;
        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "26-" + EN + ",";
        //    MixCmd += "01-" + EN + ",";
        //    MixCmd += "04-" + EN + ",";
        //    MixCmd += "0A-" + EN + ",";
        //    MixCmd += "06-" + EN + ",";
        //    MixCmd += "02-" + EN + ",";//HW有要求（过流/短路)时需要短接J1P-与J2P-
        //    MixCmd += "0E-" + EN;

        //    return RelayBoardCmd(MixCmd, out strErrorCode);

        //}
        //public override bool MixLoadToAllCircleRelay(bool Enable, out string strErrorCode)
        //{
        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "26-" + EN + ",";
        //    MixCmd += "04-" + EN + ",";
        //    MixCmd += "0A-" + EN + ",";
        //    MixCmd += "06-" + EN + ",";
        //    MixCmd += "0E-" + EN;

        //    return RelayBoardCmd(MixCmd, out strErrorCode);

        //}
        //public override bool MixSecondProtLoadRelay(bool Enable, out string strErrorCode)
        //{
        //    //if (!RY6EN_Enable(Enable))
        //    //    return false;
        //    //if (!Lp_Bn_Enable(Enable))
        //    //    return false;
        //    //if (!LSp_BSn_Enable(Enable))
        //    //    return false;
        //    //if (!Ln_TP1_Enable(Enable))
        //    //    return false;
        //    //if (!LSn_TP1S_Enable(Enable))
        //    //    return false;
        //    //return true;

        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "26-" + EN + ",";
        //    MixCmd += "06-" + EN + ",";
        //    MixCmd += "0E-" + EN + ",";
        //    MixCmd += "03-" + EN + ",";
        //    MixCmd += "09-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}
        //public override bool MixSetOCVCycle(int iCycle, out string strErrorCode)
        //{
        //    return SetOCVCycle(iCycle, out strErrorCode);

        //}
        //public override bool MixOCVEnable(bool Enable, out string strErrorCode)
        //{

        //    //if (!OCV_PSp_Enable(Enable))
        //    //    return false;
        //    //if (!OCV_PSn_Enable(Enable))
        //    //    return false;
        //    //return true;

        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "13-" + EN + ",";
        //    MixCmd += "12-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}

        //public override bool MixOCVCpEnable(bool Enable, out string strErrorCode)
        //{
        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "14-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}




        //public override bool MixLS_PS_Enable(bool Enable, out string strErrorCode)
        //{
        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "0A-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}


        //public override bool MixFilterCap(bool Enable, string Type, out string strErrorCode)
        //{
        //    strErrorCode = "";
        //    string EN = "01";
        //    string En = "00";

        //    string MixCmd = "";

        //    if (Type == "一级")
        //    {
        //        if (Enable)
        //        {
        //            MixCmd += "2C-" + EN + ",";
        //            MixCmd += "2D-" + EN;
        //        }
        //        else
        //        {
        //            MixCmd += "2C-" + En + ",";
        //            MixCmd += "2D-" + En;
        //        }
        //        return RelayBoardCmd(MixCmd, out strErrorCode);
        //    }
        //    else if (Type == "二级")
        //    {
        //        if (Enable)
        //        {
        //            MixCmd += "2C-" + EN + ",";
        //            MixCmd += "2D-" + En;
        //        }
        //        else
        //        {
        //            MixCmd += "2C-" + En + ",";
        //            MixCmd += "2D-" + En;
        //        }
        //        return RelayBoardCmd(MixCmd, out strErrorCode);
        //    }
        //    else
        //        return false;
        //}
        //public override bool MixFilterCapNew(bool Enable, string Type, out string strErrorCode)
        //{
        //    strErrorCode = "";
        //    string EN = "00";
        //    //string En = "01";
        //    if (Enable)
        //    {
        //        EN = "01";
        //        //En = "00";
        //    }
        //    string MixCmd = "";


        //    if (Type == "一级")
        //    {
        //        //if (!RY8EN_Enable(Enable))
        //        //    return false;
        //        //if (!RY9EN_Enable(!Enable))
        //        //    return false;
        //        //return true;

        //        MixCmd += "2C-" + EN + ",";
        //        MixCmd += "2D-" + EN;
        //        return RelayBoardCmd(MixCmd, out strErrorCode);
        //    }
        //    else if (Type == "二级")
        //    {
        //        //if (!RY8EN_Enable(Enable))
        //        //    return false;
        //        //if (!RY9EN_Enable(Enable))
        //        //    return false;
        //        //return true;
        //        MixCmd += "2C-" + EN + ",";
        //        if (!Enable)
        //            MixCmd += "2D-01";
        //        else
        //            MixCmd += "2D-00";
        //        return RelayBoardCmd(MixCmd, out strErrorCode);
        //    }
        //    else
        //        return false;
        //}

        //public override bool MixSwitchCellToB1B2(bool Enable, out string strErrorCode)
        //{
        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "26-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}

        //public override bool MixLoadSwitchBpToPp(bool Enable, out string strErrorCode)
        //{
        //    //if (!RY1EN_Enable(Enable))
        //    //    return false;
        //    //if(!RY3EN_Enable(Enable))
        //    //    return false;

        //    //if (!LSn_BSp_Enable(Enable))
        //    //    return false;
        //    //if(!LSp_PSp_Enable(Enable))
        //    //    return false;
        //    //return true;

        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "20-" + EN + ",";
        //    MixCmd += "23-" + EN + ",";
        //    MixCmd += "08-" + EN + ",";
        //    MixCmd += "0D-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}

        //public override bool MixLoadSwitchBpToJ2P(bool Enable, out string strErrorCode)
        //{
        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "21-" + EN + ",";
        //    MixCmd += "24-" + EN + ",";
        //    MixCmd += "0B-" + EN + ",";
        //    MixCmd += "10-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}

        //public override bool MixLoadSwitchBnToPnAndLSnToPSnBSn(bool Enable, out string strErrorCode)
        //{
        //    //if (!Ln_Pn_Enable(Enable))
        //    //    return false;
        //    //if (!Lp_Bn_Enable(Enable))
        //    //    return false;

        //    //if (!LSn_PSn_Enable(Enable))
        //    //    return false;
        //    //if (!LSp_BSn_Enable(Enable))
        //    //    return false;

        //    //return true;

        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "04-" + EN + ",";
        //    MixCmd += "06-" + EN + ",";
        //    MixCmd += "0A-" + EN + ",";
        //    MixCmd += "0E-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}
        //public override bool MixLoadSwitchBnToJ2PnAndLSnToJ2PSn(bool Enable, out string strErrorCode)
        //{

        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "02-" + EN + ",";
        //    MixCmd += "06-" + EN + ",";
        //    MixCmd += "07-" + EN + ",";
        //    MixCmd += "0E-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}

        //public override bool MixLoadSwitchBnToPn(bool Enable, out string strErrorCode)
        //{
        //    // if (!Ln_Pn_Enable(Enable))
        //    //    return false;
        //    //if (!Lp_Bn_Enable(Enable))
        //    //    return false;

        //    //return true;

        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "04-" + EN + ",";
        //    MixCmd += "06-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}

        //public override bool MixShortRY7(bool Enable, out string strErrorCode)//短接S+/S-
        //{
        //    string EN = "00";
        //    if (Enable)
        //        EN = "01";
        //    string MixCmd = "";
        //    MixCmd += "27-" + EN;
        //    return RelayBoardCmd(MixCmd, out strErrorCode);
        //}

        //设备校准
        public override bool MixChgVolCalibration()
        {
            return ChgVolCalibration();
        }
        public override bool MixDsgCurCalibration()
        {
            return DsgCurCalibration();
        }
        public override bool MixOCVCalibration()
        {
            return OCVCalibration();
        }
        public override bool MixProVoltCalibration()
        {
            return ProVoltCalibration();
        }
        public override bool MixCellVoltCalibration()
        {
            return CellVoltCalibration();
        }
        public override bool MixCellCurCalibration()
        {
            return CellCurCalibration();
        }
        public override bool MixCNTCalibration()
        {
            return CNTCalibration();
        }
        public override bool MixNTCCalibration()
        {
            return NTCCalibration();
        }
        public override bool MixIDRCalibration()
        {
            return IDRCalibration();
        }
        public override bool MixDCIRCalibration()
        {
            return DCIRCalibration();
        }
        public override bool MixStCurCalibration()
        {
            return StCurCalibration();
        }
        public override bool MixNTCRange(byte range)
        {
            return NTCRange(range);
        }
        public override bool MixStCurRange(byte R, byte range)
        {
            return StCurRange(R, range);
        }
        public override bool MixLoadVoltCalibration()
        {
            return LoadVoltCalibration();
        }
        public override bool MixLoadCurCalibration()
        {
            return LoadCurCalibration();
        }
        public override bool MixSetCurRange(byte range)
        {
            return SetCurRange(range);
        }
        public override bool MixSendChgVolt(byte DL, byte DH)
        {
            return SendChgVolt(DL, DH);
        }
        public override bool MixSendProVolt(byte DL, byte DH)
        {
            return SendProVolt(DL, DH);
        }
        public override bool MixSendCellVolt(byte DL, byte DH)
        {
            return SendCellVolt(DL, DH);
        }
        public override bool MixSendLoadPartVolt(byte DL, byte DH)
        {
            return SendLoadPartVolt(DL, DH);
        }
        public override bool MixSendLoadPartCur(byte DL, byte DH)
        {
            return SendLoadPartCur(DL, DH);
        }
        public override bool MixSendDsgCur(byte DL, byte DH)
        {
            return SendDsgCur(DL, DH);
        }
        public override bool MixSendCellCur(byte DL, byte DH)
        {
            return SendCellCur(DL, DH);
        }
        public override bool MixReadVoltADCCaliValue(out byte DL, out byte DM, out byte DH)
        {
            DM = 0x00;
            return ReadVoltADCCaliValue(out DL, out DH);
        }
        public override bool MixReadCurADCCaliValue(out byte DL, out byte DM, out byte DH, out byte DL2, out byte DH2)
        {
            DM = 0x00; DL2 = 0x00;DH2 = 0x00;
            return ReadCurADCCaliValue(out DL, out DH);
        }
        public override bool MixReadProADCCaliValue(out byte DL, out byte DM, out byte DH)
        {
            DM = 0X00;
            return ReadProADCCaliValue(out DL, out DH);
        }
        public override bool MixWriteVoltDAC_ADCCaliValue(byte Addr, byte DL, byte DH)
        {
            return WriteVoltDAC_ADCCaliValue(Addr, DL, DH);
        }
        public override bool MixWriteVoltADCCaliValue(byte Addr, byte DL, byte DM, byte DH)
        {
            return true;
        }
        public override bool MixWriteOCV_DACCaliValue(byte Addr, byte DL, byte DM, byte DH)
        {
            return WriteOCV_DACCaliValue(Addr, DL, DH);
        }
        public override bool MixWritePro_ADCValue(byte Addr, byte DL, byte DH)
        {
            return WritePro_ADCValue(Addr, DL, DH);
        }
        public override bool MixReadOCV_DACCaliValue(byte Addr)
        {
            return ReadOCV_DACCaliValue(Addr);
        }
        public override bool MixSetVoltValue(double voltage)
        {
            return SetVoltValue(voltage);
        }
        public override bool MixSetProVoltValue(int voltage)
        {
            return SetProVoltValue(voltage);
        }
        public override bool MixSetCellVoltValue(double voltage)
        {
            return SetCellVoltValue(voltage);
        }
        public override bool MixSetLoadPartVoltValue(int Voltage)
        {
            return SetLoadPartVoltValue(Voltage);
        }
        public override bool MixSetCellCurValue(int current)
        {
            return SetCellCurValue(current);
        }
        public override bool MixSetLoadPartCurValue(int current)
        {
            return SetLoadPartCurValue(current);
        }
        public override bool MixSetCurValue(byte Rang, double Current)
        {
            return SetCurValue(Current);
        }
        public override bool MixReadVoltValue(out double dblVoltage)
        {
            return ReadVoltValue(out dblVoltage);
        }
        public override bool MixReadCNTValue(byte Statu, out double dblVoltage)
        {
            return ReadCNTValue(Statu, out dblVoltage);
        }
        public override bool MixReadProVoltValue(out double dblVoltage)
        {
            return ReadProVoltValue(out dblVoltage);
        }
        public override bool MixReadCurValue(out double dblCurrent)
        {
            return ReadCurValue(out dblCurrent);
        }
        public override bool MixReadOCVValue(out double dblOCV)
        {
            return ReadOCVValue(out dblOCV);
        }
        public override bool MixReadPortVolValue(out double dblPortVol)
        {
            throw new NotImplementedException();
        }
        public override bool MixReadCellVoltValue(out double dblCellVolt)
        {
            return ReadCellVoltValue(out dblCellVolt);
        }
        public override bool MixReadLoadPatVoltValue(out double dblCellVolt)
        {
            return ReadLoadPatVoltValue(out dblCellVolt);
        }
        public override bool MixReadCellCurValue(out double dblCellCur)
        {
            return ReadCellCurValue(out dblCellCur);
        }
        public override bool MixReadStCurValue(byte R, byte Range, out double dblCellCur)
        {
            return ReadStCurValue(R, Range, out dblCellCur);
        }
        public override bool MixReadNTCValue(byte R, byte Range, out double dblCellCur)
        {
            return ReadNTCValue(R, Range, out dblCellCur);
        }
        public override bool MixReadDCIRValue(byte Range, out double dblCellCur)
        {
            return ReadDCIRValue(out dblCellCur);
        }
        public override bool MixReadLoadPartCurValue(out double dblCellCur)
        {
            return ReadLoadPartCurValue(out dblCellCur);
        }
        public override bool MixWriteOCV_DAC(out byte DL, out byte DM, out byte DH, out byte DL2, out byte DM2, out byte DH2)
        {
            DM = 0X00;DL2 = 0X00;DM2 = 0X00;DH2 = 0X00;
            return WriteOCV_DAC(out DL, out DH);
        }
        public override bool MixWriteProVolt_DAC(byte Addr, byte DL, byte DH)
        {
            return WriteProVolt_DAC(Addr, DL, DH);
        }
        public override bool MixWriteDCIR_DAC(byte Addr, byte DL, byte DH)
        {
            return WriteDCIR_DAC(Addr, DL, DH);
        }
        public override bool MixWriteCellVolt_DAC(byte Addr, byte DL, byte DH)
        {
            return WriteCellVolt_DAC(Addr, DL, DH);
        }
        public override bool MixWriteNTC_DAC(byte Addr1, byte DL1, byte DH1, byte Addr2, byte DL2, byte DH2, byte Addr3, byte DL3, byte DH3)
        {
            return WriteNTC_DAC(Addr1, DL1, DH1, Addr2, DL2, DH2, Addr3, DL3, DH3);
        }
        public override bool MixWriteLoadPart_DAC(byte Addr, byte DL, byte DH)
        {
            return WriteLoadPart_DAC(Addr, DL, DH);
        }
        public override bool MixWriteDCIR_ADC(byte[] ADC_L, byte[] ADC_H)
        {
            return WriteDCIR_ADC(ADC_L, ADC_H);
        }
        public override bool MixWriteIDR_ADC(string Write, byte[] ADC_L, byte[] ADC_H, byte[] Buf_L, byte[] Buf_M, byte[] Buf_H, byte[] Buf_HH)
        {
            return WriteIDR_ADC(Write, ADC_L, ADC_H, Buf_L, Buf_M, Buf_H, Buf_HH);
        }
        public override bool MixReadCNT_ADC(out byte DL, out byte DH)
        {
            return ReadCNT_ADC(out DL, out DH);
        }
        public override bool MixWriteCNT_ADC(byte Add, byte DL, byte DH)
        {
            return WriteCNT_ADC(Add, DL, DH);
        }
        public override bool MixRoughWriteCNT_ADC(string Write, byte[] ADC_L, byte[] ADC_H)
        {
            return RoughWriteCNT_ADC(Write, ADC_L, ADC_H);
        }
        public override bool MixWritePVVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            return WritePVVol_ADCandDAC(DAC_L, DAC_H, ADC_L, ADC_H);
        }
        public override bool MixWriteOCV_ADC(byte[] ADC_L, byte[] ADC_H)
        {
            return WriteOCV_ADC(ADC_L, ADC_H);
        }
        public override bool MixWriteLVVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            return WriteLVVol_ADCandDAC(DAC_L, DAC_H, ADC_L, ADC_H);
        }
        public override bool MixWriteCellVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            return WriteCellVol_ADCandDAC(DAC_L, DAC_H, ADC_L, ADC_H);
        }
        public override bool MixWriteCellCur_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            return WriteCellCur_ADCandDAC(DAC_L, DAC_H, ADC_L, ADC_H);
        }
        public override bool MixWriteLVCur_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            return WriteLVCur_ADCandDAC(DAC_L, DAC_H, ADC_L, ADC_H);
        }
        public override bool MixWriteCHGVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            return WriteCHGVol_ADCandDAC(DAC_L, DAC_H, ADC_L, ADC_H);
        }
        public override bool MixWriteDSGCur1_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            return WriteDSGCur1_ADCandDAC(DAC_L, DAC_H, ADC_L, ADC_H);
        }
        public override bool MixWriteDSGCur2_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)//大电流
        {
            return WriteDSGCur2_ADCandDAC(DAC_L, DAC_H, ADC_L, ADC_H);
        }
        public override bool MixWriteSTC_ADC(string Write, byte[] ADC_L, byte[] ADC_H)
        {
            return WriteSTC_ADC(Write, ADC_L, ADC_H);
        }
        public override bool MixReadCellVolt_ADCValue(out byte DL, out byte DM, out byte DH)
        {
            DM = 0x00;
            return ReadCellVolt_ADCValue(out DL, out DH);
        }
        public override bool MixReadCellCur_ADCValue(out byte DL, out byte DM, out byte DH)
        {
            DM = 0x00;
            return ReadCellCur_ADCValue(out DL, out DH);
        }
        public override bool MixReadStCur_ADCValue(byte Rang, out byte DL, out byte DH)
        {
            return ReadStCur_ADCValue(out DL, out DH);
        }
        public override bool MixReadNTC_ADCValue(byte Index, byte R, out byte DL1, out byte DH1, out byte DL2, out byte DH2, out byte DL3, out byte DH3)
        {
            return ReadNTC_ADCValue(Index, R, out DL1, out DH1, out DL2, out DH2, out DL3, out DH3);
        }
        public override bool MixReadDCIR_ADCValue(byte Rang,out byte DL, out byte DH)
        {
            return ReadDCIR_ADCValue(out DL, out DH);
        }
        public override bool MixReadLoadPartVolt_ADCValue(out byte DL, out byte DM, out byte DH)
        {
            DM = 0x00;
            return ReadLoadPartVolt_ADCValue(out DL, out DH);
        }
        public override bool MixReadLoadPartCur_ADCValue(out byte DL, out byte DH)
        {
            return ReadLoadPartCur_ADCValue(out DL, out DH);
        }
        public override bool MixProVoltEnable(byte Status)
        {
            return ProVoltEnable(Status);
        }
        public override bool MixEnable(byte Status)
        {
            return Enable(Status);
        }
        public override bool MixOCVEnable(byte Status)
        {
            return OCVEnable(Status);
        }
        public override bool MixCellEnable(byte Status)
        {
            return CellEnable(Status);
        }
        public override bool MixLoadPartEnable(byte Status)
        {
            return LoadPartEnable(Status);
        }
        public override bool MixCHGEnable()
        {
            return CHGEnable();
        }
        public override bool MixDCIREnable()
        {
            return DCIREnable();
        }
        public override bool MixStCurEnable()
        {
            return StCurEnable();
        }
        public override bool MixCNTEnable()
        {
            return CNTEnable();
        }
        public override bool MixNTCEnable()
        {
            return NTCEnable();
        }
        public override bool MixUpdate()
        {
            return Update();
        }
        public override bool MixUpdateOCV()
        {
            return UpdateOCV();
        }
        public override bool MixUpdateProVolt()
        {
            return UpdateProVolt();
        }
        public override bool MixUpdateCellVolt()
        {
            return UpdateCellVolt();
        }
        public override bool MixUpdateDCIR()
        {
            return UpdateDCIR();
        }
        public override bool MixUpdateNTC()
        {
            return UpdateNTC();
        }
        public override bool MixUpdateLoadPart()
        {
            return UpdateLoadPart();
        }
        public override bool MixReadChgVolDAC(byte addr, out byte DL, out byte DH)
        {
            return ReadChgVolDAC(addr, out DL, out DH);
        }
        public override bool MixReadChgVolADC(byte addr, out byte DL, out byte DM, out byte DH)
        {
            DM = 0x00;
            return ReadChgVolADC(addr, out DL, out DH);
        }
        public override bool MixReadOcvDAC(byte addr, out byte DL, out byte DH)
        {
            return ReadOcvDAC(addr, out DL, out DH);
        }
        public override bool MixReadOcvADC(byte addr, out byte DL, out byte DH)
        {
            return ReadOcvADC(addr, out DL, out DH);
        }
        public override bool MixReadPrgDAC(byte addr, out byte DL, out byte DH)
        {
            return ReadPrgDAC(addr, out DL, out DH);
        }
        public override bool MixReadPrgADC(byte addr, out byte DL, out byte DH)
        {
            return ReadPrgADC(addr, out DL, out DH);
        }
        public override bool MixReadCellDAC(byte addr, out byte DL, out byte DH)
        {
            return ReadCellDAC(addr, out DL, out DH);
        }
        public override bool MixReadCellADC(byte addr, out byte DL, out byte DM, out byte DH)
        {
            DM = 0x00;
            return ReadCellADC(addr, out DL, out DH);
        }
        public override bool MixReadOCPCurValue(out double dblCurrent)
        {
            dblCurrent=0;
            return true;
        }
        public override bool MixWriteCellVolt_ADC(byte Addr, byte DL, byte DM, byte DH)
        {
            return WriteCellVolt_DAC(Addr, DL, DH);
        }
        public override bool MixProVoltSetRang(byte status)
        {
            return true;
        }
        public override bool MixWriteLoadPartVol_ADC(byte Addr, byte DL, byte DM, byte DH)
        {
            return WriteLoadPart_DAC(Addr, DL, DH);
        }
        public override bool MixIDRRange(byte range)
        {
            return true;
        }
        public override bool MixReadIDRValue(out double dblIdrValue)
        {
            dblIdrValue = 0;
            return true ;
        }
        public override bool MixStCurCalibration_nA()
        {
            return true;
        }
        public override bool MixStCurRange_nA(byte R, byte range)
        {
            return true;
        }
        public override bool MixReadStCurValue_nA(byte Range, out double dblCellCur)
        {
            dblCellCur = 0;
            return true;
        }
        public override bool MixSendZeroChgVolt(byte DL, byte DH)
        {
            return true;
        }
        public override bool MixSendZeroDsgCur(byte DL, byte DH)
        {
            return true;
        }
        public override bool MixSendZeroCellVolt(byte DL, byte DH)
        {
            return true;
        }
        public override bool MixSendZeroCellCur(byte DL, byte DH)
        {
            return true;
        }
        public override bool MixSendZeroLoadPartVolt(byte DL, byte DH)
        {
            return true;
        }
        public override bool MixSendZeroLoadPartCur(byte DL, byte DH)
        {
            return true;
        }
        public override bool MixSetnARes(byte Status)
        {
            return true;
        }
        public override bool MixLoadPartDisEn()
        {
            return true;
        }
        public override bool MixReadNtcADCValue(byte addr, out byte DL, out byte DM, out byte DH)
        {
            DL = 0x00;DM = 0x00;DH = 0x00;
            return true;
        }
        public override bool MixWriteSNCode(string EqmType, string dateTime, string SNnum,string status)
        {
            return WriteSNCode(EqmType, dateTime, SNnum,status);
        }
        public override bool MixReadSNCode(out string EqmType, out string dateTime, out string SNnum)
        {
            return ReadSNCode(out EqmType, out dateTime,out SNnum);
        }
        //public override bool MixEnableOneWireComm(bool Enable, string CommVolt, string PullRes, out string strErrorCode)
        //{
        //    strErrorCode = "";
        //    string EN = "00";
        //    string En = "01";
        //    if (Enable)
        //    {
        //        EN = "01";
        //        En = "00";
        //    }
        //    string MixCmd = "";


        //    //if (!RY7EN_Enable(!Enable))
        //    //    return false;
        //    //if (!RY13EN_Enable(Enable))//DGND--PS-
        //    //    return false;
        //    MixCmd += "2E-" + En + ",";
        //    MixCmd += "2B-" + EN + ",";


        //    if (CommVolt == "3.3V")
        //    {
        //        //if (!RY12EN_Enable(true))//3.3v-->SW
        //        //    return false;
        //        MixCmd += "2A-" + "01" + ",";

        //    }
        //    else
        //    {
        //        // if (!RY12EN_Enable(false))//1.8v-->SW
        //        //    return false;
        //        MixCmd += "2A-" + "00" + ",";

        //    }

        //    if (Enable)
        //    {
        //        if (PullRes == "2K")
        //        {
        //            //if (!UpRes510R_Enable(false))//510R上拉
        //            //    return false;
        //            //if (!UpRes2K_Enable(true))//2K上拉
        //            //    return false;
        //            MixCmd += "1F-" + "00" + ",";
        //            MixCmd += "1E-" + "01";

        //        }
        //        else
        //        {
        //            //if (!UpRes2K_Enable(false))//2K上拉
        //            //    return false;
        //            //if (!UpRes510R_Enable(true))//510R上拉
        //            //    return false;

        //            MixCmd += "1E-" + "00" + ",";
        //            MixCmd += "1F-" + "01";
        //        }
        //    }
        //    else
        //    {
        //        //if (!UpRes2K_Enable(false))//2K上拉
        //        //    return false;
        //        //if (!UpRes510R_Enable(false))//510R上拉
        //        //    return false;
        //        MixCmd += "1F-" + "00" + ",";
        //        MixCmd += "1E-" + "00";
        //    }



        //    //return true;

        //    return RelayBoardCmd(MixCmd, out strErrorCode);



        //}

        //public override bool MixReadUID_Infineon_IC(out byte[] UIDData, out string Errcode)
        //{

        //    return ReadUID_Infineon_IC(out UIDData, out Errcode);
        //}
        //public override string MixECC_Infineon_IC()
        //{
        //    return ECC_Infineon_IC();
        //}

        //public override bool MixReadLock_Infineon_IC(out string NVMLock)
        //{
        //    return ReadLock_Infineon_IC(out NVMLock);
        //}
        //public override bool MixInfineon_WritePage(byte startAddre, int writeNum, byte[] DataToWrite)
        //{
        //    return Infineon_WritePage(startAddre, writeNum, DataToWrite);
        //}
        //public override bool MixInfineon_ReadPage(byte addre, int Num, out byte[] Data, out string Errcode)
        //{
        //    return Infineon_ReadPage(addre, Num, out Data, out Errcode);
        //}

        //public override bool MixReadFM1230_FileStatus(byte fileAddress, out string NVMLock)//FUDAN(FUDAN-FM1230)     读取锁状态指令
        //{
        //    return ReadFM1230_FileStatus(fileAddress, out NVMLock);
        //}
        //public override bool MixOnewireReset(string Addr)
        //{
        //    return OnewireReset(Addr);
        //}

    }
}
