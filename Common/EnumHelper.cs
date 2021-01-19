using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public static class EnumHelper
    {
        /// <summary>
        /// 根据值得到中文备注
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetEnumDesc(Type e, int? value)
        {
            FieldInfo[] fields = e.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                if ((int)System.Enum.Parse(e, fields[i].Name) == value)
                {
                    DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])fields[i].
                GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (EnumAttributes.Length > 0)
                    {
                        return EnumAttributes[0].Description;
                    }
                }
            }
            return "";
        }


        /// <summary>
        /// 根据值得到中文备注
        /// </summary>
        /// <param name="e"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String GetEnumByDesc(Type e, String descriptionStr)
        {
            FieldInfo[] fields = e.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])fields[i].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (EnumAttributes.Length > 0)
                {
                    var de = EnumAttributes[0].Description;
                    if (descriptionStr == de)
                    {
                        return fields[i].Name;
                    }
                }
            }
            return "";
        }

        public static int GetEnumValueByDesc(Type e, String descriptionStr)
        {
            foreach (int value in System.Enum.GetValues(e))
            {
                var str = GetEnumDesc(e, value);
                if (str == descriptionStr)
                {
                    return value;
                }
            }
            return 0;
        }
    }
}
