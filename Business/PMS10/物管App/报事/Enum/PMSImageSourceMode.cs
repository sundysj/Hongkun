using System;
using System.ComponentModel;

namespace Business.PMS10.物管App.报事.Models
{
    /// <summary>
    /// 图片选择方式
    /// </summary>
    [Flags]
    public enum PMSImageSourceMode
    {
        /// <summary>
        /// 直接拍照
        /// </summary>
        [Description("直接拍照")]
        TakePicture = 1 << 0,

        /// <summary>
        /// 相册选择
        /// </summary>
        [Description("相册选择")]
        FromAlbum = 1 << 1
    }
}
