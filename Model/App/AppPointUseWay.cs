using System;

namespace App.Model
{
	/// <summary>
	/// ����ʹ�÷�ʽ
	/// </summary>
	[Serializable]
	public enum AppPointUseWay
	{
        /// <summary>
        /// ��ҵ�ѵ���
        /// </summary>
        PropertyFeeDeduction = 1,

        /// <summary>
        /// �������
        /// </summary>
        StoreTradeDeduction = 2,

        /// <summary>
        /// �����Ż�ȯ�һ�
        /// </summary>
        StoreCouponExchange = 3,

        /// <summary>
        /// ��ҵ��Ʒ�һ�
        /// </summary>
        PropertyGiftExchange = 4,

        /// <summary>
        /// �̼���Ʒ�һ�
        /// </summary>
        StoreGoodsExchange = 5,

        /// <summary>
        /// �Զ����ڣ��ݲ�֧��
        /// </summary>
        AutomaticallyExpire = 99
    }

    /// <summary>
    /// ����ʹ�÷�ʽת����
    /// </summary>
    public static class AppPointUseWayConverter
    {
        /// <summary>
        /// ת��Ϊ<see cref="AppPointUseWay">ö��
        /// </summary>
        public static AppPointUseWay Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppPointUseWay.PropertyFeeDeduction;
                case "0002":
                    return AppPointUseWay.StoreTradeDeduction;
                case "0003":
                    return AppPointUseWay.StoreCouponExchange;
                case "0004":
                    return AppPointUseWay.PropertyGiftExchange;
                case "0005":
                    return AppPointUseWay.StoreGoodsExchange;
                case "0099":
                    return AppPointUseWay.AutomaticallyExpire;
                default:
                    throw new ArgumentException("�����ڸ�ʹ�÷�ʽ");
            }
        }

        /// <summary>
        /// ��ȡkey
        /// </summary>
        public static string GetKey(AppPointUseWay @enum)
        {
            switch (@enum)
            {
                case AppPointUseWay.PropertyFeeDeduction:
                    return "0001";
                case AppPointUseWay.StoreTradeDeduction:
                    return "0002";
                case AppPointUseWay.StoreCouponExchange:
                    return "0003";
                case AppPointUseWay.PropertyGiftExchange:
                    return "0004";
                case AppPointUseWay.StoreGoodsExchange:
                    return "0005";
                case AppPointUseWay.AutomaticallyExpire:
                    return "0099";
                default:
                    throw new ArgumentException("�����ڸ�ʹ�÷�ʽ");
            }
        }

        public static string GetValue(AppPointUseWay @enum)
        {
            switch (@enum)
            {
                case AppPointUseWay.PropertyFeeDeduction:
                    return "��ҵ�ѵ���";
                case AppPointUseWay.StoreTradeDeduction:
                    return "�������";
                case AppPointUseWay.StoreCouponExchange:
                    return "�����Ż�ȯ�һ�";
                case AppPointUseWay.PropertyGiftExchange:
                    return "��ҵ��Ʒ�һ�";
                case AppPointUseWay.StoreGoodsExchange:
                    return "�̼���Ʒ�һ�";
                case AppPointUseWay.AutomaticallyExpire:
                    return "�Զ�����";
                default:
                    throw new ArgumentException("�����ڸ�ʹ�÷�ʽ");
            }
        }
    }
}

