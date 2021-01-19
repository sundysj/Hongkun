using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileSoft.Common
{
    /// <summary>
    /// Dapper扩展方法
    /// </summary>
    public static class DataConversion
    {
        /// <summary>
        /// 转换为DataSet
        /// </summary>
        /// <param name="reader">提供的IDataReader对象</param>
        /// <returns>返回DataSet</returns>
        public static DataSet ToDataSet(this IDataReader reader)
        {
            DataTable table = new DataTable();
            int fieldCount = reader.FieldCount;

            for (int i = 0; i < fieldCount; i++)
            {
                table.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
            }
            table.BeginLoadData();
            object[] values = new object[fieldCount];
            while (reader.Read())
            {
                reader.GetValues(values);
                table.LoadDataRow(values, true);
            }
            table.EndLoadData();
            DataSet ds = new DataSet();
            ds.Tables.Add(table);
            return ds;
        }
    }
}