using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestSystem_Pack
{
    public delegate void UpdateUidelegate(string DeviceType, string BoxNum, string CheckType, string CheckValue, string DMMValue, string TestValue, string Err1, string Err2, string Acc, string result);
    public delegate void UpdateListBoxdelegate(string item);
    public delegate void WriteExcelData(int[,] Cell, string DMMValue, string EQMValue);
    public partial class CheckItem
    {
        UpdateUidelegate UpdateUidelegate = null;
        UpdateListBoxdelegate UpdateListBoxdelegate = null;
        WriteExcelData WriteExcelData = null;
        public CheckItem(UpdateUidelegate UpdateUi, UpdateListBoxdelegate updateListBoxdelegate, WriteExcelData writeExcelData)
        {
            UpdateUidelegate = UpdateUi;
            UpdateListBoxdelegate = updateListBoxdelegate;
            WriteExcelData=writeExcelData;
        }
    }
}
