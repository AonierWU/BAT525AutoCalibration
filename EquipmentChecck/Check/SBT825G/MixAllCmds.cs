using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public class MixAllCmds
    {

        private bool blMixcmdsSend = true;
        /// <summary>
		/// 是否使用复合指令发送
		/// </summary>
        public bool BlMixCmdsSend
        {
            get { return blMixcmdsSend; }
            set { blMixcmdsSend = value; }
        }


        private List<string> arrayMixCmds = new List<string>();
        /// <summary>
        /// 复合的指令
        /// </summary>
        public List<string> ArrayMixCmds
        {
            get { return arrayMixCmds; }
            set { arrayMixCmds = value; }
        }


        public void GetCmdToMixArrray(string strCmds)
        {
            string[] arr = strCmds.Split('-');
            arrayMixCmds.Add((arr.Length - 4 + 1).ToString("X2"));

            for (int i = 0; i < arr.Length - 4; i++)
            {
                arrayMixCmds.Add(arr[i + 3]);
            }
        }

    }
}
