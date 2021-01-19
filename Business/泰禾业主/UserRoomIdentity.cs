using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    /// <summary>
    /// 用户房屋绑定身份
    /// </summary>
    public enum UserRoomIdentity
    {
        /// <summary>
        /// 业主0013、0029
        /// </summary>
        Customer = 0,

        /// <summary>
        /// 家属0030
        /// </summary>
        FamilyMember = 1,

        /// <summary>
        /// 租户0031
        /// </summary>
        Tenant = 2,

        /// <summary>
        /// 其他0032
        /// </summary>
        Other = 3
    }

    /// <summary>
    /// 房屋身份绑定关系代码
    /// </summary>
    public class UserRoomIdentityCode
    {
        /// <summary>
        /// 业主代码：0013//ERP原先定义
        /// </summary>
        public static readonly string Customer1 = "0013";

        /// <summary>
        /// 业主代码：0029//APP添加
        /// </summary>
        public static readonly string Customer2 = "0029";

        /// <summary>
        /// 家属代码：0030
        /// </summary>
        public static readonly string FamilyMember = "0030";

        /// <summary>
        /// 租户代码：0031
        /// </summary>
        public static readonly string Tenant = "0031";

        /// <summary>
        /// 其他代码：0032
        /// </summary>
        public static readonly string Other = "0032";
    }
}
