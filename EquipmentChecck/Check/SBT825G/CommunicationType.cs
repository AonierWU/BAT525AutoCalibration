using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public class CommunicationType
    {
        private string[] strI2C;
        private string[] strHDQ;
        private string[] strSMB;
        private string[] strUse;

        protected readonly int M_L_READ_BYTE_INDEX = 0;
        protected readonly int M_L_WRITE_BYTE_INDEX = 1;
        protected readonly int M_L_READ_2BYTES_INDEX = 2;
        protected readonly int M_L_WRITE_2BYTES_INDEX = 3;
        protected readonly int M_L_READ_NBYTES_INDEX = 4;
        protected readonly int M_L_WRITE_NBYTES_INDEX = 5;
        protected readonly int M_L_WRITE_NBYTES_ROM_INDEX = 6;

        protected readonly int M_L_BQCommunicationCMD_COUNT = 7;
        private EN_BQ_COMMUNICATION_TYPE enCommnuicationType_;

        public enum EN_BQ_COMMUNICATION_TYPE
        {
            enI2C,
            enHDQ,
            enSMB,
            enNONE
        }

        public CommunicationType()
        {
            strI2C = null;
            strHDQ = null;
            strSMB = null;
            strUse = null;

            enCommnuicationType_ = EN_BQ_COMMUNICATION_TYPE.enNONE;

            InitialUseCMD();

        }

        public EN_BQ_COMMUNICATION_TYPE CommunicationType_
        {
            get { return enCommnuicationType_; }
            set
            {
                enCommnuicationType_ = value;
                if (value == EN_BQ_COMMUNICATION_TYPE.enHDQ)
                {
                    strUse = strHDQ;
                }
                else if (value == EN_BQ_COMMUNICATION_TYPE.enSMB)
                {
                    strUse = strSMB;
                }
                else
                {
                    strUse = strI2C;
                }
            }

        }
        private void InitialUseCMD()
        {
            string[] strCmdHDQ = { "50", "51", "52", "53", "54", "55", "" };
            string[] strCmdI2C = { "21", "61", "20", "60", "31", "32", "33" };
            string[] strCmdSMB = { "21", "61", "20", "60", "31", "34", "33" };

            strI2C = new string[M_L_BQCommunicationCMD_COUNT];
            strHDQ = new string[M_L_BQCommunicationCMD_COUNT];
            strSMB = new string[M_L_BQCommunicationCMD_COUNT];
            for (int i = 0; i < strCmdI2C.Length; i++)
            {
                strI2C[i] = strCmdI2C[i];
            }

            for (int i = 0; i < strCmdHDQ.Length; i++)
            {
                strHDQ[i] = strCmdHDQ[i];
            }
            for (int i = 0; i < strCmdHDQ.Length; i++)
            {
                strSMB[i] = strCmdSMB[i];
            }
            return;
        }

        public string ReadByte
        {
            get { return strUse[M_L_READ_BYTE_INDEX]; }
        }
        public string WriteByte
        {
            get { return strUse[M_L_WRITE_BYTE_INDEX]; }
        }

        //Get 2 bytes Read
        public string Read2Bytes
        {
            get { return strUse[M_L_READ_2BYTES_INDEX]; }
        }

        //Get 2 bytes Write
        public string Write2Bytes
        {
            get { return strUse[M_L_WRITE_2BYTES_INDEX]; }
        }

        //Get N bytes Read
        public string ReadNBytes
        {
            get { return strUse[M_L_READ_NBYTES_INDEX]; }
        }

        //Get N bytes Write
        public string WriteNBytes
        {
            get { return strUse[M_L_WRITE_NBYTES_INDEX]; }
        }

        //Get N bytes Write rom, srec
        public string WriteNBytesRom
        {
            get { return strUse[M_L_WRITE_NBYTES_ROM_INDEX]; }
        }
        public enum BQ_COMMNUICATION_TYPE { enI2C, enHDQ,enSMB };

        ////用函数功能实现
        //public void setType(BQ_COMMNUICATION_TYPE in_en)
        //{
        //    //enCommnuicationType_ = in_en;
        //    if (in_en == BQ_COMMNUICATION_TYPE.enHDQ)
        //    {
        //        strCMD_Use = strCMD_HDQ;
        //    }
        //    else
        //    {
        //        strCMD_Use = strCMD_I2C;
        //    }
        //}
    }
}
