using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.PMS10.物管App.报事.Enum;

namespace Business.PMS10.物管App.报事.Models
{
    public class Tb_HSPR_IncidentPushSetting
    {
        private string _pushObject;

        public string PushObject
        {
            get { return _pushObject; }
            set
            {
                _pushObject = value;

                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Contains("分派岗位"))
                        AudienceType = PMSIncidentPushAudienceType.AssignRole;
                    else if (value.Contains("处理岗位"))
                        AudienceType = PMSIncidentPushAudienceType.ProcessRole;
                    else if (value.Contains("客户管家"))
                        AudienceType = PMSIncidentPushAudienceType.RoomHousekeeper;
                    else if (value.Contains("公区管家"))
                        AudienceType = PMSIncidentPushAudienceType.RegionHousekeeper;
                    else if (value.Contains("报事客户"))
                        AudienceType = PMSIncidentPushAudienceType.Customer;
                    else if (value.Contains("报事员工"))
                        AudienceType = PMSIncidentPushAudienceType.IncidentEmployee;
                    else if (value.Contains("责任员工"))
                        AudienceType = PMSIncidentPushAudienceType.DealMan;
                    else if (value.Contains("协助员工"))
                        AudienceType = PMSIncidentPushAudienceType.AssistantMan;
                    else if (value.Contains("关闭员工"))
                        AudienceType = PMSIncidentPushAudienceType.CloseEmployee;
                    else if (value.Contains("待审岗位"))
                        AudienceType = PMSIncidentPushAudienceType.AuditRole;
                }
            }
        }

        /// <summary>
        /// 推送内容
        /// </summary>
        public string PushContent { get; set; }

        /// <summary>
        /// 是否推送App信息
        /// </summary>
        public bool PushNotification { get; set; }

        /// <summary>
        /// 是否发送手机短信
        /// </summary>
        public bool SendShortMessage { get; set; }

        public PMSIncidentPushAudienceType AudienceType { get; set; }
    }
}
