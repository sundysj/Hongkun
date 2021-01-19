using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// ���ֵֿ۹����
    /// </summary>
    [Serializable]
    public class Tb_App_Point_PropertyDeductionRule
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
        public Int16 CorpID
        {
            get;
            set;
        }

        /// <summary>
        /// С��id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public string CommunityID
        {
            get;
            set;
        }

        /// <summary>
        /// ���ֶ���
        /// </summary>
        public String DeductionObject
        {
            get;
            set;
        }

        /// <summary>
        /// ��������
        /// </summary>
        public decimal ConditionAmount
        {
            get;
            set;
        }

        /// <summary>
        /// ���ֿɵ��ý��
        /// </summary>
        public decimal DiscountsAmount
        {
            get;
            set;
        }

        /// <summary>
        /// ������Чʱ��
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// ����ʧЧʱ��
        /// </summary>
        public DateTime EndTime
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
        public String OperateUser
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