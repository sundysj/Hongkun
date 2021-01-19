using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �Ż�ȯ������Ϣ�����
    /// </summary>
    [Serializable]
    public class Tb_App_Coupon_Define_Detail
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
        /// ���η���������Ϊ-1���ʾ������
        /// </summary>
        public Int32 Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯ��ȡ��ʽ
        /// </summary>
        public String GetWay
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯ�һ���ȡ��ʼʱ��
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯ�һ���ȡ����ʱ��
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// �Ż�ȯ����ȡ����Чʱ�䣬Ϊ-1���ʾ������
        /// </summary>
        public Int16 OverdueDays
        {
            get;
            set;
        }

        /// <summary>
        /// �һ��Ż�ȯ������֣����ҽ�����ȡ��ʽΪ���ֶһ�ʱ��Ч
        /// </summary>
        public Int32 RequiredPoints
        {
            get;
            set;
        }

        /// <summary>
        /// ÿ�������ȡ���ٴΣ�Ϊ-1����������ȡ����
        /// </summary>
        public Int16 GetLimit
        {
            get;
            set;
        }

    }
}