using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace KernelDev.DataAccess
{
    /// <summary>
    /// DbParamters 的摘要说明。
    /// </summary>
    public class DbParamters : CollectionBase
    {
        private CommandType mCommandType;
        private string mstrCommandText;
        private string mstrProcParamters;
        private SqlParameter ParamterItem;

        public DbParamters()
        {
            this.mstrCommandText = "";
            this.ParamterItem = null;
            this.mstrProcParamters = "";
            this.mCommandType = CommandType.Text;
            this.mstrCommandText = "";

        }
        public DbParamters(string strCommandText)
        {
            this.mstrCommandText = "";
            this.ParamterItem = null;
            this.mstrProcParamters = "";
            this.mCommandType = CommandType.Text;
            this.mstrCommandText = strCommandText;
        }


        public DbParamters(string strCommandText, CommandType cmdType)
        {
            this.mstrCommandText = "";
            this.ParamterItem = null;
            this.mstrProcParamters = "";
            this.mCommandType = cmdType;
            this.mstrCommandText = strCommandText;
        }

        public string CommandText
        {
            get
            {
                return this.mstrCommandText;
            }
            set
            {
                this.mstrCommandText = value;
            }
        }

        public CommandType CommandType
        {
            get
            {
                return this.mCommandType;
            }
            set
            {
                this.mCommandType = value;
            }
        }

        public SqlParameter this[int index]
        {
            get
            {
                return (SqlParameter)base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }

        public SqlParameter this[string ParamterName]
        {
            get
            {
                if ((this.ParamterItem == null) || (this.ParamterItem.ParameterName != ParamterName))
                {
                    this.ParamterItem = null;
                    for (int num1 = 0; num1 < base.Count; num1++)
                    {
                        if (this[num1].ParameterName == ParamterName)
                        {
                            this.ParamterItem = (SqlParameter)base.List[num1];
                            break;
                        }
                    }
                }
                return this.ParamterItem;
            }
            set
            {
                for (int num1 = 0; num1 < base.Count; num1++)
                {
                    if (this[num1].ParameterName == ParamterName)
                    {
                        base.List[num1] = value;
                        return;
                    }
                }
            }
        }

        public string ProcParamters
        {
            get
            {
                return this.mstrProcParamters;
            }
            set
            {
                this.mstrProcParamters = value.ToLower();
            }
        }


        public void Add(SqlParameter Paramter)
        {
            base.List.Add(Paramter);
        }


        public void Add(string ParamterName, object ParamterValue)
        {
            ParamterName = this.GetCorrectedName(ParamterName);
            SqlParameter parameter1 = null;
            if ((ParamterValue == null) || (ParamterValue.ToString() == ""))
            {
                parameter1 = new SqlParameter(ParamterName, DBNull.Value);
            }
            else
            {
                parameter1 = new SqlParameter(ParamterName, ParamterValue);
            }
            base.List.Add(parameter1);
        }

        public void Add(string ParamterName, object ParamterValue, SqlDbType sqlDbType)
        {
            ParamterName = this.GetCorrectedName(ParamterName);
            SqlParameter parameter1 = null;
            if ((ParamterValue == null) || (ParamterValue.ToString() == ""))
            {
                parameter1 = new SqlParameter(ParamterName, DBNull.Value);
            }
            else
            {
                try
                {
                    switch (sqlDbType)
                    {
                        case SqlDbType.BigInt:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToInt64(ParamterValue));
                            goto Label_017A;

                        case SqlDbType.DateTime:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToDateTime(ParamterValue));
                            goto Label_017A;

                        case SqlDbType.Decimal:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToDecimal(ParamterValue));
                            goto Label_017A;

                        case SqlDbType.Float:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToDouble(ParamterValue));
                            goto Label_017A;

                        case SqlDbType.Int:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToInt32(ParamterValue));
                            goto Label_017A;

                        case SqlDbType.Money:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToDecimal(ParamterValue));
                            goto Label_017A;

                        case SqlDbType.Real:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToSingle(ParamterValue));
                            goto Label_017A;

                        case SqlDbType.SmallInt:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToInt16(ParamterValue));
                            goto Label_017A;

                        case SqlDbType.SmallMoney:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToDecimal(ParamterValue));
                            goto Label_017A;

                        case SqlDbType.TinyInt:
                            parameter1 = new SqlParameter(ParamterName, Convert.ToByte(ParamterValue));
                            goto Label_017A;
                    }
                    parameter1 = new SqlParameter(ParamterName, ParamterValue);
                }
                catch (Exception exception1)
                {
                    throw new Exception("\u9519\u8bef\u7684\u8f93\u5165,\u8bf7\u8f93\u5165\u6b63\u786e\u7684\u6570\u636e...", exception1.InnerException);
                }
            }
            Label_017A:
            base.List.Add(parameter1);
        }


        public void Add(string ParamterName, object ParamterValue, int size)
        {
            ParamterName = this.GetCorrectedName(ParamterName);
            SqlParameter parameter1 = null;
            if ((ParamterValue == null) || (ParamterValue.ToString() == ""))
            {
                parameter1 = new SqlParameter(ParamterName, DBNull.Value);
            }
            else
            {
                parameter1 = new SqlParameter(ParamterName, ParamterValue);
            }
            parameter1.Size = size;
            base.List.Add(parameter1);
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


        public void Remove(int index)
        {
            if ((index > -1) && (index < base.Count))
            {
                base.List.RemoveAt(index);
            }
        }


        public void Remove(string ParamterName)
        {
            for (int num1 = 0; num1 < base.Count; num1++)
            {
                if (this[num1].ParameterName == ParamterName)
                {
                    base.List.RemoveAt(num1);
                    return;
                }
            }
        }



    }
}
