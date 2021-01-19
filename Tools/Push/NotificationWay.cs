namespace TWTools.Push
{
    /// <summary>
    /// 通知方式
    /// </summary>
    public enum NotificationWay
    {
        All = 0,

        /// <summary>
        /// APNs通知
        /// </summary>
        APNs = 1 << 0,

        /// <summary>
        /// 应用内消息
        /// </summary>
        Message = 1 << 1
    }
}
