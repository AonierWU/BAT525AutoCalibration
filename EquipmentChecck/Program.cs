using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TestSystem_Pack
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string p = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(p);
            if (process.Length > 1)
            {
                MessageBox.Show("程序已经在运行", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                Application.Run(new frmVerifyDevice());
            }

        }
    }
}
