using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Business
{
    public class Tb_Control_AppBindingRoom
    {
        [JsonIgnore]
        public Guid IID { get; set; }

        [JsonIgnore]
        public int CorpID { get; set; }

        [JsonIgnore]
        public string CommunityID { get; set; }

        /// <summary>
        /// 注册或刷新房屋时，是否自动绑定，默认为是
        /// </summary>
        [JsonIgnore]
        public bool AutoBind { get; set; }

        /// <summary>
        /// 是否允许家庭成员绑定，默认为是
        /// </summary>
        [JsonIgnore]
        public bool AllowHouseholdBind { get; set; }

        /// <summary>
        /// 是否允许租户绑定房屋，默认为是
        /// </summary>
        [JsonIgnore]
        public bool AllowLesseeBind { get; set; }

        /// <summary>
        /// 是否允许临时客户绑定房屋，默认为否
        /// </summary>
        [JsonIgnore]
        public bool AllowTemporaryBind { get; set; }

        /// <summary>
        /// 是否允许陌生人绑定房屋，默认为否
        /// </summary>
        [JsonIgnore]
        public bool AllowStrangerBind { get; set; }

        /// <summary>
        /// 绑定时，是否需要验证客户姓名，默认为是
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        public bool CheckCustName { get; set; }

        /// <summary>
        /// 绑定时，是否需要检查客户手机号，默认为是
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        public bool CheckCustMobile { get; set; }

        /// <summary>
        /// 绑定时，是否需要客户手机验证码进行验证，默认为否
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        public bool CheckCustMobileVerifyCode { get; set; }

        /// <summary>
        /// 绑定时，是否需要验证客户证件，默认为否
        /// </summary>
        [JsonConverter(typeof(BoolConverter))]
        public bool CheckCustPapersInfo { get; set; }

        /// <summary>
        /// 绑定时是否需要物业审核，默认为否
        /// </summary>
        [JsonIgnore]
        public bool NeedPropertyAudit { get; set; }

        /// <summary>
        /// 是否启用该管控
        /// </summary>
        [JsonIgnore]
        public bool IsEnable { get; set; }

        [JsonIgnore]
        public bool IsDelete { get; set; }

        public static Tb_Control_AppBindingRoom Default => new Tb_Control_AppBindingRoom()
        {
            AutoBind = true,
            AllowHouseholdBind = true,
            AllowLesseeBind = true,
            AllowTemporaryBind = false,
            AllowStrangerBind = false,
            CheckCustName = true,
            CheckCustMobile = true,
            CheckCustPapersInfo = false,
            CheckCustMobileVerifyCode = false,
            IsEnable = true,
            IsDelete = false
        };
    }
}
