using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �û�����ʹ�ü�¼��
    /// </summary>
    [Serializable]
    public class Tb_App_Point_UseHistory
    {
        [JsonConverter(typeof(GuidConverter))]
        public Guid IID
        {
            get;
            set;
        }

        /// <summary>
        /// �û�id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid UserID
        {
            get;
            set;
        }

        /// <summary>
        /// ����ʹ�÷�ʽ
        /// </summary>
        public String UseWay
        {
            get;
            set;
        }

        /// <summary>
        /// ���λ���ʹ����
        /// </summary>
        public Int32 UsePoints
        {
            get;
            set;
        }

        /// <summary>
        /// ʹ��ʱ��
        /// </summary>
        public DateTime UseTime
        {
            get;
            set;
        }

        /// <summary>
        /// �������
        /// </summary>
        public Int32 PointBalance
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        public Boolean? IsEffect
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
    }
}