using Common;
using MobileSoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.天问业主
{
    /// <summary>
    /// 客服系统专业版
    /// </summary>
    public class IncidentAcceptManage_Pro : PubInfo
    {
        public IncidentAcceptManage_Pro()
        {
            base.Token = "20180302IncidentAcceptManage_Pro";
        }

        public override void Operate(ref Transfer Trans)
        {
            Trans.Result = JSONHelper.FromString(false, "未知错误");

            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            switch (Trans.Command)
            {
                case "VisitorReception":
                    Trans.Result = VisitorReception(Row);   // 接待登记
                    break;
            }
        }
        

        /// <summary>
        /// 接待登记
        /// </summary>
        private string VisitorReception(DataRow row)
        {
            return null;
        }
    }
}
