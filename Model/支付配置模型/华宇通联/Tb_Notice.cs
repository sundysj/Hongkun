using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.支付配置模型.华宇通联
{
    public class Tb_Notice
    {
        public string Id { get; set; }
        public int CommID { get; set; }
        public long RoomID { get; set; }
        public long CustID { get; set; }
        public string PayData { get; set; }
        public string CreateTime { get; set; }

        public string CreateUser { get; set; }
        public string DeviceMerchID { get; set; }
        public string DeviceTerID { get; set; }
    }
}
