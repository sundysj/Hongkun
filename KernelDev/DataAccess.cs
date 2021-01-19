using System;
using System.Data;
using System.Data.SqlClient;

namespace KernelDev.DataAccess
{
    /// <summary>
    /// Class1 的摘要说明。
    /// </summary>
    public class DataAccess : IDisposable
    {
        private SqlDataAdapter dsCommand;

        public DataAccess()
        {
            this.dsCommand = new SqlDataAdapter();
            this.dsCommand.SelectCommand = new SqlCommand();
            this.dsCommand.SelectCommand.Connection = new SqlConnection(AppKDev.AppWebSettings("SQLContionString").ToString());
            this.dsCommand.SelectCommand.CommandTimeout = 60;
        }

        public DataAccess(string ConnectionString)
        {
            if ((ConnectionString != "") && (ConnectionString != null))
            {
                this.dsCommand = new SqlDataAdapter();
                this.dsCommand.SelectCommand = new SqlCommand();
                this.dsCommand.SelectCommand.Connection = new SqlConnection(ConnectionString);
                this.dsCommand.SelectCommand.CommandTimeout = 60;
            }
            else
            {
                throw new ArgumentNullException("ConnectionString", "参数尚未初始化，");
            }


        }
        public DataSet DataSet()
        {
            DataSet set1 = new DataSet();
            this.dsCommand.Fill(set1);
            return set1;
        }

        public DataSet DataSet(DbParamters dbParamters)
        {
            using (SqlCommand command1 = this.dsCommand.SelectCommand)
            {
                DataSet set1 = new DataSet();
                command1.CommandType = dbParamters.CommandType;
                command1.CommandText = dbParamters.CommandText;
                command1.Parameters.Clear();
                if (dbParamters.CommandType == CommandType.StoredProcedure)
                {
                    for (int num1 = 0; num1 < dbParamters.Count; num1++)
                    {
                        if (dbParamters.ProcParamters.IndexOf(dbParamters[num1].ParameterName.ToLower().Trim()) >= 0)
                        {
                            SqlParameter parameter1 = new SqlParameter(dbParamters[num1].ParameterName, dbParamters[num1].Value);
                            parameter1.DbType = dbParamters[num1].DbType;
                            parameter1.Size = dbParamters[num1].Size;
                            parameter1.Precision = dbParamters[num1].Precision;
                            parameter1.Scale = dbParamters[num1].Scale;
                            command1.Parameters.Add(parameter1);
                        }
                    }
                }
                this.dsCommand.Fill(set1);
                return set1;
            }
        }


        public DataTable DataTable()
        {
            DataTable table1 = new DataTable();
            this.dsCommand.Fill(table1);
            return table1;
        }


        public DataTable DataTable(DbParamters dbParamters)
        {
            using (SqlCommand command1 = this.dsCommand.SelectCommand)
            {
                DataTable table1 = new DataTable();
                command1.CommandType = dbParamters.CommandType;
                command1.CommandText = dbParamters.CommandText;
                command1.Parameters.Clear();
                if (dbParamters.CommandType == CommandType.StoredProcedure)
                {
                    for (int num1 = 0; num1 < dbParamters.Count; num1++)
                    {
                        if (dbParamters.ProcParamters.IndexOf(dbParamters[num1].ParameterName.ToLower().Trim()) >= 0)
                        {
                            SqlParameter parameter1 = new SqlParameter(dbParamters[num1].ParameterName, dbParamters[num1].Value);
                            parameter1.DbType = dbParamters[num1].DbType;
                            parameter1.Size = dbParamters[num1].Size;
                            parameter1.Precision = dbParamters[num1].Precision;
                            parameter1.Scale = dbParamters[num1].Scale;
                            command1.Parameters.Add(parameter1);
                        }
                    }
                }
                this.dsCommand.Fill(table1);
                return table1;
            }
        }


        #region IDisposable 成员

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);

        }

        #endregion

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.dsCommand != null))
            {
                if (this.dsCommand.SelectCommand != null)
                {
                    if (this.dsCommand.SelectCommand.Connection != null)
                    {
                        this.dsCommand.SelectCommand.Connection.Dispose();
                    }
                    this.dsCommand.SelectCommand.Dispose();
                }
                this.dsCommand.Dispose();
                this.dsCommand = null;
            }
        }
        #endregion


        public void Excute(DbParamters dbParamters)
        {
            using (SqlCommand command1 = this.dsCommand.SelectCommand)
            {
                DataTable table1 = new DataTable();
                command1.CommandType = dbParamters.CommandType;
                command1.CommandText = dbParamters.CommandText;
                command1.Parameters.Clear();
                if (dbParamters.CommandType == CommandType.StoredProcedure)
                {
                    for (int num1 = 0; num1 < dbParamters.Count; num1++)
                    {
                        if (dbParamters.ProcParamters.IndexOf(dbParamters[num1].ParameterName.ToLower().Trim()) >= 0)
                        {
                            SqlParameter parameter1 = new SqlParameter(dbParamters[num1].ParameterName, dbParamters[num1].Value);
                            parameter1.DbType = dbParamters[num1].DbType;
                            parameter1.Size = dbParamters[num1].Size;
                            parameter1.Precision = dbParamters[num1].Precision;
                            parameter1.Scale = dbParamters[num1].Scale;
                            command1.Parameters.Add(parameter1);
                        }
                    }
                }
                if (command1.Connection.State == ConnectionState.Closed)
                {
                    command1.Connection.Open();
                }
                command1.ExecuteNonQuery();
            }
        }


        public void Excute(string strSQL)
        {
            using (SqlCommand command1 = this.dsCommand.SelectCommand)
            {
                if (command1.Connection.State == ConnectionState.Closed)
                {
                    command1.Connection.Open();
                }
                command1.CommandText = strSQL;
                command1.CommandType = CommandType.Text;
                command1.ExecuteNonQuery();
            }
        }


        public void ExecuteNonQuery()
        {
            using (SqlCommand command1 = this.dsCommand.SelectCommand)
            {
                if (command1.Connection.State == ConnectionState.Closed)
                {
                    command1.Connection.Open();
                }
                command1.ExecuteNonQuery();
            }
        }


        public bool GetBoolean(string name)
        {
            return (bool)this.GetParameterValue(name);
        }


        public byte GetByte(string name)
        {
            return (byte)this.GetParameterValue(name);
        }


        public char GetChar(string name)
        {
            return (char)this.GetParameterValue(name);
        }


        protected string GetCorrectedName(string key)
        {
            if (key == null)
            {
                throw new Exception("The parameter name is null");
            }
            key = key.Trim();
            if (key.StartsWith("@"))
            {
                return key;
            }
            return ("@" + key);
        }


        public decimal GetDecimal(string name)
        {
            return (decimal)this.GetParameterValue(name);
        }


        public double GetDouble(string name)
        {
            return (double)this.GetParameterValue(name);
        }


        public float GetFloat(string name)
        {
            return (float)this.GetParameterValue(name);
        }


        public short GetInt16(string name)
        {
            return (short)this.GetParameterValue(name);
        }


        public int GetInt32(string name)
        {
            return (int)this.GetParameterValue(name);
        }


        public long GetInt64(string name)
        {
            return (long)this.GetParameterValue(name);
        }


        private SqlParameter GetNewParameter(string name, SqlDbType type, int len, ParameterDirection dir)
        {
            name = this.GetCorrectedName(name);
            if (this.dsCommand.SelectCommand.Parameters.Contains(name))
            {
                throw new Exception("The parameter " + name + " is already registered");
            }
            SqlParameter parameter1 = new SqlParameter(name, type, len);
            parameter1.Direction = dir;
            return parameter1;
        }


        protected object GetParameterValue(string name)
        {
            name = this.GetCorrectedName(name);
            return this.dsCommand.SelectCommand.Parameters[name].Value;
        }


        public string GetString(string name)
        {
            return (string)this.GetParameterValue(name);
        }


        public object GetValue(string name)
        {
            return this.GetParameterValue(name);
        }


        public void PrepareCall(DbParamters dbParamters)
        {
            this.dsCommand.SelectCommand.CommandText = dbParamters.CommandText.ToString();
            this.dsCommand.SelectCommand.CommandType = dbParamters.CommandType;
            this.dsCommand.SelectCommand.Parameters.Clear();
            if (dbParamters.CommandType == CommandType.StoredProcedure)
            {
                for (int num1 = 0; num1 < dbParamters.Count; num1++)
                {
                    if (dbParamters.ProcParamters.IndexOf(dbParamters[num1].ParameterName.ToLower().Trim()) >= 0)
                    {
                        SqlParameter parameter1 = new SqlParameter(dbParamters[num1].ParameterName, dbParamters[num1].Value);
                        parameter1.DbType = dbParamters[num1].DbType;
                        parameter1.Size = dbParamters[num1].Size;
                        parameter1.Precision = dbParamters[num1].Precision;
                        parameter1.Scale = dbParamters[num1].Scale;
                        this.dsCommand.SelectCommand.Parameters.Add(parameter1);
                    }
                }
            }
        }

        public void RegisterInOutParameter(string name, object Value, SqlDbType type, int len)
        {
            name = this.GetCorrectedName(name);
            SqlParameter parameter1 = this.GetNewParameter(name, type, len, ParameterDirection.InputOutput);
            parameter1.Value = Value;
            this.dsCommand.SelectCommand.Parameters.Add(parameter1);
            this.dsCommand.SelectCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        }



        public void RegisterOutParameter(string name, SqlDbType type, int len)
        {
            name = this.GetCorrectedName(name);
            SqlParameter parameter1 = this.GetNewParameter(name, type, len, ParameterDirection.Output);
            this.dsCommand.SelectCommand.Parameters.Add(parameter1);
            this.dsCommand.SelectCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        }


        public void RegisterReturnParameter(string name, SqlDbType type, int len)
        {
            name = this.GetCorrectedName(name);
            SqlParameter parameter1 = this.GetNewParameter(name, type, len, ParameterDirection.ReturnValue);
            this.dsCommand.SelectCommand.Parameters.Add(parameter1);
            this.dsCommand.SelectCommand.UpdatedRowSource = UpdateRowSource.OutputParameters;
        }




    }
}
