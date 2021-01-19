using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Json;
using MobileSoft.Model.HSPR;
using Newtonsoft.Json;

namespace MobileSoft.Common
{
    public sealed class JSONHelper
    {
        private static string JSONPFromDataTable(string jsonCallBack, DataTable dt)
        {
            string rowDelimiter = "";
            StringBuilder result = new StringBuilder("[");
            foreach (DataRow row in dt.Rows)
            {
                result.Append(rowDelimiter);
                result.Append(FromDataRow(row));
                rowDelimiter = ",";
            }
            result.Append("]");
            string RetData = string.Format("{0}({1})", jsonCallBack, result.ToString());
            return RetData.ToString();
        }

        public static string FromDataTable(DataTable dt)
        {
            string rowDelimiter = "";
            StringBuilder result = new StringBuilder("[");
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Append(rowDelimiter);
                    result.Append(FromDataRow(row));
                    rowDelimiter = ",";
                }
            }
            result.Append("]");
            return result.ToString();
        }

        public static string FromDataTableJson(DataTable dt)
        {
            string rowDelimiter = "";
            StringBuilder result = new StringBuilder("");
            if (dt != null && dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    result.Append(rowDelimiter);
                    result.Append(FromDataRow(row));
                    rowDelimiter = ",";
                }
            }
            result.Append("");
            return result.ToString();
        }


        public static string FromDataRow(DataRow row)
        {
            DataColumnCollection cols = row.Table.Columns;
            string colDelimiter = "";
            StringBuilder result = new StringBuilder("{");
            for (int i = 0; i < cols.Count; i++)
            {
                result.Append(colDelimiter).Append("\"").Append(cols[i].ColumnName).Append("\":").Append(JSONValueFromDataRowObject(row[i], cols[i].DataType));
                colDelimiter = ",";
            }
            result.Append("}");
            return result.ToString();
        }
        // possible types:     
        // http://msdn.microsoft.com/en-us/library/system.data.datacolumn.datatype(VS.80).aspx     
        private static Type[] numeric = new Type[] {typeof(byte), typeof(decimal), typeof(double),typeof(Int16),
                                                       typeof(Int32), typeof(SByte), typeof(Single),
                                                       typeof(UInt16), typeof(UInt32), typeof(UInt64)};
        // I don't want to rebuild this value for every date cell in the table
        //private static long EpochTicks = new DateTime(1970, 1, 1).Ticks;
        private static string JSONValueFromDataRowObject(object value, Type DataType)
        {
            // null         
            if (value == DBNull.Value) return "null";
            // numeric
            if (Array.IndexOf(numeric, DataType) > -1)
                return value.ToString();
            // TODO: eventually want to use a stricter format
            // boolean
            if (DataType == typeof(bool))
                return ((bool)value) ? "true" : "false";
            // date -- see http://weblogs.asp.net/bleroy/archive/2008/01/18/dates-and-json.aspx
            if (DataType == typeof(DateTime))
                return "\"" + value.ToString() + "\"";
            //return "\"\\/Date(" + new TimeSpan(((DateTime)value).ToUniversalTime().Ticks - EpochTicks).TotalMilliseconds.ToString() + ")\\/\"";
            // TODO: add Timespan support
            // TODO: add Byte[] support
            //TODO: this would be _much_ faster with a state machine
            // string/char
            return "\"" + value.ToString().Replace(@"\", @"\\").Replace(Environment.NewLine, @"\n").Replace("\"", @"\""") + "\"";
        }

        private static string FromDataSet(DataSet ds, Boolean haveTableName)
        {
            if (ds == null || ds.Tables.Count == 0)
            {
                return "[]";
            }
            StringBuilder sb = new StringBuilder();
            if (haveTableName)
            {
                sb.Append("{");
            }
            foreach (DataTable dt in ds.Tables)
            {
                if (haveTableName)
                {
                    sb.Append("\"");
                    sb.Append(dt.TableName);
                    sb.Append("\":");
                }
                sb.Append("[");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append("{");
                        for (int i = 0; i < dr.Table.Columns.Count; i++)
                        {
                            sb.AppendFormat("\"{0}\":\"{1}\",", dr.Table.Columns[i].ColumnName.Replace("\"", "\\\"").Replace("\'", "\\\'"), ObjToStr(dr[i]).Replace("\"", "\\\"").Replace("\'", "\\\'")).Replace(Convert.ToString((char)13), "\\r\\n").Replace(Convert.ToString((char)10), "\\r\\n");
                        }
                        sb.Remove(sb.ToString().LastIndexOf(','), 1);
                        sb.Append("},");
                    }
                    sb.Remove(sb.ToString().LastIndexOf(','), 1);
                }
                sb.Append("],");
            }
            sb.Remove(sb.ToString().LastIndexOf(','), 1);
            if (haveTableName)
            {
                sb.Append("}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 传入返回的字符串
        /// 2016/03/23
        /// 敬志强
        /// </summary>
        /// <returns></returns>
        public static string FromString(Boolean state, String resultStr)
        {
            StringBuilder sb = new StringBuilder();
            if (state)
            {
                sb.Append("{\"Result\":\"true\",");
            }
            else
            {
                sb.Append("{\"Result\":\"false\",");
            }
            sb.Append("\"data\":\"");
            sb.Append(resultStr);
            sb.Append("\"}");
            return sb.ToString();
        }

        public static string FromStringTwo(Boolean state, String resultStr)
        {
            StringBuilder sb = new StringBuilder();
            if (state)
            {
                sb.Append("{\"Result\":\"true\",");
            }
            else
            {
                sb.Append("{\"Result\":\"false\",");
            }
            sb.Append("\"data\":");
            sb.Append(resultStr);
            sb.Append("}");
            return sb.ToString();
        }

        public static string FromStringThree(Boolean state, string Code, string resultStr)
        {
            StringBuilder sb = new StringBuilder();
            if (state)
            {
                sb.Append("{\"Result\":\"true\",");
            }
            else
            {
                sb.Append("{\"Result\":\"false\",");
            }
            sb.Append("\"retCode\":");
            sb.Append(Code);
            sb.Append(",\"Msg\":");
            sb.Append("\"" + resultStr + "\"");
            sb.Append("}");
            return sb.ToString();
        }

        public static string FromStringFour(Boolean state, string Code, string resultStr, string Data)
        {
            StringBuilder sb = new StringBuilder();
            if (state)
            {
                sb.Append("{\"Result\":\"true\",");
            }
            else
            {
                sb.Append("{\"Result\":\"false\",");
            }
            sb.Append("\"retCode\":");
            sb.Append(Code);
            sb.Append(",\"Msg\":");
            sb.Append("\"" + resultStr + "\"");
            sb.Append(",\"data\":");
            sb.Append("\"" + Data + "\"");
            sb.Append("}");
            return sb.ToString();
        }

        public static string FromStringHK(Boolean state, string resultStr)
        {
            StringBuilder sb = new StringBuilder();
            if (state)
            {
                sb.Append("{\"success\":true,");
            }
            else
            {
                sb.Append("{\"success\":false,");
            }
            sb.Append("\"data\":{");
            sb.Append("\"message\":");
            sb.Append("\"" + resultStr + "\"");
            sb.Append("}");
            sb.Append("}");
            return sb.ToString();
        }

        public static string FromStringZL(Boolean state, string resultStr,string IncidentID)
        {
            StringBuilder sb = new StringBuilder();
            if (state)
            {
                sb.Append("{\"success\":true,");
            }
            else
            {
                sb.Append("{\"success\":false,");
            }
            sb.Append("\"data\":{");
            sb.Append("\"incidentid\":");
            sb.Append("\"" + resultStr + "\"");
            sb.Append("}");
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 返回JSON字符串[单值]
        /// 碧桂园UDP使用可通用
        /// </summary>
        /// <param name="code">返回代码</param>
        /// <param name="desc">错误信息</param>
        /// <param name="resultStr">单值</param>
        /// <returns></returns>
        public static string FromStringBGY(string code, string desc, string resultStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":\"" + code + "\",");
            sb.Append("\"desc\":\"" + desc + "\",");
            sb.Append("\"data\":\"");
            sb.Append(resultStr);
            sb.Append("\"}");
            return sb.ToString();
        }


        /// <summary>
        /// 返回JSON字符串[单行]
        /// 碧桂园UDP使用可通用
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        /// <param name="row">单行</param>
        /// <returns></returns>
        public static string FromStringBGY(string code, string desc, DataRow row)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":\"" + code + "\",");
            sb.Append("\"desc\":\"" + desc + "\",");
            sb.Append("\"data\":");
            sb.Append(FromDataRow(row));
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 返回JSON字符串[单表]
        /// 碧桂园UDP使用可通用
        /// </summary>
        /// <param name="code"></param>
        /// <param name="desc"></param>
        /// <param name="dTable">单表</param>
        /// <returns></returns>
        public static string FromStringBGY(string code, string desc, DataTable dTable)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"code\":\"" + code + "\",");
            sb.Append("\"desc\":\"" + desc + "\",");
            sb.Append("\"data\":");
            sb.Append(FromDataTable(dTable));
            sb.Append("}");
            return sb.ToString();
        }



        /// <summary>
        /// 传入DataRow
        /// 2016/03/23
        /// 敬志强
        /// </summary>
        /// <returns></returns>
        public static string FromString(DataRow row)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",");
            sb.Append("\"data\":");
            sb.Append(FromDataRow(row));
            sb.Append("}");
            return sb.ToString();
        }
        /// <summary>
        /// 传入DataTable
        /// 2016/03/23
        /// 敬志强
        /// </summary>
        /// <returns></returns>
        public static string FromString(DataTable dTable)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",");
            sb.Append("\"data\":");
            sb.Append(FromDataTable(dTable));
            sb.Append("}");
            return sb.ToString();
        }
        public static string FromString_XXW(DataTable dTable, string Code, string Data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",");
            sb.Append("\"retCode\":");
            sb.Append(Code);
            sb.Append(",\"Msg\":");
            sb.Append("\"" + Data + "\"");
            sb.Append(",\"data\":");
            sb.Append("" + FromDataTable(dTable) + "");
            sb.Append("}");
            return sb.ToString();
        }

        public static string FromString_WSB(DataRow dr,bool bl, string Code, string msg)
        {
            StringBuilder sb = new StringBuilder();
            if (bl)
            {
                sb.Append("{\"Result\":\"true\",");
            }
            else
            {
                sb.Append("{\"Result\":\"false\",");
            }
                   
            sb.Append("\"data\":{");

            sb.Append("\"version\":\"v1\"");
            sb.Append(",\"returnCode\":\""+Code+"\"");
            sb.Append(",\"returnMsg\":\""+msg+"\"");
            if (bl&&dr!=null)
            {
                sb.Append("," + FromDataRow_SSB(dr) + "");
            }
            
            sb.Append("}}");
            return sb.ToString();
        }





        public static string FromDataRow_SSB(DataRow row)
        {
            DataColumnCollection cols = row.Table.Columns;
            string colDelimiter = "";
            StringBuilder result = new StringBuilder("");
            for (int i = 0; i < cols.Count; i++)
            {
                result.Append(colDelimiter).Append("\"").Append(cols[i].ColumnName).Append("\":").Append(JSONValueFromDataRowObject(row[i], cols[i].DataType));
                colDelimiter = ",";
            }
            result.Append("");
            return result.ToString();
        }


        public static string FromString(Boolean state, DataTable dTable)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"" + (state ? "true" : "false") + "\",");
            sb.Append("\"data\":");
            sb.Append(FromDataTable(dTable));
            sb.Append("}");
            return sb.ToString();
        }




        /// <summary>
        /// 传入DataTable并返回总页数和总条数
        /// 2016/03/23
        /// 敬志强
        /// </summary>
        /// <returns></returns>
        public static string FromString(DataTable dTable, int pageSize, int allCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",");
            sb.Append("\"data\":");
            sb.Append(FromDataTable(dTable));
            sb.Append(",");
            sb.Append("\"pageSize\":");
            sb.Append(pageSize);
            sb.Append(",");
            sb.Append("\"allCount\":");
            sb.Append(allCount);
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 传入DataTable
        /// 2016/03/23
        /// 敬志强
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="haveTableName">是否为里边的表加名字</param>
        /// <param name="havePrefix">是否加前缀{"Result":true,"data":}</param>
        /// <returns></returns>
        public static string FromString(DataTable dt, Boolean haveTableName, Boolean havePrefix)
        {
            StringBuilder sb = new StringBuilder();
            if (havePrefix)
            {
                sb.Append("{\"Result\":\"true\",");
                sb.Append("\"data\":");
            }
            sb.Append(FromDataTable(dt));
            if (havePrefix)
            {
                sb.Append("}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 传入DataTable
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="haveTableName">是否为里边的表加名字</param>
        /// <param name="havePrefix">是否加前缀{"Result":true,"data":}</param>
        /// <param name="dt">PageCount</param>
        /// <param name="dt">Counts</param>
        /// <returns></returns>
        public static string FromStringPaging(DataTable dt, Boolean haveTableName, Boolean havePrefix, int pageCount, int counts)
        {
            StringBuilder sb = new StringBuilder();
            if (havePrefix)
            {
                //,\"counts:" + counts + "\",
                sb.Append("{\"Result\":\"true\",\"pageCount\":\"" + pageCount + "\",\"counts\":\"" + counts + "\",");
                sb.Append("\"data\":");
            }
            sb.Append(FromDataTable(dt));
            if (havePrefix)
            {
                sb.Append("}");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 传入DataSet
        /// 2016/03/23
        /// 敬志强
        /// </summary>
        /// <param name="ds">DataSet</param>
        /// <param name="haveTableName">是否为里边的表加名字</param>
        /// <returns></returns>
        public static string FromString(DataSet ds, Boolean haveTableName, Boolean havePrefix)
        {
            StringBuilder sb = new StringBuilder();
            if (havePrefix)
            {
                sb.Append("{\"Result\":\"true\",");
                sb.Append("\"data\":");
            }
            sb.Append(FromDataSet(ds, haveTableName));
            if (havePrefix)
            {
                sb.Append("}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 传入JSON
        /// 2016/03/23
        /// 敬志强
        /// </summary>
        /// <returns></returns>
        public static string FromJsonString(Boolean State, string Json)
        {
            return FromJsonString(State, Json, true);
        }

        public static string FromJsonString(Boolean State, string Json, bool isArray)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"Result\":\"true\",");
            sb.Append("\"data\":");
            if (isArray)
                sb.Append("[");
            sb.Append(Json);

            if (isArray)
                sb.Append("]");
            sb.Append("}");
            return sb.ToString();
        }


        public static string ObjToStr(object ob)
        {
            if (ob == null)
            {
                return string.Empty;
            }
            else
                return ob.ToString();


        }

        public static DataTable JsonToDataTableThree(string strJson)
        {
            try
            {


                //转换json格式
                strJson = strJson.Replace(",\"", "◆\"").Replace("\":", "\"#").ToString();
                //取出表名   
                var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
                string strName = rg.Match(strJson).Value;
                DataTable tb = null;
                //去除表名
                if (strJson.Substring(0, 1) == "[")
                {
                    strJson = strJson.Substring(1, strJson.Length - 2);
                }
                //strJson = strJson.Substring(strJson.IndexOf("[") + 1);
                //strJson = strJson.Substring(0, strJson.IndexOf("]"));
                //获取数据   
                rg = new Regex(@"(?<={)[^}]+(?=})");
                MatchCollection mc = rg.Matches(strJson);
                for (int i = 0; i < mc.Count; i++)
                {
                    string strRow = mc[i].Value;
                    string[] strRows = strRow.Split('◆');
                    //创建表   
                    if (tb == null)
                    {
                        tb = new DataTable();
                        tb.TableName = strName;

                        DataColumn col = new DataColumn();
                        col.ColumnName = "id";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "order";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "no";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "name";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "parent";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "hierarchyId";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "type";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "lunid";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "alterTime";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "isAvailable";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "memo";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "thisLeader";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "superLeader";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "posts";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "loginName";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "password";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "mobileNo";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "email";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "attendanceCardNumber";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "workPhone";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "rtx";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "birthdate";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "national_id_type";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "national_id";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "hps_ad_userid";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "bank_cd";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "bank_nm";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "branch_ec_cd";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "hps_branch_ec_desc";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "hps_bank_nbr";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "account_ec_id";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "start_dt_chn";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "sex";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "wechat";
                        tb.Columns.Add(col);

                        tb.AcceptChanges();
                    }
                    //增加内容   
                    DataRow dr = tb.NewRow();
                    string posts = "";
                    for (int r = 0; r < strRows.Length; r++)
                    {
                        // \"id\"#\"154e7674efafc26d8600ca9493290a35\"
                        if (strRows[r].IndexOf("posts") < 0 && strRows[r].IndexOf("#") > -1)
                        {
                            dr[strRows[r].Split('#')[0].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "")] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                        }
                        else
                        {
                            if (strRows[r].IndexOf("#") > -1)
                            {
                                posts += strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "").Replace("[", "").Replace("]", "") + ",";
                            }
                            else
                            {
                                posts += strRows[r].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "").Replace("]", "") + ",";
                            }
                        }
                    }
                    dr["posts"] = posts;

                    tb.Rows.Add(dr);
                    tb.AcceptChanges();
                }
                return tb;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static DataTable JsonToDataTableTwo(string strJson)
        {
            try
            {


                //转换json格式
                strJson = strJson.Replace(",\"", "◆\"").Replace("\":", "\"#").ToString();
                //取出表名   
                var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
                string strName = rg.Match(strJson).Value;
                DataTable tb = null;
                //去除表名
                if (strJson.Substring(0, 1) == "[")
                {
                    strJson = strJson.Substring(1, strJson.Length - 2);
                }
                //strJson = strJson.Substring(strJson.IndexOf("[") + 1);
                //strJson = strJson.Substring(0, strJson.IndexOf("]"));
                //获取数据   
                rg = new Regex(@"(?<={)[^}]+(?=})");
                MatchCollection mc = rg.Matches(strJson);
                for (int i = 0; i < mc.Count; i++)
                {
                    string strRow = mc[i].Value;
                    string[] strRows = strRow.Split('◆');
                    //创建表   
                    if (tb == null)
                    {
                        tb = new DataTable();
                        tb.TableName = strName;

                        DataColumn col = new DataColumn();
                        col.ColumnName = "id";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "order";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "no";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "name";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "parent";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "hierarchyId";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "type";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "lunid";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "alterTime";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "isAvailable";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "memo";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "thisLeader";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "superLeader";
                        tb.Columns.Add(col);

                        col = new DataColumn();
                        col.ColumnName = "persons";
                        tb.Columns.Add(col);

                        tb.AcceptChanges();
                    }
                    //增加内容   
                    DataRow dr = tb.NewRow();
                    string persons = "";
                    for (int r = 0; r < strRows.Length; r++)
                    {
                        // \"id\"#\"154e7674efafc26d8600ca9493290a35\"
                        if (strRows[r].IndexOf("persons") < 0 && strRows[r].IndexOf("#") > -1)
                        {
                            dr[strRows[r].Split('#')[0].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "")] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                        }
                        else
                        {
                            if (strRows[r].IndexOf("#") > -1)
                            {
                                persons += strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "").Replace("[", "").Replace("]", "") + ",";
                            }
                            else
                            {
                                persons += strRows[r].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "").Replace("]", "") + ",";
                            }
                        }
                    }
                    dr["persons"] = persons;

                    tb.Rows.Add(dr);
                    tb.AcceptChanges();
                }
                return tb;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static DataTable JsonToDataTable(string strJson)
        {
            try
            {


                //转换json格式
                strJson = strJson.Replace(",\"", "◆\"").Replace("\":", "\"#").ToString();
                //取出表名   
                var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
                string strName = rg.Match(strJson).Value;
                DataTable tb = null;
                //去除表名
                if (strJson.Substring(0, 1) == "[")
                {
                    strJson = strJson.Substring(1, strJson.Length - 2);
                }
                //strJson = strJson.Substring(strJson.IndexOf("[") + 1);
                //strJson = strJson.Substring(0, strJson.IndexOf("]"));
                //获取数据   
                rg = new Regex(@"(?<={)[^}]+(?=})");
                MatchCollection mc = rg.Matches(strJson);
                for (int i = 0; i < mc.Count; i++)
                {
                    string strRow = mc[i].Value;
                    string[] strRows = strRow.Split('◆');
                    //创建表   
                    if (tb == null)
                    {
                        tb = new DataTable();
                        tb.TableName = strName;
                        foreach (string str in strRows)
                        {
                            var dc = new DataColumn();
                            string[] strCell = str.Split('#');
                            if (strCell[0].Substring(0, 1) == "\"")
                            {
                                int a = strCell[0].Length;
                                dc.ColumnName = strCell[0].Substring(1, a - 2);
                            }
                            else
                            {
                                dc.ColumnName = strCell[0];
                            }
                            tb.Columns.Add(dc);
                        }
                        tb.AcceptChanges();
                    }
                    //增加内容   
                    DataRow dr = tb.NewRow();
                    for (int r = 0; r < strRows.Length; r++)
                    {
                        dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                    }
                    tb.Rows.Add(dr);
                    tb.AcceptChanges();
                }
                return tb;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 对象序列号成json
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string FromString<T>(List<T> jsonObject)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                //将对象序列化为json数据
                string json = jss.Serialize(jsonObject);
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"Result\":\"true\",");
                sb.Append("\"data\":");
                sb.Append(json);
                sb.Append("}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return FromString(false, ex.Message);
            }
        }

        /// <summary>
        /// 对像序列化json 针对日期处理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string FromStringToDate<T>(List<T> jsonObject)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                //将对象序列化为json数据
                //string json = jss.Serialize(jsonObject);
                string json = ConvertObjToJson(jsonObject);
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"Result\":\"true\",");
                sb.Append("\"data\":");
                sb.Append(json);
                sb.Append("}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return FromString(false, ex.Message);
            }
        }

        public static string ConvertObjToJson<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(t.GetType());
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    ser.WriteObject(ms, t);
                //    string strJson = Encoding.UTF8.GetString(ms.ToArray());
                //    string json = jss.Serialize(t);
                //    //替换Json的date字符串
                //    string p = @"\\/Date\((\d+)\+\d+\)\\/";
                //    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDataToDateString);

                //    Regex reg = new Regex(p);
                //    strJson = reg.Replace(json, matchEvaluator);
                //    return strJson;
                //}

                string json = jss.Serialize(t);
                // 替换Json的date字符串
                string p = @"\\/Date\((\d+)\+\d+\)\\/";
                MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDataToDateString);
                Regex reg = new Regex(p);
                json = reg.Replace(json, matchEvaluator);
                return json;

            }
            catch (IOException)
            {
                //自己处理异常吧
                return "";
            }
        }


        private static string ConvertJsonDataToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();//转换为本地时间
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;

        }

        /// <summary>
        /// 对象序列号成json
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string FromString(object jsonObject)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                //将对象序列化为json数据
                string json = jss.Serialize(jsonObject);
                StringBuilder sb = new StringBuilder();
                sb.Append("{\"Result\":\"true\",");
                sb.Append("\"data\":[");
                sb.Append(json);
                sb.Append("]}");
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return FromString(false, ex.Message);
            }
        }
        public static string FromObject(object jsonObject)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                //将对象序列化为json数据
                return jss.Serialize(jsonObject);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 把对象序列化 JSON 字符串 
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实体</param>
        /// <returns>JSON字符串</returns>
        public static string GetJson<T>(T obj)
        {
            //记住 添加引用 System.ServiceModel.Web 
            /**
             * 如果不添加上面的引用,System.Runtime.Serialization.Json; Json是出不来的哦
             * */
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                json.WriteObject(ms, obj);
                string szJson = Encoding.UTF8.GetString(ms.ToArray());
                return szJson;
            }
        }
        /// <summary>
        /// 把JSON字符串还原为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="szJson">JSON字符串</param>
        /// <returns>对象实体</returns>
        public static T ParseFormJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
            {
                DataContractJsonSerializer dcj = new DataContractJsonSerializer(typeof(T));
                return (T)dcj.ReadObject(ms);
            }
        }

        public static string JsonConvertBGY(string code, string desc, object obj)
        {
            var ob = new
            {
                code,
                desc,
                data = obj
            };

            return JsonConvert.SerializeObject(ob);
        }

        public static string JsonConvertFHH(bool result, string ret, object obj)
        {
            var ob = new
            {
                Result = result ? "true" : "false",
                Ret = ret,
                data = obj
            };

            return JsonConvert.SerializeObject(ob);
        }

        /// <summary>
        /// 将json字符串反序列化为字典类型
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>字典数据</returns>
        public static Dictionary<TKey, TValue> StringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);

            return jsonDict;

        }
    }
}
