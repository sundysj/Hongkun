using System;

namespace App.Model
{
	/// <summary>
	/// �Ż�ȯϸ��
	/// </summary>
	[Serializable]
	public enum AppCouponType
    {
        /// <summary>
        /// ָ��С����ҵ�ѵ���ȯ
        /// </summary>
        PrescribedCommunityPropertyFeeVoucher = 1001,

        /// <summary>
        /// ָ��С����λ�ѵ���ȯ
        /// </summary>
        PrescribedCommunityParkingFeeVoucher = 1002,

        /// <summary>
        /// ͨ����ҵ�ѵ���ȯ
        /// </summary>
        UniversalCommunityPropertyFeeVoucher = 1003,

        /// <summary>
        /// ͨ�ó�λ�ѵ���ȯ
        /// </summary>
        UniversalCommunityParkingFeeVoucher = 1004,

        /// <summary>
        /// ָ����Ʒ����ȯ
        /// </summary>
        PrescribedGoodsVoucher = 1005,

        /// <summary>
        /// ָ����Ʒ������ȯ
        /// </summary>
        PrescribedGoodsCategoryVoucher = 1006,

        /// <summary>
        /// ָ�����̵���ȯ
        /// </summary>
        PrescribedStoreVoucher = 1007,

        /// <summary>
        /// ͨ����Ʒ����ȯ
        /// </summary>
        UniversalGoodsVoucher = 1008,

        /// <summary>
        /// ͨ����Ʒ���͵���ȯ
        /// </summary>
        UniversalGoodsCategoryVoucher = 1009,

        /// <summary>
        /// ͨ�õ��̵���ȯ
        /// </summary>
        UniversalStoreVoucher = 1010,

        /// <summary>
        /// ָ����Ʒ�ۿ�ȯ
        /// </summary>
        PrescribedGoodsDiscount = 2005,

        /// <summary>
        /// ָ����Ʒ����ۿ�ȯ
        /// </summary>
        PrescribedGoodsCategoryDiscount = 2006,

        /// <summary>
        /// ָ�������ۿ�ȯ
        /// </summary>
        PrescribedStoreDiscount = 2007,

        /// <summary>
        /// ͨ����Ʒ�ۿ�ȯ
        /// </summary>
        UniversalGoodsDiscount = 2008,

        /// <summary>
        /// ͨ����Ʒ����ۿ�ȯ
        /// </summary>
        UniversalGoodsCategoryDiscount = 2009,

        /// <summary>
        /// ͨ�õ����ۿ�ȯ
        /// </summary>
        UniversalStoreDiscount = 2010,

        /// <summary>
        /// ָ����Ʒ����ȯ
        /// </summary>
        PrescribedGoodsFullReduction = 3005,

        /// <summary>
        /// ָ����Ʒ�������ȯ
        /// </summary>
        PrescribedGoodsCategoryFullReduction = 3006,

        /// <summary>
        /// ָ����������ȯ
        /// </summary>
        PrescribedStoreFullReduction = 3007,

        /// <summary>
        /// ͨ����Ʒ����ȯ
        /// </summary>
        UniversalGoodsFullReduction = 3008,

        /// <summary>
        /// ͨ����Ʒ�������ȯ
        /// </summary>
        UniversalGoodsCategoryFullReduction = 3009,

        /// <summary>
        /// ͨ�õ�������ȯ
        /// </summary>
        UniversalStoreFullReduction = 3010,

        /// <summary>
        /// ��ҵ���
        /// </summary>
        PropertyRedPacket = 4001,

        /// <summary>
        /// ���̺��
        /// </summary>
        StoreRedPacket = 4002
	}

    /// <summary>
    /// �Ż�ȯϸ��ת����
    /// </summary>
    public static class AppCouponTypeConverter
    {
        /// <summary>
        /// ת��Ϊ<see cref="AppCouponType">ö��
        /// </summary>
        public static AppCouponType Convert(string key)
        {
            switch (key)
            {
                // ����ȯ
                case "1001":
                    return AppCouponType.PrescribedCommunityPropertyFeeVoucher;
                case "1002":
                    return AppCouponType.PrescribedCommunityParkingFeeVoucher;
                case "1003":
                    return AppCouponType.UniversalCommunityPropertyFeeVoucher;
                case "1004":
                    return AppCouponType.UniversalCommunityParkingFeeVoucher;
                case "1005":
                    return AppCouponType.PrescribedGoodsVoucher;
                case "1006":
                    return AppCouponType.PrescribedGoodsCategoryVoucher;
                case "1007":
                    return AppCouponType.PrescribedStoreVoucher;
                case "1008":
                    return AppCouponType.UniversalGoodsVoucher;
                case "1009":
                    return AppCouponType.UniversalGoodsCategoryVoucher;
                case "1010":
                    return AppCouponType.UniversalStoreVoucher;

                // �ۿ�ȯ
                case "2005":
                    return AppCouponType.PrescribedGoodsDiscount;
                case "2006":
                    return AppCouponType.PrescribedGoodsCategoryDiscount;
                case "2007":
                    return AppCouponType.PrescribedStoreDiscount;
                case "2008":
                    return AppCouponType.UniversalGoodsDiscount;
                case "2009":
                    return AppCouponType.UniversalGoodsCategoryDiscount;
                case "2010":
                    return AppCouponType.UniversalStoreDiscount;

                // ����ȯ
                case "3005":
                    return AppCouponType.PrescribedGoodsFullReduction;
                case "3006":
                    return AppCouponType.PrescribedGoodsCategoryFullReduction;
                case "3007":
                    return AppCouponType.PrescribedStoreFullReduction;
                case "3008":
                    return AppCouponType.UniversalGoodsFullReduction;
                case "3009":
                    return AppCouponType.UniversalGoodsCategoryFullReduction;
                case "3010":
                    return AppCouponType.UniversalStoreFullReduction;

                // ���
                case "4001":
                    return AppCouponType.PropertyRedPacket;
                case "4002":
                    return AppCouponType.StoreRedPacket;
                default:
                    throw new ArgumentException("�����ڸ��Ż�ȯ���");
            }
        }
    }
}

