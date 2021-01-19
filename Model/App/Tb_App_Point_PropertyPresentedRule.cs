using System;
using Newtonsoft.Json;

namespace App.Model
{
    /// <summary>
    /// 积分赠送规则表
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
        /// 物业公司编号
        /// </summary>
        public Int16 CorpID
        {
            get;
            set;
        }

        /// <summary>
        /// 小区id
        /// </summary>
        [JsonConverter(typeof(GuidConverter))]
        public string CommunityID
        {
            get;
            set;
        }

        /// <summary>
        /// 积分对象
        /// </summary>
        public String PresentedObject
        {
            get;
            set;
        }

        /// <summary>
        /// 赠送条件
        /// </summary>
        public decimal ConditionAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 赠送积分数量
        /// </summary>
        public int PresentedPoints
        {
            get;
            set;
        }

        /// <summary>
        /// 规则生效时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// 规则失效时间
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime OperateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 操作人员
        /// </summary>
        public String OperateUser
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public Boolean IsDelete
        {
            get;
            set;
        }

    }
}