using System;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace MobileSoft.DBUtility
{
    public class AccessHelperSQL
    {
        public static OleDbConnection Connect()
        {
            string ConnString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=sys.mdb";
            OleDbConnection Conn = new OleDbConnection(ConnString.ToString());
            Conn.Open();
            return Conn;
        }

        public static int Execute(string Sql)
        {
            OleDbConnection Conn = Connect();
            OleDbCommand Command = new OleDbCommand(Sql.ToString(), Conn);
            int ret = Command.ExecuteNonQuery();
            Conn.Close();
            return ret;
        }

        public static OleDbDataReader SqlReader(string Sql)
        {
            OleDbConnection Conn = Connect();
            OleDbCommand Command = new OleDbCommand(Sql.ToString(), Conn);
            return Command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static DataTable SqlAdapter(string Sql)
        {
            OleDbConnection Conn = Connect();
            OleDbDataAdapter Cmd = new OleDbDataAdapter(Sql.ToString(), Conn);
            DataTable DtTable = new DataTable();
            Cmd.Fill(DtTable);
            Conn.Close();
            return DtTable;
        }

        public static string GetValue(string Sql)
        {
            string RetString = "";
            OleDbDataReader Reader = SqlReader(Sql);
            if (Reader.Read())
            {
                RetString = Reader[0].ToString();
            }
            Reader.Close();

            return RetString;
        }
    }
}
