namespace TWTools.Push
{
    /// <summary>
    /// 推送平台
    /// </summary>
    public enum PushPlatform
    {
        /// <summary>
        /// 所有平台
        /// </summary>
        All = 0,

        /// <summary>
        /// Android
        /// </summary>
        Android = 1 << 0, //1

        /// <summary>
        /// iOS
        /// </summary> // 2
        iOS = 1 << 1,

        /// <summary>
        /// WinPhone
        /// </summary>
        WinPhone = 1 << 2  // 4
    }
}
