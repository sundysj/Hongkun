using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �Ż�ȯ��˱�
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Audit
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
        /// �Ƿ����ͨ��
        /// </summary>
        public Boolean IsAllow
        {
            get;
            set;
        }

        /// <summary>
        /// ��ע��Ϣ
        /// </summary>
        public String Remark
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

    }
}