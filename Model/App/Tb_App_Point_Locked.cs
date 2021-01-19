using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �û�����������
    /// </summary>
    [Serializable]
    public class Tb_App_Point_Locked
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
        /// ʹ�ü�¼id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public Guid UseHistoryID
        {
            get;
            set;
        }

        /// <summary>
        /// ��������������
        /// </summary>
        public Int32 LockedPoints
        {
            get;
            set;
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime LockedTime
        {
            get;
            set;
        }

    }
}