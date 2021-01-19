using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �û��������ͼ�¼��
    /// </summary>
    [Serializable]
    public class Tb_App_Point_PresentedHistory
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
        /// �������ͷ�ʽ
        /// </summary>
        public String PresentedWay
        {
            get;
            set;
        }

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        public DateTime PresentedTime
        {
            get;
            set;
        }

        /// <summary>
        /// �������ͻ�������
        /// </summary>
        public Int32 PresentedPoints
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
        /// ��ע��Ϣ
        /// </summary>
        public String Remark
        {
            get;
            set;
        }
    }
}