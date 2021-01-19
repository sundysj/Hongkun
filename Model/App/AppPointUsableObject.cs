using System;

namespace App.Model
{
    /// <summary>
    /// ���ֶ���
    /// </summary>
    [Serializable]
    public enum AppPointUsableObject
    {
        /// <summary>
        /// ��ҵ��
        /// </summary>
		PropertyFee = 1,

        /// <summary>
        /// ��λ��
        /// </summary>
        ParkingFee = 2,

        /// <summary>
        /// �������շ���
        /// </summary>
        OtherFee = 3,

        /// <summary>
        /// ��Ʒ
        /// </summary>
        Goods = 11,
    }

    /// <summary>
    /// ���ֶ���ת����
    /// </summary>
    public static class AppPointUsableObjectConverter
    {
        /// <summary>
        /// ת��Ϊ<see cref="AppPointUsableObject">ö��
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
                    throw new ArgumentException("�����ڸû��ֶ���");
            }
        }

        /// <summary>
        /// ��ȡkey
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
                    throw new ArgumentException("�����ڸû��ֶ���");
            }
        }

        public static string GetValue(AppPointUsableObject @enum)
        {
            switch (@enum)
            {
                case AppPointUsableObject.PropertyFee:
                    return "��ҵ��";
                case AppPointUsableObject.ParkingFee:
                    return "��λ��";
                case AppPointUsableObject.OtherFee:
                    return "�������շ���";
                case AppPointUsableObject.Goods:
                    return "��Ʒ";
                default:
                    throw new ArgumentException("�����ڸû��ֶ���");
            }
        }

        public static string GetValue(string key)
        {
            switch (key)
            {
                case "0001":
                    return "��ҵ��";
                case "0002":
                    return "��λ��";
                case "0003":
                    return "�������շ���";
                case "0011":
                    return "��Ʒ";
                default:
                    throw new ArgumentException("�����ڸû��ֶ���," + key);
            }
        }
    }
}

