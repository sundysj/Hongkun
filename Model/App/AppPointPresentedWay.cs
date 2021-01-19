using System;
using System.ComponentModel;

namespace App.Model
{
	/// <summary>
	/// �������ͷ�ʽ
	/// </summary>
	[Serializable]
	public enum AppPointPresentedWay
	{
        /// <summary>
        /// ÿ��ǩ��
        /// </summary>
        [Description("0001")]
        DailyCheckIn = 1,

        /// <summary>
        /// ����ǩ������
        /// </summary>
        [Description("0002")]
        ContinuousCheckInReward = 2,

        /// <summary>
        /// ��ҵǷ�ѽɷ�����
        /// </summary>
        [Description("0003")]
        PropertyArrearsPayment = 3,

        /// <summary>
        /// ��ҵԤ��ɷ�����
        /// </summary>
        [Description("0004")]
        PropertyPrestorePayment = 4,

        /// <summary>
        /// ��������
        /// </summary>
        [Description("0005")]
        StoreTrade = 5,

        /// <summary>
        /// ��ҵ��������
        /// </summary>
        [Description("0099")]
        PropertyPresented = 99
    }


    /// <summary>
    /// �������ͷ�ʽת����
    /// </summary>
    public static class AppPointPresentedWayConverter
    {
        /// <summary>
        /// ת��Ϊ<see cref="AppPointPresentedWay">ö��
        /// </summary>
        public static AppPointPresentedWay Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppPointPresentedWay.DailyCheckIn;
                case "0002":
                    return AppPointPresentedWay.ContinuousCheckInReward;
                case "0003":
                    return AppPointPresentedWay.PropertyArrearsPayment;
                case "0004":
                    return AppPointPresentedWay.PropertyPrestorePayment;
                case "0005":
                    return AppPointPresentedWay.StoreTrade;
                case "0099":
                    return AppPointPresentedWay.PropertyPresented;
                default:
                    throw new ArgumentException("�����ڸ����ͷ�ʽ");
            }
        }

        /// <summary>
        /// ��ȡkey
        /// </summary>
        public static string GetKey(AppPointPresentedWay @enum)
        {
            switch (@enum)
            {
                case AppPointPresentedWay.DailyCheckIn:
                    return "0001";
                case AppPointPresentedWay.ContinuousCheckInReward:
                    return "0002";
                case AppPointPresentedWay.PropertyArrearsPayment:
                    return "0003";
                case AppPointPresentedWay.PropertyPrestorePayment:
                    return "0004";
                case AppPointPresentedWay.StoreTrade:
                    return "0005";
                case AppPointPresentedWay.PropertyPresented:
                    return "0099";
                default:
                    throw new ArgumentException("�����ڸ����ͷ�ʽ");
            }
        }

        public static string GetValue(AppPointPresentedWay @enum)
        {
            switch (@enum)
            {
                case AppPointPresentedWay.DailyCheckIn:
                    return "ÿ��ǩ��";
                case AppPointPresentedWay.ContinuousCheckInReward:
                    return "����ǩ�����⽱��";
                case AppPointPresentedWay.PropertyArrearsPayment:
                    return "��ҵǷ�ѽɷ�����";
                case AppPointPresentedWay.PropertyPrestorePayment:
                    return "��ҵԤ��ɷ�����";
                case AppPointPresentedWay.StoreTrade:
                    return "��������";
                case AppPointPresentedWay.PropertyPresented:
                    return "��ҵ��������";
                default:
                    throw new ArgumentException("�����ڸ����ͷ�ʽ");
            }
        }
    }
}
