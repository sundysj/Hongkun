using KernelDev.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HSPR
{
    public class TbHSPRLeaseContractRenewal : ExtensibleObjectBase
    {
        public TbHSPRLeaseContractRenewal()
        {

        }
        public TbHSPRLeaseContractRenewal(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Deserialize(info, context);
        }

        public Int64 RenewalID
        {
            get
            {
                if (GetValue("RenewalID") != null && GetValue("RenewalID") != "")
                {
                    return Convert.ToInt64(GetValue("RenewalID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("RenewalID", value.ToString());
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

        public string RenewalDate
        {
            get
            {
                if (GetValue("RenewalDate") != null && GetValue("RenewalDate") != "")
                {
                    return GetValue("RenewalDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RenewalDate", value.ToString());
            }
        }

        public string ReletDate
        {
            get
            {
                if (GetValue("ReletDate") != null && GetValue("ReletDate") != "")
                {
                    return GetValue("ReletDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReletDate", value.ToString());
            }
        }

        public int IsFee
        {
            get
            {
                if (GetValue("IsFee") != null && GetValue("IsFee") != "")
                {
                    return Convert.ToInt32(GetValue("IsFee").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsFee", value.ToString());
            }
        }

        public string RenewalDetail
        {
            get
            {
                if (GetValue("RenewalDetail") != null && GetValue("RenewalDetail") != "")
                {
                    return GetValue("RenewalDetail");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RenewalDetail", value);
            }
        }

        public string AuditPer
        {
            get
            {
                if (GetValue("AuditPer") != null && GetValue("AuditPer") != "")
                {
                    return GetValue("AuditPer");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("AuditPer", value);
            }
        }

        public string ManagePer
        {
            get
            {
                if (GetValue("ManagePer") != null && GetValue("ManagePer") != "")
                {
                    return GetValue("ManagePer");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ManagePer", value);
            }
        }

        public string Memo
        {
            get
            {
                if (GetValue("Memo") != null && GetValue("Memo") != "")
                {
                    return GetValue("Memo");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Memo", value);
            }
        }

        public int IsDelete
        {
            get
            {
                if (GetValue("IsDelete") != null && GetValue("IsDelete") != "")
                {
                    return Convert.ToInt32(GetValue("IsDelete").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("IsDelete", value.ToString());
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
