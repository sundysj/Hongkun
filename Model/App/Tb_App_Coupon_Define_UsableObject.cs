using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �Ż�ȯ���ö����
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Define_UsableObject
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯ������Ϣid
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid DefineID
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
        /// �Ż�ȯ����С��id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid CommunityID
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯ�����̼�id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid StoreID
        {
            get;
            set;
        }

    }
}