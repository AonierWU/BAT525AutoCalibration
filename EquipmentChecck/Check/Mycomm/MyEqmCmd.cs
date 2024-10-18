using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace TestSystem_Pack
{
    public abstract class MyEqmCmd
    {

        //public enum SelectVoltPin { OCV, SCL, SDA };
        //public enum SelectIRRange { K0_2, K2_20, K20_200, K200_3000 };
        //public enum SelectIRType { IDR, NTC };
        //public enum SelectIDRPort { ID1, ID2, ID3, SWID, Cn, Cp, IDR, NTC };
        //public enum SelectChgDsgMode { CHG, DSG };
        //public enum SelectSCurrRange { uA2, uA20, uA200, uA2000, Hold };
        //public enum SelectReleaseMode { Auto, CHG, DSG, Load };
        //public enum ReadFlag { VOLT, CURR, BOTH };

        //public string ErrCode;

        //protected const double M_D_TEMPERATURE_FORFMAT = 0.0625f;

        public byte[] stringToByte(string Cmd)
        {
            string[] ArrStrCmd = Cmd.Split('-');
            byte[] byteCmd = new byte[ArrStrCmd.Length];

            for (int i = 0; i < ArrStrCmd.Length; i++)
            {
                byteCmd[i] = Convert.ToByte(ArrStrCmd[i], 16);
            }
            return byteCmd;
        }
        public bool Compare(byte[] SendData, byte[] ReturnData)
        {
            if (SendData.Length != ReturnData.Length)
            {
                return false;
            }
            for (int i = 0; i < SendData.Length; i++)
            {
                if (SendData[i] != ReturnData[i])
                    return false;
            }
            return true;
        }
        public string CalcuCheckSum(string strToCalcuCheckSum)
        {
            int intCheckSumTemp = 0;
            byte bytCheckSum = 0;
            byte[] data = new byte[(strToCalcuCheckSum.Length + 1) / 3];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Convert.ToByte(Convert.ToInt16(strToCalcuCheckSum.Substring(i * 3, 2), 16));
                intCheckSumTemp = intCheckSumTemp + data[i];
                intCheckSumTemp = intCheckSumTemp % 256;
            }
            bytCheckSum = Convert.ToByte(intCheckSumTemp);
            bytCheckSum = Convert.ToByte(255 - bytCheckSum);
            return bytCheckSum.ToString("X2");

        }
        public byte CalcuCheckSum(byte[] data)
        {
            int intCheckSumTemp = 0;
            byte bytCheckSum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                //data[i] = Convert.ToByte(Convert.ToInt32(strToCalcuCheckSum.Substring(i * 3, 2), 16));
                intCheckSumTemp = intCheckSumTemp + data[i];
                intCheckSumTemp = intCheckSumTemp % 256;
            }
            bytCheckSum = Convert.ToByte(intCheckSumTemp);
            bytCheckSum = Convert.ToByte(255 - bytCheckSum);
            return bytCheckSum;
        }
        //public MixAllCmds mixAllCmds = new MixAllCmds();//复合所有指令类

        protected MyComm comm;
        public bool Connect(CommType commType, string strInfo, Logger Log)
        {
            if (commType == CommType.RS232)
                comm = new RS232(strInfo + "-115200-8", Log);
            else if (commType == CommType.TcpIp)
                comm = new TcpIp(strInfo + "-80", Log);
            else
                return false;
            return comm.Connect();
        }
        public bool DisConnect()
        {
            return comm.Disconnect();
        }

        public bool DiscardInBuffer()
        {
            return comm.DiscardInBuffer();
        }
        //复合指令

        /// <summary>
        /// 设备初始化
        /// </summary>
        /// <param name="Errcode">错误代码</param>
        /// <returns></returns>
        public abstract bool MixEQMInit(out string Errcode);

        ///// <summary>
        ///// 读取开路电压、端口电压
        ///// </summary>
        ///// <param name="voltPin">OCV,SCL,SDA三种待测对象选择</param>
        ///// <param name="Delay">切入电压模块后延时</param>
        ///// <param name="isDisable">测试完成是否切走测量模块</param>
        ///// <param name="dblVolt">读取电压</param>
        ///// <param name="ErrCode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixReadVolt(SelectVoltPin voltPin, int Delay, bool isDisable, ReadFlag flag, out double dblVolt, out string Errcode);

        ///// <summary>
        ///// 切出Read OCV模块
        ///// </summary>
        ///// <returns></returns>
        //public abstract bool MixDisReadVolt(out string Errcode);


        ///// <summary>
        ///// 读取idr/ntc电阻
        ///// </summary>
        ///// <param name="iRPin">idr/ntc选择</param>
        ///// <param name="iRRange">量程切换</param>
        ///// <param name="Delay">切入模块后延时读取</param>
        ///// <param name="isDisable">测试完成后是否切出模块</param>
        ///// <param name="dblIR">读取IR值</param>
        ///// <param name="ErrCode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixReadIR(SelectIRType iRPin, SelectIRRange iRRange, SelectIDRPort IdrPort, int Delay, bool isDisable, out double dblIR, out string Errcode);

        ///// <summary>
        ///// 切出IDR/NTC电阻模块
        ///// </summary>
        ///// <returns></returns>
        //public abstract bool MixDisableIR(out string Errcode);

        ///// <summary>
        ///// LoadPart充放电测试
        ///// </summary>
        ///// <param name="chgDsgMode">充放电选择</param>
        ///// <param name="setVolt">设定电压</param>
        ///// <param name="setCur">设定电流</param>
        ///// <param name="Delay">设定延时</param>
        ///// <param name="isDisable">完成后是否切走模块</param>
        ///// <param name="dblVolt">读取电压</param>
        ///// <param name="dblCur">读取电流</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixLoadPartChgDsg(SelectChgDsgMode chgDsgMode, int setVolt, int setCur, int Delay, bool isDisable, ReadFlag flag, out double dblVolt, out double dblCur, out string Errcode);

        ///// <summary>
        ///// 切除LoadPart充放电模块
        ///// </summary>
        ///// <returns></returns>
        //public abstract bool MixDisableLoadPartChgDsg(out string Errcode);

        ///// <summary>
        ///// 设定模拟电芯模块
        ///// </summary>
        ///// <param name="setVolt">设定电压</param>
        ///// <param name="setCur">设定电流</param>
        ///// <param name="Delay">设定延时</param>
        ///// <param name="isDisable">是否自动切走电芯模块</param>
        ///// <param name="dblVolt">读取电压</param>
        ///// <param name="dblCur">读取电流</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixSetCell(int setVolt, int setCur, int Delay, bool isDisable, ReadFlag flag, bool blReadOutValue, out double dblVolt, out double dblCur, out string Errcode);

        ///// <summary>
        ///// 切走CELL模块
        ///// </summary>
        ///// <returns></returns>
        //public abstract bool MixDisableCell(out string Errcode);

        ///// <summary>
        /////读取静态电流
        ///// </summary>
        ///// <param name="sCurrRange">电流量程：2uA，20uA，200uA，2000uA</param>
        ///// <param name="Delay">延时</param>
        ///// <param name="iCycle">采样次数，2000次一个周期40ms/param>
        ///// <param name="blSO">是否包含电芯采样线电流</param>
        ///// <param name="isDisable">测试完成后是否自动切走模块</param>
        ///// <param name="dblSCur">返回静态电流</param>
        ///// <returns></returns>
        //public abstract bool MixReadStaticCurr(SelectSCurrRange sCurrRange, int Delay, int iCycle, bool blSO, bool isDisable, out double dblSCur, out string Errcode);
        //public abstract bool MixReadOneTimeStaticCurr(SelectSCurrRange sCurrRange, bool isDisable, out double dblSCur, out string Errcode);

        //public abstract bool MixDisableReadStaticCurr(out string Errcode);

        ///// <summary>
        ///// 充放电测试
        ///// </summary>
        ///// <param name="chgDsgMode">充放电模式选择</param>
        ///// <param name="SetVolt">设定电压</param>
        ///// <param name="SetCur">设定电流</param>
        ///// <param name="Delay">设定时间</param>
        ///// <param name="isDisable">测试完成自动切走模块</param>
        ///// <param name="dblVolt">返回电压</param>
        ///// <param name="dblCur">返回电流</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixChgDsgTest(SelectChgDsgMode chgDsgMode, int SetVolt, int SetCur, int Delay, bool isDisable, ReadFlag flag, out double dblVolt, out double dblCur, out string Errcode);
        ///// <summary>
        ///// 充放电切出
        ///// </summary>
        ///// <returns></returns>
        //public abstract bool MixDisableChgDsgTest(out string Errcode);

        ///// <summary>
        ///// CNT静态电流测试
        ///// </summary>
        ///// <param name="chk10mA">是否先判断电流<10mA</param>
        ///// <param name="Delay">延时</param>
        ///// <param name="isDisable">测试完成是否切走模块</param>
        ///// <param name="dblCur">返回电流</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixCNTTest(bool chk10mA, int Delay, bool isDisable, out double dblCur, out string Errcode);

        ///// <summary>
        ///// cnt静态电流切出
        ///// </summary>
        ///// <returns></returns>
        //public abstract bool MixDisableCNTTest(out string Errcode);

        ///// <summary>
        ///// DCIR测试
        ///// </summary>
        ///// <param name="chgDsgMode">充放电选择</param>
        ///// <param name="SetCur">设定电流</param>
        ///// <param name="Delay">设定时间</param>
        ///// <param name="dblDCIR">返回DCIR</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixDCIR(SelectChgDsgMode chgDsgMode, int SetCur, int Delay, bool isDisable, out double dblDCIR, out string Errcode);
        ///// <summary>
        ///// 设定编程电压
        ///// </summary>
        ///// <param name="SetVolt">设定电压</param>
        ///// <param name="Delay">设定延时</param>
        ///// <param name="isDisable">测试完成是否切走模块</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixSetProgVolt(int SetVolt, int Delay, bool isDisable, out double dblVolt, out string Errcode);
        ///// <summary>
        ///// 关闭编程电压
        ///// </summary>
        ///// <returns></returns>
        //public abstract bool MixDisableProgVolt(out string Errcode);

        ///// <summary>
        ///// 二次过压保护
        ///// </summary>
        ///// <param name="blSaveT">SaveT上拉5V</param>
        ///// <param name="SetVolt">设定电芯电压</param>
        ///// <param name="SetProTime">设定保护时间上限</param>
        ///// <param name="isDisable">切出模块</param>
        ///// <param name="dblProDelay">返回保护时间</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixSecOVP(bool blSaveT, int SetVolt, double SetProTime, bool isDisable, out double dblProDelay, out string Errcode);

        ///// <summary>
        ///// 爬升方式测试二次过压
        ///// </summary>
        ///// <param name="blSaveT">SaveT上拉5V</param>
        ///// <param name="SetVoltMin">设定下限电压</param>
        ///// <param name="SetVoltMax">设定上限电压</param>
        ///// <param name="SetProTime">设定保护时间</param>
        ///// <param name="stepV">设定步进电压</param>
        ///// <param name="stepT">设定步进时间</param>
        ///// <param name="RealProTime">真实保护时间</param>
        ///// <param name="isDisable">测试完切走模块</param>
        ///// <param name="dblProDelay">返回保护时间</param>
        ///// <param name="dblProVolt">返回保护电压</param>
        ///// <param name="Errcode">错误方式</param>
        ///// <returns></returns>
        //public abstract bool MixSecOVPScan(bool blSaveT, int SetVoltMin, int SetVoltMax, int SetProTime, int stepV, int stepT, double RealProTime, bool isDisable, out double dblProDelay, out double dblProVolt, out string Errcode);

        ///// <summary>
        ///// 切出SecOVP
        ///// </summary>
        ///// <returns></returns>
        //public abstract bool MixDisableSecOVP(out string Errcode);

        ///// <summary>
        ///// 过充过放保护测试
        ///// </summary>
        ///// <param name="LoadchgDsgMode">设定负载充放电模式</param>
        ///// <param name="SetLoadVolt">设定负载电压</param>
        ///// <param name="SetLoadCur">设定负载电流</param>
        ///// <param name="SetCellVolt">设定电芯电压</param>
        ///// <param name="SetProDelay">设定保护时间上限</param>
        ///// <param name="TrigPercent">(50-100)触发百分比(50+P*0.2%)</param>
        ///// <param name="isDisable">测试完成切走模块</param>
        ///// <param name="dblProDelay">返回保护时间</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixOVPorUVP(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetLoadCur, int SetCellVolt, double SetProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out string Errcode);

        ///// <summary>
        ///// OVP or UVP步进恢复
        ///// </summary>
        ///// <param name="LoadchgDsgMode">OVP/UVP选择</param>
        ///// <param name="ReCellVoltMin">恢复下限</param>
        ///// <param name="ReCellVoltMax">恢复上限</param>
        ///// <param name="ReleaseTime">恢复时间</param>
        ///// <param name="StepVolt">步进电压</param>
        ///// <param name="selectReleaseMode">恢复模式</param>
        ///// <param name="LoadVolt">负载电压</param>
        ///// <param name="LoadCurr">负载电流</param>
        ///// <param name="dblReleaseVolt">恢复电压</param>
        ///// <param name="Errcode"></param>
        ///// <returns></returns>
        //public abstract bool MixOVPorUVPRelease(SelectChgDsgMode LoadchgDsgMode, int ReCellVoltMin, int ReCellVoltMax, double ReleaseTime, int StepVolt, SelectReleaseMode selectReleaseMode, int LoadVolt, int LoadCurr, out double dblReleaseVolt, out string Errcode);

        ///// <summary>
        ///// 爬升方式测试OVP、UVP
        ///// </summary>
        ///// <param name="LoadchgDsgMode">设定负载方式</param>
        ///// <param name="SetLoadVolt">设定负载电压</param>
        ///// <param name="SetLoadCur">设定负载电流</param>
        ///// <param name="SetCellVoltMin">设定电芯电压下限</param>
        ///// <param name="SetCellVoltMax">设定电芯电压上限</param>
        ///// <param name="stepV">步进电压</param>
        ///// <param name="stepT">步进电流</param>
        ///// <param name="SetProDelay">设定保护延时</param>
        ///// <param name="RealProDelay">使用上限测出的实际保护时间</param>
        ///// <param name="TrigPercent">触发百分比(50+P*0.2%)</param>
        ///// <param name="isDisable">测试完切走模块</param>
        ///// <param name="dblProDelay">返回保护时间</param>
        ///// <param name="dblReleaseVolt">返回恢复电芯电压</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixOVPorUVPScan(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetLoadCur,
        //    int SetCellVoltMin, int SetCellVoltMax, int stepV, int stepT, double SetProDelay, double RealProDelay, int TrigPercent, bool isDisable, out double dblReleaseVolt, out string Errcode);

        ///// <summary>
        ///// 充放电过流保护
        ///// </summary>
        ///// <param name="chgDsgMode">充放电模式选择</param>
        ///// <param name="SetLoadVolt">设定负载电压</param>
        ///// <param name="SetLoadCur">设定负载电流</param>
        ///// <param name="SetProDelay">设定保护延时</param>
        ///// <param name="TrigPercent">触发百分比</param>
        ///// <param name="isDisable">测试完是否切除模块</param>
        ///// <param name="dblProDelay">返回保护延时</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixCocpOrDocp(SelectChgDsgMode chgDsgMode, int SetLoadVolt, int SetLoadCur, double SetProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out string Errcode);

        ///// <summary>
        ///// 爬升方式充放电过流保护
        ///// </summary>
        ///// <param name="LoadchgDsgMode">充放电模式</param>
        ///// <param name="SetLoadVolt">设定负载电压</param>
        ///// <param name="SetCurMin">设定保护电流下限</param>
        ///// <param name="SetCurMax">设定保护电流上限</param>
        ///// <param name="stepC">步进电流</param>
        ///// <param name="stepT">步进时间</param>
        ///// <param name="SetProDelay">保护延时</param>
        ///// <param name="RealProTime">上限电流测试到的真实保护时间</param>
        ///// <param name="TrigPercent">触发百分比</param>
        ///// <param name="isDisable">测试完是否切走模块</param>
        ///// <param name="dblProDelay">返回保护时间</param>
        ///// <param name="dblProCur">返回保护电流</param>
        ///// <param name="Errcode"></param>
        ///// <returns></returns>
        //public abstract bool MixCocpOrDocpScan(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetCurMin, int SetCurMax, int stepC, int stepT, double SetProDelay,
        //    double RealProTime, int TrigPercent, bool isDisable, out double dblProCur, out string Errcode);

        ///// <summary>
        ///// 短路保护测试
        ///// </summary>
        ///// <param name="chgDsgMode">负载充放电模式</param>
        ///// <param name="SetLoadVolt">设定负载电压</param>
        ///// <param name="SetLoadCur">设定负载电流</param>
        ///// <param name="SetProDelay">设定保护延时</param>
        ///// <param name="TrigPercent">触发百分比</param>
        ///// <param name="isDisable">测试完切出模块</param>
        ///// <param name="dblProDelay">返回保护延时</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixShortTest(SelectChgDsgMode chgDsgMode, int SetLoadVolt, int SetLoadCur, int SetProDelay, int TrigPercent, bool isDisable, out double dblProDelay, out string Errcode);

        ///// <summary>
        ///// 爬升方式充放电短路保护
        ///// </summary>
        ///// <param name="LoadchgDsgMode">充放电模式</param>
        ///// <param name="SetLoadVolt">设定负载电压</param>
        ///// <param name="SetCurMin">设定保护电流下限</param>
        ///// <param name="SetCurMax">设定保护电流上限</param>
        ///// <param name="stepC">步进电流</param>
        ///// <param name="stepT">步进时间</param>
        ///// <param name="SetProDelay">保护延时</param>
        ///// <param name="RealProTime">实际保护延时</param>
        ///// <param name="TrigPercent">触发百分比</param>
        ///// <param name="isDisable">测试完是否切走模块</param>
        ///// <param name="dblProDelay">返回保护时间</param>
        ///// <param name="dblProCur">返回保护电流</param>
        ///// <param name="Errcode"></param>
        ///// <returns></returns>
        //public abstract bool MixShortTestScan(SelectChgDsgMode LoadchgDsgMode, int SetLoadVolt, int SetCurMin, int SetCurMax, int stepC, int stepT, double SetProDelay,
        //    double RealProTime, int TrigPercent, bool isDisable, out double dblProDelay, out double dblProCur, out string Errcode);

        ///// <summary>
        ///// 设置通信模式
        ///// </summary>
        ///// <param name="mode">通信方式</param>
        ///// <param name="upRes">上拉电阻</param>
        ///// <param name="upVolt">上拉电压</param>
        ///// <param name="aDDR">器件地址</param>
        ///// <param name="speed">通信频率</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixComminit(string mode, string upRes, string upVolt, byte aDDR, string speed, out string Errcode);
        ///// <summary>
        ///// 通信线拉高或拉低
        ///// </summary>
        ///// <param name="blSda">SDA是否拉高</param>
        ///// <param name="blScl">SCL是否拉高</param>
        ///// <param name="Errcode"></param>
        ///// <returns></returns>
        //public abstract bool MixCommPinEnable(bool blSda, bool blScl, out string Errcode);

        ///// <summary>
        ///// BQ写数据
        ///// </summary>
        ///// <param name="Addr">器件地址</param>
        ///// <param name="RegAddr">寄存器地址</param>
        ///// <param name="Data">待写数据</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixCommWrite(byte Addr, byte RegAddr, byte[] Data, out string Errcode);

        ///// <summary>
        ///// BQ读取数据
        ///// </summary>
        ///// <param name="Addr">器件地址</param>
        ///// <param name="RegAddr">寄存器地址</param>
        ///// <param name="Data">返回数据</param>
        ///// <param name="Errcode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixCommRead(byte Addr, byte RegAddr, int Len, out byte[] Data, out string Errcode);

        //public abstract bool MixMCUDelay(Int32 Delay);

        ///// <summary>
        ///// 返回错误信息
        ///// </summary>
        ///// <param name="Msg"></param>
        ///// <returns></returns>
        //public abstract bool MixReadErrMsg(out string Msg, out string Errcode);

        //public abstract bool MixShortActiveEnable(int wait, bool isDisable, out string Errcode);
        //public abstract bool MixShortActiveDisEnable(out string Errcode);
        //public abstract bool MixReadTemp(out double dblTemp, out string Errcode);

        ///// <summary>
        ///// 继电器板符合指令
        ///// </summary>
        ///// <param name="MixCmd">格式：Addr0-EN0,Addr1-EN1,Addr2-EN2......</param>
        ///// <param name="strErrorCode">错误代码</param>
        ///// <returns></returns>
        //public abstract bool MixRelayBoardCmd(string MixCmd, out string strErrorCode);

        ////所有指令皆可复合
        //public abstract bool MixAllCmdSend(out string strErrorCode);



        ////继电器板相关指令
        //public abstract bool MixLoadToAllCircleRelay(bool Enable, out string strErrorCode);
        //public abstract bool MixPrimaryProtLoadRelay(bool Enable, out string strErrorCode);
        //public abstract bool MixSecondProtLoadRelay(bool Enable, out string strErrorCode);
        //public abstract bool MixLS_PS_Enable(bool Enable, out string strErrorCode);

        //public abstract bool MixSetOCVCycle(int iCycle, out string strErrorCode);
        //public abstract bool MixOCVEnable(bool Enable, out string strErrorCode);
        //public abstract bool MixOCVCpEnable(bool Enable, out string strErrorCode);



        ///// <summary>
        ///// 滤波电路
        ///// </summary>
        ///// <param name="Enable">true：切入电容，false：切走电容</param>
        ///// <param name="Type">选择PS:L,"PS+/PS-"或"L+/L-"</param>
        ///// <returns></returns>
        //public abstract bool MixFilterCap(bool Enable, string Type, out string strErrorCode);
        //public abstract bool MixFilterCapNew(bool Enable, string Type, out string strErrorCode);

        //public abstract bool MixSwitchCellToB1B2(bool Enable, out string strErrorCode);

        //public abstract bool MixLoadSwitchBpToPp(bool Enable, out string strErrorCode);
        //public abstract bool MixLoadSwitchBpToJ2P(bool Enable, out string strErrorCode);
        //public abstract bool MixLoadSwitchBnToPnAndLSnToPSnBSn(bool Enable, out string strErrorCode);
        //public abstract bool MixLoadSwitchBnToJ2PnAndLSnToJ2PSn(bool Enable, out string strErrorCode);
        //public abstract bool MixLoadSwitchBnToPn(bool Enable, out string strErrorCode);

        //public abstract bool MixShortRY7(bool Enable, out string strErrorCode);//短接S+/S-


        /////// <summary>
        /////// Onewire初始化
        /////// </summary>
        /////// <param name="Enable">DGND与PS-是否短接</param>
        /////// <param name="CommVolt">电压1.8V，3.3V</param>
        /////// <param name="PullRes">上拉电阻：2K，510R</param>
        /////// <returns></returns>
        ////public abstract bool MixEnableOneWireComm(bool Enable, string CommVolt, string PullRes, out string strErrorCode);
        ////public abstract bool MixReadUID_Infineon_IC(out byte[] UIDData, out string Errcode);
        ////public abstract string MixECC_Infineon_IC();
        ////public abstract bool MixReadLock_Infineon_IC(out string NVMLock);
        ////public abstract bool MixInfineon_WritePage(byte startAddre, int writeNum, byte[] DataToWrite);
        ////public abstract bool MixInfineon_ReadPage(byte addre, int Num, out byte[] Data, out string Errcode);
        ////public abstract bool MixReadFM1230_FileStatus(byte fileAddress, out string NVMLock);

        ////public abstract bool MixOnewireReset(string Addr);
        //public abstract bool MixResetRelayBoard(out string strErrorCode);



        keysight ks;
        keysight ks2;
        NI device;
        public bool InitMeter(string DeviceType, string ConnType, string Addr, string strCom)
        {
            try
            {
                if (DeviceType == "34470A" || DeviceType == "34461A")
                {
                    if (ConnType == "Keysight库")
                    {

                        ks = new keysight();
                        ks.DeviceType = DeviceType;
                        ks.ConnectAdr = Addr;
                        ks.InitConnect();
                    }
                    else
                    {
                        string[] Addrs = Addr.Split('|');
                        device = new NI();
                        device.DeviceType = DeviceType;
                        device.ConnectAdr = Addr;
                        device.InitConnect();
                    }
                    return true;
                }
                else if (DeviceType == "34401A")
                {
                    ks = new keysight();
                    ks.DeviceType = DeviceType;
                    ks.ConnectAdr = Addr;
                    ks.ComPort = strCom;
                    ks.InitConnect();
                    return true;
                }
                else if (DeviceType == "E36312A" || DeviceType == "E36313A")
                {
                    if (ConnType == "Keysight库")
                    {

                        ks2 = new keysight();
                        ks2.DeviceTypeDCsource = DeviceType;
                        ks2.ConnectAdrDCsource = Addr;
                        ks2.InitConnectDCsource();
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                return false;

            }

        }

        public bool RSTMeter(string ConnType)
        {

            try
            {
                if (ConnType == "Keysight库")
                {

                    ks.RSTDevice();
                }
                else
                {
                    ks.RSTDevice();
                }
                return true;
            }
            catch (Exception)
            {

                return false;

            }

        }

        public bool ConfMeter(string ConnType, string MeasType, string Range)
        {
            try
            {
                if (ConnType == "Keysight库")
                {
                    if (MeasType == "RES")
                    {
                        if (ks.ConfRes())
                            return true;
                    }
                    else
                    {
                        if (ks.ConfDevice(MeasType, Range))
                            return true;
                    }
                }
                else
                {
                    if (device.ConfDevice(MeasType, Range.ToString()))
                        return true;
                }
                return false;

            }
            catch (Exception)
            {

                return false;

            }
        }

        public bool SetVolandOutPut(string ConnType, double Vol, double Cur, string Ch, bool en = false)
        {
            try
            {
                if (ConnType == "Keysight库")
                {
                    if (ks2.SetVolandOutPut(Vol, Cur, Ch, en))
                        return true;
                }
                return false;
            }
            catch (Exception) { return false; };
        }
        public bool ReadMeterValue(string ConnType, out double Vol)
        {
            Vol = 0.0f;
            try
            {
                if (ConnType == "Keysight库")
                {
                    if (ks.ReadValue(out Vol))
                        return true;
                }
                else
                {
                    if (device.ReadValue(out Vol))
                        return true;
                }
                return false;

            }
            catch (Exception)
            {

                return false;

            }
        }

        public bool ReadCurValue(string ConnType, out double Vol)
        {
            Vol = 0.0f;
            try
            {
                if (ConnType == "Keysight库")
                {
                    if (ks.ReadValueCur(out Vol))
                        return true;
                }
                else
                {
                    if (device.ReadValue(out Vol))
                        return true;
                }
                return false;

            }
            catch (Exception)
            {

                return false;

            }
        }
        public bool CloseDMMConn()
        {
            try
            {
                if (ks != null)
                {
                    if (!ks.CloseConnect())
                        return false;
                }
                return true;

            }
            catch
            {
                return false;
            }
        }
        //设备校准指令
        public abstract bool MixChgVolCalibration();
        public abstract bool MixDsgCurCalibration();
        public abstract bool MixOCVCalibration();
        public abstract bool MixProVoltCalibration();
        public abstract bool MixProVoltSetRang(byte status);
        public abstract bool MixCellVoltCalibration();
        public abstract bool MixCellCurCalibration();
        public abstract bool MixCNTCalibration();
        public abstract bool MixNTCCalibration();

        public abstract bool MixIDRCalibration();
        public abstract bool MixIDRRange(byte range);
        public abstract bool MixReadIDRValue(out double dblIdrValue);
        public abstract bool MixDCIRCalibration();
        public abstract bool MixStCurCalibration();
        public abstract bool MixNTCRange(byte range);
        public abstract bool MixStCurRange(byte R, byte range);
        public abstract bool MixLoadVoltCalibration();
        public abstract bool MixLoadCurCalibration();
        public abstract bool MixSetCurRange(byte range);
        public abstract bool MixSendZeroChgVolt(byte DL, byte DH);
        public abstract bool MixSendChgVolt(byte DL, byte DH);
        public abstract bool MixSendProVolt(byte DL, byte DH);
        public abstract bool MixSendZeroCellVolt(byte DL, byte DH);
        public abstract bool MixSendCellVolt(byte DL, byte DH);
        public abstract bool MixSendZeroLoadPartVolt(byte DL, byte DH);
        public abstract bool MixSendLoadPartVolt(byte DL, byte DH);
        public abstract bool MixSendZeroLoadPartCur(byte DL, byte DH);
        public abstract bool MixSendLoadPartCur(byte DL, byte DH);
        public abstract bool MixSendDsgCur(byte DL, byte DH);
        public abstract bool MixSendZeroDsgCur(byte DL, byte DH);
        public abstract bool MixSendZeroCellCur(byte DL, byte DH);
        public abstract bool MixSendCellCur(byte DL, byte DH);
        public abstract bool MixReadVoltADCCaliValue(out byte DL, out byte DM, out byte DH);
        public abstract bool MixReadCurADCCaliValue(out byte DL, out byte DM, out byte DH, out byte DL2, out byte DH2);
        public abstract bool MixReadProADCCaliValue(out byte DL, out byte DM, out byte DH);
        public abstract bool MixWriteVoltDAC_ADCCaliValue(byte Addr, byte DL, byte DH);
        public abstract bool MixWriteVoltADCCaliValue(byte Addr, byte DL, byte DM, byte DH);
        public abstract bool MixWriteOCV_DACCaliValue(byte Addr, byte DL, byte DM, byte DH);
        public abstract bool MixWritePro_ADCValue(byte Addr, byte DL, byte DH);
        public abstract bool MixReadOCV_DACCaliValue(byte Addr);
        public abstract bool MixSetVoltValue(double voltage);
        public abstract bool MixSetProVoltValue(int voltage);
        public abstract bool MixSetCellVoltValue(double voltage);
        public abstract bool MixSetLoadPartVoltValue(int Voltage);
        public abstract bool MixSetCellCurValue(int current);
        public abstract bool MixSetLoadPartCurValue(int current);
        public abstract bool MixSetCurValue(byte Rang, double Current);
        public abstract bool MixReadVoltValue(out double dblVoltage);
        public abstract bool MixReadCNTValue(byte Statu, out double dblVoltage);
        public abstract bool MixReadProVoltValue(out double dblVoltage);
        public abstract bool MixReadCurValue(out double dblCurrent);
        public abstract bool MixReadOCPCurValue(out double dblCurrent);
        public abstract bool MixReadOCVValue(out double dblOCV);

        public abstract bool MixReadPortVolValue(out double dblPortVol);
        public abstract bool MixReadCellVoltValue(out double dblCellVolt);
        public abstract bool MixReadLoadPatVoltValue(out double dblCellVolt);
        public abstract bool MixReadCellCurValue(out double dblCellCur);
        public abstract bool MixReadStCurValue(byte R, byte Range, out double dblCellCur);
        public abstract bool MixReadNTCValue(byte R, byte Range, out double dblCellCur);
        public abstract bool MixReadDCIRValue(byte Range, out double dblCellCur);
        public abstract bool MixReadLoadPartCurValue(out double dblCellCur);
        public abstract bool MixWriteOCV_DAC(out byte DL, out byte DM, out byte DH, out byte DL2, out byte DM2, out byte DH2);
        public abstract bool MixWriteProVolt_DAC(byte Addr, byte DL, byte DH);
        public abstract bool MixWriteDCIR_DAC(byte Addr, byte DL, byte DH);
        public abstract bool MixWriteCellVolt_DAC(byte Addr, byte DL, byte DH);

        public abstract bool MixWriteCellVolt_ADC(byte Addr, byte DL, byte DM, byte DH);
        public abstract bool MixWriteNTC_DAC(byte Addr1, byte DL1, byte DH1, byte Addr2, byte DL2, byte DH2, byte Addr3, byte DL3, byte DH3);
        public abstract bool MixWriteLoadPart_DAC(byte Addr, byte DL, byte DH);
        public abstract bool MixWriteLoadPartVol_ADC(byte Addr, byte DL, byte DM, byte DH);
        public abstract bool MixWriteDCIR_ADC(byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWriteIDR_ADC(string Write, byte[] ADC_L, byte[] ADC_H, byte[] Buf_L, byte[] Buf_M, byte[] Buf_H, byte[] Buf_HH);
        public abstract bool MixReadCNT_ADC(out byte DL, out byte DH);
        public abstract bool MixWriteCNT_ADC(byte Add, byte DL, byte DH);
        public abstract bool MixRoughWriteCNT_ADC(string Write, byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWritePVVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWriteOCV_ADC(byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWriteLVVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWriteCellVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWriteCellCur_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWriteLVCur_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWriteCHGVol_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWriteDSGCur1_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixWriteDSGCur2_ADCandDAC(byte[] DAC_L, byte[] DAC_H, byte[] ADC_L, byte[] ADC_H);//大电流
        public abstract bool MixWriteSTC_ADC(string Write, byte[] ADC_L, byte[] ADC_H);
        public abstract bool MixReadCellVolt_ADCValue(out byte DL, out byte DM, out byte DH);
        public abstract bool MixReadCellCur_ADCValue(out byte DL, out byte DM, out byte DH);
        public abstract bool MixReadStCur_ADCValue(byte Rang, out byte DL, out byte DH);
        public abstract bool MixReadNTC_ADCValue(byte Index, byte R, out byte DL1, out byte DH1, out byte DL2, out byte DH2, out byte DL3, out byte DH3);
        public abstract bool MixReadDCIR_ADCValue(byte Rang, out byte DL, out byte DH);
        public abstract bool MixReadLoadPartVolt_ADCValue(out byte DL, out byte DM, out byte DH);
        public abstract bool MixReadLoadPartCur_ADCValue(out byte DL, out byte DH);
        public abstract bool MixProVoltEnable(byte Status);
        public abstract bool MixEnable(byte Status);
        public abstract bool MixCHGEnable();
        public abstract bool MixDCIREnable();
        public abstract bool MixOCVEnable(byte Status);
        public abstract bool MixCellEnable(byte Status);
        public abstract bool MixStCurEnable();
        public abstract bool MixSetnARes(byte Status);
        public abstract bool MixCNTEnable();
        public abstract bool MixNTCEnable();
        public abstract bool MixLoadPartEnable(byte Status);
        public abstract bool MixLoadPartDisEn();
        public abstract bool MixUpdate();
        public abstract bool MixUpdateOCV();
        public abstract bool MixUpdateProVolt();
        public abstract bool MixUpdateCellVolt();
        public abstract bool MixUpdateDCIR();
        public abstract bool MixUpdateNTC();
        public abstract bool MixUpdateLoadPart();
        public abstract bool MixReadChgVolDAC(byte addr, out byte DL, out byte DH);
        public abstract bool MixReadChgVolADC(byte addr, out byte DL, out byte DM, out byte DH);
        public abstract bool MixReadOcvDAC(byte addr, out byte DL, out byte DH);
        public abstract bool MixReadOcvADC(byte addr, out byte DL, out byte DH);

        public abstract bool MixReadPrgDAC(byte addr, out byte DL, out byte DH);
        public abstract bool MixReadPrgADC(byte addr, out byte DL, out byte DH);
        public abstract bool MixReadCellDAC(byte addr, out byte DL, out byte DH);
        public abstract bool MixReadCellADC(byte addr, out byte DL, out byte DM, out byte DH);
        public abstract bool MixStCurCalibration_nA();
        public abstract bool MixStCurRange_nA(byte R, byte range);
        public abstract bool MixReadStCurValue_nA(byte Range, out double dblCellCur);
        public abstract bool MixReadNtcADCValue(byte addr, out byte DL, out byte DM, out byte DH);
        public abstract bool MixWriteSNCode(string EqmType, string dateTime, string SNnum,string status);
        public abstract bool MixReadSNCode(out string EqmType, out string dateTime, out string SNnum);
        public bool ChecckDcirRange()
        {
            int ret = -1;
            try
            {
                byte[] writeByte = { 0xC8, 0xCC, 0x58, 0x00, 0x00, 0x00 };
                ret = comm.WriteCommand(writeByte);
                if (ret < 0)
                    return false;
                else return true;
            }
            catch
            {
                return false;
            }
        }
        //继电器板指令
        public bool ChgVolCal_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                {
                    states = 0x01;
                }
                else
                {
                    states = 0x00;
                }
                byte[] RY1 = { 0xCF, 0xCC, 0x08, 0x70, 0x01, states, 0x00 }; //充电电压L +/ L -
                ret = comm.WriteCommand(RY1);
                if (ret < 0)
                    return false;
                byte[] RY3 = { 0xCF, 0xCC, 0x08, 0x70, 0x03, states, 0x00 };//充电电压LS+/LS-
                ret = comm.WriteCommand(RY3);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool RY3Enable_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                {
                    states = 0x01;
                }
                else
                {
                    states = 0x00;
                }
                byte[] RY3 = { 0xCF, 0xCC, 0x08, 0x70, 0x03, states, 0x00 };//充电电压LS+/LS-
                ret = comm.WriteCommand(RY3);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool CellVolCal_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                {
                    states = 0x01;
                }
                else
                {
                    states = 0x00;
                }
                byte[] RY5 = { 0xCF, 0xCC, 0x08, 0x70, 0x05, states, 0x00 }; //串电压B+/B-
                ret = comm.WriteCommand(RY5);
                if (ret < 0)
                    return false;
                byte[] RY6 = { 0xCF, 0xCC, 0x08, 0x70, 0x06, states, 0x00 };//串电压BS+/BS-
                ret = comm.WriteCommand(RY6);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool OCVorLVVolCal_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] RY10 = { 0xCF, 0xCC, 0x08, 0x70, 0x0A, states, 0x00 }; //ocv/lv
                ret = comm.WriteCommand(RY10);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool PortVolCal_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] RY12 = { 0xC0, 0xCC, 0xB0, 0x12, 0x01, 0x00 }; //sda/ps-
                ret = comm.WriteCommand(RY12);
                if (ret < 0)
                    return false;
                byte[] RY13 = { 0xCF, 0xCC, 0x08, 0x70, 0x0D, states, 0x00 }; //sda/ps-
                ret = comm.WriteCommand(RY13);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool PrgVolCal_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] RY16 = { 0xCF, 0xCC, 0x08, 0x70, 0x10, states, 0x00 }; //sda/ps-
                ret = comm.WriteCommand(RY16);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool DsgCur3ACal_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] Lp = { 0xCF, 0xCC, 0x08, 0x70, 0x1A, states, 0x00 }; //放电电流3A：L+
                ret = comm.WriteCommand(Lp);
                if (ret < 0)
                    return false;
                byte[] Ln = { 0xCF, 0xCC, 0x08, 0x70, 0x1B, states, 0x00 }; //放电电流3A：L-
                ret = comm.WriteCommand(Ln);
                if (ret < 0)
                    return false;
                byte[] RY9 = { 0xCF, 0xCC, 0x08, 0x70, 0x09, states, 0x00 }; //放电电流3A:LS+/LS-
                ret = comm.WriteCommand(RY9);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool DsgCur30ACal_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] Lp = { 0xCF, 0xCC, 0x08, 0x70, 0x1C, states, 0x00 }; //放电电流3A：L+
                ret = comm.WriteCommand(Lp);
                if (ret < 0)
                    return false;
                byte[] Ln = { 0xCF, 0xCC, 0x08, 0x70, 0x1D, states, 0x00 }; //放电电流3A：L-
                ret = comm.WriteCommand(Ln);
                if (ret < 0)
                    return false;
                byte[] RY17 = { 0xCF, 0xCC, 0x08, 0x70, 0x11, states, 0x00 }; //放电电流3A:LS+/LS-
                ret = comm.WriteCommand(RY17);
                if (ret < 0)
                    return false;
                byte[] RY14 = { 0xCF, 0xCC, 0x08, 0x70, 0x0E, states, 0x00 };
                ret = comm.WriteCommand(RY14);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool DsgCur30ACalExtRes_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] LpRp = { 0xCF, 0xCC, 0x08, 0x70, 0x5C, states, 0x00 }; //放电电流3A：L+
                ret = comm.WriteCommand(LpRp);
                if (ret < 0)
                    return false;
                byte[] LnRn = { 0xCF, 0xCC, 0x08, 0x70, 0x5B, states, 0x00 }; //放电电流3A：L-
                ret = comm.WriteCommand(LnRn);
                if (ret < 0)
                    return false;
                byte[] RY30 = { 0xCF, 0xCC, 0x08, 0x70, 0x5E, states, 0x00 }; //放电电流3A:LS+/LS-
                ret = comm.WriteCommand(RY30);
                if (ret < 0)
                    return false;
                byte[] RY29 = { 0xCF, 0xCC, 0x08, 0x70, 0x5D, states, 0x00 };
                ret = comm.WriteCommand(RY29);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool CellCurCal_RY(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] RY24 = { 0xCF, 0xCC, 0x08, 0x70, 0x18, states, 0x00 }; //sda/ps-
                ret = comm.WriteCommand(RY24);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool LVChgCurCal_RY(bool en = false)//BAT525C
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] RY2 = { 0xCF, 0xCC, 0x08, 0x70, 0x02, states, 0x00 }; //sda/ps-
                ret = comm.WriteCommand(RY2);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool LVDsgCurCal_RY(bool en = false)//BAT525G
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] RY7 = { 0xCF, 0xCC, 0x08, 0x70, 0x07, states, 0x00 }; //(BAT525G)LV放电电流PS+/PS-
                ret = comm.WriteCommand(RY7);
                if (ret < 0)
                    return false;
                byte[] RY8 = { 0xCF, 0xCC, 0x08, 0x70, 0x08, states, 0x00 }; //(BAT525G)LV放电电流连接电源
                ret = comm.WriteCommand(RY8);
                if (ret < 0)
                    return false;
                byte[] RY27 = { 0xCF, 0xCC, 0x08, 0x70, 0x5A, states, 0x00 }; //(BAT525G)LV放电电流连接电源
                ret = comm.WriteCommand(RY27);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool CNTstCurCal_RY(string Type, bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] RY11 = { 0xCF, 0xCC, 0x08, 0x70, 0x0B, states, 0x00 }; //放电电流3A：L+
                ret = comm.WriteCommand(RY11);
                if (ret < 0)
                    return false;
                byte[] RY15 = { 0xCF, 0xCC, 0x08, 0x70, 0x0F, states, 0x00 }; //放电电流3A：L-
                ret = comm.WriteCommand(RY15);
                if (ret < 0)
                    return false;
                if (Type == "负")
                {
                    byte[] RY12 = { 0xCF, 0xCC, 0x08, 0x70, 0x0C, states, 0x00 }; //放电电流3A:LS+/LS-
                    ret = comm.WriteCommand(RY12);
                    if (ret < 0)
                        return false;
                    byte[] RY26 = { 0xCF, 0xCC, 0x08, 0x70, 0x59, states, 0x00 }; //放电电流3A:LS+/LS-
                    ret = comm.WriteCommand(RY12);
                    if (ret < 0)
                        return false;
                }
                return true;
            }
            catch { return false; }
        }
        public bool MultimeterCur10A(bool en = false)//电流档切换
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] RY4 = { 0xCF, 0xCC, 0x08, 0x70, 0x04, states, 0x00 }; //sda/ps-
                ret = comm.WriteCommand(RY4);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool StCurCal_RY(string Type, bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                if (Type == "200uA" || Type == "2000uA" || Type == "1000uA")
                {
                    byte[] RY18 = { 0xCF, 0xCC, 0x08, 0x70, 0x12, states, 0x00 }; //sda/ps-
                    ret = comm.WriteCommand(RY18);
                    if (ret < 0)
                        return false;
                    byte[] RY21 = { 0xCF, 0xCC, 0x08, 0x70, 0x15, states, 0x00 }; //sda/ps-
                    ret = comm.WriteCommand(RY21);
                    if (ret < 0)
                        return false;
                }
                else if (Type == "20000nA")
                {
                    byte[] RY19 = { 0xCF, 0xCC, 0x08, 0x70, 0x13, states, 0x00 }; //sda/ps-
                    ret = comm.WriteCommand(RY19);
                    if (ret < 0)
                        return false;
                    byte[] RY22 = { 0xCF, 0xCC, 0x08, 0x70, 0x16, states, 0x00 }; //sda/ps-
                    ret = comm.WriteCommand(RY22);
                    if (ret < 0)
                        return false;
                }
                else if (Type == "2000nA" || Type == "1000nA")
                {
                    byte[] RY20 = { 0xCF, 0xCC, 0x08, 0x70, 0x14, states, 0x00 }; //sda/ps-
                    ret = comm.WriteCommand(RY20);
                    if (ret < 0)
                        return false;
                    byte[] RY23 = { 0xCF, 0xCC, 0x08, 0x70, 0x17, states, 0x00 }; //sda/ps-
                    ret = comm.WriteCommand(RY23);
                    if (ret < 0)
                        return false;
                }
                else
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool IdrSelfCheck(bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en)
                    states = 0x01;
                else
                    states = 0x00;
                byte[] RY25 = { 0xCF, 0xCC, 0x08, 0x70, 0x19, states, 0x00 }; //sda/ps-
                ret = comm.WriteCommand(RY25);
                if (ret < 0)
                    return false;
                return true;
            }
            catch { return false; }
        }
        public bool DCIRCal_RY(int Value, bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en) states = 0x01;
                else states = 0x00;
                switch (Value)
                {
                    case 0:
                        byte[] WriteByte = { 0xCF, 0xCC, 0x08, 0x70, 0x1E, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte);
                        if (ret < 0)
                            return false;
                        break;
                    case 1:
                        byte[] WriteByte0 = { 0xCF, 0xCC, 0x08, 0x70, 0x1E, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte0);
                        if (ret < 0)
                            return false;
                        break;
                    case 10:
                        byte[] WriteByte1 = { 0xCF, 0xCC, 0x08, 0x70, 0x1F, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte1);
                        if (ret < 0)
                            return false;
                        break;
                    case 30:
                        byte[] WriteByte2 = { 0xCF, 0xCC, 0x08, 0x70, 0x20, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte2);
                        if (ret < 0)
                            return false;
                        break;
                    case 50:
                        byte[] WriteByte3 = { 0xCF, 0xCC, 0x08, 0x70, 0x21, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte3);
                        if (ret < 0)
                            return false;
                        break;
                    case 100:
                        byte[] WriteByte4 = { 0xCF, 0xCC, 0x08, 0x70, 0x22, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte4);
                        if (ret < 0)
                            return false;
                        break;
                    case 150:
                        byte[] WriteByte5 = { 0xCF, 0xCC, 0x08, 0x70, 0x23, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte5);
                        if (ret < 0)
                            return false;
                        break;
                    case 200:
                        byte[] WriteByte6 = { 0xCF, 0xCC, 0x08, 0x70, 0x24, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte6);
                        if (ret < 0)
                            return false;
                        break;
                    case 300:
                        byte[] WriteByte7 = { 0xCF, 0xCC, 0x08, 0x70, 0x25, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte7);
                        if (ret < 0)
                            return false;
                        break;
                    case 400:
                        byte[] WriteByte8 = { 0xCF, 0xCC, 0x08, 0x70, 0x26, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte8);
                        if (ret < 0)
                            return false;
                        break;
                    case 500:
                        byte[] WriteByte9 = { 0xCF, 0xCC, 0x08, 0x70, 0x27, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte9);
                        if (ret < 0)
                            return false;
                        break;
                    case 600:
                        byte[] WriteByteA = { 0xCF, 0xCC, 0x08, 0x70, 0x28, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteA);
                        if (ret < 0)
                            return false;
                        break;
                    case 700:
                        byte[] WriteByteB = { 0xCF, 0xCC, 0x08, 0x70, 0x29, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteB);
                        if (ret < 0)
                            return false;
                        break;
                    case 800:
                        byte[] WriteByteC = { 0xCF, 0xCC, 0x08, 0x70, 0x2A, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteC);
                        if (ret < 0)
                            return false;
                        break;
                    case 900:
                        byte[] WriteByteD = { 0xCF, 0xCC, 0x08, 0x70, 0x2B, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteD);
                        if (ret < 0)
                            return false;
                        break;
                    case 1000:
                        byte[] WriteByteE = { 0xCF, 0xCC, 0x08, 0x70, 0x58, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteE);
                        if (ret < 0)
                            return false;
                        break;
                    default: break;

                }
                return true;
            }
            catch { return false; }

        }
        public bool IDRResCal_RY(int Value, bool en = false)
        {
            int ret = -1;
            byte states;
            try
            {
                if (en) states = 0x01;
                else states = 0x00;
                switch (Value)
                {
                    case 0:
                        byte[] WriteByte = { 0xCF, 0xCC, 0x08, 0x70, 0x2C, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte);
                        if (ret < 0)
                            return false;
                        break;
                    case 50:
                        byte[] WriteByte1 = { 0xCF, 0xCC, 0x08, 0x70, 0x2D, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte1);
                        if (ret < 0)
                            return false;
                        break;
                    case 100:
                        byte[] WriteByte2 = { 0xCF, 0xCC, 0x08, 0x70, 0x2E, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte2);
                        if (ret < 0)
                            return false;
                        break;
                    case 150:
                        byte[] WriteByte3 = { 0xCF, 0xCC, 0x08, 0x70, 0x2F, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte3);
                        if (ret < 0)
                            return false;
                        break;
                    case 200:
                        byte[] WriteByte4 = { 0xCF, 0xCC, 0x08, 0x70, 0x30, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte4);
                        if (ret < 0)
                            return false;
                        break;
                    case 250:
                        byte[] WriteByte5 = { 0xCF, 0xCC, 0x08, 0x70, 0x31, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte5);
                        if (ret < 0)
                            return false;
                        break;
                    case 300:
                        byte[] WriteByte6 = { 0xCF, 0xCC, 0x08, 0x70, 0x32, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte6);
                        if (ret < 0)
                            return false;
                        break;
                    case 400:
                        byte[] WriteByte7 = { 0xCF, 0xCC, 0x08, 0x70, 0x33, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte7);
                        if (ret < 0)
                            return false;
                        break;
                    case 500:
                        byte[] WriteByte8 = { 0xCF, 0xCC, 0x08, 0x70, 0x34, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte8);
                        if (ret < 0)
                            return false;
                        break;
                    case 600:
                        byte[] WriteByte9 = { 0xCF, 0xCC, 0x08, 0x70, 0x35, states, 0x00 };
                        ret = comm.WriteCommand(WriteByte9);
                        if (ret < 0)
                            return false;
                        break;
                    case 800:
                        byte[] WriteByteA = { 0xCF, 0xCC, 0x08, 0x70, 0x36, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteA);
                        if (ret < 0)
                            return false;
                        break;
                    case 1000:
                        byte[] WriteByteB = { 0xCF, 0xCC, 0x08, 0x70, 0x37, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteB);
                        if (ret < 0)
                            return false;
                        break;
                    case 1500:
                        byte[] WriteByteC = { 0xCF, 0xCC, 0x08, 0x70, 0x38, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteC);
                        if (ret < 0)
                            return false;
                        break;
                    case 2000:
                        byte[] WriteByteD = { 0xCF, 0xCC, 0x08, 0x70, 0x39, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteD);
                        if (ret < 0)
                            return false;
                        break;
                    case 2500:
                        byte[] WriteByteE = { 0xCF, 0xCC, 0x08, 0x70, 0x3A, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteE);
                        if (ret < 0)
                            return false;
                        break;
                    case 3000:
                        byte[] WriteByteF = { 0xCF, 0xCC, 0x08, 0x70, 0x3B, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteF);
                        if (ret < 0)
                            return false;
                        break;
                    case 4000:
                        byte[] WriteByteG = { 0xCF, 0xCC, 0x08, 0x70, 0x3C, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteG);
                        if (ret < 0)
                            return false;
                        break;
                    case 5000:
                        byte[] WriteByteH = { 0xCF, 0xCC, 0x08, 0x70, 0x3D, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteH);
                        if (ret < 0)
                            return false;
                        break;
                    case 6000:
                        byte[] WriteByteI = { 0xCF, 0xCC, 0x08, 0x70, 0x3E, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteI);
                        if (ret < 0)
                            return false;
                        break;
                    case 8000:
                        byte[] WriteByteJ = { 0xCF, 0xCC, 0x08, 0x70, 0x3F, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteJ);
                        if (ret < 0)
                            return false;
                        break;
                    case 10000:
                        byte[] WriteByteK = { 0xCF, 0xCC, 0x08, 0x70, 0x40, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteK);
                        if (ret < 0)
                            return false;
                        break;
                    case 15000:
                        byte[] WriteByteL = { 0xCF, 0xCC, 0x08, 0x70, 0x41, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteL);
                        if (ret < 0)
                            return false;
                        break;
                    case 20000:
                        byte[] WriteByteM = { 0xCF, 0xCC, 0x08, 0x70, 0x42, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteM);
                        if (ret < 0)
                            return false;
                        break;
                    case 25000:
                        byte[] WriteByteN = { 0xCF, 0xCC, 0x08, 0x70, 0x43, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteN);
                        if (ret < 0)
                            return false;
                        break;
                    case 30000:
                        byte[] WriteByteO = { 0xCF, 0xCC, 0x08, 0x70, 0x44, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteO);
                        if (ret < 0)
                            return false;
                        break;
                    case 40000:
                        byte[] WriteByteP = { 0xCF, 0xCC, 0x08, 0x70, 0x45, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteP);
                        if (ret < 0)
                            return false;
                        break;
                    case 50000:
                        byte[] WriteByteQ = { 0xCF, 0xCC, 0x08, 0x70, 0x46, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteQ);
                        if (ret < 0)
                            return false;
                        break;
                    case 60000:
                        byte[] WriteByteR = { 0xCF, 0xCC, 0x08, 0x70, 0x47, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteR);
                        if (ret < 0)
                            return false;
                        break;
                    case 80000:
                        byte[] WriteByteS = { 0xCF, 0xCC, 0x08, 0x70, 0x48, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteS);
                        if (ret < 0)
                            return false;
                        break;
                    case 100000:
                        byte[] WriteByteT = { 0xCF, 0xCC, 0x08, 0x70, 0x49, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteT);
                        if (ret < 0)
                            return false;
                        break;
                    case 150000:
                        byte[] WriteByteU = { 0xCF, 0xCC, 0x08, 0x70, 0x4A, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteU);
                        if (ret < 0)
                            return false;
                        break;
                    case 200000:
                        byte[] WriteByteV = { 0xCF, 0xCC, 0x08, 0x70, 0x4B, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteV);
                        if (ret < 0)
                            return false;
                        break;
                    case 250000:
                        byte[] WriteByteW = { 0xCF, 0xCC, 0x08, 0x70, 0x4C, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteW);
                        if (ret < 0)
                            return false;
                        break;
                    case 300000:
                        byte[] WriteByteX = { 0xCF, 0xCC, 0x08, 0x70, 0x4D, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteX);
                        if (ret < 0)
                            return false;
                        break;
                    case 400000:
                        byte[] WriteByteY = { 0xCF, 0xCC, 0x08, 0x70, 0x4E, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteY);
                        if (ret < 0)
                            return false;
                        break;
                    case 500000:
                        byte[] WriteByteZ = { 0xCF, 0xCC, 0x08, 0x70, 0x4F, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteZ);
                        if (ret < 0)
                            return false;
                        break;
                    case 600000:
                        byte[] WriteByteAA = { 0xCF, 0xCC, 0x08, 0x70, 0x50, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteAA);
                        if (ret < 0)
                            return false;
                        break;
                    case 700000:
                        byte[] WriteByteAB = { 0xCF, 0xCC, 0x08, 0x70, 0x51, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteAB);
                        if (ret < 0)
                            return false;
                        break;
                    case 800000:
                        byte[] WriteByteAC = { 0xCF, 0xCC, 0x08, 0x70, 0x52, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteAC);
                        if (ret < 0)
                            return false;
                        break;
                    case 1000000:
                        byte[] WriteByteAD = { 0xCF, 0xCC, 0x08, 0x70, 0x53, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteAD);
                        if (ret < 0)
                            return false;
                        break;
                    case 1500000:
                        byte[] WriteByteAE = { 0xCF, 0xCC, 0x08, 0x70, 0x54, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteAE);
                        if (ret < 0)
                            return false;
                        break;
                    case 2000000:
                        byte[] WriteByteAF = { 0xCF, 0xCC, 0x08, 0x70, 0x55, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteAF);
                        if (ret < 0)
                            return false;
                        break;
                    case 2500000:
                        byte[] WriteByteAG = { 0xCF, 0xCC, 0x08, 0x70, 0x56, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteAG);
                        if (ret < 0)
                            return false;
                        break;
                    case 3000000:
                        byte[] WriteByteAH = { 0xCF, 0xCC, 0x08, 0x70, 0x57, states, 0x00 };
                        ret = comm.WriteCommand(WriteByteAH);
                        if (ret < 0)
                            return false;
                        break;
                    default: break;
                }
                return true;
            }
            catch { return false; }
        }


        public bool Reset_RY()
        {
            int ret = -1;
            byte[] WriteByte = { 0xCF, 0xCC, 0x08, 0x70, 0xFE, 0x00, 0x00 };
            ret = comm.WriteCommand(WriteByte);
            if (ret < 0)
                return false;
            return true;
        }
    }
}
