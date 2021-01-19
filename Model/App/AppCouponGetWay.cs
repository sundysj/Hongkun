using System;

namespace App.Model
{
	/// <summary>
	/// �Ż�ȯ��ȡ��ʽ
	/// </summary>
	[Serializable]
	public enum AppCouponGetWay
	{
        /// <summary>
        /// �����ȡ
        /// </summary>
        FreeReceive = 1,

        /// <summary>
        /// ���ֶһ�
        /// </summary>
        PointExchange = 2,

        /// <summary>
        /// ��ҵ�ɷ�����
        /// </summary>
        PropertyPayment = 3
    }

    /// <summary>
    /// �Ż�ȯ��ȡ��ʽת����
    /// </summary>
    public static class AppCouponGetWayConverter
    {
        /// <summary>
        /// ת��Ϊ<see cref="AppCouponGetWay">ö��
        /// </summary>
        public static AppCouponGetWay Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppCouponGetWay.FreeReceive;
                case "0002":
                    return AppCouponGetWay.PointExchange;
                case "0003":
                    return AppCouponGetWay.PropertyPayment;
                default:
                    throw new ArgumentException("�����ڸû�ȡ��ʽ");
            }
        }

        /// <summary>
        /// ��ȡkey
        /// </summary>
        public static string GetKey(AppCouponGetWay @enum)
        {
            switch (@enum)
            {
                case AppCouponGetWay.FreeReceive:
                    return "0001";
                case AppCouponGetWay.PointExchange:
                    return "0002";
                case AppCouponGetWay.PropertyPayment:
                    return "0003";
                default:
                    throw new ArgumentException("�����ڸû�ȡ��ʽ");
            }
        }
    }
}

