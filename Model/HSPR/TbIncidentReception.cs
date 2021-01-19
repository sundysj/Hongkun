using KernelDev.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HSPR
{
    [Serializable]
    public class TbIncidentReception : ExtensibleObjectBase
    {
        public TbIncidentReception()
        {

        }
        public TbIncidentReception(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Deserialize(info, context);
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

        public string ReceNum
        {
            get
            {
                if (GetValue("ReceNum") != null && GetValue("ReceNum") != "")
                {
                    return GetValue("ReceNum");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReceNum", value.ToString());
            }
        }

        public Int64 CustHoldID
        {
            get
            {
                if (GetValue("CustHoldID") != null && GetValue("CustHoldID") != "")
                {
                    return Convert.ToInt64(GetValue("CustHoldID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("CustHoldID", value.ToString());
            }
        }

        public string PostItUser
        {
            get
            {
                if (GetValue("PostItUser") != null && GetValue("PostItUser") != "")
                {
                    return GetValue("PostItUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("PostItUser", value.ToString());
            }
        }

        public string PostItMobile
        {
            get
            {
                if (GetValue("PostItMobile") != null && GetValue("PostItMobile") != "")
                {
                    return GetValue("PostItMobile");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("PostItMobile", value.ToString());
            }
        }

        public string PostItTel
        {
            get
            {
                if (GetValue("PostItTel") != null && GetValue("PostItTel") != "")
                {
                    return GetValue("PostItTel");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("PostItTel", value.ToString());
            }
        }

        public string ReceState
        {
            get
            {
                if (GetValue("ReceState") != null && GetValue("ReceState") != "")
                {
                    return GetValue("ReceState");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReceState", value.ToString());
            }
        }

        public string InfoFrom
        {
            get
            {
                if (GetValue("InfoFrom") != null && GetValue("InfoFrom") != "")
                {
                    return GetValue("InfoFrom");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("InfoFrom", value.ToString());
            }
        }

        public string RegUser
        {
            get
            {
                if (GetValue("RegUser") != null && GetValue("RegUser") != "")
                {
                    return GetValue("RegUser");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("RegUser", value.ToString());
            }
        }

        public string ReceDate
        {
            get
            {
                if (GetValue("ReceDate") != null && GetValue("ReceDate") != "")
                {
                    return GetValue("ReceDate");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReceDate", value.ToString());
            }
        }

        public string ReceMemo
        {
            get
            {
                if (GetValue("ReceMemo") != null && GetValue("ReceMemo") != "")
                {
                    return GetValue("ReceMemo");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReceMemo", value.ToString());
            }
        }

        public string ReceDocs
        {
            get
            {
                if (GetValue("ReceDocs") != null && GetValue("ReceDocs") != "")
                {
                    return GetValue("ReceDocs");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ReceDocs", value.ToString());
            }
        }

        public string IncidentFlow
        {
            get
            {
                if (GetValue("IncidentFlow") != null && GetValue("IncidentFlow") != "")
                {
                    return GetValue("IncidentFlow");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("IncidentFlow", value.ToString());
            }
        }
        public string Addr
        {
            get
            {
                if (GetValue("Addr") != null && GetValue("Addr") != "")
                {
                    return GetValue("Addr");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Addr", value.ToString());
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

        public int MTDutyType
        {
            get
            {
                if (GetValue("MTDutyType") != null && GetValue("MTDutyType") != "")
                {
                    return Convert.ToInt32(GetValue("MTDutyType").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("MTDutyType", value.ToString());
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
