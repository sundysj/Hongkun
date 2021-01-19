using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// �û�ÿ��ǩ�����ֽ������Ʊ�
    /// </summary>
    [Serializable]
    public class Tb_Control_AppPoint_DailyCheckIn
    {
        /// <summary>
        /// ��ҵ��˾���
        /// </summary>
        [JsonIgnore]
        public Int16 CorpID
        {
            get;
            set;
        }

        public string CommunityID
        {
            get;
            set;
        }

        /// <summary>
        /// ÿ��ǩ����������������
        /// </summary>
        public Int16 BaseRewardPoints
        {
            get;
            set;
        }

        /// <summary>
        /// ����ǩ���������㷽ʽ
        /// </summary>
        public String ContinuousCheckInRewardMode
        {
            get;
            set;
        }

        /// <summary>
        /// ����ǩ��������ⶥ������Ϊ-1��������
        /// </summary>
        public Int16 ContinuousCheckInLimitDays
        {
            get;
            set;
        }

        /// <summary>
        /// ����ǩ�����⽱��������
        /// </summary>
        public Int16 ContinuousCheckInAdditionalRewardPoints
        {
            get;
            set;
        }

        /// <summary>
        /// ����ǩ�����⽱���涨������������ڵ���������ⶥ����
        /// </summary>
        public Int16 ContinuousCheckInAdditionalRewardLimitDays
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
        public static Tb_Control_AppPoint_DailyCheckIn DefaultControl
        {
            get
            {
                return new Tb_Control_AppPoint_DailyCheckIn()
                {
                    CorpID = 1000,
                    CommunityID = "",
                    BaseRewardPoints = 1,
                    ContinuousCheckInRewardMode = "0001",
                    ContinuousCheckInLimitDays = 7,
                    ContinuousCheckInAdditionalRewardPoints = 0,
                    ContinuousCheckInAdditionalRewardLimitDays = 0,
                    LastEditTime = DateTime.MinValue,
                    IsEnable = true
                };
            }
        }
    }
}