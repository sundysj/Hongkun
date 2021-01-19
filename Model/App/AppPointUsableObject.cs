using System;

namespace App.Model
{
    /// <summary>
    /// 积分对象
    /// </summary>
    [Serializable]
    public enum AppPointUsableObject
    {
        /// <summary>
        /// 物业费
        /// </summary>
		PropertyFee = 1,

        /// <summary>
        /// 车位费
        /// </summary>
        ParkingFee = 2,

        /// <summary>
        /// 其他代收费用
        /// </summary>
        OtherFee = 3,

        /// <summary>
        /// 商品
        /// </summary>
        Goods = 11,
    }

    /// <summary>
    /// 积分对象转换器
    /// </summary>
    public static class AppPointUsableObjectConverter
    {
        /// <summary>
        /// 转换为<see cref="AppPointUsableObject">枚举
        /// </summary>
        public static AppPointUsableObject Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppPointUsableObject.PropertyFee;
                case "0002":
                    return AppPointUsableObject.ParkingFee;
                case "0003":
                    return AppPointUsableObject.OtherFee;
                case "0011":
                    return AppPointUsableObject.Goods;
                default:
                    throw new ArgumentException("不存在该积分对象");
            }
        }

        /// <summary>
        /// 获取key
        /// </summary>
        public static string GetKey(AppPointUsableObject @enum)
        {
            switch (@enum)
            {
                case AppPointUsableObject.PropertyFee:
                    return "0001";
                case AppPointUsableObject.ParkingFee:
                    return "0002";
                case AppPointUsableObject.OtherFee:
                    return "0003";
                case AppPointUsableObject.Goods:
                    return "0011";
                default:
                    throw new ArgumentException("不存在该积分对象");
            }
        }

        public static string GetValue(AppPointUsableObject @enum)
        {
            switch (@enum)
            {
                case AppPointUsableObject.PropertyFee:
                    return "物业费";
                case AppPointUsableObject.ParkingFee:
                    return "车位费";
                case AppPointUsableObject.OtherFee:
                    return "其他代收费用";
                case AppPointUsableObject.Goods:
                    return "商品";
                default:
                    throw new ArgumentException("不存在该积分对象");
            }
        }

        public static string GetValue(string key)
        {
            switch (key)
            {
                case "0001":
                    return "物业费";
                case "0002":
                    return "车位费";
                case "0003":
                    return "其他代收费用";
                case "0011":
                    return "商品";
                default:
                    throw new ArgumentException("不存在该积分对象," + key);
            }
        }
    }
}

