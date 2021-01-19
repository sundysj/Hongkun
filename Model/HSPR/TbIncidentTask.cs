using KernelDev.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HSPR
{
   public class TbIncidentTask : ExtensibleObjectBase
    {
        public TbIncidentTask()
        {

        }
        public TbIncidentTask(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Deserialize(info, context);
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

        public string ReceCode
        {
            get
            {
                if (GetValue("ReceCode") != null && GetValue("ReceCode") != "")
                {
                    return GetValue("ReceCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReceCode", value.ToString());
            }
        }

        public string TaskType
        {
            get
            {
                if (GetValue("TaskType") != null && GetValue("TaskType") != "")
                {
                    return GetValue("TaskType");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskType", value.ToString());
            }
        }

        public string TaskNum
        {
            get
            {
                if (GetValue("TaskNum") != null && GetValue("TaskNum") != "")
                {
                    return GetValue("TaskNum");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskNum", value.ToString());
            }
        }

        public string BigReasonCode
        {
            get
            {
                if (GetValue("BigReasonCode") != null && GetValue("BigReasonCode") != "")
                {
                    return GetValue("BigReasonCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("BigReasonCode", value.ToString());
            }
        }

        public int DealTime
        {
            get
            {
                if (GetValue("DealTime") != null && GetValue("DealTime") != "")
                {
                    return Convert.ToInt32(GetValue("DealTime").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("DealTime", value.ToString());
            }
        }
   
        
        public string IsProduct
        {
            get
            {
                if (GetValue("IsProduct") != null && GetValue("IsProduct") != "")
                {
                    return GetValue("IsProduct");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("IsProduct", value.ToString());
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

        public string AssignUser
        {
            get
            {
                if (GetValue("AssignUser") != null && GetValue("AssignUser") != "")
                {
                    return GetValue("AssignUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("AssignUser", value.ToString());
            }
        }

        public string OrderDate
        {
            get
            {
                if (GetValue("OrderDate") != null && GetValue("OrderDate") != "")
                {
                    return GetValue("OrderDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("OrderDate", value.ToString());
            }
        }

        public string TaskState
        {
            get
            {
                if (GetValue("TaskState") != null && GetValue("TaskState") != "")
                {
                    return GetValue("TaskState");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskState", value.ToString());
            }
        }

        public string TaskMemo
        {
            get
            {
                if (GetValue("TaskMemo") != null && GetValue("TaskMemo") != "")
                {
                    return GetValue("TaskMemo");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskMemo", value.ToString());
            }
        }

        public string AssignExitReason
        {
            get
            {
                if (GetValue("AssignExitReason") != null && GetValue("AssignExitReason") != "")
                {
                    return GetValue("AssignExitReason");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("AssignExitReason", value.ToString());
            }
        }

        public string AssignExitDate
        {
            get
            {
                if (GetValue("AssignExitDate") != null && GetValue("AssignExitDate") != "")
                {
                    return GetValue("AssignExitDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("AssignExitDate", value.ToString());
            }
        }

        public string SmallReasonCode
        {
            get
            {
                if (GetValue("SmallReasonCode") != null && GetValue("SmallReasonCode") != "")
                {
                    return GetValue("SmallReasonCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("SmallReasonCode", value.ToString());
            }
        }

        public string LiableUser
        {
            get
            {
                if (GetValue("LiableUser") != null && GetValue("LiableUser") != "")
                {
                    return GetValue("LiableUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LiableUser", value.ToString());
            }
        }

        public string AgentUser
        {
            get
            {
                if (GetValue("AgentUser") != null && GetValue("AgentUser") != "")
                {
                    return GetValue("AgentUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("AgentUser", value.ToString());
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

        public string AssignDate
        {
            get
            {
                if (GetValue("AssignDate") != null && GetValue("AssignDate") != "")
                {
                    return GetValue("AssignDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("AssignDate", value.ToString());
            }
        }

        public int CompanyWarranty
        {
            get
            {
                if (GetValue("CompanyWarranty") != null && GetValue("CompanyWarranty") != "")
                {
                    return Convert.ToInt32(GetValue("CompanyWarranty").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("CompanyWarranty", value.ToString());
            }
        }

        public int IndustryWarranty
        {
            get
            {
                if (GetValue("IndustryWarranty") != null && GetValue("IndustryWarranty") != "")
                {
                    return Convert.ToInt32(GetValue("IndustryWarranty").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IndustryWarranty", value.ToString());
            }
        }

        public int CompanyRepair
        {
            get
            {
                if (GetValue("CompanyRepair") != null && GetValue("CompanyRepair") != "")
                {
                    return Convert.ToInt32(GetValue("CompanyRepair").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("CompanyRepair", value.ToString());
            }
        }

        public int IndustryRepair
        {
            get
            {
                if (GetValue("IndustryRepair") != null && GetValue("IndustryRepair") != "")
                {
                    return Convert.ToInt32(GetValue("IndustryRepair").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IndustryRepair", value.ToString());
            }
        }

        public string TaskDocs
        {
            get
            {
                if (GetValue("TaskDocs") != null && GetValue("TaskDocs") != "")
                {
                    return GetValue("TaskDocs");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskDocs", value.ToString());
            }
        }

        public string DealExitReason
        {
            get
            {
                if (GetValue("DealExitReason") != null && GetValue("DealExitReason") != "")
                {
                    return GetValue("DealExitReason");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("DealExitReason", value.ToString());
            }
        }

        public string DealExitDate
        {
            get
            {
                if (GetValue("DealExitDate") != null && GetValue("DealExitDate") != "")
                {
                    return GetValue("DealExitDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("DealExitDate", value.ToString());
            }
        }

        public string TaskProcessState
        {
            get
            {
                if (GetValue("TaskProcessState") != null && GetValue("TaskProcessState") != "")
                {
                    return GetValue("TaskProcessState");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskProcessState", value.ToString());
            }
        }

        public string TaskTimeOutMemo
        {
            get
            {
                if (GetValue("TaskTimeOutMemo") != null && GetValue("TaskTimeOutMemo") != "")
                {
                    return GetValue("TaskTimeOutMemo");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskTimeOutMemo", value.ToString());
            }
        }

        public string TaskCompleteDate
        {
            get
            {
                if (GetValue("TaskCompleteDate") != null && GetValue("TaskCompleteDate") != "")
                {
                    return GetValue("TaskCompleteDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskCompleteDate", value.ToString());
            }
        }

        public string TaskDealDocs
        {
            get
            {
                if (GetValue("TaskDealDocs") != null && GetValue("TaskDealDocs") != "")
                {
                    return GetValue("TaskDealDocs");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskDealDocs", value.ToString());
            }
        }

        public string CompleteDate
        {
            get
            {
                if (GetValue("CompleteDate") != null && GetValue("CompleteDate") != "")
                {
                    return GetValue("CompleteDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("CompleteDate", value.ToString());
            }
        }

        public string CloseType
        {
            get
            {
                if (GetValue("CloseType") != null && GetValue("CloseType") != "")
                {
                    return GetValue("CloseType");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("CloseType", value.ToString());
            }
        }

        public string CloseMemo
        {
            get
            {
                if (GetValue("CloseMemo") != null && GetValue("CloseMemo") != "")
                {
                    return GetValue("CloseMemo");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("CloseMemo", value.ToString());
            }
        }

        public string IsDesign
        {
            get
            {
                if (GetValue("IsDesign") != null && GetValue("IsDesign") != "")
                {
                    return GetValue("IsDesign");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("IsDesign", value.ToString());
            }
        }

        public string ServiceEvaluation
        {
            get
            {
                if (GetValue("ServiceEvaluation") != null && GetValue("ServiceEvaluation") != "")
                {
                    return GetValue("ServiceEvaluation");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ServiceEvaluation", value.ToString());
            }
        }

        public string MaterialEvaluation
        {
            get
            {
                if (GetValue("MaterialEvaluation") != null && GetValue("MaterialEvaluation") != "")
                {
                    return GetValue("MaterialEvaluation");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("MaterialEvaluation", value.ToString());
            }
        }

        public string ComplaintNature
        {
            get
            {
                if (GetValue("ComplaintNature") != null && GetValue("ComplaintNature") != "")
                {
                    return GetValue("ComplaintNature");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ComplaintNature", value.ToString());
            }
        }

        public string CustView
        {
            get
            {
                if (GetValue("CustView") != null && GetValue("CustView") != "")
                {
                    return GetValue("CustView");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("CustView", value.ToString());
            }
        }

        public string IsCharge
        {
            get
            {
                if (GetValue("IsCharge") != null && GetValue("IsCharge") != "")
                {
                    return GetValue("IsCharge");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("IsCharge", value.ToString());
            }
        }

        public string CompleteCancelReason
        {
            get
            {
                if (GetValue("CompleteCancelReason") != null && GetValue("CompleteCancelReason") != "")
                {
                    return GetValue("CompleteCancelReason");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("CompleteCancelReason", value.ToString());
            }
        }

        public string CompleteCancelDate
        {
            get
            {
                if (GetValue("CompleteCancelDate") != null && GetValue("CompleteCancelDate") != "")
                {
                    return GetValue("CompleteCancelDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("CompleteCancelDate", value.ToString());
            }
        }

        public string NetWorkAddr
        {
            get
            {
                if (GetValue("NetWorkAddr") != null && GetValue("NetWorkAddr") != "")
                {
                    return GetValue("NetWorkAddr");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("NetWorkAddr", value.ToString());
            }
        }

        public string AnswerContent
        {
            get
            {
                if (GetValue("AnswerContent") != null && GetValue("AnswerContent") != "")
                {
                    return GetValue("AnswerContent");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("AnswerContent", value.ToString());
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
        public string TaskDate
        {
            get
            {
                if (GetValue("TaskDate") != null && GetValue("TaskDate").Trim() != "")
                {
                    return GetValue("TaskDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskDate", value);
            }
        }

        public string OprUser
        {
            get
            {
                if (GetValue("OprUser") != null && GetValue("OprUser").Trim() != "")
                {
                    return GetValue("OprUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("OprUser", value);
            }
        }

        public string DealSituation
        {
            get
            {
                if (GetValue("DealSituation") != null && GetValue("DealSituation").Trim() != "")
                {
                    return GetValue("DealSituation");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("DealSituation", value);
            }
        }

        public string DealDate
        {
            get
            {
                if (GetValue("DealDate") != null && GetValue("DealDate").Trim() != "")
                {
                    return GetValue("DealDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("DealDate", value);
            }
        }

        public string IsNotice
        {
            get
            {
                if (GetValue("IsNotice") != null && GetValue("IsNotice").Trim() != "")
                {
                    return GetValue("IsNotice");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("IsNotice", value);
            }
        }

        public string IsComplete
        {
            get
            {
                if (GetValue("IsComplete") != null && GetValue("IsComplete").Trim() != "")
                {
                    return GetValue("IsComplete");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("IsComplete", value);
            }
        }
        public int IsComplaint
        {
            get
            {
                if (GetValue("IsComplaint") != null && GetValue("IsComplaint") != "")
                {
                    return Convert.ToInt32(GetValue("IsComplaint").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsComplaint", value.ToString());
            }
        }

        public string TaskIsComplaint
        {
            get
            {
                if (GetValue("TaskIsComplaint") != null && GetValue("TaskIsComplaint") != "")
                {
                    return GetValue("TaskIsComplaint");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskIsComplaint", value.ToString());
            }
        }

        public string TaskComplaintNature
        {
            get
            {
                if (GetValue("TaskComplaintNature") != null && GetValue("TaskComplaintNature") != "")
                {
                    return GetValue("TaskComplaintNature");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskComplaintNature", value.ToString());
            }
        }

        public string TaskComplaintLevel
        {
            get
            {
                if (GetValue("TaskComplaintLevel") != null && GetValue("TaskComplaintLevel") != "")
                {
                    return GetValue("TaskComplaintLevel");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("TaskComplaintLevel", value.ToString());
            }
        }

        public string MtGuid
        {
            get
            {
                if (GetValue("MtGuid") != null && GetValue("MtGuid") != "")
                {
                    return GetValue("MtGuid");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("MtGuid", value.ToString());
            }
        }


        public int MTSendType
        {
            get
            {
                if (GetValue("MTSendType") != null && GetValue("MTSendType") != "")
                {
                    return Convert.ToInt32(GetValue("MTSendType").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("MTSendType", value.ToString());
            }
        }

        public string MTNum
        {
            get
            {
                if (GetValue("MTNum ") != null && GetValue("MTNum ") != "")
                {
                    return GetValue("MTNum ");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("MTNum ", value.ToString());
            }
        }
        public int MtTaskDutyType
        {
            get
            {
                if (GetValue("MtTaskDutyType") != null && GetValue("MtTaskDutyType") != "")
                {
                    return Convert.ToInt32(GetValue("MtTaskDutyType").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("MtTaskDutyType", value.ToString());
            }
        }
    }
}
