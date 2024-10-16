using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{

    /// <summary>
    /// 设备通信类型RS232,TcpIp
    /// </summary>
    public enum CommType
    {
        RS232,
        TcpIp
    }

    /// <summary>
    /// 通信异常代码
    /// </summary>
    public enum CommErrors
    {
        COMM_ERROR = -1001,
        COMM_NOT_OPEN_ERROR = -1002,
        COMM_OPEN_ERROR = -1003,
        COMM_CLOSE_ERROR = -1004,
        COMM_READ_ERROR = -1005,
        COMM_WRITE_ERROR = -1006,
        COMM_READ_TIMEOUT_ERROR = -1007,
        COMM_INIT_COMM_ERROR = -1008,
        CMD_WRITE_ERROR = -1009,
        CMD_CHECKSUM_ERROR = -1010,
        CMD_READ_DATA_ERROR = -1011,
        CMD_READ_DATA_LEN_ERROR = -1012,
        CMD_PACK_BACK_ERROR = -1013,
        CMD_OVER_TEMP_ERROR = -1014,
        CMD_OUTSIDE_TEMP_ERROR = -1015,
        CMD_POWER_FAIL_ERROR = -1016,
        CMD_MCU_NO_DATA_ERROR = -1017,
        COMM_READ_OVER_COUNT_ERROR = -1018,
        CMD_BQINFOR_ERROR = -1019,
        FORMAT_ERROR = -2000
    }
    public abstract class MyComm
    {
        public const int CONNECTTIMEOUT = 500;//连接超时
        public const int WRITETIMEOUT = 1000;//写超时
        public const int READTIMEOUT = 100;//读超时

        public const int INTERVALTIMEOUT = 10;//步进查询数据间隔
        public const int LOOPCOUNT = 500;//循环次数

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
        public abstract bool Connect();
        public abstract bool Disconnect();
        public abstract int WriteRead(byte[] writeBuffer, out byte[] readBuffer, Int32 WaitT = 5000);
        public abstract int WriteReadNew(byte[] writeBuffer, out byte[] readBuffer, int WaitT = 5000);

        public abstract int WriteCommand(byte[] cmd);
        public abstract int WriteReadCommand(byte[] write, out byte[] read);

        public abstract bool DiscardInBuffer();

        public abstract int WriteSNCode(byte[] writeBuffer, int WaitT = 5000);
    }


}
