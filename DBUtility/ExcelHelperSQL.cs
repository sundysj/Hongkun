using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace MobileSoft.DBUtility
{
    public class ExcelHelperSQL
    {
        public ExcelHelperSQL()
        {

        }

        /// <summary> 
        /// 绑定EXCEL数据源
        /// </summary> 
        /// <param name="FileName">Excel物理地址</param> 
        /// <param name="Table">Excel的表名</param> 
        private static DataTable BindExcel(string FileName, string Table)//绑定EXCEL文件表
        {
            string StrSQLCommand = "SELECT * FROM ["+Table+"$]";
            string StrConn;
            System.Data.OleDb.OleDbConnection Conn;
            System.Data.OleDb.OleDbDataAdapter MyCommand;
            DataTable Dt = new DataTable();
            StrConn = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + FileName + "; Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
            Conn = new System.Data.OleDb.OleDbConnection(StrConn);
            MyCommand = new System.Data.OleDb.OleDbDataAdapter(StrSQLCommand, Conn);
            MyCommand.Fill(Dt);
            return Dt;
        }
    }
}
