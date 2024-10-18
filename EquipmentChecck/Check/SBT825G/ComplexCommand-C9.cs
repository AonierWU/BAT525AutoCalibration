namespace TestSystem_Pack
{
    public partial class SBT825G
    {
        //private bool ChgDsgTest(SelectChgDsgMode chgDsgMode, int SetVolt, int SetCur, int Delay, bool isDisable, ReadFlag flag, out double dblVolt, out double dblCur, out string Errcode)
        //{
        //    Errcode = "";
        //    dblVolt = 0.0f;
        //    dblCur = 0.0f;
        //    try
        //    {

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;
        //        if (flag == ReadFlag.VOLT)
        //            YY |= 0x02;
        //        else if (flag == ReadFlag.CURR)
        //            YY |= 0x04;
        //        else if (flag == ReadFlag.BOTH)
        //            YY |= 0x06;
        //        else
        //        {
        //            Errcode = "YY位设定异常";
        //            return false;
        //        }

        //        byte TDL, TDH, VDL, VDM, VDH, CDL, CDM, CDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //        SetVolt *= 10;
        //        VDL = Convert.ToByte(SetVolt & 0x0000FF);
        //        VDM = Convert.ToByte((SetVolt & 0x00FF00) >> 8);
        //        VDH = Convert.ToByte((SetVolt & 0xFF0000) >> 16);

        //        SetCur *= 10;
        //        CDL = Convert.ToByte(SetCur & 0x0000FF);
        //        CDM = Convert.ToByte((SetCur & 0x00FF00) >> 8);
        //        CDH = Convert.ToByte((SetCur & 0xFF0000) >> 16);

        //        byte CorD = 0x00;
        //        if (chgDsgMode == SelectChgDsgMode.CHG)
        //            CorD = 0X00;
        //        else if (chgDsgMode == SelectChgDsgMode.DSG)
        //            CorD = 0x01;
        //        else
        //        {
        //            Errcode = "充放电模式未选择";
        //            return false;
        //        }

        //        string Cmd = "";
        //        Cmd = "C9-CC-0F-0B-" + CorD.ToString("X2") + "-" + VDL.ToString("X2") + "-" + VDM.ToString("X2") + "-" + VDH.ToString("X2") + "-" + CDL.ToString("X2") + "-" + CDM.ToString("X2") + "-" + CDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);

        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 12)
        //            {
        //                Errcode = "接收数据长度不为12";
        //                return false;
        //            }
        //            else if (ReturnBuffer[10] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[10].ToString("X2");
        //                return false;
        //            }
        //            else
        //            {
        //                dblVolt = Math.Round((ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01, 2);//mV
        //                dblCur = Math.Round((ReturnBuffer[9] * 256 * 256 + ReturnBuffer[8] * 256 + ReturnBuffer[7]) * 0.01, 2);//mA
        //                return true;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        private bool EQMInit(out string strErrorCode)
        {
            strErrorCode = "";
            try
            {
                string Cmd = "";
                Cmd = "C9-CC-05-00";
                Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
                byte[] SendByte = stringToByte(Cmd);
                byte[] ReturnBuffer;
                int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
                if (intResult < 0)
                {
                    strErrorCode = intResult.ToString();
                    return false;
                }
                else
                {
                    if (ReturnBuffer.Length != 6)
                    {
                        strErrorCode = "接收数据长度不为6";
                        return false;
                    }
                    else if (ReturnBuffer[4] != 0X00)
                    {
                        strErrorCode = "错误代码:" + ReturnBuffer[4];
                        return false;
                    }
                    else
                    {
                        
                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                strErrorCode = ex.Message;
                return false;
            }
        }
        //private bool ReadPinVolt(SelectVoltPin voltPin, int Delay, bool isDisable, ReadFlag flag, out double dblVolt, out string strErrorCode)
        //{
        //    dblVolt = -1.0f;
        //    strErrorCode = "";
        //    try
        //    {
        //        string SE = "00";
        //        if (voltPin == SelectVoltPin.OCV)
        //            SE = "00";
        //        else if (voltPin == SelectVoltPin.SCL)
        //            SE = "01";
        //        else if (voltPin == SelectVoltPin.SDA)
        //            SE = "02";

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        if (flag == ReadFlag.VOLT)
        //            YY |= 0x02;
        //        else if (flag == ReadFlag.CURR)
        //            YY |= 0x04;
        //        else if (flag == ReadFlag.BOTH)
        //            YY |= 0x06;
        //        else
        //        {
        //            strErrorCode = "YY位设定异常";
        //            return false;
        //        }

        //        byte TDL, TDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //        string Cmd = "";
        //        Cmd = "C9-CC-09-01-" + SE + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);

        //        if (intResult < 0)
        //        {
        //            strErrorCode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                strErrorCode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                strErrorCode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                return false;
        //            }
        //            else
        //            {
        //                dblVolt = Math.Round((ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01, 2);//mV
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strErrorCode = ex.Message;
        //        return false;
        //    }
        //}
        //private bool DisModels(string Cmds, out string strErrorCode)
        //{
        //    strErrorCode = "";
        //    try
        //    {
        //        string Cmd = "";
        //        Cmd = "C9-CC-05-" + Cmds;
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            strErrorCode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 6)
        //            {
        //                strErrorCode = "接收数据长度不为6";
        //                return false;
        //            }
        //            else if (ReturnBuffer[4] != 0X00)
        //            {
        //                strErrorCode = "错误代码:" + ReturnBuffer[4];
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        strErrorCode = ex.Message;
        //        return false;
        //    }
        //}
        //private bool ReadIR(SelectIRType iRPin, SelectIRRange iRRange, int Delay, bool isDisable, out double dblIR, out string strErrorCode)
        //{
        //    dblIR = 0.0f;
        //    strErrorCode = "";
        //    try
        //    {
        //        string SE = "00";
        //        if (iRPin == SelectIRType.IDR)
        //            SE = "00";
        //        else if (iRPin == SelectIRType.NTC)
        //            SE = "01";

        //        string Range = "00";
        //        if (iRRange == SelectIRRange.K0_2)
        //            Range = "00";
        //        else if (iRRange == SelectIRRange.K2_20)
        //            Range = "01";
        //        else if (iRRange == SelectIRRange.K20_200)
        //            Range = "02";
        //        else if (iRRange == SelectIRRange.K200_3000)
        //            Range = "03";
        //        else
        //            Range = "04";

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;
        //        //if (flag == ReadFlag.VOLT)
        //        //    YY |= 0x02;
        //        //else if (flag == ReadFlag.CURR)
        //        //    YY |= 0x04;
        //        //else if (flag == ReadFlag.BOTH)
        //        //    YY |= 0x06;
        //        //else
        //        //{
        //        //    strErrorCode = "YY位设定异常";
        //        //    return false;
        //        //}


        //        byte TDL, TDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);


        //        string Cmd = "";
        //        Cmd = "C9-CC-0A-02-" + SE + "-" + Range + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);

        //        if (intResult < 0)
        //        {
        //            strErrorCode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 11)
        //            {
        //                strErrorCode = "接收数据长度不为11";
        //                return false;
        //            }
        //            else if (ReturnBuffer[9] != 0x00)
        //            {
        //                strErrorCode = "错误代码：" + ReturnBuffer[9].ToString("X2");
        //                return false;
        //            }
        //            else
        //            {
        //                //dblIR = ReturnBuffer[7] * 256 * 256 + ReturnBuffer[6] * 256 + ReturnBuffer[5];//Ω
        //                dblIR = (ReturnBuffer[8] * 256 * 256 * 256 + ReturnBuffer[7] * 256 * 256 + ReturnBuffer[6] * 256 + ReturnBuffer[5]) * 0.01;//Ω
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strErrorCode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool LoadPartChgDsg(SelectChgDsgMode chgDsgMode, int setVolt, int setCur, int Delay, bool isDisable, ReadFlag flag, out double dblVolt, out double dblCur, out string Errcode)
        //{
        //    dblVolt = 0.0f;
        //    dblCur = 0.0f;
        //    Errcode = "";
        //    try
        //    {
        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        if (flag == ReadFlag.VOLT)
        //            YY |= 0x02;
        //        else if (flag == ReadFlag.CURR)
        //            YY |= 0x04;
        //        else if (flag == ReadFlag.BOTH)
        //            YY |= 0x06;
        //        else
        //        {
        //            Errcode = "YY位设定异常";
        //            return false;
        //        }

        //        byte TDL, TDH, VDL, VDH, CDL, CDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //        setVolt *= 10;
        //        VDL = Convert.ToByte(setVolt & 0x00FF);
        //        VDH = Convert.ToByte((setVolt & 0xFF00) >> 8);

        //        setCur *= 10;
        //        CDL = Convert.ToByte(setCur & 0x00FF);
        //        CDH = Convert.ToByte((setCur & 0xFF00) >> 8);

        //        byte CorD = 0x00;
        //        if (chgDsgMode == SelectChgDsgMode.CHG)
        //            CorD = 0X00;
        //        else if (chgDsgMode == SelectChgDsgMode.DSG)
        //            CorD = 0x01;
        //        else
        //        {
        //            Errcode = "充放电模式未选择";
        //            return false;
        //        }

        //        string Cmd = "";
        //        Cmd = "C9-CC-0D-04-" + CorD.ToString("X2") + "-" + VDL.ToString("X2") + "-" + VDH.ToString("X2") + "-" + CDL.ToString("X2") + "-" + CDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);

        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 12)
        //            {
        //                Errcode = "接收数据长度不为12";
        //                return false;
        //            }
        //            else if (ReturnBuffer[10] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[10].ToString("X2");
        //                return false;
        //            }
        //            else
        //            {
        //                dblVolt = Math.Round((ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01, 2);//mv
        //                dblCur = Math.Round((ReturnBuffer[9] * 256 * 256 + ReturnBuffer[8] * 256 + ReturnBuffer[7]) * 0.01, 2);//ma
        //                return true;
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool SetCell(int setVolt, int setCur, int Delay, bool isDisable, ReadFlag flag, bool blReadOutValue, out double dblVolt, out double dblCur, out string Errcode)
        //{
        //    dblVolt = 0.0f;
        //    dblCur = 0.0f;
        //    Errcode = "";

        //    try
        //    {
        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        if (flag == ReadFlag.VOLT)
        //            YY |= 0x02;
        //        else if (flag == ReadFlag.CURR)
        //            YY |= 0x04;
        //        else if (flag == ReadFlag.BOTH)
        //            YY |= 0x06;
        //        else
        //        {
        //            Errcode = "YY位设定异常";
        //            return false;
        //        }




        //        byte TDL, TDH, VDL, VDH, CDL, CDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //        setVolt *= 10;
        //        VDL = Convert.ToByte(setVolt & 0x00FF);
        //        VDH = Convert.ToByte((setVolt & 0xFF00) >> 8);

        //        setCur *= 10;
        //        CDL = Convert.ToByte(setCur & 0x00FF);
        //        CDH = Convert.ToByte((setCur & 0xFF00) >> 8);


        //        string Cmd = "";
        //        Cmd = "C9-CC-0C-06-" + VDL.ToString("X2") + "-" + VDH.ToString("X2") + "-" + CDL.ToString("X2") + "-" + CDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        if (!blReadOutValue)
        //        {
        //            if (mixAllCmds.BlMixCmdsSend)//需要复合
        //            {
        //                mixAllCmds.GetCmdToMixArrray(Cmd);//将指令复合
        //                return true;
        //            }
        //        }
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);

        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 12)
        //            {
        //                Errcode = "接收数据长度不为12";
        //                return false;
        //            }
        //            else if (ReturnBuffer[10] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[10].ToString("X2");
        //                return false;
        //            }
        //            else
        //            {
        //                dblVolt = Math.Round((ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01, 2);//mv
        //                dblCur = Math.Round((ReturnBuffer[9] * 256 * 256 + ReturnBuffer[8] * 256 + ReturnBuffer[7]) * 0.01, 2);//ma
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool ReadStaticCurr(SelectSCurrRange sCurrRange, int Delay, int iCycle, bool blSO, bool isDisable, out double dblSCur, out string Errcode)
        //{
        //    dblSCur = 0.0f;
        //    Errcode = "";
        //    try
        //    {
        //        string Range = "00";
        //        double Gain = 1;//
        //        if (sCurrRange == SelectSCurrRange.uA2)
        //        {
        //            Range = "03";
        //            Gain = 1;//nA
        //        }
        //        else if (sCurrRange == SelectSCurrRange.uA20)
        //        {
        //            Range = "02";
        //            Gain = 1;//nA
        //        }
        //        else if (sCurrRange == SelectSCurrRange.uA200)
        //        {
        //            Range = "01";
        //            Gain = 1.0;//uA
        //        }
        //        else if (sCurrRange == SelectSCurrRange.uA2000)
        //        {
        //            Range = "00";
        //            Gain = 1.0;//uA

        //        }
        //        else
        //        {
        //            Errcode = "量程设定NG";
        //            return false;
        //        }


        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;


        //        byte TDL, TDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //        byte SO = 0X00;
        //        if (blSO)
        //            SO = 0x01;
        //        else
        //            SO = 0x00;


        //        if (iCycle > 255)
        //        {
        //            Errcode = "采样次数设置超上限";
        //            return false;
        //        }

        //        string Cmd = "";
        //        Cmd = "C9-CC-0B-08-" + Range + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + SO.ToString("X2") + "-" + iCycle.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer, Delay + (iCycle * 90) + 500);
        //        // int intResult = comm.WriteRead(SendByte, out ReturnBuffer);

        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 11)
        //            {
        //                Errcode = "接收数据长度不为12";
        //                return false;
        //            }
        //            else if (ReturnBuffer[9] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[9].ToString("X2");
        //                return false;
        //            }
        //            else
        //            {
        //                dblSCur = Math.Round((ReturnBuffer[8] * 256 * 256 + ReturnBuffer[7] * 256 + ReturnBuffer[6]) * 0.01 * Gain, 2);//
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool ReadOneTimeStaticCurr(SelectSCurrRange sCurrRange, bool isDisable, out double dblSCur, out string Errcode)
        //{
        //    Errcode = "";
        //    dblSCur = 0.0f;
        //    try
        //    {


        //        string Range = "00";
        //        if (sCurrRange == SelectSCurrRange.uA2)
        //            Range = "00";
        //        else if (sCurrRange == SelectSCurrRange.uA20)
        //            Range = "01";
        //        else if (sCurrRange == SelectSCurrRange.uA200)
        //            Range = "02";
        //        else if (sCurrRange == SelectSCurrRange.uA2000)
        //            Range = "03";
        //        else if (sCurrRange == SelectSCurrRange.Hold)
        //            Range = "FF";
        //        else
        //        {
        //            Errcode = "量程设定NG";
        //            return false;
        //        }

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;
        //        string Cmd = "";
        //        Cmd = "C9-CC-07-14-" + Range + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 11)
        //            {
        //                Errcode = "接收数据长度不为11";
        //                return false;
        //            }
        //            else if (ReturnBuffer[9] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[9].ToString("X2");
        //                return false;
        //            }
        //            else
        //            {
        //                dblSCur = Math.Round((ReturnBuffer[8] * 256 * 256 + ReturnBuffer[7] * 256 + ReturnBuffer[6]) * 0.01, 2);//
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool CNTTest(bool chk10mA, int Delay, bool isDisable, out double dblCur, out string Errcode)
        //{
        //    Errcode = "";
        //    dblCur = 0.0f;

        //    try
        //    {
        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        byte TDL, TDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //        byte bCheck = 0x00;
        //        if (chk10mA)
        //            bCheck = 0x01;
        //        else
        //            bCheck = 0x00;

        //        string Cmd = "";
        //        Cmd = "C9-CC-09-0D-" + bCheck.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer, Delay + 500);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                Errcode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                return false;
        //            }
        //            else
        //            {
        //                dblCur = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]);//nA
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }

        //}

        //private bool DCIR(SelectChgDsgMode chgDsgMode, int SetCur, int Delay, bool isDisable, out double dblDCIR, out string Errcode)
        //{
        //    dblDCIR = 0.0f;
        //    Errcode = "";

        //    byte CorD = 0x00;
        //    if (chgDsgMode == SelectChgDsgMode.CHG)
        //        CorD = 0X00;
        //    else if (chgDsgMode == SelectChgDsgMode.DSG)
        //        CorD = 0x01;
        //    else
        //    {
        //        Errcode = "充放电模式未选择";
        //        return false;
        //    }

        //    byte CDL, CDM, CDH;

        //    SetCur *= 10;
        //    CDL = Convert.ToByte(SetCur & 0x0000FF);
        //    CDM = Convert.ToByte((SetCur & 0x00FF00) >> 8);
        //    CDH = Convert.ToByte((SetCur & 0xFF0000) >> 16);

        //    byte TDL, TDH;
        //    TDL = Convert.ToByte(Delay & 0x00FF);
        //    TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //    byte YY = 0x00;
        //    if (isDisable)
        //        YY |= 0x01;
        //    else
        //        YY |= 0x00;

        //    string Cmd = "";
        //    Cmd = "C9-CC-0C-0F-" + CorD.ToString("X2") + "-" + CDL.ToString("X2") + "-" + CDM.ToString("X2") + "-" + CDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //    Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //    byte[] SendByte = stringToByte(Cmd);
        //    byte[] ReturnBuffer;
        //    int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //    if (intResult < 0)
        //    {
        //        Errcode = intResult.ToString();
        //        return false;
        //    }
        //    else
        //    {
        //        if (ReturnBuffer.Length != 9)
        //        {
        //            Errcode = "接收数据长度不为9";
        //            return false;
        //        }
        //        else if (ReturnBuffer[7] != 0x00)
        //        {
        //            Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //            return false;
        //        }
        //        else
        //        {
        //            dblDCIR = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01;//mΩ
        //            return true;
        //        }
        //    }
        //}

        //private bool SetProgVolt(int SetVolt, int Delay, bool isDisable, out double dblVolt, out string Errcode)
        //{
        //    Errcode = "";
        //    dblVolt = 0.0f;
        //    try
        //    {
        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY = 0x07;
        //        else
        //            YY = 0x06;

        //        byte TDL, TDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //        // SetVolt *= 10;
        //        byte VDL, VDH;
        //        VDL = Convert.ToByte(SetVolt & 0x00FF);
        //        VDH = Convert.ToByte((SetVolt & 0xFF00) >> 8);

        //        string Cmd = "";
        //        Cmd = "C9-CC-0A-10-" + VDL.ToString("X2") + "-" + VDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                Errcode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                return false;
        //            }
        //            else
        //            {
        //                dblVolt = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]);//mV
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool SecOVP(bool blSaveT, int SetVolt, double SetProTime, bool isDisable, out double dblProDelay, out string Errcode)
        //{
        //    dblProDelay = 0.0f;
        //    Errcode = "";
        //    try
        //    {
        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        byte TDL, TDH;
        //        int ProT = 0;
        //        ProT = Convert.ToInt32(SetProTime * 1000);//uS
        //        TDL = Convert.ToByte(ProT & 0x00FF);
        //        TDH = Convert.ToByte((ProT & 0xFF00) >> 8);

        //        SetVolt *= 10;//mv
        //        byte VDL, VDH;
        //        VDL = Convert.ToByte(SetVolt & 0x00FF);
        //        VDH = Convert.ToByte((SetVolt & 0xFF00) >> 8);

        //        byte Up = 0x00;
        //        if (blSaveT)
        //            Up = 0x01;
        //        else
        //            Up = 0x00;

        //        string Cmd = "";
        //        Cmd = "C9-CC-0B-12-" + Up.ToString("X2") + "-" + VDL.ToString("X2") + "-" + VDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                Errcode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                if (ReturnBuffer[7] == 0x01)
        //                    Errcode += " - 超时未保护;";
        //                else if (ReturnBuffer[7] == 0x02)
        //                    Errcode += " - 开启前已保护;";
        //                return false;
        //            }
        //            else
        //            {
        //                dblProDelay = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 10;//us
        //                return true;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool OVPorUVP(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetLoadCur, int SetCellVolt, double SetProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out string Errcode)
        //{
        //    dblProDelay = 0.0f;
        //    Errcode = "";
        //    try
        //    {
        //        byte CorD = 0x00;
        //        if (LoadchgDsgMode == SelectChgDsgMode.CHG)
        //            CorD = 0X00;
        //        else if (LoadchgDsgMode == SelectChgDsgMode.DSG)
        //            CorD = 0x01;
        //        else
        //        {
        //            Errcode = "LoadchgDsgMode未选择";
        //            return false;
        //        }

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        byte LVDL, LVDM, LVDH, LCDL, LCDM, LCDH, CVDL, CVDH, TDL, TDH;

        //        SetLoadVolt *= 10;
        //        SetLoadCur *= 10;
        //        SetCellVolt *= 10;

        //        LVDL = Convert.ToByte(SetLoadVolt & 0X0000FF);
        //        LVDM = Convert.ToByte((SetLoadVolt & 0X00FF00) >> 8);
        //        LVDH = Convert.ToByte((SetLoadVolt & 0XFF0000) >> 16);

        //        LCDL = Convert.ToByte(SetLoadCur & 0X0000FF);
        //        LCDM = Convert.ToByte((SetLoadCur & 0X00FF00) >> 8);
        //        LCDH = Convert.ToByte((SetLoadCur & 0XFF0000) >> 16);

        //        CVDL = Convert.ToByte(SetCellVolt & 0X00FF);
        //        CVDH = Convert.ToByte((SetCellVolt & 0XFF00) >> 8);

        //        //if (SetProDelay > 1000)
        //        //{
        //        //    Errcode = "保护上限延时1000us";
        //        //    return false;
        //        //}
        //        TDL = Convert.ToByte(Convert.ToInt32(Math.Ceiling(SetProDelay)) & 0X00FF);
        //        TDH = Convert.ToByte((Convert.ToInt32(Math.Ceiling(SetProDelay)) & 0XFF00) >> 8);

        //        //(50+TrigP*0.2)=TrigPercent
        //        byte TrigP = 0x00;
        //        if (TrigPercent <= 50)
        //            TrigP = 0x00;
        //        else if (TrigPercent >= 100)
        //            TrigP = 0xFA;
        //        else
        //            TrigP = Convert.ToByte((TrigPercent - 50) * 10 / 2);

        //        string Cmd = "";
        //        Cmd = "C9-CC-12-19-" + CorD.ToString("X2") + "-" + LVDL.ToString("X2") + "-" + LVDM.ToString("X2") + "-" + LVDH.ToString("X2") + "-" + LCDL.ToString("X2") + "-" + LCDM.ToString("X2") + "-" + LCDH.ToString("X2")
        //           + "-" + CVDL.ToString("X2") + "-" + CVDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + TrigP.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                Errcode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                if (ReturnBuffer[7] == 0x01)
        //                    Errcode += " - 超时未保护;";
        //                else if (ReturnBuffer[7] == 0x02)
        //                    Errcode += " - 开启前，电流异常;";
        //                return false;
        //            }
        //            else
        //            {
        //                dblProDelay = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01;//10us
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool OVPorUVPScan(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetLoadCur, int SetCellVoltMin, int SetCellVoltMax, int stepV, int stepT, double SetProDelay, double RealProDelay, int TrigPercent, bool isDisable, out double dblReleaseVolt, out string Errcode)
        //{

        //    Errcode = "";
        //    dblReleaseVolt = 0.0f;
        //    try
        //    {
        //        int ComReaviceDelay = Convert.ToInt32(stepT * (SetCellVoltMax - SetCellVoltMin) / stepV + SetProDelay + 5000);


        //        byte CorD = 0x00;
        //        if (LoadchgDsgMode == SelectChgDsgMode.CHG)
        //            CorD = 0X00;
        //        else if (LoadchgDsgMode == SelectChgDsgMode.DSG)
        //            CorD = 0x01;
        //        else
        //        {
        //            Errcode = "LoadchgDsgMode未选择";
        //            return false;
        //        }

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        byte LVDL, LVDM, LVDH, LCDL, LCDM, LCDH, CMinVDL, CMinVDH, CMaxVDL, CMaxVDH, TDL, TDH;
        //        SetLoadVolt *= 10;
        //        SetLoadCur *= 10;
        //        SetCellVoltMin *= 10;
        //        SetCellVoltMax *= 10;

        //        LVDL = Convert.ToByte(SetLoadVolt & 0X0000FF);
        //        LVDM = Convert.ToByte((SetLoadVolt & 0X00FF00) >> 8);
        //        LVDH = Convert.ToByte((SetLoadVolt & 0XFF0000) >> 16);

        //        LCDL = Convert.ToByte(SetLoadCur & 0X0000FF);
        //        LCDM = Convert.ToByte((SetLoadCur & 0X00FF00) >> 8);
        //        LCDH = Convert.ToByte((SetLoadCur & 0XFF0000) >> 16);

        //        TDL = Convert.ToByte(Convert.ToInt32(Math.Ceiling(SetProDelay)) & 0X00FF);
        //        TDH = Convert.ToByte((Convert.ToInt32(Math.Ceiling(SetProDelay)) & 0XFF00) >> 8);


        //        CMinVDL = Convert.ToByte(SetCellVoltMin & 0X00FF);
        //        CMinVDH = Convert.ToByte((SetCellVoltMin & 0XFF00) >> 8);

        //        CMaxVDL = Convert.ToByte(SetCellVoltMax & 0X00FF);
        //        CMaxVDH = Convert.ToByte((SetCellVoltMax & 0XFF00) >> 8);

        //        byte stepVL, stepVH, stepTL, stepTH;
        //        stepV *= 10;
        //        stepVL = Convert.ToByte(stepV & 0X00FF);
        //        stepVH = Convert.ToByte((stepV & 0XFF00) >> 8);
        //        stepTL = Convert.ToByte(stepT & 0X00FF);
        //        stepTH = Convert.ToByte((stepT & 0XFF00) >> 8);

        //        byte RpTL, RpTM, RpTH;//真实保护时间
        //        RealProDelay *= 100;//10uS
        //        RpTL = Convert.ToByte(Convert.ToInt32(Math.Ceiling(RealProDelay)) & 0X0000FF);
        //        RpTM = Convert.ToByte((Convert.ToInt32(Math.Ceiling(RealProDelay)) & 0X00FF00) >> 8);
        //        RpTH = Convert.ToByte((Convert.ToInt32(Math.Ceiling(RealProDelay)) & 0XFF0000) >> 16);

        //        //(50+TrigP*0.2)=TrigPercent
        //        byte TrigP = 0x00;
        //        if (TrigPercent <= 50)
        //            TrigP = 0x00;
        //        else if (TrigPercent >= 100)
        //            TrigP = 0xFA;
        //        else
        //            TrigP = Convert.ToByte((TrigPercent - 50) * 10 / 2);

        //        string Cmd = "";
        //        Cmd = "C9-CC-1B-1A-" + CorD.ToString("X2") + "-" + LVDL.ToString("X2") + "-" + LVDM.ToString("X2") + "-" + LVDH.ToString("X2") + "-" + LCDL.ToString("X2") + "-" + LCDM.ToString("X2") + "-" + LCDH.ToString("X2")
        //           + "-" + CMinVDL.ToString("X2") + "-" + CMinVDH.ToString("X2") + "-" + CMaxVDL.ToString("X2") + "-" + CMaxVDH.ToString("X2") + "-" + stepVL.ToString("X2") + "-" + stepVH.ToString("X2") + "-" + stepTL.ToString("X2") + "-" + stepTH.ToString("X2")
        //           + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + RpTL.ToString("X2") + "-" + RpTM.ToString("X2") + "-" + RpTH.ToString("X2") + "-" + TrigP.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;


        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer, ComReaviceDelay);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                Errcode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                //if (ReturnBuffer[10] == 0x01)
        //                //    Errcode += " - 超时未保护;";
        //                //else if (ReturnBuffer[10] == 0x02)
        //                //    Errcode += " - 开启前，电流异常;";
        //                return false;
        //            }
        //            else
        //            {
        //                dblReleaseVolt = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01;//Mv
        //                //dblReleaseVolt= Math.Round((ReturnBuffer[9] * 256 * 256 + ReturnBuffer[8] * 256 + ReturnBuffer[7]) * 0.01,2);//mv
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}
        //private bool OVPorUVPRelease(SelectChgDsgMode LoadchgDsgMode, int ReCellVoltMin, int ReCellVoltMax, double ReleaseTime, int StepVolt, SelectReleaseMode selectReleaseMode, int LoadVolt, int LoadCurr, out double dblReleaseVolt, out string Errcode)
        //{
        //    Errcode = "";
        //    dblReleaseVolt = 0.0f;
        //    int ComReaviceDelay = Convert.ToInt32((ReleaseTime + 250) * (ReCellVoltMax - ReCellVoltMin) / StepVolt) + 500;

        //    try
        //    {
        //        byte CorD = 0x00;
        //        if (LoadchgDsgMode == SelectChgDsgMode.CHG)
        //            CorD = 0X00;
        //        else if (LoadchgDsgMode == SelectChgDsgMode.DSG)
        //            CorD = 0x01;
        //        else
        //        {
        //            Errcode = "LoadchgDsgMode未选择";
        //            return false;
        //        }


        //        byte CMinVDL, CMinVDH, CMaxVDL, CMaxVDH, RTDL, RTDH;
        //        ReCellVoltMin *= 10;
        //        ReCellVoltMax *= 10;

        //        RTDL = Convert.ToByte(Convert.ToInt32(Math.Ceiling(ReleaseTime)) & 0X00FF);
        //        RTDH = Convert.ToByte((Convert.ToInt32(Math.Ceiling(ReleaseTime)) & 0XFF00) >> 8);

        //        CMinVDL = Convert.ToByte(ReCellVoltMin & 0X00FF);
        //        CMinVDH = Convert.ToByte((ReCellVoltMin & 0XFF00) >> 8);

        //        CMaxVDL = Convert.ToByte(ReCellVoltMax & 0X00FF);
        //        CMaxVDH = Convert.ToByte((ReCellVoltMax & 0XFF00) >> 8);

        //        byte stepVL, stepVH;
        //        StepVolt *= 10;
        //        stepVL = Convert.ToByte(StepVolt & 0X00FF);
        //        stepVH = Convert.ToByte((StepVolt & 0XFF00) >> 8);

        //        byte LoadVDL, LoadVDM, LoadVDH, LoadCDL, LoadCDM, LoadCDH;
        //        LoadVolt *= 10;
        //        LoadVDL = Convert.ToByte(LoadVolt & 0X0000FF);
        //        LoadVDM = Convert.ToByte((LoadVolt & 0X00FF00) >> 8);
        //        LoadVDH = Convert.ToByte((LoadVolt & 0XFF0000) >> 16);

        //        LoadCurr *= 10;
        //        LoadCDL = Convert.ToByte(LoadCurr & 0X0000FF);
        //        LoadCDM = Convert.ToByte((LoadCurr & 0X00FF00) >> 8);
        //        LoadCDH = Convert.ToByte((LoadCurr & 0XFF0000) >> 16);

        //        byte RM = 0X00;
        //        if (selectReleaseMode == SelectReleaseMode.Auto)
        //            RM = 0X00;
        //        else if (selectReleaseMode == SelectReleaseMode.CHG)
        //            RM = 0x01;
        //        else if (selectReleaseMode == SelectReleaseMode.DSG)
        //            RM = 0x02;
        //        else if (selectReleaseMode == SelectReleaseMode.Load)
        //            RM = 0x03;

        //        else
        //        {
        //            Errcode = "恢复方式未选择";
        //            return false;
        //        }

        //        string Cmd = "";
        //        Cmd = "C9-CC-15-21-" + CorD.ToString("X2") + "-" + CMinVDL.ToString("X2") + "-" + CMinVDH.ToString("X2") + "-" + CMaxVDL.ToString("X2") + "-" + CMaxVDH.ToString("X2") + "-" + RTDL.ToString("X2") + "-" + RTDH.ToString("X2")
        //           + "-" + stepVL.ToString("X2") + "-" + stepVH.ToString("X2") + "-" + RM.ToString("X2") + "-" + LoadVDL.ToString("X2") + "-" + LoadVDM.ToString("X2") + "-" + LoadVDH.ToString("X2") + "-" + LoadCDL.ToString("X2") + "-" + LoadCDM.ToString("X2")
        //           + "-" + LoadCDH.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer, ComReaviceDelay);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                Errcode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                //if (ReturnBuffer[10] == 0x01)
        //                //    Errcode += " - 超时未保护;";
        //                //else if (ReturnBuffer[10] == 0x02)
        //                //    Errcode += " - 开启前，电流异常;";
        //                return false;
        //            }
        //            else
        //            {
        //                dblReleaseVolt = Math.Round((ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01, 2);//mv
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}


        //private bool CocpOrDocp(SelectChgDsgMode chgDsgMode, int SetLoadVolt, int SetLoadCur, double SetProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out string Errcode)
        //{
        //    dblProDelay = 0.0f;
        //    Errcode = "";
        //    try
        //    {
        //        byte CorD = 0x00;
        //        if (chgDsgMode == SelectChgDsgMode.CHG)
        //            CorD = 0X00;
        //        else if (chgDsgMode == SelectChgDsgMode.DSG)
        //            CorD = 0x01;
        //        else
        //        {
        //            Errcode = "chgDsgMode未选择";
        //            return false;
        //        }

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        byte VDL, VDM, VDH, CDL, CDM, CDH, TDL, TDH;

        //        SetLoadVolt *= 10;
        //        SetLoadCur *= 10;


        //        VDL = Convert.ToByte(SetLoadVolt & 0X0000FF);
        //        VDM = Convert.ToByte((SetLoadVolt & 0X00FF00) >> 8);
        //        VDH = Convert.ToByte((SetLoadVolt & 0XFF0000) >> 16);

        //        CDL = Convert.ToByte(SetLoadCur & 0X0000FF);
        //        CDM = Convert.ToByte((SetLoadCur & 0X00FF00) >> 8);
        //        CDH = Convert.ToByte((SetLoadCur & 0XFF0000) >> 16);


        //        //if (SetProDelay > 1000)
        //        //{
        //        //    Errcode = "保护上限延时1000us";
        //        //    return false;
        //        //}
        //        SetProDelay += 20;
        //        TDL = Convert.ToByte(Convert.ToInt32(Math.Ceiling(SetProDelay)) & 0X00FF);
        //        TDH = Convert.ToByte((Convert.ToInt32(Math.Ceiling(SetProDelay)) & 0XFF00) >> 8);

        //        //(50+TrigP*0.2)=TrigPercent
        //        byte TrigP = 0x00;
        //        if (TrigPercent <= 50)
        //            TrigP = 0x00;
        //        else if (TrigPercent >= 100)
        //            TrigP = 0xFA;
        //        else
        //            TrigP = Convert.ToByte((TrigPercent - 50) * 10 / 2);

        //        string Cmd = "";
        //        Cmd = "C9-CC-10-1B-" + CorD.ToString("X2") + "-" + VDL.ToString("X2") + "-" + VDM.ToString("X2") + "-" + VDH.ToString("X2") + "-" + CDL.ToString("X2") + "-" + CDM.ToString("X2") + "-" + CDH.ToString("X2")
        //           + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + TrigP.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                Errcode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                if (ReturnBuffer[7] == 0x01)
        //                    Errcode += " - 超时未保护;";
        //                else if (ReturnBuffer[7] == 0x02)
        //                    Errcode += " - 开启前，电流异常;";
        //                return false;
        //            }
        //            else
        //            {
        //                dblProDelay = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01;//10us
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool CocpOrDocpScan(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetCurMin, int SetCurMax, int stepC, int stepT, double SetProDelay, double RealProDelay, int TrigPercent, bool isDisable, out double dblProCur, out string Errcode)
        //{

        //    Errcode = "";
        //    dblProCur = 0.0f;
        //    try
        //    {
        //        byte CorD = 0x00;
        //        if (LoadchgDsgMode == SelectChgDsgMode.CHG)
        //            CorD = 0X00;
        //        else if (LoadchgDsgMode == SelectChgDsgMode.DSG)
        //            CorD = 0x01;
        //        else
        //        {
        //            Errcode = "LoadchgDsgMode未选择";
        //            return false;
        //        }

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;


        //        byte LVDL, LVDM, LVDH, CMinDL, CMinDM, CMinDH, CMaxDL, CMaxDM, CMaxDH, TDL, TDH;
        //        SetLoadVolt *= 10;
        //        SetCurMin *= 10;
        //        SetCurMax *= 10;

        //        LVDL = Convert.ToByte(SetLoadVolt & 0X0000FF);
        //        LVDM = Convert.ToByte((SetLoadVolt & 0X00FF00) >> 8);
        //        LVDH = Convert.ToByte((SetLoadVolt & 0XFF0000) >> 16);

        //        CMinDL = Convert.ToByte(SetCurMin & 0X0000FF);
        //        CMinDM = Convert.ToByte((SetCurMin & 0X00FF00) >> 8);
        //        CMinDH = Convert.ToByte((SetCurMin & 0XFF0000) >> 16);

        //        CMaxDL = Convert.ToByte(SetCurMax & 0X0000FF);
        //        CMaxDM = Convert.ToByte((SetCurMax & 0X00FF00) >> 8);
        //        CMaxDH = Convert.ToByte((SetCurMax & 0XFF0000) >> 16);


        //        SetProDelay += 50;//更时间中位机计算
        //        TDL = Convert.ToByte((int)SetProDelay & 0X00FF);
        //        TDH = Convert.ToByte(((int)SetProDelay & 0XFF00) >> 8);



        //        byte stepCL, stepCH, stepTL, stepTM, stepTH;
        //        //stepC *= 10;
        //        stepCL = Convert.ToByte(stepC & 0X00FF);
        //        stepCH = Convert.ToByte((stepC & 0XFF00) >> 8);

        //        stepT *= 100;
        //        stepTL = Convert.ToByte(stepT & 0X0000FF);
        //        stepTM = Convert.ToByte((stepT & 0X00FF00) >> 8);
        //        stepTH = Convert.ToByte((stepT & 0XFF0000) >> 16);

        //        byte RTL, RTM, RTH;//上限值测试的真实保护时间
        //        RealProDelay *= 100;//10us
        //        RTL = Convert.ToByte(Convert.ToInt32(RealProDelay) & 0X0000FF);
        //        RTM = Convert.ToByte((Convert.ToInt32(RealProDelay) & 0X00FF00) >> 8);
        //        RTH = Convert.ToByte((Convert.ToInt32(RealProDelay) & 0XFF0000) >> 16);



        //        //(50+TrigP*0.2)=TrigPercent
        //        byte TrigP = 0x00;
        //        if (TrigPercent <= 50)
        //            TrigP = 0x00;
        //        else if (TrigPercent >= 100)
        //            TrigP = 0xFA;
        //        else
        //            TrigP = Convert.ToByte((TrigPercent - 50) * 10 / 2);

        //        //byte RM = 0X00;
        //        //if (releaseMode == SelectReleaseMode.Auto)
        //        //    RM = 0X00;
        //        //else if (releaseMode == SelectReleaseMode.CHG)
        //        //    RM = 0x01;
        //        //else if (releaseMode == SelectReleaseMode.DSG)
        //        //    RM = 0x02;
        //        //else
        //        //{
        //        //    Errcode = "恢复方式未选择";
        //        //    return false;
        //        //}

        //        string Cmd = "";
        //        Cmd = "C9-CC-1B-1C-" + CorD.ToString("X2") + "-" + LVDL.ToString("X2") + "-" + LVDM.ToString("X2") + "-" + LVDH.ToString("X2") + "-" + CMinDL.ToString("X2") + "-" + CMinDM.ToString("X2") + "-" + CMinDH.ToString("X2")
        //           + "-" + CMaxDL.ToString("X2") + "-" + CMaxDM.ToString("X2") + "-" + CMaxDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + stepTL.ToString("X2") + "-" + stepTM.ToString("X2") + "-" + stepTH.ToString("X2") + "-" + stepCL.ToString("X2") + "-" + stepCH.ToString("X2")
        //           + "-" + RTL.ToString("X2") + "-" + RTM.ToString("X2") + "-" + RTH.ToString("X2") + "-" + TrigP.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                Errcode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                if (ReturnBuffer[7] == 0x01)
        //                    Errcode += " - 超时未保护;";
        //                else if (ReturnBuffer[7] == 0x02)
        //                    Errcode += " - 开启前，电流异常;";
        //                return false;
        //            }
        //            else
        //            {
        //                dblProCur = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.01;//10us
        //                                                                                                           //  dblProCur = Math.Round((ReturnBuffer[9] * 256 * 256 + ReturnBuffer[8] * 256 + ReturnBuffer[7]) * 0.01, 2);//mA
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool ShortTest(SelectChgDsgMode chgDsgMode, int SetLoadVolt, int SetLoadCur, int SetProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out string Errcode)
        //{
        //    dblProDelay = 0.0f;
        //    Errcode = "";
        //    try
        //    {
        //        byte CorD = 0x00;
        //        if (chgDsgMode == SelectChgDsgMode.CHG)
        //            CorD = 0X00;
        //        else if (chgDsgMode == SelectChgDsgMode.DSG)
        //            CorD = 0x01;
        //        else
        //        {
        //            Errcode = "chgDsgMode未选择";
        //            return false;
        //        }

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        byte VDL, VDM, VDH, CDL, CDM, CDH, TDL, TDM, TDH;

        //        SetLoadVolt *= 10;
        //        SetLoadCur *= 10;


        //        VDL = Convert.ToByte(SetLoadVolt & 0X0000FF);
        //        VDM = Convert.ToByte((SetLoadVolt & 0X00FF00) >> 8);
        //        VDH = Convert.ToByte((SetLoadVolt & 0XFF0000) >> 16);

        //        CDL = Convert.ToByte(SetLoadCur & 0X0000FF);
        //        CDM = Convert.ToByte((SetLoadCur & 0X00FF00) >> 8);
        //        CDH = Convert.ToByte((SetLoadCur & 0XFF0000) >> 16);


        //        if (SetProDelay > 3000)
        //        {
        //            Errcode = "保护上限延时设置>3000us";
        //            return false;
        //        }

        //        TDL = Convert.ToByte(SetProDelay & 0X0000FF);
        //        TDM = Convert.ToByte((SetProDelay & 0X00FF00) >> 8);
        //        TDH = Convert.ToByte((SetProDelay & 0XFF0000) >> 16);

        //        //(50+TrigP*0.2)=TrigPercent
        //        byte TrigP = 0x00;
        //        if (TrigPercent <= 50)
        //            TrigP = 0x00;
        //        else if (TrigPercent >= 100)
        //            TrigP = 0xFA;
        //        else
        //            TrigP = Convert.ToByte((TrigPercent - 50) * 10 / 2);

        //        string Cmd = "";
        //        Cmd = "C9-CC-11-1D-" + CorD.ToString("X2") + "-" + VDL.ToString("X2") + "-" + VDM.ToString("X2") + "-" + VDH.ToString("X2") + "-" + CDL.ToString("X2") + "-" + CDM.ToString("X2") + "-" + CDH.ToString("X2")
        //           + "-" + TDL.ToString("X2") + "-" + TDM.ToString("X2") + "-" + TDH.ToString("X2") + "-" + TrigP.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        //if (mixAllCmds.BlMixCmdsSend)//需要复合
        //        //{
        //        //    mixAllCmds.GetCmdToMixArrray(Cmd);
        //        //    return true;
        //        //}

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                Errcode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[7].ToString("X2");
        //                if (ReturnBuffer[7] == 0x01)
        //                    Errcode += " - 超时未保护;";
        //                else if (ReturnBuffer[7] == 0x02)
        //                    Errcode += " - 开启前，电流异常;";
        //                return false;
        //            }
        //            else
        //            {
        //                dblProDelay = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]);//1us
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool ShortTestScan(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetCurMin, int SetCurMax, int stepC, int stepT, double SetProDelay, double RealProTime, int TrigPercent, bool isDisable, out double dblProDelay, out double dblProCur, out string Errcode)
        //{
        //    dblProDelay = 0.0f;
        //    Errcode = "";
        //    dblProCur = 0.0f;
        //    try
        //    {
        //        byte CorD = 0x00;
        //        if (LoadchgDsgMode == SelectChgDsgMode.CHG)
        //            CorD = 0X00;
        //        else if (LoadchgDsgMode == SelectChgDsgMode.DSG)
        //            CorD = 0x01;
        //        else
        //        {
        //            Errcode = "LoadchgDsgMode未选择";
        //            return false;
        //        }

        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;


        //        byte LVDL, LVDM, LVDH, CMinDL, CMinDM, CMinDH, CMaxDL, CMaxDM, CMaxDH, TDL, TDM, TDH;
        //        SetLoadVolt *= 10;
        //        SetCurMin *= 10;
        //        SetCurMax *= 10;

        //        LVDL = Convert.ToByte(SetLoadVolt & 0X0000FF);
        //        LVDM = Convert.ToByte((SetLoadVolt & 0X00FF00) >> 8);
        //        LVDH = Convert.ToByte((SetLoadVolt & 0XFF0000) >> 16);

        //        CMinDL = Convert.ToByte(SetCurMin & 0X0000FF);
        //        CMinDM = Convert.ToByte((SetCurMin & 0X00FF00) >> 8);
        //        CMinDH = Convert.ToByte((SetCurMin & 0XFF0000) >> 16);

        //        CMaxDL = Convert.ToByte(SetCurMax & 0X0000FF);
        //        CMaxDM = Convert.ToByte((SetCurMax & 0X00FF00) >> 8);
        //        CMaxDH = Convert.ToByte((SetCurMax & 0XFF0000) >> 16);

        //        TDL = Convert.ToByte(Convert.ToInt32(Math.Ceiling(SetProDelay)) & 0X0000FF);
        //        TDM = Convert.ToByte((Convert.ToInt32(Math.Ceiling(SetProDelay)) & 0X00FF00) >> 8);
        //        TDH = Convert.ToByte((Convert.ToInt32(Math.Ceiling(SetProDelay)) & 0XFF0000) >> 16);

        //        byte stepCL, stepCH, stepTL, stepTH;
        //        stepC *= 10;
        //        stepCL = Convert.ToByte(stepC & 0X00FF);
        //        stepCH = Convert.ToByte((stepC & 0XFF00) >> 8);
        //        stepTL = Convert.ToByte(stepT & 0X00FF);
        //        stepTH = Convert.ToByte((stepT & 0XFF00) >> 8);

        //        byte RTL, RTM, RTH;
        //        RTL = Convert.ToByte(Convert.ToInt32(Math.Ceiling(RealProTime)) & 0X0000FF);
        //        RTM = Convert.ToByte((Convert.ToInt32(Math.Ceiling(RealProTime)) & 0X00FF00) >> 8);
        //        RTH = Convert.ToByte((Convert.ToInt32(Math.Ceiling(RealProTime)) & 0XFF0000) >> 16);

        //        //(50+TrigP*0.2)=TrigPercent
        //        byte TrigP = 0x00;
        //        if (TrigPercent <= 50)
        //            TrigP = 0x00;
        //        else if (TrigPercent >= 100)
        //            TrigP = 0xFA;
        //        else
        //            TrigP = Convert.ToByte((TrigPercent - 50) * 10 / 2);
        //        //byte RM = 0X00;
        //        //if (releaseMode == SelectReleaseMode.Auto)
        //        //    RM = 0X00;
        //        //else if (releaseMode == SelectReleaseMode.CHG)
        //        //    RM = 0x01;
        //        //else if (releaseMode == SelectReleaseMode.DSG)
        //        //    RM = 0x02;
        //        //else
        //        //{
        //        //    Errcode = "恢复方式未选择";
        //        //    return false;
        //        //}

        //        string Cmd = "";
        //        Cmd = "C9-CC-1B-1E-" + CorD.ToString("X2") + "-" + LVDL.ToString("X2") + "-" + LVDM.ToString("X2") + "-" + LVDH.ToString("X2") + "-" + CMinDL.ToString("X2") + "-" + CMinDM.ToString("X2") + "-" + CMinDH.ToString("X2")
        //           + "-" + CMaxDL.ToString("X2") + "-" + CMaxDM.ToString("X2") + "-" + CMaxDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDM.ToString("X2") + "-" + TDH.ToString("X2") + "-" + stepCL.ToString("X2") + "-" + stepCH.ToString("X2") + "-" + stepTL.ToString("X2") + "-" + stepTH.ToString("X2")
        //           + "-" + RTL.ToString("X2") + "-" + RTM.ToString("X2") + "-" + RTH.ToString("X2") + "-" + TrigP.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 12)
        //            {
        //                Errcode = "接收数据长度不为12";
        //                return false;
        //            }
        //            else if (ReturnBuffer[10] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[10].ToString("X2");
        //                if (ReturnBuffer[10] == 0x01)
        //                    Errcode += " - 超时未保护;";
        //                else if (ReturnBuffer[10] == 0x02)
        //                    Errcode += " - 开启前，电流异常;";
        //                return false;
        //            }
        //            else
        //            {
        //                dblProDelay = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 10;//us
        //                dblProCur = Math.Round((ReturnBuffer[9] * 256 * 256 + ReturnBuffer[8] * 256 + ReturnBuffer[7]) * 0.01, 2);//mA
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool SecOVPScan(bool blSaveT, int SetVoltMin, int SetVoltMax, int SetProTime, int stepV, int stepT, double RealProTime, bool isDisable, out double dblProDelay, out double dblProVolt, out string Errcode)
        //{
        //    dblProDelay = 0.0f;
        //    dblProVolt = 0.0f;
        //    Errcode = "";
        //    try
        //    {
        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;

        //        byte TDL, TDH;
        //        int ProT = 0;
        //        ProT = Convert.ToInt32(SetProTime * 1000);//uS
        //        TDL = Convert.ToByte(ProT & 0x00FF);
        //        TDH = Convert.ToByte((ProT & 0xFF00) >> 8);

        //        SetVoltMin *= 10;//mv
        //        SetVoltMax *= 10;//mv
        //        byte VMinDL, VMinDH, VMaxDL, VMaxDH;
        //        VMinDL = Convert.ToByte(SetVoltMin & 0x00FF);
        //        VMinDH = Convert.ToByte((SetVoltMin & 0xFF00) >> 8);

        //        VMaxDL = Convert.ToByte(SetVoltMax & 0x00FF);
        //        VMaxDH = Convert.ToByte((SetVoltMax & 0xFF00) >> 8);

        //        byte Up = 0x00;
        //        if (blSaveT)
        //            Up = 0x01;
        //        else
        //            Up = 0x00;


        //        byte stepVL, stepVH, stepTL, stepTH;
        //        stepV *= 10;
        //        stepVL = Convert.ToByte(stepV & 0X00FF);
        //        stepVH = Convert.ToByte((stepV & 0XFF00) >> 8);
        //        stepTL = Convert.ToByte(stepT & 0X00FF);
        //        stepTH = Convert.ToByte((stepT & 0XFF00) >> 8);

        //        byte RTL, RTM, RTH;
        //        RTL = Convert.ToByte(Convert.ToInt32(Math.Ceiling(RealProTime)) & 0X0000FF);
        //        RTM = Convert.ToByte((Convert.ToInt32(Math.Ceiling(RealProTime)) & 0X00FF00) >> 8);
        //        RTH = Convert.ToByte((Convert.ToInt32(Math.Ceiling(RealProTime)) & 0XFF0000) >> 16);

        //        //byte RM = 0X00;
        //        //if (releaseMode == SelectReleaseMode.Auto)
        //        //    RM = 0X00;
        //        //else if (releaseMode == SelectReleaseMode.CHG)
        //        //    RM = 0x01;
        //        //else if (releaseMode == SelectReleaseMode.DSG)
        //        //    RM = 0x02;
        //        //else
        //        //{
        //        //    Errcode = "恢复方式未选择";
        //        //    return false;
        //        //}


        //        string Cmd = "";
        //        Cmd = "C9-CC-14-1F-" + Up.ToString("X2") + "-" + VMinDL.ToString("X2") + "-" + VMinDH.ToString("X2") + "-" + VMaxDL.ToString("X2") + "-" + VMaxDH.ToString("X2") + "-" + TDL.ToString("X2") + "-" + TDH.ToString("X2")
        //              + "-" + stepTL.ToString("X2") + "-" + stepTH.ToString("X2") + "-" + stepVL.ToString("X2") + "-" + stepVH.ToString("X2") + "-" + RTL.ToString("X2") + "-" + RTM.ToString("X2") + "-" + RTH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 12)
        //            {
        //                Errcode = "接收数据长度不为12";
        //                return false;
        //            }
        //            else if (ReturnBuffer[10] != 0x00)
        //            {
        //                Errcode = "错误代码：" + ReturnBuffer[10].ToString("X2");
        //                //if (ReturnBuffer[10] == 0x01)
        //                //    Errcode += " - 超时未保护;";
        //                //else if (ReturnBuffer[10] == 0x02)
        //                //    Errcode += " - 开启前已保护;";
        //                return false;
        //            }
        //            else
        //            {
        //                dblProDelay = (ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 10;//us
        //                dblProVolt = Math.Round((ReturnBuffer[9] * 256 * 256 + ReturnBuffer[8] * 256 + ReturnBuffer[7]) * 0.01, 2);//mV
        //                return true;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool Comminit(string mode, string upRes, string upVolt, byte aDDR, string speed, out string Errcode)
        //{
        //    Errcode = "";
        //    try
        //    {
        //        byte CM = 0x00;
        //        if (mode == "I2C")
        //            CM = 0x00;
        //        else if (mode == "SMB")
        //            CM = 0x01;
        //        else if (mode == "Serial")
        //            CM = 0x03;
        //        else if (mode == "HDQ")
        //            CM = 0x08;
        //        else
        //        {
        //            Errcode = "通信模式选择NG";
        //            return false;
        //        }

        //        byte Res = 0x00;
        //        if (upRes == "2K")
        //            Res = 0x00;
        //        else if (upRes == "3K")
        //            Res = 0x01;
        //        else
        //        {
        //            Errcode = "上拉电阻选择NG";
        //            return false;
        //        }

        //        byte Vol = 0x00;
        //        if (upVolt == "1.8V")
        //            Vol = 0x01;
        //        else if (upVolt == "3.3V")
        //            Vol = 0x00;
        //        else
        //        {
        //            Errcode = "通信电压选择NG";
        //            return false;
        //        }


        //        byte bySpeed = 0x00;
        //        if (speed == "15Khz")
        //            bySpeed = 0x00;
        //        else if (speed == "100Khz")
        //            bySpeed = 0x14;
        //        else if (speed == "200Khz")
        //            bySpeed = 0x0A;
        //        else if (speed == "400Khz")
        //            bySpeed = 0x01;
        //        else
        //        {
        //            Errcode = "通信频率选择NG";
        //            return false;
        //        }


        //        string Cmd = "";
        //        Cmd = "C9-CC-0A-40-" + CM.ToString("X2") + "-" + Res.ToString("X2") + "-" + Vol.ToString("X2") + "-" + aDDR.ToString("X2") + "-" + bySpeed.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 6)
        //            {
        //                Errcode = "接收数据长度不为6";
        //                return false;
        //            }
        //            else if (ReturnBuffer[4] != 0X00)
        //            {
        //                Errcode = "错误代码:" + ReturnBuffer[4];
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool CommPinEnable(bool blSda, bool blScl, out string Errcode)
        //{
        //    Errcode = "";
        //    try
        //    {
        //        byte sda = 0x00;
        //        if (blSda)
        //            sda = 0x01;
        //        else
        //            sda = 0x00;

        //        byte scl = 0x00;
        //        if (blScl)
        //            scl = 0x01;
        //        else
        //            scl = 0x00;


        //        string Cmd = "";
        //        Cmd = "C9-CC-07-41-" + sda.ToString("X2") + "-" + scl.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 6)
        //            {
        //                Errcode = "接收数据长度不为6";
        //                return false;
        //            }
        //            else if (ReturnBuffer[4] != 0X00)
        //            {
        //                Errcode = "错误代码:" + ReturnBuffer[4];
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }

        //}

        //private bool CommWrite(byte Addr, byte RegAddr, byte[] Data, out string Errcode)
        //{
        //    try
        //    {
        //        Errcode = "";

        //        int Len = 0;
        //        if (Data.Length == 0)
        //        {
        //            Errcode = "写入数据为空";
        //            return false;
        //        }
        //        Len = Data.Length + 7;

        //        string Cmd = "";
        //        Cmd = "C9-CC-" + Len.ToString("X2") + "-45-" + Addr.ToString("X2") + "-" + RegAddr.ToString("X2") + "-";
        //        for (int i = 0; i < Data.Length; i++)
        //        {
        //            Cmd += Data[i].ToString("X2") + "-";
        //        }
        //        Cmd = Cmd.TrimEnd('-');

        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        if (mixAllCmds.BlMixCmdsSend)//需要复合
        //        {

        //            if (mixAllCmds.ArrayMixCmds.Count >= 40)
        //            {
        //                AllCmdSend(out Errcode);
        //            }
        //            mixAllCmds.GetCmdToMixArrray(Cmd);
        //            return true;
        //        }

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 6)
        //            {
        //                Errcode = "接收数据长度不为6";
        //                return false;
        //            }
        //            else if (ReturnBuffer[4] != 0X00)
        //            {
        //                Errcode = "错误代码:" + ReturnBuffer[4];
        //                return false;
        //            }
        //            else
        //            {

        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool CommRead(byte Addr, byte RegAddr, int Len, out byte[] Data, out string Errcode)
        //{
        //    Errcode = "";
        //    Data = new byte[Len];
        //    try
        //    {
        //        string Cmd = "";
        //        Cmd = "C9-CC-08-46-" + Addr.ToString("X2") + "-" + RegAddr.ToString("X2") + "-" + Len.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 6 + Len)
        //            {
        //                Errcode = "接收数据长度不为" + (Len + 6).ToString();
        //                return false;
        //            }
        //            else if (ReturnBuffer[Len + 4] != 0X00)
        //            {
        //                Errcode = "错误代码:" + ReturnBuffer[Len + 6];
        //                return false;
        //            }
        //            else
        //            {
        //                for (int i = 0; i < Len; i++)
        //                {
        //                    Data[i] = ReturnBuffer[i + 4];
        //                }
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool ReadErrMsg(out string strMsg, out string Errcode)
        //{
        //    strMsg = "";
        //    byte[] Msg;
        //    Errcode = "";
        //    try
        //    {
        //        string Cmd = "";
        //        Cmd = "C9-CC-05-F0";
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length < 6)
        //            {
        //                Errcode = "接收数据长度不足6位";
        //                return false;
        //            }
        //            else if (ReturnBuffer[2] != ReturnBuffer.Length)
        //            {
        //                Errcode = "接收长度与数据长度不符";
        //                return false;
        //            }
        //            else
        //            {
        //                Msg = new byte[ReturnBuffer.Length - 5];
        //                for (int i = 0; i < ReturnBuffer.Length - 5; i++)
        //                {
        //                    Msg[i] = ReturnBuffer[i + 4];
        //                }
        //                strMsg = Encoding.ASCII.GetString(Msg);

        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }


        //}

        //private bool ShortActiveEnable(int Delay, bool isDisable, out string strErrorCode)
        //{

        //    strErrorCode = "";
        //    try
        //    {


        //        byte YY = 0x00;
        //        if (isDisable)
        //            YY |= 0x01;
        //        else
        //            YY |= 0x00;


        //        byte TDL, TDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //        string Cmd = "";
        //        Cmd = "C9-CC-08-30-" + TDL.ToString("X2") + "-" + TDH.ToString("X2") + "-" + YY.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        if (mixAllCmds.BlMixCmdsSend)//需要复合
        //        {
        //            mixAllCmds.GetCmdToMixArrray(Cmd);
        //            return true;
        //        }

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);

        //        if (intResult < 0)
        //        {
        //            strErrorCode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 6)
        //            {
        //                strErrorCode = "接收数据长度不为6";
        //                return false;
        //            }
        //            else if (ReturnBuffer[4] != 0X00)
        //            {
        //                strErrorCode = "错误代码:" + ReturnBuffer[4];
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        strErrorCode = ex.Message;
        //        return false;
        //    }
        //}
        //private bool ReadTemp(out double dblTemp, out string strErrorCode)
        //{
        //    dblTemp = 0.0f;
        //    strErrorCode = "";
        //    try
        //    {
        //        string Cmd = "";
        //        // Cmd = "C8-CC-81-00-00-00-00";
        //        Cmd = "C9-CC-05-32";

        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteReadNew(SendByte, out ReturnBuffer, 4000);
        //        if (intResult < 0)
        //        {
        //            strErrorCode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 9)
        //            {
        //                strErrorCode = "接收数据长度不为9";
        //                return false;
        //            }
        //            else if (ReturnBuffer[7] != 0X00)
        //            {
        //                strErrorCode = "错误代码:" + ReturnBuffer[7];
        //                return false;
        //            }
        //            else
        //            {
        //                dblTemp = Math.Round((ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) * 0.0625, 2);//摄氏度
        //                return true;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        strErrorCode = ex.Message;
        //        return false;
        //    }
        //}

        ////private bool ReadTemp(out double dblTemp,out string strErrorCode)
        ////{
        ////    dblTemp = 0.0f;
        ////    strErrorCode = "";
        ////    try
        ////    {
        ////        string Cmd = "";
        ////        Cmd = "C8-CC-81-00-00-00-00";
        ////        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        ////        byte[] SendByte = stringToByte(Cmd);
        ////        byte[] ReturnBuffer;
        ////        int intResult = comm.WriteReadNew(SendByte, out ReturnBuffer, 4000);
        ////        if (intResult < 0)
        ////        {
        ////            strErrorCode = intResult.ToString();
        ////            return false;
        ////        }
        ////        else
        ////        {
        ////            if (ReturnBuffer.Length != 8)
        ////            {
        ////                strErrorCode = "接收数据长度不为8";
        ////                return false;
        ////            }
        ////            else if (ReturnBuffer[6] != 0X00)
        ////            {
        ////                strErrorCode = "错误代码:" + ReturnBuffer[6];
        ////                return false;
        ////            }
        ////            else
        ////            {
        ////                dblTemp = Math.Round((ReturnBuffer[6] * 256 * 256 + ReturnBuffer[5] * 256 + ReturnBuffer[4]) *0.0625, 2);//摄氏度
        ////                return true;
        ////            }
        ////        }
        ////    }
        ////    catch (System.Exception ex)
        ////    {
        ////        strErrorCode = ex.Message;
        ////        return false;
        ////    }
        ////}

        ////设置通信的光耦恒连
        //private bool SetCommRelayConn(bool blFlag, out string strErrorCode)
        //{

        //    strErrorCode = "";
        //    try
        //    {
        //        string Cmd = "";
        //        if (blFlag)
        //            Cmd = "C0-CC-B0-0E-00-01";
        //        else
        //            Cmd = "C0-CC-B0-0E-00-00";

        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteReadNew(SendByte, out ReturnBuffer, 1000);
        //        if (intResult < 0)
        //        {
        //            strErrorCode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            return Compare(SendByte, ReturnBuffer);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        strErrorCode = ex.Message;
        //        return false;
        //    }
        //}
        //private bool RelayBoardCmd(string MixCmd, out string strErrorCode)
        //{

        //    strErrorCode = "";
        //    string[] ArrCmd;

        //    if (MixCmd.Contains(','))
        //        ArrCmd = MixCmd.Split(',');
        //    else
        //    {
        //        ArrCmd = new string[1];
        //        ArrCmd[0] = MixCmd;
        //    }

        //    //MixCmd 格式：Addr0-EN0,Addr1-EN1....
        //    try
        //    {
        //        int Len = 5 + ArrCmd.Length * 2;
        //        string strCmd = "";

        //        for (int i = 0; i < ArrCmd.Length; i++)
        //        {
        //            strCmd += ArrCmd[i] + "-";
        //        }
        //        strCmd = strCmd.TrimEnd('-');

        //        string Cmd = "";
        //        Cmd = "C9-CC-" + Len.ToString("X2") + "-70-" + strCmd;
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        if (mixAllCmds.BlMixCmdsSend)//需要复合
        //        {
        //            mixAllCmds.GetCmdToMixArrray(Cmd);
        //            return true;
        //        }

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer, 1000);

        //        if (intResult < 0)
        //        {
        //            strErrorCode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 6)
        //            {
        //                strErrorCode = "接收数据长度不为6";
        //                return false;
        //            }
        //            else if (ReturnBuffer[4] != 0X00)
        //            {
        //                strErrorCode = "错误代码:" + ReturnBuffer[4];
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        strErrorCode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool MCUDelay(Int32 Delay, out string strErrorCode)
        //{

        //    strErrorCode = "";
        //    try
        //    {
        //        byte TDL, TDH;
        //        TDL = Convert.ToByte(Delay & 0x00FF);
        //        TDH = Convert.ToByte((Delay & 0xFF00) >> 8);

        //        string Cmd = "";
        //        Cmd = "C9-CC-07-49-" + TDL.ToString("X2") + "-" + TDH.ToString("X2");
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;


        //        if (mixAllCmds.BlMixCmdsSend)//需要复合
        //        {
        //            mixAllCmds.GetCmdToMixArrray(Cmd);
        //            return true;
        //        }


        //        int intResult = comm.WriteReadNew(SendByte, out ReturnBuffer, 4000);
        //        if (intResult < 0)
        //        {
        //            strErrorCode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 6)
        //            {
        //                strErrorCode = "接收数据长度不为6";
        //                return false;
        //            }
        //            else if (ReturnBuffer[4] != 0X00)
        //            {
        //                strErrorCode = "错误代码:" + ReturnBuffer[4];
        //                return false;
        //            }
        //            else
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        strErrorCode = ex.Message;
        //        return false;
        //    }
        //}

        //private bool AllCmdSend(out string strErrorCode)
        //{

        //    strErrorCode = "";
        //    if (!mixAllCmds.BlMixCmdsSend)
        //    {
        //        return true;
        //    }
        //    if (mixAllCmds.ArrayMixCmds.Count == 0)
        //        return true;
        //    //MixCmd 格式：Addr0-EN0,Addr1-EN1....
        //    try
        //    {
        //        string strCmd = "";
        //        for (int i = 0; i < mixAllCmds.ArrayMixCmds.Count; i++)
        //        {
        //            strCmd += mixAllCmds.ArrayMixCmds[i] + "-";
        //        }
        //        strCmd = strCmd.TrimEnd('-');
        //        int Len = 5 + mixAllCmds.ArrayMixCmds.Count;

        //        string Cmd = "";
        //        Cmd = "C9-CC-" + Len.ToString("X2") + "-F4-" + strCmd;
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;

        //        mixAllCmds.ArrayMixCmds.Clear();//发送后请空

        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer, 15000);

        //        if (intResult < 0)
        //        {
        //            strErrorCode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (ReturnBuffer.Length != 7)
        //            {
        //                strErrorCode = "接收数据长度不为7";
        //                return false;
        //            }
        //            else if (ReturnBuffer[5] != 0X00)
        //            {
        //                strErrorCode = "错误代码:" + ReturnBuffer[5] + "错误指令：" + ReturnBuffer[4];
        //                return false;
        //            }
        //            else
        //            {
        //                if (ReturnBuffer[3] == 0xF4)
        //                    return true;
        //                else
        //                {
        //                    strErrorCode = "返回指令不是F4";
        //                    return false;
        //                }
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        strErrorCode = ex.Message;
        //        return false;
        //    }

        //}

        //private bool SetOCVCycle(int Icycle, out string Errcode)
        //{
        //    Errcode = "";
        //    try
        //    {
        //        string Cmd = "";
        //        Cmd = "C5-CC-08-70-08-" + Icycle.ToString("X2") + "-00";
        //        Cmd = Cmd + "-" + CalcuCheckSum(Cmd);
        //        byte[] SendByte = stringToByte(Cmd);
        //        byte[] ReturnBuffer;
        //        int intResult = comm.WriteRead(SendByte, out ReturnBuffer);
        //        if (intResult < 0)
        //        {
        //            Errcode = intResult.ToString();
        //            return false;
        //        }
        //        else
        //        {
        //            if (Compare(SendByte, ReturnBuffer))
        //                return true;
        //            else
        //                return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Errcode = ex.Message;
        //        return false;
        //    }
        //}

    }
}
