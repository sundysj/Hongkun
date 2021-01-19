using KernelDev.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HSPR
{
    public class TbHSPRFees : ExtensibleObjectBase
    {
        public TbHSPRFees()
        {

        }
        public TbHSPRFees(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Deserialize(info, context);
        }

        public Int64 FeesID
        {
            get
            {
                if (GetValue("FeesID") != null && GetValue("FeesID") != "")
                {
                    return Convert.ToInt64(GetValue("FeesID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("FeesID", value.ToString());
            }
        }

        public int CommID
        {
            get
            {
                if (GetValue("CommID") != null && GetValue("CommID") != "")
                {
                    return Convert.ToInt32(GetValue("CommID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("CommID", value.ToString());
            }
        }

        public Int64 CostID
        {
            get
            {
                if (GetValue("CostID") != null && GetValue("CostID") != "")
                {
                    return Convert.ToInt64(GetValue("CostID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("CostID", value.ToString());
            }
        }

        public Int64 CustID
        {
            get
            {
                if (GetValue("CustID") != null && GetValue("CustID") != "")
                {
                    return Convert.ToInt64(GetValue("CustID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("CustID", value.ToString());
            }
        }

        public Int64 RoomID
        {
            get
            {
                if (GetValue("RoomID") != null && GetValue("RoomID") != "")
                {
                    return Convert.ToInt64(GetValue("RoomID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("RoomID", value.ToString());
            }
        }

        public string FeesDueDate
        {
            get
            {
                if (GetValue("FeesDueDate") != null && GetValue("FeesDueDate") != "")
                {
                    return GetValue("FeesDueDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("FeesDueDate", value.ToString());
            }
        }

        public string FeesStateDate
        {
            get
            {
                if (GetValue("FeesStateDate") != null && GetValue("FeesStateDate") != "")
                {
                    return GetValue("FeesStateDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("FeesStateDate", value.ToString());
            }
        }

        public string FeesEndDate
        {
            get
            {
                if (GetValue("FeesEndDate") != null && GetValue("FeesEndDate") != "")
                {
                    return GetValue("FeesEndDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("FeesEndDate", value.ToString());
            }
        }

        public Decimal DueAmount
        {
            get
            {
                if (GetValue("DueAmount") != null && GetValue("DueAmount") != "")
                {
                    return Convert.ToDecimal(GetValue("DueAmount"));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("DueAmount", value.ToString());
            }
        }

        public Decimal DebtsAmount
        {
            get
            {
                if (GetValue("DebtsAmount") != null && GetValue("DebtsAmount") != "")
                {
                    return Convert.ToDecimal(GetValue("DebtsAmount"));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("DebtsAmount", value.ToString());
            }
        }

        public Decimal WaivAmount
        {
            get
            {
                if (GetValue("WaivAmount") != null && GetValue("WaivAmount") != "")
                {
                    return Convert.ToDecimal(GetValue("WaivAmount"));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("WaivAmount", value.ToString());
            }
        }

        public Decimal PrecAmount
        {
            get
            {
                if (GetValue("PrecAmount") != null && GetValue("PrecAmount") != "")
                {
                    return Convert.ToDecimal(GetValue("PrecAmount"));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("PrecAmount", value.ToString());
            }
        }

        public Decimal PaidAmount
        {
            get
            {
                if (GetValue("PaidAmount") != null && GetValue("PaidAmount") != "")
                {
                    return Convert.ToDecimal(GetValue("PaidAmount"));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("PaidAmount", value.ToString());
            }
        }

        public Decimal RefundAmount
        {
            get
            {
                if (GetValue("RefundAmount") != null && GetValue("RefundAmount") != "")
                {
                    return Convert.ToDecimal(GetValue("RefundAmount"));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("RefundAmount", value.ToString());
            }
        }

        public int IsAudit
        {
            get
            {
                if (GetValue("IsAudit") != null && GetValue("IsAudit") != "")
                {
                    return Convert.ToInt32(GetValue("IsAudit").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsAudit", value.ToString());
            }
        }

        public string FeesMemo
        {
            get
            {
                if (GetValue("FeesMemo") != null && GetValue("FeesMemo") != "")
                {
                    return GetValue("FeesMemo");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("FeesMemo", value);
            }
        }

        public int AccountFlag
        {
            get
            {
                if (GetValue("AccountFlag") != null && GetValue("AccountFlag") != "")
                {
                    return Convert.ToInt32(GetValue("AccountFlag").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("AccountFlag", value.ToString());
            }
        }

        public int IsBank
        {
            get
            {
                if (GetValue("IsBank") != null && GetValue("IsBank") != "")
                {
                    return Convert.ToInt32(GetValue("IsBank").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsBank", value.ToString());
            }
        }

        public int IsCharge
        {
            get
            {
                if (GetValue("IsCharge") != null && GetValue("IsCharge") != "")
                {
                    return Convert.ToInt32(GetValue("IsCharge").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsCharge", value.ToString());
            }
        }

        public int IsFreeze
        {
            get
            {
                if (GetValue("IsFreeze") != null && GetValue("IsFreeze") != "")
                {
                    return Convert.ToInt32(GetValue("IsFreeze").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsFreeze", value.ToString());
            }
        }

        public int IsProperty
        {
            get
            {
                if (GetValue("IsProperty") != null && GetValue("IsProperty") != "")
                {
                    return Convert.ToInt32(GetValue("IsProperty").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsProperty", value.ToString());
            }
        }

        public Int64 CorpStanID
        {
            get
            {
                if (GetValue("CorpStanID") != null && GetValue("CorpStanID") != "")
                {
                    return Convert.ToInt64(GetValue("CorpStanID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("CorpStanID", value.ToString());
            }
        }

        public Int64 StanID
        {
            get
            {
                if (GetValue("StanID") != null && GetValue("StanID") != "")
                {
                    return Convert.ToInt64(GetValue("StanID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("StanID", value.ToString());
            }
        }

        public Int64 OwnerFeesID
        {
            get
            {
                if (GetValue("OwnerFeesID") != null && GetValue("OwnerFeesID") != "")
                {
                    return Convert.ToInt64(GetValue("OwnerFeesID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("OwnerFeesID", value.ToString());
            }
        }

        public string AccountsDueDate
        {
            get
            {
                if (GetValue("AccountsDueDate") != null && GetValue("AccountsDueDate") != "")
                {
                    return GetValue("AccountsDueDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("AccountsDueDate", value.ToString());
            }
        }

        public Int64 HandID
        {
            get
            {
                if (GetValue("HandID") != null && GetValue("HandID") != "")
                {
                    return Convert.ToInt64(GetValue("HandID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("HandID", value.ToString());
            }
        }

        public string MeterSign
        {
            get
            {
                if (GetValue("MeterSign") != null && GetValue("MeterSign") != "")
                {
                    return GetValue("MeterSign");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("MeterSign", value);
            }
        }

        public Decimal CalcAmount
        {
            get
            {
                if (GetValue("CalcAmount") != null && GetValue("CalcAmount") != "")
                {
                    return Convert.ToDecimal(GetValue("CalcAmount"));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("CalcAmount", value.ToString());
            }
        }

        public Decimal CalcAmount2
        {
            get
            {
                if (GetValue("CalcAmount2") != null && GetValue("CalcAmount2") != "")
                {
                    return Convert.ToDecimal(GetValue("CalcAmount2"));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("CalcAmount2", value.ToString());
            }
        }

        public Int64 IncidentID
        {
            get
            {
                if (GetValue("IncidentID") != null && GetValue("IncidentID") != "")
                {
                    return Convert.ToInt64(GetValue("IncidentID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IncidentID", value.ToString());
            }
        }

        public Int64 LeaseContID
        {
            get
            {
                if (GetValue("LeaseContID") != null && GetValue("LeaseContID") != "")
                {
                    return Convert.ToInt64(GetValue("LeaseContID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("LeaseContID", value.ToString());
            }
        }

        public Int64 ContID
        {
            get
            {
                if (GetValue("ContID") != null && GetValue("ContID") != "")
                {
                    return Convert.ToInt64(GetValue("ContID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("ContID", value.ToString());
            }
        }

        public Int64 PMeterID
        {
            get
            {
                if (GetValue("PMeterID") != null && GetValue("PMeterID") != "")
                {
                    return Convert.ToInt64(GetValue("PMeterID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("PMeterID", value.ToString());
            }
        }

        public string StanMemo
        {
            get
            {
                if (GetValue("StanMemo") != null && GetValue("StanMemo") != "")
                {
                    return GetValue("StanMemo");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("StanMemo", value);
            }
        }

        public string UserCode
        {
            get
            {
                if (GetValue("UserCode") != null && GetValue("UserCode") != "")
                {
                    return GetValue("UserCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("UserCode", value);
            }
        }

        public int IsSel
        {
            get
            {
                if (GetValue("IsSel") != null && GetValue("IsSel") != "")
                {
                    return Convert.ToInt32(GetValue("IsSel").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsSel", value.ToString());
            }
        }

        public Decimal ThisAmount
        {
            get
            {
                if (GetValue("ThisAmount") != null && GetValue("ThisAmount") != "")
                {
                    return Convert.ToDecimal(GetValue("ThisAmount"));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("ThisAmount", value.ToString());
            }
        }

        public Decimal LateFeeAmount
        {
            get
            {
                if (GetValue("LateFeeAmount") != null && GetValue("LateFeeAmount") != "")
                {
                    return Convert.ToDecimal(GetValue("LateFeeAmount").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("LateFeeAmount", value.ToString());
            }
        }

        public int IsLateFee
        {
            get
            {
                if (GetValue("IsLateFee") != null && GetValue("IsLateFee") != "")
                {
                    return Convert.ToInt32(GetValue("IsLateFee").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsLateFee", value.ToString());
            }
        }


        public string OrderCode
        {
            get
            {
                if (GetValue("OrderCode") != null && GetValue("OrderCode").Trim() != "")
                {
                    return GetValue("OrderCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("OrderCode", value);
            }
        }


        public int IsPast
        {
            get
            {
                if (GetValue("IsPast") != null && GetValue("IsPast") != "")
                {
                    return Convert.ToInt32(GetValue("IsPast").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsPast", value.ToString());
            }
        }

        public Int64 AssumeCustID
        {
            get
            {
                if (GetValue("AssumeCustID") != null && GetValue("AssumeCustID") != "")
                {
                    return Convert.ToInt64(GetValue("AssumeCustID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("AssumeCustID", value.ToString());
            }
        }

        public Int64 ContSetID
        {
            get
            {
                if (GetValue("ContSetID") != null && GetValue("ContSetID") != "")
                {
                    return Convert.ToInt64(GetValue("ContSetID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("ContSetID", value.ToString());
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
    }
}
