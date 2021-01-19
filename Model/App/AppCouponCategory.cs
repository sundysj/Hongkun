using System;

namespace App.Model
{
    /// <summary>
    /// �Ż�ȯ����
    /// </summary>
    [Serializable]
	public enum AppCouponCategory
    {
        /// <summary>
        /// ����ȯ
        /// </summary>
        Voucher = 1,

        /// <summary>
        /// �ۿ�ȯ
        /// </summary>
        Discount = 2,

        /// <summary>
        /// ����ȯ
        /// </summary>
        FullReduction = 3,

        /// <summary>
        /// ���
        /// </summary>
        RedPacket = 4,
    }

    /// <summary>
    /// �Ż�ȯ����ת����
    /// </summary>
    public static class AppCouponCategoryConverter
    {
        /// <summary>
        /// ת��Ϊ<see cref="AppCouponCategory">ö��
        /// </summary>
        public static AppCouponCategory Convert(string key)
        {
            switch (key)
            {
                case "0001":
                    return AppCouponCategory.Voucher;
                case "0002":
                    return AppCouponCategory.Discount;
                case "0003":
                    return AppCouponCategory.FullReduction;
                case "0004":
                    return AppCouponCategory.RedPacket;
                default:
                    throw new ArgumentException("�����ڸ��Ż�ȯ����");
            }
        }

        /// <summary>
        /// ��ȡkey
        /// </summary>
        public static string GetKey(AppCouponCategory @enum)
        {
            switch (@enum)
            {
                case AppCouponCategory.Voucher:
                    return "0001";
                case AppCouponCategory.Discount:
                    return "0002";
                case AppCouponCategory.FullReduction:
                    return "0003";
                case AppCouponCategory.RedPacket:
                    return "0004";
                default:
                    throw new ArgumentException("�����ڸ��Ż�ȯ����");
            }
        }
    }
}

