using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �û�����ʹ��Ȩ�޿��Ʊ�
    /// </summary>
    [Serializable]
    public class Tb_Control_AppPoint
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// ��ҵ��˾���
        /// </summary>
        [JsonIgnore]
        public Int16 CorpID
        {
            get;
            set;
        }

        /// <summary>
        /// С��id
        /// </summary>
        public string CommunityID
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ����������ҵ��
        /// </summary>
        public Boolean AllowDeductionPropertyFees
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ��������������ҵ���շ���
        /// </summary>
        public Boolean AllowDeductionOtherPropertyFees
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�������ó�λ��
        /// </summary>
        public Boolean AllowDeductionParkingFees
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ����������Ʒ����
        /// </summary>
        public Boolean AllowDeductionGoodsFees
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�����ÿ��ǩ���ͻ���
        /// </summary>
        public Boolean AllowDailyCheckInAward
        {
            get;
            set;
        }

        /// <summary>
        /// ���ֶ�����ұ�����Ĭ����100:1
        /// </summary>
        public Int16 PointExchangeRatio
        {
            get;
            set;
        }

        /// <summary>
        /// ���һ�α༭ʱ��
        /// </summary>
        public DateTime LastEditTime
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public Boolean IsEnable
        {
            get;
            set;
        }

        /// <summary>
        /// Ĭ�����ã��μ����ݿ������
        /// </summary>
        public static Tb_Control_AppPoint DefaultControl
        {
            get
            {
                return new Tb_Control_AppPoint()
                {
                    IID = Guid.Empty,
                    CorpID = 1000,
                    CommunityID = Guid.Empty.ToString(),
                    AllowDeductionPropertyFees = false,
                    AllowDeductionParkingFees = false,
                    AllowDeductionOtherPropertyFees = false,
                    AllowDeductionGoodsFees = false,
                    AllowDailyCheckInAward = false,
                    PointExchangeRatio = 100,
                    LastEditTime = DateTime.MinValue,
                    IsEnable = true
                };
            }
        }
    }
}