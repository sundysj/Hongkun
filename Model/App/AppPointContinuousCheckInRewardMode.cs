using System;

namespace App.Model
{
	/// <summary>
	/// ����ǩ������ģʽ
	/// </summary>
	[Serializable]
	public enum AppPointContinuousCheckInRewardMode
	{
        /// <summary>
        /// �������ֽ�����S=a1����������=��������
        /// </summary>
		Basal = 1,

        /// <summary>
        /// ��������ÿ�յ�����S=a1*n����������=�������֡�����ǩ������
        /// </summary>
        Increase = 2,

        /// <summary>
        /// �����ۼӣ��ݲ�֧��
        /// </summary>
        Accumulate = 3,

        /// <summary>
        /// ��ҵ�Զ��幫ʽ���ݲ�֧��
        /// </summary>
        PropertyDefined = 99
    }

    /// <summary>
    /// ����ǩ������ģʽת����
    /// </summary>
    public static class AppPointContinuousCheckInRewardModeConverter
    {
        /// <summary>
        /// ת��Ϊ<see cref="AppPointContinuousCheckInRewardMode"/>ö��
        /// </summary>
        public static AppPointContinuousCheckInRewardMode Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppPointContinuousCheckInRewardMode.Basal;
                case "0002":
                    return AppPointContinuousCheckInRewardMode.Increase;
                case "0003":
                    return AppPointContinuousCheckInRewardMode.Accumulate;
                case "0099":
                    return AppPointContinuousCheckInRewardMode.PropertyDefined;
                default:
                    throw new ArgumentException("�����ڸý���ģʽ");
            }
        }



        /// <summary>
        /// ��ȡkey
        /// </summary>
        public static string GetKey(AppPointContinuousCheckInRewardMode @enum)
        {
            switch (@enum)
            {
                case AppPointContinuousCheckInRewardMode.Basal:
                    return "0001";
                case AppPointContinuousCheckInRewardMode.Increase:
                    return "0002";
                case AppPointContinuousCheckInRewardMode.Accumulate:
                    return "0003";
                case AppPointContinuousCheckInRewardMode.PropertyDefined:
                    return "0099";
                default:
                    throw new ArgumentException("�����ڸý���ģʽ");
            }
        }
    }
}

