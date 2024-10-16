using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Xml;

namespace TestSystem_Pack
{
    public class CheckTestData
    {
        public static object dbLock = new object();
        public string testTime;

        public string Line;
        public string TesterID;
        public string Channel;
        public string ItemName;
        public string SetValue;
        public string TesterValue;
        public string DmmValue;
        public string ErrValue;

        public int ID;

        ////////////////////////////////////

        private OleDbConnection oleDbConnetion = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source=" + Application.StartupPath + @"\CheckTestData.mdb" + ";Jet OleDb:DataBase Password=sbgc201510;Persist Security Info=False");
        //private OleDbConnection oleDbConnetion = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source=" + System.IO.Directory.GetCurrentDirectory() + @"\TestData.mdb" + ";Jet OleDb:DataBase Password=sbgc201510;Persist Security Info=False");
        private OleDbDataAdapter oleDataAdapter = new OleDbDataAdapter();
        private OleDbCommand oleCommand = new OleDbCommand();
        private DataSet dataSet = new DataSet();

        public int SaveDataToLocal()
        {
            try
            {
                string strSql = "";
                int Count;
                DateTime dtNow = DateTime.Now;
                testTime = dtNow.ToString("yyyy/MM/dd HH:mm:ss");
                strSql = ("insert into  t_TestData ( Num,Line,Tester,Channel,Item,Set_Value,Tester_measurement,DMM_measurement,Error,CheckTime)");
                strSql += ("values( " + ID + ",");
                strSql += ("'" + Line + "',");
                strSql += ("'" + TesterID + "',");
                strSql += ("'" + Channel + "',");
                strSql += ("'" + ItemName + "',");
                strSql += ("'" + SetValue + "',");
                strSql += ("'" + TesterValue + "',");
                strSql += ("'" + DmmValue + "',");
                strSql += ("'" + ErrValue + "',");
                strSql += ("'" + testTime + "')");

                oleDataAdapter.SelectCommand = new OleDbCommand();
                oleDataAdapter.SelectCommand.Connection = oleDbConnetion;
                oleDataAdapter.SelectCommand.CommandText = strSql;
                oleDataAdapter.SelectCommand.CommandType = CommandType.Text;

                lock (dbLock)
                {
                    oleDbConnetion.Open();
                    Count = oleDataAdapter.SelectCommand.ExecuteNonQuery();
                    oleDbConnetion.Close();

                }
                return Count;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                oleDbConnetion.Close();
                return -1;
            }

        }


        public int MaxProName()
        {
            int MaxProNo = 0;

            string strSQL = "select max(Num) as ProNo from t_TestData";
            string strTable = "TestSetUp1";
            DataSet dataSet = new DataSet();
            dataSet.Clear();
            dataSet = GetDataSet(strSQL, strTable);

            if (dataSet != null)
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                   if( dataSet.Tables[0].Rows[0]["ProNo"].ToString()!="")
                    MaxProNo = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ProNo"].ToString());
                }

            return MaxProNo;
        }

        public DataSet GetDataSet(string strSQL, string TableName)
        {
            try
            {
                DataSet dataSet = new DataSet();
                oleDataAdapter.SelectCommand = new OleDbCommand();
                oleDataAdapter.SelectCommand.Connection = oleDbConnetion;
                oleDataAdapter.SelectCommand.CommandText = strSQL;
                oleDataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataSet.Clear();
                oleDbConnetion.Open();
                oleDataAdapter.Fill(dataSet, TableName);
                oleDbConnetion.Close();
                return dataSet;

            }
            catch (System.Exception )
            {
                return null;
            }

        }

        public string ExeCuteSQL(string strSQL, string TableName)
        {
            try
            {
                oleDataAdapter.SelectCommand = new OleDbCommand();
                oleDataAdapter.SelectCommand.Connection = oleDbConnetion;
                oleDataAdapter.SelectCommand.CommandText = strSQL;
                oleDataAdapter.SelectCommand.CommandType = CommandType.Text;
                oleDbConnetion.Open();
                oleDataAdapter.SelectCommand.ExecuteNonQuery();
                oleDbConnetion.Close();
                return "OK";
            }
            catch (System.Exception)
            {
                return "NG";
            }

        }
    }
}

