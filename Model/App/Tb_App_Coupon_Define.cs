using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �Ż�ȯ������Ϣ��
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Define
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯϸ��
        /// </summary>
        public String Category
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯϸ��
        /// </summary>
        public String Type
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯʹ��������Ϊ-1���ʾ������ʹ������
        /// </summary>
        public Decimal ConditionAmount
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯ����/�ۿ���
        /// </summary>
        public Decimal DiscountsPoints
        {
            get;
            set;
        }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime OperateTime
        {
            get;
            set;
        }

        /// <summary>
        /// ������Ա
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid OperateUser
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���ɾ��
        /// </summary>
        public Boolean IsDelete
        {
            get;
            set;
        }
    }
}