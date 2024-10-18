using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TestSystem_Pack
{
    public partial class CheckItem
    {
        public bool WriteEEPROM(string Box, string DeviceType, string WriteType, int[] ReadCaliValue, byte[] DAC_DL, byte[] DAC_DH, byte[] ADC_DL, byte[] ADC_DH, byte[] ADC_DL2, byte[] ADC_DH2, byte[] ADC_DL3, byte[] ADC_DH3, out string Point)
        {
            Point = "";
            bool IsNG = false;
            string Result = "写入成功";
            try
            {
                switch (WriteType)
                {
                    case "充电电压":
                        int[] ChgCaliValue = new int[12] { 0, 20, 100, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000 };
                        if (ChgCaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteCHGVol_ADCandDAC(DAC_DL, DAC_DH, ADC_DL, ADC_DH))
                            {
                                Point = "充电电压粗调写入失败!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "充电电压粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "3A放电电流":
                        int[] Dsg3ACaliValue = { 0, 10, 100, 600, 1000, 2000, 2500, 3000 };//电流校正点
                        if (Dsg3ACaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteDSGCur1_ADCandDAC(DAC_DL, DAC_DH, ADC_DL, ADC_DH))
                            {
                                Point = "3A放电电流粗调写入失败!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "放电电流3A粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "放电电流30A":
                        int[] Dsg30ACaliValue = { 3000, 5000, 10000, 15000, 20000, 25000, 30000 };//电流校正点
                        if (Dsg30ACaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteDSGCur2_ADCandDAC(DAC_DL, DAC_DH, ADC_DL, ADC_DH))
                            {
                                Point = "放电电流30A粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "放电电流30A粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "开路电压":
                        int[] Cali5VOCVValue = { 0, 100, 500, 1000, 2000, 3000, 4000, 5000 };//电压校正点
                        if (Cali5VOCVValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteOCV_ADC(ADC_DL, ADC_DH))
                            {
                                Point = "OCV电压粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }

                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "OCV电压粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "编程电压":
                        int[] PrgVolCaliValue = { 200, 500, 1000, 3000, 5000, 7000, 9000, 10000 };//电流校正点
                        if (PrgVolCaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWritePVVol_ADCandDAC(DAC_DL, DAC_DH, ADC_DL, ADC_DH))
                            {
                                Point = "放电电流30A粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "编程电压粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "Cell电压":
                        int[] CellVolCaliValue = { 0, 20, 100, 500, 1000, 2000, 3000, 4000, 5000 };//电流校正点
                        if (CellVolCaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteCellVol_ADCandDAC(DAC_DL, DAC_DH, ADC_DL, ADC_DH))
                            {
                                Point = "Cell电压粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "Cell电压粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "Cell电流":
                        int[] CellCurCaliValue = { 0, 100, 500, 1000, 1250 };//电流校正点
                        if (CellCurCaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteCellCur_ADCandDAC(DAC_DL, DAC_DH, ADC_DL, ADC_DH))
                            {
                                Point = "Cell电流粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "Cell电流粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "LV电压":
                        int[] LVVolCaliValue = { 0, 100, 500, 1000, 2000, 3000, 4000, 5000 };//电压校正点
                        if (LVVolCaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteLVVol_ADCandDAC(DAC_DL, DAC_DH, ADC_DL, ADC_DH))
                            {
                                Point = "LV电压粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "LV电压粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "LV电流":
                        int[] LVCurCaliValue = { 0, 100, 500, 1000, 1250 };//电流校正点
                        if (LVCurCaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteLVCur_ADCandDAC(DAC_DL, DAC_DH, ADC_DL, ADC_DH))
                            {
                                Point = "LV电流粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "LV电流粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "NTC1":
                        int[] NTC1CaliValue = { 0, 100, 500, 1000, 1500, 2000 };
                        if (NTC1CaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteIDR_ADC(WriteType, ADC_DL, ADC_DH, ADC_DL2, ADC_DH2, ADC_DL3, ADC_DH3))
                            {
                                Point = "NTC1粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "NTC1粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "NTC2":
                        int[] NTC2CaliValue = { 2000, 5000, 10000, 15000, 20000 };
                        if (NTC2CaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteIDR_ADC(WriteType, ADC_DL, ADC_DH, ADC_DL2, ADC_DH2, ADC_DL3, ADC_DH3))
                            {
                                Point = "NTC2粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "NTC2粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "NTC3":
                        int[] NTC3CaliValue = { 20000, 50000, 100000, 150000, 200000 };
                        if (NTC3CaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteIDR_ADC(WriteType, ADC_DL, ADC_DH, ADC_DL2, ADC_DH2, ADC_DL3, ADC_DH3))
                            {
                                Point = "NTC3粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "NTC3粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "NTC4":
                        int[] NTC4CaliValue = { 200000, 500000, 1000000, 2000000, 3000000 };
                        if (NTC4CaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteIDR_ADC(WriteType, ADC_DL, ADC_DH, ADC_DL2, ADC_DH2, ADC_DL3, ADC_DH3))
                            {
                                Point = "NTC4粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "NTC4粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "静态电流1":
                        int[] STC1CaliValue = { 0, 3, 6, 10, 10, 20, 40, 60, 60, 80, 100, 120, 120, 150, 180, 200 };
                        if (STC1CaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteSTC_ADC(WriteType, ADC_DL, ADC_DH))
                            {
                                Point = "STC1粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "STC1粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "静态电流2":
                        int[] STC2CaliValue = { 0, 30, 60, 100, 100, 200, 400, 600, 600, 800, 1000, 1200, 1200, 1500, 1800, 2000 };
                        if (STC2CaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteSTC_ADC(WriteType, ADC_DL, ADC_DH))
                            {
                                Point = "STC2粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "STC2粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "静态电流3":
                        int[] STC3CaliValue = { 0, 30, 60, 100, 100, 200, 400, 600, 600, 800, 1000, 1200, 1200, 1500, 1800, 2000 };
                        if (STC3CaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteSTC_ADC(WriteType, ADC_DL, ADC_DH))
                            {
                                Point = "STC3粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "STC3粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "静态电流4":
                        int[] STC4CaliValue = { 0, 300, 600, 1000, 1000, 2000, 4000, 6000, 6000, 8000, 10000, 12000, 12000, 15000, 18000, 20000 };
                        if (STC4CaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteSTC_ADC(WriteType, ADC_DL, ADC_DH))
                            {
                                Point = "STC4粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "STC4粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "直流内阻":
                        int[] DCIRCaliValue = { 0, 10, 30, 50 };
                        if (DCIRCaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixWriteDCIR_ADC(ADC_DL, ADC_DH))
                            {
                                Point = "DCIR粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "DCIR粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "CNT静态(正)":
                        int[] CNTPCaliValue = { 0, 1, 3, 5 };
                        if (CNTPCaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixRoughWriteCNT_ADC(WriteType, ADC_DL, ADC_DH))
                            {
                                Point = "CNT静态(正)粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "CNT静态(正)粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                    case "CNT静态(负)":
                        int[] CNTNCaliValue = { 0, -1, -3, -5 };
                        if (CNTNCaliValue.SequenceEqual(ReadCaliValue))//比较读取的校准点
                        {
                            if (!com.MixRoughWriteCNT_ADC(WriteType, ADC_DL, ADC_DH))
                            {
                                Point = "CNT静态(负)粗调写入!";
                                Result = "写入失败";
                                IsNG = true;
                            }
                        }
                        else
                        {
                            Point = "校准文件校准点与实际校准点不符!";
                            Result = "写入失败";
                            IsNG = true;
                        }
                        UpdateUidelegate(DeviceType, Box, "CNT静态(负)粗调写入", "校准点一次写入", "", "", "", "", "", Result);
                        break;
                }
                if (IsNG)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
