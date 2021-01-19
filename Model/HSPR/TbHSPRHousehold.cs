using KernelDev.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.HSPR
{
    [Serializable()]
    public class TbHSPRHousehold : ExtensibleObjectBase
    {
        public TbHSPRHousehold()
        {

        }
        public TbHSPRHousehold(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            Deserialize(info, context);
        }

        public Int64 HoldID
        {
            get
            {
                if (GetValue("HoldID") != null && GetValue("HoldID") != "")
                {
                    return Convert.ToInt64(GetValue("HoldID").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("HoldID", value.ToString());
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

        public string Surname
        {
            get
            {
                if (GetValue("Surname") != null && GetValue("Surname") != "")
                {
                    return GetValue("Surname");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Surname", value);
            }
        }

        public string Name
        {
            get
            {
                if (GetValue("Name") != null && GetValue("Name") != "")
                {
                    return GetValue("Name");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Name", value);
            }
        }

        public string Sex
        {
            get
            {
                if (GetValue("Sex") != null && GetValue("Sex") != "")
                {
                    return GetValue("Sex");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Sex", value);
            }
        }

        public string Birthday
        {
            get
            {
                if (GetValue("Birthday") != null && GetValue("Birthday") != "")
                {
                    return GetValue("Birthday");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Birthday", value.ToString());
            }
        }

        public string Nationality
        {
            get
            {
                if (GetValue("Nationality") != null && GetValue("Nationality") != "")
                {
                    return GetValue("Nationality");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Nationality", value);
            }
        }

        public string WorkUnit
        {
            get
            {
                if (GetValue("WorkUnit") != null && GetValue("WorkUnit") != "")
                {
                    return GetValue("WorkUnit");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("WorkUnit", value);
            }
        }

        public string PaperName
        {
            get
            {
                if (GetValue("PaperName") != null && GetValue("PaperName") != "")
                {
                    return GetValue("PaperName");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("PaperName", value);
            }
        }

        public string PaperCode
        {
            get
            {
                if (GetValue("PaperCode") != null && GetValue("PaperCode") != "")
                {
                    return GetValue("PaperCode");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("PaperCode", value);
            }
        }

        public string PassSign
        {
            get
            {
                if (GetValue("PassSign") != null && GetValue("PassSign") != "")
                {
                    return GetValue("PassSign");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("PassSign", value);
            }
        }

        public string MobilePhone
        {
            get
            {
                if (GetValue("MobilePhone") != null && GetValue("MobilePhone") != "")
                {
                    return GetValue("MobilePhone");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("MobilePhone", value);
            }
        }

        public string Relationship
        {
            get
            {
                if (GetValue("Relationship") != null && GetValue("Relationship") != "")
                {
                    return GetValue("Relationship");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Relationship", value);
            }
        }



        public string StayTime
        {
            get
            {
                if (GetValue("StayTime") != null && GetValue("StayTime") != "")
                {
                    return GetValue("StayTime");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("StayTime", value.ToString());
            }
        }

        public string ChargeCause
        {
            get
            {
                if (GetValue("ChargeCause") != null && GetValue("ChargeCause") != "")
                {
                    return GetValue("ChargeCause");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ChargeCause", value);
            }
        }

        public string ChargeTime
        {
            get
            {
                if (GetValue("ChargeTime") != null && GetValue("ChargeTime") != "")
                {
                    return GetValue("ChargeTime");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("ChargeTime", value.ToString());
            }
        }

        public string InquirePwd
        {
            get
            {
                if (GetValue("InquirePwd") != null && GetValue("InquirePwd") != "")
                {
                    return GetValue("InquirePwd");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("InquirePwd", value);
            }
        }

        public string InquireAccount
        {
            get
            {
                if (GetValue("InquireAccount") != null && GetValue("InquireAccount") != "")
                {
                    return GetValue("InquireAccount");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("InquireAccount", value);
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


        public Int64 MemberCode
        {
            get
            {
                if (GetValue("MemberCode") != null && GetValue("MemberCode") != "")
                {
                    return Convert.ToInt64(GetValue("MemberCode").ToString());
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                SetValue("MemberCode", value.ToString());
            }
        }

        public string MemberName
        {
            get
            {
                if (GetValue("MemberName") != null && GetValue("MemberName") != "")
                {
                    return GetValue("MemberName");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("MemberName", value);
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

        public string Job
        {
            get
            {
                if (GetValue("Job") != null && GetValue("Job") != "")
                {
                    return GetValue("Job");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Job", value);
            }
        }

        public string Linkman
        {
            get
            {
                if (GetValue("Linkman") != null && GetValue("Linkman") != "")
                {
                    return GetValue("Linkman");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("Linkman", value);
            }
        }

        public string LinkManTel
        {
            get
            {
                if (GetValue("LinkManTel") != null && GetValue("LinkManTel") != "")
                {
                    return GetValue("LinkManTel");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("LinkManTel", value);
            }
        }

        public string FixedTel
        {
            get
            {
                if (GetValue("FixedTel") != null && GetValue("FixedTel") != "")
                {
                    return GetValue("FixedTel");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("FixedTel", value);
            }
        }
        public string PaperCodeAddress
        {
            get
            {
                if (GetValue("PaperCodeAddress") != null && GetValue("PaperCodeAddress") != "")
                {
                    return GetValue("PaperCodeAddress");
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetValue("FixedTel", value);
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
