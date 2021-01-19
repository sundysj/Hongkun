using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// ÿ��ǩ����¼��
    /// </summary>
    [Serializable]
    public class Tb_App_DailyCheckIn
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
        /// ǩ��ʱ��
        /// </summary>
        public DateTime CheckInTime
        {
            get;
            set;
        }

        /// <summary>
        /// ������������
        /// </summary>
        public Int16 RewardPoints
        {
            get;
            set;
        }

        /// <summary>
        /// ���⽱������
        /// </summary>
        public Int16 AdditionalRewardPoints
        {
            get;
            set;
        }

        /// <summary>
        /// �Ƿ���⽱������
        /// </summary>
        public Boolean IsAdditionalReward
        {
            get;
            set;
        }
    }
}