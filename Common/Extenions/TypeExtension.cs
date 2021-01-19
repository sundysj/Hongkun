using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extenions
{
    /// <summary>
    /// 类型<see cref="Type"/>扩展类
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 判断类型是否为Nullable类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type)
        {
            return ((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// 获取Nullable类型的基础类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns> </returns>
        public static Type GetUnNullableType(this Type type)
        {
            if (!IsNullableType(type)) return type;
            var nullableConverter = new NullableConverter(type);
            return nullableConverter.UnderlyingType;
        }

        /// <summary>
        /// 从类型成员获取指定Attribute特性
        /// </summary>
        /// <typeparam name="T">Attribute特性类型</typeparam>
        /// <param name="memberInfo">类型类型成员</param>
        /// <param name="inherit">是否从继承中查找</param>
        /// <returns>存在返回第一个，不存在返回null</returns>
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            var descripts = memberInfo.GetCustomAttributes(typeof(T), inherit);
            return descripts.FirstOrDefault() as T;
        }

        /// <summary>
        /// 根据实体类生成INSERT的SQL语句
        /// </summary>
        /// <param name="tableNamePrefix">表名前缀</param>
        /// <param name="tableName">表名(此值为空时获取当前类名称作为表名，注意当此参数有值时表前缀无效)</param>
        /// <param name="nonReflectiveProperties">不进行反射的属性名</param>
        /// <param name="IsParameterization">是否生成参数化SQL，默认是</param>
        /// <returns></returns>
        public static string GetInsSql<T>(this T t, string tableNamePrefix = null, string tableName = null, string[] nonReflectiveProperties = null, bool IsParameterization = true)
        {
            tableNamePrefix = tableNamePrefix ?? string.Empty;
            tableName = tableName.IsNotNullOrEmpty() ? tableName : tableNamePrefix + t.GetType().Name;
            if (t == null) { return null; }
            List<PropertyInfo> properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            if (properties.Count <= 0) { return null; }
            string columns = string.Empty;
            string values = string.Empty;

            properties.ForEach(o =>
            {
                if (!nonReflectiveProperties.IsValuable() || !nonReflectiveProperties.ToList().Contains(o.Name))
                {
                    columns += $"{o.Name}, ";
                    values += IsParameterization ? $"@{o.Name}, " : $"'{o.GetValue(t, null)}', ";
                }
            });
            columns = columns.Trim().TrimEnd(',');
            values = values.Trim().TrimEnd(',');

            string insSql = $"INSERT INTO dbo.{tableName} ({columns}) VALUES ({values});";
            return insSql;
        }

        /// <summary>
        /// 根据实体类生成Update的SQL语句
        /// </summary>
        /// <param name="tableNamePrefix">主键列名</param>
        /// <param name="tableName">表名(此值为空时获取当前类名称作为表名，注意当此参数有值时表前缀无效)</param>
        /// <param name="nonReflectiveProperties">不进行反射的属性名</param>
        /// <returns></returns>
        public static string GetUpdSql<T>(this T t, string primaryKey, string tableName = null, string[] nonReflectiveProperties = null)
        {
            tableName = tableName.IsNotNullOrEmpty() ? tableName : t.GetType().Name;
            if (t == null) { return null; }
            List<PropertyInfo> properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            if (properties.Count <= 0) { return null; }
            string setFilds = string.Empty;

            properties.ForEach(o =>
            {
                if ((!nonReflectiveProperties.IsValuable() || !nonReflectiveProperties.ToList().Contains(o.Name)) && o.Name != primaryKey)
                {
                    setFilds += $"{o.Name} = @{o.Name}, ";
                }
            });

            string insSql = $"UPDATE {tableName} SET {setFilds.Trim().TrimEnd(',')} WHERE {primaryKey} = @{primaryKey};";
            return insSql;
        }
    }
}
