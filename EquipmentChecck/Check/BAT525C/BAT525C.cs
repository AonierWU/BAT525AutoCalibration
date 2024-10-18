using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem_Pack
{
    public partial class BAT525C : MyEqmCmd
    {
        public override bool MixEQMInit(out string Errcode)
        {
            return ReadEqmType(out Errcode);
        }
        public override bool MixChgVolCalibration()
        {
            return ChgVolCalibration();
        }
        public override bool MixSendZeroChgVolt(byte DL, byte DH)
        {
            return SendZeroChgVolt(DL, DH);
        }
        public override bool MixSendChgVolt(byte DL, byte DH)
        {
            return SendChgVolt(DL, DH);
        }
        public override bool MixSetVoltValue(double voltage)
        {
            return SetVoltValue(voltage);
        }
        public override bool MixReadVoltValue(out double dblVoltage)
        {
            return ReadVoltValue(out dblVoltage);
        }
        public override bool MixWriteVoltDAC_ADCCaliValue(byte Addr, byte DL, byte DH)
        {
            return WriteVoltDACCaliValue(Addr, DL, DH);
        }
        public override bool MixWriteVoltADCCaliValue(byte Addr, byte DL, byte DM, byte DH)
        {
            return WriteVoltADCCaliValue(Addr, DL, DM, DH);
        }
        public override bool MixReadVoltADCCaliValue(out byte DL, out byte DM, out byte DH)
        {
            return ReadVoltADCCaliValue(out DL, out DM, out DH);
        }
        public override bool MixUpdate()
        {
            return Update();
        }
        public override bool MixReadChgVolDAC(byte addr, out byte DL, out byte DH)
        {
            return ReadChgVolDAC(addr, out DL, out DH);
        }
        public override bool MixReadChgVolADC(byte addr, out byte DL, out byte DM, out byte DH)
        {
            return ReadChgVolADC(addr, out DL, out DM, out DH);
        }
        public override bool MixCHGEnable()
        {
            return CHGEnable();
        }

        public override bool MixEnable(byte Status)
        {
            return Enable(Status);
        }
        public override bool MixDsgCurCalibration()
        {
            return DsgCurCalibration();
        }
        public override bool MixSetCurRange(byte range)
        {
            return SetCurRange(range);
        }
        public override bool MixReadCurADCCaliValue(out byte DL, out byte DM, out byte DH, out byte DL2, out byte DH2)
        {
            return ReadCurADCCaliValue(out DL, out DM, out DH, out DL2, out DH2);
        }
        public override bool MixSendDsgCur(byte DL, byte DH)
        {
            return SendDsgCur(DL, DH);
        }
        public override bool MixSendZeroDsgCur(byte DL, byte DH)
        {
            return SendZeroDsgCur(DL, DH);
        }
        public override bool MixSetCurValue(byte Rang, double Current)
        {
            return SetCurValue(Rang, Current);
        }
        public override bool MixReadCurValue(out double dblCurrent)
        {
            return ReadCurValue(out dblCurrent);
        }
        public override bool MixReadOCPCurValue(out double dblCurrent)
        {
            return ReadOCPCurValue(out dblCurrent);
        }

        public override bool MixCellVoltCalibration()
        {
            return CellVoltCalibration();
        }
        public override bool MixSendZeroCellVolt(byte DL, byte DH)
        {
            return SendZeroCellVolt(DL, DH);
        }
        public override bool MixSendCellVolt(byte DL, byte DH)
        {
            return SendCellVolt(DL, DH);
        }
        public override bool MixSetCellVoltValue(double voltage)
        {
            return SetCellVoltValue(voltage);
        }
        public override bool MixReadCellVoltValue(out double dblCellVolt)
        {
            return ReadCellVoltValue(out dblCellVolt);
        }
        public override bool MixWriteCellVolt_DAC(byte Addr, byte DL, byte DH)
        {
            return WriteCellVolt_DAC(Addr, DL, DH);
        }
        public override bool MixWriteCellVolt_ADC(byte Addr, byte DL, byte DM, byte DH)
        {
            return WriteCellVolt_ADC(Addr, DL, DM, DH);
        }
        public override bool MixReadCellVolt_ADCValue(out byte DL, out byte DM, out byte DH)
        {
            return ReadCellVolt_ADCValue(out DL, out DM, out DH);
        }
        public override bool MixReadCellDAC(byte addr, out byte DL, out byte DH)
        {
            return ReadCellDAC(addr, out DL, out DH);
        }
        public override bool MixReadCellADC(byte addr, out byte DL, out byte DM, out byte DH)
        {
            return ReadCellADC(addr, out DL, out DM, out DH);
        }
        public override bool MixUpdateCellVolt()
        {
            return UpdateCellVolt();
        }

        public override bool MixCellCurCalibration()
        {
            return CellCurCalibration();
        }
        public override bool MixSendZeroCellCur(byte DL, byte DH)
        {
            return SendZeroCellCur(DL, DH);
        }
        public override bool MixSendCellCur(byte DL, byte DH)
        {
            return SendCellCur(DL, DH);
        }
        public override bool MixCellEnable(byte Status)
        {
            return CellEnable(Status);
        }
        public override bool MixReadCellCur_ADCValue(out byte DL, out byte DM, out byte DH)
        {
            return ReadCellCur_ADCValue(out DL, out DM, out DH);
        }
        public override bool MixSetCellCurValue(int current)
        {
            return SetCellCurValue(current);
        }
        public override bool MixReadCellCurValue(out double dblCellCur)
        {
            return ReadCellCurValue(out dblCellCur);
        }


        public override bool MixOCVCalibration()
        {
            return OCVCalibration();
        }
        public override bool MixWriteOCV_DAC(out byte DL, out byte DM, out byte DH, out byte DL2, out byte DM2, out byte DH2)
        {
            return WriteOCV_DAC(out DL, out DM, out DH, out DL2, out DM2, out DH2);
        }
        public override bool MixWriteOCV_DACCaliValue(byte Addr, byte DL, byte DM, byte DH)
        {
            return WriteOCV_DACCaliValue(Addr, DL, DM, DH);
        }
        public override bool MixUpdateOCV()
        {
            return UpdateOCV();
        }
        public override bool MixReadOCVValue(out double dblOCV)
        {
            return ReadOCVValue(out dblOCV);
        }
        public override bool MixReadPortVolValue(out double dblPortVol)
        {
            return ReadPortVolValue(out dblPortVol);
        }
        public override bool MixWriteOCV_ADC(byte[] ADC_L, byte[] ADC_H)
        {
            return WriteOCV_ADC(ADC_L, ADC_H);
        }
        public override bool MixOCVEnable(byte Status)
        {
            return OCVEnable(Status);
        }

        public override bool MixProVoltCalibration()
        {
            return ProVoltCalibration();
        }
        public override bool MixProVoltSetRang(byte status)
        {
            return ProVoltSetRang(status);
        }
        public override bool MixSendProVolt(byte DL, byte DH)
        {
            return SendProVolt(DL, DH);
        }
        public override bool MixWriteProVolt_DAC(byte Addr, byte DL, byte DH)
        {
            return WriteProVolt_DAC(Addr, DL, DH);
        }
        public override bool MixReadProADCCaliValue(out byte DL, out byte DM, out byte DH)
        {
            return ReadProADCCaliValue(out DL, out DM, out DH);
        }
        public override bool MixUpdateProVolt()
        {
            return UpdateProVolt();
        }
        public override bool MixSetProVoltValue(int voltage)
        {
            return SetProVoltValue(voltage);
        }
        public override bool MixReadProVoltValue(out double dblVoltage)
        {
            return ReadProVoltValue(out dblVoltage);
        }
        public override bool MixProVoltEnable(byte Status)
        {
            return ProVoltEnable(Status);
        }
        public override bool MixWritePro_ADCValue(byte Addr, byte DL, byte DH)
        {
            return WriteProVolt_DAC(Addr, DL, DH);
        }

        public override bool MixLoadVoltCalibration()
        {
            return LoadVoltCalibration();
        }
        public override bool MixSendZeroLoadPartVolt(byte DL, byte DH)
        {
            return SendZeroLoadPartVolt(DL, DH);
        }
        public override bool MixSendLoadPartVolt(byte DL, byte DH)
        {
            return SendLoadPartVolt(DL, DH);
        }
        public override bool MixWriteLoadPart_DAC(byte Addr, byte DL, byte DH)
        {
            return WriteLoadPart_DAC(Addr, DL, DH);
        }
        public override bool MixReadLoadPartVolt_ADCValue(out byte DL, out byte DM, out byte DH)
        {
            return ReadLoadPartVolt_ADCValue(out DL, out DM, out DH);
        }
        public override bool MixWriteLoadPartVol_ADC(byte Addr, byte DL, byte DM, byte DH)
        {
            return WriteLoadPartVol_ADC(Addr, DL, DM, DH);
        }
        public override bool MixUpdateLoadPart()
        {
            return UpdateLoadPart();
        }
        public override bool MixSetLoadPartVoltValue(int Voltage)
        {
            return SetLoadPartVoltValue(Voltage);
        }
        public override bool MixReadLoadPatVoltValue(out double dblCellVolt)
        {
            return ReadLoadPatVoltValue(out dblCellVolt);
        }

        public override bool MixLoadCurCalibration()
        {
            return LoadCurCalibration();
        }
        public override bool MixSendZeroLoadPartCur(byte DL, byte DH)
        {
            return SendZeroLoadPartCur(DL, DH);
        }
        public override bool MixSendLoadPartCur(byte DL, byte DH)
        {
            return SendLoadPartCur(DL, DH);
        }
        public override bool MixLoadPartEnable(byte Status)
        {
            return LoadPartEnable(Status);
        }
        public override bool MixLoadPartDisEn()
        {
            return LoadPartDisEN();
        }
        public override bool MixReadLoadPartCur_ADCValue(out byte DL, out byte DH)
        {
            return ReadLoadPartCur_ADCValue(out DL, out DH);
        }
        public override bool MixSetLoadPartCurValue(int current)
        {
            return SetLoadPartCurValue(current);
        }
        public override bool MixReadLoadPartCurValue(out double dblCellCur)
        {
            return ReadLoadPartCurValue(out dblCellCur);
        }


        public override bool MixDCIRCalibration()
        {
            return DCIRCalibration();
        }
        public override bool MixReadDCIR_ADCValue(byte Rang, out byte DL, out byte DH)
        {
            return ReadDCIR_ADCValue(Rang, out DL, out DH);
        }
        public override bool MixWriteDCIR_DAC(byte Addr, byte DL, byte DH)
        {
            return WriteDCIR_DAC(Addr, DL, DH);
        }
        public override bool MixUpdateDCIR()
        {
            return UpdateDCIR();
        }
        public override bool MixReadDCIRValue(byte Rang, out double dblCellCur)
        {
            return ReadDCIRValue(Rang, out dblCellCur);
        }
        public override bool MixDCIREnable()
        {
            return DCIREnable();
        }



        public override bool MixNTCCalibration()
        {
            return NTCCalibration();
        }
        public override bool MixNTCRange(byte range)
        {
            return NTCRange(range);
        }
        public override bool MixReadNTC_ADCValue(byte Index, byte R, out byte DL1, out byte DH1, out byte DL2, out byte DH2, out byte DL3, out byte DH3)
        {
            DL2 = 0x00; DH2 = 0x00; DL3 = 0x00; DH3 = 0x00;
            return ReadNTC_ADCValue(R, out DL1, out DH1, out DH2);
        }
        public override bool MixWriteNTC_DAC(byte Addr1, byte DL1, byte DH1, byte Addr2, byte DL2, byte DH2, byte Addr3, byte DL3, byte DH3)
        {
            return WriteNTC_DAC(Addr1, DL1, DH1, DH2);
        }
        public override bool MixUpdateNTC()
        {
            return UpdateNTC();
        }
        public override bool MixReadNTCValue(byte R, byte Range, out double dblCellCur)
        {
            return ReadNTCValue(out dblCellCur);
        }
        public override bool MixNTCEnable()
        {
            return NTCEnable();
        }
        public override bool MixIDRCalibration()
        {
            return IDRCalibration();
        }
        public override bool MixIDRRange(byte range)
        {
            return IDRRange(range);
        }
        public override bool MixReadIDRValue(out double dblIdrValue)
        {
            return ReadIDRValue(out dblIdrValue);
        }
        public override bool MixReadNtcADCValue(byte addr, out byte DL, out byte DM, out byte DH)
        {
            return ReadNtcADCValue(addr, out DL, out DM, out DH);
        }



        public override bool MixStCurCalibration()
        {
            return StCurCalibration();
        }
        public override bool MixStCurRange(byte R, byte range)
        {
            return StCurRange(range);
        }
        public override bool MixReadStCur_ADCValue(byte Rang, out byte DL, out byte DH)
        {
            return ReadStCur_ADCValue(Rang, out DL, out DH);
        }
        public override bool MixReadStCurValue(byte R, byte Range, out double dblCellCur)
        {
            return ReadStCurValue(Range, out dblCellCur);
        }
        public override bool MixStCurCalibration_nA()
        {
            return StCurCalibration_nA();
        }
        public override bool MixStCurRange_nA(byte R, byte range)
        {
            return StCurRange_nA(R, range);
        }
        public override bool MixReadStCurValue_nA(byte Range, out double dblCellCur)
        {
            return ReadStCurValue_nA(Range, out dblCellCur);
        }
        public override bool MixStCurEnable()
        {
            return StCurEnable();
        }
        public override bool MixSetnARes(byte Status)
        {
            return SetnARes(Status);
        }
        public override bool MixWriteSNCode(string EqmType, string dateTime, string SNnum,string Status)
        {
            return true ;
        }
        public override bool MixReadSNCode(out string EqmType, out string dateTime, out string SNnum)
        {
            EqmType = "";
            dateTime = "";
            SNnum = "";
            return true;
        }









        public override bool MixCNTCalibration()
        {
            return true;
        }






















        public override bool MixReadOCV_DACCaliValue(byte Addr)
        {
            throw new NotImplementedException();
        }








        public override bool MixReadCNTValue(byte Statu, out double dblVoltage)
        {
            throw new NotImplementedException();
        }
















        public override bool MixWriteDCIR_ADC(byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }
        public override bool MixWriteIDR_ADC(string Write, byte[] ADC_L, byte[] ADC_H, byte[] Buf_L, byte[] Buf_M, byte[] Buf_H, byte[] Buf_HH)
        {
            throw new NotImplementedException();
        }
        public override bool MixReadCNT_ADC(out byte DL, out byte DH)
        {
            throw new NotImplementedException();
        }
        public override bool MixWriteCNT_ADC(byte Add, byte DL, byte DH)
        {
            throw new NotImplementedException();
        }
        public override bool MixRoughWriteCNT_ADC(string Write, byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }
        public override bool MixWritePVVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }

        public override bool MixWriteLVVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }
        public override bool MixWriteCellVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }
        public override bool MixWriteCellCur_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }
        public override bool MixWriteLVCur_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }
        public override bool MixWriteCHGVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }
        public override bool MixWriteDSGCur1_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }
        public override bool MixWriteDSGCur2_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H)//大电流
        {
            throw new NotImplementedException();
        }
        public override bool MixWriteSTC_ADC(string Write, byte[] ADC_L, byte[] ADC_H)
        {
            throw new NotImplementedException();
        }















        public override bool MixCNTEnable()
        {
            throw new NotImplementedException();
        }










        public override bool MixReadOcvDAC(byte addr, out byte DL, out byte DH)
        {
            throw new NotImplementedException();
        }
        public override bool MixReadOcvADC(byte addr, out byte DL, out byte DH)
        {
            throw new NotImplementedException();
        }
        public override bool MixReadPrgDAC(byte addr, out byte DL, out byte DH)
        {
            throw new NotImplementedException();
        }
        public override bool MixReadPrgADC(byte addr, out byte DL, out byte DH)
        {
            throw new NotImplementedException();
        }



    }
}
