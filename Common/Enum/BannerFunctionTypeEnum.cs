using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    /// <summary>
    /// 首页埋点功能来源枚举
    /// </summary>
    public enum BannerFunctionTypeEnum
    {
        /// <summary>
        /// 该值不允许使用 ，仅作为占位符存在
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 首页Banner
        /// </summary>
        Banner = 1,

        /// <summary>
        /// 我的管家
        /// </summary>
        MyHousekeeper = 2,

        /// <summary>
        /// 访客邀请
        /// </summary>
        VisitorsInvited = 3,

        /// <summary>
        /// 报事报修
        /// </summary>
        ReportRepair = 4,

        /// <summary>
        /// 房屋账单
        /// </summary>
        HouseBill = 5,

        /// <summary>
        /// 社区活动    
        /// </summary>
        CommunityActivity = 6,

        /// <summary>
        /// 投诉赞赏
        /// </summary>
        ComplainAppreciate = 7,

        /// <summary>
        /// 热销推荐
        /// </summary>
        HotSaleRecommend = 8,

        /// <summary>
        /// 阳光物业
        /// </summary>
        SunProperty = 9,

        /// <summary>
        /// 畅销爆品
        /// </summary>
        BestSellingProduct = 10,

        /// <summary>
        /// 精品房源
        /// </summary>
        FineHouses = 11,
    }
}
