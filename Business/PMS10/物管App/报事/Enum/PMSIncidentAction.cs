using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.PMS10.物管App.报事.Enum
{
    public enum PMSIncidentAction
    {
        /// <summary>
        /// 已受理待分派
        /// </summary>
        Accepted,

        /// <summary>
        /// 口派转书面
        /// </summary>
        VerbalToWritten,

        /// <summary>
        /// 分派催办
        /// </summary>
        AssignUrged,

        /// <summary>
        /// 已派单待接单
        /// </summary>
        Assigned,

        /// <summary>
        /// 已抢单待接单
        /// </summary>
        Snatched,

        /// <summary>
        /// 处理催办
        /// </summary>
        DealUrged,

        /// <summary>
        /// 转发
        /// </summary>
        Transpond,

        /// <summary>
        /// 已接单待到场
        /// </summary>
        Received,

        /// <summary>
        /// 已到场处理中
        /// </summary>
        Arrived,

        /// <summary>
        /// 已处理待关闭
        /// </summary>
        Dealt,

        /// <summary>
        /// 关闭退回
        /// </summary>
        CloseReturn,

        /// <summary>
        /// 已关闭待回访
        /// </summary>
        Closed,

        /// <summary>
        /// 回访退回
        /// </summary>
        ReplyReturn,

        /// <summary>
        /// 已回访
        /// </summary>
        Replied,

        /// <summary>
        /// 已废弃
        /// </summary>
        Deleted,

        /// <summary>
        /// 发起协助申请
        /// </summary>
        AssistApply,

        /// <summary>
        /// 协助申请结果
        /// </summary>
        AssistApplyResult,

        /// <summary>
        /// 延期协助申请
        /// </summary>
        DelayApply,

        /// <summary>
        /// 延期申请结果
        /// </summary>
        DelayApplyResult,

        /// <summary>
        /// 发起非正常关闭申请
        /// </summary>
        UnnormalCloseApply,

        /// <summary>
        /// 非正常关闭申请结果
        /// </summary>
        UnnormalCloseApplyResult,

        /// <summary>
        /// 免费申请
        /// </summary>
        FreeApply,

        /// <summary>
        /// 免费申请结果
        /// </summary>
        FreeApplyResult
    }
}
