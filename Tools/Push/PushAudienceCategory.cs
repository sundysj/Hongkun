using System;

namespace TWTools.Push
{
    /// <summary>
    /// 推送目标设备类型
    /// </summary>
    public enum PushAudienceCategory
    {
        /// <summary>
        /// 广播
        /// </summary>
        All = 0,

        /// <summary>
        /// 标签
        /// </summary>
        Tags = 1,

        /// <summary>
        /// 别名
        /// </summary>
        Alias = 2,

        /// <summary>
        /// 设备ID
        /// </summary>
        RegistrationID = 3,

        /// <summary>
        /// 用户群组
        /// </summary>
        UserGroup
    }

    /// <summary>
    /// 推送设备二级关联方式
    /// </summary>
    public enum PushAudienceSecondCategory
    {
        /// <summary>
        /// 标签取交集
        /// </summary>
        TagsAnd = 0,

        /// <summary>
        /// 标签取补集
        /// </summary>
        [Obsolete("SDK暂不支持")]
        TagsNot = 1
    }
}
