using System;
using KernelDev.DataAccess;
using System.Runtime.Serialization;

namespace MobileSoft.Model.HSPR
{
    [Serializable()]
    public class Tb_HSPR_IncidentAccept_bl : Tb_HSPR_IncidentAccept_jh
    {

        public string DispRemarks
        {
            get;
            set;
        }

    }
}
