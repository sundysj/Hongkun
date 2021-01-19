using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �������͹����
    /// </summary>
    [Serializable]
    public class Tb_App_Point_PropertyPresentedRule
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
        public String PresentedObject
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
        /// ���ͻ�������
        /// </summary>
        public int PresentedPoints
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