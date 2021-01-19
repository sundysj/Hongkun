using KernelDev.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HSPR
{
    [Serializable()]
    public class TbIncidentTaskProcess : ExtensibleObjectBase
    {
        public TbIncidentTaskProcess()
        {

        }
        public TbIncidentTaskProcess(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Deserialize(info, context);
        }

        public string ProcessCode
        {
            get
            {
                if (GetValue("ProcessCode") != null && GetValue("ProcessCode") != "")
                {
                    return GetValue("ProcessCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ProcessCode", value.ToString());
            }
        }

        public string TaskCode
        {
            get
            {
                if (GetValue("TaskCode") != null && GetValue("TaskCode") != "")
                {
                    return GetValue("TaskCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskCode", value.ToString());
            }
        }

        public string BussType
        {
            get
            {
                if (GetValue("BussType") != null && GetValue("BussType") != "")
                {
                    return GetValue("BussType");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("BussType", value.ToString());
            }
        }

        public string InfoType
        {
            get
            {
                if (GetValue("InfoType") != null && GetValue("InfoType") != "")
                {
                    return GetValue("InfoType");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("InfoType", value.ToString());
            }
        }

        public string ProcessContent
        {
            get
            {
                if (GetValue("ProcessContent") != null && GetValue("ProcessContent") != "")
                {
                    return GetValue("ProcessContent");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ProcessContent", value.ToString());
            }
        }

        public string ProcessDocs
        {
            get
            {
                if (GetValue("ProcessDocs") != null && GetValue("ProcessDocs") != "")
                {
                    return GetValue("ProcessDocs");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ProcessDocs", value.ToString());
            }
        }

        public string ProcessUser
        {
            get
            {
                if (GetValue("ProcessUser") != null && GetValue("ProcessUser") != "")
                {
                    return GetValue("ProcessUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ProcessUser", value.ToString());
            }
        }

        public string ProcessDate
        {
            get
            {
                if (GetValue("ProcessDate") != null && GetValue("ProcessDate") != "")
                {
                    return GetValue("ProcessDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ProcessDate", value.ToString());
            }
        }

        public string RepeatIncidnetDate
        {
            get
            {
                if (GetValue("RepeatIncidnetDate") != null && GetValue("RepeatIncidnetDate") != "")
                {
                    return GetValue("RepeatIncidnetDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RepeatIncidnetDate", value.ToString());
            }
        }

        public string RemindersContent
        {
            get
            {
                if (GetValue("RemindersContent") != null && GetValue("RemindersContent") != "")
                {
                    return GetValue("RemindersContent");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RemindersContent", value.ToString());
            }
        }

        public string RemindersDocs
        {
            get
            {
                if (GetValue("RemindersDocs") != null && GetValue("RemindersDocs") != "")
                {
                    return GetValue("RemindersDocs");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RemindersDocs", value.ToString());
            }
        }

        public string RemindersNoticeMethod
        {
            get
            {
                if (GetValue("RemindersNoticeMethod") != null && GetValue("RemindersNoticeMethod") != "")
                {
                    return GetValue("RemindersNoticeMethod");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RemindersNoticeMethod", value.ToString());
            }
        }

        public string RemindersNoticeUser
        {
            get
            {
                if (GetValue("RemindersNoticeUser") != null && GetValue("RemindersNoticeUser") != "")
                {
                    return GetValue("RemindersNoticeUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RemindersNoticeUser", value.ToString());
            }
        }

        public string RemindersUser
        {
            get
            {
                if (GetValue("RemindersUser") != null && GetValue("RemindersUser") != "")
                {
                    return GetValue("RemindersUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RemindersUser", value.ToString());
            }
        }

        public string RemindersDate
        {
            get
            {
                if (GetValue("RemindersDate") != null && GetValue("RemindersDate") != "")
                {
                    return GetValue("RemindersDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RemindersDate", value.ToString());
            }
        }

        public string LiableCompanyCode
        {
            get
            {
                if (GetValue("LiableCompanyCode") != null && GetValue("LiableCompanyCode") != "")
                {
                    return GetValue("LiableCompanyCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LiableCompanyCode", value.ToString());
            }
        }

        public string ThreeCompanyCode
        {
            get
            {
                if (GetValue("ThreeCompanyCode") != null && GetValue("ThreeCompanyCode") != "")
                {
                    return GetValue("ThreeCompanyCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ThreeCompanyCode", value.ToString());
            }
        }

        public string UpgradeType
        {
            get
            {
                if (GetValue("UpgradeType") != null && GetValue("UpgradeType") != "")
                {
                    return GetValue("UpgradeType");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("UpgradeType", value.ToString());
            }
        }

        public string TaskLevelCode
        {
            get
            {
                if (GetValue("TaskLevelCode") != null && GetValue("TaskLevelCode") != "")
                {
                    return GetValue("TaskLevelCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskLevelCode", value.ToString());
            }
        }

        public string Negotiation
        {
            get
            {
                if (GetValue("Negotiation") != null && GetValue("Negotiation") != "")
                {
                    return GetValue("Negotiation");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Negotiation", value.ToString());
            }
        }

        public string CC
        {
            get
            {
                if (GetValue("CC") != null && GetValue("CC") != "")
                {
                    return GetValue("CC");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("CC", value.ToString());
            }
        }

        public string NegotiationContent
        {
            get
            {
                if (GetValue("NegotiationContent") != null && GetValue("NegotiationContent") != "")
                {
                    return GetValue("NegotiationContent");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationContent", value.ToString());
            }
        }

        public string NegotiationDocs
        {
            get
            {
                if (GetValue("NegotiationDocs") != null && GetValue("NegotiationDocs") != "")
                {
                    return GetValue("NegotiationDocs");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationDocs", value.ToString());
            }
        }

        public string NegotiationNoticeSys
        {
            get
            {
                if (GetValue("NegotiationNoticeSys") != null && GetValue("NegotiationNoticeSys") != "")
                {
                    return GetValue("NegotiationNoticeSys");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationNoticeSys", value.ToString());
            }
        }

        public string NegotiationNoticeMsg
        {
            get
            {
                if (GetValue("NegotiationNoticeMsg") != null && GetValue("NegotiationNoticeMsg") != "")
                {
                    return GetValue("NegotiationNoticeMsg");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationNoticeMsg", value.ToString());
            }
        }

        public string NegotiationScope
        {
            get
            {
                if (GetValue("NegotiationScope") != null && GetValue("NegotiationScope") != "")
                {
                    return GetValue("NegotiationScope");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationScope", value.ToString());
            }
        }

        public string NegotiationNoticeUser
        {
            get
            {
                if (GetValue("NegotiationNoticeUser") != null && GetValue("NegotiationNoticeUser") != "")
                {
                    return GetValue("NegotiationNoticeUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationNoticeUser", value.ToString());
            }
        }

        public string NegotiationStartUser
        {
            get
            {
                if (GetValue("NegotiationStartUser") != null && GetValue("NegotiationStartUser") != "")
                {
                    return GetValue("NegotiationStartUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationStartUser", value.ToString());
            }
        }

        public string NegotiationStartDate
        {
            get
            {
                if (GetValue("NegotiationStartDate") != null && GetValue("NegotiationStartDate") != "")
                {
                    return GetValue("NegotiationStartDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationStartDate", value.ToString());
            }
        }

        public string LitigationMemo
        {
            get
            {
                if (GetValue("LitigationMemo") != null && GetValue("LitigationMemo") != "")
                {
                    return GetValue("LitigationMemo");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LitigationMemo", value.ToString());
            }
        }

        public string LitigationDocs
        {
            get
            {
                if (GetValue("LitigationDocs") != null && GetValue("LitigationDocs") != "")
                {
                    return GetValue("LitigationDocs");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LitigationDocs", value.ToString());
            }
        }

        public string LitigationNoticeUser
        {
            get
            {
                if (GetValue("LitigationNoticeUser") != null && GetValue("LitigationNoticeUser") != "")
                {
                    return GetValue("LitigationNoticeUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LitigationNoticeUser", value.ToString());
            }
        }

        public string LitigationNotice
        {
            get
            {
                if (GetValue("LitigationNotice") != null && GetValue("LitigationNotice") != "")
                {
                    return GetValue("LitigationNotice");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LitigationNotice", value.ToString());
            }
        }

        public string LitigationStartDate
        {
            get
            {
                if (GetValue("LitigationStartDate") != null && GetValue("LitigationStartDate") != "")
                {
                    return GetValue("LitigationStartDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LitigationStartDate", value.ToString());
            }
        }

        public string LitigationStartUser
        {
            get
            {
                if (GetValue("LitigationStartUser") != null && GetValue("LitigationStartUser") != "")
                {
                    return GetValue("LitigationStartUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LitigationStartUser", value.ToString());
            }
        }

        public string VisitUser
        {
            get
            {
                if (GetValue("VisitUser") != null && GetValue("VisitUser") != "")
                {
                    return GetValue("VisitUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisitUser", value.ToString());
            }
        }

        public string VisitDate
        {
            get
            {
                if (GetValue("VisitDate") != null && GetValue("VisitDate") != "")
                {
                    return GetValue("VisitDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisitDate", value.ToString());
            }
        }

        public string VisitedUser
        {
            get
            {
                if (GetValue("VisitedUser") != null && GetValue("VisitedUser") != "")
                {
                    return GetValue("VisitedUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisitedUser", value.ToString());
            }
        }

        public string VisiteMemo
        {
            get
            {
                if (GetValue("VisiteMemo") != null && GetValue("VisiteMemo") != "")
                {
                    return GetValue("VisiteMemo");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisiteMemo", value.ToString());
            }
        }

        public string VisiteIsTimely
        {
            get
            {
                if (GetValue("VisiteIsTimely") != null && GetValue("VisiteIsTimely") != "")
                {
                    return GetValue("VisiteIsTimely");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisiteIsTimely", value.ToString());
            }
        }

        public string VisiteIsSolve
        {
            get
            {
                if (GetValue("VisiteIsSolve") != null && GetValue("VisiteIsSolve") != "")
                {
                    return GetValue("VisiteIsSolve");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisiteIsSolve", value.ToString());
            }
        }

        public string VisiteIsCharge
        {
            get
            {
                if (GetValue("VisiteIsCharge") != null && GetValue("VisiteIsCharge") != "")
                {
                    return GetValue("VisiteIsCharge");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisiteIsCharge", value.ToString());
            }
        }

        public string VisiteEvaluation
        {
            get
            {
                if (GetValue("VisiteEvaluation") != null && GetValue("VisiteEvaluation") != "")
                {
                    return GetValue("VisiteEvaluation");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisiteEvaluation", value.ToString());
            }
        }

        public string VisiteSuggest
        {
            get
            {
                if (GetValue("VisiteSuggest") != null && GetValue("VisiteSuggest") != "")
                {
                    return GetValue("VisiteSuggest");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisiteSuggest", value.ToString());
            }
        }

        public string TimeOutRemark
        {
            get
            {
                if (GetValue("TimeOutRemark") != null && GetValue("TimeOutRemark") != "")
                {
                    return GetValue("TimeOutRemark");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TimeOutRemark", value.ToString());
            }
        }

        public string NoticeRange
        {
            get
            {
                if (GetValue("NoticeRange") != null && GetValue("NoticeRange") != "")
                {
                    return GetValue("NoticeRange");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NoticeRange", value);
            }
        }

        public string ReporterMan
        {
            get
            {
                if (GetValue("ReporterMan") != null && GetValue("ReporterMan") != "")
                {
                    return GetValue("ReporterMan");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReporterMan", value);
            }
        }

        public string ReporterDate
        {
            get
            {
                if (GetValue("ReporterDate") != null && GetValue("ReporterDate") != "")
                {
                    return GetValue("ReporterDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReporterDate", value.ToString());
            }
        }

        public string NegotiationCode
        {
            get
            {
                if (GetValue("NegotiationCode") != null && GetValue("NegotiationCode") != "")
                {
                    return GetValue("NegotiationCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationCode", value.ToString());
            }
        }

        public string CCCode
        {
            get
            {
                if (GetValue("CCCode") != null && GetValue("CCCode") != "")
                {
                    return GetValue("CCCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("CCCode", value.ToString());
            }
        }

        public string NegotiationNoticeUserCode
        {
            get
            {
                if (GetValue("NegotiationNoticeUserCode") != null && GetValue("NegotiationNoticeUserCode") != "")
                {
                    return GetValue("NegotiationNoticeUserCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NegotiationNoticeUserCode", value.ToString());
            }
        }

        public string NetAddr
        {
            get
            {
                if (GetValue("NetAddr") != null && GetValue("NetAddr").Trim() != "")
                {
                    return GetValue("NetAddr");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NetAddr", value);
            }
        }

        public string LitigationNoticeUserCode
        {
            get
            {
                if (GetValue("LitigationNoticeUserCode") != null && GetValue("LitigationNoticeUserCode").Trim() != "")
                {
                    return GetValue("LitigationNoticeUserCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LitigationNoticeUserCode", value);
            }
        }

        public string ReplayContent
        {
            get
            {
                if (GetValue("ReplayContent") != null && GetValue("ReplayContent").Trim() != "")
                {
                    return GetValue("ReplayContent");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReplayContent", value);
            }
        }

        public string SQLEx
        {
            get
            {
                if (GetValue("SQLEx") != null && GetValue("SQLEx").Trim() != "")
                {
                    return GetValue("SQLEx");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("SQLEx", value);
            }
        }

        public string ReplayCount
        {
            get
            {
                if (GetValue("ReplayCount") != null && GetValue("ReplayCount").Trim() != "")
                {
                    return GetValue("ReplayCount");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReplayCount", value);
            }
        }


        public string VisiteExitType
        {
            get
            {
                if (GetValue("VisiteExitType") != null && GetValue("VisiteExitType") != "")
                {
                    return GetValue("VisiteExitType");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("VisiteExitType", value.ToString());
            }
        }
    }
}
