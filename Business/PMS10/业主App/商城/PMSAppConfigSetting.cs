using Common;
using MobileSoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    class PMSAppConfigSetting : PubInfo
    {
        public PMSAppConfigSetting()
        {
            base.Token = "202008051534_PMSAppConfigSetting";
        }

        public override void Operate(ref Transfer Trans)
        {
            DataTable dAttributeTable = base.XmlToDatatTable(Trans.Attribute);
            DataRow Row = dAttributeTable.Rows[0];

            //防止未捕获异常出现
            try
            {
                switch (Trans.Command)
                {
                    case "GetBusinessOpenStatus":
                        Trans.Result = GetBusinessOpenStatus();
                        break;
                    default:
                        Trans.Result = new ApiResult(false, "未知错误").toJson();
                        break;
                }
            }
            catch (Exception ex)
            {
                GetLog().Error(ex.Message + Environment.CommandLine + ex.StackTrace + Environment.NewLine + ex.Source);
                Trans.Result = new ApiResult(false, ex.Message + ex.StackTrace).toJson();
            }
        }


        private String GetBusinessOpenStatus()
        {
            int result = 0;
            try
            {
                var openStatus = Global_Fun.AppWebSettings("Business_Open");
                if (!String.IsNullOrEmpty(openStatus))
                {
                    int.TryParse(openStatus, out result);
                }
            }
            catch (Exception ex)
            {
                //
            }
            return new ApiResult(true, result).toJson();
        }
    }
}
